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
            BusinessLayer.AddressBusiness.GetAllAddresses();

            var suggestion = new Suggestion
            {
                Title = "Volley in winter?",
                Description = "Keen på å spille volleyball eller?",
                Sport = new Sport { Name = "Volleyball" },
                MinimumUsers = 5,
                MaximumUsers = 10,
                StartDate = new DateTime(2012, 5, 5),
                EndDate = new DateTime(2012, 5, 15),
                JoinedUsers = new List<JoinedUser> { new JoinedUser { User = new User { FirstName="PETAH" } } }
            };

            var viewModel = new DashboardViewModel() { SelectedSuggestion = suggestion };

            return View(viewModel);
        }

    }
}
