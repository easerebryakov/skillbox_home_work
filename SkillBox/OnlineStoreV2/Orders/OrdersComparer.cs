using System.Collections.Generic;

namespace OnlineStoreV2
{
	public class OrdersComparer : IEqualityComparer<Order>
	{
		public bool Equals(Order x, Order y)
		{
			if (x == null || y == null)
				return false;
			return x.Number == y.Number;
		}

		public int GetHashCode(Order obj)
		{
			return obj.Number;
		}
	}
}