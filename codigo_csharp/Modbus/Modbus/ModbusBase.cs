using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Reflection;
using System.Text;

namespace Modbus;

public abstract class ModbusBase
{
	protected const ushort PROTOCOL_ID = 0;

	private const int DEFAULT_RX_TIMEOUT = 6000;

	protected const int MBAP_HEADER_LEN = 7;

	protected const char ASCII_START_FRAME = ':';

	protected const char ASCII_STOP_FRAME_1ST = '\r';

	protected const char ASCII_STOP_FRAME_2ND = '\n';

	public const ushort MAX_COILS_IN_READ_MSG = 2000;

	public const ushort MAX_DISCRETE_INPUTS_IN_READ_MSG = 2000;

	public const ushort MAX_HOLDING_REGISTERS_IN_READ_MSG = 125;

	public const ushort MAX_INPUT_REGISTERS_IN_READ_MSG = 125;

	public const ushort MAX_COILS_IN_WRITE_MSG = 1968;

	public const ushort MAX_HOLDING_REGISTERS_IN_WRITE_MSG = 123;

	public const ushort MAX_HOLDING_REGISTERS_TO_READ_IN_READWRITE_MSG = 125;

	public const ushort MAX_HOLDING_REGISTERS_TO_WRITE_IN_READWRITE_MSG = 121;

	protected ConnectionType connection_type;

	protected DeviceType device_type;

	protected int rx_timeout = 6000;

	protected Errors error;

	protected int interframe_delay;

	protected int interchar_delay;

	public int RxTimeout
	{
		get
		{
			return rx_timeout;
		}
		set
		{
			rx_timeout = value;
		}
	}

	public Errors Error => error;

	protected byte[] GetBytes(ushort value)
	{
		return new byte[2]
		{
			(byte)(value >> 8),
			(byte)(value & 0xFF)
		};
	}

	protected byte[] GetASCIIBytesFromBinaryBuffer(byte[] buffer)
	{
		List<char> list = new List<char>();
		int i = 0;
		int num = 0;
		for (; i < buffer.Length * 2; i++)
		{
			list.Add((byte)((i % 2 == 0) ? (buffer[num] >> 4) : (buffer[num] & 0xF)) switch
			{
				1 => '1', 
				2 => '2', 
				3 => '3', 
				4 => '4', 
				5 => '5', 
				6 => '6', 
				7 => '7', 
				8 => '8', 
				9 => '9', 
				10 => 'A', 
				11 => 'B', 
				12 => 'C', 
				13 => 'D', 
				14 => 'E', 
				15 => 'F', 
				_ => '0', 
			});
			if (i % 2 != 0)
			{
				num++;
			}
		}
		return Encoding.ASCII.GetBytes(list.ToArray());
	}

	protected byte[] GetBinaryBufferFromASCIIBytes(byte[] buffer)
	{
		List<byte> list = new List<byte>();
		char[] chars = Encoding.ASCII.GetChars(buffer);
		byte b = 0;
		for (int i = 0; i < buffer.Length; i++)
		{
			byte b2 = chars[i] switch
			{
				'1' => 1, 
				'2' => 2, 
				'3' => 3, 
				'4' => 4, 
				'5' => 5, 
				'6' => 6, 
				'7' => 7, 
				'8' => 8, 
				'9' => 9, 
				'A' => 10, 
				'B' => 11, 
				'C' => 12, 
				'D' => 13, 
				'E' => 14, 
				'F' => 15, 
				_ => 0, 
			};
			if (i % 2 != 0)
			{
				b |= b2;
				list.Add(b);
				b = 0;
			}
			else
			{
				b = (byte)(b2 << 4);
			}
		}
		return list.ToArray();
	}

	protected ushort ToUInt16(byte[] value, int offset)
	{
		return (ushort)((value[offset] << 8) | (value[offset + 1] & 0xFF));
	}

	protected byte EightBitToByte(bool[] array, int offset)
	{
		if (array.Length < 8)
		{
			throw new Exception(MethodBase.GetCurrentMethod().Name + ": The array must be at least 8-bit length!");
		}
		byte b = 0;
		for (int i = 0; i < 8; i++)
		{
			switch (array[offset + i])
			{
			case true:
				b |= (byte)(1 << i);
				break;
			case false:
				b &= (byte)(~(1 << i));
				break;
			}
		}
		return b;
	}

	protected int GetInterframeDelay(SerialPort sp)
	{
		if (sp.BaudRate > 19200)
		{
			return 2;
		}
		int num = 1 + sp.DataBits;
		num += ((sp.Parity != Parity.None) ? 1 : 0);
		switch (sp.StopBits)
		{
		case StopBits.One:
			num++;
			break;
		case StopBits.Two:
		case StopBits.OnePointFive:
			num += 2;
			break;
		}
		return Convert.ToInt32(Math.Ceiling(1.0 / ((double)sp.BaudRate / ((double)num * 3.5) / 1000.0)));
	}

	protected int GetIntercharDelay(SerialPort sp)
	{
		if (sp.BaudRate > 19200)
		{
			return 1;
		}
		int num = 1 + sp.DataBits;
		num += ((sp.Parity != Parity.None) ? 1 : 0);
		switch (sp.StopBits)
		{
		case StopBits.One:
			num++;
			break;
		case StopBits.Two:
		case StopBits.OnePointFive:
			num += 2;
			break;
		}
		return Convert.ToInt32(Math.Ceiling(1.0 / ((double)sp.BaudRate / ((double)num * 1.5) / 1000.0)));
	}
}
