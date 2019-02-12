using System;
using Kontur.Logging;

namespace VkAppBot
{
	public class DisposablesLogWrapper : IDisposable
	{
		public ILog Log { get; }

		public DisposablesLogWrapper(ILog log)
		{
			Log = log;
		}

		public void Dispose()
		{
		}
	}
}