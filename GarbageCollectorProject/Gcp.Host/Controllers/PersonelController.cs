﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Gcp.Host.Entity;
using Gcp.Host.Models;

namespace Gcp.Host.Controllers
{
    public class PersonelController : BaseController
    {
        // GET: api/Personel
        public dynamic GetPersonel()
        {
            var personelModel = new PersonelModel
            {
                ListPersonel = db.Personel.Include(i => i.PersonelDetay)

            };
            return personelModel;

        }

        // GET: api/Personel/5
        [ResponseType(typeof(Personel))]
        public IHttpActionResult GetPersonel(int id)
        {
            var personel = new PersonelModel();
            try
            {
                personel.Personel = db.Personel.Include(i => i.PersonelDetay).Single(x => x.PersonelID == id);
            }
            catch
            {
                return NotFound();
            }
            return Ok(personel);
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

            return CreatedAtRoute("DefaultApi", new { id = personel.PersonelID }, personel);
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
    }
}