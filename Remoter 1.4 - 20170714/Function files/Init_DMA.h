#ifndef _Init_DMA_H_HE
#define _Init_DMA_H_HE


#define UART1_BUF_SIZE 18

/* include ------------------------------------------------------*/
#include "stm32f10x.h"


/* Functions declaration --------------------------------------------------------------*/
void MYDMA_Config(DMA_Channel_TypeDef* DMA_CHx,u32 cpar,u32 cmar,u16 cndtr);
void MYDMA_Enable(DMA_Channel_TypeDef*DMA_CHx);
void Init_USART1(u32 bound);
void Init_USART2(u32 bound);








#endif

