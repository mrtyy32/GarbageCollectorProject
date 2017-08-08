using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Gcp.Host.Entity;

namespace Gcp.Host.Controllers
{
    public class AraclarController : BaseController
    {

        // GET: api/Araclar
        public IQueryable<Araclar> GetAraclar()
        {
            return db.Araclar.Include(a=> a.AraclarDetay).Include(mar=> mar.Marka).Include(mod=> mod.Marka.Model);
        }

        // GET: api/Araclar/5
        [ResponseType(typeof(Araclar))]
        public IHttpActionResult GetAraclar(string id)
        {
	        var araclar = db.Araclar.Find(id);
	        return araclar == null ? (IHttpActionResult) NotFound() : Ok(araclar);
        }

        // PUT: api/Araclar/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAraclar(string id, Araclar araclar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != araclar.AracID)
            {
                return BadRequest();
            }

            db.Entry(araclar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
	            if (!AraclarExists(id))
                {
                    return NotFound();
                }
	            throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Araclar
        [ResponseType(typeof(Araclar))]
        public IHttpActionResult PostAraclar(Araclar araclar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Araclar.Add(araclar);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
	            if (AraclarExists(araclar.AracID))
                {
                    return Conflict();
                }
	            throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = araclar.AracID }, araclar);
        }

        // DELETE: api/Araclar/5
        [ResponseType(typeof(Araclar))]
        public IHttpActionResult DeleteAraclar(string id)
        {
            var araclar = db.Araclar.Find(id);
	        if (araclar == null) return NotFound();
	        db.Araclar.Remove(araclar);
	        db.SaveChanges();

	        return Ok(araclar);
        }

      private bool AraclarExists(string id)
        {
            return db.Araclar.Count(e => e.AracID == id) > 0;
        }
    }
}