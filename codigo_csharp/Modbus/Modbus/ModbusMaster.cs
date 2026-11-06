using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Modbus;

public abstract class ModbusMaster : ModbusBase
{
	protected bool connected;

	protected List<byte> send_buffer = new List<byte>();

	protected List<byte> receive_buffer = new List<byte>();

	protected ushort transaction_id;

	protected Stopwatch sw_ch;

	public bool Connected => connected;

	public ModbusMaster()
	{
		device_type = DeviceType.MASTER;
		sw_ch = new Stopwatch();
	}

	public abstract void Connect();

	public abstract void Disconnect();

	protected abstract void Send();

	protected abstract int ReceiveByte();

	protected void InitTCPUDPMasterMessage()
	{
		transaction_id++;
		send_buffer.Clear();
	}

	protected void BuildMBAPHeader(byte dest_address, ushort message_len)
	{
		send_buffer.InsertRange(0, GetBytes(transaction_id));
		send_buffer.InsertRange(2, GetBytes(0));
		send_buffer.InsertRange(4, GetBytes(message_len));
		send_buffer.Insert(6, dest_address);
	}

	protected void Query(byte unit_id, ushort msg_len)
	{
		error = Errors.NO_ERROR;
		switch (connection_type)
		{
		case ConnectionType.SERIAL_ASCII:
		{
			send_buffer.Insert(0, unit_id);
			byte[] aSCIIBytesFromBinaryBuffer = GetASCIIBytesFromBinaryBuffer(new byte[1] { LRC.CalcLRC(send_buffer.ToArray(), 0, send_buffer.Count) });
			send_buffer = GetASCIIBytesFromBinaryBuffer(send_buffer.ToArray()).ToList();
			send_buffer.AddRange(aSCIIBytesFromBinaryBuffer);
			send_buffer.Insert(0, Encoding.ASCII.GetBytes(new char[1] { ':' }).First());
			char[] chars = new char[2] { '\r', '\n' };
			send_buffer.AddRange(Encoding.ASCII.GetBytes(chars));
			break;
		}
		case ConnectionType.SERIAL_RTU:
			send_buffer.Insert(0, unit_id);
			send_buffer.AddRange(BitConverter.GetBytes(CRC16.CalcCRC16(send_buffer.ToArray(), 0, send_buffer.Count)));
			Thread.Sleep(interframe_delay);
			break;
		case ConnectionType.TCP_IP:
		case ConnectionType.UDP_IP:
			BuildMBAPHeader(unit_id, msg_len);
			break;
		}
		Send();
		receive_buffer.Clear();
		bool flag = false;
		bool flag2 = false;
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		long elapsedMilliseconds;
		do
		{
			int num = ReceiveByte();
			if (num > -1)
			{
				if (!flag2)
				{
					flag2 = true;
				}
				if (connection_type == ConnectionType.SERIAL_ASCII && (byte)num == Encoding.ASCII.GetBytes(new char[1] { ':' }).First())
				{
					receive_buffer.Clear();
				}
				receive_buffer.Add((byte)num);
			}
			else if (num == -1 && flag2)
			{
				flag = true;
			}
			elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
		}
		while (!flag && rx_timeout > elapsedMilliseconds);
		sw_ch.Stop();
		stopwatch.Stop();
		if (elapsedMilliseconds >= rx_timeout)
		{
			error = Errors.RX_TIMEOUT;
			return;
		}
		int num2;
		switch (connection_type)
		{
		default:
			num2 = 5;
			break;
		case ConnectionType.SERIAL_ASCII:
			num2 = 11;
			break;
		case ConnectionType.TCP_IP:
		case ConnectionType.UDP_IP:
			num2 = 9;
			break;
		}
		if (receive_buffer.Count < num2)
		{
			error = Errors.WRONG_MESSAGE_LEN;
			return;
		}
		switch (connection_type)
		{
		case ConnectionType.SERIAL_ASCII:
		{
			if (receive_buffer[0] != send_buffer[0])
			{
				error = Errors.START_CHAR_NOT_FOUND;
				return;
			}
			receive_buffer.RemoveRange(0, 1);
			char[] first = new char[2] { '\r', '\n' };
			char[] chars2 = Encoding.ASCII.GetChars(receive_buffer.GetRange(receive_buffer.Count - 2, 2).ToArray());
			if (!first.SequenceEqual(chars2))
			{
				error = Errors.END_CHARS_NOT_FOUND;
				break;
			}
			receive_buffer.RemoveRange(receive_buffer.Count - 2, 2);
			receive_buffer = GetBinaryBufferFromASCIIBytes(receive_buffer.ToArray()).ToList();
			byte b3 = LRC.CalcLRC(receive_buffer.ToArray(), 0, receive_buffer.Count - 1);
			byte b4 = receive_buffer[receive_buffer.Count - 1];
			if (b3 != b4)
			{
				error = Errors.WRONG_LRC;
				break;
			}
			receive_buffer.RemoveRange(receive_buffer.Count - 1, 1);
			receive_buffer.RemoveRange(0, 1);
			break;
		}
		case ConnectionType.SERIAL_RTU:
		{
			ushort num5 = CRC16.CalcCRC16(receive_buffer.ToArray(), 0, receive_buffer.Count - 2);
			ushort num6 = BitConverter.ToUInt16(receive_buffer.ToArray(), receive_buffer.Count - 2);
			if (num6 != num5)
			{
				error = Errors.WRONG_CRC;
				return;
			}
			byte b2 = receive_buffer[0];
			if (b2 != send_buffer[0])
			{
				error = Errors.WRONG_RESPONSE_ADDRESS;
				return;
			}
			receive_buffer.RemoveRange(0, 1);
			receive_buffer.RemoveRange(receive_buffer.Count - 2, 2);
			break;
		}
		case ConnectionType.TCP_IP:
		case ConnectionType.UDP_IP:
		{
			ushort num3 = ToUInt16(receive_buffer.ToArray(), 0);
			if (num3 != transaction_id)
			{
				error = Errors.WRONG_TRANSACTION_ID;
				return;
			}
			if (ToUInt16(receive_buffer.ToArray(), 2) != 0)
			{
				error = Errors.WRONG_TRANSACTION_ID;
				return;
			}
			ushort num4 = ToUInt16(receive_buffer.ToArray(), 4);
			if (receive_buffer.Count - 7 + 1 < num4)
			{
				error = Errors.WRONG_MESSAGE_LEN;
				return;
			}
			byte b = receive_buffer[6];
			if (b != send_buffer[6])
			{
				error = Errors.WRONG_RESPONSE_UNIT_ID;
				return;
			}
			receive_buffer.RemoveRange(0, 7);
			break;
		}
		}
		if (receive_buffer[0] > 128)
		{
			switch (receive_buffer[1])
			{
			case 1:
				error = Errors.EXCEPTION_ILLEGAL_FUNCTION;
				break;
			case 2:
				error = Errors.EXCEPTION_ILLEGAL_DATA_ADDRESS;
				break;
			case 3:
				error = Errors.EXCEPTION_ILLEGAL_DATA_VALUE;
				break;
			case 4:
				error = Errors.EXCEPTION_SLAVE_DEVICE_FAILURE;
				break;
			}
		}
	}

