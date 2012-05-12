using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Domain;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer
{
    public class SuggestionViewModel
    {
        public int Id { get; set; }
        public Sport Sport { get; set; }
        public String Title { get; set; }
        public User Creator { get; set; }
        [Display(Name="Time Period")]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Display(Name="Status")]
        [UIHint("IsClosed")]
        public bool IsClosed { get; set; }
        [UIHint("Location")]
        public Location Location { get; set; }
        [Display(Name = "Users")]
        public List<JoinedUserViewModel> JoinedUsers { get; set; }
        public String Description { get; set; }
        public int MaximumUsers { get; set; }

        public static SuggestionViewModel FromModel(Suggestion suggestion)
        {
            return new SuggestionViewModel
            {
                Id = suggestion.Id,
                Creator = suggestion.Creator,
                EndDate = suggestion.EndDate,
                IsClosed = suggestion.IsClosed,
                JoinedUsers = suggestion.JoinedUsers.Select(x => new JoinedUserViewModel
                {
                    User = x.User,
                    Weekdays = x.Weekdays
                }).ToList(),
                Location = suggestion.Location,
                Sport = suggestion.Sport,
                StartDate = suggestion.StartDate,
                Title = suggestion.Title,
                MaximumUsers = suggestion.MaximumUsers,
                Description = suggestion.Description
            };
        }
    }
}