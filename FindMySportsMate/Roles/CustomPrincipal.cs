using System.Web.Security;
using System;
using System.Security.Principal;

namespace PresentationLayer
{

    public class CustomPrincipal : RolePrincipal
    {
        public string UserID { get; set; }

        public CustomPrincipal(String name, IIdentity provider, string userID)
            : base(name, provider)
        {
            UserID = userID;
        }
    }
}