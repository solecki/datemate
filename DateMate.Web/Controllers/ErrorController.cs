using DateMate.Controllers;
using System.Web.Mvc;

namespace DateMate.Web.Controllers
{
	// Controller to handle server's HTTP errors.
	public class ErrorController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult NotFound()
		{
			Response.StatusCode = 404;

			return View();
		}

		public ActionResult InternalError()
		{
			Response.StatusCode = 500;

			return View();
		}
    }
}