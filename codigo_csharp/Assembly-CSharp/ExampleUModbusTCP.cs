using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Auttom.Stopper.Logic;
using Comunication.NamedPipe;
using ComunicationInterface;
using Modbus;
using UnityEngine;
using UnityEngine.UI;

public class ExampleUModbusTCP : MonoBehaviour
{
	private CancellationTokenSource cancellationSource;

	private CancellationToken cancellationToken;

	private Task atualizacaoTask;

	private ICommunication COMUNICACAO;

	private PipeClient PIPE_CLIENT;

	private SmartDN BANCADA;

	private SmartDN BACANDA_N2;

	private string SENHA = "5d410190959d2289ab3688ea0dc4d9e2";

	public float DELAY_LER_COMUNICACAO = 0.1f;

	public static int DROPDOWN_TIPO_DE_BANCADA = 0;

	private bool OFFSET;

	private int OFFSET_ENTRADAS;

	private int OFFSET_SAIDAS;

	public static bool PLC_SIM = false;

	public GameObject TEXTO_PLC_SIM_ON;

	public GameObject TEXTO_PLC_SIM_OFF;

	public GameObject POP_UP_TEXTO_SIEMENS;

	private int PORTA_COMUNICACAO = 102;

	private Stopper ESTOQUE_STOPPER;

	public static int ESTOQUE_BLOCO_OPERACAO_ATUAL;

	public static int ESTOQUE_BLOCO_PEDIDO_ATUAL;

	public GameObject TEXTFIELD_POSICAO_DO_BLOCO;

	public string VALOR_BIT_EstoqueStatusBancada = "0";

	public static int PROCESSO_BLOCO_OPERACAO_ATUAL;

	public static int PROCESSO_BLOCO_PEDIDO_ATUAL;

	public string VALOR_BIT_ProcessoStatusBancada = "0";

	private Stopper QUALIDADE_STOPPER;

	public static int QUALIDADE_BLOCO_OPERACAO_ATUAL;

	public static int QUALIDADE_BLOCO_PEDIDO_ATUAL;

	public string VALOR_BIT_QualidadeStatusBancada = "0";

	private Stopper EXPEDICAO_STOPPER;

	public static int EXPEDICAO_BLOCO_OPERACAO_ATUAL;

	public static int EXPEDICAO_BLOCO_PEDIDO_ATUAL;

	public GameObject TEXTFIELD_POSICAO_PARA_GUARDAR;

	public string VALOR_BIT_ExpedicaoStatusBancada = "0";

	public bool MINIMIZAR_FUNCAO = true;

	public bool ler;

	public float timer;

	public bool LER_COMUNICACAO;

	public bool PODE_GRAVAR_MODBUS = true;

	public InputField inputIP;

	public InputField inputPort;

	public InputField inputAddress;

	public InputField inputResponseValue;

	public GameObject LER_GRAVAR;

	public static string ADDRESS_PARA_LER = "1";

	private UModbusTCP m_oUModbusTCP;

	private UModbusTCP.ResponseData m_oUModbusTCPResponse;

	private UModbusTCP.ExceptionData m_oUModbusTCPException;

	private bool m_bUpdateResponse;

	private int m_iResponseValue;

	private ModbusSlave server;

	private void Start()
	{
		LER_COMUNICACAO = false;
		string iP = IPManager.GetIP(ADDRESSFAM.IPv4);
		inputIP.text = iP;
		inputPort.text = "502";
	}

	public void BOTAO_PLC_SIM()
	{
		PLC_SIM = !PLC_SIM;
		if (!PLC_SIM)
		{
			TEXTO_PLC_SIM_OFF.SetActive(value: true);
			TEXTO_PLC_SIM_ON.SetActive(value: false);
			POP_UP_TEXTO_SIEMENS.SetActive(value: false);
			PORTA_COMUNICACAO = 102;
			PIPE_CLIENT?.End();
		}
		if (PLC_SIM)
		{
			inputIP.text = "127.0.0.1";
			TEXTO_PLC_SIM_OFF.SetActive(value: false);
			TEXTO_PLC_SIM_ON.SetActive(value: true);
			POP_UP_TEXTO_SIEMENS.SetActive(value: true);
			string path = "Gateway/Gateway.exe";
			string dataPath = Application.dataPath;
			Process process = new Process();
			process.StartInfo.FileName = Path.Combine(dataPath, path);
			process.Start();
		}
	}

	public void ENCERROU_SIEMENS()
	{
		PLC_SIM = false;
		TEXTO_PLC_SIM_OFF.SetActive(value: true);
		TEXTO_PLC_SIM_ON.SetActive(value: false);
		POP_UP_TEXTO_SIEMENS.SetActive(value: false);
	}

