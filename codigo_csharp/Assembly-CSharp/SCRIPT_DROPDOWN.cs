using UnityEngine;
using UnityEngine.UI;

public class SCRIPT_DROPDOWN : MonoBehaviour
{
	public GameObject OBJETO_ENVIAR_VALORES;

	public Dropdown VALORES;

	public bool FUNCAO_ANIMACAO;

	public bool FUNCAO_TIPO_DE_BANCADA;

	private void Start()
	{
		VALORES.onValueChanged.AddListener(delegate
		{
			FUNCAO_VALORES(VALORES);
		});
	}

	private void MENSAGEM_CAMERA_LIVRE()
	{
		VALORES.value = 0;
	}

	public void FUNCAO_VALORES(Dropdown sender)
	{
		if (FUNCAO_ANIMACAO)
		{
			_ = sender.value;
			if (sender.value == 1)
			{
				OBJETO_ENVIAR_VALORES.GetComponent<Animation>().CrossFade("CAMERA_FRONTAL", 0.1f);
				Object[] array = Object.FindObjectsOfType(typeof(GameObject));
				for (int i = 0; i < array.Length; i++)
				{
					((GameObject)array[i]).SendMessage("MENSAGEM_SOM_BOTAO_1", SendMessageOptions.DontRequireReceiver);
				}
			}
			if (sender.value == 2)
			{
				OBJETO_ENVIAR_VALORES.GetComponent<Animation>().CrossFade("CAMERA_TOPO", 0.1f);
				Object[] array = Object.FindObjectsOfType(typeof(GameObject));
				for (int i = 0; i < array.Length; i++)
				{
					((GameObject)array[i]).SendMessage("MENSAGEM_SOM_BOTAO_1", SendMessageOptions.DontRequireReceiver);
				}
			}
			if (sender.value == 3)
			{
				OBJETO_ENVIAR_VALORES.GetComponent<Animation>().CrossFade("CAMERA_LATERAL", 0.1f);
				Object[] array = Object.FindObjectsOfType(typeof(GameObject));
				for (int i = 0; i < array.Length; i++)
				{
					((GameObject)array[i]).SendMessage("MENSAGEM_SOM_BOTAO_1", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		if (!FUNCAO_TIPO_DE_BANCADA)
		{
			return;
		}
		if (sender.value == 0)
		{
			ExampleUModbusTCP.DROPDOWN_TIPO_DE_BANCADA = 0;
			Object[] array = Object.FindObjectsOfType(typeof(GameObject));
			for (int i = 0; i < array.Length; i++)
			{
				((GameObject)array[i]).SendMessage("MENSAGEM_SOM_BOTAO_1", SendMessageOptions.DontRequireReceiver);
			}
		}
		if (sender.value == 1)
		{
			ExampleUModbusTCP.DROPDOWN_TIPO_DE_BANCADA = 1;
			Object[] array = Object.FindObjectsOfType(typeof(GameObject));
			for (int i = 0; i < array.Length; i++)
			{
				((GameObject)array[i]).SendMessage("MENSAGEM_SOM_BOTAO_1", SendMessageOptions.DontRequireReceiver);
			}
		}
		if (sender.value == 2)
		{
			ExampleUModbusTCP.DROPDOWN_TIPO_DE_BANCADA = 2;
			Object[] array = Object.FindObjectsOfType(typeof(GameObject));
			for (int i = 0; i < array.Length; i++)
			{
				((GameObject)array[i]).SendMessage("MENSAGEM_SOM_BOTAO_1", SendMessageOptions.DontRequireReceiver);
			}
		}
		if (sender.value == 3)
		{
			ExampleUModbusTCP.DROPDOWN_TIPO_DE_BANCADA = 3;
			Object[] array = Object.FindObjectsOfType(typeof(GameObject));
			for (int i = 0; i < array.Length; i++)
			{
				((GameObject)array[i]).SendMessage("MENSAGEM_SOM_BOTAO_1", SendMessageOptions.DontRequireReceiver);
			}
		}
		if (sender.value == 4)
		{
			ExampleUModbusTCP.DROPDOWN_TIPO_DE_BANCADA = 4;
			Object[] array = Object.FindObjectsOfType(typeof(GameObject));
			for (int i = 0; i < array.Length; i++)
			{
				((GameObject)array[i]).SendMessage("MENSAGEM_SOM_BOTAO_1", SendMessageOptions.DontRequireReceiver);
			}
		}
		if (sender.value == 5)
		{
			ExampleUModbusTCP.DROPDOWN_TIPO_DE_BANCADA = 5;
			Object[] array = Object.FindObjectsOfType(typeof(GameObject));
			for (int i = 0; i < array.Length; i++)
			{
				((GameObject)array[i]).SendMessage("MENSAGEM_SOM_BOTAO_1", SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
