using JetBrains.Annotations;

namespace TwitterConsole.Objects
{
	public class TwitterAuthenticator
	{
		private readonly string login;
		private readonly string password;

		public TwitterAuthenticator(string login, string password)
		{
			this.login = login;
			this.password = password;
		}

		[CanBeNull]
		public TwitterAccount Authenticate()
		{
			return new TwitterAccount("name");
		}
	}
}