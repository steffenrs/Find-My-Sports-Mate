using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentationLayer.Controllers
{
    public class SportController : Controller
    {
        [CustomAuthorizeAttribute]
        public String GetSports()
        {
            return BusinessLayer.SportBusiness.GetAll();
        }
    }
}
