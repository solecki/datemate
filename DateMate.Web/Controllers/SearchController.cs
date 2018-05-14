using System.Web.Mvc;

namespace DateMate.Controllers
{
	[Authorize]
	public class SearchController : BaseController
    {
		[ValidateInput(false)]
		public ActionResult Index(string q = "")
		{
			q = Server.HtmlEncode(q);
			if (q.Trim(' ').Length < 3)
			{
				// Short search queries might load the server too much: Abort search. 
				TempData["Heading"] = "<h1>Search error</h1>";
				TempData["Message"] = "<p>You have to use <em>more than two characters</em> when searching for a user.</p>";

				return RedirectToAction("Index", "Status");
			}

			ViewBag.SearchString = q;
			var foundUsers = UnitOfWork.Users.SearchUsersByName(q, 20);

			return View(foundUsers);
		}
	}
}