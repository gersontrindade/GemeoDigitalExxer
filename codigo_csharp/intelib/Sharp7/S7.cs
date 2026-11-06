using System;
using System.Text;

namespace Sharp7;

public static class S7
{
	private static long bias = 621355968000000000L;

	private static int BCDtoByte(byte B)
	{
		return (B >> 4) * 10 + (B & 0xF);
	}

	private static byte ByteToBCD(int Value)
	{
		return (byte)((Value / 10 << 4) | (Value % 10));
	}

	private static byte[] CopyFrom(byte[] Buffer, int Pos, int Size)
	{
		byte[] array = new byte[Size];
		Array.Copy(Buffer, Pos, array, 0, Size);
		return array;
	}

	public static int DataSizeByte(int WordLength)
	{
		return WordLength switch
		{
			1 => 1, 
			2 => 1, 
			3 => 1, 
			4 => 2, 
			6 => 4, 
			5 => 2, 
			7 => 4, 
			8 => 4, 
			28 => 2, 
			29 => 2, 
			_ => 0, 
		};
	}

	public static bool GetBitAt(byte[] Buffer, int Pos, int Bit)
	{
		byte[] array = new byte[8] { 1, 2, 4, 8, 16, 32, 64, 128 };
		if (Bit < 0)
		{
			Bit = 0;
		}
		if (Bit > 7)
		{
			Bit = 7;
		}
		return (Buffer[Pos] & array[Bit]) != 0;
	}

	public static void SetBitAt(ref byte[] Buffer, int Pos, int Bit, bool Value)
	{
		byte[] array = new byte[8] { 1, 2, 4, 8, 16, 32, 64, 128 };
		if (Bit < 0)
		{
			Bit = 0;
		}
		if (Bit > 7)
		{
			Bit = 7;
		}
		if (Value)
		{
			Buffer[Pos] |= array[Bit];
		}
		else
		{
			Buffer[Pos] = (byte)(Buffer[Pos] & ~array[Bit]);
		}
	}

	public static int GetSIntAt(byte[] Buffer, int Pos)
	{
		int num = Buffer[Pos];
		if (num < 128)
		{
			return num;
		}
		return num - 256;
	}

	public static void SetSIntAt(byte[] Buffer, int Pos, int Value)
	{
		if (Value < -128)
		{
			Value = -128;
		}
		if (Value > 127)
		{
			Value = 127;
		}
		Buffer[Pos] = (byte)Value;
	}

	public static int GetIntAt(byte[] Buffer, int Pos)
	{
		return (Buffer[Pos] << 8) | Buffer[Pos + 1];
	}

	public static void SetIntAt(byte[] Buffer, int Pos, short Value)
	{
		Buffer[Pos] = (byte)(Value >> 8);
		Buffer[Pos + 1] = (byte)(Value & 0xFF);
	}

	public static int GetDIntAt(byte[] Buffer, int Pos)
	{
		return (((Buffer[Pos] << 8) + Buffer[Pos + 1] << 8) + Buffer[Pos + 2] << 8) + Buffer[Pos + 3];
	}

	public static void SetDIntAt(byte[] Buffer, int Pos, int Value)
	{
		Buffer[Pos + 3] = (byte)(Value & 0xFF);
		Buffer[Pos + 2] = (byte)((Value >> 8) & 0xFF);
		Buffer[Pos + 1] = (byte)((Value >> 16) & 0xFF);
		Buffer[Pos] = (byte)((Value >> 24) & 0xFF);
	}

	public static long GetLIntAt(byte[] Buffer, int Pos)
	{
		return (long)(((((((((ulong)Buffer[Pos] << 8) + Buffer[Pos + 1] << 8) + Buffer[Pos + 2] << 8) + Buffer[Pos + 3] << 8) + Buffer[Pos + 4] << 8) + Buffer[Pos + 5] << 8) + Buffer[Pos + 6] << 8) + Buffer[Pos + 7]);
	}

