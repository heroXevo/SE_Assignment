using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Business_Logic;

namespace TraderPlaceApp.Models
{

    public class RegisterModel
    {

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage="The {0} must be at least {2} charracters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]        
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword{ get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string surName { get; set; }

        [Required]
        [Display(Name = "Age")]
        [Range(18, 65, ErrorMessage ="Sorry you must be between 18 and 65 to register.")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }


    }

    public class LoginModel
    {

        [Required(ErrorMessage = "please enter you username")]
        [Display(Name = "UserName")]
        public string userName{get; set;}

        [Required(ErrorMessage = "please enter you password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password{get; set;}

    }


}
