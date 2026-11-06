using System;
using System.Collections.Generic;
using TcpLib;

namespace IsoOnTcp;

public class ISOonTCP
{
	public delegate void _OnReceived(IsoServiceProvider client, byte[] data);

	public delegate void _TCPSend(ConnectionState state, byte[] data);

	public delegate void _Log(string message);

	public bool Connected;

	public int PduSize = 9;

	public List<byte[]> LocalTsaps;

	public bool EnableLocalTsapCheck;

	public _OnReceived OnReceived;

	public _TCPSend TCPSend;

	public _Log Log;

	public void Process(IsoServiceProvider client, byte[] packet)
	{
		Process(client, packet, packet.Length);
	}

	public void SetValidTsaps(List<byte[]> tsaps)
	{
		LocalTsaps = tsaps;
	}

	public void Process(IsoServiceProvider client, byte[] inPacket, int len)
	{
		byte[] array = new byte[len];
		Array.Copy(inPacket, array, len);
		int num = 0;
		while (num < len)
		{
			TPKT tPKT = new TPKT(array, len);
			TPDU tPDU = new TPDU(tPKT.Payload);
			switch (tPDU.PDUType)
			{
			case 14:
				if (!Connected)
				{
					TPKT tPKT2 = new TPKT();
					TPDU tPDU2 = new TPDU();
					tPDU2.PDUType = 13;
					Connected = false;
					try
					{
						tPDU2.PduCon = tPDU.PduCon.HandleConnectRequest(LocalTsaps, PduSize, EnableLocalTsapCheck);
						tPKT2.SetPayload(tPDU2.GetBytes());
						TCPSend(client.client, tPKT2.GetBytes());
						Connected = true;
					}
					catch
					{
					}
				}
				break;
			case 15:
				if (!Connected)
				{
					Log("DT packet before state 'connected' received.");
				}
				else
				{
					OnReceived(client, tPDU.PduData.Payload);
				}
				break;
			case 8:
				client.client.EndConnection();
				break;
			default:
				Log("Cannot handle pdu type " + tPDU.PDUType);
				break;
			}
			num += tPKT.Length;
			Array.Copy(inPacket, num, array, 0, len - num);
		}
	}

	public int Send(ConnectionState state, byte[] data, int len)
	{
		byte[] array = new byte[len];
		Array.Copy(data, array, len);
		return Send(state, array);
	}

	public int Send(ConnectionState state, byte[] data)
	{
		if (!Connected)
		{
			Log("Cannot send in state 'not connected'");
			return -1;
		}
		TPKT tPKT = new TPKT();
		TPDU tPDU = new TPDU();
		tPDU.MakeDataPacket(data);
		tPKT.SetPayload(tPDU.GetBytes());
		TCPSend(state, tPKT.GetBytes());
		return 0;
	}
}
