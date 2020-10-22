//=============================================
//  Developer:	<Subin Sunu Jacob>
//  Create date: <8th Oct,2020>
//  Related To : To manage Admin related requests/response scenerio
//=============================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineRecruitmentSystem.ExceptionLayer;
using OnlineRecruitmentSystem.Models;

namespace OnlineRecruitmentSystem.Controllers
{
    /// <summary>
    /// This class recieves requests from MVC controller to make changes into the User database
    /// </summary>
    public class AdminController : ApiController
    {
        /// <summary>
        /// This method fetches data of all users from the database
        /// </summary>
        /// <returns> List of Users </returns>
        // GET: api/Admin
        [HttpGet]
        public List<User> GetUsers()
        {
            try
            {
                //establishing database connection
                using (OnlineRecruitmentEntities1 db = new OnlineRecruitmentEntities1())
                {
                    List<User> userList = db.Users.ToList();
                    return userList;
                }
            }
            catch (Exception ex)
            {
                //throws user defined exception 
                throw new UserException(ex.Message);
            }
        }

        // GET: api/Admin/5
        [ResponseType(typeof(User))]
        [HttpGet]
        public User GetUser(int id)
        {
            try
            {
                //establishing database connection
                using (OnlineRecruitmentEntities1 db = new OnlineRecruitmentEntities1())
                {
                    User user = db.Users.Where(f => f.userId == id).FirstOrDefault();

                    return user;
                }
            }
            catch (Exception ex)
            {
                //throws user defined exception 
                throw new UserException(ex.Message);
            }
        }

        // PUT: api/Admin/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public bool PutUser(int id, User user)
        {
            try
            {
                //establishing database connection
                using (OnlineRecruitmentEntities1 db = new OnlineRecruitmentEntities1())
                {
                    User item = db.Users.Where(f => f.userId == id).FirstOrDefault();

                    if (item != null)
                    {
                        item.password = user.password;
                        item.name = user.name;
                        item.email = user.email;
                        item.phone = user.phone;
                        item.gender = user.gender;

                        db.SaveChanges();

                        return true;
                    }
                    else
                    {
                        //throws user defined exception 
                        throw new UserException("User does not exist.");
                    }

                }
            }
            catch (Exception ex)
            {
                //throws user defined exception 
                throw new UserException(ex.Message);
            }
        }

        //// POST: api/Admin
        //[ResponseType(typeof(User))]
        //public IHttpActionResult PostUser(User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Users.Add(user);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = user.userId }, user);
        //}

        // DELETE: api/Admin/5
        [ResponseType(typeof(User))]
        [HttpDelete]
        public bool DeleteUser(int id)
        {
            try
            {
                //establishing database connection
                using (OnlineRecruitmentEntities1 db = new OnlineRecruitmentEntities1())
                {
                    User user = db.Users.Where(f => f.userId == id).FirstOrDefault();
                    if (user != null)
                    {
                        db.Users.Remove(user);
                        db.SaveChanges();
                        return true;
                    }

                    else
                    {
                        //throws user defined exception 
                        throw new UserException("User does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                //throws user defined exception 
                throw new UserException(ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            //establishing database connection
            using (OnlineRecruitmentEntities1 db = new OnlineRecruitmentEntities1())
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
        }

        private bool UserExists(int id)
        {
            //establishing database connection
            using (OnlineRecruitmentEntities1 db = new OnlineRecruitmentEntities1())
            {
                return db.Users.Count(e => e.userId == id) > 0;

            }
        }
    }
}
