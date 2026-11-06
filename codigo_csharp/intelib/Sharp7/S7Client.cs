using System;
using System.Runtime.InteropServices;

namespace Sharp7;

public class S7Client
{
	public struct S7DataItem
	{
		public int Area;

		public int WordLen;

		public int Result;

		public int DBNumber;

		public int Start;

		public int Amount;

		public IntPtr pData;
	}

	public struct S7OrderCode
	{
		public string Code;

		public byte V1;

		public byte V2;

		public byte V3;
	}

	public struct S7CpuInfo
	{
		public string ModuleTypeName;

		public string SerialNumber;

		public string ASName;

		public string Copyright;

		public string ModuleName;
	}

	public struct S7CpInfo
	{
		public int MaxPduLength;

		public int MaxConnections;

		public int MaxMpiRate;

		public int MaxBusRate;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct S7BlocksList
	{
		public int OBCount;

		public int FBCount;

		public int FCCount;

		public int SFBCount;

		public int SFCCount;

		public int DBCount;

		public int SDBCount;
	}

	public struct S7BlockInfo
	{
		public int BlkType;

		public int BlkNumber;

		public int BlkLang;

		public int BlkFlags;

		public int MC7Size;

		public int LoadSize;

		public int LocalData;

		public int SBBLength;

		public int CheckSum;

		public int Version;

		public string CodeDate;

		public string IntfDate;

		public string Author;

		public string Family;

		public string Header;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SZL_HEADER
	{
		public ushort LENTHDR;

		public ushort N_DR;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct S7SZL
	{
		public SZL_HEADER Header;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public byte[] Data;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct S7SZLList
	{
		public SZL_HEADER Header;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8190)]
		public ushort[] Data;
	}

	public struct S7Protection
	{
		public ushort sch_schal;

		public ushort sch_par;

		public ushort sch_rel;

		public ushort bart_sch;

		public ushort anl_sch;
	}

	public delegate void S7CliCompletion(IntPtr usrPtr, int opCode, int opResult);

	public const int Block_OB = 56;

	public const int Block_DB = 65;

	public const int Block_SDB = 66;

	public const int Block_FC = 67;

	public const int Block_SFC = 68;

	public const int Block_FB = 69;

	public const int Block_SFB = 70;

	public const byte SubBlk_OB = 8;

	public const byte SubBlk_DB = 10;

	public const byte SubBlk_SDB = 11;

	public const byte SubBlk_FC = 12;

	public const byte SubBlk_SFC = 13;

	public const byte SubBlk_FB = 14;

	public const byte SubBlk_SFB = 15;

	public const byte BlockLangAWL = 1;

	public const byte BlockLangKOP = 2;

	public const byte BlockLangFUP = 3;

	public const byte BlockLangSCL = 4;

	public const byte BlockLangDB = 5;

	public const byte BlockLangGRAPH = 6;

	public static readonly int MaxVars = 20;

	private const byte TS_ResBit = 3;

	private const byte TS_ResByte = 4;

	private const byte TS_ResInt = 5;

	private const byte TS_ResReal = 7;

	private const byte TS_ResOctet = 9;

	private const ushort Code7Ok = 0;

	private const ushort Code7AddressOutOfRange = 5;

	private const ushort Code7InvalidTransportSize = 6;

	private const ushort Code7WriteDataSizeMismatch = 7;

	private const ushort Code7ResItemNotAvailable = 10;

	private const ushort Code7ResItemNotAvailable1 = 53769;

	private const ushort Code7InvalidValue = 56321;

	private const ushort Code7NeedPassword = 53825;

	private const ushort Code7InvalidPassword = 54786;

	private const ushort Code7NoPasswordToClear = 54788;

	private const ushort Code7NoPasswordToSet = 54789;

	private const ushort Code7FunNotAvailable = 33028;

	private const ushort Code7DataOverPDU = 34048;

	public static readonly ushort CONNTYPE_PG = 1;

	public static readonly ushort CONNTYPE_OP = 2;

	public static readonly ushort CONNTYPE_BASIC = 3;

	public int _LastError;

	private byte[] ISO_CR = new byte[22]
	{
		3, 0, 0, 22, 17, 224, 0, 0, 0, 1,
		0, 192, 1, 10, 193, 2, 1, 0, 194, 2,
		1, 2
	};

	private byte[] TPKT_ISO = new byte[7] { 3, 0, 0, 31, 2, 240, 128 };

	private byte[] S7_PN = new byte[25]
	{
		3, 0, 0, 25, 2, 240, 128, 50, 1, 0,
		0, 4, 0, 0, 8, 0, 0, 240, 0, 0,
		1, 0, 1, 0, 30
	};

	private byte[] S7_RW = new byte[35]
	{
		3, 0, 0, 31, 2, 240, 128, 50, 1, 0,
		0, 5, 0, 0, 14, 0, 0, 4, 1, 18,
		10, 16, 2, 0, 0, 0, 0, 132, 0, 0,
		0, 0, 4, 0, 0
	};

	private static int Size_RD = 31;

	private static int Size_WR = 35;

	private byte[] S7_MRD_HEADER = new byte[19]
	{
		3, 0, 0, 31, 2, 240, 128, 50, 1, 0,
		0, 5, 0, 0, 14, 0, 0, 4, 1
	};

	private byte[] S7_MRD_ITEM = new byte[12]
	{
		18, 10, 16, 2, 0, 0, 0, 0, 132, 0,
		0, 0
	};

	private byte[] S7_MWR_HEADER = new byte[19]
	{
		3, 0, 0, 31, 2, 240, 128, 50, 1, 0,
		0, 5, 0, 0, 14, 0, 0, 5, 1
	};

	private byte[] S7_MWR_PARAM = new byte[12]
	{
		18, 10, 16, 2, 0, 0, 0, 0, 132, 0,
		0, 0
	};

	private byte[] S7_SZL_FIRST = new byte[33]
	{
		3, 0, 0, 33, 2, 240, 128, 50, 7, 0,
		0, 5, 0, 0, 8, 0, 8, 0, 1, 18,
		4, 17, 68, 1, 0, 255, 9, 0, 4, 0,
		0, 0, 0
	};

	private byte[] S7_SZL_NEXT = new byte[33]
	{
		3, 0, 0, 33, 2, 240, 128, 50, 7, 0,
		0, 6, 0, 0, 12, 0, 4, 0, 1, 18,
		8, 18, 68, 1, 1, 0, 0, 0, 0, 10,
		0, 0, 0
	};

	private byte[] S7_GET_DT = new byte[29]
	{
		3, 0, 0, 29, 2, 240, 128, 50, 7, 0,
		0, 56, 0, 0, 8, 0, 4, 0, 1, 18,
		4, 17, 71, 1, 0, 10, 0, 0, 0
	};

	private byte[] S7_SET_DT = new byte[39]
	{
		3, 0, 0, 39, 2, 240, 128, 50, 7, 0,
		0, 137, 3, 0, 8, 0, 14, 0, 1, 18,
		4, 17, 71, 2, 0, 255, 9, 0, 10, 0,
		25, 19, 18, 6, 23, 55, 19, 0, 1
	};

