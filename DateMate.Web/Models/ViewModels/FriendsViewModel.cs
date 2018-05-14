using DateMate.Data.Entities;
using DateMate.Entities;
using System.Collections.Generic;

namespace DateMate.Models.ViewModels
{
	// All relevant friendship data. Both the profile user and the logged in user is needed
	// to figure out which information to display from a friendship object since they consist
	// of both ends of a friendship, i.e. two users.
	public class FriendsViewModel
	{
		public IEnumerable<Friendship> AllFriends { get; set; }
		public IEnumerable<Friendship> ReceivedPendingFriendRequests { get; set; }
		public IEnumerable<Friendship> SentPendingFriendRequests { get; set; }
		public User LoggedInUser { get; set; }
		public User ProfileUser { get; set; }
	}
}