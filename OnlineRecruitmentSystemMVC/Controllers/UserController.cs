using OnlineRecruitmentSystemMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace OnlineRecruitmentSystemMVC.Controllers
{
    
    public class UserController : Controller
    {
        /// <summary>
        /// Method for redirecting to the view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Signup(int id = 0)
        {
            return View(new UserMvcModel());

        }

        /// <summary>
        /// Method to add/register user
        /// </summary>
        /// <param name="objMUM"></param>
        /// <returns></returns
        /// 

        [HttpPost]
        public ActionResult Signup(UserMvcModel objMUM)
        {
            GlobalVariables objGlobal = new GlobalVariables();

            HttpResponseMessage response = objGlobal.WebApiClient.PostAsJsonAsync("Users", objMUM).Result;
            TempData["SuccessMessage"] = "Registered Successfully";
            return RedirectToAction("SignIn");
        }


        /// <summary>
        /// Method to validate logIn credentials of a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult SignIn(UserMvcModel user)
        {

            IEnumerable<UserMvcModel> signInUser;
            GlobalVariables objGlobal = new GlobalVariables();
            //get the response in the httpresponsemessage object from .net.http namespace
            HttpResponseMessage response = objGlobal.WebApiClient.GetAsync("Users").Result;

            signInUser = response.Content.ReadAsAsync<IEnumerable<UserMvcModel>>().Result;

            foreach (UserMvcModel u in signInUser)
            {
                if (u.email == user.email && u.password == user.password)
                {
                    TempData["Message"] = "Logined sucessfully.";
                    Session["UserID"] = u.userId.ToString();
                    Session["UserName"] = u.name.ToString();
                    if(u.roleId==3)
                    {
                        return RedirectToAction("Details", "JobSeekers",new { id=u.userId});
                    }
                    if (u.roleId == 2)
                    {
                        return RedirectToAction("Sample", "Employer", new { id = u.userId });
                    }
                    if (u.roleId == 1)
                    {
                        return RedirectToAction("Index", "Admin", new { id = u.userId });
                    }
                    
                }
                else
                {
                    ModelState.Clear();
                }
            }
            TempData["Message"] = "Invalid Email or Password";
            return View();
        }                      

    }
}