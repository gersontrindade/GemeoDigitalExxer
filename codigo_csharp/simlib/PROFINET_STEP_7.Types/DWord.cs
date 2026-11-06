using System;

namespace PROFINET_STEP_7.Types;

public static class DWord
{
	public static uint FromByteArray(byte[] bytes)
	{
		return FromBytes(bytes[3], bytes[2], bytes[1], bytes[0]);
	}

	public static uint FromBytes(byte v1, byte v2, byte v3, byte v4)
	{
		return (uint)((double)(int)v1 + (double)(int)v2 * Math.Pow(2.0, 8.0) + (double)(int)v3 * Math.Pow(2.0, 16.0) + (double)(int)v4 * Math.Pow(2.0, 24.0));
	}

	public static byte[] ToByteArray(uint value)
	{
		byte[] array = new byte[4];
		int num = 4;
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

	public static byte[] ToByteArray(uint[] value)
	{
		ByteArray byteArray = new ByteArray();
		foreach (uint value2 in value)
		{
			byteArray.Add(ToByteArray(value2));
		}
		return byteArray.array;
	}

	public static uint[] ToArray(byte[] bytes)
	{
		uint[] array = new uint[bytes.Length / 4];
		int num = 0;
		for (int i = 0; i < bytes.Length / 4; i++)
		{
			array[i] = FromByteArray(new byte[4]
			{
				bytes[num++],
				bytes[num++],
				bytes[num++],
				bytes[num++]
			});
		}
		return array;
	}
}
