using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Gcp.Host.Data;

namespace Gcp.Host.Controllers
{
    public class MarkaController : BaseController
    {

        // GET: api/Marka
        public IQueryable<Marka> GetMarka()
        {
            return db.Marka;
        }

        // GET: api/Marka/5
        [ResponseType(typeof(Marka))]
        public IHttpActionResult GetMarka(int id)
        {
	        var marka = db.Marka.Find(id);
	        return marka == null ? (IHttpActionResult) NotFound() : Ok(marka);
        }

        // PUT: api/Marka/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMarka(int id, Marka marka)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != marka.MarkaID)
            {
                return BadRequest();
            }

            db.Entry(marka).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
	            if (!MarkaExists(id))
                {
                    return NotFound();
                }
	            throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Marka
        [ResponseType(typeof(Marka))]
        public IHttpActionResult PostMarka(Marka marka)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Marka.Add(marka);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
	            if (MarkaExists(marka.MarkaID))
                {
                    return Conflict();
                }
	            throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = marka.MarkaID }, marka);
        }

        // DELETE: api/Marka/5
        [ResponseType(typeof(Marka))]
        public IHttpActionResult DeleteMarka(int id)
        {
            var marka = db.Marka.Find(id);
	        if (marka == null) return NotFound();
	        db.Marka.Remove(marka);
	        db.SaveChanges();

	        return Ok(marka);
        }

        private bool MarkaExists(int id)
        {
            return db.Marka.Count(e => e.MarkaID == id) > 0;
        }
    }
}