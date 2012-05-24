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
            try
            {
                using (var db = new MyDbContext())
                {
                    db.JoinedUser.Add(suggestion.JoinedUsers[0]);
                    db.Suggestion.Add(suggestion);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new DomainException("Could not create suggestion", e);
            } 
        }

        public static Suggestion Get(int id)
        {
            try
            {
                validateId(id);

                using (var db = new MyDbContext())
                {
                    return (from s in db.Suggestion.Include("Sport").Include("JoinedUsers").Include("Creator").Include("Location") where s.Id == id select s).First();
                }
            }
            catch (Exception e)
            {
                throw new DomainException("Could not fetch suggestion", e);
            }
        }

        public static void Update(Suggestion updatedSuggestion)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var suggestion = db.Suggestion.Single((i => i.Id == updatedSuggestion.Id));
                    updatedSuggestion.CreatorId = suggestion.CreatorId;
                    db.Entry(suggestion).CurrentValues.SetValues(updatedSuggestion);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new DomainException("Could not update suggestion", e);
            }
        }

        public static List<Suggestion> GetAllByCreator(int creatorId)
        {
            try
            {
                validateId(creatorId);

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
            catch (Exception e)
            {
                throw new DomainException("Could not get suggestions by creator", e);
            }
        }

        public static List<Suggestion> GetByJoinedUser(int userId)
        {
            try
            {
                validateId(userId);

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
            catch (Exception e)
            {
                throw new DomainException("Could not get suggestions by joined user", e);
            }
        }

        public static List<Suggestion> GetAll()
        {
            try
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
            catch (Exception e)
            {
                throw new DomainException("Could not get all suggestions", e);
            }
        }

        private static void validateId(int id)
        {
            if (id < 0)
                throw new DomainException("Invalid suggestion id");
        }       
    }
}

