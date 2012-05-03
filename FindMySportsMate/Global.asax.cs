using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Security.Principal;
using System.Threading;

namespace PresentationLayer
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie formsCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (formsCookie != null)
            {
                FormsAuthenticationTicket auth = FormsAuthentication.Decrypt(formsCookie.Value);

                string userId = auth.UserData;
                //Guid userID = new Guid(auth.UserData);

                var principal = new CustomPrincipal(Roles.Provider.Name, new GenericIdentity(auth.Name), userId);

                Context.User = Thread.CurrentPrincipal = principal;
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }


        
    }


}