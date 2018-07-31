/*
 *
 *  Copyright 2018，程豪琪，哈尔滨工业大学（深圳）
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

// 初始化8x1键盘对应IO设置，配置按键扫描程序

//*键盘：
//*				GPIOB5	(K1)
//*				GPIOB6	(K2)
//*				GPIOB13 (K3)	//GPIOB7  (K3)
//*				GPIOB14	(K4)	//GPIOB8  (K4)
//*				GPIOB9	(K5)
//*				GPIOB10	(K6)
//*				GPIOB11	(K7)
//*				GPIOB12	(K8)

//*     效果： 		按下K1，顺时针转;
//*			   		按下K2，逆时针转;
//* 		   		按下K3，加速;
//*			   		按下K4，减速 ;
//*			   		按下K5，暂停;
//*			   		按下K6，启动;
//*			   		按下K7，计时加长;
//*			   		按下K8，计时减少；

#include "key8.h"
#include "delay.h"




void GPIO_Key_Init(void)
{
	GPIO_InitTypeDef GPIO_InitStructure;

	// 对应IO端口时钟使能
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOB,ENABLE);	

	// KEY1~KEY8对应IO端口初始化设置 
	GPIO_InitStructure.GPIO_Pin = KEY1_Pin|KEY2_Pin|KEY3_Pin|KEY4_Pin|KEY5_Pin|KEY6_Pin|KEY7_Pin|KEY8_Pin;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPU;	// 	上拉输入
	GPIO_Init(GPIOB,&GPIO_InitStructure);			//	GPIOB
}


/*******************************************************************************
* 函 数 名         : Key8_scan
* 函数功能         : 按键键盘*8扫描程序，按键按下对应IO口值为0
* 输    入         : 无
* 输    出         : 0(无键按下)/按键对应标志值
*******************************************************************************/
uint16_t Key8_scan() 
{
		// 不支持连续按键
		if(KEY1 == 0 ||KEY2 == 0 ||KEY3 == 0 ||KEY4 == 0 ||KEY5 == 0 ||KEY6 == 0 ||KEY7 == 0 ||KEY8 == 0) 
		{
			delay_ms(15);
			if(KEY1 == 0)	{while(KEY1 == 0){;} 	return KEY_CC;}
			if(KEY2 == 0) 	{while(KEY2 == 0){;} 	return KEY_CCW;}
			if(KEY3 == 0) 	{while(KEY3 == 0){;} 	return KEY_INC;}
			if(KEY4 == 0) 	{while(KEY4 == 0){;} 	return KEY_DEC;}
			if(KEY5 == 0) 	{while(KEY5 == 0){;} 	return KEY_STOP;}
			if(KEY6 == 0) 	{while(KEY6 == 0){;} 	return KEY_START;}
			if(KEY7 == 0) 	{while(KEY7 == 0){;} 	return KEY_TIMEUP;}
			if(KEY8 == 0) 	{while(KEY8 == 0){;} 	return KEY_TIMEDOWN;}
		}		
		return  0;				// 无按键按下 Key8_ret 返回 0
}



	

