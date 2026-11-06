using System;
using System.Collections.Generic;
using System.Net;
using PlcsimS7online;
using TcpLib;

namespace IsoOnTcp;

public class IsoToS7online : IDisposable
{
	public delegate void dataReceived(string sourceIP, byte[] data, string message);

	private TcpServer m_Server;

	private IsoServiceProvider m_Provider;

	private bool m_enableTsapCheck;

	private bool m_Disposed;

	public Queue<QueueEntry> m_ReceiveQueue = new Queue<QueueEntry>(100);

	public Queue<QueueEntry> m_SendQueue = new Queue<QueueEntry>(100);

	private IPAddress m_NetworkIpAdress;

	public string Name => m_Provider.m_Name;

	public IPAddress NetworkIpAdress => m_NetworkIpAdress;

	public event dataReceived monitorDataReceived;

	public IsoToS7online(bool enableTsapCheck)
	{
		m_enableTsapCheck = enableTsapCheck;
	}

	~IsoToS7online()
	{
		Dispose(disposing: false);
	}

	public void start(string name, IPAddress networkIpAdress, List<byte[]> tsaps, IPAddress plcsimIp, int plcsimRackNumber, int plcsimSlotNumber)
	{
		m_Provider = new IsoServiceProvider();
		m_Provider.ISOsrv.OnReceived = IsoReceived;
		m_Provider.ISOsrv.SetValidTsaps(tsaps);
		m_Provider.ISOsrv.EnableLocalTsapCheck = m_enableTsapCheck;
		m_NetworkIpAdress = networkIpAdress;
		m_Provider.m_PlcsimIpAdress = plcsimIp;
		m_Provider.m_PlcsimRackNumber = plcsimRackNumber;
		m_Provider.m_PlcsimSlotNumber = plcsimSlotNumber;
		m_Provider.m_Name = name;
		m_Server = new TcpServer(m_Provider, 102);
		m_Server.Start(m_NetworkIpAdress);
	}

	public void stop()
	{
		m_Server.Stop();
	}

	private void Dispose(bool disposing)
	{
		if (disposing && !m_Disposed)
		{
			try
			{
				stop();
			}
			catch
			{
			}
		}
		m_Disposed = true;
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	public void IsoReceived(IsoServiceProvider client, byte[] data)
	{
		if (data.Length < 5)
		{
			return;
		}
		if (!client.m_S7ProtocolVersionDetected)
		{
			string empty = string.Empty;
			bool flag = false;
			if (data[0] == 114)
			{
				flag = client.InitPlcsim(PlcsimProtocolType.S7commPlus);
				empty = "Connecting to Plcsim using S7Comm-Plus mode for 1200/1500";
			}
			else
			{
				flag = client.InitPlcsim(PlcsimProtocolType.S7comm);
				empty = "Connecting to Plcsim using S7Comm mode for 300/400 or 1200/1500 (not optimized)";
			}
			if (!flag)
			{
				if (this.monitorDataReceived != null)
				{
					this.monitorDataReceived(client.client.RemoteEndPoint.ToString(), null, "Failed to connect to Plcsim");
				}
				client.client.EndConnection();
				return;
			}
			if (this.monitorDataReceived != null)
			{
				this.monitorDataReceived(client.client.RemoteEndPoint.ToString(), null, empty);
			}
			client.m_S7ProtocolVersionDetected = true;
		}
		PlcS7onlineMsgPump.WndProcMessage wndProcMessage = new PlcS7onlineMsgPump.WndProcMessage();
		wndProcMessage.pdu = data;
		wndProcMessage.pdulength = data.Length;
		if (this.monitorDataReceived != null)
		{
			this.monitorDataReceived(client.client.RemoteEndPoint.ToString(), data, string.Empty);
		}
		byte[] array = null;
		array = S7ProtoHook.RequestExchange(data);
		if (array == null)
		{
			client.SendDataToPlcsim(wndProcMessage);
		}
		else
		{
			client.IsoSend(client.client, array);
		}
	}
}
