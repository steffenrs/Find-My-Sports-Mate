using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using Domain;

namespace PresentationLayer
{
    public class Database
    {
        private static DataContext db;
        static Database()
        {
             db = new DataContext("Data Source=localhost;Initial Catalog=AddressDB;Integrated Security=True");
        }

        public static List<Domain.AddressBook> Get()
        {     
            Table<DataAddressBook> addressBook = db.GetTable<DataAddressBook>();
            var q = from a in addressBook select a;

            var domainList = new List<Domain.AddressBook>();

            foreach (var item in q)
            {
                domainList.Add(new Domain.AddressBook()
                {
                    FirstName = item.FirstName,
                    SurName = item.SurName,
                    Address = item.Address
                });
            }

            return domainList;
        }
    }
}