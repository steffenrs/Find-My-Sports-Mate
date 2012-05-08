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
            ValidateName(name);

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

        private static void ValidateName(string name)
        {
            if (name == null || name.Equals(""))
                throw new DomainException("Name cannot be null or empty");
        }

        public static void Save(Sport suggestionSport)
        {
            using (var db = new MyDbContext())
            {
                db.Sport.Add(suggestionSport);
                db.SaveChanges();
            }
        }
    }
}
