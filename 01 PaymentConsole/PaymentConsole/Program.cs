using System;
using Nager.Date;

namespace PaymentConsole
{
	public class Program
	{
		public static void Main(string[] args)
		{
			const int limit = 10000;

			Console.Write("Введите вид платежа: ");
			var paymentType = Console.ReadLine();
			var serviceFee = GetFee(paymentType);

			Console.WriteLine("Взимается комиссия {0}%", serviceFee);
			Console.Write("Введите сумму для пополнения счёта: ");

			var sumString = Console.ReadLine();

			var success = double.TryParse(sumString, out var sum);
			var result = success && sum > 0;

			if (result)
			{
				if (sum > limit)
				{
					Console.WriteLine("Введенная сумма превышает допустимый лимит ({0} р.)", limit);
				}
				else
				{
					Console.Write("Введите промокод: ");
					var promoCode = Console.ReadLine();
					var bonusValue = GetBonus(promoCode, CountryCode.RU, DateTime.Now);
					var bonusMessage = string.Empty;
					switch (bonusValue)
					{
						case -1:
							bonusMessage = "Введен неверный промокод. Операция пройдет без начисления бонусов";
							break;
						case 0:
							bonusMessage = "Операция пройдет без начисления бонусов";
							break;
						default:
							bonusMessage = $"Вам будут начислены бонусы в количестве: {bonusValue}";
							break;
					}
					Console.WriteLine(bonusMessage);

					var feeValue = sum * serviceFee / 100;
					Console.WriteLine("Платеж проведен успешно.");
					Console.WriteLine("Комиссия составила {0} р.", feeValue);
				}

				Console.ReadKey();
			}

			if (!success)
			{
				Console.WriteLine("Вы ввели некорректную сумму");
				Console.ReadKey();
			}
		}

		public static int GetBonus(string promoCode,
			CountryCode countryCode,
			DateTime dateTime,
			string countyCode = null)
		{
			var gladFactor = HolidayChecker.CheckOnHoliday(dateTime, countryCode, countyCode) ? 2 : 1;
			var bonus = 0;
			switch (promoCode)
			{
				case "code1":
					bonus = 1;
					break;
				case "code2":
					bonus = 2;
					break;
				case "code3":
					bonus = 3;
					break;
				case "":
					break;
				default:
					return -1;
			}

			return bonus * gladFactor;
		}

		private static int GetFee(string paymentType)
		{
			var random = new Random();
			var min = 0;
			int max;

			switch (paymentType)
			{
				case "Сотовый оператор":
					max = 5;
					break;
				case "Интернет":
					min = 6;
					max = 10;
					break;
				case "ЖКХ":
					min = 11;
					max = 15;
					break;
				default:
					min = 16;
					max = 20;
					break;
			}

			return random.Next(min, max);
		}
	}
}
