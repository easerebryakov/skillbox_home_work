using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;
using JetBrains.Annotations;
using Kontur.Logging;

namespace MetcastDataTransformer
{
	public class MetcastXmlParser : IMetcastXmlParser
	{
		private readonly XDocument metcastDataDocument;
		private readonly ColorConsoleLog log;

		public MetcastXmlParser([NotNull] XDocument metcastDataDocument, ColorConsoleLog log)
		{
			this.metcastDataDocument = metcastDataDocument;
			this.log = log;
		}

		public List<MetcastTownInfo> GetMetcastTownInfos()
		{
			var infos = new List<MetcastTownInfo>();
			foreach (var townElement in metcastDataDocument.XPathSelectElements("//TOWN"))
			{
				var name = townElement.Element("CITYNAME")?.Value;
				try
				{
					var day = int.Parse(townElement.Attribute("day")?.Value);
					var month = int.Parse(townElement.Attribute("month")?.Value);
					var year = int.Parse(townElement.Attribute("year")?.Value);
					var pressure = int.Parse(townElement.Element("PRESSURE")?.Value);
					var temperature = int.Parse(townElement.Element("TEMPERATURE")?.Attribute("value")?.Value);
					var relwet = int.Parse(townElement.Element("RELWET")?.Value);

					infos.Add(new MetcastTownInfo
					{
						Day = day,
						Month = month,
						Name = name,
						Pressure = pressure,
						Relwet = relwet,
						Temperature = temperature,
						Year = year
					});
				}
				catch
				{
					log.Warn($"Некорректные данные для города {name}");
				}
			}

			return infos;
		}
	}
}