/**************************************************************************
**
**		Programs function: LED Configuration
**		John Hee		07/12/2017
**		
**************************************************************************/
#include "Init_LED.h"

void Init_LED(void)
{
//LED,D2иак╦
	    //Define structures for initiation
        GPIO_InitTypeDef GPIO_InitStructure;
        
        //Enable Tclock
        RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOG | RCC_APB2Periph_GPIOD | RCC_APB2Periph_AFIO, ENABLE);
        
        //Config I/O ports,     Used I/O ports: PG14
        GPIO_InitStructure.GPIO_Pin = GPIO_Pin_14; 
        GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
        GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;        
        GPIO_Init(GPIOG, &GPIO_InitStructure);      //LED, D2
}












