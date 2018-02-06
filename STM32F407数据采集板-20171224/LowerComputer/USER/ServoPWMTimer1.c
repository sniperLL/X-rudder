#include "stm32f4xx_conf.h"
#include "string.h"
#include "delay.h"

#define ENCODERTBSIZE 8
#define ENCODERRBSIZE 20
#define UPPERTBSIZE 37
#define INSRBSIZE 200
#define MPU6050_RX_BUF 33

extern uint8_t data[8];
extern uint8_t encoderTxBuffer[ENCODERTBSIZE];
extern uint8_t encoderRxBuffer[ENCODERRBSIZE];
extern uint8_t upperTxBuffer[UPPERTBSIZE];
extern uint8_t insRxBuffer[INSRBSIZE];
extern uint8_t  MPU6050ReceiveBuff[MPU6050_RX_BUF];
extern u16 ADCConvertedValue;

uint8_t allChannelData[UPPERTBSIZE] = {0x00};
uint8_t channelZeroToThreeData[ENCODERRBSIZE] = {0x00};
uint8_t encoderRxBufferClear[ENCODERRBSIZE] = {0x00};

TIM_TimeBaseInitTypeDef TIM_TimeBaseInitStrecture;
extern NVIC_InitTypeDef NVIC_InitStructure;
extern GPIO_InitTypeDef GPIO_InitStructure;


void ddelay_ms(unsigned int ms)                        // ��ʱ�ӳ���
{    
  unsigned int a,b; 
  for(a=ms;a>0;a--)
  for(b=123;b>0;b--);
}

/****************************************************
*�� �� ��:ServoPWMTimer1BaseInit
*��������:��ʼ����ʱ��1 ��ʱ����1ms
*��    ��:��
*�� �� ֵ:��
****************************************************/
void ServoPWMTimer1BaseInit(void)
{
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_TIM1,ENABLE);

	NVIC_InitStructure.NVIC_IRQChannel = TIM1_UP_TIM10_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 1;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0;
	NVIC_Init(&NVIC_InitStructure);

	TIM_TimeBaseInitStrecture.TIM_Period = 10000 - 1;
	TIM_TimeBaseInitStrecture.TIM_Prescaler = 1680 - 1;
	TIM_TimeBaseInitStrecture.TIM_ClockDivision = TIM_CKD_DIV1;
	TIM_TimeBaseInitStrecture.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBaseInitStrecture.TIM_RepetitionCounter = 0;
	TIM_TimeBaseInit(TIM1,&TIM_TimeBaseInitStrecture);

	TIM_ClearFlag(TIM1,TIM_FLAG_Update);
	TIM_ITConfig(TIM1,TIM_IT_Update,ENABLE);
	TIM_Cmd(TIM1,ENABLE);
}

/****************************************************
*�� �� ��:TIM1_UP_TIM10_IRQHandler
*��������:��ʱ��1���жϷ����� ����PWM
*��    ��:��
*�� �� ֵ:��
*****************************************************/
static unsigned int num;

void TIM1_UP_TIM10_IRQHandler(void)
{

	TIM_ClearFlag(TIM1,TIM_FLAG_Update);
	
	//MPU6050 ����
	if(MPU6050ReceiveBuff[0] == 0x55 && MPU6050ReceiveBuff[1] == 0x51)
	{
		for(int i = 0;i < 33 ;i++)
			allChannelData[i] = MPU6050ReceiveBuff[i];
	}
	GetDepth();
	memcpy(upperTxBuffer,allChannelData,UPPERTBSIZE);
	DMA_Cmd(DMA1_Stream6 ,ENABLE);
}

void ServoPwmPinInit(void)
{
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOF, ENABLE);
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOC, ENABLE);
	
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_9;
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_OUT;
  GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
  GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_NOPULL;
  GPIO_Init(GPIOF, &GPIO_InitStructure);
	GPIO_SetBits(GPIOF,GPIO_Pin_9);//��ʼ�øߵ�λ
	
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_0;
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_OUT;
  GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
  GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_NOPULL;
  GPIO_Init(GPIOC, &GPIO_InitStructure);
	GPIO_SetBits(GPIOC,GPIO_Pin_9);//��ʼ�øߵ�λ
}
