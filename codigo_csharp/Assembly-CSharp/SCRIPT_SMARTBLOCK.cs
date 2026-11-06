using System.Collections;
using UnityEngine;

public class SCRIPT_SMARTBLOCK : MonoBehaviour
{
	[Header("DADOS DESEJADOS PARA O BLOCO")]
	public int COR_DO_BLOCO;

	public int COR_DA_LAMINA_1_DESEJADO;

	public int COR_DA_LAMINA_2_DESEJADO;

	public int COR_DA_LAMINA_3_DESEJADO;

	public int ANDAR_DO_BLOCO_DESEJADO;

	public int TOTAL_DE_ANDARES_DESEJADO;

	public int NUMERO_OPERACAO_DESEJADO;

	public int DEBUG_NUMERO_OPERACAO_DESEJADO;

	public int NUMERO_DO_PEDIDO_DESEJADO;

	public int PADRAO_LAMINA_1_DESEJADO;

	public int PADRAO_LAMINA_2_DESEJADO;

	public int PADRAO_LAMINA_3_DESEJADO;

	[Header("DADOS ATUAIS DO BLOCO")]
	public int COR_DO_BLOCO_ANDAR_1;

	public int ANDAR_DO_BLOCO_1_DESEJADO;

	public int COR_DO_BLOCO_ANDAR_2;

	public int ANDAR_DO_BLOCO_2_DESEJADO;

	public int COR_DO_BLOCO_ANDAR_3;

	public int ANDAR_DO_BLOCO_3_DESEJADO;

	public int TAMPA_ANEXADA;

	public int ANDAR_DO_BLOCO = 1;

	public int TOTAL_DE_ANDARES = 1;

	[Header("SMARTBLOCK BASE")]
	public bool SMARTBLOCK_BASE;

	public bool PODE_CONFERIR_BLOCO_ATINGIDO_ANDAR = true;

	public bool PODE_CONFERIR_BLOCO_ATINGIDO_ANDAR_DESEJADO = true;

	public bool PODE_CONFERIR_ESTE_BLOCO_ANDAR = true;

	public bool PODE_CONFERIR_BLOCO_ATINGIDO_TAMPA = true;

	public bool PODE_CONFERIR_BLOCO_ATINGIDO_COR = true;

	public bool PODE_CONFERIR_BLOCO_ATINGIDO_TOTAL_DE_ANDARES = true;

	public GameObject BLOCO_CHILD_DESDE_BLOCO;

	public bool BLOCO_CAINDO_C0M_GRAVIDADE;

	public Vector3 POSICAO_BASE_EM_HOLDER;

	public Vector3 POSICAO_BASE_EM_BASE;

	public bool POSSO_VIRAR_PARENTE_DE_BASE = true;

	public bool POSSO_VIRAR_PARENTE_DE_BASE_UMA_VEZ = true;

	public bool ESTOU_EM_TRIGGER_ESTEIRA;

	public bool ESTOU_EM_TRIGGER_RETORNO;

	public bool JA_TENHO_PARENTE;

	public bool JA_ESTOU_COLIDINDO_ESTEIRA_1;

	public bool JA_ESTOU_COLIDINDO_ESTEIRA_2;

	public bool LAMINA_POSICAO_1_ANEXADA;

	public bool LAMINA_POSICAO_2_ANEXADA;

	public bool LAMINA_POSICAO_3_ANEXADA;

	public float ROTACAO_QUE_ENTROU_RETORNO;

	public float DIFERENCA_LOCAL_PARENTEADO_RETORNO_X;

	public float DIFERENCA_LOCAL_PARENTEADO_RETORNO_Z;

	public bool ESTOU_NO_TRIGGER_QUALIDADE;

	public bool ESTOU_EM_TRIGGER_PODE_VIRAR_PARENTE;

	[Header("SMARTBLOCK TAMPA")]
	public bool SMARTBLOCK_TAMPA;

	public Vector3 POSICAO_TAMPA_EM_BASE;

	public Vector3 POSICAO_TAMPA_EM_HOLDER;

	public bool pode_ir_para_local_correto_da_tampa;

	[Header("SMARTBLOCK LÂMINA")]
	public int COR_DA_LAMINA_ATUAL;

	public bool SMARTBLOCK_LAMINA;

	public bool SOU_LAMINA_POSICAO_1;

	public bool SOU_LAMINA_POSICAO_2;

	public bool SOU_LAMINA_POSICAO_3;

	public Vector3 POSICAO_LAMINA_1;

	public Vector3 ROTACAO_LAMINA_1;

	public Vector3 POSICAO_LAMINA_2;

	public Vector3 ROTACAO_LAMINA_2;

	public Vector3 POSICAO_LAMINA_3;

	public Vector3 ROTACAO_LAMINA_3;

	public bool POSSO_VIRAR_PARENTE_LOCAL_LAMINA = true;

	public bool LAMINA_ESTA_NA_BANCADA_PROCESSO;

	[Header("AUDIO")]
	public GameObject SOM_GARRA_PEGOU_BLOCO;

	public GameObject SOM_VENTOSA_PEGOU_BLOCO;

	public GameObject SOM_BLOCO_HIT;

	[Header("ETC")]
	private bool MINIMIZAR_FUNCAO = true;

	private Rigidbody OBJETO_RIGIDBODY;

	public static float FORCA_ESTEIRA = 300f;

	public static float FORCA_EXPULSADOR = 300f;

	public float FREQUENCIA_SMARTBLOCK_BASE_EMPURRADINHA = 0.5f;

	private float ULTIMO_SMARTBLOCK_BASE_EMPURRADINHA;

	private void Start()
	{
		OBJETO_RIGIDBODY = GetComponent<Rigidbody>();
		COR_DO_BLOCO_ANDAR_1 = COR_DO_BLOCO;
		ANDAR_DO_BLOCO_1_DESEJADO = ANDAR_DO_BLOCO_DESEJADO;
		ESTOU_NO_TRIGGER_QUALIDADE = false;
		if (SMARTBLOCK_BASE)
		{
			base.gameObject.transform.name = "SMARTBLOCK_BASE";
		}
		if (SMARTBLOCK_TAMPA)
		{
			StartCoroutine(START_TAMPA());
		}
	}

	private IEnumerator START_TAMPA()
	{
		yield return new WaitForSeconds(3f);
		pode_ir_para_local_correto_da_tampa = true;
	}

	private IEnumerator DELAY_PODE_CONFERIR_BLOCO_ATINGIDO_ANDAR()
	{
		yield return new WaitForSeconds(2f);
		PODE_CONFERIR_BLOCO_ATINGIDO_ANDAR = true;
	}

