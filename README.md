# 4StepperMotorsDriveBySTM32F103x
基于 STM32F103x系列 单片机的三轴步进电机驱动程序

已不再做电机控制0_0 

这个demo本身是很简单的，本科的时候学习用的 

另外再上传另一个demo供大家学习用，类似的，使用时切换到该分支, `git checkout HGRobotSimpleDemo`.

[HGRobotSimpleDemo](https://github.com/luoliCHq/4StepperMotorsDriveBySTM32F103x/tree/HGRobotSimpleDemo), 一个简单的滑轨机器人程序, 适合初学者, 只是给出了大致的架构, 只包括上位机与机器人通信(WinForm->LwIP栈->步进电机),板子用的stm32F4.

上位机的话用了因为用了海康的网络球机，所以有网络通信/串口通信/视频监控的功能，都是用SDK开发的。

实际上真正的机器人用的是伺服电机，步进电机也就拿来入门即可。

![avatar](https://github.com/luoliCHq/4StepperMotorsDriveBySTM32F103x/blob/HGRobotSimpleDemo/HGRobotApp.png )


------------------------------------------------------------------------------------------------------------------------

[4StepperMotorsDriveBySTM32F103x](https://github.com/luoliCHq/4StepperMotorsDriveBySTM32F103x)

### 1.相关的GPIO接口说明：  	
* 电机用GPIOC口

	```c++
	// (MotorX)A4988--> GPIOC0-GPIO1
	GPIOC0 	--> ENABLE
	GPIOA1 	--> STEP(TIM2 CH2)
	GPIOC1	--> DIR  
	```

	```c++
	// (MotorY)A4988--> GPIOC2-GPIO3
	GPIOC2 	--> ENABLE
	GPIOA7 	--> STEP(TM3 CH2)
	GPIOC3	--> DIR  
	```

	```c++	
	// (MotorZ)A4988--> GPIOC4-GPIO5
	GPIOC4 	--> ENABLE
	GPIOB7 	--> STEP(TIM4 CH2)
	GPIOC5	--> DIR
	```
	
* 机械手（二八步进电机）控制信号使用GPIOC口

	```c++
	GPIOC6	--> MotorH_A
	GPIOC7	--> MotorH_B	
	GPIOC8	--> MotorH_C
	GPIOC9	--> MotorH_D
	```

* 串口通信对应引脚宏定义

	```c
	#define MASTER_CTRL				GPIOA
	#define MASTER_TXD 				GPIO_Pin_9
	#define MASTER_RXD 				GPIO_Pin_10
	```
				
### 2.如何实时控制PWM输出通道的通断？

选择有很多种，比方说定时器使能/关闭，PWM输出通道使能/关闭，占空比为0/50%，驱动器使能引脚0/1
但关键是，你要能在程序中有办法监测到电机当前是停止的，还是运行的
所以选择设置/读取定时器使能位/PWM输出通道使能位
```c	
TIM_OCENSet(TIM4,OC2_ENR);
TIM_Cmd(TIM4,DISABLE);

TIM_OCENReset(TIM4,OC2_ENR);			
TIM_Cmd(TIM4,ENABLE);

TIM_OCENSet(TIM4,OC2_ENR,DISABLE);
TIM_Cmd(TIM4,DISABLE);

TIM_OCENSet(TIM4,OC2_ENR,ENABLE);
TIM_Cmd(TIM4,ENABLE);
```	

这里控制pwm通断其实就是为了启动或停止电机,然而启动和停止分别包含加速过程和减速过程,所以这里统一采用TIM_OCENReset()
其它的方式都打开使能并配合上加减速过程,实现电机的启动和停止
实际采用TIM_Cmd()更节能一些
	
### 3.如何实时改变PWM的频率以改变电机的速度？

```c
// fpwm_psc = 1，即为1分频，该函数内部已经作减一操作，故不用再减一
TIM_ChangePrescaler(TIMx,fpwm_psc);	
// 这里采用TIM_ChangePrescaler方法，因为占空比始终不变，例如可以为50%
TIM_SetAutoreload(TIMx,fpwm_arr)		
```
* （全步进模式）计算转速

```c
// 一个脉冲周期T = arr/[(72*1000000)/fpwm_psc] = 72*1000000/（fpwm_psc*arr）(s) ,f = 1/T (Hz)
// 转速n = (f/200)*60 (r/min)，细分数1.8°，200个脉冲转一圈
// 占空比			p = pulse_ccr / arr;
```						
				

### 4.键盘相关的GPIO接口说明：

* 相关GPIO口： 

	```c
	GPIOB5	(K1) 
	GPIOB6	(K2)				
	GPIOB13 (K3)	//GPIOB7  (K3)
	GPIOB14	(K4)	//GPIOB8  (K4)	
	GPIOB9	(K5)
	GPIOB10	(K6)
	GPIOB11	(K7)
	GPIOB12	(K8)
	```	

* 效果： 

	```c
	按下K1，顺时针转
	按下K2，逆时针转;
	按下K3，加速;
	按下K4，减速 ;
	按下K5，暂停;
	按下K6，启动;
	按下K7，计时加长;
	按下K8，计时减少；
	```	
	
### 5. UC/OSII系统使用说明：

* sys.h 文件中 SYSTEM_SUPPORT_UCOS 置1
* stm32f10x_it.c 文件中 USE_UCOSII 置1（实际 stm32f10x_it.c 这个文件没有在我的项目中用到）

### 附：重构顺序：

* 1. 键盘

	```c
	确认按键后返回相应键值
	可以在主程序中根据键值设计响应程序
	```

* 2. 计时定时器 TIM1	

* 3. PWM 生产定时器 TIM2,TIM3,TIM4			

	```c
	自动重装载计数值、计数器分频系数可调
	默认pwm占空比50%
	默认关闭pwm通道输出使能
	```

* 4. 四二步进电机基本控制
	
	```c
	启动/停止电机函数，启动过程包括电机pwm使能，和加减速过程
	改变电机速度函数
	改变电机方向函数，如果电机此时处于运行状态，应先停止在反方向启动
	```

* 5. 机械手八二电机基本控制
	
	```c
	打开机械手
	关闭机械手
	保持机械手当前状态
	```
	
* 6. GPIO口初始化

	```c
	全部存储在GPIO_Motor.c文件中
	含有基础的XYZ三轴步进电机使能和反向对应GPIO端口初始化
	含有基础的XYZ三轴步进电机PWM脉冲信号对应GPIO端口初始化
	含有定时器TIM2，TIM3，TIM4的pwm输出通道对应GPIO端口初始化
	含有机械手八二马达的L289N驱动器控制对应GPIO端口引脚初始化
	```
