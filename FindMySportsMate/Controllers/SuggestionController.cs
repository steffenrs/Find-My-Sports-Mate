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
            // TODO: validate div fields


            Sport suggestionSport = new Sport { Name = model.Sport };

            Suggestion suggestion = new Suggestion {    Title = model.Title, 
                                                        StartDate = model.StartDate,
                                                        EndDate = model.EndDate, 
                                                        Description = model.Description, 
                                                        MinimumUsers = model.MinPeople, 
                                                        MaximumUsers = model.MaxPeople,  
                                                        Sport = suggestionSport };
            try
            {
                BusinessLayer.SuggestionBusiness.HandleNewSuggestion(suggestion);
            }
            catch (DomainException e)
            {
                ViewBag.ExceptionMessage = e.Message;
                return View();
            }

            return RedirectToAction("Index", "Dashboard");
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

        public JsonResult GetSuggestion(int id)
        {
            var suggestion = new Suggestion
            {
                Title = "WeTennis!?",
                Description = "TenSing",
                Sport = new Sport { Name = "Tennis" },
                MinimumUsers = 2,
                MaximumUsers = 8,
                StartDate = new DateTime(2012, 5, 6),
                EndDate = new DateTime(2012, 5, 16),
                Open = true,
                JoinedUsers = new List<JoinedUser> { new JoinedUser { User = new User { FirstName = "HerreGud", UserName = "PetterPan" } } }
            };

            return Json(suggestion, JsonRequestBehavior.AllowGet);
        }
    }
}