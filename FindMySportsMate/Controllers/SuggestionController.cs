using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using System.Text;
using PresentationLayer;
using System.Text.RegularExpressions;

namespace PresentationLayer.Controllers
{
    public class SuggestionController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateSuggestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Suggestion suggestion = GetDomainFromViewModel(model);
              
                try
                {
                    // Add creator id
                    User currentUser = BusinessLayer.UserBusiness.GetUserByEmail(HttpContext.User.Identity.Name);
                    suggestion.CreatorId = currentUser.Id;

                    // Create suggestion
                    BusinessLayer.SuggestionBusiness.Create(suggestion, model.Weekdays.ToString());
                    return RedirectToAction("Index", "Dashboard");
                }
                catch (DomainException e)
                {
                    ViewBag.ExceptionMessage = e.Message;
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            try
            {
                Suggestion suggestion = BusinessLayer.SuggestionBusiness.GetById(Id);
                EditSuggestionViewModel viewModel = new EditSuggestionViewModel
                {
                    Title = suggestion.Title,
                    Sport = suggestion.Sport.Name,
                    StartDate = suggestion.StartDate,
                    EndDate = suggestion.EndDate,
                    Description = suggestion.Description,
                    MinPeople = suggestion.MinimumUsers,
                    MaxPeople = suggestion.MaximumUsers,
                    OriginalId = suggestion.Id
                };

                return View(viewModel);
            }
            catch (DomainException e) { return View("Error"); } // didnt find suggestion 404
        }

        [HttpPost]
        public ActionResult Edit(EditSuggestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Suggestion suggestion = GetDomainFromViewModel(model);
                suggestion.Id = model.OriginalId;

                try
                {
                    // Validate suggestion with user
                    User currentUser = BusinessLayer.UserBusiness.GetUserByEmail(HttpContext.User.Identity.Name);

                    // Update suggestion
                    BusinessLayer.SuggestionBusiness.Update(suggestion, currentUser.Id);
                    return RedirectToAction("Index", "Dashboard");
                }
                catch (DomainException e)
                {
                    ViewBag.ExceptionMessage = e.Message;
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        private Suggestion GetDomainFromViewModel(CreateSuggestionViewModel model)
        {
            // Get or create sport
            var suggestionSport = BusinessLayer.SportBusiness.Get(model.Sport);

            Suggestion suggestion = new Suggestion
            {
                Title = model.Title,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Description = model.Description,
                MinimumUsers = model.MinPeople,
                MaximumUsers = model.MaxPeople,
                SportId = suggestionSport.Name,
                JoinedUsers = new List<JoinedUser>()
                {
                    
                }
            };

            return suggestion;
        }

        [CustomAuthorizeAttribute]
        public JsonResult GetSuggestion(int id)
        {
            Suggestion suggestion = BusinessLayer.SuggestionBusiness.GetById(id);

            //Manually specify properties to prevent circual reference error.
            return Json(
                new
                {
                    SelectedSuggestion = SuggestionViewModel.FromModel(suggestion)
                }, JsonRequestBehavior.AllowGet);
        }
    }
}