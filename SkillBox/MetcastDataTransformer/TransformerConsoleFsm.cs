using System;
using System.IO;
using System.Xml.Linq;
using Kontur.Logging;

namespace MetcastDataTransformer
{
	public class TransformerConsoleFsm
	{
		private readonly ColorConsoleLog colorConsoleLog;

		public TransformerConsoleFsm(ColorConsoleLog colorConsoleLog)
		{
			this.colorConsoleLog = colorConsoleLog;
		}

		public void Start()
		{
			while (true)
			{
				Console.Write("Укажите путь до файла: ");
				var pathToInputFile = Console.ReadLine();
				if (!File.Exists(pathToInputFile))
				{
					colorConsoleLog.Warn($"Файл не найден: '{pathToInputFile}'");
					continue;
				}

				XDocument inputDocument;
				try
				{
					inputDocument = XDocument.Load(pathToInputFile);
				}
				catch (Exception e)
				{
					colorConsoleLog.Error($"Произошла ошибка при чтении файла: '{pathToInputFile}'");
					colorConsoleLog.Error($"Message: {e.Message}\nStacktrace: {e.StackTrace}");
					continue;
				}
				
				var metcastInfo = new MetcastXmlParser(inputDocument, colorConsoleLog).GetMetcastTownInfos();
				var transformer = new DataTransformer(metcastInfo);
				var outputDocument = transformer.Transform();

				var oldFileName = Path.GetFileNameWithoutExtension(pathToInputFile);
				var newFileName = $"{oldFileName}_output.xml";
				var outputPath = Path.Combine(Path.GetDirectoryName(pathToInputFile), newFileName);
				if (File.Exists(outputPath))
					File.Delete(outputPath);
				outputDocument.Save(outputPath);

				colorConsoleLog.Info($"Выходной файл сохранен по пути: '{outputPath}'");
			}
		}
	}
}