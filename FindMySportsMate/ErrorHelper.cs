using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace PresentationLayer
{
    public static class ErrorHelper
    {
        public static ErrorViewModel ErrorModelForDomainException(DomainException e)
        {
            String innerMessage = "";
            if (e.InnerException != null)
                innerMessage = e.InnerException.Message;

            ErrorViewModel model = new ErrorViewModel { domainErrorMessage = e.Message, innerErrorMessage = innerMessage };
            return model;
        }
    }
}