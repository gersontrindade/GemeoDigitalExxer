using System;
using System.IO;
using System.Windows.Forms;

namespace NetToPLCSim;

internal class CmdLineArgs
{
	private bool m_ArgsGiven;

	private string m_StartIni;

	private eAutoStopService m_AutoStopService;

	private eAutoStart m_AutoStart;

	public bool ArgsGiven => m_ArgsGiven;

	public eAutoStopService AutoStopService => m_AutoStopService;

	public eAutoStart AutoStart => m_AutoStart;

	public string StartIni => m_StartIni;

	public CmdLineArgs()
	{
		setDefaults();
	}

	private void setDefaults()
	{
		m_ArgsGiven = false;
		m_StartIni = string.Empty;
		m_AutoStopService = eAutoStopService.ASK;
		m_AutoStart = eAutoStart.NO;
	}

	public void parseCmdLineArgs(string[] args)
	{
		int num = 0;
		foreach (string text in args)
		{
			num++;
			if (num == 1)
			{
				continue;
			}
			switch (text)
			{
			case "-help":
			case "--help":
			case "-?":
				MessageBox.Show(getHelpText(), "Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				continue;
			}
			if (text.StartsWith("-s="))
			{
				string text2 = text.Substring(3, text.Length - 3).ToUpper();
				if (text2 == "YES")
				{
					m_AutoStopService = eAutoStopService.YES;
				}
				else if (text2 == "NO")
				{
					m_AutoStopService = eAutoStopService.NO;
				}
				continue;
			}
			if (text == "-autostart")
			{
				m_AutoStart = eAutoStart.YES;
				continue;
			}
			if (!text.StartsWith("-f=") && num != 2)
			{
				continue;
			}
			if (text.StartsWith("-f="))
			{
				string text2 = text.Substring(3, text.Length - 3);
				m_StartIni = text2;
			}
			else if (num == 2)
			{
				string text2 = text.ToUpper();
				if (text2.EndsWith(".INI"))
				{
					m_StartIni = text;
				}
			}
			if (!m_StartIni.StartsWith(Environment.CurrentDirectory))
			{
				m_StartIni = Environment.CurrentDirectory + "\\" + m_StartIni;
			}
			if (!File.Exists(m_StartIni))
			{
				m_StartIni = string.Empty;
			}
		}
		if (m_StartIni == string.Empty)
		{
			m_AutoStart = eAutoStart.NO;
		}
		m_AutoStopService = eAutoStopService.YES;
		m_AutoStart = eAutoStart.YES;
		m_ArgsGiven = true;
	}

	public string getHelpText()
	{
		return "NetToPLCSim - A network interface to Plcsim." + Environment.NewLine + Environment.NewLine + "Command line options:" + Environment.NewLine + Environment.NewLine + "NetToPLCSim.exe [configuration.ini] [-f=configuration.ini] [-s=Option] [-autostart]" + Environment.NewLine + "Options:" + Environment.NewLine + "-f=configuration.ini\tStart with this station configuration" + Environment.NewLine + "-s=Option\t\tAutostop IEPG-Helper service" + Environment.NewLine + "\t\tOptions: NO, YES, ASK" + Environment.NewLine + "-autostart\t\tAutostart with configuration file";
	}
}
