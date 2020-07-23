/*
 * @ Author: 程豪琪
 * @ Description: 滑轨机器人控制主程序
 */
#include "main.h"
#include "stm32f4xx_hal.h"
#include "motor_ioinit.h"
#include "motor.h"
#include "motor_ctrl.h"

// lwip 头文件
#include "string.h"
#include "bsp_debug_usart.h"

#include "lwip/opt.h"
#include "lwip/init.h"
#include "lwip/netif.h"
#include "lwip/timeouts.h"
#include "netif/etharp.h"
#include "ethernetif.h"
#include "app_ethernet.h"
#include "tcp_echoserver.h"


// 定义电机结构体变量
volatile MOTOR motor_rail;
volatile MOTOR motor_camera;

// 定义lwip网络协议相关变量
struct netif gnetif; // 定义网络接口变量




int main(void)
{
	
	/* 复位所有外设，初始化Flash接口和系统滴答定时器 */
	HAL_Init();
	/* 配置系统时钟 */
	SystemClock_Config();
	/* 初始化串口并配置串口中断优先级 */
	MX_DEBUG_USART_Init();
	/* 初始化LWIP内核 */
	lwip_init();
	/* 配置网络接口，这里使用的是静态IP地址，而非DHCP */
	Netif_Config();
	
	while(1) 
	{	
		tcp_echoserver_connect(); // 进行创建TCPserver
		
	}
	
	 
	
}

/**
  * 函数功能: 系统时钟配置
  * 输入参数: 无
  * 返 回 值: 无
  * 说    明: 无
  */
void SystemClock_Config(void)
{
	RCC_OscInitTypeDef RCC_OscInitStruct;
	RCC_ClkInitTypeDef RCC_ClkInitStruct;

	__HAL_RCC_PWR_CLK_ENABLE();                                     //使能PWR时钟
  
	__HAL_PWR_VOLTAGESCALING_CONFIG(PWR_REGULATOR_VOLTAGE_SCALE1);  //设置调压器输出电压级别1
  
	RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSE;      // 外部晶振，8MHz
	RCC_OscInitStruct.HSEState = RCC_HSE_ON;                        //打开HSE 
	RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;                    //打开PLL
	RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSE;            //PLL时钟源选择HSE
	RCC_OscInitStruct.PLL.PLLM = 8;                                 //8分频MHz
	RCC_OscInitStruct.PLL.PLLN = 336;                               //336倍频
	RCC_OscInitStruct.PLL.PLLP = RCC_PLLP_DIV2;                     //2分频，得到168MHz主时钟
	RCC_OscInitStruct.PLL.PLLQ = 7;                                 //USB/SDIO/随机数产生器等的主PLL分频系数
	HAL_RCC_OscConfig(&RCC_OscInitStruct);

	RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK|RCC_CLOCKTYPE_SYSCLK
							  |RCC_CLOCKTYPE_PCLK1|RCC_CLOCKTYPE_PCLK2;
	RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;       // 系统时钟：168MHz
	RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;              // AHB时钟： 168MHz
	RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV4;               // APB1时钟：42MHz
	RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV2;               // APB2时钟：84MHz
	HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_5);

	HAL_RCC_EnableCSS();                                            // 使能CSS功能，优先使用外部晶振，内部时钟源为备用

	// HAL_RCC_GetHCLKFreq()/1000    1ms中断一次（HAL_Delay()延时单位为1ms）
	// HAL_RCC_GetHCLKFreq()/100000	 10us中断一次（HAL_Delay()延时单位为10us）
	// HAL_RCC_GetHCLKFreq()/1000000 1us中断一次（HAL_Delay()延时单位为1us）
	HAL_SYSTICK_Config(HAL_RCC_GetHCLKFreq()/100000);                // 配置并启动系统滴答定时器
	/* 系统滴答定时器时钟源 */
	HAL_SYSTICK_CLKSourceConfig(SYSTICK_CLKSOURCE_HCLK);

	/* 系统滴答定时器中断优先级配置 */
	HAL_NVIC_SetPriority(SysTick_IRQn, 0, 0);
}



/**
  * 函数功能: 配置网络接口
  * 输入参数: 无
  * 返 回 值: 无
  * 说    明: 无
  */
static void Netif_Config(void)
{
  ip_addr_t ipaddr;
  ip_addr_t netmask;
  ip_addr_t gw;
  
  /* Initializes the dynamic memory heap defined by MEM_SIZE.*/
  mem_init();
  
  /* Initializes the memory pools defined by MEMP_NUM_x.*/
  memp_init();  
#ifdef USE_DHCP
  ip_addr_set_zero_ip4(&ipaddr);
  ip_addr_set_zero_ip4(&netmask);
  ip_addr_set_zero_ip4(&gw);
#else
  IP_ADDR4(&ipaddr,IP_ADDR0,IP_ADDR1,IP_ADDR2,IP_ADDR3);
  IP_ADDR4(&netmask,NETMASK_ADDR0,NETMASK_ADDR1,NETMASK_ADDR2,NETMASK_ADDR3);
  IP_ADDR4(&gw,GW_ADDR0,GW_ADDR1,GW_ADDR2,GW_ADDR3);
  
	printf("静态IP地址........................%d.%d.%d.%d\r\n",IP_ADDR0,IP_ADDR1,IP_ADDR2,IP_ADDR3);
	printf("子网掩码..........................%d.%d.%d.%d\r\n",NETMASK_ADDR0,NETMASK_ADDR1,NETMASK_ADDR2,NETMASK_ADDR3);
	printf("默认网关..........................%d.%d.%d.%d\r\n",GW_ADDR0,GW_ADDR1,GW_ADDR2,GW_ADDR3);
#endif /* USE_DHCP */
  
  /* Add the network interface */    
  netif_add(&gnetif, &ipaddr, &netmask, &gw, NULL, &ethernetif_init, &ethernet_input);
  
  /* Registers the default network interface */
  netif_set_default(&gnetif);
  
  if (netif_is_link_up(&gnetif))
  {
    printf("成功连接网卡\n");
    /* When the netif is fully configured this function must be called */
    netif_set_up(&gnetif);
  }
  else
  {
    /* When the netif link is down this function must be called */
    netif_set_down(&gnetif);
  }
  
  /* Set the link callback function, this function is called on change of link status*/
  netif_set_link_callback(&gnetif, ethernetif_update_config);
}