	private byte[] S7_SET_PWD = new byte[37]
	{
		3, 0, 0, 37, 2, 240, 128, 50, 7, 0,
		0, 39, 0, 0, 8, 0, 12, 0, 1, 18,
		4, 17, 69, 1, 0, 255, 9, 0, 8, 0,
		0, 0, 0, 0, 0, 0, 0
	};

	private byte[] S7_CLR_PWD = new byte[29]
	{
		3, 0, 0, 29, 2, 240, 128, 50, 7, 0,
		0, 41, 0, 0, 8, 0, 4, 0, 1, 18,
		4, 17, 69, 2, 0, 10, 0, 0, 0
	};

	private byte[] S7_STOP = new byte[33]
	{
		3, 0, 0, 33, 2, 240, 128, 50, 1, 0,
		0, 14, 0, 0, 16, 0, 0, 41, 0, 0,
		0, 0, 0, 9, 80, 95, 80, 82, 79, 71,
		82, 65, 77
	};

	private byte[] S7_HOT_START = new byte[37]
	{
		3, 0, 0, 37, 2, 240, 128, 50, 1, 0,
		0, 12, 0, 0, 20, 0, 0, 40, 0, 0,
		0, 0, 0, 0, 253, 0, 0, 9, 80, 95,
		80, 82, 79, 71, 82, 65, 77
	};

	private byte[] S7_COLD_START = new byte[39]
	{
		3, 0, 0, 39, 2, 240, 128, 50, 1, 0,
		0, 15, 0, 0, 22, 0, 0, 40, 0, 0,
		0, 0, 0, 0, 253, 0, 2, 67, 32, 9,
		80, 95, 80, 82, 79, 71, 82, 65, 77
	};

	private const byte pduStart = 40;

	private const byte pduStop = 41;

	private const byte pduAlreadyStarted = 2;

	private const byte pduAlreadyStopped = 7;

	private byte[] S7_GET_STAT = new byte[33]
	{
		3, 0, 0, 33, 2, 240, 128, 50, 7, 0,
		0, 44, 0, 0, 8, 0, 8, 0, 1, 18,
		4, 17, 68, 1, 0, 255, 9, 0, 4, 4,
		36, 0, 0
	};

	private byte[] S7_BI = new byte[37]
	{
		3, 0, 0, 37, 2, 240, 128, 50, 7, 0,
		0, 5, 0, 0, 8, 0, 12, 0, 1, 18,
		4, 17, 67, 3, 0, 255, 9, 0, 8, 48,
		65, 48, 48, 48, 48, 48, 65
	};

	private static int ISOTCP = 102;

	private static int MinPduSize = 16;

	private static int MinPduSizeToRequest = 240;

	private static int MaxPduSizeToRequest = 960;

	private static int DefaultTimeout = 2000;

	private static int IsoHSize = 7;

	private int _PDULength;

	private int _PduSizeRequested = 480;

	private int _PLCPort = ISOTCP;

	private int _RecvTimeout = DefaultTimeout;

	private int _SendTimeout = DefaultTimeout;

	private int _ConnTimeout = DefaultTimeout;

	private string IPAddress;

	private byte LocalTSAP_HI;

	private byte LocalTSAP_LO;

	private byte RemoteTSAP_HI;

	private byte RemoteTSAP_LO;

	private byte LastPDUType;

	private ushort ConnType = CONNTYPE_PG;

	private byte[] PDU = new byte[2048];

	private MsgSocket Socket;

	private int Time_ms;

	public int ExecutionTime => Time_ms;

	public int PduSizeNegotiated => _PDULength;

	public int PduSizeRequested
	{
		get
		{
			return _PduSizeRequested;
		}
		set
		{
			if (value < MinPduSizeToRequest)
			{
				value = MinPduSizeToRequest;
			}
			if (value > MaxPduSizeToRequest)
			{
				value = MaxPduSizeToRequest;
			}
			_PduSizeRequested = value;
		}
	}

	public int PLCPort
	{
		get
		{
			return _PLCPort;
		}
		set
		{
			_PLCPort = value;
		}
	}

	public int ConnTimeout
	{
		get
		{
			return _ConnTimeout;
		}
		set
		{
			_ConnTimeout = value;
		}
	}

	public int RecvTimeout
	{
		get
		{
			return _RecvTimeout;
		}
		set
		{
			_RecvTimeout = value;
		}
	}

	public int SendTimeout
	{
		get
		{
			return _SendTimeout;
		}
		set
		{
			_SendTimeout = value;
		}
	}

	public bool Connected
	{
		get
		{
			if (Socket != null)
			{
				return Socket.Connected;
			}
			return false;
		}
	}

	private void CreateSocket()
	{
		try
		{
			Socket = new MsgSocket();
			Socket.ConnectTimeout = _ConnTimeout;
			Socket.ReadTimeout = _RecvTimeout;
			Socket.WriteTimeout = _SendTimeout;
		}
		catch
		{
		}
	}

	private int TCPConnect()
	{
		if (_LastError == 0)
		{
			try
			{
				_LastError = Socket.Connect(IPAddress, _PLCPort);
			}
			catch
			{
				_LastError = 3;
			}
		}
		return _LastError;
	}

	private void RecvPacket(byte[] Buffer, int Start, int Size)
	{
		if (Connected)
		{
			_LastError = Socket.Receive(Buffer, Start, Size);
		}
		else
		{
			_LastError = 9;
		}
	}

	private void SendPacket(byte[] Buffer, int Len)
	{
		_LastError = Socket.Send(Buffer, Len);
	}

	private void SendPacket(byte[] Buffer)
	{
		if (Connected)
		{
			SendPacket(Buffer, Buffer.Length);
		}
		else
		{
			_LastError = 9;
		}
	}

	private int RecvIsoPacket()
	{
		bool flag = false;
		int num = 0;
		while (_LastError == 0 && !flag)
		{
			RecvPacket(PDU, 0, 4);
			if (_LastError == 0)
			{
				num = S7.GetWordAt(PDU, 2);
				if (num == IsoHSize)
				{
					RecvPacket(PDU, 4, 3);
				}
				else if (num > _PduSizeRequested + IsoHSize || num < MinPduSize)
				{
					_LastError = 196608;
				}
				else
				{
					flag = true;
				}
			}
		}
		if (_LastError == 0)
		{
			RecvPacket(PDU, 4, 3);
			LastPDUType = PDU[5];
			RecvPacket(PDU, 7, num - IsoHSize);
		}
		if (_LastError == 0)
		{
			return num;
		}
		return 0;
	}

	private int ISOConnect()
	{
		ISO_CR[16] = LocalTSAP_HI;
		ISO_CR[17] = LocalTSAP_LO;
		ISO_CR[20] = RemoteTSAP_HI;
		ISO_CR[21] = RemoteTSAP_LO;
		SendPacket(ISO_CR);
		if (_LastError == 0)
		{
			int num = RecvIsoPacket();
			if (_LastError == 0)
			{
				if (num == 22)
				{
					if (LastPDUType != 208)
					{
						_LastError = 65536;
					}
				}
				else
				{
					_LastError = 196608;
				}
			}
		}
		return _LastError;
	}

