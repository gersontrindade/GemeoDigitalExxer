using UnityEngine;
using UnityEngine.UI;

public class SCRIPT_TOGGLES : MonoBehaviour
{
	public bool OBJETO_ADMINISTRACAO;

	public GameObject PARENTE_BANCADAS_PROVISORIO;

	public GameObject PARENTE_BANCADAS_FINAL;

	public bool MINIMIZAR_FUNCAO = true;

	public GameObject ESTOQUE_TOGGLE;

	public GameObject ESTOQUE_INPUTS;

	public GameObject ESTOQUE_3D;

	public GameObject ESTOQUE_CHECKBOX_REMOVER_BLOCO;

	public static bool VAR_CHECKBOX_REMOVER_BLOCO = false;

	public static bool ESTOQUE_ATIVO = true;

	public bool BIT_EstoqueScannerOk;

	public bool BIT_EstoqueSegurancaOk;

	public bool BIT_EstoqueGarraAvancada;

	public bool BIT_EstoqueGarraRecuada;

	public bool BIT_EstoqueGarraAberta;

	public bool BIT_EstoqueGarraFechada;

	public bool BIT_EstoqueAlimentadorAvancado;

	public bool BIT_EstoqueAlimentadorBuffer;

	public bool BIT_EstoqueAlimentadorRecuado;

	public bool BIT_EstoqueSensorVacuo;

	public bool BIT_EstoqueManoplaAutomatico;

	public bool BIT_EstoqueBotaoVerde;

	public bool BIT_EstoqueBotaoVermelho;

	public bool BIT_EstoquePecaEsteira;

	public bool BIT_EstoqueEsteiraLiberada;

	public bool BIT_EstoqueOperacao;

	public bool BIT_EstoquePosicaoBloco;

	public bool BIT_EstoqueAtuadorEmPosicao;

	public bool BIT_EstoqueAtuadorOcupado;

	public bool BIT_EstoqueAvancaAlimentador;

	public bool BIT_EstoqueRecuaAlimentador;

	public bool BIT_EstoqueAcionaVacuo;

	public bool BIT_EstoqueAvancaGarra;

	public bool BIT_EstoqueFechaGarra;

	public bool BIT_EstoqueEsteira1;

	public bool BIT_EstoqueEsteira2;

	public bool BIT_EstoqueStatusBancada;

	public bool BIT_EstoqueDriveAtuador;

	public bool BIT_EstoquePosicaoAtuador;

	public GameObject OBJETO_TEXTO_BIT_EstoquePosicaoAtuador;

	public bool BIT_EstoqueStopper1;

	public bool BIT_EstoqueStopper2;

	public static bool ATIVAR_BIT_EstoqueScannerOk = false;

	public static bool ATIVAR_BIT_EstoqueSegurancaOk = false;

	public static bool ATIVAR_BIT_EstoqueGarraAvancada = false;

	public static bool ATIVAR_BIT_EstoqueGarraRecuada = false;

	public static bool ATIVAR_BIT_EstoqueGarraAberta = false;

	public static bool ATIVAR_BIT_EstoqueGarraFechada = false;

	public static bool ATIVAR_BIT_EstoqueAlimentadorAvancado = false;

	public static bool ATIVAR_BIT_EstoqueAlimentadorBuffer = false;

	public static bool ATIVAR_BIT_EstoqueAlimentadorRecuado = false;

	public static bool ATIVAR_BIT_EstoqueSensorVacuo = false;

	public static bool ATIVAR_BIT_EstoqueManoplaAutomatico = false;

	public static bool ATIVAR_BIT_EstoqueBotaoVerde = false;

	public static bool ATIVAR_BIT_EstoqueBotaoVermelho = false;

	public static bool ATIVAR_BIT_EstoquePecaEsteira = false;

	public static bool ATIVAR_BIT_EstoqueEsteiraLiberada = false;

	public static bool ATIVAR_BIT_EstoqueOperacao = false;

	public static bool ATIVAR_BIT_EstoquePosicaoBloco = false;

	public static string VALOR_BIT_EstoquePosicaoBloco = "0";

	public static bool ATIVAR_BIT_EstoqueAtuadorEmPosicao = false;

	public static bool ATIVAR_BIT_EstoqueAtuadorOcupado = false;

	public static bool ATIVAR_BIT_EstoqueAvancaAlimentador = false;

	public static bool ATIVAR_BIT_EstoqueRecuaAlimentador = false;

	public static bool ATIVAR_BIT_EstoqueAcionaVacuo = false;

	public static bool ATIVAR_BIT_EstoqueAvancaGarra = false;

	public static bool ATIVAR_BIT_EstoqueFechaGarra = false;

	public static bool ATIVAR_BIT_EstoqueEsteira1 = false;

	public static bool ATIVAR_BIT_EstoqueEsteira2 = false;

	public static bool ATIVAR_BIT_EstoqueStatusBancada = false;

	public static bool ATIVAR_BIT_EstoqueDriveAtuador = false;

	public static bool ATIVAR_BIT_EstoquePosicaoAtuador = false;

	public static string VALOR_BIT_EstoquePosicaoAtuador = "0";

	public static bool ATIVAR_BIT_EstoqueStopper1 = false;

	public static bool ATIVAR_BIT_EstoqueStopper2 = false;

	public GameObject PROCESSO_TOGGLE;

	public GameObject PROCESSO_INPUTS;

	public GameObject PROCESSO_3D;

	public static bool PROCESSO_ATIVO = true;

	public bool BIT_ProcessoScannerOk;

	public bool BIT_ProcessoSegurancaOk;

	public bool BIT_ProcessoAlimentadorAvancado;

	public bool BIT_ProcessoAlimentadorBuffer;

	public bool BIT_ProcessoAlimentadorRecuado;

	public bool BIT_ProcessoSensorVacuo;

	public bool BIT_ProcessoSensorVacuoMesa;

	public bool BIT_ProcessoManoplaAutomatico;

	public bool BIT_ProcessoBotaoVerde;

	public bool BIT_ProcessoBotaoVermelho;

	public bool BIT_ProcessoPecaEsteira;

	public bool BIT_ProcessoEsteiraLiberada;

	public bool BIT_ProcessoOperacao;

	public bool BIT_ProcessoPosicaoBloco;

	public bool BIT_ProcessoAtuadorEmPosicao;

	public bool BIT_ProcessoAtuadorOcupado;

	public bool BIT_ProcessoAvancaAlimentador;

	public bool BIT_ProcessoRecuaAlimentador;

	public bool BIT_ProcessoAcionaVacuoManipulador;

	public bool BIT_ProcessoAcionaVacuoMesa;

	public bool BIT_ProcessoEsteira1;

	public bool BIT_ProcessoEsteira2;

	public bool BIT_ProcessoStatusBancada;

	public bool BIT_ProcessoDriveAtuador;

	public bool BIT_ProcessoPosicaoAtuador;

	public GameObject OBJETO_TEXTO_BIT_ProcessoPosicaoAtuador;

	public static bool ATIVAR_BIT_ProcessoScannerOk = false;

	public static bool ATIVAR_BIT_ProcessoSegurancaOk = false;

	public static bool ATIVAR_BIT_ProcessoAlimentadorAvancado = false;

	public static bool ATIVAR_BIT_ProcessoAlimentadorBuffer = false;

	public static bool ATIVAR_BIT_ProcessoAlimentadorRecuado = false;

	public static bool ATIVAR_BIT_ProcessoSensorVacuo = false;

	public static bool ATIVAR_BIT_ProcessoSensorVacuoMesa = false;

	public static bool ATIVAR_BIT_ProcessoManoplaAutomatico = false;

	public static bool ATIVAR_BIT_ProcessoBotaoVerde = false;

	public static bool ATIVAR_BIT_ProcessoBotaoVermelho = false;

