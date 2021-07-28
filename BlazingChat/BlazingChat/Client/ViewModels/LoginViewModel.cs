using BlazingChat.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingChat.Client.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        private readonly HttpClient httpClient;

        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public LoginViewModel() { }
        public LoginViewModel(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task LoginUser()
        {
            await httpClient.PostAsJsonAsync<User>("user/loginuser", this);
        }

        public static implicit operator LoginViewModel(User user) => 
            new() 
            { 
                EmailAddress = user.EmailAddress, 
                Password = user.Password
            };
        public static implicit operator User(LoginViewModel loginViewModel) =>
            new()
            {
                EmailAddress = loginViewModel.EmailAddress,
                Password = loginViewModel.Password
            };
    }
}
