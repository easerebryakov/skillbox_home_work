namespace TransportsApp
{
	public class ElectricityTrolley : RailwayTransport, IHavingElectricityEngine
	{
		public override string Name { get; } = "Электрическая вагонетка";

		public int EnginePower { get; }

		public int? AccumulatorPower { get; }

		public ElectricityTrolley(string id, int enginePower, int? accumulatorPower)
			: base(id)
		{
			EnginePower = enginePower;
			AccumulatorPower = accumulatorPower;
		}
	}
}