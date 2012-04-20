using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace PresentationLayer
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            var addresses = AddressBusiness.GetAllAddresses();

            var viewModel = new DashboardViewModel() { Addresses = addresses };

            return View(viewModel);
        }

    }
}
