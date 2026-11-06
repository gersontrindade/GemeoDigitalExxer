using System;

namespace Comunication.NamedPipe;

internal class Program
{
	private static void Main(string[] args)
	{
		PipeClient pipeClient = new PipeClient();
		pipeClient.Start();
		Console.WriteLine(pipeClient.serverPort);
		Console.ReadKey();
		pipeClient.End();
	}
}