	public bool[] ReadCoils(byte unit_id, ushort start_address, ushort len)
	{
		if (len < 1)
		{
			error = Errors.ZERO_REGISTERS_REQUESTED;
			return null;
		}
		if (len > 2000)
		{
			error = Errors.TOO_MANY_REGISTERS_REQUESTED;
			return null;
		}
		ushort msg_len = 6;
		InitTCPUDPMasterMessage();
		send_buffer.Add(1);
		send_buffer.AddRange(GetBytes(start_address));
		send_buffer.AddRange(GetBytes(len));
		Query(unit_id, msg_len);
		if (error != Errors.NO_ERROR)
		{
			return null;
		}
		BitArray bitArray = new BitArray(receive_buffer.GetRange(2, receive_buffer.Count - 2).ToArray());
		bool[] array = new bool[bitArray.Count];
		bitArray.CopyTo(array, 0);
		return array;
	}

	public bool[] ReadDiscreteInputs(byte unit_id, ushort start_address, ushort len)
	{
		if (len < 1)
		{
			error = Errors.ZERO_REGISTERS_REQUESTED;
			return null;
		}
		if (len > 2000)
		{
			error = Errors.TOO_MANY_REGISTERS_REQUESTED;
			return null;
		}
		ushort msg_len = 6;
		InitTCPUDPMasterMessage();
		send_buffer.Add(2);
		send_buffer.AddRange(GetBytes(start_address));
		send_buffer.AddRange(GetBytes(len));
		Query(unit_id, msg_len);
		if (error != Errors.NO_ERROR)
		{
			return null;
		}
		BitArray bitArray = new BitArray(receive_buffer.GetRange(2, receive_buffer.Count - 2).ToArray());
		bool[] array = new bool[bitArray.Count];
		bitArray.CopyTo(array, 0);
		return array;
	}

