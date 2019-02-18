using System;
using Nager.Date;
using NUnit.Framework;

namespace PaymentConsole.Tests
{
	[TestFixture]
	public class PaymentConsoleTests
	{
		[TestCase("18.2.2019", "", 0)]
		[TestCase("18.2.2019", "code1", 1)]
		[TestCase("18.2.2019", "code2", 2)]
		[TestCase("18.2.2019", "code3", 3)]
		[TestCase("17.2.2019", "code1", 2)]
		[TestCase("17.2.2019", "code2", 4)]
		[TestCase("17.2.2019", "code3", 6)]
		public void Test(string dateStr, string code, int expectedBonus)
		{
			var parts = dateStr.Split('.');
			var day = int.Parse(parts[0]);
			var month = int.Parse(parts[1]);
			var year = int.Parse(parts[2]);
			var dateTime = new DateTime(year, month, day);
			var bonus = Program.GetBonus(code, CountryCode.RU, dateTime);
			Assert.AreEqual(expectedBonus, bonus);
		}
	}
}