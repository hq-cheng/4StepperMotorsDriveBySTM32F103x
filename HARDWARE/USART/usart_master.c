#include "usart_master.h"

unsigned char USART1_RX_COUNT = 0;

// 串口USART1的GPIO口初始化
void GPIO_USART1_Init()
{
	GPIO_InitTypeDef GPIO_InitStructure;
	
	// USART1-TXD
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA,ENABLE);
	GPIO_InitStructure.GPIO_Pin = MASTER_TXD;				// 初始化GPIOA9(TXD)
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;			// 复用推挽输出
	GPIO_Init(MASTER_CTRL, &GPIO_InitStructure);	

	// USART1-RXD
	GPIO_InitStructure.GPIO_Pin = MASTER_RXD;				// 初始化GPIOA10(RXD)
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;	// 浮空输入
	GPIO_Init(MASTER_CTRL, &GPIO_InitStructure);
}

// 初始化NVIC串口优先级
void USART1Nvic_Config_Init()
{
    NVIC_InitTypeDef NVIC_InitStructure;
    
    NVIC_InitStructure.NVIC_IRQChannel = USART1_IRQn;     	// USART1全局中断通道,响应何种串口中断类型可以在中断服务子程序中设计
	// 抢占优先级,必定时器1要高，保证接收数据不会丢失
    NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 1;   
    NVIC_InitStructure.NVIC_IRQChannelSubPriority = 1;     	// 子优先级
    NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;        	// IQR通道使能
    NVIC_Init(&NVIC_InitStructure);
}

// 初始化设置串口USART1
void USART1_Master_Init(u32 bound)
{
	USART_InitTypeDef USART_InitStructure;
	
	// 串口时钟使能
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_USART1,ENABLE);
	
	GPIO_USART1_Init();
	
	// 串口复位
	USART_DeInit(USART1);
	
	// 串口参数初始化
	USART_InitStructure.USART_BaudRate = bound;				// 设置波特率
	USART_InitStructure.USART_StopBits = USART_StopBits_1;	// 一个停止位
	USART_InitStructure.USART_Parity = USART_Parity_No;		// 无奇偶校验位（也就是说一帧数据字是踏踏实实的8位）
	// 无硬件数据流控制
	USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;
	// 收发模式
	USART_InitStructure.USART_Mode = USART_Mode_Rx | USART_Mode_Tx;
	
#if IS_MUTI_COMUNICATION
	//多处理器模式下，一帧数据字长为9位，MSB位可以作数据（0）/地址（1）标志位
	USART_InitStructure.USART_WordLength = USART_WordLength_9b;
	USART_Init(USART1,&USART_InitStructure);				// 初始化串口
	USART_SetAddress(USART1,MASTER_ADDR);					// 多处理器模式下，设置主机地址
	
#else
	// 普通收发模式
	// 字长为8位
	USART_InitStructure.USART_WordLength = USART_WordLength_8b;
	USART_Init(USART1,&USART_InitStructure);				// 初始化串口

#endif

	USART1Nvic_Config_Init();								// 串口NVIC中断优先级初始化
	USART_ITConfig(USART1,USART_IT_RXNE,ENABLE);			// 使能串口接收中断

	// 串口使能
	USART_Cmd(USART1,ENABLE);
}

// 串口中断服务子程序
void USART1_IRQHandler(void)
{
	u8 Res;
	// 串口中断类型：接收中断请求
	if(USART_GetFlagStatus(USART1, USART_IT_RXNE) != RESET)
	{
		Res = USART_ReceiveData(USART1);					//(USART1->DR);	//读取接收到的数据  // USARTx_DR读操作可以将标志位 RXNE 清0
		if(Res == 0xEE)
		{
			USART1_RX_COUNT = 0;
		}
		else
		{
			buf[USART1_RX_COUNT] = Res;
			USART1_RX_COUNT ++;
			if(USART1_RX_COUNT > 200-1) USART1_RX_COUNT = 0;	
		}

	}
}



