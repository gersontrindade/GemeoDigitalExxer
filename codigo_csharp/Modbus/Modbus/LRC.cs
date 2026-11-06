namespace Modbus;

internal static class LRC
{
	public static byte CalcLRC(byte[] buffer, int offset, int lenght)
	{
		byte b = 0;
		for (int i = 0; i < lenght; i++)
		{
			b += buffer[i + offset];
		}
		return (byte)(-(sbyte)b);
	}
}
