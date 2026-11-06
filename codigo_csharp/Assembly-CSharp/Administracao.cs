using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Administracao : MonoBehaviour
{
	private class Service
	{
		public string DoLogin(string username, string password, string urlBase, string pin, string ip, string code)
		{
			try
			{
				byte[] bytes = new UTF8Encoding().GetBytes(username + ":" + password);
				HttpClient httpClient = new HttpClient();
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
				httpClient.BaseAddress = new Uri(urlBase + "/api/auth");
				httpClient.Timeout = TimeSpan.FromMinutes(2.0);
				HttpResponseMessage result = httpClient.PostAsync(content: new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{ "pin", pin },
					{ "ip", ip },
					{ "code", code }
				}), requestUri: urlBase + "/api/auth").Result;
				string result2 = result.Content.ReadAsStringAsync().Result;
				if (result.StatusCode == HttpStatusCode.NotFound)
				{
					return result.StatusCode.ToString() + ";Certifique-se que a URL utilizada está correta";
				}
				JObject jObject = JsonConvert.DeserializeObject<JObject>(result2);
				if (result.StatusCode == HttpStatusCode.OK)
				{
					return jObject["code"].Value<int>() + ";" + jObject["data"]["access_token"].Value<string>() + ";" + jObject["data"]["expires_in"].Value<string>();
				}
				return jObject["code"].Value<int>() + ";" + jObject["data"]["errorMessage"].Value<string>();
			}
			catch (Exception)
			{
				return "408;Não foi possível se conectar com o servidor";
			}
		}

		public string DoVerify(string urlBase, string key)
		{
			try
			{
				HttpClient httpClient = new HttpClient();
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);
				httpClient.BaseAddress = new Uri(urlBase + "/api/verify");
				httpClient.Timeout = TimeSpan.FromMinutes(2.0);
				HttpResponseMessage result = httpClient.GetAsync(urlBase + "/api/verify").Result;
				string result2 = result.Content.ReadAsStringAsync().Result;
				if (result.StatusCode == HttpStatusCode.NotFound)
				{
					return result.StatusCode.ToString() + ";Certifique-se que a URL utilizada está correta";
				}
				JObject jObject = JsonConvert.DeserializeObject<JObject>(result2);
				if (result.StatusCode == HttpStatusCode.OK)
				{
					return jObject["code"].Value<int>() + ";" + jObject["data"]["success"].Value<string>();
				}
				return jObject["code"].Value<int>() + ";" + jObject["data"]["errorMessage"].Value<string>();
			}
			catch (Exception)
			{
				return "408;Não foi possível se conectar com o servidor";
			}
		}

		public string DoLogout(string urlBase, string key)
		{
			try
			{
				HttpClient httpClient = new HttpClient();
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);
				httpClient.BaseAddress = new Uri(urlBase + "/api/logout");
				httpClient.Timeout = TimeSpan.FromMinutes(2.0);
				HttpResponseMessage result = httpClient.PostAsync(urlBase + "/api/logout", null).Result;
				string result2 = result.Content.ReadAsStringAsync().Result;
				if (result.StatusCode == HttpStatusCode.NotFound)
				{
					return result.StatusCode.ToString() + ";Certifique-se que a URL utilizada está correta";
				}
				JObject jObject = JsonConvert.DeserializeObject<JObject>(result2);
				if (result.StatusCode == HttpStatusCode.OK)
				{
					return jObject["code"].Value<int>() + ";" + jObject["data"]["success"].Value<string>();
				}
				return jObject["code"].Value<int>() + ";" + jObject["data"]["errorMessage"].Value<string>();
			}
			catch (Exception)
			{
				return "408;Não foi possível se conectar com o servidor";
			}
		}
	}

	public string COMUNICACAO_ATUAL = "Manual";

	public static bool COMUNICACAO_MANUAL = false;

	public static bool COMUNICACAO_MODBUS_SLAVE = false;

	public static bool COMUNICACAO_MODBUS_MASTER = false;

	public static bool COMUNICACAO_SIEMENS = false;

	public GameObject TEXTO_COMUNICAO_ATUAL;

	public bool LOGIN_NECESSARIO = true;

	private Service login_service;

	public string token;

	public string erro_de_login;

	public int tempo_de_verificacao;

	public float ultimo_tempo_de_verificacao;

	public string LOGIN_USUARIO_SALVO_NO_PC;

	public string LOGIN_SENHA_SALVO_NO_PC;

	public string LOGIN_PIN_SALVO_NO_PC;

	public bool PODE_APARECER_BOTAO_INICIAR;

	public string LOGIN_USUARIO;

	public string LOGIN_SENHA;

	public string LOGIN_PIN;

	public bool MOSTRAR_BOTAO_ERRO_DE_CONECTIVIDADE;

	public bool MOSTRAR_BOTAO_ERRO_DE_LOGIN_INCORRETO;

	public GameObject LOCAL_ESTEIRA_1;

	public GameObject LOCAL_ESTEIRA_2;

	public GameObject LOCAL_ESTOQUE_1;

	public GameObject LOCAL_ESTOQUE_2;

	public GameObject LOCAL_ESTOQUE_3;

	public GameObject LOCAL_ESTOQUE_4;

	public GameObject LOCAL_ESTOQUE_5;

	public GameObject LOCAL_ESTOQUE_6;

	public GameObject LOCAL_ESTOQUE_7;

	public GameObject LOCAL_ESTOQUE_8;

	public GameObject LOCAL_ESTOQUE_9;

	public GameObject LOCAL_ESTOQUE_10;

	public GameObject LOCAL_ESTOQUE_11;

	public GameObject LOCAL_ESTOQUE_12;

	public GameObject LOCAL_ESTOQUE_13;

	public GameObject LOCAL_ESTOQUE_14;

	public GameObject LOCAL_ESTOQUE_15;

	public GameObject LOCAL_ESTOQUE_16;

	public GameObject LOCAL_ESTOQUE_17;

	public GameObject LOCAL_ESTOQUE_18;

	public GameObject LOCAL_ESTOQUE_19;

	public GameObject LOCAL_ESTOQUE_20;

	public GameObject LOCAL_ESTOQUE_21;

	public GameObject LOCAL_ESTOQUE_22;

	public GameObject LOCAL_ESTOQUE_23;

	public GameObject LOCAL_ESTOQUE_24;

	public GameObject LOCAL_ESTOQUE_25;

	public GameObject LOCAL_ESTOQUE_26;

	public GameObject LOCAL_ESTOQUE_27;

	public GameObject LOCAL_ESTOQUE_28;

	public AudioClip SOM_ESTEIRA_LIGADA;

	public AudioClip SOM_VACUO_LIGADO;

	public AudioClip SOM_ALIMENTADOR_ANDANDO;

	public AudioClip SOM_MESA_GIRANDO;

	public AudioClip SOM_COOLLER;

	public GameObject SOM_LAMINAS_BLOCO_NO_ELEVADOR;

	public GameObject SOM_GARRA_PEGOU;

	public GameObject SOM_VACUO_PEGOU_BLOCO;

	public GameObject SOM_BRACO_EM_POSICAO;

	public GameObject SOM_LAMINAS_EXPULSOR;

	public GameObject SOM_MICRO_CILINDRO_RECUAR;

	public GameObject SOM_MICRO_CILINDRO_AVANCAR;

	public bool ESTOQUE_PODE_TOCAR_SOM_MICRO_CILINDRO_1_AVANCAR = true;

	public bool ESTOQUE_PODE_TOCAR_SOM_MICRO_CILINDRO_1_RECUAR;

	public bool ESTOQUE_PODE_TOCAR_SOM_MICRO_CILINDRO_2_AVANCAR = true;

	public bool ESTOQUE_PODE_TOCAR_SOM_MICRO_CILINDRO_2_RECUAR;

	public bool QUALIDADE_PODE_TOCAR_SOM_MICRO_CILINDRO_1_AVANCAR = true;

	public bool QUALIDADE_PODE_TOCAR_SOM_MICRO_CILINDRO_1_RECUAR;

	public bool QUALIDADE_PODE_TOCAR_SOM_MICRO_CILINDRO_2_AVANCAR = true;

	public bool QUALIDADE_PODE_TOCAR_SOM_MICRO_CILINDRO_2_RECUAR;

	public bool EXPEDICAO_PODE_TOCAR_SOM_MICRO_CILINDRO_1_AVANCAR = true;

	public bool EXPEDICAO_PODE_TOCAR_SOM_MICRO_CILINDRO_1_RECUAR;

	public bool EXPEDICAO_PODE_TOCAR_SOM_MICRO_CILINDRO_2_AVANCAR = true;

	public bool EXPEDICAO_PODE_TOCAR_SOM_MICRO_CILINDRO_2_RECUAR;

	[Header("MODBUS")]
	public float DELAY_LER_CADA_ADDRESS_MODBUS = 0.5f;

	public GameObject TOGGLE_MODBUS;

	public static bool GRAVAR_DADOS_NO_MODBUS = false;

	public static bool LER_DADOS_DO_MODBUS = false;

	[Header("SMARTBLOCK")]
	public GameObject SMARTBLOCK_INSTANCIADOR;

	public GameObject SMARTBLOCK_INSTANCIADO;

	public GameObject SMARTBLOCK_OPCOES;

	public GameObject PREFAB_SMARTBLOCK_BASE_PRETO;

	public GameObject PREFAB_SMARTBLOCK_BASE_VERMELHO;

	public GameObject PREFAB_SMARTBLOCK_BASE_AZUL;

	public bool VAR_INSTANCIAR_SMARTBLOCK_TOGGLE;

	[Header("BANCADA ESTOQUE")]
	public GameObject ESTOQUE_ESTEIRA_COLISOES;

	public GameObject ESTOQUE_COLISAO_ESTEIRA_1;

	public GameObject ESTOQUE_COLISAO_ESTEIRA_2;

	public GameObject ESTOQUE_MICRO_CILINDRO_1_3D;

	public GameObject ESTOQUE_MICRO_CILINDRO_2_3D;

	public GameObject ESTOQUE_BRACO_3D;

	public GameObject ESTOQUE_BRACO_2_3D;

	public GameObject ESTOQUE_ELEVADOR_3D;

	public GameObject ESTOQUE_GARRA_ESQUERDA_3D;

	public GameObject ESTOQUE_GARRA_DIREITA_3D;

	public GameObject ESTOQUE_VENTOSA_3D;

	public GameObject ESTOQUE_ALIMENTADOR_3D;

	public GameObject ESTOQUE_HOLDER_GARRA;

	public GameObject ESTOQUE_HOLDER_VENTOSA;

	public AnimationClip ESTOQUE_MICRO_CILINDRO_1_POS_1;

	public AnimationClip ESTOQUE_MICRO_CILINDRO_1_POS_2;

	public AnimationClip ESTOQUE_MICRO_CILINDRO_2_POS_1;

	public AnimationClip ESTOQUE_MICRO_CILINDRO_2_POS_2;

	public AnimationClip ESTOQUE_GARRA_ESQUERDA_3D_ABRIR;

	public AnimationClip ESTOQUE_GARRA_ESQUERDA_3D_FECHAR;

	public AnimationClip ESTOQUE_GARRA_DIREITA_3D_ABRIR;

	public AnimationClip ESTOQUE_GARRA_DIREITA_3D_FECHAR;

	public AnimationClip ESTOQUE_BRACO_2_3D_AVANCAR;

	public AnimationClip ESTOQUE_BRACO_2_3D_RECUAR;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_0;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_1;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_2;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_3;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_4;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_5;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_6;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_7;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_8;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_9;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_10;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_11;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_12;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_13;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_14;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_15;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_16;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_17;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_18;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_19;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_20;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_21;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_22;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_23;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_24;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_25;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_26;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_27;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_28;

	public AnimationClip ESTOQUE_BRACO_3D_POSICAO_29;

	public float VELOCIDADE_ESTOQUE_BRACO_3D = 1f;

	public float VELOCIDADE_ESTOQUE_GARRA_3D = 1f;

	public float VELOCIDADE_ESTOQUE_BRACO_2_3D = 1f;

	public float VELOCIDADE_ESTOQUE_ALIMENTADOR_3D = 1f;

	public Vector3 ESTOQUE_BRACO_3D_ULTIMA_ROTACAO;

	public Vector3 ESTOQUE_ELEVADOR_3D_ULTIMA_POSICAO;

	public float FREQUENCIA_ESTOQUE_BRACO_3D_ROTACAO = 0.5f;

	private float ULTIMA_FREQUENCIA_ESTOQUE_BRACO_3D_ROTACAO;

	public float LIMITE_POSICAO_X_NEGATIVO_ESTOQUE_ALIMENTADOR_3D;

	public float LIMITE_POSICAO_X_POSITIVO_ESTOQUE_ALIMENTADOR_3D;

	public float LIMITE_POSICAO_Y_ESTOQUE_ALIMENTADOR_3D;

	public float LIMITE_POSICAO_Z_ESTOQUE_ALIMENTADOR_3D;

	public Vector3 POSICAO_ESTOQUE_HOLDER_VENTOSA_1;

	public Vector3 POSICAO_ESTOQUE_HOLDER_VENTOSA_2;

	public bool EXECUTANDO_ANIMACAO;

	public static bool MOSTRAR_BOTAO_ERRO_DE_COMUNICACAO = false;

	public static string MENSAGEM_ERRO_DE_COMUNICACAO;

	[Header("BANCADA PROCESSO")]
	public static GameObject PROCESSO_LAMINA_LOCAL_1_OBJETO_ATUAL;

	public GameObject PROCESSO_LAMINA_LOCAL_1_OBJETO_ATUAL_DEBUG;

	public static GameObject PROCESSO_LAMINA_LOCAL_2_OBJETO_ATUAL;

	public GameObject PROCESSO_LAMINA_LOCAL_2_OBJETO_ATUAL_DEBUG;

	public static GameObject PROCESSO_LAMINA_LOCAL_3_OBJETO_ATUAL;

	public GameObject PROCESSO_LAMINA_LOCAL_3_OBJETO_ATUAL_DEBUG;

	public static GameObject PROCESSO_LAMINA_LOCAL_4_OBJETO_ATUAL;

	public GameObject PROCESSO_LAMINA_LOCAL_4_OBJETO_ATUAL_DEBUG;

	public static GameObject PROCESSO_LAMINA_LOCAL_5_OBJETO_ATUAL;

	public GameObject PROCESSO_LAMINA_LOCAL_5_OBJETO_ATUAL_DEBUG;

	public static GameObject PROCESSO_LAMINA_LOCAL_6_OBJETO_ATUAL;

	public GameObject PROCESSO_LAMINA_LOCAL_6_OBJETO_ATUAL_DEBUG;

	public static GameObject PROCESSO_LAMINA_SENDO_SEGURADA_MANIPULADOR;

	public static GameObject PROCESSO_LAMINA_SENDO_SEGURADA_MESA;

	public GameObject PROCESSO_LOCAL_INSTANCIAR_LAMINA;

	public GameObject SMARTBLOCK_LAMINA_BRANCO;

	public GameObject SMARTBLOCK_LAMINA_PRETO;

	public GameObject SMARTBLOCK_LAMINA_VERDE;

	public GameObject SMARTBLOCK_LAMINA_AMARELO;

	public GameObject SMARTBLOCK_LAMINA_AZUL;

	public GameObject SMARTBLOCK_LAMINA_VERMELHO;

	public GameObject SMARTBLOCK_LAMINA_INSTANCIADA;

	public GameObject PROCESSO_ESTEIRA_COLISOES;

	public GameObject PROCESSO_COLISAO_ESTEIRA_1;

	public GameObject PROCESSO_COLISAO_ESTEIRA_2;

	public GameObject PROCESSO_VENTOSA_3D;

	public GameObject PROCESSO_ALIMENTADOR_3D;

	public GameObject PROCESSO_HOLDER_GARRA;

	public GameObject PROCESSO_HOLDER_VENTOSA;

	public GameObject PROCESSO_HOLDER_VENTOSA_MESA;

	public GameObject PROCESSO_ATUADOR_HORIZONTAL_3D;

	public GameObject PROCESSO_ATUADOR_FRONTAL_3D;

	public GameObject PROCESSO_ATUADOR_VERTICAL_3D;

	public AnimationClip PROCESSO_ATUADOR_HORIZONTAL_3D_BUSCAR_LAMINA_1;

	public AnimationClip PROCESSO_ATUADOR_HORIZONTAL_3D_BUSCAR_LAMINA_2;

	public AnimationClip PROCESSO_ATUADOR_HORIZONTAL_3D_BUSCAR_LAMINA_3;

	public AnimationClip PROCESSO_ATUADOR_HORIZONTAL_3D_BUSCAR_LAMINA_4;

	public AnimationClip PROCESSO_ATUADOR_HORIZONTAL_3D_BUSCAR_LAMINA_5;

	public AnimationClip PROCESSO_ATUADOR_HORIZONTAL_3D_BUSCAR_LAMINA_6;

	public AnimationClip PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO_CANETA;

	public AnimationClip PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO_INICIAL;

	public AnimationClip PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO_LARGAR_LAMINA_1;

	public AnimationClip PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO_LARGAR_LAMINA_2;

	public AnimationClip PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO_LARGAR_LAMINA_3;

	public AnimationClip PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO_LARGAR_LAMINA_MEIO;

	public AnimationClip PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO_PEGAR_LAMINA_MEIO;

	public Vector3 PROCESSO_ATUADOR_FRONTAL_3D_ULTIMA_POSICAO;

	public Vector3 PROCESSO_ATUADOR_HORIZONTAL_3D_ULTIMA_POSICAO;

	public float FREQUENCIA_PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO = 0.5f;

	private float ULTIMA_FREQUENCIA_PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO;

	public float VELOCIDADE_PROCESSO_ATUADOR_HORIZONTAL_3D = 1f;

	public float VELOCIDADE_PROCESSO_ALIMENTADOR_3D = 1f;

	public bool EXECUTANDO_ANIMACAO_PROCESSO;

	public float LIMITE_POSICAO_X_PROCESSO_ALIMENTADOR_3D;

	public float LIMITE_POSICAO_Y_PROCESSO_ALIMENTADOR_3D;

	public float LIMITE_POSICAO_Z_NEGATIVO_PROCESSO_ALIMENTADOR_3D;

	public float LIMITE_POSICAO_Z_POSITIVO_PROCESSO_ALIMENTADOR_3D;

	[Header("BANCADA QUALIDADE")]
	public GameObject QUALIDADE_ESTEIRA_COLISOES;

	public GameObject QUALIDADE_COLISAO_ESTEIRA_1;

	public GameObject QUALIDADE_COLISAO_ESTEIRA_2;

	public GameObject QUALIDADE_MICRO_CILINDRO_1_3D;

	public GameObject QUALIDADE_MICRO_CILINDRO_2_3D;

	public GameObject QUALIDADE_ALIMENTADOR_3D;

	public GameObject QUALIDADE_VENTOSA_3D;

	public GameObject QUALIDADE_HOLDER_VENTOSA;

	public GameObject QUALIDADE_ROBO_PARTE_11_3D;

	public AnimationClip QUALIDADE_MICRO_CILINDRO_1_POS_1;

	public AnimationClip QUALIDADE_MICRO_CILINDRO_1_POS_2;

	public AnimationClip QUALIDADE_MICRO_CILINDRO_2_POS_1;

	public AnimationClip QUALIDADE_MICRO_CILINDRO_2_POS_2;

	public AnimationClip QUALIDADE_INSPECIONAR_PARADO;

	public AnimationClip QUALIDADE_INSPECIONAR_PARTE_1;

	public AnimationClip QUALIDADE_INSPECIONAR_PARTE_2;

	public AnimationClip QUALIDADE_INSPECIONAR_PARTE_3;

	public AnimationClip QUALIDADE_INSPECIONAR_PARTE_4;

	public AnimationClip QUALIDADE_INSPECAO_FALHOU_PARTE_1;

	public AnimationClip QUALIDADE_INSPECAO_FALHOU_PARTE_2;

	public float VELOCIDADE_QUALIDADE_ALIMENTADOR_3D = 1f;

	public float VELOCIDADE_QUALIDADE_INSPECIONAR = 1f;

	public float VELOCIDADE_QUALIDADE_ROBO = 1f;

	public float LIMITE_POSICAO_X_NEGATIVO_QUALIDADE_ALIMENTADOR_3D;

	public float LIMITE_POSICAO_X_POSITIVO_QUALIDADE_ALIMENTADOR_3D;

	public float LIMITE_POSICAO_Y_QUALIDADE_ALIMENTADOR_3D;

	public float LIMITE_POSICAO_Z_QUALIDADE_ALIMENTADOR_3D;

	public Vector3 POSICAO_QUALIDADE_HOLDER_VENTOSA_1;

	public Vector3 POSICAO_QUALIDADE_HOLDER_VENTOSA_2;

	public static int ANDARES_DE_BLOCOS_QUALIDADE = 0;

	public static bool EXECUTANDO_ANIMACAO_QUALIDADE = false;

	public bool PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo;

	public GameObject GAMEOBJECT_FLASH_1;

	public GameObject LOCAL_INSTANCIAR_GAMEOBJECT_FLASH_1;

	public static bool PODE_INSTANCIAR_FLASH = true;

	public static string MONTAGEM_COR_LAMINA_ATUAL = "0";

	[Header("EXPEDIÇÃO")]
	public GameObject EXPEDICAO_ESTEIRA_COLISOES;

	public GameObject EXPEDICAO_COLISAO_ESTEIRA_1;

	public GameObject EXPEDICAO_COLISAO_ESTEIRA_2;

	public GameObject EXPEDICAO_MICRO_CILINDRO_1_3D;

	public GameObject EXPEDICAO_MICRO_CILINDRO_2_3D;

	public GameObject EXPEDICAO_ALIMENTADOR_3D;

	public GameObject EXPEDICAO_VENTOSA_3D;

	public GameObject EXPEDICAO_HOLDER_VENTOSA;

	public GameObject EXPEDICAO_BRACO_ALTURA_3D;

	public GameObject EXPEDICAO_BRACO_HORIZONTAL_3D;

	public GameObject EXPEDICAO_BRACO_ROTACAO_3D;

	public AnimationClip EXPEDICAO_MICRO_CILINDRO_1_POS_1;

	public AnimationClip EXPEDICAO_MICRO_CILINDRO_1_POS_2;

	public AnimationClip EXPEDICAO_MICRO_CILINDRO_2_POS_1;

	public AnimationClip EXPEDICAO_MICRO_CILINDRO_2_POS_2;

	public float VELOCIDADE_EXPEDICAO_ALIMENTADOR_3D = 1f;

	public float VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D = 1f;

	public float VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_0_ANIMACAO = 1f;

	public AnimationClip EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_0;

	public AnimationClip EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_1;

	public AnimationClip EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_2;

	public AnimationClip EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_3;

	public AnimationClip EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_4;

	public AnimationClip EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_5;

	public AnimationClip EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_6;

	public AnimationClip EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_7;

	public AnimationClip EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_8;

	public AnimationClip EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_9;

	public AnimationClip EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_10;

	public AnimationClip EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_11;

	public AnimationClip EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_12;

	public AnimationClip EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_13;

	public AnimationClip EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_13_DO_14_PRO_13;

	public AnimationClip EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_14;

	public Vector3 EXPEDICAO_BRACO_ROTACAO_3D_ULTIMA_ROTACAO;

	public Vector3 EXPEDICAO_BRACO_HORIZONTAL_3D_ULTIMA_POSICAO;

	public Vector3 EXPEDICAO_BRACO_ALTURA_3D_ULTIMA_POSICAO;

	public float FREQUENCIA_EXPEDICAO_BRACO_ROTACAO_3D = 0.5f;

	private float ULTIMA_FREQUENCIA_EXPEDICAO_BRACO_ROTACAO_3D;

	public bool EXECUTANDO_ANIMACAO_EXPEDICAO;

	public float LIMITE_POSICAO_X_NEGATIVO_EXPEDICAO_ALIMENTADOR_3D;

	public float LIMITE_POSICAO_X_POSITIVO_EXPEDICAO_ALIMENTADOR_3D;

	public float LIMITE_POSICAO_Y_EXPEDICAO_ALIMENTADOR_3D;

	public float LIMITE_POSICAO_Z_EXPEDICAO_ALIMENTADOR_3D;

	public Vector3 POSICAO_EXPEDICAO_HOLDER_VENTOSA_1;

	public Vector3 POSICAO_EXPEDICAO_HOLDER_VENTOSA_2;

	public float LIMITE_POSICAO_X_EXPEDICAO_BRACO_ALTURA_3D;

	public float LIMITE_POSICAO_Y_EXPEDICAO_BRACO_ALTURA_3D;

	public float LIMITE_POSICAO_Z_NEGATIVO_EXPEDICAO_BRACO_ALTURA_3D;

	public float LIMITE_POSICAO_Z_POSITIVO_EXPEDICAO_BRACO_ALTURA_3D;

	public float LIMITE_POSICAO_X_EXPEDICAO_BRACO_HORIZONTAL_3D;

	public float LIMITE_POSICAO_Y_NEGATIVO_EXPEDICAO_BRACO_HORIZONTAL_3D;

	public float LIMITE_POSICAO_Y_POSITIVO_EXPEDICAO_BRACO_HORIZONTAL_3D;

	public float LIMITE_POSICAO_Z_EXPEDICAO_BRACO_HORIZONTAL_3D;

	[Header("RETORNO ESQUERDA")]
	public GameObject RETORNO_ESQUERDA_BASE_3D;

	public GameObject RETORNO_ESQUERDA_COLISOES;

	public GameObject RETORNO_ESQUERDA_BASE_COLISAO;

	public GameObject RETORNO_ESQUERDA_GIRO_COLISAO;

	public GameObject RETORNO_ESQUERA_COLISAO_ADICIONAL;

	public GameObject RETORNO_ESQUERDA_CIRCULO_3D;

	public bool RETORNO_ESQUERDA;

	[Header("RETORNO DIREITA")]
	public GameObject RETORNO_DIREITA_BASE_3D;

	public GameObject RETORNO_DIREITA_COLISOES;

	public GameObject RETORNO_DIREITA_BASE_COLISAO;

	public GameObject RETORNO_DIREITA_GIRO_COLISAO;

	public GameObject RETORNO_DIREITA_COLISAO_ADICIONAL;

	public GameObject RETORNO_DIREITA_CIRCULO_3D;

	public bool RETORNO_DIREITA;

	[Header("ETC")]
	public static string BANCADA_1;

	public static string BANCADA_2;

	public static string BANCADA_3;

	public static string BANCADA_4;

	public float VELOCIDADE_MICRO_CILINDRO = 1f;

	public float FREQUENCIA_ADD_SMARTBLOCK;

	private float ULTIMO_ADD_SMARTBLOCK;

	public float VELOCIDADE_ROTACIONAR_RETORNOS = 250f;

	public bool ADICIONAR_ANIMACOES_AUTOMATICAMENTE = true;

	public float VELOCIDADE_ANIMACAO_1 = 0.3f;

	public bool MENU_QUIT_GAME;

	public GameObject FADE_1;

	public GameObject CAMERA;

	public GameObject CAMERA_VISUAL;

	public GameObject OPCOES_UI;

	public GameObject BANCADAS_ATIVAR_UI;

	public GameObject BANCADAS_INPUTS_UI;

	public GameObject SMARTBLOCK_UI;

	public bool MINIMIZAR_FUNCAO = true;

	public bool reiniciar_servidor_agora;

	public bool mostrar_menu;

	public static bool menu_options = false;

	public Texture2D imagem_logo;

	public Texture2D imagem_logo_senai;

	public GUISkin skin_1;

	public GUISkin skin_2;

	public GUISkin skin_login;

	public GUISkin skin_erro_internet;

	public GUISkin skin_ERRO_COMUNICAO;

	public GUISkin skin_login_placeholder;

	public GUISkin skin_options;

	public GUISkin skin_fechar_aplicativo;

	public GUISkin SKIN_COMUNICACAO_ESCOLHIDA;

	public Texture2D textura_comandos_camera;

	public Texture2D logo_auttom;

	public GameObject som_botao_1;

	public GameObject som_botao_2;

	public GameObject som_botao_3;

	public GameObject som_botao_instanciar;

	public GameObject som_botao_delete;

	public GameObject modbus_menu;

	public GameObject CAMERA_POS_1;

	public GameObject CAMERA_POS_2;

	public GameObject CAMERA_POS_3;

	public GameObject CAMERA_POS_4;

	private void VOID_VERIFICAR_LOGIN()
	{
		if (LOGIN_NECESSARIO)
		{
			string[] array = login_service.DoVerify("https://simulators.labtronix.com.br", token).Split(';');
			MonoBehaviour.print("VERIFICAR LOGIN");
			if (array[0] != "200")
			{
				MOSTRAR_BOTAO_ERRO_DE_CONECTIVIDADE = true;
				MonoBehaviour.print("ERRO DO SERVIDOR");
			}
		}
	}

	private void MENSAGEM_BOTAO_MANUAL()
	{
		COMUNICACAO_ATUAL = "Manual";
		COMUNICACAO_MANUAL = true;
		COMUNICACAO_MODBUS_SLAVE = false;
		COMUNICACAO_MODBUS_MASTER = false;
		COMUNICACAO_SIEMENS = false;
	}

	private void MENSAGEM_BOTAO_MODBUS_SLAVE()
	{
		COMUNICACAO_ATUAL = "Modbus Slave";
		COMUNICACAO_MANUAL = false;
		COMUNICACAO_MODBUS_SLAVE = true;
		COMUNICACAO_MODBUS_MASTER = false;
		COMUNICACAO_SIEMENS = false;
	}

	private void MENSAGEM_BOTAO_MODBUS_MASTER()
	{
		COMUNICACAO_ATUAL = "Modubs Master";
		COMUNICACAO_MANUAL = false;
		COMUNICACAO_MODBUS_SLAVE = false;
		COMUNICACAO_MODBUS_MASTER = true;
		COMUNICACAO_SIEMENS = false;
	}

	private void MENSAGEM_BOTAO_SIEMENS()
	{
		COMUNICACAO_ATUAL = "Siemens";
		COMUNICACAO_MANUAL = false;
		COMUNICACAO_MODBUS_SLAVE = false;
		COMUNICACAO_MODBUS_MASTER = false;
		COMUNICACAO_SIEMENS = true;
	}

	private void Start()
	{
		if (LOGIN_NECESSARIO)
		{
			login_service = new Service();
		}
		if (MINIMIZAR_FUNCAO)
		{
			ESTOQUE_ESTEIRA_COLISOES = GameObject.Find("ESTOQUE_ESTEIRA_COLISOES");
			ESTOQUE_COLISAO_ESTEIRA_1 = GameObject.Find("ESTOQUE_COLISAO_ESTEIRA_1");
			ESTOQUE_COLISAO_ESTEIRA_2 = GameObject.Find("ESTOQUE_COLISAO_ESTEIRA_2");
			ESTOQUE_MICRO_CILINDRO_1_3D = GameObject.Find("ESTOQUE_MICRO_CILINDRO_1_3D");
			ESTOQUE_MICRO_CILINDRO_2_3D = GameObject.Find("ESTOQUE_MICRO_CILINDRO_2_3D");
			ESTOQUE_BRACO_3D = GameObject.Find("ESTOQUE_BRACO_3D");
			ESTOQUE_BRACO_2_3D = GameObject.Find("ESTOQUE_BRACO_2_3D");
			ESTOQUE_ELEVADOR_3D = GameObject.Find("ESTOQUE_ELEVADOR_3D");
			ESTOQUE_GARRA_ESQUERDA_3D = GameObject.Find("ESTOQUE_GARRA_ESQUERDA_3D");
			ESTOQUE_GARRA_DIREITA_3D = GameObject.Find("ESTOQUE_GARRA_DIREITA_3D");
			ESTOQUE_VENTOSA_3D = GameObject.Find("ESTOQUE_VENTOSA_3D");
			ESTOQUE_ALIMENTADOR_3D = GameObject.Find("ESTOQUE_ALIMENTADOR_3D");
			ESTOQUE_HOLDER_GARRA = GameObject.Find("ESTOQUE_HOLDER_GARRA");
			ESTOQUE_HOLDER_VENTOSA = GameObject.Find("ESTOQUE_HOLDER_VENTOSA");
			ESTOQUE_ESTEIRA_COLISOES.transform.parent = GameObject.Find("ESTOQUE_3D").transform;
			ESTOQUE_MICRO_CILINDRO_1_3D.AddComponent<BoxCollider>();
			ESTOQUE_MICRO_CILINDRO_1_3D.GetComponent<BoxCollider>().size = new Vector3(94.34981f, 2f, 136.8396f);
			ESTOQUE_MICRO_CILINDRO_2_3D.AddComponent<BoxCollider>();
			ESTOQUE_MICRO_CILINDRO_2_3D.GetComponent<BoxCollider>().size = new Vector3(94.34981f, 2f, 136.8396f);
			ESTOQUE_VENTOSA_3D.AddComponent<BoxCollider>();
			ESTOQUE_VENTOSA_3D.AddComponent<Rigidbody>();
			ESTOQUE_VENTOSA_3D.GetComponent<Rigidbody>().useGravity = false;
			ESTOQUE_VENTOSA_3D.GetComponent<Rigidbody>().isKinematic = true;
			ESTOQUE_VENTOSA_3D.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			ESTOQUE_VENTOSA_3D.GetComponent<BoxCollider>().size = new Vector3(13.94f, 109.4857f, 108.6916f);
			ESTOQUE_VENTOSA_3D.GetComponent<BoxCollider>().center = new Vector3(-27.5f, 0.03709413f, 0.5148337f);
			if (ADICIONAR_ANIMACOES_AUTOMATICAMENTE)
			{
				ESTOQUE_BRACO_3D.AddComponent<Animation>();
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_0, "ESTOQUE_BRACO_3D_POSICAO_0");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_1, "ESTOQUE_BRACO_3D_POSICAO_1");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_2, "ESTOQUE_BRACO_3D_POSICAO_2");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_3, "ESTOQUE_BRACO_3D_POSICAO_3");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_4, "ESTOQUE_BRACO_3D_POSICAO_4");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_5, "ESTOQUE_BRACO_3D_POSICAO_5");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_6, "ESTOQUE_BRACO_3D_POSICAO_6");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_7, "ESTOQUE_BRACO_3D_POSICAO_7");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_8, "ESTOQUE_BRACO_3D_POSICAO_8");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_9, "ESTOQUE_BRACO_3D_POSICAO_9");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_10, "ESTOQUE_BRACO_3D_POSICAO_10");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_11, "ESTOQUE_BRACO_3D_POSICAO_11");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_12, "ESTOQUE_BRACO_3D_POSICAO_12");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_13, "ESTOQUE_BRACO_3D_POSICAO_13");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_14, "ESTOQUE_BRACO_3D_POSICAO_14");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_15, "ESTOQUE_BRACO_3D_POSICAO_15");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_16, "ESTOQUE_BRACO_3D_POSICAO_16");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_17, "ESTOQUE_BRACO_3D_POSICAO_17");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_18, "ESTOQUE_BRACO_3D_POSICAO_18");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_19, "ESTOQUE_BRACO_3D_POSICAO_19");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_20, "ESTOQUE_BRACO_3D_POSICAO_20");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_21, "ESTOQUE_BRACO_3D_POSICAO_21");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_22, "ESTOQUE_BRACO_3D_POSICAO_22");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_23, "ESTOQUE_BRACO_3D_POSICAO_23");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_24, "ESTOQUE_BRACO_3D_POSICAO_24");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_25, "ESTOQUE_BRACO_3D_POSICAO_25");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_26, "ESTOQUE_BRACO_3D_POSICAO_26");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_27, "ESTOQUE_BRACO_3D_POSICAO_27");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_28, "ESTOQUE_BRACO_3D_POSICAO_28");
				ESTOQUE_BRACO_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_3D_POSICAO_29, "ESTOQUE_BRACO_3D_POSICAO_29");
				ESTOQUE_MICRO_CILINDRO_1_3D.AddComponent<Animation>();
				ESTOQUE_MICRO_CILINDRO_1_3D.GetComponent<Animation>().AddClip(ESTOQUE_MICRO_CILINDRO_1_POS_1, "ESTOQUE_MICRO_CILINDRO_1_POS_1");
				ESTOQUE_MICRO_CILINDRO_1_3D.GetComponent<Animation>().AddClip(ESTOQUE_MICRO_CILINDRO_1_POS_2, "ESTOQUE_MICRO_CILINDRO_1_POS_2");
				ESTOQUE_MICRO_CILINDRO_2_3D.AddComponent<Animation>();
				ESTOQUE_MICRO_CILINDRO_2_3D.GetComponent<Animation>().AddClip(ESTOQUE_MICRO_CILINDRO_2_POS_1, "ESTOQUE_MICRO_CILINDRO_2_POS_1");
				ESTOQUE_MICRO_CILINDRO_2_3D.GetComponent<Animation>().AddClip(ESTOQUE_MICRO_CILINDRO_2_POS_2, "ESTOQUE_MICRO_CILINDRO_2_POS_2");
				ESTOQUE_GARRA_ESQUERDA_3D.AddComponent<Animation>();
				ESTOQUE_GARRA_ESQUERDA_3D.GetComponent<Animation>().AddClip(ESTOQUE_GARRA_ESQUERDA_3D_ABRIR, "ESTOQUE_GARRA_ESQUERDA_3D_ABRIR");
				ESTOQUE_GARRA_ESQUERDA_3D.GetComponent<Animation>().AddClip(ESTOQUE_GARRA_ESQUERDA_3D_FECHAR, "ESTOQUE_GARRA_ESQUERDA_3D_FECHAR");
				ESTOQUE_GARRA_DIREITA_3D.AddComponent<Animation>();
				ESTOQUE_GARRA_DIREITA_3D.GetComponent<Animation>().AddClip(ESTOQUE_GARRA_DIREITA_3D_ABRIR, "ESTOQUE_GARRA_DIREITA_3D_ABRIR");
				ESTOQUE_GARRA_DIREITA_3D.GetComponent<Animation>().AddClip(ESTOQUE_GARRA_DIREITA_3D_FECHAR, "ESTOQUE_GARRA_DIREITA_3D_FECHAR");
				ESTOQUE_BRACO_2_3D.AddComponent<Animation>();
				ESTOQUE_BRACO_2_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_2_3D_AVANCAR, "ESTOQUE_BRACO_2_3D_AVANCAR");
				ESTOQUE_BRACO_2_3D.GetComponent<Animation>().AddClip(ESTOQUE_BRACO_2_3D_RECUAR, "ESTOQUE_BRACO_2_3D_RECUAR");
			}
			ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_0", VELOCIDADE_ESTOQUE_BRACO_3D);
			if (MINIMIZAR_FUNCAO)
			{
				ESTOQUE_COLISAO_ESTEIRA_1.AddComponent<AudioSource>();
				ESTOQUE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().loop = true;
				ESTOQUE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().spatialBlend = 1f;
				ESTOQUE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().dopplerLevel = 0f;
				ESTOQUE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().minDistance = 10f;
				ESTOQUE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().clip = SOM_ESTEIRA_LIGADA;
				ESTOQUE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().playOnAwake = true;
				ESTOQUE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().Play();
				ESTOQUE_COLISAO_ESTEIRA_2.AddComponent<AudioSource>();
				ESTOQUE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().loop = true;
				ESTOQUE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().spatialBlend = 1f;
				ESTOQUE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().dopplerLevel = 0f;
				ESTOQUE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().minDistance = 10f;
				ESTOQUE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().clip = SOM_ESTEIRA_LIGADA;
				ESTOQUE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().playOnAwake = true;
				ESTOQUE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().Play();
				ESTOQUE_ALIMENTADOR_3D.AddComponent<AudioSource>();
				ESTOQUE_ALIMENTADOR_3D.GetComponent<AudioSource>().pitch = 0.5f;
				ESTOQUE_ALIMENTADOR_3D.GetComponent<AudioSource>().loop = true;
				ESTOQUE_ALIMENTADOR_3D.GetComponent<AudioSource>().spatialBlend = 1f;
				ESTOQUE_ALIMENTADOR_3D.GetComponent<AudioSource>().dopplerLevel = 0f;
				ESTOQUE_ALIMENTADOR_3D.GetComponent<AudioSource>().minDistance = 20f;
				ESTOQUE_ALIMENTADOR_3D.GetComponent<AudioSource>().clip = SOM_ALIMENTADOR_ANDANDO;
				ESTOQUE_ALIMENTADOR_3D.GetComponent<AudioSource>().playOnAwake = true;
				ESTOQUE_ALIMENTADOR_3D.GetComponent<AudioSource>().Play();
				ESTOQUE_HOLDER_VENTOSA.AddComponent<AudioSource>();
				ESTOQUE_HOLDER_VENTOSA.GetComponent<AudioSource>().loop = true;
				ESTOQUE_HOLDER_VENTOSA.GetComponent<AudioSource>().spatialBlend = 1f;
				ESTOQUE_HOLDER_VENTOSA.GetComponent<AudioSource>().dopplerLevel = 0f;
				ESTOQUE_HOLDER_VENTOSA.GetComponent<AudioSource>().minDistance = 20f;
				ESTOQUE_HOLDER_VENTOSA.GetComponent<AudioSource>().clip = SOM_VACUO_LIGADO;
				ESTOQUE_HOLDER_VENTOSA.GetComponent<AudioSource>().playOnAwake = true;
				ESTOQUE_HOLDER_VENTOSA.GetComponent<AudioSource>().Play();
			}
		}
		if (MINIMIZAR_FUNCAO)
		{
			PROCESSO_LOCAL_INSTANCIAR_LAMINA = GameObject.Find("PROCESSO_LOCAL_INSTANCIAR_LAMINA");
			PROCESSO_ESTEIRA_COLISOES = GameObject.Find("PROCESSO_ESTEIRA_COLISOES");
			PROCESSO_COLISAO_ESTEIRA_1 = GameObject.Find("PROCESSO_COLISAO_ESTEIRA_1");
			PROCESSO_COLISAO_ESTEIRA_2 = GameObject.Find("PROCESSO_COLISAO_ESTEIRA_2");
			PROCESSO_VENTOSA_3D = GameObject.Find("PROCESSO_VENTOSA_3D");
			PROCESSO_ALIMENTADOR_3D = GameObject.Find("PROCESSO_ATUADOR_VERTICAL_3D");
			PROCESSO_HOLDER_GARRA = GameObject.Find("PROCESSO_HOLDER_GARRA");
			PROCESSO_HOLDER_VENTOSA = GameObject.Find("PROCESSO_HOLDER_VENTOSA");
			PROCESSO_ATUADOR_HORIZONTAL_3D = GameObject.Find("PROCESSO_ATUADOR_HORIZONTAL_3D");
			PROCESSO_ATUADOR_FRONTAL_3D = GameObject.Find("PROCESSO_ATUADOR_FRONTAL_3D");
			PROCESSO_ATUADOR_VERTICAL_3D = GameObject.Find("PROCESSO_ATUADOR_VERTICAL_3D");
			PROCESSO_ESTEIRA_COLISOES.transform.parent = GameObject.Find("PROCESSO_3D").transform;
			PROCESSO_VENTOSA_3D.AddComponent<BoxCollider>();
			PROCESSO_VENTOSA_3D.AddComponent<Rigidbody>();
			PROCESSO_VENTOSA_3D.GetComponent<Rigidbody>().useGravity = false;
			PROCESSO_VENTOSA_3D.GetComponent<Rigidbody>().isKinematic = true;
			PROCESSO_VENTOSA_3D.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			PROCESSO_VENTOSA_3D.GetComponent<BoxCollider>().size = new Vector3(13.94f, 109.4857f, 108.6916f);
			PROCESSO_VENTOSA_3D.GetComponent<BoxCollider>().center = new Vector3(-27.5f, 0.03709413f, 0.5148337f);
			_ = ADICIONAR_ANIMACOES_AUTOMATICAMENTE;
			if (MINIMIZAR_FUNCAO)
			{
				PROCESSO_COLISAO_ESTEIRA_1.AddComponent<AudioSource>();
				PROCESSO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().loop = true;
				PROCESSO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().spatialBlend = 1f;
				PROCESSO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().dopplerLevel = 0f;
				PROCESSO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().minDistance = 10f;
				PROCESSO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().clip = SOM_ESTEIRA_LIGADA;
				PROCESSO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().playOnAwake = true;
				PROCESSO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().Play();
				PROCESSO_COLISAO_ESTEIRA_2.AddComponent<AudioSource>();
				PROCESSO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().loop = true;
				PROCESSO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().spatialBlend = 1f;
				PROCESSO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().dopplerLevel = 0f;
				PROCESSO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().minDistance = 10f;
				PROCESSO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().clip = SOM_ESTEIRA_LIGADA;
				PROCESSO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().playOnAwake = true;
				PROCESSO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().Play();
				PROCESSO_ALIMENTADOR_3D.AddComponent<AudioSource>();
				PROCESSO_ALIMENTADOR_3D.GetComponent<AudioSource>().pitch = 0.5f;
				PROCESSO_ALIMENTADOR_3D.GetComponent<AudioSource>().loop = true;
				PROCESSO_ALIMENTADOR_3D.GetComponent<AudioSource>().spatialBlend = 1f;
				PROCESSO_ALIMENTADOR_3D.GetComponent<AudioSource>().dopplerLevel = 0f;
				PROCESSO_ALIMENTADOR_3D.GetComponent<AudioSource>().minDistance = 20f;
				PROCESSO_ALIMENTADOR_3D.GetComponent<AudioSource>().clip = SOM_ALIMENTADOR_ANDANDO;
				PROCESSO_ALIMENTADOR_3D.GetComponent<AudioSource>().playOnAwake = true;
				PROCESSO_ALIMENTADOR_3D.GetComponent<AudioSource>().Play();
				PROCESSO_HOLDER_VENTOSA.AddComponent<AudioSource>();
				PROCESSO_HOLDER_VENTOSA.GetComponent<AudioSource>().loop = true;
				PROCESSO_HOLDER_VENTOSA.GetComponent<AudioSource>().spatialBlend = 1f;
				PROCESSO_HOLDER_VENTOSA.GetComponent<AudioSource>().dopplerLevel = 0f;
				PROCESSO_HOLDER_VENTOSA.GetComponent<AudioSource>().minDistance = 20f;
				PROCESSO_HOLDER_VENTOSA.GetComponent<AudioSource>().clip = SOM_VACUO_LIGADO;
				PROCESSO_HOLDER_VENTOSA.GetComponent<AudioSource>().playOnAwake = true;
				PROCESSO_HOLDER_VENTOSA.GetComponent<AudioSource>().Play();
				PROCESSO_HOLDER_VENTOSA_MESA.AddComponent<AudioSource>();
				PROCESSO_HOLDER_VENTOSA_MESA.GetComponent<AudioSource>().loop = true;
				PROCESSO_HOLDER_VENTOSA_MESA.GetComponent<AudioSource>().spatialBlend = 1f;
				PROCESSO_HOLDER_VENTOSA_MESA.GetComponent<AudioSource>().dopplerLevel = 0f;
				PROCESSO_HOLDER_VENTOSA_MESA.GetComponent<AudioSource>().minDistance = 20f;
				PROCESSO_HOLDER_VENTOSA_MESA.GetComponent<AudioSource>().clip = SOM_VACUO_LIGADO;
				PROCESSO_HOLDER_VENTOSA_MESA.GetComponent<AudioSource>().playOnAwake = true;
				PROCESSO_HOLDER_VENTOSA_MESA.GetComponent<AudioSource>().Play();
			}
		}
		if (MINIMIZAR_FUNCAO)
		{
			QUALIDADE_ESTEIRA_COLISOES = GameObject.Find("QUALIDADE_ESTEIRA_COLISOES");
			QUALIDADE_COLISAO_ESTEIRA_1 = GameObject.Find("QUALIDADE_COLISAO_ESTEIRA_1");
			QUALIDADE_COLISAO_ESTEIRA_2 = GameObject.Find("QUALIDADE_COLISAO_ESTEIRA_2");
			QUALIDADE_MICRO_CILINDRO_1_3D = GameObject.Find("QUALIDADE_MICRO_CILINDRO_1_3D");
			QUALIDADE_MICRO_CILINDRO_2_3D = GameObject.Find("QUALIDADE_MICRO_CILINDRO_2_3D");
			QUALIDADE_ALIMENTADOR_3D = GameObject.Find("QUALIDADE_ALIMENTADOR_3D");
			QUALIDADE_VENTOSA_3D = GameObject.Find("QUALIDADE_VENTOSA_3D");
			QUALIDADE_HOLDER_VENTOSA = GameObject.Find("QUALIDADE_HOLDER_VENTOSA");
			QUALIDADE_ROBO_PARTE_11_3D = GameObject.Find("QUALIDADE_ROBO_PARTE_11_3D");
			QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_24_HOME", VELOCIDADE_QUALIDADE_ROBO);
			QUALIDADE_ESTEIRA_COLISOES.transform.parent = GameObject.Find("QUALIDADE_3D").transform;
			QUALIDADE_MICRO_CILINDRO_1_3D.AddComponent<BoxCollider>();
			QUALIDADE_MICRO_CILINDRO_1_3D.GetComponent<BoxCollider>().size = new Vector3(94.34981f, 2f, 136.8396f);
			QUALIDADE_MICRO_CILINDRO_2_3D.AddComponent<BoxCollider>();
			QUALIDADE_MICRO_CILINDRO_2_3D.GetComponent<BoxCollider>().size = new Vector3(94.34981f, 2f, 136.8396f);
			QUALIDADE_VENTOSA_3D.AddComponent<BoxCollider>();
			QUALIDADE_VENTOSA_3D.AddComponent<Rigidbody>();
			QUALIDADE_VENTOSA_3D.GetComponent<Rigidbody>().useGravity = false;
			QUALIDADE_VENTOSA_3D.GetComponent<Rigidbody>().isKinematic = true;
			QUALIDADE_VENTOSA_3D.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			QUALIDADE_VENTOSA_3D.GetComponent<BoxCollider>().size = new Vector3(13.94f, 109.4857f, 108.6916f);
			QUALIDADE_VENTOSA_3D.GetComponent<BoxCollider>().center = new Vector3(-27.5f, 0.03709413f, 0.5148337f);
			if (ADICIONAR_ANIMACOES_AUTOMATICAMENTE)
			{
				QUALIDADE_MICRO_CILINDRO_1_3D.AddComponent<Animation>();
				QUALIDADE_MICRO_CILINDRO_1_3D.GetComponent<Animation>().AddClip(QUALIDADE_MICRO_CILINDRO_1_POS_1, "QUALIDADE_MICRO_CILINDRO_1_POS_1");
				QUALIDADE_MICRO_CILINDRO_1_3D.GetComponent<Animation>().AddClip(QUALIDADE_MICRO_CILINDRO_1_POS_2, "QUALIDADE_MICRO_CILINDRO_1_POS_2");
				QUALIDADE_MICRO_CILINDRO_2_3D.AddComponent<Animation>();
				QUALIDADE_MICRO_CILINDRO_2_3D.GetComponent<Animation>().AddClip(QUALIDADE_MICRO_CILINDRO_2_POS_1, "QUALIDADE_MICRO_CILINDRO_2_POS_1");
				QUALIDADE_MICRO_CILINDRO_2_3D.GetComponent<Animation>().AddClip(QUALIDADE_MICRO_CILINDRO_2_POS_2, "QUALIDADE_MICRO_CILINDRO_2_POS_2");
			}
			QUALIDADE_ROBO_PARTE_11_3D.GetComponent<SCRIPT_QUALIDADE_ROBO_PARTE_11_3D>().GAMEOBJECT_FLASH = GAMEOBJECT_FLASH_1;
			if (MINIMIZAR_FUNCAO)
			{
				QUALIDADE_COLISAO_ESTEIRA_1.AddComponent<AudioSource>();
				QUALIDADE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().loop = true;
				QUALIDADE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().spatialBlend = 1f;
				QUALIDADE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().dopplerLevel = 0f;
				QUALIDADE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().minDistance = 10f;
				QUALIDADE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().clip = SOM_ESTEIRA_LIGADA;
				QUALIDADE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().playOnAwake = true;
				QUALIDADE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().Play();
				QUALIDADE_COLISAO_ESTEIRA_2.AddComponent<AudioSource>();
				QUALIDADE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().loop = true;
				QUALIDADE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().spatialBlend = 1f;
				QUALIDADE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().dopplerLevel = 0f;
				QUALIDADE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().minDistance = 10f;
				QUALIDADE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().clip = SOM_ESTEIRA_LIGADA;
				QUALIDADE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().playOnAwake = true;
				QUALIDADE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().Play();
				QUALIDADE_ALIMENTADOR_3D.AddComponent<AudioSource>();
				QUALIDADE_ALIMENTADOR_3D.GetComponent<AudioSource>().pitch = 0.5f;
				QUALIDADE_ALIMENTADOR_3D.GetComponent<AudioSource>().loop = true;
				QUALIDADE_ALIMENTADOR_3D.GetComponent<AudioSource>().spatialBlend = 1f;
				QUALIDADE_ALIMENTADOR_3D.GetComponent<AudioSource>().dopplerLevel = 0f;
				QUALIDADE_ALIMENTADOR_3D.GetComponent<AudioSource>().minDistance = 20f;
				QUALIDADE_ALIMENTADOR_3D.GetComponent<AudioSource>().clip = SOM_ALIMENTADOR_ANDANDO;
				QUALIDADE_ALIMENTADOR_3D.GetComponent<AudioSource>().playOnAwake = true;
				QUALIDADE_ALIMENTADOR_3D.GetComponent<AudioSource>().Play();
				QUALIDADE_HOLDER_VENTOSA.AddComponent<AudioSource>();
				QUALIDADE_HOLDER_VENTOSA.GetComponent<AudioSource>().loop = true;
				QUALIDADE_HOLDER_VENTOSA.GetComponent<AudioSource>().spatialBlend = 1f;
				QUALIDADE_HOLDER_VENTOSA.GetComponent<AudioSource>().dopplerLevel = 0f;
				QUALIDADE_HOLDER_VENTOSA.GetComponent<AudioSource>().minDistance = 20f;
				QUALIDADE_HOLDER_VENTOSA.GetComponent<AudioSource>().clip = SOM_VACUO_LIGADO;
				QUALIDADE_HOLDER_VENTOSA.GetComponent<AudioSource>().playOnAwake = true;
				QUALIDADE_HOLDER_VENTOSA.GetComponent<AudioSource>().Play();
			}
		}
		if (MINIMIZAR_FUNCAO)
		{
			EXPEDICAO_ESTEIRA_COLISOES = GameObject.Find("EXPEDICAO_ESTEIRA_COLISOES");
			EXPEDICAO_COLISAO_ESTEIRA_1 = GameObject.Find("EXPEDICAO_COLISAO_ESTEIRA_1");
			EXPEDICAO_COLISAO_ESTEIRA_2 = GameObject.Find("EXPEDICAO_COLISAO_ESTEIRA_2");
			EXPEDICAO_MICRO_CILINDRO_1_3D = GameObject.Find("EXPEDICAO_MICRO_CILINDRO_1_3D");
			EXPEDICAO_MICRO_CILINDRO_2_3D = GameObject.Find("EXPEDICAO_MICRO_CILINDRO_2_3D");
			EXPEDICAO_ALIMENTADOR_3D = GameObject.Find("EXPEDICAO_ALIMENTADOR_3D");
			EXPEDICAO_VENTOSA_3D = GameObject.Find("EXPEDICAO_VENTOSA_3D");
			EXPEDICAO_HOLDER_VENTOSA = GameObject.Find("EXPEDICAO_HOLDER_VENTOSA");
			EXPEDICAO_BRACO_ALTURA_3D = GameObject.Find("EXPEDICAO_BRACO_ALTURA_3D");
			EXPEDICAO_BRACO_HORIZONTAL_3D = GameObject.Find("EXPEDICAO_BRACO_HORIZONTAL_3D");
			EXPEDICAO_BRACO_ROTACAO_3D = GameObject.Find("EXPEDICAO_BRACO_ROTACAO_3D");
			EXPEDICAO_ESTEIRA_COLISOES.transform.parent = GameObject.Find("EXPEDICAO_3D").transform;
			EXPEDICAO_MICRO_CILINDRO_1_3D.AddComponent<BoxCollider>();
			EXPEDICAO_MICRO_CILINDRO_1_3D.GetComponent<BoxCollider>().size = new Vector3(94.34981f, 2f, 136.8396f);
			EXPEDICAO_MICRO_CILINDRO_2_3D.AddComponent<BoxCollider>();
			EXPEDICAO_MICRO_CILINDRO_2_3D.GetComponent<BoxCollider>().size = new Vector3(94.34981f, 2f, 136.8396f);
			EXPEDICAO_VENTOSA_3D.AddComponent<BoxCollider>();
			EXPEDICAO_VENTOSA_3D.AddComponent<Rigidbody>();
			EXPEDICAO_VENTOSA_3D.GetComponent<Rigidbody>().useGravity = false;
			EXPEDICAO_VENTOSA_3D.GetComponent<Rigidbody>().isKinematic = true;
			EXPEDICAO_VENTOSA_3D.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			EXPEDICAO_VENTOSA_3D.GetComponent<BoxCollider>().size = new Vector3(13.94f, 109.4857f, 108.6916f);
			EXPEDICAO_VENTOSA_3D.GetComponent<BoxCollider>().center = new Vector3(-27.5f, 0.03709413f, 0.5148337f);
			if (ADICIONAR_ANIMACOES_AUTOMATICAMENTE)
			{
				EXPEDICAO_MICRO_CILINDRO_1_3D.AddComponent<Animation>();
				EXPEDICAO_MICRO_CILINDRO_1_3D.GetComponent<Animation>().AddClip(EXPEDICAO_MICRO_CILINDRO_1_POS_1, "EXPEDICAO_MICRO_CILINDRO_1_POS_1");
				EXPEDICAO_MICRO_CILINDRO_1_3D.GetComponent<Animation>().AddClip(EXPEDICAO_MICRO_CILINDRO_1_POS_2, "EXPEDICAO_MICRO_CILINDRO_1_POS_2");
				EXPEDICAO_MICRO_CILINDRO_2_3D.AddComponent<Animation>();
				EXPEDICAO_MICRO_CILINDRO_2_3D.GetComponent<Animation>().AddClip(EXPEDICAO_MICRO_CILINDRO_2_POS_1, "EXPEDICAO_MICRO_CILINDRO_2_POS_1");
				EXPEDICAO_MICRO_CILINDRO_2_3D.GetComponent<Animation>().AddClip(EXPEDICAO_MICRO_CILINDRO_2_POS_2, "EXPEDICAO_MICRO_CILINDRO_2_POS_2");
				EXPEDICAO_BRACO_HORIZONTAL_3D.AddComponent<Animation>();
				EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_0, "EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_0");
				EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_1, "EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_1");
				EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_2, "EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_2");
				EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_3, "EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_3");
				EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_4, "EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_4");
				EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_5, "EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_5");
				EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_6, "EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_6");
				EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_7, "EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_7");
				EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_8, "EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_8");
				EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_9, "EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_9");
				EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_10, "EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_10");
				EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_11, "EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_11");
				EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_12, "EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_12");
				EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_13, "EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_13");
				EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_13_DO_14_PRO_13, "EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_13_DO_14_PRO_13");
				EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().AddClip(EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_14, "EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_14");
			}
			EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_0", VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_0_ANIMACAO);
			EXPEDICAO_ALIMENTADOR_3D.transform.localPosition = new Vector3(LIMITE_POSICAO_X_POSITIVO_EXPEDICAO_ALIMENTADOR_3D, LIMITE_POSICAO_Y_EXPEDICAO_ALIMENTADOR_3D, LIMITE_POSICAO_Z_EXPEDICAO_ALIMENTADOR_3D);
			if (MINIMIZAR_FUNCAO)
			{
				EXPEDICAO_COLISAO_ESTEIRA_1.AddComponent<AudioSource>();
				EXPEDICAO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().loop = true;
				EXPEDICAO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().spatialBlend = 1f;
				EXPEDICAO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().dopplerLevel = 0f;
				EXPEDICAO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().minDistance = 10f;
				EXPEDICAO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().clip = SOM_ESTEIRA_LIGADA;
				EXPEDICAO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().playOnAwake = true;
				EXPEDICAO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().Play();
				EXPEDICAO_COLISAO_ESTEIRA_2.AddComponent<AudioSource>();
				EXPEDICAO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().loop = true;
				EXPEDICAO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().spatialBlend = 1f;
				EXPEDICAO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().dopplerLevel = 0f;
				EXPEDICAO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().minDistance = 10f;
				EXPEDICAO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().clip = SOM_ESTEIRA_LIGADA;
				EXPEDICAO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().playOnAwake = true;
				EXPEDICAO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().Play();
				EXPEDICAO_ALIMENTADOR_3D.AddComponent<AudioSource>();
				EXPEDICAO_ALIMENTADOR_3D.GetComponent<AudioSource>().pitch = 0.5f;
				EXPEDICAO_ALIMENTADOR_3D.GetComponent<AudioSource>().loop = true;
				EXPEDICAO_ALIMENTADOR_3D.GetComponent<AudioSource>().spatialBlend = 1f;
				EXPEDICAO_ALIMENTADOR_3D.GetComponent<AudioSource>().dopplerLevel = 0f;
				EXPEDICAO_ALIMENTADOR_3D.GetComponent<AudioSource>().minDistance = 20f;
				EXPEDICAO_ALIMENTADOR_3D.GetComponent<AudioSource>().clip = SOM_ALIMENTADOR_ANDANDO;
				EXPEDICAO_ALIMENTADOR_3D.GetComponent<AudioSource>().playOnAwake = true;
				EXPEDICAO_ALIMENTADOR_3D.GetComponent<AudioSource>().Play();
				EXPEDICAO_HOLDER_VENTOSA.AddComponent<AudioSource>();
				EXPEDICAO_HOLDER_VENTOSA.GetComponent<AudioSource>().loop = true;
				EXPEDICAO_HOLDER_VENTOSA.GetComponent<AudioSource>().spatialBlend = 1f;
				EXPEDICAO_HOLDER_VENTOSA.GetComponent<AudioSource>().dopplerLevel = 0f;
				EXPEDICAO_HOLDER_VENTOSA.GetComponent<AudioSource>().minDistance = 20f;
				EXPEDICAO_HOLDER_VENTOSA.GetComponent<AudioSource>().clip = SOM_VACUO_LIGADO;
				EXPEDICAO_HOLDER_VENTOSA.GetComponent<AudioSource>().playOnAwake = true;
				EXPEDICAO_HOLDER_VENTOSA.GetComponent<AudioSource>().Play();
			}
		}
		if (MINIMIZAR_FUNCAO)
		{
			RETORNO_ESQUERDA_BASE_3D = GameObject.Find("RETORNO_ESQUERDA_BASE_3D");
			RETORNO_ESQUERDA_COLISOES = GameObject.Find("RETORNO_ESQUERDA_COLISOES");
			RETORNO_ESQUERDA_BASE_COLISAO = GameObject.Find("RETORNO_ESQUERDA_BASE_COLISAO");
			RETORNO_ESQUERDA_GIRO_COLISAO = GameObject.Find("RETORNO_ESQUERDA_GIRO_COLISAO");
			RETORNO_ESQUERA_COLISAO_ADICIONAL = GameObject.Find("RETORNO_ESQUERA_COLISAO_ADICIONAL");
			RETORNO_ESQUERDA_CIRCULO_3D = GameObject.Find("RETORNO_ESQUERDA_CIRCULO_3D");
			RETORNO_ESQUERDA_COLISOES.transform.parent = GameObject.Find("RETORNO_ESQUERDA_BASE_3D").transform;
			RETORNO_ESQUERDA_CIRCULO_3D.AddComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>();
			RETORNO_ESQUERDA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR_Z = VELOCIDADE_ROTACIONAR_RETORNOS;
		}
		if (MINIMIZAR_FUNCAO)
		{
			RETORNO_DIREITA_BASE_3D = GameObject.Find("RETORNO_DIREITA_BASE_3D");
			RETORNO_DIREITA_COLISOES = GameObject.Find("RETORNO_DIREITA_COLISOES");
			RETORNO_DIREITA_BASE_COLISAO = GameObject.Find("RETORNO_DIREITA_BASE_COLISAO");
			RETORNO_DIREITA_GIRO_COLISAO = GameObject.Find("RETORNO_DIREITA_GIRO_COLISAO");
			RETORNO_DIREITA_COLISAO_ADICIONAL = GameObject.Find("RETORNO_DIREITA_COLISAO_ADICIONAL");
			RETORNO_DIREITA_CIRCULO_3D = GameObject.Find("RETORNO_DIREITA_CIRCULO_3D");
			RETORNO_DIREITA_COLISOES.transform.parent = GameObject.Find("RETORNO_DIREITA_BASE_3D").transform;
			RETORNO_DIREITA_CIRCULO_3D.AddComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>();
			RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR_Z = VELOCIDADE_ROTACIONAR_RETORNOS;
		}
		if (MINIMIZAR_FUNCAO)
		{
			MOSTRAR_BOTAO_ERRO_DE_COMUNICACAO = false;
			reiniciar_servidor_agora = false;
			COMUNICACAO_ATUAL = "Manual";
			COMUNICACAO_MANUAL = true;
			COMUNICACAO_MODBUS_MASTER = false;
			COMUNICACAO_MODBUS_SLAVE = false;
			COMUNICACAO_SIEMENS = false;
			(CAMERA_VISUAL.GetComponent("BlurOptimized") as MonoBehaviour).enabled = true;
		}
	}

	private void Update()
	{
		if (MINIMIZAR_FUNCAO && LOGIN_NECESSARIO && Time.time > (float)tempo_de_verificacao + ultimo_tempo_de_verificacao)
		{
			VOID_VERIFICAR_LOGIN();
			ultimo_tempo_de_verificacao = Time.time;
		}
		TEXTO_COMUNICAO_ATUAL.GetComponent<Text>().text = COMUNICACAO_ATUAL;
		if (reiniciar_servidor_agora)
		{
			StartCoroutine(AGUARDAR_E_INSTANCIAR_OBJETO());
		}
		if (!MENU_QUIT_GAME)
		{
			menu_options = TOGGLE_MODBUS.GetComponent<Toggle>().isOn;
		}
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			MENU_QUIT_GAME = !MENU_QUIT_GAME;
			TOGGLE_MODBUS.GetComponent<Toggle>().isOn = false;
			menu_options = false;
			VAR_INSTANCIAR_SMARTBLOCK_TOGGLE = false;
		}
		if (mostrar_menu)
		{
			if (!menu_options)
			{
				modbus_menu.SetActive(value: false);
				(CAMERA_VISUAL.GetComponent("BlurOptimized") as MonoBehaviour).enabled = false;
			}
			if (menu_options)
			{
				modbus_menu.SetActive(value: true);
				(CAMERA_VISUAL.GetComponent("BlurOptimized") as MonoBehaviour).enabled = true;
			}
		}
		if (menu_options)
		{
			VAR_INSTANCIAR_SMARTBLOCK_TOGGLE = false;
		}
		if (VAR_INSTANCIAR_SMARTBLOCK_TOGGLE)
		{
			SMARTBLOCK_OPCOES.SetActive(value: true);
		}
		else
		{
			SMARTBLOCK_OPCOES.SetActive(value: false);
		}
		if (MINIMIZAR_FUNCAO)
		{
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueEsteira1)
				{
					ESTOQUE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().volume = 0.2f;
					ESTOQUE_COLISAO_ESTEIRA_1.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
					if (BANCADA_1 == "ESTOQUE (UI INPUTS HOLDER)")
					{
						MonoBehaviour.print("[ESTOQUE] ESTÁ NA ESQUERDA DA FILA E ESTÁ LIGADA COM RETORNO DA ESQUERDA");
						RETORNO_ESQUERA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
						RETORNO_ESQUERDA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
						RETORNO_ESQUERDA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					}
					if (BANCADA_1 == "ESTOQUE (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 1)
					{
						MonoBehaviour.print("[ESTOQUE] ESTÁ NA DIREITA DA FILA DE 1 BANCADA E ESTÁ LIGADA COM RETORNO DA DIREITA");
						RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
						RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
						RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					}
					if (BANCADA_2 == "ESTOQUE (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 2)
					{
						MonoBehaviour.print("[ESTOQUE] ESTÁ NA DIREITA DA FILA DE 2 BANCADAS E ESTÁ LIGADA COM RETORNO DA DIREITA");
						RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
						RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
						RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					}
					if (BANCADA_3 == "ESTOQUE (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 3)
					{
						MonoBehaviour.print("[ESTOQUE] ESTÁ NA DIREITA DA FILA DE 3 BANCADAS ESTÁ LIGADA COM RETORNO DA DIREITA");
						RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
						RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
						RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					}
					if (BANCADA_4 == "ESTOQUE (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 4)
					{
						MonoBehaviour.print("[ESTOQUE] ESTÁ NA DIREITA DA FILA DE 4 BANCADAS E ESTÁ LIGADA COM RETORNO DA DIREITA");
						RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
						RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
						RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					}
				}
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueEsteira1)
				{
					ESTOQUE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().volume = 0f;
					ESTOQUE_COLISAO_ESTEIRA_1.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
					if (BANCADA_1 == "ESTOQUE (UI INPUTS HOLDER)")
					{
						RETORNO_ESQUERA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
						RETORNO_ESQUERDA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
						RETORNO_ESQUERDA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					}
					if (BANCADA_1 == "ESTOQUE (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 1)
					{
						RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
						RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
						RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					}
					if (BANCADA_2 == "ESTOQUE (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 2)
					{
						RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
						RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
						RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					}
					if (BANCADA_3 == "ESTOQUE (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 3)
					{
						RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
						RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
						RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					}
					if (BANCADA_4 == "ESTOQUE (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 4)
					{
						RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
						RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
						RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					}
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueEsteira2)
				{
					ESTOQUE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().volume = 0.2f;
					ESTOQUE_COLISAO_ESTEIRA_2.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
				}
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueEsteira2)
				{
					ESTOQUE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().volume = 0f;
					ESTOQUE_COLISAO_ESTEIRA_2.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueStopper1)
				{
					ESTOQUE_MICRO_CILINDRO_1_3D.GetComponent<Animation>().CrossFade("ESTOQUE_MICRO_CILINDRO_1_POS_1", VELOCIDADE_MICRO_CILINDRO);
					if (ESTOQUE_PODE_TOCAR_SOM_MICRO_CILINDRO_1_RECUAR)
					{
						UnityEngine.Object.Instantiate(SOM_MICRO_CILINDRO_RECUAR, base.transform.position, base.transform.rotation);
						ESTOQUE_PODE_TOCAR_SOM_MICRO_CILINDRO_1_RECUAR = false;
						ESTOQUE_PODE_TOCAR_SOM_MICRO_CILINDRO_1_AVANCAR = true;
					}
				}
				if (SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueStopper1)
				{
					ESTOQUE_MICRO_CILINDRO_1_3D.GetComponent<Animation>().CrossFade("ESTOQUE_MICRO_CILINDRO_1_POS_2", VELOCIDADE_MICRO_CILINDRO);
					if (ESTOQUE_PODE_TOCAR_SOM_MICRO_CILINDRO_1_AVANCAR)
					{
						UnityEngine.Object.Instantiate(SOM_MICRO_CILINDRO_AVANCAR, base.transform.position, base.transform.rotation);
						ESTOQUE_PODE_TOCAR_SOM_MICRO_CILINDRO_1_AVANCAR = false;
						ESTOQUE_PODE_TOCAR_SOM_MICRO_CILINDRO_1_RECUAR = true;
					}
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueStopper2)
				{
					ESTOQUE_MICRO_CILINDRO_2_3D.GetComponent<Animation>().CrossFade("ESTOQUE_MICRO_CILINDRO_2_POS_1", VELOCIDADE_MICRO_CILINDRO);
					if (ESTOQUE_PODE_TOCAR_SOM_MICRO_CILINDRO_2_RECUAR)
					{
						UnityEngine.Object.Instantiate(SOM_MICRO_CILINDRO_RECUAR, base.transform.position, base.transform.rotation);
						ESTOQUE_PODE_TOCAR_SOM_MICRO_CILINDRO_2_RECUAR = false;
						ESTOQUE_PODE_TOCAR_SOM_MICRO_CILINDRO_2_AVANCAR = true;
					}
				}
				if (SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueStopper2)
				{
					ESTOQUE_MICRO_CILINDRO_2_3D.GetComponent<Animation>().CrossFade("ESTOQUE_MICRO_CILINDRO_2_POS_2", VELOCIDADE_MICRO_CILINDRO);
					if (ESTOQUE_PODE_TOCAR_SOM_MICRO_CILINDRO_2_AVANCAR)
					{
						UnityEngine.Object.Instantiate(SOM_MICRO_CILINDRO_AVANCAR, base.transform.position, base.transform.rotation);
						ESTOQUE_PODE_TOCAR_SOM_MICRO_CILINDRO_2_AVANCAR = false;
						ESTOQUE_PODE_TOCAR_SOM_MICRO_CILINDRO_2_RECUAR = true;
					}
				}
			}
			if (SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueDriveAtuador)
			{
				EXECUTANDO_ANIMACAO = true;
			}
			if (EXECUTANDO_ANIMACAO)
			{
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "0")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_0", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "1")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_1", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "2")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_2", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "3")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_3", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "4")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_4", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "5")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_5", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "6")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_6", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "7")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_7", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "8")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_8", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "9")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_9", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "10")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_10", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "11")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_11", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "12")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_12", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "13")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_13", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "14")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_14", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "15")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_15", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "16")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_16", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "17")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_17", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "18")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_18", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "19")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_19", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "20")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_20", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "21")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_21", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "22")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_22", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "23")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_23", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "24")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_24", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "25")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_25", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "26")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_26", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "27")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_27", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "28")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_28", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_EstoquePosicaoAtuador == "29")
				{
					ESTOQUE_BRACO_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_3D_POSICAO_29", VELOCIDADE_ESTOQUE_BRACO_3D);
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (Time.time > FREQUENCIA_ESTOQUE_BRACO_3D_ROTACAO + ULTIMA_FREQUENCIA_ESTOQUE_BRACO_3D_ROTACAO)
				{
					ESTOQUE_BRACO_3D_ULTIMA_ROTACAO = ESTOQUE_BRACO_3D.transform.localEulerAngles;
					ESTOQUE_ELEVADOR_3D_ULTIMA_POSICAO = ESTOQUE_ELEVADOR_3D.transform.localPosition;
					ULTIMA_FREQUENCIA_ESTOQUE_BRACO_3D_ROTACAO = Time.time;
				}
				if (ESTOQUE_BRACO_3D.transform.localEulerAngles == ESTOQUE_BRACO_3D_ULTIMA_ROTACAO && ESTOQUE_ELEVADOR_3D.transform.localPosition == ESTOQUE_ELEVADOR_3D_ULTIMA_POSICAO && Time.time > FREQUENCIA_ESTOQUE_BRACO_3D_ROTACAO + ULTIMA_FREQUENCIA_ESTOQUE_BRACO_3D_ROTACAO - 0.2f)
				{
					EXECUTANDO_ANIMACAO = false;
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAtuadorEmPosicao = true;
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAtuadorOcupado = EXECUTANDO_ANIMACAO;
				}
				if (ESTOQUE_BRACO_3D.transform.localEulerAngles != ESTOQUE_BRACO_3D_ULTIMA_ROTACAO && ESTOQUE_ELEVADOR_3D.transform.localPosition != ESTOQUE_ELEVADOR_3D_ULTIMA_POSICAO && Time.time > FREQUENCIA_ESTOQUE_BRACO_3D_ROTACAO + ULTIMA_FREQUENCIA_ESTOQUE_BRACO_3D_ROTACAO - 0.2f)
				{
					MonoBehaviour.print("[ESTOQUE] ATUADOR ELÉTRICO FORA DE POSIÇÃO");
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAtuadorEmPosicao = false;
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAtuadorOcupado = EXECUTANDO_ANIMACAO;
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueFechaGarra)
				{
					ESTOQUE_GARRA_ESQUERDA_3D.GetComponent<Animation>().CrossFade("ESTOQUE_GARRA_ESQUERDA_3D_ABRIR", VELOCIDADE_ESTOQUE_GARRA_3D);
					ESTOQUE_GARRA_DIREITA_3D.GetComponent<Animation>().CrossFade("ESTOQUE_GARRA_DIREITA_3D_ABRIR", VELOCIDADE_ESTOQUE_GARRA_3D);
					ESTOQUE_HOLDER_GARRA.transform.DetachChildren();
					ESTOQUE_HOLDER_GARRA.SetActive(value: false);
				}
				if (SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueFechaGarra)
				{
					ESTOQUE_GARRA_ESQUERDA_3D.GetComponent<Animation>().CrossFade("ESTOQUE_GARRA_ESQUERDA_3D_FECHAR", VELOCIDADE_ESTOQUE_GARRA_3D);
					ESTOQUE_GARRA_DIREITA_3D.GetComponent<Animation>().CrossFade("ESTOQUE_GARRA_DIREITA_3D_FECHAR", VELOCIDADE_ESTOQUE_GARRA_3D);
					ESTOQUE_HOLDER_GARRA.SetActive(value: true);
				}
				if (ESTOQUE_GARRA_ESQUERDA_3D.transform.localPosition.x == 219.7f)
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueGarraAberta = true;
				}
				if (ESTOQUE_GARRA_ESQUERDA_3D.transform.localPosition.x != 219.7f)
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueGarraAberta = false;
				}
				if (ESTOQUE_GARRA_ESQUERDA_3D.transform.localPosition.x == 190.1601f && ESTOQUE_HOLDER_GARRA.transform.childCount > 0)
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueGarraFechada = true;
				}
				if (ESTOQUE_GARRA_ESQUERDA_3D.transform.localPosition.x != 190.1601f)
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueGarraFechada = false;
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAvancaGarra)
				{
					ESTOQUE_BRACO_2_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_2_3D_AVANCAR", VELOCIDADE_ESTOQUE_BRACO_2_3D);
				}
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAvancaGarra)
				{
					ESTOQUE_BRACO_2_3D.GetComponent<Animation>().CrossFade("ESTOQUE_BRACO_2_3D_RECUAR", VELOCIDADE_ESTOQUE_BRACO_2_3D);
				}
				if (ESTOQUE_BRACO_2_3D.transform.localPosition.y == 767.7272f)
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueGarraAvancada = true;
				}
				if (ESTOQUE_BRACO_2_3D.transform.localPosition.y != 767.7272f)
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueGarraAvancada = false;
				}
				if (ESTOQUE_BRACO_2_3D.transform.localPosition.y == 374.5f)
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueGarraRecuada = true;
				}
				if (ESTOQUE_BRACO_2_3D.transform.localPosition.y != 374.5f)
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueGarraRecuada = false;
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAvancaAlimentador && !SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueRecuaAlimentador)
				{
					ESTOQUE_ALIMENTADOR_3D.transform.Translate(-Vector3.right * Time.deltaTime * VELOCIDADE_ESTOQUE_ALIMENTADOR_3D);
					ESTOQUE_ALIMENTADOR_3D.GetComponent<AudioSource>().volume = 0.5f;
				}
				if (SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueRecuaAlimentador && !SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAvancaAlimentador)
				{
					ESTOQUE_ALIMENTADOR_3D.transform.Translate(Vector3.right * Time.deltaTime * VELOCIDADE_ESTOQUE_ALIMENTADOR_3D);
					ESTOQUE_ALIMENTADOR_3D.GetComponent<AudioSource>().volume = 0.5f;
				}
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAvancaAlimentador && !SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueRecuaAlimentador)
				{
					ESTOQUE_ALIMENTADOR_3D.GetComponent<AudioSource>().volume = 0f;
				}
				if (ESTOQUE_ALIMENTADOR_3D.transform.localPosition.x <= LIMITE_POSICAO_X_NEGATIVO_ESTOQUE_ALIMENTADOR_3D)
				{
					ESTOQUE_ALIMENTADOR_3D.transform.localPosition = new Vector3(LIMITE_POSICAO_X_NEGATIVO_ESTOQUE_ALIMENTADOR_3D, LIMITE_POSICAO_Y_ESTOQUE_ALIMENTADOR_3D, LIMITE_POSICAO_Z_ESTOQUE_ALIMENTADOR_3D);
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAlimentadorAvancado = true;
				}
				else
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAlimentadorAvancado = false;
				}
				if (ESTOQUE_ALIMENTADOR_3D.transform.localPosition.x >= LIMITE_POSICAO_X_POSITIVO_ESTOQUE_ALIMENTADOR_3D)
				{
					ESTOQUE_ALIMENTADOR_3D.transform.localPosition = new Vector3(LIMITE_POSICAO_X_POSITIVO_ESTOQUE_ALIMENTADOR_3D, LIMITE_POSICAO_Y_ESTOQUE_ALIMENTADOR_3D, LIMITE_POSICAO_Z_ESTOQUE_ALIMENTADOR_3D);
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAlimentadorRecuado = true;
				}
				else
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAlimentadorRecuado = false;
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAcionaVacuo)
				{
					ESTOQUE_HOLDER_VENTOSA.SetActive(value: true);
					ESTOQUE_HOLDER_VENTOSA.GetComponent<AudioSource>().volume = 0.5f;
				}
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueAcionaVacuo)
				{
					ESTOQUE_HOLDER_VENTOSA.transform.DetachChildren();
					ESTOQUE_HOLDER_VENTOSA.SetActive(value: false);
					ESTOQUE_HOLDER_VENTOSA.GetComponent<AudioSource>().volume = 0f;
				}
				if (ESTOQUE_HOLDER_VENTOSA.transform.childCount < 1)
				{
					ESTOQUE_HOLDER_VENTOSA.transform.localPosition = POSICAO_ESTOQUE_HOLDER_VENTOSA_1;
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueSensorVacuo = false;
				}
				if (ESTOQUE_HOLDER_VENTOSA.transform.childCount >= 1)
				{
					ESTOQUE_HOLDER_VENTOSA.transform.localPosition = POSICAO_ESTOQUE_HOLDER_VENTOSA_2;
					SCRIPT_TOGGLES.ATIVAR_BIT_EstoqueSensorVacuo = true;
				}
			}
		}
		if (MINIMIZAR_FUNCAO)
		{
			PROCESSO_LAMINA_LOCAL_1_OBJETO_ATUAL_DEBUG = PROCESSO_LAMINA_LOCAL_1_OBJETO_ATUAL;
			PROCESSO_LAMINA_LOCAL_2_OBJETO_ATUAL_DEBUG = PROCESSO_LAMINA_LOCAL_2_OBJETO_ATUAL;
			PROCESSO_LAMINA_LOCAL_3_OBJETO_ATUAL_DEBUG = PROCESSO_LAMINA_LOCAL_3_OBJETO_ATUAL;
			PROCESSO_LAMINA_LOCAL_4_OBJETO_ATUAL_DEBUG = PROCESSO_LAMINA_LOCAL_4_OBJETO_ATUAL;
			PROCESSO_LAMINA_LOCAL_5_OBJETO_ATUAL_DEBUG = PROCESSO_LAMINA_LOCAL_5_OBJETO_ATUAL;
			PROCESSO_LAMINA_LOCAL_6_OBJETO_ATUAL_DEBUG = PROCESSO_LAMINA_LOCAL_6_OBJETO_ATUAL;
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoEsteira1)
				{
					PROCESSO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().volume = 0.2f;
					PROCESSO_COLISAO_ESTEIRA_1.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
					if (BANCADA_1 == "PROCESSO (UI INPUTS HOLDER)")
					{
						MonoBehaviour.print("[PROCESSO] ESTÁ NA ESQUERDA DA FILA E ESTÁ LIGADA COM RETORNO DA ESQUERDA");
						RETORNO_ESQUERA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
						RETORNO_ESQUERDA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
						RETORNO_ESQUERDA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					}
					if (BANCADA_1 == "PROCESSO (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 1)
					{
						MonoBehaviour.print("[PROCESSO] ESTÁ NA DIREITA DA FILA DE 1 BANCADA E ESTÁ LIGADA COM RETORNO DA DIREITA");
						RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
						RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
						RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					}
					if (BANCADA_2 == "PROCESSO (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 2)
					{
						MonoBehaviour.print("[PROCESSO] ESTÁ NA DIREITA DA FILA DE 2 BANCADAS E ESTÁ LIGADA COM RETORNO DA DIREITA");
						RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
						RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
						RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					}
					if (BANCADA_3 == "PROCESSO (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 3)
					{
						MonoBehaviour.print("[PROCESSO] ESTÁ NA DIREITA DA FILA DE 3 BANCADAS ESTÁ LIGADA COM RETORNO DA DIREITA");
						RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
						RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
						RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					}
					if (BANCADA_4 == "PROCESSO (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 4)
					{
						MonoBehaviour.print("[PROCESSO] ESTÁ NA DIREITA DA FILA DE 4 BANCADAS E ESTÁ LIGADA COM RETORNO DA DIREITA");
						RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
						RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
						RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					}
				}
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoEsteira1)
				{
					PROCESSO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().volume = 0f;
					PROCESSO_COLISAO_ESTEIRA_1.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
					if (BANCADA_1 == "PROCESSO (UI INPUTS HOLDER)")
					{
						RETORNO_ESQUERA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
						RETORNO_ESQUERDA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
						RETORNO_ESQUERDA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					}
					if (BANCADA_1 == "PROCESSO (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 1)
					{
						RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
						RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
						RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					}
					if (BANCADA_2 == "PROCESSO (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 2)
					{
						RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
						RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
						RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					}
					if (BANCADA_3 == "PROCESSO (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 3)
					{
						RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
						RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
						RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					}
					if (BANCADA_4 == "PROCESSO (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 4)
					{
						RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
						RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
						RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					}
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoEsteira2)
				{
					PROCESSO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().volume = 0.2f;
					PROCESSO_COLISAO_ESTEIRA_2.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
				}
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoEsteira2)
				{
					PROCESSO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().volume = 0f;
					PROCESSO_COLISAO_ESTEIRA_2.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
				}
			}
			if (SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoDriveAtuador)
			{
				EXECUTANDO_ANIMACAO_PROCESSO = true;
			}
			if (EXECUTANDO_ANIMACAO_PROCESSO)
			{
				if (SCRIPT_TOGGLES.VALOR_BIT_ProcessoPosicaoAtuador == "0")
				{
					PROCESSO_ATUADOR_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO_INICIAL", VELOCIDADE_PROCESSO_ATUADOR_HORIZONTAL_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ProcessoPosicaoAtuador == "1")
				{
					PROCESSO_ATUADOR_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("PROCESSO_ATUADOR_HORIZONTAL_3D_BUSCAR_LAMINA_1", VELOCIDADE_PROCESSO_ATUADOR_HORIZONTAL_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ProcessoPosicaoAtuador == "2")
				{
					PROCESSO_ATUADOR_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("PROCESSO_ATUADOR_HORIZONTAL_3D_BUSCAR_LAMINA_2", VELOCIDADE_PROCESSO_ATUADOR_HORIZONTAL_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ProcessoPosicaoAtuador == "3")
				{
					PROCESSO_ATUADOR_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("PROCESSO_ATUADOR_HORIZONTAL_3D_BUSCAR_LAMINA_3", VELOCIDADE_PROCESSO_ATUADOR_HORIZONTAL_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ProcessoPosicaoAtuador == "4")
				{
					PROCESSO_ATUADOR_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("PROCESSO_ATUADOR_HORIZONTAL_3D_BUSCAR_LAMINA_4", VELOCIDADE_PROCESSO_ATUADOR_HORIZONTAL_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ProcessoPosicaoAtuador == "5")
				{
					PROCESSO_ATUADOR_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("PROCESSO_ATUADOR_HORIZONTAL_3D_BUSCAR_LAMINA_5", VELOCIDADE_PROCESSO_ATUADOR_HORIZONTAL_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ProcessoPosicaoAtuador == "6")
				{
					PROCESSO_ATUADOR_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("PROCESSO_ATUADOR_HORIZONTAL_3D_BUSCAR_LAMINA_6", VELOCIDADE_PROCESSO_ATUADOR_HORIZONTAL_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ProcessoPosicaoAtuador == "7")
				{
					PROCESSO_ATUADOR_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO_LARGAR_LAMINA_MEIO", VELOCIDADE_PROCESSO_ATUADOR_HORIZONTAL_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ProcessoPosicaoAtuador == "8")
				{
					PROCESSO_ATUADOR_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO_CANETA", VELOCIDADE_PROCESSO_ATUADOR_HORIZONTAL_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ProcessoPosicaoAtuador == "9")
				{
					PROCESSO_ATUADOR_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO_LARGAR_LAMINA_1", VELOCIDADE_PROCESSO_ATUADOR_HORIZONTAL_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ProcessoPosicaoAtuador == "10")
				{
					PROCESSO_ATUADOR_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO_LARGAR_LAMINA_2", VELOCIDADE_PROCESSO_ATUADOR_HORIZONTAL_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ProcessoPosicaoAtuador == "11")
				{
					PROCESSO_ATUADOR_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO_LARGAR_LAMINA_3", VELOCIDADE_PROCESSO_ATUADOR_HORIZONTAL_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ProcessoPosicaoAtuador == "12")
				{
					PROCESSO_ATUADOR_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO_LARGAR_LAMINA_4", VELOCIDADE_PROCESSO_ATUADOR_HORIZONTAL_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ProcessoPosicaoAtuador == "13")
				{
					PROCESSO_ATUADOR_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO_LARGAR_LAMINA_5", VELOCIDADE_PROCESSO_ATUADOR_HORIZONTAL_3D);
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ProcessoPosicaoAtuador == "14")
				{
					PROCESSO_ATUADOR_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO_LARGAR_LAMINA_6", VELOCIDADE_PROCESSO_ATUADOR_HORIZONTAL_3D);
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (Time.time > FREQUENCIA_PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO + ULTIMA_FREQUENCIA_PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO)
				{
					PROCESSO_ATUADOR_HORIZONTAL_3D_ULTIMA_POSICAO = PROCESSO_ATUADOR_HORIZONTAL_3D.transform.localPosition;
					PROCESSO_ATUADOR_FRONTAL_3D_ULTIMA_POSICAO = PROCESSO_ATUADOR_FRONTAL_3D.transform.localPosition;
					ULTIMA_FREQUENCIA_PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO = Time.time;
				}
				if (PROCESSO_ATUADOR_HORIZONTAL_3D.transform.localPosition == PROCESSO_ATUADOR_HORIZONTAL_3D_ULTIMA_POSICAO && PROCESSO_ATUADOR_FRONTAL_3D.transform.localPosition == PROCESSO_ATUADOR_FRONTAL_3D_ULTIMA_POSICAO && Time.time > FREQUENCIA_PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO + ULTIMA_FREQUENCIA_PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO - 0.2f)
				{
					EXECUTANDO_ANIMACAO_PROCESSO = false;
					SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAtuadorEmPosicao = true;
					SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAtuadorOcupado = EXECUTANDO_ANIMACAO_PROCESSO;
				}
				if (PROCESSO_ATUADOR_HORIZONTAL_3D.transform.localPosition != PROCESSO_ATUADOR_HORIZONTAL_3D_ULTIMA_POSICAO && PROCESSO_ATUADOR_FRONTAL_3D.transform.localPosition != PROCESSO_ATUADOR_FRONTAL_3D_ULTIMA_POSICAO && Time.time > FREQUENCIA_PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO + ULTIMA_FREQUENCIA_PROCESSO_ATUADOR_HORIZONTAL_3D_POSICAO - 0.2f)
				{
					MonoBehaviour.print("[PROCESSO] ATUADOR ELÉTRICO FORA DE POSIÇÃO");
					SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAtuadorEmPosicao = false;
					SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAtuadorOcupado = EXECUTANDO_ANIMACAO_PROCESSO;
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAvancaAlimentador)
				{
					PROCESSO_ALIMENTADOR_3D.transform.Translate(-Vector3.forward * Time.deltaTime * VELOCIDADE_PROCESSO_ALIMENTADOR_3D);
				}
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAvancaAlimentador)
				{
					PROCESSO_ALIMENTADOR_3D.transform.Translate(Vector3.forward * Time.deltaTime * VELOCIDADE_PROCESSO_ALIMENTADOR_3D);
				}
				if (PROCESSO_ALIMENTADOR_3D.transform.localPosition.z <= LIMITE_POSICAO_Z_NEGATIVO_PROCESSO_ALIMENTADOR_3D)
				{
					PROCESSO_ALIMENTADOR_3D.transform.localPosition = new Vector3(LIMITE_POSICAO_X_PROCESSO_ALIMENTADOR_3D, LIMITE_POSICAO_Y_PROCESSO_ALIMENTADOR_3D, LIMITE_POSICAO_Z_NEGATIVO_PROCESSO_ALIMENTADOR_3D);
					SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAlimentadorAvancado = true;
				}
				else
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAlimentadorAvancado = false;
				}
				if (PROCESSO_ALIMENTADOR_3D.transform.localPosition.z >= LIMITE_POSICAO_Z_POSITIVO_PROCESSO_ALIMENTADOR_3D)
				{
					PROCESSO_ALIMENTADOR_3D.transform.localPosition = new Vector3(LIMITE_POSICAO_X_PROCESSO_ALIMENTADOR_3D, LIMITE_POSICAO_Y_PROCESSO_ALIMENTADOR_3D, LIMITE_POSICAO_Z_POSITIVO_PROCESSO_ALIMENTADOR_3D);
					SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAlimentadorRecuado = true;
				}
				else
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAlimentadorRecuado = false;
				}
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAlimentadorAvancado && !SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAlimentadorRecuado)
				{
					PROCESSO_ALIMENTADOR_3D.GetComponent<AudioSource>().volume = 0.5f;
				}
				if (SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAlimentadorAvancado || SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAlimentadorRecuado)
				{
					PROCESSO_ALIMENTADOR_3D.GetComponent<AudioSource>().volume = 0f;
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAcionaVacuoManipulador)
				{
					PROCESSO_HOLDER_VENTOSA.SetActive(value: true);
					PROCESSO_HOLDER_VENTOSA.GetComponent<AudioSource>().volume = 0.5f;
				}
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAcionaVacuoManipulador)
				{
					PROCESSO_HOLDER_VENTOSA.transform.DetachChildren();
					PROCESSO_HOLDER_VENTOSA.SetActive(value: false);
					PROCESSO_HOLDER_VENTOSA.GetComponent<AudioSource>().volume = 0f;
					if ((bool)PROCESSO_LAMINA_SENDO_SEGURADA_MANIPULADOR)
					{
						PROCESSO_LAMINA_SENDO_SEGURADA_MANIPULADOR.GetComponent<Rigidbody>().useGravity = true;
						PROCESSO_LAMINA_SENDO_SEGURADA_MANIPULADOR.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
						PROCESSO_LAMINA_SENDO_SEGURADA_MANIPULADOR = null;
					}
				}
				if (PROCESSO_HOLDER_VENTOSA.transform.childCount < 1)
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoSensorVacuo = false;
				}
				if (PROCESSO_HOLDER_VENTOSA.transform.childCount >= 1)
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoSensorVacuo = true;
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAcionaVacuoMesa)
				{
					PROCESSO_HOLDER_VENTOSA_MESA.SetActive(value: true);
					PROCESSO_HOLDER_VENTOSA_MESA.GetComponent<AudioSource>().volume = 0.5f;
				}
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_ProcessoAcionaVacuoMesa)
				{
					PROCESSO_HOLDER_VENTOSA_MESA.transform.DetachChildren();
					PROCESSO_HOLDER_VENTOSA_MESA.SetActive(value: false);
					PROCESSO_HOLDER_VENTOSA_MESA.GetComponent<AudioSource>().volume = 0f;
					if ((bool)PROCESSO_LAMINA_SENDO_SEGURADA_MESA)
					{
						PROCESSO_LAMINA_SENDO_SEGURADA_MESA.GetComponent<Rigidbody>().useGravity = true;
						PROCESSO_LAMINA_SENDO_SEGURADA_MESA.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
						PROCESSO_LAMINA_SENDO_SEGURADA_MESA = null;
					}
				}
			}
		}
		if (MINIMIZAR_FUNCAO)
		{
			if (SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeEsteira1)
			{
				MonoBehaviour.print("ESTEIRA QUALIDADE 1 ATIVA");
				QUALIDADE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().volume = 0.2f;
				QUALIDADE_COLISAO_ESTEIRA_1.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
				if (BANCADA_1 == "QUALIDADE (UI INPUTS HOLDER)")
				{
					RETORNO_ESQUERA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
					RETORNO_ESQUERDA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					RETORNO_ESQUERDA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
				}
				if (BANCADA_1 == "QUALIDADE (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 1)
				{
					RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
					RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
				}
				if (BANCADA_2 == "QUALIDADE (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 2)
				{
					RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
					RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
				}
				if (BANCADA_3 == "QUALIDADE (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 3)
				{
					RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
					RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
				}
				if (BANCADA_4 == "QUALIDADE (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 4)
				{
					RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
					RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
				}
			}
			if (!SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeEsteira1)
			{
				QUALIDADE_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().volume = 0f;
				QUALIDADE_COLISAO_ESTEIRA_1.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
				if (BANCADA_1 == "QUALIDADE (UI INPUTS HOLDER)")
				{
					RETORNO_ESQUERA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
					RETORNO_ESQUERDA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					RETORNO_ESQUERDA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
				}
				if (BANCADA_1 == "QUALIDADE (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 1)
				{
					RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
					RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
				}
				if (BANCADA_2 == "QUALIDADE (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 2)
				{
					RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
					RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
				}
				if (BANCADA_3 == "QUALIDADE (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 3)
				{
					RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
					RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
				}
				if (BANCADA_4 == "QUALIDADE (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 4)
				{
					RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
					RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
				}
			}
			if (SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeEsteira2)
			{
				MonoBehaviour.print("ESTEIRA QUALIDADE 2 ATIVA");
				QUALIDADE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().volume = 0.2f;
				QUALIDADE_COLISAO_ESTEIRA_2.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
			}
			if (!SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeEsteira2)
			{
				QUALIDADE_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().volume = 0f;
				QUALIDADE_COLISAO_ESTEIRA_2.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeStopper1)
				{
					QUALIDADE_MICRO_CILINDRO_1_3D.GetComponent<Animation>().CrossFade("QUALIDADE_MICRO_CILINDRO_1_POS_1", VELOCIDADE_MICRO_CILINDRO);
					if (QUALIDADE_PODE_TOCAR_SOM_MICRO_CILINDRO_1_RECUAR)
					{
						UnityEngine.Object.Instantiate(SOM_MICRO_CILINDRO_RECUAR, base.transform.position, base.transform.rotation);
						QUALIDADE_PODE_TOCAR_SOM_MICRO_CILINDRO_1_RECUAR = false;
						QUALIDADE_PODE_TOCAR_SOM_MICRO_CILINDRO_1_AVANCAR = true;
					}
				}
				if (SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeStopper1)
				{
					QUALIDADE_MICRO_CILINDRO_1_3D.GetComponent<Animation>().CrossFade("QUALIDADE_MICRO_CILINDRO_1_POS_2", VELOCIDADE_MICRO_CILINDRO);
					if (QUALIDADE_PODE_TOCAR_SOM_MICRO_CILINDRO_1_AVANCAR)
					{
						UnityEngine.Object.Instantiate(SOM_MICRO_CILINDRO_AVANCAR, base.transform.position, base.transform.rotation);
						QUALIDADE_PODE_TOCAR_SOM_MICRO_CILINDRO_1_AVANCAR = false;
						QUALIDADE_PODE_TOCAR_SOM_MICRO_CILINDRO_1_RECUAR = true;
					}
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeStopper2)
				{
					QUALIDADE_MICRO_CILINDRO_2_3D.GetComponent<Animation>().CrossFade("QUALIDADE_MICRO_CILINDRO_2_POS_1", VELOCIDADE_MICRO_CILINDRO);
					if (QUALIDADE_PODE_TOCAR_SOM_MICRO_CILINDRO_2_RECUAR)
					{
						UnityEngine.Object.Instantiate(SOM_MICRO_CILINDRO_RECUAR, base.transform.position, base.transform.rotation);
						QUALIDADE_PODE_TOCAR_SOM_MICRO_CILINDRO_2_RECUAR = false;
						QUALIDADE_PODE_TOCAR_SOM_MICRO_CILINDRO_2_AVANCAR = true;
					}
				}
				if (SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeStopper2)
				{
					QUALIDADE_MICRO_CILINDRO_2_3D.GetComponent<Animation>().CrossFade("QUALIDADE_MICRO_CILINDRO_2_POS_2", VELOCIDADE_MICRO_CILINDRO);
					if (QUALIDADE_PODE_TOCAR_SOM_MICRO_CILINDRO_2_AVANCAR)
					{
						UnityEngine.Object.Instantiate(SOM_MICRO_CILINDRO_AVANCAR, base.transform.position, base.transform.rotation);
						QUALIDADE_PODE_TOCAR_SOM_MICRO_CILINDRO_2_AVANCAR = false;
						QUALIDADE_PODE_TOCAR_SOM_MICRO_CILINDRO_2_RECUAR = true;
					}
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeAvancaAlimentador && !SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeRecuaAlimentador)
				{
					QUALIDADE_ALIMENTADOR_3D.transform.Translate(-Vector3.right * Time.deltaTime * VELOCIDADE_QUALIDADE_ALIMENTADOR_3D);
					QUALIDADE_ALIMENTADOR_3D.GetComponent<AudioSource>().volume = 0.5f;
				}
				if (SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeRecuaAlimentador && !SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeAvancaAlimentador)
				{
					QUALIDADE_ALIMENTADOR_3D.transform.Translate(Vector3.right * Time.deltaTime * VELOCIDADE_QUALIDADE_ALIMENTADOR_3D);
					QUALIDADE_ALIMENTADOR_3D.GetComponent<AudioSource>().volume = 0.5f;
				}
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeAvancaAlimentador && !SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeRecuaAlimentador)
				{
					QUALIDADE_ALIMENTADOR_3D.GetComponent<AudioSource>().volume = 0f;
				}
				if (QUALIDADE_ALIMENTADOR_3D.transform.localPosition.x <= LIMITE_POSICAO_X_NEGATIVO_QUALIDADE_ALIMENTADOR_3D)
				{
					QUALIDADE_ALIMENTADOR_3D.transform.localPosition = new Vector3(LIMITE_POSICAO_X_NEGATIVO_QUALIDADE_ALIMENTADOR_3D, LIMITE_POSICAO_Y_QUALIDADE_ALIMENTADOR_3D, LIMITE_POSICAO_Z_QUALIDADE_ALIMENTADOR_3D);
					SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeAlimentadorAvancado = true;
				}
				else
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeAlimentadorAvancado = false;
				}
				if (QUALIDADE_ALIMENTADOR_3D.transform.localPosition.x >= LIMITE_POSICAO_X_POSITIVO_QUALIDADE_ALIMENTADOR_3D)
				{
					QUALIDADE_ALIMENTADOR_3D.transform.localPosition = new Vector3(LIMITE_POSICAO_X_POSITIVO_QUALIDADE_ALIMENTADOR_3D, LIMITE_POSICAO_Y_QUALIDADE_ALIMENTADOR_3D, LIMITE_POSICAO_Z_QUALIDADE_ALIMENTADOR_3D);
					SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeAlimentadorRecuado = true;
				}
				else
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeAlimentadorRecuado = false;
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeAcionaVacuo)
				{
					QUALIDADE_HOLDER_VENTOSA.SetActive(value: true);
					QUALIDADE_HOLDER_VENTOSA.GetComponent<AudioSource>().volume = 0.5f;
				}
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeAcionaVacuo)
				{
					QUALIDADE_HOLDER_VENTOSA.transform.DetachChildren();
					QUALIDADE_HOLDER_VENTOSA.SetActive(value: false);
					QUALIDADE_HOLDER_VENTOSA.GetComponent<AudioSource>().volume = 0f;
				}
				if (QUALIDADE_HOLDER_VENTOSA.transform.childCount < 1)
				{
					QUALIDADE_HOLDER_VENTOSA.transform.localPosition = POSICAO_QUALIDADE_HOLDER_VENTOSA_1;
					SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeSensorVacuo = false;
				}
				if (QUALIDADE_HOLDER_VENTOSA.transform.childCount >= 1)
				{
					QUALIDADE_HOLDER_VENTOSA.transform.localPosition = POSICAO_QUALIDADE_HOLDER_VENTOSA_2;
					SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeSensorVacuo = true;
				}
			}
			if (SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeInspecaoFalhou)
			{
				_ = SCRIPT_TOGGLES.ATIVAR_BIT_QualidadeInspecaoOk;
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_MontagemTriggerCamera)
				{
					PODE_INSTANCIAR_FLASH = true;
				}
				if (SCRIPT_TOGGLES.ATIVAR_BIT_MontagemTriggerCamera)
				{
					if (PODE_INSTANCIAR_FLASH)
					{
						UnityEngine.Object.Instantiate(GAMEOBJECT_FLASH_1, LOCAL_INSTANCIAR_GAMEOBJECT_FLASH_1.transform.position, LOCAL_INSTANCIAR_GAMEOBJECT_FLASH_1.transform.rotation);
					}
					if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "7")
					{
						if (SCRIPT_TOGGLES.VALOR_BIT_MontagemCorEsperada == MONTAGEM_COR_LAMINA_ATUAL)
						{
							SCRIPT_TOGGLES.ATIVAR_BIT_MontagemResultadoInspecao = true;
						}
						if (SCRIPT_TOGGLES.VALOR_BIT_MontagemCorEsperada != MONTAGEM_COR_LAMINA_ATUAL)
						{
							SCRIPT_TOGGLES.ATIVAR_BIT_MontagemResultadoInspecao = false;
						}
					}
					PODE_INSTANCIAR_FLASH = false;
				}
			}
			if (!EXECUTANDO_ANIMACAO_QUALIDADE)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_MontagemRoboOcupado = false;
				SCRIPT_TOGGLES.ATIVAR_BIT_MontagemRoboEmPosicao = true;
			}
			if (EXECUTANDO_ANIMACAO_QUALIDADE)
			{
				SCRIPT_TOGGLES.ATIVAR_BIT_MontagemRoboOcupado = true;
				SCRIPT_TOGGLES.ATIVAR_BIT_MontagemRoboEmPosicao = false;
			}
		}
		if (!SCRIPT_TOGGLES.ATIVAR_BIT_MontagemDriveRobo)
		{
			PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = true;
		}
		if (SCRIPT_TOGGLES.ATIVAR_BIT_MontagemDriveRobo && PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo)
		{
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "0")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_24_HOME", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "1")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_1_PEGAR_BLOCO_ESTEIRA_COLOCAR_B1", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "2")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_2_PEGAR_BLOCO_ESTEIRA_COLOCAR_B2", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "3")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_3_PEGAR_BLOCO_ESTEIRA_COLOCAR_B3", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "4")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_4_PEGAR_LAMINA_LOCAL_1_ESQUERDA", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "5")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_5_PEGAR_LAMINA_LOCAL_2_ESQUERDA", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "6")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_6_PEGAR_LAMINA_LOCAL_3_ESQUERDA", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "7")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_7_ANALISAR_LAMINA", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "8")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_8_REPROVAR_LAMINA", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "9")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_9_ANEXANDO_LAMINA_LADO_1_B1", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "10")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_10_ANEXANDO_LAMINA_LADO_3_B1", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "11")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_11_ANEXANDO_LAMINA_LADO_2_B1", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "12")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_12_ANEXANDO_LAMINA_LADO_1_B2", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "13")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_13_ANEXANDO_LAMINA_LADO_3_B2", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "14")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_14_ANEXANDO_LAMINA_LADO_2_B2", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "15")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_15_ANEXANDO_LAMINA_LADO_1_B3", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "16")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_16_ANEXANDO_LAMINA_LADO_3_B3", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "17")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_17_ANEXANDO_LAMINA_LADO_2_B3", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "18")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_18_PEGAR_BLOCO_B3_COLOCAR_TAMPA", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "19")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_19_EMPILHAR_BLOCO_2_ANDARES_B1", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "20")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_20_EMPILHAR_BLOCO_3_ANDARES_B1", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "21")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_21_PEGAR_BLOCO_B3_COLOCAR_ESTEIRA", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "22")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_22_PEGAR_BLOCO_B1_COLOCAR_ESTEIRA", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "23")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_24_HOME", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "24")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_24_HOME", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "25")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_25_PEGAR_LAMINA_LOCAL_1_DIREITA", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "26")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_26_PEGAR_LAMINA_LOCAL_2_DIREITA", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
			if (SCRIPT_TOGGLES.VALOR_BIT_MontagemPosicaoRobo == "27")
			{
				QUALIDADE_ROBO_PARTE_11_3D.GetComponent<Animation>().CrossFade("QUALIDADE_27_PEGAR_LAMINA_LOCAL_3_DIREITA", VELOCIDADE_QUALIDADE_ROBO);
				PODE_ENVIAR_SINAL_UNICO_ATIVAR_BIT_MontagemDriveRobo = false;
			}
		}
		if (MINIMIZAR_FUNCAO)
		{
			if (SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoEsteira1)
			{
				EXPEDICAO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().volume = 0.2f;
				EXPEDICAO_COLISAO_ESTEIRA_1.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
				if (BANCADA_1 == "EXPEDICAO (UI INPUTS HOLDER)")
				{
					RETORNO_ESQUERA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
					RETORNO_ESQUERDA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					RETORNO_ESQUERDA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
				}
				if (BANCADA_1 == "EXPEDICAO (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 1)
				{
					RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
					RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
				}
				if (BANCADA_2 == "EXPEDICAO (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 2)
				{
					RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
					RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
				}
				if (BANCADA_3 == "EXPEDICAO (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 3)
				{
					RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
					RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
				}
				if (BANCADA_4 == "EXPEDICAO (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 4)
				{
					RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
					RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
					RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = true;
				}
			}
			if (!SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoEsteira1)
			{
				EXPEDICAO_COLISAO_ESTEIRA_1.GetComponent<AudioSource>().volume = 0f;
				EXPEDICAO_COLISAO_ESTEIRA_1.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
				if (BANCADA_1 == "EXPEDICAO (UI INPUTS HOLDER)")
				{
					RETORNO_ESQUERA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
					RETORNO_ESQUERDA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					RETORNO_ESQUERDA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
				}
				if (BANCADA_1 == "EXPEDICAO (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 1)
				{
					RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
					RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
				}
				if (BANCADA_2 == "EXPEDICAO (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 2)
				{
					RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
					RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
				}
				if (BANCADA_3 == "EXPEDICAO (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 3)
				{
					RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
					RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
				}
				if (BANCADA_4 == "EXPEDICAO (UI INPUTS HOLDER)" && SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 4)
				{
					RETORNO_DIREITA_COLISAO_ADICIONAL.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
					RETORNO_DIREITA_CIRCULO_3D.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
					RETORNO_DIREITA_GIRO_COLISAO.GetComponent<SCRIPT_ROTACIONAR_CONSTANTEMENTE>().ROTACIONAR = false;
				}
			}
			if (SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoEsteira2)
			{
				EXPEDICAO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().volume = 0.2f;
				EXPEDICAO_COLISAO_ESTEIRA_2.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = true;
			}
			if (!SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoEsteira2)
			{
				EXPEDICAO_COLISAO_ESTEIRA_2.GetComponent<AudioSource>().volume = 0f;
				EXPEDICAO_COLISAO_ESTEIRA_2.GetComponent<SCRIPT_ESTEIRA>().ESTEIRA_LIGADA = false;
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoStopper1)
				{
					EXPEDICAO_MICRO_CILINDRO_1_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_MICRO_CILINDRO_1_POS_1", VELOCIDADE_MICRO_CILINDRO);
					if (EXPEDICAO_PODE_TOCAR_SOM_MICRO_CILINDRO_1_RECUAR)
					{
						UnityEngine.Object.Instantiate(SOM_MICRO_CILINDRO_RECUAR, base.transform.position, base.transform.rotation);
						EXPEDICAO_PODE_TOCAR_SOM_MICRO_CILINDRO_1_RECUAR = false;
						EXPEDICAO_PODE_TOCAR_SOM_MICRO_CILINDRO_1_AVANCAR = true;
					}
				}
				if (SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoStopper1)
				{
					EXPEDICAO_MICRO_CILINDRO_1_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_MICRO_CILINDRO_1_POS_2", VELOCIDADE_MICRO_CILINDRO);
					if (EXPEDICAO_PODE_TOCAR_SOM_MICRO_CILINDRO_1_AVANCAR)
					{
						UnityEngine.Object.Instantiate(SOM_MICRO_CILINDRO_AVANCAR, base.transform.position, base.transform.rotation);
						EXPEDICAO_PODE_TOCAR_SOM_MICRO_CILINDRO_1_AVANCAR = false;
						EXPEDICAO_PODE_TOCAR_SOM_MICRO_CILINDRO_1_RECUAR = true;
					}
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoStopper2)
				{
					EXPEDICAO_MICRO_CILINDRO_2_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_MICRO_CILINDRO_2_POS_1", VELOCIDADE_MICRO_CILINDRO);
					if (EXPEDICAO_PODE_TOCAR_SOM_MICRO_CILINDRO_2_RECUAR)
					{
						UnityEngine.Object.Instantiate(SOM_MICRO_CILINDRO_RECUAR, base.transform.position, base.transform.rotation);
						EXPEDICAO_PODE_TOCAR_SOM_MICRO_CILINDRO_2_RECUAR = false;
						EXPEDICAO_PODE_TOCAR_SOM_MICRO_CILINDRO_2_AVANCAR = true;
					}
				}
				if (SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoStopper2)
				{
					EXPEDICAO_MICRO_CILINDRO_2_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_MICRO_CILINDRO_2_POS_2", VELOCIDADE_MICRO_CILINDRO);
					if (EXPEDICAO_PODE_TOCAR_SOM_MICRO_CILINDRO_2_AVANCAR)
					{
						UnityEngine.Object.Instantiate(SOM_MICRO_CILINDRO_AVANCAR, base.transform.position, base.transform.rotation);
						EXPEDICAO_PODE_TOCAR_SOM_MICRO_CILINDRO_2_AVANCAR = false;
						EXPEDICAO_PODE_TOCAR_SOM_MICRO_CILINDRO_2_RECUAR = true;
					}
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAvancaAlimentador)
				{
					EXPEDICAO_ALIMENTADOR_3D.transform.Translate(-Vector3.right * Time.deltaTime * VELOCIDADE_EXPEDICAO_ALIMENTADOR_3D);
				}
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAvancaAlimentador)
				{
					EXPEDICAO_ALIMENTADOR_3D.transform.Translate(Vector3.right * Time.deltaTime * VELOCIDADE_EXPEDICAO_ALIMENTADOR_3D);
				}
				if (EXPEDICAO_ALIMENTADOR_3D.transform.localPosition.x <= LIMITE_POSICAO_X_NEGATIVO_EXPEDICAO_ALIMENTADOR_3D)
				{
					EXPEDICAO_ALIMENTADOR_3D.transform.localPosition = new Vector3(LIMITE_POSICAO_X_NEGATIVO_EXPEDICAO_ALIMENTADOR_3D, LIMITE_POSICAO_Y_EXPEDICAO_ALIMENTADOR_3D, LIMITE_POSICAO_Z_EXPEDICAO_ALIMENTADOR_3D);
					SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAlimentadorAvancado = true;
				}
				else
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAlimentadorAvancado = false;
				}
				if (EXPEDICAO_ALIMENTADOR_3D.transform.localPosition.x >= LIMITE_POSICAO_X_POSITIVO_EXPEDICAO_ALIMENTADOR_3D)
				{
					EXPEDICAO_ALIMENTADOR_3D.transform.localPosition = new Vector3(LIMITE_POSICAO_X_POSITIVO_EXPEDICAO_ALIMENTADOR_3D, LIMITE_POSICAO_Y_EXPEDICAO_ALIMENTADOR_3D, LIMITE_POSICAO_Z_EXPEDICAO_ALIMENTADOR_3D);
					SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAlimentadorRecuado = true;
				}
				else
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAlimentadorRecuado = false;
				}
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAlimentadorAvancado && !SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAlimentadorRecuado)
				{
					EXPEDICAO_ALIMENTADOR_3D.GetComponent<AudioSource>().volume = 0.5f;
				}
				if (SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAlimentadorAvancado || SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAlimentadorRecuado)
				{
					EXPEDICAO_ALIMENTADOR_3D.GetComponent<AudioSource>().volume = 0f;
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAcionaVacuo)
				{
					EXPEDICAO_HOLDER_VENTOSA.SetActive(value: true);
					EXPEDICAO_HOLDER_VENTOSA.GetComponent<AudioSource>().volume = 0.5f;
				}
				if (!SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAcionaVacuo)
				{
					EXPEDICAO_HOLDER_VENTOSA.transform.DetachChildren();
					EXPEDICAO_HOLDER_VENTOSA.SetActive(value: false);
					EXPEDICAO_HOLDER_VENTOSA.GetComponent<AudioSource>().volume = 0f;
				}
				if (EXPEDICAO_HOLDER_VENTOSA.transform.childCount < 1)
				{
					EXPEDICAO_HOLDER_VENTOSA.transform.localPosition = POSICAO_EXPEDICAO_HOLDER_VENTOSA_1;
					SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoSensorVacuo = false;
				}
				if (EXPEDICAO_HOLDER_VENTOSA.transform.childCount >= 1)
				{
					EXPEDICAO_HOLDER_VENTOSA.transform.localPosition = POSICAO_EXPEDICAO_HOLDER_VENTOSA_2;
					SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoSensorVacuo = true;
				}
			}
			if (SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoDriveAtuador)
			{
				EXECUTANDO_ANIMACAO_EXPEDICAO = true;
				MonoBehaviour.print("PODE ANIMAR BRAÇO EXPEDIÇÃO");
			}
			if (EXECUTANDO_ANIMACAO_EXPEDICAO)
			{
				if (SCRIPT_TOGGLES.VALOR_BIT_ExpedicaoPosicaoAtuador == "0")
				{
					EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_0", VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D);
					SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL = 0;
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ExpedicaoPosicaoAtuador == "1")
				{
					EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_1", VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D);
					SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL = 1;
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ExpedicaoPosicaoAtuador == "2")
				{
					EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_2", VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D);
					SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL = 2;
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ExpedicaoPosicaoAtuador == "3")
				{
					EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_3", VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D);
					SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL = 3;
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ExpedicaoPosicaoAtuador == "4")
				{
					EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_4", VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D);
					SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL = 4;
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ExpedicaoPosicaoAtuador == "5")
				{
					EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_5", VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D);
					SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL = 5;
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ExpedicaoPosicaoAtuador == "6")
				{
					EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_6", VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D);
					SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL = 6;
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ExpedicaoPosicaoAtuador == "7")
				{
					EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_7", VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D);
					SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL = 7;
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ExpedicaoPosicaoAtuador == "8")
				{
					EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_8", VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D);
					SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL = 8;
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ExpedicaoPosicaoAtuador == "9")
				{
					EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_9", VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D);
					SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL = 9;
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ExpedicaoPosicaoAtuador == "10")
				{
					EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_10", VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D);
					SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL = 10;
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ExpedicaoPosicaoAtuador == "11")
				{
					EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_11", VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D);
					SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL = 11;
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ExpedicaoPosicaoAtuador == "12")
				{
					EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_12", VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D);
					SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL = 12;
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ExpedicaoPosicaoAtuador == "13" && SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL != 14)
				{
					EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_13", VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D);
					SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL = 13;
					MonoBehaviour.print("ANIMAR EXPEDIÇÃO POS 13 NORMAL");
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ExpedicaoPosicaoAtuador == "13" && SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL == 14)
				{
					EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_13_DO_14_PRO_13", VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D);
					MonoBehaviour.print("ANIMAR EXPEDIÇÃO POS 13 DO 14 PRO 13");
				}
				if (SCRIPT_TOGGLES.VALOR_BIT_ExpedicaoPosicaoAtuador == "14")
				{
					SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL = 14;
					EXPEDICAO_BRACO_HORIZONTAL_3D.GetComponent<Animation>().CrossFade("EXPEDICAO_BRACO_HORIZONTAL_3D_POSICAO_14", VELOCIDADE_EXPEDICAO_BRACO_HORIZONTAL_3D);
				}
				MonoBehaviour.print(SCRIPT_TOGGLES.EXPEDICAO_POSICAO_ATUADOR_ATUAL);
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (Time.time > FREQUENCIA_EXPEDICAO_BRACO_ROTACAO_3D + ULTIMA_FREQUENCIA_EXPEDICAO_BRACO_ROTACAO_3D)
				{
					EXPEDICAO_BRACO_ROTACAO_3D_ULTIMA_ROTACAO = EXPEDICAO_BRACO_ROTACAO_3D.transform.localEulerAngles;
					EXPEDICAO_BRACO_ALTURA_3D_ULTIMA_POSICAO = EXPEDICAO_BRACO_ALTURA_3D.transform.localPosition;
					EXPEDICAO_BRACO_HORIZONTAL_3D_ULTIMA_POSICAO = EXPEDICAO_BRACO_HORIZONTAL_3D.transform.localPosition;
					ULTIMA_FREQUENCIA_EXPEDICAO_BRACO_ROTACAO_3D = Time.time;
				}
				if (EXPEDICAO_BRACO_ROTACAO_3D.transform.localEulerAngles == EXPEDICAO_BRACO_ROTACAO_3D_ULTIMA_ROTACAO && EXPEDICAO_BRACO_ALTURA_3D.transform.localPosition == EXPEDICAO_BRACO_ALTURA_3D_ULTIMA_POSICAO && EXPEDICAO_BRACO_HORIZONTAL_3D.transform.localPosition == EXPEDICAO_BRACO_HORIZONTAL_3D_ULTIMA_POSICAO && Time.time > FREQUENCIA_EXPEDICAO_BRACO_ROTACAO_3D + ULTIMA_FREQUENCIA_EXPEDICAO_BRACO_ROTACAO_3D - 0.2f)
				{
					EXECUTANDO_ANIMACAO_EXPEDICAO = false;
					SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAtuadorEmPosicao = true;
					SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAtuadorOcupado = EXECUTANDO_ANIMACAO_EXPEDICAO;
				}
				if ((EXPEDICAO_BRACO_ROTACAO_3D.transform.localEulerAngles != EXPEDICAO_BRACO_ROTACAO_3D_ULTIMA_ROTACAO || EXPEDICAO_BRACO_ALTURA_3D.transform.localPosition != EXPEDICAO_BRACO_ALTURA_3D_ULTIMA_POSICAO || EXPEDICAO_BRACO_HORIZONTAL_3D.transform.localPosition != EXPEDICAO_BRACO_HORIZONTAL_3D_ULTIMA_POSICAO) && Time.time > FREQUENCIA_EXPEDICAO_BRACO_ROTACAO_3D + ULTIMA_FREQUENCIA_EXPEDICAO_BRACO_ROTACAO_3D - 0.2f)
				{
					SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAtuadorEmPosicao = false;
					SCRIPT_TOGGLES.ATIVAR_BIT_ExpedicaoAtuadorOcupado = EXECUTANDO_ANIMACAO_EXPEDICAO;
				}
			}
		}
		if (!MINIMIZAR_FUNCAO)
		{
			return;
		}
		if (SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 4)
		{
			if (BANCADA_4 != "QUALIDADE (UI INPUTS HOLDER)" && BANCADA_1 != "QUALIDADE (UI INPUTS HOLDER)")
			{
				RETORNO_ESQUERDA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.32046f, 61.50217f);
				RETORNO_DIREITA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.320454f, -49.662f);
			}
			if (BANCADA_4 != "QUALIDADE (UI INPUTS HOLDER)" && (BANCADA_1 == "QUALIDADE (UI INPUTS HOLDER)" || BANCADA_2 == "QUALIDADE (UI INPUTS HOLDER)"))
			{
				RETORNO_ESQUERDA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.32046f, 61.50217f);
				RETORNO_DIREITA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.320454f, -49.662f);
			}
			CAMERA_POS_1.SetActive(value: true);
			CAMERA_POS_2.SetActive(value: true);
			CAMERA_POS_3.SetActive(value: true);
			CAMERA_POS_4.SetActive(value: true);
		}
		if (SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 3)
		{
			if (BANCADA_3 == "QUALIDADE (UI INPUTS HOLDER)")
			{
				RETORNO_ESQUERDA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.32046f, 61.50217f);
				RETORNO_DIREITA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.320454f, -26.166f);
			}
			if (BANCADA_3 != "QUALIDADE (UI INPUTS HOLDER)" && BANCADA_1 != "QUALIDADE (UI INPUTS HOLDER)")
			{
				RETORNO_ESQUERDA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.32046f, 61.50217f);
				RETORNO_DIREITA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.320454f, -14.272f);
			}
			if (BANCADA_3 != "QUALIDADE (UI INPUTS HOLDER)" && (BANCADA_1 == "QUALIDADE (UI INPUTS HOLDER)" || BANCADA_2 == "QUALIDADE (UI INPUTS HOLDER)"))
			{
				RETORNO_ESQUERDA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.32046f, 61.50217f);
				RETORNO_DIREITA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.320454f, -26.166f);
			}
			CAMERA_POS_1.SetActive(value: true);
			CAMERA_POS_2.SetActive(value: true);
			CAMERA_POS_3.SetActive(value: true);
			CAMERA_POS_4.SetActive(value: false);
		}
		if (SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 2)
		{
			if (BANCADA_2 != "QUALIDADE (UI INPUTS HOLDER)" && BANCADA_1 != "QUALIDADE (UI INPUTS HOLDER)")
			{
				RETORNO_ESQUERDA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.32046f, 61.50217f);
				RETORNO_DIREITA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.320454f, 9.354f);
			}
			if (BANCADA_2 != "QUALIDADE (UI INPUTS HOLDER)" && BANCADA_1 == "QUALIDADE (UI INPUTS HOLDER)")
			{
				RETORNO_ESQUERDA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.32046f, 61.50217f);
				RETORNO_DIREITA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.320454f, -2.54f);
			}
			if (BANCADA_2 == "QUALIDADE (UI INPUTS HOLDER)")
			{
				RETORNO_ESQUERDA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.32046f, 61.50217f);
				RETORNO_DIREITA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.320454f, -2.449362f);
			}
			CAMERA_POS_1.SetActive(value: true);
			CAMERA_POS_2.SetActive(value: true);
			CAMERA_POS_3.SetActive(value: false);
			CAMERA_POS_4.SetActive(value: false);
		}
		if (SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 1)
		{
			RETORNO_ESQUERDA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.32046f, 61.50217f);
			if (BANCADA_1 != "QUALIDADE (UI INPUTS HOLDER)")
			{
				RETORNO_DIREITA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.320454f, 32.99f);
			}
			if (BANCADA_1 == "QUALIDADE (UI INPUTS HOLDER)")
			{
				RETORNO_DIREITA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.320454f, 21.11289f);
				MonoBehaviour.print("BANCADA QUALIDADE SOZINHA");
			}
			CAMERA_POS_1.SetActive(value: true);
			CAMERA_POS_2.SetActive(value: false);
			CAMERA_POS_3.SetActive(value: false);
			CAMERA_POS_4.SetActive(value: false);
		}
		if (SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 0)
		{
			RETORNO_ESQUERDA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.32046f, 1000f);
			RETORNO_DIREITA_BASE_3D.transform.position = new Vector3(-10.12447f, 7.320454f, 1500f);
			CAMERA_POS_1.SetActive(value: false);
			CAMERA_POS_2.SetActive(value: false);
			CAMERA_POS_3.SetActive(value: false);
			CAMERA_POS_4.SetActive(value: false);
		}
	}

	private void OnGUI()
	{
		GUI.skin = skin_login;
		GUI.skin = skin_erro_internet;
		if (MOSTRAR_BOTAO_ERRO_DE_CONECTIVIDADE)
		{
			GUI.Box(new Rect(-100f, -50f, Screen.width + 200, Screen.height + 200), "");
			GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 10 - 70 - 70 + 100 - 80, 500f, 200f), "Erro de Conexão\n(Verifique sua Internet)");
			if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 10 + 100, 200f, 40f), "OK"))
			{
				MENSAGEM_REINICIAR_SERVIDOR_AGORA();
				MOSTRAR_BOTAO_ERRO_DE_CONECTIVIDADE = false;
			}
		}
		GUI.skin = skin_login;
		if (MOSTRAR_BOTAO_ERRO_DE_LOGIN_INCORRETO)
		{
			GUI.Box(new Rect(-100f, -50f, Screen.width + 200, Screen.height + 200), "");
			GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 10 - 70 - 70 + 100 - 80, 500f, 200f), "Erro de Login\n(" + erro_de_login + ")");
			if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 10 + 100, 200f, 40f), "OK"))
			{
				MOSTRAR_BOTAO_ERRO_DE_LOGIN_INCORRETO = false;
			}
		}
		GUI.skin = skin_ERRO_COMUNICAO;
		if (MOSTRAR_BOTAO_ERRO_DE_COMUNICACAO)
		{
			GUI.Box(new Rect(-100f, -50f, Screen.width + 200, Screen.height + 200), "");
			GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 10 - 70 - 70 + 100 - 80, 500f, 200f), "Erro de Comunicação\n" + MENSAGEM_ERRO_DE_COMUNICACAO);
			if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 10 + 100 - 50, 200f, 40f), "OK"))
			{
				MOSTRAR_BOTAO_ERRO_DE_COMUNICACAO = false;
				UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
				for (int i = 0; i < array.Length; i++)
				{
					((GameObject)array[i]).SendMessage("VOID_CLICOU_BOTAO_ENCERRAR_POR_SCRIPT", SendMessageOptions.DontRequireReceiver);
				}
				MENSAGEM_FECHAR_MODBUS_UI();
			}
		}
		GUI.skin = skin_login;
		if (!PODE_APARECER_BOTAO_INICIAR && !MOSTRAR_BOTAO_ERRO_DE_LOGIN_INCORRETO && !MENU_QUIT_GAME)
		{
			GUI.DrawTexture(new Rect((float)Screen.width / 1.2f - (float)(imagem_logo.width / 2 / 4), (float)Screen.height / 1.3f - (float)(imagem_logo.height / 2 / 4), imagem_logo.width / 4, imagem_logo.height / 4), imagem_logo);
			GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400f, 400f), "");
			GUI.Label(new Rect(Screen.width / 2 - 125, Screen.height / 2 - 10 - 70 - 30 - 40, 250f, 30f), "LOGIN");
			LOGIN_USUARIO = GUI.TextField(new Rect(Screen.width / 2 - 125, Screen.height / 2 - 10 - 70 - 40, 250f, 30f), LOGIN_USUARIO);
			GUI.Label(new Rect(Screen.width / 2 - 125, Screen.height / 2 - 10 + 50 - 70 - 30 - 10, 250f, 30f), "SENHA");
			LOGIN_SENHA = GUI.PasswordField(new Rect(Screen.width / 2 - 125, Screen.height / 2 - 10 + 50 - 70 - 10, 250f, 30f), LOGIN_SENHA, "*"[0], 25);
			GUI.Label(new Rect(Screen.width / 2 - 125, Screen.height / 2 - 10 + 50 + 50 - 70 - 30 + 20, 250f, 30f), "PIN");
			LOGIN_PIN = GUI.TextField(new Rect(Screen.width / 2 - 125, Screen.height / 2 - 10 + 50 + 50 - 70 + 20, 250f, 30f), LOGIN_PIN);
			GUI.DrawTexture(new Rect((float)Screen.width / 1.2f - (float)(imagem_logo_senai.width / 2 / 6 - 5), (float)Screen.height / 1.3f + 128f - (float)(imagem_logo_senai.height / 2 / 6), imagem_logo_senai.width / 6, imagem_logo_senai.height / 6), imagem_logo_senai);
			GUI.skin = skin_login_placeholder;
			GUI.skin = skin_login;
			if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 10 + 100 + 20, 200f, 40f), "ENTRAR"))
			{
				UnityEngine.Object.Instantiate(som_botao_1, base.transform.position, base.transform.rotation);
				if (!LOGIN_NECESSARIO)
				{
					PODE_APARECER_BOTAO_INICIAR = true;
					(CAMERA_VISUAL.GetComponent("BlurOptimized") as MonoBehaviour).enabled = false;
				}
				if (LOGIN_NECESSARIO)
				{
					PlayerPrefs.SetString("PLAYER_PREFS_USUARIO_SALVO_NO_PC", LOGIN_USUARIO);
					string value = Convert.ToBase64String(new UTF8Encoding().GetBytes(LOGIN_SENHA));
					PlayerPrefs.SetString("PLAYER_PREFS_LOGIN_SENHA_SALVO_NO_PC", value);
					PlayerPrefs.SetString("PLAYER_PREFS_LOGIN_PIN_SALVO_NO_PC", LOGIN_PIN);
					string[] array2 = login_service.DoLogin(LOGIN_USUARIO, LOGIN_SENHA, "https://simulators.labtronix.com.br", LOGIN_PIN, "192.168.0.1", "200").Split(';');
					if (array2[0] == "200")
					{
						token = array2[1];
						tempo_de_verificacao = int.Parse(array2[2]);
						MonoBehaviour.print("LOGIN REALIZADO COM SUCESSO: tempo " + tempo_de_verificacao);
						PODE_APARECER_BOTAO_INICIAR = true;
						UnityEngine.Object.Instantiate(som_botao_1, base.transform.position, base.transform.rotation);
						(CAMERA_VISUAL.GetComponent("BlurOptimized") as MonoBehaviour).enabled = false;
					}
					else
					{
						MonoBehaviour.print("ERRO DE LOGIN: " + array2[1]);
						erro_de_login = array2[1];
						MOSTRAR_BOTAO_ERRO_DE_LOGIN_INCORRETO = true;
						UnityEngine.Object.Instantiate(som_botao_1, base.transform.position, base.transform.rotation);
					}
				}
			}
		}
		GUI.skin = skin_2;
		if (PODE_APARECER_BOTAO_INICIAR && !mostrar_menu && !MENU_QUIT_GAME && GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height - Screen.height / 4 + 100, 200f, 50f), "Iniciar"))
		{
			CAMERA.GetComponent<Animation>().CrossFade("CAMERA_FRONTAL", 0.1f);
			UnityEngine.Object.Instantiate(som_botao_1, base.transform.position, base.transform.rotation);
			modbus_menu.SetActive(value: false);
			mostrar_menu = true;
			BANCADAS_ATIVAR_UI.SetActive(value: true);
			BANCADAS_INPUTS_UI.SetActive(value: true);
			SMARTBLOCK_UI.SetActive(value: true);
			OPCOES_UI.SetActive(value: true);
		}
		GUI.skin = skin_fechar_aplicativo;
		if (!MENU_QUIT_GAME)
		{
			return;
		}
		GUI.Box(new Rect(-100f, -50f, Screen.width + 200, Screen.height + 200), "");
		GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 125, 400f, 100f), "FECHAR APLICATIVO");
		if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200f, 50f), "CONFIRMAR"))
		{
			if (LOGIN_NECESSARIO)
			{
				login_service.DoLogout("https://simulators.labtronix.com.br", token);
				MonoBehaviour.print("LOGOUT");
			}
			UnityEngine.Object.Instantiate(som_botao_1, base.transform.position, base.transform.rotation);
			Application.Quit();
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 25, 200f, 50f), "CANCELAR"))
		{
			MENU_QUIT_GAME = false;
			TOGGLE_MODBUS.GetComponent<Toggle>().isOn = false;
			menu_options = false;
			UnityEngine.Object.Instantiate(som_botao_1, base.transform.position, base.transform.rotation);
		}
	}

	private void OnApplicationQuit()
	{
		MonoBehaviour.print("TESTE DE FECHAR APP - DO LOGOUT");
		PlayerPrefs.SetString("PLAYER_PREFS_USUARIO_SALVO_NO_PC", "Digite seu e-mail");
		PlayerPrefs.SetString("PLAYER_PREFS_LOGIN_SENHA_SALVO_NO_PC", "Senha");
		PlayerPrefs.SetString("PLAYER_PREFS_LOGIN_PIN_SALVO_NO_PC", "PIN da turma");
		string value = Convert.ToBase64String(new UTF8Encoding().GetBytes("Senha"));
		PlayerPrefs.SetString("PLAYER_PREFS_LOGIN_SENHA_SALVO_NO_PC", value);
		if (LOGIN_NECESSARIO)
		{
			login_service.DoLogout("https://simulators.labtronix.com.br", token);
		}
	}

	private void MENSAGEM_FECHAR_MODBUS_UI()
	{
		TOGGLE_MODBUS.GetComponent<Toggle>().isOn = false;
		menu_options = false;
		MENSAGEM_SOM_BOTAO_1();
	}

	private void MENSAGEM_SOM_BOTAO_1()
	{
		UnityEngine.Object.Instantiate(som_botao_1, base.transform.position, base.transform.rotation);
	}

	private void MENSAGEM_SOM_BOTAO_2()
	{
		UnityEngine.Object.Instantiate(som_botao_2, base.transform.position, base.transform.rotation);
	}

	private void MENSAGEM_SOM_BOTAO_3()
	{
		UnityEngine.Object.Instantiate(som_botao_3, base.transform.position, base.transform.rotation);
	}

	private void INSTANCIAR_SMARTBLOCK()
	{
		if (!(Time.time > FREQUENCIA_ADD_SMARTBLOCK + ULTIMO_ADD_SMARTBLOCK))
		{
			return;
		}
		if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DO_BLOCO == "Bloco (Preto)" && SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_DO_PEDIDO_DESEJADO != "")
		{
			INSTANCIAR_SMARTBLOCK_TOGGLE();
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Esteira [1]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTEIRA_1.transform.position, LOCAL_ESTEIRA_1.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Esteira [2]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTEIRA_2.transform.position, LOCAL_ESTEIRA_2.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [1]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_1.transform.position, LOCAL_ESTOQUE_1.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [2]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_2.transform.position, LOCAL_ESTOQUE_2.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [3]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_3.transform.position, LOCAL_ESTOQUE_3.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [4]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_4.transform.position, LOCAL_ESTOQUE_4.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [5]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_5.transform.position, LOCAL_ESTOQUE_5.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [6]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_6.transform.position, LOCAL_ESTOQUE_6.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [7]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_7.transform.position, LOCAL_ESTOQUE_7.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [8]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_8.transform.position, LOCAL_ESTOQUE_8.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [9]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_9.transform.position, LOCAL_ESTOQUE_9.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [10]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_10.transform.position, LOCAL_ESTOQUE_10.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [11]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_11.transform.position, LOCAL_ESTOQUE_11.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [12]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_12.transform.position, LOCAL_ESTOQUE_12.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [13]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_13.transform.position, LOCAL_ESTOQUE_13.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [14]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_14.transform.position, LOCAL_ESTOQUE_14.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [15]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_15.transform.position, LOCAL_ESTOQUE_15.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [16]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_16.transform.position, LOCAL_ESTOQUE_16.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [17]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_17.transform.position, LOCAL_ESTOQUE_17.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [18]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_18.transform.position, LOCAL_ESTOQUE_18.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [19]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_19.transform.position, LOCAL_ESTOQUE_19.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [20]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_20.transform.position, LOCAL_ESTOQUE_20.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [21]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_21.transform.position, LOCAL_ESTOQUE_21.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [22]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_22.transform.position, LOCAL_ESTOQUE_22.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [23]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_23.transform.position, LOCAL_ESTOQUE_23.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [24]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_24.transform.position, LOCAL_ESTOQUE_24.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [25]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_25.transform.position, LOCAL_ESTOQUE_25.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [26]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_26.transform.position, LOCAL_ESTOQUE_26.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [27]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_27.transform.position, LOCAL_ESTOQUE_27.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [28]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_PRETO, LOCAL_ESTOQUE_28.transform.position, LOCAL_ESTOQUE_28.transform.rotation);
				}
			}
			SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().NUMERO_DO_PEDIDO_DESEJADO = int.Parse(SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_DO_PEDIDO_DESEJADO);
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Vermelha)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 1;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Azul)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 2;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Amarela)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 3;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Verde)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 4;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Preta)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 5;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Branca)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 6;
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Vermelha)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 1;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Azul)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 2;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Amarela)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 3;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Verde)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 4;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Preta)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 5;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Branca)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 6;
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Vermelha)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 1;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Azul)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 2;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Amarela)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 3;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Verde)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 4;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Preta)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 5;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Branca)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 6;
				}
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_ANDAR_DO_BLOCO_DESEJADO == "Andar desejado (1)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO_DESEJADO = 1;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_ANDAR_DO_BLOCO_DESEJADO == "Andar desejado (2)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO_DESEJADO = 2;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_ANDAR_DO_BLOCO_DESEJADO == "Andar desejado (3)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO_DESEJADO = 3;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_TOTAL_DE_ANDARES_DESEJADO == "Total de andares (1)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES_DESEJADO = 1;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_TOTAL_DE_ANDARES_DESEJADO == "Total de andares (2)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES_DESEJADO = 2;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_TOTAL_DE_ANDARES_DESEJADO == "Total de andares (3)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES_DESEJADO = 3;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_OPERACAO_DESEJADO == "Operação (Estoque)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().NUMERO_OPERACAO_DESEJADO = 0;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_OPERACAO_DESEJADO == "Operação (Processo)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().NUMERO_OPERACAO_DESEJADO = 1;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_OPERACAO_DESEJADO == "Operação (Montagem)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().NUMERO_OPERACAO_DESEJADO = 2;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_OPERACAO_DESEJADO == "Operação (Expedição)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().NUMERO_OPERACAO_DESEJADO = 3;
			}
		}
		if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DO_BLOCO == "Bloco (Vermelho)" && SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_DO_PEDIDO_DESEJADO != "")
		{
			INSTANCIAR_SMARTBLOCK_TOGGLE();
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Esteira [1]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTEIRA_1.transform.position, LOCAL_ESTEIRA_1.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Esteira [2]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTEIRA_2.transform.position, LOCAL_ESTEIRA_2.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [1]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_1.transform.position, LOCAL_ESTOQUE_1.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [2]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_2.transform.position, LOCAL_ESTOQUE_2.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [3]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_3.transform.position, LOCAL_ESTOQUE_3.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [4]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_4.transform.position, LOCAL_ESTOQUE_4.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [5]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_5.transform.position, LOCAL_ESTOQUE_5.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [6]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_6.transform.position, LOCAL_ESTOQUE_6.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [7]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_7.transform.position, LOCAL_ESTOQUE_7.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [8]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_8.transform.position, LOCAL_ESTOQUE_8.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [9]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_9.transform.position, LOCAL_ESTOQUE_9.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [10]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_10.transform.position, LOCAL_ESTOQUE_10.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [11]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_11.transform.position, LOCAL_ESTOQUE_11.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [12]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_12.transform.position, LOCAL_ESTOQUE_12.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [13]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_13.transform.position, LOCAL_ESTOQUE_13.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [14]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_14.transform.position, LOCAL_ESTOQUE_14.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [15]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_15.transform.position, LOCAL_ESTOQUE_15.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [16]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_16.transform.position, LOCAL_ESTOQUE_16.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [17]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_17.transform.position, LOCAL_ESTOQUE_17.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [18]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_18.transform.position, LOCAL_ESTOQUE_18.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [19]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_19.transform.position, LOCAL_ESTOQUE_19.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [20]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_20.transform.position, LOCAL_ESTOQUE_20.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [21]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_21.transform.position, LOCAL_ESTOQUE_21.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [22]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_22.transform.position, LOCAL_ESTOQUE_22.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [23]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_23.transform.position, LOCAL_ESTOQUE_23.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [24]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_24.transform.position, LOCAL_ESTOQUE_24.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [25]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_25.transform.position, LOCAL_ESTOQUE_25.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [26]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_26.transform.position, LOCAL_ESTOQUE_26.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [27]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_27.transform.position, LOCAL_ESTOQUE_27.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [28]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_VERMELHO, LOCAL_ESTOQUE_28.transform.position, LOCAL_ESTOQUE_28.transform.rotation);
				}
			}
			SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().NUMERO_DO_PEDIDO_DESEJADO = int.Parse(SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_DO_PEDIDO_DESEJADO);
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Vermelha)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 1;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Azul)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 2;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Amarela)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 3;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Verde)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 4;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Preta)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 5;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Branca)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 6;
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Vermelha)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 1;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Azul)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 2;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Amarela)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 3;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Verde)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 4;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Preta)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 5;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Branca)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 6;
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Vermelha)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 1;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Azul)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 2;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Amarela)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 3;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Verde)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 4;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Preta)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 5;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Branca)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 6;
				}
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_ANDAR_DO_BLOCO_DESEJADO == "Andar desejado (1)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO_DESEJADO = 1;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_ANDAR_DO_BLOCO_DESEJADO == "Andar desejado (2)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO_DESEJADO = 2;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_ANDAR_DO_BLOCO_DESEJADO == "Andar desejado (3)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO_DESEJADO = 3;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_TOTAL_DE_ANDARES_DESEJADO == "Total de andares (1)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES_DESEJADO = 1;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_TOTAL_DE_ANDARES_DESEJADO == "Total de andares (2)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES_DESEJADO = 2;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_TOTAL_DE_ANDARES_DESEJADO == "Total de andares (3)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES_DESEJADO = 3;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_OPERACAO_DESEJADO == "Operação (Estoque)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().NUMERO_OPERACAO_DESEJADO = 0;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_OPERACAO_DESEJADO == "Operação (Processo)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().NUMERO_OPERACAO_DESEJADO = 1;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_OPERACAO_DESEJADO == "Operação (Montagem)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().NUMERO_OPERACAO_DESEJADO = 2;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_OPERACAO_DESEJADO == "Operação (Expedição)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().NUMERO_OPERACAO_DESEJADO = 3;
			}
		}
		if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DO_BLOCO == "Bloco (Azul)" && SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_DO_PEDIDO_DESEJADO != "")
		{
			INSTANCIAR_SMARTBLOCK_TOGGLE();
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Esteira [1]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTEIRA_1.transform.position, LOCAL_ESTEIRA_1.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Esteira [2]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTEIRA_2.transform.position, LOCAL_ESTEIRA_2.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [1]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_1.transform.position, LOCAL_ESTOQUE_1.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [2]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_2.transform.position, LOCAL_ESTOQUE_2.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [3]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_3.transform.position, LOCAL_ESTOQUE_3.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [4]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_4.transform.position, LOCAL_ESTOQUE_4.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [5]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_5.transform.position, LOCAL_ESTOQUE_5.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [6]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_6.transform.position, LOCAL_ESTOQUE_6.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [7]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_7.transform.position, LOCAL_ESTOQUE_7.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [8]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_8.transform.position, LOCAL_ESTOQUE_8.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [9]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_9.transform.position, LOCAL_ESTOQUE_9.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [10]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_10.transform.position, LOCAL_ESTOQUE_10.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [11]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_11.transform.position, LOCAL_ESTOQUE_11.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [12]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_12.transform.position, LOCAL_ESTOQUE_12.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [13]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_13.transform.position, LOCAL_ESTOQUE_13.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [14]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_14.transform.position, LOCAL_ESTOQUE_14.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [15]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_15.transform.position, LOCAL_ESTOQUE_15.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [16]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_16.transform.position, LOCAL_ESTOQUE_16.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [17]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_17.transform.position, LOCAL_ESTOQUE_17.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [18]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_18.transform.position, LOCAL_ESTOQUE_18.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [19]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_19.transform.position, LOCAL_ESTOQUE_19.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [20]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_20.transform.position, LOCAL_ESTOQUE_20.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [21]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_21.transform.position, LOCAL_ESTOQUE_21.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [22]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_22.transform.position, LOCAL_ESTOQUE_22.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [23]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_23.transform.position, LOCAL_ESTOQUE_23.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [24]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_24.transform.position, LOCAL_ESTOQUE_24.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [25]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_25.transform.position, LOCAL_ESTOQUE_25.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [26]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_26.transform.position, LOCAL_ESTOQUE_26.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [27]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_27.transform.position, LOCAL_ESTOQUE_27.transform.rotation);
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR == "Local: Estoque [28]")
				{
					SMARTBLOCK_INSTANCIADO = UnityEngine.Object.Instantiate(PREFAB_SMARTBLOCK_BASE_AZUL, LOCAL_ESTOQUE_28.transform.position, LOCAL_ESTOQUE_28.transform.rotation);
				}
			}
			SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().NUMERO_DO_PEDIDO_DESEJADO = int.Parse(SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_DO_PEDIDO_DESEJADO);
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Vermelha)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 1;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Azul)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 2;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Amarela)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 3;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Verde)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 4;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Preta)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 5;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_1 == "Lâmina 1 (Branca)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_1_DESEJADO = 6;
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Vermelha)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 1;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Azul)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 2;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Amarela)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 3;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Verde)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 4;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Preta)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 5;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_2 == "Lâmina 2 (Branca)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_2_DESEJADO = 6;
				}
			}
			if (MINIMIZAR_FUNCAO)
			{
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Vermelha)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 1;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Azul)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 2;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Amarela)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 3;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Verde)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 4;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Preta)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 5;
				}
				if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_COR_DA_LAMINA_3 == "Lâmina 3 (Branca)")
				{
					SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().COR_DA_LAMINA_3_DESEJADO = 6;
				}
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_ANDAR_DO_BLOCO_DESEJADO == "Andar desejado (1)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO_DESEJADO = 1;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_ANDAR_DO_BLOCO_DESEJADO == "Andar desejado (2)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO_DESEJADO = 2;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_ANDAR_DO_BLOCO_DESEJADO == "Andar desejado (3)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO_DESEJADO = 3;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_TOTAL_DE_ANDARES_DESEJADO == "Total de andares (1)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES_DESEJADO = 1;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_TOTAL_DE_ANDARES_DESEJADO == "Total de andares (2)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES_DESEJADO = 2;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_TOTAL_DE_ANDARES_DESEJADO == "Total de andares (3)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().TOTAL_DE_ANDARES_DESEJADO = 3;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_OPERACAO_DESEJADO == "Operação (Estoque)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().NUMERO_OPERACAO_DESEJADO = 0;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_OPERACAO_DESEJADO == "Operação (Processo)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().NUMERO_OPERACAO_DESEJADO = 1;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_OPERACAO_DESEJADO == "Operação (Montagem)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().NUMERO_OPERACAO_DESEJADO = 2;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_NUMERO_OPERACAO_DESEJADO == "Operação (Expedição)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().NUMERO_OPERACAO_DESEJADO = 3;
			}
		}
		if (MINIMIZAR_FUNCAO && (bool)SMARTBLOCK_INSTANCIADO)
		{
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_PADRAO_LAMINA_1 == "Padrão Lâmina 1 (1)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().PADRAO_LAMINA_1_DESEJADO = 1;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_PADRAO_LAMINA_1 == "Padrão Lâmina 1 (2)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().PADRAO_LAMINA_1_DESEJADO = 2;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_PADRAO_LAMINA_1 == "Padrão Lâmina 1 (3)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().PADRAO_LAMINA_1_DESEJADO = 3;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_PADRAO_LAMINA_2 == "Padrão Lâmina 2 (1)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().PADRAO_LAMINA_2_DESEJADO = 1;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_PADRAO_LAMINA_2 == "Padrão Lâmina 2 (2)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().PADRAO_LAMINA_2_DESEJADO = 2;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_PADRAO_LAMINA_2 == "Padrão Lâmina 2 (3)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().PADRAO_LAMINA_2_DESEJADO = 3;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_PADRAO_LAMINA_3 == "Padrão Lâmina 3 (1)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().PADRAO_LAMINA_3_DESEJADO = 1;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_PADRAO_LAMINA_3 == "Padrão Lâmina 3 (2)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().PADRAO_LAMINA_3_DESEJADO = 2;
			}
			if (SCRIPT_CRIAR_SMARTBLOCK.ESCOLHIDO_PADRAO_LAMINA_3 == "Padrão Lâmina 3 (3)")
			{
				SMARTBLOCK_INSTANCIADO.GetComponent<SCRIPT_SMARTBLOCK>().PADRAO_LAMINA_3_DESEJADO = 3;
			}
		}
		ULTIMO_ADD_SMARTBLOCK = Time.time;
		UnityEngine.Object.Instantiate(som_botao_instanciar, base.transform.position, base.transform.rotation);
	}

	private void INSTANCIAR_SMARTBLOCK_TOGGLE()
	{
		if (!menu_options)
		{
			VAR_INSTANCIAR_SMARTBLOCK_TOGGLE = !VAR_INSTANCIAR_SMARTBLOCK_TOGGLE;
			UnityEngine.Object.Instantiate(som_botao_1, base.transform.position, base.transform.rotation);
		}
	}

	private void FECHAR_OPCOES_ADD_BLOCK()
	{
		VAR_INSTANCIAR_SMARTBLOCK_TOGGLE = false;
	}

	private void FECHAR_OPCOES_ADD_BLOCK_X()
	{
		VAR_INSTANCIAR_SMARTBLOCK_TOGGLE = false;
		UnityEngine.Object.Instantiate(som_botao_1, base.transform.position, base.transform.rotation);
	}

	private void ENVIAR_MENSAGEM_DELETAR_SMARTBLOCK()
	{
		UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
		for (int i = 0; i < array.Length; i++)
		{
			((GameObject)array[i]).SendMessage("VOID_PODE_DELETAR_BLOCO", SendMessageOptions.DontRequireReceiver);
		}
		UnityEngine.Object.Instantiate(som_botao_delete, base.transform.position, base.transform.rotation);
	}

	private void MENSAGEM_MODO_JANELA()
	{
		Screen.fullScreen = false;
		Screen.SetResolution(1280, 720, fullscreen: false);
		MENSAGEM_REINICIAR_SERVIDOR_AGORA();
	}

	private void MENSAGEM_TELA_CHEIA()
	{
		Screen.fullScreen = true;
		Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, fullscreen: true);
		MonoBehaviour.print(Screen.currentResolution);
		MENSAGEM_REINICIAR_SERVIDOR_AGORA();
	}

	private void MENSAGEM_REINICIAR_SERVIDOR_AGORA()
	{
		if (LOGIN_NECESSARIO)
		{
			login_service.DoLogout("https://simulators.labtronix.com.br", token);
		}
		reiniciar_servidor_agora = true;
	}

	private IEnumerator AGUARDAR_E_INSTANCIAR_OBJETO()
	{
		FADE_1.SetActive(value: true);
		yield return new WaitForSeconds(3f);
		Application.LoadLevel(0);
	}

	public void MENSAGEM_INSTANCIAR_LAMINA_BRANCA_PARTE_2()
	{
		if (PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.childCount <= 0)
		{
			SMARTBLOCK_LAMINA_INSTANCIADA = UnityEngine.Object.Instantiate(SMARTBLOCK_LAMINA_BRANCO, PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.position, PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.rotation);
			SMARTBLOCK_LAMINA_INSTANCIADA.transform.parent = PROCESSO_LOCAL_INSTANCIAR_LAMINA.gameObject.transform;
			SMARTBLOCK_LAMINA_INSTANCIADA.GetComponent<Rigidbody>().useGravity = false;
			SMARTBLOCK_LAMINA_INSTANCIADA.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		}
	}

	public void MENSAGEM_INSTANCIAR_LAMINA_PRETA_PARTE_2()
	{
		if (PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.childCount <= 0)
		{
			SMARTBLOCK_LAMINA_INSTANCIADA = UnityEngine.Object.Instantiate(SMARTBLOCK_LAMINA_PRETO, PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.position, PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.rotation);
			SMARTBLOCK_LAMINA_INSTANCIADA.transform.parent = PROCESSO_LOCAL_INSTANCIAR_LAMINA.gameObject.transform;
			SMARTBLOCK_LAMINA_INSTANCIADA.GetComponent<Rigidbody>().useGravity = false;
			SMARTBLOCK_LAMINA_INSTANCIADA.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		}
	}

	public void MENSAGEM_INSTANCIAR_LAMINA_VERDE_PARTE_2()
	{
		if (PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.childCount <= 0)
		{
			SMARTBLOCK_LAMINA_INSTANCIADA = UnityEngine.Object.Instantiate(SMARTBLOCK_LAMINA_VERDE, PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.position, PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.rotation);
			SMARTBLOCK_LAMINA_INSTANCIADA.transform.parent = PROCESSO_LOCAL_INSTANCIAR_LAMINA.gameObject.transform;
			SMARTBLOCK_LAMINA_INSTANCIADA.GetComponent<Rigidbody>().useGravity = false;
			SMARTBLOCK_LAMINA_INSTANCIADA.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		}
	}

	public void MENSAGEM_INSTANCIAR_LAMINA_AMARELA_PARTE_2()
	{
		if (PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.childCount <= 0)
		{
			SMARTBLOCK_LAMINA_INSTANCIADA = UnityEngine.Object.Instantiate(SMARTBLOCK_LAMINA_AMARELO, PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.position, PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.rotation);
			SMARTBLOCK_LAMINA_INSTANCIADA.transform.parent = PROCESSO_LOCAL_INSTANCIAR_LAMINA.gameObject.transform;
			SMARTBLOCK_LAMINA_INSTANCIADA.GetComponent<Rigidbody>().useGravity = false;
			SMARTBLOCK_LAMINA_INSTANCIADA.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		}
	}

	public void MENSAGEM_INSTANCIAR_LAMINA_AZUL_PARTE_2()
	{
		if (PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.childCount <= 0)
		{
			SMARTBLOCK_LAMINA_INSTANCIADA = UnityEngine.Object.Instantiate(SMARTBLOCK_LAMINA_AZUL, PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.position, PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.rotation);
			SMARTBLOCK_LAMINA_INSTANCIADA.transform.parent = PROCESSO_LOCAL_INSTANCIAR_LAMINA.gameObject.transform;
			SMARTBLOCK_LAMINA_INSTANCIADA.GetComponent<Rigidbody>().useGravity = false;
			SMARTBLOCK_LAMINA_INSTANCIADA.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		}
	}

	public void MENSAGEM_INSTANCIAR_LAMINA_VERMELHA_PARTE_2()
	{
		if (PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.childCount <= 0)
		{
			SMARTBLOCK_LAMINA_INSTANCIADA = UnityEngine.Object.Instantiate(SMARTBLOCK_LAMINA_VERMELHO, PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.position, PROCESSO_LOCAL_INSTANCIAR_LAMINA.transform.rotation);
			SMARTBLOCK_LAMINA_INSTANCIADA.transform.parent = PROCESSO_LOCAL_INSTANCIAR_LAMINA.gameObject.transform;
			SMARTBLOCK_LAMINA_INSTANCIADA.GetComponent<Rigidbody>().useGravity = false;
			SMARTBLOCK_LAMINA_INSTANCIADA.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		}
	}
}
