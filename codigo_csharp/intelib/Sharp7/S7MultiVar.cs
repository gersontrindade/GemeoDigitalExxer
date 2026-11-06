using System;
using System.Runtime.InteropServices;

namespace Sharp7;

public class S7MultiVar
{
	private S7Client FClient;

	private GCHandle[] Handles = new GCHandle[S7Client.MaxVars];

	private int Count;

	private S7Client.S7DataItem[] Items = new S7Client.S7DataItem[S7Client.MaxVars];

	public int[] Results = new int[S7Client.MaxVars];

	private bool AdjustWordLength(int Area, ref int WordLen, ref int Amount, ref int Start)
	{
		int num = S7.DataSizeByte(WordLen);
		if (num == 0)
		{
			return false;
		}
		if (Area == 28)
		{
			WordLen = 28;
		}
		if (Area == 29)
		{
			WordLen = 29;
		}
		if (WordLen == 1)
		{
			Amount = 1;
		}
		else if (WordLen != 28 && WordLen != 29)
		{
			Amount *= num;
			Start *= 8;
			WordLen = 2;
		}
		return true;
	}

	public S7MultiVar(S7Client Client)
	{
		FClient = Client;
		for (int i = 0; i < S7Client.MaxVars; i++)
		{
			Results[i] = 12582912;
		}
	}

	~S7MultiVar()
	{
		Clear();
	}

	public bool Add<T>(S7Consts.S7Tag Tag, ref T[] Buffer, int Offset)
	{
		return Add(Tag.Area, Tag.WordLen, Tag.DBNumber, Tag.Start, Tag.Elements, ref Buffer, Offset);
	}

	public bool Add<T>(S7Consts.S7Tag Tag, ref T[] Buffer)
	{
		return Add(Tag.Area, Tag.WordLen, Tag.DBNumber, Tag.Start, Tag.Elements, ref Buffer);
	}

	public bool Add<T>(int Area, int WordLen, int DBNumber, int Start, int Amount, ref T[] Buffer)
	{
		return Add(Area, WordLen, DBNumber, Start, Amount, ref Buffer, 0);
	}

	public bool Add<T>(int Area, int WordLen, int DBNumber, int Start, int Amount, ref T[] Buffer, int Offset)
	{
		if (Count < S7Client.MaxVars)
		{
			if (AdjustWordLength(Area, ref WordLen, ref Amount, ref Start))
			{
				Items[Count].Area = Area;
				Items[Count].WordLen = WordLen;
				Items[Count].Result = 12582912;
				Items[Count].DBNumber = DBNumber;
				Items[Count].Start = Start;
				Items[Count].Amount = Amount;
				GCHandle gCHandle = GCHandle.Alloc(Buffer, GCHandleType.Pinned);
				if (IntPtr.Size == 4)
				{
					Items[Count].pData = (IntPtr)(gCHandle.AddrOfPinnedObject().ToInt32() + Offset * Marshal.SizeOf(typeof(T)));
				}
				else
				{
					Items[Count].pData = (IntPtr)(gCHandle.AddrOfPinnedObject().ToInt64() + Offset * Marshal.SizeOf(typeof(T)));
				}
				Handles[Count] = gCHandle;
				Count++;
				return true;
			}
			return false;
		}
		return false;
	}

	public int Read()
	{
		int result = 36700160;
		try
		{
			if (Count > 0)
			{
				int num = FClient.ReadMultiVars(Items, Count);
				if (num == 0)
				{
					for (int i = 0; i < S7Client.MaxVars; i++)
					{
						Results[i] = Items[i].Result;
					}
				}
				result = num;
			}
			else
			{
				result = 36700160;
			}
		}
		finally
		{
			Clear();
		}
		return result;
	}

	public int Write()
	{
		int result = 36700160;
		try
		{
			if (Count > 0)
			{
				int num = FClient.WriteMultiVars(Items, Count);
				if (num == 0)
				{
					for (int i = 0; i < S7Client.MaxVars; i++)
					{
						Results[i] = Items[i].Result;
					}
				}
				result = num;
			}
			else
			{
				result = 36700160;
			}
		}
		finally
		{
			Clear();
		}
		return result;
	}

	public void Clear()
	{
		for (int i = 0; i < Count; i++)
		{
			_ = ref Handles[i];
			Handles[i].Free();
		}
		Count = 0;
	}
}
