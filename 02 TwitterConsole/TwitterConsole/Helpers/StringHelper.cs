namespace TwitterConsole.Helpers
{
	public static class StringHelper
	{
		public static string Cut(string inputString, int maxStringLength)
		{
			return inputString.Length <= maxStringLength
				? inputString
				: inputString.Substring(0, maxStringLength < 0 ? 0 : maxStringLength);
		}
	}
}
