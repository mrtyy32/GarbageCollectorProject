using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Gcp.Host.Data;

namespace Gcp.Host.Controllers
{
    public class AraclarGecmisController : BaseController
    {

        // GET: api/AraclarGecmis
        public IQueryable<AraclarGecmis> GetAraclarGecmis()
        {
            return db.AraclarGecmis
				.Include("Araclar").Where(x=> x.AracID == x.Araclar.AracID)
				.Include("Personel").Where(x=> x.PersonelID == x.Personel.PersonelID)
				.Include("Vardiya").Where(x=> x.VardiyaID == x.Vardiya.VardiyaID);
        }

        // GET: api/AraclarGecmis/5
        [ResponseType(typeof(AraclarGecmis))]
        public IHttpActionResult GetAraclarGecmis(int id)
        {
            var araclarGecmis = db.AraclarGecmis.Find(id);
            if (araclarGecmis == null)
            {
                return NotFound();
            }

            return Ok(araclarGecmis);
        }

        // PUT: api/AraclarGecmis/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAraclarGecmis(int id, AraclarGecmis araclarGecmis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != araclarGecmis.AracGecmisID)
            {
                return BadRequest();
            }

            db.Entry(araclarGecmis).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
	            if (!AraclarGecmisExists(id))
                {
                    return NotFound();
                }
	            throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AraclarGecmis
        [ResponseType(typeof(AraclarGecmis))]
        public IHttpActionResult PostAraclarGecmis(AraclarGecmis araclarGecmis)
        {
	        if (!ModelState.IsValid) return BadRequest(ModelState);
	        db.AraclarGecmis.Add(araclarGecmis);
	        db.SaveChanges();

	        return CreatedAtRoute("DefaultApi", new {id = araclarGecmis.AracGecmisID}, araclarGecmis);
        }

        // DELETE: api/AraclarGecmis/5
        [ResponseType(typeof(AraclarGecmis))]
        public IHttpActionResult DeleteAraclarGecmis(int id)
        {
            var araclarGecmis = db.AraclarGecmis.Find(id);
	        if (araclarGecmis == null) return NotFound();
	        db.AraclarGecmis.Remove(araclarGecmis);
	        db.SaveChanges();

	        return Ok(araclarGecmis);
        }

        private bool AraclarGecmisExists(int id)
        {
            return db.AraclarGecmis.Count(e => e.AracGecmisID == id) > 0;
        }

		[Route("api/AraclarGecmis/AracGecmis/{aracId}")]
		[HttpGet]
		public IHttpActionResult AracGecmis(int aracId)
		{
			var gecmis = GetAraclarGecmis().Where(x=>x.AracID == aracId).ToList();
			
			return Ok(gecmis);
		}
	}
}