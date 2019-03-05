using System;

namespace TransportsApp
{
	public abstract class WaterTransport : Transport
	{
		protected WaterTransport(string id) : base(id)
		{
		}

		public override void Move()
		{
			Console.WriteLine($"{Name} {Id} плывет по воде");
		}
	}
}