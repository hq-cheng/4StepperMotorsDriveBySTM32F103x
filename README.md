一个简单的滑轨机器人程序, 适合初学者, 只是给出了大致的架构, 只包括上位机与机器人通信(WinForm->LwIP栈->步进电机),板子用的stm32F4


// 文件架构:

// CORE 存储官网固件库中CMSIS文件夹的内容，其实就是一些内核汇编文件
	-- Device 包含微控制器的启动代码以及专用系统文件
		-- Device\ST\STM32F4xx\Source\Templates\arm\startup_stm32f407xx.s 该芯片对应的启动文件
	-- DSP 数字信号处理库，官网提供一些源代码
	-- Include Cortex_M 内核汇编文件以及设备文件
	
// STM32F4xx_HAL_Driver HAL接口函数库文件
	-- Inc HAL库头文件
	-- Src HAL库源文件
		-- stm32f4xx_hal_msp_template.c 一般不会添加到工程里，都是些空函数，没卵用
		-- stm32f4xx_hal_timebase_rtc_alarm_template.c 不添加，会冲突
		-- stm32f4xx_hal_timebase_rtc_wakeup_template.c 不添加，会冲突
		-- stm32f4xx_hal_timebase_tim_template.c 不添加，会冲突
	 
// SYSTEM 存放自己写的关于系统时钟，操作系统的一些配置源文件
	-- sys/system_stm32f4xx.c 定义了系统初始化函数SystemInit以及系统时钟更新函数等
	
// USER 包含工程文件、主函数、一些比较重要的*.c和*.h文件，例如，stm32f4xx_it.h

// OBJ 存放编译过程中的中间文件与可执行文件，之前这些文件默认存储在Listing和Objects文件夹

// HARDWARE 存放自己写的外设相关的头文件与源文件

// C/C++选项卡中，定义预处理符号：USE_HAL_DRIVER,STM32F407xx
	-- ..\CORE\Include
	-- ..\STM32F4xx_HAL_Driver\Inc
	-- ..\CORE\Device\ST\STM32F4xx\Include 这是三个关于系统的头文件文件夹
	
// HGRobotApp 上位机程序(WinForm, C#)

 