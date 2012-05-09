using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using Domain;

namespace PresentationLayer
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        [CustomAuthorize]
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var allSuggestions = SuggestionBusiness.GetAll();

            var viewModel = new DashboardViewModel()
            {
                AllSuggestions = allSuggestions,
                SelectedSuggestion = new SuggestionViewModel
                {
                    Creator = allSuggestions[0].Creator,
                    EndDate = allSuggestions[0].EndDate,
                    IsClosed = allSuggestions[0].IsClosed,
                    JoinedUsers = allSuggestions[0].JoinedUsers.Select(x => new JoinedUserViewModel
                    {
                        User = x.User,
                        Weekdays = x.Weekdays
                    }).ToList(),
                    Location = allSuggestions[0].Location,
                    Sport = allSuggestions[0].Sport,
                    StartDate = allSuggestions[0].StartDate,
                    Title = allSuggestions[0].Title,
                    MaximumUsers = allSuggestions[0].MaximumUsers,
                    Description = allSuggestions[0].Description
                },
                JoinedSuggestions = new List<Suggestion>()
            };

            return View(viewModel);
        }

    }
}

