using DateMate.Data.Context;
using DateMate.Data.Repositories.Interfaces;

namespace DateMate.Data.Repositories
{
	// Keeping track of changes between data context transactions.
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DateMateContext _context;
		public IUserRepository Users { get; private set; }
		public IMessageRepository Messages { get; private set; }
		public IFriendshipRepository Friendships { get; private set; }
		public IPictureRepository Pictures { get; private set;}

		public UnitOfWork(DateMateContext context)
		{
			// The same context will be used for all repositories.
			_context = context;
			Users = new UserRepository(_context);
			Messages = new MessageRepository(_context);
			Friendships = new FriendshipRepository(_context);
			Pictures = new PictureRepository(_context);
			_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
		}

		public int Complete()
		{
			return _context.SaveChanges();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
