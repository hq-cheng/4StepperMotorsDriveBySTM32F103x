/*
 * @ Author: luolichq
 * @ Description: 滑轨机器人步进电机结构体MOTOR类型初始化
 * @ More:
 * 		motor_camera	PULSE-		ENA-	DIR-		
 *						PI5			PD3		PD7
 *		motor_rail		PULSE-		ENA-	DIR-
 *						PI6			PD11	PF11
 */
#include "motor_ctrl.h"

/*******************************************************************************
* 函 数 名         : MOTOR_SetENA
* 函数功能         : 单个电机运行使能设置
* 输    入         : motor, motorEna
* 输    出         : 无
*******************************************************************************/
void MOTOR_SetENA(MOTOR motor, FunctionalState motorEna)
{
	/* 点电机状态发生改变，修更新点击当前状态（结构体变量的属性） */
	motor._mtEnable = motorEna;
	
	switch(motorEna) 
	{
		case ENABLE:
			if(motor._mtId == MOTOR_RAIL)
			{
				HAL_GPIO_WritePin(GPIOD, GPIO_PIN_11, GPIO_PIN_RESET); // PD11置0
			}
			if(motor._mtId == MOTOR_CAMERA) 
			{
				HAL_GPIO_WritePin(GPIOD, GPIO_PIN_3, GPIO_PIN_RESET); // PD3置0(实际光耦取输出为1)
				
			}
			break;
		case DISABLE:
			if(motor._mtId == MOTOR_RAIL)
			{
				HAL_GPIO_WritePin(GPIOD, GPIO_PIN_11, GPIO_PIN_SET); // PD11置1
			}
			if(motor._mtId == MOTOR_CAMERA) 
			{
				HAL_GPIO_WritePin(GPIOD, GPIO_PIN_3, GPIO_PIN_SET); // PD3置1		
			}			
			break;
	}
}

/*******************************************************************************
* 函 数 名         : MOTOR_SetTIMENA
* 函数功能         : 单个电机对应的TIM运行使能设置
* 输    入         : motor, motorTIMState
* 输    出         : 无
*******************************************************************************/
void MOTOR_SetTIMENA(MOTOR motor, MotorTIMStatus motorTIMState)
{
	motor._mtTIMState = motorTIMState;
	switch(motorTIMState) 
	{
		case STARTTIM:
			if(motor._mtId == MOTOR_RAIL)
			{
				HAL_TIM_PWM_Start(&tim8_handler, TIM_CHANNEL_2);
			}
			if(motor._mtId == MOTOR_CAMERA) 
			{
				HAL_TIM_PWM_Start(&tim8_handler, TIM_CHANNEL_1);
				
			}
			break;
		case STOPTIM:
			if(motor._mtId == MOTOR_RAIL)
			{
				HAL_TIM_PWM_Stop(&tim8_handler, TIM_CHANNEL_2);
				
			}
			if(motor._mtId == MOTOR_CAMERA) 
			{
				HAL_TIM_PWM_Stop(&tim8_handler, TIM_CHANNEL_1);
			}				
			break;
	}		
}


/*******************************************************************************
* 函 数 名         : MOTOR_SetDIR
* 函数功能         : 单个电机运行方向设置
* 输    入         : motor, motorDir
* 输    出         : 无
*******************************************************************************/
void MOTOR_SetDIR(MOTOR motor, MotorDirection motorDir) 
{
	motor._mtDir = motorDir;
	
	switch(motorDir) 
	{
		case FORWARD:
			if(motor._mtId == MOTOR_RAIL)
			{
				HAL_GPIO_WritePin(GPIOF, GPIO_PIN_11, GPIO_PIN_RESET); // PF11置0
				
			}
			if(motor._mtId == MOTOR_CAMERA) 
			{
				HAL_GPIO_WritePin(GPIOD, GPIO_PIN_7, GPIO_PIN_RESET); // PD7置0
			}
			HAL_Delay(1); // 延时10us，建立方向
			break;
		case BACKWARD:
			if(motor._mtId == MOTOR_RAIL)
			{
				HAL_GPIO_WritePin(GPIOF, GPIO_PIN_11, GPIO_PIN_SET); // PF11置1
			}
			if(motor._mtId == MOTOR_CAMERA) 
			{
				HAL_GPIO_WritePin(GPIOD, GPIO_PIN_7, GPIO_PIN_SET); // PD7置1
				
			}		
			HAL_Delay(1);			
			break;
	}	
}

/*******************************************************************************
* 函 数 名         : MOTOR_SetSpeed
* 函数功能         : 单个电机运行输出速度设置
* 输    入         : motorSpeed
* 输    出         : 无
*******************************************************************************/
void MOTOR_SetSpeed(uint32_t motorSpeed)
{
	tim8_handler.Instance->PSC = motorSpeed;
	motor_rail._mtSpeed = motorSpeed;        // TIM8_CH1，TIM8_CH2实际是一个定时器，速度一致
	motor_camera._mtSpeed = motorSpeed;
}







