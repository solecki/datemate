using DateMate.Data.Context;
using DateMate.Data.Repositories.Interfaces;
using DateMate.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DateMate.Data.Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(DateMateContext context) : base(context) { }

		public DateMateContext DateMateContext
		{
			get { return DataContext as DateMateContext; }
		}

		// Checks if a user exists by looking up specified id.
		public bool UserExists(int userId) => DateMateContext.Users.Any(u => u.Id == userId);

		public User GetByUsername(string username) => DateMateContext.Users
			.SingleOrDefault(u => u.LoginName == username);

		//	Retrieves all users whose name, including the login name, contains the specified search term.
		public IEnumerable<User> SearchUsersByName(string name, int resultSize)
		{
			return DateMateContext.Users
				.Where(u => (u.FirstName + u.Surname + u.LoginName).Contains(name) &&
					u.Searchable == true)
				.Take(resultSize)
				.ToList();
		}

		// Retrieves random users with the searchable flag set to true.
		public IEnumerable<User> GetRandomUsers(int resultSize)
		{
		return DateMateContext.Users
				.OrderBy(u => Guid.NewGuid())
				.Where(u => u.Searchable != false)
				.Include(u => u.Picture)
				.Take(resultSize)
				.ToList();
		}

		// Retrieves a user and eager loads its messages.
		public User GetUserIncludeMessages(int userId)
		{
			return DateMateContext.Users
				.Where(u => u.Id == userId)
				.Include(u => u.Messages)
				.FirstOrDefault();
		}

		public User GetUserWithPicture(int userId)
		{
			return DateMateContext.Users
				.Where(u => u.Id == userId)
				.Include(u => u.Picture)
				.SingleOrDefault();
		}

		//	Returns a User entity if provided username and password matches the data context's
		//	corresponding values.
		public User Login(string username, string password)
		{
			return DateMateContext.Users
				.Where(u => u.LoginName == username && u.Password == password)
				.SingleOrDefault();
		}
	}
}
