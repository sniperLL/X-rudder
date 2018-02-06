/*********************************************************************************************************
**
**  	Programs Function : Control the X-shaped rudder submarine through a remoter
** 	John Hee		07/12/2017
** 
**********************************************************************************************************/

#include "stm32f10x.h"
#include "Init_LED.h"
#include "Init_Delay.h"
#include "Init_PWM_cap.h"
#include "Init_DMA.h"


/* Define parameters-------------------------------------------------------------*/
extern float direction_Data;
extern float tail_Data;
extern float head_Data;
extern float motor_Data;
extern u8 UART1_Buffer[UART1_BUF_SIZE];

/**************************************************************************
**
** Functions name��main()
**
**************************************************************************/
int main()
{
		Init_LED();
		TIM8_Init();      //���ڲ�׽ң������PWM  ��ʶĿǰ��������ˮƽ��ƫ�������ߵľ���
//		TIM3_Init();
//		TIM4_Init();
//		TIM5_Init();
		Init_USART1(115200);
//		Init_USART2(115200);
		MYDMA_Config(DMA1_Channel4, (u32)&USART1->DR, (u32)UART1_Buffer, UART1_BUF_SIZE);
//		MYDMA_Config(DMA1_Channel7, (u32)&USART2->DR, (u32)UART1_Buffer, UART1_BUF_SIZE);
		
		while(1)
		{
			USART_DMACmd(USART1,USART_DMAReq_Tx,ENABLE); //ʹ�ܴ���1��DMA����    
			MYDMA_Enable(DMA1_Channel4);//��ʼһ��DMA����
			while(1)
			{
				if(DMA_GetFlagStatus(DMA1_FLAG_TC4) != RESET)	//�ж�ͨ��4�������
				{
					DMA_ClearFlag(DMA1_FLAG_TC4);//���ͨ��4������ɱ�־
					break;
				}	
			}
			
//			USART_DMACmd(USART2,USART_DMAReq_Tx,ENABLE); //ʹ�ܴ���2��DMA����    
//			MYDMA_Enable(DMA1_Channel7);//��ʼһ��DMA����
//			while(1)
//			{
//				if(DMA_GetFlagStatus(DMA1_FLAG_TC7) != RESET)	//�ж�ͨ��6�������
//				{
//					DMA_ClearFlag(DMA1_FLAG_TC7);//���ͨ��6������ɱ�־
//					break;
//				}	
//			}
			
			GPIO_SetBits(GPIOG, GPIO_Pin_14);
			Delay_ms(50);
			GPIO_ResetBits(GPIOG, GPIO_Pin_14);
			Delay_ms(50);
		}
}







