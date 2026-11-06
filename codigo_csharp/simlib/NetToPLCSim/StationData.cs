using System.ComponentModel;
using System.Net;

namespace NetToPLCSim;

public class StationData : INotifyPropertyChanged
{
	private string m_Name;

	private IPAddress m_NetworkIpAddress;

	private IPAddress m_PlcsimIpAddress;

	private int m_PlcsimRackNumber;

	private int m_PlcsimSlotNumber;

	private bool m_TsapCheckEnabled;

	private bool m_Connected;

	private string m_Status;

	public string Name
	{
		get
		{
			return m_Name;
		}
		set
		{
			m_Name = value;
			NotifyPropertyChanged("Name");
		}
	}

	public IPAddress NetworkIpAddress
	{
		get
		{
			return m_NetworkIpAddress;
		}
		set
		{
			m_NetworkIpAddress = value;
			NotifyPropertyChanged("NetworkIpAddress");
		}
	}

	public IPAddress PlcsimIpAddress
	{
		get
		{
			return m_PlcsimIpAddress;
		}
		set
		{
			m_PlcsimIpAddress = value;
			NotifyPropertyChanged("PlcsimIpAddress");
		}
	}

	public int PlcsimRackNumber
	{
		get
		{
			return m_PlcsimRackNumber;
		}
		set
		{
			m_PlcsimRackNumber = value;
			NotifyPropertyChanged("PlcsimRackSlot");
		}
	}

	public int PlcsimSlotNumber
	{
		get
		{
			return m_PlcsimSlotNumber;
		}
		set
		{
			m_PlcsimSlotNumber = value;
			NotifyPropertyChanged("PlcsimRackSlot");
		}
	}

	public string PlcsimRackSlot => m_PlcsimRackNumber + "/" + m_PlcsimSlotNumber;

	public bool TsapCheckEnabled
	{
		get
		{
			return m_TsapCheckEnabled;
		}
		set
		{
			m_TsapCheckEnabled = value;
			NotifyPropertyChanged("TsapCheckEnabled");
		}
	}

	public bool Connected
	{
		get
		{
			return m_Connected;
		}
		set
		{
			m_Connected = value;
			NotifyPropertyChanged("Connected");
		}
	}

	public string Status
	{
		get
		{
			return m_Status;
		}
		set
		{
			m_Status = value;
			NotifyPropertyChanged("Status");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public StationData()
	{
		m_Name = string.Empty;
		m_NetworkIpAddress = IPAddress.None;
		m_PlcsimIpAddress = IPAddress.None;
		m_PlcsimRackNumber = 0;
		m_PlcsimSlotNumber = 2;
		m_TsapCheckEnabled = false;
		m_Status = StationStatus.READY.ToString();
	}

	public StationData(string name, IPAddress networkIpAddress, IPAddress plcsimIpAddress, int rack, int slot, bool tsapCheckEnabled)
	{
		m_Name = name;
		m_NetworkIpAddress = networkIpAddress;
		m_PlcsimIpAddress = plcsimIpAddress;
		m_PlcsimRackNumber = rack;
		m_PlcsimSlotNumber = slot;
		m_TsapCheckEnabled = tsapCheckEnabled;
		m_Status = StationStatus.READY.ToString();
	}

	private void NotifyPropertyChanged(string name)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(name));
		}
	}

	public StationData ShallowCopy()
	{
		return (StationData)MemberwiseClone();
	}
}
