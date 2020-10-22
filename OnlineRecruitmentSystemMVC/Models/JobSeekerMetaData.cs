using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineRecruitmentSystemMVC.Models
{

    [MetadataType(typeof(JobSeekerMetaData))]
    public partial class JobSeekerMvcModel
    {

    }

    public class JobSeekerMetaData
    {
        [Required(ErrorMessage = "*Address is required")]
        public string address { get; set; }

        [Required(ErrorMessage = "*Experience is required")]
        [Range(0, 40, ErrorMessage = "Experience must be a valid number between 0 and 40")]
        public Nullable<int> experience { get; set; }


        [Required(ErrorMessage = "*Profession is required")]
        public string profession { get; set; }


        [Required(ErrorMessage = "*SkillSet is required")]
        public string skillset { get; set; }


        [Required(ErrorMessage = "*Resume is required")]
        public string resume { get; set; }


        [Required(ErrorMessage = "*Qualification is required")]
        public string qualification { get; set; }


    }
}