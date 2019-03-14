using System;
using System.Collections.Generic;
using System.Linq;

namespace TransportsApp
{
	public class Station<T>
		where T : Transport
	{
		private readonly EventHandler<string> stationNotification;
		private readonly List<T> transports;

		public string StationName { get; }

		public Station(string stationName, EventHandler<string> stationNotification, params T[] initialTransports)
		{
			this.stationNotification = stationNotification;
			StationName = stationName;
			transports = initialTransports.ToList();
		}

		public void Take(params T[] inputTransports)
		{
			foreach (var transport in inputTransports)
			{
				if (transports.Contains(transport))
				{
					Notify($"{transport} уже на станции {StationName}");
				}
				else
				{
					transports.Add(transport);
					Notify($"Прибыл {transport} со станции {StationName}");
				}
			}
		}

		public bool TrySend(T transport)
		{
			if (transports.Contains(transport))
			{
				transports.Remove(transport);
				Notify($"Отправлен {transport} со станции {StationName}");
				return true;
			}

			Notify($"{transport} на станции {StationName} отсутствует");
			return false;
		}

		private void Notify(string notification)
		{
			stationNotification.Invoke(this, notification);
		}
	}
}