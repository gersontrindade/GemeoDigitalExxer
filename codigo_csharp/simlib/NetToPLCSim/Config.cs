using System.ComponentModel;

namespace NetToPLCSim;

internal class Config
{
	public BindingList<StationData> Stations = new BindingList<StationData>();

	public bool IsStationNameUnique(string name)
	{
		foreach (StationData station in Stations)
		{
			if (station.Name == name)
			{
				return false;
			}
		}
		return true;
	}

	public bool IsStationNameUniqueExcept(string name, int index)
	{
		for (int i = 0; i < Stations.Count; i++)
		{
			if (i != index && Stations[i].Name == name)
			{
				return false;
			}
		}
		return true;
	}
}
