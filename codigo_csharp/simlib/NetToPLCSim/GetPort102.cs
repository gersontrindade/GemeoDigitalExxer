using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NetToPLCSim;

internal class GetPort102
{
	private string m_Servicename;

	private TcpListener m_Listener;

	public bool Success { get; private set; }

	public bool AutoCloseOnSuccess { get; set; }

	public GetPort102()
	{
		m_Servicename = Tools.GetS7DOSHelperServiceName();
	}

	private bool Step1()
	{
		return Tools.StopService(m_Servicename, 20000, dontShowMessageBoxes: true);
	}

	private bool Step2()
	{
		try
		{
			m_Listener = new TcpListener(IPAddress.Any, 102);
			m_Listener.Start();
		}
		catch
		{
			return false;
		}
		return true;
	}

	private bool Step3()
	{
		return Tools.StartService(m_Servicename, 20000, dontShowMessageBoxes: true, restartIfRunning: false);
	}

	private bool Step4()
	{
		try
		{
			m_Listener.Stop();
			return true;
		}
		catch
		{
			return false;
		}
	}

	private bool Step5()
	{
		return Tools.IsTcpPortAvailable(102);
	}

	public void StartGetPortBack()
	{
		Success = false;
		Thread.Sleep(50);
		Thread.Sleep(50);
		if (Step1())
		{
			Thread.Sleep(50);
			if (!Step2())
			{
				SystemSounds.Exclamation.Play();
				return;
			}
			Thread.Sleep(50);
			Thread.Sleep(50);
			if (!Step3())
			{
				Step4();
				SystemSounds.Exclamation.Play();
				return;
			}
			Thread.Sleep(50);
			Thread.Sleep(50);
			Thread.Sleep(50);
			if (!Step4())
			{
				SystemSounds.Exclamation.Play();
				return;
			}
			Thread.Sleep(50);
			Thread.Sleep(50);
			if (Step5())
			{
				Success = true;
			}
			else
			{
				SystemSounds.Exclamation.Play();
			}
		}
		else
		{
			SystemSounds.Exclamation.Play();
		}
		if (Success)
		{
			_ = AutoCloseOnSuccess;
		}
	}
}
