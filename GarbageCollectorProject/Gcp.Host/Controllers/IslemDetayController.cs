using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Gcp.Host.Data;

namespace Gcp.Host.Controllers
{
    public class IslemDetayController : BaseController
    {

        // GET: api/IslemDetay
        public dynamic GetIslemDetay()
        {
	        var detay = db.IslemDetay.Include("Islem").Where(x => x.IslemID == x.Islem.IslemID);
	        return detay;
        }

        // GET: api/IslemDetay/5
        [ResponseType(typeof(IslemDetay))]
        public IHttpActionResult GetIslemDetay(int id)
        {
	        var islemDetay = db.IslemDetay.Find(id);
	        return islemDetay == null ? (IHttpActionResult) NotFound() : Ok(islemDetay);
        }

        // PUT: api/IslemDetay/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIslemDetay(int id, IslemDetay islemDetay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != islemDetay.IslemDetayID)
            {
                return BadRequest();
            }

            db.Entry(islemDetay).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
	            if (!IslemDetayExists(id))
                {
                    return NotFound();
                }
	            throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/IslemDetay
        [ResponseType(typeof(IslemDetay))]
        public IHttpActionResult PostIslemDetay(IslemDetay islemDetay)
        {
	        if (!ModelState.IsValid) return BadRequest(ModelState);
	        db.IslemDetay.Add(islemDetay);
	        db.SaveChanges();

	        return CreatedAtRoute("DefaultApi", new {id = islemDetay.IslemDetayID}, islemDetay);
        }

        // DELETE: api/IslemDetay/5
        [ResponseType(typeof(IslemDetay))]
        public IHttpActionResult DeleteIslemDetay(int id)
        {
            var islemDetay = db.IslemDetay.Find(id);
	        if (islemDetay == null) return NotFound();
	        db.IslemDetay.Remove(islemDetay);
	        db.SaveChanges();

	        return Ok(islemDetay);
        }

        private bool IslemDetayExists(int id)
        {
            return db.IslemDetay.Count(e => e.IslemDetayID == id) > 0;
        }
    }
}