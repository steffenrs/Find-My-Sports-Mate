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
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
