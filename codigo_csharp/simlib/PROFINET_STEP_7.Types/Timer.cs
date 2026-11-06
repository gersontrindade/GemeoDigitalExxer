using System;
using PROFINET_STEP_7.Profinet;

namespace PROFINET_STEP_7.Types;

public static class Timer
{
	public static double FromByteArray(byte[] bytes)
	{
		double num = 0.0;
		string text = Conversion.ValToBinString((short)Word.FromBytes(bytes[1], bytes[0]));
		num = (double)Conversion.BinStringToInt(text.Substring(4, 4)) * 100.0;
		num += (double)Conversion.BinStringToInt(text.Substring(8, 4)) * 10.0;
		num += (double)Conversion.BinStringToInt(text.Substring(12, 4));
		switch (text.Substring(2, 2))
		{
		case "00":
			num *= 0.01;
			break;
		case "01":
			num *= 0.1;
			break;
		case "10":
			num *= 1.0;
			break;
		case "11":
			num *= 10.0;
			break;
		}
		return num;
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

	public static double[] ToArray(byte[] bytes)
	{
		double[] array = new double[bytes.Length / 2];
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
