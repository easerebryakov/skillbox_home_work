using System;
using ConsoleApplicationManager;

namespace OnlineStoreV2
{
	public static class ActionsHelper
	{
		public static DataInputAction CreateOrderWithForceNumberAction(Context ctx, OrderStorage storage)
		{
			return DataInputAction.CreateNew("Введите новый номер заказа:", ActionsKeys.NewOrderNumber)
				.SetDescendantAction(WorkAction.CreateNew(ctx1 =>
				{
					var newOrderNumber = ctx[ActionsKeys.NewOrderNumber];
					if (int.TryParse(newOrderNumber, out var num))
					{
						var creationSuccess = storage.TryAddNewOrder(num);
						Console.WriteLine(creationSuccess
							? $"Создан новый заказ с номером '{num}'"
							: $"Не удалось создать заказ с номером '{num}'");
					}
					else
					{
						Console.WriteLine($"Указаное значение не является целым числом '{newOrderNumber}'");
					}
				}));
		}

		public static WorkAction CreateOrderWithFreeNumberAction(OrderStorage storage)
		{
			return WorkAction.CreateNew(ctx1 =>
			{
				var creationSuccess = storage.TryAddNewOrder(out var num);
				Console.WriteLine(creationSuccess
					? $"Создан новый заказ с номером '{num}'"
					: $"Не удалось создать заказ");
			});
		}

		public static WorkAction ActionForExecutionOrder(OrderStorage storage)
		{
			return WorkAction.CreateNew(ctx1 =>
			{
				if (storage.GetOrdersCountInQueue() == 0)
				{
					Console.WriteLine($"Нельзя выполнить несуществующий заказ");
					return;
				}
				var executionSuccess = storage.TryExecuteNextOrder(out var orderNumber);
				if (executionSuccess)
					Console.WriteLine($"Заказ '{orderNumber}' выполнен");
			});
		}
	}
}