using UnityEngine;

public class SCRIPT_VELOCIDADE_ANIMACAO : MonoBehaviour
{
	public float velocidade_animacao = 1f;

	public string anim = "";

	private void Start()
	{
		GetComponent<Animation>()[anim].speed = velocidade_animacao;
	}
}