	public static bool ATIVAR_BIT_ProcessoPecaEsteira = false;

	public static bool ATIVAR_BIT_ProcessoEsteiraLiberada = false;

	public static bool ATIVAR_BIT_ProcessoOperacao = false;

	public static bool ATIVAR_BIT_ProcessoAtuadorEmPosicao = false;

	public static bool ATIVAR_BIT_ProcessoAtuadorOcupado = false;

	public static bool ATIVAR_BIT_ProcessoAvancaAlimentador = false;

	public static bool ATIVAR_BIT_ProcessoRecuaAlimentador = false;

	public static bool ATIVAR_BIT_ProcessoAcionaVacuoManipulador = false;

	public static bool ATIVAR_BIT_ProcessoAcionaVacuoMesa = false;

	public static bool ATIVAR_BIT_ProcessoEsteira1 = false;

	public static bool ATIVAR_BIT_ProcessoEsteira2 = false;

	public static bool ATIVAR_BIT_ProcessoStatusBancada = false;

	public static bool ATIVAR_BIT_ProcessoDriveAtuador = false;

	public static bool ATIVAR_BIT_ProcessoPosicaoAtuador = false;

	public static string VALOR_BIT_ProcessoPosicaoAtuador = "0";

	public GameObject QUALIDADE_TOGGLE;

	public GameObject QUALIDADE_INPUTS;

	public GameObject QUALIDADE_3D;

	public static bool QUALIDADE_ATIVO = true;

	public static bool QUALIDADE_EXECUTANDO_ANIMACAO = false;

	public static bool QUALIDADE_CLICOU_INSPECIONAR = false;

	public bool BIT_QualidadeScannerOk;

	public bool BIT_QualidadeSegurancaOk;

	public bool BIT_QualidadeAlimentadorAvancado;

	public bool BIT_QualidadeALimentadorBuffer;

	public bool BIT_QualidadeAlimentadorRecuado;

	public bool BIT_QualidadeSensorVacuo;

	public bool BIT_QualidadeManoplaAutomatico;

	public bool BIT_QualidadeBotaoVerde;

	public bool BIT_QualidadeBotaoVermelho;

	public bool BIT_QualidadePecaEsteira;

	public bool BIT_QualidadeEsteiraLiberada;

	public bool BIT_QualidadeOperacao;

	public bool BIT_QualidadeInspecaoFalhou;

	public bool BIT_QualidadeInspecaoOk;

	public bool BIT_QualidadeAvancaAlimentador;

	public bool BIT_QualidadeRecuaAlimentador;

	public bool BIT_QualidadeAcionaVacuo;

	public bool BIT_QualidadeEsteira1;

	public bool BIT_QualidadeEsteira2;

	public bool BIT_QualidadeStatusBancada;

	public bool BIT_QualidadeInspecionar;

	public bool BIT_QualidadeStopper1;

	public bool BIT_QualidadeStopper2;

	public bool BIT_MontagemDriveRobo;

	public bool BIT_MontagemPosicaoRobo;

	public bool BIT_MontagemCorEsperada;

	public GameObject OBJETO_TEXTO_BIT_MontagemPosicaoRobo;

	public GameObject OBJETO_TEXTO_BIT_MontagemCorEsperada;

	public bool BIT_MontagemRoboEmPosicao;

	public bool BIT_MontagemRoboOcupado;

	public bool BIT_MontagemTriggerCamera;

	public bool BIT_MontagemResultadoInspecao;

	public static bool ATIVAR_BIT_QualidadeScannerOk = false;

	public static bool ATIVAR_BIT_QualidadeSegurancaOk = false;

	public static bool ATIVAR_BIT_QualidadeAlimentadorAvancado = false;

	public static bool ATIVAR_BIT_QualidadeALimentadorBuffer = false;

	public static bool ATIVAR_BIT_QualidadeAlimentadorRecuado = false;

	public static bool ATIVAR_BIT_QualidadeSensorVacuo = false;

	public static bool ATIVAR_BIT_QualidadeManoplaAutomatico = false;

	public static bool ATIVAR_BIT_QualidadeBotaoVerde = false;

	public static bool ATIVAR_BIT_QualidadeBotaoVermelho = false;

	public static bool ATIVAR_BIT_QualidadePecaEsteira = false;

	public static bool ATIVAR_BIT_QualidadeEsteiraLiberada = false;

	public static bool ATIVAR_BIT_QualidadeOperacao = false;

	public static bool ATIVAR_BIT_QualidadeInspecaoFalhou = false;

	public static bool ATIVAR_BIT_QualidadeInspecaoOk = false;

	public static bool ATIVAR_BIT_QualidadeAvancaAlimentador = false;

	public static bool ATIVAR_BIT_QualidadeRecuaAlimentador = false;

	public static bool ATIVAR_BIT_QualidadeAcionaVacuo = false;

	public static bool ATIVAR_BIT_QualidadeEsteira1 = false;

	public static bool ATIVAR_BIT_QualidadeEsteira2 = false;

	public static bool ATIVAR_BIT_QualidadeStatusBancada = false;

	public static bool ATIVAR_BIT_QualidadeInspecionar = false;

	public static bool ATIVAR_BIT_QualidadeStopper1 = false;

	public static bool ATIVAR_BIT_QualidadeStopper2 = false;

	public static bool ATIVAR_BIT_MontagemDriveRobo = false;

	public static bool ATIVAR_BIT_MontagemPosicaoRobo = false;

	public static string VALOR_BIT_MontagemPosicaoRobo = "0";

	public static string VALOR_BIT_MontagemCorEsperada = "0";

	public static bool ATIVAR_BIT_MontagemRoboEmPosicao = false;

	public static bool ATIVAR_BIT_MontagemRoboOcupado = false;

	public static bool ATIVAR_BIT_MontagemTriggerCamera = false;

	public static bool ATIVAR_BIT_MontagemResultadoInspecao = false;

	public GameObject EXPEDICAO_TOGGLE;

	public GameObject EXPEDICAO_INPUTS;

	public GameObject EXPEDICAO_3D;

	public static bool EXPEDICAO_ATIVO = true;

	public bool BIT_ExpedicaoScannerOk;

	public bool BIT_ExpedicaoSegurancaOk;

	public bool BIT_ExpedicaoAlimentadorAvancado;

	public bool BIT_ExpedicaoAlimentadorRecuado;

	public bool BIT_ExpedicaoSensorVacuo;

	public bool BIT_ExpedicaoManoplaAutomatico;

	public bool BIT_ExpedicaoBotaoVerde;

	public bool BIT_ExpedicaoBotaoVermelho;

	public bool BIT_ExpedicaoPecaEsteira;

	public bool BIT_ExpedicaoEsteiraLiberada;

	public bool BIT_ExpedicaoOperacao;

	public bool BIT_ExpedicaoPosicaoGuardar;

	public bool BIT_ExpedicaoAtuadorEmPosicao;

	public bool BIT_ExpedicaoAtuadorOcupado;

	public bool BIT_ExpedicaoAvancaAlimentador;

	public bool BIT_ExpedicaoAcionaVacuo;

	public bool BIT_ExpedicaoEsteira1;

	public bool BIT_ExpedicaoEsteira2;

	public bool BIT_ExpedicaoStatusBancada;

	public bool BIT_ExpedicaoDriveAtuador;

	public bool BIT_ExpedicaoPosicaoAtuador;

	public GameObject OBJETO_TEXTO_BIT_ExpedicaoPosicaoAtuador;

	public bool BIT_ExpedicaoStopper1;

	public bool BIT_ExpedicaoStopper2;

	public static bool ATIVAR_BIT_ExpedicaoScannerOk = false;

	public static bool ATIVAR_BIT_ExpedicaoSegurancaOk = false;

