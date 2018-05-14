using DateMate.Data.Context;
using DateMate.Data.Entities;
using DateMate.Data.Repositories.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DateMate.Data.Repositories
{
	public class PictureRepository : Repository<Picture>, IPictureRepository
	{
		public DateMateContext DateMateContext
		{
			get { return DataContext as DateMateContext; }
		}

		public PictureRepository(DbContext context) : base(context) { }

		public new void Edit(Picture picture)
		{
			DateMateContext.Pictures.AddOrUpdate(picture);
		}

		// Returns Picture for specified user id.
		public Picture GetPicture(int userId)
		{
			return DateMateContext.Pictures
				.Where(p => p.Id == userId)
				.SingleOrDefault();
		}

	}
}
