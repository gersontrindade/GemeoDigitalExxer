using System;

namespace IsoOnTcp;

public class TPDU
{
	public enum TPDU_TYPES
	{
		ED = 1,
		EA = 2,
		UD = 4,
		RJ = 5,
		AK = 6,
		ER = 7,
		DR = 8,
		DC = 12,
		CC = 13,
		CR = 14,
		DT = 15
	}

	public ushort Li;

	public int PDUType;

	public TPDUData PduData;

	public TPDUConnection PduCon;

	public TPDU()
	{
	}

	public TPDU(byte[] packet)
		: this(packet, packet.Length)
	{
	}

	private TPDU(byte[] packet, int packetLen)
	{
		if (packetLen < 3)
		{
			throw new Exception("TPUD: Packet size lower than 3 bytes.");
		}
		Li = packet[0];
		PDUType = packet[1] >> 4;
		switch (PDUType)
		{
		case 14:
			PduCon = new TPDUConnection(packet);
			break;
		case 13:
			PduCon = new TPDUConnection(packet);
			break;
		case 15:
			PduData = new TPDUData(packet);
			break;
		}
	}

	public byte[] GetBytes()
	{
		byte[] array = null;
		switch (PDUType)
		{
		case 15:
			array = new byte[PduData.PayloadLength + 3];
			Li = 2;
			array[0] = Convert.ToByte(Li);
			array[1] = Convert.ToByte(PDUType << 4);
			array[2] = Convert.ToByte(PduData.TPDUNr);
			if (PduData.EOT)
			{
				array[2] += 128;
			}
			Array.Copy(PduData.Payload, 0, array, 3, PduData.Payload.Length);
			break;
		case 13:
		case 14:
		{
			byte[] bytes = PduCon.GetBytes();
			int num = bytes.Length + 2;
			Li = Convert.ToByte(bytes.Length + 1);
			array = new byte[num];
			array[0] = Convert.ToByte(Li);
			array[1] = Convert.ToByte(PDUType << 4);
			Array.Copy(bytes, 0, array, 2, bytes.Length);
			break;
		}
		}
		return array;
	}

	public void MakeDataPacket(byte[] data)
	{
		PDUType = 15;
		Li = 2;
		PduData = new TPDUData();
		PduData.TPDUNr = 0;
		PduData.EOT = true;
		PduData.Payload = new byte[data.Length];
		PduData.PayloadLength = data.Length;
		Array.Copy(data, PduData.Payload, data.Length);
	}
}
