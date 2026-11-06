using UnityEngine;

public class SCRIPT_VIRAR_PARENTE_AO_COLIDIR : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		base.transform.parent = collision.transform;
	}
}
