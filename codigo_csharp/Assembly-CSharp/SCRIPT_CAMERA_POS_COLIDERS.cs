using UnityEngine;
using UnityEngine.EventSystems;

public class SCRIPT_CAMERA_POS_COLIDERS : MonoBehaviour
{
	public string MENSAGEM;

	private void OnMouseEnter()
	{
		if (!Administracao.menu_options)
		{
			GetComponent<MeshRenderer>().enabled = true;
		}
	}

	private void OnMouseUp()
	{
		if (!Administracao.menu_options && !EventSystem.current.IsPointerOverGameObject())
		{
			Object[] array = Object.FindObjectsOfType(typeof(GameObject));
			for (int i = 0; i < array.Length; i++)
			{
				((GameObject)array[i]).SendMessage(MENSAGEM, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	private void OnMouseExit()
	{
		if (!Administracao.menu_options)
		{
			GetComponent<MeshRenderer>().enabled = false;
		}
	}

	private void Update()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			GetComponent<MeshRenderer>().enabled = false;
		}
	}
}
