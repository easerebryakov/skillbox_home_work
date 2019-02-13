using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Kontur.Logging;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using VkNet;
using VkNet.Utils;

namespace VkAppBot
{
	public class BotRunner
	{
		private static readonly ILog Log = new Log4netWrapper();
		private static readonly ColorConsoleLog ConsoleLog = new ColorConsoleLog();

		private static readonly string Token =
			"";

		public static void Main(string[] args)
		{
			var vkApi = new VkApi(new Logger<VkApi>(new BotLoggerFactory(Log)));
			var vkClient = new VkClient(vkApi);

			#region Auth

			ConsoleLog.Info("Идет авторизация...");
			var vkAuth = new VkAuthorizer(vkApi, Log, "", "");
			var authResult = vkAuth.TryAuthorize();
			if (authResult)
				ConsoleLog.Info("Авторизация прошла успешно");
			else
			{
				ConsoleLog.Info("Авторизация закончилась с ошибкой. См. логи");
				return;
			}

			#endregion

			#region RunBot

			try
			{
				RunBot(vkApi, vkClient);
			}
			catch (Exception e)
			{
				Log.Error($"Message: {e.Message}\nStackTrace: {e.StackTrace}\nInnerException: {e.InnerException}");
				ConsoleLog.Error($"Произошла ошибка в боте. Ему стало плохо. См. логи");
			}

			#endregion

		}

		private static void RunBot(VkApi vkApi, IVkClient vkClient)
		{
			var webClient = new WebClient
			{
				Encoding = Encoding.UTF8
			};

			var parameters = new VkParameters
			{
				{"group_id", "178216097" }
			};

			dynamic responseOnLongPollApi = JObject.Parse(vkApi.Call("groups.getLongPollServer", parameters).RawJson);
			var key = responseOnLongPollApi.response.key.ToString();
			var server = responseOnLongPollApi.response.server.ToString();
			var ts = responseOnLongPollApi.response.ts.ToString();

			var json = string.Empty;
			var url = string.Empty;
			
			while (true)
			{
				url = string.Format("{0}?act=a_check&key={1}&ts={2}&wait=1", server, key,
					json != string.Empty ? JObject.Parse(json)["ts"].ToString() : ts);

				json = webClient.DownloadString(url);
				var messages = JObject.Parse(json)["updates"].ToList();

				foreach (var messageObj in messages)
				{
					if (messageObj["type"].ToString() != "message_new")
						continue;
					
					var inputMessageStr = messageObj["object"]["body"].ToString();
					ConsoleLog.Info($"Input message: {inputMessageStr}");
					var from = messageObj["object"]["user_id"].ToString();
					Log.Info($"Input message: {inputMessageStr}");

					var offset = 0;
					var batchSize = 1000;
					//key - city id, value - count users in city
					var citiesDictionary = new Dictionary<string, int>();
					var countUsersWithoutCities = 0;
					var urlBotMsg = $"https://api.vk.com/method/messages.send?v=5.41&access_token={Token}&user_id=";
					while (true)
					{
						if (vkClient.TryGetMembersCountFromGroup(inputMessageStr, out var membersCount))
						{
							if (membersCount > 1000)
							{
								webClient.DownloadString(string.Format(urlBotMsg + "{0}&message={1}", from,
									$"Бот еще маленький. В группе {inputMessageStr} - {membersCount} человек. Бот умеет считать только до 1000"));
								break;
							}
						}
						else
						{
							webClient.DownloadString(string.Format(urlBotMsg + "{0}&message={1}", from,
								$"Неверный идентификатор группы: {inputMessageStr}"));
							break;
						}

						vkClient.TryGetMembersIdsFromGroup(inputMessageStr, offset, batchSize, out var membersIds);
						var cities = vkClient.GetUsersCities(membersIds.Select(i => i.ToString()).ToArray());
						foreach (var city in cities)
						{
							if (string.IsNullOrEmpty(city))
							{
								countUsersWithoutCities++;
							}
							else if (citiesDictionary.ContainsKey(city))
								citiesDictionary[city]++;
							else
								citiesDictionary.Add(city, 1);
						}

						if (membersIds.Count() < batchSize)
						{
							var stringBuilder = new StringBuilder();
							var i = 0;

							foreach (var cityInfo in citiesDictionary)
							{
								stringBuilder.AppendLine($"{cityInfo.Key} - {cityInfo.Value}");
								i++;
								if (i == 20)
								{
									webClient.DownloadString(string.Format(urlBotMsg + "{0}&message={1}", from,
										stringBuilder));
									i = 0;
									stringBuilder = new StringBuilder();
								}
							}

							if (countUsersWithoutCities > 0)
								stringBuilder.AppendLine($"Без указания города: {countUsersWithoutCities}");

							webClient.DownloadString(string.Format(urlBotMsg + "{0}&message={1}", from,
								stringBuilder));

							//vkApi.Messages.Send(new MessagesSendParams
							//{
							//	Message = $"{stringBuilder}",
							//	UserId = long.Parse(from),
							//	RandomId = new Random().Next()
							//});
							break;
						}
						offset = offset + membersIds.Count();
					}
					
					Thread.Sleep(200);
				}
			}
		}
	}
}
