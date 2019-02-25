using System;
using System.Text.RegularExpressions;

namespace TwitterConsole
{
	public static class StringExtensions
	{
		public static string RemoveLineBreaks(this string lines)
		{
			return lines.Replace("\r", "").Replace("\n", "");
		}

		public static string ReplaceLineBreaks(this string lines, string replacement)
		{
			return lines.Replace("\r\n", replacement)
				.Replace("\r", replacement)
				.Replace("\n", replacement);
		}

		public static string NormalizeSpace(this string input)
		{
			return Regex.Replace(input, @"\s{2,}", " ").Trim();
		}

		public static string SubstringBeforeFirstIndex(this string value, string a)
		{
			var posA = value.IndexOf(a, StringComparison.InvariantCulture);
			return posA == -1 ? "" : value.Substring(0, posA);
		}

		public static string SubstringBeforeLastIndex(this string value, string a)
		{
			var posA = value.LastIndexOf(a, StringComparison.InvariantCulture);
			return posA == -1 ? "" : value.Substring(0, posA);
		}

		public static string SubstringAfterFirstIndex(this string value, string a)
		{
			var posA = value.IndexOf(a, StringComparison.InvariantCulture);
			if (posA == -1)
				return "";
			var adjustedPosA = posA + a.Length;
			return adjustedPosA >= value.Length ? "" : value.Substring(adjustedPosA);
		}

		public static string SubstringAfterLastIndex(this string value, string a)
		{
			var posA = value.LastIndexOf(a, StringComparison.InvariantCulture);
			if (posA == -1)
				return "";
			var adjustedPosA = posA + a.Length;
			return adjustedPosA >= value.Length ? "" : value.Substring(adjustedPosA);
		}
	}
}