/*----------------------------------------- include --------------------------------------------------*/
#include "DMA.h"

/*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::;
*
*       Programs function: Receive and send datas through serial ports by DMA
*       Functions name: void MYDMA_Config(DMA_Channel_TypeDef* DMA_CHx, u32 cpar, u32 cmar, u16 cndtr);         //DMA1_CHx configuration
*                                     void MYDMA_Enable(DMA_Channel_TypeDef* DMA_CHx);        //Enable DMA1_CHx
* 
*
:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::;*/
/* Define parameters-------------------------------------------------*/
u16 DMA1_MEN_LEN;       //Save length of datas that sent or received everytime


/*-------------------------------------- MYDMA_Config() -----------------------------------------------------------*/
//DMA_CHx:DMA_Channel_CHx
//cpar:�����ַ
//cmar:�洢����ַ
//cndtr:���ݴ�����
//dir:���ݴ��䷽��(DMA_DIR_PeripheralDST : ���ڴ��ȡ���͵����� ; DMA_DIR_PeripheralSRC : �������ȡ���͵��ڴ�)
//mode:work mode(DMA_Mode_Circular; DMA_Mode_Normal)

void MYDMA_Config(DMA_Channel_TypeDef* DMA_CHx, u32 cpar, u32 cmar, u16 cndtr, u32 dir, u32 mode)         //Config DMA1_CHx
{
        //Define structures for initiation
        DMA_InitTypeDef DMA_InitStructure;
        
        //Enable clock
        RCC_AHBPeriphClockCmd(RCC_AHBPeriph_DMA1, ENABLE);
        
        DMA1_MEN_LEN = cndtr;    //����DMAÿ�����ݴ��ͳ���
        
        DMA_DeInit(DMA_CHx);      //��DMA��ͨ��1�Ĵ�������Ϊȱʡֵ
        
        //DMA Initiation Configuration
        DMA_InitStructure.DMA_BufferSize = cndtr;      //DMAͨ����DMA����Ĵ�С
        DMA_InitStructure.DMA_DIR = dir;      //���ݴ��䷽��
        DMA_InitStructure.DMA_M2M = DMA_M2M_Disable;             //DMAͨ��xû������Ϊ�ڴ浽�ڴ洫��
        DMA_InitStructure.DMA_MemoryBaseAddr = cmar;            //DMA�ڴ����ַ
        DMA_InitStructure.DMA_MemoryDataSize = DMA_MemoryDataSize_Byte;          //���ݿ��Ϊ8λ
        DMA_InitStructure.DMA_MemoryInc = DMA_MemoryInc_Enable;         //�ڴ��ַ�Ĵ�������
        DMA_InitStructure.DMA_Mode = mode;            //����ģʽ
        DMA_InitStructure.DMA_PeripheralBaseAddr = cpar;         //DMA�������ַ
        DMA_InitStructure.DMA_PeripheralDataSize = DMA_PeripheralDataSize_Byte;          //���ݿ��Ϊ8λ
        DMA_InitStructure.DMA_PeripheralInc = DMA_PeripheralInc_Disable;        //�����ַ�Ĵ�������
        DMA_InitStructure.DMA_Priority = DMA_Priority_Medium;   //DMA_Channel has medium priority 
        
        DMA_Init(DMA_CHx, &DMA_InitStructure);
}

/*---------------------------------------- MYDMA_Enable() ----------------------------------------------------------*/
//Start DMA transmission or reception
void MYDMA_Enable(DMA_Channel_TypeDef* DMA_CHx)        //Enable DMA1_CHx
{
        DMA_Cmd(DMA_CHx, DISABLE);
        DMA_SetCurrDataCounter(DMA_CHx, DMA1_MEN_LEN);
        DMA_Cmd(DMA_CHx, ENABLE);
        
}






