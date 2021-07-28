using BlazingChat.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingChat.Client.ViewModels
{
    public class ProfileViewModel : IProfileViewModel
    {
        private readonly HttpClient httpClient;
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string message { get; set; }
        public string AboutMe { get; set; }

        public static implicit operator ProfileViewModel(User user) 
            => new ProfileViewModel 
            { 
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.EmailAddress,
                UserId = user.UserId,
                AboutMe = user.AboutMe
            };
        public static implicit operator User(ProfileViewModel profileViewModel)
            => new User
            {
                FirstName = profileViewModel.FirstName,
                LastName = profileViewModel.LastName,
                EmailAddress = profileViewModel.Email,
                UserId = profileViewModel.UserId,
                AboutMe = profileViewModel.AboutMe
            };
        public ProfileViewModel() { }
        public ProfileViewModel(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task UpdateProfile()
        {
            User user = this;
            await httpClient.PutAsJsonAsync("user/updateprofile/" + this.UserId, user);
            this.message = "Profile updated succesfully.";
        }
        public async Task GetProfile()
        {
            User user = await httpClient.GetFromJsonAsync<User>("user/getprofile/" + this.UserId);
            LoadCurrentObject(user);
            this.message = "Profile loaded succesfully.";
        }

        private void LoadCurrentObject(ProfileViewModel profileViewModel)
        {
            this.FirstName = profileViewModel.FirstName;
            this.LastName = profileViewModel.LastName;
            this.Email = profileViewModel.Email;
            this.AboutMe = profileViewModel.AboutMe;
        }
    }
}
