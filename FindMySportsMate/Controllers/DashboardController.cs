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
            Suggestion selectedSuggestion = null;
            if (allSuggestions.Count > 0)
            {
                selectedSuggestion = allSuggestions[0];
                selectedSuggestion.JoinedUsers = JoinedUserBusiness.GetBySuggestion(selectedSuggestion.Id);
            }
            var viewModel = new DashboardViewModel()
            {
                AllSuggestions = allSuggestions,
                SelectedSuggestion = selectedSuggestion == null ? null : SuggestionViewModel.FromModel(selectedSuggestion),
                JoinedSuggestions = new List<Suggestion>()
            };

            return View(viewModel);
        }

    }
}

