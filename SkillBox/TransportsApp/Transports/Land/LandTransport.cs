using System;

namespace TransportsApp
{
	public abstract class LandTransport : Transport
	{
		protected LandTransport(string id) : base(id)
		{
		}

		public override void Move()
		{
			Console.WriteLine($"{Name} {Id} движется по земле на колесах");
		}
	}
}