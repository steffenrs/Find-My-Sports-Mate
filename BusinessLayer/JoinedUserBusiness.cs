using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using DataAccessLayer;

namespace BusinessLayer
{
    public class JoinedUserBusiness
    {
        public static List<JoinedUser> GetBySuggestion(int id)
        {
            return JoinedUserAccess.GetForSuggestion(id);
        }
    }
}
