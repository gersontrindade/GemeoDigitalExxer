using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Modbus;

public abstract class ModbusSlave : ModbusBase
{
	protected volatile bool run;

	private long char_tmo;

	protected Datastore[] modbus_db;

	protected Thread gest_request;

	private Stopwatch sw_ch;

	public Datastore[] ModbusDB
	{
		get
		{
			return modbus_db;
		}
		set
		{
			modbus_db = value;
		}
	}

	public ModbusSlave(Datastore[] ds)
	{
		device_type = DeviceType.SLAVE;
		modbus_db = ds;
		sw_ch = new Stopwatch();
	}

	protected abstract void GestRequests();

	public abstract void StartListen();

	public abstract void StopListen();

	private bool IsAllRegistersPresent(byte unit_id, ModbusDBTables table, ushort starting_address, ushort quantity_of_registers)
	{
		bool result = true;
		switch (table)
		{
		case ModbusDBTables.DISCRETE_INPUTS_REGISTERS:
			try
			{
				modbus_db.Single((Datastore x) => x.UnitID == unit_id).DiscreteInputs.ToList().GetRange(starting_address, quantity_of_registers);
			}
			catch
			{
				result = false;
			}
			break;
		case ModbusDBTables.COIL_REGISTERS:
			try
			{
				modbus_db.Single((Datastore x) => x.UnitID == unit_id).Coils.ToList().GetRange(starting_address, quantity_of_registers);
			}
			catch
			{
				result = false;
			}
			break;
		case ModbusDBTables.INPUT_REGISTERS:
			try
			{
				modbus_db.Single((Datastore x) => x.UnitID == unit_id).InputRegisters.ToList().GetRange(starting_address, quantity_of_registers);
			}
			catch
			{
				result = false;
			}
			break;
		case ModbusDBTables.HOLDING_REGISTERS:
			try
			{
				modbus_db.Single((Datastore x) => x.UnitID == unit_id).HoldingRegisters.ToList().GetRange(starting_address, quantity_of_registers);
			}
			catch
			{
				result = false;
			}
			break;
		default:
			result = false;
			break;
		}
		return result;
	}

	private void BuildExceptionMessage(List<byte> send_buffer, ModbusCodes code, Errors error)
	{
		byte item = 0;
		send_buffer.Clear();
		switch (error)
		{
		case Errors.EXCEPTION_ILLEGAL_FUNCTION:
			item = 1;
			break;
		case Errors.EXCEPTION_ILLEGAL_DATA_ADDRESS:
			item = 2;
			break;
		case Errors.EXCEPTION_ILLEGAL_DATA_VALUE:
			item = 3;
			break;
		case Errors.EXCEPTION_SLAVE_DEVICE_FAILURE:
			item = 4;
			break;
		}
		send_buffer.Add((byte)(128 + (byte)code));
		send_buffer.Add(item);
	}

