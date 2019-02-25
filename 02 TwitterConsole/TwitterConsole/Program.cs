using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

			#region tweets

			var tweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions()).ToList();
			const int maxShowCount = 15;
			var tweetsFromServiceCount = tweets.Count;
			var tweetsShowCount = maxShowCount <= tweetsFromServiceCount
				? maxShowCount
				: tweetsFromServiceCount;

			var nowDateTime = DateTime.UtcNow;

			for (var i = tweetsShowCount - 1; i >= 0; i--)
			{
				var tweet = tweets[i];
				var creatingDate = tweet.CreatedDate;
				var ageDescriprion = DateTimeCalculator.GetAgeDescription(creatingDate, nowDateTime);
				var hashTagsDescription = GetHashTagsDescription(tweet.Text);
				Console.WriteLine($"{tweet.Text} - {ageDescriprion}");
				Console.WriteLine(hashTagsDescription);
			}

			#endregion

			#region trends

			Console.WriteLine("Тренды:");
			var trends = service.ListLocalTrendsFor(new ListLocalTrendsForOptions { Id = 1 }); // 1 - весь мир

			foreach (var trend in trends)
			{
				Console.WriteLine(trend.Name);
				Console.WriteLine();
			}

			var trendsSharpString = GetTrendsSharpString(trends);
			Console.WriteLine(trendsSharpString);

			#endregion
		}

		private static string GetHashTagsDescription(string tweetText)
		{
			var regex = new Regex(@"#(\w+)");
			var matchesCollection = regex.Matches(tweetText);
			if (matchesCollection.Count == 0)
				return string.Empty;
			var result = matchesCollection
				.Cast<Match>()
				.Select(match => match.Value)
				.Aggregate((match, match1) => $"{match}\n{match1}");
			return $"{result}\n";
		}

		private static string GetTrendsSharpString(TwitterTrends trends)
		{
			const string separator = " ***** ";
			var strBuilder = new StringBuilder();
			foreach (var trend in trends)
			{
				if (trend.Name.StartsWith("#"))
					strBuilder.Append($"{trend.Name}{separator}");
			}

			var result = strBuilder.ToString().SubstringBeforeLastIndex(separator);
			return result;
		}
	}
}
