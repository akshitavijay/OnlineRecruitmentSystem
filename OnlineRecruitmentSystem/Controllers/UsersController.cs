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
using OnlineRecruitmentSystem.Models;

namespace OnlineRecruitmentSystem.Controllers
{
    public class UsersController : ApiController
    {
        private OnlineRecruitmentEntities1 db = new OnlineRecruitmentEntities1();

        /// <summary>
        /// Method to add a new user to data base
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(User))]
        public IHttpActionResult SignUp(User objUser)
        {
            db.Users.Add(new User()
            {
                name = objUser.name,
                password = objUser.password,
                email = objUser.email,
                gender = objUser.gender,
                roleId = objUser.roleId,
                phone = objUser.phone
            });
            db.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Method to fetch the list of users
        /// </summary>
        /// <returns></returns>
        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }


        /// <summary>
        /// Method to search for a particular user in the list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET: api/Users/5
        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

    }
}