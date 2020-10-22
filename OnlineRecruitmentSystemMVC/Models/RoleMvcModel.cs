using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineRecruitmentSystemMVC.Models
{
    public class RoleMvcModel
    {
        public int roleId { get; set; }
        public string roleName { get; set; }

        public virtual ICollection<UserMvcModel> Users { get; set; }
    }
}