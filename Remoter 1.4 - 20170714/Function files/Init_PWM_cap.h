#ifndef _Init_PWM_cap_H_HE
#define _Init_PWM_cap_H_HE


#define K8 1000.0 / (1486.0 - 990.0)           //表示水平向右偏移1000毫米
#define B8 -1486.0 * 1000.0 / (1486 - 990)			 //表示水平向左偏移1000毫米

#define K3 35.0 / (1487.0 - 995.0)
#define B3 -1487.0 * 35 / (1487 - 995)

#define K4 35.0 / (1484.0 - 989.0)
#define B4 -1484.0 * 35 / (1484 - 989)

#define K5 1000.0 / (1495.0 - 992.0)
#define B5 -1495.0 * 1000 / (1495 - 992)



/* include ------------------------------------------------------*/
#include "stm32f10x.h"


/* Functions declaration --------------------------------------------------------------*/
void TIM8_Init(void);
void TIM3_Init(void);
void TIM4_Init(void);
void TIM5_Init(void);









#endif

