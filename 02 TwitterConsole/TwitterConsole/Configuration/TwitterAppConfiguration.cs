using AppConfigurations;

namespace TwitterConsole
{
	[Configuration("settings/config")]
	public class TwitterAppConfiguration : AppConfiguration
	{
		[Setting("consumerKey")]
		public string ConsumerKey { get; set; }

		[Setting("consumerSecret")]
		public string ConsumerSecret { get; set; }
	}
}