using System;
using System.Collections;
using System.Collections.Generic;
using HexDumping;

namespace IsoOnTcp;

public class TPDUConnection
{
	public const int MAX_TSAP_LEN = 32;

	public ushort DstRef;

	public ushort SrcRef;

	public int ClassOption;

	private ArrayList Varpart;

	private int MaxTDPUSize;

	public TPDUConnection()
	{
	}

	public TPDUConnection(byte[] packet)
	{
		int num = packet[0];
		TPDU.TPDU_TYPES tPDU_TYPES = (TPDU.TPDU_TYPES)(packet[1] >> 4);
		if (tPDU_TYPES != TPDU.TPDU_TYPES.CR && tPDU_TYPES != TPDU.TPDU_TYPES.CC)
		{
			throw new ApplicationException("TPDU: This can only handle CC/CR TDPUs");
		}
		if (BitConverter.IsLittleEndian)
		{
			DstRef = ByteConvert.DoReverseEndian(BitConverter.ToUInt16(packet, 2));
			SrcRef = ByteConvert.DoReverseEndian(BitConverter.ToUInt16(packet, 4));
		}
		else
		{
			DstRef = BitConverter.ToUInt16(packet, 2);
			SrcRef = BitConverter.ToUInt16(packet, 4);
		}
		ClassOption = (packet[6] & 0xF0) >> 4;
		if (ClassOption > 4)
		{
			throw new Exception("TPDU: Class option number not allowed.");
		}
		int num2 = 7;
		Varpart = new ArrayList();
		try
		{
			while (num2 < num)
			{
				VarParam varParam = default(VarParam);
				varParam.code = packet[num2];
				num2++;
				varParam.length = packet[num2];
				num2++;
				varParam.value = new byte[varParam.length];
				Array.Copy(packet, num2, varParam.value, 0, varParam.length);
				num2 += varParam.length;
				Varpart.Add(varParam);
			}
		}
		catch
		{
			throw new Exception("TPDU: Error parsing variable part of CR/CC PDU.");
		}
	}

	public TPDUConnection HandleConnectRequest(List<byte[]> LocalTsaps, int maxTDPUSize, bool enableTsapCheck)
	{
		new ArrayList();
		TPDUConnection tPDUConnection = new TPDUConnection();
		Random random = new Random();
		MaxTDPUSize = maxTDPUSize;
		tPDUConnection.DstRef = SrcRef;
		tPDUConnection.SrcRef = Convert.ToUInt16(random.Next(1, 32766));
		tPDUConnection.ClassOption = 0;
		int num = Varpart.IndexOf((byte)194);
		if (num < 0)
		{
			throw new Exception("TPDU: Missing destination tsap in connect request.");
		}
		if (enableTsapCheck)
		{
			bool flag = false;
			foreach (byte[] LocalTsap in LocalTsaps)
			{
				if (((VarParam)Varpart[num]).length == LocalTsap.Length)
				{
					for (int i = 0; i < LocalTsap.Length && ((VarParam)Varpart[num]).value[i] == LocalTsap[i]; i++)
					{
						if (i == LocalTsap.Length - 1)
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					break;
				}
			}
			if (!flag)
			{
				throw new Exception("TPDU: Destination tsap mismatch.");
			}
		}
		int num2 = Varpart.IndexOf((byte)193);
		if (num2 < 0)
		{
			throw new Exception("TPDU: Missing source tsap in connect request.");
		}
		VarParam varParam = default(VarParam);
		tPDUConnection.Varpart = new ArrayList();
		varParam.code = 193;
		varParam.length = ((VarParam)Varpart[num2]).length;
		varParam.value = new byte[varParam.length];
		Array.Copy(((VarParam)Varpart[num2]).value, varParam.value, varParam.length);
		tPDUConnection.Varpart.Add(varParam);
		varParam.code = 194;
		varParam.length = ((VarParam)Varpart[num]).length;
		varParam.value = new byte[varParam.length];
		Array.Copy(((VarParam)Varpart[num]).value, varParam.value, varParam.length);
		tPDUConnection.Varpart.Add(varParam);
		int num3 = Varpart.IndexOf((byte)192);
		if (num3 >= 0 && ((VarParam)Varpart[num3]).value[0] < MaxTDPUSize)
		{
			MaxTDPUSize = ((VarParam)Varpart[num3]).value[0];
		}
		varParam.code = 192;
		varParam.length = 1;
		varParam.value = new byte[1];
		varParam.value[0] = Convert.ToByte(MaxTDPUSize);
		tPDUConnection.Varpart.Add(varParam);
		return tPDUConnection;
	}

	public byte[] GetBytes()
	{
		int num = 5;
		foreach (VarParam item in Varpart)
		{
			num += 2 + item.length;
		}
		byte[] array = new byte[num];
		if (BitConverter.IsLittleEndian)
		{
			Buffer.BlockCopy(BitConverter.GetBytes(ByteConvert.DoReverseEndian(DstRef)), 0, array, 0, 2);
			Buffer.BlockCopy(BitConverter.GetBytes(ByteConvert.DoReverseEndian(SrcRef)), 0, array, 2, 2);
		}
		else
		{
			Buffer.BlockCopy(BitConverter.GetBytes(DstRef), 0, array, 0, 2);
			Buffer.BlockCopy(BitConverter.GetBytes(SrcRef), 0, array, 2, 2);
		}
		array[4] = (byte)ClassOption;
		int num2 = 5;
		foreach (VarParam item2 in Varpart)
		{
			array[num2++] = item2.code;
			array[num2++] = item2.length;
			Buffer.BlockCopy(item2.value, 0, array, num2, item2.length);
			num2 += item2.length;
		}
		return array;
	}

	public int GetMaxTDPSize()
	{
		return MaxTDPUSize;
	}

	public string GetStringDump()
	{
		string text = "Source reference      : " + SrcRef + Environment.NewLine;
		text = text + "Destination reference : " + DstRef + Environment.NewLine;
		text = text + "Class number          : " + ClassOption + Environment.NewLine;
		text = text + "---------------------" + Environment.NewLine;
		foreach (VarParam item in Varpart)
		{
			string[] obj = new string[7] { text, "Var. part code   : ", null, null, null, null, null };
			byte code = item.code;
			obj[2] = code.ToString();
			obj[3] = " <";
			VP code2 = (VP)item.code;
			obj[4] = code2.ToString();
			obj[5] = ">";
			obj[6] = Environment.NewLine;
			text = string.Concat(obj);
			string text2 = text;
			code = item.length;
			text = text2 + "Var. part length : " + code + Environment.NewLine;
			text = text + "Var. part value  : " + Environment.NewLine;
			text = text + Utils.HexDump(item.value, item.length) + Environment.NewLine;
			text = text + "---------------------" + Environment.NewLine;
		}
		return text;
	}
}
