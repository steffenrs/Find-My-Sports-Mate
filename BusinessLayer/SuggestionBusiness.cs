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
            throw new DomainException("Database not implemented for 'Suggestion'");
            SuggestionAccess.CreateSuggestion(suggestion);
        }
    }
}
