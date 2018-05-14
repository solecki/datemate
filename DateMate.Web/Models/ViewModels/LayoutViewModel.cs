using DateMate.Data.Entities;
using System.Collections.Generic;

namespace DateMate.Models.ViewModels
{
	// Data that might be needed for every view.
	public class LayoutViewModel
	{
		public IEnumerable<Friendship> ReceivedPendingFriendRequests { get; set; }
	}
}