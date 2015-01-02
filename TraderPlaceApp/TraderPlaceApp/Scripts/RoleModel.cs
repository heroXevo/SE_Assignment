using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using System.Globalization;
using System.ComponentModel.DataAnnotations;


namespace TraderPlaceApp.Models
{
    public class RoleModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Role ID")]
        public int RoleID { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

  

        

    }
}
