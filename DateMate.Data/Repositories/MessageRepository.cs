using DateMate.Data.Context;
using DateMate.Data.Repositories.Interfaces;
using DateMate.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DateMate.Data.Repositories
{
	public class MessageRepository : Repository<Message>, IMessageRepository
	{
		public DateMateContext DateMateContext
		{
			get { return DataContext as DateMateContext; }
		}

		public MessageRepository(DbContext context) : base(context) { }

		// Returns all messages for specified user id.
		public List<Message> GetMessages(int userId)
		{
			return DateMateContext.Messages
				.Where(m => m.To.Id == userId)
				.ToList();
		}
	}
}
