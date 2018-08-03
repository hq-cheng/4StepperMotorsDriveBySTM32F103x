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
 
#include "usart_master.h"

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

// 初始化USART1接收缓冲区结构体参数
void Init_USART1_Buffer()
{
	volatile REC_BUFFER* pbuffer = &rec_buffer;
	int i;
	pbuffer->buf_id = 1;
	pbuffer->sbuf_id = 2;
	pbuffer->isnc_id = 3;
	pbuffer->buf_index = 0;
	pbuffer->sbuf_index = 0;
	pbuffer->isnc_index = 0;
	
	for(i=0;i<BUF_MAXSIZE;i++)								// 初始化清空缓存区，万一有内存残余呢
	{
		pbuffer->buf[i] = 0;
		pbuffer->sbuf[i] = 0;
		pbuffer->isNeedChange[i] = 0;
	}
	
	for(i=0;i<SBUF_MAXSIZE;i++)	
	{
		pbuffer->sbuf[i] = 0;
		pbuffer->isNeedChange[i] = 0;
	}
	
	for(i=0;i<ISNC_MAXSIZE;i++)	
	{
		pbuffer->isNeedChange[i] = 0;
	}
	
	pbuffer->isbufNull = 1;
	pbuffer->issbufNull = 1;
	pbuffer->isisncNull = 1;
	
}

// 检查缓冲区是否为空
unsigned char Check_Null_Buffer(unsigned char BUF_ID)
{
	int i;
	switch(BUF_ID)
	{
		case 1:
			for(i=0;i<BUF_MAXSIZE;i++)
			{
				if(rec_buffer.buf[i] != 0) return 0;
			}
			break;
		case 2:
			for(i=0;i<SBUF_MAXSIZE;i++)	
			{
				if(rec_buffer.sbuf[i] != 0) return 0;
			}
			break;
		case 3:
			for(i=0;i<ISNC_MAXSIZE;i++)	
			{
				if(rec_buffer.isNeedChange[i] != 0) return 0;
			}
			break;
	}
	return 1;
}

// 串口中断服务子程序
void USART1_IRQHandler(void)
{
	u8 Rec;
	// 串口中断类型：接收中断请求
	if(USART_GetFlagStatus(USART1, USART_IT_RXNE) != RESET)
	{
		Rec = USART_ReceiveData(USART1);					//(USART1->DR);	//读取接收到的数据  // USARTx_DR读操作可以将标志位 RXNE 清0
		
		if(Rec == 0xEE)										// 将 sbuf 数组的索引值清0
		{
			rec_buffer.buf_index = 0;
		}
		if(Rec == 0xEF)										// 将 isNeedChange 数组的索引值清0
		{
			rec_buffer.isnc_index = 0;
		}
		if(Rec == 0xFF)										// 将 buf 数组的索引值清0
		{
			rec_buffer.sbuf_index = 0;
		}
		
		
		if((Rec & 0xF0) == 0xF0 && (Rec != 0xFF))			// 存取印章的 id 值到 sbuf 数组
		{
			rec_buffer.sbuf[rec_buffer.sbuf_index] = Rec;
			rec_buffer.sbuf_index ++;		
			if(rec_buffer.sbuf_index > SBUF_MAXSIZE-1) rec_buffer.sbuf_index = 0;
		}
		if(Rec == 0xED)										// 收到 0xED(don't change) 表示下一次操作不用更换印章
		{
			rec_buffer.isNeedChange[rec_buffer.isnc_index] = 0;
			rec_buffer.isnc_index ++;						
			if(rec_buffer.isnc_index > ISNC_MAXSIZE-1) rec_buffer.isnc_index = 0;
		}
		if(Rec == 0xEC)										// 收到 0xEC(change) 表示下一次操作需要更换印章
		{
			rec_buffer.isNeedChange[rec_buffer.isnc_index] = 1;
			rec_buffer.isnc_index ++;						
			if(rec_buffer.isnc_index > ISNC_MAXSIZE-1) rec_buffer.isnc_index = 0;
		}
		if(Rec == 0xEB)										// 收到 0xEB(be obliged to) 表示最开始第一次操作一定需要更换印章
		{
			rec_buffer.isNeedChange[rec_buffer.isnc_index] = 11;
			rec_buffer.isnc_index ++;				
			if(rec_buffer.isnc_index > ISNC_MAXSIZE-1) rec_buffer.isnc_index = 0;
		}
		if(Rec <= 0xE9)										// 存取目标点坐标值到 buf 数组
		{
			rec_buffer.buf[rec_buffer.buf_index] = Rec;
			rec_buffer.buf_index ++;
			if(rec_buffer.buf_index > BUF_MAXSIZE-1) rec_buffer.buf_index = 0;	
		}
		if(Rec == 0xEA)
		{
			rec_buffer.isbufNull = 0;						// buf 数组收到数据，不为空
			rec_buffer.isbufNull = 0;						// sbuf 数组收到数据，不为空
			rec_buffer.isisncNull = 0;						// isNeedChange 数组收到数据不为空
		}
		
	}
}



