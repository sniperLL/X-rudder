/* include --------------------------------------------------*/
#include "Init_PWM.h"

/**************************************************************************
**
**      Programs function: PWM Configuration
**      Functions name: Init_PWM(double Dutyfactor)
**
**************************************************************************/
/* Define parameters -------------------------------------------------------------*/

float DutyfactorSet_LEFT_UP = DutyfactorM;
float DutyfactorSet_RIGHT_UP = DutyfactorM;
float DutyfactorSet_LEFT_DOWN = DutyfactorM;
float DutyfactorSet_RIGHT_DOWN = DutyfactorM;
float DutyfactorSet_HORIZ_LEFT = DutyfactorM;
float DutyfactorSet_HORIZ_RIGHT = DutyfactorM;
float DutyfactorSet_MOTOR = DutyfactorMo;

void Init_PWM(float dutyfactor1, 
                          float dutyfactor2, 
                          float dutyfactor3, 
                          float dutyfactor4, 
                          float dutyfactor5, 
                          float dutyfactor6,
                          float dutyfactor7)
{
        //Define structures for initiation
        GPIO_InitTypeDef GPIO_InitStructure;
        TIM_TimeBaseInitTypeDef TIM_TimeBaseInitStructure;
        TIM_OCInitTypeDef TIM_OCInitStructure;
        
        //Enable clocks
        RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOD | RCC_APB2Periph_GPIOC | RCC_APB2Periph_AFIO, ENABLE);
        RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM4 | RCC_APB1Periph_TIM3, ENABLE);
        
        //Used I/O ports: PD12/13/14/15,PC6/7/8
        GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;
        GPIO_InitStructure.GPIO_Pin = GPIO_Pin_12 | GPIO_Pin_13 | GPIO_Pin_14 | GPIO_Pin_15;
        GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
        GPIO_Init(GPIOD, &GPIO_InitStructure);
        
        GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;
        GPIO_InitStructure.GPIO_Pin = GPIO_Pin_6 |  GPIO_Pin_7 |  GPIO_Pin_8 ;
        GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
        GPIO_Init(GPIOC, &GPIO_InitStructure);
	
				GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;          // �������ת����
        GPIO_InitStructure.GPIO_Pin = GPIO_Pin_9 |  GPIO_Pin_10;
        GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
        GPIO_Init(GPIOC, &GPIO_InitStructure);
        
        //Remap pins of timers
        GPIO_PinRemapConfig(GPIO_Remap_TIM4, ENABLE);    //Remap TIM4_Channel1/2/3/4 to PD12/13/14/15 
        GPIO_PinRemapConfig(GPIO_FullRemap_TIM3, ENABLE);    //FullRemap TIM3_Channel1/2/3 to PC6/7/8 
	
				//TIM4/TIM3 Initiation
        TIM_TimeBaseInitStructure.TIM_Period = 20000-1;      //Config auto-reload register's value, timing cycle is 20ms
        TIM_TimeBaseInitStructure.TIM_Prescaler = 72-1;    //���Զ���Ԥ��Ƶϵ��Ϊ0����ʱ����ʱ��Ƶ��Ϊ72M�ṩ����ʱ����ʱ��	0~65535֮��
        TIM_TimeBaseInitStructure.TIM_ClockDivision = TIM_CKD_DIV1;     //Clock division is 0
        TIM_TimeBaseInitStructure.TIM_CounterMode = TIM_CounterMode_Up;         ////TIM���ϼ���ģʽ ��0��ʼ���ϼ�����������1000����������¼�
        TIM_TimeBaseInit(TIM4, &TIM_TimeBaseInitStructure);
        
        TIM_TimeBaseInitStructure.TIM_Period = 20000-1;      //Config auto-reload register's value, timing cycle is 20ms
        TIM_TimeBaseInitStructure.TIM_Prescaler = 72-1;    //���Զ���Ԥ��Ƶϵ��Ϊ0����ʱ����ʱ��Ƶ��Ϊ72M�ṩ����ʱ����ʱ��	0~65535֮��
        TIM_TimeBaseInitStructure.TIM_ClockDivision = TIM_CKD_DIV1;     //Clock division is 0
        TIM_TimeBaseInitStructure.TIM_CounterMode = TIM_CounterMode_Up;         ////TIM���ϼ���ģʽ ��0��ʼ���ϼ�����������1000����������¼�
        TIM_TimeBaseInit(TIM3, &TIM_TimeBaseInitStructure);

        //Enable TIM4/TIM3
        TIM_Cmd(TIM4, ENABLE);
        TIM_Cmd(TIM3, ENABLE);
				
        //Output compare state parameters initiation
        TIM_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM1;
				TIM_OCInitStructure.TIM_Pulse = dutyfactor1;//Config duty cycle, duty cycle = (CCRx/ARR)*100% or (TIM_Pulse/TIM_Period)*100%
				TIM_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_High;       //TIM output compare polarity is high
				TIM_OCInitStructure.TIM_OutputState = TIM_OutputState_Enable;
				TIM_OC1Init(TIM4, &TIM_OCInitStructure);
        
				TIM_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM1;
				TIM_OCInitStructure.TIM_Pulse = dutyfactor2;//Config duty cycle, duty cycle = (CCRx/ARR)*100% or (TIM_Pulse/TIM_Period)*100%
				TIM_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_High;       //TIM output compare polarity is high
				TIM_OCInitStructure.TIM_OutputState = TIM_OutputState_Enable;
				TIM_OC2Init(TIM4, &TIM_OCInitStructure);
        
        TIM_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM1;
				TIM_OCInitStructure.TIM_Pulse = dutyfactor3;//Config duty cycle, duty cycle = (CCRx/ARR)*100% or (TIM_Pulse/TIM_Period)*100%
				TIM_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_High;       //TIM output compare polarity is high
				TIM_OCInitStructure.TIM_OutputState = TIM_OutputState_Enable;
				TIM_OC3Init(TIM4, &TIM_OCInitStructure);
        
        TIM_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM1;
				TIM_OCInitStructure.TIM_Pulse = dutyfactor4;//Config duty cycle, duty cycle = (CCRx/ARR)*100% or (TIM_Pulse/TIM_Period)*100%
				TIM_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_High;       //TIM output compare polarity is high
				TIM_OCInitStructure.TIM_OutputState = TIM_OutputState_Enable;
				TIM_OC4Init(TIM4, &TIM_OCInitStructure);
        
        TIM_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM1;
				TIM_OCInitStructure.TIM_Pulse = dutyfactor5;//Config duty cycle, duty cycle = (CCRx/ARR)*100% or (TIM_Pulse/TIM_Period)*100%
				TIM_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_High;       //TIM output compare polarity is high
				TIM_OCInitStructure.TIM_OutputState = TIM_OutputState_Enable;
				TIM_OC1Init(TIM3, &TIM_OCInitStructure);
        
        TIM_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM1;
				TIM_OCInitStructure.TIM_Pulse = dutyfactor6;//Config duty cycle, duty cycle = (CCRx/ARR)*100% or (TIM_Pulse/TIM_Period)*100%
				TIM_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_High;       //TIM output compare polarity is high
				TIM_OCInitStructure.TIM_OutputState = TIM_OutputState_Enable;
				TIM_OC2Init(TIM3, &TIM_OCInitStructure);
        
        TIM_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM1;
				TIM_OCInitStructure.TIM_Pulse = dutyfactor7 * ( 20000 / 1000 );//Config duty cycle, duty cycle = (CCRx/ARR)*100% or (TIM_Pulse/TIM_Period)*100%
				TIM_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_High;       //TIM output compare polarity is high
				TIM_OCInitStructure.TIM_OutputState = TIM_OutputState_Enable;
				TIM_OC3Init(TIM3, &TIM_OCInitStructure);				
}




