using System;

namespace IsoOnTcp;

public struct VarParam : IComparable
{
	public byte code;

	public byte length;

	public byte[] value;

	public override bool Equals(object obj)
	{
		return code == (byte)obj;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public int CompareTo(object obj)
	{
		byte b = (byte)obj;
		if (code < b)
		{
			return -1;
		}
		if (code > b)
		{
			return 1;
		}
		return 0;
	}
}
