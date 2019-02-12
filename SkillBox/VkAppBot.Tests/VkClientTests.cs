using System;
using System.Linq;
using Kontur.Logging;
using NUnit.Framework;
using VkNet;

namespace VkAppBot.Tests
{
	[TestFixture]
    public class VkClientTests
    {
	    private VkClient vkClient;

		[OneTimeSetUp]
		public void Setup()
		{
			var fakeLog = new FakeLog();
			var vkApi = new VkApi();
			var auth = new VkAuthorizer(vkApi, fakeLog, "", "");
			var authResult = auth.TryAuthorize();
			if (!authResult)
				throw new Exception("Не прошла авторизация");
			vkClient = new VkClient(vkApi);
		}

		[Test]
		public void GetMembers_TakeMembers_ShouldBeAny()
		{
			vkClient.TryGetMembersIdsFromGroup("ru_3dnews", 0, 100, out var membersIds);
			Assert.IsTrue(membersIds.Any());
		}

		[Test]
		public void GetCityId_ShouldBe_EqualsIds()
		{
			var users = vkClient.GetUsersCities("hermit_esa");
			Assert.AreEqual("Екатеринбург", users.ToList()[0]);
		}

		[Test]
		public void GetCityName()
		{
			var names = vkClient.GetCitiesNamesByIds("1", "2");
			Assert.AreEqual(names["1"], "Москва");
			Assert.AreEqual(names["2"], "Санкт-Петербург");
		}
    }
}
