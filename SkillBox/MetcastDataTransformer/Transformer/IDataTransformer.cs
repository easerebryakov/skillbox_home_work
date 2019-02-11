using System.Xml.Linq;

namespace MetcastDataTransformer
{
	public interface IDataTransformer
	{
		XDocument Transform();
	}
}