using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Domain;

namespace PresentationLayer
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base() {
            Database.Connection.ConnectionString = Properties.Settings.Default.Database;
        }

        public DbSet<AddressBook> Test { get; set; }
 //       public DbSet<User> User { get; set; }
    }
}
