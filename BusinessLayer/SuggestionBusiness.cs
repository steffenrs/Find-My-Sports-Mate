using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using DataAccessLayer;

namespace BusinessLayer
{
    public static class SuggestionBusiness
    {
        public static void HandleNewSuggestion(Suggestion suggestion)
        {
            SuggestionAccess.CreateSuggestion(suggestion);
        }
    }
}
