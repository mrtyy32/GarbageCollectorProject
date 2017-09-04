using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Gcp.Host.Data;

namespace Gcp.Host.Controllers
{
	public class AraclarController : BaseController
	{

		// GET: api/Araclar
		public dynamic GetAraclar()
		{
			//var araclar = db.Araclar.SelectMany(s=> s.Personel).AsQueryable();
			//var arac = araclar.OfType<Araclar>().ToList();
			var araclar =
				db.Araclar
					.Include("Marka")
					.Where(x => x.MarkaID == x.Marka.MarkaID)
					.Include("Model")
					.Where(x => x.ModelID == x.Model.ModelID)
					.Include(i => i.AraclarDetay)
					.Include(i=> i.Personel).ToList();
			return araclar;
		}

		// GET: api/Araclar/5
		[ResponseType(typeof(Araclar))]
		public IHttpActionResult GetAraclar(int id)
		{
			var araclar = db.Araclar.Find(id);
			return araclar == null ? (IHttpActionResult)NotFound() : Ok(araclar);
		}

		// PUT: api/Araclar/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutAraclar(int id, Araclar araclar)
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
			if (!AraclarExists(araclar.AracID))
			{
				db.Araclar.Add(araclar);
			}


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
		public IHttpActionResult DeleteAraclar(int id)
		{
			var araclar = db.Araclar.Find(id);
			if (araclar == null) return NotFound();
			db.Araclar.Remove(araclar);
			db.SaveChanges();

			return Ok(araclar);
		}

		private bool AraclarExists(int id)
		{
			return db.Araclar.Count(e => e.AracID == id) > 0;
		}
	}
}