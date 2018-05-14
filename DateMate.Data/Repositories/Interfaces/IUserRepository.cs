using DateMate.Entities;
using System.Collections.Generic;

namespace DateMate.Data.Repositories.Interfaces
{
	//	Repository methods for the User entity.
	public interface IUserRepository : IRepository<User>
	{
		User GetByUsername(string name);
		IEnumerable<User> SearchUsersByName(string name, int resultSize);
		IEnumerable<User> GetRandomUsers(int resultSize);
		User GetUserIncludeMessages(int userId);
		User GetUserWithPicture(int userId);
		User Login(string username, string password);
		bool UserExists(int userId);
	}
}
