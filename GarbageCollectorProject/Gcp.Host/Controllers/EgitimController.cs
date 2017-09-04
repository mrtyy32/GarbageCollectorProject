using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Gcp.Host.Data;

namespace Gcp.Host.Controllers
{
    public class EgitimController : BaseController
    {

        // GET: api/Egitim
        public IQueryable<Egitim> GetEgitim()
        {
            return db.Egitim;
        }

        // GET: api/Egitim/5
        [ResponseType(typeof(Egitim))]
        public IHttpActionResult GetEgitim(int id)
        {
	        var egitim = db.Egitim.Find(id);
	        return egitim == null ? (IHttpActionResult) NotFound() : Ok(egitim);
        }

        // PUT: api/Egitim/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEgitim(int id, Egitim egitim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != egitim.EgitimID)
            {
                return BadRequest();
            }

            db.Entry(egitim).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
	            if (!EgitimExists(id))
                {
                    return NotFound();
                }
	            throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Egitim
        [ResponseType(typeof(Egitim))]
        public IHttpActionResult PostEgitim(Egitim egitim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Egitim.Add(egitim);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
	            if (EgitimExists(egitim.EgitimID))
                {
                    return Conflict();
                }
	            throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = egitim.EgitimID }, egitim);
        }

        // DELETE: api/Egitim/5
        [ResponseType(typeof(Egitim))]
        public IHttpActionResult DeleteEgitim(int id)
        {
            var egitim = db.Egitim.Find(id);
            if (egitim == null)
            {
                return NotFound();
            }

            db.Egitim.Remove(egitim);
            db.SaveChanges();

            return Ok(egitim);
        }

        private bool EgitimExists(int id)
        {
            return db.Egitim.Count(e => e.EgitimID == id) > 0;
        }
    }
}