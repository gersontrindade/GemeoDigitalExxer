#define TRACE
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HexDumping;

namespace PlcsimS7online;

public abstract class PlcS7onlineMsgPump : Form
{
	protected enum PlcsimConnectionState
	{
		NotConnected,
		ConnectState1,
		ConnectState2,
		ConnectState3,
		Connected,
		DisconnectState1,
		DisconnectState2
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	protected struct S7OexchangeBlock
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public ushort[] unknown;

		public byte headerlength;

		public ushort user;

		public byte rb_type;

		public byte priority;

		public byte reserved_1;

		public ushort reserved_2;

		public byte subsystem;

		public byte opcode;

		public ushort response;

		public ushort fill_length_1;

		public byte reserved_3;

		public ushort seg_length_1;

		public ushort offset_1;

		public ushort reserved_4;

		public ushort fill_length_2;

		public byte reserved_5;

		public ushort seg_length_2;

		public ushort offset_2;

		public ushort reserved_6;

		public byte application_block_opcode;

		public byte application_block_subsystem;

		public ushort application_block_id;

		public ushort application_block_service;

		public byte application_block_local_address_station;

		public byte application_block_local_address_segment;

		public byte application_block_ssap;

		public byte application_block_dsap;

		public byte application_block_remote_address_station;

		public byte application_block_remote_address_segment;

		public ushort application_block_service_class;

		public int application_block_receive_l_sdu_buffer_ptr;

		public byte application_block_receive_l_sdu_length;

		public byte application_block_reserved_1;

		public byte application_block_reserved;

		public int application_block_send_l_sdu_buffer_ptr;

		public byte application_block_send_l_sdu_length;

		public ushort application_block_l_status;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public ushort[] application_block_reserved_2;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
		public byte[] reserved;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public byte[] reference;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
		public byte[] user_data_1;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class WndProcMessage
	{
		public int pdulength;

		public byte[] pdu;
	}

	public enum MessageFromPlcsimType
	{
		Pdu,
		ConnectSuccess,
		ConnectError,
		StatusMessage,
		ErrorMessage
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class MessageFromPlcsim
	{
		public MessageFromPlcsimType type;

		public string textmessage;

		public byte[] pdu;
	}

	public delegate void OnDataFromPlcsimReceived(MessageFromPlcsim message);

	private const int WM_USER = 1024;

	public const int WM_M_CONNECTPLCSIM = 2025;

	public const int WM_M_SENDDATA = 2026;

	public const int WM_M_EXIT = 2027;

	protected const int WM_SINEC = 1524;

	protected byte m_application_block_opcode;

	protected byte m_application_block_subsystem;

	protected ushort m_user;

	protected int m_connectionHandle;

	protected PlcsimConnectionState m_PlcsimConnectionState;

	protected IPAddress m_PlcIPAddress;

	protected int m_PlcRack;

	protected int m_PlcSlot;

	protected int m_FdrLen;

	protected static bool m_TraceEnabled;

	public event OnDataFromPlcsimReceived eventOnDataFromPlcsimReceived;

	public PlcS7onlineMsgPump(IPAddress plc_ipaddress, int rack, int slot)
	{
		m_PlcIPAddress = plc_ipaddress;
		m_PlcRack = rack;
		m_PlcSlot = slot;
		m_connectionHandle = -1;
		m_user = 1;
		m_PlcsimConnectionState = PlcsimConnectionState.NotConnected;
		m_FdrLen = Marshal.SizeOf((object)default(S7OexchangeBlock));
		m_TraceEnabled = false;
		if (m_TraceEnabled)
		{
			Trace.Listeners.Add(new TextWriterTraceListener("PlcsimS7online.log"));
			DoTrace("Start trace...");
		}
	}

	[DllImport("S7onlinx.dll")]
	protected static extern int SetSinecHWnd(int handle, IntPtr hwnd);

	[DllImport("S7onlinx.dll")]
	protected static extern int SetSinecHWndMsg(int handle, IntPtr hwnd, uint msg_id);

	[DllImport("S7onlinx.dll")]
	protected static extern int SCP_open([MarshalAs(UnmanagedType.LPStr)] string name);

	[DllImport("S7onlinx.dll")]
	protected static extern int SCP_close(int handle);

	[DllImport("S7onlinx.dll")]
	protected static extern int SCP_send(int handle, ushort length, byte[] data);

	[DllImport("S7onlinx.dll")]
	protected static extern int SCP_receive(int handle, ushort timeout, int[] recievendlength, ushort length, byte[] data);

	[DllImport("S7onlinx.dll")]
	protected static extern int SCP_get_errno();

	public void Run()
	{
		Application.ThreadException += Application_ThreadException;
		AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		Application.Run();
	}

