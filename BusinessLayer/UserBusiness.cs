﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace BusinessLayer
{
    public static class UserBusiness
    {
        public static void UpdateUser(User currentUser, User newUser)
        {
            User user = ExtensionMethods.CopyTo<User>(currentUser,newUser);
            //database stuff
        }

        public static void RegisterUser(User user) 
        {
        }

        public static User GetUser(User user)
        {
            return user;
        }

        public static User GetUserById(int userId)
        {
            User user = new User();
            return user;
        }

        //public static List<User> GetAllUsers()
        //{
        //    //
        //}

        public static void DeleteUser(User user)
        {
        }
    }
}
