using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Web.Security;

namespace Domain
{
    public class User
    {
        //// TODO add tables and add column attributes
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string StreetAddress { get; set; }
        public string Area { get; set; }
        public string State { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; } // true = male, false = female
        public DateTime BirthDate { get; set; }
        public double Height { get; set; }
        public string PhoneNumber { get; set; }
    }

}