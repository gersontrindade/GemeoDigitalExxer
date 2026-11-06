using System.Collections;
using UnityEngine;

public class SCRIPT_ENVIAR_MENSAGEM : MonoBehaviour
{
	public bool ENVIAR_APOS_START;

	public bool ENVIAR_APOS_AGUARDAR;

	public bool ENVIAR_APOS_CHAMAR;

	public float TEMPO_DE_ESPERA;

	public string MENSAGEM = "NOME DA MENSAGEM";

	private void Start()
	{
		if (ENVIAR_APOS_START)
		{
			Object[] array = Object.FindObjectsOfType(typeof(GameObject));
			for (int i = 0; i < array.Length; i++)
			{
				((GameObject)array[i]).SendMessage(MENSAGEM, SendMessageOptions.DontRequireReceiver);
			}
		}
		if (ENVIAR_APOS_AGUARDAR)
		{
			StartCoroutine(AGUARDAR_E_ENVIAR_MENSAGEM());
		}
	}

	private IEnumerator AGUARDAR_E_ENVIAR_MENSAGEM()
	{
		yield return new WaitForSeconds(TEMPO_DE_ESPERA);
		Object[] array = Object.FindObjectsOfType(typeof(GameObject));
		for (int i = 0; i < array.Length; i++)
		{
			((GameObject)array[i]).SendMessage(MENSAGEM, SendMessageOptions.DontRequireReceiver);
		}
	}

	private void VOID_ENVIAR_APOS_CHAMAR()
	{
		if (ENVIAR_APOS_CHAMAR)
		{
			Object[] array = Object.FindObjectsOfType(typeof(GameObject));
			for (int i = 0; i < array.Length; i++)
			{
				((GameObject)array[i]).SendMessage(MENSAGEM, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
