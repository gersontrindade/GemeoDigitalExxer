using UnityEngine;

public class SCRIPT_INSTANCIAR_FLASH : MonoBehaviour
{
	public GameObject OBJETO;

	private void VOID_INSTANCIAR_FLASH()
	{
		Object.Instantiate(OBJETO, base.transform.position, base.transform.rotation);
	}
}
