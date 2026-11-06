using UnityEngine;
using UnityEngine.EventSystems;

public class SCRIPT_BOTOES_SINALIZADOR : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	public bool BANCADA_ESTOQUE;

	public bool BANCADA_PROCESSO;

	public bool BANCADA_QUALIDADE;

	public bool BANCADA_EXPEDICAO;

	public bool SOU_BOTAO_VERDE;

	public bool SOU_BOTAO_VERMELHO;

	public bool SOU_BOTAO_AZUL;

	public bool SOU_BOTAO_MANOPLA;

	public bool SOU_BOTAO_EMERGENCIA;

	private static bool ESTOQUE_BOTAO_EMERGENCIA_PRESSIONADO;

	private static bool ESTOQUE_BOTAO_MANOPLA_PRESSIONADO;

	public GameObject ESTOQUE_BOTAO_EMERGENCIA_3D;

	public GameObject ESTOQUE_BOTAO_MANOPLA_3D;

	public GameObject ESTOQUE_SINALEIRO_VERMELHO_3D;

	public GameObject ESTOQUE_SINALEIRO_AZUL_3D;

	public GameObject ESTOQUE_SINALEIRO_VERDE_3D;

	public AnimationClip ESTOQUE_BOTAO_EMERGENCIA_DOWN;

	public AnimationClip ESTOQUE_BOTAO_EMERGENCIA_UP;

	private static bool PROCESSO_BOTAO_EMERGENCIA_PRESSIONADO;

	private static bool PROCESSO_BOTAO_MANOPLA_PRESSIONADO;

	public GameObject PROCESSO_BOTAO_EMERGENCIA_3D;

	public GameObject PROCESSO_BOTAO_MANOPLA_3D;

	public GameObject PROCESSO_SINALEIRO_VERMELHO_3D;

	public GameObject PROCESSO_SINALEIRO_AZUL_3D;

	public GameObject PROCESSO_SINALEIRO_VERDE_3D;

	public AnimationClip PROCESSO_BOTAO_EMERGENCIA_DOWN;

	public AnimationClip PROCESSO_BOTAO_EMERGENCIA_UP;

	private static bool QUALIDADE_BOTAO_EMERGENCIA_PRESSIONADO;

	private static bool QUALIDADE_BOTAO_MANOPLA_PRESSIONADO;

	public GameObject QUALIDADE_BOTAO_EMERGENCIA_3D;

	public GameObject QUALIDADE_BOTAO_MANOPLA_3D;

	public GameObject QUALIDADE_SINALEIRO_VERMELHO_3D;

	public GameObject QUALIDADE_SINALEIRO_AZUL_3D;

	public GameObject QUALIDADE_SINALEIRO_VERDE_3D;

	public AnimationClip QUALIDADE_BOTAO_EMERGENCIA_DOWN;

	public AnimationClip QUALIDADE_BOTAO_EMERGENCIA_UP;

	private static bool EXPEDICAO_BOTAO_EMERGENCIA_PRESSIONADO;

	private static bool EXPEDICAO_BOTAO_MANOPLA_PRESSIONADO;

	public GameObject EXPEDICAO_BOTAO_EMERGENCIA_3D;

	public GameObject EXPEDICAO_BOTAO_MANOPLA_3D;

	public GameObject EXPEDICAO_SINALEIRO_VERMELHO_3D;

	public GameObject EXPEDICAO_SINALEIRO_AZUL_3D;

	public GameObject EXPEDICAO_SINALEIRO_VERDE_3D;

	public AnimationClip EXPEDICAO_BOTAO_EMERGENCIA_DOWN;

	public AnimationClip EXPEDICAO_BOTAO_EMERGENCIA_UP;

	public GameObject SOM_BOTAO_EMERGENCIA_DOWN;

	public GameObject SOM_BOTAO_EMERGENCIA_UP;

	public GameObject SOM_BOTAO_MANOPLA;

	public GameObject SOM_BOTAO_AZUL;

	public GameObject SOM_BOTAO_AZUL_UP;

	public bool MINIMIZAR_FUNCAO = true;

	public AnimationClip BOTAO_MANOPLA_ESQUERDA;

	public AnimationClip BOTAO_MANOPLA_DIREITA;

	public float VELOCIDADE_ANIMACAO_BOTAO_1 = 1f;

	public void OnPointerDown(PointerEventData eventData)
	{
		if (BANCADA_ESTOQUE)
		{
			if (SOU_BOTAO_VERDE)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueBotaoVerde = true;
				Object.Instantiate(SOM_BOTAO_AZUL, base.transform.position, base.transform.rotation);
			}
			if (SOU_BOTAO_VERMELHO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueBotaoVermelho = true;
				Object.Instantiate(SOM_BOTAO_AZUL, base.transform.position, base.transform.rotation);
			}
			if (SOU_BOTAO_AZUL)
			{
				if (!ESTOQUE_BOTAO_EMERGENCIA_PRESSIONADO)
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueSegurancaOk = true;
					ESTOQUE_SINALEIRO_VERMELHO_3D.SetActive(value: false);
					ESTOQUE_SINALEIRO_AZUL_3D.SetActive(value: false);
					ESTOQUE_SINALEIRO_VERDE_3D.SetActive(value: true);
				}
				Object.Instantiate(SOM_BOTAO_AZUL, base.transform.position, base.transform.rotation);
			}
		}
		if (BANCADA_PROCESSO)
		{
			if (SOU_BOTAO_VERDE)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoBotaoVerde = true;
				Object.Instantiate(SOM_BOTAO_AZUL, base.transform.position, base.transform.rotation);
			}
			if (SOU_BOTAO_VERMELHO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoBotaoVermelho = true;
				Object.Instantiate(SOM_BOTAO_AZUL, base.transform.position, base.transform.rotation);
			}
			if (SOU_BOTAO_AZUL)
			{
				if (!ESTOQUE_BOTAO_EMERGENCIA_PRESSIONADO)
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoSegurancaOk = true;
					PROCESSO_SINALEIRO_VERMELHO_3D.SetActive(value: false);
					PROCESSO_SINALEIRO_AZUL_3D.SetActive(value: false);
					PROCESSO_SINALEIRO_VERDE_3D.SetActive(value: true);
				}
				Object.Instantiate(SOM_BOTAO_AZUL, base.transform.position, base.transform.rotation);
			}
		}
		if (BANCADA_QUALIDADE)
		{
			if (SOU_BOTAO_VERDE)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeBotaoVerde = true;
				Object.Instantiate(SOM_BOTAO_AZUL, base.transform.position, base.transform.rotation);
			}
			if (SOU_BOTAO_VERMELHO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeBotaoVermelho = true;
				Object.Instantiate(SOM_BOTAO_AZUL, base.transform.position, base.transform.rotation);
			}
			if (SOU_BOTAO_AZUL)
			{
				if (!QUALIDADE_BOTAO_EMERGENCIA_PRESSIONADO)
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeSegurancaOk = true;
					QUALIDADE_SINALEIRO_VERMELHO_3D.SetActive(value: false);
					QUALIDADE_SINALEIRO_AZUL_3D.SetActive(value: false);
					QUALIDADE_SINALEIRO_VERDE_3D.SetActive(value: true);
				}
				Object.Instantiate(SOM_BOTAO_AZUL, base.transform.position, base.transform.rotation);
			}
		}
		if (!BANCADA_EXPEDICAO)
		{
			return;
		}
		if (SOU_BOTAO_VERDE)
		{
			SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoBotaoVerde = true;
			Object.Instantiate(SOM_BOTAO_AZUL, base.transform.position, base.transform.rotation);
		}
		if (SOU_BOTAO_VERMELHO)
		{
			SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoBotaoVermelho = true;
			Object.Instantiate(SOM_BOTAO_AZUL, base.transform.position, base.transform.rotation);
		}
		if (SOU_BOTAO_AZUL)
		{
			if (!EXPEDICAO_BOTAO_EMERGENCIA_PRESSIONADO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoSegurancaOk = true;
				EXPEDICAO_SINALEIRO_VERMELHO_3D.SetActive(value: false);
				EXPEDICAO_SINALEIRO_AZUL_3D.SetActive(value: false);
				EXPEDICAO_SINALEIRO_VERDE_3D.SetActive(value: true);
			}
			Object.Instantiate(SOM_BOTAO_AZUL, base.transform.position, base.transform.rotation);
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (BANCADA_ESTOQUE)
		{
			if (SOU_BOTAO_VERDE)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueBotaoVerde = false;
				Object.Instantiate(SOM_BOTAO_AZUL_UP, base.transform.position, base.transform.rotation);
			}
			if (SOU_BOTAO_VERMELHO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueBotaoVermelho = false;
				Object.Instantiate(SOM_BOTAO_AZUL_UP, base.transform.position, base.transform.rotation);
			}
			if (SOU_BOTAO_AZUL)
			{
				Object.Instantiate(SOM_BOTAO_AZUL_UP, base.transform.position, base.transform.rotation);
			}
		}
		if (BANCADA_PROCESSO)
		{
			if (SOU_BOTAO_VERDE)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoBotaoVerde = false;
				Object.Instantiate(SOM_BOTAO_AZUL_UP, base.transform.position, base.transform.rotation);
			}
			if (SOU_BOTAO_VERMELHO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoBotaoVermelho = false;
				Object.Instantiate(SOM_BOTAO_AZUL_UP, base.transform.position, base.transform.rotation);
			}
			if (SOU_BOTAO_AZUL)
			{
				Object.Instantiate(SOM_BOTAO_AZUL_UP, base.transform.position, base.transform.rotation);
			}
		}
		if (BANCADA_QUALIDADE)
		{
			if (SOU_BOTAO_VERDE)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeBotaoVerde = false;
				Object.Instantiate(SOM_BOTAO_AZUL_UP, base.transform.position, base.transform.rotation);
			}
			if (SOU_BOTAO_VERMELHO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeBotaoVermelho = false;
				Object.Instantiate(SOM_BOTAO_AZUL_UP, base.transform.position, base.transform.rotation);
			}
			if (SOU_BOTAO_AZUL)
			{
				Object.Instantiate(SOM_BOTAO_AZUL_UP, base.transform.position, base.transform.rotation);
			}
		}
		if (BANCADA_EXPEDICAO)
		{
			if (SOU_BOTAO_VERDE)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoBotaoVerde = false;
				Object.Instantiate(SOM_BOTAO_AZUL_UP, base.transform.position, base.transform.rotation);
			}
			if (SOU_BOTAO_VERMELHO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoBotaoVermelho = false;
				Object.Instantiate(SOM_BOTAO_AZUL_UP, base.transform.position, base.transform.rotation);
			}
			if (SOU_BOTAO_AZUL)
			{
				Object.Instantiate(SOM_BOTAO_AZUL_UP, base.transform.position, base.transform.rotation);
			}
		}
	}

	private void Start()
	{
		if (BANCADA_ESTOQUE)
		{
			if (SOU_BOTAO_EMERGENCIA)
			{
				ESTOQUE_BOTAO_EMERGENCIA_3D = GameObject.Find("ESTOQUE_BOTAO_EMERGENCIA_3D");
				ESTOQUE_BOTAO_EMERGENCIA_3D.AddComponent<Animation>();
				ESTOQUE_BOTAO_EMERGENCIA_3D.GetComponent<Animation>().AddClip(ESTOQUE_BOTAO_EMERGENCIA_DOWN, "ESTOQUE_BOTAO_EMERGENCIA_DOWN");
				ESTOQUE_BOTAO_EMERGENCIA_3D.GetComponent<Animation>().AddClip(ESTOQUE_BOTAO_EMERGENCIA_UP, "ESTOQUE_BOTAO_EMERGENCIA_UP");
			}
			if (SOU_BOTAO_MANOPLA)
			{
				ESTOQUE_BOTAO_MANOPLA_3D = GameObject.Find("ESTOQUE_BOTAO_MANOPLA_3D");
				ESTOQUE_BOTAO_MANOPLA_3D.AddComponent<Animation>();
				ESTOQUE_BOTAO_MANOPLA_3D.GetComponent<Animation>().AddClip(BOTAO_MANOPLA_ESQUERDA, "BOTAO_MANOPLA_ESQUERDA");
				ESTOQUE_BOTAO_MANOPLA_3D.GetComponent<Animation>().AddClip(BOTAO_MANOPLA_DIREITA, "BOTAO_MANOPLA_DIREITA");
			}
			ESTOQUE_SINALEIRO_VERMELHO_3D.SetActive(value: true);
			ESTOQUE_SINALEIRO_AZUL_3D.SetActive(value: true);
			ESTOQUE_SINALEIRO_VERDE_3D.SetActive(value: false);
			ESTOQUE_BOTAO_EMERGENCIA_PRESSIONADO = false;
			ESTOQUE_BOTAO_MANOPLA_PRESSIONADO = false;
		}
		if (BANCADA_PROCESSO)
		{
			if (SOU_BOTAO_EMERGENCIA)
			{
				PROCESSO_BOTAO_EMERGENCIA_3D = GameObject.Find("PROCESSO_BOTAO_EMERGENCIA_3D");
				PROCESSO_BOTAO_EMERGENCIA_3D.AddComponent<Animation>();
				PROCESSO_BOTAO_EMERGENCIA_3D.GetComponent<Animation>().AddClip(PROCESSO_BOTAO_EMERGENCIA_DOWN, "PROCESSO_BOTAO_EMERGENCIA_DOWN");
				PROCESSO_BOTAO_EMERGENCIA_3D.GetComponent<Animation>().AddClip(PROCESSO_BOTAO_EMERGENCIA_UP, "PROCESSO_BOTAO_EMERGENCIA_UP");
			}
			if (SOU_BOTAO_MANOPLA)
			{
				PROCESSO_BOTAO_MANOPLA_3D = GameObject.Find("PROCESSO_BOTAO_MANOPLA_3D");
				PROCESSO_BOTAO_MANOPLA_3D.AddComponent<Animation>();
				PROCESSO_BOTAO_MANOPLA_3D.GetComponent<Animation>().AddClip(BOTAO_MANOPLA_ESQUERDA, "BOTAO_MANOPLA_ESQUERDA");
				PROCESSO_BOTAO_MANOPLA_3D.GetComponent<Animation>().AddClip(BOTAO_MANOPLA_DIREITA, "BOTAO_MANOPLA_DIREITA");
			}
			PROCESSO_SINALEIRO_VERMELHO_3D.SetActive(value: true);
			PROCESSO_SINALEIRO_AZUL_3D.SetActive(value: true);
			PROCESSO_SINALEIRO_VERDE_3D.SetActive(value: false);
			PROCESSO_BOTAO_EMERGENCIA_PRESSIONADO = false;
			PROCESSO_BOTAO_MANOPLA_PRESSIONADO = false;
		}
		if (BANCADA_QUALIDADE)
		{
			if (SOU_BOTAO_EMERGENCIA)
			{
				QUALIDADE_BOTAO_EMERGENCIA_3D = GameObject.Find("QUALIDADE_BOTAO_EMERGENCIA_3D");
				QUALIDADE_BOTAO_EMERGENCIA_3D.AddComponent<Animation>();
				QUALIDADE_BOTAO_EMERGENCIA_3D.GetComponent<Animation>().AddClip(QUALIDADE_BOTAO_EMERGENCIA_DOWN, "QUALIDADE_BOTAO_EMERGENCIA_DOWN");
				QUALIDADE_BOTAO_EMERGENCIA_3D.GetComponent<Animation>().AddClip(QUALIDADE_BOTAO_EMERGENCIA_UP, "QUALIDADE_BOTAO_EMERGENCIA_UP");
			}
			if (SOU_BOTAO_MANOPLA)
			{
				QUALIDADE_BOTAO_MANOPLA_3D = GameObject.Find("QUALIDADE_BOTAO_MANOPLA_3D");
				QUALIDADE_BOTAO_MANOPLA_3D.AddComponent<Animation>();
				QUALIDADE_BOTAO_MANOPLA_3D.GetComponent<Animation>().AddClip(BOTAO_MANOPLA_ESQUERDA, "BOTAO_MANOPLA_ESQUERDA");
				QUALIDADE_BOTAO_MANOPLA_3D.GetComponent<Animation>().AddClip(BOTAO_MANOPLA_DIREITA, "BOTAO_MANOPLA_DIREITA");
			}
			QUALIDADE_SINALEIRO_VERMELHO_3D.SetActive(value: true);
			QUALIDADE_SINALEIRO_AZUL_3D.SetActive(value: true);
			QUALIDADE_SINALEIRO_VERDE_3D.SetActive(value: false);
			QUALIDADE_BOTAO_EMERGENCIA_PRESSIONADO = false;
			QUALIDADE_BOTAO_MANOPLA_PRESSIONADO = false;
		}
		if (BANCADA_EXPEDICAO)
		{
			if (SOU_BOTAO_EMERGENCIA)
			{
				EXPEDICAO_BOTAO_EMERGENCIA_3D = GameObject.Find("EXPEDICAO_BOTAO_EMERGENCIA_3D");
				EXPEDICAO_BOTAO_EMERGENCIA_3D.AddComponent<Animation>();
				EXPEDICAO_BOTAO_EMERGENCIA_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BOTAO_EMERGENCIA_DOWN, "EXPEDICAO_BOTAO_EMERGENCIA_DOWN");
				EXPEDICAO_BOTAO_EMERGENCIA_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BOTAO_EMERGENCIA_UP, "EXPEDICAO_BOTAO_EMERGENCIA_UP");
			}
			if (SOU_BOTAO_MANOPLA)
			{
				EXPEDICAO_BOTAO_MANOPLA_3D = GameObject.Find("EXPEDICAO_BOTAO_MANOPLA_3D");
				EXPEDICAO_BOTAO_MANOPLA_3D.AddComponent<Animation>();
				EXPEDICAO_BOTAO_MANOPLA_3D.GetComponent<Animation>().AddClip(BOTAO_MANOPLA_ESQUERDA, "BOTAO_MANOPLA_ESQUERDA");
				EXPEDICAO_BOTAO_MANOPLA_3D.GetComponent<Animation>().AddClip(BOTAO_MANOPLA_DIREITA, "BOTAO_MANOPLA_DIREITA");
			}
			EXPEDICAO_SINALEIRO_VERMELHO_3D.SetActive(value: true);
			EXPEDICAO_SINALEIRO_AZUL_3D.SetActive(value: true);
			EXPEDICAO_SINALEIRO_VERDE_3D.SetActive(value: false);
			EXPEDICAO_BOTAO_EMERGENCIA_PRESSIONADO = false;
			EXPEDICAO_BOTAO_MANOPLA_PRESSIONADO = false;
		}
	}

	public void MENSAGEM_BOTAO_EMERGENCIA_PRESSIONADO()
	{
		if (BANCADA_ESTOQUE && SOU_BOTAO_EMERGENCIA)
		{
			ESTOQUE_BOTAO_EMERGENCIA_PRESSIONADO = !ESTOQUE_BOTAO_EMERGENCIA_PRESSIONADO;
			if (ESTOQUE_BOTAO_EMERGENCIA_PRESSIONADO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueSegurancaOk = false;
				ESTOQUE_SINALEIRO_VERMELHO_3D.SetActive(value: true);
				ESTOQUE_SINALEIRO_AZUL_3D.SetActive(value: true);
				ESTOQUE_SINALEIRO_VERDE_3D.SetActive(value: false);
				Object.Instantiate(SOM_BOTAO_EMERGENCIA_DOWN, base.transform.position, base.transform.rotation);
			}
			if (!ESTOQUE_BOTAO_EMERGENCIA_PRESSIONADO)
			{
				Object.Instantiate(SOM_BOTAO_EMERGENCIA_UP, base.transform.position, base.transform.rotation);
			}
		}
		if (BANCADA_PROCESSO && SOU_BOTAO_EMERGENCIA)
		{
			PROCESSO_BOTAO_EMERGENCIA_PRESSIONADO = !PROCESSO_BOTAO_EMERGENCIA_PRESSIONADO;
			if (PROCESSO_BOTAO_EMERGENCIA_PRESSIONADO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoSegurancaOk = false;
				PROCESSO_SINALEIRO_VERMELHO_3D.SetActive(value: true);
				PROCESSO_SINALEIRO_AZUL_3D.SetActive(value: true);
				PROCESSO_SINALEIRO_VERDE_3D.SetActive(value: false);
				Object.Instantiate(SOM_BOTAO_EMERGENCIA_DOWN, base.transform.position, base.transform.rotation);
			}
			if (!PROCESSO_BOTAO_EMERGENCIA_PRESSIONADO)
			{
				Object.Instantiate(SOM_BOTAO_EMERGENCIA_UP, base.transform.position, base.transform.rotation);
			}
		}
		if (BANCADA_QUALIDADE && SOU_BOTAO_EMERGENCIA)
		{
			QUALIDADE_BOTAO_EMERGENCIA_PRESSIONADO = !QUALIDADE_BOTAO_EMERGENCIA_PRESSIONADO;
			if (QUALIDADE_BOTAO_EMERGENCIA_PRESSIONADO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeSegurancaOk = false;
				QUALIDADE_SINALEIRO_VERMELHO_3D.SetActive(value: true);
				QUALIDADE_SINALEIRO_AZUL_3D.SetActive(value: true);
				QUALIDADE_SINALEIRO_VERDE_3D.SetActive(value: false);
				Object.Instantiate(SOM_BOTAO_EMERGENCIA_DOWN, base.transform.position, base.transform.rotation);
			}
			if (!QUALIDADE_BOTAO_EMERGENCIA_PRESSIONADO)
			{
				Object.Instantiate(SOM_BOTAO_EMERGENCIA_UP, base.transform.position, base.transform.rotation);
			}
		}
		if (BANCADA_EXPEDICAO && SOU_BOTAO_EMERGENCIA)
		{
			EXPEDICAO_BOTAO_EMERGENCIA_PRESSIONADO = !EXPEDICAO_BOTAO_EMERGENCIA_PRESSIONADO;
			if (EXPEDICAO_BOTAO_EMERGENCIA_PRESSIONADO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoSegurancaOk = false;
				EXPEDICAO_SINALEIRO_VERMELHO_3D.SetActive(value: true);
				EXPEDICAO_SINALEIRO_AZUL_3D.SetActive(value: true);
				EXPEDICAO_SINALEIRO_VERDE_3D.SetActive(value: false);
				Object.Instantiate(SOM_BOTAO_EMERGENCIA_DOWN, base.transform.position, base.transform.rotation);
			}
			if (!EXPEDICAO_BOTAO_EMERGENCIA_PRESSIONADO)
			{
				Object.Instantiate(SOM_BOTAO_EMERGENCIA_UP, base.transform.position, base.transform.rotation);
			}
		}
	}

	public void MENSAGEM_BOTAO_MANOPLA_PRESSIONADO()
	{
		if (BANCADA_ESTOQUE && SOU_BOTAO_MANOPLA)
		{
			ESTOQUE_BOTAO_MANOPLA_PRESSIONADO = !ESTOQUE_BOTAO_MANOPLA_PRESSIONADO;
			if (ESTOQUE_BOTAO_MANOPLA_PRESSIONADO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueManoplaAutomatico = true;
				Object.Instantiate(SOM_BOTAO_MANOPLA, base.transform.position, base.transform.rotation);
			}
			if (!ESTOQUE_BOTAO_MANOPLA_PRESSIONADO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueManoplaAutomatico = false;
				Object.Instantiate(SOM_BOTAO_MANOPLA, base.transform.position, base.transform.rotation);
			}
		}
		if (BANCADA_PROCESSO && SOU_BOTAO_MANOPLA)
		{
			PROCESSO_BOTAO_MANOPLA_PRESSIONADO = !PROCESSO_BOTAO_MANOPLA_PRESSIONADO;
			if (PROCESSO_BOTAO_MANOPLA_PRESSIONADO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoManoplaAutomatico = true;
				Object.Instantiate(SOM_BOTAO_MANOPLA, base.transform.position, base.transform.rotation);
			}
			if (!PROCESSO_BOTAO_MANOPLA_PRESSIONADO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoManoplaAutomatico = false;
				Object.Instantiate(SOM_BOTAO_MANOPLA, base.transform.position, base.transform.rotation);
			}
		}
		if (BANCADA_QUALIDADE && SOU_BOTAO_MANOPLA)
		{
			QUALIDADE_BOTAO_MANOPLA_PRESSIONADO = !QUALIDADE_BOTAO_MANOPLA_PRESSIONADO;
			if (QUALIDADE_BOTAO_MANOPLA_PRESSIONADO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeManoplaAutomatico = true;
				Object.Instantiate(SOM_BOTAO_MANOPLA, base.transform.position, base.transform.rotation);
			}
			if (!QUALIDADE_BOTAO_MANOPLA_PRESSIONADO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeManoplaAutomatico = false;
				Object.Instantiate(SOM_BOTAO_MANOPLA, base.transform.position, base.transform.rotation);
			}
		}
		if (BANCADA_EXPEDICAO && SOU_BOTAO_MANOPLA)
		{
			EXPEDICAO_BOTAO_MANOPLA_PRESSIONADO = !EXPEDICAO_BOTAO_MANOPLA_PRESSIONADO;
			if (EXPEDICAO_BOTAO_MANOPLA_PRESSIONADO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoManoplaAutomatico = true;
				Object.Instantiate(SOM_BOTAO_MANOPLA, base.transform.position, base.transform.rotation);
			}
			if (!EXPEDICAO_BOTAO_MANOPLA_PRESSIONADO)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoManoplaAutomatico = false;
				Object.Instantiate(SOM_BOTAO_MANOPLA, base.transform.position, base.transform.rotation);
			}
		}
	}

	private void Update()
	{
		if (BANCADA_ESTOQUE)
		{
			if (SOU_BOTAO_EMERGENCIA)
			{
				if (ESTOQUE_BOTAO_EMERGENCIA_PRESSIONADO)
				{
					ESTOQUE_BOTAO_EMERGENCIA_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BOTAO_EMERGENCIA_DOWN", VELOCIDADE_ANIMACAO_BOTAO_1);
				}
				if (!ESTOQUE_BOTAO_EMERGENCIA_PRESSIONADO)
				{
					ESTOQUE_BOTAO_EMERGENCIA_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BOTAO_EMERGENCIA_UP", VELOCIDADE_ANIMACAO_BOTAO_1);
				}
			}
			if (SOU_BOTAO_MANOPLA)
			{
				if (ESTOQUE_BOTAO_MANOPLA_PRESSIONADO)
				{
					ESTOQUE_BOTAO_MANOPLA_3D.GetComponent<Animation>().CrossFade("BOTAO_MANOPLA_DIREITA", VELOCIDADE_ANIMACAO_BOTAO_1);
				}
				if (!ESTOQUE_BOTAO_MANOPLA_PRESSIONADO)
				{
					ESTOQUE_BOTAO_MANOPLA_3D.GetComponent<Animation>().CrossFade("BOTAO_MANOPLA_ESQUERDA", VELOCIDADE_ANIMACAO_BOTAO_1);
				}
			}
		}
		if (BANCADA_PROCESSO)
		{
			if (SOU_BOTAO_EMERGENCIA)
			{
				if (PROCESSO_BOTAO_EMERGENCIA_PRESSIONADO)
				{
					PROCESSO_BOTAO_EMERGENCIA_3D.GetComponent<Animation>().CrossFade("PROCESSO_BOTAO_EMERGENCIA_DOWN", VELOCIDADE_ANIMACAO_BOTAO_1);
				}
				if (!PROCESSO_BOTAO_EMERGENCIA_PRESSIONADO)
				{
					PROCESSO_BOTAO_EMERGENCIA_3D.GetComponent<Animation>().CrossFade("PROCESSO_BOTAO_EMERGENCIA_UP", VELOCIDADE_ANIMACAO_BOTAO_1);
				}
			}
			if (SOU_BOTAO_MANOPLA)
			{
				if (PROCESSO_BOTAO_MANOPLA_PRESSIONADO)
				{
					PROCESSO_BOTAO_MANOPLA_3D.GetComponent<Animation>().CrossFade("BOTAO_MANOPLA_DIREITA", VELOCIDADE_ANIMACAO_BOTAO_1);
				}
				if (!PROCESSO_BOTAO_MANOPLA_PRESSIONADO)
				{
					PROCESSO_BOTAO_MANOPLA_3D.GetComponent<Animation>().CrossFade("BOTAO_MANOPLA_ESQUERDA", VELOCIDADE_ANIMACAO_BOTAO_1);
				}
			}
		}
		if (BANCADA_QUALIDADE)
		{
			if (SOU_BOTAO_EMERGENCIA)
			{
				if (QUALIDADE_BOTAO_EMERGENCIA_PRESSIONADO)
				{
					QUALIDADE_BOTAO_EMERGENCIA_3D.GetComponent<Animation>().CrossFade("QUALIDADE_BOTAO_EMERGENCIA_DOWN", VELOCIDADE_ANIMACAO_BOTAO_1);
				}
				if (!QUALIDADE_BOTAO_EMERGENCIA_PRESSIONADO)
				{
					QUALIDADE_BOTAO_EMERGENCIA_3D.GetComponent<Animation>().CrossFade("QUALIDADE_BOTAO_EMERGENCIA_UP", VELOCIDADE_ANIMACAO_BOTAO_1);
				}
			}
			if (SOU_BOTAO_MANOPLA)
			{
				if (QUALIDADE_BOTAO_MANOPLA_PRESSIONADO)
				{
					QUALIDADE_BOTAO_MANOPLA_3D.GetComponent<Animation>().CrossFade("BOTAO_MANOPLA_DIREITA", VELOCIDADE_ANIMACAO_BOTAO_1);
				}
				if (!QUALIDADE_BOTAO_MANOPLA_PRESSIONADO)
				{
					QUALIDADE_BOTAO_MANOPLA_3D.GetComponent<Animation>().CrossFade("BOTAO_MANOPLA_ESQUERDA", VELOCIDADE_ANIMACAO_BOTAO_1);
				}
			}
		}
		if (!BANCADA_EXPEDICAO)
		{
			return;
		}
		if (SOU_BOTAO_EMERGENCIA)
		{
			if (EXPEDICAO_BOTAO_EMERGENCIA_PRESSIONADO)
			{
				EXPEDICAO_BOTAO_EMERGENCIA_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BOTAO_EMERGENCIA_DOWN", VELOCIDADE_ANIMACAO_BOTAO_1);
			}
			if (!EXPEDICAO_BOTAO_EMERGENCIA_PRESSIONADO)
			{
				EXPEDICAO_BOTAO_EMERGENCIA_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BOTAO_EMERGENCIA_UP", VELOCIDADE_ANIMACAO_BOTAO_1);
			}
		}
		if (SOU_BOTAO_MANOPLA)
		{
			if (EXPEDICAO_BOTAO_MANOPLA_PRESSIONADO)
			{
				EXPEDICAO_BOTAO_MANOPLA_3D.GetComponent<Animation>().CrossFade("BOTAO_MANOPLA_DIREITA", VELOCIDADE_ANIMACAO_BOTAO_1);
			}
			if (!EXPEDICAO_BOTAO_MANOPLA_PRESSIONADO)
			{
				EXPEDICAO_BOTAO_MANOPLA_3D.GetComponent<Animation>().CrossFade("BOTAO_MANOPLA_ESQUERDA", VELOCIDADE_ANIMACAO_BOTAO_1);
			}
		}
	}
}
