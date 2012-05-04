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

        private static void validateName(string name)
        {
            if (name == null || name.Equals(""))
                throw new DomainException("Name cannot be null or empty");
        }
    }
}
