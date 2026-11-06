using System.Collections.Generic;
using System.Linq;

namespace ComunicationInterface;

public class SmartDN
{
	private class Enderecamento
	{
		public int Endereco { get; set; }

		public int Bit { get; set; }

		public int Offset { get; set; }

		public int DB { get; set; }

		public int OffsetDB { get; set; }
	}

	private readonly Enderecamento enderecoEstoqueScannerOk = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueSegurancaOk = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueAlimentadorAvancado = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueAlimentadorBuffer = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueAlimentadorRecuado = new Enderecamento
	{
		Endereco = 0,
		Bit = 4,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueGarraAberta = new Enderecamento
	{
		Endereco = 0,
		Bit = 5,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueGarraFechada = new Enderecamento
	{
		Endereco = 0,
		Bit = 6,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueGarraAvancada = new Enderecamento
	{
		Endereco = 0,
		Bit = 7,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueGarraRecuada = new Enderecamento
	{
		Endereco = 0,
		Bit = 8,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueSensorVacuo = new Enderecamento
	{
		Endereco = 0,
		Bit = 9,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueManoplaAutomatico = new Enderecamento
	{
		Endereco = 0,
		Bit = 10,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueBotaoVerde = new Enderecamento
	{
		Endereco = 0,
		Bit = 11,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueBotaoVermelho = new Enderecamento
	{
		Endereco = 0,
		Bit = 12,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoquePecaEsteira = new Enderecamento
	{
		Endereco = 3,
		Bit = 0,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueNumeroPedido = new Enderecamento
	{
		Endereco = 4,
		Bit = 0,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoquePosicaoBloco = new Enderecamento
	{
		Endereco = 5,
		Bit = 0,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueAtuadorEmPosicao = new Enderecamento
	{
		Endereco = 6,
		Bit = 0,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueAtuadorOcupado = new Enderecamento
	{
		Endereco = 6,
		Bit = 1,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueAvancaAlimentador = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 10,
		DB = 100,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEstoqueRecuaAlimentador = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 10,
		DB = 100,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEstoqueAcionaVacuo = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 10,
		DB = 100,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEstoqueAvancaGarra = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 10,
		DB = 100,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEstoqueFechaGarra = new Enderecamento
	{
		Endereco = 0,
		Bit = 4,
		Offset = 10,
		DB = 100,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEstoqueEsteira1 = new Enderecamento
	{
		Endereco = 0,
		Bit = 5,
		Offset = 10,
		DB = 100,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEstoqueEsteira2 = new Enderecamento
	{
		Endereco = 0,
		Bit = 6,
		Offset = 10,
		DB = 100,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEstoqueValvula1Stopper = new Enderecamento
	{
		Endereco = 0,
		Bit = 7,
		Offset = 10,
		DB = 100,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEstoqueValvula2Stopper = new Enderecamento
	{
		Endereco = 0,
		Bit = 8,
		Offset = 10,
		DB = 100,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEstoqueDriveAtuador = new Enderecamento
	{
		Endereco = 3,
		Bit = 0,
		Offset = 10,
		DB = 100,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEstoquePosicaoAtuador = new Enderecamento
	{
		Endereco = 4,
		Bit = 0,
		Offset = 10,
		DB = 100,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoProcessoScannerOk = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoProcessoSegurancaOk = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoProcessoEixoZAvancado = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoProcessoEixoZRecuado = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoProcessoSensorVacuoManipulador = new Enderecamento
	{
		Endereco = 0,
		Bit = 4,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoProcessoSensorVacuoMesa = new Enderecamento
	{
		Endereco = 0,
		Bit = 5,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoProcessoManoplaAutomatico = new Enderecamento
	{
		Endereco = 0,
		Bit = 6,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoProcessoBotaoVerde = new Enderecamento
	{
		Endereco = 0,
		Bit = 7,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoProcessoBotaoVermelho = new Enderecamento
	{
		Endereco = 0,
		Bit = 8,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoProcessoPecaEsteira = new Enderecamento
	{
		Endereco = 3,
		Bit = 0,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoProcessoAtuadorEmPosicao = new Enderecamento
	{
		Endereco = 4,
		Bit = 0,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoProcessoAtuadorOcupado = new Enderecamento
	{
		Endereco = 4,
		Bit = 1,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoProcessoAvancaEixoZ = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoProcessoAcionaVacuoManipulador = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoProcessoAcionaVacuoMesa = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoProcessoEsteira1 = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoProcessoEsteira2 = new Enderecamento
	{
		Endereco = 0,
		Bit = 4,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoProcessoValvula1Stopper = new Enderecamento
	{
		Endereco = 0,
		Bit = 5,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoProcessoValvula2Stopper = new Enderecamento
	{
		Endereco = 0,
		Bit = 6,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoProcessoDriveAtuador = new Enderecamento
	{
		Endereco = 3,
		Bit = 0,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoProcessoPosicaoAtuador = new Enderecamento
	{
		Endereco = 4,
		Bit = 0,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoMontagemScannerOk = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemSegurancaOk = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemSensorVacuo = new Enderecamento
	{
		Endereco = 3,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemAlimentadorAvancado = new Enderecamento
	{
		Endereco = 3,
		Bit = 1,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemAlimentadorBuffer = new Enderecamento
	{
		Endereco = 3,
		Bit = 2,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemAlimentadorRecuado = new Enderecamento
	{
		Endereco = 3,
		Bit = 3,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemRampaAlimentadora = new Enderecamento
	{
		Endereco = 3,
		Bit = 4,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemManoplaAutomatico = new Enderecamento
	{
		Endereco = 3,
		Bit = 5,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemBotaoVerde = new Enderecamento
	{
		Endereco = 3,
		Bit = 6,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemBotaoVermelho = new Enderecamento
	{
		Endereco = 3,
		Bit = 7,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemPecaEsteira = new Enderecamento
	{
		Endereco = 3,
		Bit = 8,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemAndarBloco = new Enderecamento
	{
		Endereco = 4,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemTotalBlocos = new Enderecamento
	{
		Endereco = 5,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemNumeroPedido = new Enderecamento
	{
		Endereco = 6,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemCorLamina1 = new Enderecamento
	{
		Endereco = 7,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemCorLamina2 = new Enderecamento
	{
		Endereco = 8,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemCorLamina3 = new Enderecamento
	{
		Endereco = 9,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemPadraoLamina1 = new Enderecamento
	{
		Endereco = 10,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemPadraoLamina2 = new Enderecamento
	{
		Endereco = 11,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemPadraoLamina3 = new Enderecamento
	{
		Endereco = 12,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemRoboEmPosicao = new Enderecamento
	{
		Endereco = 13,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemRoboOcupado = new Enderecamento
	{
		Endereco = 13,
		Bit = 1,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemResultadoInspecao = new Enderecamento
	{
		Endereco = 14,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoMontagemAvancaAlimentador = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 55,
		DB = 102,
		OffsetDB = 15
	};

	private readonly Enderecamento enderecoMontagemRecuaAlimentador = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 55,
		DB = 102,
		OffsetDB = 15
	};

	private readonly Enderecamento enderecoMontagemAcionaVacuo = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 55,
		DB = 102,
		OffsetDB = 15
	};

	private readonly Enderecamento enderecoMontagemEsteira1 = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 55,
		DB = 102,
		OffsetDB = 15
	};

	private readonly Enderecamento enderecoMontagemEsteira2 = new Enderecamento
	{
		Endereco = 0,
		Bit = 4,
		Offset = 55,
		DB = 102,
		OffsetDB = 15
	};

	private readonly Enderecamento enderecoMontagemValvula1Stopper = new Enderecamento
	{
		Endereco = 0,
		Bit = 5,
		Offset = 55,
		DB = 102,
		OffsetDB = 15
	};

	private readonly Enderecamento enderecoMontagemValvula2Stopper = new Enderecamento
	{
		Endereco = 0,
		Bit = 6,
		Offset = 55,
		DB = 102,
		OffsetDB = 15
	};

	private readonly Enderecamento enderecoMontagemDriveRobo = new Enderecamento
	{
		Endereco = 1,
		Bit = 0,
		Offset = 55,
		DB = 102,
		OffsetDB = 15
	};

	private readonly Enderecamento enderecoMontagemPosicaoRobo = new Enderecamento
	{
		Endereco = 2,
		Bit = 0,
		Offset = 55,
		DB = 102,
		OffsetDB = 15
	};

	private readonly Enderecamento enderecoMontagemTriggerCamera = new Enderecamento
	{
		Endereco = 3,
		Bit = 0,
		Offset = 55,
		DB = 102,
		OffsetDB = 15
	};

	private readonly Enderecamento enderecoMontagemCorEsperada = new Enderecamento
	{
		Endereco = 4,
		Bit = 0,
		Offset = 55,
		DB = 102,
		OffsetDB = 15
	};

	private readonly Enderecamento enderecoExpedicaoScannerOk = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoSegurancaOk = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoAlimentadorAvancado = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoAlimentadorRecuado = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoSensorVacuo = new Enderecamento
	{
		Endereco = 0,
		Bit = 4,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoManoplaAutomatico = new Enderecamento
	{
		Endereco = 0,
		Bit = 5,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoBotaoVerde = new Enderecamento
	{
		Endereco = 0,
		Bit = 6,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoBotaoVermelho = new Enderecamento
	{
		Endereco = 0,
		Bit = 7,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoPecaEsteira = new Enderecamento
	{
		Endereco = 3,
		Bit = 0,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoPosicaoGuardar = new Enderecamento
	{
		Endereco = 4,
		Bit = 0,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoNumeroPedido = new Enderecamento
	{
		Endereco = 5,
		Bit = 0,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoAtuadorEmPosicao = new Enderecamento
	{
		Endereco = 6,
		Bit = 0,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoAtuadorOcupado = new Enderecamento
	{
		Endereco = 6,
		Bit = 1,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoAvancaAlimentador = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 70,
		DB = 103,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoExpedicaoAcionaVacuo = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 70,
		DB = 103,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoExpedicaoEsteira1 = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 70,
		DB = 103,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoExpedicaoEsteira2 = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 70,
		DB = 103,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoExpedicaoValvula1Stopper = new Enderecamento
	{
		Endereco = 0,
		Bit = 4,
		Offset = 70,
		DB = 103,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoExpedicaoValvula2Stopper = new Enderecamento
	{
		Endereco = 0,
		Bit = 5,
		Offset = 70,
		DB = 103,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoExpedicaoDriveAtuador = new Enderecamento
	{
		Endereco = 3,
		Bit = 0,
		Offset = 70,
		DB = 103,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoExpedicaoPosicaoAtuador = new Enderecamento
	{
		Endereco = 4,
		Bit = 0,
		Offset = 70,
		DB = 103,
		OffsetDB = 10
	};

	public ICommunication comm { get; private set; }

	public bool EstoqueScannerOk
	{
		get
		{
			return LerBit(enderecoEstoqueScannerOk, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEstoqueScannerOk, digital: true, value);
		}
	}

	public bool EstoqueSegurancaOk
	{
		get
		{
			return LerBit(enderecoEstoqueSegurancaOk, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEstoqueSegurancaOk, digital: true, value);
		}
	}

	public bool EstoqueAlimentadorAvancado
	{
		get
		{
			return LerBit(enderecoEstoqueAlimentadorAvancado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEstoqueAlimentadorAvancado, digital: true, value);
		}
	}

	public bool EstoqueAlimentadorBuffer
	{
		get
		{
			return LerBit(enderecoEstoqueAlimentadorBuffer, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEstoqueAlimentadorBuffer, digital: true, value);
		}
	}

	public bool EstoqueAlimentadorRecuado
	{
		get
		{
			return LerBit(enderecoEstoqueAlimentadorRecuado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEstoqueAlimentadorRecuado, digital: true, value);
		}
	}

	public bool EstoqueGarraAberta
	{
		get
		{
			return LerBit(enderecoEstoqueGarraAberta, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEstoqueGarraAberta, digital: true, value);
		}
	}

	public bool EstoqueGarraFechada
	{
		get
		{
			return LerBit(enderecoEstoqueGarraFechada, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEstoqueGarraFechada, digital: true, value);
		}
	}

	public bool EstoqueGarraAvancada
	{
		get
		{
			return LerBit(enderecoEstoqueGarraAvancada, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEstoqueGarraAvancada, digital: true, value);
		}
	}

	public bool EstoqueGarraRecuada
	{
		get
		{
			return LerBit(enderecoEstoqueGarraRecuada, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEstoqueGarraRecuada, digital: true, value);
		}
	}

	public bool EstoqueSensorVacuo
	{
		get
		{
			return LerBit(enderecoEstoqueSensorVacuo, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEstoqueSensorVacuo, digital: true, value);
		}
	}

	public bool EstoqueManoplaAutomatico
	{
		get
		{
			return LerBit(enderecoEstoqueManoplaAutomatico, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEstoqueManoplaAutomatico, digital: true, value);
		}
	}

	public bool EstoqueBotaoVerde
	{
		get
		{
			return LerBit(enderecoEstoqueBotaoVerde, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEstoqueBotaoVerde, digital: true, value);
		}
	}

	public bool EstoqueBotaoVermelho
	{
		get
		{
			return LerBit(enderecoEstoqueBotaoVermelho, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEstoqueBotaoVermelho, digital: true, value);
		}
	}

	public bool EstoquePecaEsteira
	{
		get
		{
			return LerBit(enderecoEstoquePecaEsteira, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEstoquePecaEsteira, digital: true, value);
		}
	}

	public ushort EstoqueNumeroPedido
	{
		get
		{
			return InverterBytes(LerTag(enderecoEstoqueNumeroPedido, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoEstoqueNumeroPedido, digital: false, InverterBytes(value));
		}
	}

	public ushort EstoquePosicaoBloco
	{
		get
		{
			return InverterBytes(LerTag(enderecoEstoquePosicaoBloco, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoEstoquePosicaoBloco, digital: false, InverterBytes(value));
		}
	}

	public bool EstoqueAtuadorEmPosicao
	{
		get
		{
			return LerBit(enderecoEstoqueAtuadorEmPosicao, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEstoqueAtuadorEmPosicao, digital: true, value);
		}
	}

	public bool EstoqueAtuadorOcupado
	{
		get
		{
			return LerBit(enderecoEstoqueAtuadorOcupado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEstoqueAtuadorOcupado, digital: true, value);
		}
	}

	public bool EstoqueAvancaAlimentador => LerBit(enderecoEstoqueAvancaAlimentador, entrada: false);

	public bool EstoqueRecuaAlimentador => LerBit(enderecoEstoqueRecuaAlimentador, entrada: false);

	public bool EstoqueAcionaVacuo => LerBit(enderecoEstoqueAcionaVacuo, entrada: false);

	public bool EstoqueAvancaGarra => LerBit(enderecoEstoqueAvancaGarra, entrada: false);

	public bool EstoqueFechaGarra => LerBit(enderecoEstoqueFechaGarra, entrada: false);

	public bool EstoqueEsteira1 => LerBit(enderecoEstoqueEsteira1, entrada: false);

	public bool EstoqueEsteira2 => LerBit(enderecoEstoqueEsteira2, entrada: false);

	public bool EstoqueValvula1Stopper => LerBit(enderecoEstoqueValvula1Stopper, entrada: false);

	public bool EstoqueValvula2Stopper => LerBit(enderecoEstoqueValvula2Stopper, entrada: false);

	public bool EstoqueDriveAtuador => LerBit(enderecoEstoqueDriveAtuador, entrada: false);

	public ushort EstoquePosicaoAtuador => InverterBytes(LerTag(enderecoEstoquePosicaoAtuador, entrada: false));

	public bool ProcessoScannerOk
	{
		get
		{
			return LerBit(enderecoProcessoScannerOk, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoProcessoScannerOk, digital: true, value);
		}
	}

	public bool ProcessoSegurancaOk
	{
		get
		{
			return LerBit(enderecoProcessoSegurancaOk, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoProcessoSegurancaOk, digital: true, value);
		}
	}

	public bool ProcessoEixoZAvancado
	{
		get
		{
			return LerBit(enderecoProcessoEixoZAvancado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoProcessoEixoZAvancado, digital: true, value);
		}
	}

	public bool ProcessoEixoZRecuado
	{
		get
		{
			return LerBit(enderecoProcessoEixoZRecuado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoProcessoEixoZRecuado, digital: true, value);
		}
	}

	public bool ProcessoSensorVacuoManipulador
	{
		get
		{
			return LerBit(enderecoProcessoSensorVacuoManipulador, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoProcessoSensorVacuoManipulador, digital: true, value);
		}
	}

	public bool ProcessoSensorVacuoMesa
	{
		get
		{
			return LerBit(enderecoProcessoSensorVacuoMesa, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoProcessoSensorVacuoMesa, digital: true, value);
		}
	}

	public bool ProcessoManoplaAutomatico
	{
		get
		{
			return LerBit(enderecoProcessoManoplaAutomatico, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoProcessoManoplaAutomatico, digital: true, value);
		}
	}

	public bool ProcessoBotaoVerde
	{
		get
		{
			return LerBit(enderecoProcessoBotaoVerde, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoProcessoBotaoVerde, digital: true, value);
		}
	}

	public bool ProcessoBotaoVermelho
	{
		get
		{
			return LerBit(enderecoProcessoBotaoVermelho, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoProcessoBotaoVermelho, digital: true, value);
		}
	}

	public bool ProcessoPecaEsteira
	{
		get
		{
			return LerBit(enderecoProcessoPecaEsteira, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoProcessoPecaEsteira, digital: true, value);
		}
	}

	public bool ProcessoAtuadorEmPosicao
	{
		get
		{
			return LerBit(enderecoProcessoAtuadorEmPosicao, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoProcessoAtuadorEmPosicao, digital: true, value);
		}
	}

	public bool ProcessoAtuadorOcupado
	{
		get
		{
			return LerBit(enderecoProcessoAtuadorOcupado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoProcessoAtuadorOcupado, digital: true, value);
		}
	}

	public bool ProcessoAvancaEixoZ => LerBit(enderecoProcessoAvancaEixoZ, entrada: false);

	public bool ProcessoAcionaVacuoManipulador => LerBit(enderecoProcessoAcionaVacuoManipulador, entrada: false);

	public bool ProcessoAcionaVacuoMesa => LerBit(enderecoProcessoAcionaVacuoMesa, entrada: false);

	public bool ProcessoEsteira1 => LerBit(enderecoProcessoEsteira1, entrada: false);

	public bool ProcessoEsteira2 => LerBit(enderecoProcessoEsteira2, entrada: false);

	public bool ProcessoValvula1Stopper => LerBit(enderecoProcessoValvula1Stopper, entrada: false);

	public bool ProcessoValvula2Stopper => LerBit(enderecoProcessoValvula2Stopper, entrada: false);

	public bool ProcessoDriveAtuador => LerBit(enderecoProcessoDriveAtuador, entrada: false);

	public ushort ProcessoPosicaoAtuador => InverterBytes(LerTag(enderecoProcessoPosicaoAtuador, entrada: false));

	public bool MontagemScannerOk
	{
		get
		{
			return LerBit(enderecoMontagemScannerOk, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoMontagemScannerOk, digital: true, value);
		}
	}

	public bool MontagemSegurancaOk
	{
		get
		{
			return LerBit(enderecoMontagemSegurancaOk, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoMontagemSegurancaOk, digital: true, value);
		}
	}

	public bool MontagemSensorVacuo
	{
		get
		{
			return LerBit(enderecoMontagemSensorVacuo, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoMontagemSensorVacuo, digital: true, value);
		}
	}

	public bool MontagemAlimentadorAvancado
	{
		get
		{
			return LerBit(enderecoMontagemAlimentadorAvancado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoMontagemAlimentadorAvancado, digital: true, value);
		}
	}

	public bool MontagemAlimentadorBuffer
	{
		get
		{
			return LerBit(enderecoMontagemAlimentadorBuffer, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoMontagemAlimentadorBuffer, digital: true, value);
		}
	}

	public bool MontagemAlimentadorRecuado
	{
		get
		{
			return LerBit(enderecoMontagemAlimentadorRecuado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoMontagemAlimentadorRecuado, digital: true, value);
		}
	}

	public bool MontagemRampaAlimentadora
	{
		get
		{
			return LerBit(enderecoMontagemRampaAlimentadora, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoMontagemRampaAlimentadora, digital: true, value);
		}
	}

	public bool MontagemManoplaAutomatico
	{
		get
		{
			return LerBit(enderecoMontagemManoplaAutomatico, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoMontagemManoplaAutomatico, digital: true, value);
		}
	}

	public bool MontagemBotaoVerde
	{
		get
		{
			return LerBit(enderecoMontagemBotaoVerde, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoMontagemBotaoVerde, digital: true, value);
		}
	}

	public bool MontagemBotaoVermelho
	{
		get
		{
			return LerBit(enderecoMontagemBotaoVermelho, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoMontagemBotaoVermelho, digital: true, value);
		}
	}

	public bool MontagemPecaEsteira
	{
		get
		{
			return LerBit(enderecoMontagemPecaEsteira, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoMontagemPecaEsteira, digital: true, value);
		}
	}

	public ushort MontagemAndarBloco
	{
		get
		{
			return InverterBytes(LerTag(enderecoMontagemAndarBloco, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoMontagemAndarBloco, digital: false, InverterBytes(value));
		}
	}

	public ushort MontagemTotalBlocos
	{
		get
		{
			return InverterBytes(LerTag(enderecoMontagemTotalBlocos, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoMontagemTotalBlocos, digital: false, InverterBytes(value));
		}
	}

	public ushort MontagemNumeroPedido
	{
		get
		{
			return InverterBytes(LerTag(enderecoMontagemNumeroPedido, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoMontagemNumeroPedido, digital: false, InverterBytes(value));
		}
	}

	public ushort MontagemCorLamina1
	{
		get
		{
			return InverterBytes(LerTag(enderecoMontagemCorLamina1, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoMontagemCorLamina1, digital: false, InverterBytes(value));
		}
	}

	public ushort MontagemCorLamina2
	{
		get
		{
			return InverterBytes(LerTag(enderecoMontagemCorLamina2, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoMontagemCorLamina2, digital: false, InverterBytes(value));
		}
	}

	public ushort MontagemCorLamina3
	{
		get
		{
			return InverterBytes(LerTag(enderecoMontagemCorLamina3, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoMontagemCorLamina3, digital: false, InverterBytes(value));
		}
	}

	public ushort MontagemPadraoLamina1
	{
		get
		{
			return InverterBytes(LerTag(enderecoMontagemPadraoLamina1, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoMontagemPadraoLamina1, digital: false, InverterBytes(value));
		}
	}

	public ushort MontagemPadraoLamina2
	{
		get
		{
			return InverterBytes(LerTag(enderecoMontagemPadraoLamina2, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoMontagemPadraoLamina2, digital: false, InverterBytes(value));
		}
	}

	public ushort MontagemPadraoLamina3
	{
		get
		{
			return InverterBytes(LerTag(enderecoMontagemPadraoLamina3, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoMontagemPadraoLamina3, digital: false, InverterBytes(value));
		}
	}

	public bool MontagemRoboEmPosicao
	{
		get
		{
			return LerBit(enderecoMontagemRoboEmPosicao, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoMontagemRoboEmPosicao, digital: true, value);
		}
	}

	public bool MontagemRoboOcupado
	{
		get
		{
			return LerBit(enderecoMontagemRoboOcupado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoMontagemRoboOcupado, digital: true, value);
		}
	}

	public bool MontagemResultadoInspecao
	{
		get
		{
			return LerBit(enderecoMontagemResultadoInspecao, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoMontagemResultadoInspecao, digital: true, value);
		}
	}

	public bool MontagemAvancaAlimentador => LerBit(enderecoMontagemAvancaAlimentador, entrada: false);

	public bool MontagemRecuaAlimentador => LerBit(enderecoMontagemRecuaAlimentador, entrada: false);

	public bool MontagemAcionaVacuo => LerBit(enderecoMontagemAcionaVacuo, entrada: false);

	public bool MontagemEsteira1 => LerBit(enderecoMontagemEsteira1, entrada: false);

	public bool MontagemEsteira2 => LerBit(enderecoMontagemEsteira2, entrada: false);

	public bool MontagemValvula1Stopper => LerBit(enderecoMontagemValvula1Stopper, entrada: false);

	public bool MontagemValvula2Stopper => LerBit(enderecoMontagemValvula2Stopper, entrada: false);

	public bool MontagemDriveRobo => LerBit(enderecoMontagemDriveRobo, entrada: false);

	public ushort MontagemPosicaoRobo => InverterBytes(LerTag(enderecoMontagemPosicaoRobo, entrada: false));

	public bool MontagemTriggerCamera => LerBit(enderecoMontagemTriggerCamera, entrada: false);

	public ushort MontagemCorEsperada => InverterBytes(LerTag(enderecoMontagemCorEsperada, entrada: false));

	public bool ExpedicaoScannerOk
	{
		get
		{
			return LerBit(enderecoExpedicaoScannerOk, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoExpedicaoScannerOk, digital: true, value);
		}
	}

	public bool ExpedicaoSegurancaOk
	{
		get
		{
			return LerBit(enderecoExpedicaoSegurancaOk, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoExpedicaoSegurancaOk, digital: true, value);
		}
	}

	public bool ExpedicaoAlimentadorAvancado
	{
		get
		{
			return LerBit(enderecoExpedicaoAlimentadorAvancado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoExpedicaoAlimentadorAvancado, digital: true, value);
		}
	}

	public bool ExpedicaoAlimentadorRecuado
	{
		get
		{
			return LerBit(enderecoExpedicaoAlimentadorRecuado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoExpedicaoAlimentadorRecuado, digital: true, value);
		}
	}

	public bool ExpedicaoSensorVacuo
	{
		get
		{
			return LerBit(enderecoExpedicaoSensorVacuo, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoExpedicaoSensorVacuo, digital: true, value);
		}
	}

	public bool ExpedicaoManoplaAutomatico
	{
		get
		{
			return LerBit(enderecoExpedicaoManoplaAutomatico, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoExpedicaoManoplaAutomatico, digital: true, value);
		}
	}

	public bool ExpedicaoBotaoVerde
	{
		get
		{
			return LerBit(enderecoExpedicaoBotaoVerde, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoExpedicaoBotaoVerde, digital: true, value);
		}
	}

	public bool ExpedicaoBotaoVermelho
	{
		get
		{
			return LerBit(enderecoExpedicaoBotaoVermelho, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoExpedicaoBotaoVermelho, digital: true, value);
		}
	}

	public bool ExpedicaoPecaEsteira
	{
		get
		{
			return LerBit(enderecoExpedicaoPecaEsteira, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoExpedicaoPecaEsteira, digital: true, value);
		}
	}

	public ushort ExpedicaoPosicaoGuardar
	{
		get
		{
			return InverterBytes(LerTag(enderecoExpedicaoPosicaoGuardar, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoExpedicaoPosicaoGuardar, digital: false, InverterBytes(value));
		}
	}

	public ushort ExpedicaoNumeroPedido
	{
		get
		{
			return InverterBytes(LerTag(enderecoExpedicaoNumeroPedido, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoExpedicaoNumeroPedido, digital: false, InverterBytes(value));
		}
	}

	public bool ExpedicaoAtuadorEmPosicao
	{
		get
		{
			return LerBit(enderecoExpedicaoAtuadorEmPosicao, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoExpedicaoAtuadorEmPosicao, digital: true, value);
		}
	}

	public bool ExpedicaoAtuadorOcupado
	{
		get
		{
			return LerBit(enderecoExpedicaoAtuadorOcupado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoExpedicaoAtuadorOcupado, digital: true, value);
		}
	}

	public bool ExpedicaoAvancaAlimentador => LerBit(enderecoExpedicaoAvancaAlimentador, entrada: false);

	public bool ExpedicaoAcionaVacuo => LerBit(enderecoExpedicaoAcionaVacuo, entrada: false);

	public bool ExpedicaoEsteira1 => LerBit(enderecoExpedicaoEsteira1, entrada: false);

	public bool ExpedicaoEsteira2 => LerBit(enderecoExpedicaoEsteira2, entrada: false);

	public bool ExpedicaoValvula1Stopper => LerBit(enderecoExpedicaoValvula1Stopper, entrada: false);

	public bool ExpedicaoValvula2Stopper => LerBit(enderecoExpedicaoValvula2Stopper, entrada: false);

	public bool ExpedicaoDriveAtuador => LerBit(enderecoExpedicaoDriveAtuador, entrada: false);

	public ushort ExpedicaoPosicaoAtuador => InverterBytes(LerTag(enderecoExpedicaoPosicaoAtuador, entrada: false));

	public SmartDN(ICommunication comm)
	{
		this.comm = comm;
		CriarListas();
		EstoqueScannerOk = true;
		ProcessoScannerOk = true;
		MontagemScannerOk = true;
		ExpedicaoScannerOk = true;
	}

	private void CriarListas()
	{
		comm.TagsLidas = new List<Tag>();
		for (uint num = 0u; num < 5; num++)
		{
			comm.TagsLidas.Add(new Tag(TipoDado.Input, num, 0, 10, 100, 10));
		}
		for (uint num2 = 0u; num2 < 5; num2++)
		{
			comm.TagsLidas.Add(new Tag(TipoDado.Input, num2, 0, 30, 101, 10));
		}
		for (uint num3 = 0u; num3 < 5; num3++)
		{
			comm.TagsLidas.Add(new Tag(TipoDado.Input, num3, 0, 55, 102, 15));
		}
		for (uint num4 = 0u; num4 < 5; num4++)
		{
			comm.TagsLidas.Add(new Tag(TipoDado.Input, num4, 0, 70, 103, 10));
		}
		comm.TagsEscrever = new List<Tag>();
		for (uint num5 = 0u; num5 < 10; num5++)
		{
			comm.TagsEscrever.Add(new Tag(TipoDado.Output, num5, 0, 0, 100));
		}
		for (uint num6 = 0u; num6 < 10; num6++)
		{
			comm.TagsEscrever.Add(new Tag(TipoDado.Output, num6, 0, 20, 101));
		}
		for (uint num7 = 0u; num7 < 15; num7++)
		{
			comm.TagsEscrever.Add(new Tag(TipoDado.Output, num7, 0, 40, 102));
		}
		for (uint num8 = 0u; num8 < 10; num8++)
		{
			comm.TagsEscrever.Add(new Tag(TipoDado.Output, num8, 0, 60, 103));
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
