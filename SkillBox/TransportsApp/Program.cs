using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportsApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var transports = new Transport[]
			{
				new Aircraft("Розовый слоник", 1000, 2000),
				new Helicopter("Пушистая сова", 500, 400),
				new FuelCar("Резвый конь", 200, 60),
				new MotorBoatOnFuel("Синий дельфин", 50, 50),
				new ElectricityTrolley("Старая телега", 10, 10)
			};

			foreach (var transport in transports)
				transport.Move();
		}
	}
}
