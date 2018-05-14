using System.Web.Mvc;

namespace DateMate.Controllers
{
	// Controller acting as a mediator for displaying messages from other controllers
	// through the server side. Messages can be sent to this controller through ViewBag's
	// Heading and Message properties.
	public class StatusController : BaseController
    {
        public ActionResult Index()
        {
			ViewBag.Heading = TempData["Heading"];
			ViewBag.Message = TempData["Message"];

			return View();
        }
    }
}