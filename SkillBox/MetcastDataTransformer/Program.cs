using System;
using Kontur.Logging;

namespace MetcastDataTransformer
{
	public class Program
	{
		private static readonly ColorConsoleLog ColorConsoleLog = new ColorConsoleLog();

		public static void Main(string[] args)
		{
			try
			{
				var fsm = new TransformerConsoleFsm(ColorConsoleLog);
				fsm.Start();
			}
			catch (Exception e)
			{
				ColorConsoleLog.Error($"Unhandled exception: {e.Message}");
			}
		}
	}
}
