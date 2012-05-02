using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace BusinessLayer
{
    public static class SuggestionBusiness
    {
        public static void WriteSuggestionToDatabase(Suggestion suggestion)
        {
            throw new DomainException("Database not implemented with suggestions.");
        }
    }
}
