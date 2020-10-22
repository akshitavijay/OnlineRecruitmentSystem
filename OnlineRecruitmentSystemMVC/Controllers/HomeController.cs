using Newtonsoft.Json;
using OnlineRecruitmentSystemMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineRecruitmentSystemMVC.Controllers
{
    //Home Controller
    public class HomeController : Controller
    {
        //Index Method
        public ActionResult Index()
        {
            return View();
        }

        //About the portal
        public ActionResult About()
        {          

            return View();
        }

        //Contact Details
        public ActionResult Contact()
        {           

            return View();
        }

        //Search Method used to search jobs
        public ActionResult Search(string searchBy, string search)
        {
            
            IEnumerable<EmployerMvcModel> empList;
            GlobalVariables objGlobal = new GlobalVariables();
            //used to get the Response message in HttpResponseMessage
            HttpResponseMessage response = objGlobal.WebApiClient.GetAsync("Search?searchBy=" + searchBy + "&search=" + search).Result;
                    
            empList = response.Content.ReadAsAsync<IEnumerable<EmployerMvcModel>>().Result;
            ViewBag.Message = "Search for job";
            return View(empList);
        }
    }
}