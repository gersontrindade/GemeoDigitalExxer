using System.Text;

namespace PROFINET_STEP_7.Types;

public static class String
{
	public static byte[] ToByteArray(string value)
	{
		char[] array = value.ToCharArray();
		byte[] array2 = new byte[value.Length];
		for (int i = 0; i <= array.Length - 1; i++)
		{
			array2[i] = (byte)Asc(array[i].ToString());
		}
		return array2;
	}

	public static string FromByteArray(byte[] bytes)
	{
		return Encoding.ASCII.GetString(bytes);
	}

	private static int Asc(string s)
	{
		byte[] bytes = Encoding.ASCII.GetBytes(s);
		if (bytes.Length != 0)
		{
			return bytes[0];
		}
		return 0;
	}
}
