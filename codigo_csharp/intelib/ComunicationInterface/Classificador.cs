using System.Collections.Generic;
using System.Linq;

namespace ComunicationInterface;

public class Classificador
{
	private class Enderecamento
	{
		public int Endereco { get; set; }

		public int Bit { get; set; }

		public int Offset { get; set; }

		public int DB { get; set; }

		public int OffsetDB { get; set; }
	}

	private readonly Enderecamento enderecoCapacitivoEntrada = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEixoXRecuadoInicio = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEixoXAvancadoInicio = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEixoYRecuadoInicio = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEixoYAvancadoInicio = new Enderecamento
	{
		Endereco = 0,
		Bit = 4,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEixoZRecuadoInicio = new Enderecamento
	{
		Endereco = 0,
		Bit = 5,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEixoZAvancadoInicio = new Enderecamento
	{
		Endereco = 0,
		Bit = 6,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoIndutivoEsteira = new Enderecamento
	{
		Endereco = 0,
		Bit = 7,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoReflexivoEsteira = new Enderecamento
	{
		Endereco = 0,
		Bit = 8,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEspelhoDescarte1 = new Enderecamento
	{
		Endereco = 0,
		Bit = 9,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEspelhoDescarte2 = new Enderecamento
	{
		Endereco = 0,
		Bit = 10,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoCapacitivoSaida = new Enderecamento
	{
		Endereco = 0,
		Bit = 11,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEixoXRecuadoFinal = new Enderecamento
	{
		Endereco = 0,
		Bit = 12,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEixoXAvancadoFinal = new Enderecamento
	{
		Endereco = 0,
		Bit = 13,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEixoYRecuadoFinal = new Enderecamento
	{
		Endereco = 0,
		Bit = 14,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEixoYAvancadoFinal = new Enderecamento
	{
		Endereco = 0,
		Bit = 15,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEixoZRecuadoFinal = new Enderecamento
	{
		Endereco = 1,
		Bit = 0,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEixoZAvancadoFinal = new Enderecamento
	{
		Endereco = 1,
		Bit = 1,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoBotaoInicio = new Enderecamento
	{
		Endereco = 1,
		Bit = 2,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoBotaoReset = new Enderecamento
	{
		Endereco = 1,
		Bit = 3,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoParadaGeral = new Enderecamento
	{
		Endereco = 1,
		Bit = 4,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMovimentaEixoXInicio = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 5,
		DB = 100,
		OffsetDB = 5
	};

	private readonly Enderecamento enderecoMovimentaEixoYInicio = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 5,
		DB = 100,
		OffsetDB = 5
	};

	private readonly Enderecamento enderecoMovimentaEixoZInicio = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 5,
		DB = 100,
		OffsetDB = 5
	};

	private readonly Enderecamento enderecoVentosaInicio = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 5,
		DB = 100,
		OffsetDB = 5
	};

	private readonly Enderecamento enderecoEsteiraAvancar = new Enderecamento
	{
		Endereco = 0,
		Bit = 4,
		Offset = 5,
		DB = 100,
		OffsetDB = 5
	};

	private readonly Enderecamento enderecoEsteiraRecuar = new Enderecamento
	{
		Endereco = 0,
		Bit = 5,
		Offset = 5,
		DB = 100,
		OffsetDB = 5
	};

	private readonly Enderecamento enderecoCilindroDescarte1 = new Enderecamento
	{
		Endereco = 0,
		Bit = 6,
		Offset = 5,
		DB = 100,
		OffsetDB = 5
	};

	private readonly Enderecamento enderecoCilindroDescarte2 = new Enderecamento
	{
		Endereco = 0,
		Bit = 7,
		Offset = 5,
		DB = 100,
		OffsetDB = 5
	};

	private readonly Enderecamento enderecoMovimentaEixoXFinal = new Enderecamento
	{
		Endereco = 0,
		Bit = 8,
		Offset = 5,
		DB = 100,
		OffsetDB = 5
	};

	private readonly Enderecamento enderecoMovimentaEixoYFinal = new Enderecamento
	{
		Endereco = 0,
		Bit = 9,
		Offset = 5,
		DB = 100,
		OffsetDB = 5
	};

	private readonly Enderecamento enderecoMovimentaEixoZFinal = new Enderecamento
	{
		Endereco = 0,
		Bit = 10,
		Offset = 5,
		DB = 100,
		OffsetDB = 5
	};

	private readonly Enderecamento enderecoVentosaFinal = new Enderecamento
	{
		Endereco = 0,
		Bit = 11,
		Offset = 5,
		DB = 100,
		OffsetDB = 5
	};

	private readonly Enderecamento enderecoEsteiraVelocidade = new Enderecamento
	{
		Endereco = 2,
		Bit = 0,
		Offset = 5,
		DB = 100,
		OffsetDB = 5
	};

	public ICommunication comm { get; private set; }

	public bool CapacitivoEntrada
	{
		get
		{
			return LerBit(enderecoCapacitivoEntrada, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoCapacitivoEntrada, digital: true, value);
		}
	}

	public bool EixoXRecuadoInicio
	{
		get
		{
			return LerBit(enderecoEixoXRecuadoInicio, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEixoXRecuadoInicio, digital: true, value);
		}
	}

	public bool EixoXAvancadoInicio
	{
		get
		{
			return LerBit(enderecoEixoXAvancadoInicio, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEixoXAvancadoInicio, digital: true, value);
		}
	}

	public bool EixoYRecuadoInicio
	{
		get
		{
			return LerBit(enderecoEixoYRecuadoInicio, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEixoYRecuadoInicio, digital: true, value);
		}
	}

	public bool EixoYAvancadoInicio
	{
		get
		{
			return LerBit(enderecoEixoYAvancadoInicio, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEixoYAvancadoInicio, digital: true, value);
		}
	}

	public bool EixoZRecuadoInicio
	{
		get
		{
			return LerBit(enderecoEixoZRecuadoInicio, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEixoZRecuadoInicio, digital: true, value);
		}
	}

	public bool EixoZAvancadoInicio
	{
		get
		{
			return LerBit(enderecoEixoZAvancadoInicio, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEixoZAvancadoInicio, digital: true, value);
		}
	}

	public bool IndutivoEsteira
	{
		get
		{
			return LerBit(enderecoIndutivoEsteira, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoIndutivoEsteira, digital: true, value);
		}
	}

	public bool ReflexivoEsteira
	{
		get
		{
			return LerBit(enderecoReflexivoEsteira, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoReflexivoEsteira, digital: true, value);
		}
	}

	public bool EspelhoDescarte1
	{
		get
		{
			return LerBit(enderecoEspelhoDescarte1, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEspelhoDescarte1, digital: true, value);
		}
	}

	public bool EspelhoDescarte2
	{
		get
		{
			return LerBit(enderecoEspelhoDescarte2, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEspelhoDescarte2, digital: true, value);
		}
	}

	public bool CapacitivoSaida
	{
		get
		{
			return LerBit(enderecoCapacitivoSaida, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoCapacitivoSaida, digital: true, value);
		}
	}

	public bool EixoXRecuadoFinal
	{
		get
		{
			return LerBit(enderecoEixoXRecuadoFinal, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEixoXRecuadoFinal, digital: true, value);
		}
	}

	public bool EixoXAvancadoFinal
	{
		get
		{
			return LerBit(enderecoEixoXAvancadoFinal, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEixoXAvancadoFinal, digital: true, value);
		}
	}

	public bool EixoYRecuadoFinal
	{
		get
		{
			return LerBit(enderecoEixoYRecuadoFinal, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEixoYRecuadoFinal, digital: true, value);
		}
	}

	public bool EixoYAvancadoFinal
	{
		get
		{
			return LerBit(enderecoEixoYAvancadoFinal, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEixoYAvancadoFinal, digital: true, value);
		}
	}

	public bool EixoZRecuadoFinal
	{
		get
		{
			return LerBit(enderecoEixoZRecuadoFinal, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEixoZRecuadoFinal, digital: true, value);
		}
	}

	public bool EixoZAvancadoFinal
	{
		get
		{
			return LerBit(enderecoEixoZAvancadoFinal, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEixoZAvancadoFinal, digital: true, value);
		}
	}

	public bool BotaoInicio
	{
		get
		{
			return LerBit(enderecoBotaoInicio, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoBotaoInicio, digital: true, value);
		}
	}

	public bool BotaoReset
	{
		get
		{
			return LerBit(enderecoBotaoReset, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoBotaoReset, digital: true, value);
		}
	}

	public bool ParadaGeral
	{
		get
		{
			return LerBit(enderecoParadaGeral, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoParadaGeral, digital: true, value);
		}
	}

	public bool MovimentaEixoXInicio => LerBit(enderecoMovimentaEixoXInicio, entrada: false);

	public bool MovimentaEixoYInicio => LerBit(enderecoMovimentaEixoYInicio, entrada: false);

	public bool MovimentaEixoZInicio => LerBit(enderecoMovimentaEixoZInicio, entrada: false);

	public bool VentosaInicio => LerBit(enderecoVentosaInicio, entrada: false);

	public bool EsteiraAvancar => LerBit(enderecoEsteiraAvancar, entrada: false);

	public bool EsteiraRecuar => LerBit(enderecoEsteiraRecuar, entrada: false);

	public bool CilindroDescarte1 => LerBit(enderecoCilindroDescarte1, entrada: false);

	public bool CilindroDescarte2 => LerBit(enderecoCilindroDescarte2, entrada: false);

	public bool MovimentaEixoXFinal => LerBit(enderecoMovimentaEixoXFinal, entrada: false);

	public bool MovimentaEixoYFinal => LerBit(enderecoMovimentaEixoYFinal, entrada: false);

	public bool MovimentaEixoZFinal => LerBit(enderecoMovimentaEixoZFinal, entrada: false);

	public bool VentosaFinal => LerBit(enderecoVentosaFinal, entrada: false);

	public ushort EsteiraVelocidade => InverterBytes(LerTag(enderecoEsteiraVelocidade, entrada: false));

	public Classificador(ICommunication comm)
	{
		this.comm = comm;
		CriarListas();
	}

	private void CriarListas()
	{
		comm.TagsLidas = new List<Tag>();
		for (uint num = 0u; num < 3; num++)
		{
			comm.TagsLidas.Add(new Tag(TipoDado.Input, num, 0, 5, 100, 5));
		}
		comm.TagsEscrever = new List<Tag>();
		for (uint num2 = 0u; num2 < 2; num2++)
		{
			comm.TagsEscrever.Add(new Tag(TipoDado.Output, num2, 0, 0, 100));
		}
	}

	private void AtualizarTag(Enderecamento enderecamento, bool digital, object value)
	{
		comm.TagsEscrever.Select(delegate(Tag t)
		{
			if (t.Offset == enderecamento.Offset && t.Endereco == enderecamento.Endereco)
			{
				if (digital)
				{
					t.Bits.Set(enderecamento.Bit, (bool)value);
				}
				else
				{
					t.Valor = (ushort)value;
				}
			}
			return t;
		}).ToList();
	}

	private ushort LerTag(Enderecamento enderecamento, bool entrada)
	{
		if (!entrada)
		{
			return comm.TagsLidas.Where((Tag t) => t.Offset == enderecamento.Offset && t.Endereco == enderecamento.Endereco)?.First().Valor ?? 0;
		}
		return comm.TagsEscrever.Where((Tag t) => t.Offset == enderecamento.Offset && t.Endereco == enderecamento.Endereco)?.First().Valor ?? 0;
	}

	private bool LerBit(Enderecamento enderecamento, bool entrada)
	{
		if (!entrada)
		{
			return comm.TagsLidas.Where((Tag t) => t.Offset == enderecamento.Offset && t.Endereco == enderecamento.Endereco)?.First().Bits.Get(enderecamento.Bit) ?? false;
		}
		return comm.TagsEscrever.Where((Tag t) => t.Offset == enderecamento.Offset && t.Endereco == enderecamento.Endereco)?.First().Bits.Get(enderecamento.Bit) ?? false;
	}

	private ushort InverterBytes(ushort valor)
	{
		if (comm is SiemensCommunication || comm is SiemensShap7Comunication)
		{
			valor = (ushort)(((valor & 0xFF00) >>> 8) | ((valor & 0xFF) << 8));
		}
		return valor;
	}
}
