using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportsApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//var transports = new Transport[]
			//{
			//	new Aircraft("Розовый слоник", 1000, 2000),
			//	new Helicopter("Пушистая сова", 500, 400),
			//	new FuelCar("Резвый конь", 200, 60),
			//	new MotorBoatOnFuel("Синий дельфин", 50, 50),
			//	new ElectricityTrolley("Старая телега", 10, 10)
			//};

			//foreach (var transport in transports)
			//	transport.Move();

			var car1 = new FuelCar("Резвый конь", 200, 60);
			var car2 = new FuelCar("Маленькая кошка", 200, 60);
			var car3 = new HybridCar("Гибрыдный слон", 200, 60, 111);
			var carStation = new Station<LandTransport>("Автомобильная стоянка", StationNotification, car1, car3);
			carStation.Take(car2);
			carStation.TrySend(car1);
			carStation.Take(car3);
			carStation.TrySend(car3);
		}

		private static void StationNotification(object sender, string e)
		{
			Console.WriteLine(e);
		}
	}
}
