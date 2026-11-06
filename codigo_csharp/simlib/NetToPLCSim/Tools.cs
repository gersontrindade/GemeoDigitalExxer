using System;
using System.Net;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Windows.Forms;

namespace NetToPLCSim;

public class Tools
{
	public static bool StopService(string serviceName, int timeoutMilliseconds, bool dontShowMessageBoxes)
	{
		ServiceController serviceController = new ServiceController(serviceName);
		bool flag = false;
		do
		{
			try
			{
				TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
				if (!flag)
				{
					serviceController.Stop();
				}
				serviceController.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
				if (!dontShowMessageBoxes)
				{
					MessageBox.Show("O serviço '" + serviceName + "' foi interrompido com sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				flag = false;
			}
			catch (System.ServiceProcess.TimeoutException)
			{
				flag = MessageBox.Show("Ocorreu um tempo limite interrompendo o serviço '" + serviceName + "'.\nVocê gostaria de tentar de novo?", "Erro", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry;
			}
			catch (Exception ex2)
			{
				if (!dontShowMessageBoxes)
				{
					MessageBox.Show("Não foi possível interromper o serviço '" + serviceName + "'\n" + ex2.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				flag = false;
			}
			serviceController.Refresh();
			if (serviceController.Status == ServiceControllerStatus.Stopped)
			{
				flag = false;
			}
		}
		while (flag);
		return serviceController.Status == ServiceControllerStatus.Stopped;
	}

	public static bool StartService(string serviceName, int timeoutMilliseconds, bool dontShowMessageBoxes, bool restartIfRunning)
	{
		ServiceController serviceController = new ServiceController(serviceName);
		bool flag = false;
		if (restartIfRunning && serviceController.Status == ServiceControllerStatus.Running)
		{
			do
			{
				try
				{
					TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
					if (!flag)
					{
						serviceController.Stop();
					}
					serviceController.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
					flag = false;
				}
				catch (System.ServiceProcess.TimeoutException)
				{
					flag = MessageBox.Show("Ocorreu um tempo limite interrompendo o serviço '" + serviceName + "'.\nVocê gostaria de tentar de novo?", "Erro", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry;
				}
				catch (Exception ex2)
				{
					if (!dontShowMessageBoxes)
					{
						MessageBox.Show("Não é possível interromper o serviço '" + serviceName + "' antes sem ele estar iniciado.\n" + ex2.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					flag = false;
				}
				serviceController.Refresh();
				if (serviceController.Status == ServiceControllerStatus.Stopped)
				{
					flag = false;
				}
			}
			while (flag);
		}
		do
		{
			try
			{
				TimeSpan timeout2 = TimeSpan.FromMilliseconds(timeoutMilliseconds);
				if (!flag)
				{
					serviceController.Start();
				}
				serviceController.WaitForStatus(ServiceControllerStatus.Running, timeout2);
				flag = false;
			}
			catch (System.ServiceProcess.TimeoutException)
			{
				flag = MessageBox.Show("Ocorreu um tempo limite ao iniciar o serviço '" + serviceName + "'.\nVocê gostaria de tentar de novo?", "Erro", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry;
			}
			catch (Exception ex4)
			{
				if (!dontShowMessageBoxes)
				{
					MessageBox.Show("Não foi possível iniciar o serviço '" + serviceName + "'\n" + ex4.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				flag = false;
			}
			serviceController.Refresh();
			if (serviceController.Status == ServiceControllerStatus.Running)
			{
				flag = false;
			}
		}
		while (flag);
		return serviceController.Status == ServiceControllerStatus.Running;
	}

	public static string GetS7DOSHelperServiceName()
	{
		string machineName = ".";
		ServiceController[] array = null;
		try
		{
			array = ServiceController.GetServices(machineName);
		}
		catch
		{
			return string.Empty;
		}
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].ServiceName == "s7oiehsx")
			{
				return array[i].ServiceName;
			}
			if (array[i].ServiceName == "s7oiehsx64")
			{
				return array[i].ServiceName;
			}
		}
		return string.Empty;
	}

	public static bool IsTcpPortAvailable(int port)
	{
		bool result = true;
		IPEndPoint[] activeTcpListeners = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners();
		for (int i = 0; i < activeTcpListeners.Length; i++)
		{
			if (activeTcpListeners[i].Port == port)
			{
				result = false;
				break;
			}
		}
		return result;
	}
}
