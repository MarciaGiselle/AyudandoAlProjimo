using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Proyecto.BaseDatos;

namespace ProyectoWeb.Controllers
{
    public class PropuestasApiController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/PropuestasApi
        public IQueryable<Propuestas> GetPropuestas()
        {
            return db.Propuestas;
        }

        // GET: api/PropuestasApi/5
        [ResponseType(typeof(Propuestas))]
        public IHttpActionResult GetPropuestas(int id)
        {
            Propuestas propuestas = db.Propuestas.Find(id);
            if (propuestas == null)
            {
                return NotFound();
            }

            return Ok(propuestas);
        }

        // PUT: api/PropuestasApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPropuestas(int id, Propuestas propuestas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != propuestas.IdPropuesta)
            {
                return BadRequest();
            }

            db.Entry(propuestas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropuestasExists(id))
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

        // POST: api/PropuestasApi
        [ResponseType(typeof(Propuestas))]
        public IHttpActionResult PostPropuestas(Propuestas propuestas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Propuestas.Add(propuestas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = propuestas.IdPropuesta }, propuestas);
        }

        // DELETE: api/PropuestasApi/5
        [ResponseType(typeof(Propuestas))]
        public IHttpActionResult DeletePropuestas(int id)
        {
            Propuestas propuestas = db.Propuestas.Find(id);
            if (propuestas == null)
            {
                return NotFound();
            }

            db.Propuestas.Remove(propuestas);
            db.SaveChanges();

            return Ok(propuestas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropuestasExists(int id)
        {
            return db.Propuestas.Count(e => e.IdPropuesta == id) > 0;
        }
    }
}