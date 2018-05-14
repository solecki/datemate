using DateMate.Data.Entities;
using System.Collections.Generic;

namespace DateMate.Data.Repositories.Interfaces
{
	public interface IFriendshipRepository : IRepository<Friendship>
	{
		IEnumerable<Friendship> GetAllFriendships(int userId);
		IEnumerable<Friendship> GetFriends(int userId);
		IEnumerable<Friendship> GetReceivedFriendRequests(int userId, FriendshipRequestStatus status);
		IEnumerable<Friendship> GetSentFriendRequests(int userId, FriendshipRequestStatus status);
	}
}
