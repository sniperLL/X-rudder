/* include -------------------------------------------------------------*/

#include "stm32f10x.h"
#include "Init_PWM.h"
#include "Delay_ms.h"
#include "Init_LED.h"
#include "Init_USART.h"
#include "Init_TIMER.h"
#include "dma.h"
#include "Ctrl_RUDDER.h"
#include "ADC.h"
#include "pid.h"

typedef union
{
	float angleset_float;
	u8 angleset_u8[4];
}angleset;

//PID控制电机的变量以及函数
//脉冲计数
u16 pulseCount = 0;
u16 pulseCountPre = 0;
//void PulseGpioConfig(void);
void PulseRccConfig(void);
void PulseTimConfig(void);
void UpperDataAnalyze(void);//u8 upperData[17]);
//产生PWM脉冲
void PwmGpioConfig(void);
void PwmPd13(void);
void MotorGpioConfig(void);
void Delay_ms(u16 time);
//电机转速
float motorSpeed = 0.0;   //电机实际速度
//定时获取脉冲数
void Tim1Init(void);
void NvicConfig(void);
//电机速度值转换为十六进制
typedef union
{
	float motorSpeedFloat;
	u8 motorSpeedHex[4];
}motorSpeedRealUn;
//串口数据
//#define rxBuffSize 17
//#define txBuffSize 4
//extern u8 usart3DmaTxBuf[rxBuffSize];
//extern u8 usart1DmaRxBuf[txBuffSize];
//pid参数
extern struct _pid 
{
	float setSpeed;
	float actualSpeed;
	float err;
	float err_next;
	float err_last;
	float kp,ki,kd;
} pid;
extern float setSpeed;  //上位机指定速度
extern float actualSpeed;
extern float actualSpeedPwm;
extern float err;
extern float err_next;
extern float kp,ki,kd;
extern float err_last;
/**********************************************************************************************************
**
** Programs function：Lower computer control steering engines 
**                                      and datas reception and transmission through USART1/2 DMA
**
**********************************************************************************************************/
/* define --------------------------------------------------------------*/

/* Define parameters-------------------------------------------------------------*/
//extern u8 Uart1_Buffer[UART1_BUF_SIZE];     //Receive data buffer size
extern u8 UART3_Buffer[UART3_BUF_SIZE];
extern u8 Uart2_Buffer[UART2_BUF_SIZE];     //Send data buffer size
extern u8 ReceiveBuf[UART2_BUF_SIZE];	//Receive data temp buffer area
extern u8 LastReceiveBuf[UART2_BUF_SIZE];
//extern u8 SendBuf[UART3_BUF_SIZE];           //Send data temp buffer area
extern float DutyfactorSet_LEFT_UP;
extern float DutyfactorSet_RIGHT_UP;
extern float DutyfactorSet_LEFT_DOWN;
extern float DutyfactorSet_RIGHT_DOWN;
extern float DutyfactorSet_HORIZ_LEFT;
extern float DutyfactorSet_HORIZ_RIGHT;
extern float DutyfactorSet_MOTOR;
float AngelSet_LEFT_UP_Buf = AngelSet_LEFT_UP_RESET;
float AngelSet_RIGHT_UP_Buf = AngelSet_RIGHT_UP_RESET;
float AngelSet_LEFT_DOWN_Buf = AngelSet_LEFT_DOWN_RESET;
float AngelSet_RIGHT_DOWN_Buf = AngelSet_RIGHT_DOWN_RESET;
float AngelSet_HORIZ_LEFT_Buf = AngelSet_HORIZ_LEFT_RESET;
float AngelSet_HORIZ_RIGHT_Buf = AngelSet_HORIZ_RIGHT_RESET;
float SpeedSet_MOTOR_Buf = SpeedSet_MOTOR_RESET;
extern u8 Warning_Flag;       //Safe:0x00; Danger:0xFF
angleset _LEFT_UP;
angleset _RIGHT_UP;
angleset _LEFT_DOWN;
angleset _RIGHT_DOWN;
angleset _HORIZ_LEFT;
angleset _HORIZ_RIGHT;
angleset _MOTOR;
/**************************************************************************
**
** Functions name：main()
**
**************************************************************************/