	private void Update()
	{
		if (LER_COMUNICACAO)
		{
			if (SCRIPT_TOGGLES.VAR_CHECKBOX_REMOVER_BLOCO)
			{
				VOID_BOTAO_REMOVER_BLOCO();
			}
			if (MINIMIZAR_FUNCAO)
			{
				_ = SCRIPT_TOGGLES.ATIVAR_BIT_EstoquePecaEsteira;
				_ = SCRIPT_TOGGLES.ATIVAR_BIT_QualidadePecaEsteira;
				_ = SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoPecaEsteira;
			}
			if (MINIMIZAR_FUNCAO)
			{
				_ = SCRIPT_TOGGLES.ATIVAR_BIT_EstoquePecaEsteira;
				_ = SCRIPT_TOGGLES.ATIVAR_BIT_QualidadePecaEsteira;
				_ = SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoPecaEsteira;
			}
		}
		if (m_bUpdateResponse)
		{
			m_bUpdateResponse = false;
			inputResponseValue.text = m_iResponseValue.ToString();
		}
	}

	public void BOTAO_LER_DADOS()
	{
		try
		{
			if (Administracao.COMUNICACAO_MODBUS_SLAVE)
			{
				MonoBehaviour.print("LER [MODBUS SLAVE]");
				if (!LER_COMUNICACAO)
				{
					_ = inputIP.text;
					COMUNICACAO = new ModbusSlaveCommunication(SENHA);
					BANCADA = new SmartDN(COMUNICACAO);
					COMUNICACAO.Conectar();
					VERIFICACAO_BANCADA();
					cancellationSource = new CancellationTokenSource();
					cancellationToken = cancellationSource.Token;
					atualizacaoTask = Task.Factory.StartNew((Func<Task>)VOID_LER_DADOS_COMUNICACAO, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
				}
				LER_COMUNICACAO = true;
			}
			else if (Administracao.COMUNICACAO_MODBUS_MASTER)
			{
				MonoBehaviour.print("LER [MODBUS MASTER]");
				if (!LER_COMUNICACAO)
				{
					string text = inputIP.text;
					COMUNICACAO = new ModbusMasterCommunication(text, SENHA);
					BANCADA = new SmartDN(COMUNICACAO);
					COMUNICACAO.Conectar();
					VERIFICACAO_BANCADA();
					cancellationSource = new CancellationTokenSource();
					cancellationToken = cancellationSource.Token;
					atualizacaoTask = Task.Factory.StartNew((Func<Task>)VOID_LER_DADOS_COMUNICACAO, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
				}
				LER_COMUNICACAO = true;
			}
			else
			{
				if (!Administracao.COMUNICACAO_SIEMENS)
				{
					return;
				}
				MonoBehaviour.print("LER [SIEMENS]");
				if (!LER_COMUNICACAO)
				{
					string text2 = inputIP.text;
					if (PLC_SIM)
					{
						PIPE_CLIENT = new PipeClient();
						PIPE_CLIENT.Start();
						PORTA_COMUNICACAO = PIPE_CLIENT.serverPort;
					}
					COMUNICACAO = new SiemensShap7Comunication(text2, PORTA_COMUNICACAO, SENHA);
					BANCADA = new SmartDN(COMUNICACAO);
					COMUNICACAO.Conectar();
					VERIFICACAO_BANCADA();
					cancellationSource = new CancellationTokenSource();
					cancellationToken = cancellationSource.Token;
					atualizacaoTask = Task.Factory.StartNew((Func<Task>)VOID_LER_DADOS_COMUNICACAO, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
				}
				LER_COMUNICACAO = true;
			}
		}
		catch (Exception ex)
		{
			Administracao.MOSTRAR_BOTAO_ERRO_DE_COMUNICACAO = true;
			Administracao.MENSAGEM_ERRO_DE_COMUNICACAO = ex.Message;
		}
	}

	private void VERIFICACAO_BANCADA()
	{
		if (DROPDOWN_TIPO_DE_BANCADA == 0)
		{
			OFFSET = false;
		}
		else if (DROPDOWN_TIPO_DE_BANCADA == 1)
		{
			OFFSET = true;
			OFFSET_ENTRADAS = 0;
			OFFSET_SAIDAS = 10;
		}
		else if (DROPDOWN_TIPO_DE_BANCADA == 2)
		{
			OFFSET = true;
			OFFSET_ENTRADAS = 20;
			OFFSET_SAIDAS = 30;
		}
		else if (DROPDOWN_TIPO_DE_BANCADA == 3)
		{
			OFFSET = true;
			OFFSET_ENTRADAS = 40;
			OFFSET_SAIDAS = 55;
		}
		else if (DROPDOWN_TIPO_DE_BANCADA == 4)
		{
			OFFSET = true;
			OFFSET_ENTRADAS = 60;
			OFFSET_SAIDAS = 70;
		}
	}

	private IEnumerator COROUTINE_LER_COMUNICACAO()
	{
		while (true)
		{
			MonoBehaviour.print("LENDO COROUTINE");
			VOID_LER_DADOS_COMUNICACAO();
			yield return new WaitForSeconds(DELAY_LER_COMUNICACAO);
		}
	}

	private async Task VOID_LER_DADOS_COMUNICACAO()
	{
		MonoBehaviour.print("LENDO VOID CHAMADO PELA COROUTINE");
		do
		{
			try
			{
				if (LER_COMUNICACAO)
				{
					COMUNICACAO.AtualizarDados(OFFSET, OFFSET_ENTRADAS, OFFSET_SAIDAS);
					MonoBehaviour.print("OFFSET" + OFFSET);
					MonoBehaviour.print("OFFSET_ENTRADAS" + OFFSET_ENTRADAS);
					MonoBehaviour.print("OFFSET_SAIDAS" + OFFSET_SAIDAS);
					if (MINIMIZAR_FUNCAO)
					{
						BANCADA.EstoqueSegurancaOk = SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueSegurancaOk;
						BANCADA.EstoqueGarraAvancada = SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueGarraAvancada;
						BANCADA.EstoqueGarraRecuada = SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueGarraRecuada;
						BANCADA.EstoqueGarraAberta = SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueGarraAberta;
						BANCADA.EstoqueGarraFechada = SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueGarraFechada;
						BANCADA.EstoqueAlimentadorAvancado = SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAlimentadorAvancado;
						BANCADA.EstoqueAlimentadorBuffer = SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAlimentadorBuffer;
						BANCADA.EstoqueAlimentadorRecuado = SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAlimentadorRecuado;
						BANCADA.EstoqueSensorVacuo = SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueSensorVacuo;
						BANCADA.EstoqueManoplaAutomatico = SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueManoplaAutomatico;
						BANCADA.EstoqueBotaoVerde = SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueBotaoVerde;
						BANCADA.EstoqueBotaoVermelho = SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueBotaoVermelho;
						BANCADA.EstoquePecaEsteira = SCRIPT_TOGGLES.ATIVAR_BIT_EstoquePecaEsteira;
						BANCADA.EstoqueNumeroPedido = (ushort)SCRIPT_TRIGGERS.ESTOQUE_NUMERO_DO_PEDIDO_PARA_MANDAR_MODBUS;
						BANCADA.EstoqueAtuadorEmPosicao = SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAtuadorEmPosicao;
						BANCADA.EstoqueAtuadorOcupado = SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAtuadorOcupado;
						SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAvancaAlimentador = BANCADA.EstoqueAvancaAlimentador;
						SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueRecuaAlimentador = BANCADA.EstoqueRecuaAlimentador;
						SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAcionaVacuo = BANCADA.EstoqueAcionaVacuo;
						SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAvancaGarra = BANCADA.EstoqueAvancaGarra;
						SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueFechaGarra = BANCADA.EstoqueFechaGarra;
						SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueEsteira1 = BANCADA.EstoqueEsteira1;
						SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueEsteira2 = BANCADA.EstoqueEsteira2;
						SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueDriveAtuador = BANCADA.EstoqueDriveAtuador;
						SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador = BANCADA.EstoquePosicaoAtuador.ToString();
						SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueStopper1 = BANCADA.EstoqueValvula1Stopper;
						SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueStopper2 = BANCADA.EstoqueValvula2Stopper;
					}
					if (MINIMIZAR_FUNCAO)
					{
						BANCADA.ProcessoSegurancaOk = SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoSegurancaOk;
						BANCADA.ProcessoEixoZAvancado = SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAlimentadorAvancado;
						BANCADA.ProcessoEixoZRecuado = SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAlimentadorRecuado;
						BANCADA.ProcessoSensorVacuoManipulador = SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoSensorVacuo;
						BANCADA.ProcessoSensorVacuoMesa = SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoSensorVacuoMesa;
						BANCADA.ProcessoManoplaAutomatico = SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoManoplaAutomatico;
						BANCADA.ProcessoBotaoVerde = SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoBotaoVerde;
						BANCADA.ProcessoBotaoVermelho = SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoBotaoVermelho;
						BANCADA.ProcessoAtuadorEmPosicao = SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAtuadorEmPosicao;
						BANCADA.ProcessoAtuadorOcupado = SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAtuadorOcupado;
						SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAvancaAlimentador = BANCADA.ProcessoAvancaEixoZ;
						SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAcionaVacuoManipulador = BANCADA.ProcessoAcionaVacuoManipulador;
						SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAcionaVacuoMesa = BANCADA.ProcessoAcionaVacuoMesa;
						SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoEsteira1 = BANCADA.ProcessoEsteira1;
						SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoEsteira2 = BANCADA.ProcessoEsteira2;
						SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoDriveAtuador = BANCADA.ProcessoDriveAtuador;
						SCRIPT_TOGGLES.VALOR_BIT_ProcessoPosicaoAtuador = BANCADA.ProcessoPosicaoAtuador.ToString();
					}
					if (MINIMIZAR_FUNCAO)
					{
						BANCADA.MontagemSegurancaOk = SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeSegurancaOk;
						BANCADA.MontagemAlimentadorAvancado = SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeAlimentadorAvancado;
						BANCADA.MontagemAlimentadorBuffer = SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeALimentadorBuffer;
						BANCADA.MontagemAlimentadorRecuado = SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeAlimentadorRecuado;
						BANCADA.MontagemSensorVacuo = SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeSensorVacuo;
						BANCADA.MontagemManoplaAutomatico = SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeManoplaAutomatico;
						BANCADA.MontagemBotaoVerde = SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeBotaoVerde;
						BANCADA.MontagemBotaoVermelho = SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeBotaoVermelho;
						BANCADA.MontagemPecaEsteira = SCRIPT_TOGGLES.ATIVAR_BIT_QualidadePecaEsteira;
						BANCADA.MontagemNumeroPedido = (ushort)SCRIPT_TRIGGERS.MONTAGEM_NUMERO_DO_PEDIDO_PARA_MANDAR_MODBUS;
						BANCADA.MontagemRoboEmPosicao = SCRIPT_TOGGLES.ATIVAR_BIT_MontagemRoboEmPosicao;
						BANCADA.MontagemRoboOcupado = SCRIPT_TOGGLES.ATIVAR_BIT_MontagemRoboOcupado;
						BANCADA.MontagemResultadoInspecao = SCRIPT_TOGGLES.ATIVAR_BIT_MontagemResultadoInspecao;
						BANCADA.MontagemAndarBloco = Convert.ToUInt16(int.Parse(SCRIPT_QUALIDADE_DADOS.DESEJADO_ANDAR_DO_BLOCO.ToString()));
						BANCADA.MontagemTotalBlocos = Convert.ToUInt16(int.Parse(SCRIPT_QUALIDADE_DADOS.DESEJADO_TOTAL_DE_ANDARES.ToString()));
						BANCADA.MontagemCorLamina1 = Convert.ToUInt16(int.Parse(SCRIPT_QUALIDADE_DADOS.DESEJADO_COR_LAMINA_1.ToString()));
						BANCADA.MontagemCorLamina2 = Convert.ToUInt16(int.Parse(SCRIPT_QUALIDADE_DADOS.DESEJADO_COR_LAMINA_2.ToString()));
						BANCADA.MontagemCorLamina3 = Convert.ToUInt16(int.Parse(SCRIPT_QUALIDADE_DADOS.DESEJADO_COR_LAMINA_3.ToString()));
						BANCADA.MontagemPadraoLamina1 = Convert.ToUInt16(int.Parse(SCRIPT_QUALIDADE_DADOS.DESEJADO_PADRAO_LAMINA_1.ToString()));
						BANCADA.MontagemPadraoLamina2 = Convert.ToUInt16(int.Parse(SCRIPT_QUALIDADE_DADOS.DESEJADO_PADRAO_LAMINA_2.ToString()));
						BANCADA.MontagemPadraoLamina3 = Convert.ToUInt16(int.Parse(SCRIPT_QUALIDADE_DADOS.DESEJADO_PADRAO_LAMINA_3.ToString()));
						SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeAvancaAlimentador = BANCADA.MontagemAvancaAlimentador;
						SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeRecuaAlimentador = BANCADA.MontagemRecuaAlimentador;
						SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeAcionaVacuo = BANCADA.MontagemAcionaVacuo;
						SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeEsteira1 = BANCADA.MontagemEsteira1;
						SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeEsteira2 = BANCADA.MontagemEsteira2;
						SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeStopper1 = BANCADA.MontagemValvula1Stopper;
						SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeStopper2 = BANCADA.MontagemValvula2Stopper;
						SCRIPT_TOGGLES.ATIVAR_BIT_MontagemDriveRobo = BANCADA.MontagemDriveRobo;
						SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo = BANCADA.MontagemPosicaoRobo.ToString();
						SCRIPT_TOGGLES.VALOR_BIT_MontagemCorEsperada = BANCADA.MontagemCorEsperada.ToString();
						SCRIPT_TOGGLES.ATIVAR_BIT_MontagemTriggerCamera = BANCADA.MontagemTriggerCamera;
					}
					if (MINIMIZAR_FUNCAO)
					{
						BANCADA.ExpedicaoSegurancaOk = SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoSegurancaOk;
						BANCADA.ExpedicaoAlimentadorAvancado = SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAlimentadorAvancado;
						BANCADA.ExpedicaoAlimentadorRecuado = SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAlimentadorRecuado;
						BANCADA.ExpedicaoSensorVacuo = SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoSensorVacuo;
						BANCADA.ExpedicaoManoplaAutomatico = SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoManoplaAutomatico;
						BANCADA.ExpedicaoBotaoVerde = SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoBotaoVerde;
						BANCADA.ExpedicaoBotaoVermelho = SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoBotaoVermelho;
						BANCADA.ExpedicaoPecaEsteira = SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoPecaEsteira;
						BANCADA.ExpedicaoNumeroPedido = (ushort)SCRIPT_TRIGGERS.EXPEDICAO_NUMERO_DO_PEDIDO_PARA_MANDAR_MODBUS;
						BANCADA.ExpedicaoAtuadorEmPosicao = SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAtuadorEmPosicao;
						BANCADA.ExpedicaoAtuadorOcupado = SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAtuadorOcupado;
						SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAvancaAlimentador = BANCADA.ExpedicaoAvancaAlimentador;
						SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAcionaVacuo = BANCADA.ExpedicaoAcionaVacuo;
						SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoEsteira1 = BANCADA.ExpedicaoEsteira1;
						SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoEsteira2 = BANCADA.ExpedicaoEsteira2;
						SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoDriveAtuador = BANCADA.ExpedicaoDriveAtuador;
						SCRIPT_TOGGLES.VALOR_BIT_ExpedicaoPosicaoAtuador = BANCADA.ExpedicaoPosicaoAtuador.ToString();
						SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoStopper1 = BANCADA.ExpedicaoValvula1Stopper;
						SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoStopper2 = BANCADA.ExpedicaoValvula2Stopper;
					}
					await Task.Delay(20);
					MonoBehaviour.print("atualizando dados");
				}
			}
			catch (ObjectDisposedException ex)
			{
				MonoBehaviour.print(ex.Message);
			}
			catch (Exception ex2)
			{
				Administracao.MOSTRAR_BOTAO_ERRO_DE_COMUNICACAO = true;
				Administracao.MENSAGEM_ERRO_DE_COMUNICACAO = ex2.Message;
				PARAR_MODBUS();
			}
		}
		while (!cancellationToken.IsCancellationRequested);
	}

	private void UModbusTCPOnResponseData(ushort _oID, byte _oUnit, byte _oFunction, byte[] _oValues)
	{
		int num = _oValues[8];
		byte[] array = new byte[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = _oValues[9 + i];
		}
		int[] intsOfBytes = UModbusTCPHelpers.GetIntsOfBytes(array);
		m_iResponseValue = intsOfBytes[0];
		m_bUpdateResponse = true;
	}

	private void UModbusTCPOnException(ushort _oID, byte _oUnit, byte _oFunction, byte _oException)
	{
	}

	public void VOID_BOTAO_REMOVER_BLOCO()
	{
		_ = TEXTFIELD_POSICAO_DO_BLOCO.GetComponent<InputField>().text != "";
		MonoBehaviour.print("BOTAO_REMOVER_BLOCO");
	}

	private void OnApplicationQuit()
	{
		if (COMUNICACAO != null && COMUNICACAO.Conectado)
		{
			cancellationSource?.Cancel();
			atualizacaoTask?.Wait();
			PIPE_CLIENT?.End();
			COMUNICACAO.Desconectar();
		}
	}

	public void PARAR_MODBUS()
	{
		if (COMUNICACAO != null && COMUNICACAO.Conectado)
		{
			LER_COMUNICACAO = false;
			cancellationSource?.Cancel();
			atualizacaoTask?.Wait();
			PIPE_CLIENT?.End();
			COMUNICACAO.Desconectar();
			MonoBehaviour.print("ENCERRAR");
		}
	}
}
