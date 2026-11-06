using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Modbus;

namespace ComunicationInterface;

public class ModbusMasterCommunication : ICommunication
{
	private ModbusMaster client;

	private CancellationTokenSource cancellationSource;

	private CancellationToken cancellationToken;

	private Task atualizacaoTask;

	private Stopwatch stopwatch;

	public bool Conectado { get; private set; }

	public bool Heartbeat { get; private set; }

	public ushort TamanhoBuffer { get; set; } = 100;

	public List<Tag> TagsLidas { get; set; }

	public List<Tag> TagsEscrever { get; set; }

	public ModbusMasterCommunication(string ip, string password)
	{
		new Validation(password);
		client = new ModbusMasterTCP(ip, 502);
	}

	public void AtualizarDados(bool offset, int offsetSaidas, int offsetEntradas)
	{
		EscreverTags(offset, offsetSaidas);
		LerTags(offset, offsetEntradas);
	}

	public void Conectar()
	{
		client.Connect();
		stopwatch = new Stopwatch();
		stopwatch.Start();
		cancellationSource = new CancellationTokenSource();
		cancellationToken = cancellationSource.Token;
		atualizacaoTask = Task.Factory.StartNew((Func<Task>)StatusComunicacao, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
	}

	public void Desconectar()
	{
		cancellationSource?.Cancel();
		atualizacaoTask?.Wait();
		client?.Disconnect();
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
			ushort[] values = (from t in TagsEscrever
				where t.Offset == valorOffset
				orderby t.Endereco
				select t.Valor).ToArray();
			client.WriteMultipleRegisters(1, 0, values);
		}
		else
		{
			foreach (List<Tag> item in (from t in TagsEscrever
				group t by t.Offset into t
				select t.ToList()).ToList())
			{
				ushort[] values2 = (from t in item
					orderby t.Endereco
					select t.Valor).ToArray();
				client.WriteMultipleRegisters(1, (ushort)item.First().Offset, values2);
			}
		}
		ushort[] values3 = ((!Heartbeat) ? new ushort[1] : new ushort[1] { 1 });
		client.WriteMultipleRegisters(1, 900, values3);
	}

	private void LerTags(bool offset, int valorOffset)
	{
		ushort[] array = client.ReadHoldingRegisters(1, 0, TamanhoBuffer);
		foreach (Tag tagsLida in TagsLidas)
		{
			if (offset)
			{
				if (tagsLida.Offset == valorOffset)
				{
					tagsLida.Valor = (ushort)((array != null) ? array[tagsLida.Endereco + tagsLida.OffsetDB] : 0);
				}
			}
			else
			{
				tagsLida.Valor = (ushort)((array != null) ? array[tagsLida.Endereco + tagsLida.Offset] : 0);
			}
		}
	}
}