	private int NegotiatePduLength()
	{
		S7.SetWordAt(S7_PN, 23, (ushort)_PduSizeRequested);
		SendPacket(S7_PN);
		if (_LastError == 0)
		{
			int num = RecvIsoPacket();
			if (_LastError == 0)
			{
				if (num == 27 && PDU[17] == 0 && PDU[18] == 0)
				{
					_PDULength = S7.GetWordAt(PDU, 25);
					if (_PDULength <= 0)
					{
						_LastError = 1048576;
					}
				}
				else
				{
					_LastError = 1048576;
				}
			}
		}
		return _LastError;
	}

	private int CpuError(ushort Error)
	{
		switch (Error)
		{
		case 0:
			return 0;
		case 5:
			return 9437184;
		case 6:
			return 10485760;
		case 7:
			return 11534336;
		case 10:
		case 53769:
			return 12582912;
		case 34048:
			return 7340032;
		case 56321:
			return 13631488;
		case 33028:
			return 20971520;
		case 53825:
			return 30408704;
		case 54786:
			return 31457280;
		case 54788:
		case 54789:
			return 32505856;
		default:
			return 36700160;
		}
	}

	public S7Client()
	{
		CreateSocket();
	}

	~S7Client()
	{
		Disconnect();
	}

	public int Connect()
	{
		_LastError = 0;
		Time_ms = 0;
		int tickCount = Environment.TickCount;
		if (!Connected)
		{
			TCPConnect();
			if (_LastError == 0)
			{
				ISOConnect();
				if (_LastError == 0)
				{
					_LastError = NegotiatePduLength();
				}
			}
		}
		if (_LastError != 0)
		{
			Disconnect();
		}
		else
		{
			Time_ms = Environment.TickCount - tickCount;
		}
		return _LastError;
	}

	public int ConnectTo(string Address, int Rack, int Slot, int Port)
	{
		_PLCPort = Port;
		ushort remoteTSAP = (ushort)((ConnType << 8) + Rack * 32 + Slot);
		SetConnectionParams(Address, 256, remoteTSAP);
		return Connect();
	}

	public int SetConnectionParams(string Address, ushort LocalTSAP, ushort RemoteTSAP)
	{
		int num = LocalTSAP & 0xFFFF;
		int num2 = RemoteTSAP & 0xFFFF;
		IPAddress = Address;
		LocalTSAP_HI = (byte)(num >> 8);
		LocalTSAP_LO = (byte)(num & 0xFF);
		RemoteTSAP_HI = (byte)(num2 >> 8);
		RemoteTSAP_LO = (byte)(num2 & 0xFF);
		return 0;
	}

	public int SetConnectionType(ushort ConnectionType)
	{
		ConnType = ConnectionType;
		return 0;
	}

	public int Disconnect()
	{
		Socket.Close();
		return 0;
	}

	public int GetParam(int ParamNumber, ref int Value)
	{
		int result = 0;
		switch (ParamNumber)
		{
		case 2:
			Value = PLCPort;
			break;
		case 3:
			Value = ConnTimeout;
			break;
		case 4:
			Value = SendTimeout;
			break;
		case 5:
			Value = RecvTimeout;
			break;
		case 10:
			Value = PduSizeRequested;
			break;
		default:
			result = 38797312;
			break;
		}
		return result;
	}

	public int SetParam(int ParamNumber, ref int Value)
	{
		int result = 0;
		switch (ParamNumber)
		{
		case 2:
			PLCPort = Value;
			break;
		case 3:
			ConnTimeout = Value;
			break;
		case 4:
			SendTimeout = Value;
			break;
		case 5:
			RecvTimeout = Value;
			break;
		case 10:
			PduSizeRequested = Value;
			break;
		default:
			result = 38797312;
			break;
		}
		return result;
	}

	public int SetAsCallBack(S7CliCompletion Completion, IntPtr usrPtr)
	{
		return 40894464;
	}

	public int ReadArea(int Area, int DBNumber, int Start, int Amount, int WordLen, byte[] Buffer)
	{
		int BytesRead = 0;
		return ReadArea(Area, DBNumber, Start, Amount, WordLen, Buffer, ref BytesRead);
	}

	public int ReadArea(int Area, int DBNumber, int Start, int Amount, int WordLen, byte[] Buffer, ref int BytesRead)
	{
		int num = 0;
		int num2 = 1;
		_LastError = 0;
		Time_ms = 0;
		int tickCount = Environment.TickCount;
		if (Area == 28)
		{
			WordLen = 28;
		}
		if (Area == 29)
		{
			WordLen = 29;
		}
		num2 = S7.DataSizeByte(WordLen);
		if (num2 == 0)
		{
			return 5242880;
		}
		switch (WordLen)
		{
		case 1:
			Amount = 1;
			break;
		default:
			Amount *= num2;
			num2 = 1;
			WordLen = 2;
			break;
		case 28:
		case 29:
			break;
		}
		int num3 = (_PDULength - 18) / num2;
		int num4 = Amount;
		while (num4 > 0 && _LastError == 0)
		{
			int num5 = num4;
			if (num5 > num3)
			{
				num5 = num3;
			}
			int num6 = num5 * num2;
			Array.Copy(S7_RW, 0, PDU, 0, Size_RD);
			PDU[27] = (byte)Area;
			if (Area == 132)
			{
				S7.SetWordAt(PDU, 25, (ushort)DBNumber);
			}
			int num7;
			if (WordLen == 1 || WordLen == 28 || WordLen == 29)
			{
				num7 = Start;
				PDU[22] = (byte)WordLen;
			}
			else
			{
				num7 = Start << 3;
			}
			S7.SetWordAt(PDU, 23, (ushort)num5);
			PDU[30] = (byte)(num7 & 0xFF);
			num7 >>= 8;
			PDU[29] = (byte)(num7 & 0xFF);
			num7 >>= 8;
			PDU[28] = (byte)(num7 & 0xFF);
			SendPacket(PDU, Size_RD);
			if (_LastError == 0)
			{
				int num8 = RecvIsoPacket();
				if (_LastError == 0)
				{
					if (num8 < 25)
					{
						_LastError = 262144;
					}
					else if (PDU[21] != byte.MaxValue)
					{
						_LastError = CpuError(PDU[21]);
					}
					else
					{
						Array.Copy(PDU, 25, Buffer, num, num6);
						num += num6;
					}
				}
			}
			num4 -= num5;
			Start += num5 * num2;
		}
		if (_LastError == 0)
		{
			BytesRead = num;
			Time_ms = Environment.TickCount - tickCount;
		}
		else
		{
			BytesRead = 0;
		}
		return _LastError;
	}

	public int WriteArea(int Area, int DBNumber, int Start, int Amount, int WordLen, byte[] Buffer)
	{
		int BytesWritten = 0;
		return WriteArea(Area, DBNumber, Start, Amount, WordLen, Buffer, ref BytesWritten);
	}

