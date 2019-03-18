using System.Collections.Generic;
using System.Linq;

namespace OnlineStoreV2
{
	public class OrderStorage
	{
		public HashSet<Order> ArchivalOrders { get; }

		private readonly int maxOrders;
		//Для выполнения заказа по номеру использовать List
		private readonly Queue<Order> ordersQueue;

		public OrderStorage(int maxOrders)
		{
			this.maxOrders = maxOrders;
			ordersQueue = new Queue<Order>();
			ArchivalOrders = new HashSet<Order>();
		}

		public bool TryAddNewOrder(out int orderNumber)
		{
			orderNumber = 0;
			if (!CheckFreeSlots(out _))
				return false;
			var firstAvailableNumber = Enumerable.Range(1, int.MaxValue)
				.Except(ArchivalOrders.Select(o => o.Number))
				.Except(ordersQueue.Select(o => o.Number))
				.First();
			var newOrder = new Order(firstAvailableNumber);
			ordersQueue.Enqueue(newOrder);
			orderNumber = newOrder.Number;
			return true;
		}

		public bool TryAddNewOrder(int orderNumber)
		{
			if (!CheckFreeSlots(out _))
				return false;
			if (!ArchivalOrders.Select(o => o.Number).Contains(orderNumber) &&
				!ordersQueue.Select(o => o.Number).Contains(orderNumber))
			{
				var newOrder = new Order(orderNumber);
				ordersQueue.Enqueue(newOrder);
			}

			return true;
		}

		public bool TryExecuteNextOrder(out int orderNumber)
		{
			orderNumber = 0;
			if (!ordersQueue.Any())
				return false;
			var nextOrder = ordersQueue.Dequeue();
			orderNumber = nextOrder.Number;
			nextOrder.Execute();
			ArchivalOrders.Add(nextOrder);
			return true;
		}

		public bool CheckFreeSlots(out int freeSlots)
		{
			freeSlots = maxOrders - ordersQueue.Count;
			return freeSlots > 0;
		}

		public int GetOrdersCountInQueue() => ordersQueue.Count;
	}
}