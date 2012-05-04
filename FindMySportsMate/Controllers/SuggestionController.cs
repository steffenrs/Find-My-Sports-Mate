using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using System.Text;

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
            if (ModelState.IsValid)
            {
                Sport suggestionSport;
                try { suggestionSport = BusinessLayer.SportBusiness.GetByName(model.Sport); }
                catch (DomainException e) { suggestionSport = new Sport { Name = model.Sport }; }

                User currentUser = new User { Id = 1 };
                JoinedUser joinedUser = new JoinedUser { UserId = currentUser.Id, Weekdays = getWeeklyParticipation(model) };
       
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
                    JoinedUsers = new List<JoinedUser> { joinedUser }
                };
                
                try
                {
                    BusinessLayer.SuggestionBusiness.New(suggestion);
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

        private String getWeeklyParticipation(CreateSuggestionViewModel model)
        {
            StringBuilder weeklyActivity = new StringBuilder();

            if (model.Monday == true)
                weeklyActivity.Append("Mo,");
            if (model.Tuesday == true)
                weeklyActivity.Append("Tu,");
            if (model.Wednesday == true)
                weeklyActivity.Append("We,");
            if (model.Thursday == true)
                weeklyActivity.Append("Th,");
            if (model.Friday == true)
                weeklyActivity.Append("Fr,");
            if (model.Saturday == true)
                weeklyActivity.Append("Sa,");
            if (model.Sunday == true)
                weeklyActivity.Append("Su,");

            // Remove last comma character
            if (weeklyActivity.Length > 0)
                weeklyActivity.Remove(weeklyActivity.Length - 1, 1);

            return weeklyActivity.ToString();
        }

        [CustomAuthorizeAttribute]
        public JsonResult GetSuggestion(int id)
        {
            Suggestion suggestion = BusinessLayer.SuggestionBusiness.Get(id);

            return Json(suggestion, JsonRequestBehavior.AllowGet);
        }

     

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var care = User.Identity;
            Suggestion suggestion = BusinessLayer.SuggestionBusiness.Get(Id);

            if (suggestion == null)
            {
                //TODO return 404 view with message
                return null;
            }
            else
            {
                CreateSuggestionViewModel viewModel = new CreateSuggestionViewModel {   Title = suggestion.Title, 
                                                                                        Sport = suggestion.Sport.Name, 
                                                                                        StartDate = suggestion.StartDate, 
                                                                                        EndDate = suggestion.EndDate, 
                                                                                        Description = suggestion.Description, 
                                                                                        MinPeople = suggestion.MinimumUsers, 
                                                                                        MaxPeople = suggestion.MaximumUsers };

                return View("Create", viewModel);
            }
        }
    }
}