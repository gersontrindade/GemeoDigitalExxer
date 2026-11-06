using System.Collections;
using UnityEngine;

public class SCRIPT_ATIVAR_DESATIVAR_TOGGLE : MonoBehaviour
{
	public float TEMPO_DE_ESPERA;

	public GameObject OBJETO;

	private void Start()
	{
		StartCoroutine(AGUARDAR());
	}

	private IEnumerator AGUARDAR()
	{
		yield return new WaitForSeconds(TEMPO_DE_ESPERA);
		OBJETO.SetActive(value: true);
	}
}
