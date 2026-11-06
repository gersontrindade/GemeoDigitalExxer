using System.Net.NetworkInformation;
using System.Net.Sockets;

public class IPManager
{
	public static string GetIP(ADDRESSFAM Addfam)
	{
		if (Addfam == ADDRESSFAM.IPv6 && !Socket.OSSupportsIPv6)
		{
			return null;
		}
		string result = "";
		NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
		foreach (NetworkInterface networkInterface in allNetworkInterfaces)
		{
			NetworkInterfaceType networkInterfaceType = NetworkInterfaceType.Wireless80211;
			NetworkInterfaceType networkInterfaceType2 = NetworkInterfaceType.Ethernet;
			if ((networkInterface.NetworkInterfaceType != networkInterfaceType && networkInterface.NetworkInterfaceType != networkInterfaceType2) || networkInterface.OperationalStatus != OperationalStatus.Up)
			{
				continue;
			}
			foreach (UnicastIPAddressInformation unicastAddress in networkInterface.GetIPProperties().UnicastAddresses)
			{
				switch (Addfam)
				{
				case ADDRESSFAM.IPv4:
					if (unicastAddress.Address.AddressFamily == AddressFamily.InterNetwork)
					{
						result = unicastAddress.Address.ToString();
					}
					break;
				case ADDRESSFAM.IPv6:
					if (unicastAddress.Address.AddressFamily == AddressFamily.InterNetworkV6)
					{
						result = unicastAddress.Address.ToString();
					}
					break;
				}
			}
		}
		return result;
	}
}