	private void Update()
	{
		if (SMARTBLOCK_LAMINA && SCRIPT_COLISAO_LOCAL_LAMINA_DESENHAR.STATIC_LAMINA_NA_MESA && SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAcionaVacuoMesa)
		{
			SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoSensorVacuoMesa = true;
		}
		if (SMARTBLOCK_LAMINA && !SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAcionaVacuoMesa)
		{
			SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoSensorVacuoMesa = false;
		}
		if (SMARTBLOCK_BASE)
		{
			DEBUG_NUMERO_OPERACAO_DESEJADO = NUMERO_OPERACAO_DESEJADO;
		}
		if ((bool)base.transform.parent && base.transform.parent.name == "QUALIDADE_HOLDER_BRACO")
		{
			base.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		}
		if (base.transform.parent == null)
		{
			JA_TENHO_PARENTE = false;
			if (SMARTBLOCK_BASE)
			{
				BLOCO_CAINDO_C0M_GRAVIDADE = true;
			}
			if (SMARTBLOCK_BASE && !JA_TENHO_PARENTE && !JA_TENHO_PARENTE && Time.time > FREQUENCIA_SMARTBLOCK_BASE_EMPURRADINHA + ULTIMO_SMARTBLOCK_BASE_EMPURRADINHA)
			{
				SMARTBLOCK_BASE_EMPURRADINHA();
				ULTIMO_SMARTBLOCK_BASE_EMPURRADINHA = Time.time;
			}
		}
		if ((bool)base.transform.parent && SMARTBLOCK_BASE)
		{
			BLOCO_CAINDO_C0M_GRAVIDADE = false;
		}
		if (MINIMIZAR_FUNCAO)
		{
			if (SMARTBLOCK_BASE && !ESTOU_EM_TRIGGER_ESTEIRA)
			{
				POSSO_VIRAR_PARENTE_DE_BASE = true;
			}
			if (SMARTBLOCK_BASE && ESTOU_EM_TRIGGER_ESTEIRA)
			{
				POSSO_VIRAR_PARENTE_DE_BASE = false;
			}
		}
		if (MINIMIZAR_FUNCAO)
		{
			if (SMARTBLOCK_TAMPA && !ESTOU_EM_TRIGGER_ESTEIRA)
			{
				POSSO_VIRAR_PARENTE_DE_BASE = true;
			}
			if (SMARTBLOCK_TAMPA && ESTOU_EM_TRIGGER_ESTEIRA)
			{
				POSSO_VIRAR_PARENTE_DE_BASE = false;
			}
		}
		_ = MINIMIZAR_FUNCAO;
		if (SMARTBLOCK_BASE && (bool)base.gameObject.transform.parent && base.transform.parent.gameObject.tag == "SMARTBLOCK_HOLDER")
		{
			base.transform.localPosition = POSICAO_BASE_EM_HOLDER;
		}
		if (SMARTBLOCK_BASE && (bool)base.gameObject.transform.parent && base.transform.parent.gameObject.tag == "SMARTBLOCK_BASE")
		{
			base.transform.localPosition = POSICAO_BASE_EM_BASE;
			if ((bool)OBJETO_RIGIDBODY)
			{
				OBJETO_RIGIDBODY.constraints = RigidbodyConstraints.FreezeAll;
			}
		}
		if (SMARTBLOCK_TAMPA && (bool)base.gameObject.transform.parent && base.gameObject.transform.parent.name == "SMARTBLOCK_BASE")
		{
			base.transform.localPosition = POSICAO_TAMPA_EM_BASE;
		}
		if (SMARTBLOCK_TAMPA && (bool)base.gameObject.transform.parent && base.transform.parent.gameObject.tag == "SMARTBLOCK_HOLDER")
		{
			base.transform.localPosition = POSICAO_TAMPA_EM_HOLDER;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (SMARTBLOCK_BASE && ANDAR_DO_BLOCO == 1 && other.gameObject.transform.tag == "RETORNO_ESQUERDA" && !JA_TENHO_PARENTE && !JA_ESTOU_COLIDINDO_ESTEIRA_2)
		{
			MonoBehaviour.print("BASE VIRA PARENTE DE RETORNO_ESQUERDA");
			ROTACAO_QUE_ENTROU_RETORNO = base.gameObject.transform.eulerAngles.y;
			base.transform.parent = other.gameObject.transform;
			base.transform.position = new Vector3(base.transform.position.x + DIFERENCA_LOCAL_PARENTEADO_RETORNO_X, base.transform.position.y, base.transform.position.z + DIFERENCA_LOCAL_PARENTEADO_RETORNO_Z);
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, 1.903716f, base.transform.localPosition.z);
			base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
			JA_TENHO_PARENTE = true;
			ESTOU_EM_TRIGGER_RETORNO = true;
		}
		if (SMARTBLOCK_BASE && ANDAR_DO_BLOCO == 1 && other.gameObject.transform.tag == "RETORNO_DIREITA" && !JA_TENHO_PARENTE && !JA_ESTOU_COLIDINDO_ESTEIRA_1)
		{
			MonoBehaviour.print("BASE VIRA PARENTE DE RETORNO_DIREITA");
			ROTACAO_QUE_ENTROU_RETORNO = base.gameObject.transform.eulerAngles.y;
			base.transform.parent = other.gameObject.transform;
			base.transform.position = new Vector3(base.transform.position.x - DIFERENCA_LOCAL_PARENTEADO_RETORNO_X, base.transform.position.y, base.transform.position.z - DIFERENCA_LOCAL_PARENTEADO_RETORNO_Z);
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, 1.903716f, base.transform.localPosition.z);
			base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
			JA_TENHO_PARENTE = true;
			ESTOU_EM_TRIGGER_RETORNO = true;
		}
		if (SMARTBLOCK_BASE && ANDAR_DO_BLOCO == 1 && other.gameObject.transform.tag == "QUALIDADE_DADOS_DO_BLOCO_TRIGGER" && base.transform.parent == null)
		{
			MonoBehaviour.print("ANTIGO COLIDIU COM TRIGGER QUALIDADE_DADOS_DO_BLOCO_TRIGGER");
			MonoBehaviour.print("CORRIGIR ROTAÇÃO E SIZE DO BLOCO QUE ESTÁ BUGADO");
			base.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			base.transform.localScale = new Vector3(3.164069f, 1.381633f, 3.150012f);
		}
		if (MINIMIZAR_FUNCAO && SMARTBLOCK_BASE && other.gameObject.transform.tag == "TRIGGER_PODE_VIRAR_PARENTE")
		{
			ESTOU_EM_TRIGGER_PODE_VIRAR_PARENTE = true;
		}
		if (SMARTBLOCK_TAMPA && other.gameObject.transform.name == "EMPILHADOR_TRIGGER_ESTOU_BANCADA")
		{
			GetComponent<BoxCollider>().isTrigger = false;
			MonoBehaviour.print("TAMPA TRIGGER FALSE");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (MINIMIZAR_FUNCAO)
		{
			if (SMARTBLOCK_BASE && other.gameObject.transform.tag == "TRIGGER_ESTEIRA")
			{
				MonoBehaviour.print("BASE FORA DA TRIGGER ESTEIRA");
				ESTOU_EM_TRIGGER_ESTEIRA = false;
			}
			if (SMARTBLOCK_TAMPA && other.gameObject.transform.tag == "TRIGGER_ESTEIRA")
			{
				MonoBehaviour.print("BASE FORA DA TRIGGER ESTEIRA");
				ESTOU_EM_TRIGGER_ESTEIRA = false;
			}
		}
		if (SMARTBLOCK_BASE && other.gameObject.transform.tag == "RETORNO_ESQUERDA")
		{
			ESTOU_EM_TRIGGER_RETORNO = false;
		}
		if (SMARTBLOCK_BASE && other.gameObject.transform.tag == "RETORNO_DIREITA")
		{
			ESTOU_EM_TRIGGER_RETORNO = false;
		}
		if (SMARTBLOCK_BASE && ANDAR_DO_BLOCO == 1 && other.gameObject.transform.tag == "QUALIDADE_DADOS_DO_BLOCO_TRIGGER")
		{
			MonoBehaviour.print("SAIU DO TRIGGER QUALIDADE_DADOS_DO_BLOCO_TRIGGER");
			ESTOU_NO_TRIGGER_QUALIDADE = false;
		}
		if (MINIMIZAR_FUNCAO && SMARTBLOCK_BASE && other.gameObject.transform.tag == "TRIGGER_PODE_VIRAR_PARENTE")
		{
			ESTOU_EM_TRIGGER_PODE_VIRAR_PARENTE = false;
		}
		if (SMARTBLOCK_TAMPA && other.gameObject.transform.name == "EMPILHADOR_TRIGGER_ESTOU_BANCADA")
		{
			GetComponent<BoxCollider>().isTrigger = true;
			MonoBehaviour.print("TAMPA TRIGGER FALSE");
		}
	}

