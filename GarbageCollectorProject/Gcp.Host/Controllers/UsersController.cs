using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Gcp.Host.Data;

namespace Gcp.Host.Controllers
{
	public class UsersController : BaseController
	{
		// GET: api/Users
		public IQueryable<User> GetUser()
		{
			return db.User;
		}

		// GET: api/Users/5
		[ResponseType(typeof(User))]
		public IHttpActionResult GetUser(int id)
		{
			var user = db.User.Find(id);
			return user == null ? (IHttpActionResult)NotFound() : Ok(user);
		}

		// PUT: api/Users/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutUser(int id, User user)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != user.UserID)
			{
				return BadRequest();
			}

			db.Entry(user).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!UserExists(id))
				{
					return NotFound();
				}
				throw;
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/Users
		[ResponseType(typeof(User))]
		public IHttpActionResult PostUser(User user)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			db.User.Add(user);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = user.UserID }, user);
		}

		// DELETE: api/Users/5
		[ResponseType(typeof(User))]
		public IHttpActionResult DeleteUser(int id)
		{
			var user = db.User.Find(id);
			if (user == null) return NotFound();
			db.User.Remove(user);
			db.SaveChanges();

			return Ok(user);
		}

		private bool UserExists(int id)
		{
			return db.User.Count(e => e.UserID == id) > 0;
		}
		[Route("api/Users/Login/{UserName}/{UserPassword}")]
		[HttpGet]
		public bool Login(string UserName, string UserPassword)
		{
			var us = db.User.SingleOrDefault(x => x.UserName == UserName && x.UserPassword == UserPassword);
			if (us == null)
			{
				return false;
			}
			return true;
		}
	}
}