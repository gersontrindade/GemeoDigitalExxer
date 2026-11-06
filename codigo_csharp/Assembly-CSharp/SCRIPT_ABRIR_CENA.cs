using System.Collections;
using UnityEngine;

public class SCRIPT_ABRIR_CENA : MonoBehaviour
{
	public int CENA;

	public float DELAY;

	public bool TOCAR_SOM;

	public GameObject SOM_1;

	public void ABRIR_CENA()
	{
		ABRIR_CENA_PARTE_2();
	}

	private IEnumerator ABRIR_CENA_PARTE_2()
	{
		if (TOCAR_SOM)
		{
			Object.Instantiate(SOM_1, base.transform.position, base.transform.rotation);
		}
		yield return new WaitForSeconds(DELAY);
		Application.LoadLevel(CENA);
	}
}
