using System;
using Kontur.Logging;
using Microsoft.Extensions.Logging;

namespace VkAppBot
{
	public class BotLog : ILogger
	{
		private readonly DisposablesLogWrapper logWrapper;
		private ILog log;

		public BotLog(ILog log)
		{
			logWrapper = new DisposablesLogWrapper(log);
			this.log = log;
		}

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			var message = formatter(state, exception);
			switch (logLevel)
			{
				case LogLevel.Information:
					log.Info(message);
					break;
				case LogLevel.Critical:
				case LogLevel.Error:
					log.Error(message);
					break;
				case LogLevel.Warning:
					log.Warn(message);
					break;
				case LogLevel.Debug:
					log.Debug(message);
					break;
				default:
					log.Info(message);
					break;
			}
		}

		public bool IsEnabled(LogLevel logLevel) => true;

		public IDisposable BeginScope<TState>(TState state)
		{
			return logWrapper;
		}
	}
}