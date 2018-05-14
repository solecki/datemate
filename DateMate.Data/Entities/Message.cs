using System;
using System.ComponentModel.DataAnnotations;

namespace DateMate.Entities
{
	public class Message
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Message can't be empty")]
		[Display(Name = "Message")]
		public string Text { get; set; }

		public DateTime TimeStamp { get; set; }

		public int? UserId { get; set; } // Needs to be nullable for cascade on delete to work.
		public virtual User User { get; set; }
		public int ToId { get; set; }
		public User To { get; set; }
	}
}