int main()
{
	SystemInit();
	Init_LED();
	Init_PWM(DutyfactorSet_LEFT_UP, 
                          DutyfactorSet_RIGHT_UP, 
                          DutyfactorSet_LEFT_DOWN, 
                          DutyfactorSet_RIGHT_DOWN, 
                          DutyfactorSet_HORIZ_LEFT, 
                          DutyfactorSet_HORIZ_RIGHT,
                          DutyfactorSet_MOTOR);
//	Init_USART1();
	Init_USART2();		// 与上位机通讯串口DMA接收   
//	Init_USART3();
	Init_TIMER2();
	NvicConfig();
//	PulseGpioConfig();
	PulseRccConfig();
	PulseTimConfig();
	Tim1Init();
//	Init_ADC1();    // 进水报警的信号采集
        
/*------------------------------- USART1 DMA Configuration ------------------------------------*/
////DMA1_Channel5-----Receive from USART1 to Uart1_Buffer[UART1_BUF_SIZE] by circular mode
//MYDMA_Config(DMA1_Channel5, (u32)&USART1->DR, (u32)Uart1_Buffer, UART1_BUF_SIZE, DMA_DIR_PeripheralSRC, DMA_Mode_Circular);
//USART_DMACmd(USART1, USART_DMAReq_Rx, ENABLE);          //Enable USART1 DMA reception
//MYDMA_Enable(DMA1_Channel5);            //Start DMA reception
	
/*------------------------------- USART2 DMA Configuration ------------------------------------*/
	//DMA1_Channel6-----Receive from USART2 to Uart1_Buffer[UART2_BUF_SIZE] by circular mode
	MYDMA_Config(DMA1_Channel6, (u32)&USART2->DR, (u32)Uart2_Buffer, UART2_BUF_SIZE, DMA_DIR_PeripheralSRC, DMA_Mode_Circular);
	USART_DMACmd(USART2, USART_DMAReq_Rx, ENABLE);          //Enable USART2 DMA reception
	MYDMA_Enable(DMA1_Channel6);            //Start DMA reception
        
	GPIO_SetBits(GPIOG, GPIO_Pin_14);
	
	GPIO_SetBits(GPIOC, GPIO_Pin_9);
	GPIO_ResetBits(GPIOC, GPIO_Pin_10);
//	TIM_SetCompare3(TIM3, (0.0 * 20000 / 1000));
	while(1)
	{
                if (DMA_GetFlagStatus(DMA1_FLAG_TC6) != RESET)  //Judge if Channel6 complete reception
                {
                        int i = 0;
												int j = 0;
                        DMA_ClearFlag(DMA1_FLAG_TC6);   //Clear Channel6 complete reception flag
                        for (i = 0; i < UART2_BUF_SIZE; i++)
                                        ReceiveBuf[i] = Uart2_Buffer[i];
                        if (ReceiveBuf[0] == 0x5A && ReceiveBuf[1] == 0x01 && ReceiveBuf[31] == 0x33)  // 判断数据是否准确
                        {
                                for (i = 0; i < UART2_BUF_SIZE; i++)
                                        LastReceiveBuf[i] = ReceiveBuf[i]; 
                        }
                        else    // 如果不准确则使用上一次数据控制电机舵机
                        {
                                for (i = 0; i < UART2_BUF_SIZE; i++)
                                        ReceiveBuf[i] = LastReceiveBuf[i];
                        }
												
												for(i = 0, j = 2; i < 4; i++,j++)
												{
													_LEFT_UP.angleset_u8[i] = ReceiveBuf[j];
												}
												AngelSet_LEFT_UP_Buf = _LEFT_UP.angleset_float;
												for(i = 0, j = 6; i < 4; i++,j++)
												{
													_RIGHT_UP.angleset_u8[i] = ReceiveBuf[j];
												}
												AngelSet_RIGHT_UP_Buf = _RIGHT_UP.angleset_float;
												for(i = 0, j = 10; i < 4; i++,j++)
												{
													_LEFT_DOWN.angleset_u8[i] = ReceiveBuf[j];
												}
												AngelSet_LEFT_DOWN_Buf = _LEFT_DOWN.angleset_float;
												for(i = 0, j = 14; i < 4; i++,j++)
												{
													_RIGHT_DOWN.angleset_u8[i] = ReceiveBuf[j];
												}
												AngelSet_RIGHT_DOWN_Buf = _RIGHT_DOWN.angleset_float;
												for(i = 0, j = 18; i < 4; i++,j++)
												{
													_HORIZ_LEFT.angleset_u8[i] = ReceiveBuf[j];
												}
												AngelSet_HORIZ_LEFT_Buf = _HORIZ_LEFT.angleset_float;
												for(i = 0, j = 22; i < 4; i++,j++)
												{
													_HORIZ_RIGHT.angleset_u8[i] = ReceiveBuf[j];
												}
												AngelSet_HORIZ_RIGHT_Buf = _HORIZ_RIGHT.angleset_float;
												for(i = 0, j = 26; i < 4; i++,j++)
												{
													_MOTOR.angleset_u8[i] = ReceiveBuf[j];
												}
												if(ReceiveBuf[32] == 0xFF)
												{
													GPIO_SetBits(GPIOC, GPIO_Pin_10);
													GPIO_ResetBits(GPIOC, GPIO_Pin_9);
												}
												else
												{
													GPIO_SetBits(GPIOC, GPIO_Pin_9);
													GPIO_ResetBits(GPIOC, GPIO_Pin_10);
												}
//												SpeedSet_MOTOR_Buf = _MOTOR.angleset_float; //获取上位机给的速度给PWM脉宽   非PID控制
												setSpeed = _MOTOR.angleset_float;
												
//												setSpeed = 60;
//                        AngelSet_LEFT_UP_Buf = ReceiveBuf[2] << (8*3) | ReceiveBuf[3] << (8*2) | ReceiveBuf[4] << (8*1) | ReceiveBuf[5]; 
//                        AngelSet_RIGHT_UP_Buf = ReceiveBuf[6] << (8*3) | ReceiveBuf[7] << (8*2) | ReceiveBuf[8] << (8*1) | ReceiveBuf[9]; 
//                        AngelSet_LEFT_DOWN_Buf = ReceiveBuf[10] << (8*3) | ReceiveBuf[11] << (8*2) | ReceiveBuf[12] << (8*1) | ReceiveBuf[13]; 
//                        AngelSet_RIGHT_DOWN_Buf = ReceiveBuf[14] << (8*3) | ReceiveBuf[15] << (8*2) | ReceiveBuf[16] << (8*1) | ReceiveBuf[17]; 
//                        AngelSet_HORIZ_LEFT_Buf = ReceiveBuf[18] << (8*3) | ReceiveBuf[19] << (8*2) | ReceiveBuf[20] << (8*1) | ReceiveBuf[21]; 
//                        AngelSet_HORIZ_RIGHT_Buf = ReceiveBuf[22] << (8*3) | ReceiveBuf[23] << (8*2) | ReceiveBuf[24] << (8*1) | ReceiveBuf[25]; 
//                        SpeedSet_MOTOR_Buf = ReceiveBuf[26] << (8*3) | ReceiveBuf[27] << (8*2) | ReceiveBuf[28] << (8*1) | ReceiveBuf[29]; 
//                        UART3_Buffer[41] = Warning_Flag;
//                        UART3_Buffer[42] = ReceiveBuf[30];    //Reset encoder flag
												if(ReceiveBuf[30] == 0xFF)    //进水报警    人工报警
												{
													while(1)
													{
														TIM_Cmd(TIM2, DISABLE); //close timer2
														TIM_Cmd(TIM1, DISABLE); //不在进行pid控制
														TIM_SetCompare1(TIM4, DutyfactorM);
														TIM_SetCompare2(TIM4, DutyfactorM);
														TIM_SetCompare3(TIM4, DutyfactorM);
														TIM_SetCompare4(TIM4, DutyfactorM);
														TIM_SetCompare1(TIM3, DutyfactorM);
														TIM_SetCompare2(TIM3, DutyfactorM);
														TIM_SetCompare3(TIM3, DutyfactorMo);
														TIM_Cmd(TIM2, ENABLE); //enable timer2
													}													
												}
												//获取电机pid参数
												kp = (int)ReceiveBuf[37];
												ki = (int)ReceiveBuf[38];
												kd = (int)ReceiveBuf[39];
                }
								UART3_Buffer[41] = Warning_Flag;
                UART3_Buffer[42] = ReceiveBuf[30];    //Reset encoder flag
//                ReceiveBuf[30] =  0x00;
								
								
	}
        
}

