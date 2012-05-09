using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace PresentationLayer
{
    public class DashboardViewModel
    {
        public List<Suggestion> AllSuggestions {get; set;}
        public List<Suggestion> JoinedSuggestions { get; set; }
        public SuggestionViewModel SelectedSuggestion { get; set; }
    }
}