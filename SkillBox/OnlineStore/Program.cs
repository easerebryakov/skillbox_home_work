using System;

namespace OnlineStore
{
	public class Program
	{
		private static readonly OrderState[] OrdersStates = {
			OrderState.Boxing,
			OrderState.WaitingForPayment,
			OrderState.Boxing,
			OrderState.WaitingForPayment,
			OrderState.Registration
		};

		public static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("Введите номер заказ:");
				var numberOfOrder = Console.ReadLine();
				if (!int.TryParse(numberOfOrder, out var numberOfOrderInt))
				{
					Console.WriteLine("Введеное значение не является числом");
					continue;
				}
				Console.WriteLine(!CheckOnOrderExist(numberOfOrderInt)
					? $"Заказ с номером '{numberOfOrder}' не найден"
					: $"Статус заказа под номером '{numberOfOrderInt}': {OrdersStates[numberOfOrderInt]}");
			}
		}

		private static bool CheckOnOrderExist(int orderNum) => OrdersStates.Length - 1 >= orderNum;
	}
}
