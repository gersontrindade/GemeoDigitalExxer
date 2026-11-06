using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PlcsimS7online;

public class PlcS7onlineMsgPumpTia : PlcS7onlineMsgPump
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	private struct UserDataConnectionConfigTia
	{
		public byte routing_enabled;

		public byte b01;

		public byte b02;

		public byte b03;

		public byte b04;

		public byte b05;

		public byte b06;

		public byte b07;

		public byte b08;

		public byte destination_1;

		public byte destination_2;

		public byte destination_3;

		public byte destination_4;

		public byte b13;

		public byte b14;

		public byte b15;

		public byte b16;

		public byte accessname_length;

		public byte accessname01;

		public byte accessname02;

		public byte accessname03;

		public byte accessname04;

		public byte accessname05;

		public byte accessname06;

		public byte accessname07;

		public byte accessname08;

		public byte accessname09;

		public byte accessname10;

		public byte accessname11;

		public byte accessname12;

		public byte accessname13;

		public byte accessname14;

		public byte accessname15;

		public byte accessname16;

		public UserDataConnectionConfigTia(bool useConstructor)
		{
			this = default(UserDataConnectionConfigTia);
			b01 = 2;
			b02 = 6;
			b03 = 32;
			b04 = 12;
			b05 = 1;
			b15 = 1;
			accessname_length = 16;
			accessname01 = Convert.ToByte('S');
			accessname02 = Convert.ToByte('I');
			accessname03 = Convert.ToByte('M');
			accessname04 = Convert.ToByte('A');
			accessname05 = Convert.ToByte('T');
			accessname06 = Convert.ToByte('I');
			accessname07 = Convert.ToByte('C');
			accessname08 = Convert.ToByte('-');
			accessname09 = Convert.ToByte('R');
			accessname10 = Convert.ToByte('O');
			accessname11 = Convert.ToByte('O');
			accessname12 = Convert.ToByte('T');
			accessname13 = Convert.ToByte('-');
			accessname14 = Convert.ToByte('H');
			accessname15 = Convert.ToByte('M');
			accessname16 = Convert.ToByte('I');
		}
	}

	private const int SIZE_OF_USER_DATA_1 = 1024;

	private const int SEG_LENGTH_1 = 1024;

	public PlcS7onlineMsgPumpTia(IPAddress plc_ipaddress, int rack, int slot)
		: base(plc_ipaddress, rack, slot)
	{
	}

	protected override void WndProc(ref Message m)
	{
		switch (m.Msg)
		{
		case 2025:
			ConnectPlcsimStatemachine();
			break;
		case 2026:
			if (m.LParam != IntPtr.Zero && m_PlcsimConnectionState == PlcsimConnectionState.Connected)
			{
				WndProcMessage receivedMessage = GetReceivedMessage(ref m);
				SendDataToPlcsim(receivedMessage);
			}
			break;
		case 2027:
			if (m_PlcsimConnectionState == PlcsimConnectionState.Connected)
			{
				m_PlcsimConnectionState = PlcsimConnectionState.DisconnectState1;
				DisconnectPlcsimStatemachine();
			}
			break;
		case 1524:
			switch (m_PlcsimConnectionState)
			{
			case PlcsimConnectionState.NotConnected:
			case PlcsimConnectionState.ConnectState1:
			case PlcsimConnectionState.ConnectState2:
			case PlcsimConnectionState.ConnectState3:
				ConnectPlcsimStatemachine();
				break;
			case PlcsimConnectionState.Connected:
				ReceiveFromPlcsimAndSendBack();
				break;
			}
			break;
		default:
			base.WndProc(ref m);
			break;
		}
	}

	private void ReceiveFromPlcsimAndSendBack()
	{
		int[] recievendlength = new int[1];
		byte[] array = new byte[m_FdrLen];
		if (PlcS7onlineMsgPump.SCP_receive(m_connectionHandle, 0, recievendlength, (ushort)m_FdrLen, array) == -1)
		{
			if (m_connectionHandle > 0)
			{
				if (m_PlcsimConnectionState == PlcsimConnectionState.ConnectState1 || m_PlcsimConnectionState == PlcsimConnectionState.ConnectState2 || m_PlcsimConnectionState == PlcsimConnectionState.ConnectState3)
				{
					SendConnectErrorMessage("ERROR: Connect failed in SCP_receive at " + m_PlcsimConnectionState);
				}
				else
				{
					int number = PlcS7onlineMsgPump.SCP_get_errno();
					string errTxt = GetErrTxt(number);
					SendErrorMessage("ERROR: SCP_receive() failed. m_connectionHandle = " + m_connectionHandle + " SCP_get_errno=" + number + " (" + errTxt + ")");
				}
			}
			ExitThisThread();
		}
		else
		{
			GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			S7OexchangeBlock s7OexchangeBlock = (S7OexchangeBlock)Marshal.PtrToStructure(gCHandle.AddrOfPinnedObject(), typeof(S7OexchangeBlock));
			gCHandle.Free();
			if ((s7OexchangeBlock.opcode != 6 || s7OexchangeBlock.response != 1) && s7OexchangeBlock.opcode == 7 && s7OexchangeBlock.response == 3)
			{
				byte[] array2 = new byte[s7OexchangeBlock.fill_length_1];
				Array.Copy(s7OexchangeBlock.user_data_1, array2, s7OexchangeBlock.fill_length_1);
				SendPduMessage(array2);
				S7OexchangeBlock newS7OexchangeBlock = GetNewS7OexchangeBlock(0, 2, 64, 7, ushort.MaxValue);
				newS7OexchangeBlock.offset_1 = 80;
				newS7OexchangeBlock.fill_length_1 = 0;
				newS7OexchangeBlock.seg_length_1 = 1024;
				newS7OexchangeBlock.application_block_opcode = s7OexchangeBlock.application_block_opcode;
				newS7OexchangeBlock.application_block_subsystem = s7OexchangeBlock.application_block_subsystem;
				SendS7OExchangeBlockToPlcsim(newS7OexchangeBlock);
			}
		}
	}

	private void SendDataToPlcsim(WndProcMessage msg)
	{
		if (++m_user >= 32000)
		{
			m_user = 1;
		}
		S7OexchangeBlock newS7OexchangeBlock = GetNewS7OexchangeBlock(0, 2, 64, 6, ushort.MaxValue);
		newS7OexchangeBlock.offset_1 = 80;
		newS7OexchangeBlock.application_block_opcode = m_application_block_opcode;
		newS7OexchangeBlock.application_block_subsystem = m_application_block_subsystem;
		newS7OexchangeBlock.fill_length_1 = (ushort)msg.pdu.Length;
		newS7OexchangeBlock.seg_length_1 = 1024;
		newS7OexchangeBlock.user_data_1 = new byte[1024];
		Array.Copy(msg.pdu, newS7OexchangeBlock.user_data_1, msg.pdu.Length);
		SendS7OExchangeBlockToPlcsim(newS7OexchangeBlock);
	}

	private void ConnectPlcsimStatemachine()
	{
		switch (m_PlcsimConnectionState)
		{
		case PlcsimConnectionState.NotConnected:
		{
			m_connectionHandle = PlcS7onlineMsgPump.SCP_open("S7ONLINE");
			if (m_connectionHandle < 0)
			{
				SendConnectErrorMessage("ERROR: SCP_open failed");
				m_PlcsimConnectionState = PlcsimConnectionState.NotConnected;
				ExitThisThread();
			}
			PlcS7onlineMsgPump.SetSinecHWndMsg(m_connectionHandle, base.Handle, 1524u);
			S7OexchangeBlock newS7OexchangeBlock = GetNewS7OexchangeBlock(0, 2, 64, 0, ushort.MaxValue);
			newS7OexchangeBlock.offset_1 = 80;
			newS7OexchangeBlock.application_block_opcode = byte.MaxValue;
			newS7OexchangeBlock.application_block_subsystem = byte.MaxValue;
			SendS7OExchangeBlockToPlcsim(newS7OexchangeBlock);
			m_PlcsimConnectionState = PlcsimConnectionState.ConnectState1;
			break;
		}
		case PlcsimConnectionState.ConnectState1:
		{
			S7OexchangeBlock s7OexchangeBlock = ReceiveFromPlcsimDirect();
			if (s7OexchangeBlock.opcode != 0 || s7OexchangeBlock.response != 1)
			{
				SendConnectErrorMessage("ERROR: Connect failed at ConnectState1");
				m_PlcsimConnectionState = PlcsimConnectionState.NotConnected;
				ExitThisThread();
			}
			m_application_block_opcode = s7OexchangeBlock.application_block_opcode;
			m_application_block_subsystem = s7OexchangeBlock.application_block_subsystem;
			S7OexchangeBlock newS7OexchangeBlock = GetNewS7OexchangeBlock(0, 2, 64, 1, ushort.MaxValue);
			newS7OexchangeBlock.fill_length_1 = 34;
			newS7OexchangeBlock.seg_length_1 = 34;
			newS7OexchangeBlock.offset_1 = 80;
			newS7OexchangeBlock.application_block_opcode = m_application_block_opcode;
			newS7OexchangeBlock.application_block_subsystem = m_application_block_subsystem;
			UserDataConnectionConfigTia userDataConnectionConfigTia = new UserDataConnectionConfigTia(useConstructor: true);
			userDataConnectionConfigTia.destination_1 = m_PlcIPAddress.GetAddressBytes()[0];
			userDataConnectionConfigTia.destination_2 = m_PlcIPAddress.GetAddressBytes()[1];
			userDataConnectionConfigTia.destination_3 = m_PlcIPAddress.GetAddressBytes()[2];
			userDataConnectionConfigTia.destination_4 = m_PlcIPAddress.GetAddressBytes()[3];
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf((object)userDataConnectionConfigTia));
			Marshal.StructureToPtr((object)userDataConnectionConfigTia, intPtr, fDeleteOld: false);
			newS7OexchangeBlock.user_data_1 = new byte[1024];
			Marshal.Copy(intPtr, newS7OexchangeBlock.user_data_1, 0, Marshal.SizeOf((object)userDataConnectionConfigTia));
			Marshal.FreeHGlobal(intPtr);
			SendS7OExchangeBlockToPlcsim(newS7OexchangeBlock);
			m_PlcsimConnectionState = PlcsimConnectionState.ConnectState2;
			break;
		}
		case PlcsimConnectionState.ConnectState2:
		{
			S7OexchangeBlock s7OexchangeBlock = ReceiveFromPlcsimDirect();
			if (s7OexchangeBlock.opcode != 1 || s7OexchangeBlock.response != 1)
			{
				SendConnectErrorMessage("ERROR: Connect failed at ConnectState2");
				m_PlcsimConnectionState = PlcsimConnectionState.NotConnected;
				ExitThisThread();
				break;
			}
			S7OexchangeBlock newS7OexchangeBlock = GetNewS7OexchangeBlock(0, 2, 64, 13, ushort.MaxValue);
			newS7OexchangeBlock.fill_length_1 = 0;
			newS7OexchangeBlock.seg_length_1 = 1024;
			newS7OexchangeBlock.offset_1 = 80;
			newS7OexchangeBlock.application_block_opcode = m_application_block_opcode;
			newS7OexchangeBlock.application_block_subsystem = m_application_block_subsystem;
			SendS7OExchangeBlockToPlcsim(newS7OexchangeBlock);
			newS7OexchangeBlock = GetNewS7OexchangeBlock(0, 2, 64, 7, ushort.MaxValue);
			newS7OexchangeBlock.fill_length_1 = 0;
			newS7OexchangeBlock.seg_length_1 = 0;
			newS7OexchangeBlock.offset_1 = 80;
			newS7OexchangeBlock.application_block_opcode = m_application_block_opcode;
			newS7OexchangeBlock.application_block_subsystem = m_application_block_subsystem;
			SendS7OExchangeBlockToPlcsim(newS7OexchangeBlock);
			SendConnectSuccessMessage("Connected");
			m_PlcsimConnectionState = PlcsimConnectionState.Connected;
			break;
		}
		}
	}

	private void DisconnectPlcsimStatemachine()
	{
		if (m_PlcsimConnectionState == PlcsimConnectionState.DisconnectState1)
		{
			S7OexchangeBlock s7OexchangeBlock = default(S7OexchangeBlock);
			s7OexchangeBlock = GetNewS7OexchangeBlock(0, 2, 64, 12, ushort.MaxValue);
			s7OexchangeBlock.fill_length_1 = 0;
			s7OexchangeBlock.seg_length_1 = 0;
			s7OexchangeBlock.offset_1 = 80;
			s7OexchangeBlock.application_block_opcode = m_application_block_opcode;
			s7OexchangeBlock.application_block_subsystem = m_application_block_subsystem;
			SendS7OExchangeBlockToPlcsim(s7OexchangeBlock);
			m_PlcsimConnectionState = PlcsimConnectionState.NotConnected;
			ExitThisThread();
		}
	}
}