	public static void SetLIntAt(byte[] Buffer, int Pos, long Value)
	{
		Buffer[Pos + 7] = (byte)(Value & 0xFF);
		Buffer[Pos + 6] = (byte)((Value >> 8) & 0xFF);
		Buffer[Pos + 5] = (byte)((Value >> 16) & 0xFF);
		Buffer[Pos + 4] = (byte)((Value >> 24) & 0xFF);
		Buffer[Pos + 3] = (byte)((Value >> 32) & 0xFF);
		Buffer[Pos + 2] = (byte)((Value >> 40) & 0xFF);
		Buffer[Pos + 1] = (byte)((Value >> 48) & 0xFF);
		Buffer[Pos] = (byte)((Value >> 56) & 0xFF);
	}

	public static byte GetUSIntAt(byte[] Buffer, int Pos)
	{
		return Buffer[Pos];
	}

	public static void SetUSIntAt(byte[] Buffer, int Pos, byte Value)
	{
		Buffer[Pos] = Value;
	}

	public static ushort GetUIntAt(byte[] Buffer, int Pos)
	{
		return (ushort)((Buffer[Pos] << 8) | Buffer[Pos + 1]);
	}

	public static void SetUIntAt(byte[] Buffer, int Pos, ushort Value)
	{
		Buffer[Pos] = (byte)(Value >> 8);
		Buffer[Pos + 1] = (byte)(Value & 0xFF);
	}

	public static uint GetUDIntAt(byte[] Buffer, int Pos)
	{
		return (uint)((((((Buffer[Pos] << 8) | Buffer[Pos + 1]) << 8) | Buffer[Pos + 2]) << 8) | Buffer[Pos + 3]);
	}

	public static void SetUDIntAt(byte[] Buffer, int Pos, uint Value)
	{
		Buffer[Pos + 3] = (byte)(Value & 0xFF);
		Buffer[Pos + 2] = (byte)((Value >> 8) & 0xFF);
		Buffer[Pos + 1] = (byte)((Value >> 16) & 0xFF);
		Buffer[Pos] = (byte)((Value >> 24) & 0xFF);
	}

	public static ulong GetULIntAt(byte[] Buffer, int Pos)
	{
		return ((((((((((((((ulong)Buffer[Pos] << 8) | Buffer[Pos + 1]) << 8) | Buffer[Pos + 2]) << 8) | Buffer[Pos + 3]) << 8) | Buffer[Pos + 4]) << 8) | Buffer[Pos + 5]) << 8) | Buffer[Pos + 6]) << 8) | Buffer[Pos + 7];
	}

	public static void SetULintAt(byte[] Buffer, int Pos, ulong Value)
	{
		Buffer[Pos + 7] = (byte)(Value & 0xFF);
		Buffer[Pos + 6] = (byte)((Value >> 8) & 0xFF);
		Buffer[Pos + 5] = (byte)((Value >> 16) & 0xFF);
		Buffer[Pos + 4] = (byte)((Value >> 24) & 0xFF);
		Buffer[Pos + 3] = (byte)((Value >> 32) & 0xFF);
		Buffer[Pos + 2] = (byte)((Value >> 40) & 0xFF);
		Buffer[Pos + 1] = (byte)((Value >> 48) & 0xFF);
		Buffer[Pos] = (byte)((Value >> 56) & 0xFF);
	}

	public static byte GetByteAt(byte[] Buffer, int Pos)
	{
		return Buffer[Pos];
	}

	public static void SetByteAt(byte[] Buffer, int Pos, byte Value)
	{
		Buffer[Pos] = Value;
	}

	public static ushort GetWordAt(byte[] Buffer, int Pos)
	{
		return GetUIntAt(Buffer, Pos);
	}

	public static void SetWordAt(byte[] Buffer, int Pos, ushort Value)
	{
		SetUIntAt(Buffer, Pos, Value);
	}

	public static uint GetDWordAt(byte[] Buffer, int Pos)
	{
		return GetUDIntAt(Buffer, Pos);
	}

	public static void SetDWordAt(byte[] Buffer, int Pos, uint Value)
	{
		SetUDIntAt(Buffer, Pos, Value);
	}

	public static ulong GetLWordAt(byte[] Buffer, int Pos)
	{
		return GetULIntAt(Buffer, Pos);
	}

	public static void SetLWordAt(byte[] Buffer, int Pos, ulong Value)
	{
		SetULintAt(Buffer, Pos, Value);
	}

