using System;
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
                throw new DomainException("Could not update user", e);
            }
        }

        public static void Register(User user)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.User.Add(user);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new DomainException("Could not create user", e);
            }
        }

        public static User GetUserByEmail(string userEmail)
        {
            try
            {
                if (userEmail == null || userEmail.Equals(""))
                    throw new DomainException("Invalid email address");

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
            try
            {
                if (userId < 0)
                    throw new DomainException("Invalid user id");

                using (var db = new MyDbContext())
                {
                    return db.User.Find(userId);
                }
            }
            catch (Exception e)
            {
                throw new DomainException("Could not fetch user", e);
            }
        }
    }
}
