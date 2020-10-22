using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineRecruitmentSystemMVC.Models
{
    public partial class JobSeekerMvcModel
    {
        public int jobSeekerID { get; set; }
        public Nullable<int> userId { get; set; }
        public string address { get; set; }
              
        public Nullable<int> experience { get; set; }
       
        public string profession { get; set; }
        public string skillset { get; set; }
        public string resume { get; set; }
        public string qualification { get; set; }

        public virtual UserMvcModel User { get; set; }
    }
}