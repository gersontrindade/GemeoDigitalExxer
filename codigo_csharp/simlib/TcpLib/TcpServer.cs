using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TcpLib;

public class TcpServer : IDisposable
{
	private int m_port;

	private Socket m_listener;

	private TcpServiceProvider m_provider;

	private ArrayList m_connections;

	private int _maxConnections = 100;

	private AsyncCallback ConnectionReady;

	private WaitCallback AcceptConnection;

	private AsyncCallback ReceivedDataReady;

	private bool Disposed;

	public int MaxConnections
	{
		get
		{
			return _maxConnections;
		}
		set
		{
			_maxConnections = value;
		}
	}

	public int CurrentConnections
	{
		get
		{
			lock (this)
			{
				return m_connections.Count;
			}
		}
	}

	public TcpServer(TcpServiceProvider provider, int port)
	{
		m_provider = provider;
		m_port = port;
		m_listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		m_connections = new ArrayList();
		ConnectionReady = ConnectionReady_Handler;
		AcceptConnection = AcceptConnection_Handler;
		ReceivedDataReady = ReceivedDataReady_Handler;
	}

	~TcpServer()
	{
		Dispose(disposing: false);
	}

	private void Dispose(bool disposing)
	{
		if (disposing && !Disposed)
		{
			try
			{
				Stop();
			}
			catch
			{
			}
		}
		Disposed = true;
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	public bool Start(IPAddress ip)
	{
		try
		{
			m_listener.Bind(new IPEndPoint(ip, m_port));
			m_listener.Listen(100);
			m_listener.BeginAccept(ConnectionReady, null);
			return true;
		}
		catch
		{
			return false;
		}
	}

	private void ConnectionReady_Handler(IAsyncResult ar)
	{
		lock (this)
		{
			if (m_listener != null)
			{
				Socket socket = m_listener.EndAccept(ar);
				if (m_connections.Count >= _maxConnections)
				{
					socket.Shutdown(SocketShutdown.Both);
					socket.Close();
				}
				else
				{
					ConnectionState connectionState = new ConnectionState();
					connectionState.m_conn = socket;
					connectionState.m_server = this;
					connectionState.m_provider = (TcpServiceProvider)m_provider.Clone();
					connectionState.m_buffer = new byte[4];
					m_connections.Add(connectionState);
					ThreadPool.QueueUserWorkItem(AcceptConnection, connectionState);
				}
				m_listener.BeginAccept(ConnectionReady, null);
			}
		}
	}

	private void AcceptConnection_Handler(object state)
	{
		ConnectionState connectionState = state as ConnectionState;
		try
		{
			connectionState.m_provider.OnAcceptConnection(connectionState);
		}
		catch
		{
		}
		if (connectionState.m_conn.Connected)
		{
			connectionState.m_conn.BeginReceive(connectionState.m_buffer, 0, 0, SocketFlags.None, ReceivedDataReady, connectionState);
		}
	}

	private void ReceivedDataReady_Handler(IAsyncResult ar)
	{
		ConnectionState connectionState = ar.AsyncState as ConnectionState;
		try
		{
			if (connectionState.m_conn.Available == 0)
			{
				DropConnection(connectionState);
				return;
			}
		}
		catch
		{
			DropConnection(connectionState);
			return;
		}
		connectionState.m_conn.EndReceive(ar);
		if (connectionState.m_conn.Available == 0)
		{
			DropConnection(connectionState);
			return;
		}
		try
		{
			connectionState.m_provider.OnReceiveData(connectionState);
		}
		catch
		{
		}
		if (!connectionState.m_conn.Connected)
		{
			return;
		}
		try
		{
			connectionState.m_conn.BeginReceive(connectionState.m_buffer, 0, 0, SocketFlags.None, ReceivedDataReady, connectionState);
		}
		catch
		{
			DropConnection(connectionState);
		}
	}

	public void Stop()
	{
		lock (this)
		{
			m_listener.Close();
			m_listener = null;
			foreach (object connection in m_connections)
			{
				ConnectionState connectionState = connection as ConnectionState;
				try
				{
					connectionState.m_provider.OnDropConnection(connectionState);
				}
				catch
				{
				}
				connectionState.m_conn.Shutdown(SocketShutdown.Both);
				connectionState.m_conn.Close();
			}
			m_connections.Clear();
		}
	}

	internal void DropConnection(ConnectionState st)
	{
		lock (this)
		{
			try
			{
				st.m_provider.OnDropConnection(st);
			}
			catch
			{
			}
			try
			{
				st.m_conn.Shutdown(SocketShutdown.Both);
			}
			catch
			{
			}
			st.m_conn.Close();
			if (m_connections.Contains(st))
			{
				m_connections.Remove(st);
			}
		}
	}
}
