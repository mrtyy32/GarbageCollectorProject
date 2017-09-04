using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Gcp.Host.Data;

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
        public IHttpActionResult GetAraclarDetay(int id)
        {
	        var araclarDetay = db.AraclarDetay.Find(id);
	        return araclarDetay == null ? (IHttpActionResult) NotFound() : Ok(araclarDetay);
        }

        // PUT: api/AraclarDetay/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAraclarDetay(int id, AraclarDetay araclarDetay)
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
	        if (!ModelState.IsValid) return BadRequest(ModelState);
	        db.AraclarDetay.Add(araclarDetay);
	        db.SaveChanges();

	        return CreatedAtRoute("DefaultApi", new {id = araclarDetay.AracDetayID}, araclarDetay);
        }

        // DELETE: api/AraclarDetay/5
        [ResponseType(typeof(AraclarDetay))]
        public IHttpActionResult DeleteAraclarDetay(int id)
        {
            var araclarDetay = db.AraclarDetay.Find(id);
	        if (araclarDetay == null) return NotFound();
	        db.AraclarDetay.Remove(araclarDetay);
	        db.SaveChanges();

	        return Ok(araclarDetay);
        }

        private bool AraclarDetayExists(int id)
        {
            return db.AraclarDetay.Count(e => e.AracDetayID == id) > 0;
        }

	    [Route("api/AraclarDetay/DetayDokum/{aracId}")]
	    [HttpGet]
	    [ResponseType(typeof(AraclarDetay))]
	    public IHttpActionResult DetayDokum(int aracId)
	    {
		    var detaydokum = db.AraclarDetay.Where(x => x.AracID == aracId);
		    if (detaydokum == null) return NotFound();
			return Ok(detaydokum);
	    }
	}
}