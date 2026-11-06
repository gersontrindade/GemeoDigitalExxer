using UnityEngine;

public class SCRIPT_BANCADAS_MOVER : MonoBehaviour
{
	public bool BANCADA_ESTOQUE;

	public bool BANCADA_PROCESSO;

	public bool BANCADA_QUALIDADE;

	public bool BANCADA_EXPEDICAO;

	public Transform BANCADA_POSICAO_1;

	public Transform BANCADA_POSICAO_2;

	public Transform BANCADA_POSICAO_3;

	public Transform BANCADA_POSICAO_4;

	public GameObject BANCADA;

	public int NUMERO_NA_FILA_PARENTE;

	public float VELOCIDADE_MUDAR_LOCAL = 1f;

	public bool debug;

	private void Start()
	{
		if (BANCADA_ESTOQUE)
		{
			BANCADA = GameObject.Find("ESTOQUE_3D");
		}
		if (BANCADA_PROCESSO)
		{
			BANCADA = GameObject.Find("PROCESSO_3D");
		}
		if (BANCADA_QUALIDADE)
		{
			BANCADA = GameObject.Find("QUALIDADE_3D");
		}
		if (BANCADA_EXPEDICAO)
		{
			BANCADA = GameObject.Find("EXPEDICAO_3D");
		}
	}

	private void Update()
	{
		if (debug)
		{
			MonoBehaviour.print(base.transform.GetSiblingIndex());
		}
		NUMERO_NA_FILA_PARENTE = base.transform.GetSiblingIndex();
		if (base.transform.GetSiblingIndex() == 0)
		{
			if (!BANCADA_QUALIDADE)
			{
				BANCADA.transform.position = Vector3.Lerp(base.transform.position, new Vector3(BANCADA.transform.position.x, BANCADA.transform.position.y, BANCADA_POSICAO_1.transform.position.z), VELOCIDADE_MUDAR_LOCAL);
				Administracao.BANCADA_1 = base.gameObject.name;
			}
			if (BANCADA_QUALIDADE)
			{
				BANCADA.transform.position = Vector3.Lerp(base.transform.position, new Vector3(BANCADA.transform.position.x, BANCADA.transform.position.y, 35.41f), VELOCIDADE_MUDAR_LOCAL);
				Administracao.BANCADA_1 = base.gameObject.name;
			}
		}
		if (base.transform.GetSiblingIndex() == 1)
		{
			if (!BANCADA_QUALIDADE && Administracao.BANCADA_1 != "QUALIDADE (UI INPUTS HOLDER)")
			{
				BANCADA.transform.position = Vector3.Lerp(base.transform.position, new Vector3(BANCADA.transform.position.x, BANCADA.transform.position.y, BANCADA_POSICAO_2.transform.position.z), VELOCIDADE_MUDAR_LOCAL);
				Administracao.BANCADA_2 = base.gameObject.name;
			}
			if (!BANCADA_QUALIDADE && Administracao.BANCADA_1 == "QUALIDADE (UI INPUTS HOLDER)")
			{
				BANCADA.transform.position = Vector3.Lerp(base.transform.position, new Vector3(BANCADA.transform.position.x, BANCADA.transform.position.y, BANCADA_POSICAO_2.transform.position.z - 11.894f), VELOCIDADE_MUDAR_LOCAL);
				Administracao.BANCADA_2 = base.gameObject.name;
			}
			if (BANCADA_QUALIDADE)
			{
				BANCADA.transform.position = Vector3.Lerp(base.transform.position, new Vector3(BANCADA.transform.position.x, BANCADA.transform.position.y, 11.82f), VELOCIDADE_MUDAR_LOCAL);
				Administracao.BANCADA_2 = base.gameObject.name;
			}
		}
		if (base.transform.GetSiblingIndex() == 2)
		{
			if (!BANCADA_QUALIDADE && Administracao.BANCADA_1 != "QUALIDADE (UI INPUTS HOLDER)")
			{
				BANCADA.transform.position = Vector3.Lerp(base.transform.position, new Vector3(BANCADA.transform.position.x, BANCADA.transform.position.y, BANCADA_POSICAO_3.transform.position.z), VELOCIDADE_MUDAR_LOCAL);
				Administracao.BANCADA_3 = base.gameObject.name;
			}
			if (!BANCADA_QUALIDADE && (Administracao.BANCADA_1 == "QUALIDADE (UI INPUTS HOLDER)" || Administracao.BANCADA_2 == "QUALIDADE (UI INPUTS HOLDER)"))
			{
				BANCADA.transform.position = Vector3.Lerp(base.transform.position, new Vector3(BANCADA.transform.position.x, BANCADA.transform.position.y, BANCADA_POSICAO_3.transform.position.z - 11.894f), VELOCIDADE_MUDAR_LOCAL);
				Administracao.BANCADA_3 = base.gameObject.name;
			}
			if (BANCADA_QUALIDADE)
			{
				BANCADA.transform.position = Vector3.Lerp(base.transform.position, new Vector3(BANCADA.transform.position.x, BANCADA.transform.position.y, -11.76f), VELOCIDADE_MUDAR_LOCAL);
				Administracao.BANCADA_3 = base.gameObject.name;
			}
		}
		if (base.transform.GetSiblingIndex() == 3)
		{
			if (BANCADA_QUALIDADE)
			{
				BANCADA.transform.position = Vector3.Lerp(base.transform.position, new Vector3(BANCADA.transform.position.x, BANCADA.transform.position.y, BANCADA_POSICAO_4.transform.position.z), VELOCIDADE_MUDAR_LOCAL);
				Administracao.BANCADA_4 = base.gameObject.name;
			}
			if (!BANCADA_QUALIDADE && Administracao.BANCADA_1 != "QUALIDADE (UI INPUTS HOLDER)")
			{
				BANCADA.transform.position = Vector3.Lerp(base.transform.position, new Vector3(BANCADA.transform.position.x, BANCADA.transform.position.y, BANCADA_POSICAO_4.transform.position.z), VELOCIDADE_MUDAR_LOCAL);
				Administracao.BANCADA_4 = base.gameObject.name;
			}
			if (!BANCADA_QUALIDADE && (Administracao.BANCADA_1 == "QUALIDADE (UI INPUTS HOLDER)" || Administracao.BANCADA_2 == "QUALIDADE (UI INPUTS HOLDER)"))
			{
				BANCADA.transform.position = Vector3.Lerp(base.transform.position, new Vector3(BANCADA.transform.position.x, BANCADA.transform.position.y, BANCADA_POSICAO_4.transform.position.z), VELOCIDADE_MUDAR_LOCAL);
				Administracao.BANCADA_4 = base.gameObject.name;
			}
		}
	}

	public void MOVER_ESQUERDA()
	{
		base.transform.SetSiblingIndex(base.transform.GetSiblingIndex() - 1);
		Object[] array = Object.FindObjectsOfType(typeof(GameObject));
		for (int i = 0; i < array.Length; i++)
		{
			((GameObject)array[i]).SendMessage("DESTRUIR_LAMINA_AGORA", SendMessageOptions.DontRequireReceiver);
		}
	}

	public void MOVER_DIREITA()
	{
		base.transform.SetSiblingIndex(base.transform.GetSiblingIndex() + 1);
		Object[] array = Object.FindObjectsOfType(typeof(GameObject));
		for (int i = 0; i < array.Length; i++)
		{
			((GameObject)array[i]).SendMessage("DESTRUIR_LAMINA_AGORA", SendMessageOptions.DontRequireReceiver);
		}
	}
}
