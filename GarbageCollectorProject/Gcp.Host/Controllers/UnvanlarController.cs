using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Gcp.Host.Data;

namespace Gcp.Host.Controllers
{
    public class UnvanlarController : BaseController
    {

        // GET: api/Unvanlar
        public IQueryable<Unvanlar> GetUnvanlar()
        {
            return db.Unvanlar;
        }

        // GET: api/Unvanlar/5
        [ResponseType(typeof(Unvanlar))]
        public IHttpActionResult GetUnvanlar(int id)
        {
	        var unvanlar = db.Unvanlar.Find(id);
	        return unvanlar == null ? (IHttpActionResult) NotFound() : Ok(unvanlar);
        }

        // PUT: api/Unvanlar/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUnvanlar(int id, Unvanlar unvanlar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != unvanlar.UnvanID)
            {
                return BadRequest();
            }

            db.Entry(unvanlar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
	            if (!UnvanlarExists(id))
                {
                    return NotFound();
                }
	            throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Unvanlar
        [ResponseType(typeof(Unvanlar))]
        public IHttpActionResult PostUnvanlar(Unvanlar unvanlar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Unvanlar.Add(unvanlar);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = unvanlar.UnvanID }, unvanlar);
        }

        // DELETE: api/Unvanlar/5
        [ResponseType(typeof(Unvanlar))]
        public IHttpActionResult DeleteUnvanlar(int id)
        {
            var unvanlar = db.Unvanlar.Find(id);
            if (unvanlar == null)
            {
                return NotFound();
            }

            db.Unvanlar.Remove(unvanlar);
            db.SaveChanges();

            return Ok(unvanlar);
        }

        private bool UnvanlarExists(int id)
        {
            return db.Unvanlar.Count(e => e.UnvanID == id) > 0;
        }
    }
}