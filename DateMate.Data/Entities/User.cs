using DateMate.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DateMate.Entities
{
	//	Models a user account.
	public class User
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "First name is required")]
		[StringLength(64, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 64 characters")]
		[Display(ResourceType = typeof(Resources.Entity), Name = nameof(Resources.Entity.FirstName))]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Surname is required")]
		[StringLength(64, MinimumLength = 2, ErrorMessage = "Surname must be between 2 and 64 characters")]
		[Display(ResourceType = typeof(Resources.Entity), Name = nameof(Resources.Entity.Surname))]
		public string Surname { get; set; }

		[Required(ErrorMessage = "Birth date is required")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[DataType(DataType.Date)]
		[Display(ResourceType = typeof(Resources.Entity), Name = nameof(Resources.Entity.BirthDate))]
		public DateTime BirthDate { get; set; }

		[Required(ErrorMessage = "Username is required")]
		[StringLength(64, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 64 characters")]
		[Index(IsUnique = true)]
		[Display(ResourceType = typeof(Resources.Entity), Name = nameof(Resources.Entity.LoginName))]
		public string LoginName { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[StringLength(64, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 64 characters")]
		[DataType(DataType.Password)]
		[Display(ResourceType = typeof(Resources.Entity), Name = nameof(Resources.Entity.Password))]
		public string Password { get; set; }

		public string Salt { get; set; }

		[EmailAddress(ErrorMessage = "Enter a valid e-mail address")]
		[StringLength(256)]
		[Display(ResourceType = typeof(Resources.Entity), Name = nameof(Resources.Entity.EmailAddress))]
		//[Index(IsUnique = true)]
		public string EmailAddress { get; set; }
		[Display(ResourceType = typeof(Resources.Entity), Name = nameof(Resources.Entity.Searchable))]
		public bool Searchable { get; set; }

		public ICollection<Friendship> Friends { get; set; }
		public ICollection<Message> Messages { get; set; }

		[Display(ResourceType = typeof(Resources.Entity), Name = nameof(Resources.Entity.Picture))]
		public virtual Picture Picture { get; set; }

		public override string ToString()
		{
			return FirstName + " " + Surname;
		}
	}
}
