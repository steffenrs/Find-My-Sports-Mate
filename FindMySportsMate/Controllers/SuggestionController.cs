using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using System.Text;
using PresentationLayer;

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
                    BusinessLayer.SuggestionBusiness.Create(suggestion);
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
                Suggestion suggestion = BusinessLayer.SuggestionBusiness.Read(Id);
                CreateSuggestionViewModel viewModel = new CreateSuggestionViewModel
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

                return View("Create", viewModel);
            }
            catch (DomainException e) { return View("Error"); } // didnt find suggestion 404
        }

        [HttpPost]
        public ActionResult Edit(CreateSuggestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Suggestion suggestion = GetDomainFromViewModel(model);
                suggestion.Id = model.OriginalId;

                try
                {
                    BusinessLayer.SuggestionBusiness.Update(suggestion);
                    return RedirectToAction("Index", "Dashboard");
                }
                catch (DomainException e)
                {
                    ViewBag.ExceptionMessage = e.Message;
                    return View("Create", model);
                }
            }
            else
            {
                return View("Create", model);
            }
        }

        private Suggestion GetDomainFromViewModel(CreateSuggestionViewModel model)
        {
            //Fetch user info
            User currentUser = new User { Id = 1 };

            // Fetch sport
            Sport suggestionSport;
            try { suggestionSport = BusinessLayer.SportBusiness.GetByName(model.Sport); }
            catch (DomainException e)
            {
                suggestionSport = new Sport { Name = model.Sport };
                BusinessLayer.SportBusiness.Save(suggestionSport);
            }

            Suggestion suggestion = new Suggestion
            {
                Title = model.Title,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Description = model.Description,
                MinimumUsers = model.MinPeople,
                MaximumUsers = model.MaxPeople,
                SportId = suggestionSport.Name,
                CreatorId = currentUser.Id,
            };

            return suggestion;
        }

        [CustomAuthorizeAttribute]
        public JsonResult GetSuggestion(int id)
        {
            Suggestion suggestion = BusinessLayer.SuggestionBusiness.Get(id);
            //Manually specify properties to prevent circual reference error.
            return Json(
                new
                {
                    SelectedSuggestion = SuggestionViewModel.FromModel(suggestion)
                }, JsonRequestBehavior.AllowGet);
        }
    }
}