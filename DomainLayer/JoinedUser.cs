using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    [Table("JoinedUser")]
    public class JoinedUser
    {
        [Key, Column(Order=1)]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Key, Column(Order=0)]
        public int SuggestionId { get; set; }

        public String Weekdays { get; set; }
    }
}
