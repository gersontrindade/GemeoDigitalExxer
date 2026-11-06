using System;

namespace PROFINET_STEP_7.Types;

public static class Counter
{
	public static ushort FromByteArray(byte[] bytes)
	{
		return FromBytes(bytes[1], bytes[0]);
	}

	public static ushort FromBytes(byte LoVal, byte HiVal)
	{
		return (ushort)(HiVal * 256 + LoVal);
	}

	public static byte[] ToByteArray(ushort value)
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

	public static byte[] ToByteArray(ushort[] value)
	{
		ByteArray byteArray = new ByteArray();
		foreach (ushort value2 in value)
		{
			byteArray.Add(ToByteArray(value2));
		}
		return byteArray.array;
	}

	public static ushort[] ToArray(byte[] bytes)
	{
		ushort[] array = new ushort[bytes.Length / 2];
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
}
