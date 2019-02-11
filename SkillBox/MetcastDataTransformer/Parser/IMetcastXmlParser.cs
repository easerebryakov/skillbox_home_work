using System.Collections.Generic;

namespace MetcastDataTransformer
{
	public interface IMetcastXmlParser
	{
		List<MetcastTownInfo> GetMetcastTownInfos();
	}
}