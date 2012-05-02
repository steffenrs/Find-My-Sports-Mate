using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

using System.ComponentModel.DataAnnotations;

namespace Domain
{
    [Table(name: "Address")]
    public class AddressBook
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Address { get; set; }
    }
}