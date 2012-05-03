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
                Sport suggestionSport;
                try { suggestionSport = BusinessLayer.SportBusiness.GetByName(model.Sport); }
                catch (DomainException e) { suggestionSport = new Sport { Name = model.Sport }; }

                User currentUser = new User { Id = 1 };
       
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

        public JsonResult GetSuggestion(int id)
        {
            Suggestion suggestion = BusinessLayer.SuggestionBusiness.Get(id);

            return Json(suggestion, JsonRequestBehavior.AllowGet);
        }

     

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Suggestion suggestion;
            try { suggestion = BusinessLayer.SuggestionBusiness.Get(Id); }
            catch (DomainException e) { return null; }
            
            CreateSuggestionViewModel viewModel = new CreateSuggestionViewModel {   Title = suggestion.Title, 
                                                                                    Sport = suggestion.Sport.Name, 
                                                                                    StartDate = suggestion.StartDate, 
                                                                                    EndDate = suggestion.EndDate, 
                                                                                    Description = suggestion.Description, 
                                                                                    MinPeople = suggestion.MinimumUsers, 
                                                                                    MaxPeople = suggestion.MaximumUsers };
            return View("Create", viewModel);
        }

        [HttpPost]
        public ActionResult Edit(CreateSuggestionViewModel model)
        {
            return null;
        }
    }
}