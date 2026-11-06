using System.IO.Ports;

namespace Modbus;

public sealed class ModbusMasterSerial : ModbusMaster
{
	private SerialPort sp;

	private long char_tmo;

	public ModbusMasterSerial(ModbusSerialType type, string port, int baudrate, int databits, Parity parity, StopBits stopbits, Handshake handshake)
	{
		switch (type)
		{
		case ModbusSerialType.RTU:
			connection_type = ConnectionType.SERIAL_RTU;
			break;
		case ModbusSerialType.ASCII:
			connection_type = ConnectionType.SERIAL_ASCII;
			break;
		}
		sp = new SerialPort(port, baudrate, parity, databits, stopbits);
		sp.Handshake = handshake;
		interframe_delay = GetInterframeDelay(sp);
		interchar_delay = GetIntercharDelay(sp);
	}

	public override void Connect()
	{
		sp.Open();
		if (sp.IsOpen)
		{
			sp.DiscardInBuffer();
			sp.DiscardOutBuffer();
			connected = true;
		}
	}

	public override void Disconnect()
	{
		sp.Close();
		connected = false;
	}

	protected override void Send()
	{
		sp.Write(send_buffer.ToArray(), 0, send_buffer.Count);
		char_tmo = 0L;
	}

	protected override int ReceiveByte()
	{
		bool flag = false;
		if (!sw_ch.IsRunning)
		{
			sw_ch.Start();
		}
		int result;
		do
		{
			if (sp.BytesToRead > 0)
			{
				result = sp.ReadByte();
				flag = true;
			}
			else
			{
				result = -1;
			}
		}
		while (!flag && sw_ch.ElapsedMilliseconds - char_tmo < interchar_delay);
		if (flag)
		{
			char_tmo = sw_ch.ElapsedMilliseconds;
		}
		return result;
	}
}
