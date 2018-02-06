/**************************************************************************
**
**		Programs function: Delay Functions
**		John Hee		07/12/2017
**		
**************************************************************************/
#include "Init_Delay.h"

void Delay_ms(int ms)
{
		int i,j;
	    for(i = 0; i < ms; i++)
			for(j = 0; j < 10260; j++);
}

void Delay_us(int us) 
{ 
		int i,j;
		for(i = 0; i < us; i++)
			for(j = 0; j < 9; j++);
}










