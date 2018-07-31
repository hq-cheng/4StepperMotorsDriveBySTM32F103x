#ifndef __TIMRE3_H
#define __TIMRE3_H

#include "sys.h"

// 这里为什么不添加这些需要的头文件呢？因为在stm32f10x_conf.h头文件中已经使能了
//#include "stm32f10x_rcc.h"
//#include "stm32f10x_tim.h"


void TIM3_Config_Init(u16 arr,u16 psc);        // Timer3初始化设置
void TIM3Nvic_Config_Init(void);     // Timer中断优先级设置

extern uint16_t k_time, count_time;


#endif



