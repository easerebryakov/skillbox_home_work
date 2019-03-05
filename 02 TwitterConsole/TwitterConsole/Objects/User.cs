namespace TwitterConsole.Objects
{
	public class User
	{
		public string FirstName { get; }

		public string SecondName { get; }

		public TwitterAccount Account { get; }

		public User(string firstName, string secondName, TwitterAccount account)
		{
			FirstName = firstName;
			SecondName = secondName;
			Account = account;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is User user))
				return false;
			return Account.UserName == user.Account.UserName;
		}
	}
}