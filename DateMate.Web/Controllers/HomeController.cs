using DateMate.Data.Repositories;
using System.Web.Mvc;

namespace DateMate.Controllers
{
	public class HomeController : BaseController
	{
		public ActionResult Index()
		{
			// Searchable users to display on the front page.
			var randUsers = UnitOfWork.Users.GetRandomUsers(10);

			return View(randUsers);
		}
	}
}