#include "timer3.h"
#include "GPIO_Motor.h"

void TIM3Nvic_Config_Init(void)
{
    NVIC_InitTypeDef NVIC_InitStructure;
    
    NVIC_InitStructure.NVIC_IRQChannel = TIM3_IRQn;     // 链接TIM3中断
    NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 2;   // 抢占优先级
    NVIC_InitStructure.NVIC_IRQChannelSubPriority = 3;      // 子优先级
    NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;     // IQR通道使能
    NVIC_Init(&NVIC_InitStructure);
}

void TIM3_Config_Init(u16 arr,u16 psc)
{
    TIM_TimeBaseInitTypeDef TIM_TimeBaseStructure;

    /* 1. 定时器所需的系统时钟使能 */
    RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM3,ENABLE);
    /* 2. 定时器TIM3初始化 */
	
    TIM_TimeBaseStructure.TIM_Period = arr;     // 自动重装周期值
    TIM_TimeBaseStructure.TIM_Prescaler = psc;  // 预分频系数
    TIM_TimeBaseStructure.TIM_ClockDivision = TIM_CKD_DIV1;     // CKD[1:0]： 时钟分频因子 (Clock division)，与CK_INT有关
    TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;     // 向上计数模式
    TIM_TimeBaseInit(TIM3,&TIM_TimeBaseStructure);

	/* 使能前，清除更新中断标志位 */
    TIM_ClearFlag(TIM3,TIM_FLAG_Update);		//使能前清除中断标志位，以免一启用中断后立即产生中断
	
	/* 4. 允许更新中断使能 */
    TIM_ITConfig(TIM3,TIM_IT_Update,ENABLE);

    /* 5. 中断优先级设置 */
    TIM3Nvic_Config_Init();

    /* 6. 打开/使能TIM3 */
    TIM_Cmd(TIM3,ENABLE);
}

/* 7. 书写定时器中断子程序 */
void TIM3_IRQHandler(void)
{
//	static uint16_t count=0;
	// 检查标志位，判断更新中断是否发生
	if(TIM_GetITStatus(TIM3,TIM_IT_Update) != RESET)
	{
		// 清除该中断标志位
		TIM_ClearITPendingBit(TIM3,TIM_IT_Update);
		
		count_time ++;
		if(count_time == k_time*1000)
		{
			count_time = 0;
			LED = !LED;
			MotorZ_DIR_Bit = !MotorZ_DIR_Bit;
		}

		
//		count++;
//		if(count == 3000)
//		{
//			count = 0;
//			Direction = !Direction;
//		}
	}

	// stm32是如何进入中断服务子程序的，与启动文件startup_stm32f10x_md.s有关，内核决定了Handler的入口地址
}