////PWM波输出引脚初始化
//void PwmGpioConfig()
//{
//  GPIO_InitTypeDef GPIO_InitStructure;
//	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOD, ENABLE);

//	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_13;			
//  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;
//  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
//  GPIO_Init(GPIOD, &GPIO_InitStructure);	
//}
//方波输入引脚初始化
void PulseGpioConfig()
{
	GPIO_InitTypeDef GPIO_InitStructure;
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_0;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING; //
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz; //50M时钟速度
	GPIO_Init(GPIOA, &GPIO_InitStructure);
}

void PulseRccConfig()
{
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA, ENABLE);
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_TIM8, ENABLE);
}

void PulseTimConfig()
{
	TIM_TimeBaseInitTypeDef   TIM_TimeBaseStructure;
	//配置TIMER8作为计数器
	TIM_DeInit(TIM8);

	TIM_TimeBaseStructure.TIM_Period = 0xFFFF;
	TIM_TimeBaseStructure.TIM_Prescaler = 0x00;
	TIM_TimeBaseStructure.TIM_ClockDivision = 0x0;
	TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBaseInit(TIM8, &TIM_TimeBaseStructure); // Time base configuration

	TIM_ETRClockMode2Config(TIM8, TIM_ExtTRGPSC_OFF, TIM_ExtTRGPolarity_NonInverted, 0);

	TIM_SetCounter(TIM8, 0);
	TIM_Cmd(TIM8, ENABLE);
}

