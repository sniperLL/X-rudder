#ifndef _DMA_H_H
#define _DMA_H_H

/* include ----------------------------------------------------*/
#include "stm32f10x.h"


/* Define parameters  -------------------------------------*/


/* Functions declaration ---------------------------------------*/
//DMA_CHx:DMAͨ��CHx
//cpar:�����ַ
//cmar:�洢����ַ
//cndtr:���ݴ�����
//dir:���ݴ��䷽��(DMA_DIR_PeripheralDST : ���ڴ��ȡ���͵����� ; DMA_DIR_PeripheralSRC : �������ȡ���͵��ڴ�)
//mode:����ģʽ(DMA_Mode_Circular : ѭ��ģʽ ; DMA_Mode_Normal : ����ģʽ)
void MYDMA_Config(DMA_Channel_TypeDef* DMA_CHx, u32 cpar, u32 cmar, u16 cndtr, u32 dir, u32 mode);         //Config DMA1_CHx

void MYDMA_Enable(DMA_Channel_TypeDef* DMA_CHx);        //Enable DMA1_CHx













#endif



