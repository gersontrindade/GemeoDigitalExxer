using UnityEngine;
using UnityEngine.EventSystems;

public class ExampleClass : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log(base.gameObject.name + " Was Clicked.");
	}
}
