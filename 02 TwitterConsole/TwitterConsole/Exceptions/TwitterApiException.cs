using System;

namespace TwitterConsole
{
	public class TwitterApiException : Exception
	{
		public TwitterApiException(string message)
			: base(message)
		{
			
		}
	}
}