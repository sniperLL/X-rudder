/**************************************************************************
**
**		Programs function: PWM Configuration
**		John Hee		07/12/2017
**		
**************************************************************************/
#include "Init_PWM_cap.h"
#include "Init_DMA.h"

typedef union
{
	float remoterData_float;
	u8 remoterData_u8[4];
}remoterData;

/* Define parameters -------------------------------------------------------------*/
float direction_Data;
float tail_Data;
float head_Data;
float motor_Data;

remoterData _Direction_Data;
remoterData _Tail_Data;
remoterData _Head_Data;
remoterData _Motor_Data;

u8 TIM8CH2_CAPTURE_STA = 0;	//输入捕获状态
u16 TIM8CH2_CAPTURE_VAL;		//输入捕获值

u8 TIM3CH2_CAPTURE_STA = 0;	//输入捕获状态
u16 TIM3CH2_CAPTURE_VAL;		//输入捕获值

u8 TIM4CH2_CAPTURE_STA = 0;	//输入捕获状态
u16 TIM4CH2_CAPTURE_VAL;		//输入捕获值

u8 TIM5CH2_CAPTURE_STA = 0;	//输入捕获状态
u16 TIM5CH2_CAPTURE_VAL;		//输入捕获值

extern u8 UART1_Buffer[UART1_BUF_SIZE];

int i,j;

//Configurate timers:TIM8/3/4/5   C7 方向舵  CH1
void TIM8_Init(void)
{
	GPIO_InitTypeDef GPIO_InitStructure;
	TIM_TimeBaseInitTypeDef TIM_TimeBaseStructure;
	TIM_ICInitTypeDef TIM_ICInitStructure;
	NVIC_InitTypeDef NVIC_InitStructure;
	
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOC | RCC_APB2Periph_TIM8, ENABLE);
	
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_7;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOC, &GPIO_InitStructure);
	GPIO_ResetBits(GPIOC, GPIO_Pin_7);
	
	TIM_TimeBaseStructure.TIM_Period = 0xffff;    
	TIM_TimeBaseStructure.TIM_Prescaler = 71;      //0.001ms TIM_CNT记一次数据
	TIM_TimeBaseStructure.TIM_ClockDivision = 0x0;
	TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBaseStructure.TIM_RepetitionCounter = 0;
	TIM_TimeBaseInit(TIM8, & TIM_TimeBaseStructure);

	TIM_ICInitStructure.TIM_Channel = TIM_Channel_2;         
	TIM_ICInitStructure.TIM_ICPolarity = TIM_ICPolarity_Rising;      
	TIM_ICInitStructure.TIM_ICSelection = TIM_ICSelection_DirectTI;    
	TIM_ICInitStructure.TIM_ICPrescaler = TIM_ICPSC_DIV1;         
	TIM_ICInitStructure.TIM_ICFilter = 0x0;                           
	TIM_PWMIConfig(TIM8, &TIM_ICInitStructure); 
	
	TIM_SelectInputTrigger(TIM8, TIM_TS_TI2FP2);               
	TIM_SelectSlaveMode(TIM8, TIM_SlaveMode_Reset);
	TIM_SelectMasterSlaveMode(TIM8, TIM_MasterSlaveMode_Enable); 
	
	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_0);
	
	NVIC_InitStructure.NVIC_IRQChannel = TIM8_CC_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 1;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE; 
	NVIC_Init(&NVIC_InitStructure);

	TIM_ITConfig(TIM8, TIM_IT_CC2, ENABLE);
	TIM_ClearITPendingBit(TIM8, TIM_IT_CC2);
	TIM_Cmd(TIM8,ENABLE);  
}

