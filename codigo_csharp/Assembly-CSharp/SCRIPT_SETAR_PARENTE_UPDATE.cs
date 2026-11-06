using UnityEngine;

public class SCRIPT_SETAR_PARENTE_UPDATE : MonoBehaviour
{
	public string OBJETO_NOME;

	public GameObject OBJETO_3D;

	private void Update()
	{
		OBJETO_3D = GameObject.Find(OBJETO_NOME);
		base.transform.parent = OBJETO_3D.transform;
	}
}
