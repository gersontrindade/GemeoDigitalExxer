namespace IsoOnTcp;

internal static class S7ProtoHook
{
	public static byte[] RequestExchange(byte[] pdubytes)
	{
		if (pdubytes.Length == 26 && pdubytes[0] == 50 && pdubytes[1] == 7 && pdubytes[6] == 0 && pdubytes[7] == 8 && pdubytes[8] == 0 && pdubytes[9] == 8 && pdubytes[10] == 0 && pdubytes[11] == 1 && pdubytes[12] == 18 && pdubytes[13] == 4 && pdubytes[14] == 17 && pdubytes[15] == 68 && pdubytes[16] == 1 && pdubytes[17] == 0 && pdubytes[18] == byte.MaxValue && pdubytes[19] == 9 && pdubytes[20] == 0 && pdubytes[21] == 4)
		{
			short int16at = ByteConvert.GetInt16at(pdubytes, 22);
			short int16at2 = ByteConvert.GetInt16at(pdubytes, 24);
			if (int16at == 116 && int16at2 == 0)
			{
				return GetSzlId0x0074_response(ref pdubytes);
			}
			if (int16at == 3956 && int16at2 == 0)
			{
				return GetSzlId0x0f74_idx0_response(ref pdubytes);
			}
			if (int16at == 372)
			{
				return GetSzlId0x0174_response(int16at2, ref pdubytes);
			}
		}
		return null;
	}

	public static byte[] GetSzlId0x0074_response(ref byte[] pdubytes)
	{
		byte[] obj = new byte[58]
		{
			50, 7, 0, 0, 0, 0, 0, 12, 0, 36,
			0, 1, 18, 8, 18, 132, 1, 0, 0, 0,
			0, 0, 255, 9, 0, 32, 0, 116, 0, 0,
			0, 4, 0, 6, 0, 1, 0, 0, 0, 4,
			1, 0, 0, 5, 0, 0, 0, 6, 0, 0,
			0, 11, 0, 0, 0, 12, 0, 0
		};
		obj[4] = pdubytes[4];
		obj[5] = pdubytes[5];
		return obj;
	}

	public static byte[] GetSzlId0x0f74_idx0_response(ref byte[] pdubytes)
	{
		byte[] obj = new byte[34]
		{
			50, 7, 0, 0, 0, 0, 0, 12, 0, 12,
			0, 1, 18, 8, 18, 132, 1, 0, 0, 0,
			0, 0, 255, 9, 0, 8, 15, 116, 0, 0,
			0, 4, 0, 6
		};
		obj[4] = pdubytes[4];
		obj[5] = pdubytes[5];
		return obj;
	}

	public static byte[] GetSzlId0x0174_response(short szl_index, ref byte[] pdubytes)
	{
		byte[] array = new byte[38]
		{
			50, 7, 0, 0, 0, 0, 0, 12, 0, 16,
			0, 1, 18, 8, 18, 132, 1, 0, 0, 0,
			0, 0, 255, 9, 0, 12, 1, 116, 0, 0,
			0, 4, 0, 1, 0, 0, 0, 0
		};
		array[4] = pdubytes[4];
		array[5] = pdubytes[5];
		array[28] = pdubytes[24];
		array[29] = pdubytes[25];
		array[34] = pdubytes[24];
		array[35] = pdubytes[25];
		if (szl_index == 1 || szl_index == 4 || szl_index == 5 || szl_index == 6 || szl_index == 11 || szl_index == 12)
		{
			if (szl_index == 4)
			{
				array[36] = 1;
			}
			return array;
		}
		return null;
	}

	public static byte[] GetSzlId0131_idx3_response(ref byte[] pdubytes)
	{
		byte[] obj = new byte[74]
		{
			50, 7, 0, 0, 0, 0, 0, 12, 0, 52,
			0, 1, 18, 8, 18, 132, 1, 0, 0, 0,
			0, 0, 255, 9, 0, 48, 1, 49, 0, 3,
			0, 40, 0, 1, 0, 3, 0, 240, 131, 1,
			0, 0, 0, 0, 0, 1, 2, 9, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0
		};
		obj[4] = pdubytes[4];
		obj[5] = pdubytes[5];
		return obj;
	}

