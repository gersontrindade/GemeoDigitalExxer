using System;

namespace ComunicationInterface;

internal class ConnectionException : ApplicationException
{
	public ConnectionException(string message)
		: base(message)
	{
	}
}
