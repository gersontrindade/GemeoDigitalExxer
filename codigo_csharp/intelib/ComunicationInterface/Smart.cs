using System.Collections.Generic;
using System.Linq;

namespace ComunicationInterface;

public class Smart
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

	private readonly Enderecamento enderecoEstoqueGarraAvancada = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueGarraRecuada = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueGarraAberta = new Enderecamento
	{
		Endereco = 0,
		Bit = 4,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueGarraFechada = new Enderecamento
	{
		Endereco = 0,
		Bit = 5,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueAlimentadorAvancado = new Enderecamento
	{
		Endereco = 0,
		Bit = 6,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueAlimentadorBuffer = new Enderecamento
	{
		Endereco = 0,
		Bit = 7,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueAlimentadorRecuado = new Enderecamento
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

	private readonly Enderecamento enderecoEstoqueEsteiraLiberada = new Enderecamento
	{
		Endereco = 3,
		Bit = 1,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueOperacao = new Enderecamento
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
		Endereco = 8,
		Bit = 0,
		Offset = 0,
		DB = 100,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEstoqueAtuadorOcupado = new Enderecamento
	{
		Endereco = 8,
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

	private readonly Enderecamento enderecoEstoqueStatusBancada = new Enderecamento
	{
		Endereco = 2,
		Bit = 0,
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

	private readonly Enderecamento enderecoLaminadorScannerOk = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorSegurancaOk = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorAlimentadorAvancado = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorAlimentadorBuffer = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorAlimentadorRecuado = new Enderecamento
	{
		Endereco = 0,
		Bit = 4,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorAlimentadorForno = new Enderecamento
	{
		Endereco = 0,
		Bit = 5,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorElevacaoAvancado = new Enderecamento
	{
		Endereco = 0,
		Bit = 6,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorExpulsador1Avancado = new Enderecamento
	{
		Endereco = 0,
		Bit = 7,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorExpulsador2Avancado = new Enderecamento
	{
		Endereco = 0,
		Bit = 8,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorExpulsador3Avancado = new Enderecamento
	{
		Endereco = 0,
		Bit = 9,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorExpulsador4Avancado = new Enderecamento
	{
		Endereco = 0,
		Bit = 10,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorExpulsador5Avancado = new Enderecamento
	{
		Endereco = 0,
		Bit = 11,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorExpulsador6Avancado = new Enderecamento
	{
		Endereco = 0,
		Bit = 12,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorSensorVacuo = new Enderecamento
	{
		Endereco = 0,
		Bit = 13,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorManoplaAutomatico = new Enderecamento
	{
		Endereco = 0,
		Bit = 14,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorBotaoVerde = new Enderecamento
	{
		Endereco = 0,
		Bit = 15,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorBotaoVermelho = new Enderecamento
	{
		Endereco = 1,
		Bit = 0,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorSensorTrava1 = new Enderecamento
	{
		Endereco = 1,
		Bit = 1,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorSensorTrava2 = new Enderecamento
	{
		Endereco = 1,
		Bit = 2,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorSensorTrava3 = new Enderecamento
	{
		Endereco = 1,
		Bit = 3,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorTemperaturaResistencia = new Enderecamento
	{
		Endereco = 2,
		Bit = 0,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorPecaEsteira = new Enderecamento
	{
		Endereco = 3,
		Bit = 0,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorEsteiraLiberada = new Enderecamento
	{
		Endereco = 3,
		Bit = 1,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorOperacao = new Enderecamento
	{
		Endereco = 4,
		Bit = 0,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorCorLamina1 = new Enderecamento
	{
		Endereco = 5,
		Bit = 0,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorCorLamina2 = new Enderecamento
	{
		Endereco = 6,
		Bit = 0,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorCorLamina3 = new Enderecamento
	{
		Endereco = 7,
		Bit = 0,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorAtuadorEmPosicao = new Enderecamento
	{
		Endereco = 8,
		Bit = 0,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorAtuadorOcupado = new Enderecamento
	{
		Endereco = 8,
		Bit = 1,
		Offset = 20,
		DB = 101,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoLaminadorLigaResistencia = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorRecuaAlimentador = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorAcionaVacuo = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorAvancaElevacao = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorAvancaExpulsador1 = new Enderecamento
	{
		Endereco = 0,
		Bit = 4,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorAvancaExpulsador2 = new Enderecamento
	{
		Endereco = 0,
		Bit = 5,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorAvancaExpulsador3 = new Enderecamento
	{
		Endereco = 0,
		Bit = 6,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorAvancaExpulsador4 = new Enderecamento
	{
		Endereco = 0,
		Bit = 7,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorAvancaExpulsador5 = new Enderecamento
	{
		Endereco = 0,
		Bit = 8,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorAvancaExpulsador6 = new Enderecamento
	{
		Endereco = 0,
		Bit = 9,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorEsteira1 = new Enderecamento
	{
		Endereco = 0,
		Bit = 10,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorEsteira2 = new Enderecamento
	{
		Endereco = 0,
		Bit = 11,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorMotorVibracao = new Enderecamento
	{
		Endereco = 0,
		Bit = 12,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorResistenciaVibracao = new Enderecamento
	{
		Endereco = 0,
		Bit = 13,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorVentiladorForno = new Enderecamento
	{
		Endereco = 0,
		Bit = 14,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorAvancaAlimentador = new Enderecamento
	{
		Endereco = 0,
		Bit = 15,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorStatusBancada = new Enderecamento
	{
		Endereco = 2,
		Bit = 0,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorDriveAtuador = new Enderecamento
	{
		Endereco = 3,
		Bit = 0,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoLaminadorPosicaoAtuador = new Enderecamento
	{
		Endereco = 4,
		Bit = 0,
		Offset = 30,
		DB = 101,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEmpilhadorScannerOk = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorSegurancaOk = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorAlimentadorAvancado = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorAlimentadorBuffer = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorAlimentadorRecuado = new Enderecamento
	{
		Endereco = 0,
		Bit = 4,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorElevadorRecuado = new Enderecamento
	{
		Endereco = 0,
		Bit = 5,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorElevadorPosicao1 = new Enderecamento
	{
		Endereco = 0,
		Bit = 6,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorElevadorPosicao2 = new Enderecamento
	{
		Endereco = 0,
		Bit = 7,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorElevadorAvancado = new Enderecamento
	{
		Endereco = 0,
		Bit = 8,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorGarraFechada = new Enderecamento
	{
		Endereco = 0,
		Bit = 9,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorGarraAberta = new Enderecamento
	{
		Endereco = 0,
		Bit = 10,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorPrensaRecuada = new Enderecamento
	{
		Endereco = 0,
		Bit = 11,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorExpulsaTampaAvancada = new Enderecamento
	{
		Endereco = 0,
		Bit = 12,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorExpulsaTampaRecuada = new Enderecamento
	{
		Endereco = 0,
		Bit = 13,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorPosicionaMesa = new Enderecamento
	{
		Endereco = 1,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorSensorVacuo = new Enderecamento
	{
		Endereco = 1,
		Bit = 1,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorSensorPressao = new Enderecamento
	{
		Endereco = 1,
		Bit = 2,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorManoplaAutomatico = new Enderecamento
	{
		Endereco = 1,
		Bit = 3,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorBotaoVerde = new Enderecamento
	{
		Endereco = 1,
		Bit = 4,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorBotaoVermelho = new Enderecamento
	{
		Endereco = 1,
		Bit = 5,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorPecaEsteira = new Enderecamento
	{
		Endereco = 3,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorEsteiraLiberada = new Enderecamento
	{
		Endereco = 3,
		Bit = 1,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorOperacao = new Enderecamento
	{
		Endereco = 4,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorAndarBloco = new Enderecamento
	{
		Endereco = 5,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorTotalBlocos = new Enderecamento
	{
		Endereco = 6,
		Bit = 0,
		Offset = 40,
		DB = 102,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoEmpilhadorGiraMesa = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 50,
		DB = 102,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEmpilhadorAvancaAlimentador = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 50,
		DB = 102,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEmpilhadorRecuaAlimentador = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 50,
		DB = 102,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEmpilhadorAcionaVacuo = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 50,
		DB = 102,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEmpilhadorAvancaElevador = new Enderecamento
	{
		Endereco = 0,
		Bit = 4,
		Offset = 50,
		DB = 102,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEmpilhadorRecuaElevador = new Enderecamento
	{
		Endereco = 0,
		Bit = 5,
		Offset = 50,
		DB = 102,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEmpilhadorFechaGarra = new Enderecamento
	{
		Endereco = 0,
		Bit = 6,
		Offset = 50,
		DB = 102,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEmpilhadorAvancaPrensa = new Enderecamento
	{
		Endereco = 0,
		Bit = 7,
		Offset = 50,
		DB = 102,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEmpilhadorAvancaExpulsadorTampas = new Enderecamento
	{
		Endereco = 0,
		Bit = 8,
		Offset = 50,
		DB = 102,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEmpilhadorEsteira1 = new Enderecamento
	{
		Endereco = 0,
		Bit = 9,
		Offset = 50,
		DB = 102,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEmpilhadorEsteira2 = new Enderecamento
	{
		Endereco = 1,
		Bit = 0,
		Offset = 50,
		DB = 102,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoEmpilhadorStatusBancada = new Enderecamento
	{
		Endereco = 2,
		Bit = 0,
		Offset = 50,
		DB = 102,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoQualidadeScannerOk = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoQualidadeSegurancaOk = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoQualidadeAlimentadorAvancado = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoQualidadeAlimentadorBuffer = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoQualidadeAlimentadorRecuado = new Enderecamento
	{
		Endereco = 0,
		Bit = 4,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoQualidadeSensorVacuo = new Enderecamento
	{
		Endereco = 0,
		Bit = 5,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoQualidadeManoplaAutomatico = new Enderecamento
	{
		Endereco = 0,
		Bit = 6,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoQualidadeBotaoVerde = new Enderecamento
	{
		Endereco = 0,
		Bit = 7,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoQualidadeBotaoVermelho = new Enderecamento
	{
		Endereco = 0,
		Bit = 8,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoQualidadePecaEsteira = new Enderecamento
	{
		Endereco = 3,
		Bit = 0,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoQualidadeEsteiraLiberada = new Enderecamento
	{
		Endereco = 3,
		Bit = 1,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoQualidadeOperacao = new Enderecamento
	{
		Endereco = 4,
		Bit = 0,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoQualidadeInspecaoFalhou = new Enderecamento
	{
		Endereco = 8,
		Bit = 0,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoQualidadeInspecaoOk = new Enderecamento
	{
		Endereco = 8,
		Bit = 1,
		Offset = 60,
		DB = 103,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoQualidadeAvancaAlimentador = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 70,
		DB = 103,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoQualidadeRecuaAlimentador = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 70,
		DB = 103,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoQualidadeAcionaVacuo = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 70,
		DB = 103,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoQualidadeEsteira1 = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 70,
		DB = 103,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoQualidadeEsteira2 = new Enderecamento
	{
		Endereco = 0,
		Bit = 4,
		Offset = 70,
		DB = 103,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoQualidadeStatusBancada = new Enderecamento
	{
		Endereco = 2,
		Bit = 0,
		Offset = 70,
		DB = 103,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoQualidadeInspecionar = new Enderecamento
	{
		Endereco = 3,
		Bit = 0,
		Offset = 70,
		DB = 103,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoExpedicaoScannerOk = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 80,
		DB = 104,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoSegurancaOk = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 80,
		DB = 104,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoAlimentadorAvancado = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 80,
		DB = 104,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoAlimentadorRecuado = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 80,
		DB = 104,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoSensorVacuo = new Enderecamento
	{
		Endereco = 0,
		Bit = 4,
		Offset = 80,
		DB = 104,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoManoplaAutomatico = new Enderecamento
	{
		Endereco = 0,
		Bit = 5,
		Offset = 80,
		DB = 104,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoBotaoVerde = new Enderecamento
	{
		Endereco = 0,
		Bit = 6,
		Offset = 80,
		DB = 104,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoBotaoVermelho = new Enderecamento
	{
		Endereco = 0,
		Bit = 7,
		Offset = 80,
		DB = 104,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoPecaEsteira = new Enderecamento
	{
		Endereco = 3,
		Bit = 0,
		Offset = 80,
		DB = 104,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoEsteiraLiberada = new Enderecamento
	{
		Endereco = 3,
		Bit = 1,
		Offset = 80,
		DB = 104,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoOperacao = new Enderecamento
	{
		Endereco = 4,
		Bit = 0,
		Offset = 80,
		DB = 104,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoPosicaoGuardar = new Enderecamento
	{
		Endereco = 5,
		Bit = 0,
		Offset = 80,
		DB = 104,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoAtuadorEmPosicao = new Enderecamento
	{
		Endereco = 8,
		Bit = 0,
		Offset = 80,
		DB = 104,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoAtuadorOcupado = new Enderecamento
	{
		Endereco = 8,
		Bit = 1,
		Offset = 80,
		DB = 104,
		OffsetDB = 0
	};

	private readonly Enderecamento enderecoExpedicaoAvancaAlimentador = new Enderecamento
	{
		Endereco = 0,
		Bit = 0,
		Offset = 90,
		DB = 104,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoExpedicaoAcionaVacuo = new Enderecamento
	{
		Endereco = 0,
		Bit = 1,
		Offset = 90,
		DB = 104,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoExpedicaoEsteira1 = new Enderecamento
	{
		Endereco = 0,
		Bit = 2,
		Offset = 90,
		DB = 104,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoExpedicaoEsteira2 = new Enderecamento
	{
		Endereco = 0,
		Bit = 3,
		Offset = 90,
		DB = 104,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoExpedicaoStatusBancada = new Enderecamento
	{
		Endereco = 2,
		Bit = 0,
		Offset = 90,
		DB = 104,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoExpedicaoDriveAtuador = new Enderecamento
	{
		Endereco = 3,
		Bit = 0,
		Offset = 90,
		DB = 104,
		OffsetDB = 10
	};

	private readonly Enderecamento enderecoExpedicaoPosicaoAtuador = new Enderecamento
	{
		Endereco = 4,
		Bit = 0,
		Offset = 90,
		DB = 104,
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

	public bool EstoqueEsteiraLiberada
	{
		get
		{
			return LerBit(enderecoEstoqueEsteiraLiberada, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEstoqueEsteiraLiberada, digital: true, value);
		}
	}

	public ushort EstoqueOperacao
	{
		get
		{
			return InverterBytes(LerTag(enderecoEstoqueOperacao, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoEstoqueOperacao, digital: false, InverterBytes(value));
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

	public ushort EstoqueStatusBancada => InverterBytes(LerTag(enderecoEstoqueStatusBancada, entrada: false));

	public bool EstoqueDriveAtuador => LerBit(enderecoEstoqueDriveAtuador, entrada: false);

	public ushort EstoquePosicaoAtuador => InverterBytes(LerTag(enderecoEstoquePosicaoAtuador, entrada: false));

	public bool LaminadorScannerOk
	{
		get
		{
			return LerBit(enderecoLaminadorScannerOk, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorScannerOk, digital: true, value);
		}
	}

	public bool LaminadorSegurancaOk
	{
		get
		{
			return LerBit(enderecoLaminadorSegurancaOk, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorSegurancaOk, digital: true, value);
		}
	}

	public bool LaminadorAlimentadorAvancado
	{
		get
		{
			return LerBit(enderecoLaminadorAlimentadorAvancado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorAlimentadorAvancado, digital: true, value);
		}
	}

	public bool LaminadorAlimentadorBuffer
	{
		get
		{
			return LerBit(enderecoLaminadorAlimentadorBuffer, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorAlimentadorBuffer, digital: true, value);
		}
	}

	public bool LaminadorAlimentadorRecuado
	{
		get
		{
			return LerBit(enderecoLaminadorAlimentadorRecuado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorAlimentadorRecuado, digital: true, value);
		}
	}

	public bool LaminadorAlimentadorForno
	{
		get
		{
			return LerBit(enderecoLaminadorAlimentadorForno, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorAlimentadorForno, digital: true, value);
		}
	}

	public bool LaminadorElevacaoAvancado
	{
		get
		{
			return LerBit(enderecoLaminadorElevacaoAvancado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorElevacaoAvancado, digital: true, value);
		}
	}

	public bool LaminadorExpulsador1Avancado
	{
		get
		{
			return LerBit(enderecoLaminadorExpulsador1Avancado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorExpulsador1Avancado, digital: true, value);
		}
	}

	public bool LaminadorExpulsador2Avancado
	{
		get
		{
			return LerBit(enderecoLaminadorExpulsador2Avancado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorExpulsador2Avancado, digital: true, value);
		}
	}

	public bool LaminadorExpulsador3Avancado
	{
		get
		{
			return LerBit(enderecoLaminadorExpulsador3Avancado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorExpulsador3Avancado, digital: true, value);
		}
	}

	public bool LaminadorExpulsador4Avancado
	{
		get
		{
			return LerBit(enderecoLaminadorExpulsador4Avancado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorExpulsador4Avancado, digital: true, value);
		}
	}

	public bool LaminadorExpulsador5Avancado
	{
		get
		{
			return LerBit(enderecoLaminadorExpulsador5Avancado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorExpulsador5Avancado, digital: true, value);
		}
	}

	public bool LaminadorExpulsador6Avancado
	{
		get
		{
			return LerBit(enderecoLaminadorExpulsador6Avancado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorExpulsador6Avancado, digital: true, value);
		}
	}

	public bool LaminadorSensorVacuo
	{
		get
		{
			return LerBit(enderecoLaminadorSensorVacuo, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorSensorVacuo, digital: true, value);
		}
	}

	public bool LaminadorManoplaAutomatico
	{
		get
		{
			return LerBit(enderecoLaminadorManoplaAutomatico, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorManoplaAutomatico, digital: true, value);
		}
	}

	public bool LaminadorBotaoVerde
	{
		get
		{
			return LerBit(enderecoLaminadorBotaoVerde, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorBotaoVerde, digital: true, value);
		}
	}

	public bool LaminadorBotaoVermelho
	{
		get
		{
			return LerBit(enderecoLaminadorBotaoVermelho, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorBotaoVermelho, digital: true, value);
		}
	}

	public bool LaminadorSensorTrava1
	{
		get
		{
			return LerBit(enderecoLaminadorSensorTrava1, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorSensorTrava1, digital: true, value);
		}
	}

	public bool LaminadorSensorTrava2
	{
		get
		{
			return LerBit(enderecoLaminadorSensorTrava2, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorSensorTrava2, digital: true, value);
		}
	}

	public bool LaminadorSensorTrava3
	{
		get
		{
			return LerBit(enderecoLaminadorSensorTrava3, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorSensorTrava3, digital: true, value);
		}
	}

	public bool LaminadorPecaEsteira
	{
		get
		{
			return LerBit(enderecoLaminadorPecaEsteira, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorPecaEsteira, digital: true, value);
		}
	}

	public bool LaminadorEsteiraLiberada
	{
		get
		{
			return LerBit(enderecoLaminadorEsteiraLiberada, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorEsteiraLiberada, digital: true, value);
		}
	}

	public ushort LaminadorOperacao
	{
		get
		{
			return InverterBytes(LerTag(enderecoLaminadorOperacao, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoLaminadorOperacao, digital: false, InverterBytes(value));
		}
	}

	public ushort LaminadorCorLamina1
	{
		get
		{
			return InverterBytes(LerTag(enderecoLaminadorCorLamina1, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoLaminadorCorLamina1, digital: false, InverterBytes(value));
		}
	}

	public ushort LaminadorCorLamina2
	{
		get
		{
			return InverterBytes(LerTag(enderecoLaminadorCorLamina2, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoLaminadorCorLamina2, digital: false, InverterBytes(value));
		}
	}

	public ushort LaminadorCorLamina3
	{
		get
		{
			return InverterBytes(LerTag(enderecoLaminadorCorLamina3, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoLaminadorCorLamina3, digital: false, InverterBytes(value));
		}
	}

	public bool LaminadorAtuadorEmPosicao
	{
		get
		{
			return LerBit(enderecoLaminadorAtuadorEmPosicao, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorAtuadorEmPosicao, digital: true, value);
		}
	}

	public bool LaminadorAtuadorOcupado
	{
		get
		{
			return LerBit(enderecoLaminadorAtuadorOcupado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoLaminadorAtuadorOcupado, digital: true, value);
		}
	}

	public ushort LaminadorTemperaturaResistencia
	{
		get
		{
			return InverterBytes(LerTag(enderecoLaminadorTemperaturaResistencia, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoLaminadorTemperaturaResistencia, digital: false, InverterBytes(value));
		}
	}

	public bool LaminadorLigaResistencia => LerBit(enderecoLaminadorLigaResistencia, entrada: false);

	public bool LaminadorRecuaAlimentador => LerBit(enderecoLaminadorRecuaAlimentador, entrada: false);

	public bool LaminadorAcionaVacuo => LerBit(enderecoLaminadorAcionaVacuo, entrada: false);

	public bool LaminadorAvancaElevacao => LerBit(enderecoLaminadorAvancaElevacao, entrada: false);

	public bool LaminadorAvancaExpulsador1 => LerBit(enderecoLaminadorAvancaExpulsador1, entrada: false);

	public bool LaminadorAvancaExpulsador2 => LerBit(enderecoLaminadorAvancaExpulsador2, entrada: false);

	public bool LaminadorAvancaExpulsador3 => LerBit(enderecoLaminadorAvancaExpulsador3, entrada: false);

	public bool LaminadorAvancaExpulsador4 => LerBit(enderecoLaminadorAvancaExpulsador4, entrada: false);

	public bool LaminadorAvancaExpulsador5 => LerBit(enderecoLaminadorAvancaExpulsador5, entrada: false);

	public bool LaminadorAvancaExpulsador6 => LerBit(enderecoLaminadorAvancaExpulsador6, entrada: false);

	public bool LaminadorEsteira1 => LerBit(enderecoLaminadorEsteira1, entrada: false);

	public bool LaminadorEsteira2 => LerBit(enderecoLaminadorEsteira2, entrada: false);

	public bool LaminadorMotorVibracao => LerBit(enderecoLaminadorMotorVibracao, entrada: false);

	public bool LaminadorResistenciaVibracao => LerBit(enderecoLaminadorResistenciaVibracao, entrada: false);

	public bool LaminadorVentiladorForno => LerBit(enderecoLaminadorVentiladorForno, entrada: false);

	public bool LaminadorAvancaAlimentador => LerBit(enderecoLaminadorAvancaAlimentador, entrada: false);

	public ushort LaminadorStatusBancada => InverterBytes(LerTag(enderecoLaminadorStatusBancada, entrada: false));

	public bool LaminadorDriveAtuador => LerBit(enderecoLaminadorDriveAtuador, entrada: false);

	public ushort LaminadorPosicaoAtuador => InverterBytes(LerTag(enderecoLaminadorPosicaoAtuador, entrada: false));

	public bool EmpilhadorScannerOk
	{
		get
		{
			return LerBit(enderecoEmpilhadorScannerOk, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorScannerOk, digital: true, value);
		}
	}

	public bool EmpilhadorSegurancaOk
	{
		get
		{
			return LerBit(enderecoEmpilhadorSegurancaOk, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorSegurancaOk, digital: true, value);
		}
	}

	public bool EmpilhadorAlimentadorAvancado
	{
		get
		{
			return LerBit(enderecoEmpilhadorAlimentadorAvancado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorAlimentadorAvancado, digital: true, value);
		}
	}

	public bool EmpilhadorAlimentadorBuffer
	{
		get
		{
			return LerBit(enderecoEmpilhadorAlimentadorBuffer, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorAlimentadorBuffer, digital: true, value);
		}
	}

	public bool EmpilhadorAlimentadorRecuado
	{
		get
		{
			return LerBit(enderecoEmpilhadorAlimentadorRecuado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorAlimentadorRecuado, digital: true, value);
		}
	}

	public bool EmpilhadorElevadorRecuado
	{
		get
		{
			return LerBit(enderecoEmpilhadorElevadorRecuado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorElevadorRecuado, digital: true, value);
		}
	}

	public bool EmpilhadorElevadorPosicao1
	{
		get
		{
			return LerBit(enderecoEmpilhadorElevadorPosicao1, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorElevadorPosicao1, digital: true, value);
		}
	}

	public bool EmpilhadorElevadorPosicao2
	{
		get
		{
			return LerBit(enderecoEmpilhadorElevadorPosicao2, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorElevadorPosicao2, digital: true, value);
		}
	}

	public bool EmpilhadorElevadorAvancado
	{
		get
		{
			return LerBit(enderecoEmpilhadorElevadorAvancado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorElevadorAvancado, digital: true, value);
		}
	}

	public bool EmpilhadorGarraFechada
	{
		get
		{
			return LerBit(enderecoEmpilhadorGarraFechada, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorGarraFechada, digital: true, value);
		}
	}

	public bool EmpilhadorGarraAberta
	{
		get
		{
			return LerBit(enderecoEmpilhadorGarraAberta, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorGarraAberta, digital: true, value);
		}
	}

	public bool EmpilhadorPrensaRecuada
	{
		get
		{
			return LerBit(enderecoEmpilhadorPrensaRecuada, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorPrensaRecuada, digital: true, value);
		}
	}

	public bool EmpilhadorExpulsaTampaAvancada
	{
		get
		{
			return LerBit(enderecoEmpilhadorExpulsaTampaAvancada, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorExpulsaTampaAvancada, digital: true, value);
		}
	}

	public bool EmpilhadorExpulsaTampaRecuada
	{
		get
		{
			return LerBit(enderecoEmpilhadorExpulsaTampaRecuada, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorExpulsaTampaRecuada, digital: true, value);
		}
	}

	public bool EmpilhadorPosicionaMesa
	{
		get
		{
			return LerBit(enderecoEmpilhadorPosicionaMesa, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorPosicionaMesa, digital: true, value);
		}
	}

	public bool EmpilhadorSensorVacuo
	{
		get
		{
			return LerBit(enderecoEmpilhadorSensorVacuo, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorSensorVacuo, digital: true, value);
		}
	}

	public bool EmpilhadorSensorPressao
	{
		get
		{
			return LerBit(enderecoEmpilhadorSensorPressao, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorSensorPressao, digital: true, value);
		}
	}

	public bool EmpilhadorManoplaAutomatico
	{
		get
		{
			return LerBit(enderecoEmpilhadorManoplaAutomatico, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorManoplaAutomatico, digital: true, value);
		}
	}

	public bool EmpilhadorBotaoVerde
	{
		get
		{
			return LerBit(enderecoEmpilhadorBotaoVerde, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorBotaoVerde, digital: true, value);
		}
	}

	public bool EmpilhadorBotaoVermelho
	{
		get
		{
			return LerBit(enderecoEmpilhadorBotaoVermelho, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorBotaoVermelho, digital: true, value);
		}
	}

	public bool EmpilhadorPecaEsteira
	{
		get
		{
			return LerBit(enderecoEmpilhadorPecaEsteira, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorPecaEsteira, digital: true, value);
		}
	}

	public bool EmpilhadorEsteiraLiberada
	{
		get
		{
			return LerBit(enderecoEmpilhadorEsteiraLiberada, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorEsteiraLiberada, digital: true, value);
		}
	}

	public ushort EmpilhadorOperacao
	{
		get
		{
			return InverterBytes(LerTag(enderecoEmpilhadorOperacao, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorOperacao, digital: false, InverterBytes(value));
		}
	}

	public ushort EmpilhadorAndarBloco
	{
		get
		{
			return InverterBytes(LerTag(enderecoEmpilhadorAndarBloco, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorAndarBloco, digital: false, InverterBytes(value));
		}
	}

	public ushort EmpilhadorTotalBlocos
	{
		get
		{
			return InverterBytes(LerTag(enderecoEmpilhadorTotalBlocos, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoEmpilhadorTotalBlocos, digital: false, InverterBytes(value));
		}
	}

	public bool EmpilhadorGiraMesa => LerBit(enderecoEmpilhadorGiraMesa, entrada: false);

	public bool EmpilhadorAvancaAlimentador => LerBit(enderecoEmpilhadorAvancaAlimentador, entrada: false);

	public bool EmpilhadorRecuaAlimentador => LerBit(enderecoEmpilhadorRecuaAlimentador, entrada: false);

	public bool EmpilhadorAcionaVacuo => LerBit(enderecoEmpilhadorAcionaVacuo, entrada: false);

	public bool EmpilhadorAvancaElevador => LerBit(enderecoEmpilhadorAvancaElevador, entrada: false);

	public bool EmpilhadorRecuaElevador => LerBit(enderecoEmpilhadorRecuaElevador, entrada: false);

	public bool EmpilhadorFechaGarra => LerBit(enderecoEmpilhadorFechaGarra, entrada: false);

	public bool EmpilhadorAvancaPrensa => LerBit(enderecoEmpilhadorAvancaPrensa, entrada: false);

	public bool EmpilhadorAvancaExpulsadorTampas => LerBit(enderecoEmpilhadorAvancaExpulsadorTampas, entrada: false);

	public bool EmpilhadorEsteira1 => LerBit(enderecoEmpilhadorEsteira1, entrada: false);

	public bool EmpilhadorEsteira2 => LerBit(enderecoEmpilhadorEsteira2, entrada: false);

	public ushort EmpilhadorStatusBancada => InverterBytes(LerTag(enderecoEmpilhadorStatusBancada, entrada: false));

	public bool QualidadeScannerOk
	{
		get
		{
			return LerBit(enderecoQualidadeScannerOk, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoQualidadeScannerOk, digital: true, value);
		}
	}

	public bool QualidadeSegurancaOk
	{
		get
		{
			return LerBit(enderecoQualidadeSegurancaOk, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoQualidadeSegurancaOk, digital: true, value);
		}
	}

	public bool QualidadeAlimentadorAvancado
	{
		get
		{
			return LerBit(enderecoQualidadeAlimentadorAvancado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoQualidadeAlimentadorAvancado, digital: true, value);
		}
	}

	public bool QualidadeAlimentadorBuffer
	{
		get
		{
			return LerBit(enderecoQualidadeAlimentadorBuffer, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoQualidadeAlimentadorBuffer, digital: true, value);
		}
	}

	public bool QualidadeAlimentadorRecuado
	{
		get
		{
			return LerBit(enderecoQualidadeAlimentadorRecuado, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoQualidadeAlimentadorRecuado, digital: true, value);
		}
	}

	public bool QualidadeSensorVacuo
	{
		get
		{
			return LerBit(enderecoQualidadeSensorVacuo, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoQualidadeSensorVacuo, digital: true, value);
		}
	}

	public bool QualidadeManoplaAutomatico
	{
		get
		{
			return LerBit(enderecoQualidadeManoplaAutomatico, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoQualidadeManoplaAutomatico, digital: true, value);
		}
	}

	public bool QualidadeBotaoVerde
	{
		get
		{
			return LerBit(enderecoQualidadeBotaoVerde, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoQualidadeBotaoVerde, digital: true, value);
		}
	}

	public bool QualidadeBotaoVermelho
	{
		get
		{
			return LerBit(enderecoQualidadeBotaoVermelho, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoQualidadeBotaoVermelho, digital: true, value);
		}
	}

	public bool QualidadePecaEsteira
	{
		get
		{
			return LerBit(enderecoQualidadePecaEsteira, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoQualidadePecaEsteira, digital: true, value);
		}
	}

	public bool QualidadeEsteiraLiberada
	{
		get
		{
			return LerBit(enderecoQualidadeEsteiraLiberada, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoQualidadeEsteiraLiberada, digital: true, value);
		}
	}

	public ushort QualidadeOperacao
	{
		get
		{
			return InverterBytes(LerTag(enderecoQualidadeOperacao, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoQualidadeOperacao, digital: false, InverterBytes(value));
		}
	}

	public bool QualidadeInspecaoFalhou
	{
		get
		{
			return LerBit(enderecoQualidadeInspecaoFalhou, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoQualidadeInspecaoFalhou, digital: true, value);
		}
	}

	public bool QualidadeInspecaoOk
	{
		get
		{
			return LerBit(enderecoQualidadeInspecaoOk, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoQualidadeInspecaoOk, digital: true, value);
		}
	}

	public bool QualidadeAvancaAlimentador => LerBit(enderecoQualidadeAvancaAlimentador, entrada: false);

	public bool QualidadeRecuaAlimentador => LerBit(enderecoQualidadeRecuaAlimentador, entrada: false);

	public bool QualidadeAcionaVacuo => LerBit(enderecoQualidadeAcionaVacuo, entrada: false);

	public bool QualidadeEsteira1 => LerBit(enderecoQualidadeEsteira1, entrada: false);

	public bool QualidadeEsteira2 => LerBit(enderecoQualidadeEsteira2, entrada: false);

	public ushort QualidadeStatusBancada => InverterBytes(LerTag(enderecoQualidadeStatusBancada, entrada: false));

	public bool QualidadeInspecionar => LerBit(enderecoQualidadeInspecionar, entrada: false);

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

	public bool ExpedicaoEsteiraLiberada
	{
		get
		{
			return LerBit(enderecoExpedicaoEsteiraLiberada, entrada: true);
		}
		set
		{
			AtualizarTag(enderecoExpedicaoEsteiraLiberada, digital: true, value);
		}
	}

	public ushort ExpedicaoOperacao
	{
		get
		{
			return InverterBytes(LerTag(enderecoExpedicaoOperacao, entrada: true));
		}
		set
		{
			AtualizarTag(enderecoExpedicaoOperacao, digital: false, InverterBytes(value));
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

	public ushort ExpedicaoStatusBancada => InverterBytes(LerTag(enderecoExpedicaoStatusBancada, entrada: false));

	public bool ExpedicaoDriveAtuador => LerBit(enderecoExpedicaoDriveAtuador, entrada: false);

	public ushort ExpedicaoPosicaoAtuador => InverterBytes(LerTag(enderecoExpedicaoPosicaoAtuador, entrada: false));

	public Smart(ICommunication comm)
	{
		this.comm = comm;
		CriarListas();
		EstoqueScannerOk = true;
		LaminadorScannerOk = true;
		LaminadorSensorTrava1 = false;
		LaminadorSensorTrava2 = false;
		LaminadorSensorTrava3 = false;
		EmpilhadorScannerOk = true;
		QualidadeScannerOk = true;
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
		for (uint num3 = 0u; num3 < 3; num3++)
		{
			comm.TagsLidas.Add(new Tag(TipoDado.Input, num3, 0, 50, 102, 10));
		}
		for (uint num4 = 0u; num4 < 4; num4++)
		{
			comm.TagsLidas.Add(new Tag(TipoDado.Input, num4, 0, 70, 103, 10));
		}
		for (uint num5 = 0u; num5 < 5; num5++)
		{
			comm.TagsLidas.Add(new Tag(TipoDado.Input, num5, 0, 90, 104, 10));
		}
		comm.TagsEscrever = new List<Tag>();
		for (uint num6 = 0u; num6 < 10; num6++)
		{
			comm.TagsEscrever.Add(new Tag(TipoDado.Output, num6, 0, 0, 100));
		}
		for (uint num7 = 0u; num7 < 10; num7++)
		{
			comm.TagsEscrever.Add(new Tag(TipoDado.Output, num7, 0, 20, 101));
		}
		for (uint num8 = 0u; num8 < 10; num8++)
		{
			comm.TagsEscrever.Add(new Tag(TipoDado.Output, num8, 0, 40, 102));
		}
		for (uint num9 = 0u; num9 < 10; num9++)
		{
			comm.TagsEscrever.Add(new Tag(TipoDado.Output, num9, 0, 60, 103));
		}
		for (uint num10 = 0u; num10 < 10; num10++)
		{
			comm.TagsEscrever.Add(new Tag(TipoDado.Output, num10, 0, 80, 104));
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
