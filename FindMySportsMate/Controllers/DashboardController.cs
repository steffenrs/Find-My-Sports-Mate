﻿using System;
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
            var suggestion = new Suggestion
            {
                Title = "Volley in winter?",
                Creator = new Domain.User { FirstName = "Frank the tank" },
                IsClosed = false,
                Description = "Keen på å spille volleyball eller?",
                Sport = new Sport { Name = "Volleyball" },
                MinimumUsers = 5,
                MaximumUsers = 10,
                StartDate = new DateTime(2012, 5, 5),
                EndDate = new DateTime(2012, 5, 15),
                Location = new Location { Name = "Helvete" },
                JoinedUsers = new List<JoinedUser> { new JoinedUser { User = new User { FirstName="PETAH" } } }
            };

            var viewModel = new DashboardViewModel() { SelectedSuggestion = suggestion };

            return View(viewModel);
        }

    }
}
