namespace OnlineStoreV2
{
	public class Order
	{
		public int Number { get; }

		public OrderState State { get; private set; }

		public Order(int number)
		{
			Number = number;
			State = OrderState.WaitingExecute;
		}

		public void Execute()
		{
			State = OrderState.Finished;
		}
	}
}