void TIM3_Init(void)
{
	GPIO_InitTypeDef GPIO_InitStructure;
	TIM_TimeBaseInitTypeDef TIM_TimeBaseStructure;
	TIM_ICInitTypeDef TIM_ICInitStructure;
	NVIC_InitTypeDef NVIC_InitStructure;
	
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM3, ENABLE);
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA, ENABLE);
	
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_7;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOA, &GPIO_InitStructure);
	GPIO_ResetBits(GPIOA, GPIO_Pin_7);
	
	TIM_TimeBaseStructure.TIM_Period = 0xffff;    
	TIM_TimeBaseStructure.TIM_Prescaler = 71;      //0.01ms TIM_CNT记一次数据
	TIM_TimeBaseStructure.TIM_ClockDivision = 0x0;
	TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBaseStructure.TIM_RepetitionCounter = 0;
	TIM_TimeBaseInit(TIM3, & TIM_TimeBaseStructure);

	TIM_ICInitStructure.TIM_Channel = TIM_Channel_2;         
	TIM_ICInitStructure.TIM_ICPolarity = TIM_ICPolarity_Rising;      
	TIM_ICInitStructure.TIM_ICSelection = TIM_ICSelection_DirectTI;    
	TIM_ICInitStructure.TIM_ICPrescaler = TIM_ICPSC_DIV1;         
	TIM_ICInitStructure.TIM_ICFilter = 0x0;                            
	TIM_PWMIConfig(TIM3, &TIM_ICInitStructure); 
	
	TIM_SelectInputTrigger(TIM3, TIM_TS_TI2FP2);               
	TIM_SelectSlaveMode(TIM3, TIM_SlaveMode_Reset);
	TIM_SelectMasterSlaveMode(TIM3, TIM_MasterSlaveMode_Enable); 
	
	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_0);
	
	NVIC_InitStructure.NVIC_IRQChannel = TIM3_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 2;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE; 
	NVIC_Init(&NVIC_InitStructure);
	
//	TIM_ITConfig(TIM3, TIM_IT_CC2 | TIM_IT_Update, ENABLE);
	TIM_ITConfig(TIM3, TIM_IT_CC2, ENABLE);
	TIM_ClearITPendingBit(TIM3, TIM_IT_CC2);
	TIM_Cmd(TIM3,ENABLE);  
}

void TIM4_Init(void)
{
	GPIO_InitTypeDef GPIO_InitStructure;
	TIM_TimeBaseInitTypeDef TIM_TimeBaseStructure;
	TIM_ICInitTypeDef TIM_ICInitStructure;
	NVIC_InitTypeDef NVIC_InitStructure;
	
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM4, ENABLE);
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOB, ENABLE);
	
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_7;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOB, &GPIO_InitStructure);
	GPIO_ResetBits(GPIOB, GPIO_Pin_7);
	
	TIM_TimeBaseStructure.TIM_Period = 0xffff;    
	TIM_TimeBaseStructure.TIM_Prescaler = 71;      //0.01ms TIM_CNT记一次数据
	TIM_TimeBaseStructure.TIM_ClockDivision = 0x0;
	TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBaseStructure.TIM_RepetitionCounter = 0;
	TIM_TimeBaseInit(TIM4, & TIM_TimeBaseStructure);

	TIM_ICInitStructure.TIM_Channel = TIM_Channel_2;         
	TIM_ICInitStructure.TIM_ICPolarity = TIM_ICPolarity_Rising;      
	TIM_ICInitStructure.TIM_ICSelection = TIM_ICSelection_DirectTI;    
	TIM_ICInitStructure.TIM_ICPrescaler = TIM_ICPSC_DIV1;         
	TIM_ICInitStructure.TIM_ICFilter = 0x0;                            
	TIM_PWMIConfig(TIM4, &TIM_ICInitStructure); 
	
	TIM_SelectInputTrigger(TIM4, TIM_TS_TI2FP2);               
	TIM_SelectSlaveMode(TIM4, TIM_SlaveMode_Reset);
	TIM_SelectMasterSlaveMode(TIM4, TIM_MasterSlaveMode_Enable); 
	
	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_0);
	
	NVIC_InitStructure.NVIC_IRQChannel = TIM4_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 3;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE; 
	NVIC_Init(&NVIC_InitStructure);
	
//	TIM_ITConfig(TIM4, TIM_IT_CC2 | TIM_IT_Update, ENABLE);
	TIM_ITConfig(TIM4, TIM_IT_CC2, ENABLE);
	TIM_ClearITPendingBit(TIM4, TIM_IT_CC2);
	TIM_Cmd(TIM4,ENABLE);  
}

