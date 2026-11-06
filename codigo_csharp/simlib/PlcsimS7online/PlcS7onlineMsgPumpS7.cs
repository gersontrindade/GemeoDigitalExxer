using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PlcsimS7online;

public class PlcS7onlineMsgPumpS7 : PlcS7onlineMsgPump
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	private struct UserDataConnectionConfigS7
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

		public byte b17;

		public byte connection_type;

		public byte rack_slot;

		public byte b20;

		public byte size_to_end;

		public byte size_of_subnet;

		public byte subnet_1;

		public byte subnet_2;

		public byte b25;

		public byte b26;

		public byte subnet_3;

		public byte subnet_4;

		public byte size_of_routing_destination;

		public byte routing_destination_1;

		public byte routing_destination_2;

		public byte routing_destination_3;

		public byte routing_destination_4;

		public UserDataConnectionConfigS7(bool useConstructor)
		{
			this = default(UserDataConnectionConfigS7);
			b01 = 2;
			b02 = 1;
			b04 = 12;
			b05 = 1;
			b15 = 1;
			b17 = 2;
		}
	}

	private const int SIZE_OF_USER_DATA_1 = 1024;

	private const int SEG_LENGTH_1 = 480;

	private bool m_opcode6_response1_was_received;

	public PlcS7onlineMsgPumpS7(IPAddress plc_ipaddress, int rack, int slot)
		: base(plc_ipaddress, rack, slot)
	{
		m_opcode6_response1_was_received = false;
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
			ExitThisThread();
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
			return;
		}
		GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
		S7OexchangeBlock s7OexchangeBlock = (S7OexchangeBlock)Marshal.PtrToStructure(gCHandle.AddrOfPinnedObject(), typeof(S7OexchangeBlock));
		gCHandle.Free();
		if (s7OexchangeBlock.opcode == 6 && s7OexchangeBlock.response == 1)
		{
			if (!m_opcode6_response1_was_received)
			{
				S7OexchangeBlock newS7OexchangeBlock = GetNewS7OexchangeBlock(0, 2, 64, 7, 255);
				newS7OexchangeBlock.offset_1 = 80;
				newS7OexchangeBlock.fill_length_1 = 0;
				newS7OexchangeBlock.seg_length_1 = 480;
				newS7OexchangeBlock.application_block_opcode = s7OexchangeBlock.application_block_opcode;
				newS7OexchangeBlock.application_block_subsystem = s7OexchangeBlock.application_block_subsystem;
				SendS7OExchangeBlockToPlcsim(newS7OexchangeBlock);
				m_opcode6_response1_was_received = true;
			}
		}
		else if (s7OexchangeBlock.opcode == 7 && s7OexchangeBlock.response == 3)
		{
			byte[] array2 = new byte[s7OexchangeBlock.fill_length_1];
			Array.Copy(s7OexchangeBlock.user_data_1, array2, s7OexchangeBlock.fill_length_1);
			SendPduMessage(array2);
			S7OexchangeBlock newS7OexchangeBlock2 = GetNewS7OexchangeBlock(0, 2, 64, 7, 0);
			newS7OexchangeBlock2.offset_1 = 80;
			newS7OexchangeBlock2.fill_length_1 = 0;
			newS7OexchangeBlock2.seg_length_1 = 480;
			newS7OexchangeBlock2.application_block_opcode = s7OexchangeBlock.application_block_opcode;
			newS7OexchangeBlock2.application_block_subsystem = s7OexchangeBlock.application_block_subsystem;
			newS7OexchangeBlock2.user_data_1 = s7OexchangeBlock.user_data_1;
			SendS7OExchangeBlockToPlcsim(newS7OexchangeBlock2);
		}
	}

	private void SendDataToPlcsim(WndProcMessage msg)
	{
		if (++m_user >= 32000)
		{
			m_user = 1;
		}
		S7OexchangeBlock newS7OexchangeBlock = GetNewS7OexchangeBlock(m_user, 2, 64, 6, 255);
		newS7OexchangeBlock.offset_1 = 80;
		newS7OexchangeBlock.application_block_opcode = m_application_block_opcode;
		newS7OexchangeBlock.application_block_subsystem = m_application_block_subsystem;
		newS7OexchangeBlock.fill_length_1 = (ushort)msg.pdu.Length;
		newS7OexchangeBlock.seg_length_1 = (ushort)msg.pdu.Length;
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
			S7OexchangeBlock newS7OexchangeBlock = GetNewS7OexchangeBlock(1, 2, 64, 0, 255);
			newS7OexchangeBlock.offset_1 = 0;
			SendS7OExchangeBlockToPlcsim(newS7OexchangeBlock);
			m_PlcsimConnectionState = PlcsimConnectionState.ConnectState1;
			break;
		}
		case PlcsimConnectionState.ConnectState1:
		{
			S7OexchangeBlock s7OexchangeBlock = ReceiveFromPlcsimDirect();
			m_application_block_opcode = s7OexchangeBlock.application_block_opcode;
			m_application_block_subsystem = s7OexchangeBlock.application_block_subsystem;
			S7OexchangeBlock newS7OexchangeBlock = GetNewS7OexchangeBlock(1, 2, 64, 1, 255);
			newS7OexchangeBlock.offset_1 = 80;
			newS7OexchangeBlock.fill_length_1 = 126;
			newS7OexchangeBlock.seg_length_1 = 126;
			newS7OexchangeBlock.application_block_opcode = m_application_block_opcode;
			newS7OexchangeBlock.application_block_ssap = 2;
			newS7OexchangeBlock.application_block_remote_address_station = 114;
			newS7OexchangeBlock.application_block_subsystem = m_application_block_subsystem;
			UserDataConnectionConfigS7 userDataConnectionConfigS = new UserDataConnectionConfigS7(useConstructor: true);
			userDataConnectionConfigS.rack_slot = (byte)(m_PlcSlot + m_PlcRack * 32);
			userDataConnectionConfigS.connection_type = 1;
			userDataConnectionConfigS.destination_1 = m_PlcIPAddress.GetAddressBytes()[0];
			userDataConnectionConfigS.destination_2 = m_PlcIPAddress.GetAddressBytes()[1];
			userDataConnectionConfigS.destination_3 = m_PlcIPAddress.GetAddressBytes()[2];
			userDataConnectionConfigS.destination_4 = m_PlcIPAddress.GetAddressBytes()[3];
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf((object)userDataConnectionConfigS));
			Marshal.StructureToPtr((object)userDataConnectionConfigS, intPtr, fDeleteOld: false);
			newS7OexchangeBlock.user_data_1 = new byte[1024];
			Marshal.Copy(intPtr, newS7OexchangeBlock.user_data_1, 0, Marshal.SizeOf((object)userDataConnectionConfigS));
			Marshal.FreeHGlobal(intPtr);
			SendS7OExchangeBlockToPlcsim(newS7OexchangeBlock);
			m_PlcsimConnectionState = PlcsimConnectionState.ConnectState2;
			break;
		}
		case PlcsimConnectionState.ConnectState2:
			if (ReceiveFromPlcsimDirect().response != 1)
			{
				SendConnectErrorMessage("ERROR: Connect failed at ConnectState2");
				m_PlcsimConnectionState = PlcsimConnectionState.NotConnected;
				ExitThisThread();
			}
			else
			{
				SendConnectSuccessMessage("Connected");
				m_PlcsimConnectionState = PlcsimConnectionState.Connected;
			}
			break;
		}
	}
}
