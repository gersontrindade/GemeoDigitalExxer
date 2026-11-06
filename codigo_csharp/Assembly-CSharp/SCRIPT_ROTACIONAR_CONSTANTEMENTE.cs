using System.Collections;
using UnityEngine;

public class SCRIPT_ROTACIONAR_CONSTANTEMENTE : MonoBehaviour
{
	public bool ROTACIONAR;

	public bool ROTACIONAR_APOS_START;

	public bool ROTACIONAR_APOS_AGUARDAR;

	public bool ROTACIONAR_APOS_CHAMAR;

	public float TEMPO_DE_ESPERA;

	public float ROTACIONAR_X;

	public float ROTACIONAR_Y;

	public float ROTACIONAR_Z;

	private void Start()
	{
		if (ROTACIONAR_APOS_START)
		{
			ROTACIONAR = true;
		}
		if (ROTACIONAR_APOS_AGUARDAR)
		{
			StartCoroutine(AGUARDAR());
		}
	}

	private IEnumerator AGUARDAR()
	{
		yield return new WaitForSeconds(TEMPO_DE_ESPERA);
		ROTACIONAR = true;
	}

	private void Update()
	{
		if (ROTACIONAR)
		{
			base.transform.Rotate(ROTACIONAR_X * Time.deltaTime, ROTACIONAR_Y * Time.deltaTime, ROTACIONAR_Z * Time.deltaTime);
		}
	}
}
