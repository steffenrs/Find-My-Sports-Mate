using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace PresentationLayer
{
    [Table(Name="AddressBook")]
    public class DataAddressBook
    {
        [Column]
        public string FirstName { get; set; }
        [Column]
        public string SurName { get; set; }
        [Column]
        public string Address { get; set; }
    }
}