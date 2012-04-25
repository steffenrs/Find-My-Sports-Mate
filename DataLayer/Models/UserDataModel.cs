using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using Domain;

namespace PresentationLayer.Models
{
    [Table(Name="User")]
    class UserDataModel
    {

        // TODO add tables and add column attributes
        [Column]
        public string id { get; set; }
        [Column]
        public string UserName { get; set; }
        [Column]
        public string Email { get; set; }
        [Column]
        public string Password { get; set; }
        [Column]
        public string StreetAddress { get; set; }
        [Column]
        public string Area { get; set; }
        [Column]
        public string State { get; set; }
        [Column]
        public string firstName { get; set; }
        [Column]
        public string lastName { get; set; }
        [Column]
        public bool gender { get; set; } // true = male, false = female
        [Column]
        public DateTime birthDate { get; set; }
        [Column]
        public double height { get; set; }
        [Column]
        public string phoneNumber { get; set; }
        [Column] // NHibernate can figure out this itself, but are you able to do it in LINQ - List as a column?
        public List<Sport> favoriteSports { get; set; }
    }
}
