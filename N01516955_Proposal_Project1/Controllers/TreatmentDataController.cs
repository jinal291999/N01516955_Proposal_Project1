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
using N01516955_Proposal_Project1.Models;

namespace N01516955_Proposal_Project1.Controllers
{
    public class TreatmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TreatmentData/ListTreatments
        [HttpGet]
        public IHttpActionResult ListTreatments()
        {
            List<Treatment> Treatments = db.Treatments.ToList();
            List<TreatmentDto> TreatmentDtos = new List<TreatmentDto>();

            Treatments.ForEach(a => TreatmentDtos.Add(new TreatmentDto()
            {
                 Id = a.Id,
               Name = a.Name,
               Duration = a.Duration,
               Cost = a.Cost,

            }));
            return Ok(TreatmentDtos);
        }

        // GET: api/TreatmentData/FindTreatment
        [ResponseType(typeof(Treatment))]
        [HttpGet]
        public IHttpActionResult FindTreatment(int id)
        {
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return NotFound();
            }

            return Ok(treatment);
        }

        // PUT: api/TreatmentData/UpdateTreatment/1
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTreatment(int id, Treatment treatment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != treatment.Id)
            {
                return BadRequest();
            }

            db.Entry(treatment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreatmentExists(id))
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

        // POST: api/TreatmentData/AddTreatment
        [ResponseType(typeof(Treatment))]
        [HttpPost]
        public IHttpActionResult AddTreatment(Treatment treatment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Treatments.Add(treatment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = treatment.Id }, treatment);
        }

        // DELETE: api/TreatmentData/5
        [ResponseType(typeof(Treatment))]
        public IHttpActionResult DeleteTreatment(int id)
        {
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return NotFound();
            }

            db.Treatments.Remove(treatment);
            db.SaveChanges();

            return Ok(treatment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TreatmentExists(int id)
        {
            return db.Treatments.Count(e => e.Id == id) > 0;
        }
    }
}