using System;

namespace TransportsApp
{
	public class Aircraft : AirTransport, IHavingFuelEngine
	{
		public override string Name { get; } = "Самолёт";

		public int EnginePower { get; }

		public int ValueOfTank { get; }


		public Aircraft(string id, int enginePower, int valueOfTank)
			: base(id)
		{
			EnginePower = enginePower;
			ValueOfTank = valueOfTank;
		}

		public override void Move()
		{
			Console.WriteLine($"{Name} {Id} движется по воздуху, используя крылья");
		}
	}
}