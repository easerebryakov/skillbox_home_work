using System.Collections.Generic;

namespace VkAppBot
{
	public interface IVkClient
	{
		bool TryGetMembersIdsFromGroup(string groupId, int offset, int count, out IEnumerable<int> membersIds);

		IEnumerable<string> GetUsersCities(params string[] ids);

		Dictionary<string, string> GetCitiesNamesByIds(params string[] ids);
	}
}