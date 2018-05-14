using DateMate.Data.Context;
using DateMate.Data.Repositories;
using DateMate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace DateMate.Controllers.Api
{
	// RESTfulisch API controller for managing Message entities.
	[Authorize]
	public class MessageController : ApiController
    {
		private UnitOfWork _unitOfWork;

		public MessageController()
		{
			_unitOfWork = new UnitOfWork(new DateMateContext());
		}

		// Returns HTTP response with list of messages for specified *user id*.
		[ResponseType(typeof(List<Post>))]
		public IHttpActionResult Get(int id)
		{
			// Response data.
			var messageList = _unitOfWork.Messages
				.GetMessages(id)
				.Select(m => new Post
				{
					From = m.User.FirstName + " " + m.User.Surname,
					FromId = m.User.Id,
					TimeStamp = m.TimeStamp.ToString("yyyy/MM/dd hh:mm:ss tt"),
					Text = m.Text
				})
				.ToList();

			if (messageList.Count > 0)
				return Ok(messageList);

			// No Posts for id exists.
			return StatusCode(HttpStatusCode.NoContent);
		}

		// Adds a new message to the data contex.
		[ResponseType(typeof(Message))]
		public IHttpActionResult Post([FromBody]Message message)
        {
			var date = DateTime.Now;
			// Response data.
			var returnResource = new Message
			{
				UserId = message.UserId,
				ToId = message.ToId,
				TimeStamp = date,
				Text = message.Text
			};

			// Prevent message post forgery.
			int loggedInUserId = _unitOfWork.Users.GetByUsername(User.Identity.Name).Id;
			if (!ModelState.IsValid || loggedInUserId != returnResource.UserId)
				// Return the generic HTTP 400 status code since the client request was invalid.
				return Content(HttpStatusCode.BadRequest, "Invalid data.");

			message.Text = HttpContext.Current.Server.HtmlEncode(message.Text);
			message.TimeStamp = date;

			_unitOfWork.Messages.Add(message);
			_unitOfWork.Complete();

			return Created(Request.RequestUri, returnResource);
        }

		// Dispose data context on controller disposal.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
				_unitOfWork.Dispose();
			base.Dispose(disposing);
		}
	}

	// Message resource model.
	public class Post
	{
		public string Text { get; set; }
		public string From { get; set; }
		public int FromId { get; set; }
		public string TimeStamp { get; set; }
	}
}