	public static bool ATIVAR_BIT_ExpedicaoAlimentadorAvancado = false;

	public static bool ATIVAR_BIT_ExpedicaoAlimentadorRecuado = false;

	public static bool ATIVAR_BIT_ExpedicaoSensorVacuo = false;

	public static bool ATIVAR_BIT_ExpedicaoManoplaAutomatico = false;

	public static bool ATIVAR_BIT_ExpedicaoBotaoVerde = false;

	public static bool ATIVAR_BIT_ExpedicaoBotaoVermelho = false;

	public static bool ATIVAR_BIT_ExpedicaoPecaEsteira = false;

	public static bool ATIVAR_BIT_ExpedicaoEsteiraLiberada = false;

	public static bool ATIVAR_BIT_ExpedicaoOperacao = false;

	public static bool ATIVAR_BIT_ExpedicaoPosicaoGuardar = false;

	public static bool ATIVAR_BIT_ExpedicaoAtuadorEmPosicao = false;

	public static bool ATIVAR_BIT_ExpedicaoAtuadorOcupado = false;

	public static bool ATIVAR_BIT_ExpedicaoAvancaAlimentador = false;

	public static bool ATIVAR_BIT_ExpedicaoAcionaVacuo = false;

	public static bool ATIVAR_BIT_ExpedicaoEsteira1 = false;

	public static bool ATIVAR_BIT_ExpedicaoEsteira2 = false;

	public static bool ATIVAR_BIT_ExpedicaoStatusBancada = false;

	public static bool ATIVAR_BIT_ExpedicaoDriveAtuador = false;

	public static bool ATIVAR_BIT_ExpedicaoPosicaoAtuador = false;

	public static string VALOR_BIT_ExpedicaoPosicaoAtuador = "0";

	public static bool ATIVAR_BIT_ExpedicaoStopper1 = false;

	public static bool ATIVAR_BIT_ExpedicaoStopper2 = false;

	public static int EXPEDICAO_POSICAO_ATUADOR_ATUAL = 0;

	private void Start()
	{
		ESTOQUE_3D = GameObject.Find("ESTOQUE_3D");
		PROCESSO_3D = GameObject.Find("PROCESSO_3D");
		QUALIDADE_3D = GameObject.Find("QUALIDADE_3D");
		EXPEDICAO_3D = GameObject.Find("EXPEDICAO_3D");
		if (MINIMIZAR_FUNCAO)
		{
			ATIVAR_BIT_EstoqueAvancaAlimentador = false;
			ATIVAR_BIT_EstoqueRecuaAlimentador = false;
			ATIVAR_BIT_EstoqueAcionaVacuo = false;
			ATIVAR_BIT_EstoqueAvancaGarra = false;
			ATIVAR_BIT_EstoqueFechaGarra = false;
			ATIVAR_BIT_EstoqueEsteira1 = false;
			ATIVAR_BIT_EstoqueEsteira2 = false;
			ATIVAR_BIT_EstoqueDriveAtuador = false;
			VALOR_BIT_EstoquePosicaoAtuador = "0";
			ATIVAR_BIT_EstoqueStopper1 = false;
			ATIVAR_BIT_EstoqueStopper2 = false;
		}
		if (MINIMIZAR_FUNCAO)
		{
			ATIVAR_BIT_ProcessoAvancaAlimentador = false;
			ATIVAR_BIT_ProcessoRecuaAlimentador = false;
			ATIVAR_BIT_ProcessoAcionaVacuoManipulador = false;
			ATIVAR_BIT_ProcessoAcionaVacuoMesa = false;
			ATIVAR_BIT_EstoqueFechaGarra = false;
			ATIVAR_BIT_ProcessoEsteira1 = false;
			ATIVAR_BIT_ProcessoEsteira2 = false;
			ATIVAR_BIT_ProcessoDriveAtuador = false;
			VALOR_BIT_ProcessoPosicaoAtuador = "0";
		}
		if (MINIMIZAR_FUNCAO)
		{
			QUALIDADE_EXECUTANDO_ANIMACAO = false;
			QUALIDADE_CLICOU_INSPECIONAR = false;
			ATIVAR_BIT_QualidadeAvancaAlimentador = false;
			ATIVAR_BIT_QualidadeRecuaAlimentador = false;
			ATIVAR_BIT_QualidadeAcionaVacuo = false;
			ATIVAR_BIT_QualidadeInspecionar = false;
			ATIVAR_BIT_QualidadeEsteira1 = false;
			ATIVAR_BIT_QualidadeEsteira2 = false;
			ATIVAR_BIT_QualidadeStopper1 = false;
			ATIVAR_BIT_QualidadeStopper2 = false;
			ATIVAR_BIT_MontagemDriveRobo = false;
			ATIVAR_BIT_MontagemTriggerCamera = false;
			VALOR_BIT_MontagemPosicaoRobo = "0";
			VALOR_BIT_MontagemCorEsperada = "0";
		}
		if (MINIMIZAR_FUNCAO)
		{
			ATIVAR_BIT_ExpedicaoAvancaAlimentador = false;
			ATIVAR_BIT_ExpedicaoAcionaVacuo = false;
			ATIVAR_BIT_ExpedicaoEsteira1 = false;
			ATIVAR_BIT_ExpedicaoEsteira2 = false;
			ATIVAR_BIT_ExpedicaoDriveAtuador = false;
			VALOR_BIT_ExpedicaoPosicaoAtuador = "0";
			ATIVAR_BIT_ExpedicaoStopper1 = false;
			ATIVAR_BIT_ExpedicaoStopper2 = false;
		}
	}

