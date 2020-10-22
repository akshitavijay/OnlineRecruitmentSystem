using OnlineRecruitmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineRecruitmentSystem.Controllers
{
    //Search Controller 
    public class SearchController : ApiController
    {

        private OnlineRecruitmentEntities1 db = new OnlineRecruitmentEntities1();


        //Get Method for search
        public IEnumerable<Employer> GetSearch(string searchBy, string search)
        {
            if (searchBy == "Job Category")
            {
                return db.Employers.Where(x => x.jobCategory.StartsWith(search) || search == null).ToList();
            }
            if (searchBy == "Skill Set")
            {
                return db.Employers.Where(x => x.skillSet.StartsWith(search) || search == null);
            }
           
            if (searchBy == "Designation")
            {
                return db.Employers.Where(x => x.designation.StartsWith(search) || search == null);
            }
            else
            {
                return db.Employers.Where(x => x.location.StartsWith(search) || search == null);
            }
        }


        //Used to dispose the database object
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }

}
