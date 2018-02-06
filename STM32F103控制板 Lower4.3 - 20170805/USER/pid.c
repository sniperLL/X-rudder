#include "pid.h"
#include "stm32f10x.h"

struct _pid
{
	float setSpeed;
	float actualSpeed;
	float actualSpeedPwm;
	float err;
	float err_next;
	float kp,ki,kd;
	float err_last;
} pid;

//结构体赋值存在问题故而直接用独立变量的形式做PID
float setSpeed = 0.0;
float actualSpeed = 0.0;
float actualSpeedPwm = 0.0;
float err = 0.0;
float err_next = 0.0;
float kp = 10,ki = 3,kd = 2;
float err_last = 0.0;

void PidInit(void)
{
	pid.setSpeed = 45.0;
	pid.actualSpeed = 0.0;
	pid.err = 0.0;
	pid.err_last = 0.0;
	pid.err_next = 0.0;
	//需要根据实际调整参数
	pid.kp = 0;
	pid.ki = 0;
	pid.kd = 0;
}
//此函数暂时不用
float MotorPid(float speed)
{
	float incrementSpeed = 0.0;
	pid.setSpeed = speed;
	pid.err = pid.setSpeed - pid.actualSpeed;
	incrementSpeed = pid.kp * (pid.err - pid.err_next) + pid.ki * pid.err + pid.kd * (pid.err - 2 * pid.err_next + pid.err_last);
	pid.actualSpeed += incrementSpeed;
	//根据实际的速度和PWM的对应函数转换出电机所需速度的PWM值
//	float speedToPwm = 0.0;
	pid.err_last = pid.err_next;
	pid.err_next = pid.err;
//	return speedToPwm;
	return pid.actualSpeed;
}

/*************************************************************
****函数名称：MotorPidContorl
****函数参数：speedErr  电机速度差
****函数功能：利用电机速度差值获取电机所需的PWM值控制电机转速
*************************************************************/
float MotorPidContorl(float speedErr)
{
	float incrementSpeedPwm = 0.0;
	pid.err = speedErr;
	incrementSpeedPwm = pid.kp * (pid.err - pid.err_next) + pid.ki * pid.err + pid.kd * (pid.err - 2 * pid.err_next + pid.err_last);
	pid.actualSpeedPwm += incrementSpeedPwm;
	if(pid.actualSpeedPwm > 1000)
		pid.actualSpeedPwm = 1000;
	else if (pid.actualSpeedPwm < 0)
		pid.actualSpeedPwm = 0;
	//根据实际的速度和PWM的对应函数转换出电机所需速度的PWM值
//	float speedToPwm = 0.0;
	pid.err_last = pid.err_next;
	pid.err_next = pid.err;
//	return speedToPwm;
	return pid.actualSpeedPwm;
}

float MotorPidCtl(float speedErr)
{
	float incrementSpeedPwm = 0.0;
	err = speedErr;
	incrementSpeedPwm = kp * (err - err_next) + ki * err + kd * (err - 2 * err_next + err_last);
	actualSpeedPwm += incrementSpeedPwm;
	if(actualSpeedPwm > 1000)
		actualSpeedPwm = 1000;
	else if (actualSpeedPwm < 0)
		actualSpeedPwm = 0;
	//根据实际的速度和PWM的对应函数转换出电机所需速度的PWM值
//	float speedToPwm = 0.0;
	err_last = err_next;
	err_next = err;
//	return speedToPwm;
	return actualSpeedPwm;
}
