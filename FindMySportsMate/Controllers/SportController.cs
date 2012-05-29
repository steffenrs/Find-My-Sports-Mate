using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentationLayer
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
