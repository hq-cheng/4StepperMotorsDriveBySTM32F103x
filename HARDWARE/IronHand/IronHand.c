/*
 * luoliCHq
 */

#include "IronHand.h"
#include "delay.h"



// 二八步进电机转动，机械手旋转（7.2°/Steps,50Steps/r）
void Get_Seal_By(u16 Dir,u16 Speed,u16 Steps)
{
	int i,k=0;
	for(k=0;k<Steps;k++)
	{
		for(i=0;i<8;i++)
		{
			if(Dir == 1)
			{//顺时针			
				Set_Motor_CWPort(i);
			}
			if(Dir == 0)
			{//逆时针
				Set_Motor_CCWPort(i);
			}
			delay_us(Speed);
		}		
	}

}

void Set_Motor_CWPort(int k)
{
	switch(k)
	{
		case 0:
			MotorH_BitA = 0;MotorH_BitB=1;MotorH_BitC=0;MotorH_BitD=1;
			break;
		case 1:
			MotorH_BitA = 0;MotorH_BitB=0;MotorH_BitC=0;MotorH_BitD=1;
			break;
		case 2:	
			MotorH_BitA = 1;MotorH_BitB=0;MotorH_BitC=0;MotorH_BitD=1;
			break;
		case 3:	
			MotorH_BitA = 1;MotorH_BitB=0;MotorH_BitC=0;MotorH_BitD=0;
			break;
		case 4:	
			MotorH_BitA = 1;MotorH_BitB=0;MotorH_BitC=1;MotorH_BitD=0;
			break;
		case 5:
			MotorH_BitA = 0;MotorH_BitB=0;MotorH_BitC=1;MotorH_BitD=0;
			break;
		case 6:	
			MotorH_BitA = 0;MotorH_BitB=1;MotorH_BitC=1;MotorH_BitD=0;
			break;
		case 7:	
			MotorH_BitA = 0;MotorH_BitB=1;MotorH_BitC=0;MotorH_BitD=0;
			break;
	}
}

void Set_Motor_CCWPort(int k)
{
	switch(k)
	{
		case 0:
			MotorH_BitA = 0;MotorH_BitB=1;MotorH_BitC=0;MotorH_BitD=0;
			break;
		case 1:
			MotorH_BitA = 0;MotorH_BitB=1;MotorH_BitC=1;MotorH_BitD=0;
			break;
		case 2:	
			MotorH_BitA = 0;MotorH_BitB=0;MotorH_BitC=1;MotorH_BitD=0;
			break;
		case 3:	
			MotorH_BitA = 1;MotorH_BitB=0;MotorH_BitC=1;MotorH_BitD=0;
			break;
		case 4:	
			MotorH_BitA = 1;MotorH_BitB=0;MotorH_BitC=0;MotorH_BitD=0;
			break;
		case 5:
			MotorH_BitA = 1;MotorH_BitB=0;MotorH_BitC=0;MotorH_BitD=1;
			break;
		case 6:	
			MotorH_BitA = 0;MotorH_BitB=0;MotorH_BitC=0;MotorH_BitD=1;
			break;
		case 7:	
			MotorH_BitA = 0;MotorH_BitB=1;MotorH_BitC=0;MotorH_BitD=1;
			break;
	}
}

