using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sharp7;

namespace ComunicationInterface;

public class SiemensShap7Comunication : ICommunication
{
	private readonly int RACK;

	private readonly int SLOT = 1;

	private S7Client client;

	private string Ip;

	private int Port;

	private CancellationTokenSource cancellationSource;

	private CancellationToken cancellationToken;

	private Task atualizacaoTask;

	private Stopwatch stopwatch;

	public bool Conectado { get; private set; }

	public bool Heartbeat { get; private set; }

	public ushort TamanhoBuffer { get; set; }

	public List<Tag> TagsLidas { get; set; }

	public List<Tag> TagsEscrever { get; set; }

	public SiemensShap7Comunication(string ip, int port, string password)
	{
		new Validation(password);
		Ip = ip;
		Port = port;
		client = new S7Client();
	}

	public void AtualizarDados(bool offset, int offsetSaidas, int offsetEntradas)
	{
		EscreverTags(offset, offsetSaidas);
		LerTags(offset, offsetEntradas);
	}

	public void Conectar()
	{
		int num = client.ConnectTo(Ip, RACK, SLOT, Port);
		if (num == 0)
		{
			stopwatch = new Stopwatch();
			stopwatch.Start();
			cancellationSource = new CancellationTokenSource();
			cancellationToken = cancellationSource.Token;
			atualizacaoTask = Task.Factory.StartNew((Func<Task>)StatusComunicacao, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
			return;
		}
		throw new ConnectionException("Falha ao se conectar com o PLC, codigo erro " + num);
	}

	public void Desconectar()
	{
		cancellationSource?.Cancel();
		atualizacaoTask?.Wait();
		client.Disconnect();
		Conectado = false;
		stopwatch.Stop();
	}

	private async Task StatusComunicacao()
	{
		do
		{
			Conectado = client.Connected;
			if (stopwatch != null && stopwatch.ElapsedMilliseconds >= 500)
			{
				Heartbeat = !Heartbeat;
				stopwatch.Restart();
			}
			await Task.Run(delegate
			{
				Thread.Sleep(10);
			});
		}
		while (!cancellationToken.IsCancellationRequested);
		Conectado = false;
	}

	private void EscreverTags(bool offset, int valorOffset)
	{
		if (offset)
		{
			ushort[] array = (from t in TagsEscrever
				where t.Offset == valorOffset && t.Endereco <= 2
				orderby t.Endereco
				select t.Valor).ToArray();
			byte[] array2 = new byte[array.Length * 2];
			Buffer.BlockCopy(array, 0, array2, 0, array.Length * 2);
			client.WriteArea(129, 0, 0, array.Length, 4, array2);
			ushort[] array3 = (from t in TagsEscrever
				where t.Offset == valorOffset && t.Endereco > 2
				orderby t.Endereco
				select t.Valor).ToArray();
			array2 = new byte[array3.Length * 2];
			Buffer.BlockCopy(array3, 0, array2, 0, array3.Length * 2);
			client.WriteArea(131, 0, 100, array3.Length, 4, array2);
		}
		else
		{
			foreach (List<Tag> item in (from t in TagsEscrever
				group t by t.DB into t
				select t.ToList()).ToList())
			{
				ushort[] array4 = (from t in item
					orderby t.Endereco
					select t.Valor).ToArray();
				byte[] array5 = new byte[array4.Length * 2];
				Buffer.BlockCopy(array4, 0, array5, 0, array4.Length * 2);
				client.WriteArea(132, item.First().DB, item.First().OffsetDB * 2, array4.Length, 4, array5);
			}
		}
		byte[] buffer = ((!Heartbeat) ? new byte[1] : new byte[1] { 1 });
		client.WriteArea(131, 0, 900, 1, 2, buffer);
	}

	private void LerTags(bool offset, int valorOffset)
	{
		if (offset)
		{
			int num = TagsLidas.Where((Tag t) => t.Offset == valorOffset && t.Endereco <= 1).Count();
			byte[] array = new byte[num * 2];
			client.ReadArea(130, 0, 0, num, 4, array);
			ushort[] array2 = new ushort[num];
			Buffer.BlockCopy(array, 0, array2, 0, num * 2);
			num = TagsLidas.Where((Tag t) => t.Offset == valorOffset && t.Endereco > 1).Count();
			byte[] array3 = new byte[num * 2];
			client.ReadArea(131, 0, 200, num, 4, array3);
			ushort[] array4 = new ushort[num];
			Buffer.BlockCopy(array3, 0, array4, 0, num * 2);
			ushort[] array5 = new ushort[array2.Length + array4.Length];
			array2.CopyTo(array5, 0);
			array4.CopyTo(array5, array2.Length);
			{
				foreach (Tag tagsLida in TagsLidas)
				{
					if (tagsLida.Offset == valorOffset)
					{
						tagsLida.Valor = (ushort)((array5 != null) ? array5[tagsLida.Endereco] : 0);
					}
				}
				return;
			}
		}
		foreach (List<Tag> item in (from t in TagsLidas
			group t by t.DB into t
			select t.ToList()).ToList())
		{
			int num2 = item.Count();
			byte[] array6 = new byte[num2 * 2];
			client.ReadArea(132, item.First().DB, item.First().OffsetDB * 2, num2, 4, array6);
			ushort[] array7 = new ushort[num2];
			Buffer.BlockCopy(array6, 0, array7, 0, num2 * 2);
			foreach (Tag tagsLida2 in TagsLidas)
			{
				if (tagsLida2.Offset == item.First().Offset)
				{
					tagsLida2.Valor = (ushort)((array7 != null) ? array7[tagsLida2.Endereco] : 0);
				}
			}
		}
	}
}
