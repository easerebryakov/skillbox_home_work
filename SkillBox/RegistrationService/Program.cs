using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplicationManager;

namespace RegistrationService
{
	public class Program
	{
		private static readonly Dictionary<string, string> RegistrationStorage = new Dictionary<string, string>();

		public static void Main(string[] args)
		{
			var enterAction = DataInputAction.CreateNew($"Введите логин:", "login")
				.SetDescendantAction(DynamicSelectionActionWithCondition.CreateNew(ctx =>
				{
					var login = ctx["login"];
					if (RegistrationStorage.ContainsKey(login))
					{
						Console.WriteLine($"Здравствуйте, {RegistrationStorage[login]}");
						return new WorkAction(ctx1 => { });
					}

					Console.WriteLine("Пользователь не найден. Зарегистрируйтесь...");
					return DataInputAction.CreateNew("Введите логин:", "newLogin")
						.SetDescendantAction(DataInputAction.CreateNew("Введите имя:", "newName")
							.SetDescendantAction(new WorkAction(ctx1 =>
							{
								var newLogin = ctx1["newLogin"];
								if (RegistrationStorage.ContainsKey(newLogin))
								{
									Console.WriteLine($"Логин {newLogin} занят");
									return;
								}
								var newName = ctx1["newName"];
								RegistrationStorage.Add(newLogin, newName);
								Console.WriteLine($"Прошла регистрация логин - {newLogin}, имя - {newName}");
							})));
				}));

			var action = SelectionAction.CreateNew()
				.AddAction("1", "Ввести учетную запись", enterAction)
				.AddAction("2", "Показать имена всех пользователей", WorkAction.CreateNew(ctx =>
				{
					foreach (var name in RegistrationStorage.Values)
					{
						Console.WriteLine(name);
					}
				}));

			var worker = new ConsoleActionsWorker(action, new WorkerSettings
			{
				SelectDescription = "Введите номер действия",
				ReselectDescriptionIfUnknownAction = "Указанное действие не найдено. Повторите выбор"
			});

			worker.Start();
		}
	}
}
