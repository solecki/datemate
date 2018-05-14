using DateMate.Data.Context;
using DateMate.Data.Entities;
using DateMate.Data.Repositories;
using DateMate.Models.ViewModels;
using System.Web.Mvc;

namespace DateMate.Controllers
{
	// Instatiates and provides read-only access to the UnitOfWork, and disposes
	// its data context when the controller is disposed.
	public class BaseController : Controller
	{
		protected UnitOfWork UnitOfWork { get; }
		public LayoutViewModel LayoutViewModel { get; set; }
		protected int LoggedInUserId { get; set; } // Just a convenient way of getting the id quickly. -1 == unauthorized.

		public BaseController()
		{
			UnitOfWork = new UnitOfWork(new DateMateContext());
			LayoutViewModel = new LayoutViewModel();
			LoggedInUserId = -1;

			// Data for all controllers and the layout view.
			if (System.Web.HttpContext.Current.Request.IsAuthenticated)
			{
				LoggedInUserId = UnitOfWork.Users.GetByUsername(System.Web.HttpContext.Current.User.Identity.Name).Id;
				LayoutViewModel.ReceivedPendingFriendRequests = UnitOfWork.Friendships.GetReceivedFriendRequests(LoggedInUserId, FriendshipRequestStatus.Pending);

				ViewData["LayoutViewModel"] = LayoutViewModel;
			}
		}

		// Dispose data context on controller disposal.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
				UnitOfWork.Dispose();

			base.Dispose(disposing);
		}
	}
}