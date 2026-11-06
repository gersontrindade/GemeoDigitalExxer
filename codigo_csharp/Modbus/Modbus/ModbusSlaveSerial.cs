using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace Modbus;

public sealed class ModbusSlaveSerial : ModbusSlave
{
	private SerialPort sp;

	public ModbusSlaveSerial(Datastore[] modbus_db, ModbusSerialType type, string port, int baudrate, int databits, Parity parity, StopBits stopbits, Handshake handshake)
		: base(modbus_db)
	{
		switch (type)
		{
		case ModbusSerialType.ASCII:
			connection_type = ConnectionType.SERIAL_ASCII;
			break;
		case ModbusSerialType.RTU:
			connection_type = ConnectionType.SERIAL_RTU;
			break;
		}
		sp = new SerialPort(port, baudrate, parity, databits, stopbits);
		sp.Handshake = handshake;
		interframe_delay = GetInterframeDelay(sp);
		interchar_delay = GetIntercharDelay(sp);
	}

	protected override void GestRequests()
	{
		try
		{
			List<byte> send_buffer = new List<byte>();
			List<byte> receive_buffer = new List<byte>();
			while (run)
			{
				IncomingMessagePolling(send_buffer, receive_buffer, sp);
				Thread.Sleep(1);
			}
		}
		catch
		{
		}
	}

	public override void StartListen()
	{
		sp.Open();
		if (sp.IsOpen)
		{
			sp.DiscardInBuffer();
			sp.DiscardOutBuffer();
			if (gest_request == null)
			{
				run = true;
				gest_request = new Thread(GestRequests);
				gest_request.Start();
			}
		}
	}

	public override void StopListen()
	{
		if (gest_request != null)
		{
			run = false;
			gest_request.Join();
			gest_request = null;
		}
		if (sp != null && sp.IsOpen)
		{
			sp.Close();
		}
	}
}
