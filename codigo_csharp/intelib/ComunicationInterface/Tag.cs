using System.Collections;

namespace ComunicationInterface;

public class Tag
{
	private ushort _valor;

	public TipoDado TipoDado { get; set; }

	public uint Endereco { get; set; }

	public ushort Valor
	{
		get
		{
			int[] array = new int[1];
			Bits.CopyTo(array, 0);
			if (_valor != (ushort)array[0])
			{
				_valor = (ushort)array[0];
			}
			return _valor;
		}
		set
		{
			_valor = value;
			Bits = new BitArray(new int[1] { value });
		}
	}

	public BitArray Bits { get; set; }

	public int Offset { get; set; }

	public int DB { get; set; }

	public int OffsetDB { get; set; }

	public Tag(TipoDado tipoDado, uint endereco, ushort valor, int offset = 0, int db = 0, int offsetDB = 0)
	{
		TipoDado = tipoDado;
		Endereco = endereco;
		Valor = valor;
		Offset = offset;
		DB = db;
		OffsetDB = offsetDB;
	}
}
