using UnityEngine;
using UnityEngine.UI;

public class SCRIPT_BOTAO_ENCERRAR : MonoBehaviour
{
	private void VOID_CLICOU_BOTAO_ENCERRAR_POR_SCRIPT()
	{
		GetComponent<Button>().onClick.Invoke();
		MonoBehaviour.print("BOTAO ERRO");
	}
}
