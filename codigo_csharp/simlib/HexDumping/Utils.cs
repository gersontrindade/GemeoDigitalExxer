using System.Text;

namespace HexDumping;

public class Utils
{
	public static string HexDumpAll(byte[] bytes)
	{
		int len = bytes.Length;
		return HexDump(bytes, len);
	}

	public static string HexDump(byte[] bytes, int len)
	{
		if (bytes == null)
		{
			return "<null>";
		}
		StringBuilder stringBuilder = new StringBuilder((len + 15) / 16 * 78);
		char[] array = new char[78];
		for (int i = 0; i < 75; i++)
		{
			array[i] = ' ';
		}
		array[76] = '\r';
		array[77] = '\n';
		for (int j = 0; j < len; j += 16)
		{
			array[0] = HexChar(j >> 28);
			array[1] = HexChar(j >> 24);
			array[2] = HexChar(j >> 20);
			array[3] = HexChar(j >> 16);
			array[4] = HexChar(j >> 12);
			array[5] = HexChar(j >> 8);
			array[6] = HexChar(j >> 4);
			array[7] = HexChar(j);
			int num = 11;
			int num2 = 60;
			for (int k = 0; k < 16; k++)
			{
				if (j + k >= len)
				{
					array[num] = ' ';
					array[num + 1] = ' ';
					array[num2] = ' ';
				}
				else
				{
					byte b = bytes[j + k];
					array[num] = HexChar(b >> 4);
					array[num + 1] = HexChar(b);
					array[num2] = (char)((b < 32) ? 183 : b);
				}
				num += ((k == 8) ? 4 : 3);
				num2++;
			}
			stringBuilder.Append(array);
		}
		return stringBuilder.ToString();
	}

	private static char HexChar(int value)
	{
		value &= 0xF;
		if (value >= 0 && value <= 9)
		{
			return (char)(48 + value);
		}
		return (char)(65 + (value - 10));
	}
}
