using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore
{
	public class Program
	{
		//private static readonly OrderState[] OrdersStates = {
		//	OrderState.Boxing,
		//	OrderState.WaitingForPayment,
		//	OrderState.Boxing,
		//	OrderState.WaitingForPayment,
		//	OrderState.Registration
		//};

		private const int GenerateOrdersCount = 50;

		public static void Main(string[] args)
		{
			var ordersStates = GenerateRandomOrdersStates();

			while (true)
			{
				Console.WriteLine("Введите номер заказ:");
				var numberOfOrder = Console.ReadLine();
				if (!int.TryParse(numberOfOrder, out var numberOfOrderInt))
				{
					Console.WriteLine("Введеное значение не является числом");
					continue;
				}
				Console.WriteLine(!CheckOnOrderExist(numberOfOrderInt, ordersStates)
					? $"Заказ с номером '{numberOfOrder}' не найден"
					: $"Статус заказа под номером '{numberOfOrderInt}': {ordersStates[numberOfOrderInt]}");
			}
		}

		private static bool CheckOnOrderExist(int orderNum, IEnumerable<OrderState> ordersStates)
			=> ordersStates.Count() - 1 >= orderNum;

		private static OrderState[] GenerateRandomOrdersStates()
		{
			var result = new OrderState[GenerateOrdersCount];
			var random = new Random();
			var possibleStatesCount = Enum.GetNames(typeof(OrderState)).Length;
			for (var i = 0; i < GenerateOrdersCount; i++)
			{
				var state = (OrderState)random.Next(0, possibleStatesCount);
				result[i] = state;
			}

			return result;
		}
	}
}
