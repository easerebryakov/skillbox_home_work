using System;

namespace TransportsApp
{
	public abstract class Transport
	{
		public abstract string Name { get; }

		public string Id { get; }

		protected Transport(string id)
		{
			Id = id;
		}

		public virtual void Move()
		{
			Console.WriteLine($"{Name} {Id} передвигается неизвестно как");
		}
	}
}