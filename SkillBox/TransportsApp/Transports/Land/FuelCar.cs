using System;

namespace TransportsApp
{
	public class FuelCar : LandTransport, IHavingFuelEngine
	{
		public override string Name { get; } = "Бензиновый автомобиль";

		public int EnginePower { get; }

		public int ValueOfTank { get; }


		public FuelCar(string id, int enginePower, int valueOfTank)
			: base(id)
		{
			EnginePower = enginePower;
			ValueOfTank = valueOfTank;
		}
	}
}