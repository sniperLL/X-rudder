/* include --------------------------------------------------*/
#include "Init_USART.h"
#include "Init_PWM.h"
#include "string.h"

/*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::;
*
*       Programs function: USART1/2/3 Initiation
*       Functions name: Init_USART1(void)
*                                     Init_USART2(void)
*                                     Init_USART3(void)
*                                       
*
:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::;*/
/* Define parameters -------------------------------------------------*/
int dmaFlag = 0;

u8 UART1_Buffer[UART1_BUF_SIZE] = {0x00};
u8 UART3_Buffer[UART3_BUF_SIZE];
u8 Uart2_Buffer[UART2_BUF_SIZE] = {0x5A, 0x01, 								// 与上位机通讯的端口
																		0x00, 0x00, 0x00, 0x5A, 	// 左上舵
																		0x00, 0x00, 0x00, 0x5A, 	// 右上舵
																		0x00, 0x00, 0x00, 0x5A, 	// 左下舵
																		0x00, 0x00, 0x00, 0x5A, 	// 右下舵
																		0x00, 0x00, 0x00, 0x5A, 	// 左水平舵
																		0x00, 0x00, 0x00, 0x5A, 	// 右水平舵
																		0x00, 0x00, 0x03, 0xE8, 	// 电机转速
																		0x00, 0x33, 0x00,					// 进水报警、校验位、电机正反转
																		0x00, 0x00, 0x00, 0x00};	// 卡舵标志位   Receive data buffer area;
//u8 Uart1_Buffer[UART1_BUF_SIZE] = {0x5A, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x33};
//u8 Uart2_Buffer[UART2_BUF_SIZE] = {0x5A, 0x01, 0x00, 0x00, 0x33};
u8 ReceiveBuf[UART2_BUF_SIZE] = {0x5A, 0x01, 													
																	0x00, 0x00, 0x00, 0x5A, 
																	0x00, 0x00, 0x00, 0x5A, 
																	0x00, 0x00, 0x00, 0x5A, 
																	0x00, 0x00, 0x00, 0x5A, 
																	0x00, 0x00, 0x00, 0x5A, 
																	0x00, 0x00, 0x00, 0x5A, 
																	0x00, 0x00, 0x03, 0xE8, 
																	0x00, 0x33, 0x00,
																	0x00, 0x00, 0x00, 0x00,
																	0x00, 0x00, 0x00};	//Receive data buffer area
u8 LastReceiveBuf[UART2_BUF_SIZE] = {0x5A, 0x01, 
																			0x00, 0x00, 0x00, 0x5A, 
																			0x00, 0x00, 0x00, 0x5A, 
																			0x00, 0x00, 0x00, 0x5A, 
																			0x00, 0x00, 0x00, 0x5A, 
																			0x00, 0x00, 0x00, 0x5A, 
																			0x00, 0x00, 0x00, 0x5A, 
																			0x00, 0x00, 0x03, 0xE8, 
																			0x00, 0x33, 0x00,
																			0x00, 0x00, 0x00, 0x00};
//u8 SendBuf[UART2_BUF_SIZE] = {0x5A, 0x01, 0x00, 0x00, 0x33};    //Send data buffer area

