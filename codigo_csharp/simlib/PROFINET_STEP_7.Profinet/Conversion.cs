using System;

namespace PROFINET_STEP_7.Profinet;

public static class Conversion
{
	public static int BinStringToInt(string txt)
	{
		int num = 0;
		int num2 = 0;
		for (num = txt.Length - 1; num >= 0; num += -1)
		{
			if (int.Parse(txt.Substring(num, 1)) == 1)
			{
				num2 += (int)Math.Pow(2.0, txt.Length - 1 - num);
			}
		}
		return num2;
	}

	public static byte? BinStringToByte(string txt)
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

	public static string ValToBinString(object value)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		string text = "";
		long num4 = 0L;
		try
		{
			if (value.GetType().Name.IndexOf("[]") < 0)
			{
				switch (value.GetType().Name)
				{
				case "Byte":
					num3 = 7;
					num4 = (byte)value;
					break;
				case "Int16":
					num3 = 15;
					num4 = (short)value;
					break;
				case "Int32":
					num3 = 31;
					num4 = (int)value;
					break;
				case "Int64":
					num3 = 63;
					num4 = (long)value;
					break;
				default:
					throw new Exception();
				}
				for (num = num3; num >= 0; num += -1)
				{
					text = (((num4 & (long)Math.Pow(2.0, num)) <= 0) ? (text + "0") : (text + "1"));
				}
			}
			else
			{
				switch (value.GetType().Name)
				{
				case "Byte[]":
				{
					num3 = 7;
					byte[] array4 = (byte[])value;
					for (num2 = 0; num2 <= array4.Length - 1; num2++)
					{
						for (num = num3; num >= 0; num += -1)
						{
							text = (((array4[num2] & (byte)Math.Pow(2.0, num)) <= 0) ? (text + "0") : (text + "1"));
						}
					}
					break;
				}
				case "Int16[]":
				{
					num3 = 15;
					short[] array2 = (short[])value;
					for (num2 = 0; num2 <= array2.Length - 1; num2++)
					{
						for (num = num3; num >= 0; num += -1)
						{
							text = (((array2[num2] & (byte)Math.Pow(2.0, num)) <= 0) ? (text + "0") : (text + "1"));
						}
					}
					break;
				}
				case "Int32[]":
				{
					num3 = 31;
					int[] array3 = (int[])value;
					for (num2 = 0; num2 <= array3.Length - 1; num2++)
					{
						for (num = num3; num >= 0; num += -1)
						{
							text = (((array3[num2] & (byte)Math.Pow(2.0, num)) <= 0) ? (text + "0") : (text + "1"));
						}
					}
					break;
				}
				case "Int64[]":
				{
					num3 = 63;
					byte[] array = (byte[])value;
					for (num2 = 0; num2 <= array.Length - 1; num2++)
					{
						for (num = num3; num >= 0; num += -1)
						{
							text = (((array[num2] & (byte)Math.Pow(2.0, num)) <= 0) ? (text + "0") : (text + "1"));
						}
					}
					break;
				}
				default:
					throw new Exception();
				}
			}
			return text;
		}
		catch
		{
			return "";
		}
	}
}
