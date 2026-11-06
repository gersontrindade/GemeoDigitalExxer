using System.IO.Pipes;

namespace Comunication.NamedPipe;

public class PipeClient
{
	private StreamString ss;

	private NamedPipeClientStream pipeClient;

	public int serverPort { get; private set; }

	public void Start()
	{
		pipeClient = new NamedPipeClientStream(".", "PLCpipe", PipeDirection.InOut, PipeOptions.None);
		pipeClient.Connect();
		ss = new StreamString(pipeClient);
		if (ss.ReadString() == "I am the one true server!")
		{
			ss.WriteString("port");
			serverPort = int.Parse(ss.ReadString());
		}
	}

	public void End()
	{
		if (ss.ReadString() == "I am the one true server!")
		{
			ss.WriteString("exit");
		}
		pipeClient.Close();
	}
}
