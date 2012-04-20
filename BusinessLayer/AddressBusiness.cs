using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PresentationLayer;

namespace BusinessLayer
{
    public class AddressBusiness
    {
        public static List<Domain.AddressBook> GetAllAddresses()
        {
            var addresses = Database.Get();

            return addresses;
        }

    }
}
