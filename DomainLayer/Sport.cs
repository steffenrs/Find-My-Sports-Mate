using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    [Table(name: "Sport")]
    public class Sport
    {
        [Key]
        public string Name { get; set; }
    }
}
