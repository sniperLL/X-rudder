#include "stm32f4xx_conf.h"

#define UPPERTBSIZE 37

void GPIO_Configuration(void);
void DMA_Configuration(void);
u16 Get_Adc(u8 ch);
extern uint8_t allChannelData[UPPERTBSIZE];

u16 ADCConvertedValue = 0;
/*����ADC3�����ݼĴ�����ַ��DMA����Ҫ�õ���������ݵ�ַ
 *ADC3�����ݵ�ַΪ�������ַ+ƫ�Ƶ�ַ������ַ��RM0090 Reference
 *manual���ο��ֲᣩ�ĵ�ַӳ����Ϊ0x40012200��ADC_DR
 *ƫ�Ƶ�ַΪ0x4C����ʵ�ʵ�ַΪ0x40012200+0x4C = 0x4001224C */
#define ADC3_DR_Address   ((uint32_t)0x4001224C)

void Adc3_Init(void)
{
	/* Enable peripheral clocks ------------------------------------------------*/
	/* Enable DMA2 clock */ 
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_DMA2 | RCC_AHB1Periph_GPIOF, ENABLE);
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_ADC3, ENABLE);
	GPIO_Configuration();
	DMA_Configuration();
}

void DMA_Configuration(void)
{
	ADC_InitTypeDef ADC_InitStructure;
  DMA_InitTypeDef DMA_InitStructure;
	ADC_CommonInitTypeDef ADC_CommonInitStructure;
	NVIC_InitTypeDef NVIC_InitStructure;
	
	/* DMA�ж����� */
	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_1);        //Ƕ�����ȼ�����Ϊ 1
	NVIC_InitStructure.NVIC_IRQChannel = DMA2_Stream0_IRQn;   //Ƕ��ͨ��ΪDMA2_Stream0_IRQn
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 2; //��ռ���ȼ�Ϊ 1
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 3;    //��Ӧ���ȼ�Ϊ 0
	NVIC_InitStructure.NVIC_IRQChannelCmd = DISABLE;     //ͨ���ж�ʹ��
	NVIC_Init(&NVIC_InitStructure);
  /* DMA2 Stream0 channel0 configuration **************************************/
  DMA_InitStructure.DMA_Channel = DMA_Channel_2;  
  DMA_InitStructure.DMA_PeripheralBaseAddr = (uint32_t)ADC3_DR_Address;
  DMA_InitStructure.DMA_Memory0BaseAddr = (uint32_t)&ADCConvertedValue;
  DMA_InitStructure.DMA_DIR = DMA_DIR_PeripheralToMemory;
  DMA_InitStructure.DMA_BufferSize = 1;
  DMA_InitStructure.DMA_PeripheralInc = DMA_PeripheralInc_Disable;
  DMA_InitStructure.DMA_MemoryInc = DMA_MemoryInc_Disable;
  DMA_InitStructure.DMA_PeripheralDataSize = DMA_PeripheralDataSize_HalfWord;
  DMA_InitStructure.DMA_MemoryDataSize = DMA_MemoryDataSize_HalfWord;
  DMA_InitStructure.DMA_Mode = DMA_Mode_Circular;
  DMA_InitStructure.DMA_Priority = DMA_Priority_High;
  DMA_InitStructure.DMA_FIFOMode = DMA_FIFOMode_Disable;         
  DMA_InitStructure.DMA_FIFOThreshold = DMA_FIFOThreshold_HalfFull;
  DMA_InitStructure.DMA_MemoryBurst = DMA_MemoryBurst_Single;
  DMA_InitStructure.DMA_PeripheralBurst = DMA_PeripheralBurst_Single;
  DMA_Init(DMA2_Stream0, &DMA_InitStructure);
  DMA_Cmd(DMA2_Stream0, ENABLE);
	DMA_ITConfig(DMA2_Stream0, DMA_IT_TC, ENABLE); //����DMA2��������ж�
  /* ADC Common Init **********************************************************/
  ADC_CommonInitStructure.ADC_Mode = ADC_Mode_Independent;
  ADC_CommonInitStructure.ADC_Prescaler = ADC_Prescaler_Div4;
  ADC_CommonInitStructure.ADC_DMAAccessMode = ADC_DMAAccessMode_Disabled;
  ADC_CommonInitStructure.ADC_TwoSamplingDelay = ADC_TwoSamplingDelay_5Cycles;
  ADC_CommonInit(&ADC_CommonInitStructure);
  /* ADC3 Init ****************************************************************/
  ADC_InitStructure.ADC_Resolution = ADC_Resolution_12b;
  ADC_InitStructure.ADC_ScanConvMode = DISABLE;
  ADC_InitStructure.ADC_ContinuousConvMode = DISABLE;
  ADC_InitStructure.ADC_ExternalTrigConvEdge = ADC_ExternalTrigConvEdge_None;
  ADC_InitStructure.ADC_DataAlign = ADC_DataAlign_Right;
  ADC_InitStructure.ADC_NbrOfConversion = 1;
  ADC_Init(ADC3, &ADC_InitStructure);
  /* ADC3 regular channel9 configuration *************************************/
  ADC_RegularChannelConfig(ADC3, ADC_Channel_9, 1, ADC_SampleTime_15Cycles);
 /* Enable DMA request after last transfer (Single-ADC mode) */
  ADC_DMARequestAfterLastTransferCmd(ADC3, ENABLE);
  /* Enable ADC3 DMA */
  ADC_DMACmd(ADC3, ENABLE);
  /* Enable ADC3 */
  ADC_Cmd(ADC3, ENABLE);
	/* Start ADC3 Software Conversion */ 
  ADC_SoftwareStartConv(ADC3);   //��������ⲿ��������������ʼת��
}

