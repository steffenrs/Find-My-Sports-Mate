using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;

namespace PresentationLayer.Controllers
{
    public class SuggestionController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateSuggestionViewModel model)
        {
            // TODO: validate div fields


            // TODO: map from viewModel to suggestion
            Suggestion suggestion = new Suggestion();
            suggestion.Title = model.Title;
            suggestion.StartDate = model.StartDate;
            suggestion.EndDate = model.EndDate;

            // TODO: write to database via Business layer
            try
            {
                BusinessLayer.SuggestionBusiness.WriteSuggestionToDatabase(suggestion);
            }
            catch (DomainException e)
            {
                // TODO: implement exception handling
            }

            // TODO: redirect with right view
            return View();
        }

    }
}