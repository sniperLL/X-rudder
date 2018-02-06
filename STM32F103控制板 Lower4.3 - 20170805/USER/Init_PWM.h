#ifndef _Init_PWM_H_H
#define _Init_PWM_H_H

/* -----ÿ0.5���ǣ�pwm�����źţ�TIM_Pulse���ı�50/9.0 ------*/
#define dutyfactor_change 500/9.0
#define DutyfactorL 100*(-30 + 90 + 45)/9.0     	//      -30��(60��) 
#define DutyfactorM 100*(0 + 90 + 45)/9.0            //        0��(90��)
#define DutyfactorR 100*(30 + 90 + 45)/9.0           //      30��(120��)
#define DutyfactorMo 0

/* include ------------------------------------------------------*/
#include "stm32f10x.h"


/* Define parameters  -------------------------------------------------------------*/


/* Functions declaration --------------------------------------------------------------*/
void Init_PWM(float dutyfactor1, 
                          float dutyfactor2, 
                          float dutyfactor3, 
                          float dutyfactor4, 
                          float dutyfactor5, 
                          float dutyfactor6,
                          float dutyfactor7);










#endif


