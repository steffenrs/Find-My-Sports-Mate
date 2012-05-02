using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    [Table(name: "Suggestion")]
    public class Suggestion
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Id")]
        public Sport Sport { get; set; }

        [ForeignKey("Id")]
        public Location Location { get; set; }

        [ForeignKey("Id")]
        public User Creator { get; set; }

        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
        public List<JoinedUser> JoinedUsers { get; set; }
        public Boolean IsClosed { get; set; }
        public int MinimumUsers { get; set; }
        public int MaximumUsers { get; set; }
    }
}
