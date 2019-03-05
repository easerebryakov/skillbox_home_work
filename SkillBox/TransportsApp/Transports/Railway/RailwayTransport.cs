using System;

namespace TransportsApp
{
	public abstract class RailwayTransport : Transport
	{
		protected RailwayTransport(string id) : base(id)
		{
		}

		public override void Move()
		{
			Console.WriteLine($"{Name} {Id} передвигается по рельсам");
		}
	}
}