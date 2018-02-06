#ifndef _Ctrl_RUDDER_H_H
#define _Ctrl_RUDDER_H_H

/*---------------------------------------------------------------------
*       �� = aa * t + bb 
*       �� : ��λ�����յ��ĽǶ��ź�,��
*        t : pwm�����źŸߵ�ƽʱ��,ms
*       
*       Dutyfactor = kk * t = kk * ( �� - bb )  / aa  
*
-----------------------------------------------------------------------*/
#define aa 90.0
#define bb -45.0
#define kk 1000

#define LEFT_UP 1
#define RIGHT_UP 2
#define LEFT_DOWN 3
#define RIGHT_DOWN 4
#define HORIZ_LEFT 5
#define HORIZ_RIGHT 6


/* include ----------------------------------------------------*/
#include "stm32f10x.h"

/* Define parameters -------------------------------------------------*/


/* Functions declaration -------------------------------------------------*/
//num: Channel number of PWM signals
void Ctrl_RUDDER(u8 num, double pwmBuf); 

//speed: PWM signal
void Ctrl_MOTOR(double speed); 

void Reset_Vehicle(void);










#endif



