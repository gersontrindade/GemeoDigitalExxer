using System;
using System.Reflection;

namespace PROFINET_STEP_7.Types;

public static class Struct
{
	public static int GetStructSize(Type structType)
	{
		double num = 0.0;
		FieldInfo[] fields = structType.GetFields();
		for (int i = 0; i < fields.Length; i++)
		{
			switch (fields[i].FieldType.Name)
			{
			case "Boolean":
				num += 0.125;
				break;
			case "Byte":
				num = Math.Ceiling(num);
				num += 1.0;
				break;
			case "Int16":
			case "UInt16":
				num = Math.Ceiling(num);
				if (num / 2.0 - Math.Floor(num / 2.0) > 0.0)
				{
					num += 1.0;
				}
				num += 2.0;
				break;
			case "Int32":
			case "UInt32":
				num = Math.Ceiling(num);
				if (num / 2.0 - Math.Floor(num / 2.0) > 0.0)
				{
					num += 1.0;
				}
				num += 4.0;
				break;
			case "Float":
			case "Double":
				num = Math.Ceiling(num);
				if (num / 2.0 - Math.Floor(num / 2.0) > 0.0)
				{
					num += 1.0;
				}
				num += 4.0;
				break;
			}
		}
		return (int)num;
	}

	public static object FromBytes(Type structType, byte[] bytes)
	{
		if (bytes == null)
		{
			return null;
		}
		if (bytes.Length != GetStructSize(structType))
		{
			return null;
		}
		int num = 0;
		int num2 = 0;
		double num3 = 0.0;
		object obj = Activator.CreateInstance(structType);
		FieldInfo[] fields = obj.GetType().GetFields();
		foreach (FieldInfo fieldInfo in fields)
		{
			switch (fieldInfo.FieldType.Name)
			{
			case "Boolean":
				num = (int)Math.Floor(num3);
				num2 = (int)((num3 - (double)num) / 0.125);
				if ((bytes[num] & (int)Math.Pow(2.0, num2)) != 0)
				{
					fieldInfo.SetValue(obj, true);
				}
				else
				{
					fieldInfo.SetValue(obj, false);
				}
				num3 += 0.125;
				break;
			case "Byte":
				num3 = Math.Ceiling(num3);
				fieldInfo.SetValue(obj, bytes[(int)num3]);
				num3 += 1.0;
				break;
			case "Int16":
			case "UInt16":
				num3 = Math.Ceiling(num3);
				if (num3 / 2.0 - Math.Floor(num3 / 2.0) > 0.0)
				{
					num3 += 1.0;
				}
				fieldInfo.SetValue(obj, Word.FromBytes(bytes[(int)num3 + 1], bytes[(int)num3]));
				num3 += 2.0;
				break;
			case "Int32":
			case "UInt32":
				num3 = Math.Ceiling(num3);
				if (num3 / 2.0 - Math.Floor(num3 / 2.0) > 0.0)
				{
					num3 += 1.0;
				}
				fieldInfo.SetValue(obj, DWord.FromBytes(bytes[(int)num3], bytes[(int)num3 + 1], bytes[(int)num3 + 2], bytes[(int)num3 + 3]));
				num3 += 4.0;
				break;
			case "Double":
				num3 = Math.Ceiling(num3);
				if (num3 / 2.0 - Math.Floor(num3 / 2.0) > 0.0)
				{
					num3 += 1.0;
				}
				fieldInfo.SetValue(obj, Double.FromByteArray(new byte[4]
				{
					bytes[(int)num3],
					bytes[(int)num3 + 1],
					bytes[(int)num3 + 2],
					bytes[(int)num3 + 3]
				}));
				num3 += 4.0;
				break;
			}
		}
		return obj;
	}

	public static byte[] ToBytes(object structValue)
	{
		Type type = structValue.GetType();
		byte[] array = new byte[GetStructSize(type)];
		byte[] array2 = null;
		int num = 0;
		int num2 = 0;
		double num3 = 0.0;
		FieldInfo[] fields = type.GetFields();
		foreach (FieldInfo fieldInfo in fields)
		{
			array2 = null;
			switch (fieldInfo.FieldType.Name)
			{
			case "Boolean":
				num = (int)Math.Floor(num3);
				num2 = (int)((num3 - (double)num) / 0.125);
				if ((bool)fieldInfo.GetValue(structValue))
				{
					array[num] |= (byte)Math.Pow(2.0, num2);
				}
				else
				{
					array[num] &= (byte)(~(byte)Math.Pow(2.0, num2));
				}
				num3 += 0.125;
				break;
			case "Byte":
				num3 = (int)Math.Ceiling(num3);
				num = (int)num3;
				array[num] = (byte)fieldInfo.GetValue(structValue);
				num3 += 1.0;
				break;
			case "Int16":
				array2 = Int.ToByteArray((short)fieldInfo.GetValue(structValue));
				break;
			case "UInt16":
				array2 = Word.ToByteArray((ushort)fieldInfo.GetValue(structValue));
				break;
			case "Int32":
				array2 = DInt.ToByteArray((int)fieldInfo.GetValue(structValue));
				break;
			case "UInt32":
				array2 = DWord.ToByteArray((uint)fieldInfo.GetValue(structValue));
				break;
			case "Double":
				array2 = Double.ToByteArray((double)fieldInfo.GetValue(structValue));
				break;
			}
			if (array2 != null)
			{
				num3 = Math.Ceiling(num3);
				if (num3 / 2.0 - Math.Floor(num3 / 2.0) > 0.0)
				{
					num3 += 1.0;
				}
				num = (int)num3;
				for (int j = 0; j < array2.Length; j++)
				{
					array[num + j] = array2[j];
				}
				num3 += (double)array2.Length;
			}
		}
		return array;
	}
}
