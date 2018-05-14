using DateMate.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DateMate.Data.Entities
{
	public class Picture
	{
		// Has a one-to-zero-or-one relationship to User, hence the PK is also a FK.
		[ForeignKey("User")]
		public int Id { get; set; }
		[Required]
		public string FileName { get; set; }
		public virtual User User { get; set; }

		public override string ToString()
		{
			return FileName;
		}
	}
}
