using UnityEngine;

public class SCRIPT_LIMITAR_FPS : MonoBehaviour
{
	public int FPS = 60;

	private void Start()
	{
		Application.targetFrameRate = FPS;
	}
}
