using TcpLib;

namespace IsoOnTcp;

public class QueueEntry
{
	public IsoServiceProvider isoclient;

	public ConnectionState client;

	public byte[] message;
}