/*------ Serial port1(USART1) ---------------------------------*/
/* 串口1（USART1） ---------------------------------*/
void Init_USART1(void)
{
	//定义初始化结构体
	GPIO_InitTypeDef GPIO_InitStrue;
	USART_InitTypeDef USART_InitStrue;
	NVIC_InitTypeDef NVIC_InitStrue;
	DMA_InitTypeDef DMA_InitStructure;
	
	//时钟使能
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA | RCC_APB2Periph_USART1 | RCC_APB2Periph_AFIO, ENABLE);
	RCC_AHBPeriphClockCmd(RCC_AHBPeriph_DMA1, ENABLE);

	//配置GPIO口
	GPIO_InitStrue.GPIO_Pin = GPIO_Pin_9;   //TXD
	GPIO_InitStrue.GPIO_Mode = GPIO_Mode_AF_PP;
	GPIO_InitStrue.GPIO_Speed =   GPIO_Speed_10MHz;
	GPIO_Init(GPIOA,&GPIO_InitStrue);
	
	GPIO_InitStrue.GPIO_Pin = GPIO_Pin_10;  //RXD
	GPIO_InitStrue.GPIO_Mode = GPIO_Mode_IN_FLOATING;
	GPIO_InitStrue.GPIO_Speed =   GPIO_Speed_10MHz;
	GPIO_Init(GPIOA,&GPIO_InitStrue);
	
	//配置串口
	USART_InitStrue.USART_BaudRate = 115200;
	USART_InitStrue.USART_WordLength = USART_WordLength_8b;
	USART_InitStrue.USART_StopBits = USART_StopBits_1;
	USART_InitStrue.USART_Parity = USART_Parity_No;
	USART_InitStrue.USART_Mode = USART_Mode_Rx | USART_Mode_Tx;
	USART_InitStrue.USART_HardwareFlowControl = USART_HardwareFlowControl_None;        
	USART_Init(USART1, &USART_InitStrue);
	
	//DMA Configuration  
	DMA_DeInit(DMA1_Channel5);  
	DMA_InitStructure.DMA_PeripheralBaseAddr = (u32)(&USART1->DR);  
	DMA_InitStructure.DMA_MemoryBaseAddr = (uint32_t)UART1_Buffer;   
	DMA_InitStructure.DMA_DIR = DMA_DIR_PeripheralSRC;   
	DMA_InitStructure.DMA_BufferSize = UART1_BUF_SIZE;  
	DMA_InitStructure.DMA_PeripheralInc = DMA_PeripheralInc_Disable;  
	DMA_InitStructure.DMA_MemoryInc = DMA_MemoryInc_Enable;  
	DMA_InitStructure.DMA_PeripheralDataSize = DMA_PeripheralDataSize_Byte;  
	DMA_InitStructure.DMA_MemoryDataSize = DMA_MemoryDataSize_Byte;   
	DMA_InitStructure.DMA_Mode = DMA_Mode_Normal;   //DMA_Mode_Normal 
	DMA_InitStructure.DMA_Priority = DMA_Priority_Medium;   
	DMA_InitStructure.DMA_M2M = DMA_M2M_Disable;  
	DMA_Init(DMA1_Channel5,&DMA_InitStructure);
	DMA_Cmd(DMA1_Channel5,ENABLE); 	
	
	//初始化中断优先级
	NVIC_InitStrue.NVIC_IRQChannel = USART1_IRQn;
	NVIC_InitStrue.NVIC_IRQChannelCmd = ENABLE;
	NVIC_InitStrue.NVIC_IRQChannelPreemptionPriority = 1;
	NVIC_InitStrue.NVIC_IRQChannelSubPriority = 1;
	NVIC_Init(&NVIC_InitStrue);
	
	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_2);
						
	//串口中断设置
//       USART_ITConfig(USART1, USART_IT_RXNE, ENABLE);
	USART_ITConfig(USART1,USART_IT_IDLE,ENABLE);         //开启USART3总线空闲中断
	
	//串口使能
	USART_DMACmd(USART1,USART_DMAReq_Rx,ENABLE);
	USART_Cmd(USART1, ENABLE);
	
//	while(dmaFlag == 0)
//	{
//		memcpy(Uart3_Buffer,Uart1_Buffer,41);
//		DMA_Cmd(DMA1_Channel2,ENABLE);
//	}
	
        
}

/* 中断响应函数 ------------------------------------------------------------------------*/

