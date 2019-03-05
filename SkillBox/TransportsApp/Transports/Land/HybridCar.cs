namespace TransportsApp
{
	public class HybridCar : LandTransport, IHavingFuelEngine, IHavingElectricityEngine
	{
		public override string Name { get; } = "Гибридный автомобиль";

		public int EnginePower { get; }

		public int ValueOfTank { get; }

		public int? AccumulatorPower { get; }

		public HybridCar(string id,
			int enginePower,
			int valueOfTank,
			int? accumulatorPower)
			: base(id)
		{
			EnginePower = enginePower;
			ValueOfTank = valueOfTank;
			AccumulatorPower = accumulatorPower;
		}
	}
}