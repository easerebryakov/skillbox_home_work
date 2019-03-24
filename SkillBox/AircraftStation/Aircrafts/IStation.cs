using System;

namespace AircraftStation
{
	public interface IStation
	{
		EventHandler OnStationStateChange { get; set; }
		bool TryTakeAircraft(string id);
		bool TrySendAircraft(out string aircraftId);
		bool CheckFreeSlots(out int freeSlots);
		int GetAircraftsCountOnStation();
		bool AnyAircraftsOnStation();
		bool CheckOnAircraftOnStation(string id);
	}
}