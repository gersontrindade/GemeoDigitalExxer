using UnityEngine;

public class SCRIPT_SOMBRA : MonoBehaviour
{
	public float VELOCIDADE_CROSSFADE = 1f;

	private void Update()
	{
		if (SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 0)
		{
			GetComponent<Animation>().CrossFade("SOMBRA_ANIMACAO_0", VELOCIDADE_CROSSFADE);
		}
		if (SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 1)
		{
			GetComponent<Animation>().CrossFade("SOMBRA_ANIMACAO_1", VELOCIDADE_CROSSFADE);
		}
		if (SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 2)
		{
			GetComponent<Animation>().CrossFade("SOMBRA_ANIMACAO_2", VELOCIDADE_CROSSFADE);
		}
		if (SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 3)
		{
			GetComponent<Animation>().CrossFade("SOMBRA_ANIMACAO_3", VELOCIDADE_CROSSFADE);
		}
		if (SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 4)
		{
			GetComponent<Animation>().CrossFade("SOMBRA_ANIMACAO_4", VELOCIDADE_CROSSFADE);
		}
		if (SCRIPT_NUMERO_PARENTES.NUMERO_DE_PARENTES == 5)
		{
			GetComponent<Animation>().CrossFade("SOMBRA_ANIMACAO_5", VELOCIDADE_CROSSFADE);
		}
	}
}
