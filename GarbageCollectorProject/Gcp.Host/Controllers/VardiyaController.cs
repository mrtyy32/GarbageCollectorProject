using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Gcp.Host.Data;

namespace Gcp.Host.Controllers
{
    public class VardiyaController : BaseController
    {


        // GET: api/Vardiya
        public IQueryable<Vardiya> GetVardiya()
        {
            return db.Vardiya;
        }

        // GET: api/Vardiya/5
        [ResponseType(typeof(Vardiya))]
        public IHttpActionResult GetVardiya(int id)
        {
	        var vardiya = db.Vardiya.Find(id);
	        return vardiya == null ? (IHttpActionResult) NotFound() : Ok(vardiya);
        }

        // PUT: api/Vardiya/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVardiya(int id, Vardiya vardiya)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vardiya.VardiyaID)
            {
                return BadRequest();
            }

            db.Entry(vardiya).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
	            if (!VardiyaExists(id))
                {
                    return NotFound();
                }
	            throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Vardiya
        [ResponseType(typeof(Vardiya))]
        public IHttpActionResult PostVardiya(Vardiya vardiya)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vardiya.Add(vardiya);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
	            if (VardiyaExists(vardiya.VardiyaID))
                {
                    return Conflict();
                }
	            throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = vardiya.VardiyaID }, vardiya);
        }

        // DELETE: api/Vardiya/5
        [ResponseType(typeof(Vardiya))]
        public IHttpActionResult DeleteVardiya(int id)
        {
            var vardiya = db.Vardiya.Find(id);
            if (vardiya == null)
            {
                return NotFound();
            }

            db.Vardiya.Remove(vardiya);
            db.SaveChanges();

            return Ok(vardiya);
        }

        private bool VardiyaExists(int id)
        {
            return db.Vardiya.Count(e => e.VardiyaID == id) > 0;
        }
    }
}