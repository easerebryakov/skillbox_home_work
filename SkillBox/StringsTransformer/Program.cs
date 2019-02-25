using System;
using System.Linq;

namespace StringsTransformer
{
	public partial class Program
	{
		private static readonly char[] DelimetrChars = { ' ', ',', '.', ':', '\t' };
		private const int AllowedTextLength = 140;

		public static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("Введите текст:");
				var inputText = Console.ReadLine();
				if (string.IsNullOrEmpty(inputText))
				{
					Console.WriteLine("Вы ввели пустой текст");
					continue;
				}

				string cutText;
				if (inputText.Length <= 140)
				{
					Console.WriteLine("Будет обработан исходный текст");
					cutText = inputText;
				}

				else
				{
					Console.WriteLine($"Текст будет обрезан до символов в кол-ве: {AllowedTextLength}");
					cutText = inputText.Substring(0, AllowedTextLength);
					Console.WriteLine($"Текст для обработки:\n{cutText}");
				}

				var words = cutText
					.Split(new[] {" - "}, StringSplitOptions.RemoveEmptyEntries)
					.SelectMany(p => p.Split(DelimetrChars)
						.Where(w => !string.IsNullOrEmpty(w)))
					.ToList();

				if (words.Count == 0)
				{
					Console.WriteLine("В введеном тексте нет слов");
					continue;
				}

				Console.WriteLine($"Всего слов в тексте: {words.Count}");

				words.Sort(new StringByLengthComparer());
				var bigWord = words[0];

				Console.WriteLine(
					$"Самое большое слово в тексте: {bigWord}. Количество символов в нем: {bigWord.Length}");
			}
		}
	}
}
