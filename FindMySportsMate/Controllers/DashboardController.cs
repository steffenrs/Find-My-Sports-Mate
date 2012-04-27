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

            var suggestion = new Suggestion { Title = "Volleyball?" };

            var viewModel = new DashboardViewModel() { SelectedSuggestion = suggestion };

            return View(viewModel);
        }

    }
}
