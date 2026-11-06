using UnityEngine;

public class Prever_E_Seguir : MonoBehaviour
{
	public Transform target;

	public float smoothTime;

	public float xVelocity;

	public float yVelocity;

	public float zVelocity;

	private void Update()
	{
		if ((bool)target)
		{
			float x = Mathf.SmoothDamp(base.transform.position.x, target.position.x, ref xVelocity, smoothTime);
			float y = Mathf.SmoothDamp(base.transform.position.y, target.position.y, ref yVelocity, smoothTime);
			float z = Mathf.SmoothDamp(base.transform.position.z, target.position.z, ref zVelocity, smoothTime);
			base.transform.position = new Vector3(x, y, z);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, target.transform.rotation, Time.deltaTime * 3f);
		}
	}
}
