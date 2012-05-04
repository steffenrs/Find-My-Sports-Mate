using System.Web.Mvc;
using System.Web;
namespace PresentationLayer
{


    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        //basic auth that allowes everybody that is logged in to view
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return (HttpContext.Current.Request.IsAuthenticated);
        }
    }
}