//200ms的周期做一次实际速度计算
void Tim1Init(void)
{
	TIM_TimeBaseInitTypeDef TIM_TimeBaseInitStructure;
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_TIM1, ENABLE);
	TIM_TimeBaseInitStructure.TIM_Period = 2000-1;      //Config auto-reload register's value, timing cycle is 50ms
	TIM_TimeBaseInitStructure.TIM_Prescaler = 7200-1;    //若自定义预分频系数为0，则定时器的时钟频率为72M提供给定时器的时钟	0~65535之间
	TIM_TimeBaseInitStructure.TIM_ClockDivision = TIM_CKD_DIV1;     //Clock division is 0
	TIM_TimeBaseInitStructure.TIM_RepetitionCounter = 0;//重复计数设置  
	TIM_TimeBaseInitStructure.TIM_CounterMode = TIM_CounterMode_Up;//向上计数模式  
  TIM_TimeBaseInit(TIM1, &TIM_TimeBaseInitStructure); //参数初始化  
  TIM_ClearFlag(TIM1, TIM_FLAG_Update);//清中断标志位  
  
	TIM_ITConfig(TIM1, TIM_IT_Update, ENABLE);
	TIM_Cmd(TIM1, ENABLE);
}
void NvicConfig(void)
{
	NVIC_InitTypeDef NVIC_InitStrue;
//	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_2);
	NVIC_InitStrue.NVIC_IRQChannel = TIM1_UP_IRQn;
	NVIC_InitStrue.NVIC_IRQChannelCmd = ENABLE;
	NVIC_InitStrue.NVIC_IRQChannelPreemptionPriority = 1;
	NVIC_InitStrue.NVIC_IRQChannelSubPriority = 0;
	NVIC_Init(&NVIC_InitStrue);
}

void TIM1_UP_IRQHandler(void) 
{
	float temp = 0.0;
	if (TIM_GetITStatus(TIM1, TIM_IT_Update) != RESET)
  {
		pulseCount = TIM8->CNT;
		if((pulseCount - pulseCountPre) >= 0)
			motorSpeed = ((pulseCount - pulseCountPre) / 4 ) / 0.2;  //0.05表示50ms
		else
			motorSpeed = (((pulseCount - pulseCountPre) + 65536 ) / 4 ) / 0.2;  //0.05表示50ms
		
		//串口三DMA发送电机速度值给上位机
//		motorSpeedReal.motorSpeedFloat = motorSpeed;
//		memcpy(usart3DmaTxBuf,motorSpeedReal.motorSpeedHex,txBuffSize);
//		DMA_Cmd(DMA1_Channel2,ENABLE);
		//上位机数据转换后的PWM值给电机
//		memcpy(motorSpeedReal.motorSpeedHex,usart1DmaRxBuf,rxBuffSize);
//		TIM_SetCompare1(TIM4, motorSpeedReal.motorSpeedFloat);
//		UpperDataAnalyze();//usart1DmaRxBuf);
		
		temp = MotorPidCtl(setSpeed - motorSpeed);
		if(setSpeed != 0)
			TIM_SetCompare3(TIM3, (temp * 20000.0 / 1000.0)); //PID调节出来的PWM值直接用于设置Pulse
		else
			TIM_SetCompare3(TIM3, 0);
		
		pulseCountPre = pulseCount;
		
		TIM_ClearITPendingBit(TIM1, TIM_IT_Update);
	}
}
//接收上位机的电机速度以及PID参数
//void UpperDataAnalyze()//u8 upperData[17])
//{
//	int i = 0;
////	float kp,ki,kd;
////	u8 dataCheckOut = 0x00;
//	//和校验
////	for(i=0;i<16;i++)
////		dataCheckOut += upperData[i];
////	if(dataCheckOut != upperData[16])
////		return;
//	if(0xFF != usart1DmaRxBuf[16])
//		return;
//	//数据转换
//	for(i=0;i<4;i++)
//		motorSpeedReal.motorSpeedHex[i] = usart1DmaRxBuf[i];
//	setSpeed = motorSpeedReal.motorSpeedFloat;
//	//后面是PID参数的数据转换，借用联合体
//	for(i=4;i<8;i++)
//		motorSpeedReal.motorSpeedHex[i-4] = usart1DmaRxBuf[i];
//	kp = motorSpeedReal.motorSpeedFloat;
//	
//	for(i=8;i<12;i++)
//		motorSpeedReal.motorSpeedHex[i-8] = usart1DmaRxBuf[i];
//	ki = motorSpeedReal.motorSpeedFloat;
//	
//	for(i=12;i<16;i++)
//		motorSpeedReal.motorSpeedHex[i-12] = usart1DmaRxBuf[i];
//	kd = motorSpeedReal.motorSpeedFloat;
//	
////	pid.kp = kp;
////	pid.ki = ki;
////	pid.kd = kd;
//}







