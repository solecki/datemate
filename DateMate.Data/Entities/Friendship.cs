using DateMate.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DateMate.Data.Entities
{
	// Models a relationship between two users.
	public class Friendship
	{
		public int RequestById { get; set; }
		[Column(Order = 0)]
		public User RequestBy { get; set; }

		public int RequestToId { get; set; }
		[Column(Order = 1)]
		public User RequestTo { get; set; }

		public FriendshipRequestStatus Status { get; set; }
	}

	// The status states of a friendship request.
	public enum FriendshipRequestStatus
	{
		Pending = 1,
		Confirmed,
		Declined
	}
}