	public int WriteArea(int Area, int DBNumber, int Start, int Amount, int WordLen, byte[] Buffer, ref int BytesWritten)
	{
		int num = 0;
		int num2 = 1;
		_LastError = 0;
		Time_ms = 0;
		int tickCount = Environment.TickCount;
		if (Area == 28)
		{
			WordLen = 28;
		}
		if (Area == 29)
		{
			WordLen = 29;
		}
		num2 = S7.DataSizeByte(WordLen);
		if (num2 == 0)
		{
			return 5242880;
		}
		switch (WordLen)
		{
		case 1:
			Amount = 1;
			break;
		default:
			Amount *= num2;
			num2 = 1;
			WordLen = 2;
			break;
		case 28:
		case 29:
			break;
		}
		int num3 = (_PDULength - 35) / num2;
		int num4 = Amount;
		while (num4 > 0 && _LastError == 0)
		{
			int num5 = num4;
			if (num5 > num3)
			{
				num5 = num3;
			}
			int num6 = num5 * num2;
			int num7 = Size_WR + num6;
			Array.Copy(S7_RW, 0, PDU, 0, Size_WR);
			S7.SetWordAt(PDU, 2, (ushort)num7);
			int num8 = num6 + 4;
			S7.SetWordAt(PDU, 15, (ushort)num8);
			PDU[17] = 5;
			PDU[27] = (byte)Area;
			if (Area == 132)
			{
				S7.SetWordAt(PDU, 25, (ushort)DBNumber);
			}
			int num9;
			if (WordLen == 1 || WordLen == 28 || WordLen == 29)
			{
				num9 = Start;
				num8 = num6;
				PDU[22] = (byte)WordLen;
			}
			else
			{
				num9 = Start << 3;
				num8 = num6 << 3;
			}
			S7.SetWordAt(PDU, 23, (ushort)num5);
			PDU[30] = (byte)(num9 & 0xFF);
			num9 >>= 8;
			PDU[29] = (byte)(num9 & 0xFF);
			num9 >>= 8;
			PDU[28] = (byte)(num9 & 0xFF);
			switch (WordLen)
			{
			case 1:
				PDU[32] = 3;
				break;
			case 28:
			case 29:
				PDU[32] = 9;
				break;
			default:
				PDU[32] = 4;
				break;
			}
			S7.SetWordAt(PDU, 33, (ushort)num8);
			Array.Copy(Buffer, num, PDU, 35, num6);
			SendPacket(PDU, num7);
			if (_LastError == 0)
			{
				num8 = RecvIsoPacket();
				if (_LastError == 0)
				{
					if (num8 == 22)
					{
						if (PDU[21] != byte.MaxValue)
						{
							_LastError = CpuError(PDU[21]);
						}
					}
					else
					{
						_LastError = 196608;
					}
				}
			}
			num += num6;
			num4 -= num5;
			Start += num5 * num2;
		}
		if (_LastError == 0)
		{
			BytesWritten = num;
			Time_ms = Environment.TickCount - tickCount;
		}
		else
		{
			BytesWritten = 0;
		}
		return _LastError;
	}

	public int ReadMultiVars(S7DataItem[] Items, int ItemsCount)
	{
		byte[] array = new byte[12];
		byte[] array2 = new byte[1024];
		_LastError = 0;
		Time_ms = 0;
		int tickCount = Environment.TickCount;
		if (ItemsCount > MaxVars)
		{
			return 4194304;
		}
		Array.Copy(S7_MRD_HEADER, 0, PDU, 0, S7_MRD_HEADER.Length);
		S7.SetWordAt(PDU, 13, (ushort)(ItemsCount * array.Length + 2));
		PDU[18] = (byte)ItemsCount;
		int num = 19;
		for (int i = 0; i < ItemsCount; i++)
		{
			Array.Copy(S7_MRD_ITEM, array, array.Length);
			array[3] = (byte)Items[i].WordLen;
			S7.SetWordAt(array, 4, (ushort)Items[i].Amount);
			if (Items[i].Area == 132)
			{
				S7.SetWordAt(array, 6, (ushort)Items[i].DBNumber);
			}
			array[8] = (byte)Items[i].Area;
			int start = Items[i].Start;
			array[11] = (byte)(start & 0xFF);
			start >>= 8;
			array[10] = (byte)(start & 0xFF);
			start >>= 8;
			array[9] = (byte)(start & 0xFF);
			Array.Copy(array, 0, PDU, num, array.Length);
			num += array.Length;
		}
		if (num > _PDULength)
		{
			return 7340032;
		}
		S7.SetWordAt(PDU, 2, (ushort)num);
		SendPacket(PDU, num);
		if (_LastError != 0)
		{
			return _LastError;
		}
		int num2 = RecvIsoPacket();
		if (_LastError != 0)
		{
			return _LastError;
		}
		if (num2 < 22)
		{
			_LastError = 196608;
			return _LastError;
		}
		_LastError = CpuError(S7.GetWordAt(PDU, 17));
		if (_LastError != 0)
		{
			return _LastError;
		}
		int byteAt = S7.GetByteAt(PDU, 20);
		if (byteAt != ItemsCount || byteAt > MaxVars)
		{
			_LastError = 8388608;
			return _LastError;
		}
		num = 21;
		for (int j = 0; j < ItemsCount; j++)
		{
			Array.Copy(PDU, num, array2, 0, num2 - num);
			if (array2[0] == byte.MaxValue)
			{
				int num3 = S7.GetWordAt(array2, 2);
				if (array2[1] != 9 && array2[1] != 7 && array2[1] != 3)
				{
					num3 >>= 3;
				}
				Marshal.Copy(array2, 4, Items[j].pData, num3);
				Items[j].Result = 0;
				if (num3 % 2 != 0)
				{
					num3++;
				}
				num = num + 4 + num3;
			}
			else
			{
				Items[j].Result = CpuError(array2[0]);
				num += 4;
			}
		}
		Time_ms = Environment.TickCount - tickCount;
		return _LastError;
	}

