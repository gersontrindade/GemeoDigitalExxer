using UnityEngine;

public class SCRIPT_SEMPRE_OLHAR_CAMERA : MonoBehaviour
{
	private void Update()
	{
		base.transform.LookAt(Camera.main.transform.position);
	}
}
