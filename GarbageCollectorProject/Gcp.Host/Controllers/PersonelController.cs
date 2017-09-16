using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Gcp.Host.Data;
using Gcp.Host.Models;

namespace Gcp.Host.Controllers
{
    public class PersonelController : BaseController
    {
        // GET: api/Personel
        public dynamic GetPersonel()
        {
            var personels =
                db.Personel
                    .Include("Vardiya")
                    .Where(v => v.VardiyaID == v.Vardiya.VardiyaID)
                    .Include("Egitim")
                    .Where(e => e.EgitimID == e.Egitim.EgitimID)
                    .Include("Unvanlar")
                    .Where(u => u.UnvanID == u.Unvanlar.UnvanID)
                    .Include("Amir")
                    .Where(a => a.AmirID == a.Amir.PersonelID)
                    .Include("Araclar").DefaultIfEmpty().ToList();
	        return personels;

        }

        // GET: api/Personel/5
        [ResponseType(typeof(Personel))]
        public IHttpActionResult GetPersonel(int id)
        {
            var personel = new PersonelModel();
            try
            {
                personel.Personel = db.Personel.Find(id);
            }
            catch
            {
                return NotFound();
            }
            return Ok(personel.Personel);
        }

        // PUT: api/Personel/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersonel(int id, Personel personel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personel.PersonelID)
            {
                return BadRequest();
            }

            db.Entry(personel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonelExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Personel
        [ResponseType(typeof(Personel))]
        public IHttpActionResult PostPersonel(Personel personel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Personel.Add(personel);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateException)
			{
				if (PersonelExists(personel.PersonelID))
				{
					return Conflict();
				}
				throw;
			}
			CreatedAtRoute("DefaultApi", new { id = personel.PersonelID }, personel);
	        return Ok(personel);
        }

        // DELETE: api/Personel/5
        [ResponseType(typeof(Personel))]
        public IHttpActionResult DeletePersonel(int id)
        {
            var personel = db.Personel.Find(id);
            if (personel == null)
            {
                return NotFound();
            }

            db.Personel.Remove(personel);
            db.SaveChanges();

            return Ok(personel);
        }

        private bool PersonelExists(int id)
        {
            return db.Personel.Count(e => e.PersonelID == id) > 0;
        }

	    [Route("api/Personel/amirDropDown")]
	    [HttpGet]
	    [ResponseType(typeof(Personel))]
	    public IHttpActionResult amirDropDown()
	    {
		    var amir = db.Personel.Where(x=> x.AmirMi);
		    return Ok(amir);
	    }

		[Route("api/Personel/personelGiderTablosu")]
		[HttpGet]
		[ResponseType(typeof(Personel))]
		public IHttpActionResult personelGiderTablosu()
		{
			var dagilim = db.Personel.Select(x => new
			{
				x.PersonelAd, x.PersonelSoyad,
				x.Maas
			}).ToList();
			return Ok(dagilim);
		}
	}
}