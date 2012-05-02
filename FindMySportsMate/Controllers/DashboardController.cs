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

        public ActionResult Index()
        {
            var allSuggestions = SuggestionBusiness.GetAll();

            var viewModel = new DashboardViewModel() 
            { 
                AllSuggestions = allSuggestions, 
                SelectedSuggestion = allSuggestions[0],
                JoinedSuggestions = new List<Suggestion>()
            };

            return View(viewModel);
        }

    }
}
