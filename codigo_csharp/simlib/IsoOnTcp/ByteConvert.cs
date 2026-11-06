using System;
using System.Net;

namespace IsoOnTcp;

internal class ByteConvert
{
	public static ushort DoReverseEndian(ushort x)
	{
		return Convert.ToUInt16(((x << 8) & 0xFF00) | (x >> 8));
	}

	public static uint DoReverseEndian(uint x)
	{
		return (x << 24) | ((x & 0xFF00) << 8) | ((x & 0xFF0000) >> 8) | (x >> 24);
	}

	public static ulong DoReverseEndian(ulong x)
	{
		return (x << 56) | ((x & 0xFF00) << 40) | ((x & 0xFF0000) << 24) | ((x & 0xFF000000u) << 8) | ((x & 0xFF00000000L) >> 8) | ((x & 0xFF0000000000L) >> 24) | ((x & 0xFF000000000000L) >> 40) | (x >> 56);
	}

	public static short GetInt16at(byte[] value, int startindex)
	{
		if (BitConverter.IsLittleEndian)
		{
			return IPAddress.HostToNetworkOrder(BitConverter.ToInt16(value, startindex));
		}
		return BitConverter.ToInt16(value, startindex);
	}
}
