namespace TransportsApp
{
	public interface IHavingElectricityEngine : IHavingEngine
	{
		int? AccumulatorPower { get; }
	}
}