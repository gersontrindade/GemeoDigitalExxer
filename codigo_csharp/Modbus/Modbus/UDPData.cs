using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Modbus;

internal class UDPData
{
	private MemoryStream ms;

	private UdpClient client;

	private IPEndPoint remote_ep;

	public long Length => ms.Length;

	public UDPData(UdpClient udp_client, byte[] rx_data, IPEndPoint remote_endpoint)
	{
		client = udp_client;
		remote_ep = remote_endpoint;
		ms = new MemoryStream(rx_data);
	}

	public int ReadByte()
	{
		return ms.ReadByte();
	}

	public void WriteResp(byte[] buffer, int offset, int size)
	{
		byte[] array = new byte[size];
		Buffer.BlockCopy(buffer, offset, array, 0, size);
		client.Send(array, size, remote_ep);
	}

	public void Close()
	{
		if (ms != null)
		{
			ms.Close();
		}
	}
}
