using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplicationManager;

namespace AircraftStation
{
	public class Program
	{
		private static readonly Station Station = new Station(5);

		public static void Main(string[] args)
		{
			var action = DynamicSelectionActionWithCondition.CreateNew(ctx =>
			{
				if (Station.CheckFreeSlots(out var countFreeSlots))
				{
					return SelectionAction
						.CreateNew($"Количество свободных мест для стоянки - {countFreeSlots}")
						.AddAction("1", "Принять самолёт", DataInputAction.CreateNew("Укажите номер борта самолёта", "airNum")
							.SetDescendantAction(WorkAction.CreateNew(ctx1 =>
							{
								var id = ctx1["airNum"];
								Console.WriteLine(Station.TryTakeAircraft(id)
									? $"Самолёт с номером борта '{id}' принят"
									: $"Самолёт с номером борта '{id}' не удалось принять");
							})))
						.AddAction("2", "Отправить самолёт", SendAircraftAction());
				}

				return SelectionAction
					.CreateNew("Свободных мест для самолётов нет")
					.AddAction("1", "Отправить самолёт", SendAircraftAction());
			});

			var worker = new ConsoleActionsWorker(action, new WorkerSettings
			{
				SelectDescription = "Введите номер действия",
				ReselectDescriptionIfUnknownAction = "Указанное действие не найдено. Повторите выбор"
			});

			worker.Start();
		}

		private static WorkAction SendAircraftAction()
		{
			return WorkAction.CreateNew(ctx1 =>
			{
				if (Station.GetAircraftsCountInStation() == 0)
				{
					Console.WriteLine("Нельзя отправить, т.к. ничего нет");
					return;
				}
					
				var trySendAircraft = Station.TrySendAircraft(out var aircraftNumber);
				Console.WriteLine(trySendAircraft
					? $"Самолет с номером борта '{aircraftNumber}' отправлен"
					: $"Самолет с номером борта '{aircraftNumber}' не удалось отправить");
			});
		}
	}
}
