using System.Collections.Generic;

namespace StringsTransformer
{
	public partial class Program
	{
		public class StringByLengthComparer : IComparer<string>
		{
			public int Compare(string x, string y)
			{
				if (x == null)
					return 1;
				if (y == null)
					return -1;
				if (x.Length >= y.Length)
					return -1;
				return 1;
			}
		}
	}
}