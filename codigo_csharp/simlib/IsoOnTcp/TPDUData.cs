using System;

namespace IsoOnTcp;

public class TPDUData
{
	public const int HLEN_LI_NORMAL_DT_CLASS_01 = 2;

	public const int DT_HLEN = 3;

	public int TPDUNr;

	public bool EOT;

	public byte[] Payload;

	public int PayloadLength;

	public TPDUData()
	{
	}

	public TPDUData(byte[] packet)
		: this(packet, packet.Length)
	{
	}

	public TPDUData(byte[] packet, int packetLen)
	{
		if (packetLen < 3)
		{
			throw new Exception("TPDU: DT packet size lower than minimum of 3 bytes.");
		}
		byte num = packet[0];
		if (packet[1] >> 4 != 15)
		{
			throw new ApplicationException("TPDU: This can only handle DT TDPUs");
		}
		if (num != 2)
		{
			throw new Exception("TPDU: Header length indicator in DT packet other than 2 in class 0/1 PDUs are not allowed.");
		}
		TPDUNr = packet[2] & 0x7F;
		if ((packet[2] & 0x80) != 0)
		{
			EOT = true;
		}
		else
		{
			EOT = false;
		}
		PayloadLength = packetLen - 3;
		Payload = new byte[PayloadLength];
		Array.Copy(packet, 3, Payload, 0, PayloadLength);
	}
}
