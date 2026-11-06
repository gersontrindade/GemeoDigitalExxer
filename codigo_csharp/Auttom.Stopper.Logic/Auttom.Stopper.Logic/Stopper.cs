using System.Collections;

namespace Auttom.Stopper.Logic;

public class Stopper
{
	private int PedidoProcesso;

	private readonly int Ocupada = 15;

	private readonly int Emergencia = 14;

	private readonly int Esperando = 11;

	private readonly int ProntoColocar = 10;

	public Bancada BancadaDefinida { get; private set; }

	public bool ValvulaChegada { get; private set; }

	public bool ValvulaSaida { get; private set; }

	public bool ProntoReceber { get; private set; }

	public int ProximaOperacao { get; private set; }

	public Stopper(Bancada bancadaDefinida)
	{
		BancadaDefinida = bancadaDefinida;
		ValvulaChegada = false;
		ValvulaSaida = true;
		ProntoReceber = false;
	}

	public bool Atualizar(int statusBancada, bool temBloco, int statusOperacoes, int numeroPedido, bool valvulaChegadaLivre = true)
	{
		BitArray bitArray = new BitArray(new int[1] { statusBancada });
		ProximaOperacao = statusOperacoes;
		if (bitArray[Emergencia] || (bitArray[Ocupada] && !bitArray[Esperando] && !bitArray[ProntoColocar]))
		{
			if (temBloco)
			{
				ValvulaChegada = true;
				ValvulaSaida = false;
			}
			else
			{
				ValvulaChegada = false;
				ValvulaSaida = true;
			}
			return false;
		}
		if (bitArray[ProntoColocar] && bitArray[Ocupada])
		{
			if (valvulaChegadaLivre)
			{
				ValvulaChegada = true;
			}
			if (ValvulaChegada && ValvulaSaida && temBloco)
			{
				ProximaOperacao = (int)BancadaDefinida;
				ValvulaSaida = false;
			}
			else if (ValvulaChegada && !temBloco)
			{
				ValvulaSaida = true;
				ProntoReceber = true;
			}
			else
			{
				ValvulaSaida = false;
			}
		}
		else if (temBloco)
		{
			ProntoReceber = false;
			if (BancadaDefinida == (Bancada)(statusOperacoes + 1))
			{
				if (!bitArray[Esperando] || BancadaDefinida != Bancada.Empilhador)
				{
					ValvulaChegada = true;
					ValvulaSaida = true;
					PedidoProcesso = numeroPedido;
					return true;
				}
				if (PedidoProcesso == numeroPedido)
				{
					ValvulaChegada = true;
					ValvulaSaida = true;
					return true;
				}
				ValvulaChegada = true;
				ValvulaSaida = false;
			}
			else
			{
				ValvulaChegada = true;
				ValvulaSaida = false;
			}
		}
		else
		{
			ValvulaChegada = false;
			ValvulaSaida = true;
			ProntoReceber = false;
		}
		return false;
	}
}
