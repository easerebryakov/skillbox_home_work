using System.Collections.Generic;
using System.Xml.Linq;

namespace MetcastDataTransformer
{
	public class DataTransformer : IDataTransformer
	{
		private readonly List<MetcastTownInfo> metcastTownInfos;

		public DataTransformer(List<MetcastTownInfo> metcastTownInfos)
		{
			this.metcastTownInfos = metcastTownInfos;
		}

		public XDocument Transform()
		{
			//key - temperature, value - count city
			var temperatureDict = new SortedDictionary<int, int>();

			var dataEl = new XElement("Data");
			var newDocument = new XDocument(dataEl);

			foreach (var metcastTownInfo in metcastTownInfos)
			{
				var temperature = metcastTownInfo.Temperature;
				if (temperatureDict.ContainsKey(temperature))
					temperatureDict[temperature] += 1;
				else
					temperatureDict.Add(temperature, 1);
			}

			foreach (var temperatureItem in temperatureDict)
			{
				dataEl
					.AddElementAndReturnHim("City")
					.AddElementAndReturnSelf("Temperature", temperatureItem.Key.ToString())
					.AddElementAndReturnSelf("Count", temperatureItem.Value.ToString());
			}

			return newDocument;
		}
	}
}