using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineRecruitmentSystemMVC.Models
{
    public class EmployerMvcModel
    {
        public int jobID { get; set; }
        public Nullable<int> userId { get; set; }
        public string CompName { get; set; }
        public string JobCategory { get; set; }
        public string JobTitle { get; set; }
        public Nullable<int> CurrentOpenings { get; set; }
        public Nullable<System.DateTime> JoiningDate { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public Nullable<int> Experience { get; set; }
        public string Qualification { get; set; }
        public string SkillSet { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public string ContactDetails { get; set; }

        public virtual UserMvcModel User { get; set; }
    }
}