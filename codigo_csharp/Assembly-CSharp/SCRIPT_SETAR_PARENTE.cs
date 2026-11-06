using UnityEngine;

public class SCRIPT_SETAR_PARENTE : MonoBehaviour
{
	public string OBJETO_NOME;

	public GameObject OBJETO_3D;

	private void Start()
	{
		OBJETO_3D = GameObject.Find(OBJETO_NOME);
		base.transform.parent = OBJETO_3D.transform;
	}
}
