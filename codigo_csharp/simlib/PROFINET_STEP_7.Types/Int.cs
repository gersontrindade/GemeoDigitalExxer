using System;

namespace PROFINET_STEP_7.Types;

public static class Int
{
	public static short FromByteArray(byte[] bytes)
	{
		return FromBytes(bytes[1], bytes[0]);
	}

	public static short FromBytes(byte LoVal, byte HiVal)
	{
		return (short)(HiVal * 256 + LoVal);
	}

	public static byte[] ToByteArray(short value)
	{
		byte[] array = new byte[2];
		int num = 2;
		long num2 = value;
		for (int i = 0; i < num; i++)
		{
			long num3 = (long)Math.Pow(256.0, i);
			long num4 = num2 / num3;
			array[num - i - 1] = (byte)(num4 & 0xFF);
			num2 -= array[num - i - 1] * num3;
		}
		return array;
	}

	public static byte[] ToByteArray(short[] value)
	{
		ByteArray byteArray = new ByteArray();
		foreach (short value2 in value)
		{
			byteArray.Add(ToByteArray(value2));
		}
		return byteArray.array;
	}

	public static short[] ToArray(byte[] bytes)
	{
		short[] array = new short[bytes.Length / 2];
		int num = 0;
		for (int i = 0; i < bytes.Length / 2; i++)
		{
			array[i] = FromByteArray(new byte[2]
			{
				bytes[num++],
				bytes[num++]
			});
		}
		return array;
	}

	public static short CWord(int value)
	{
		if (value > 32767)
		{
			value -= 32768;
			value = 32768 - value;
			value *= -1;
		}
		return (short)value;
	}
}
