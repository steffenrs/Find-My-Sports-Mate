﻿using System;
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SportId { get; set; }
        [ForeignKey("SportId")]
        public virtual Sport Sport { get; set; }

        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

        public int CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public virtual User Creator { get; set; }

        public List<JoinedUser> JoinedUsers { get; set; }

        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
        public Boolean IsClosed { get; set; }
        public int MinimumUsers { get; set; }
        public int MaximumUsers { get; set; }
    }
}
