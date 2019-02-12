using System;
using JetBrains.Annotations;
using Kontur.Logging;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Exception;
using VkNet.Model;

namespace VkAppBot
{
	public class VkAuthorizer
	{
		private readonly VkApi vkApi;
		private readonly ILog log;
		private readonly string login;
		private readonly string password;

		public VkAuthorizer(VkApi vkApi,
			ILog log,
			[CanBeNull] string login,
			[CanBeNull] string password)
		{
			this.vkApi = vkApi;
			this.log = log;
			this.login = login;
			this.password = password;
		}

		public bool TryAuthorize()
		{
			var authorizeParams = new ApiAuthParams
			{
				ApplicationId = 6858286,
				Login = login,
				Password = password,
				Settings = Settings.All | Settings.Messages
			};

			try
			{
				vkApi.Authorize(authorizeParams);
				return true;
			}
			catch (VkApiException e)
			{
				log.Error($"Message: {e.Message}\nStacktrace: {e.StackTrace}");
				return false;
			}
			catch (Exception e)
			{
				log.Error($"Неизвестная ошибка при авторизации.\nMessage: {e.Message}\nStacktrace: {e.StackTrace}");
				return false;
			}
		}
	}
}