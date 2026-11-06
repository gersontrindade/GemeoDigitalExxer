using System.Collections;
using UnityEngine;

public class SCRIPT_SOM_MASTER_FADE_IN : MonoBehaviour
{
	private void Start()
	{
		AudioListener.volume = 0f;
		StartCoroutine(AGUARDAR());
	}

	private IEnumerator AGUARDAR()
	{
		yield return new WaitForSeconds(1f);
		AudioListener.volume = 1f;
	}
}
