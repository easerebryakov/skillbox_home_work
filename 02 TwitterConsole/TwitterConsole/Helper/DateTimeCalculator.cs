using System;

namespace TwitterConsole
{
	public static class DateTimeCalculator
	{
		public static TimeSpan CalculateAge(DateTime creatingDateTime, DateTime nowDateTime)
		{
			return nowDateTime - creatingDateTime;
		}

		public static string GetAgeDescription(DateTime creatingDateTime, DateTime nowDateTime)
		{
			var age = CalculateAge(creatingDateTime, nowDateTime);
			return $"Days: {age.Days}. Hours: {age.Hours}. Minutes: {age.Minutes}";
		}
	}
}