	public int WriteMultiVars(S7DataItem[] Items, int ItemsCount)
	{
		byte[] array = new byte[S7_MWR_PARAM.Length];
		byte[] array2 = new byte[1024];
		_LastError = 0;
		Time_ms = 0;
		int tickCount = Environment.TickCount;
		if (ItemsCount > MaxVars)
		{
			return 4194304;
		}
		Array.Copy(S7_MWR_HEADER, 0, PDU, 0, S7_MWR_HEADER.Length);
		int num = ItemsCount * S7_MWR_PARAM.Length + 2;
		S7.SetWordAt(PDU, 13, (ushort)num);
		PDU[18] = (byte)ItemsCount;
		int num2 = S7_MWR_HEADER.Length;
		for (int i = 0; i < ItemsCount; i++)
		{
			Array.Copy(S7_MWR_PARAM, 0, array, 0, S7_MWR_PARAM.Length);
			array[3] = (byte)Items[i].WordLen;
			array[8] = (byte)Items[i].Area;
			S7.SetWordAt(array, 4, (ushort)Items[i].Amount);
			S7.SetWordAt(array, 6, (ushort)Items[i].DBNumber);
			int start = Items[i].Start;
			array[11] = (byte)(start & 0xFF);
			start >>= 8;
			array[10] = (byte)(start & 0xFF);
			start >>= 8;
			array[9] = (byte)(start & 0xFF);
			Array.Copy(array, 0, PDU, num2, array.Length);
			num2 += S7_MWR_PARAM.Length;
		}
		int num3 = 0;
		for (int j = 0; j < ItemsCount; j++)
		{
			array2[0] = 0;
			switch (Items[j].WordLen)
			{
			case 1:
				array2[1] = 3;
				break;
			case 28:
			case 29:
				array2[1] = 9;
				break;
			default:
				array2[1] = 4;
				break;
			}
			int num4 = ((Items[j].WordLen != 29 && Items[j].WordLen != 28) ? Items[j].Amount : (Items[j].Amount * 2));
			if (array2[1] != 9 && array2[1] != 3)
			{
				S7.SetWordAt(array2, 2, (ushort)(num4 * 8));
			}
			else
			{
				S7.SetWordAt(array2, 2, (ushort)num4);
			}
			Marshal.Copy(Items[j].pData, array2, 4, num4);
			if (num4 % 2 != 0)
			{
				array2[num4 + 4] = 0;
				num4++;
			}
			Array.Copy(array2, 0, PDU, num2, num4 + 4);
			num2 = num2 + num4 + 4;
			num3 = num3 + num4 + 4;
		}
		if (num2 > _PDULength)
		{
			return 7340032;
		}
		S7.SetWordAt(PDU, 2, (ushort)num2);
		S7.SetWordAt(PDU, 15, (ushort)num3);
		SendPacket(PDU, num2);
		RecvIsoPacket();
		if (_LastError == 0)
		{
			_LastError = CpuError(S7.GetWordAt(PDU, 17));
			if (_LastError != 0)
			{
				return _LastError;
			}
			int byteAt = S7.GetByteAt(PDU, 20);
			if (byteAt != ItemsCount || byteAt > MaxVars)
			{
				_LastError = 8388608;
				return _LastError;
			}
			for (int k = 0; k < ItemsCount; k++)
			{
				if (PDU[k + 21] == byte.MaxValue)
				{
					Items[k].Result = 0;
				}
				else
				{
					Items[k].Result = CpuError(PDU[k + 21]);
				}
			}
			Time_ms = Environment.TickCount - tickCount;
		}
		return _LastError;
	}

	public int DBRead(int DBNumber, int Start, int Size, byte[] Buffer)
	{
		return ReadArea(132, DBNumber, Start, Size, 2, Buffer);
	}

	public int DBWrite(int DBNumber, int Start, int Size, byte[] Buffer)
	{
		return WriteArea(132, DBNumber, Start, Size, 2, Buffer);
	}

	public int MBRead(int Start, int Size, byte[] Buffer)
	{
		return ReadArea(131, 0, Start, Size, 2, Buffer);
	}

	public int MBWrite(int Start, int Size, byte[] Buffer)
	{
		return WriteArea(131, 0, Start, Size, 2, Buffer);
	}

	public int EBRead(int Start, int Size, byte[] Buffer)
	{
		return ReadArea(129, 0, Start, Size, 2, Buffer);
	}

	public int EBWrite(int Start, int Size, byte[] Buffer)
	{
		return WriteArea(129, 0, Start, Size, 2, Buffer);
	}

	public int ABRead(int Start, int Size, byte[] Buffer)
	{
		return ReadArea(130, 0, Start, Size, 2, Buffer);
	}

	public int ABWrite(int Start, int Size, byte[] Buffer)
	{
		return WriteArea(130, 0, Start, Size, 2, Buffer);
	}

	public int TMRead(int Start, int Amount, ushort[] Buffer)
	{
		byte[] array = new byte[Amount * 2];
		int num = ReadArea(29, 0, Start, Amount, 29, array);
		if (num == 0)
		{
			for (int i = 0; i < Amount; i++)
			{
				Buffer[i] = (ushort)((array[i * 2 + 1] << 8) + array[i * 2]);
			}
		}
		return num;
	}

	public int TMWrite(int Start, int Amount, ushort[] Buffer)
	{
		byte[] array = new byte[Amount * 2];
		for (int i = 0; i < Amount; i++)
		{
			array[i * 2 + 1] = (byte)((Buffer[i] & 0xFF00) >> 8);
			array[i * 2] = (byte)(Buffer[i] & 0xFF);
		}
		return WriteArea(29, 0, Start, Amount, 29, array);
	}

	public int CTRead(int Start, int Amount, ushort[] Buffer)
	{
		byte[] array = new byte[Amount * 2];
		int num = ReadArea(28, 0, Start, Amount, 28, array);
		if (num == 0)
		{
			for (int i = 0; i < Amount; i++)
			{
				Buffer[i] = (ushort)((array[i * 2 + 1] << 8) + array[i * 2]);
			}
		}
		return num;
	}

	public int CTWrite(int Start, int Amount, ushort[] Buffer)
	{
		byte[] array = new byte[Amount * 2];
		for (int i = 0; i < Amount; i++)
		{
			array[i * 2 + 1] = (byte)((Buffer[i] & 0xFF00) >> 8);
			array[i * 2] = (byte)(Buffer[i] & 0xFF);
		}
		return WriteArea(28, 0, Start, Amount, 28, array);
	}

	public int ListBlocks(ref S7BlocksList List)
	{
		return 40894464;
	}

	private string SiemensTimestamp(long EncodedDate)
	{
		return new DateTime(1984, 1, 1).AddSeconds(EncodedDate * 86400).ToShortDateString();
	}

	public int GetAgBlockInfo(int BlockType, int BlockNum, ref S7BlockInfo Info)
	{
		_LastError = 0;
		Time_ms = 0;
		int tickCount = Environment.TickCount;
		S7_BI[30] = (byte)BlockType;
		S7_BI[31] = (byte)(BlockNum / 10000 + 48);
		BlockNum %= 10000;
		S7_BI[32] = (byte)(BlockNum / 1000 + 48);
		BlockNum %= 1000;
		S7_BI[33] = (byte)(BlockNum / 100 + 48);
		BlockNum %= 100;
		S7_BI[34] = (byte)(BlockNum / 10 + 48);
		BlockNum %= 10;
		S7_BI[35] = (byte)(BlockNum / 1 + 48);
		SendPacket(S7_BI);
		if (_LastError == 0)
		{
			if (RecvIsoPacket() > 32)
			{
				ushort wordAt = S7.GetWordAt(PDU, 27);
				if (wordAt == 0)
				{
					Info.BlkFlags = PDU[42];
					Info.BlkLang = PDU[43];
					Info.BlkType = PDU[44];
					Info.BlkNumber = S7.GetWordAt(PDU, 45);
					Info.LoadSize = S7.GetDIntAt(PDU, 47);
					Info.CodeDate = SiemensTimestamp(S7.GetWordAt(PDU, 59));
					Info.IntfDate = SiemensTimestamp(S7.GetWordAt(PDU, 65));
					Info.SBBLength = S7.GetWordAt(PDU, 67);
					Info.LocalData = S7.GetWordAt(PDU, 71);
					Info.MC7Size = S7.GetWordAt(PDU, 73);
					Info.Author = S7.GetCharsAt(PDU, 75, 8).Trim(default(char));
					Info.Family = S7.GetCharsAt(PDU, 83, 8).Trim(default(char));
					Info.Header = S7.GetCharsAt(PDU, 91, 8).Trim(default(char));
					Info.Version = PDU[99];
					Info.CheckSum = S7.GetWordAt(PDU, 101);
				}
				else
				{
					_LastError = CpuError(wordAt);
				}
			}
			else
			{
				_LastError = 196608;
			}
		}
		if (_LastError == 0)
		{
			Time_ms = Environment.TickCount - tickCount;
		}
		return _LastError;
	}

