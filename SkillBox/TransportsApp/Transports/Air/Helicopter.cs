using System;

namespace TransportsApp
{
	public class Helicopter : AirTransport, IHavingFuelEngine
	{
		public override string Name { get; } = "Вертолёт";

		public int EnginePower { get; }

		public int ValueOfTank { get; }


		public Helicopter(string id, int enginePower, int valueOfTank)
			: base(id)
		{
			EnginePower = enginePower;
			ValueOfTank = valueOfTank;
		}

		public override void Move()
		{
			Console.WriteLine($"{Name} {Id} движется по воздуху, используя пропеллеры");
		}
	}
}