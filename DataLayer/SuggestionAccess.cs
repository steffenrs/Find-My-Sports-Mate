using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using System.Data.Objects;

namespace DataAccessLayer
{
    public static class SuggestionAccess
    {
        public static void Create(Suggestion suggestion)
        {
            using (var db = new MyDbContext())
            {
                db.JoinedUser.Add(suggestion.JoinedUsers[0]);
                db.Suggestion.Add(suggestion);
                db.SaveChanges();
            }
        }

        public static Suggestion Get(int id)
        {
            validateId(id);

            Suggestion suggestion;
            using (var db = new MyDbContext())
            {
                suggestion = (from s in db.Suggestion.Include("Sport").Include("JoinedUsers").Include("Creator").Include("Location") where s.Id == id select s).First();
            }

            if (suggestion == null)
                throw new DomainException("Could not find suggestion with id: " + id);
            else
                return suggestion;
        }

        public static void Update(Suggestion updatedSuggestion)
        {
            using (var db = new MyDbContext())
            {
                var suggestion = db.Suggestion.Single((i => i.Id == updatedSuggestion.Id));
                updatedSuggestion.CreatorId = suggestion.CreatorId;
                db.Entry(suggestion).CurrentValues.SetValues(updatedSuggestion);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (var db = new MyDbContext())
            {
                Suggestion suggestion = db.Suggestion.Single((i => i.Id == id));
                db.Suggestion.Remove(suggestion);
                db.SaveChanges();
            }
        }

        public static List<Suggestion> GetAllByCreator(int creatorId)
        {
            using (var db = new MyDbContext())
            {
                return (from s in db.Suggestion
                        .Include("Sport")
                        .Include("JoinedUsers")
                        .Include("Creator") 
                        .Include("Location")
                        where s.CreatorId == creatorId 
                        select s).ToList();
            }
        }

         public static List<Suggestion> GetByJoinedUser(int userId)
        {
            using (var db = new MyDbContext())
            {
                var query = from s in db.Suggestion
                            from ju in db.JoinedUser
                            where ju.UserId == userId
                            && s.Id == ju.SuggestionId
                            select new
                            {
                                s,
                                sport = s.Sport,
                                ju = s.JoinedUsers
                            };

                var suggestions = query.AsEnumerable().Select(m => m.s);

                return suggestions.ToList();
            }
        }

        public static List<Suggestion> GetAll()
        {
            using (var db = new MyDbContext())
            {
                return (from s in db.Suggestion
                        .Include("Sport")
                        .Include("JoinedUsers")
                        .Include("Creator") 
                        .Include("Location")
                        where s.IsClosed == false
                        select s).ToList();
            }
        }

        private static void validateId(int id)
        {
            if (id < 1)
                throw new DomainException("Suggestion id cannot be less than 1");
        }       
    }
}