void TIM5_Init(void)
{
	GPIO_InitTypeDef GPIO_InitStructure;
	TIM_TimeBaseInitTypeDef TIM_TimeBaseStructure;
	TIM_ICInitTypeDef TIM_ICInitStructure;
	NVIC_InitTypeDef NVIC_InitStructure;
	
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM5, ENABLE);
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA, ENABLE);
	
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_1;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOA, &GPIO_InitStructure);
	GPIO_ResetBits(GPIOA, GPIO_Pin_1);
	
	TIM_TimeBaseStructure.TIM_Period = 0xffff;    
	TIM_TimeBaseStructure.TIM_Prescaler = 71;      //0.01ms TIM_CNT记一次数据
	TIM_TimeBaseStructure.TIM_ClockDivision = 0x0;
	TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBaseStructure.TIM_RepetitionCounter = 0;
	TIM_TimeBaseInit(TIM5, & TIM_TimeBaseStructure);

	TIM_ICInitStructure.TIM_Channel = TIM_Channel_2;         
	TIM_ICInitStructure.TIM_ICPolarity = TIM_ICPolarity_Rising;      
	TIM_ICInitStructure.TIM_ICSelection = TIM_ICSelection_DirectTI;    
	TIM_ICInitStructure.TIM_ICPrescaler = TIM_ICPSC_DIV1;         
	TIM_ICInitStructure.TIM_ICFilter = 0x0;                            
	TIM_PWMIConfig(TIM5, &TIM_ICInitStructure); 
	
	TIM_SelectInputTrigger(TIM5, TIM_TS_TI2FP2);               
	TIM_SelectSlaveMode(TIM5, TIM_SlaveMode_Reset);
	TIM_SelectMasterSlaveMode(TIM4, TIM_MasterSlaveMode_Enable); 
	
	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_0);
	
	NVIC_InitStructure.NVIC_IRQChannel = TIM5_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 4;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE; 
	NVIC_Init(&NVIC_InitStructure);
	
//	TIM_ITConfig(TIM5, TIM_IT_CC2 | TIM_IT_Update, ENABLE);
	TIM_ITConfig(TIM5, TIM_IT_CC2, ENABLE);
	TIM_ClearITPendingBit(TIM5, TIM_IT_CC2);
	TIM_Cmd(TIM5,ENABLE);  
}

//Interruption services
void TIM8_CC_IRQHandler()		//Direction Rudders
{
	if(TIM_GetITStatus(TIM8, TIM_IT_CC2) != RESET)	//发生捕获事件
	{
		if(TIM8CH2_CAPTURE_STA & 0X40)	//捕获到一个下降沿
		{
			TIM8CH2_CAPTURE_STA |= 0X80;	//标记完成一次捕获
			TIM8CH2_CAPTURE_VAL = TIM_GetCapture1(TIM8);
			direction_Data = K8 * TIM8CH2_CAPTURE_VAL + B8;
			_Direction_Data.remoterData_float = direction_Data;
			for(i = 0, j = 1; i < 4; i++, j++)
			{
				UART1_Buffer[j] = _Direction_Data.remoterData_u8[i];
			}
			TIM_OC2PolarityConfig(TIM8, TIM_ICPolarity_Rising);	//设置为上升沿捕获
		}
		else
		{
			TIM8CH2_CAPTURE_STA = 0;
			TIM8CH2_CAPTURE_VAL = 0;
			TIM_SetCounter(TIM8, 0);
			TIM8CH2_CAPTURE_STA |= 0X40;		//标记捕获到了上升沿
			TIM_OC2PolarityConfig(TIM8, TIM_ICPolarity_Falling);	//设置为下降沿捕获
		}
	}
	TIM_ClearITPendingBit(TIM8 , TIM_IT_CC2);
}

void TIM3_IRQHandler()		//Tail Rudders
{
	if(TIM_GetITStatus(TIM3, TIM_IT_CC2) != RESET)	//发生捕获事件
	{
		if(TIM3CH2_CAPTURE_STA & 0X40)	//捕获到一个下降沿
		{
			TIM3CH2_CAPTURE_STA |= 0X80;	//标记完成一次捕获
			TIM3CH2_CAPTURE_VAL = TIM_GetCapture1(TIM3);
			tail_Data = K3 * TIM3CH2_CAPTURE_VAL + B3;
//			if(tail_Data >= 0)
//			{
//				UART1_Buffer[3] = 0x00;
//				_Tail_Data.remoterData_float = tail_Data;
//			}
//			else
//			{
//				UART1_Buffer[3] = 0XFF;
//				_Tail_Data.remoterData_float = -tail_Data;
//			}
			_Tail_Data.remoterData_float = tail_Data;
			for(i = 0, j = 5; i < 4; i++, j++)
			{
				UART1_Buffer[j] = _Tail_Data.remoterData_u8[i];
			}
			TIM_OC2PolarityConfig(TIM3, TIM_ICPolarity_Rising);	//设置为上升沿捕获
		}
		else
		{
			TIM3CH2_CAPTURE_STA = 0;
			TIM3CH2_CAPTURE_VAL = 0;
			TIM_SetCounter(TIM3, 0);
			TIM3CH2_CAPTURE_STA |= 0X40;		//标记捕获到了上升沿
			TIM_OC2PolarityConfig(TIM3, TIM_ICPolarity_Falling);	//设置为下降沿捕获
		}
	}
	TIM_ClearITPendingBit(TIM3 , TIM_IT_CC2);
}

