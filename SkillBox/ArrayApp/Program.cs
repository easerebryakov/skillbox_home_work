using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

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

			//first way
			var revertedDays = days.Reverse();
			Console.WriteLine(revertedDays.Aggregate((s, s1) => $"{s}\n{s1}"));

			Console.WriteLine("\nSecond way:");

			//second way
			ReverseArray(days);
			Console.WriteLine(days.Aggregate((s, s1) => $"{s}\n{s1}"));
		}

		private static void ReverseArray(IList<string> days)
		{
			int i;
			var j = days.Count - 1;

			for (i = 0; i < j; i++)
			{
				var left = days[i];
				days[i] = days[j];
				days[j] = left;
				j--;
			}
		}
	}
}
