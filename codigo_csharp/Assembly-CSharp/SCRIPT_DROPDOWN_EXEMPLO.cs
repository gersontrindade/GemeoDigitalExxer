using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCRIPT_DROPDOWN_EXEMPLO : MonoBehaviour
{
	public string ESCOLHIDO_COR_DO_BLOCO;

	private void Start()
	{
		Dropdown dropdown = base.transform.GetComponent<Dropdown>();
		dropdown.options.Clear();
		foreach (string item in new List<string> { "Bloco (Preto)", "Bloco (Vermelho)", "Bloco (Azul)" })
		{
			dropdown.options.Add(new Dropdown.OptionData
			{
				text = item
			});
		}
		SELECIONADO_COR_DO_BLOCO(dropdown);
		dropdown.onValueChanged.AddListener(delegate
		{
			SELECIONADO_COR_DO_BLOCO(dropdown);
		});
	}

	private void SELECIONADO_COR_DO_BLOCO(Dropdown dropdown)
	{
		int value = dropdown.value;
		ESCOLHIDO_COR_DO_BLOCO = dropdown.options[value].text;
	}

	private void Update()
	{
	}
}
