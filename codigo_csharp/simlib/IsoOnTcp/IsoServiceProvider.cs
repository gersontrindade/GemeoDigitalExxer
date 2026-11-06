using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using PlcsimS7online;
using TcpLib;

namespace IsoOnTcp;

public class IsoServiceProvider : TcpServiceProvider
{
	public PlcS7onlineMsgPump m_PlcS7onlineMsgPump;

	private IntPtr m_PlcS7onlineMsgPump_Handle;

	private AutoResetEvent m_autoEvent_MsgPumpThreadStart;

	private AutoResetEvent m_autoEvent_ConnectPlcsim;

	private bool m_ConnectPlcsimSuccess;

	public int m_PlcsimRackNumber;

	public int m_PlcsimSlotNumber;

	public IPAddress m_PlcsimIpAdress;

	public bool m_S7ProtocolVersionDetected;

	public string m_Name;

	public ISOonTCP ISOsrv = new ISOonTCP();

	public ConnectionState client;

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern int SendMessage(IntPtr hwnd, [MarshalAs(UnmanagedType.U4)] int Msg, IntPtr wParam, IntPtr lParam);

	public IsoServiceProvider()
	{
		ISOsrv.Log = IsoLog;
		ISOsrv.TCPSend = TCPSend;
		m_PlcS7onlineMsgPump_Handle = IntPtr.Zero;
	}

	~IsoServiceProvider()
	{
		ExitPlcsimMessagePump();
	}

	public void IsoLog(string message)
	{
		Console.WriteLine(message);
	}

	public void TCPSend(ConnectionState state, byte[] data)
	{
		client = state;
		if (!client.Write(data, 0, data.Length))
		{
			client.EndConnection();
		}
	}

	public void IsoSend(ConnectionState state, byte[] data)
	{
		try
		{
			ISOsrv.Send(state, data);
		}
		catch
		{
			client.EndConnection();
		}
	}

	public override object Clone()
	{
		return new IsoServiceProvider
		{
			ISOsrv = 
			{
				OnReceived = ISOsrv.OnReceived,
				EnableLocalTsapCheck = ISOsrv.EnableLocalTsapCheck,
				LocalTsaps = ISOsrv.LocalTsaps
			},
			m_PlcsimIpAdress = m_PlcsimIpAdress,
			m_PlcsimRackNumber = m_PlcsimRackNumber,
			m_PlcsimSlotNumber = m_PlcsimSlotNumber,
			m_PlcS7onlineMsgPump_Handle = m_PlcS7onlineMsgPump_Handle,
			m_S7ProtocolVersionDetected = m_S7ProtocolVersionDetected,
			m_Name = m_Name
		};
	}

	public override void OnReceiveData(ConnectionState state)
	{
		client = state;
		byte[] array = new byte[1024];
		while (state.AvailableData > 0)
		{
			int num = client.Read(array, 0, 1024);
			if (num > 0)
			{
				try
				{
					ISOsrv.Process(this, array, num);
					if (!ISOsrv.Connected)
					{
						client.EndConnection();
					}
				}
				catch
				{
					client.EndConnection();
				}
			}
			else
			{
				client.EndConnection();
			}
		}
	}

	public override void OnAcceptConnection(ConnectionState state)
	{
		client = state;
	}

	public override void OnDropConnection(ConnectionState state)
	{
		ExitPlcsimMessagePump();
		client = state;
	}

	private void ExitPlcsimMessagePump()
	{
		SendMessage(m_PlcS7onlineMsgPump_Handle, 2027, IntPtr.Zero, IntPtr.Zero);
	}

	public bool InitPlcsim(PlcsimProtocolType plcsimVersion)
	{
		m_autoEvent_MsgPumpThreadStart = new AutoResetEvent(initialState: false);
		StartPlcS7onlineMsgPump(plcsimVersion);
		m_autoEvent_MsgPumpThreadStart.WaitOne();
		try
		{
			m_ConnectPlcsimSuccess = false;
			m_autoEvent_ConnectPlcsim = new AutoResetEvent(initialState: false);
			SendMessage(m_PlcS7onlineMsgPump_Handle, 2025, IntPtr.Zero, IntPtr.Zero);
			m_autoEvent_ConnectPlcsim.WaitOne();
		}
		catch
		{
			return false;
		}
		return m_ConnectPlcsimSuccess;
	}

	public void SendDataToPlcsim(PlcS7onlineMsgPump.WndProcMessage msg)
	{
		int num = Marshal.SizeOf((object)msg);
		byte[] array = new byte[num];
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.StructureToPtr((object)msg, intPtr, fDeleteOld: false);
		Marshal.Copy(intPtr, array, 0, num);
		Marshal.FreeHGlobal(intPtr);
		IntPtr intPtr2 = Marshal.AllocHGlobal(IntPtr.Size * 3 + num);
		Marshal.WriteIntPtr(intPtr2, 0, IntPtr.Zero);
		Marshal.WriteIntPtr(intPtr2, IntPtr.Size, (IntPtr)num);
		IntPtr intPtr3 = new IntPtr(intPtr2.ToInt64() + IntPtr.Size * 3);
		Marshal.WriteIntPtr(intPtr2, IntPtr.Size * 2, intPtr3);
		Marshal.Copy(array, 0, intPtr3, num);
		SendMessage(m_PlcS7onlineMsgPump_Handle, 2026, IntPtr.Zero, intPtr2);
		Marshal.FreeHGlobal(intPtr2);
	}

	public void StartPlcS7onlineMsgPump(PlcsimProtocolType plcsimVersion)
	{
		Thread thread = new Thread(StartPlcS7onlineMsgPumpThread);
		if (plcsimVersion == PlcsimProtocolType.S7commPlus)
		{
			m_PlcS7onlineMsgPump = new PlcS7onlineMsgPumpTia(m_PlcsimIpAdress, m_PlcsimRackNumber, m_PlcsimSlotNumber);
		}
		else
		{
			m_PlcS7onlineMsgPump = new PlcS7onlineMsgPumpS7(m_PlcsimIpAdress, m_PlcsimRackNumber, m_PlcsimSlotNumber);
		}
		m_PlcS7onlineMsgPump.eventOnDataFromPlcsimReceived += OnDataFromPlcsimReceived;
		thread.Start();
	}

	private void StartPlcS7onlineMsgPumpThread()
	{
		m_PlcS7onlineMsgPump_Handle = m_PlcS7onlineMsgPump.Handle;
		m_autoEvent_MsgPumpThreadStart.Set();
		m_PlcS7onlineMsgPump.Run();
	}

	private void OnDataFromPlcsimReceived(PlcS7onlineMsgPump.MessageFromPlcsim message)
	{
		switch (message.type)
		{
		case PlcS7onlineMsgPump.MessageFromPlcsimType.Pdu:
			IsoSend(client, message.pdu);
			break;
		case PlcS7onlineMsgPump.MessageFromPlcsimType.ConnectError:
			m_ConnectPlcsimSuccess = false;
			m_autoEvent_ConnectPlcsim.Set();
			break;
		case PlcS7onlineMsgPump.MessageFromPlcsimType.ConnectSuccess:
			m_ConnectPlcsimSuccess = true;
			m_autoEvent_ConnectPlcsim.Set();
			break;
		}
	}
}
