using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Sport
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
