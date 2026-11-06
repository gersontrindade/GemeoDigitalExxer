using UnityEngine;

public class SCRIPT_DELETAR_BLOCO : MonoBehaviour
{
	public bool PODE_DELETAR_BLOCO;

	public GUISkin SKIN_1;

	public Texture2D cursor_normal;

	public Texture2D cursor_deletar;

	public Texture2D cursor_deletar_agora;

	public CursorMode cursorMode;

	public Vector2 hotSpot = Vector2.zero;

	public Camera camera;

	public GameObject OBJETO_SELECIONADO;

	public GameObject SOM_DESTRUIR;

	public void VOID_PODE_DELETAR_BLOCO()
	{
		PODE_DELETAR_BLOCO = !PODE_DELETAR_BLOCO;
	}

	private void Start()
	{
		PODE_DELETAR_BLOCO = false;
	}

	private void Update()
	{
		if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hitInfo))
		{
			_ = hitInfo.transform;
			OBJETO_SELECIONADO = hitInfo.transform.gameObject;
			if (OBJETO_SELECIONADO.tag == "SMARTBLOCK_BASE" && OBJETO_SELECIONADO.GetComponent<SCRIPT_SMARTBLOCK>().ANDAR_DO_BLOCO == 1 && PODE_DELETAR_BLOCO && Input.GetMouseButtonUp(0))
			{
				Object.Instantiate(SOM_DESTRUIR, base.transform.position, base.transform.rotation);
				hitInfo.transform.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().SendMessage("DELETAR_SMARTBLOCK");
				PODE_DELETAR_BLOCO = false;
			}
			if (OBJETO_SELECIONADO.tag == "SMARTBLOCK_TAMPA" && !OBJETO_SELECIONADO.GetComponent<SCRIPT_SMARTBLOCK>().JA_TENHO_PARENTE && PODE_DELETAR_BLOCO && Input.GetMouseButtonUp(0) && OBJETO_SELECIONADO.transform.parent.name != "SMARTBLOCK_BASE")
			{
				Object.Instantiate(SOM_DESTRUIR, base.transform.position, base.transform.rotation);
				hitInfo.transform.gameObject.GetComponent<SCRIPT_SMARTBLOCK>().SendMessage("DELETAR_SMARTBLOCK");
				PODE_DELETAR_BLOCO = false;
			}
		}
		if (Input.GetMouseButtonUp(1))
		{
			PODE_DELETAR_BLOCO = false;
		}
	}

	private void OnGUI()
	{
		GUI.skin = SKIN_1;
		if (PODE_DELETAR_BLOCO)
		{
			GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400f, 30f), "Selecione um bloco para remover.");
		}
	}
}
