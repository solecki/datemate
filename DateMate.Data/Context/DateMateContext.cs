using DateMate.Data.Entities;
using DateMate.Entities;
using System.Data.Entity;

namespace DateMate.Data.Context
{
	public class DateMateContext : DbContext
	{
		public DateMateContext() : base("DateMateConnection") { }

		public DbSet<User> Users { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Friendship> Friendships { get; set; }
		public DbSet<Picture> Pictures { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// One-to-many relationship between User and Messages.
			modelBuilder.Entity<User>()
				.HasMany(u => u.Messages)
				.WithRequired(u => u.To)
				.WillCascadeOnDelete();

			// Two one-to-many relationships between User and Friendship.
			modelBuilder.Entity<User>()
				.HasMany(u => u.Friends)
				.WithRequired(f => f.RequestBy)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(u => u.Friends)
				.WithRequired(f => f.RequestTo)
				.WillCascadeOnDelete(false);

			// Composite key for friendship table since we only can have one friendship between the same users.
			modelBuilder.Entity<Friendship>()
				.HasKey(f => new { f.RequestById, f.RequestToId });

			base.OnModelCreating(modelBuilder);
		}
	}
}
