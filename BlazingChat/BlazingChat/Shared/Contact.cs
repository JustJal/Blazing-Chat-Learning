using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingChat.Shared
{
    public class Contact
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Contact()
        {

        }
        public Contact(int id, string firstName, string lastName)
        {
            (ID, FirstName, LastName) = (id, firstName, lastName);

        }
    }
}
