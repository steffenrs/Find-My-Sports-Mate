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
        public static void New(Suggestion suggestion)
        {
            SuggestionAccess.CreateSuggestion(suggestion);
        }

        public static Suggestion Get(int id)
        {
            return null;
        }

        public static List<Suggestion> GetAll()
        {
            return new List<Suggestion>();
        }

        public static List<Suggestion> GetByUser(int userId)
        {
            return new List<Suggestion>();
        }
    }
}
