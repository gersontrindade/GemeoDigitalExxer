using UnityEngine;

public class SCRIPT_INSTANCIADOR_LAMINA : MonoBehaviour
{
	public bool LAMINA_NO_TRIGGER;

	public GameObject LAMINA_INSTANCIAR;

	public GameObject LOCAL_INSTANCIAR_LAMINA;

	public GameObject LAMINA_NO_TRIGGER_ATUAL;

	private void Start()
	{
		INSTANCIAR_LAMINA_AGORA();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "LAMINA")
		{
			MonoBehaviour.print("LAMINA_NO_TRIGGER");
			LAMINA_NO_TRIGGER = true;
			LAMINA_NO_TRIGGER_ATUAL = other.gameObject;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "LAMINA")
		{
			MonoBehaviour.print("SEM LAMINA_NO_TRIGGER");
			LAMINA_NO_TRIGGER = false;
			LAMINA_NO_TRIGGER_ATUAL = null;
			INSTANCIAR_LAMINA_AGORA();
		}
	}

	private void Update()
	{
		Input.GetKeyUp(KeyCode.Space);
		if (LAMINA_NO_TRIGGER_ATUAL == null)
		{
			INSTANCIAR_LAMINA_AGORA();
		}
	}

	private void INSTANCIAR_LAMINA_AGORA()
	{
		Object.Instantiate(LAMINA_INSTANCIAR, LOCAL_INSTANCIAR_LAMINA.transform.position, LOCAL_INSTANCIAR_LAMINA.transform.rotation);
	}
}
