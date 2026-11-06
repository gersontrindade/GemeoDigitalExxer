using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class UModbusTCP : MonoBehaviour
{
	public enum CONNECTION_MODE
	{
		LINEAR,
		PARALLEL
	}

	public delegate void ResponseData(ushort _usId, byte _bUnit, byte _bFunction, byte[] _bData);

	public delegate void ExceptionData(ushort _usId, byte _bUnit, byte _bFunction, byte _bException);

	private const byte FUNCTION_READ_COIL = 1;

	private const byte FUNCTION_READ_DISCRETE_INPUTS = 2;

	private const byte FUNCTION_READ_HOLDING_REGISTER = 3;

	private const byte FUNCTION_READ_INPUT_REGISTER = 4;

	private const byte FUNCTION_WRITE_SINGLE_COIL = 5;

	private const byte FUNCTION_WRITE_SINGLE_REGISTER = 6;

	private const byte FUNCTION_WRITE_MULTIPLE_COILS = 15;

	private const byte FUNCTION_WRITE_MULTIPLE_REGISTER = 16;

	private const byte FUNCTION_READ_WRITE_MULTIPLE_REGISTER = 23;

	public const byte EXCEPTION_ILEGAL_FUNCTION = 1;

	public const byte EXCEPTION_ILEGAL_DATA_ADDRESS = 2;

	public const byte EXCEPTION_ILEGAL_DATA_VALUE = 3;

	public const byte EXCEPTION_SLAVE_DEVICA_FAILURE = 4;

	public const byte EXCEPTION_ACKNOWLEDGE = 5;

	public const byte EXCEPTION_SLAVE_IS_BUSY = 6;

	public const byte EXCEPTION_GATE_PATH_UNAVAILABLE = 10;

	public const byte EXCEPTION_NOT_CONNECTED = 253;

	public const byte EXCEPTION_CONNECTION_LOST = 254;

	public const byte EXCEPTION_TIMEOUT = byte.MaxValue;

	private const byte EXCEPTION_WRONG_OFFSET = 128;

	private const byte EXCEPTION_SEND_FAIL = 100;

	private static UModbusTCP m_oInstance;

	private static string m_sIp;

	private static ushort m_usPort;

	private static ushort m_usConnectTimeout = 500;

	private static ushort m_usTimeout = 500;

	private static ushort m_usRefresh = 10;

	private static bool m_bConnected = false;

	private static bool m_bSyncConnected = false;

	private static bool m_bAsyncConnected = false;

	private Socket m_oAsyncTCPSocket;

	private byte[] m_bAsyncTCPSocketBuffer = new byte[2048];

	private Socket m_oSyncTCPSocket;

	private byte[] m_bSyncTCPSocketBuffer = new byte[2048];

	public static UModbusTCP Instance
	{
		get
		{
			if (m_oInstance == null)
			{
				m_oInstance = new GameObject
				{
					name = "UModbusTCP"
				}.AddComponent<UModbusTCP>();
			}
			return m_oInstance;
		}
	}

	public ushort timeout
	{
		get
		{
			return m_usTimeout;
		}
		set
		{
			m_usTimeout = value;
		}
	}

	public ushort connectTimeout
	{
		get
		{
			return m_usConnectTimeout;
		}
		set
		{
			m_usConnectTimeout = value;
		}
	}

	public ushort refresh
	{
		get
		{
			return m_usRefresh;
		}
		set
		{
			m_usRefresh = value;
		}
	}

	public bool connected => m_bConnected;

	public string ip => m_sIp;

	public ushort port => m_usPort;

	public event ResponseData OnResponseData;

	public event ExceptionData OnException;

	private void Awake()
	{
		if (m_oInstance != null && m_oInstance != this)
		{
			UnityEngine.Object.Destroy(GetComponent<UModbusTCP>());
		}
		else
		{
			m_oInstance = this;
		}
	}

	public UModbusTCP()
	{
	}

	public UModbusTCP(string _sIp, ushort _usPort)
	{
		m_sIp = _sIp;
		m_usPort = _usPort;
		Connect(_sIp, _usPort);
	}

	public void ConnectWithCoroutine(string _sIp, ushort _usPort, CONNECTION_MODE _eConnectionMode = CONNECTION_MODE.LINEAR)
	{
		m_sIp = _sIp;
		m_usPort = _usPort;
		StartCoroutine(ConnectCoroutine(_sIp, _usPort, _eConnectionMode));
	}

	public void StopConnectionCoroutine()
	{
		StopCoroutine("ConnectCoroutine");
		m_sIp = "0.0.0.0";
		m_usPort = 502;
		m_bConnected = false;
		m_bSyncConnected = false;
		m_bAsyncConnected = false;
	}

	public void Connect(string _sIp, ushort _usPort, CONNECTION_MODE _eConnectionMode = CONNECTION_MODE.LINEAR)
	{
		m_sIp = _sIp;
		m_usPort = _usPort;
		try
		{
			if (!IPAddress.TryParse(_sIp, out var _))
			{
				_sIp = Dns.GetHostEntry(_sIp).AddressList[0].ToString();
			}
			m_oAsyncTCPSocket = new Socket(IPAddress.Parse(_sIp).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			m_oAsyncTCPSocket.ReceiveTimeout = m_usConnectTimeout;
			m_oAsyncTCPSocket.SendTimeout = m_usConnectTimeout;
			if (_eConnectionMode != CONNECTION_MODE.LINEAR && _eConnectionMode == CONNECTION_MODE.PARALLEL)
			{
				m_oAsyncTCPSocket.BeginConnect(new IPEndPoint(IPAddress.Parse(_sIp), _usPort), ConnectCoroutineCallback, null);
			}
			else
			{
				m_oAsyncTCPSocket.Connect(new IPEndPoint(IPAddress.Parse(_sIp), _usPort));
			}
			m_oAsyncTCPSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, m_usTimeout);
			m_oAsyncTCPSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, m_usTimeout);
			m_oAsyncTCPSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Debug, 1);
			m_oSyncTCPSocket = new Socket(IPAddress.Parse(_sIp).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			m_oSyncTCPSocket.ReceiveTimeout = m_usConnectTimeout;
			m_oSyncTCPSocket.SendTimeout = m_usConnectTimeout;
			if (_eConnectionMode != CONNECTION_MODE.LINEAR && _eConnectionMode == CONNECTION_MODE.PARALLEL)
			{
				m_oSyncTCPSocket.BeginConnect(new IPEndPoint(IPAddress.Parse(_sIp), _usPort), ConnectCoroutineCallback, null);
			}
			else
			{
				m_oSyncTCPSocket.Connect(new IPEndPoint(IPAddress.Parse(_sIp), _usPort));
			}
			m_oSyncTCPSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, m_usTimeout);
			m_oSyncTCPSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, m_usTimeout);
			m_oSyncTCPSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Debug, 1);
			if (_eConnectionMode == CONNECTION_MODE.LINEAR)
			{
				m_bConnected = true;
			}
		}
		catch (IOException ex)
		{
			m_bConnected = false;
			m_bSyncConnected = false;
			m_bAsyncConnected = false;
			throw ex;
		}
	}

	public IEnumerator ConnectCoroutine(string _sIp, ushort _usPort, CONNECTION_MODE _eConnectionMode = CONNECTION_MODE.LINEAR)
	{
		m_sIp = _sIp;
		m_usPort = _usPort;
		try
		{
			m_bConnected = false;
			m_bSyncConnected = false;
			m_bAsyncConnected = false;
			if (!IPAddress.TryParse(_sIp, out var _))
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(_sIp);
				_sIp = hostEntry.AddressList[0].ToString();
			}
			m_oAsyncTCPSocket = new Socket(IPAddress.Parse(_sIp).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			m_oAsyncTCPSocket.ReceiveTimeout = m_usConnectTimeout;
			m_oAsyncTCPSocket.SendTimeout = m_usConnectTimeout;
			CONNECTION_MODE cONNECTION_MODE = _eConnectionMode;
			if (cONNECTION_MODE != CONNECTION_MODE.LINEAR && cONNECTION_MODE == CONNECTION_MODE.PARALLEL)
			{
				m_oAsyncTCPSocket.BeginConnect(new IPEndPoint(IPAddress.Parse(_sIp), _usPort), ConnectCoroutineCallback, null);
			}
			else
			{
				m_oAsyncTCPSocket.Connect(new IPEndPoint(IPAddress.Parse(_sIp), _usPort));
			}
			m_oAsyncTCPSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, m_usTimeout);
			m_oAsyncTCPSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, m_usTimeout);
			m_oAsyncTCPSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Debug, 1);
			m_oSyncTCPSocket = new Socket(IPAddress.Parse(_sIp).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			m_oSyncTCPSocket.ReceiveTimeout = m_usConnectTimeout;
			m_oSyncTCPSocket.SendTimeout = m_usConnectTimeout;
			cONNECTION_MODE = _eConnectionMode;
			if (cONNECTION_MODE != CONNECTION_MODE.LINEAR && cONNECTION_MODE == CONNECTION_MODE.PARALLEL)
			{
				m_oSyncTCPSocket.BeginConnect(new IPEndPoint(IPAddress.Parse(_sIp), _usPort), ConnectCoroutineCallback, null);
			}
			else
			{
				m_oSyncTCPSocket.Connect(new IPEndPoint(IPAddress.Parse(_sIp), _usPort));
			}
			m_oSyncTCPSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, m_usTimeout);
			m_oSyncTCPSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, m_usTimeout);
			m_oSyncTCPSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Debug, 1);
			if (_eConnectionMode == CONNECTION_MODE.LINEAR)
			{
				m_bConnected = true;
			}
		}
		catch
		{
			m_bConnected = false;
			m_bSyncConnected = false;
			m_bAsyncConnected = false;
			Debug.Log("Error connection on ConnectCoroutine");
		}
		yield break;
	}

	private void ConnectCoroutineCallback(IAsyncResult _oResult)
	{
		m_bAsyncConnected = m_oAsyncTCPSocket.Connected;
		m_bSyncConnected = m_oSyncTCPSocket.Connected;
		if (m_bAsyncConnected && m_bSyncConnected)
		{
			m_bConnected = true;
		}
		else
		{
			m_bConnected = false;
		}
	}

	public void Disconnect()
	{
		Dispose();
	}

	~UModbusTCP()
	{
		Dispose();
	}

	public void Dispose()
	{
		m_sIp = "0.0.0.0";
		m_usPort = 502;
		m_bConnected = false;
		m_bSyncConnected = false;
		m_bAsyncConnected = false;
		if (m_oAsyncTCPSocket != null)
		{
			if (m_oAsyncTCPSocket.Connected)
			{
				try
				{
					m_oAsyncTCPSocket.Shutdown(SocketShutdown.Both);
				}
				catch
				{
				}
				m_oAsyncTCPSocket.Close();
			}
			m_oAsyncTCPSocket = null;
		}
		if (m_oSyncTCPSocket == null)
		{
			return;
		}
		if (m_oSyncTCPSocket.Connected)
		{
			try
			{
				m_oSyncTCPSocket.Shutdown(SocketShutdown.Both);
			}
			catch
			{
			}
			m_oSyncTCPSocket.Close();
		}
		m_oSyncTCPSocket = null;
	}

	internal void CallException(ushort _usId, byte _bUnit, byte _bFunction, byte _bException)
	{
		if (m_oAsyncTCPSocket != null && m_oSyncTCPSocket != null)
		{
			if (_bException == 254)
			{
				m_oSyncTCPSocket = null;
				m_oAsyncTCPSocket = null;
			}
			if (this.OnException != null)
			{
				this.OnException(_usId, _bUnit, _bFunction, _bException);
			}
		}
	}

	internal static ushort SwapUInt16(ushort inValue)
	{
		return (ushort)(((inValue & 0xFF00) >> 8) | ((inValue & 0xFF) << 8));
	}

	public void ReadCoils_1(ushort _usId, byte _bUnit, ushort _usStartAddress, ushort _usNumInputs)
	{
		WriteAsyncData(CreateReadHeader(_usId, _bUnit, _usStartAddress, _usNumInputs, 1), _usId);
	}

	public void ReadCoils(ushort _usId, byte _bUnit, ushort _usStartAddress, ushort _usNumInputs, ref byte[] _bValues)
	{
		_bValues = WriteSyncData(CreateReadHeader(_usId, _bUnit, _usStartAddress, _usNumInputs, 1), _usId);
	}

	public void ReadDiscreteInputs(ushort _usId, byte _bUnit, ushort _usStartAddress, ushort _usNumInputs)
	{
		WriteAsyncData(CreateReadHeader(_usId, _bUnit, _usStartAddress, _usNumInputs, 2), _usId);
	}

	public void ReadDiscreteInputs(ushort _usId, byte _bUnit, ushort _usStartAddress, ushort _usNumInputs, ref byte[] _bValues)
	{
		_bValues = WriteSyncData(CreateReadHeader(_usId, _bUnit, _usStartAddress, _usNumInputs, 2), _usId);
	}

	public byte[] ReadHoldingRegister(ushort _usId, byte _bUnit, ushort _usStartAddress, ushort _usNumInputs)
	{
		byte[] array = CreateReadHeader(_usId, _bUnit, _usStartAddress, _usNumInputs, 3);
		WriteAsyncData(array, _usId);
		return array;
	}

	public void ReadHoldingRegister(ushort _usId, byte _bUnit, ushort _usStartAddress, ushort _usNumInputs, ref byte[] _bValues)
	{
		_bValues = WriteSyncData(CreateReadHeader(_usId, _bUnit, _usStartAddress, _usNumInputs, 3), _usId);
	}

	public void ReadInputRegister(ushort _usId, byte _bUnit, ushort _usStartAddress, ushort _usNumInputs)
	{
		WriteAsyncData(CreateReadHeader(_usId, _bUnit, _usStartAddress, _usNumInputs, 4), _usId);
	}

	public void ReadInputRegister(ushort _usId, byte _bUnit, ushort _usStartAddress, ushort _usNumInputs, ref byte[] _bValues)
	{
		_bValues = WriteSyncData(CreateReadHeader(_usId, _bUnit, _usStartAddress, _usNumInputs, 4), _usId);
	}

	public byte[] WriteSingleCoils(ushort _usId, byte _bUnit, ushort _usStartAddress, bool _bOnOff)
	{
		byte[] array = CreateWriteHeader(_usId, _bUnit, _usStartAddress, 1, 1, 5);
		if (_bOnOff)
		{
			array[10] = byte.MaxValue;
		}
		else
		{
			array[10] = 0;
		}
		WriteAsyncData(array, _usId);
		return array;
	}

	public void WriteSingleCoils(ushort _usId, byte _bUnit, ushort _usStartAddress, bool _bOnOff, ref byte[] _bResult)
	{
		byte[] array = CreateWriteHeader(_usId, _bUnit, _usStartAddress, 1, 1, 5);
		if (_bOnOff)
		{
			array[10] = byte.MaxValue;
		}
		else
		{
			array[10] = 0;
		}
		_bResult = WriteSyncData(array, _usId);
	}

	public void WriteMultipleCoils(ushort _usId, byte _bUnit, ushort _usStartAddress, ushort _usNumBits, byte[] _bValues)
	{
		byte b = Convert.ToByte(_bValues.Length);
		byte[] array = CreateWriteHeader(_usId, _bUnit, _usStartAddress, _usNumBits, (byte)(b + 2), 15);
		Array.Copy(_bValues, 0, array, 13, b);
		WriteAsyncData(array, _usId);
	}

	public void WriteMultipleCoils(ushort _usId, byte _bUnit, ushort _usStartAddress, ushort numBits, byte[] _bValues, ref byte[] _bResult)
	{
		byte b = Convert.ToByte(_bValues.Length);
		byte[] array = CreateWriteHeader(_usId, _bUnit, _usStartAddress, numBits, (byte)(b + 2), 15);
		Array.Copy(_bValues, 0, array, 13, b);
		_bResult = WriteSyncData(array, _usId);
	}

	public byte[] WriteSingleRegister(ushort _usId, byte _bUnit, ushort _usStartAddress, byte[] _bValues)
	{
		byte[] array = CreateWriteHeader(_usId, _bUnit, _usStartAddress, 1, 1, 6);
		array[10] = _bValues[0];
		array[11] = _bValues[1];
		WriteAsyncData(array, _usId);
		return array;
	}

	public byte[] WriteSingleRegister(ushort _usId, byte _bUnit, ushort _usStartAddress, byte[] _bValues, ref byte[] _bResult)
	{
		byte[] array = CreateWriteHeader(_usId, _bUnit, _usStartAddress, 1, 1, 6);
		array[10] = _bValues[0];
		array[11] = _bValues[1];
		_bResult = WriteSyncData(array, _usId);
		return array;
	}

	public byte[] WriteMultipleRegister(ushort _usId, byte _bUnit, ushort _usStartAddress, byte[] _bValues)
	{
		ushort num = Convert.ToUInt16(_bValues.Length);
		if (num % 2 > 0)
		{
			num++;
		}
		byte[] array = CreateWriteHeader(_usId, _bUnit, _usStartAddress, Convert.ToUInt16(num / 2), Convert.ToUInt16(num + 2), 16);
		Array.Copy(_bValues, 0, array, 13, _bValues.Length);
		WriteAsyncData(array, _usId);
		return array;
	}

	public byte[] WriteMultipleRegister(ushort _usId, byte _bUnit, ushort _usStartAddress, byte[] _bValues, ref byte[] _bResult)
	{
		ushort num = Convert.ToUInt16(_bValues.Length);
		if (num % 2 > 0)
		{
			num++;
		}
		byte[] array = CreateWriteHeader(_usId, _bUnit, _usStartAddress, Convert.ToUInt16(num / 2), Convert.ToUInt16(num + 2), 16);
		Array.Copy(_bValues, 0, array, 13, _bValues.Length);
		_bResult = WriteSyncData(array, _usId);
		return array;
	}

	public byte[] ReadWriteMultipleRegister(ushort _usId, byte _bUnit, ushort _usStartAddress, ushort _usNumInputs, ushort _usStartWriteAddress, byte[] _bValues)
	{
		ushort num = Convert.ToUInt16(_bValues.Length);
		if (num % 2 > 0)
		{
			num++;
		}
		byte[] array = CreateReadWriteHeader(_usId, _bUnit, _usStartAddress, _usNumInputs, _usStartWriteAddress, Convert.ToUInt16(num / 2));
		Array.Copy(_bValues, 0, array, 17, _bValues.Length);
		WriteAsyncData(array, _usId);
		return array;
	}

	public byte[] ReadWriteMultipleRegister(ushort _usId, byte _bUnit, ushort _usStartAddress, ushort _usNumInputs, ushort _usStartWriteAddress, byte[] _bValues, ref byte[] _bResult)
	{
		ushort num = Convert.ToUInt16(_bValues.Length);
		if (num % 2 > 0)
		{
			num++;
		}
		byte[] array = CreateReadWriteHeader(_usId, _bUnit, _usStartAddress, _usNumInputs, _usStartWriteAddress, Convert.ToUInt16(num / 2));
		Array.Copy(_bValues, 0, array, 17, _bValues.Length);
		_bResult = WriteSyncData(array, _usId);
		return array;
	}

	private byte[] CreateReadHeader(ushort _usId, byte _bUnit, ushort _usStartAddress, ushort _usLength, byte _bFunction)
	{
		byte[] array = new byte[12];
		byte[] bytes = BitConverter.GetBytes((short)_usId);
		array[0] = bytes[1];
		array[1] = bytes[0];
		array[5] = 6;
		array[6] = _bUnit;
		array[7] = _bFunction;
		byte[] bytes2 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)_usStartAddress));
		array[8] = bytes2[0];
		array[9] = bytes2[1];
		byte[] bytes3 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)_usLength));
		array[10] = bytes3[0];
		array[11] = bytes3[1];
		return array;
	}

	private byte[] CreateWriteHeader(ushort _usId, byte _bUnit, ushort _usStartAddress, ushort _usNumData, ushort _usNumBytes, byte _bFunction)
	{
		byte[] array = new byte[_usNumBytes + 11];
		byte[] bytes = BitConverter.GetBytes((short)_usId);
		array[0] = bytes[1];
		array[1] = bytes[0];
		byte[] bytes2 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)(5 + _usNumBytes)));
		array[4] = bytes2[0];
		array[5] = bytes2[1];
		array[6] = _bUnit;
		array[7] = _bFunction;
		byte[] bytes3 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)_usStartAddress));
		array[8] = bytes3[0];
		array[9] = bytes3[1];
		if (_bFunction >= 15)
		{
			byte[] bytes4 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)_usNumData));
			array[10] = bytes4[0];
			array[11] = bytes4[1];
			array[12] = (byte)(_usNumBytes - 2);
		}
		return array;
	}

	private byte[] CreateReadWriteHeader(ushort _usId, byte _bUnit, ushort _usStartReadAddress, ushort _usNumRead, ushort _usStartWriteAddress, ushort _usNumWrite)
	{
		byte[] array = new byte[_usNumWrite * 2 + 17];
		byte[] bytes = BitConverter.GetBytes((short)_usId);
		array[0] = bytes[1];
		array[1] = bytes[0];
		byte[] bytes2 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)(11 + _usNumWrite * 2)));
		array[4] = bytes2[0];
		array[5] = bytes2[1];
		array[6] = _bUnit;
		array[7] = 23;
		byte[] bytes3 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)_usStartReadAddress));
		array[8] = bytes3[0];
		array[9] = bytes3[1];
		byte[] bytes4 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)_usNumRead));
		array[10] = bytes4[0];
		array[11] = bytes4[1];
		byte[] bytes5 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)_usStartWriteAddress));
		array[12] = bytes5[0];
		array[13] = bytes5[1];
		byte[] bytes6 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)_usNumWrite));
		array[14] = bytes6[0];
		array[15] = bytes6[1];
		array[16] = (byte)(_usNumWrite * 2);
		return array;
	}

	private void WriteAsyncData(byte[] _bWriteData, ushort _usId)
	{
		if (m_oAsyncTCPSocket != null && m_oAsyncTCPSocket.Connected)
		{
			try
			{
				m_oAsyncTCPSocket.BeginSend(_bWriteData, 0, _bWriteData.Length, SocketFlags.None, OnSend, null);
				m_oAsyncTCPSocket.BeginReceive(m_bAsyncTCPSocketBuffer, 0, m_bAsyncTCPSocketBuffer.Length, SocketFlags.None, OnReceive, m_oAsyncTCPSocket);
				return;
			}
			catch (SystemException)
			{
				CallException(_usId, _bWriteData[6], _bWriteData[7], 254);
				return;
			}
		}
		CallException(_usId, _bWriteData[6], _bWriteData[7], 254);
	}

	private void OnSend(IAsyncResult _oResult)
	{
		if (!_oResult.IsCompleted)
		{
			CallException(ushort.MaxValue, byte.MaxValue, byte.MaxValue, 100);
		}
	}

	private void OnReceive(IAsyncResult _oResult)
	{
		if (!_oResult.IsCompleted)
		{
			CallException(255, byte.MaxValue, byte.MaxValue, 254);
		}
		ushort usId = SwapUInt16(BitConverter.ToUInt16(m_bAsyncTCPSocketBuffer, 0));
		byte bUnit = m_bAsyncTCPSocketBuffer[6];
		byte b = m_bAsyncTCPSocketBuffer[7];
		if (b >= 5 && b != 23)
		{
			byte[] destinationArray = new byte[2];
			Array.Copy(m_bAsyncTCPSocketBuffer, 10, destinationArray, 0, 2);
		}
		else
		{
			byte[] destinationArray = new byte[m_bAsyncTCPSocketBuffer[8]];
			Array.Copy(m_bAsyncTCPSocketBuffer, 9, destinationArray, 0, m_bAsyncTCPSocketBuffer[8]);
		}
		if (b > 128)
		{
			b -= 128;
			CallException(usId, bUnit, b, m_bAsyncTCPSocketBuffer[8]);
		}
		else if (this.OnResponseData != null)
		{
			this.OnResponseData(usId, bUnit, b, m_bAsyncTCPSocketBuffer);
		}
	}

	private byte[] WriteSyncData(byte[] _bWriteData, ushort _uId)
	{
		if (m_oSyncTCPSocket.Connected)
		{
			try
			{
				m_oSyncTCPSocket.Send(_bWriteData, 0, _bWriteData.Length, SocketFlags.None);
				int num = m_oSyncTCPSocket.Receive(m_bSyncTCPSocketBuffer, 0, m_bSyncTCPSocketBuffer.Length, SocketFlags.None);
				byte bUnit = m_bSyncTCPSocketBuffer[6];
				byte b = m_bSyncTCPSocketBuffer[7];
				if (num == 0)
				{
					CallException(_uId, bUnit, _bWriteData[7], 254);
				}
				if (b > 128)
				{
					b -= 128;
					CallException(_uId, bUnit, b, m_bSyncTCPSocketBuffer[8]);
					return null;
				}
				byte[] array;
				if (b >= 5 && b != 23)
				{
					array = new byte[2];
					Array.Copy(m_bSyncTCPSocketBuffer, 10, array, 0, 2);
				}
				else
				{
					array = new byte[m_bSyncTCPSocketBuffer[8]];
					Array.Copy(m_bSyncTCPSocketBuffer, 9, array, 0, m_bSyncTCPSocketBuffer[8]);
				}
				return array;
			}
			catch (SystemException)
			{
				CallException(_uId, _bWriteData[6], _bWriteData[7], 254);
			}
		}
		else
		{
			CallException(_uId, _bWriteData[6], _bWriteData[7], 254);
		}
		return null;
	}
}
