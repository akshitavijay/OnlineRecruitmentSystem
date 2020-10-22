using OnlineRecruitmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace OnlineRecruitmentSystem.Controllers
{
    public class EmployerController : ApiController
    {
        private OnlineRecruitmentEntities1 db = new OnlineRecruitmentEntities1();

        // GET: api/Employer
        public IQueryable<Employer> GetEmployers()
        {
            return db.Employers;
        }

        [HttpGet]
        [Route("api/Employer/SearchUser/{id}")]
        public IQueryable<Employer> SearchUser(int id)
        {
            return (db.Employers.Where(j => j.userId == id));
            //if (employer == null)
            //{
            //    return NotFound();
            //}

            //return Ok(employer);
        }


        // GET: api/Employer/5
        [ResponseType(typeof(Employer))]
        public IHttpActionResult GetEmployer(int id)
        {
            Employer emp = db.Employers.Where(j=>j.jobID==id).FirstOrDefault();
            if (emp == null)
            {
                return NotFound();
            }

            return Ok(emp);
        }

        // PUT: api/Employer/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployer(int id, Employer employer)
        {

            if (id != employer.jobID)
            {
                return BadRequest();
            }

            db.Entry(employer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)    //Handling Validation errors (if exists)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employer
        [ResponseType(typeof(Employer))]
        public IHttpActionResult PostEmployer(Employer employer)
        {


            db.Employers.Add(employer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployerExists(employer.jobID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = employer.jobID }, employer);
        }

        // DELETE: api/Employer/5
        [ResponseType(typeof(Employer))]
        public IHttpActionResult DeleteEmployer(int id)
        {
            Employer employer = db.Employers.Find(id);
            if (employer == null)
            {
                return NotFound();
            }

            db.Employers.Remove(employer);
            db.SaveChanges();

            return Ok(employer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployerExists(int id)
        {
            return db.Employers.Count(e => e.jobID == id) > 0;
        }
    }
}

