using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace PresentationLayer
{
    public class UserViewModel : RegisterViewModel
    {

        [Display(Name = "Gender")]
        public bool Gender { get; set; } // true = male, false = female
        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }        
    }


}