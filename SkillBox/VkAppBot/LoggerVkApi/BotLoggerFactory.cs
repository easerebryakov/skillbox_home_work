using Kontur.Logging;
using Microsoft.Extensions.Logging;

namespace VkAppBot
{
	public class BotLoggerFactory : ILoggerFactory
	{
		private readonly ILog log;

		public BotLoggerFactory(ILog log)
		{
			this.log = log;
		}

		public void Dispose()
		{
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new BotLog(log);
		}

		public void AddProvider(ILoggerProvider provider)
		{
		}
	}
}