	private void Update()
	{
		if (OBJETO_ADMINISTRACAO)
		{
			if (!PROCESSO_ATIVO)
			{
				MonoBehaviour.print("DESTRUIR LAMINA NO UPDATE");
				Object[] array = Object.FindObjectsOfType(typeof(GameObject));
				for (int i = 0; i < array.Length; i++)
				{
					((GameObject)array[i]).SendMessage("DESTRUIR_LAMINA_AGORA", SendMessageOptions.DontRequireReceiver);
				}
			}
			ESTOQUE_ATIVO = ESTOQUE_3D.activeSelf;
			if (ESTOQUE_TOGGLE.GetComponent<Toggle>().isOn)
			{
				ESTOQUE_3D.SetActive(value: true);
			}
			if (!ESTOQUE_TOGGLE.GetComponent<Toggle>().isOn)
			{
				ESTOQUE_3D.SetActive(value: false);
			}
			if (ESTOQUE_TOGGLE.GetComponent<Toggle>().isOn)
			{
				ESTOQUE_INPUTS.SetActive(value: true);
				ESTOQUE_INPUTS.transform.SetParent(PARENTE_BANCADAS_FINAL.transform, worldPositionStays: false);
			}
			if (!ESTOQUE_TOGGLE.GetComponent<Toggle>().isOn)
			{
				ESTOQUE_INPUTS.SetActive(value: false);
				ESTOQUE_INPUTS.transform.SetParent(PARENTE_BANCADAS_PROVISORIO.transform, worldPositionStays: false);
			}
			VAR_CHECKBOX_REMOVER_BLOCO = ESTOQUE_CHECKBOX_REMOVER_BLOCO.GetComponent<Toggle>().isOn;
			PROCESSO_ATIVO = PROCESSO_3D.activeSelf;
			if (PROCESSO_TOGGLE.GetComponent<Toggle>().isOn)
			{
				PROCESSO_3D.SetActive(value: true);
			}
			if (!PROCESSO_TOGGLE.GetComponent<Toggle>().isOn)
			{
				PROCESSO_3D.SetActive(value: false);
			}
			if (PROCESSO_TOGGLE.GetComponent<Toggle>().isOn)
			{
				PROCESSO_INPUTS.SetActive(value: true);
				PROCESSO_INPUTS.transform.SetParent(PARENTE_BANCADAS_FINAL.transform, worldPositionStays: false);
			}
			if (!PROCESSO_TOGGLE.GetComponent<Toggle>().isOn)
			{
				PROCESSO_INPUTS.SetActive(value: false);
				PROCESSO_INPUTS.transform.SetParent(PARENTE_BANCADAS_PROVISORIO.transform, worldPositionStays: false);
			}
			QUALIDADE_ATIVO = QUALIDADE_3D.activeSelf;
			if (QUALIDADE_TOGGLE.GetComponent<Toggle>().isOn)
			{
				QUALIDADE_3D.SetActive(value: true);
			}
			if (!QUALIDADE_TOGGLE.GetComponent<Toggle>().isOn)
			{
				QUALIDADE_3D.SetActive(value: false);
			}
			if (QUALIDADE_TOGGLE.GetComponent<Toggle>().isOn)
			{
				QUALIDADE_INPUTS.SetActive(value: true);
				QUALIDADE_INPUTS.transform.SetParent(PARENTE_BANCADAS_FINAL.transform, worldPositionStays: false);
			}
			if (!QUALIDADE_TOGGLE.GetComponent<Toggle>().isOn)
			{
				QUALIDADE_INPUTS.SetActive(value: false);
				QUALIDADE_INPUTS.transform.SetParent(PARENTE_BANCADAS_PROVISORIO.transform, worldPositionStays: false);
			}
			EXPEDICAO_ATIVO = EXPEDICAO_3D.activeSelf;
			if (EXPEDICAO_TOGGLE.GetComponent<Toggle>().isOn)
			{
				EXPEDICAO_3D.SetActive(value: true);
			}
			if (!EXPEDICAO_TOGGLE.GetComponent<Toggle>().isOn)
			{
				EXPEDICAO_3D.SetActive(value: false);
			}
			if (EXPEDICAO_TOGGLE.GetComponent<Toggle>().isOn)
			{
				EXPEDICAO_INPUTS.SetActive(value: true);
				EXPEDICAO_INPUTS.transform.SetParent(PARENTE_BANCADAS_FINAL.transform, worldPositionStays: false);
			}
			if (!EXPEDICAO_TOGGLE.GetComponent<Toggle>().isOn)
			{
				EXPEDICAO_INPUTS.SetActive(value: false);
				EXPEDICAO_INPUTS.transform.SetParent(PARENTE_BANCADAS_PROVISORIO.transform, worldPositionStays: false);
			}
		}
		if (MINIMIZAR_FUNCAO)
		{
			if (Administracao.COMUNICACAO_MANUAL)
			{
				if (BIT_EstoqueSegurancaOk)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueSegurancaOk;
				}
				if (BIT_EstoqueGarraAvancada)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueGarraAvancada;
				}
				if (BIT_EstoqueGarraRecuada)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueGarraRecuada;
				}
				if (BIT_EstoqueGarraAberta)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueGarraAberta;
				}
				if (BIT_EstoqueGarraFechada)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueGarraFechada;
				}
				if (BIT_EstoqueAlimentadorAvancado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueAlimentadorAvancado;
				}
				if (BIT_EstoqueAlimentadorBuffer)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueAlimentadorBuffer;
				}
				if (BIT_EstoqueAlimentadorRecuado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueAlimentadorRecuado;
				}
				if (BIT_EstoqueSensorVacuo)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueSensorVacuo;
				}
				if (BIT_EstoqueManoplaAutomatico)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueManoplaAutomatico;
				}
				if (BIT_EstoqueBotaoVerde)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueBotaoVerde;
				}
				if (BIT_EstoqueBotaoVermelho)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueBotaoVermelho;
				}
				if (BIT_EstoquePecaEsteira)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoquePecaEsteira;
				}
				if (BIT_EstoqueEsteiraLiberada)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueEsteiraLiberada;
				}
				if (BIT_EstoqueAtuadorEmPosicao)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueAtuadorEmPosicao;
				}
				if (BIT_EstoqueAtuadorOcupado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueAtuadorOcupado;
				}
				if (BIT_EstoqueAvancaAlimentador)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_EstoqueAvancaAlimentador = true;
					}
					else
					{
						ATIVAR_BIT_EstoqueAvancaAlimentador = false;
					}
				}
				if (BIT_EstoqueRecuaAlimentador)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_EstoqueRecuaAlimentador = true;
					}
					else
					{
						ATIVAR_BIT_EstoqueRecuaAlimentador = false;
					}
				}
				if (BIT_EstoqueAcionaVacuo)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_EstoqueAcionaVacuo = true;
					}
					else
					{
						ATIVAR_BIT_EstoqueAcionaVacuo = false;
					}
				}
				if (BIT_EstoqueAvancaGarra)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_EstoqueAvancaGarra = true;
					}
					else
					{
						ATIVAR_BIT_EstoqueAvancaGarra = false;
					}
				}
				if (BIT_EstoqueFechaGarra)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_EstoqueFechaGarra = true;
					}
					else
					{
						ATIVAR_BIT_EstoqueFechaGarra = false;
					}
				}
				if (BIT_EstoqueEsteira1)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_EstoqueEsteira1 = true;
					}
					else
					{
						ATIVAR_BIT_EstoqueEsteira1 = false;
					}
				}
				if (BIT_EstoqueEsteira2)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_EstoqueEsteira2 = true;
					}
					else
					{
						ATIVAR_BIT_EstoqueEsteira2 = false;
					}
				}
				if (BIT_EstoqueDriveAtuador)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_EstoqueDriveAtuador = true;
					}
					else
					{
						ATIVAR_BIT_EstoqueDriveAtuador = false;
					}
				}
				if (BIT_EstoquePosicaoAtuador)
				{
					OBJETO_TEXTO_BIT_EstoquePosicaoAtuador.GetComponent<Text>().text = "36 [" + VALOR_BIT_EstoquePosicaoAtuador + "]";
				}
				if (BIT_EstoqueStopper1)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_EstoqueStopper1 = true;
					}
					else
					{
						ATIVAR_BIT_EstoqueStopper1 = false;
					}
				}
				if (BIT_EstoqueStopper2)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_EstoqueStopper2 = true;
					}
					else
					{
						ATIVAR_BIT_EstoqueStopper2 = false;
					}
				}
			}
			if (Administracao.COMUNICACAO_MODBUS_MASTER || Administracao.COMUNICACAO_MODBUS_SLAVE || Administracao.COMUNICACAO_SIEMENS)
			{
				if (BIT_EstoqueSegurancaOk)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueSegurancaOk;
				}
				if (BIT_EstoqueGarraAvancada)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueGarraAvancada;
				}
				if (BIT_EstoqueGarraRecuada)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueGarraRecuada;
				}
				if (BIT_EstoqueGarraAberta)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueGarraAberta;
				}
				if (BIT_EstoqueGarraFechada)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueGarraFechada;
				}
				if (BIT_EstoqueAlimentadorAvancado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueAlimentadorAvancado;
				}
				if (BIT_EstoqueAlimentadorBuffer)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueAlimentadorBuffer;
				}
				if (BIT_EstoqueAlimentadorRecuado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueAlimentadorRecuado;
				}
				if (BIT_EstoqueSensorVacuo)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueSensorVacuo;
				}
				if (BIT_EstoqueManoplaAutomatico)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueManoplaAutomatico;
				}
				if (BIT_EstoqueBotaoVerde)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueBotaoVerde;
				}
				if (BIT_EstoqueBotaoVermelho)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueBotaoVermelho;
				}
				if (BIT_EstoquePecaEsteira)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoquePecaEsteira;
				}
				if (BIT_EstoqueEsteiraLiberada)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueEsteiraLiberada;
				}
				if (BIT_EstoqueAtuadorEmPosicao)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueAtuadorEmPosicao;
				}
				if (BIT_EstoqueAtuadorOcupado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueAtuadorOcupado;
				}
				if (BIT_EstoqueAvancaAlimentador)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueAvancaAlimentador;
				}
				if (BIT_EstoqueRecuaAlimentador)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueRecuaAlimentador;
				}
				if (BIT_EstoqueAcionaVacuo)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueAcionaVacuo;
				}
				if (BIT_EstoqueAvancaGarra)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueAvancaGarra;
				}
				if (BIT_EstoqueFechaGarra)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueFechaGarra;
				}
				if (BIT_EstoqueEsteira1)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueEsteira1;
				}
				if (BIT_EstoqueEsteira2)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueEsteira2;
				}
				if (BIT_EstoqueDriveAtuador)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueDriveAtuador;
				}
				if (BIT_EstoquePosicaoAtuador)
				{
					OBJETO_TEXTO_BIT_EstoquePosicaoAtuador.GetComponent<Text>().text = "36 [" + VALOR_BIT_EstoquePosicaoAtuador + "]";
				}
				if (BIT_EstoqueStopper1)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueStopper1;
				}
				if (BIT_EstoqueStopper2)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_EstoqueStopper2;
				}
			}
		}
		if (MINIMIZAR_FUNCAO)
		{
			if (Administracao.COMUNICACAO_MANUAL)
			{
				if (BIT_ProcessoSegurancaOk)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoSegurancaOk;
				}
				if (BIT_ProcessoAlimentadorAvancado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoAlimentadorAvancado;
				}
				if (BIT_ProcessoAlimentadorBuffer)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoAlimentadorBuffer;
				}
				if (BIT_ProcessoAlimentadorRecuado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoAlimentadorRecuado;
				}
				if (BIT_ProcessoSensorVacuo)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoSensorVacuo;
				}
				if (BIT_ProcessoSensorVacuoMesa)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoSensorVacuoMesa;
				}
				if (BIT_ProcessoManoplaAutomatico)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoManoplaAutomatico;
				}
				if (BIT_ProcessoBotaoVerde)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoBotaoVerde;
				}
				if (BIT_ProcessoBotaoVermelho)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoBotaoVermelho;
				}
				if (BIT_ProcessoPecaEsteira)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoPecaEsteira;
				}
				if (BIT_ProcessoEsteiraLiberada)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoEsteiraLiberada;
				}
				if (BIT_ProcessoAtuadorEmPosicao)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoAtuadorEmPosicao;
				}
				if (BIT_ProcessoAtuadorOcupado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoAtuadorOcupado;
				}
				if (BIT_ProcessoAvancaAlimentador)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_ProcessoAvancaAlimentador = true;
					}
					else
					{
						ATIVAR_BIT_ProcessoAvancaAlimentador = false;
					}
				}
				if (BIT_ProcessoRecuaAlimentador)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_ProcessoRecuaAlimentador = true;
					}
					else
					{
						ATIVAR_BIT_ProcessoRecuaAlimentador = false;
					}
				}
				if (BIT_ProcessoAcionaVacuoManipulador)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_ProcessoAcionaVacuoManipulador = true;
					}
					else
					{
						ATIVAR_BIT_ProcessoAcionaVacuoManipulador = false;
					}
				}
				if (BIT_ProcessoAcionaVacuoMesa)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_ProcessoAcionaVacuoMesa = true;
					}
					else
					{
						ATIVAR_BIT_ProcessoAcionaVacuoMesa = false;
					}
				}
				if (BIT_ProcessoEsteira1)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_ProcessoEsteira1 = true;
					}
					else
					{
						ATIVAR_BIT_ProcessoEsteira1 = false;
					}
				}
				if (BIT_ProcessoEsteira2)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_ProcessoEsteira2 = true;
					}
					else
					{
						ATIVAR_BIT_ProcessoEsteira2 = false;
					}
				}
				if (BIT_ProcessoDriveAtuador)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_ProcessoDriveAtuador = true;
					}
					else
					{
						ATIVAR_BIT_ProcessoDriveAtuador = false;
					}
				}
				if (BIT_ProcessoPosicaoAtuador)
				{
					OBJETO_TEXTO_BIT_ProcessoPosicaoAtuador.GetComponent<Text>().text = "0 [" + VALOR_BIT_ProcessoPosicaoAtuador + "]";
				}
			}
			if (Administracao.COMUNICACAO_MODBUS_MASTER || Administracao.COMUNICACAO_MODBUS_SLAVE || Administracao.COMUNICACAO_SIEMENS)
			{
				if (BIT_ProcessoSegurancaOk)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoSegurancaOk;
				}
				if (BIT_ProcessoAlimentadorAvancado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoAlimentadorAvancado;
				}
				if (BIT_ProcessoAlimentadorBuffer)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoAlimentadorBuffer;
				}
				if (BIT_ProcessoAlimentadorRecuado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoAlimentadorRecuado;
				}
				if (BIT_ProcessoSensorVacuo)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoSensorVacuo;
				}
				if (BIT_ProcessoSensorVacuoMesa)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoSensorVacuoMesa;
				}
				if (BIT_ProcessoManoplaAutomatico)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoManoplaAutomatico;
				}
				if (BIT_ProcessoBotaoVerde)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoBotaoVerde;
				}
				if (BIT_ProcessoBotaoVermelho)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoBotaoVermelho;
				}
				if (BIT_ProcessoPecaEsteira)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoPecaEsteira;
				}
				if (BIT_ProcessoEsteiraLiberada)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoEsteiraLiberada;
				}
				if (BIT_ProcessoAtuadorEmPosicao)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoAtuadorEmPosicao;
				}
				if (BIT_ProcessoAtuadorOcupado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoAtuadorOcupado;
				}
				if (BIT_ProcessoAvancaAlimentador)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoAvancaAlimentador;
				}
				if (BIT_ProcessoRecuaAlimentador)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoRecuaAlimentador;
				}
				if (BIT_ProcessoAcionaVacuoManipulador)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoAcionaVacuoManipulador;
				}
				if (BIT_ProcessoAcionaVacuoMesa)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoAcionaVacuoMesa;
				}
				if (BIT_ProcessoEsteira1)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoEsteira1;
				}
				if (BIT_ProcessoEsteira2)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoEsteira2;
				}
				if (BIT_ProcessoDriveAtuador)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_ProcessoDriveAtuador;
				}
				if (BIT_ProcessoPosicaoAtuador)
				{
					OBJETO_TEXTO_BIT_ProcessoPosicaoAtuador.GetComponent<Text>().text = "0 [" + VALOR_BIT_ProcessoPosicaoAtuador + "]";
				}
			}
		}
		if (MINIMIZAR_FUNCAO)
		{
			if (Administracao.COMUNICACAO_MANUAL)
			{
				if (BIT_QualidadeSegurancaOk)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeSegurancaOk;
				}
				if (BIT_QualidadeAlimentadorAvancado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeAlimentadorAvancado;
				}
				if (BIT_QualidadeALimentadorBuffer)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeALimentadorBuffer;
				}
				if (BIT_QualidadeAlimentadorRecuado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeAlimentadorRecuado;
				}
				if (BIT_QualidadeSensorVacuo)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeSensorVacuo;
				}
				if (BIT_QualidadeManoplaAutomatico)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeManoplaAutomatico;
				}
				if (BIT_QualidadeBotaoVerde)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeBotaoVerde;
				}
				if (BIT_QualidadeBotaoVermelho)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeBotaoVermelho;
				}
				if (BIT_QualidadePecaEsteira)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadePecaEsteira;
				}
				if (BIT_QualidadeInspecaoFalhou)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeInspecaoFalhou;
				}
				if (BIT_QualidadeInspecaoOk)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeInspecaoOk;
				}
				if (BIT_QualidadeEsteiraLiberada)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeEsteiraLiberada;
				}
				if (BIT_MontagemRoboEmPosicao)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_MontagemRoboEmPosicao;
				}
				if (BIT_MontagemRoboOcupado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_MontagemRoboOcupado;
				}
				if (BIT_MontagemResultadoInspecao)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_MontagemResultadoInspecao;
				}
				if (BIT_QualidadeAvancaAlimentador)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_QualidadeAvancaAlimentador = true;
					}
					else
					{
						ATIVAR_BIT_QualidadeAvancaAlimentador = false;
					}
				}
				if (BIT_QualidadeRecuaAlimentador)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_QualidadeRecuaAlimentador = true;
					}
					else
					{
						ATIVAR_BIT_QualidadeRecuaAlimentador = false;
					}
				}
				if (BIT_QualidadeAcionaVacuo)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_QualidadeAcionaVacuo = true;
					}
					else
					{
						ATIVAR_BIT_QualidadeAcionaVacuo = false;
					}
				}
				if (BIT_MontagemDriveRobo)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_MontagemDriveRobo = true;
					}
					else
					{
						ATIVAR_BIT_MontagemDriveRobo = false;
					}
				}
				if (BIT_MontagemTriggerCamera)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_MontagemTriggerCamera = true;
					}
					else
					{
						ATIVAR_BIT_MontagemTriggerCamera = false;
					}
				}
				if (BIT_MontagemPosicaoRobo)
				{
					OBJETO_TEXTO_BIT_MontagemPosicaoRobo.GetComponent<Text>().text = "0 [" + VALOR_BIT_MontagemPosicaoRobo + "]";
				}
				if (BIT_MontagemCorEsperada)
				{
					OBJETO_TEXTO_BIT_MontagemCorEsperada.GetComponent<Text>().text = "0 [" + VALOR_BIT_MontagemCorEsperada + "]";
				}
				if (BIT_QualidadeInspecionar)
				{
					if (GetComponent<Toggle>().isOn)
					{
						if (QUALIDADE_CLICOU_INSPECIONAR)
						{
							ATIVAR_BIT_QualidadeInspecaoOk = false;
							ATIVAR_BIT_QualidadeInspecaoFalhou = false;
							VOID_QUALIDADE_CLICOU_INSPECIONAR();
						}
						QUALIDADE_EXECUTANDO_ANIMACAO = true;
					}
					else
					{
						ATIVAR_BIT_QualidadeInspecionar = false;
					}
				}
				if (BIT_QualidadeEsteira1)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_QualidadeEsteira1 = true;
					}
					else
					{
						ATIVAR_BIT_QualidadeEsteira1 = false;
					}
				}
				if (BIT_QualidadeEsteira2)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_QualidadeEsteira2 = true;
					}
					else
					{
						ATIVAR_BIT_QualidadeEsteira2 = false;
					}
				}
				if (BIT_QualidadeStopper1)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_QualidadeStopper1 = true;
					}
					else
					{
						ATIVAR_BIT_QualidadeStopper1 = false;
					}
				}
				if (BIT_QualidadeStopper2)
				{
					if (GetComponent<Toggle>().isOn)
					{
						ATIVAR_BIT_QualidadeStopper2 = true;
					}
					else
					{
						ATIVAR_BIT_QualidadeStopper2 = false;
					}
				}
			}
			if (Administracao.COMUNICACAO_MODBUS_MASTER || Administracao.COMUNICACAO_MODBUS_SLAVE || Administracao.COMUNICACAO_SIEMENS)
			{
				if (BIT_QualidadeSegurancaOk)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeSegurancaOk;
				}
				if (BIT_QualidadeAlimentadorAvancado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeAlimentadorAvancado;
				}
				if (BIT_QualidadeALimentadorBuffer)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeALimentadorBuffer;
				}
				if (BIT_QualidadeAlimentadorRecuado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeAlimentadorRecuado;
				}
				if (BIT_QualidadeSensorVacuo)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeSensorVacuo;
				}
				if (BIT_QualidadeManoplaAutomatico)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeManoplaAutomatico;
				}
				if (BIT_QualidadeBotaoVerde)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeBotaoVerde;
				}
				if (BIT_QualidadeBotaoVermelho)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeBotaoVermelho;
				}
				if (BIT_QualidadePecaEsteira)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadePecaEsteira;
				}
				if (BIT_QualidadeInspecaoFalhou)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeInspecaoFalhou;
				}
				if (BIT_QualidadeInspecaoOk)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeInspecaoOk;
				}
				if (BIT_QualidadeEsteiraLiberada)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeEsteiraLiberada;
				}
				if (BIT_MontagemRoboEmPosicao)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_MontagemRoboEmPosicao;
				}
				if (BIT_MontagemRoboOcupado)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_MontagemRoboOcupado;
				}
				if (BIT_MontagemResultadoInspecao)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_MontagemResultadoInspecao;
				}
				if (BIT_QualidadeAvancaAlimentador)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeAvancaAlimentador;
				}
				if (BIT_QualidadeRecuaAlimentador)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeRecuaAlimentador;
				}
				if (BIT_QualidadeAcionaVacuo)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeAcionaVacuo;
				}
				if (BIT_MontagemDriveRobo)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_MontagemDriveRobo;
				}
				if (BIT_MontagemTriggerCamera)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_MontagemTriggerCamera;
				}
				if (BIT_MontagemPosicaoRobo)
				{
					OBJETO_TEXTO_BIT_MontagemPosicaoRobo.GetComponent<Text>().text = "0 [" + VALOR_BIT_MontagemPosicaoRobo + "]";
				}
				if (BIT_MontagemCorEsperada)
				{
					OBJETO_TEXTO_BIT_MontagemCorEsperada.GetComponent<Text>().text = "0 [" + VALOR_BIT_MontagemCorEsperada + "]";
				}
				if (BIT_QualidadeInspecionar)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeInspecionar;
					if (GetComponent<Toggle>().isOn)
					{
						if (QUALIDADE_CLICOU_INSPECIONAR)
						{
							ATIVAR_BIT_QualidadeInspecaoOk = false;
							ATIVAR_BIT_QualidadeInspecaoFalhou = false;
							VOID_QUALIDADE_CLICOU_INSPECIONAR();
						}
						QUALIDADE_EXECUTANDO_ANIMACAO = true;
					}
					else
					{
						ATIVAR_BIT_QualidadeInspecionar = false;
					}
				}
				if (BIT_QualidadeEsteira1)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeEsteira1;
				}
				if (BIT_QualidadeEsteira2)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeEsteira2;
				}
				if (BIT_QualidadeStopper1)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeStopper1;
				}
				if (BIT_QualidadeStopper2)
				{
					GetComponent<Toggle>().isOn = ATIVAR_BIT_QualidadeStopper2;
				}
			}
		}
		if (!MINIMIZAR_FUNCAO)
		{
			return;
		}
		if (Administracao.COMUNICACAO_MANUAL)
		{
			if (BIT_ExpedicaoSegurancaOk)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoSegurancaOk;
			}
			if (BIT_ExpedicaoAlimentadorAvancado)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoAlimentadorAvancado;
			}
			if (BIT_ExpedicaoAlimentadorRecuado)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoAlimentadorRecuado;
			}
			if (BIT_ExpedicaoSensorVacuo)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoSensorVacuo;
			}
			if (BIT_ExpedicaoManoplaAutomatico)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoManoplaAutomatico;
			}
			if (BIT_ExpedicaoBotaoVerde)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoBotaoVerde;
			}
			if (BIT_ExpedicaoBotaoVermelho)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoBotaoVermelho;
			}
			if (BIT_ExpedicaoPecaEsteira)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoPecaEsteira;
			}
			if (BIT_ExpedicaoEsteiraLiberada)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoEsteiraLiberada;
			}
			if (BIT_ExpedicaoAtuadorEmPosicao)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoAtuadorEmPosicao;
			}
			if (BIT_ExpedicaoAtuadorOcupado)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoAtuadorOcupado;
			}
			if (BIT_ExpedicaoAvancaAlimentador)
			{
				if (GetComponent<Toggle>().isOn)
				{
					ATIVAR_BIT_ExpedicaoAvancaAlimentador = true;
				}
				else
				{
					ATIVAR_BIT_ExpedicaoAvancaAlimentador = false;
				}
			}
			if (BIT_ExpedicaoAcionaVacuo)
			{
				if (GetComponent<Toggle>().isOn)
				{
					ATIVAR_BIT_ExpedicaoAcionaVacuo = true;
				}
				else
				{
					ATIVAR_BIT_ExpedicaoAcionaVacuo = false;
				}
			}
			if (BIT_ExpedicaoEsteira1)
			{
				if (GetComponent<Toggle>().isOn)
				{
					ATIVAR_BIT_ExpedicaoEsteira1 = true;
				}
				else
				{
					ATIVAR_BIT_ExpedicaoEsteira1 = false;
				}
			}
			if (BIT_ExpedicaoEsteira2)
			{
				if (GetComponent<Toggle>().isOn)
				{
					ATIVAR_BIT_ExpedicaoEsteira2 = true;
				}
				else
				{
					ATIVAR_BIT_ExpedicaoEsteira2 = false;
				}
			}
			if (BIT_ExpedicaoDriveAtuador)
			{
				if (GetComponent<Toggle>().isOn)
				{
					ATIVAR_BIT_ExpedicaoDriveAtuador = true;
				}
				else
				{
					ATIVAR_BIT_ExpedicaoDriveAtuador = false;
				}
			}
			if (BIT_ExpedicaoPosicaoAtuador)
			{
				OBJETO_TEXTO_BIT_ExpedicaoPosicaoAtuador.GetComponent<Text>().text = "18 [" + VALOR_BIT_ExpedicaoPosicaoAtuador + "]";
			}
			if (BIT_ExpedicaoStopper1)
			{
				if (GetComponent<Toggle>().isOn)
				{
					ATIVAR_BIT_ExpedicaoStopper1 = true;
				}
				else
				{
					ATIVAR_BIT_ExpedicaoStopper1 = false;
				}
			}
			if (BIT_ExpedicaoStopper2)
			{
				if (GetComponent<Toggle>().isOn)
				{
					ATIVAR_BIT_ExpedicaoStopper2 = true;
				}
				else
				{
					ATIVAR_BIT_ExpedicaoStopper2 = false;
				}
			}
		}
		if (Administracao.COMUNICACAO_MODBUS_MASTER || Administracao.COMUNICACAO_MODBUS_SLAVE || Administracao.COMUNICACAO_SIEMENS)
		{
			if (BIT_ExpedicaoSegurancaOk)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoSegurancaOk;
			}
			if (BIT_ExpedicaoAlimentadorAvancado)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoAlimentadorAvancado;
			}
			if (BIT_ExpedicaoAlimentadorRecuado)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoAlimentadorRecuado;
			}
			if (BIT_ExpedicaoSensorVacuo)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoSensorVacuo;
			}
			if (BIT_ExpedicaoManoplaAutomatico)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoManoplaAutomatico;
			}
			if (BIT_ExpedicaoBotaoVerde)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoBotaoVerde;
			}
			if (BIT_ExpedicaoBotaoVermelho)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoBotaoVermelho;
			}
			if (BIT_ExpedicaoPecaEsteira)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoPecaEsteira;
			}
			if (BIT_ExpedicaoEsteiraLiberada)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoEsteiraLiberada;
			}
			if (BIT_ExpedicaoAtuadorEmPosicao)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoAtuadorEmPosicao;
			}
			if (BIT_ExpedicaoAtuadorOcupado)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoAtuadorOcupado;
			}
			if (BIT_ExpedicaoAvancaAlimentador)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoAvancaAlimentador;
			}
			if (BIT_ExpedicaoEsteira1)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoEsteira1;
			}
			if (BIT_ExpedicaoEsteira2)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoEsteira2;
			}
			if (BIT_ExpedicaoAcionaVacuo)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoAcionaVacuo;
			}
			if (BIT_ExpedicaoPosicaoAtuador)
			{
				OBJETO_TEXTO_BIT_ExpedicaoPosicaoAtuador.GetComponent<Text>().text = "18 [" + VALOR_BIT_ExpedicaoPosicaoAtuador + "]";
			}
			if (BIT_ExpedicaoDriveAtuador)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoDriveAtuador;
			}
			if (BIT_ExpedicaoPosicaoAtuador)
			{
				OBJETO_TEXTO_BIT_ExpedicaoPosicaoAtuador.GetComponent<Text>().text = "18 [" + VALOR_BIT_ExpedicaoPosicaoAtuador + "]";
			}
			if (BIT_ExpedicaoStopper1)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoStopper1;
			}
			if (BIT_ExpedicaoStopper2)
			{
				GetComponent<Toggle>().isOn = ATIVAR_BIT_ExpedicaoStopper2;
			}
		}
	}

	public void ESTOQUE_VOID_POSICAO_PARA_BUSCAR()
	{
		if (Administracao.COMUNICACAO_MANUAL)
		{
			bool flag = true;
			if (VALOR_BIT_EstoquePosicaoAtuador == "0" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "1";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "1" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "2";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "2" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "3";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "3" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "4";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "4" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "5";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "5" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "6";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "6" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "7";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "7" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "8";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "8" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "9";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "9" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "10";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "10" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "11";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "11" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "12";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "12" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "13";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "13" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "14";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "14" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "15";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "15" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "16";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "16" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "17";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "17" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "18";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "18" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "19";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "19" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "20";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "20" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "21";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "21" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "22";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "22" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "23";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "23" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "24";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "24" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "25";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "25" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "26";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "26" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "27";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "27" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "28";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "28" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "29";
				flag = false;
			}
			if (VALOR_BIT_EstoquePosicaoAtuador == "29" && flag)
			{
				VALOR_BIT_EstoquePosicaoAtuador = "0";
				flag = false;
			}
		}
	}

	public void PROCESSO_VOID_POSICAO_PARA_BUSCAR()
	{
		if (Administracao.COMUNICACAO_MANUAL)
		{
			bool flag = true;
			if (VALOR_BIT_ProcessoPosicaoAtuador == "0" && flag)
			{
				VALOR_BIT_ProcessoPosicaoAtuador = "1";
				flag = false;
			}
			if (VALOR_BIT_ProcessoPosicaoAtuador == "1" && flag)
			{
				VALOR_BIT_ProcessoPosicaoAtuador = "2";
				flag = false;
			}
			if (VALOR_BIT_ProcessoPosicaoAtuador == "2" && flag)
			{
				VALOR_BIT_ProcessoPosicaoAtuador = "3";
				flag = false;
			}
			if (VALOR_BIT_ProcessoPosicaoAtuador == "3" && flag)
			{
				VALOR_BIT_ProcessoPosicaoAtuador = "4";
				flag = false;
			}
			if (VALOR_BIT_ProcessoPosicaoAtuador == "4" && flag)
			{
				VALOR_BIT_ProcessoPosicaoAtuador = "5";
				flag = false;
			}
			if (VALOR_BIT_ProcessoPosicaoAtuador == "5" && flag)
			{
				VALOR_BIT_ProcessoPosicaoAtuador = "6";
				flag = false;
			}
			if (VALOR_BIT_ProcessoPosicaoAtuador == "6" && flag)
			{
				VALOR_BIT_ProcessoPosicaoAtuador = "7";
				flag = false;
			}
			if (VALOR_BIT_ProcessoPosicaoAtuador == "7" && flag)
			{
				VALOR_BIT_ProcessoPosicaoAtuador = "8";
				flag = false;
			}
			if (VALOR_BIT_ProcessoPosicaoAtuador == "8" && flag)
			{
				VALOR_BIT_ProcessoPosicaoAtuador = "9";
				flag = false;
			}
			if (VALOR_BIT_ProcessoPosicaoAtuador == "9" && flag)
			{
				VALOR_BIT_ProcessoPosicaoAtuador = "10";
				flag = false;
			}
			if (VALOR_BIT_ProcessoPosicaoAtuador == "10" && flag)
			{
				VALOR_BIT_ProcessoPosicaoAtuador = "11";
				flag = false;
			}
			if (VALOR_BIT_ProcessoPosicaoAtuador == "11" && flag)
			{
				VALOR_BIT_ProcessoPosicaoAtuador = "12";
				flag = false;
			}
			if (VALOR_BIT_ProcessoPosicaoAtuador == "12" && flag)
			{
				VALOR_BIT_ProcessoPosicaoAtuador = "13";
				flag = false;
			}
			if (VALOR_BIT_ProcessoPosicaoAtuador == "13" && flag)
			{
				VALOR_BIT_ProcessoPosicaoAtuador = "14";
				flag = false;
			}
			if (VALOR_BIT_ProcessoPosicaoAtuador == "14" && flag)
			{
				VALOR_BIT_ProcessoPosicaoAtuador = "0";
				flag = false;
			}
		}
	}

	public void MONTAGEM_VOID_POSICAO_PARA_BUSCAR()
	{
		if (Administracao.COMUNICACAO_MANUAL)
		{
			bool flag = true;
			if (VALOR_BIT_MontagemPosicaoRobo == "0" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "1";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "1" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "2";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "2" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "3";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "3" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "4";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "4" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "5";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "5" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "6";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "6" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "7";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "7" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "8";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "8" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "9";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "9" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "10";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "10" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "11";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "11" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "12";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "12" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "13";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "13" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "14";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "14" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "15";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "15" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "16";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "16" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "17";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "17" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "18";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "18" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "19";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "19" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "20";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "20" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "21";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "21" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "22";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "22" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "23";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "23" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "24";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "24" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "25";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "25" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "26";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "26" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "27";
				flag = false;
			}
			if (VALOR_BIT_MontagemPosicaoRobo == "27" && flag)
			{
				VALOR_BIT_MontagemPosicaoRobo = "0";
				flag = false;
			}
		}
	}

	public void MONTAGEM_VOID_COR_ESPERADA()
	{
		if (Administracao.COMUNICACAO_MANUAL)
		{
			bool flag = true;
			if (VALOR_BIT_MontagemCorEsperada == "0" && flag)
			{
				VALOR_BIT_MontagemCorEsperada = "1";
				flag = false;
			}
			if (VALOR_BIT_MontagemCorEsperada == "1" && flag)
			{
				VALOR_BIT_MontagemCorEsperada = "2";
				flag = false;
			}
			if (VALOR_BIT_MontagemCorEsperada == "2" && flag)
			{
				VALOR_BIT_MontagemCorEsperada = "3";
				flag = false;
			}
			if (VALOR_BIT_MontagemCorEsperada == "3" && flag)
			{
				VALOR_BIT_MontagemCorEsperada = "4";
				flag = false;
			}
			if (VALOR_BIT_MontagemCorEsperada == "4" && flag)
			{
				VALOR_BIT_MontagemCorEsperada = "5";
				flag = false;
			}
			if (VALOR_BIT_MontagemCorEsperada == "5" && flag)
			{
				VALOR_BIT_MontagemCorEsperada = "6";
				flag = false;
			}
			if (VALOR_BIT_MontagemCorEsperada == "6" && flag)
			{
				VALOR_BIT_MontagemCorEsperada = "0";
				flag = false;
			}
		}
	}

	public void EXPEDICAO_VOID_POSICAO_PARA_BUSCAR()
	{
		if (Administracao.COMUNICACAO_MANUAL)
		{
			bool flag = true;
			if (VALOR_BIT_ExpedicaoPosicaoAtuador == "0" && flag)
			{
				VALOR_BIT_ExpedicaoPosicaoAtuador = "1";
				flag = false;
			}
			if (VALOR_BIT_ExpedicaoPosicaoAtuador == "1" && flag)
			{
				VALOR_BIT_ExpedicaoPosicaoAtuador = "2";
				flag = false;
			}
			if (VALOR_BIT_ExpedicaoPosicaoAtuador == "2" && flag)
			{
				VALOR_BIT_ExpedicaoPosicaoAtuador = "3";
				flag = false;
			}
			if (VALOR_BIT_ExpedicaoPosicaoAtuador == "3" && flag)
			{
				VALOR_BIT_ExpedicaoPosicaoAtuador = "4";
				flag = false;
			}
			if (VALOR_BIT_ExpedicaoPosicaoAtuador == "4" && flag)
			{
				VALOR_BIT_ExpedicaoPosicaoAtuador = "5";
				flag = false;
			}
			if (VALOR_BIT_ExpedicaoPosicaoAtuador == "5" && flag)
			{
				VALOR_BIT_ExpedicaoPosicaoAtuador = "6";
				flag = false;
			}
			if (VALOR_BIT_ExpedicaoPosicaoAtuador == "6" && flag)
			{
				VALOR_BIT_ExpedicaoPosicaoAtuador = "7";
				flag = false;
			}
			if (VALOR_BIT_ExpedicaoPosicaoAtuador == "7" && flag)
			{
				VALOR_BIT_ExpedicaoPosicaoAtuador = "8";
				flag = false;
			}
			if (VALOR_BIT_ExpedicaoPosicaoAtuador == "8" && flag)
			{
				VALOR_BIT_ExpedicaoPosicaoAtuador = "9";
				flag = false;
			}
			if (VALOR_BIT_ExpedicaoPosicaoAtuador == "9" && flag)
			{
				VALOR_BIT_ExpedicaoPosicaoAtuador = "10";
				flag = false;
			}
			if (VALOR_BIT_ExpedicaoPosicaoAtuador == "10" && flag)
			{
				VALOR_BIT_ExpedicaoPosicaoAtuador = "11";
				flag = false;
			}
			if (VALOR_BIT_ExpedicaoPosicaoAtuador == "11" && flag)
			{
				VALOR_BIT_ExpedicaoPosicaoAtuador = "12";
				flag = false;
			}
			if (VALOR_BIT_ExpedicaoPosicaoAtuador == "12" && flag)
			{
				VALOR_BIT_ExpedicaoPosicaoAtuador = "13";
				flag = false;
			}
			if (VALOR_BIT_ExpedicaoPosicaoAtuador == "13" && flag)
			{
				VALOR_BIT_ExpedicaoPosicaoAtuador = "14";
				flag = false;
			}
			if (VALOR_BIT_ExpedicaoPosicaoAtuador == "14" && flag)
			{
				VALOR_BIT_ExpedicaoPosicaoAtuador = "0";
				flag = false;
			}
		}
	}

	private void VOID_QUALIDADE_CLICOU_INSPECIONAR()
	{
		if (!QUALIDADE_CLICOU_INSPECIONAR)
		{
			QUALIDADE_CLICOU_INSPECIONAR = true;
		}
	}

	private void DESTRUIR_LAMINA_AGORA()
	{
		if (BIT_ProcessoSensorVacuoMesa)
		{
			SCRIPT_COLISAO_LOCAL_LAMINA_DESENHAR.STATIC_LAMINA_NA_MESA = false;
		}
	}
}