	private void DELETAR_SMARTBLOCK()
	{
		if (!ESTOU_NO_TRIGGER_QUALIDADE)
		{
			Object.Destroy(base.gameObject);
		}
		if (ESTOU_NO_TRIGGER_QUALIDADE)
		{
			Object[] array = Object.FindObjectsOfType(typeof(GameObject));
			for (int i = 0; i < array.Length; i++)
			{
				((GameObject)array[i]).SendMessage("RESETAR_QUALIDADE", SendMessageOptions.DontRequireReceiver);
			}
			ESTOU_NO_TRIGGER_QUALIDADE = false;
			Object.Destroy(base.gameObject);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (MINIMIZAR_FUNCAO)
		{
			if (SMARTBLOCK_BASE && other.gameObject.transform.tag == "TRIGGER_ESTEIRA")
			{
				ESTOU_EM_TRIGGER_ESTEIRA = true;
			}
			if (SMARTBLOCK_BASE && other.gameObject.transform.tag != "TRIGGER_ESTEIRA")
			{
				ESTOU_EM_TRIGGER_ESTEIRA = false;
			}
			if (SMARTBLOCK_TAMPA && other.gameObject.transform.tag == "TRIGGER_ESTEIRA")
			{
				ESTOU_EM_TRIGGER_ESTEIRA = true;
			}
			if (SMARTBLOCK_TAMPA && other.gameObject.transform.tag != "TRIGGER_ESTEIRA")
			{
				ESTOU_EM_TRIGGER_ESTEIRA = false;
			}
		}
		if (SMARTBLOCK_TAMPA && other.gameObject.transform.tag == "EXPULSOR_LAMINAS")
		{
			MonoBehaviour.print("EMPURRA LAMINA");
			base.transform.Translate(Vector3.forward * Time.deltaTime * FORCA_EXPULSADOR / 150f);
		}
		if (SMARTBLOCK_TAMPA && other.gameObject.transform.tag == "TRIGGER_CONTAINER_TAMPAS" && pode_ir_para_local_correto_da_tampa)
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, 0f);
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		if (SMARTBLOCK_BASE && collision.gameObject.transform.tag == "ESTEIRA_1" && !JA_TENHO_PARENTE && collision.gameObject.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA)
		{
			OBJETO_RIGIDBODY.velocity = new Vector3(0f, 0.5f, FORCA_ESTEIRA * Time.deltaTime);
			base.transform.localPosition = new Vector3(-12.20447f, base.transform.position.y, base.transform.position.z);
		}
		if (SMARTBLOCK_BASE && collision.gameObject.transform.tag == "ESTEIRA_2" && !JA_TENHO_PARENTE && collision.gameObject.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA)
		{
			OBJETO_RIGIDBODY.velocity = new Vector3(0f, 0.5f, (0f - FORCA_ESTEIRA) * Time.deltaTime);
			base.transform.localPosition = new Vector3(-8.024f, base.transform.position.y, base.transform.position.z);
		}
		if (SMARTBLOCK_BASE && collision.gameObject.transform.tag == "ESTEIRA_1")
		{
			JA_ESTOU_COLIDINDO_ESTEIRA_1 = true;
			JA_ESTOU_COLIDINDO_ESTEIRA_2 = false;
		}
		if (SMARTBLOCK_BASE && collision.gameObject.transform.tag == "ESTEIRA_2")
		{
			JA_ESTOU_COLIDINDO_ESTEIRA_1 = false;
			JA_ESTOU_COLIDINDO_ESTEIRA_2 = true;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (MINIMIZAR_FUNCAO && SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "CHAO")
		{
			MonoBehaviour.print("DESTRUIR ESSA LÂMINA POIS CAIU NO CHÃO");
			Object.Destroy(base.gameObject);
		}
		if (MINIMIZAR_FUNCAO && SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "LAMINA" && GetComponent<Rigidbody>().velocity.magnitude > 0.01f)
		{
			MonoBehaviour.print("DESTRUIR ESSA LÂMINA POIS TOCOU EM OUTRA LÂMINA");
			Object.Destroy(base.gameObject);
		}
		if (MINIMIZAR_FUNCAO && SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "LOCAL_LAMINA_DESENHAR")
		{
			MonoBehaviour.print("LAMINA NO LOCAL DE DESENHAR");
			SCRIPT_COLISAO_LOCAL_LAMINA_DESENHAR.STATIC_LAMINA_NA_MESA = true;
		}
		if (SMARTBLOCK_BASE && collision.gameObject.transform.tag == "COLISAO_OBJETOS_ROTACIONAIS")
		{
			base.transform.parent = collision.gameObject.transform;
			JA_TENHO_PARENTE = false;
			if (collision.gameObject.transform.name == "MONTAGEM_DE_LAMINAS_ELEVADOR_ALTURA_3D")
			{
				base.transform.localPosition = new Vector3(0.4209185f, 0f, 125.8981f);
			}
			MonoBehaviour.print("COLIDINDO COM ROTACIONAL");
		}
		if (SMARTBLOCK_TAMPA && collision.gameObject.transform.tag == "COLISAO_OBJETOS_ROTACIONAIS")
		{
			base.transform.parent = collision.gameObject.transform;
			JA_TENHO_PARENTE = false;
		}
		if (MINIMIZAR_FUNCAO)
		{
			if (SMARTBLOCK_BASE && collision.gameObject.transform.tag == "CHAO")
			{
				MonoBehaviour.print("SMARTBLOCK CAIU NO CHÃO... DESTRUIR ELE");
				Object.Destroy(base.gameObject);
			}
			if (SMARTBLOCK_TAMPA && collision.gameObject.transform.tag == "CHAO")
			{
				MonoBehaviour.print("TAMPA CAIU NO CHÃO... DESTRUIR ELA");
				Object.Destroy(base.gameObject);
			}
		}
		if (MINIMIZAR_FUNCAO)
		{
			JA_TENHO_PARENTE = false;
			if (base.transform.parent != null && SMARTBLOCK_BASE && collision.gameObject.transform.tag == "ESTEIRA_2" && base.transform.parent.tag == "RETORNO_ESQUERDA")
			{
				MonoBehaviour.print("DESPARENTEAR BASE DO RETORNO ESQUERDA");
				base.transform.parent = null;
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z - 2f);
				JA_TENHO_PARENTE = false;
				if (MINIMIZAR_FUNCAO)
				{
					if (ROTACAO_QUE_ENTROU_RETORNO <= 0f)
					{
						base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, 0f, base.transform.eulerAngles.z);
						MonoBehaviour.print("ENTROU COM ROTAÇÃO MENOR OU IGUAL QUE [0]");
						base.transform.localScale = new Vector3(3.164066f, 1.381629f, 3.150004f);
					}
					if (ROTACAO_QUE_ENTROU_RETORNO > 0f && ROTACAO_QUE_ENTROU_RETORNO <= 90f)
					{
						base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, 0f, base.transform.eulerAngles.z);
						MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [0] E MENOR OU IGUAL QUE [90]");
						base.transform.localScale = new Vector3(3.164066f, 1.381629f, 3.150004f);
					}
					if (ROTACAO_QUE_ENTROU_RETORNO > 90f && ROTACAO_QUE_ENTROU_RETORNO <= 180f)
					{
						base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, 0f, base.transform.eulerAngles.z);
						MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [90] E MENOR OU IGUAL QUE [180]");
						base.transform.localScale = new Vector3(3.164066f, 1.381629f, 3.150004f);
					}
					if (ROTACAO_QUE_ENTROU_RETORNO > 180f && ROTACAO_QUE_ENTROU_RETORNO <= 270f)
					{
						base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, 0f, base.transform.eulerAngles.z);
						MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [180] E MENOR OU IGUAL QUE 270");
						base.transform.localScale = new Vector3(3.164066f, 1.381629f, 3.150004f);
					}
					if (ROTACAO_QUE_ENTROU_RETORNO > 270f && ROTACAO_QUE_ENTROU_RETORNO <= 360f)
					{
						base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, 0f, base.transform.eulerAngles.z);
						MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [270] E MENOR OU IGUAL QUE [360]");
						base.transform.localScale = new Vector3(3.164066f, 1.381629f, 3.150004f);
					}
					if (ROTACAO_QUE_ENTROU_RETORNO > 360f)
					{
						base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, 0f, base.transform.eulerAngles.z);
						MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [360]");
						base.transform.localScale = new Vector3(3.164066f, 1.381629f, 3.150004f);
					}
				}
			}
		}
		if (MINIMIZAR_FUNCAO)
		{
			JA_TENHO_PARENTE = false;
			if (base.transform.parent != null && SMARTBLOCK_BASE && collision.gameObject.transform.tag == "ESTEIRA_1" && base.transform.parent.tag == "RETORNO_DIREITA")
			{
				MonoBehaviour.print("DESPARENTEAR BASE DO RETORNO DIREITA");
				base.transform.parent = null;
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z + 2f);
				JA_TENHO_PARENTE = false;
				if (MINIMIZAR_FUNCAO)
				{
					if (ROTACAO_QUE_ENTROU_RETORNO <= 0f)
					{
						base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, 180f, base.transform.eulerAngles.z);
						MonoBehaviour.print("ENTROU COM ROTAÇÃO MENOR OU IGUAL QUE [0]");
						base.transform.localScale = new Vector3(3.164066f, 1.381629f, 3.150004f);
					}
					if (ROTACAO_QUE_ENTROU_RETORNO > 0f && ROTACAO_QUE_ENTROU_RETORNO <= 90f)
					{
						base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, 180f, base.transform.eulerAngles.z);
						MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [0] E MENOR OU IGUAL QUE [90]");
						base.transform.localScale = new Vector3(3.164066f, 1.381629f, 3.150004f);
					}
					if (ROTACAO_QUE_ENTROU_RETORNO > 90f && ROTACAO_QUE_ENTROU_RETORNO <= 180f)
					{
						base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, 180f, base.transform.eulerAngles.z);
						MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [90] E MENOR OU IGUAL QUE [180]");
						base.transform.localScale = new Vector3(3.164066f, 1.381629f, 3.150004f);
					}
					if (ROTACAO_QUE_ENTROU_RETORNO > 180f && ROTACAO_QUE_ENTROU_RETORNO <= 270f)
					{
						base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, 180f, base.transform.eulerAngles.z);
						MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [180] E MENOR OU IGUAL QUE 270");
						base.transform.localScale = new Vector3(3.164066f, 1.381629f, 3.150004f);
					}
					if (ROTACAO_QUE_ENTROU_RETORNO > 270f && ROTACAO_QUE_ENTROU_RETORNO <= 360f)
					{
						base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, 180f, base.transform.eulerAngles.z);
						MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [270] E MENOR OU IGUAL QUE [360]");
						base.transform.localScale = new Vector3(3.164066f, 1.381629f, 3.150004f);
					}
					if (ROTACAO_QUE_ENTROU_RETORNO > 360f)
					{
						base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, 180f, base.transform.eulerAngles.z);
						MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [360]");
						base.transform.localScale = new Vector3(3.164066f, 1.381629f, 3.150004f);
					}
				}
			}
		}
		if (MINIMIZAR_FUNCAO)
		{
			if (SMARTBLOCK_BASE && collision.gameObject.transform.tag == "SMARTBLOCK_HOLDER" && JA_TENHO_PARENTE && collision.gameObject.transform.name == "ESTOQUE_HOLDER_GARRA" && ANDAR_DO_BLOCO == 1)
			{
				JA_TENHO_PARENTE = false;
				base.transform.parent = null;
			}
			if (SMARTBLOCK_BASE && collision.gameObject.transform.tag == "SMARTBLOCK_HOLDER" && JA_TENHO_PARENTE && collision.gameObject.transform.name == "MONTAGEM_DE_BLOCOS_HOLDER_GARRA" && ANDAR_DO_BLOCO == 1)
			{
				JA_TENHO_PARENTE = false;
				base.transform.parent = null;
			}
			if (SMARTBLOCK_BASE && collision.gameObject.transform.tag == "SMARTBLOCK_HOLDER" && JA_TENHO_PARENTE && collision.gameObject.transform.name == "SMARTBLOCK_HOLDER_MONTAGEM_B1" && ANDAR_DO_BLOCO == 1)
			{
				JA_TENHO_PARENTE = false;
				base.transform.parent = null;
			}
			if (SMARTBLOCK_BASE && collision.gameObject.transform.tag == "SMARTBLOCK_HOLDER" && JA_TENHO_PARENTE && collision.gameObject.transform.name == "SMARTBLOCK_HOLDER_MONTAGEM_B2" && ANDAR_DO_BLOCO == 1)
			{
				JA_TENHO_PARENTE = false;
				base.transform.parent = null;
			}
			if (SMARTBLOCK_BASE && collision.gameObject.transform.tag == "SMARTBLOCK_HOLDER" && JA_TENHO_PARENTE && collision.gameObject.transform.name == "SMARTBLOCK_HOLDER_MONTAGEM_B3" && ANDAR_DO_BLOCO == 1)
			{
				JA_TENHO_PARENTE = false;
				base.transform.parent = null;
			}
		}
		if (SMARTBLOCK_BASE && collision.gameObject.transform.tag == "SMARTBLOCK_HOLDER" && !JA_TENHO_PARENTE && collision.gameObject.transform.childCount < 1 && ANDAR_DO_BLOCO == 1 && (collision.gameObject.transform.name == "SMARTBLOCK_HOLDER_MONTAGEM_B1" || collision.gameObject.transform.name == "SMARTBLOCK_HOLDER_MONTAGEM_B2" || collision.gameObject.transform.name == "SMARTBLOCK_HOLDER_MONTAGEM_B3"))
		{
			MonoBehaviour.print("BASE VIRA PARENTE DE HOLDER MONTAGEM B1/B2/B3");
			base.transform.parent = collision.gameObject.transform;
			JA_TENHO_PARENTE = true;
			base.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			base.transform.localScale = new Vector3(1.05469f, 13.81632f, 1.050004f);
		}
		if (SMARTBLOCK_BASE && collision.gameObject.transform.tag == "SMARTBLOCK_HOLDER" && !JA_TENHO_PARENTE && collision.gameObject.transform.childCount < 1 && ANDAR_DO_BLOCO == 1)
		{
			MonoBehaviour.print("BASE VIRA PARENTE DE HOLDER");
			base.transform.parent = collision.gameObject.transform;
			JA_TENHO_PARENTE = true;
			base.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			base.transform.localScale = new Vector3(1.05469f, 13.81632f, 1.050004f);
			ROTACAO_QUE_ENTROU_RETORNO = base.gameObject.transform.eulerAngles.y;
			if (collision.gameObject.transform.name != "MONTAGEM_DE_BLOCOS_HOLDER_GARRA" && collision.gameObject.transform.name == "SMARTBLOCK_HOLDER_MONTAGEM_B1" && collision.gameObject.transform.name == "SMARTBLOCK_HOLDER_MONTAGEM_B2" && collision.gameObject.transform.name == "SMARTBLOCK_HOLDER_MONTAGEM_B3")
			{
				if (ROTACAO_QUE_ENTROU_RETORNO <= 0f)
				{
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, 0f, base.transform.localEulerAngles.z);
					MonoBehaviour.print("ENTROU COM ROTAÇÃO MENOR OU IGUAL QUE [0]");
				}
				if (ROTACAO_QUE_ENTROU_RETORNO > 0f && ROTACAO_QUE_ENTROU_RETORNO < 90f)
				{
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, 0f, base.transform.localEulerAngles.z);
					MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [0] E MENOR QUE [90]");
				}
				if (ROTACAO_QUE_ENTROU_RETORNO > 0f && ROTACAO_QUE_ENTROU_RETORNO == 90f)
				{
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, 90f, base.transform.localEulerAngles.z);
					MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [0] E IGUAL QUE [90]");
				}
				if (ROTACAO_QUE_ENTROU_RETORNO > 90f && ROTACAO_QUE_ENTROU_RETORNO < 180f)
				{
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, 90f, base.transform.localEulerAngles.z);
					MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [90] E MENORQUE [180]");
				}
				if (ROTACAO_QUE_ENTROU_RETORNO > 90f && ROTACAO_QUE_ENTROU_RETORNO == 180f)
				{
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, 180f, base.transform.localEulerAngles.z);
					MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [90] E IGUAL QUE [180]");
				}
				if (ROTACAO_QUE_ENTROU_RETORNO > 180f && ROTACAO_QUE_ENTROU_RETORNO < 270f)
				{
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, 0f, base.transform.localEulerAngles.z);
					MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [180] E MENOR QUE 270");
				}
				if (ROTACAO_QUE_ENTROU_RETORNO > 180f && ROTACAO_QUE_ENTROU_RETORNO == 270f)
				{
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, 270f, base.transform.localEulerAngles.z);
					MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [180] E IGUAL QUE 270");
				}
				if (ROTACAO_QUE_ENTROU_RETORNO > 270f && ROTACAO_QUE_ENTROU_RETORNO < 360f)
				{
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, 0f, base.transform.localEulerAngles.z);
					MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [270] E MENOR QUE [360]");
				}
				if (ROTACAO_QUE_ENTROU_RETORNO > 270f && ROTACAO_QUE_ENTROU_RETORNO == 360f)
				{
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, 270f, base.transform.localEulerAngles.z);
					MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [270] E IGUAL QUE [360]");
				}
				if (ROTACAO_QUE_ENTROU_RETORNO > 360f)
				{
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, 0f, base.transform.localEulerAngles.z);
					MonoBehaviour.print("ENTROU COM ROTAÇÃO MAIOR QUE [360]");
				}
				Object.Instantiate(SOM_GARRA_PEGOU_BLOCO, base.transform.position, base.transform.rotation);
			}
			if (collision.gameObject.transform.name == "MONTAGEM_DE_BLOCOS_HOLDER_GARRA")
			{
				base.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				MonoBehaviour.print("ENTROU NO HOLDER MONTAGEM_DE_BLOCOS_HOLDER_GARRA");
				Object.Instantiate(SOM_GARRA_PEGOU_BLOCO, base.transform.position, base.transform.rotation);
			}
		}
		if (SMARTBLOCK_BASE && ANDAR_DO_BLOCO == 1 && ESTOU_EM_TRIGGER_PODE_VIRAR_PARENTE && !JA_TENHO_PARENTE && BLOCO_CAINDO_C0M_GRAVIDADE && POSSO_VIRAR_PARENTE_DE_BASE && !ESTOU_EM_TRIGGER_RETORNO && collision.gameObject.transform.tag == "SMARTBLOCK_BASE" && collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().POSSO_VIRAR_PARENTE_DE_BASE && !collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().ESTOU_EM_TRIGGER_RETORNO)
		{
			if (MINIMIZAR_FUNCAO)
			{
				if (TOTAL_DE_ANDARES == 1 && collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES == 1 && PODE_CONFERIR_BLOCO_ATINGIDO_ANDAR)
				{
					MonoBehaviour.print("SE ESSE BLOCO TIVER [1 ANDAR] E O BLOCO QUE COLIDIU TIVER [1 ANDAR]");
					PODE_CONFERIR_BLOCO_ATINGIDO_ANDAR = false;
					collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES = 2;
					collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO_2_DESEJADO = ANDAR_DO_BLOCO_DESEJADO;
					collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().COR_DO_BLOCO_ANDAR_2 = COR_DO_BLOCO;
					collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().TAMPA_ANEXADA = TAMPA_ANEXADA;
					GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO = 2;
					GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES = 2;
					StartCoroutine(DELAY_PODE_CONFERIR_BLOCO_ATINGIDO_ANDAR());
				}
				if (TOTAL_DE_ANDARES == 1 && collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES == 2 && PODE_CONFERIR_BLOCO_ATINGIDO_ANDAR)
				{
					MonoBehaviour.print("SE ESSE BLOCO TIVER [1 ANDAR] E O BLOCO QUE COLIDIU TIVER [2 ANDARES]");
					PODE_CONFERIR_BLOCO_ATINGIDO_ANDAR = false;
					collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES = 3;
					collision.gameObject.transform.parent.GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES = 3;
					collision.gameObject.transform.parent.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO_3_DESEJADO = ANDAR_DO_BLOCO_DESEJADO;
					collision.gameObject.transform.parent.GetComponent<SCRIPT_SMARTBLOCK>().COR_DO_BLOCO_ANDAR_3 = COR_DO_BLOCO;
					collision.gameObject.transform.parent.GetComponent<SCRIPT_SMARTBLOCK>().TAMPA_ANEXADA = TAMPA_ANEXADA;
					GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO = 3;
					GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES = 3;
					StartCoroutine(DELAY_PODE_CONFERIR_BLOCO_ATINGIDO_ANDAR());
				}
				if (TOTAL_DE_ANDARES == 2 && collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES == 1 && PODE_CONFERIR_BLOCO_ATINGIDO_ANDAR)
				{
					MonoBehaviour.print("SE ESSE BLOCO TIVER [2 ANDARES] E O BLOCO QUE COLIDIU TIVER [1 ANDAR]");
					PODE_CONFERIR_BLOCO_ATINGIDO_ANDAR = false;
					collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES = 3;
					collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO_2_DESEJADO = ANDAR_DO_BLOCO_DESEJADO;
					collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO_3_DESEJADO = base.gameObject.transform.Find("SMARTBLOCK_BASE").gameObject.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO_DESEJADO;
					collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().COR_DO_BLOCO_ANDAR_2 = COR_DO_BLOCO;
					collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().COR_DO_BLOCO_ANDAR_3 = base.gameObject.transform.Find("SMARTBLOCK_BASE").gameObject.GetComponent<SCRIPT_SMARTBLOCK>().COR_DO_BLOCO;
					collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().TAMPA_ANEXADA = TAMPA_ANEXADA;
					GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO = 2;
					GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES = 3;
					base.gameObject.transform.Find("SMARTBLOCK_BASE").gameObject.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO = 3;
					base.gameObject.transform.Find("SMARTBLOCK_BASE").gameObject.GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES = 3;
					StartCoroutine(DELAY_PODE_CONFERIR_BLOCO_ATINGIDO_ANDAR());
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				MonoBehaviour.print("ESTE BLOCO VIRA PARENTE: " + base.gameObject.GetInstanceID());
				base.transform.parent = collision.gameObject.transform;
				GetComponent<Rigidbody>().mass = 0.1f;
				JA_TENHO_PARENTE = true;
				base.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				base.transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}
		if (SMARTBLOCK_TAMPA && collision.gameObject.transform.tag == "SMARTBLOCK_BASE" && !JA_TENHO_PARENTE && GetComponent<Rigidbody>().velocity.magnitude > 0.01f && POSSO_VIRAR_PARENTE_DE_BASE)
		{
			MonoBehaviour.print("TAMPA VIRA PARENTE DE BASE");
			GetComponent<Rigidbody>().useGravity = false;
			base.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			base.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
			base.gameObject.GetComponent<Rigidbody>().isKinematic = true;
			base.transform.parent = collision.gameObject.transform;
			JA_TENHO_PARENTE = true;
			GetComponent<BoxCollider>().isTrigger = true;
			if (collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO == 1)
			{
				collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().TAMPA_ANEXADA = 1;
				base.gameObject.GetComponent<Rigidbody>().isKinematic = true;
				base.gameObject.GetComponent<Rigidbody>().useGravity = false;
				MonoBehaviour.print("PREVINIR BUG QUE ATRAVESSA EMPILHADOR 1");
				base.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				base.transform.localScale = new Vector3(1f, 0.3604857f, 1f);
			}
			if (collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO == 2)
			{
				collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().TAMPA_ANEXADA = 1;
				collision.gameObject.transform.parent.GetComponent<SCRIPT_SMARTBLOCK>().TAMPA_ANEXADA = 1;
				collision.gameObject.transform.parent.transform.parent = null;
				collision.gameObject.transform.parent.transform.position = new Vector3(base.transform.position.x, 8.38f, base.transform.position.z);
				MonoBehaviour.print("PREVINIR BUG QUE ATRAVESSA EMPILHADOR 2");
			}
			if (collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO == 3)
			{
				collision.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().TAMPA_ANEXADA = 1;
				collision.gameObject.transform.parent.GetComponent<SCRIPT_SMARTBLOCK>().TAMPA_ANEXADA = 1;
				collision.gameObject.transform.parent.transform.parent.GetComponent<SCRIPT_SMARTBLOCK>().TAMPA_ANEXADA = 1;
				collision.gameObject.transform.parent.transform.parent.transform.parent = null;
				collision.gameObject.transform.parent.transform.parent.transform.position = new Vector3(base.transform.position.x, 8.38f, base.transform.position.z);
				MonoBehaviour.print("PREVINIR BUG QUE ATRAVESSA EMPILHADOR 3");
			}
		}
		if (MINIMIZAR_FUNCAO)
		{
			if (SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "SMARTBLOCK_BASE" && !JA_TENHO_PARENTE && SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "9")
			{
				MonoBehaviour.print("LÂMINA VIRA PARENTE DE BASE NA POSIÇÃO LADO 1");
				Object.Destroy(GetComponent<Rigidbody>());
				Object.Destroy(GetComponent<BoxCollider>());
				base.transform.parent = collision.gameObject.transform;
				JA_TENHO_PARENTE = true;
				base.gameObject.transform.parent.GetComponent<SCRIPT_SMARTBLOCK>().LAMINA_POSICAO_3_ANEXADA = true;
				SOU_LAMINA_POSICAO_3 = true;
				base.transform.localPosition = POSICAO_LAMINA_3;
				base.transform.localEulerAngles = ROTACAO_LAMINA_3;
			}
			if (SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "SMARTBLOCK_BASE" && !JA_TENHO_PARENTE && SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "10")
			{
				MonoBehaviour.print("LÂMINA VIRA PARENTE DE BASE NA POSIÇÃO LADO 2");
				Object.Destroy(GetComponent<Rigidbody>());
				Object.Destroy(GetComponent<BoxCollider>());
				base.transform.parent = collision.gameObject.transform;
				JA_TENHO_PARENTE = true;
				base.gameObject.transform.parent.GetComponent<SCRIPT_SMARTBLOCK>().LAMINA_POSICAO_1_ANEXADA = true;
				SOU_LAMINA_POSICAO_1 = true;
				base.transform.localPosition = POSICAO_LAMINA_1;
				base.transform.localEulerAngles = ROTACAO_LAMINA_1;
			}
			if (SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "SMARTBLOCK_BASE" && !JA_TENHO_PARENTE && SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "11")
			{
				MonoBehaviour.print("LÂMINA VIRA PARENTE DE BASE NA POSIÇÃO LADO 3");
				Object.Destroy(GetComponent<Rigidbody>());
				Object.Destroy(GetComponent<BoxCollider>());
				base.transform.parent = collision.gameObject.transform;
				JA_TENHO_PARENTE = true;
				base.gameObject.transform.parent.GetComponent<SCRIPT_SMARTBLOCK>().LAMINA_POSICAO_2_ANEXADA = true;
				SOU_LAMINA_POSICAO_2 = true;
				base.transform.localPosition = POSICAO_LAMINA_2;
				base.transform.localEulerAngles = ROTACAO_LAMINA_2;
			}
			if (SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "SMARTBLOCK_BASE" && !JA_TENHO_PARENTE && SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "12")
			{
				MonoBehaviour.print("LÂMINA VIRA PARENTE DE BASE NA POSIÇÃO LADO 1");
				Object.Destroy(GetComponent<Rigidbody>());
				Object.Destroy(GetComponent<BoxCollider>());
				base.transform.parent = collision.gameObject.transform;
				JA_TENHO_PARENTE = true;
				base.gameObject.transform.parent.GetComponent<SCRIPT_SMARTBLOCK>().LAMINA_POSICAO_3_ANEXADA = true;
				SOU_LAMINA_POSICAO_3 = true;
				base.transform.localPosition = POSICAO_LAMINA_3;
				base.transform.localEulerAngles = ROTACAO_LAMINA_3;
			}
			if (SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "SMARTBLOCK_BASE" && !JA_TENHO_PARENTE && SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "13")
			{
				MonoBehaviour.print("LÂMINA VIRA PARENTE DE BASE NA POSIÇÃO LADO 2");
				Object.Destroy(GetComponent<Rigidbody>());
				Object.Destroy(GetComponent<BoxCollider>());
				base.transform.parent = collision.gameObject.transform;
				JA_TENHO_PARENTE = true;
				base.gameObject.transform.parent.GetComponent<SCRIPT_SMARTBLOCK>().LAMINA_POSICAO_1_ANEXADA = true;
				SOU_LAMINA_POSICAO_1 = true;
				base.transform.localPosition = POSICAO_LAMINA_1;
				base.transform.localEulerAngles = ROTACAO_LAMINA_1;
			}
			if (SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "SMARTBLOCK_BASE" && !JA_TENHO_PARENTE && SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "14")
			{
				MonoBehaviour.print("LÂMINA VIRA PARENTE DE BASE NA POSIÇÃO LADO 3");
				Object.Destroy(GetComponent<Rigidbody>());
				Object.Destroy(GetComponent<BoxCollider>());
				base.transform.parent = collision.gameObject.transform;
				JA_TENHO_PARENTE = true;
				base.gameObject.transform.parent.GetComponent<SCRIPT_SMARTBLOCK>().LAMINA_POSICAO_2_ANEXADA = true;
				SOU_LAMINA_POSICAO_2 = true;
				base.transform.localPosition = POSICAO_LAMINA_2;
				base.transform.localEulerAngles = ROTACAO_LAMINA_2;
			}
			if (SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "SMARTBLOCK_BASE" && !JA_TENHO_PARENTE && SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "15")
			{
				MonoBehaviour.print("LÂMINA VIRA PARENTE DE BASE NA POSIÇÃO LADO 1");
				Object.Destroy(GetComponent<Rigidbody>());
				Object.Destroy(GetComponent<BoxCollider>());
				base.transform.parent = collision.gameObject.transform;
				JA_TENHO_PARENTE = true;
				base.gameObject.transform.parent.GetComponent<SCRIPT_SMARTBLOCK>().LAMINA_POSICAO_3_ANEXADA = true;
				SOU_LAMINA_POSICAO_3 = true;
				base.transform.localPosition = POSICAO_LAMINA_3;
				base.transform.localEulerAngles = ROTACAO_LAMINA_3;
			}
			if (SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "SMARTBLOCK_BASE" && !JA_TENHO_PARENTE && SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "16")
			{
				MonoBehaviour.print("LÂMINA VIRA PARENTE DE BASE NA POSIÇÃO LADO 2");
				Object.Destroy(GetComponent<Rigidbody>());
				Object.Destroy(GetComponent<BoxCollider>());
				base.transform.parent = collision.gameObject.transform;
				JA_TENHO_PARENTE = true;
				base.gameObject.transform.parent.GetComponent<SCRIPT_SMARTBLOCK>().LAMINA_POSICAO_1_ANEXADA = true;
				SOU_LAMINA_POSICAO_1 = true;
				base.transform.localPosition = POSICAO_LAMINA_1;
				base.transform.localEulerAngles = ROTACAO_LAMINA_1;
			}
			if (SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "SMARTBLOCK_BASE" && !JA_TENHO_PARENTE && SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "17")
			{
				MonoBehaviour.print("LÂMINA VIRA PARENTE DE BASE NA POSIÇÃO LADO 3");
				Object.Destroy(GetComponent<Rigidbody>());
				Object.Destroy(GetComponent<BoxCollider>());
				base.transform.parent = collision.gameObject.transform;
				JA_TENHO_PARENTE = true;
				base.gameObject.transform.parent.GetComponent<SCRIPT_SMARTBLOCK>().LAMINA_POSICAO_2_ANEXADA = true;
				SOU_LAMINA_POSICAO_2 = true;
				base.transform.localPosition = POSICAO_LAMINA_2;
				base.transform.localEulerAngles = ROTACAO_LAMINA_2;
			}
		}
		if (MINIMIZAR_FUNCAO)
		{
			if (SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "COLISAO_LAMINA_FICAR_1")
			{
				if (Administracao.PROCESSO_LAMINA_LOCAL_1_OBJETO_ATUAL == null && POSSO_VIRAR_PARENTE_LOCAL_LAMINA)
				{
					MonoBehaviour.print("NÃO TEM LAMINA NO LOCAL 1");
					Administracao.PROCESSO_LAMINA_LOCAL_1_OBJETO_ATUAL = base.gameObject;
					GetComponent<Rigidbody>().useGravity = false;
					GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					base.transform.parent = GameObject.Find("LOCAL_LAMINA_FICAR_1").gameObject.transform;
					base.transform.localPosition = GameObject.Find("LOCAL_LAMINA_FICAR_1").gameObject.transform.position;
					base.transform.rotation = GameObject.Find("LOCAL_LAMINA_FICAR_1").gameObject.transform.rotation;
					POSSO_VIRAR_PARENTE_LOCAL_LAMINA = false;
				}
				if ((bool)Administracao.PROCESSO_LAMINA_LOCAL_1_OBJETO_ATUAL && POSSO_VIRAR_PARENTE_LOCAL_LAMINA)
				{
					MonoBehaviour.print("DESTRUIR LÂMINA ATUAL + TEM LAMINA NO LOCAL 1");
					if (!Administracao.PROCESSO_LAMINA_LOCAL_1_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_1 && !Administracao.PROCESSO_LAMINA_LOCAL_1_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_2 && !Administracao.PROCESSO_LAMINA_LOCAL_1_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_3)
					{
						if (Administracao.PROCESSO_LAMINA_LOCAL_1_OBJETO_ATUAL.transform.parent.name != "QUALIDADE_HOLDER_BRACO")
						{
							MonoBehaviour.print("ESTA LAMINA  SERÁ DESTRUÍDA");
							Object.Destroy(Administracao.PROCESSO_LAMINA_LOCAL_1_OBJETO_ATUAL);
						}
						if (Administracao.PROCESSO_LAMINA_LOCAL_1_OBJETO_ATUAL.transform.parent.name == "QUALIDADE_HOLDER_BRACO")
						{
							MonoBehaviour.print("ESTA LAMINA ESTÁ NO BRAÇO POR ISSO NÃO SERÁ DESTRUÍDA");
						}
					}
					Administracao.PROCESSO_LAMINA_LOCAL_1_OBJETO_ATUAL = base.gameObject;
					GetComponent<Rigidbody>().useGravity = false;
					GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					base.transform.parent = GameObject.Find("LOCAL_LAMINA_FICAR_1").gameObject.transform;
					base.transform.localPosition = GameObject.Find("LOCAL_LAMINA_FICAR_1").gameObject.transform.position;
					base.transform.rotation = GameObject.Find("LOCAL_LAMINA_FICAR_1").gameObject.transform.rotation;
					POSSO_VIRAR_PARENTE_LOCAL_LAMINA = false;
				}
			}
			if (SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "COLISAO_LAMINA_FICAR_2")
			{
				if (Administracao.PROCESSO_LAMINA_LOCAL_2_OBJETO_ATUAL == null && POSSO_VIRAR_PARENTE_LOCAL_LAMINA)
				{
					MonoBehaviour.print("NÃO TEM LAMINA NO LOCAL 2");
					Administracao.PROCESSO_LAMINA_LOCAL_2_OBJETO_ATUAL = base.gameObject;
					GetComponent<Rigidbody>().useGravity = false;
					GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					base.transform.parent = GameObject.Find("LOCAL_LAMINA_FICAR_2").gameObject.transform;
					base.transform.localPosition = GameObject.Find("LOCAL_LAMINA_FICAR_2").gameObject.transform.position;
					base.transform.rotation = GameObject.Find("LOCAL_LAMINA_FICAR_2").gameObject.transform.rotation;
					POSSO_VIRAR_PARENTE_LOCAL_LAMINA = false;
				}
				if ((bool)Administracao.PROCESSO_LAMINA_LOCAL_2_OBJETO_ATUAL && POSSO_VIRAR_PARENTE_LOCAL_LAMINA)
				{
					MonoBehaviour.print("DESTRUIR LÂMINA ATUAL + TEM LAMINA NO LOCAL 2");
					if (!Administracao.PROCESSO_LAMINA_LOCAL_2_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_1 && !Administracao.PROCESSO_LAMINA_LOCAL_2_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_2 && !Administracao.PROCESSO_LAMINA_LOCAL_2_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_3)
					{
						if (Administracao.PROCESSO_LAMINA_LOCAL_2_OBJETO_ATUAL.transform.parent.name != "QUALIDADE_HOLDER_BRACO")
						{
							MonoBehaviour.print("ESTA LAMINA  SERÁ DESTRUÍDA");
							Object.Destroy(Administracao.PROCESSO_LAMINA_LOCAL_2_OBJETO_ATUAL);
						}
						if (Administracao.PROCESSO_LAMINA_LOCAL_2_OBJETO_ATUAL.transform.parent.name == "QUALIDADE_HOLDER_BRACO")
						{
							MonoBehaviour.print("ESTA LAMINA ESTÁ NO BRAÇO POR ISSO NÃO SERÁ DESTRUÍDA");
						}
					}
					Administracao.PROCESSO_LAMINA_LOCAL_2_OBJETO_ATUAL = base.gameObject;
					GetComponent<Rigidbody>().useGravity = false;
					GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					base.transform.parent = GameObject.Find("LOCAL_LAMINA_FICAR_2").gameObject.transform;
					base.transform.localPosition = GameObject.Find("LOCAL_LAMINA_FICAR_2").gameObject.transform.position;
					base.transform.rotation = GameObject.Find("LOCAL_LAMINA_FICAR_2").gameObject.transform.rotation;
					POSSO_VIRAR_PARENTE_LOCAL_LAMINA = false;
				}
			}
			if (SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "COLISAO_LAMINA_FICAR_3")
			{
				if (Administracao.PROCESSO_LAMINA_LOCAL_3_OBJETO_ATUAL == null && POSSO_VIRAR_PARENTE_LOCAL_LAMINA)
				{
					MonoBehaviour.print("NÃO TEM LAMINA NO LOCAL 3");
					Administracao.PROCESSO_LAMINA_LOCAL_3_OBJETO_ATUAL = base.gameObject;
					GetComponent<Rigidbody>().useGravity = false;
					GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					base.transform.parent = GameObject.Find("LOCAL_LAMINA_FICAR_3").gameObject.transform;
					base.transform.localPosition = GameObject.Find("LOCAL_LAMINA_FICAR_3").gameObject.transform.position;
					base.transform.rotation = GameObject.Find("LOCAL_LAMINA_FICAR_3").gameObject.transform.rotation;
					POSSO_VIRAR_PARENTE_LOCAL_LAMINA = false;
				}
				if ((bool)Administracao.PROCESSO_LAMINA_LOCAL_3_OBJETO_ATUAL && POSSO_VIRAR_PARENTE_LOCAL_LAMINA)
				{
					MonoBehaviour.print("DESTRUIR LÂMINA ATUAL + TEM LAMINA NO LOCAL 3");
					if (!Administracao.PROCESSO_LAMINA_LOCAL_3_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_1 && !Administracao.PROCESSO_LAMINA_LOCAL_3_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_2 && !Administracao.PROCESSO_LAMINA_LOCAL_3_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_3)
					{
						if (Administracao.PROCESSO_LAMINA_LOCAL_3_OBJETO_ATUAL.transform.parent.name != "QUALIDADE_HOLDER_BRACO")
						{
							MonoBehaviour.print("ESTA LAMINA  SERÁ DESTRUÍDA");
							Object.Destroy(Administracao.PROCESSO_LAMINA_LOCAL_3_OBJETO_ATUAL);
						}
						if (Administracao.PROCESSO_LAMINA_LOCAL_3_OBJETO_ATUAL.transform.parent.name == "QUALIDADE_HOLDER_BRACO")
						{
							MonoBehaviour.print("ESTA LAMINA ESTÁ NO BRAÇO POR ISSO NÃO SERÁ DESTRUÍDA");
						}
					}
					Administracao.PROCESSO_LAMINA_LOCAL_3_OBJETO_ATUAL = base.gameObject;
					GetComponent<Rigidbody>().useGravity = false;
					GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					base.transform.parent = GameObject.Find("LOCAL_LAMINA_FICAR_3").gameObject.transform;
					base.transform.localPosition = GameObject.Find("LOCAL_LAMINA_FICAR_3").gameObject.transform.position;
					base.transform.rotation = GameObject.Find("LOCAL_LAMINA_FICAR_3").gameObject.transform.rotation;
					POSSO_VIRAR_PARENTE_LOCAL_LAMINA = false;
				}
			}
			if (SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "COLISAO_LAMINA_FICAR_4")
			{
				if (Administracao.PROCESSO_LAMINA_LOCAL_4_OBJETO_ATUAL == null && POSSO_VIRAR_PARENTE_LOCAL_LAMINA)
				{
					MonoBehaviour.print("NÃO TEM LAMINA NO LOCAL 4");
					Administracao.PROCESSO_LAMINA_LOCAL_4_OBJETO_ATUAL = base.gameObject;
					GetComponent<Rigidbody>().useGravity = false;
					GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					base.transform.parent = GameObject.Find("LOCAL_LAMINA_FICAR_4").gameObject.transform;
					base.transform.localPosition = GameObject.Find("LOCAL_LAMINA_FICAR_4").gameObject.transform.position;
					base.transform.rotation = GameObject.Find("LOCAL_LAMINA_FICAR_4").gameObject.transform.rotation;
					POSSO_VIRAR_PARENTE_LOCAL_LAMINA = false;
				}
				if ((bool)Administracao.PROCESSO_LAMINA_LOCAL_4_OBJETO_ATUAL && POSSO_VIRAR_PARENTE_LOCAL_LAMINA)
				{
					MonoBehaviour.print("DESTRUIR LÂMINA ATUAL + TEM LAMINA NO LOCAL 4");
					if (!Administracao.PROCESSO_LAMINA_LOCAL_4_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_1 && !Administracao.PROCESSO_LAMINA_LOCAL_4_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_2 && !Administracao.PROCESSO_LAMINA_LOCAL_4_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_3)
					{
						if (Administracao.PROCESSO_LAMINA_LOCAL_4_OBJETO_ATUAL.transform.parent.name != "QUALIDADE_HOLDER_BRACO")
						{
							MonoBehaviour.print("ESTA LAMINA  SERÁ DESTRUÍDA");
							Object.Destroy(Administracao.PROCESSO_LAMINA_LOCAL_4_OBJETO_ATUAL);
						}
						if (Administracao.PROCESSO_LAMINA_LOCAL_4_OBJETO_ATUAL.transform.parent.name == "QUALIDADE_HOLDER_BRACO")
						{
							MonoBehaviour.print("ESTA LAMINA ESTÁ NO BRAÇO POR ISSO NÃO SERÁ DESTRUÍDA");
						}
					}
					Administracao.PROCESSO_LAMINA_LOCAL_4_OBJETO_ATUAL = base.gameObject;
					GetComponent<Rigidbody>().useGravity = false;
					GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					base.transform.parent = GameObject.Find("LOCAL_LAMINA_FICAR_4").gameObject.transform;
					base.transform.localPosition = GameObject.Find("LOCAL_LAMINA_FICAR_4").gameObject.transform.position;
					base.transform.rotation = GameObject.Find("LOCAL_LAMINA_FICAR_4").gameObject.transform.rotation;
					POSSO_VIRAR_PARENTE_LOCAL_LAMINA = false;
				}
			}
			if (SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "COLISAO_LAMINA_FICAR_5")
			{
				if (Administracao.PROCESSO_LAMINA_LOCAL_5_OBJETO_ATUAL == null && POSSO_VIRAR_PARENTE_LOCAL_LAMINA)
				{
					MonoBehaviour.print("NÃO TEM LAMINA NO LOCAL 5");
					Administracao.PROCESSO_LAMINA_LOCAL_5_OBJETO_ATUAL = base.gameObject;
					GetComponent<Rigidbody>().useGravity = false;
					GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					base.transform.parent = GameObject.Find("LOCAL_LAMINA_FICAR_5").gameObject.transform;
					base.transform.localPosition = GameObject.Find("LOCAL_LAMINA_FICAR_5").gameObject.transform.position;
					base.transform.rotation = GameObject.Find("LOCAL_LAMINA_FICAR_5").gameObject.transform.rotation;
					POSSO_VIRAR_PARENTE_LOCAL_LAMINA = false;
				}
				if ((bool)Administracao.PROCESSO_LAMINA_LOCAL_5_OBJETO_ATUAL && POSSO_VIRAR_PARENTE_LOCAL_LAMINA)
				{
					MonoBehaviour.print("DESTRUIR LÂMINA ATUAL + TEM LAMINA NO LOCAL 5");
					if (!Administracao.PROCESSO_LAMINA_LOCAL_5_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_1 && !Administracao.PROCESSO_LAMINA_LOCAL_5_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_2 && !Administracao.PROCESSO_LAMINA_LOCAL_5_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_3)
					{
						if (Administracao.PROCESSO_LAMINA_LOCAL_5_OBJETO_ATUAL.transform.parent.name != "QUALIDADE_HOLDER_BRACO")
						{
							MonoBehaviour.print("ESTA LAMINA  SERÁ DESTRUÍDA");
							Object.Destroy(Administracao.PROCESSO_LAMINA_LOCAL_5_OBJETO_ATUAL);
						}
						if (Administracao.PROCESSO_LAMINA_LOCAL_5_OBJETO_ATUAL.transform.parent.name == "QUALIDADE_HOLDER_BRACO")
						{
							MonoBehaviour.print("ESTA LAMINA ESTÁ NO BRAÇO POR ISSO NÃO SERÁ DESTRUÍDA");
						}
					}
					Administracao.PROCESSO_LAMINA_LOCAL_5_OBJETO_ATUAL = base.gameObject;
					GetComponent<Rigidbody>().useGravity = false;
					GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					base.transform.parent = GameObject.Find("LOCAL_LAMINA_FICAR_5").gameObject.transform;
					base.transform.localPosition = GameObject.Find("LOCAL_LAMINA_FICAR_5").gameObject.transform.position;
					base.transform.rotation = GameObject.Find("LOCAL_LAMINA_FICAR_5").gameObject.transform.rotation;
					POSSO_VIRAR_PARENTE_LOCAL_LAMINA = false;
				}
			}
			if (SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "COLISAO_LAMINA_FICAR_6")
			{
				if (Administracao.PROCESSO_LAMINA_LOCAL_6_OBJETO_ATUAL == null && POSSO_VIRAR_PARENTE_LOCAL_LAMINA)
				{
					MonoBehaviour.print("NÃO TEM LAMINA NO LOCAL 6");
					Administracao.PROCESSO_LAMINA_LOCAL_6_OBJETO_ATUAL = base.gameObject;
					GetComponent<Rigidbody>().useGravity = false;
					GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					base.transform.parent = GameObject.Find("LOCAL_LAMINA_FICAR_6").gameObject.transform;
					base.transform.localPosition = GameObject.Find("LOCAL_LAMINA_FICAR_6").gameObject.transform.position;
					base.transform.rotation = GameObject.Find("LOCAL_LAMINA_FICAR_6").gameObject.transform.rotation;
					POSSO_VIRAR_PARENTE_LOCAL_LAMINA = false;
				}
				if ((bool)Administracao.PROCESSO_LAMINA_LOCAL_6_OBJETO_ATUAL && POSSO_VIRAR_PARENTE_LOCAL_LAMINA)
				{
					MonoBehaviour.print("DESTRUIR LÂMINA ATUAL + TEM LAMINA NO LOCAL 6");
					if (!Administracao.PROCESSO_LAMINA_LOCAL_6_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_1 && !Administracao.PROCESSO_LAMINA_LOCAL_6_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_2 && !Administracao.PROCESSO_LAMINA_LOCAL_6_OBJETO_ATUAL.GetComponent<SCRIPT_SMARTBLOCK>().SOU_LAMINA_POSICAO_3)
					{
						if (Administracao.PROCESSO_LAMINA_LOCAL_6_OBJETO_ATUAL.transform.parent.name != "QUALIDADE_HOLDER_BRACO")
						{
							MonoBehaviour.print("ESTA LAMINA  SERÁ DESTRUÍDA");
							Object.Destroy(Administracao.PROCESSO_LAMINA_LOCAL_6_OBJETO_ATUAL);
						}
						if (Administracao.PROCESSO_LAMINA_LOCAL_6_OBJETO_ATUAL.transform.parent.name == "QUALIDADE_HOLDER_BRACO")
						{
							MonoBehaviour.print("ESTA LAMINA ESTÁ NO BRAÇO POR ISSO NÃO SERÁ DESTRUÍDA");
						}
					}
					Administracao.PROCESSO_LAMINA_LOCAL_6_OBJETO_ATUAL = base.gameObject;
					GetComponent<Rigidbody>().useGravity = false;
					GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					base.transform.parent = GameObject.Find("LOCAL_LAMINA_FICAR_6").gameObject.transform;
					base.transform.localPosition = GameObject.Find("LOCAL_LAMINA_FICAR_6").gameObject.transform.position;
					base.transform.rotation = GameObject.Find("LOCAL_LAMINA_FICAR_6").gameObject.transform.rotation;
					POSSO_VIRAR_PARENTE_LOCAL_LAMINA = false;
				}
			}
		}
		if (MINIMIZAR_FUNCAO && SMARTBLOCK_LAMINA && collision.gameObject.transform.name == "PROCESSO_HOLDER_VENTOSA" && Administracao.PROCESSO_LAMINA_SENDO_SEGURADA_MANIPULADOR == null && POSSO_VIRAR_PARENTE_LOCAL_LAMINA)
		{
			MonoBehaviour.print("LÂMINA NA VENTOSA MANIPULADOR");
			GetComponent<Rigidbody>().useGravity = false;
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			base.transform.localPosition = collision.gameObject.transform.position;
			base.transform.parent = collision.gameObject.transform;
			Administracao.PROCESSO_LAMINA_SENDO_SEGURADA_MANIPULADOR = base.gameObject;
			MonoBehaviour.print("CORRIGIR ESCALA E ROTAÇÃO LAMINA + ATUALIZAR DADOS SOBRE ELA / SÓ PRECISA FAZER AQUI NA PROCESSO_HOLDER_VENTOSA_MESA NÃO PRECISA");
			base.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
			base.transform.localScale = new Vector3(0.008921498f, 0.05881323f, 0.003333329f);
		}
		if (MINIMIZAR_FUNCAO && SMARTBLOCK_LAMINA && collision.gameObject.transform.name == "PROCESSO_HOLDER_VENTOSA_MESA" && Administracao.PROCESSO_LAMINA_SENDO_SEGURADA_MESA == null)
		{
			MonoBehaviour.print("LÂMINA NA VENTOSA MANIPULADOR");
			GetComponent<Rigidbody>().useGravity = false;
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			base.transform.localPosition = collision.gameObject.transform.position;
			base.transform.parent = collision.gameObject.transform;
			Administracao.PROCESSO_LAMINA_SENDO_SEGURADA_MESA = base.gameObject;
		}
		if (MINIMIZAR_FUNCAO && SMARTBLOCK_LAMINA && collision.gameObject.transform.name == "QUALIDADE_HOLDER_BRACO" && collision.gameObject.transform.childCount < 1)
		{
			MonoBehaviour.print("[LÂMINA] COM [QUALIDADE_HOLDER_BRACO]");
			MonoBehaviour.print("[LÂMINA] cor " + COR_DA_LAMINA_ATUAL);
			Administracao.MONTAGEM_COR_LAMINA_ATUAL = COR_DA_LAMINA_ATUAL.ToString();
			GetComponent<Rigidbody>().useGravity = false;
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			base.transform.localPosition = collision.gameObject.transform.position;
			base.transform.parent = collision.gameObject.transform;
			MonoBehaviour.print("CORRIGIR ESCALA E ROTAÇÃO LAMINA + ATUALIZAR DADOS SOBRE ELA / SÓ PRECISA FAZER AQUI NA PROCESSO_HOLDER_VENTOSA_MESA NÃO PRECISA");
			base.transform.localScale = new Vector3(0.003316122f, 0.05881323f, 0.003333329f);
			base.transform.localPosition = new Vector3(0.262f, 0.23f, -0.039f);
			SCRIPT_QUALIDADE_ROBO_PARTE_11_3D.QUALIDADE_LAMINA_SENDO_SEGURADA_AGORA = base.gameObject;
		}
		if (SMARTBLOCK_TAMPA && collision.gameObject.transform.tag == "SMARTBLOCK_HOLDER" && !JA_TENHO_PARENTE)
		{
			_ = collision.gameObject.transform.childCount;
			_ = 1;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (MINIMIZAR_FUNCAO && SMARTBLOCK_LAMINA && collision.gameObject.transform.tag == "LOCAL_LAMINA_DESENHAR")
		{
			MonoBehaviour.print("LAMINA NO LOCAL DE DESENHAR");
			SCRIPT_COLISAO_LOCAL_LAMINA_DESENHAR.STATIC_LAMINA_NA_MESA = false;
		}
		if (SMARTBLOCK_BASE)
		{
			JA_ESTOU_COLIDINDO_ESTEIRA_1 = false;
			JA_ESTOU_COLIDINDO_ESTEIRA_2 = false;
		}
		if (SMARTBLOCK_BASE && collision.gameObject.transform.tag == "COLISAO_OBJETOS_ROTACIONAIS" && !JA_TENHO_PARENTE)
		{
			base.transform.parent = null;
		}
		if (SMARTBLOCK_TAMPA && collision.gameObject.transform.tag == "COLISAO_OBJETOS_ROTACIONAIS" && !JA_TENHO_PARENTE)
		{
			base.transform.parent = null;
		}
	}

	private void MENSAGEM_DELETAR_SMARTBLOCK()
	{
		if (!SMARTBLOCK_TAMPA)
		{
			base.transform.parent = null;
		}
		if (SMARTBLOCK_BASE)
		{
			Object.Destroy(base.gameObject);
		}
		if (SMARTBLOCK_TAMPA)
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void DESTRUIR_LAMINA_AGORA()
	{
		if (SMARTBLOCK_LAMINA && !JA_TENHO_PARENTE && LAMINA_ESTA_NA_BANCADA_PROCESSO && !SOU_LAMINA_POSICAO_1 && !SOU_LAMINA_POSICAO_2 && !SOU_LAMINA_POSICAO_3)
		{
			MonoBehaviour.print("destruir lamina");
			SCRIPT_COLISAO_LOCAL_LAMINA_DESENHAR.STATIC_LAMINA_NA_MESA = false;
			Administracao.PROCESSO_LAMINA_LOCAL_1_OBJETO_ATUAL = null;
			Administracao.PROCESSO_LAMINA_LOCAL_2_OBJETO_ATUAL = null;
			Administracao.PROCESSO_LAMINA_LOCAL_3_OBJETO_ATUAL = null;
			Administracao.PROCESSO_LAMINA_LOCAL_4_OBJETO_ATUAL = null;
			Administracao.PROCESSO_LAMINA_LOCAL_5_OBJETO_ATUAL = null;
			Administracao.PROCESSO_LAMINA_LOCAL_6_OBJETO_ATUAL = null;
		}
	}

	private void SMARTBLOCK_BASE_EMPURRADINHA()
	{
		GetComponent<Rigidbody>().WakeUp();
	}

	public void MANDAR_INFORMACOES_DO_BLOCO_PARA_QUALIDADE()
	{
		MonoBehaviour.print("NOVO COLIDIU COM TRIGGER QUALIDADE_DADOS_DO_BLOCO_TRIGGER");
		SCRIPT_QUALIDADE_DADOS.ATUAL_TAMPA_ANEXADA = TAMPA_ANEXADA;
		SCRIPT_QUALIDADE_DADOS.DESEJADO_ANDAR_DO_BLOCO = ANDAR_DO_BLOCO_DESEJADO;
		SCRIPT_QUALIDADE_DADOS.DESEJADO_TOTAL_DE_ANDARES = TOTAL_DE_ANDARES_DESEJADO;
		SCRIPT_QUALIDADE_DADOS.DESEJADO_COR_LAMINA_1 = COR_DA_LAMINA_1_DESEJADO;
		SCRIPT_QUALIDADE_DADOS.DESEJADO_COR_LAMINA_2 = COR_DA_LAMINA_2_DESEJADO;
		SCRIPT_QUALIDADE_DADOS.DESEJADO_COR_LAMINA_3 = COR_DA_LAMINA_3_DESEJADO;
		SCRIPT_QUALIDADE_DADOS.DESEJADO_PADRAO_LAMINA_1 = PADRAO_LAMINA_1_DESEJADO;
		SCRIPT_QUALIDADE_DADOS.DESEJADO_PADRAO_LAMINA_2 = PADRAO_LAMINA_2_DESEJADO;
		SCRIPT_QUALIDADE_DADOS.DESEJADO_PADRAO_LAMINA_3 = PADRAO_LAMINA_3_DESEJADO;
		SCRIPT_QUALIDADE_DADOS.DESEJADO_NUMERO_DO_PEDIDO = NUMERO_DO_PEDIDO_DESEJADO;
	}
}
