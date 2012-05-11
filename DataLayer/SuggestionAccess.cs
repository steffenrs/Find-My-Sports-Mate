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
                suggestion = (from s in db.Suggestion.Include("Sport").Include("JoinedUsers").Include("Creator") where s.Id == id select s).First();
            }

            if (suggestion == null)
                throw new DomainException("Could not find suggestion with id: " + id);
            else
                return suggestion;
        }

        public static void Update(Suggestion updatedSuggestion)
        {
            validateId(updatedSuggestion.Id);

            using (var db = new MyDbContext())
            {
                Suggestion suggestion = db.Suggestion.Single((i => i.Id == updatedSuggestion.Id));

                suggestion.Title = updatedSuggestion.Title;
                suggestion.Sport = updatedSuggestion.Sport;
                suggestion.StartDate = updatedSuggestion.StartDate;
                suggestion.EndDate = updatedSuggestion.EndDate;
                suggestion.Description = updatedSuggestion.Description;
                suggestion.MinimumUsers = updatedSuggestion.MinimumUsers;
                suggestion.MaximumUsers = updatedSuggestion.MaximumUsers;

                db.SaveChanges();
            }


        }

        public static void Delete(int id)
        {
            validateId(id);



        }

        public static List<Suggestion> GetAll()
        {
            List<Suggestion> allSuggestions = new List<Suggestion>();
           
            using (var db = new MyDbContext())
            {
                var care = (from s in db.Suggestion.Include("Sport").Include("JoinedUsers").Include("Creator") select s).ToList();
                return care;
            }

            //TODO: THROW EXCEPTION!
        }

        private static void validateId(int id)
        {
            if (id < 1)
                throw new DomainException("Suggestion id cannot be less than 1");
        }
    }
}

