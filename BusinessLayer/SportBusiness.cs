using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace BusinessLayer
{
    public static class SportBusiness
    {
        public static Sport GetByName(string name)
        {
            return DataAccessLayer.SportAccess.GetByName(name);
        }
    }
}