	public int GetPgBlockInfo(ref S7BlockInfo Info, byte[] Buffer, int Size)
	{
		return 40894464;
	}

	public int ListBlocksOfType(int BlockType, ushort[] List, ref int ItemsCount)
	{
		return 40894464;
	}

	public int Upload(int BlockType, int BlockNum, byte[] UsrData, ref int Size)
	{
		return 40894464;
	}

	public int FullUpload(int BlockType, int BlockNum, byte[] UsrData, ref int Size)
	{
		return 40894464;
	}

	public int Download(int BlockNum, byte[] UsrData, int Size)
	{
		return 40894464;
	}

	public int Delete(int BlockType, int BlockNum)
	{
		return 40894464;
	}

	public int DBGet(int DBNumber, byte[] UsrData, ref int Size)
	{
		S7BlockInfo Info = default(S7BlockInfo);
		int tickCount = Environment.TickCount;
		Time_ms = 0;
		_LastError = GetAgBlockInfo(65, DBNumber, ref Info);
		if (_LastError == 0)
		{
			int mC7Size = Info.MC7Size;
			if (mC7Size <= UsrData.Length)
			{
				Size = mC7Size;
				_LastError = DBRead(DBNumber, 0, mC7Size, UsrData);
				if (_LastError == 0)
				{
					Size = mC7Size;
				}
			}
			else
			{
				_LastError = 35651584;
			}
		}
		if (_LastError == 0)
		{
			Time_ms = Environment.TickCount - tickCount;
		}
		return _LastError;
	}

	public int DBFill(int DBNumber, int FillChar)
	{
		S7BlockInfo Info = default(S7BlockInfo);
		int tickCount = Environment.TickCount;
		Time_ms = 0;
		_LastError = GetAgBlockInfo(65, DBNumber, ref Info);
		if (_LastError == 0)
		{
			byte[] array = new byte[Info.MC7Size];
			for (int i = 0; i < Info.MC7Size; i++)
			{
				array[i] = (byte)FillChar;
			}
			_LastError = DBWrite(DBNumber, 0, Info.MC7Size, array);
		}
		if (_LastError == 0)
		{
			Time_ms = Environment.TickCount - tickCount;
		}
		return _LastError;
	}

	public int GetPlcDateTime(ref DateTime DT)
	{
		_LastError = 0;
		Time_ms = 0;
		int tickCount = Environment.TickCount;
		SendPacket(S7_GET_DT);
		if (_LastError == 0)
		{
			if (RecvIsoPacket() > 30)
			{
				if (S7.GetWordAt(PDU, 27) == 0 && PDU[29] == byte.MaxValue)
				{
					DT = S7.GetDateTimeAt(PDU, 35);
				}
				else
				{
					_LastError = 8388608;
				}
			}
			else
			{
				_LastError = 196608;
			}
		}
		if (_LastError == 0)
		{
			Time_ms = Environment.TickCount - tickCount;
		}
		return _LastError;
	}

	public int SetPlcDateTime(DateTime DT)
	{
		_LastError = 0;
		Time_ms = 0;
		int tickCount = Environment.TickCount;
		S7.SetDateTimeAt(S7_SET_DT, 31, DT);
		SendPacket(S7_SET_DT);
		if (_LastError == 0)
		{
			if (RecvIsoPacket() > 30)
			{
				if (S7.GetWordAt(PDU, 27) != 0)
				{
					_LastError = 8388608;
				}
			}
			else
			{
				_LastError = 196608;
			}
		}
		if (_LastError == 0)
		{
			Time_ms = Environment.TickCount - tickCount;
		}
		return _LastError;
	}

	public int SetPlcSystemDateTime()
	{
		return SetPlcDateTime(DateTime.Now);
	}

	public int GetOrderCode(ref S7OrderCode Info)
	{
		S7SZL SZL = default(S7SZL);
		int Size = 1024;
		SZL.Data = new byte[Size];
		int tickCount = Environment.TickCount;
		_LastError = ReadSZL(17, 0, ref SZL, ref Size);
		if (_LastError == 0)
		{
			Info.Code = S7.GetCharsAt(SZL.Data, 2, 20);
			Info.V1 = SZL.Data[Size - 3];
			Info.V2 = SZL.Data[Size - 2];
			Info.V3 = SZL.Data[Size - 1];
		}
		if (_LastError == 0)
		{
			Time_ms = Environment.TickCount - tickCount;
		}
		return _LastError;
	}

	public int GetCpuInfo(ref S7CpuInfo Info)
	{
		S7SZL SZL = default(S7SZL);
		int Size = 1024;
		SZL.Data = new byte[Size];
		int tickCount = Environment.TickCount;
		_LastError = ReadSZL(28, 0, ref SZL, ref Size);
		if (_LastError == 0)
		{
			Info.ModuleTypeName = S7.GetCharsAt(SZL.Data, 172, 32);
			Info.SerialNumber = S7.GetCharsAt(SZL.Data, 138, 24);
			Info.ASName = S7.GetCharsAt(SZL.Data, 2, 24);
			Info.Copyright = S7.GetCharsAt(SZL.Data, 104, 26);
			Info.ModuleName = S7.GetCharsAt(SZL.Data, 36, 24);
		}
		if (_LastError == 0)
		{
			Time_ms = Environment.TickCount - tickCount;
		}
		return _LastError;
	}

	public int GetCpInfo(ref S7CpInfo Info)
	{
		S7SZL SZL = default(S7SZL);
		int Size = 1024;
		SZL.Data = new byte[Size];
		int tickCount = Environment.TickCount;
		_LastError = ReadSZL(305, 1, ref SZL, ref Size);
		if (_LastError == 0)
		{
			Info.MaxPduLength = S7.GetIntAt(PDU, 2);
			Info.MaxConnections = S7.GetIntAt(PDU, 4);
			Info.MaxMpiRate = S7.GetDIntAt(PDU, 6);
			Info.MaxBusRate = S7.GetDIntAt(PDU, 10);
		}
		if (_LastError == 0)
		{
			Time_ms = Environment.TickCount - tickCount;
		}
		return _LastError;
	}

