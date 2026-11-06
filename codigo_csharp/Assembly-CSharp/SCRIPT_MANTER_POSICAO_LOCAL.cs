using UnityEngine;

public class SCRIPT_MANTER_POSICAO_LOCAL : MonoBehaviour
{
	public float POS_LOCAL_X;

	public float POS_LOCAL_Y;

	public float POS_LOCAL_Z;

	private void Update()
	{
		base.transform.localPosition = new Vector3(POS_LOCAL_X, POS_LOCAL_Y, POS_LOCAL_Z);
	}
}