	protected void SendS7OExchangeBlockToPlcsim(S7OexchangeBlock data)
	{
		int num = Marshal.SizeOf((object)data);
		byte[] array = new byte[num];
		ushort length = (ushort)(data.seg_length_1 + data.headerlength);
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.StructureToPtr((object)data, intPtr, fDeleteOld: false);
		Marshal.Copy(intPtr, array, 0, num);
		Marshal.DestroyStructure(intPtr, typeof(S7OexchangeBlock));
		Marshal.FreeHGlobal(intPtr);
		if (SCP_send(m_connectionHandle, length, array) < 0)
		{
			SendErrorMessage("ERROR: SCP_send failed");
			ExitThisThread();
		}
	}

	protected WndProcMessage GetReceivedMessage(ref Message m)
	{
		byte[] array = new byte[Marshal.ReadInt32(m.LParam, IntPtr.Size)];
		Marshal.Copy(Marshal.ReadIntPtr(m.LParam, IntPtr.Size * 2), array, 0, array.Length);
		GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
		WndProcMessage result = (WndProcMessage)Marshal.PtrToStructure(gCHandle.AddrOfPinnedObject(), typeof(WndProcMessage));
		gCHandle.Free();
		return result;
	}

	public List<string> ListReachablePartners()
	{
		List<string> list = new List<string>();
		m_connectionHandle = SCP_open("S7ONLINE");
		if (m_connectionHandle >= 0)
		{
			S7OexchangeBlock newS7OexchangeBlock = GetNewS7OexchangeBlock(1, 2, 122, 128, 255);
			newS7OexchangeBlock.priority = 1;
			newS7OexchangeBlock.fill_length_1 = 13;
			newS7OexchangeBlock.seg_length_1 = 0;
			newS7OexchangeBlock.offset_1 = 80;
			SendS7OExchangeBlockToPlcsim(newS7OexchangeBlock);
			S7OexchangeBlock s7OexchangeBlock = ReceiveFromPlcsimDirect();
			while (s7OexchangeBlock.user != 0 && s7OexchangeBlock.fill_length_1 >= 64)
			{
				IPAddress iPAddress = new IPAddress(new byte[4]
				{
					s7OexchangeBlock.user_data_1[4],
					s7OexchangeBlock.user_data_1[5],
					s7OexchangeBlock.user_data_1[6],
					s7OexchangeBlock.user_data_1[7]
				});
				string asciiStringFromBytes = GetAsciiStringFromBytes(s7OexchangeBlock.user_data_1, 32, 40);
				list.Add(iPAddress.ToString() + " [" + asciiStringFromBytes + "]");
				s7OexchangeBlock = ReceiveFromPlcsimDirect();
			}
		}
		return list;
	}

	private string GetAsciiStringFromBytes(byte[] buffer, int start, int maxlen)
	{
		for (int i = start; i < start + maxlen; i++)
		{
			if (buffer[i] == 0)
			{
				return Encoding.ASCII.GetString(buffer, start, i - start);
			}
		}
		return Encoding.ASCII.GetString(buffer, start, maxlen);
	}

	protected S7OexchangeBlock ReceiveFromPlcsimDirect()
	{
		S7OexchangeBlock s7OexchangeBlock = default(S7OexchangeBlock);
		int[] recievendlength = new int[1];
		int num = Marshal.SizeOf((object)s7OexchangeBlock);
		byte[] array = new byte[num];
		if (SCP_receive(m_connectionHandle, 0, recievendlength, (ushort)num, array) == 0)
		{
			GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			s7OexchangeBlock = (S7OexchangeBlock)Marshal.PtrToStructure(gCHandle.AddrOfPinnedObject(), typeof(S7OexchangeBlock));
			gCHandle.Free();
		}
		return s7OexchangeBlock;
	}

	protected void ExitThisThread()
	{
		DoTrace("ExitThisThread()");
		Application.ExitThread();
		if (m_connectionHandle >= 0)
		{
			SCP_close(m_connectionHandle);
			m_connectionHandle = -1;
		}
	}

	protected void SendErrorMessage(string text)
	{
		if (this.eventOnDataFromPlcsimReceived != null)
		{
			MessageFromPlcsim messageFromPlcsim = new MessageFromPlcsim();
			messageFromPlcsim.type = MessageFromPlcsimType.ErrorMessage;
			messageFromPlcsim.textmessage = text;
			messageFromPlcsim.pdu = null;
			this.eventOnDataFromPlcsimReceived(messageFromPlcsim);
		}
	}

