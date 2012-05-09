using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace BusinessLayer
{
    public static class SportBusiness
    {
        public static Sport Get(string name)
        {
            return DataAccessLayer.SportAccess.GetOrCreate(name);
        }

        public static void Create(Sport suggestionSport)
        {
            DataAccessLayer.SportAccess.Create(suggestionSport);
        }
    }
}
