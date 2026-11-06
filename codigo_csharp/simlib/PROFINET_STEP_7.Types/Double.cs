using System;
using PROFINET_STEP_7.Profinet;

namespace PROFINET_STEP_7.Types;

public static class Double
{
	public static double FromByteArray(byte[] bytes)
	{
		byte b = bytes[0];
		byte b2 = bytes[1];
		byte b3 = bytes[2];
		byte b4 = bytes[3];
		if (b + b2 + b3 + b4 == 0)
		{
			return 0.0;
		}
		string text = ValToBinString(b) + ValToBinString(b2) + ValToBinString(b3) + ValToBinString(b4);
		int num = int.Parse(text.Substring(0, 1));
		int num2 = Conversion.BinStringToInt(text.Substring(1, 8));
		string text2 = text.Substring(9, 23);
		double num3 = 1.0;
		double num4 = 1.0;
		for (int i = 0; i <= 22; i++)
		{
			num4 /= 2.0;
			if (text2.Substring(i, 1) == "1")
			{
				num3 += num4;
			}
		}
		return Math.Pow(-1.0, num) * Math.Pow(2.0, num2 - 127) * num3;
	}

	public static double FromDWord(int value)
	{
		return FromByteArray(DInt.ToByteArray(value));
	}

	public static double FromDWord(uint value)
	{
		return FromByteArray(DWord.ToByteArray(value));
	}

	public static byte[] ToByteArray(double value)
	{
		double num = value;
		string text = "";
		byte[] array = new byte[4];
		if (num != 0.0)
		{
			if (num < 0.0)
			{
				num *= -1.0;
				text = "1";
			}
			else
			{
				text = "0";
			}
			int num2 = (int)Math.Floor(Math.Log(num) / Math.Log(2.0));
			num = num / Math.Pow(2.0, num2) - 1.0;
			text += ValToBinString((byte)(num2 + 127));
			for (int i = 1; i <= 23; i++)
			{
				if (!(num - Math.Pow(2.0, -i) < 0.0))
				{
					num -= Math.Pow(2.0, -i);
					text += "1";
				}
				else
				{
					text += "0";
				}
			}
			array[0] = BinStringToByte(text.Substring(0, 8)).Value;
			array[1] = BinStringToByte(text.Substring(8, 8)).Value;
			array[2] = BinStringToByte(text.Substring(16, 8)).Value;
			array[3] = BinStringToByte(text.Substring(24, 8)).Value;
		}
		else
		{
			array[0] = 0;
			array[1] = 0;
			array[2] = 0;
			array[3] = 0;
		}
		return array;
	}

	public static byte[] ToByteArray(double[] value)
	{
		ByteArray byteArray = new ByteArray();
		foreach (double value2 in value)
		{
			byteArray.Add(ToByteArray(value2));
		}
		return byteArray.array;
	}

	public static double[] ToArray(byte[] bytes)
	{
		double[] array = new double[bytes.Length / 4];
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

	private static string ValToBinString(byte value)
	{
		string text = "";
		for (int i = 7; i >= 0; i += -1)
		{
			text = (((value & (byte)Math.Pow(2.0, i)) <= 0) ? (text + "0") : (text + "1"));
		}
		return text;
	}

	private static byte? BinStringToByte(string txt)
	{
		int num = 0;
		int num2 = 0;
		if (txt.Length == 8)
		{
			for (num = 7; num >= 0; num += -1)
			{
				if (int.Parse(txt.Substring(num, 1)) == 1)
				{
					num2 += (int)Math.Pow(2.0, txt.Length - 1 - num);
				}
			}
			return (byte)num2;
		}
		return null;
	}
}
