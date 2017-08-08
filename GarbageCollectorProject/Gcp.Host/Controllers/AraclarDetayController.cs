using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Gcp.Host.Entity;

namespace Gcp.Host.Controllers
{
    public class AraclarDetayController : BaseController
    {

        // GET: api/AraclarDetay
        public IQueryable<AraclarDetay> GetAraclarDetay()
        {
            return db.AraclarDetay;
        }

        // GET: api/AraclarDetay/5
        [ResponseType(typeof(AraclarDetay))]
        public IHttpActionResult GetAraclarDetay(string id)
        {
	        var araclarDetay = db.AraclarDetay.Find(id);
	        return araclarDetay == null ? (IHttpActionResult) NotFound() : Ok(araclarDetay);
        }

        // PUT: api/AraclarDetay/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAraclarDetay(string id, AraclarDetay araclarDetay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != araclarDetay.AracDetayID)
            {
                return BadRequest();
            }

            db.Entry(araclarDetay).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
	            if (!AraclarDetayExists(id))
                {
                    return NotFound();
                }
	            throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AraclarDetay
        [ResponseType(typeof(AraclarDetay))]
        public IHttpActionResult PostAraclarDetay(AraclarDetay araclarDetay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AraclarDetay.Add(araclarDetay);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
	            if (AraclarDetayExists(araclarDetay.AracDetayID))
                {
                    return Conflict();
                }
	            throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = araclarDetay.AracDetayID }, araclarDetay);
        }

        // DELETE: api/AraclarDetay/5
        [ResponseType(typeof(AraclarDetay))]
        public IHttpActionResult DeleteAraclarDetay(string id)
        {
            var araclarDetay = db.AraclarDetay.Find(id);
	        if (araclarDetay == null) return NotFound();
	        db.AraclarDetay.Remove(araclarDetay);
	        db.SaveChanges();

	        return Ok(araclarDetay);
        }


        private bool AraclarDetayExists(string id)
        {
            return db.AraclarDetay.Count(e => e.AracDetayID == id) > 0;
        }
    }
}