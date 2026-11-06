using UnityEngine;

public class SCRIPT_NUMERO_PARENTES : MonoBehaviour
{
	public static int NUMERO_DE_PARENTES;

	private void Update()
	{
		NUMERO_DE_PARENTES = base.gameObject.transform.childCount;
	}
}
