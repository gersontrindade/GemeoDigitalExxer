using UnityEngine;

public class SCRIPT_MOVER_TEXTURA : MonoBehaviour
{
	public float ScrollX;

	public float ScrollY = -0.5f;

	private void Update()
	{
		float x = Time.time * ScrollX;
		float y = Time.time * ScrollY;
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x, y);
	}
}