	protected void SendConnectSuccessMessage(string text)
	{
		if (this.eventOnDataFromPlcsimReceived != null)
		{
			MessageFromPlcsim messageFromPlcsim = new MessageFromPlcsim();
			messageFromPlcsim.type = MessageFromPlcsimType.ConnectSuccess;
			messageFromPlcsim.textmessage = text;
			messageFromPlcsim.pdu = null;
			this.eventOnDataFromPlcsimReceived(messageFromPlcsim);
		}
	}

	protected void SendConnectErrorMessage(string text)
	{
		if (this.eventOnDataFromPlcsimReceived != null)
		{
			MessageFromPlcsim messageFromPlcsim = new MessageFromPlcsim();
			messageFromPlcsim.type = MessageFromPlcsimType.ConnectError;
			messageFromPlcsim.textmessage = text;
			messageFromPlcsim.pdu = null;
			this.eventOnDataFromPlcsimReceived(messageFromPlcsim);
		}
	}

	protected void SendStatusMessage(string text)
	{
		if (this.eventOnDataFromPlcsimReceived != null)
		{
			MessageFromPlcsim messageFromPlcsim = new MessageFromPlcsim();
			messageFromPlcsim.type = MessageFromPlcsimType.StatusMessage;
			messageFromPlcsim.textmessage = text;
			messageFromPlcsim.pdu = null;
			this.eventOnDataFromPlcsimReceived(messageFromPlcsim);
		}
	}

	protected void SendPduMessage(byte[] data)
	{
		if (this.eventOnDataFromPlcsimReceived != null)
		{
			MessageFromPlcsim messageFromPlcsim = new MessageFromPlcsim();
			messageFromPlcsim.type = MessageFromPlcsimType.Pdu;
			messageFromPlcsim.textmessage = string.Empty;
			messageFromPlcsim.pdu = data;
			this.eventOnDataFromPlcsimReceived(messageFromPlcsim);
		}
	}

	protected S7OexchangeBlock GetNewS7OexchangeBlock(ushort user, byte rb_type, byte subsystem, byte opcode, ushort response)
	{
		return new S7OexchangeBlock
		{
			headerlength = 80,
			user = user,
			rb_type = rb_type,
			subsystem = subsystem,
			opcode = opcode,
			response = response
		};
	}

	protected void DumpS7OexchangeBlock(S7OexchangeBlock fd)
	{
		string text = "******************** S7OexchangeBlock **********************************" + Environment.NewLine;
		text = text + "** Header Start **" + Environment.NewLine;
		text = text + "headerlength : " + fd.headerlength + Environment.NewLine;
		text = text + "user         : 0x" + $"{fd.user:X02}" + Environment.NewLine;
		text = text + "rb_type      : 0x" + $"{fd.rb_type:X02}" + Environment.NewLine;
		text = text + "priority     : 0x" + $"{fd.priority:X02}" + Environment.NewLine;
		text = text + "reserved_1   : 0x" + $"{fd.reserved_1:X02}" + Environment.NewLine;
		text = text + "reserved_2   : 0x" + $"{fd.reserved_2:X02}" + Environment.NewLine;
		text = text + "subsystem    : 0x" + $"{fd.subsystem:X02}" + Environment.NewLine;
		text = text + "opcode       : 0x" + $"{fd.opcode:X02}" + Environment.NewLine;
		text = text + "response     : 0x" + $"{fd.response:X02}" + Environment.NewLine;
		text = text + "fill_length_1: 0x" + $"{fd.fill_length_1:X02}" + Environment.NewLine;
		text = text + "reserved_3   : 0x" + $"{fd.reserved_3:X02}" + Environment.NewLine;
		text = text + "seg_length_1 : 0x" + $"{fd.seg_length_1:X02}" + Environment.NewLine;
		text = text + "offset_1     : 0x" + $"{fd.offset_1:X02}" + Environment.NewLine;
		text = text + "reserved_4   : 0x" + $"{fd.reserved_4:X02}" + Environment.NewLine;
		text = text + "fill_length_2: 0x" + $"{fd.fill_length_2:X02}" + Environment.NewLine;
		text = text + "reserved_5   : 0x" + $"{fd.reserved_5:X02}" + Environment.NewLine;
		text = text + "seg_length_2 : 0x" + $"{fd.seg_length_2:X02}" + Environment.NewLine;
		text = text + "offset_2     : 0x" + $"{fd.offset_2:X02}" + Environment.NewLine;
		text = text + "reserved_6   : 0x" + $"{fd.reserved_6:X02}" + Environment.NewLine;
		text = text + "** End of Header **" + Environment.NewLine;
		text = text + "** Start of application Block **" + Environment.NewLine;
		text = text + "application_block_opcode                 : 0x" + $"{fd.application_block_opcode:X02}" + Environment.NewLine;
		text = text + "application_block_subsystem              : 0x" + $"{fd.application_block_subsystem:X02}" + Environment.NewLine;
		text = text + "application_block_id                     : 0x" + $"{fd.application_block_id:X02}" + Environment.NewLine;
		text = text + "application_block_service                : 0x" + $"{fd.application_block_service:X02}" + Environment.NewLine;
		text = text + "application_block_local_address_station  : 0x" + $"{fd.application_block_local_address_station:X02}" + Environment.NewLine;
		text = text + "application_block_local_address_segment  : 0x" + $"{fd.application_block_local_address_segment:X02}" + Environment.NewLine;
		text = text + "application_block_ssap                   : 0x" + $"{fd.application_block_ssap:X02}" + Environment.NewLine;
		text = text + "application_block_dsap                   : 0x" + $"{fd.application_block_dsap:X02}" + Environment.NewLine;
		text = text + "application_block_remote_address_station : 0x" + $"{fd.application_block_remote_address_station:X02}" + Environment.NewLine;
		text = text + "application_block_remote_address_segment : 0x" + $"{fd.application_block_remote_address_segment:X02}" + Environment.NewLine;
		text = text + "application_block_service_class          : 0x" + $"{fd.application_block_service_class:X02}" + Environment.NewLine;
		text = text + "application_block_receive_l_sdu_length   : 0x" + $"{fd.application_block_receive_l_sdu_length:X02}" + Environment.NewLine;
		text = text + "application_block_reserved_1             : 0x" + $"{fd.application_block_reserved_1:X02}" + Environment.NewLine;
		text = text + "application_block_reserved               : 0x" + $"{fd.application_block_reserved:X02}" + Environment.NewLine;
		text = text + "application_block_send_l_sdu_length      : 0x" + $"{fd.application_block_send_l_sdu_length:X02}" + Environment.NewLine;
		text = text + "application_block_l_status               : 0x" + $"{fd.application_block_l_status:X02}" + Environment.NewLine;
		text = text + "** End of application Block **" + Environment.NewLine;
		text = text + "** Start user_data_1 **" + Environment.NewLine;
		if (fd.user_data_1 != null)
		{
			text += Utils.HexDump(fd.user_data_1, fd.user_data_1.Length);
		}
		text = text + "** End user_data_1 **" + Environment.NewLine;
		text += "**************************************************************";
		DoTrace(text, includeDate: false);
	}