void USART1_IRQHandler(void)
{
     
        uint32_t temp = 0;  
	uint16_t i = 0;  
        
        if( USART_GetITStatus(USART1, USART_IT_IDLE))
        {
                //清除中断标志
//              USART_ClearITPendingBit(USART1, USART_IT_RXNE); 
                temp = USART1->SR;  
                temp = USART1->DR;  
		DMA_Cmd(DMA1_Channel5,DISABLE);  
//		if(Uart1_Buffer[0] == 0xFF)
//		{
//			for (i = 0;i <= 40 ;i++)  
//			{  
//				USART_SendData(USART3,Uart1_Buffer[i]); 
//				while (USART_GetFlagStatus(USART3, USART_FLAG_TC) == RESET)
//				{}
//			}
//		}
//		USART_SendData(USART3,0xff); 
//		while (USART_GetFlagStatus(USART3, USART_FLAG_TC) == RESET)
//		{}
		
		//采用USART3 DMA方式发送数据
		memcpy(UART3_Buffer,UART1_Buffer,UART1_BUF_SIZE);
		if(UART3_Buffer[0] == 0xFF && UART3_Buffer[40] == 0x33)
			DMA_Cmd(DMA1_Channel2,ENABLE);
		
    DMA_SetCurrDataCounter(DMA1_Channel5,UART1_BUF_SIZE);  
		DMA_Cmd(DMA1_Channel5,ENABLE); 
		
//		DMA_SetCurrDataCounter(DMA1_Channel2,UART3_BUF_SIZE);   //每次发送完成之后都要重新设置发送指针
//    DMA_Cmd(DMA1_Channel2, DISABLE);
//		DMA_ClearITPendingBit(DMA1_IT_TC2);
//		DMA_Cmd(DMA1_Channel2, DISABLE);					
        }
}




/* 串口2（USART2） ---------------------------------*/
void Init_USART2(void)   //串口2配置
{
        //定义初始化结构体
        GPIO_InitTypeDef GPIO_InitStrue;
        USART_InitTypeDef USART_InitStrue;
        
        //时钟使能
        RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA | RCC_APB2Periph_AFIO, ENABLE);
        RCC_APB1PeriphClockCmd(RCC_APB1Periph_USART2, ENABLE);
				RCC_AHBPeriphClockCmd(RCC_AHBPeriph_DMA1, ENABLE);
        
        //配置GPIO口
        GPIO_InitStrue.GPIO_Pin = GPIO_Pin_2;   //TXD
        GPIO_InitStrue.GPIO_Mode = GPIO_Mode_AF_PP;
        GPIO_InitStrue.GPIO_Speed =   GPIO_Speed_10MHz;
        GPIO_Init(GPIOA,&GPIO_InitStrue);
        
        GPIO_InitStrue.GPIO_Pin = GPIO_Pin_3;  //RXD
        GPIO_InitStrue.GPIO_Mode = GPIO_Mode_IN_FLOATING;
        GPIO_InitStrue.GPIO_Speed =   GPIO_Speed_10MHz;
        GPIO_Init(GPIOA,&GPIO_InitStrue);
        
        //配置串口
        USART_InitStrue.USART_BaudRate = 115200;
        USART_InitStrue.USART_WordLength = USART_WordLength_8b;
        USART_InitStrue.USART_StopBits = USART_StopBits_1;
        USART_InitStrue.USART_Parity = USART_Parity_No;
        USART_InitStrue.USART_Mode = USART_Mode_Rx | USART_Mode_Tx;
        USART_InitStrue.USART_HardwareFlowControl = USART_HardwareFlowControl_None;        
        USART_Init(USART2, &USART_InitStrue);

        //串口使能
        USART_Cmd(USART2, ENABLE);
}

/* 串口3（USART3） ---------------------------------*/
void Init_USART3(void)   //串口3配置
{
	//定义初始化结构体
	GPIO_InitTypeDef GPIO_InitStrue;
	USART_InitTypeDef USART_InitStrue;
	NVIC_InitTypeDef NVIC_InitStrue;
	DMA_InitTypeDef DMA_InitStructure;
	
	//时钟使能
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOD | RCC_APB2Periph_AFIO, ENABLE);
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_USART3, ENABLE);
	RCC_AHBPeriphClockCmd(RCC_AHBPeriph_DMA1, ENABLE);
	
	//配置GPIO口
	GPIO_InitStrue.GPIO_Pin = GPIO_Pin_8;   //TXD
	GPIO_InitStrue.GPIO_Mode = GPIO_Mode_AF_PP;
	GPIO_InitStrue.GPIO_Speed =   GPIO_Speed_10MHz;
	GPIO_Init(GPIOD,&GPIO_InitStrue);
		
	GPIO_PinRemapConfig(GPIO_FullRemap_USART3, ENABLE);
	
