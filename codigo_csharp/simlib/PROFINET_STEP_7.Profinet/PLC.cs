using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using PROFINET_STEP_7.Types;

namespace PROFINET_STEP_7.Profinet;

public class PLC
{
	public ExceptionCode lastErrorCode;

	public string lastErrorString;

	public bool IsConnected;

	public int LastReadTime;

	public int LastWriteTime;

	private Socket mSocket;

	public string IP { get; set; }

	public CPU_Type CPU { get; set; }

	public short Rack { get; set; }

	public short Slot { get; set; }

	public string Name { get; set; }

	public object Tag { get; set; }

	public bool IsAvailable
	{
		get
		{
			if (new Ping().Send(IP).Status == IPStatus.Success)
			{
				return true;
			}
			return false;
		}
	}

	public PLC()
	{
		IP = "localhost";
		CPU = CPU_Type.S7400;
		Rack = 0;
		Slot = 3;
	}

	public PLC(CPU_Type cpu, string ip, short rack, short slot)
	{
		IP = ip;
		CPU = cpu;
		Rack = rack;
		Slot = slot;
	}

	public PLC(CPU_Type cpu, string ip, short rack, short slot, string name, object tag)
	{
		IP = ip;
		CPU = cpu;
		Rack = rack;
		Slot = slot;
		Name = name;
		Tag = tag;
	}

