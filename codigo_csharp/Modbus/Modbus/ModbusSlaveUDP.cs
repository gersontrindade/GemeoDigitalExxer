using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Modbus;

public sealed class ModbusSlaveUDP : ModbusSlave
{
	private UdpClient udpl;

	private ManualResetEvent mre = new ManualResetEvent(initialState: false);

	public ModbusSlaveUDP(Datastore[] modbus_db, IPAddress local_address, int port)
		: base(modbus_db)
	{
		connection_type = ConnectionType.UDP_IP;
		udpl = new UdpClient(new IPEndPoint(local_address, port));
	}

	private void DoAcceptUdpDataCallback(IAsyncResult ar)
	{
		UdpClient udpClient = null;
		UDPData uDPData = null;
		byte[] array = null;
		IPEndPoint remoteEP = null;
		try
		{
			List<byte> send_buffer = new List<byte>();
			List<byte> receive_buffer = new List<byte>();
			udpClient = (UdpClient)ar.AsyncState;
			array = udpClient.EndReceive(ar, ref remoteEP);
			uDPData = new UDPData(udpClient, array, remoteEP);
			mre.Set();
			IncomingMessagePolling(send_buffer, receive_buffer, uDPData);
		}
		catch
		{
		}
		finally
		{
			uDPData?.Close();
		}
	}

	protected override void GestRequests()
	{
		while (run)
		{
			mre.Reset();
			udpl.BeginReceive(DoAcceptUdpDataCallback, udpl);
			mre.WaitOne();
		}
	}

	public override void StartListen()
	{
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
			gest_request.Join();
			gest_request = null;
		}
		udpl.Close();
	}
}