//	GPIO_InitStrue.GPIO_Pin = GPIO_Pin_11;  //RXD
//	GPIO_InitStrue.GPIO_Mode = GPIO_Mode_IN_FLOATING;
//	GPIO_InitStrue.GPIO_Speed =   GPIO_Speed_10MHz;
//	GPIO_Init(GPIOB,&GPIO_InitStrue);
	
	//配置串口
	USART_InitStrue.USART_BaudRate = 115200;
	USART_InitStrue.USART_WordLength = USART_WordLength_8b;
	USART_InitStrue.USART_StopBits = USART_StopBits_1;
	USART_InitStrue.USART_Parity = USART_Parity_No;
	USART_InitStrue.USART_Mode = USART_Mode_Rx | USART_Mode_Tx;
	USART_InitStrue.USART_HardwareFlowControl = USART_HardwareFlowControl_None;        
	USART_Init(USART3, &USART_InitStrue);
	
	//DMA Configuration  
	DMA_DeInit(DMA1_Channel2);  
	DMA_InitStructure.DMA_PeripheralBaseAddr = (u32)(&USART3->DR);  
	DMA_InitStructure.DMA_MemoryBaseAddr = (uint32_t)UART3_Buffer;   
	DMA_InitStructure.DMA_DIR = DMA_DIR_PeripheralDST;    //外设作为目的地 
	DMA_InitStructure.DMA_BufferSize = UART3_BUF_SIZE;  
	DMA_InitStructure.DMA_PeripheralInc = DMA_PeripheralInc_Disable;  
	DMA_InitStructure.DMA_MemoryInc = DMA_MemoryInc_Enable;  
	DMA_InitStructure.DMA_PeripheralDataSize = DMA_PeripheralDataSize_Byte;  
	DMA_InitStructure.DMA_MemoryDataSize = DMA_MemoryDataSize_Byte;   
	DMA_InitStructure.DMA_Mode = DMA_Mode_Normal;  
	DMA_InitStructure.DMA_Priority = DMA_Priority_VeryHigh;   
	DMA_InitStructure.DMA_M2M = DMA_M2M_Disable;  
	DMA_Init(DMA1_Channel2,&DMA_InitStructure);
	DMA_ITConfig(DMA1_Channel2, DMA_IT_TC, ENABLE);  //开启DMA发送完成中断
//	DMA_Cmd(DMA1_Channel2,ENABLE); 	
	
	//初始化中断优先级
	NVIC_InitStrue.NVIC_IRQChannel = DMA1_Channel2_IRQn;  //USART3 DMA 发送完成中断
	NVIC_InitStrue.NVIC_IRQChannelCmd = ENABLE;
	NVIC_InitStrue.NVIC_IRQChannelPreemptionPriority = 1;
	NVIC_InitStrue.NVIC_IRQChannelSubPriority = 2;
	NVIC_Init(&NVIC_InitStrue);
	
	NVIC_InitStrue.NVIC_IRQChannel = USART3_IRQn;  //USART3 DMA 发送完成中断
	NVIC_InitStrue.NVIC_IRQChannelCmd = ENABLE;
	NVIC_InitStrue.NVIC_IRQChannelPreemptionPriority = 1;
	NVIC_InitStrue.NVIC_IRQChannelSubPriority = 4;
	NVIC_Init(&NVIC_InitStrue);
	
	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_2);   //设置中断优先级

	//串口使能
	USART_DMACmd(USART3,USART_DMAReq_Tx,ENABLE);
	DMA_ClearITPendingBit(DMA1_IT_TC2);
	USART_Cmd(USART3, ENABLE);
}

//DMA发送完成中断标识位清除  以及关闭DMA  只有需要发送数据是在开启
void DMA1_Channel2_IRQHandler(void)
{
	uint32_t temp = 0;
  if(DMA_GetITStatus(DMA1_IT_TC2))
	{
    /* USART2 DMA 传输完成 */
		dmaFlag = 1;
		
		temp = USART3->SR;  
		temp = USART3->DR;
		DMA_Cmd(DMA1_Channel2, DISABLE);
		DMA_ClearFlag(DMA1_FLAG_TC2);  
    DMA_ClearITPendingBit(DMA1_IT_TC2);
		USART_ClearITPendingBit(USART3, USART_IT_TC);
		DMA_SetCurrDataCounter(DMA1_Channel2,UART3_BUF_SIZE);   //每次发送完成之后都要重新设置发送指针
    
		
  }
}






