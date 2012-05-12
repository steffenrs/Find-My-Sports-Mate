using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace DataAccessLayer
{
    public class JoinedUserAccess
    {
        public static void Add(JoinedUser joinedUser)
        {
            using (var db = new MyDbContext())
            {
                db.JoinedUser.Add(joinedUser);
                db.SaveChanges();
            }
        }

        public static List<JoinedUser> GetForSuggestion(int suggestionId)
        {
            using (var db = new MyDbContext())
            {
                return (from j in db.JoinedUser.Include("User") where j.SuggestionId == suggestionId select j).ToList();
            }
        }
    }
}
