using DateMate.Data.Entities;

namespace DateMate.Data.Repositories.Interfaces
{
	public interface IPictureRepository : IRepository<Picture>
	{
		Picture GetPicture(int userId);
	}
}
