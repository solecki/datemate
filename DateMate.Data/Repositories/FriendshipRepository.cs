using DateMate.Data.Context;
using DateMate.Data.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DateMate.Data.Repositories.Interfaces
{
	public class FriendshipRepository : Repository<Friendship>, IFriendshipRepository
	{
		public DateMateContext DateMateContext
		{
			get { return DataContext as DateMateContext; }
		}

		public FriendshipRepository(DbContext context) : base(context) { }

		// Returns list of all friendships (i.e. independent of friendship status) for the specified user id.
		public IEnumerable<Friendship> GetAllFriendships(int userId)
		{
			return DateMateContext.Friendships
				.Where(f => f.RequestToId == userId || f.RequestById == userId)
				.ToList();
		}

		// Returns list of confirmed friendships for the specified user id.
		public IEnumerable<Friendship> GetFriends(int userId)
		{
			return DateMateContext.Friendships
				.Where(f => f.Status == FriendshipRequestStatus.Confirmed && (f.RequestById == userId || f.RequestToId == userId))
				.Include(f => f.RequestBy)
				.Include(f => f.RequestTo)
				.ToList();
		}

		public IEnumerable<Friendship> GetReceivedFriendRequests(int userId, FriendshipRequestStatus status)
		{
			return DateMateContext.Friendships
				.Where(f => f.RequestToId == userId && f.Status == status)
				.Include(f => f.RequestBy)
				.ToList();
		}

		public IEnumerable<Friendship> GetSentFriendRequests(int userId, FriendshipRequestStatus status = FriendshipRequestStatus.Confirmed)
		{
			return DateMateContext.Friendships
				.Where(f => f.RequestById == userId && f.Status == status)
				.Include(f => f.RequestTo)
				.ToList();
		}
	}
}
