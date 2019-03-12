using System;
using TweetSharp;

namespace TwitterConsole
{
	public static class TwitterServiceExtensions
	{
		public static T Execute<T>(this TwitterService service, Func<TwitterService, T> func)
		{
			try
			{
				return func.Invoke(service);
			}
			catch (Exception e)
			{
				throw new TwitterApiException(e.ToString());
			}
		}

		public static void Execute(this TwitterService service, Action<TwitterService> action)
		{
			try
			{
				action.Invoke(service);
			}
			catch (Exception e)
			{
				throw new TwitterApiException(e.ToString());
			}
		}
	}
}