using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Gcp.Host.Data;

namespace Gcp.Host.Controllers
{
    public class IslemController : BaseController
    {
        // GET: api/Islem
        public dynamic GetIslem()
        {
            var islem = db.Islem.Include("IslemDetay").ToList();
	        return islem;
        }

        // GET: api/Islem/5
        [ResponseType(typeof(Islem))]
        public IHttpActionResult GetIslem(int id)
        {
            Islem islem = db.Islem.Find(id);
            if (islem == null)
            {
                return NotFound();
            }

            return Ok(islem);
        }

        // PUT: api/Islem/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIslem(int id, Islem islem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != islem.IslemID)
            {
                return BadRequest();
            }

            db.Entry(islem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IslemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Islem
        [ResponseType(typeof(Islem))]
        public IHttpActionResult PostIslem(Islem islem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Islem.Add(islem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = islem.IslemID }, islem);
        }

        // DELETE: api/Islem/5
        [ResponseType(typeof(Islem))]
        public IHttpActionResult DeleteIslem(int id)
        {
            Islem islem = db.Islem.Find(id);
            if (islem == null)
            {
                return NotFound();
            }

            db.Islem.Remove(islem);
            db.SaveChanges();

            return Ok(islem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IslemExists(int id)
        {
            return db.Islem.Count(e => e.IslemID == id) > 0;
        }
    }
}