	public ushort[] ReadHoldingRegisters(byte unit_id, ushort start_address, ushort len)
	{
		if (len < 1)
		{
			error = Errors.ZERO_REGISTERS_REQUESTED;
			return null;
		}
		if (len > 125)
		{
			error = Errors.TOO_MANY_REGISTERS_REQUESTED;
			return null;
		}
		ushort msg_len = 6;
		InitTCPUDPMasterMessage();
		send_buffer.Add(3);
		send_buffer.AddRange(GetBytes(start_address));
		send_buffer.AddRange(GetBytes(len));
		Query(unit_id, msg_len);
		if (error != Errors.NO_ERROR)
		{
			return null;
		}
		List<ushort> list = new List<ushort>();
		for (int i = 0; i < receive_buffer[1]; i += 2)
		{
			list.Add(ToUInt16(receive_buffer.ToArray(), i + 2));
		}
		return list.ToArray();
	}

	public ushort[] ReadInputRegisters(byte unit_id, ushort start_address, ushort len)
	{
		if (len < 1)
		{
			error = Errors.ZERO_REGISTERS_REQUESTED;
			return null;
		}
		if (len > 125)
		{
			error = Errors.TOO_MANY_REGISTERS_REQUESTED;
			return null;
		}
		ushort msg_len = 6;
		InitTCPUDPMasterMessage();
		send_buffer.Add(4);
		send_buffer.AddRange(GetBytes(start_address));
		send_buffer.AddRange(GetBytes(len));
		Query(unit_id, msg_len);
		if (error != Errors.NO_ERROR)
		{
			return null;
		}
		List<ushort> list = new List<ushort>();
		for (int i = 0; i < receive_buffer[1]; i += 2)
		{
			list.Add(ToUInt16(receive_buffer.ToArray(), i + 2));
		}
		return list.ToArray();
	}

	public void WriteSingleCoil(byte unit_id, ushort address, bool value)
	{
		ushort msg_len = 6;
		InitTCPUDPMasterMessage();
		send_buffer.Add(5);
		send_buffer.AddRange(GetBytes(address));
		send_buffer.AddRange(GetBytes((ushort)(value ? 65280u : 0u)));
		Query(unit_id, msg_len);
		if (error == Errors.NO_ERROR)
		{
			ushort num = ToUInt16(receive_buffer.ToArray(), 1);
			ushort num2 = ToUInt16(receive_buffer.ToArray(), 3);
			if (num != address)
			{
				error = Errors.WRONG_RESPONSE_ADDRESS;
			}
			else if (num2 == 65280 && !value)
			{
				error = Errors.WRONG_RESPONSE_VALUE;
			}
		}
	}

	public void WriteSingleRegister(byte unit_id, ushort address, ushort value)
	{
		ushort msg_len = 6;
		InitTCPUDPMasterMessage();
		send_buffer.Add(6);
		send_buffer.AddRange(GetBytes(address));
		send_buffer.AddRange(GetBytes(value));
		Query(unit_id, msg_len);
		if (error == Errors.NO_ERROR)
		{
			ushort num = ToUInt16(receive_buffer.ToArray(), 1);
			if (num != address)
			{
				error = Errors.WRONG_RESPONSE_ADDRESS;
			}
		}
	}

	public void WriteMultipleCoils(byte unit_id, ushort start_address, bool[] values)
	{
		if (values == null)
		{
			error = Errors.ZERO_REGISTERS_REQUESTED;
			return;
		}
		if (values.Length < 1)
		{
			error = Errors.ZERO_REGISTERS_REQUESTED;
			return;
		}
		if (values.Length > 1968)
		{
			error = Errors.TOO_MANY_REGISTERS_REQUESTED;
			return;
		}
		byte b = (byte)(values.Length / 8 + ((values.Length % 8 != 0) ? 1 : 0));
		ushort msg_len = (ushort)(7 + b);
		byte[] array = new byte[b];
		BitArray bitArray = new BitArray(values);
		bitArray.CopyTo(array, 0);
		InitTCPUDPMasterMessage();
		send_buffer.Add(15);
		send_buffer.AddRange(GetBytes(start_address));
		send_buffer.AddRange(GetBytes((ushort)values.Length));
		send_buffer.Add(b);
		send_buffer.AddRange(array);
		Query(unit_id, msg_len);
		if (error == Errors.NO_ERROR)
		{
			ushort num = ToUInt16(receive_buffer.ToArray(), 1);
			ushort num2 = ToUInt16(receive_buffer.ToArray(), 3);
			if (num != start_address)
			{
				error = Errors.WRONG_RESPONSE_ADDRESS;
			}
			else if (num2 != values.Length)
			{
				error = Errors.WRONG_RESPONSE_REGISTERS;
			}
		}
	}

