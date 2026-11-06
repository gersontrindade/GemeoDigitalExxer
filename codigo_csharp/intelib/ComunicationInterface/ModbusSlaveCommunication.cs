using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Modbus;

namespace ComunicationInterface;

public class ModbusSlaveCommunication : ICommunication
{
	private Datastore[] datastores = new Datastore[1]
	{
		new Datastore(1)
	};

	private ModbusSlave server;

	private CancellationTokenSource cancellationSource;

	private CancellationToken cancellationToken;

	private Task atualizacaoTask;

	private Stopwatch stopwatch;

	public bool Conectado { get; private set; }

	public bool Heartbeat { get; private set; }

	public ushort TamanhoBuffer { get; set; }

	public List<Tag> TagsLidas { get; set; }

	public List<Tag> TagsEscrever { get; set; }

	public ModbusSlaveCommunication(string password)
	{
		new Validation(password);
		server = new ModbusSlaveTCP(datastores, IPAddress.Any, 502);
	}

	public void AtualizarDados(bool offset, int offsetSaidas, int offsetEntradas)
	{
		foreach (Tag item in TagsEscrever)
		{
			if (!offset)
			{
				datastores[0].HoldingRegisters[item.Offset + item.Endereco] = item.Valor;
			}
			else if (item.Offset == offsetSaidas)
			{
				datastores[0].HoldingRegisters[item.Endereco] = item.Valor;
			}
		}
		datastores[0].HoldingRegisters[900] = (ushort)(Heartbeat ? 1u : 0u);
		foreach (Tag tagsLida in TagsLidas)
		{
			if (!offset)
			{
				tagsLida.Valor = datastores[0].HoldingRegisters[tagsLida.Offset + tagsLida.Endereco];
			}
			else if (tagsLida.Offset == offsetEntradas)
			{
				tagsLida.Valor = datastores[0].HoldingRegisters[tagsLida.OffsetDB + tagsLida.Endereco];
			}
		}
	}

	public void Conectar()
	{
		server.StartListen();
		Conectado = true;
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
		server.StopListen();
		Conectado = false;
		stopwatch.Stop();
	}

	private async Task StatusComunicacao()
	{
		do
		{
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
	}
}
