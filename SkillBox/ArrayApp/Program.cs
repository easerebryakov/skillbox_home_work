using System;
using System.Linq;

namespace ArrayApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WriteDaysArray();
			Console.WriteLine();
			WriteChessBoard();
		}

		private static void WriteChessBoard()
		{
			const int horizontal = 8;
			const int vertical = 8;

			var cells = new int[horizontal, vertical];

			for (var i = 0; i < horizontal; i++)
			{
				for (var v = 0; v < vertical; v++)
				{
					var division = (i + v) % 2;
					cells[i, v] = division == 0 ? 1 : 0;
				}
			}

			for (var i = horizontal - 1; i >= 0; i--)
			{
				for (var v = vertical - 1; v >= 0; v--)
				{
					var cellValue = cells[i, v];
					Console.Write(cellValue);
				}
				Console.WriteLine();
			}

		}

		public static void WriteDaysArray()
		{
			var days = new[]
			{
				"Понедельник",
				"Вторник",
				"Среда",
				"Четверг",
				"Пятница",
				"Суббота",
				"Воскресенье"
			};

			var revertedDays = days.Reverse();
			Console.WriteLine(revertedDays.Aggregate((s, s1) => $"{s}\n{s1}"));
		}
	}
}
