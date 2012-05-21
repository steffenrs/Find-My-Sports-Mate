using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace DataAccessLayer
{
    public static class SportAccess
    {
        public static Sport GetOrCreate(string name)
        {
            validateName(name);

            Sport sport;
            using (var db = new MyDbContext())
            {
                sport = db.Sport.Where(p => p.Name == name).FirstOrDefault();

                if (sport == null)
                {
                    sport = new Sport();
                    sport.Name = name;
                    db.Sport.Add(sport);
                    db.SaveChanges();
                }

                return sport;
            }
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

        public static List<Sport> GetAll()
        {
            using (var db = new MyDbContext())
            {
                return (from s in db.Sport select s).ToList();
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
