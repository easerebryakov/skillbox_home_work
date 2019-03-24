namespace OnlineStoreV2
{
	public interface IOrderStorage
	{
		bool TryAddNewOrder(out int orderNumber);
		bool TryAddNewOrder(int orderNumber);
		bool TryExecuteNextOrder(out int orderNumber);
		bool CheckFreeSlots(out int freeSlots);
		int GetOrdersCountInQueue();
	}
}