using DateMate.Data.Entities;
using DateMate.Entities;
using System.Collections.Generic;

namespace DateMate.Models.ViewModels
{
	public class ProfilePageViewModel
	{
		public User ProfileUser { get; set; }
		public User LoggedInUser { get; set; }
		// To keep track of which users the logged in user is able to send friend requests to.
		public IEnumerable<Friendship> AllFriendships { get; set; }
	}
}