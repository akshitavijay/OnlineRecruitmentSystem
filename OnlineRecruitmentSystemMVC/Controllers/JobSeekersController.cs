//=============================================
//  Developer:	<Samarth Chadda>
//  Create date: <9th Oct,2020>
//  Related To : To manage JobSeeker related requests/response scenerio
//=============================================
using OnlineRecruitmentSystemMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

namespace OnlineRecruitmentSystemMVC.Controllers
{
    /// <summary>
    /// JobSeeker Controller class in MVC manages the client side requests and responses, respective to JobSeeker controller in WebApi 
    /// </summary>
    public class JobSeekersController : Controller
    {
        /// <summary>
        /// This method calls for all jobseekers data from its respective method in the WebApi controller
        /// </summary>
        /// <returns></returns>       
        public ActionResult Index()
        {
            IEnumerable<JobSeekerMvcModel> jobSeekerList;

            //This will help connect with the respective WebApi method and get the list of all users
            GlobalVariables objGlobal = new GlobalVariables();
            //calling 'GetJobSeeker' method of WebApi controller
            HttpResponseMessage response = objGlobal.WebApiClient.GetAsync("JobSeekers").Result;
            jobSeekerList = response.Content.ReadAsAsync<IEnumerable<JobSeekerMvcModel>>().Result;
            return View(jobSeekerList);
        }



        /// <summary>
        /// Method for creating a new jobseeker
        /// </summary>
        /// <param name="objJobSeeker"></param>
        /// <returns></returns>
        // POST: JobSeekers/Create      
        public ActionResult Create(JobSeekerMvcModel objJobSeeker)
        {          
            GlobalVariables objGlobal = new GlobalVariables();
            HttpResponseMessage response = objGlobal.WebApiClient.PostAsJsonAsync("JobSeekers", objJobSeeker).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(objJobSeeker);

        }


        /// <summary>
        /// Method for checking if particular jobSeeker exists in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // PUT: JobSeekers/Edit
        public ActionResult Edit(int? id)
        {
            try
            {                
                GlobalVariables objGlobal = new GlobalVariables();
                HttpResponseMessage response = objGlobal.WebApiClient.GetAsync("JobSeekers/" + id.ToString()).Result;

                //if response is valid i.e. particular jobseeker exists
                if (response.IsSuccessStatusCode)
                {
                    return View(response.Content.ReadAsAsync<JobSeekerMvcModel>().Result);
                }
                else
                {
                    throw new Exception("Details Not Found");
                }
            }
            catch (Exception ex)
            {
                //return the Error view in case of exception
                return View("~/Views/Shared/Error.cshtml", ex);
            }
                                          
        }

        /// <summary>
        /// Method for editing the jobseeker details
        /// </summary>
        /// <param name="objJobSeeker"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(JobSeekerMvcModel objJobSeeker)
        {
            //JobSeekersController objController = new JobSeekersController();
            //if(objController.Find(objJobSeeker.userId))
            //{
                GlobalVariables objGlobal = new GlobalVariables();
                HttpResponseMessage response = objGlobal.WebApiClient.PutAsJsonAsync("JobSeekers", objJobSeeker).Result;

            //if response is valid
            if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Details/"+objJobSeeker.userId);
                    }
            //}
            //else
            //{
            //    objController.Create(objJobSeeker);
            //}

            return View(objJobSeeker);
            
        }

        /// <summary>
        /// Method for finding particular jobseeker
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Boolean Find(int? id)
        {
            GlobalVariables objGlobal = new GlobalVariables();
            //calling 'FindJobSeeker' method of WebApi controller
            HttpResponseMessage response = objGlobal.WebApiClient.GetAsync("JobSeekers/FindJobSeeker/" + id.ToString()).Result;

            return response.IsSuccessStatusCode;            

        }



        /// <summary>
        /// This method helps in rendering the Details VIEW
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            try
            {
                GlobalVariables objGlobal = new GlobalVariables();
                HttpResponseMessage response = objGlobal.WebApiClient.GetAsync("JobSeekers/" + id.ToString()).Result;

                if(response.IsSuccessStatusCode)
                {
                    return View(response.Content.ReadAsAsync<JobSeekerMvcModel>().Result);
                }
                else
                {
                    string message = "Details Not Found";
                    throw new Exception(message);
                }
               
            }
            catch(Exception ex)
            {               
                return View("~/Views/Shared/Error.cshtml",ex);
            }
           

        }



    }
}