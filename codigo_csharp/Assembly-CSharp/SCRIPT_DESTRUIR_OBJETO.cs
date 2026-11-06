using System.Collections;
using UnityEngine;

public class SCRIPT_DESTRUIR_OBJETO : MonoBehaviour
{
	public bool DESTRUIR_APOS_START;

	public bool DESTRUIR_APOS_AGUARDAR;

	public bool DESTRUIR_APOS_CHAMAR;

	public bool DESTRUIR_SE_NAO_TIVER_PARENTE;

	public float TEMPO_DE_ESPERA;

	public GameObject OBJETO;

	private void Start()
	{
		if (DESTRUIR_APOS_START)
		{
			Object.Destroy(OBJETO);
		}
		if (DESTRUIR_APOS_AGUARDAR)
		{
			StartCoroutine(AGUARDAR_E_DESTRUIR_OBJETO());
		}
		if (DESTRUIR_SE_NAO_TIVER_PARENTE)
		{
			StartCoroutine(AGUARDAR_E_DESTRUIR_OBJETO_SEM_PARENT());
		}
	}

	private IEnumerator AGUARDAR_E_DESTRUIR_OBJETO()
	{
		yield return new WaitForSeconds(TEMPO_DE_ESPERA);
		Object.Destroy(OBJETO);
	}

	private IEnumerator AGUARDAR_E_DESTRUIR_OBJETO_SEM_PARENT()
	{
		yield return new WaitForSeconds(TEMPO_DE_ESPERA);
		if (base.transform.parent == null)
		{
			Object.Destroy(OBJETO);
		}
	}

	private void VOID_DESTRUIR_APOS_CHAMAR()
	{
		if (DESTRUIR_APOS_CHAMAR)
		{
			Object.Destroy(OBJETO);
		}
	}
}
