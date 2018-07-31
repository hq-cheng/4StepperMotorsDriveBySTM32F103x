// *键盘：
// *				GPIOA8(K2)	GPIOA10(K4)	 GPIOA12(K6)  GPIOA14(K8)
// *				GPIOA9(K1)	GPIOA11(K3)	 GPIOA13(K5)  GPIOA15(K7)
              
// * 效果： 			按下K1，顺时针转;
// *				按下K2，逆时针转;
// *				按下K3，加速;
// *				按下K4，减速 ;
// *				按下K5，暂停;
// *				按下K6，启动;
// *				按下K7，计时加长;
// *				按下K8，计时减少；


#include "exti.h"




void EXTIx_Config_Init(void)
{
    EXTI_InitTypeDef EXTI_InitStructure;

    /* 1. 对于按键来说，首先应该初始化按键IO口为输入模式 */
    // GPIO_Key_Init();     // 主程序调用了

    /* 2. 使能系统时钟（复用功能时钟） */
    RCC_APB2PeriphClockCmd(RCC_APB2Periph_AFIO,ENABLE);

    /* 3. 确定中断线与IO口的映射关系 */
    GPIO_EXTILineConfig(GPIO_PortSourceGPIOA,KEY1_Source);  // KEY1

    /* 4. 对应的中断线初始化设置 */
    EXTI_InitStructure.EXTI_Line = KEY1_EXTI_Line;
    EXTI_InitStructure.EXTI_Mode = EXTI_Mode_Interrupt;
    EXTI_InitStructure.EXTI_Trigger = EXTI_Trigger_Falling; // 下降沿触发
    EXTI_InitStructure.EXTI_LineCmd = ENABLE;
    EXTI_Init(&EXTI_InitStructure);

    GPIO_EXTILineConfig(GPIO_PortSourceGPIOA,KEY2_Source);  // KEY2
    EXTI_InitStructure.EXTI_Line = KEY2_EXTI_Line;
    EXTI_Init(&EXTI_InitStructure);

    GPIO_EXTILineConfig(GPIO_PortSourceGPIOA,KEY3_Source);  // KEY3
    EXTI_InitStructure.EXTI_Line = KEY3_EXTI_Line;
    EXTI_Init(&EXTI_InitStructure);

    GPIO_EXTILineConfig(GPIO_PortSourceGPIOA,KEY4_Source);  // KEY4
    EXTI_InitStructure.EXTI_Line = KEY4_EXTI_Line;
    EXTI_Init(&EXTI_InitStructure);

    GPIO_EXTILineConfig(GPIO_PortSourceGPIOA,KEY5_Source);  // KEY5
    EXTI_InitStructure.EXTI_Line = KEY5_EXTI_Line;
    EXTI_Init(&EXTI_InitStructure);

    GPIO_EXTILineConfig(GPIO_PortSourceGPIOA,KEY6_Source);  // KEY6
    EXTI_InitStructure.EXTI_Line = KEY6_EXTI_Line;
    EXTI_Init(&EXTI_InitStructure);

    GPIO_EXTILineConfig(GPIO_PortSourceGPIOA,KEY7_Source);  // KEY7
    EXTI_InitStructure.EXTI_Line = KEY7_EXTI_Line;
    EXTI_Init(&EXTI_InitStructure);

    GPIO_EXTILineConfig(GPIO_PortSourceGPIOA,KEY8_Source);  // KEY8
    EXTI_InitStructure.EXTI_Line = KEY8_EXTI_Line;
    EXTI_Init(&EXTI_InitStructure);

    /* 5. 配置中断优先级 */
    EXTIxNvic_Config_Init();

}


void EXTIxNvic_Config_Init(void)
{
    NVIC_InitTypeDef NVIC_InitStructure;
    NVIC_InitStructure.NVIC_IRQChannel = KEY2_1_EXTI_IRQ ;			//使能按键KEY1、KEY2所在的外部中断通道
  	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0x01;	//抢占优先级1 
  	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0x00;			//子优先级0
  	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;					//使能外部中断通道
  	NVIC_Init(&NVIC_InitStructure); 

    NVIC_InitStructure.NVIC_IRQChannel = KEY8_3_EXTI_IRQ;			//使能按键KEY3~KEY8所在的外部中断通道
  	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0x01;	//抢占优先级1
  	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0x01;			//子优先级1
  	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;					//使能外部中断通道
  	NVIC_Init(&NVIC_InitStructure); 
}



void EXTI9_5_IRQHandler(void)
{
    if(EXTI_GetITStatus(KEY1_EXTI_Line) != RESET)
    {
        EXTI_ClearITPendingBit(KEY1_EXTI_Line);
        // pass
		Direction = 1;
		
    }
    if(EXTI_GetITStatus(KEY2_EXTI_Line) != RESET)
    {
        EXTI_ClearITPendingBit(KEY2_EXTI_Line);
        // pass
		Direction = 0;
    }
}

void EXTI15_10_IRQHandler(void)
{
    if(EXTI_GetITStatus(KEY3_EXTI_Line) != RESET)
    {
        EXTI_ClearITPendingBit(KEY3_EXTI_Line);
        // pass
		DeSpeed -= 2;
    }
    if(EXTI_GetITStatus(KEY4_EXTI_Line) != RESET)
    {
        EXTI_ClearITPendingBit(KEY4_EXTI_Line);
        // pass
		DeSpeed += 2;
    }
    if(EXTI_GetITStatus(KEY5_EXTI_Line) != RESET)
    {
        EXTI_ClearITPendingBit(KEY5_EXTI_Line);
        // pass
		DeSpeed = 0;
    }
    if(EXTI_GetITStatus(KEY6_EXTI_Line) != RESET)
    {
        EXTI_ClearITPendingBit(KEY6_EXTI_Line);
        // pass
		DeSpeed = 200;
    }
    if(EXTI_GetITStatus(KEY7_EXTI_Line) != RESET)
    {
        EXTI_ClearITPendingBit(KEY7_EXTI_Line);
        // pass
    }
    if(EXTI_GetITStatus(KEY8_EXTI_Line) != RESET)
    {
        EXTI_ClearITPendingBit(KEY8_EXTI_Line);
        // pass
    }
}

