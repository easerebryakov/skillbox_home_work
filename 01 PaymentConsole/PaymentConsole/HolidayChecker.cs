using System;
using Nager.Date;

namespace PaymentConsole
{
	public static class HolidayChecker
	{
		public static bool CheckOnHoliday(DateTime dateTime,
			CountryCode countryCode,
			string countyCode = null)
		{
			var isHolidayOrWeekend = DateSystem.IsWeekend(dateTime, CountryCode.RU)
									|| DateSystem.IsPublicHoliday(dateTime, CountryCode.RU)
									|| countyCode != null && DateSystem.IsOfficialPublicHolidayByCounty(dateTime, countryCode, countyCode);
			return isHolidayOrWeekend;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dateStr">dd.mm.yyyy</param>
		/// <param name="countryCode"></param>
		/// <param name="countyCode"></param>
		/// <returns></returns>
		public static bool CheckOnHoliday(string dateStr,
			CountryCode countryCode,
			string countyCode = null)
		{
			var parts = dateStr.Split('.');
			var day = int.Parse(parts[0]);
			var month = int.Parse(parts[1]);
			var year = int.Parse(parts[2]);
			var dateTime = new DateTime(year, month, day);
			return CheckOnHoliday(dateTime, countryCode, countyCode);
		}
	}
}