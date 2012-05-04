using System.Web.Security;
using System;
using System.Security.Principal;

namespace PresentationLayer
{

    class CustomPrincipal : RolePrincipal
    {
        public Guid UserID { get; set; }

        public CustomPrincipal(String name, IIdentity provider, Guid userID)
            : base(name, provider)
        {
            UserID = userID;
        }
    }
}