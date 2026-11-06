using System;

namespace PROFINET_STEP_7.Types;

public static class Boolean
{
	public static bool GetValue(byte value, int bit)
	{
		if ((value & (int)Math.Pow(2.0, bit)) != 0)
		{
			return true;
		}
		return false;
	}

	public static byte SetBit(byte value, int bit)
	{
		return (byte)(value | (byte)Math.Pow(2.0, bit));
	}

	public static byte ClearBit(byte value, int bit)
	{
		return (byte)(value & (byte)(~(byte)Math.Pow(2.0, bit)));
	}
}
