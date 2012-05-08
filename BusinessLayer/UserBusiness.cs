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

        public static User GetUserByEmail(string userEmail)
        {
            return DataAccessLayer.UserAccess.GetUserByEmail(userEmail);
        }

        public static User GetUserById(int userId)
        {
            return DataAccessLayer.UserAccess.GetUserById(userId);
        }

        //public static List<User> GetAllUsers()
        //{
        //    //
        //}

        public static void Delete(User user)
        {
            DataAccessLayer.UserAccess.Delete(user);
        }
    }
}