	public ExceptionCode Open()
	{
		byte[] buffer = new byte[256];
		try
		{
			if (new Ping().Send(IP).Status != IPStatus.Success)
			{
				throw new Exception();
			}
		}
		catch
		{
			lastErrorCode = ExceptionCode.IPAdressNotAvailable;
			lastErrorString = "endereço de IP'" + IP + "' não existe!";
			return lastErrorCode;
		}
		try
		{
			mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			mSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);
			mSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 3000);
			IPEndPoint remoteEP = new IPEndPoint(new IPAddress(IPToByteArray(IP)), 102);
			new IPEndPoint(Dns.GetHostEntry(Dns.GetHostName()).AddressList[0], 102);
			mSocket.Connect(remoteEP);
		}
		catch (Exception ex)
		{
			lastErrorCode = ExceptionCode.ConnectionError;
			lastErrorString = ex.Message;
			return ExceptionCode.ConnectionError;
		}
		try
		{
			byte[] array = new byte[22]
			{
				3, 0, 0, 22, 17, 224, 0, 0, 0, 46,
				0, 193, 2, 1, 0, 194, 2, 3, 0, 192,
				1, 9
			};
			switch (CPU)
			{
			case CPU_Type.S7200:
				array[11] = 193;
				array[12] = 2;
				array[13] = 16;
				array[14] = 0;
				array[15] = 194;
				array[16] = 2;
				array[17] = 16;
				array[18] = 0;
				break;
			case CPU_Type.S7300:
			case CPU_Type.S71200:
				array[11] = 193;
				array[12] = 2;
				array[13] = 1;
				array[14] = 0;
				array[15] = 194;
				array[16] = 2;
				array[17] = 3;
				array[18] = (byte)(Rack * 2 * 16 + Slot);
				break;
			case CPU_Type.S7400:
				array[11] = 193;
				array[12] = 2;
				array[13] = 1;
				array[14] = 0;
				array[15] = 194;
				array[16] = 2;
				array[17] = 3;
				array[18] = (byte)(Rack * 2 * 16 + Slot);
				break;
			default:
				return ExceptionCode.WrongCPU_Type;
			}
			mSocket.Send(array, 22, SocketFlags.None);
			if (mSocket.Receive(buffer, 22, SocketFlags.None) != 22)
			{
				throw new Exception(ExceptionCode.WrongNumberReceivedBytes.ToString());
			}
			byte[] buffer2 = new byte[25]
			{
				3, 0, 0, 25, 2, 240, 128, 50, 1, 0,
				0, 255, 255, 0, 8, 0, 0, 240, 0, 0,
				3, 0, 3, 1, 0
			};
			mSocket.Send(buffer2, 25, SocketFlags.None);
			if (mSocket.Receive(buffer, 27, SocketFlags.None) != 27)
			{
				throw new Exception(ExceptionCode.WrongNumberReceivedBytes.ToString());
			}
			IsConnected = true;
		}
		catch
		{
			lastErrorCode = ExceptionCode.ConnectionError;
			lastErrorString = "Não é possível estabelecer conexão com o PLC!";
			IsConnected = false;
			return ExceptionCode.ConnectionError;
		}
		return ExceptionCode.ExceptionNo;
	}

	public void Close()
	{
		if (mSocket.Connected)
		{
			mSocket.Close();
			IsConnected = false;
		}
	}

	private byte[] IPToByteArray(string ip)
	{
		byte[] array = new byte[4];
		string text = ip;
		string text2 = null;
		try
		{
			text2 = text.Substring(0, text.IndexOf("."));
			array[0] = byte.Parse(text2);
			text = text.Substring(text2.Length + 1);
			text2 = text.Substring(0, text.IndexOf("."));
			array[1] = byte.Parse(text2);
			text = text.Substring(text2.Length + 1);
			text2 = text.Substring(0, text.IndexOf("."));
			array[2] = byte.Parse(text2);
			text = text.Substring(text2.Length + 1);
			array[3] = byte.Parse(text);
			return array;
		}
		catch
		{
			array[0] = 0;
			array[1] = 0;
			array[2] = 0;
			array[3] = 0;
			return array;
		}
	}

	public byte[] ReadBytes(DataType DataType, int DB, int StartByteAdr, int count)
	{
		byte[] array = new byte[count];
		try
		{
			int num = 31;
			ByteArray byteArray = new ByteArray(num);
			byteArray.Add(new byte[3] { 3, 0, 0 });
			byteArray.Add((byte)num);
			byteArray.Add(new byte[18]
			{
				2, 240, 128, 50, 1, 0, 0, 0, 0, 0,
				14, 0, 0, 4, 1, 18, 10, 16
			});
			if ((uint)(DataType - 28) <= 1u)
			{
				byteArray.Add((byte)DataType);
			}
			else
			{
				byteArray.Add(2);
			}
			byteArray.Add(Word.ToByteArray((ushort)count));
			byteArray.Add(Word.ToByteArray((ushort)DB));
			byteArray.Add((byte)DataType);
			byteArray.Add(0);
			if ((uint)(DataType - 28) <= 1u)
			{
				byteArray.Add(Word.ToByteArray((ushort)StartByteAdr));
			}
			else
			{
				byteArray.Add(Word.ToByteArray((ushort)(StartByteAdr * 8)));
			}
			mSocket.Send(byteArray.array, byteArray.array.Length, SocketFlags.None);
			byte[] array2 = new byte[512];
			mSocket.Receive(array2, 512, SocketFlags.None);
			if (array2[21] != byte.MaxValue)
			{
				throw new Exception(ExceptionCode.WrongNumberReceivedBytes.ToString());
			}
			for (int i = 0; i < count; i++)
			{
				array[i] = array2[i + 25];
			}
			return array;
		}
		catch
		{
			lastErrorCode = ExceptionCode.WriteData;
			lastErrorString = "";
			return null;
		}
	}

	public object Read(DataType DataType, int DB, int StartByteAdr, VarType VarType, int VarCount)
	{
		byte[] array = null;
		int num = 0;
		switch (VarType)
		{
		case VarType.Byte:
			num = VarCount;
			if (num < 1)
			{
				num = 1;
			}
			array = ReadBytes(DataType, DB, StartByteAdr, num);
			if (array == null)
			{
				return null;
			}
			if (VarCount == 1)
			{
				return array[0];
			}
			return array;
		case VarType.Word:
			num = VarCount * 2;
			array = ReadBytes(DataType, DB, StartByteAdr, num);
			if (array == null)
			{
				return null;
			}
			if (VarCount == 1)
			{
				return Word.FromByteArray(array);
			}
			return Word.ToArray(array);
		case VarType.Int:
			num = VarCount * 2;
			array = ReadBytes(DataType, DB, StartByteAdr, num);
			if (array == null)
			{
				return null;
			}
			if (VarCount == 1)
			{
				return Int.FromByteArray(array);
			}
			return Int.ToArray(array);
		case VarType.DWord:
			num = VarCount * 4;
			array = ReadBytes(DataType, DB, StartByteAdr, num);
			if (array == null)
			{
				return null;
			}
			if (VarCount == 1)
			{
				return DWord.FromByteArray(array);
			}
			return DWord.ToArray(array);
		case VarType.DInt:
			num = VarCount * 4;
			array = ReadBytes(DataType, DB, StartByteAdr, num);
			if (array == null)
			{
				return null;
			}
			if (VarCount == 1)
			{
				return DInt.FromByteArray(array);
			}
			return DInt.ToArray(array);
		case VarType.Real:
			num = VarCount * 4;
			array = ReadBytes(DataType, DB, StartByteAdr, num);
			if (array == null)
			{
				return null;
			}
			if (VarCount == 1)
			{
				return PROFINET_STEP_7.Types.Double.FromByteArray(array);
			}
			return PROFINET_STEP_7.Types.Double.ToArray(array);
		case VarType.String:
			num = VarCount;
			array = ReadBytes(DataType, DB, StartByteAdr, num);
			if (array == null)
			{
				return null;
			}
			return PROFINET_STEP_7.Types.String.FromByteArray(array);
		case VarType.Timer:
			num = VarCount * 2;
			array = ReadBytes(DataType, DB, StartByteAdr, num);
			if (array == null)
			{
				return null;
			}
			if (VarCount == 1)
			{
				return Timer.FromByteArray(array);
			}
			return Timer.ToArray(array);
		case VarType.Counter:
			num = VarCount * 2;
			array = ReadBytes(DataType, DB, StartByteAdr, num);
			if (array == null)
			{
				return null;
			}
			if (VarCount == 1)
			{
				return Counter.FromByteArray(array);
			}
			return Counter.ToArray(array);
		default:
			return null;
		}
	}

	public object Read(string variable)
	{
		string text = variable.ToUpper();
		text = text.Replace(" ", "");
		try
		{
			switch (text.Substring(0, 2))
			{
			case "DB":
			{
				string[] array = text.Split('.');
				if (array.Length < 2)
				{
					throw new Exception();
				}
				int dB = int.Parse(array[0].Substring(2));
				DataType dataType = DataType.DataBlock;
				string text3 = array[1].Substring(0, 3);
				int num2 = int.Parse(array[1].Substring(3));
				switch (text3)
				{
				case "DBB":
					return (byte)Read(DataType.DataBlock, dB, num2, VarType.Byte, 1);
				case "DBW":
					return (ushort)Read(DataType.DataBlock, dB, num2, VarType.Word, 1);
				case "DBD":
					return (uint)Read(DataType.DataBlock, dB, num2, VarType.DWord, 1);
				case "DBX":
				{
					int startByteAdr = num2;
					int num = int.Parse(array[2]);
					if (num > 7)
					{
						throw new Exception();
					}
					return ((bool[])Read(DataType.DataBlock, dB, startByteAdr, VarType.Bit, 1))[num];
				}
				default:
					throw new Exception();
				}
			}
			case "IB":
				return (byte)Read(DataType.Input, 0, int.Parse(text.Substring(2)), VarType.Byte, 1);
			case "IW":
				return (ushort)Read(DataType.Input, 0, int.Parse(text.Substring(2)), VarType.Word, 1);
			case "ID":
				return (uint)Read(DataType.Input, 0, int.Parse(text.Substring(2)), VarType.DWord, 1);
			case "QB":
				return (byte)Read(DataType.Output, 0, int.Parse(text.Substring(2)), VarType.Byte, 1);
			case "QW":
				return (ushort)Read(DataType.Output, 0, int.Parse(text.Substring(2)), VarType.Word, 1);
			case "QD":
				return (uint)Read(DataType.Output, 0, int.Parse(text.Substring(2)), VarType.DWord, 1);
			case "MB":
				return (byte)Read(DataType.Marker, 0, int.Parse(text.Substring(2)), VarType.Byte, 1);
			case "MW":
				return (ushort)Read(DataType.Marker, 0, int.Parse(text.Substring(2)), VarType.Word, 1);
			case "MD":
				return (uint)Read(DataType.Marker, 0, int.Parse(text.Substring(2)), VarType.DWord, 1);
			default:
			{
				DataType dataType;
				switch (text.Substring(0, 1))
				{
				case "E":
				case "I":
					dataType = DataType.Input;
					break;
				case "A":
				case "O":
					dataType = DataType.Output;
					break;
				case "M":
					dataType = DataType.Marker;
					break;
				case "T":
					return (double)Read(DataType.Timer, 0, int.Parse(text.Substring(1)), VarType.Timer, 1);
				case "Z":
				case "C":
					return (ushort)Read(DataType.Counter, 0, int.Parse(text.Substring(1)), VarType.Counter, 1);
				default:
					throw new Exception();
				}
				string text2 = text.Substring(1);
				if (text2.IndexOf(".") == -1)
				{
					throw new Exception();
				}
				int startByteAdr = int.Parse(text2.Substring(0, text2.IndexOf(".")));
				int num = int.Parse(text2.Substring(text2.IndexOf(".") + 1));
				if (num > 7)
				{
					throw new Exception();
				}
				return ((bool[])Read(dataType, 0, startByteAdr, VarType.Bit, 1))[num];
			}
			}
		}
		catch
		{
			lastErrorCode = ExceptionCode.WrongVariableFormat;
			lastErrorString = "Die Variable '" + variable + "' konnte nicht entschlüsselt werden!";
			return lastErrorCode;
		}
	}

	public object ReadStruct(Type structType, int DB)
	{
		double num = Struct.GetStructSize(structType);
		byte[] bytes = (byte[])Read(DataType.DataBlock, DB, 0, VarType.Byte, (int)num);
		return Struct.FromBytes(structType, bytes);
	}

	public ExceptionCode WriteBytes(DataType DataType, int DB, int StartByteAdr, byte[] value)
	{
		byte[] array = new byte[513];
		int num = 0;
		try
		{
			num = value.Length;
			int num2 = 35 + value.Length;
			ByteArray byteArray = new ByteArray(num2);
			byteArray.Add(new byte[3] { 3, 0, 0 });
			byteArray.Add((byte)num2);
			byteArray.Add(new byte[7] { 2, 240, 128, 50, 1, 0, 0 });
			byteArray.Add(Word.ToByteArray((ushort)(num - 1)));
			byteArray.Add(new byte[2] { 0, 14 });
			byteArray.Add(Word.ToByteArray((ushort)(num + 4)));
			byteArray.Add(new byte[6] { 5, 1, 18, 10, 16, 2 });
			byteArray.Add(Word.ToByteArray((ushort)num));
			byteArray.Add(Word.ToByteArray((ushort)DB));
			byteArray.Add((byte)DataType);
			byteArray.Add(0);
			byteArray.Add(Word.ToByteArray((ushort)(StartByteAdr * 8)));
			byteArray.Add(new byte[2] { 0, 4 });
			byteArray.Add(Word.ToByteArray((ushort)(num * 8)));
			byteArray.Add(value);
			mSocket.Send(byteArray.array, byteArray.array.Length, SocketFlags.None);
			mSocket.Receive(array, 512, SocketFlags.None);
			if (array[21] != byte.MaxValue)
			{
				throw new Exception(ExceptionCode.WrongNumberReceivedBytes.ToString());
			}
			return ExceptionCode.ExceptionNo;
		}
		catch
		{
			lastErrorCode = ExceptionCode.WriteData;
			lastErrorString = "";
			return lastErrorCode;
		}
	}

	public object Write(DataType DataType, int DB, int StartByteAdr, object value)
	{
		byte[] array = null;
		switch (value.GetType().Name)
		{
		case "Byte":
			array = PROFINET_STEP_7.Types.Byte.ToByteArray((byte)value);
			break;
		case "Int16":
			array = Int.ToByteArray((short)value);
			break;
		case "UInt16":
			array = Word.ToByteArray((ushort)value);
			break;
		case "Int32":
			array = DInt.ToByteArray((int)value);
			break;
		case "UInt32":
			array = DWord.ToByteArray((uint)value);
			break;
		case "Double":
			array = PROFINET_STEP_7.Types.Double.ToByteArray((double)value);
			break;
		case "Byte[]":
			array = (byte[])value;
			break;
		case "Int16[]":
			array = Int.ToByteArray((short[])value);
			break;
		case "UInt16[]":
			array = Word.ToByteArray((ushort[])value);
			break;
		case "Int32[]":
			array = DInt.ToByteArray((int[])value);
			break;
		case "UInt32[]":
			array = DWord.ToByteArray((uint[])value);
			break;
		case "Double[]":
			array = PROFINET_STEP_7.Types.Double.ToByteArray((double[])value);
			break;
		case "String":
			array = PROFINET_STEP_7.Types.String.ToByteArray(value as string);
			break;
		default:
			return ExceptionCode.WrongVariableFormat;
		}
		return WriteBytes(DataType, DB, StartByteAdr, array);
	}

	public object Write(string variable, object value)
	{
		string text = variable.ToUpper();
		text = text.Replace(" ", "");
		try
		{
			switch (text.Substring(0, 2))
			{
			case "DB":
			{
				string[] array = text.Split('.');
				if (array.Length < 2)
				{
					throw new Exception();
				}
				int dB = int.Parse(array[0].Substring(2));
				DataType dataType = DataType.DataBlock;
				string text3 = array[1].Substring(0, 3);
				int num2 = int.Parse(array[1].Substring(3));
				switch (text3)
				{
				case "DBB":
				{
					object obj = Convert.ChangeType(value, typeof(byte));
					return Write(DataType.DataBlock, dB, num2, (byte)obj);
				}
				case "DBW":
				{
					object obj = Convert.ChangeType(value, typeof(ushort));
					return Write(DataType.DataBlock, dB, num2, (ushort)obj);
				}
				case "DBD":
				{
					object obj = Convert.ChangeType(value, typeof(uint));
					return Write(DataType.DataBlock, dB, num2, (uint)obj);
				}
				case "DBX":
				{
					int startByteAdr = num2;
					int num = int.Parse(array[2]);
					if (num > 7)
					{
						throw new Exception();
					}
					byte b2 = (byte)Read(DataType.DataBlock, dB, startByteAdr, VarType.Byte, 1);
					b2 = (((int)value != 1) ? ((byte)(b2 & (b2 ^ (byte)Math.Pow(2.0, num)))) : ((byte)(b2 | (byte)Math.Pow(2.0, num))));
					return Write(DataType.DataBlock, dB, startByteAdr, b2);
				}
				case "DBS":
					return Write(DataType.DataBlock, dB, num2, (string)value);
				default:
					throw new Exception();
				}
			}
			case "EB":
			{
				object obj = Convert.ChangeType(value, typeof(byte));
				return Write(DataType.Input, 0, int.Parse(text.Substring(2)), (byte)obj);
			}
			case "EW":
			{
				object obj = Convert.ChangeType(value, typeof(ushort));
				return Write(DataType.Input, 0, int.Parse(text.Substring(2)), (ushort)obj);
			}
			case "ED":
			{
				object obj = Convert.ChangeType(value, typeof(uint));
				return Write(DataType.Input, 0, int.Parse(text.Substring(2)), (uint)obj);
			}
			case "AB":
			{
				object obj = Convert.ChangeType(value, typeof(byte));
				return Write(DataType.Output, 0, int.Parse(text.Substring(2)), (byte)obj);
			}
			case "AW":
			{
				object obj = Convert.ChangeType(value, typeof(ushort));
				return Write(DataType.Output, 0, int.Parse(text.Substring(2)), (ushort)obj);
			}
			case "AD":
			{
				object obj = Convert.ChangeType(value, typeof(uint));
				return Write(DataType.Output, 0, int.Parse(text.Substring(2)), (uint)obj);
			}
			case "MB":
			{
				object obj = Convert.ChangeType(value, typeof(byte));
				return Write(DataType.Marker, 0, int.Parse(text.Substring(2)), (byte)obj);
			}
			case "MW":
			{
				object obj = Convert.ChangeType(value, typeof(ushort));
				return Write(DataType.Marker, 0, int.Parse(text.Substring(2)), (ushort)obj);
			}
			case "MD":
				return Write(DataType.Marker, 0, int.Parse(text.Substring(2)), value);
			default:
			{
				DataType dataType;
				switch (text.Substring(0, 1))
				{
				case "E":
				case "I":
					dataType = DataType.Input;
					break;
				case "A":
				case "O":
					dataType = DataType.Output;
					break;
				case "M":
					dataType = DataType.Marker;
					break;
				case "T":
					return Write(DataType.Timer, 0, int.Parse(text.Substring(1)), (double)value);
				case "Z":
				case "C":
					return Write(DataType.Counter, 0, int.Parse(text.Substring(1)), (short)value);
				default:
					throw new Exception("Unbekannte Variable");
				}
				string text2 = text.Substring(1);
				if (text2.IndexOf(".") == -1)
				{
					throw new Exception("Unbekannte Variable");
				}
				int startByteAdr = int.Parse(text2.Substring(0, text2.IndexOf(".")));
				int num = int.Parse(text2.Substring(text2.IndexOf(".") + 1));
				if (num > 7)
				{
					throw new Exception("Unbekannte Variable");
				}
				byte b = (byte)Read(dataType, 0, startByteAdr, VarType.Byte, 1);
				b = (((int)value != 1) ? ((byte)(b & (b ^ (byte)Math.Pow(2.0, num)))) : ((byte)(b | (byte)Math.Pow(2.0, num))));
				return Write(dataType, 0, startByteAdr, b);
			}
			}
		}
		catch (Exception ex)
		{
			_ = ex.Message;
			lastErrorCode = ExceptionCode.WrongVariableFormat;
			lastErrorString = "A variável '" + variable + "' não pôde ser decifrada!";
			return lastErrorCode;
		}
	}

	public ExceptionCode WriteStruct(object structValue, int DB)
	{
		try
		{
			byte[] value = Struct.ToBytes(structValue);
			return WriteBytes(DataType.DataBlock, DB, 0, value);
		}
		catch
		{
			lastErrorCode = ExceptionCode.WriteData;
			lastErrorString = "Ocorreu um erro ao gravar os dados!";
			return lastErrorCode;
		}
	}
}