	public static float GetRealAt(byte[] Buffer, int Pos)
	{
		return BitConverter.ToSingle(BitConverter.GetBytes(GetUDIntAt(Buffer, Pos)), 0);
	}

	public static void SetRealAt(byte[] Buffer, int Pos, float Value)
	{
		byte[] bytes = BitConverter.GetBytes(Value);
		Buffer[Pos] = bytes[3];
		Buffer[Pos + 1] = bytes[2];
		Buffer[Pos + 2] = bytes[1];
		Buffer[Pos + 3] = bytes[0];
	}

	public static double GetLRealAt(byte[] Buffer, int Pos)
	{
		return BitConverter.ToDouble(BitConverter.GetBytes(GetULIntAt(Buffer, Pos)), 0);
	}

	public static void SetLRealAt(byte[] Buffer, int Pos, double Value)
	{
		byte[] bytes = BitConverter.GetBytes(Value);
		Buffer[Pos] = bytes[7];
		Buffer[Pos + 1] = bytes[6];
		Buffer[Pos + 2] = bytes[5];
		Buffer[Pos + 3] = bytes[4];
		Buffer[Pos + 4] = bytes[3];
		Buffer[Pos + 5] = bytes[2];
		Buffer[Pos + 6] = bytes[1];
		Buffer[Pos + 7] = bytes[0];
	}

	public static DateTime GetDateTimeAt(byte[] Buffer, int Pos)
	{
		int num = BCDtoByte(Buffer[Pos]);
		num = ((num >= 90) ? (num + 1900) : (num + 2000));
		int month = BCDtoByte(Buffer[Pos + 1]);
		int day = BCDtoByte(Buffer[Pos + 2]);
		int hour = BCDtoByte(Buffer[Pos + 3]);
		int minute = BCDtoByte(Buffer[Pos + 4]);
		int second = BCDtoByte(Buffer[Pos + 5]);
		int millisecond = BCDtoByte(Buffer[Pos + 6]) * 10 + BCDtoByte(Buffer[Pos + 7]) / 10;
		try
		{
			return new DateTime(num, month, day, hour, minute, second, millisecond);
		}
		catch (ArgumentOutOfRangeException)
		{
			return new DateTime(0L);
		}
	}

	public static void SetDateTimeAt(byte[] Buffer, int Pos, DateTime Value)
	{
		int num = Value.Year;
		int month = Value.Month;
		int day = Value.Day;
		int hour = Value.Hour;
		int minute = Value.Minute;
		int second = Value.Second;
		int num2 = (int)(Value.DayOfWeek + 1);
		int value = Value.Millisecond / 10;
		int num3 = Value.Millisecond % 10;
		if (num > 1999)
		{
			num -= 2000;
		}
		Buffer[Pos] = ByteToBCD(num);
		Buffer[Pos + 1] = ByteToBCD(month);
		Buffer[Pos + 2] = ByteToBCD(day);
		Buffer[Pos + 3] = ByteToBCD(hour);
		Buffer[Pos + 4] = ByteToBCD(minute);
		Buffer[Pos + 5] = ByteToBCD(second);
		Buffer[Pos + 6] = ByteToBCD(value);
		Buffer[Pos + 7] = ByteToBCD(num3 * 10 + num2);
	}

	public static DateTime GetDateAt(byte[] Buffer, int Pos)
	{
		try
		{
			return new DateTime(1990, 1, 1).AddDays(GetIntAt(Buffer, Pos));
		}
		catch (ArgumentOutOfRangeException)
		{
			return new DateTime(0L);
		}
	}

	public static void SetDateAt(byte[] Buffer, int Pos, DateTime Value)
	{
		SetIntAt(Buffer, Pos, (short)(Value - new DateTime(1990, 1, 1)).Days);
	}

	public static DateTime GetTODAt(byte[] Buffer, int Pos)
	{
		try
		{
			return new DateTime(0L).AddMilliseconds(GetDIntAt(Buffer, Pos));
		}
		catch (ArgumentOutOfRangeException)
		{
			return new DateTime(0L);
		}
	}

	public static void SetTODAt(byte[] Buffer, int Pos, DateTime Value)
	{
		SetDIntAt(Buffer, Pos, (int)Math.Round(Value.TimeOfDay.TotalMilliseconds));
	}

