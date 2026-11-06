using UnityEngine;

public class SCRIPT_REMOVER_PARENTE : MonoBehaviour
{
	private void Start()
	{
		base.transform.parent = null;
	}
}
