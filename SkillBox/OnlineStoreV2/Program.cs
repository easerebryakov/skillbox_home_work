using ConsoleApplicationManager;

namespace OnlineStoreV2
{
	public class Program
	{
		private static readonly OrderStorage Storage = new OrderStorage(10);

		public static void Main(string[] args)
		{
			var action = DynamicSelectionActionWithCondition.CreateNew(ctx =>
			{
				if (Storage.CheckFreeSlots(out var countFreeSlots))
				{
					return SelectionAction
						.CreateNew($"Количество свободных слотов - {countFreeSlots}")
						.AddAction("1", "Создать заказ", SelectionAction.CreateNew()
							.AddAction("1", "Создать заказ со свободным номером", ActionsHelper.CreateOrderWithFreeNumberAction(Storage))
							.AddAction("2", "Создать заказ с номером", ActionsHelper.CreateOrderWithForceNumberAction(ctx, Storage)))
						.AddAction("2", "Выполнить следующий заказ", ActionsHelper.ActionForExecutionOrder(Storage));
				}

				return SelectionAction
					.CreateNew("Свободных слотов на заказы нет")
					.AddAction("1", "Выполнить следующий заказ", ActionsHelper.ActionForExecutionOrder(Storage));
			});

			var worker = new ConsoleActionsWorker(action, new WorkerSettings
			{
				SelectDescription = "Выберете действие",
				ReselectDescriptionIfUnknownAction = "Указанное действие не найдено. Повторите выбор"
			});

			worker.Start();
		}
	}
}