	public static DateTime GetLTODAt(byte[] Buffer, int Pos)
	{
		try
		{
			return new DateTime(Math.Abs(GetLIntAt(Buffer, Pos) / 100));
		}
		catch (ArgumentOutOfRangeException)
		{
			return new DateTime(0L);
		}
	}

	public static void SetLTODAt(byte[] Buffer, int Pos, DateTime Value)
	{
		SetLIntAt(Buffer, Pos, Value.TimeOfDay.Ticks * 100);
	}

	public static DateTime GetLDTAt(byte[] Buffer, int Pos)
	{
		try
		{
			return new DateTime(GetLIntAt(Buffer, Pos) / 100 + bias);
		}
		catch (ArgumentOutOfRangeException)
		{
			return new DateTime(0L);
		}
	}

	public static void SetLDTAt(byte[] Buffer, int Pos, DateTime Value)
	{
		SetLIntAt(Buffer, Pos, (Value.Ticks - bias) * 100);
	}

	public static DateTime GetDTLAt(byte[] Buffer, int Pos)
	{
		int year = Buffer[Pos] * 256 + Buffer[Pos + 1];
		int month = Buffer[Pos + 2];
		int day = Buffer[Pos + 3];
		int hour = Buffer[Pos + 5];
		int minute = Buffer[Pos + 6];
		int second = Buffer[Pos + 7];
		int millisecond = (int)GetUDIntAt(Buffer, Pos + 8) / 1000000;
		try
		{
			return new DateTime(year, month, day, hour, minute, second, millisecond);
		}
		catch (ArgumentOutOfRangeException)
		{
			return new DateTime(0L);
		}
	}

	public static void SetDTLAt(byte[] Buffer, int Pos, DateTime Value)
	{
		short value = (short)Value.Year;
		byte b = (byte)Value.Month;
		byte b2 = (byte)Value.Day;
		byte b3 = (byte)Value.Hour;
		byte b4 = (byte)Value.Minute;
		byte b5 = (byte)Value.Second;
		byte b6 = (byte)(Value.DayOfWeek + 1);
		int value2 = Value.Millisecond * 1000000;
		byte[] bytes = BitConverter.GetBytes(value);
		Buffer[Pos] = bytes[1];
		Buffer[Pos + 1] = bytes[0];
		Buffer[Pos + 2] = b;
		Buffer[Pos + 3] = b2;
		Buffer[Pos + 4] = b6;
		Buffer[Pos + 5] = b3;
		Buffer[Pos + 6] = b4;
		Buffer[Pos + 7] = b5;
		SetDIntAt(Buffer, Pos + 8, value2);
	}

	public static string GetStringAt(byte[] Buffer, int Pos)
	{
		int count = Buffer[Pos + 1];
		return Encoding.UTF8.GetString(Buffer, Pos + 2, count);
	}

	public static void SetStringAt(byte[] Buffer, int Pos, int MaxLen, string Value)
	{
		int length = Value.Length;
		Buffer[Pos] = (byte)MaxLen;
		Buffer[Pos + 1] = (byte)length;
		Encoding.UTF8.GetBytes(Value, 0, length, Buffer, Pos + 2);
	}

	public static string GetCharsAt(byte[] Buffer, int Pos, int Size)
	{
		return Encoding.UTF8.GetString(Buffer, Pos, Size);
	}

	public static void SetCharsAt(byte[] Buffer, int Pos, string Value)
	{
		int num = Buffer.Length - Pos;
		if (num > Value.Length)
		{
			num = Value.Length;
		}
		Encoding.UTF8.GetBytes(Value, 0, num, Buffer, Pos);
	}

	public static int GetCounter(ushort Value)
	{
		return BCDtoByte((byte)Value) * 100 + BCDtoByte((byte)(Value >> 8));
	}

	public static int GetCounterAt(ushort[] Buffer, int Index)
	{
		return GetCounter(Buffer[Index]);
	}

	public static ushort ToCounter(int Value)
	{
		return (ushort)(ByteToBCD(Value / 100) + (ByteToBCD(Value % 100) << 8));
	}

	public static void SetCounterAt(ushort[] Buffer, int Pos, int Value)
	{
		Buffer[Pos] = ToCounter(Value);
	}
}