	public int ReadSZL(int ID, int Index, ref S7SZL SZL, ref int Size)
	{
		int num = 0;
		bool flag = false;
		bool flag2 = true;
		byte b = 0;
		ushort num2 = 0;
		_LastError = 0;
		Time_ms = 0;
		int tickCount = Environment.TickCount;
		SZL.Header.LENTHDR = 0;
		do
		{
			if (flag2)
			{
				S7.SetWordAt(S7_SZL_FIRST, 11, ++num2);
				S7.SetWordAt(S7_SZL_FIRST, 29, (ushort)ID);
				S7.SetWordAt(S7_SZL_FIRST, 31, (ushort)Index);
				SendPacket(S7_SZL_FIRST);
			}
			else
			{
				S7.SetWordAt(S7_SZL_NEXT, 11, ++num2);
				PDU[24] = b;
				SendPacket(S7_SZL_NEXT);
			}
			if (_LastError != 0)
			{
				return _LastError;
			}
			int num3 = RecvIsoPacket();
			if (_LastError == 0)
			{
				if (flag2)
				{
					if (num3 > 32)
					{
						if (S7.GetWordAt(PDU, 27) == 0 && PDU[29] == byte.MaxValue)
						{
							int num4 = S7.GetWordAt(PDU, 31) - 8;
							flag = PDU[26] == 0;
							b = PDU[24];
							SZL.Header.LENTHDR = S7.GetWordAt(PDU, 37);
							SZL.Header.N_DR = S7.GetWordAt(PDU, 39);
							Array.Copy(PDU, 41, SZL.Data, num, num4);
							num += num4;
							SZL.Header.LENTHDR += SZL.Header.LENTHDR;
						}
						else
						{
							_LastError = 8388608;
						}
					}
					else
					{
						_LastError = 196608;
					}
				}
				else if (num3 > 32)
				{
					if (S7.GetWordAt(PDU, 27) == 0 && PDU[29] == byte.MaxValue)
					{
						int num4 = S7.GetWordAt(PDU, 31);
						flag = PDU[26] == 0;
						b = PDU[24];
						Array.Copy(PDU, 37, SZL.Data, num, num4);
						num += num4;
						SZL.Header.LENTHDR += SZL.Header.LENTHDR;
					}
					else
					{
						_LastError = 8388608;
					}
				}
				else
				{
					_LastError = 196608;
				}
			}
			flag2 = false;
		}
		while (!flag && _LastError == 0);
		if (_LastError == 0)
		{
			Size = SZL.Header.LENTHDR;
			Time_ms = Environment.TickCount - tickCount;
		}
		return _LastError;
	}

	public int ReadSZLList(ref S7SZLList List, ref int ItemsCount)
	{
		return 40894464;
	}

	public int PlcHotStart()
	{
		_LastError = 0;
		int tickCount = Environment.TickCount;
		SendPacket(S7_HOT_START);
		if (_LastError == 0)
		{
			if (RecvIsoPacket() > 18)
			{
				if (PDU[19] != 40)
				{
					_LastError = 14680064;
				}
				else if (PDU[20] == 2)
				{
					_LastError = 15728640;
				}
				else
				{
					_LastError = 14680064;
				}
			}
			else
			{
				_LastError = 196608;
			}
		}
		if (_LastError == 0)
		{
			Time_ms = Environment.TickCount - tickCount;
		}
		return _LastError;
	}

	public int PlcColdStart()
	{
		_LastError = 0;
		int tickCount = Environment.TickCount;
		SendPacket(S7_COLD_START);
		if (_LastError == 0)
		{
			if (RecvIsoPacket() > 18)
			{
				if (PDU[19] != 40)
				{
					_LastError = 14680064;
				}
				else if (PDU[20] == 2)
				{
					_LastError = 15728640;
				}
				else
				{
					_LastError = 14680064;
				}
			}
			else
			{
				_LastError = 196608;
			}
		}
		if (_LastError == 0)
		{
			Time_ms = Environment.TickCount - tickCount;
		}
		return _LastError;
	}

	public int PlcStop()
	{
		_LastError = 0;
		int tickCount = Environment.TickCount;
		SendPacket(S7_STOP);
		if (_LastError == 0)
		{
			if (RecvIsoPacket() > 18)
			{
				if (PDU[19] != 41)
				{
					_LastError = 16777216;
				}
				else if (PDU[20] == 7)
				{
					_LastError = 19922944;
				}
				else
				{
					_LastError = 16777216;
				}
			}
			else
			{
				_LastError = 196608;
			}
		}
		if (_LastError == 0)
		{
			Time_ms = Environment.TickCount - tickCount;
		}
		return _LastError;
	}

	public int PlcCopyRamToRom(uint Timeout)
	{
		return 40894464;
	}

	public int PlcCompress(uint Timeout)
	{
		return 40894464;
	}

	public int PlcGetStatus(ref int Status)
	{
		_LastError = 0;
		int tickCount = Environment.TickCount;
		SendPacket(S7_GET_STAT);
		if (_LastError == 0)
		{
			if (RecvIsoPacket() > 30)
			{
				ushort wordAt = S7.GetWordAt(PDU, 27);
				if (wordAt == 0)
				{
					byte b = PDU[44];
					if (b == 0 || b == 4 || b == 8)
					{
						Status = PDU[44];
					}
					else
					{
						Status = 4;
					}
				}
				else
				{
					_LastError = CpuError(wordAt);
				}
			}
			else
			{
				_LastError = 196608;
			}
		}
		if (_LastError == 0)
		{
			Time_ms = Environment.TickCount - tickCount;
		}
		return _LastError;
	}

	public int SetSessionPassword(string Password)
	{
		byte[] array = new byte[8] { 32, 32, 32, 32, 32, 32, 32, 32 };
		_LastError = 0;
		int tickCount = Environment.TickCount;
		S7.SetCharsAt(array, 0, Password);
		array[0] = (byte)(array[0] ^ 0x55);
		array[1] = (byte)(array[1] ^ 0x55);
		for (int i = 2; i < 8; i++)
		{
			array[i] = (byte)(array[i] ^ 0x55 ^ array[i - 2]);
		}
		Array.Copy(array, 0, S7_SET_PWD, 29, 8);
		SendPacket(S7_SET_PWD);
		if (_LastError == 0)
		{
			if (RecvIsoPacket() > 32)
			{
				ushort wordAt = S7.GetWordAt(PDU, 27);
				if (wordAt != 0)
				{
					_LastError = CpuError(wordAt);
				}
			}
			else
			{
				_LastError = 196608;
			}
		}
		if (_LastError == 0)
		{
			Time_ms = Environment.TickCount - tickCount;
		}
		return _LastError;
	}

	public int ClearSessionPassword()
	{
		_LastError = 0;
		_ = Environment.TickCount;
		SendPacket(S7_CLR_PWD);
		if (_LastError == 0)
		{
			if (RecvIsoPacket() > 30)
			{
				ushort wordAt = S7.GetWordAt(PDU, 27);
				if (wordAt != 0)
				{
					_LastError = CpuError(wordAt);
				}
			}
			else
			{
				_LastError = 196608;
			}
		}
		return _LastError;
	}

	public int GetProtection(ref S7Protection Protection)
	{
		S7SZL SZL = default(S7SZL);
		int Size = 256;
		SZL.Data = new byte[Size];
		_LastError = ReadSZL(562, 4, ref SZL, ref Size);
		if (_LastError == 0)
		{
			Protection.sch_schal = S7.GetWordAt(SZL.Data, 2);
			Protection.sch_par = S7.GetWordAt(SZL.Data, 4);
			Protection.sch_rel = S7.GetWordAt(SZL.Data, 6);
			Protection.bart_sch = S7.GetWordAt(SZL.Data, 8);
			Protection.anl_sch = S7.GetWordAt(SZL.Data, 10);
		}
		return _LastError;
	}

