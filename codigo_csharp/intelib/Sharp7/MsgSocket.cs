using System;
using System.Net.Sockets;
using System.Threading;

namespace Sharp7;

internal class MsgSocket
{
	private Socket TCPSocket;

	private int _ReadTimeout = 2000;

	private int _WriteTimeout = 2000;

	private int _ConnectTimeout = 1000;

	public int LastError;

	public bool Connected
	{
		get
		{
			if (TCPSocket != null)
			{
				return TCPSocket.Connected;
			}
			return false;
		}
	}

	public int ReadTimeout
	{
		get
		{
			return _ReadTimeout;
		}
		set
		{
			_ReadTimeout = value;
		}
	}

	public int WriteTimeout
	{
		get
		{
			return _WriteTimeout;
		}
		set
		{
			_WriteTimeout = value;
		}
	}

	public int ConnectTimeout
	{
		get
		{
			return _ConnectTimeout;
		}
		set
		{
			_ConnectTimeout = value;
		}
	}

	~MsgSocket()
	{
		Close();
	}

	public void Close()
	{
		if (TCPSocket != null)
		{
			TCPSocket.Dispose();
			TCPSocket = null;
		}
	}

	private void CreateSocket()
	{
		TCPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		TCPSocket.NoDelay = true;
	}

	private void TCPPing(string Host, int Port)
	{
		LastError = 0;
		Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		try
		{
			if (!socket.BeginConnect(Host, Port, null, null).AsyncWaitHandle.WaitOne(_ConnectTimeout, exitContext: true))
			{
				LastError = 3;
			}
		}
		catch
		{
			LastError = 3;
		}
		socket.Close();
	}

	public int Connect(string Host, int Port)
	{
		LastError = 0;
		if (!Connected)
		{
			TCPPing(Host, Port);
			if (LastError == 0)
			{
				try
				{
					CreateSocket();
					TCPSocket.Connect(Host, Port);
				}
				catch
				{
					LastError = 3;
				}
			}
		}
		return LastError;
	}

	private int WaitForData(int Size, int Timeout)
	{
		bool flag = false;
		int tickCount = Environment.TickCount;
		LastError = 0;
		try
		{
			int available = TCPSocket.Available;
			while (available < Size && !flag)
			{
				Thread.Sleep(2);
				available = TCPSocket.Available;
				flag = Environment.TickCount - tickCount > Timeout;
				if (flag && available > 0)
				{
					try
					{
						byte[] buffer = new byte[available];
						TCPSocket.Receive(buffer, 0, available, SocketFlags.None);
					}
					catch
					{
					}
				}
			}
		}
		catch
		{
			LastError = 5;
		}
		if (flag)
		{
			LastError = 5;
		}
		return LastError;
	}

	public int Receive(byte[] Buffer, int Start, int Size)
	{
		int num = 0;
		LastError = WaitForData(Size, _ReadTimeout);
		if (LastError == 0)
		{
			try
			{
				num = TCPSocket.Receive(Buffer, Start, Size, SocketFlags.None);
			}
			catch
			{
				LastError = 5;
			}
			if (num == 0)
			{
				LastError = 5;
				Close();
			}
		}
		return LastError;
	}

	public int Send(byte[] Buffer, int Size)
	{
		LastError = 0;
		try
		{
			TCPSocket.Send(Buffer, Size, SocketFlags.None);
		}
		catch
		{
			LastError = 7;
			Close();
		}
		return LastError;
	}
}
