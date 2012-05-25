using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace DataAccessLayer
{
    public class LocationAccess
    {
        public static List<Location> getAll()
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    return db.Location.ToList();
                }
            }
            catch (Exception e)
            {
                throw new DomainException("Could not get all locations", e);
            }
        }

    }
}