	protected string GetErrTxt(int number)
	{
		return number switch
		{
			202 => "S7Online: Ressourcenengpass im Treiber oder in der Library", 
			203 => "S7Online: Konfigurationsfehler", 
			205 => "S7Online: Auftrag zur Zeit nicht erlaubt", 
			206 => "S7Online: Parameterfehler", 
			207 => "S7Online: Gerät bereits/noch nicht geöffnet.", 
			208 => "S7Online: CP reagiert nicht", 
			209 => "S7Online: Fehler in der Firmware", 
			210 => "S7Online: Speicherengpaß im Treiber", 
			215 => "S7Online: Keine Nachricht vorhanden", 
			216 => "S7Online: Fehler bei Zugriff auf Anwendungspuffer", 
			219 => "S7Online: Timeout abgelaufen", 
			225 => "S7Online: Die maximale Anzahl an Anmeldungen ist überschritten", 
			226 => "S7Online: Der Auftrag wurde abgebrochen", 
			233 => "S7Online: Ein Hilfsprogramm konnte nicht gestartet werden", 
			234 => "S7Online: Keine Autorisierung für diese Funktion vorhanden", 
			304 => "S7Online: Initialisierung noch nicht abgeschlossen", 
			305 => "S7Online: Funktion nicht implementiert", 
			4865 => "S7Online: CP-Name nicht vorhanden", 
			4866 => "S7Online: CP-Name nicht konfiguriert", 
			4867 => "S7Online: Kanalname nicht vorhanden", 
			4868 => "S7Online: Kanalname nicht konfiguriert", 
			_ => "S7Online: fehler nicht definiert", 
		};
	}

	private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
	{
		MessageBox.Show(e.Exception.Message, "Unhandled Thread Exception");
	}

	private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
	{
		MessageBox.Show((e.ExceptionObject as Exception).Message, "Unhandled UI Exception");
	}

	protected static void DoTrace(string message)
	{
		DoTrace(message, includeDate: true);
	}

	protected static void DoTrace(string message, bool includeDate)
	{
		if (m_TraceEnabled)
		{
			if (includeDate)
			{
				Trace.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") + ": " + message);
			}
			else
			{
				Trace.WriteLine(message);
			}
			Trace.Flush();
		}
	}
}
