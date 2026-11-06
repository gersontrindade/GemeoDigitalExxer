using System;

namespace IsoOnTcp;

public class TPKT
{
	public const int TPKT_HEADER_LENGTH = 4;

	public const int TPKT_VERSION = 3;

	public int Version = 3;

	public int Reserved;

	public ushort Length;

	private byte[] _Payload;

	public int PayloadLength;

	public byte[] Payload
	{
		get
		{
			return _Payload;
		}
		set
		{
			SetPayload(value);
		}
	}

	public TPKT()
	{
	}

	public TPKT(byte[] packet)
		: this(packet, packet.Length)
	{
	}

	public TPKT(byte[] packet, int packetLen)
	{
		if (packetLen < 4)
		{
			throw new Exception("TPKT: The packet did not contain the minimum number of bytes for an TPKT header packet.");
		}
		Version = packet[0];
		if (Version != 3)
		{
			throw new Exception("TPKT: Version in header is not valid (!=3).");
		}
		if (BitConverter.IsLittleEndian)
		{
			Length = ByteConvert.DoReverseEndian(BitConverter.ToUInt16(packet, 2));
		}
		else
		{
			Length = BitConverter.ToUInt16(packet, 2);
		}
		if (Length > packetLen)
		{
			throw new Exception("TPKT: Length in header is greater than packet length.");
		}
		PayloadLength = Length - 4;
		_Payload = new byte[PayloadLength];
		Array.Copy(packet, 4, _Payload, 0, PayloadLength);
	}

	public void SetPayload(byte[] data)
	{
		_Payload = new byte[data.Length];
		Array.Copy(data, 0, _Payload, 0, data.Length);
		PayloadLength = data.Length;
		Length = Convert.ToUInt16(PayloadLength + 4);
	}

	public byte[] GetBytes()
	{
		byte[] array = new byte[Length];
		array[0] = Convert.ToByte(Version);
		array[1] = Convert.ToByte(Reserved);
		if (BitConverter.IsLittleEndian)
		{
			Buffer.BlockCopy(BitConverter.GetBytes(ByteConvert.DoReverseEndian(Length)), 0, array, 2, 2);
		}
		else
		{
			Buffer.BlockCopy(BitConverter.GetBytes(Length), 0, array, 2, 2);
		}
		Array.Copy(_Payload, 0, array, 4, PayloadLength);
		return array;
	}
}