	public void WriteMultipleRegisters(byte unit_id, ushort start_address, ushort[] values)
	{
		if (values == null)
		{
			error = Errors.ZERO_REGISTERS_REQUESTED;
			return;
		}
		if (values.Length < 1)
		{
			error = Errors.ZERO_REGISTERS_REQUESTED;
			return;
		}
		if (values.Length > 123)
		{
			error = Errors.TOO_MANY_REGISTERS_REQUESTED;
			return;
		}
		ushort msg_len = (ushort)(7 + values.Length * 2);
		InitTCPUDPMasterMessage();
		send_buffer.Add(16);
		send_buffer.AddRange(GetBytes(start_address));
		send_buffer.AddRange(GetBytes((ushort)values.Length));
		send_buffer.Add((byte)(values.Length * 2));
		for (int i = 0; i < values.Length; i++)
		{
			send_buffer.AddRange(GetBytes(values[i]));
		}
		Query(unit_id, msg_len);
		if (error == Errors.NO_ERROR)
		{
			ushort num = ToUInt16(receive_buffer.ToArray(), 1);
			ushort num2 = ToUInt16(receive_buffer.ToArray(), 3);
			if (num != start_address)
			{
				error = Errors.WRONG_RESPONSE_ADDRESS;
			}
			else if (num2 != values.Length)
			{
				error = Errors.WRONG_RESPONSE_REGISTERS;
			}
		}
	}

	public void MaskWriteRegister(byte unit_id, ushort address, ushort and_mask, ushort or_mask)
	{
		ushort msg_len = 8;
		InitTCPUDPMasterMessage();
		send_buffer.Add(22);
		send_buffer.AddRange(GetBytes(address));
		send_buffer.AddRange(GetBytes(and_mask));
		send_buffer.AddRange(GetBytes(or_mask));
		Query(unit_id, msg_len);
		if (error != Errors.NO_ERROR)
		{
			return;
		}
		ushort num = ToUInt16(receive_buffer.ToArray(), 1);
		if (address != num)
		{
			error = Errors.WRONG_RESPONSE_ADDRESS;
			return;
		}
		ushort num2 = ToUInt16(receive_buffer.ToArray(), 3);
		if (and_mask != num2)
		{
			error = Errors.WRONG_RESPONSE_AND_MASK;
			return;
		}
		ushort num3 = ToUInt16(receive_buffer.ToArray(), 5);
		if (or_mask != num3)
		{
			error = Errors.WRONG_RESPONSE_OR_MASK;
		}
	}

	public ushort[] ReadWriteMultipleRegisters(byte unit_id, ushort read_start_address, ushort read_len, ushort write_start_address, ushort[] values)
	{
		if (values == null)
		{
			error = Errors.ZERO_REGISTERS_REQUESTED;
			return null;
		}
		if (read_len < 1 || values.Length < 1)
		{
			error = Errors.ZERO_REGISTERS_REQUESTED;
			return null;
		}
		if (read_len > 125 || values.Length > 121)
		{
			error = Errors.TOO_MANY_REGISTERS_REQUESTED;
			return null;
		}
		ushort msg_len = (ushort)(11 + values.Length * 2);
		InitTCPUDPMasterMessage();
		send_buffer.Add(23);
		send_buffer.AddRange(GetBytes(read_start_address));
		send_buffer.AddRange(GetBytes(read_len));
		send_buffer.AddRange(GetBytes(write_start_address));
		send_buffer.AddRange(GetBytes((ushort)values.Length));
		send_buffer.Add((byte)(values.Length * 2));
		for (int i = 0; i < values.Length; i++)
		{
			send_buffer.AddRange(GetBytes(values[i]));
		}
		Query(unit_id, msg_len);
		if (error != Errors.NO_ERROR)
		{
			return null;
		}
		List<ushort> list = new List<ushort>();
		for (int j = 0; j < receive_buffer[1]; j += 2)
		{
			list.Add(ToUInt16(receive_buffer.ToArray(), j + 2));
		}
		return list.ToArray();
	}
}
