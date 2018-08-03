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

#ifndef __MOTOR_H
#define __MOTOR_H

#include "sys.h"


// 这么写变量太多了，我想把它放到结构体中
// 分为外部可变参数，跟随可变参数，和不可变参数

// 1. 外部可变参数，不需要实时修改，但在结构体外部程序中修改，多用于外部赋值改变电机当前状态

// 2. 跟随可变参数，也就是需要通过读取电机对应寄存器当前的值来实时修改，而外部则不能修改，多用于程序读取当前状态

// 3. 不可变参数，在初始化后就不再改变了
typedef __packed struct {
	
	/* 外部可变参数 */
	unsigned char direction;		// 电机方向
	uint16_t speed;					// 电机速度，改变psc,进而pwm脉冲频率，最后改变速度
	float destn;					// 电机最终移动目的地坐标（mm）
	int totalTime;					// 电机移动到最终目的地总共所需时间(ms)
	unsigned char nextMove;
	
	
	/* 跟随可变参数 */
	uint16_t running;				// 电机当前运行状态，启动/停止电机时这个值会被实时修改
									// 为了不扰乱程序，这些读取的状态用于显示，不用于修改状态
	unsigned char isResetPoint;		// 电机是否在复位点，1/在，0/不在
	unsigned char isDestn;			// 电机是否到达指定坐标位置
	
	/* 不可变参数 */
	unsigned char id;				// 电机id
	TIM_TypeDef* MOTOx;				// 电机pwm脉冲所属定时器
	FunctionalState enable;			// 电机使能,默认打开

} MOTOR_CONTROL;

// 电机对应的定时器宏定义
#define MOTOX	TIM2
#define MOTOY	TIM3
#define MOTOZ	TIM4

// 电机方向宏定义
#define FORWARD 	1
#define	REVERSE 	0

// 电机运行状态宏定义
#define RUNNING 	((uint16_t)1)
#define	STOPPING 	((uint16_t)0)

// 电机结构体变量的外向声明
extern volatile MOTOR_CONTROL motor1;
extern volatile MOTOR_CONTROL motor2;
extern volatile MOTOR_CONTROL motor3;

void Init_Motor_Struct(unsigned char MOTOID);

#endif

