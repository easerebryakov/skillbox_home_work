using TwitterConsole.Helpers;

namespace TwitterConsole.Objects
{
	public class Twitt
	{
		private const int MaxTwittLength = 140;

		public string TwittMessage { get; }

		public string[] HashTags { get; }

		public Twitt(string message)
		{
			TwittMessage = StringHelper.Cut(message, MaxTwittLength);
			HashTags = ExtractHashTagsFromMessage(TwittMessage);
		}

		private string[] ExtractHashTagsFromMessage(string message)
		{
			return new string[] { };
		}
	}
}