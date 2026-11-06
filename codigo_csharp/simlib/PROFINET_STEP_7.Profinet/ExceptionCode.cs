namespace PROFINET_STEP_7.Profinet;

public enum ExceptionCode
{
	ExceptionNo = 0,
	WrongCPU_Type = 1,
	ConnectionError = 2,
	IPAdressNotAvailable = 3,
	WrongVariableFormat = 10,
	WrongNumberReceivedBytes = 11,
	SendData = 20,
	ReadData = 30,
	WriteData = 50
}
