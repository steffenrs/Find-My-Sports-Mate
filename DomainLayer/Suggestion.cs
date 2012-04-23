using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Suggestion
    {
        public string id { get; set; }
        public Sport sport { get; set; }
        public string description { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public Location location { get; set; }
        public List<JoinedUser> joinedUsers { get; set; }
        public User creator { get; set; }
        public Boolean open { get; set; }
        public int minimumUsers { get; set; }
        public int maximumUsers { get; set; }
    }
}
