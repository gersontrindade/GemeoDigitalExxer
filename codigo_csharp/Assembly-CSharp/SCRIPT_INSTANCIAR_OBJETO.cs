using System.Collections;
using UnityEngine;

public class SCRIPT_INSTANCIAR_OBJETO : MonoBehaviour
{
	public bool INSTANCIAR_APOS_START;

	public bool INSTANCIAR_APOS_AGUARDAR;

	public bool INSTANCIAR_APOS_CHAMAR;

	public float TEMPO_DE_ESPERA;

	public GameObject OBJETO;

	private void Start()
	{
		if (INSTANCIAR_APOS_START)
		{
			Object.Instantiate(OBJETO, base.transform.position, base.transform.rotation);
		}
		if (INSTANCIAR_APOS_AGUARDAR)
		{
			StartCoroutine(AGUARDAR_E_INSTANCIAR_OBJETO());
		}
	}

	private IEnumerator AGUARDAR_E_INSTANCIAR_OBJETO()
	{
		yield return new WaitForSeconds(TEMPO_DE_ESPERA);
		Object.Instantiate(OBJETO, base.transform.position, base.transform.rotation);
	}

	private void VOID_INSTANCIAR_APOS_CHAMAR()
	{
		if (INSTANCIAR_APOS_CHAMAR)
		{
			Object.Instantiate(OBJETO, base.transform.position, base.transform.rotation);
		}
	}
}
