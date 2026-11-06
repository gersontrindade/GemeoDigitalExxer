using System;

namespace TcpLib;

public abstract class TcpServiceProvider : ICloneable
{
	public virtual object Clone()
	{
		throw new Exception("Derived clases must override Clone method.");
	}

	public abstract void OnAcceptConnection(ConnectionState state);

	public abstract void OnReceiveData(ConnectionState state);

	public abstract void OnDropConnection(ConnectionState state);
}
