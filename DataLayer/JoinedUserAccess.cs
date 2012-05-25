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
            try
            {
                using (var db = new MyDbContext())
                {
                    db.JoinedUser.Add(joinedUser);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new DomainException("Could not add joined user", e);
            }
        }

        public static List<JoinedUser> GetForSuggestion(int suggestionId)
        {
            if (suggestionId < 0)
                throw new DomainException("Invalid suggestion id");

            try
            {
                using (var db = new MyDbContext())
                {
                    return (from j in db.JoinedUser.Include("User") where j.SuggestionId == suggestionId select j).ToList();
                }
            }
            catch (Exception e)
            {
                throw new DomainException("Could not get joined users for suggestion", e);
            }
        }
    }
}
