using JetBrains.Annotations;

namespace TwitterConsole.Objects
{
	public class TwitterManager
	{
		[CanBeNull]
		public TwitterAccount TwitterAccount { get; private set; }

		public (bool isSuccess, string message) TryAuthenticate(string login, string password)
		{
			var tw = new TwitterAuthenticator(login, password);
			var account = tw.Authenticate();
			if (account == null)
				return (false, "Authenticate error");

			TwitterAccount = account;
			return (true, null);
		}

		public void SendTwitt(string message)
		{
			var newTwitt = new Twitt(message);
		}

		public void ExitFromAccount()
		{
			TwitterAccount = null;
		}
	}
}