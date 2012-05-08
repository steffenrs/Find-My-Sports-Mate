using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace DataAccessLayer
{
    public static class SportAccess
    {
        public static Sport GetByName(string name)
        {
            validateName(name);

            Sport sport;
            using (var db = new MyDbContext())
            {
                sport = db.Sport.Where(p => p.Name == name).FirstOrDefault();
            }

            if (sport == null)
                throw new DomainException("Could not find sport with name: " + name);
            else
                return sport;
        }

        public static void Create(Sport suggestionSport)
        {
            validateSport(suggestionSport);
            using (var db = new MyDbContext())
            {
                db.Sport.Add(suggestionSport);
                db.SaveChanges();
            }
        }

        private static void validateName(string name)
        {
            if (name == null || name.Equals(""))
                throw new DomainException("Name cannot be null or empty");
        }

        private static void validateSport(Sport sport)
        {
            if (sport == null)
                throw new DomainException("Sport cannot be a null reference");
            validateName(sport.Name);
        }
    }
}
