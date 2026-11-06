namespace PROFINET_STEP_7.Types;

public static class Byte
{
	public static byte[] ToByteArray(byte value)
	{
		return new byte[1] { value };
	}

	public static byte FromByteArray(byte[] bytes)
	{
		return bytes[0];
	}
}
