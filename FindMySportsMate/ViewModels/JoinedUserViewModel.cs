using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace PresentationLayer
{
    public class JoinedUserViewModel
    {
        public User User { get; set; }
        public string Weekdays { get; set; }
    }
}