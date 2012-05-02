using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class JoinedUser
    {
        [Key, Column(Order=1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Key, Column(Order=2)]
        public int SuggestionId { get; set; }

        public String Weekdays { get; set; }
    }
}