void TIM4_IRQHandler()		//Head Rudders
{
	if(TIM_GetITStatus(TIM4, TIM_IT_CC2) != RESET)	//发生捕获事件
	{
		if(TIM4CH2_CAPTURE_STA & 0X40)	//捕获到一个下降沿
		{
			TIM4CH2_CAPTURE_STA |= 0X80;	//标记完成一次捕获
			TIM4CH2_CAPTURE_VAL = TIM_GetCapture1(TIM4);
			head_Data = K4 * TIM4CH2_CAPTURE_VAL + B4;
//			if(head_Data >= 0)
//			{
//				UART1_Buffer[5] = 0x00;
//				_Head_Data.remoterData_float = head_Data;	
//			}
//			else
//			{
//				UART1_Buffer[5] = 0xFF;
//				_Head_Data.remoterData_float = -head_Data;
//			}
			_Head_Data.remoterData_float = head_Data;
			for(i = 0, j = 9; i < 4; i++, j++)
			{
				UART1_Buffer[j] = _Head_Data.remoterData_u8[i];
			}
			TIM_OC2PolarityConfig(TIM4, TIM_ICPolarity_Rising);	//设置为上升沿捕获
		}
		else
		{
			TIM4CH2_CAPTURE_STA = 0;
			TIM4CH2_CAPTURE_VAL = 0;
			TIM_SetCounter(TIM4, 0);
			TIM4CH2_CAPTURE_STA |= 0X40;		//标记捕获到了上升沿
			TIM_OC2PolarityConfig(TIM4, TIM_ICPolarity_Falling);	//设置为下降沿捕获
		}
	}
	TIM_ClearITPendingBit(TIM4 , TIM_IT_CC2);
}

void TIM5_IRQHandler()		//Motor
{
	if(TIM_GetITStatus(TIM5, TIM_IT_CC2) != RESET)	//发生捕获事件
	{
		if(TIM5CH2_CAPTURE_STA & 0X40)	//捕获到一个下降沿
		{
			TIM5CH2_CAPTURE_STA |= 0X80;	//标记完成一次捕获
			TIM5CH2_CAPTURE_VAL = TIM_GetCapture1(TIM5);
			motor_Data = K5 * TIM5CH2_CAPTURE_VAL + B5;
			if(motor_Data < 100)
				motor_Data = 0;
//			if(motor_Data >= 0)
//			{
//				UART1_Buffer[7] = 0x00;
//				_Motor_Data.remoterData_float = motor_Data;	
//			}
//			else
//			{
//				UART1_Buffer[7] = 0xFF;
//				_Motor_Data.remoterData_float = -motor_Data;
//			}
			_Motor_Data.remoterData_float = motor_Data;
			for(i = 0, j = 13; i < 4; i++, j++)
			{
				UART1_Buffer[j] = _Motor_Data.remoterData_u8[i];
			}
			TIM_OC2PolarityConfig(TIM5, TIM_ICPolarity_Rising);	//设置为上升沿捕获
		}
		else
		{
			TIM5CH2_CAPTURE_STA = 0;
			TIM5CH2_CAPTURE_VAL = 0;
			TIM_SetCounter(TIM5, 0);
			TIM5CH2_CAPTURE_STA |= 0X40;		//标记捕获到了上升沿
			TIM_OC2PolarityConfig(TIM5, TIM_ICPolarity_Falling);	//设置为下降沿捕获
		}
	}
	TIM_ClearITPendingBit(TIM5 , TIM_IT_CC2);
}












