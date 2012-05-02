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
        public static void Update(User currentUser, User newUser)
        {
            User user = ExtensionMethods.CopyTo<User>(currentUser,newUser);
            //database stuff
        }

        public static void Register(User user) 
        {
            using (var db = new MyDbContext())
            {
                db.User.Add(user);
                db.SaveChanges();
            }
        }

        public static User GetUserByEmail(string userEmail)
        {
            var user = new User();
            using (var db = new MyDbContext())
            {
                var dbUser = db.User.Where(u => u.Email == userEmail).FirstOrDefault();
                user = (User)dbUser;
            }

            return user;
        }

        public static User GetUserById(int userId)
        {
            User user = new User();
            using (var db = new MyDbContext())
            {
                user = db.User.Find(userId);
            }
            return user;
        }

        //public static List<User> GetAllUsers()
        //{
        //    //
        //}

        public static void Delete(User user)
        {
            using (var db = new MyDbContext())
            {
                user = db.User.Remove(user);
            }
        }
    }
}
