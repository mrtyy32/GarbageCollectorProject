using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Gcp.Host.Entity;

namespace Gcp.Host.Controllers
{
    public class ModelController : BaseController
    {

        // GET: api/Model
        public IQueryable<Model> GetModel()
        {
            return db.Model;
        }

        // GET: api/Model/5
        [ResponseType(typeof(Model))]
        public IHttpActionResult GetModel(int id)
        {
	        var model = db.Model.Find(id);
	        return model == null ? (IHttpActionResult) NotFound() : Ok(model);
        }

        // PUT: api/Model/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutModel(int id, Model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.ModelID)
            {
                return BadRequest();
            }

            db.Entry(model).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
	            if (!ModelExists(id))
                {
                    return NotFound();
                }
	            throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Model
        [ResponseType(typeof(Model))]
        public IHttpActionResult PostModel(Model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Model.Add(model);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
	            if (ModelExists(model.ModelID))
                {
                    return Conflict();
                }
	            throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = model.ModelID }, model);
        }

        // DELETE: api/Model/5
        [ResponseType(typeof(Model))]
        public IHttpActionResult DeleteModel(int id)
        {
            var model = db.Model.Find(id);
	        if (model == null) return NotFound();
	        db.Model.Remove(model);
	        db.SaveChanges();

	        return Ok(model);
        }

        private bool ModelExists(int id)
        {
            return db.Model.Count(e => e.ModelID == id) > 0;
        }
    }
}