//=============================================
//  Developer:	<Subin Sunu Jacob>
//  Create date: <9th Oct,2020>
//  Related To : To manage Admin related requests/response scenerio
//=============================================
using OnlineRecruitmentSystemMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OnlineRecruitmentSystemMVC.Controllers
{
    /// <summary>
    /// Admin Controller class in MVC manages the client side requests from admin and response data from respective 
    /// admin controller in WebApi 
    /// </summary>
    public class AdminController : Controller
    {
        /// <summary>
        /// This ActionResult method calls for all user data from its respective method in the WebApi controller
        /// </summary>
        /// <returns> List of Users </returns>
        // GET: Admin       
        public ActionResult Index()
        {
            IEnumerable<UserMvcModel> userList = null;
            try
            {
                //This will help connect with the respective WebApi method and get the list of all users
                GlobalVariables objGlobal = new GlobalVariables();
                HttpResponseMessage response = objGlobal.WebApiClient.GetAsync("Admin").Result;

                if (response.IsSuccessStatusCode)
                {
                    userList = response.Content.ReadAsAsync<IEnumerable<UserMvcModel>>().Result;

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            catch (Exception)
            {
                return View();
            }
            return View(userList);
        }

        /// <summary>
        /// This method fetches the user from database based on the id recieved from the client side
        /// </summary>
        /// <param name="id"></param>
        /// <returns> The user with the same ID as passed from the client </returns>
        public ActionResult Edit(int id)
        {
            //checks if the user with the specific id exists if not then returns a new userModel
            if (id == 0)
                return View(new UserMvcModel());
            else
            {
                //Gets the user with this specific id from the respective method from web api
                GlobalVariables objGlobal = new GlobalVariables();
                HttpResponseMessage response = objGlobal.WebApiClient.GetAsync("Admin/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<UserMvcModel>().Result);
            }
        }
        /// <summary>
        /// This function sends the updated user type to the database through WebApi for updation into the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(UserMvcModel user)
        {
            //Calls the Update user method in the webapi and updates the changes in the user
            GlobalVariables objGlobal = new GlobalVariables();
            HttpResponseMessage response = objGlobal.WebApiClient.PutAsJsonAsync("Admin/" + user.userId, user).Result;
            //Message to be displayed after updation using aletifyjs
            TempData["SuccessMessage"] = "Updated Successfully";
            //redirects to the the index view after updation
            return RedirectToAction("Index");
        }

        /// <summary>
        /// This deletes a user record on the request of the client from the database using delete method in WebApi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            //Calls the delete method in webapi and passes along the id of the user that needs to be deleted
            GlobalVariables objGlobal = new GlobalVariables();
            HttpResponseMessage response = objGlobal.WebApiClient.DeleteAsync("Admin/" + id.ToString()).Result;
            //the message displayed after successful deletion of the respective user
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }


    }
}