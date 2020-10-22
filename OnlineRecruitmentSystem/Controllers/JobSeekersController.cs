﻿//=====================================================
//  Developer:	<Samarth Chadda>
//  Create date: <9th Oct,2020>
//  Related To : To manage the Add,Edit, Details functionality of JobSeeker 
//=====================================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineRecruitmentSystem.Models;

namespace OnlineRecruitmentSystem.Controllers
{

    /// <summary>
    /// This class recieves requests from JobSeeker MVC controller to make changes into the JOBSEEKER table in database
    /// </summary>
    public class JobSeekersController : ApiController
    {
        /// <summary>
        /// Creating object of Entity Class , in order to make changes into collections(which result into changes in database)
        /// </summary>
        private OnlineRecruitmentEntities1 db = new OnlineRecruitmentEntities1();


        /// <summary>
        /// Method to get all the jobseekers exists in collection(or in database)
        /// </summary>
        /// <returns></returns>
        // GET: api/JobSeekers
        public IQueryable<JobSeeker> GetJobSeekers()
        {

            return db.JobSeekers;
        }


        /// <summary>
        /// Method to find jobseeker with particular userId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/JobSeekers/FindJobSeeker/{id}")]        
        public Boolean FindJobSeeker(int? id)
        {
            //retrieving jobseeker with particular userId 
            JobSeeker jobSeeker = db.JobSeekers.Where(x => x.userId == id).FirstOrDefault();
            if(jobSeeker==null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        /// <summary>
        /// Method to get a particular jobseeker
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/JobSeekers/5
        [ResponseType(typeof(JobSeeker))]
        public IHttpActionResult GetJobSeeker(int id)
        {
            //first checking if given userId exists in USERS collection
            User userObj = db.Users.Where(x => x.userId == id).FirstOrDefault();

            JobSeeker jobSeeker;
            if (userObj == null)
            {
                return NotFound();
            }
            else
            {
                jobSeeker = db.JobSeekers.Where(x => x.userId == id).FirstOrDefault();
                if(jobSeeker==null)
                {
                    //creating a dummy jobseeker with given userId
                    JobSeeker newSeeker = new JobSeeker()
                    {
                        //jobSeekerID = 0,
                        userId = id,
                        address = "",
                        experience = null,
                        profession = "",
                        skillset = "",
                        resume = "",
                        qualification = ""
                    };                                                     
                    
                    return Ok(newSeeker);
                }
                else
                {
                    //creating object for existing jobSeeker
                    JobSeeker existsSeeker = new JobSeeker()
                    {
                        jobSeekerID = jobSeeker.jobSeekerID,
                        userId = id,
                        address = jobSeeker.address,
                        experience = jobSeeker.experience,
                        profession = jobSeeker.profession,
                        skillset = jobSeeker.skillset,
                        resume = jobSeeker.resume,
                        qualification = jobSeeker.qualification
                       
                    };
                   

                    return Ok(existsSeeker);
                }

            }          

        }


        /// <summary>
        /// Method for handling the EDIT functionality
        /// </summary>
        /// <param name="jobSeeker"></param>
        /// <returns></returns>
        // PUT: api/JobSeekers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJobSeeker(JobSeeker jobSeeker)
        {
            try
            {
                //creating object of current controller(So that other method can be called form this method)
                JobSeekersController objJobSeeker = new JobSeekersController();

                //if jobseeker already exist in JOBSEEKER table
                if (objJobSeeker.FindJobSeeker(jobSeeker.userId))
                {
                    //retrieving particular jobseeker 
                    var updJobSeeker = db.JobSeekers.Where(x => x.jobSeekerID == jobSeeker.jobSeekerID).FirstOrDefault();
                    if (updJobSeeker != null)
                    {
                        //updating the existing jobseeker with new values
                        updJobSeeker.jobSeekerID = jobSeeker.jobSeekerID;
                        updJobSeeker.userId = jobSeeker.userId;
                        updJobSeeker.address = jobSeeker.address;
                        updJobSeeker.experience = jobSeeker.experience;
                        updJobSeeker.profession = jobSeeker.profession;
                        updJobSeeker.skillset = jobSeeker.skillset;
                        updJobSeeker.resume = jobSeeker.resume;
                        updJobSeeker.qualification = jobSeeker.qualification;
                        //saving changes into database
                        db.SaveChanges();

                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    //If jobseeker Already does not exist, so we are creating a new jobseeker
                    objJobSeeker.PostJobSeeker(jobSeeker);
                }


                return Ok();
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

        }

        /// <summary>
        /// Method for creating a new jobseeker
        /// </summary>
        /// <param name="jobSeeker"></param>
        /// <returns></returns>
        // POST: api/JobSeekers
        [ResponseType(typeof(JobSeeker))]
        public IHttpActionResult PostJobSeeker(JobSeeker jobSeeker)
        {           
            //adding new jobseeker to JobSeekers Collection
            db.JobSeekers.Add(jobSeeker);

            try
            {
                //saving changes into database
                db.SaveChanges();
            }
            catch (DbUpdateException)     
            {
                //if jobSeeker with same jobseekerID already EXISTS
                if (JobSeekerExists(jobSeeker.jobSeekerID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = jobSeeker.jobSeekerID }, jobSeeker);
        }

        //// DELETE: api/JobSeekers/5
        //[ResponseType(typeof(JobSeeker))]
        //public IHttpActionResult DeleteJobSeeker(int id)
        //{
        //    JobSeeker jobSeeker = db.JobSeekers.Find(id);
        //    if (jobSeeker == null)
        //    {
        //        return NotFound();
        //    }

        //    db.JobSeekers.Remove(jobSeeker);
        //    db.SaveChanges();

        //    return Ok(jobSeeker);
        //}


        /// <summary>
        /// This method performs all object cleanup, so the garbage collector no longer needs to call the objects' Object. Finalize override.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        /// <summary>
        /// Method for checking if jobseeker already exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool JobSeekerExists(int id)
        {
            return db.JobSeekers.Count(e => e.jobSeekerID == id) > 0;
        }
    }
}