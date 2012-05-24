﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using DataAccessLayer;

namespace DataAccessLayer
{
    public class UserAccess
    {
        public static void Update(User newUser)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    User dbUser = db.User.Single(i => i.Id == newUser.Id);
                    dbUser.Area = newUser.Area;
                    dbUser.BirthDate = newUser.BirthDate;
                    dbUser.FirstName = newUser.FirstName;
                    dbUser.Gender = newUser.Gender;
                    dbUser.LastName = newUser.LastName;
                    dbUser.PhoneNumber = newUser.PhoneNumber;
                    dbUser.State = newUser.State;
                    dbUser.StreetAddress = newUser.StreetAddress;
                    dbUser.Password = newUser.Password;
                    db.SaveChanges();

                }
            }
            catch (Exception e)
            {
            }
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
            try
            {
                var user = new User();
                using (var db = new MyDbContext())
                {
                    var dbUser = db.User.Where(u => u.Email == userEmail).First();
                    user = (User)dbUser;
                }

                return user;
            }
            catch (Exception e)
            {
                throw new DomainException("Could not fetch user", e);
            } 
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
    }
}
