using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Modbus;

public sealed class ModbusSlaveTCP : ModbusSlave
{
	private TcpListener tcpl;

	private ManualResetEvent mre = new ManualResetEvent(initialState: false);

	private List<IPEndPoint> remote_clients_connected = new List<IPEndPoint>();

	public IPEndPoint[] RemoteClientsConnected => remote_clients_connected.ToArray();

	public event EventHandler<ModbusTCPUDPClientConnectedEventArgs> TCPClientConnected;

	public event EventHandler<ModbusTCPUDPClientConnectedEventArgs> TCPClientDisconnected;

	public ModbusSlaveTCP(Datastore[] modbus_db, IPAddress local_address, int port)
		: base(modbus_db)
	{
		connection_type = ConnectionType.TCP_IP;
		tcpl = new TcpListener(local_address, port);
	}

	private bool IsClientConnected(TcpClient client)
	{
		bool result = true;
		if (client.Client.Poll(0, SelectMode.SelectRead))
		{
			byte[] buffer = new byte[1];
			if (client.Client.Receive(buffer, SocketFlags.Peek) == 0)
			{
				result = false;
			}
		}
		return result;
	}

	private void DoAcceptTcpClientCallback(IAsyncResult ar)
	{
		TcpListener tcpListener = null;
		TcpClient tcpClient = null;
		NetworkStream networkStream = null;
		IPEndPoint iPEndPoint = null;
		try
		{
			List<byte> send_buffer = new List<byte>();
			List<byte> receive_buffer = new List<byte>();
			tcpListener = (TcpListener)ar.AsyncState;
			tcpClient = tcpListener.EndAcceptTcpClient(ar);
			iPEndPoint = (IPEndPoint)tcpClient.Client.RemoteEndPoint;
			remote_clients_connected.Add(iPEndPoint);
			this.TCPClientConnected?.Invoke(this, new ModbusTCPUDPClientConnectedEventArgs(iPEndPoint));
			mre.Set();
			networkStream = tcpClient.GetStream();
			while (run && IsClientConnected(tcpClient))
			{
				IncomingMessagePolling(send_buffer, receive_buffer, networkStream);
				Thread.Sleep(1);
			}
		}
		catch
		{
		}
		finally
		{
			networkStream?.Close();
			tcpClient?.Close();
			if (iPEndPoint != null)
			{
				remote_clients_connected.Remove(iPEndPoint);
				this.TCPClientDisconnected?.Invoke(this, new ModbusTCPUDPClientConnectedEventArgs(iPEndPoint));
			}
		}
	}

	protected override void GestRequests()
	{
		while (run)
		{
			mre.Reset();
			tcpl.BeginAcceptTcpClient(DoAcceptTcpClientCallback, tcpl);
			mre.WaitOne();
		}
	}

	public override void StartListen()
	{
		tcpl.Start();
		if (gest_request == null)
		{
			run = true;
			gest_request = new Thread(GestRequests);
			gest_request.Start();
		}
	}

	public override void StopListen()
	{
		if (gest_request != null)
		{
			run = false;
			mre.Set();
			gest_request.Join();
			gest_request = null;
		}
		tcpl.Stop();
	}
}
