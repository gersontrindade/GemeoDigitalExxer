using System;
using System.Collections.Generic;

public static class UModbusTCPHelpers
{
	public static byte[] GetBytesOfInt(int _iValue)
	{
		byte[] array = new byte[2];
		array[1] = (byte)(_iValue >> 8);
		array[0] = (byte)_iValue;
		if (BitConverter.IsLittleEndian)
		{
			Array.Reverse((Array)array);
		}
		return array;
	}

	public static byte[] GetBytesOfBool(bool _bValue)
	{
		if (!_bValue)
		{
			return new byte[2];
		}
		return new byte[2] { 0, 1 };
	}

	public static int[] GetIntsOfBytes(byte[] _oArray)
	{
		if (BitConverter.IsLittleEndian)
		{
			Array.Reverse((Array)_oArray);
		}
		List<int> list = new List<int>();
		if (_oArray.Length < 2)
		{
			int item = _oArray[0];
			list.Add(item);
		}
		else
		{
			for (int i = 0; i < _oArray.Length; i += 2)
			{
				int item2 = (_oArray[i + 1] << 8) | _oArray[i];
				list.Add(item2);
			}
		}
		list.Reverse();
		return list.ToArray();
	}
}