	private int ReadByte(object flux)
	{
		int result = -1;
		switch (connection_type)
		{
		case ConnectionType.UDP_IP:
		{
			UDPData uDPData = (UDPData)flux;
			if (uDPData.Length > 0)
			{
				result = uDPData.ReadByte();
			}
			break;
		}
		case ConnectionType.TCP_IP:
		{
			NetworkStream networkStream = (NetworkStream)flux;
			if (networkStream.DataAvailable)
			{
				result = networkStream.ReadByte();
			}
			break;
		}
		case ConnectionType.SERIAL_RTU:
		case ConnectionType.SERIAL_ASCII:
		{
			SerialPort serialPort = (SerialPort)flux;
			bool flag = false;
			if (!sw_ch.IsRunning)
			{
				sw_ch.Start();
			}
			do
			{
				if (serialPort.BytesToRead > 0)
				{
					result = serialPort.ReadByte();
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
			break;
		}
		}
		return result;
	}

	private void WriteBuffer(object flux, byte[] buffer, int offset, int size)
	{
		switch (connection_type)
		{
		case ConnectionType.UDP_IP:
		{
			UDPData uDPData = (UDPData)flux;
			uDPData.WriteResp(buffer, offset, size);
			break;
		}
		case ConnectionType.TCP_IP:
		{
			NetworkStream networkStream = (NetworkStream)flux;
			networkStream.Write(buffer, offset, size);
			break;
		}
		case ConnectionType.SERIAL_RTU:
		case ConnectionType.SERIAL_ASCII:
		{
			SerialPort serialPort = (SerialPort)flux;
			serialPort.Write(buffer, offset, size);
			break;
		}
		}
	}

	protected void IncomingMessagePolling(List<byte> send_buffer, List<byte> receive_buffer, object flux)
	{
		ushort transaction_id = 0;
		byte unit_id = 0;
		Stopwatch stopwatch = new Stopwatch();
		error = Errors.NO_ERROR;
		receive_buffer.Clear();
		int num = -1;
		stopwatch.Start();
		bool flag = false;
		bool flag2 = false;
		char_tmo = 0L;
		long elapsedMilliseconds;
		do
		{
			num = ReadByte(flux);
			if (num != -1)
			{
				if (!flag)
				{
					flag = true;
				}
				if (connection_type == ConnectionType.SERIAL_ASCII && (byte)num == Encoding.ASCII.GetBytes(new char[1] { ':' }).First())
				{
					receive_buffer.Clear();
				}
				receive_buffer.Add((byte)num);
			}
			else if (num == -1 && flag)
			{
				flag2 = true;
			}
			else
			{
				Thread.Sleep(1);
			}
			elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
		}
		while (elapsedMilliseconds < rx_timeout && run && !flag2);
		sw_ch.Stop();
		stopwatch.Stop();
		if (!run)
		{
			error = Errors.THREAD_BLOCK_REQUEST;
			return;
		}
		if (elapsedMilliseconds >= rx_timeout)
		{
			error = Errors.RX_TIMEOUT;
			return;
		}
		if (receive_buffer.Count < 3)
		{
			error = Errors.WRONG_MESSAGE_LEN;
			return;
		}
		switch (connection_type)
		{
		case ConnectionType.SERIAL_ASCII:
		{
			if (Encoding.ASCII.GetChars(receive_buffer.ToArray()).FirstOrDefault() != ':')
			{
				error = Errors.START_CHAR_NOT_FOUND;
				return;
			}
			receive_buffer.RemoveAt(0);
			char[] first = new char[2] { '\r', '\n' };
			char[] chars = Encoding.ASCII.GetChars(receive_buffer.GetRange(receive_buffer.Count - 2, 2).ToArray());
			if (!first.SequenceEqual(chars))
			{
				error = Errors.END_CHARS_NOT_FOUND;
				return;
			}
			receive_buffer.RemoveRange(receive_buffer.Count - 2, 2);
			receive_buffer = GetBinaryBufferFromASCIIBytes(receive_buffer.ToArray()).ToList();
			byte b = receive_buffer[receive_buffer.Count - 1];
			byte b2 = LRC.CalcLRC(receive_buffer.ToArray(), 0, receive_buffer.Count - 1);
			if (b != b2)
			{
				error = Errors.WRONG_LRC;
				return;
			}
			receive_buffer.RemoveAt(receive_buffer.Count - 1);
			unit_id = receive_buffer[0];
			if (!modbus_db.Any((Datastore x) => x.UnitID == unit_id))
			{
				return;
			}
			receive_buffer.RemoveAt(0);
			break;
		}
		case ConnectionType.SERIAL_RTU:
		{
			ushort num3 = BitConverter.ToUInt16(receive_buffer.ToArray(), receive_buffer.Count - 2);
			ushort num4 = CRC16.CalcCRC16(receive_buffer.ToArray(), 0, receive_buffer.Count - 2);
			if (num3 != num4)
			{
				error = Errors.WRONG_CRC;
				return;
			}
			unit_id = receive_buffer[0];
			if (!modbus_db.Any((Datastore x) => x.UnitID == unit_id))
			{
				return;
			}
			receive_buffer.RemoveRange(0, 1);
			receive_buffer.RemoveRange(receive_buffer.Count - 2, 2);
			break;
		}
		case ConnectionType.TCP_IP:
		case ConnectionType.UDP_IP:
		{
			transaction_id = ToUInt16(receive_buffer.ToArray(), 0);
			if (ToUInt16(receive_buffer.ToArray(), 2) != 0)
			{
				error = Errors.WRONG_PROTOCOL_ID;
				return;
			}
			ushort num2 = ToUInt16(receive_buffer.ToArray(), 4);
			if (receive_buffer.Count - 6 != num2)
			{
				error = Errors.WRONG_MESSAGE_LEN;
				return;
			}
			unit_id = receive_buffer[6];
			if (!modbus_db.Any((Datastore x) => x.UnitID == unit_id))
			{
				return;
			}
			receive_buffer.RemoveRange(0, 7);
			break;
		}
		}
		AdjAndReply(send_buffer, receive_buffer, flux, unit_id, transaction_id);
	}

	private void AdjAndReply(List<byte> send_buffer, List<byte> receive_buffer, object flux, byte unit_id, ushort transaction_id)
	{
		send_buffer.Clear();
		ModbusCodes modbusCodes = (ModbusCodes)receive_buffer[0];
		switch (modbusCodes)
		{
		case ModbusCodes.READ_COILS:
		{
			ushort num = ToUInt16(receive_buffer.ToArray(), 1);
			ushort num2 = ToUInt16(receive_buffer.ToArray(), 3);
			if (modbus_db.Single((Datastore x) => x.UnitID == unit_id).Coils.Length == 0)
			{
				error = Errors.EXCEPTION_ILLEGAL_FUNCTION;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (num2 < 1 || num2 > 2000)
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_VALUE;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (!IsAllRegistersPresent(unit_id, ModbusDBTables.COIL_REGISTERS, num, num2))
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_ADDRESS;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			send_buffer.Add(1);
			int num9 = num2 / 8 + ((num2 % 8 != 0) ? 1 : 0);
			send_buffer.Add((byte)num9);
			bool[] array = new bool[num9 * 8];
			try
			{
				modbus_db.Single((Datastore x) => x.UnitID == unit_id).Coils.ToList().GetRange(num, num2).CopyTo(array);
			}
			catch
			{
				BuildExceptionMessage(send_buffer, modbusCodes, Errors.EXCEPTION_SLAVE_DEVICE_FAILURE);
				break;
			}
			for (int num12 = 0; num12 < num9; num12++)
			{
				send_buffer.Add(EightBitToByte(array, num12 * 8));
			}
			break;
		}
		case ModbusCodes.READ_DISCRETE_INPUTS:
		{
			ushort num = ToUInt16(receive_buffer.ToArray(), 1);
			ushort num2 = ToUInt16(receive_buffer.ToArray(), 3);
			if (modbus_db.Single((Datastore x) => x.UnitID == unit_id).DiscreteInputs.Length == 0)
			{
				error = Errors.EXCEPTION_ILLEGAL_FUNCTION;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (num2 < 1 || num2 > 2000)
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_VALUE;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (!IsAllRegistersPresent(unit_id, ModbusDBTables.DISCRETE_INPUTS_REGISTERS, num, num2))
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_ADDRESS;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			send_buffer.Add(2);
			int num9 = num2 / 8 + ((num2 % 8 != 0) ? 1 : 0);
			send_buffer.Add((byte)num9);
			bool[] array = new bool[num9 * 8];
			try
			{
				modbus_db.Single((Datastore x) => x.UnitID == unit_id).DiscreteInputs.ToList().GetRange(num, num2).CopyTo(array);
			}
			catch
			{
				BuildExceptionMessage(send_buffer, modbusCodes, Errors.EXCEPTION_SLAVE_DEVICE_FAILURE);
				break;
			}
			for (int num10 = 0; num10 < num9; num10++)
			{
				send_buffer.Add(EightBitToByte(array, num10 * 8));
			}
			break;
		}
		case ModbusCodes.READ_HOLDING_REGISTERS:
		{
			ushort num = ToUInt16(receive_buffer.ToArray(), 1);
			ushort num2 = ToUInt16(receive_buffer.ToArray(), 3);
			if (modbus_db.Single((Datastore x) => x.UnitID == unit_id).HoldingRegisters.Length == 0)
			{
				error = Errors.EXCEPTION_ILLEGAL_FUNCTION;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (num2 < 1 || num2 > 125)
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_VALUE;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (!IsAllRegistersPresent(unit_id, ModbusDBTables.HOLDING_REGISTERS, num, num2))
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_ADDRESS;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			send_buffer.Add(3);
			send_buffer.Add((byte)(2 * num2));
			try
			{
				for (int num13 = 0; num13 < num2 * 2; num13 += 2)
				{
					send_buffer.AddRange(GetBytes(modbus_db.Single((Datastore x) => x.UnitID == unit_id).HoldingRegisters[num + num13 / 2]));
				}
			}
			catch
			{
				BuildExceptionMessage(send_buffer, modbusCodes, Errors.EXCEPTION_SLAVE_DEVICE_FAILURE);
			}
			break;
		}
		case ModbusCodes.READ_INPUT_REGISTERS:
		{
			ushort num = ToUInt16(receive_buffer.ToArray(), 1);
			ushort num2 = ToUInt16(receive_buffer.ToArray(), 3);
			if (modbus_db.Single((Datastore x) => x.UnitID == unit_id).InputRegisters.Length == 0)
			{
				error = Errors.EXCEPTION_ILLEGAL_FUNCTION;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (num2 < 1 || num2 > 125)
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_VALUE;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (!IsAllRegistersPresent(unit_id, ModbusDBTables.INPUT_REGISTERS, num, num2))
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_ADDRESS;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			send_buffer.Add(4);
			send_buffer.Add((byte)(2 * num2));
			try
			{
				for (int num16 = 0; num16 < num2 * 2; num16 += 2)
				{
					send_buffer.AddRange(GetBytes(modbus_db.Single((Datastore x) => x.UnitID == unit_id).InputRegisters[num + num16 / 2]));
				}
			}
			catch
			{
				BuildExceptionMessage(send_buffer, modbusCodes, Errors.EXCEPTION_SLAVE_DEVICE_FAILURE);
			}
			break;
		}
		case ModbusCodes.WRITE_SINGLE_COIL:
		{
			ushort num = ToUInt16(receive_buffer.ToArray(), 1);
			ushort num11 = ToUInt16(receive_buffer.ToArray(), 3);
			bool flag;
			switch (num11)
			{
			case 0:
				flag = false;
				break;
			case 65280:
				flag = true;
				break;
			default:
				error = Errors.EXCEPTION_ILLEGAL_DATA_VALUE;
				flag = false;
				break;
			}
			if (modbus_db.Single((Datastore x) => x.UnitID == unit_id).Coils.Length == 0)
			{
				error = Errors.EXCEPTION_ILLEGAL_FUNCTION;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (error == Errors.EXCEPTION_ILLEGAL_DATA_VALUE)
			{
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (!IsAllRegistersPresent(unit_id, ModbusDBTables.COIL_REGISTERS, num, 1))
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_ADDRESS;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (error == Errors.WRONG_WRITE_SINGLE_COIL_VALUE)
			{
				BuildExceptionMessage(send_buffer, modbusCodes, Errors.EXCEPTION_ILLEGAL_DATA_VALUE);
				break;
			}
			try
			{
				modbus_db.Single((Datastore x) => x.UnitID == unit_id).Coils[num] = flag;
			}
			catch
			{
				BuildExceptionMessage(send_buffer, modbusCodes, Errors.EXCEPTION_SLAVE_DEVICE_FAILURE);
				break;
			}
			send_buffer.Add(5);
			send_buffer.AddRange(GetBytes(num));
			send_buffer.AddRange(GetBytes(num11));
			break;
		}
		case ModbusCodes.WRITE_SINGLE_REGISTER:
		{
			ushort num = ToUInt16(receive_buffer.ToArray(), 1);
			ushort num11 = ToUInt16(receive_buffer.ToArray(), 3);
			if (modbus_db.Single((Datastore x) => x.UnitID == unit_id).HoldingRegisters.Length == 0)
			{
				error = Errors.EXCEPTION_ILLEGAL_FUNCTION;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (num11 < 0 || num11 > ushort.MaxValue)
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_VALUE;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (!IsAllRegistersPresent(unit_id, ModbusDBTables.HOLDING_REGISTERS, num, 1))
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_ADDRESS;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			try
			{
				modbus_db.Single((Datastore x) => x.UnitID == unit_id).HoldingRegisters[num] = num11;
			}
			catch
			{
				BuildExceptionMessage(send_buffer, modbusCodes, Errors.EXCEPTION_SLAVE_DEVICE_FAILURE);
				break;
			}
			send_buffer.Add(6);
			send_buffer.AddRange(GetBytes(num));
			send_buffer.AddRange(GetBytes(num11));
			break;
		}
		case ModbusCodes.WRITE_MULTIPLE_COILS:
		{
			ushort num = ToUInt16(receive_buffer.ToArray(), 1);
			ushort num2 = ToUInt16(receive_buffer.ToArray(), 3);
			if (modbus_db.Single((Datastore x) => x.UnitID == unit_id).Coils.Length == 0)
			{
				error = Errors.EXCEPTION_ILLEGAL_FUNCTION;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (num2 < 1 || num2 > 1968)
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_VALUE;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (!IsAllRegistersPresent(unit_id, ModbusDBTables.COIL_REGISTERS, num, num2))
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_ADDRESS;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			ushort num5 = receive_buffer[5];
			byte[] bytes = receive_buffer.GetRange(6, num5).ToArray();
			BitArray bitArray = new BitArray(bytes);
			try
			{
				bitArray.CopyTo(modbus_db.Single((Datastore x) => x.UnitID == unit_id).Coils, num);
			}
			catch
			{
				BuildExceptionMessage(send_buffer, modbusCodes, Errors.EXCEPTION_SLAVE_DEVICE_FAILURE);
				break;
			}
			send_buffer.Add(15);
			send_buffer.AddRange(GetBytes(num));
			send_buffer.AddRange(GetBytes(num2));
			break;
		}
		case ModbusCodes.WRITE_MULTIPLE_REGISTERS:
		{
			ushort num = ToUInt16(receive_buffer.ToArray(), 1);
			ushort num2 = ToUInt16(receive_buffer.ToArray(), 3);
			if (modbus_db.Single((Datastore x) => x.UnitID == unit_id).HoldingRegisters.Length == 0)
			{
				error = Errors.EXCEPTION_ILLEGAL_FUNCTION;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (num2 < 1 || num2 > 123)
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_VALUE;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (!IsAllRegistersPresent(unit_id, ModbusDBTables.HOLDING_REGISTERS, num, num2))
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_ADDRESS;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			ushort num5 = receive_buffer[5];
			try
			{
				for (int num8 = 0; num8 < num5; num8 += 2)
				{
					modbus_db.Single((Datastore x) => x.UnitID == unit_id).HoldingRegisters[num + num8 / 2] = ToUInt16(receive_buffer.ToArray(), 6 + num8);
				}
			}
			catch
			{
				BuildExceptionMessage(send_buffer, modbusCodes, Errors.EXCEPTION_SLAVE_DEVICE_FAILURE);
				break;
			}
			send_buffer.Add(16);
			send_buffer.AddRange(GetBytes(num));
			send_buffer.AddRange(GetBytes(num2));
			break;
		}
		case ModbusCodes.MASK_WRITE_REGISTER:
		{
			ushort num = ToUInt16(receive_buffer.ToArray(), 1);
			ushort num14 = ToUInt16(receive_buffer.ToArray(), 3);
			ushort num15 = ToUInt16(receive_buffer.ToArray(), 5);
			if (modbus_db.Single((Datastore x) => x.UnitID == unit_id).HoldingRegisters.Length == 0)
			{
				error = Errors.EXCEPTION_ILLEGAL_FUNCTION;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if ((num14 < 0 || num14 > ushort.MaxValue) && (num14 < 0 || num14 > ushort.MaxValue))
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_VALUE;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (!IsAllRegistersPresent(unit_id, ModbusDBTables.HOLDING_REGISTERS, num, 1))
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_ADDRESS;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			try
			{
				modbus_db.Single((Datastore x) => x.UnitID == unit_id).HoldingRegisters[num] = (ushort)((modbus_db.Single((Datastore x) => x.UnitID == unit_id).HoldingRegisters[num] & num14) | (num15 & ~num14));
			}
			catch
			{
				BuildExceptionMessage(send_buffer, modbusCodes, Errors.EXCEPTION_SLAVE_DEVICE_FAILURE);
				break;
			}
			send_buffer.Add(22);
			send_buffer.AddRange(GetBytes(num));
			send_buffer.AddRange(GetBytes(num14));
			send_buffer.AddRange(GetBytes(num15));
			break;
		}
		case ModbusCodes.READ_WRITE_MULTIPLE_REGISTERS:
		{
			ushort num = ToUInt16(receive_buffer.ToArray(), 1);
			ushort num2 = ToUInt16(receive_buffer.ToArray(), 3);
			ushort num3 = ToUInt16(receive_buffer.ToArray(), 5);
			ushort num4 = ToUInt16(receive_buffer.ToArray(), 7);
			if (modbus_db.Single((Datastore x) => x.UnitID == unit_id).HoldingRegisters.Length == 0)
			{
				error = Errors.EXCEPTION_ILLEGAL_FUNCTION;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if ((num2 < 1 || num2 > 125) && (num4 < 1 || num4 > 121))
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_VALUE;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			if (!IsAllRegistersPresent(unit_id, ModbusDBTables.HOLDING_REGISTERS, num, num2) || !IsAllRegistersPresent(unit_id, ModbusDBTables.HOLDING_REGISTERS, num3, num4))
			{
				error = Errors.EXCEPTION_ILLEGAL_DATA_ADDRESS;
				BuildExceptionMessage(send_buffer, modbusCodes, error);
				break;
			}
			ushort num5 = receive_buffer[9];
			try
			{
				for (int num6 = 0; num6 < num5; num6 += 2)
				{
					modbus_db.Single((Datastore x) => x.UnitID == unit_id).HoldingRegisters[num3 + num6 / 2] = ToUInt16(receive_buffer.ToArray(), 10 + num6);
				}
			}
			catch
			{
				BuildExceptionMessage(send_buffer, modbusCodes, Errors.EXCEPTION_SLAVE_DEVICE_FAILURE);
				break;
			}
			send_buffer.Add(23);
			send_buffer.AddRange(GetBytes((ushort)(num2 * 2)));
			try
			{
				for (int num7 = 0; num7 < num2 * 2; num7 += 2)
				{
					send_buffer.AddRange(GetBytes(modbus_db.Single((Datastore x) => x.UnitID == unit_id).HoldingRegisters[num + num7 / 2]));
				}
			}
			catch
			{
				BuildExceptionMessage(send_buffer, modbusCodes, Errors.EXCEPTION_SLAVE_DEVICE_FAILURE);
			}
			break;
		}
		default:
			error = Errors.EXCEPTION_ILLEGAL_FUNCTION;
			BuildExceptionMessage(send_buffer, modbusCodes, error);
			break;
		}
		SendReply(send_buffer, flux, unit_id, transaction_id);
	}

	private void SendReply(List<byte> send_buffer, object flux, byte unit_id, ushort transaction_id)
	{
		switch (connection_type)
		{
		case ConnectionType.SERIAL_ASCII:
			send_buffer.Insert(0, unit_id);
			send_buffer.Add(LRC.CalcLRC(send_buffer.ToArray(), 0, send_buffer.Count));
			send_buffer = GetASCIIBytesFromBinaryBuffer(send_buffer.ToArray()).ToList();
			send_buffer.Insert(0, Encoding.ASCII.GetBytes(new char[1] { ':' }).First());
			send_buffer.AddRange(Encoding.ASCII.GetBytes(new char[2] { '\r', '\n' }));
			break;
		case ConnectionType.SERIAL_RTU:
			send_buffer.Insert(0, unit_id);
			send_buffer.AddRange(BitConverter.GetBytes(CRC16.CalcCRC16(send_buffer.ToArray(), 0, send_buffer.Count)));
			Thread.Sleep(interframe_delay);
			break;
		case ConnectionType.TCP_IP:
		case ConnectionType.UDP_IP:
			send_buffer.InsertRange(0, GetBytes(transaction_id));
			send_buffer.InsertRange(2, GetBytes(0));
			send_buffer.InsertRange(4, GetBytes((ushort)(1 + send_buffer.Count - 4)));
			send_buffer.Insert(6, unit_id);
			break;
		}
		WriteBuffer(flux, send_buffer.ToArray(), 0, send_buffer.Count);
	}
}
