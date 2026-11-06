using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using NetToPLCSim;
using PROFINET_STEP_7.Profinet;

namespace ComunicationInterface;

public class SiemensCommunication : ICommunication
{
	private Principal client;

	private CancellationTokenSource cancellationSource;

	private CancellationToken cancellationToken;

	private Task atualizacaoTask;

	public bool Conectado { get; private set; }

	public bool Heartbeat { get; private set; }

	public ushort TamanhoBuffer { get; set; }

	public List<Tag> TagsLidas { get; set; }

	public List<Tag> TagsEscrever { get; set; }

	public SiemensCommunication(string ip, bool plcSim, string password)
	{
		new Validation(password);
		PrepararClient(ip);
		if (plcSim)
		{
			if (!client.CheckPort102())
			{
				throw new ConnectionException("Não foi possível liberar a porta de comunicação");
			}
			client.StartCon();
		}
	}

	public void AtualizarDados(bool offset, int offsetSaidas, int offsetEntradas)
	{
		EscreverTags(offset, offsetSaidas);
		LerTags(offset, offsetEntradas);
	}

	public void Conectar()
	{
		if (client.ConnectToPlc())
		{
			cancellationSource = new CancellationTokenSource();
			cancellationToken = cancellationSource.Token;
			atualizacaoTask = Task.Factory.StartNew((Func<Task>)StatusComunicacao, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
			return;
		}
		throw new ConnectionException("Falha ao se conectar com o PLC");
	}

	public void Desconectar()
	{
		cancellationSource?.Cancel();
		atualizacaoTask?.Wait();
		if (client.Station.Connected)
		{
			if (client.Plc != null && client.Plc.IsConnected)
			{
				client.Plc.Close();
			}
			client.StopCon();
		}
	}

	private void PrepararClient(string ip)
	{
		client = new Principal(",K*@xSEx&SFz7Asb");
		client.ProcessPLCSim = "Siemens.Simatic.PlcSim";
		client.Station.Name = "PLC";
		client.Station.NetworkIpAddress = IPAddress.Parse("127.0.0.1");
		client.Station.PlcsimIpAddress = IPAddress.Parse(ip);
		client.Station.PlcsimRackNumber = 0;
		client.Station.PlcsimSlotNumber = 1;
		client.Station.TsapCheckEnabled = false;
	}

	private async Task StatusComunicacao()
	{
		do
		{
			if (client.Plc != null && client.Plc.IsConnected)
			{
				Conectado = true;
			}
			else
			{
				Conectado = false;
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
			ushort[] valores = (from t in TagsEscrever
				where t.Offset == valorOffset
				orderby t.Endereco
				select t.Valor).ToArray();
			valores = InverterBytes(valores);
			client.Plc.Write(DataType.Input, 0, 0, valores);
			return;
		}
		foreach (List<Tag> item in (from t in TagsEscrever
			group t by t.DB into t
			select t.ToList()).ToList())
		{
			ushort[] valores2 = (from t in item
				orderby t.Endereco
				select t.Valor).ToArray();
			valores2 = InverterBytes(valores2);
			client.Plc.Write(DataType.DataBlock, item.First().DB, item.First().OffsetDB * 2, valores2);
		}
	}

	private void LerTags(bool offset, int valorOffset)
	{
		if (offset)
		{
			int varCount = TagsLidas.Where((Tag t) => t.Offset == valorOffset).Count();
			ushort[] valores = (ushort[])client.Plc.Read(DataType.Output, 0, 0, VarType.Word, varCount);
			valores = InverterBytes(valores);
			{
				foreach (Tag tagsLida in TagsLidas)
				{
					if (tagsLida.Offset == valorOffset)
					{
						tagsLida.Valor = (ushort)((valores != null) ? valores[tagsLida.Endereco] : 0);
					}
				}
				return;
			}
		}
		foreach (List<Tag> item in (from t in TagsLidas
			group t by t.DB into t
			select t.ToList()).ToList())
		{
			ushort[] valores2 = (ushort[])client.Plc.Read(DataType.DataBlock, item.First().DB, item.First().OffsetDB * 2, VarType.Word, item.Count());
			valores2 = InverterBytes(valores2);
			foreach (Tag tagsLida2 in TagsLidas)
			{
				if (tagsLida2.Offset == item.First().Offset)
				{
					tagsLida2.Valor = (ushort)((valores2 != null) ? valores2[tagsLida2.Endereco] : 0);
				}
			}
		}
	}

	private ushort[] InverterBytes(ushort[] valores)
	{
		if (valores != null)
		{
			for (int i = 0; i < valores.Length; i++)
			{
				valores[i] = (ushort)(((valores[i] & 0xFF00) >>> 8) | ((valores[i] & 0xFF) << 8));
			}
		}
		return valores;
	}
}
