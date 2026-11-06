using System;
using System.Net;

namespace Modbus;

public sealed class ModbusTCPUDPClientConnectedEventArgs : EventArgs
{
	private IPEndPoint remote_ep;

	public IPEndPoint RemoteEndPoint => remote_ep;

	public ModbusTCPUDPClientConnectedEventArgs(IPEndPoint remote_ep)
	{
		this.remote_ep = remote_ep;
	}
}
