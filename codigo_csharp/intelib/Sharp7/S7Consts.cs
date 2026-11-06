using System.Runtime.InteropServices;

namespace Sharp7;

public static class S7Consts
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct S7Tag
	{
		public int Area;

		public int DBNumber;

		public int Start;

		public int Elements;

		public int WordLen;
	}

	public const int errTCPSocketCreation = 1;

	public const int errTCPConnectionTimeout = 2;

	public const int errTCPConnectionFailed = 3;

	public const int errTCPReceiveTimeout = 4;

	public const int errTCPDataReceive = 5;

	public const int errTCPSendTimeout = 6;

	public const int errTCPDataSend = 7;

	public const int errTCPConnectionReset = 8;

	public const int errTCPNotConnected = 9;

	public const int errTCPUnreachableHost = 10065;

	public const int errIsoConnect = 65536;

	public const int errIsoInvalidPDU = 196608;

	public const int errIsoInvalidDataSize = 262144;

	public const int errCliNegotiatingPDU = 1048576;

	public const int errCliInvalidParams = 2097152;

	public const int errCliJobPending = 3145728;

	public const int errCliTooManyItems = 4194304;

	public const int errCliInvalidWordLen = 5242880;

	public const int errCliPartialDataWritten = 6291456;

	public const int errCliSizeOverPDU = 7340032;

	public const int errCliInvalidPlcAnswer = 8388608;

	public const int errCliAddressOutOfRange = 9437184;

	public const int errCliInvalidTransportSize = 10485760;

	public const int errCliWriteDataSizeMismatch = 11534336;

	public const int errCliItemNotAvailable = 12582912;

	public const int errCliInvalidValue = 13631488;

	public const int errCliCannotStartPLC = 14680064;

	public const int errCliAlreadyRun = 15728640;

	public const int errCliCannotStopPLC = 16777216;

	public const int errCliCannotCopyRamToRom = 17825792;

	public const int errCliCannotCompress = 18874368;

	public const int errCliAlreadyStop = 19922944;

	public const int errCliFunNotAvailable = 20971520;

	public const int errCliUploadSequenceFailed = 22020096;

	public const int errCliInvalidDataSizeRecvd = 23068672;

	public const int errCliInvalidBlockType = 24117248;

	public const int errCliInvalidBlockNumber = 25165824;

	public const int errCliInvalidBlockSize = 26214400;

	public const int errCliNeedPassword = 30408704;

	public const int errCliInvalidPassword = 31457280;

	public const int errCliNoPasswordToSetOrClear = 32505856;

	public const int errCliJobTimeout = 33554432;

	public const int errCliPartialDataRead = 34603008;

	public const int errCliBufferTooSmall = 35651584;

	public const int errCliFunctionRefused = 36700160;

	public const int errCliDestroying = 37748736;

	public const int errCliInvalidParamNumber = 38797312;

	public const int errCliCannotChangeParam = 39845888;

	public const int errCliFunctionNotImplemented = 40894464;

	public const int p_u16_LocalPort = 1;

	public const int p_u16_RemotePort = 2;

	public const int p_i32_PingTimeout = 3;

	public const int p_i32_SendTimeout = 4;

	public const int p_i32_RecvTimeout = 5;

	public const int p_i32_WorkInterval = 6;

	public const int p_u16_SrcRef = 7;

	public const int p_u16_DstRef = 8;

	public const int p_u16_SrcTSap = 9;

	public const int p_i32_PDURequest = 10;

	public const int p_i32_MaxClients = 11;

	public const int p_i32_BSendTimeout = 12;

	public const int p_i32_BRecvTimeout = 13;

	public const int p_u32_RecoveryTime = 14;

	public const int p_u32_KeepAliveTime = 15;

	public const byte S7AreaPE = 129;

	public const byte S7AreaPA = 130;

	public const byte S7AreaMK = 131;

	public const byte S7AreaDB = 132;

	public const byte S7AreaCT = 28;

	public const byte S7AreaTM = 29;

	public const int S7WLBit = 1;

	public const int S7WLByte = 2;

	public const int S7WLChar = 3;

	public const int S7WLWord = 4;

	public const int S7WLInt = 5;

	public const int S7WLDWord = 6;

	public const int S7WLDInt = 7;

	public const int S7WLReal = 8;

	public const int S7WLCounter = 28;

	public const int S7WLTimer = 29;

	public const int S7CpuStatusUnknown = 0;

	public const int S7CpuStatusRun = 8;

	public const int S7CpuStatusStop = 4;
}
