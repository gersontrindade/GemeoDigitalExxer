using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using IsoOnTcp;
using PROFINET_STEP_7.Profinet;

namespace NetToPLCSim;

public class Principal
{
	private List<IsoToS7online> m_servers = new List<IsoToS7online>();

	private ExceptionCode errCode;

	private string statusConnect = "";

	private string m_Process = "";

	private bool m_S7DOSServiceStopped;

	private string m_IEPGhelperServiceName = string.Empty;

	private CmdLineArgs StartArgs = new CmdLineArgs();

	private StationData station = new StationData();

	private PLC plc;

	public PLC Plc
	{
		get
		{
			return plc;
		}
		set
		{
			plc = value;
		}
	}

	public StationData Station
	{
		get
		{
			return station;
		}
		set
		{
			station = value;
		}
	}

	public string StatusConnect
	{
		get
		{
			return statusConnect;
		}
		set
		{
			statusConnect = value;
		}
	}

	public string ProcessPLCSim
	{
		get
		{
			return m_Process;
		}
		set
		{
			m_Process = value;
		}
	}

	public Principal(string password)
	{
		if (password != ",K*@xSEx&SFz7Asb")
		{
			throw new Exception("Acesso negado");
		}
		m_IEPGhelperServiceName = Tools.GetS7DOSHelperServiceName();
	}

	public bool CheckPort102()
	{
		StartArgs.parseCmdLineArgs(Environment.GetCommandLineArgs());
		if (!Tools.IsTcpPortAvailable(102))
		{
			StatusConnect = "Porta 102 não disponível!";
			if (!string.IsNullOrEmpty(m_IEPGhelperServiceName))
			{
				if (StartArgs.AutoStopService == eAutoStopService.ASK)
				{
					m_S7DOSServiceStopped = GetPort102back(autoclose: false);
				}
				else if (StartArgs.AutoStopService == eAutoStopService.YES)
				{
					m_S7DOSServiceStopped = GetPort102back(autoclose: true);
				}
				if (Tools.IsTcpPortAvailable(102))
				{
					StatusConnect = "Porta 102 OK";
				}
			}
		}
		else
		{
			StatusConnect = "Porta 102 OK";
		}
		return Tools.IsTcpPortAvailable(102);
	}

	public void StartCon()
	{
		KillPLCSim();
		List<byte[]> list = new List<byte[]>();
		byte b = (byte)((station.PlcsimRackNumber << 4) | station.PlcsimSlotNumber);
		list.Add(new byte[2] { 1, b });
		list.Add(new byte[2] { 2, b });
		list.Add(new byte[2] { 3, b });
		IsoToS7online isoToS7online = new IsoToS7online(station.TsapCheckEnabled);
		m_servers.Add(isoToS7online);
		try
		{
			isoToS7online.start(station.Name, station.NetworkIpAddress, list, station.PlcsimIpAddress, station.PlcsimRackNumber, station.PlcsimSlotNumber);
		}
		catch
		{
			station.Connected = false;
			station.Status = StationStatus.ERROR.ToString();
			throw new Exception("Error ao iniciar o servidor para a conexão do PLC");
		}
		station.Connected = true;
		station.Status = StationStatus.RUNNING.ToString();
	}

	private bool GetPort102back(bool autoclose)
	{
		GetPort102 getPort = new GetPort102();
		getPort.StartGetPortBack();
		getPort.AutoCloseOnSuccess = autoclose;
		if (Tools.IsTcpPortAvailable(102))
		{
			StatusConnect = "Porta 102 OK";
		}
		else
		{
			StatusConnect = "Porta 102 não disponível!";
		}
		return getPort.Success;
	}

	public void StopCon()
	{
		StopServer();
		if (m_S7DOSServiceStopped)
		{
			Tools.StartService(m_IEPGhelperServiceName, 20000, dontShowMessageBoxes: false, restartIfRunning: true);
			m_S7DOSServiceStopped = false;
		}
		KillPLCSim();
	}

	private void KillPLCSim()
	{
		try
		{
			foreach (Process item in from x in Process.GetProcesses(".")
				where x.ProcessName.Contains(ProcessPLCSim)
				select x)
			{
				item.Kill();
			}
		}
		catch
		{
		}
	}

	private void StopServer()
	{
		foreach (IsoToS7online server in m_servers)
		{
			server.stop();
		}
		m_servers.Clear();
		station.Status = StationStatus.STOPPED.ToString();
	}

	public bool ConnectToPlc()
	{
		try
		{
			IPAddress iPAddress = Station.PlcsimIpAddress;
			if (Station.Connected)
			{
				iPAddress = Station.NetworkIpAddress;
			}
			Plc = new PLC(CPU_Type.S71200, iPAddress.ToString(), (short)Station.PlcsimRackNumber, (short)Station.PlcsimSlotNumber);
			if (!Plc.IsAvailable)
			{
				throw new Exception("O PLC a ser conectado não pode ser encontrado!");
			}
			errCode = Plc.Open();
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
		return Plc.IsConnected;
	}
}
