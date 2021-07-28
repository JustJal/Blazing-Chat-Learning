using BlazingChat.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingChat.Client.ViewModels
{
    public class ContactsViewModel : IContactsViewModel
    {
        //properties
        public List<Contact> Contacts { get; set; }
        private HttpClient _httpClient;

        //methods
        public ContactsViewModel()
        {
        }
        public ContactsViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task GetContacts()
        {
            List<User> users = await _httpClient.GetFromJsonAsync<List<User>>("user/getallcontacts");
            LoadCurrentObject(users);
        }

        private void LoadCurrentObject(List<User> users)
        {
            this.Contacts = new List<Contact>();
            foreach (User user in users)
            {
                this.Contacts.Add(user);
            }
        }
    }
}
