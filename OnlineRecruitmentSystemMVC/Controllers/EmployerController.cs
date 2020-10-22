using OnlineRecruitmentSystemMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OnlineRecruitmentSystemMVC.Controllers
{
    public class EmployerController : Controller
    {
        //Calling GetEmployer by id Method from API Controller      
        public ActionResult Sample(int id)
        {
            IEnumerable<EmployerMvcModel> cityList;
            GlobalVariables objGlobal = new GlobalVariables();
            HttpResponseMessage response = objGlobal.WebApiClient.GetAsync("Employer/SearchUser/" + id.ToString()).Result;
            cityList = response.Content.ReadAsAsync<IEnumerable<EmployerMvcModel>>().Result;
            return View(cityList);
        }

        // Loading AddOrEdit view when create  button is clicked
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new EmployerMvcModel());
            else
            {
                GlobalVariables objGlobal = new GlobalVariables();
                HttpResponseMessage response = objGlobal.WebApiClient.GetAsync("Employer/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<EmployerMvcModel>().Result);
            }
        }

        // Loading AddOrEdit view when Edit button is clicked
        [HttpPost]
        public ActionResult AddOrEdit(EmployerMvcModel emp)
        {
            if (emp.jobID == 0)
            {
                // Saving new data in Employer table
                GlobalVariables objGlobal = new GlobalVariables();
                HttpResponseMessage response = objGlobal.WebApiClient.PostAsJsonAsync("Employer", emp).Result;
                TempData["SuccessMessage"] = "Saved Succesfully";
            }
            else
            {
                // Updating Data in employer table
                GlobalVariables objGlobal = new GlobalVariables();
                HttpResponseMessage response = objGlobal.WebApiClient.PutAsJsonAsync("Employer/" + emp.jobID.ToString(), emp).Result;
                TempData["SuccessMessage"] = "Update Succesfully";
            }
            return RedirectToAction("Sample", new { id =emp.userId});
        }


        //Calling Delete by id from API controller
        public ActionResult Delete(int id)
        {
            GlobalVariables objGlobal = new GlobalVariables();
            HttpResponseMessage response = objGlobal.WebApiClient.DeleteAsync("Employer/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Succesfully";
            return RedirectToAction("Sample");
        }
    }
}