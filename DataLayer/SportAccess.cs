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
            try
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
            catch (Exception e)
            {
                throw new DomainException("Could not find or create sport", e);
            }
        }

        public static List<Sport> GetAll()
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    return (from s in db.Sport select s).ToList();
                }
            }
            catch (Exception e)
            {
                throw new DomainException("Could not get all sports", e);
            }   
        }

        private static void validateName(string name)
        {
            if (name == null || name.Equals(""))
                throw new DomainException("Invalid sport");
        }

        private static void validateSport(Sport sport)
        {
            if (sport == null)
                throw new DomainException("Invalid sport");
            validateName(sport.Name);
        }
    }
}
