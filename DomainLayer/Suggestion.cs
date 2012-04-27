using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Suggestion
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public Sport Sport { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Location Location { get; set; }
        public List<JoinedUser> JoinedUsers { get; set; }
        public User Creator { get; set; }
        public Boolean Open { get; set; }
        public int MinimumUsers { get; set; }
        public int MaximumUsers { get; set; }
    }
}
