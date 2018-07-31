// *键盘：
// *				GPIOA8(K2)	GPIOA10(K4)	 GPIOA12(K6)  GPIOA14(K8)
// *				GPIOA9(K1)	GPIOA11(K3)	 GPIOA13(K5)  GPIOA15(K7)
              
// * 效果： 		按下K1，顺时针转;
// *				按下K2，逆时针转;
// *				按下K3，加速;
// *				按下K4，减速 ;
// *				按下K5，暂停;
// *				按下K6，启动;
// *				按下K7，计时加长;
// *				按下K8，计时减少；

#ifndef __EXTI_H
#define	__EXTI_H

#include "sys.h"

#define KEY1_Source GPIO_PinSource9
#define KEY2_Source GPIO_PinSource8
#define KEY3_Source GPIO_PinSource11
#define KEY4_Source GPIO_PinSource10
#define KEY5_Source GPIO_PinSource13
#define KEY6_Source GPIO_PinSource12
#define KEY7_Source GPIO_PinSource15
#define KEY8_Source GPIO_PinSource14



#define KEY1_EXTI_Line  EXTI_Line9
#define KEY2_EXTI_Line  EXTI_Line8
#define KEY3_EXTI_Line  EXTI_Line11
#define KEY4_EXTI_Line  EXTI_Line10
#define KEY5_EXTI_Line  EXTI_Line13
#define KEY6_EXTI_Line  EXTI_Line12
#define KEY7_EXTI_Line  EXTI_Line15
#define KEY8_EXTI_Line  EXTI_Line14


#define KEY2_1_EXTI_IRQ   EXTI9_5_IRQn

#define KEY8_3_EXTI_IRQ   EXTI15_10_IRQn




void EXTIxNvic_Config_Init(void);
void EXTIx_Config_Init(void);
 
extern u16 Direction,DeSpeed;

#endif

