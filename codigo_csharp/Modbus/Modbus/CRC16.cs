namespace Modbus;

internal static class CRC16
{
	private const ushort POLY = 40961;

	private static ushort[] crc_tab16;

	private static void InitCRC16Tab()
	{
		if (crc_tab16 != null)
		{
			return;
		}
		crc_tab16 = new ushort[256];
		for (int i = 0; i < 256; i++)
		{
			ushort num = 0;
			ushort num2 = (ushort)i;
			for (int j = 0; j < 8; j++)
			{
				num = ((((num ^ num2) & 1) != 1) ? ((ushort)(num >> 1)) : ((ushort)((num >> 1) ^ 0xA001)));
				num2 >>= 1;
			}
			crc_tab16[i] = num;
		}
	}

	private static ushort UpdateCRC16(ushort crc, byte bt)
	{
		ushort num = (ushort)(0xFF & bt);
		InitCRC16Tab();
		ushort num2 = (ushort)(crc ^ num);
		crc = (ushort)((crc >> 8) ^ crc_tab16[num2 & 0xFF]);
		return crc;
	}

	public static ushort CalcCRC16(byte[] buffer, int offset, int length)
	{
		ushort num = ushort.MaxValue;
		for (int i = 0; i < length; i++)
		{
			num = UpdateCRC16(num, buffer[offset + i]);
		}
		return num;
	}
}
