using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Domain;

namespace DataAccessLayer
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base() {
            Database.Connection.ConnectionString = Properties.Settings.Default.Database;
        }

       // public DbSet<AddressBook> Test { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Suggestion> Suggestion { get; set; }
        public DbSet<Sport> Sport { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<JoinedUser> JoinedUser { get; set; }
    }
}
