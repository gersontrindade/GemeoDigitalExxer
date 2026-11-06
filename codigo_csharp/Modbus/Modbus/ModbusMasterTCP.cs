using System.Net.Sockets;

namespace Modbus;

public class ModbusMasterTCP : ModbusMaster
{
	private string _remote_host;

	private int _port;

	private TcpClient tcpc;

	private NetworkStream ns;

	public ModbusMasterTCP(string remote_host, int port)
	{
		connection_type = ConnectionType.TCP_IP;
		_remote_host = remote_host;
		_port = port;
	}

	public override void Connect()
	{
		if (tcpc == null)
		{
			tcpc = new TcpClient();
		}
		tcpc.Connect(_remote_host, _port);
		if (tcpc.Connected)
		{
			ns = tcpc.GetStream();
			connected = true;
		}
	}

	public override void Disconnect()
	{
		ns.Close();
		tcpc.Close();
		tcpc = null;
		connected = false;
	}

	protected override void Send()
	{
		ns.Write(send_buffer.ToArray(), 0, send_buffer.Count);
	}

	protected override int ReceiveByte()
	{
		if (ns.DataAvailable)
		{
			return ns.ReadByte();
		}
		return -1;
	}
}
