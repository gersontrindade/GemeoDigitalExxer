using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Modbus;

public class ModbusMasterUDP : ModbusMaster
{
	private string _remote_host;

	private int _port;

	private List<byte> tmp_rx_buffer = new List<byte>();

	private UdpClient udpc;

	public ModbusMasterUDP(string remote_host, int port)
	{
		connection_type = ConnectionType.UDP_IP;
		_remote_host = remote_host;
		_port = port;
		udpc = new UdpClient();
	}

	public override void Connect()
	{
		udpc.Connect(_remote_host, _port);
		if (udpc.Client.Connected)
		{
			connected = true;
		}
	}

	public override void Disconnect()
	{
		udpc.Close();
		connected = false;
	}

	protected override void Send()
	{
		udpc.Send(send_buffer.ToArray(), send_buffer.Count);
	}

	protected override int ReceiveByte()
	{
		IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
		if (udpc.Available > 0)
		{
			tmp_rx_buffer.AddRange(udpc.Receive(ref remoteEP));
		}
		if (tmp_rx_buffer.Count > 0)
		{
			byte result = tmp_rx_buffer[0];
			tmp_rx_buffer.RemoveAt(0);
			return result;
		}
		return -1;
	}
}
