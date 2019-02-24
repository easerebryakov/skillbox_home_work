using System;
using System.Diagnostics;
using System.Linq;
using TweetSharp;

namespace TwitterConsole
{
	public class Program
	{
		public static void Main(string[] args)
		{
			const string consumerKey = "";
			const string consumerSecret = "";
			var service = new TwitterService(consumerKey, consumerSecret);

			var requestToken = service.GetRequestToken();

			var uri = service.GetAuthorizationUri(requestToken);
			Process.Start(uri.ToString());

			var verifier = Console.ReadLine();
			var access = service.GetAccessToken(requestToken, verifier);

			service.AuthenticateWith(access.Token, access.TokenSecret);

			var tweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions()).ToList();
			const int maxShowCount = 15;
			var tweetsFromServiceCount = tweets.Count;
			var tweetsShowCount = maxShowCount <= tweetsFromServiceCount
				? maxShowCount
				: tweetsFromServiceCount;

			var nowDateTime = DateTime.Now;

			for (var i = tweetsShowCount - 1; i >= 0; i--)
			{
				var tweet = tweets[i];
				var creatingDate = tweet.CreatedDate;
				var ageDescriprion = DateTimeCalculator.GetAgeDescription(creatingDate, nowDateTime);
				Console.WriteLine($"{tweet.Text} - {ageDescriprion}");
				Console.WriteLine();
			}

			var trends = service.ListLocalTrendsFor(new ListLocalTrendsForOptions { Id = 1 }); // 1 - весь мир

			foreach (var trend in trends)
			{
				Console.WriteLine(trend.Name);
				Console.WriteLine();
			}
		}
	}
}
