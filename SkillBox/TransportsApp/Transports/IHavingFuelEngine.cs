namespace TransportsApp
{
	public interface IHavingFuelEngine : IHavingEngine
	{
		int ValueOfTank { get; }
	}
}