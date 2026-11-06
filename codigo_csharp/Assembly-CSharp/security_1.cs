using UnityEngine;

public class security_1 : MonoBehaviour
{
	public GUISkin skin_1;

	public string mensagem_watermark = "PROPRIEDADE DE MICROBLAST GAMES LTDA\nESTE PRODUTO NÃO PODE SER UTILIZADO SEM AUTORIZAÇÃO DA EMPRESA";

	public string mensagem_watermark_review;

	public bool tentou_hackear;

	private void Start()
	{
		mensagem_watermark_review = mensagem_watermark;
	}

	private void Update()
	{
		if (mensagem_watermark_review != mensagem_watermark)
		{
			MonoBehaviour.print("QUIT GAME");
			Application.Quit();
			tentou_hackear = true;
		}
		if (mensagem_watermark == "")
		{
			MonoBehaviour.print("QUIT GAME");
			Application.Quit();
			tentou_hackear = true;
		}
		if (mensagem_watermark != mensagem_watermark_review)
		{
			MonoBehaviour.print("QUIT GAME");
			Application.Quit();
			tentou_hackear = true;
		}
		if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.E))
		{
			mensagem_watermark = " ";
			mensagem_watermark_review = " ";
		}
	}

	private void OnGUI()
	{
		GUI.skin = skin_1;
		GUI.Box(new Rect(0f, 0f, Screen.width, Screen.height), mensagem_watermark);
		GUI.Box(new Rect(0f, Screen.height / 2, Screen.width, Screen.height), mensagem_watermark);
		GUI.Box(new Rect(0f, Screen.height - 70, Screen.width, Screen.height), mensagem_watermark);
		if (tentou_hackear)
		{
			GUI.Button(new Rect(0f, 0f, Screen.width, Screen.height), "PIRATA DETECTADO");
		}
	}
}
