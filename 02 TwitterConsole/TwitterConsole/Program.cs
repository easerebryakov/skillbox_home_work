using System;
using System.Collections.Generic;
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
			try
			{
				Run();
			}
			catch (TwitterApiException e)
			{
				Console.WriteLine($"При работе с Api твиттера произошла ошибка:\n{e}");
			}
			catch (Exception e)
			{
				Console.WriteLine($"При работе приложения произошла неизвестная ошибка:\n{e}");
			}
		}

		private static void Run()
		{
			var configuration = new TwitterAppConfiguration();
			var service = new TwitterService(configuration.ConsumerKey, configuration.ConsumerSecret);
			var requestToken = service.Execute(ts => ts.GetRequestToken());
			var uri = service.Execute(ts => ts.GetAuthorizationUri(requestToken));
			Process.Start(uri.ToString());

			var verifier = Console.ReadLine();
			var access = service.Execute(ts => ts.GetAccessToken(requestToken, verifier));

			service.Execute(ts => ts.AuthenticateWith(access.Token, access.TokenSecret));

			#region tweets

			var tweets = service.Execute(ts => ts.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions()).ToList());
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
			var trendsNames = service
				.Execute(ts => ts.ListLocalTrendsFor(new ListLocalTrendsForOptions { Id = 1 }))
				.Select(t => t.Name); // 1 - весь мир

			foreach (var trendName in trendsNames)
			{
				Console.WriteLine(trendName);
				Console.WriteLine();
			}

			var trendsSharpString = GetTrendsSharpString(trendsNames);
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

		private static string GetTrendsSharpString(IEnumerable<string> trendsNames)
		{
			const string separator = " ***** ";
			var strBuilder = new StringBuilder();
			foreach (var trendName in trendsNames)
			{
				if (trendName.StartsWith("#"))
					strBuilder.Append($"{trendName}{separator}");
			}

			var result = strBuilder.ToString().SubstringBeforeLastIndex(separator);
			return result;
		}
	}
}
