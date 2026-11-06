using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCRIPT_CRIAR_SMARTBLOCK : MonoBehaviour
{
	public static string ESCOLHIDO_COR_DO_BLOCO;

	public static string ESCOLHIDO_COR_DA_LAMINA_1;

	public static string ESCOLHIDO_COR_DA_LAMINA_2;

	public static string ESCOLHIDO_COR_DA_LAMINA_3;

	public static string ESCOLHIDO_ANDAR_DO_BLOCO_DESEJADO;

	public static string ESCOLHIDO_TOTAL_DE_ANDARES_DESEJADO;

	public static string ESCOLHIDO_NUMERO_OPERACAO_DESEJADO;

	public static string ESCOLHIDO_NUMERO_DO_PEDIDO_DESEJADO;

	public static string ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR;

	public static string ESCOLHIDO_PADRAO_LAMINA_1;

	public static string ESCOLHIDO_PADRAO_LAMINA_2;

	public static string ESCOLHIDO_PADRAO_LAMINA_3;

	public GameObject TEXTFIELD_NUMERO_DO_PEDIDO_DESEJADO;

	public bool DROPDOWN_COR_DO_BLOCO;

	public bool DROPDOWN_COR_DA_LAMINA_1;

	public bool DROPDOWN_COR_DA_LAMINA_2;

	public bool DROPDOWN_COR_DA_LAMINA_3;

	public bool DROPDOWN_ANDAR_DO_BLOCO_DESEJADO;

	public bool DROPDOWN_TOTAL_DE_ANDARES_DESEJADO;

	public bool DROPDOWN_NUMERO_OPERACAO_DESEJADO;

	public bool DROPDOWN_NUMERO_DO_PEDIDO_DESEJADO;

	public bool DROPDOWN_LOCAL_INSTANCIAR;

	public bool DROPDOWN_PADRAO_LAMINA_1;

	public bool DROPDOWN_PADRAO_LAMINA_2;

	public bool DROPDOWN_PADRAO_LAMINA_3;

	private void Awake()
	{
		if (DROPDOWN_PADRAO_LAMINA_1)
		{
			Dropdown dropdown_DROPDOWN_PADRAO_LAMINA_1 = base.transform.GetComponent<Dropdown>();
			dropdown_DROPDOWN_PADRAO_LAMINA_1.options.Clear();
			foreach (string item in new List<string> { "Padrão Lâmina 1 (1)", "Padrão Lâmina 1 (2)", "Padrão Lâmina 1 (3)" })
			{
				dropdown_DROPDOWN_PADRAO_LAMINA_1.options.Add(new Dropdown.OptionData
				{
					text = item
				});
			}
			VOID_DROPDOWN_PADRAO_LAMINA_1(dropdown_DROPDOWN_PADRAO_LAMINA_1);
			dropdown_DROPDOWN_PADRAO_LAMINA_1.onValueChanged.AddListener(delegate
			{
				VOID_DROPDOWN_PADRAO_LAMINA_1(dropdown_DROPDOWN_PADRAO_LAMINA_1);
			});
		}
		if (DROPDOWN_PADRAO_LAMINA_2)
		{
			Dropdown dropdown_DROPDOWN_PADRAO_LAMINA_2 = base.transform.GetComponent<Dropdown>();
			dropdown_DROPDOWN_PADRAO_LAMINA_2.options.Clear();
			foreach (string item2 in new List<string> { "Padrão Lâmina 2 (1)", "Padrão Lâmina 2 (2)", "Padrão Lâmina 2 (3)" })
			{
				dropdown_DROPDOWN_PADRAO_LAMINA_2.options.Add(new Dropdown.OptionData
				{
					text = item2
				});
			}
			VOID_DROPDOWN_PADRAO_LAMINA_2(dropdown_DROPDOWN_PADRAO_LAMINA_2);
			dropdown_DROPDOWN_PADRAO_LAMINA_2.onValueChanged.AddListener(delegate
			{
				VOID_DROPDOWN_PADRAO_LAMINA_2(dropdown_DROPDOWN_PADRAO_LAMINA_2);
			});
		}
		if (DROPDOWN_PADRAO_LAMINA_3)
		{
			Dropdown dropdown_DROPDOWN_PADRAO_LAMINA_3 = base.transform.GetComponent<Dropdown>();
			dropdown_DROPDOWN_PADRAO_LAMINA_3.options.Clear();
			foreach (string item3 in new List<string> { "Padrão Lâmina 3 (1)", "Padrão Lâmina 3 (2)", "Padrão Lâmina 3 (3)" })
			{
				dropdown_DROPDOWN_PADRAO_LAMINA_3.options.Add(new Dropdown.OptionData
				{
					text = item3
				});
			}
			VOID_DROPDOWN_PADRAO_LAMINA_3(dropdown_DROPDOWN_PADRAO_LAMINA_3);
			dropdown_DROPDOWN_PADRAO_LAMINA_3.onValueChanged.AddListener(delegate
			{
				VOID_DROPDOWN_PADRAO_LAMINA_3(dropdown_DROPDOWN_PADRAO_LAMINA_3);
			});
		}
		if (DROPDOWN_COR_DO_BLOCO)
		{
			Dropdown dropdown_DROPDOWN_COR_DO_BLOCO = base.transform.GetComponent<Dropdown>();
			dropdown_DROPDOWN_COR_DO_BLOCO.options.Clear();
			foreach (string item4 in new List<string> { "Bloco (Preto)", "Bloco (Vermelho)", "Bloco (Azul)" })
			{
				dropdown_DROPDOWN_COR_DO_BLOCO.options.Add(new Dropdown.OptionData
				{
					text = item4
				});
			}
			VOID_DROPDOWN_COR_DO_BLOCO(dropdown_DROPDOWN_COR_DO_BLOCO);
			dropdown_DROPDOWN_COR_DO_BLOCO.onValueChanged.AddListener(delegate
			{
				VOID_DROPDOWN_COR_DO_BLOCO(dropdown_DROPDOWN_COR_DO_BLOCO);
			});
		}
		if (DROPDOWN_COR_DA_LAMINA_1)
		{
			Dropdown dropdown_DROPDOWN_COR_DA_LAMINA_1 = base.transform.GetComponent<Dropdown>();
			dropdown_DROPDOWN_COR_DA_LAMINA_1.options.Clear();
			foreach (string item5 in new List<string> { "Lâmina 1 (Vermelha)", "Lâmina 1 (Azul)", "Lâmina 1 (Amarela)", "Lâmina 1 (Verde)", "Lâmina 1 (Preta)", "Lâmina 1 (Branca)" })
			{
				dropdown_DROPDOWN_COR_DA_LAMINA_1.options.Add(new Dropdown.OptionData
				{
					text = item5
				});
			}
			VOID_DROPDOWN_COR_DA_LAMINA_1(dropdown_DROPDOWN_COR_DA_LAMINA_1);
			dropdown_DROPDOWN_COR_DA_LAMINA_1.onValueChanged.AddListener(delegate
			{
				VOID_DROPDOWN_COR_DA_LAMINA_1(dropdown_DROPDOWN_COR_DA_LAMINA_1);
			});
		}
		if (DROPDOWN_COR_DA_LAMINA_2)
		{
			Dropdown dropdown_DROPDOWN_COR_DA_LAMINA_2 = base.transform.GetComponent<Dropdown>();
			dropdown_DROPDOWN_COR_DA_LAMINA_2.options.Clear();
			foreach (string item6 in new List<string> { "Lâmina 2 (Vermelha)", "Lâmina 2 (Azul)", "Lâmina 2 (Amarela)", "Lâmina 2 (Verde)", "Lâmina 2 (Preta)", "Lâmina 2 (Branca)" })
			{
				dropdown_DROPDOWN_COR_DA_LAMINA_2.options.Add(new Dropdown.OptionData
				{
					text = item6
				});
			}
			VOID_DROPDOWN_COR_DA_LAMINA_2(dropdown_DROPDOWN_COR_DA_LAMINA_2);
			dropdown_DROPDOWN_COR_DA_LAMINA_2.onValueChanged.AddListener(delegate
			{
				VOID_DROPDOWN_COR_DA_LAMINA_2(dropdown_DROPDOWN_COR_DA_LAMINA_2);
			});
		}
		if (DROPDOWN_COR_DA_LAMINA_3)
		{
			Dropdown dropdown_DROPDOWN_COR_DA_LAMINA_3 = base.transform.GetComponent<Dropdown>();
			dropdown_DROPDOWN_COR_DA_LAMINA_3.options.Clear();
			foreach (string item7 in new List<string> { "Lâmina 3 (Vermelha)", "Lâmina 3 (Azul)", "Lâmina 3 (Amarela)", "Lâmina 3 (Verde)", "Lâmina 3 (Preta)", "Lâmina 3 (Branca)" })
			{
				dropdown_DROPDOWN_COR_DA_LAMINA_3.options.Add(new Dropdown.OptionData
				{
					text = item7
				});
			}
			VOID_DROPDOWN_COR_DA_LAMINA_3(dropdown_DROPDOWN_COR_DA_LAMINA_3);
			dropdown_DROPDOWN_COR_DA_LAMINA_3.onValueChanged.AddListener(delegate
			{
				VOID_DROPDOWN_COR_DA_LAMINA_3(dropdown_DROPDOWN_COR_DA_LAMINA_3);
			});
		}
		if (DROPDOWN_ANDAR_DO_BLOCO_DESEJADO)
		{
			Dropdown dropdown_DROPDOWN_ANDAR_DO_BLOCO_DESEJADO = base.transform.GetComponent<Dropdown>();
			dropdown_DROPDOWN_ANDAR_DO_BLOCO_DESEJADO.options.Clear();
			foreach (string item8 in new List<string> { "Andar desejado (1)", "Andar desejado (2)", "Andar desejado (3)" })
			{
				dropdown_DROPDOWN_ANDAR_DO_BLOCO_DESEJADO.options.Add(new Dropdown.OptionData
				{
					text = item8
				});
			}
			VOID_DROPDOWN_ANDAR_DO_BLOCO_DESEJADO(dropdown_DROPDOWN_ANDAR_DO_BLOCO_DESEJADO);
			dropdown_DROPDOWN_ANDAR_DO_BLOCO_DESEJADO.onValueChanged.AddListener(delegate
			{
				VOID_DROPDOWN_ANDAR_DO_BLOCO_DESEJADO(dropdown_DROPDOWN_ANDAR_DO_BLOCO_DESEJADO);
			});
		}
		if (DROPDOWN_TOTAL_DE_ANDARES_DESEJADO)
		{
			Dropdown dropdown_DROPDOWN_TOTAL_DE_ANDARES_DESEJADO = base.transform.GetComponent<Dropdown>();
			dropdown_DROPDOWN_TOTAL_DE_ANDARES_DESEJADO.options.Clear();
			foreach (string item9 in new List<string> { "Total de andares (1)", "Total de andares (2)", "Total de andares (3)" })
			{
				dropdown_DROPDOWN_TOTAL_DE_ANDARES_DESEJADO.options.Add(new Dropdown.OptionData
				{
					text = item9
				});
			}
			VOID_DROPDOWN_TOTAL_DE_ANDARES_DESEJADO(dropdown_DROPDOWN_TOTAL_DE_ANDARES_DESEJADO);
			dropdown_DROPDOWN_TOTAL_DE_ANDARES_DESEJADO.onValueChanged.AddListener(delegate
			{
				VOID_DROPDOWN_TOTAL_DE_ANDARES_DESEJADO(dropdown_DROPDOWN_TOTAL_DE_ANDARES_DESEJADO);
			});
		}
		if (DROPDOWN_NUMERO_OPERACAO_DESEJADO)
		{
			Dropdown dropdown_DROPDOWN_NUMERO_OPERACAO_DESEJADO = base.transform.GetComponent<Dropdown>();
			dropdown_DROPDOWN_NUMERO_OPERACAO_DESEJADO.options.Clear();
			foreach (string item10 in new List<string> { "Operação (Estoque)", "Operação (Processo)", "Operação (Montagem)", "Operação (Expedição)" })
			{
				dropdown_DROPDOWN_NUMERO_OPERACAO_DESEJADO.options.Add(new Dropdown.OptionData
				{
					text = item10
				});
			}
			VOID_DROPDOWN_NUMERO_OPERACAO_DESEJADO(dropdown_DROPDOWN_NUMERO_OPERACAO_DESEJADO);
			dropdown_DROPDOWN_NUMERO_OPERACAO_DESEJADO.onValueChanged.AddListener(delegate
			{
				VOID_DROPDOWN_NUMERO_OPERACAO_DESEJADO(dropdown_DROPDOWN_NUMERO_OPERACAO_DESEJADO);
			});
		}
		if (!DROPDOWN_LOCAL_INSTANCIAR)
		{
			return;
		}
		Dropdown dropdown_DROPDOWN_LOCAL_INSTANCIAR = base.transform.GetComponent<Dropdown>();
		dropdown_DROPDOWN_LOCAL_INSTANCIAR.options.Clear();
		foreach (string item11 in new List<string>
		{
			"Local: Esteira [1]", "Local: Esteira [2]", "Local: Estoque [1]", "Local: Estoque [2]", "Local: Estoque [3]", "Local: Estoque [4]", "Local: Estoque [5]", "Local: Estoque [6]", "Local: Estoque [7]", "Local: Estoque [8]",
			"Local: Estoque [9]", "Local: Estoque [10]", "Local: Estoque [11]", "Local: Estoque [12]", "Local: Estoque [13]", "Local: Estoque [14]", "Local: Estoque [15]", "Local: Estoque [16]", "Local: Estoque [17]", "Local: Estoque [18]",
			"Local: Estoque [19]", "Local: Estoque [20]", "Local: Estoque [21]", "Local: Estoque [22]", "Local: Estoque [23]", "Local: Estoque [24]", "Local: Estoque [25]", "Local: Estoque [26]", "Local: Estoque [27]", "Local: Estoque [28]"
		})
		{
			dropdown_DROPDOWN_LOCAL_INSTANCIAR.options.Add(new Dropdown.OptionData
			{
				text = item11
			});
		}
		VOID_DROPDOWN_LOCAL_INSTANCIAR(dropdown_DROPDOWN_LOCAL_INSTANCIAR);
		dropdown_DROPDOWN_LOCAL_INSTANCIAR.onValueChanged.AddListener(delegate
		{
			VOID_DROPDOWN_LOCAL_INSTANCIAR(dropdown_DROPDOWN_LOCAL_INSTANCIAR);
		});
	}

	private void VOID_DROPDOWN_COR_DO_BLOCO(Dropdown dropdown_DROPDOWN_COR_DO_BLOCO)
	{
		if (DROPDOWN_COR_DO_BLOCO)
		{
			int value = dropdown_DROPDOWN_COR_DO_BLOCO.value;
			ESCOLHIDO_COR_DO_BLOCO = dropdown_DROPDOWN_COR_DO_BLOCO.options[value].text;
		}
	}

	private void VOID_DROPDOWN_COR_DA_LAMINA_1(Dropdown dropdown_DROPDOWN_COR_DA_LAMINA_1)
	{
		if (DROPDOWN_COR_DA_LAMINA_1)
		{
			int value = dropdown_DROPDOWN_COR_DA_LAMINA_1.value;
			ESCOLHIDO_COR_DA_LAMINA_1 = dropdown_DROPDOWN_COR_DA_LAMINA_1.options[value].text;
		}
	}

	private void VOID_DROPDOWN_COR_DA_LAMINA_2(Dropdown dropdown_DROPDOWN_COR_DA_LAMINA_2)
	{
		if (DROPDOWN_COR_DA_LAMINA_2)
		{
			int value = dropdown_DROPDOWN_COR_DA_LAMINA_2.value;
			ESCOLHIDO_COR_DA_LAMINA_2 = dropdown_DROPDOWN_COR_DA_LAMINA_2.options[value].text;
		}
	}

	private void VOID_DROPDOWN_COR_DA_LAMINA_3(Dropdown dropdown_DROPDOWN_COR_DA_LAMINA_3)
	{
		if (DROPDOWN_COR_DA_LAMINA_3)
		{
			int value = dropdown_DROPDOWN_COR_DA_LAMINA_3.value;
			ESCOLHIDO_COR_DA_LAMINA_3 = dropdown_DROPDOWN_COR_DA_LAMINA_3.options[value].text;
		}
	}

	private void VOID_DROPDOWN_ANDAR_DO_BLOCO_DESEJADO(Dropdown dropdown_DROPDOWN_ANDAR_DO_BLOCO_DESEJADO)
	{
		if (DROPDOWN_ANDAR_DO_BLOCO_DESEJADO)
		{
			int value = dropdown_DROPDOWN_ANDAR_DO_BLOCO_DESEJADO.value;
			ESCOLHIDO_ANDAR_DO_BLOCO_DESEJADO = dropdown_DROPDOWN_ANDAR_DO_BLOCO_DESEJADO.options[value].text;
		}
	}

	private void VOID_DROPDOWN_TOTAL_DE_ANDARES_DESEJADO(Dropdown dropdown_DROPDOWN_TOTAL_DE_ANDARES_DESEJADO)
	{
		if (DROPDOWN_TOTAL_DE_ANDARES_DESEJADO)
		{
			int value = dropdown_DROPDOWN_TOTAL_DE_ANDARES_DESEJADO.value;
			ESCOLHIDO_TOTAL_DE_ANDARES_DESEJADO = dropdown_DROPDOWN_TOTAL_DE_ANDARES_DESEJADO.options[value].text;
		}
	}

	private void VOID_DROPDOWN_NUMERO_OPERACAO_DESEJADO(Dropdown dropdown_DROPDOWN_NUMERO_OPERACAO_DESEJADO)
	{
		if (DROPDOWN_NUMERO_OPERACAO_DESEJADO)
		{
			int value = dropdown_DROPDOWN_NUMERO_OPERACAO_DESEJADO.value;
			ESCOLHIDO_NUMERO_OPERACAO_DESEJADO = dropdown_DROPDOWN_NUMERO_OPERACAO_DESEJADO.options[value].text;
		}
	}

	private void VOID_DROPDOWN_LOCAL_INSTANCIAR(Dropdown dropdown_DROPDOWN_LOCAL_INSTANCIAR)
	{
		if (DROPDOWN_LOCAL_INSTANCIAR)
		{
			int value = dropdown_DROPDOWN_LOCAL_INSTANCIAR.value;
			ESCOLHIDO_DROPDOWN_LOCAL_INSTANCIAR = dropdown_DROPDOWN_LOCAL_INSTANCIAR.options[value].text;
		}
	}

	private void VOID_DROPDOWN_PADRAO_LAMINA_1(Dropdown dropdown_DROPDOWN_PADRAO_LAMINA_1)
	{
		if (DROPDOWN_PADRAO_LAMINA_1)
		{
			int value = dropdown_DROPDOWN_PADRAO_LAMINA_1.value;
			ESCOLHIDO_PADRAO_LAMINA_1 = dropdown_DROPDOWN_PADRAO_LAMINA_1.options[value].text;
		}
	}

	private void VOID_DROPDOWN_PADRAO_LAMINA_2(Dropdown dropdown_DROPDOWN_PADRAO_LAMINA_2)
	{
		if (DROPDOWN_PADRAO_LAMINA_2)
		{
			int value = dropdown_DROPDOWN_PADRAO_LAMINA_2.value;
			ESCOLHIDO_PADRAO_LAMINA_2 = dropdown_DROPDOWN_PADRAO_LAMINA_2.options[value].text;
		}
	}

	private void VOID_DROPDOWN_PADRAO_LAMINA_3(Dropdown dropdown_DROPDOWN_PADRAO_LAMINA_3)
	{
		if (DROPDOWN_PADRAO_LAMINA_3)
		{
			int value = dropdown_DROPDOWN_PADRAO_LAMINA_3.value;
			ESCOLHIDO_PADRAO_LAMINA_3 = dropdown_DROPDOWN_PADRAO_LAMINA_3.options[value].text;
		}
	}

	private void Update()
	{
		if (DROPDOWN_NUMERO_DO_PEDIDO_DESEJADO)
		{
			ESCOLHIDO_NUMERO_DO_PEDIDO_DESEJADO = TEXTFIELD_NUMERO_DO_PEDIDO_DESEJADO.GetComponent<InputField>().text;
		}
	}
}
