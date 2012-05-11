using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace DataAccessLayer
{
    class LocationAccess
    {
        public static List<Location> getAll()
        {
            using (var db = new MyDbContext())
            {
                return db.Location.ToList();
            }
        }

    }
}
