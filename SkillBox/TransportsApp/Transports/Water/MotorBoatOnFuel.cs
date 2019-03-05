namespace TransportsApp
{
	public class MotorBoatOnFuel : WaterTransport, IHavingFuelEngine
	{
		public override string Name { get; } = "Моторная лодка на топливе";

		public int EnginePower { get; }

		public int ValueOfTank { get; }

		public MotorBoatOnFuel(string id, int enginePower, int valueOfTank)
			: base(id)
		{
			EnginePower = enginePower;
			ValueOfTank = valueOfTank;
		}
	}
}