	public int IsoExchangeBuffer(byte[] Buffer, ref int Size)
	{
		_LastError = 0;
		Time_ms = 0;
		int tickCount = Environment.TickCount;
		Array.Copy(TPKT_ISO, 0, PDU, 0, TPKT_ISO.Length);
		S7.SetWordAt(PDU, 2, (ushort)(Size + TPKT_ISO.Length));
		try
		{
			Array.Copy(Buffer, 0, PDU, TPKT_ISO.Length, Size);
		}
		catch
		{
			return 196608;
		}
		SendPacket(PDU, TPKT_ISO.Length + Size);
		if (_LastError == 0)
		{
			int num = RecvIsoPacket();
			if (_LastError == 0)
			{
				Array.Copy(PDU, TPKT_ISO.Length, Buffer, 0, num - TPKT_ISO.Length);
				Size = num - TPKT_ISO.Length;
			}
		}
		if (_LastError == 0)
		{
			Time_ms = Environment.TickCount - tickCount;
		}
		else
		{
			Size = 0;
		}
		return _LastError;
	}

	public int AsReadArea(int Area, int DBNumber, int Start, int Amount, int WordLen, byte[] Buffer)
	{
		return 40894464;
	}

	public int AsWriteArea(int Area, int DBNumber, int Start, int Amount, int WordLen, byte[] Buffer)
	{
		return 40894464;
	}

	public int AsDBRead(int DBNumber, int Start, int Size, byte[] Buffer)
	{
		return 40894464;
	}

	public int AsDBWrite(int DBNumber, int Start, int Size, byte[] Buffer)
	{
		return 40894464;
	}

	public int AsMBRead(int Start, int Size, byte[] Buffer)
	{
		return 40894464;
	}

	public int AsMBWrite(int Start, int Size, byte[] Buffer)
	{
		return 40894464;
	}

	public int AsEBRead(int Start, int Size, byte[] Buffer)
	{
		return 40894464;
	}

	public int AsEBWrite(int Start, int Size, byte[] Buffer)
	{
		return 40894464;
	}

	public int AsABRead(int Start, int Size, byte[] Buffer)
	{
		return 40894464;
	}

	public int AsABWrite(int Start, int Size, byte[] Buffer)
	{
		return 40894464;
	}

	public int AsTMRead(int Start, int Amount, ushort[] Buffer)
	{
		return 40894464;
	}

	public int AsTMWrite(int Start, int Amount, ushort[] Buffer)
	{
		return 40894464;
	}

	public int AsCTRead(int Start, int Amount, ushort[] Buffer)
	{
		return 40894464;
	}

	public int AsCTWrite(int Start, int Amount, ushort[] Buffer)
	{
		return 40894464;
	}

	public int AsListBlocksOfType(int BlockType, ushort[] List)
	{
		return 40894464;
	}

	public int AsReadSZL(int ID, int Index, ref S7SZL Data, ref int Size)
	{
		return 40894464;
	}

	public int AsReadSZLList(ref S7SZLList List, ref int ItemsCount)
	{
		return 40894464;
	}

	public int AsUpload(int BlockType, int BlockNum, byte[] UsrData, ref int Size)
	{
		return 40894464;
	}

	public int AsFullUpload(int BlockType, int BlockNum, byte[] UsrData, ref int Size)
	{
		return 40894464;
	}

	public int ASDownload(int BlockNum, byte[] UsrData, int Size)
	{
		return 40894464;
	}

	public int AsPlcCopyRamToRom(uint Timeout)
	{
		return 40894464;
	}

	public int AsPlcCompress(uint Timeout)
	{
		return 40894464;
	}

	public int AsDBGet(int DBNumber, byte[] UsrData, ref int Size)
	{
		return 40894464;
	}

	public int AsDBFill(int DBNumber, int FillChar)
	{
		return 40894464;
	}

	public bool CheckAsCompletion(ref int opResult)
	{
		opResult = 0;
		return false;
	}

	public int WaitAsCompletion(int Timeout)
	{
		return 40894464;
	}

	public string ErrorText(int Error)
	{
		return Error switch
		{
			0 => "OK", 
			1 => "SYS : Error creating the Socket", 
			2 => "TCP : Connection Timeout", 
			3 => "TCP : Connection Error", 
			4 => "TCP : Data receive Timeout", 
			5 => "TCP : Error receiving Data", 
			6 => "TCP : Data send Timeout", 
			7 => "TCP : Error sending Data", 
			8 => "TCP : Connection reset by the Peer", 
			9 => "CLI : Client not connected", 
			10065 => "TCP : Unreachable host", 
			65536 => "ISO : Connection Error", 
			196608 => "ISO : Invalid PDU received", 
			262144 => "ISO : Invalid Buffer passed to Send/Receive", 
			1048576 => "CLI : Error in PDU negotiation", 
			2097152 => "CLI : invalid param(s) supplied", 
			3145728 => "CLI : Job pending", 
			4194304 => "CLI : too may items (>20) in multi read/write", 
			5242880 => "CLI : invalid WordLength", 
			6291456 => "CLI : Partial data written", 
			7340032 => "CPU : total data exceeds the PDU size", 
			8388608 => "CLI : invalid CPU answer", 
			9437184 => "CPU : Address out of range", 
			10485760 => "CPU : Invalid Transport size", 
			11534336 => "CPU : Data size mismatch", 
			12582912 => "CPU : Item not available", 
			13631488 => "CPU : Invalid value supplied", 
			14680064 => "CPU : Cannot start PLC", 
			15728640 => "CPU : PLC already RUN", 
			16777216 => "CPU : Cannot stop PLC", 
			17825792 => "CPU : Cannot copy RAM to ROM", 
			18874368 => "CPU : Cannot compress", 
			19922944 => "CPU : PLC already STOP", 
			20971520 => "CPU : Function not available", 
			22020096 => "CPU : Upload sequence failed", 
			23068672 => "CLI : Invalid data size received", 
			24117248 => "CLI : Invalid block type", 
			25165824 => "CLI : Invalid block number", 
			26214400 => "CLI : Invalid block size", 
			30408704 => "CPU : Function not authorized for current protection level", 
			31457280 => "CPU : Invalid password", 
			32505856 => "CPU : No password to set or clear", 
			33554432 => "CLI : Job Timeout", 
			36700160 => "CLI : function refused by CPU (Unknown error)", 
			34603008 => "CLI : Partial data read", 
			35651584 => "CLI : The buffer supplied is too small to accomplish the operation", 
			37748736 => "CLI : Cannot perform (destroying)", 
			38797312 => "CLI : Invalid Param Number", 
			39845888 => "CLI : Cannot change this param now", 
			40894464 => "CLI : Function not implemented", 
			_ => "CLI : Unknown error (0x" + Convert.ToString(Error, 16) + ")", 
		};
	}

	public int LastError()
	{
		return _LastError;
	}

	public int RequestedPduLength()
	{
		return _PduSizeRequested;
	}

	public int NegotiatedPduLength()
	{
		return _PDULength;
	}

	public int ExecTime()
	{
		return Time_ms;
	}
}
