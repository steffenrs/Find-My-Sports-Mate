using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace DataAccessLayer
{
    public static class SuggestionAccess
    {
        public static void Create(Suggestion suggestion)
        {
            using (var db = new MyDbContext())
            {
                db.Suggestion.Add(suggestion);
                db.JoinedUser.Add(suggestion.JoinedUsers[0]);

                db.SaveChanges();
            }
        }
    }
}
