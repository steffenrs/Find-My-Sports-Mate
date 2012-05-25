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

        public static String GetAll()
        {
            List<Sport> sports = DataAccessLayer.SportAccess.GetAll();

            StringBuilder formattedString = new StringBuilder();
            
            foreach (Sport sport in sports)
            {
                formattedString.Append(sport.Name + ",");
            }

            // Remove last comma
            if (formattedString.Length > 0)
                formattedString.Remove(formattedString.Length - 1, 1);

            return formattedString.ToString();
        }
    }
}