void GPIO_Configuration(void)
{
  GPIO_InitTypeDef GPIO_InitStructure;
  /* Configure PF.3 (ADC Channel9) as analog input -------------------------*/
  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_3;
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AN;
  GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_NOPULL;
  GPIO_Init(GPIOF, &GPIO_InitStructure);
}

//���ADCֵ
//ch: @ref ADC_channels 
//ͨ��ֵ 0~16ȡֵ��ΧΪ��ADC_Channel_0~ADC_Channel_16
//����ֵ:ת�����
u16 Get_Adc(u8 ch)   
{
	  	//����ָ��ADC�Ĺ�����ͨ����һ�����У�����ʱ��
	ADC_RegularChannelConfig(ADC3, ch, 1, ADC_SampleTime_480Cycles );	//ADC1,ADCͨ��,480������,��߲���ʱ�������߾�ȷ��			    
  
	ADC_SoftwareStartConv(ADC3);		//ʹ��ָ����ADC3�����ת����������	
	 
	while(!ADC_GetFlagStatus(ADC3, ADC_FLAG_EOC ));//�ȴ�ת������

	return ADC_GetConversionValue(ADC3);	//�������һ��ADC3�������ת�����
}

//��ȡͨ��ch��ת��ֵ��ȡtimes��,Ȼ��ƽ�� 
//ch:ͨ�����
//times:��ȡ����
//����ֵ:ͨ��ch��times��ת�����ƽ��ֵ
u16 Get_Adc_Average(u8 ch,u8 times)
{
	u32 temp_val=0;
	u8 t;
	for(t=0;t<times;t++)
	{
		temp_val+=Get_Adc(ch);
	}
	return temp_val/times;
} 

void DMA2_Stream0_IRQHandler(void)
{
//	u16 adcx;
	float temp;
  if (DMA_GetITStatus(DMA2_Stream0, DMA_IT_TCIF0) != RESET)
	{
		DMA_ClearITPendingBit(DMA2_Stream0, DMA_IT_TCIF0);
//		temp = ADC3->SR;  
//		temp = ADC3->DR;
		DMA_Cmd(DMA2_Stream0, DISABLE);
//		temp = ENCODERRBSIZE - DMA_GetCurrDataCounter(DMA1_Stream1);
		DMA_SetCurrDataCounter(DMA2_Stream0,sizeof(ADCConvertedValue));
		/*����û�����*/
//		adcx=Get_Adc_Average(ADC_Channel_9,5);//��ȡͨ��5��ת��ֵ��5��ȡƽ��
		temp=(float)(((ADCConvertedValue /4096) * 50000) / 9800);  //��ȡ���ֵ
		for(int i = 33;i < 37;i++)
		{
			 allChannelData[i] = floattodecimal(temp,i-33);
		}
		DMA_Cmd(DMA2_Stream0, ENABLE);
	}
}

//�������ã�ֻ�ǻ�ȡѹ������������A/D�����ֵ  ����λ������ת��
void GetDepth()
{
	ADC_SoftwareStartConv(ADC3);
	for(int i = 33;i < 37;i++)
	{
		 allChannelData[i] = decimaltohex(ADCConvertedValue,i-33);
	}
}
