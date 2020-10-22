using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineRecruitmentSystemMVC.Models
{
    public class UserMvcModel
    {
        [Key]
        public int userId { get; set; }

        [Required(ErrorMessage = "Password Can not be left blank")]
        [StringLength(20, ErrorMessage = "Password can be of maximum 20 characters")]
        [RegularExpression("^(.{0,7}|[^0-9]*|[^A-Z])$", ErrorMessage = "Invalid Password")]
        public string password { get; set; }

        public Nullable<int> roleId { get; set; }

        [Required(ErrorMessage = "Name Can not be left blank")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Name (Characters only)")]
        [StringLength(100, ErrorMessage = "Name can be of maximum 100 characters")]
        public string name { get; set; }

        [Required(ErrorMessage = "Email Can not be left blank")]
        [RegularExpression(@"^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+.)+[a-z]{2,5}$", ErrorMessage = "Invalid Name (Enter a Valid Email ID)")]
        [StringLength(20, ErrorMessage = "Email can be of maximum 20 characters")]
        public string email { get; set; }

        [Required(ErrorMessage = "Phone Number Can not be left blank")]
        [RegularExpression("^[7-9][0-9]{9}$", ErrorMessage = "Invalid Phone Number (Enter a Valid 10 digit Phone Number)")]
        [StringLength(20, ErrorMessage = "Phone Number can be of maximum 20 characters")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Gender Can not be left blank")]
        [RegularExpression("^[MFO]*$", ErrorMessage = "Enter M for male, F for female and O for other")]
        [StringLength(1, ErrorMessage = "Gender can take a maximum of 1 character")]
        public string gender { get; set; }

        public virtual ICollection<JobSeekerMvcModel> JobSeekers { get; set; }
        public virtual RoleMvcModel Role { get; set; }
    }
}