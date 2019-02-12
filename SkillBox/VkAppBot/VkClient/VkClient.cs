using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using VkNet;
using VkNet.Exception;
using VkNet.Utils;

namespace VkAppBot
{
	public class VkClient : IVkClient
	{
		private readonly VkApi vkApi;

		public VkClient(VkApi vkApi)
		{
			this.vkApi = vkApi;
		}

		public bool TryGetMembersIdsFromGroup(string groupId, int offset, int count, out IEnumerable<int> membersIds)
		{
			var parameters = new VkParameters
			{
				{"group_id", groupId},
				{ "offset", offset},
				{ "count", count}
			};

			try
			{
				var response = JObject.Parse(vkApi.Call("groups.getMembers", parameters).RawJson);
				membersIds = response["response"]["items"].Select(token => token.Value<int>());
				return true;
			}
			catch (InvalidGroupIdException)
			{
				membersIds = new List<int>();
				return false;
			}
		}

		public IEnumerable<string> GetUsersCities(params string[] ids)
		{
			var parameters = new VkParameters
			{
				{"user_ids", string.Join(",", ids)},
				{"fields", "city" }
			};

			var response = JObject.Parse(vkApi.Call("users.get", parameters).RawJson)["response"];

			foreach (var userItem in response)
			{
				var cityObj = userItem["city"];
				if (cityObj == null)
					yield return null;
				else
				{
					yield return cityObj["title"].Value<string>();
				}
			}
		}

		//key - city id, value - city name 
		public Dictionary<string, string> GetCitiesNamesByIds(params string[] ids)
		{
			var parameters = new VkParameters
			{
				{"city_ids", string.Join(",", ids.Distinct())}
			};

			var response = JObject.Parse(vkApi.Call("database.getCitiesById", parameters).RawJson)["response"];

			var dictionary = new Dictionary<string, string>();
			foreach (var cityItem in response)
			{
				var id = cityItem["id"].Value<string>();
				var name = cityItem["title"].Value<string>();
				dictionary.Add(id, name);
			}

			return dictionary;
		}
	}
}