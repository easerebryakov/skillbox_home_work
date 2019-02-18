using System;
using Nager.Date;
using NUnit.Framework;

namespace PaymentConsole.Tests
{
	[TestFixture]
	public class NagerTests
	{
		[TestCase("1.1.2019", true)]
		[TestCase("17.2.2019", true)]
		[TestCase("18.2.2019", false)]
		[TestCase("09.05.2019", true)]
		[TestCase("09.05.2020", true)]
		public void CheckOnHolidayOrNot(string dateStr, bool expectedHoliday)
		{
			var isHolidayOrWeekend = HolidayChecker.CheckOnHoliday(dateStr, CountryCode.RU);
			Assert.AreEqual(expectedHoliday, isHolidayOrWeekend);
		}
	}
}
