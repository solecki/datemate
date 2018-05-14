using DateMate.Entities;
using System.Collections.Generic;

namespace DateMate.Data.Repositories.Interfaces
{
	public interface IMessageRepository : IRepository<Message>
	{
		List<Message> GetMessages(int userId);
	}
}
