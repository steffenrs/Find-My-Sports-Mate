using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using System.Text;
using PresentationLayer;
using System.Text.RegularExpressions;
using BusinessLayer;

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
            catch (DomainException e)
            {
                return View("Error");
            }
            catch (Exception e)
            {
                return View("Error");
            }
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
                    int userId = BusinessLayer.UserBusiness.GetUserByEmail(HttpContext.User.Identity.Name).Id;

                    // Update suggestion
                    BusinessLayer.SuggestionBusiness.Update(suggestion, userId);
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
        [HttpPost]
        public ActionResult GetSuggestion(int id)
        {
            Suggestion suggestion = BusinessLayer.SuggestionBusiness.GetById(id);
            suggestion.JoinedUsers = BusinessLayer.JoinedUserBusiness.GetBySuggestion(suggestion.Id);

            var model = new DashboardViewModel { SelectedSuggestion = SuggestionViewModel.FromModel(suggestion) };

            return PartialView("_SuggestionDetails", model.SelectedSuggestion);
        }

        [CustomAuthorizeAttribute]
        [HttpPost]
        public ActionResult Join(int id, Weekdays weekdays)
        {         
            try
            {
                Suggestion suggestion = BusinessLayer.SuggestionBusiness.GetById(id);
                int userId = BusinessLayer.UserBusiness.GetUserByEmail(HttpContext.User.Identity.Name).Id;
                BusinessLayer.SuggestionBusiness.JoinSuggestion(userId, id, weekdays.ToString());

                //get updated location
                suggestion.Location = BusinessLayer.SuggestionBusiness.GetById(id).Location;
                suggestion.JoinedUsers = BusinessLayer.JoinedUserBusiness.GetBySuggestion(suggestion.Id);

                var model = new DashboardViewModel { SelectedSuggestion = SuggestionViewModel.FromModel(suggestion) };
      
                return PartialView("_SuggestionDetails", model.SelectedSuggestion);
            }
            catch(DomainException e)
            {
                return View("Error");
            };
        }

        [CustomAuthorizeAttribute]
        public ActionResult Open(int id)
        {
            int userId = BusinessLayer.UserBusiness.GetUserByEmail(HttpContext.User.Identity.Name).Id;
            BusinessLayer.SuggestionBusiness.Open(id, userId);

            var model = new DashboardViewModel { AllSuggestions = SuggestionBusiness.GetAll(), OwnedSuggestions = SuggestionBusiness.GetByCreator(userId), JoinedSuggestions = SuggestionBusiness.GetByUser(userId) };

            return PartialView("_SuggestionTable", model);
        }

        [CustomAuthorizeAttribute]
        public ActionResult Close(int id)
        {
            int userId = BusinessLayer.UserBusiness.GetUserByEmail(HttpContext.User.Identity.Name).Id;
            BusinessLayer.SuggestionBusiness.Close(id, userId);

            var model = new DashboardViewModel { AllSuggestions = SuggestionBusiness.GetAll(), OwnedSuggestions = SuggestionBusiness.GetByCreator(userId), JoinedSuggestions = SuggestionBusiness.GetByUser(userId) };

            return PartialView("_SuggestionTable", model);
        }
    }
}