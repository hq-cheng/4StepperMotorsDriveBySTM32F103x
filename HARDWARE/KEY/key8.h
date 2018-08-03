/*
 *
 *  Copyright 2018，程豪琪，哈尔滨工业大学
 *  All Rights Reserved.
 */
/*
 * *******************************郑重声明****************************************
 
 * 该程序为本人所写，付出了大量的精力，现将其公开出来供大家参考学习；
 * 任何个人和组织不得未经授权将此程序转载，或用于商业行为！
 * 
 * 由于本人水平有限，程序难免出现错误，可以通过下面的联系方式联系本人
 * 谢谢你的指正！
 *
 * 邮箱：18s153717@stu.hit.edu.cn
 * github：https://github.com/clearcumt
 * 博客：https://www.cnblogs.com/loveclear/
 * 
 * ******************************************************************************
 */
 
#ifndef __KEY8_H
#define __KEY8_H
#include "sys.h"

//*键盘：
//*				GPIOB5	(K1)
//*				GPIOB6	(K2)
//*				GPIOB13 (K3)	//GPIOB7  (K3)
//*				GPIOB14	(K4)	//GPIOB8  (K4)
//*				GPIOB9	(K5)
//*				GPIOB10	(K6)
//*				GPIOB11	(K7)
//*				GPIOB12	(K8)

//*效果： 			按下K1，顺时针转;
//*			   		按下K2，逆时针转;
//* 		   		按下K3，加速;
//*			   		按下K4，减速 ;
//*			   		按下K5，暂停;
//*			   		按下K6，启动;
//*			   		按下K7，计时加长;
//*			   		按下K8，计时减少；


#define KEY1 GPIO_ReadInputDataBit(GPIOB,GPIO_Pin_5)
#define KEY2 GPIO_ReadInputDataBit(GPIOB,GPIO_Pin_6)
//#define KEY3 GPIO_ReadInputDataBit(GPIOB,GPIO_Pin_7)
#define KEY3 GPIO_ReadInputDataBit(GPIOB,GPIO_Pin_13)
//#define KEY4 GPIO_ReadInputDataBit(GPIOB,GPIO_Pin_8)
#define KEY4 GPIO_ReadInputDataBit(GPIOB,GPIO_Pin_14)
#define KEY5 GPIO_ReadInputDataBit(GPIOB,GPIO_Pin_9)
#define KEY6 GPIO_ReadInputDataBit(GPIOB,GPIO_Pin_10)
#define KEY7 GPIO_ReadInputDataBit(GPIOB,GPIO_Pin_11)
#define KEY8 GPIO_ReadInputDataBit(GPIOB,GPIO_Pin_12)

#define KEY1_Pin GPIO_Pin_5
#define KEY2_Pin GPIO_Pin_6
//#define KEY3_Pin GPIO_Pin_7
#define KEY3_Pin GPIO_Pin_13
//#define KEY4_Pin GPIO_Pin_8
#define KEY4_Pin GPIO_Pin_14
#define KEY5_Pin GPIO_Pin_9
#define KEY6_Pin GPIO_Pin_10
#define KEY7_Pin GPIO_Pin_11
#define KEY8_Pin GPIO_Pin_12


#define KEY_CC			1
#define KEY_CCW			2
#define KEY_INC			3
#define KEY_DEC			4
#define KEY_STOP 		5
#define KEY_START		6
#define KEY_TIMEUP		7
#define KEY_TIMEDOWN	8

// 按键外部函数声明
void GPIO_Key_Init(void);
uint16_t Key8_scan(void);
#endif


