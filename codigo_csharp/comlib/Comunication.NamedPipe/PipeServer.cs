using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace Comunication.NamedPipe;

public class PipeServer
{
	private int numThreads = 1;

	private int serverPort;

	public PipeServer(int port)
	{
		serverPort = port;
	}

	public void Start()
	{
		Thread[] array = new Thread[numThreads];
		int i;
		for (i = 0; i < numThreads; i++)
		{
			array[i] = new Thread(ServerThread);
			array[i].Start();
		}
		Thread.Sleep(250);
		while (i > 0)
		{
			for (int j = 0; j < numThreads; j++)
			{
				if (array[j] != null && array[j].Join(250))
				{
					array[j] = null;
					i--;
				}
			}
		}
	}

	private void ServerThread(object data)
	{
		NamedPipeServerStream namedPipeServerStream = new NamedPipeServerStream("PLCpipe", PipeDirection.InOut, numThreads);
		namedPipeServerStream.WaitForConnection();
		try
		{
			string text;
			do
			{
				StreamString streamString = new StreamString(namedPipeServerStream);
				streamString.WriteString("I am the one true server!");
				text = streamString.ReadString();
				if (text == "port")
				{
					streamString.WriteString(serverPort.ToString());
				}
			}
			while (text != "exit");
		}
		catch (IOException ex)
		{
			throw ex;
		}
		namedPipeServerStream.Close();
	}
}
