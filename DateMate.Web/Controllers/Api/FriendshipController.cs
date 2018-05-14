using DateMate.Data.Context;
using DateMate.Data.Entities;
using DateMate.Data.Repositories;
using DateMate.Entities;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace DateMate.Controllers.Api
{
	// RESTfulisch API controller for friendship entities. A 'friendship' is definied as an initiated friendship,
	// independent of the relationsship's status, i.e.: A confirmed, pending, and rejected status
	// of a friendship are all friendships so long as they exist in the Frienships table in the data context.
	[Authorize]
	public class FriendshipController : ApiController
	{
		private UnitOfWork _unitOfWork;

		public FriendshipController()
		{
			_unitOfWork = new UnitOfWork(new DateMateContext());
		}

		// Returns list of all friends for the specified *user id*.
		[ResponseType(typeof(List<Friendship>))]
		public IHttpActionResult Get(int id)
		{
			var friends = _unitOfWork.Friendships.GetFriends(id);

			if (friends == null)
				return StatusCode(HttpStatusCode.NoContent);

			var loggedInUserId = _unitOfWork.Users.GetByUsername(User.Identity.Name).Id;
			var friendList = new List<Friend>();

			// Create resource model. 
			foreach (var friendship in friends)
			{
				// Filter out the correct user (i.e. not the one with the specified id) from the friendship objects.
				User user = friendship.RequestById == id ? friendship.RequestTo : friendship.RequestBy;

				Friend friend = new Friend
				{
					Name = user.ToString(),
					UserId = user.Id
				};

				friendList.Add(friend);
			}

			return Ok(friendList);
		}

		// Adds a Friendship with a pending request status, i.e. creates a new friend request.
		[ResponseType(typeof(Friendship))]
		public IHttpActionResult Post(Friendship friendship)
		{
			var loggedInUser = _unitOfWork.Users.GetByUsername(User.Identity.Name);

			// Checks for bad requests:
			if (friendship == null)
				return BadRequest();

			// Sent friendship already exists.
			if (_unitOfWork.Friendships.GetById(friendship.RequestById, friendship.RequestToId) != null)
				return BadRequest();

			// Request is from another id than the logged in user's.
			if (friendship.RequestById != loggedInUser.Id)
				return BadRequest();

			if (friendship.Status != FriendshipRequestStatus.Pending)
				return BadRequest();

			_unitOfWork.Friendships.Add(friendship);
			_unitOfWork.Complete();

			return StatusCode(HttpStatusCode.NoContent);
		}

		// Confirms friendship for specified user's id. Since friendships have a composite primary key, we'll
		// update the friendship for the key where RequestById == id, and RequestToId == friendship.RequestToId.
		// (The friend request sender's id and the logged in user's id.)
		[ResponseType(typeof(void))]
		public IHttpActionResult Put(int id, Friendship friendship)
		{
			// Check for bad requests:
			if (id != friendship.RequestById || friendship.Status != FriendshipRequestStatus.Confirmed)
				return BadRequest();

			// Friendship already exists.
			if (_unitOfWork.Friendships.GetById(id, friendship.RequestToId) == null)
				return BadRequest();

			_unitOfWork.Friendships.Edit(friendship);
			_unitOfWork.Complete();

			return StatusCode(HttpStatusCode.NoContent);
		}

		// Deletes friendship for specified *user id*.
		[ResponseType(typeof(Friendship))]
		public IHttpActionResult Delete(int id)
		{
			// Get logged in user's id to target the composite key for the friendship table.
			int loggedInUserId = _unitOfWork.Users.GetByUsername(User.Identity.Name).Id;

			// RequestFromId == id, RequestToId == loggedInUserId.
			Friendship friendship = _unitOfWork.Friendships.GetById(id, loggedInUserId);
			if (friendship == null)
				return NotFound();

			_unitOfWork.Friendships.Remove(friendship);
			_unitOfWork.Complete();

			return Ok(friendship);
		}

		// Dispose data context on controller disposal.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
				_unitOfWork.Dispose();
			base.Dispose(disposing);
		}
	}

	// Resource model for friend list.
	public class Friend
	{
		public string Name { get; set; }
		public int UserId { get; set; }
	}
}