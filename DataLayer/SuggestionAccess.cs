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
                db.SaveChanges();
            }
        }

        public static Suggestion Read(int id)
        {
            validateId(id);

            Suggestion suggestion;
            using (var db = new MyDbContext())
            {
                suggestion = db.Suggestion.Find(id);
            }

            if (suggestion == null)
                throw new DomainException("Could not find suggestion with id: " + id);
            else
                return suggestion;
        }

        public static void Update(Suggestion suggestion)
        {
            validateId(suggestion.Id);



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
                var care = (from s in db.Suggestion.Include("Sport").Include("JoinedUsers") select s).ToList();
                return care;
            }

            //TODO: THROW EXCEPTION!
        }

        private static void validateId(int id)
        {
            if (id < 1)
                throw new DomainException("Suggestion id cannot be less than 1");
        }


        public static Suggestion Get(int id)
        {
            using (var db = new MyDbContext())
            {
                var suggestion = (from s in db.Suggestion.Include("Sport").Include("JoinedUsers") select s).First();
                return suggestion;
            }

            //TODO: throw exception
        }
    }
}
