using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Domain;

namespace PresentationLayer
{
    public class SuggestionViewModel
    {
        public Sport Sport { get; set; }
        public String Title { get; set; }
        public User Creator { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsClosed { get; set; }
        public Location Location { get; set; }
        public List<JoinedUserViewModel> JoinedUsers { get; set; }
        public String Description { get; set; }
        public int MaximumUsers { get; set; }
    }
}