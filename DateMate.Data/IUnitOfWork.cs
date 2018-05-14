using DateMate.Data.Repositories.Interfaces;
using System;

namespace DateMate.Data.Repositories
{
	public interface IUnitOfWork : IDisposable
	{
		// Are interfaces to allow for unit testing, so this interface can be mocked for that purpose.
		IUserRepository Users { get; }
		IMessageRepository Messages { get; }
		IFriendshipRepository Friendships { get; }
		IPictureRepository Pictures { get; }

		// Save changes to the data context upon unit of work completion.
		int Complete();
	}
}
