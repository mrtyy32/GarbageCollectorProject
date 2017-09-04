using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Gcp.Host.Data;

namespace Gcp.Host.Controllers
{
    public class KurumlarController : BaseController
    {
        // GET: api/Kurumlar
        public IQueryable<Kurumlar> GetKurumlar()
        {
            return db.Kurumlar;
        }

        // GET: api/Kurumlar/5
        [ResponseType(typeof(Kurumlar))]
        public IHttpActionResult GetKurumlar(int id)
        {
	        var kurumlar = db.Kurumlar.Find(id);
	        return kurumlar == null ? (IHttpActionResult) NotFound() : Ok(kurumlar);
        }

        // PUT: api/Kurumlar/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKurumlar(int id, Kurumlar kurumlar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kurumlar.KurumID)
            {
                return BadRequest();
            }

            db.Entry(kurumlar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
	            if (!KurumlarExists(id))
                {
                    return NotFound();
                }
	            throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Kurumlar
        [ResponseType(typeof(Kurumlar))]
        public IHttpActionResult PostKurumlar(Kurumlar kurumlar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Kurumlar.Add(kurumlar);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
	            if (KurumlarExists(kurumlar.KurumID))
                {
                    return Conflict();
                }
	            throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = kurumlar.KurumID }, kurumlar);
        }

        // DELETE: api/Kurumlar/5
        [ResponseType(typeof(Kurumlar))]
        public IHttpActionResult DeleteKurumlar(int id)
        {
            var kurumlar = db.Kurumlar.Find(id);
	        if (kurumlar == null) return NotFound();
	        db.Kurumlar.Remove(kurumlar);
	        db.SaveChanges();

	        return Ok(kurumlar);
        }

        private bool KurumlarExists(int id)
        {
            return db.Kurumlar.Count(e => e.KurumID == id) > 0;
        }
    }
}