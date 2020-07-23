/*
 * @ Author: luolichq
 * @ Description: 滑轨机器人步进电机结构体MOTOR类型初始化
 * @ More:
 * 		motor_camera	PULSE-		ENA-	DIR-		
 *						PI5			PD3		PD7
 *		motor_rail		PULSE-		ENA-	DIR-
 *						PI6			PD11	PF11
 */
#include "motor_ioinit.h"

TIM_HandleTypeDef tim8_handler;

/*
 * 定义步进电机DIR和ENA口对应的GPIO口，初始化结构体变量
 */
void MOTOR_CTRL_GPIO_Init(void)
{
	/* 定义步进电机DIR和ENA口对应的GPIO口，初始化结构体变量 */
	GPIO_InitTypeDef GPIO_InitStruct = {0};

	/* 使能GPIO对应的IO端口时钟 */
	__HAL_RCC_GPIOF_CLK_ENABLE();
	__HAL_RCC_GPIOD_CLK_ENABLE();

	/* 配置PD3、PD7、PD11、PF11引脚输出电压为低电平 */
	HAL_GPIO_WritePin(GPIOF, GPIO_PIN_11, GPIO_PIN_SET);
	HAL_GPIO_WritePin(GPIOD, GPIO_PIN_11|GPIO_PIN_3|GPIO_PIN_7, GPIO_PIN_SET);

	/* PF11引脚的IO模式 */
	GPIO_InitStruct.Pin = GPIO_PIN_11;
	GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
	GPIO_InitStruct.Pull = GPIO_NOPULL;
	GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_MEDIUM;
	HAL_GPIO_Init(GPIOF, &GPIO_InitStruct);

	/* 配置PD3、PD7、PD11引脚的IO模式 */
	GPIO_InitStruct.Pin = GPIO_PIN_11|GPIO_PIN_3|GPIO_PIN_7;
	GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
	GPIO_InitStruct.Pull = GPIO_NOPULL;
	GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_MEDIUM;
	HAL_GPIO_Init(GPIOD, &GPIO_InitStruct);
}

/*
 * 定义步进电机PULSE口对应的GPIO口，初始化结构体变量
 */
void MOTOR_PULSE_GPIO_Init(void) 
{
	/* 定义步进电机PULSE口对应的GPIO口，初始化结构体变量 */
	GPIO_InitTypeDef GPIO_InitStruct = {0};

	/* 使能GPIO对应的IO端口时钟 */
	__HAL_RCC_GPIOI_CLK_ENABLE();

	/* 配置PI5、PI6引脚的IO模式 */
	GPIO_InitStruct.Pin = GPIO_PIN_5|GPIO_PIN_6;
	GPIO_InitStruct.Mode = GPIO_MODE_AF_PP;
	GPIO_InitStruct.Pull = GPIO_NOPULL;
	GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_MEDIUM;
	GPIO_InitStruct.Alternate = GPIO_AF3_TIM8;
	
	HAL_GPIO_Init(GPIOI, &GPIO_InitStruct);		
}


/*
 * 定义步进电机PWM对应的定时器TIM8的结构体变量
 */
void MOTOR_TIM8PWM_Init(uint32_t arr, uint32_t psc)
{
	TIM_OC_InitTypeDef TIM_OCInitStructure = {0};
	
	/* 开启TIM8复用功能，配置PI5、PI6引脚为复用推挽输出 */
	MOTOR_PULSE_GPIO_Init();
	
	/* 使能TIM8时钟 */
	__HAL_RCC_TIM8_CLK_ENABLE();

	/* 3. 初始化TIM8，设置TIMx的ARR和PSC */
	tim8_handler.Instance = TIM8;
	tim8_handler.Init.Prescaler = psc-1; //预分频值，+1为分频系数，取值必须在0x0000~0xFFFF之间
	tim8_handler.Init.Period = arr - 1; //自动重装载值，取值必须在0x0000~0xFFFF之间
	tim8_handler.Init.CounterMode = TIM_COUNTERMODE_UP; //向上计数模式
	tim8_handler.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1; //时钟分割，这里为0
	tim8_handler.Init.RepetitionCounter = 0;
	tim8_handler.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_ENABLE; // 开启自动重装载模式
	HAL_TIM_PWM_Init(&tim8_handler);
	
	/* 4. 设置TIM8_CHx的PWM模式，使能TIM8_CHx输出 */
	TIM_OCInitStructure.Pulse = arr/2; //设置待装入捕获比较寄存器的脉冲值（占空比）,取值必须在0x0000~0xFFFF之间
	TIM_OCInitStructure.OCMode = TIM_OCMODE_PWM1; //TIM脉冲宽度调制模式1
	TIM_OCInitStructure.OCPolarity = TIM_OCPOLARITY_HIGH; //输出极性:TIM输出比较极性高
	TIM_OCInitStructure.OCNPolarity = TIM_OCNPOLARITY_HIGH; // 互补输出极性
	TIM_OCInitStructure.OCFastMode = TIM_OCFAST_DISABLE; 
	TIM_OCInitStructure.OCIdleState = TIM_OCIDLESTATE_RESET; //选择空闲状态下得非工作状态
	TIM_OCInitStructure.OCNIdleState = TIM_OCNIDLESTATE_RESET; //选择互补空闲状态下得非工作状态
	HAL_TIM_PWM_ConfigChannel(&tim8_handler, &TIM_OCInitStructure, TIM_CHANNEL_1); // 使能TIM_CHx的预装载寄存器
	HAL_TIM_PWM_ConfigChannel(&tim8_handler, &TIM_OCInitStructure, TIM_CHANNEL_2);
	
	/* 使能TIM8_CHx的定时器 */
	HAL_TIM_PWM_Start(&tim8_handler, TIM_CHANNEL_1); 
	HAL_TIM_PWM_Start(&tim8_handler, TIM_CHANNEL_2);
}