	public static byte[] GetSzlId0x0011_response(ref byte[] pdubytes)
	{
		byte[] obj = new byte[146]
		{
			50, 7, 0, 0, 0, 0, 0, 12, 0, 124,
			0, 1, 18, 8, 18, 132, 1, 0, 0, 0,
			0, 0, 255, 9, 0, 120, 0, 17, 0, 0,
			0, 28, 0, 4, 0, 1, 54, 69, 83, 55,
			32, 49, 53, 49, 45, 56, 65, 66, 48, 49,
			45, 48, 65, 66, 48, 32, 0, 192, 0, 3,
			0, 1, 0, 6, 54, 69, 83, 55, 32, 49,
			53, 49, 45, 56, 65, 66, 48, 49, 45, 48,
			65, 66, 48, 32, 0, 192, 0, 3, 0, 1,
			0, 7, 32, 32, 32, 32, 32, 32, 32, 32,
			32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
			32, 32, 0, 192, 86, 3, 2, 6, 0, 129,
			66, 111, 111, 116, 32, 76, 111, 97, 100, 101,
			114, 32, 32, 32, 32, 32, 32, 32, 32, 32,
			0, 0, 65, 32, 9, 9
		};
		obj[4] = pdubytes[4];
		obj[5] = pdubytes[5];
		obj[28] = pdubytes[24];
		obj[29] = pdubytes[25];
		obj[34] = pdubytes[24];
		obj[35] = pdubytes[25];
		return obj;
	}

	public static byte[] GetSzlId0x001c_response(ref byte[] pdubytes)
	{
		byte[] obj = new byte[374]
		{
			50, 7, 0, 0, 0, 0, 0, 12, 1, 96,
			0, 1, 18, 8, 18, 132, 1, 0, 0, 0,
			0, 0, 255, 9, 1, 92, 0, 28, 0, 0,
			0, 34, 0, 10, 0, 1, 73, 77, 49, 53,
			49, 45, 56, 45, 67, 80, 85, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 2,
			73, 77, 49, 53, 49, 45, 56, 32, 80, 78,
			47, 68, 80, 32, 67, 80, 85, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 3, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 4, 79, 114,
			105, 103, 105, 110, 97, 108, 32, 83, 105, 101,
			109, 101, 110, 115, 32, 69, 113, 117, 105, 112,
			109, 101, 110, 116, 0, 0, 0, 0, 0, 0,
			0, 5, 83, 32, 67, 45, 67, 54, 84, 87,
			55, 52, 56, 56, 50, 48, 49, 50, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 7, 73, 77, 49, 53,
			49, 45, 56, 32, 80, 78, 47, 68, 80, 32,
			67, 80, 85, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 8,
			77, 77, 67, 32, 50, 57, 48, 48, 70, 67,
			49, 65, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 9, 0, 42, 246, 0, 0, 1,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 10, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 11, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0
		};
		obj[4] = pdubytes[4];
		obj[5] = pdubytes[5];
		obj[28] = pdubytes[24];
		obj[29] = pdubytes[25];
		obj[34] = pdubytes[24];
		obj[35] = pdubytes[25];
		return obj;
	}

	public static void ResponseExchange(ref byte[] pdubytes)
	{
		if (pdubytes.Length == 20 && pdubytes[0] == 50 && pdubytes[1] == 3 && pdubytes[6] == 0 && pdubytes[7] == 8 && pdubytes[8] == 0 && pdubytes[9] == 0 && pdubytes[12] == 240)
		{
			pdubytes[14] = 0;
			pdubytes[15] = 1;
			pdubytes[16] = 0;
			pdubytes[17] = 1;
		}
	}
}
