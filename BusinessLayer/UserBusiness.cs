using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using DataAccessLayer;

namespace BusinessLayer
{
    public static class UserBusiness
    {
        public static void Update(User newUser)
        {
            DataAccessLayer.UserAccess.Update(newUser);
        }

        public static void Register(User user) 
        {
            DataAccessLayer.UserAccess.Register(user);
        }

        public static User Get(string userEmail)
        {
            return DataAccessLayer.UserAccess.GetUserByEmail(userEmail);
        }

        public static User Get(int userId)
        {
            return DataAccessLayer.UserAccess.GetUserById(userId);
        }
    }
}
