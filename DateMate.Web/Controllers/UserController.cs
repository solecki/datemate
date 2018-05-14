using DateMate.Common.Helpers;
using DateMate.Common.Security;
using DateMate.Data.Entities;
using DateMate.Data.Repositories;
using DateMate.Entities;
using DateMate.Models.ViewModels;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DateMate.Controllers
{
	[Authorize]
	public class UserController : BaseController
	{
		// Redirect to profile page.
		public ActionResult Index()
		{
			var loggedInUser = UnitOfWork.Users.GetUserWithPicture(LoggedInUserId);

			// Avoid null pointer. (In case login session is still active but the user no longer
			// exists in the data context.)
			if (LoggedInUserId == -1)
				return RedirectToAction("Logout", "User");

			return RedirectToAction("ProfilePage", "User", new { id = LoggedInUserId });
		}

		public ActionResult ProfilePage(int id = -1)
		{
			var model = new ProfilePageViewModel
			{
				ProfileUser = UnitOfWork.Users.GetUserWithPicture(id),
				LoggedInUser = UnitOfWork.Users.GetUserWithPicture(LoggedInUserId),
				AllFriendships = UnitOfWork.Friendships.GetAllFriendships(LoggedInUserId)
			};

			return View(model);
		}

		public ActionResult Friends(int id = -1)
		{
			if (!UnitOfWork.Users.UserExists(id))
				return RedirectToAction("Friends", "User", new { id = LoggedInUserId });

			FriendsViewModel model = new FriendsViewModel
			{
				AllFriends = UnitOfWork.Friendships.GetFriends(id),
				ReceivedPendingFriendRequests = UnitOfWork.Friendships.GetReceivedFriendRequests(id, FriendshipRequestStatus.Pending),
				SentPendingFriendRequests = UnitOfWork.Friendships.GetSentFriendRequests(id, FriendshipRequestStatus.Pending),
				LoggedInUser = UnitOfWork.Users.GetUserWithPicture(LoggedInUserId),
				ProfileUser = UnitOfWork.Users.GetUserWithPicture(id)
			};

			return View(model);
		}

		// Model for editing a User and Picture entity.
		public ActionResult Edit()
		{
			var loggedInUser = UnitOfWork.Users.GetUserWithPicture(LoggedInUserId);

			return View(loggedInUser);
		}

		// Updates a user's properties and/or its picture, if it survives all validations.
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Edit(User user, HttpPostedFileBase uploadedImage, string newPassword, string passwordConfirmation, string oldPassword)
		{
			var loggedInUser = UnitOfWork.Users.GetUserWithPicture(LoggedInUserId);
			bool updatedPassword = false;

			if (loggedInUser.LoginName != user.LoginName)
			{
				ModelState.AddModelError("", "Changing username is not allowed");
				return View();
			}

			// Age restriction.
			var lowerBoundDate = (DateTime.Today.AddYears(-18));
			var upperBoundDate = (DateTime.Today.AddYears(-100));
			if ((user.BirthDate > lowerBoundDate || user.BirthDate < upperBoundDate))
			{
				ModelState.AddModelError("BirthDate", "You must choose an age between 18 and 100, even if you're just pretending, you sicko");
				return View();
			}

			// Disregard validating the Password property: we'll validate untracked password fields if
			// they're not empty.
			ModelState.Remove("Password");

			if (!string.IsNullOrEmpty(newPassword) || !string.IsNullOrEmpty(passwordConfirmation) || !string.IsNullOrEmpty(oldPassword))
			{
				// Password update: validate.
				if (newPassword != passwordConfirmation || loggedInUser.Password != Encryption.GeneratePasswordHash(oldPassword, loggedInUser.Salt))
				{
					ModelState.AddModelError("", "Make sure your new passwords match each other and that your old password is correct");
					return View();
				}

				// Update the user object's password.
				user.Salt = Encryption.GenerateSalt();
				user.Password = Encryption.GeneratePasswordHash(newPassword, user.Salt);
				updatedPassword = true;
			}

			if (uploadedImage != null && uploadedImage.ContentLength > 0)
			{
				if (uploadedImage.ContentLength > 3000000)
				{
					// File too large.
					ModelState.AddModelError("", "The image file size must be less than 3 MB");
					return View();
				}

				// New image valid: Remove old image from disc, save the new one, and create a new
				// Picture object for the logged in user.
				var iu = new ImageUpload(uploadedImage);
				if (iu.ValidateImage())
				{
					if (UnitOfWork.Pictures.GetPicture(user.Id) != null)
					{
						if (System.IO.File.Exists(Server.MapPath("~/Content/Media/Profile-pictures/" + UnitOfWork.Pictures.GetPicture(user.Id).FileName)))
						{
							System.IO.File.Delete(Server.MapPath("~/Content/Media/Profile-pictures/" + UnitOfWork.Pictures.GetPicture(user.Id).FileName));
						}
					}

					// Create new Picture object to update the data context with.
					var picture = new Picture
					{
						FileName = iu.FileName,
						Id = user.Id
					};
					UnitOfWork.Pictures.Edit(picture);

					var path = Path.Combine(Server.MapPath("~/Content/Media/Profile-pictures/"), iu.FileName);
					iu.Image.SaveAs(path);
				}
				else
				{
					ModelState.AddModelError("", "You must choose an image with one of the following file extensions: .gif, .jpg, .jpeg, .png");
					return View();
				}
			}

			if (!updatedPassword)
			{
				// Add old password and salt to user object's properties since it's null from post request.
				user.Password = loggedInUser.Password;
				user.Salt = loggedInUser.Salt;
			}

			UnitOfWork.Users.Edit(user);
			UnitOfWork.Complete();

			TempData["Heading"] = "<h1>Account updated</h1>";
			TempData["Message"] = "<p>Your account was successfully updated!</p>";

			return RedirectToAction("Index", "Status");
		}

		[AllowAnonymous]
		public ActionResult Register()
		{
			if (User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "User");

			return View();
		}

		[AllowAnonymous]
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Register(User user, HttpPostedFileBase uploadedImage, string passwordConfirmation)
		{
			if (UnitOfWork.Users.GetByUsername(user.LoginName) != null)
			{
				ModelState.AddModelError("loginName", "The username already exists");
				return View();
			}

			// Age restriction.
			var lowerBoundDate = (DateTime.Today.AddYears(-18));
			var upperBoundDate = (DateTime.Today.AddYears(-100));
			if ((user.BirthDate > lowerBoundDate || user.BirthDate < upperBoundDate))
			{
				ModelState.AddModelError("BirthDate", "You must be between the age of 18 and 100 to fool around, you dummy");
				return View();
			}

			if (ModelState.IsValid && user.Password == passwordConfirmation)
			{
				if (uploadedImage != null && uploadedImage.ContentLength > 0)
				{
					if (uploadedImage.ContentLength > 3000000)
					{
						// File too large.
						ModelState.AddModelError("", "The image file size must be less than 3 MB");
						return View();
					}

					// Valid image uploaded: save image to disc and create Picture object to store in data context.
					var iu = new ImageUpload(uploadedImage);
					if (iu.ValidateImage())
					{

						user.Picture = new Picture
						{
							FileName = iu.FileName,
							Id = user.Id
						};

						var path = Path.Combine(Server.MapPath("~/Content/Media/Profile-pictures/"), iu.FileName);
						iu.Image.SaveAs(path);
					}
					else
					{
						ModelState.AddModelError("", "You must choose an image with one of the following file extensions: .gif, .jpg, .jpeg, .png");
						return View();
					}
				}

				// Create hash 'n' salt for password.
				user.Salt = Encryption.GenerateSalt();
				user.Password = Encryption.GeneratePasswordHash(user.Password, user.Salt);

				user.Searchable = true;
				UnitOfWork.Users.Add(user);
				UnitOfWork.Complete();

				TempData["Heading"] = "<h1>Successful registration</h1>";
				TempData["Message"] = "<p>Your user account was successfully registered! You can log in <a href=\"/User/Login/\">here</a>.</p>";
				return RedirectToAction("Index", "Status");
			}

			// If here: modelstate is valid but passwords don't match each other.
			ModelState.AddModelError("Password", "The passwords you entered did not match");

			return View();
		}

		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			if (User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "User");

			if (!string.IsNullOrEmpty(returnUrl))
				ViewBag.ReturnUrl = returnUrl;

			return View();
		}

		[AllowAnonymous]
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Login(string loginName, string password, string returnUrl)
		{
			var user = UnitOfWork.Users.GetByUsername(loginName);

			if (user != null)
			{
				// Provided login name exists in database: try to get data context match together with password.
				var hashedPassword = Encryption.GeneratePasswordHash(password, user.Salt);
				var dbUser = UnitOfWork.Users.Login(loginName, hashedPassword);

				if (dbUser != null)
				{
					// Login successfull: Create login cookie.
					FormsAuthentication.SetAuthCookie(user.LoginName, true);
					LoggedInUserId = dbUser.Id;

					TempData["Heading"] = "<h1>Login successful</h1>";
					TempData["Message"] = "<p>Welcome or whatever.</p>";

					return Redirect("http://" + Request.Url.Authority + returnUrl);
				}
			}

			// Password and/or login name didn't check out.
			ModelState.AddModelError("", "Wrong username or password");

			return View();
		}

		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			LoggedInUserId = -1;
			TempData["Heading"] = "<h1>Logged out</h1>";
			TempData["Message"] = "<p>Thanks for visiting, and welcome back whenever you're in the mood for some tushy.</p>";
			return RedirectToAction("Index", "Status");
		}
	}
}