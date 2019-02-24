using System;
using System.Linq;
using NUnit.Framework;

namespace TwitterConsole.Tests
{
	[TestFixture]
	public class DateTimeCalculatorTests
	{
		[TestCase("2010.1.1.0.0", "2010.1.2.0.0", "1.0.0", "Days: 1. Hours: 0. Minutes: 0")]
		[TestCase("2010.1.1.0.0", "2011.1.2.0.0", "366.0.0", "Days: 366. Hours: 0. Minutes: 0")]
		[TestCase("2010.1.1.0.0", "2010.1.1.1.0", "0.1.0", "Days: 0. Hours: 1. Minutes: 0")]
		[TestCase("2010.1.1.0.0", "2010.1.1.0.35", "0.0.35", "Days: 0. Hours: 0. Minutes: 35")]
		public void AgeTests(string creatingDateTimeStr, string nowDateTimeStr, string ageExpectedDateTimeStr, string expectedAgeDescription)
		{
			var creatingParts = creatingDateTimeStr
				.Split('.')
				.Select(int.Parse)
				.ToList();
			var nowParts = nowDateTimeStr
				.Split('.')
				.Select(int.Parse)
				.ToList();
			var creatingDateTime = new DateTime(creatingParts[0], creatingParts[1], creatingParts[2], creatingParts[3], creatingParts[4], 0);
			var nowDateDime = new DateTime(nowParts[0], nowParts[1], nowParts[2], nowParts[3], nowParts[4], 0);
			var age = DateTimeCalculator.CalculateAge(creatingDateTime, nowDateDime);
			var ageExpectedParts = ageExpectedDateTimeStr
				.Split('.')
				.Select(int.Parse)
				.ToList();
			var ageDescription = DateTimeCalculator.GetAgeDescription(creatingDateTime, nowDateDime);
			Assert.AreEqual(new TimeSpan(ageExpectedParts[0], ageExpectedParts[1], ageExpectedParts[2], 0), age);
			Assert.AreEqual(expectedAgeDescription, ageDescription);
		}
	}
}
