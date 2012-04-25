using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class JoinedUser
    {
        public User user { get; set; }
        public List<DateTime> joinedDates { get; set; }
    }
}
