using System.Net;
using System.Net.Sockets;

namespace TcpLib;

public class ConnectionState
{
	internal Socket m_conn;

	internal TcpServer m_server;

	internal TcpServiceProvider m_provider;

	internal byte[] m_buffer;

	public EndPoint RemoteEndPoint => m_conn.RemoteEndPoint;

	public int AvailableData => m_conn.Available;

	public bool Connected => m_conn.Connected;

	public int Read(byte[] buffer, int offset, int count)
	{
		try
		{
			if (m_conn.Available > 0)
			{
				return m_conn.Receive(buffer, offset, count, SocketFlags.None);
			}
			return 0;
		}
		catch
		{
			return 0;
		}
	}

	public bool Write(byte[] buffer, int offset, int count)
	{
		try
		{
			m_conn.Send(buffer, offset, count, SocketFlags.None);
			return true;
		}
		catch
		{
			return false;
		}
	}

	public void EndConnection()
	{
		if (m_conn != null && m_conn.Connected)
		{
			m_conn.Shutdown(SocketShutdown.Both);
			m_conn.Close();
		}
		m_server.DropConnection(this);
	}
}
