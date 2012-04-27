using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PresentationLayer;
using Domain;

namespace BusinessLayer
{
    public class AddressBusiness
    {
        public static void GetAllAddresses()
        {
            using (var db = new MyDbContext())
            {
                db.Test.Add(new AddressBook { SurName = "sd", Address = "asd", FirstName = "asdgk" });
                db.SaveChanges(); 
            }

        }

    }
}
