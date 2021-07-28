using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingChat.Client.ViewModels
{
    public interface IContactsViewModel
    {
        public List<Contact> Contacts { get; set; }
        public Task GetContacts();
    }
}
