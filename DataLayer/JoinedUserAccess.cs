using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace DataAccessLayer
{
    public class JoinedUserAccess
    {
        public static void AddJoinedUser(JoinedUser joinedUser)
        {
            using (var db = new MyDbContext())
            {
                db.JoinedUser.Add(joinedUser);
                db.SaveChanges();
            }
        }
    }
}
