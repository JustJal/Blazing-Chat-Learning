using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingChat.Client.ViewModels
{
    public interface IProfileViewModel
    {
        int UserId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string AboutMe { get; set; }
        string message { get; set; }
        string ProfilePictureUrl { get; set; }

        Task UpdateProfile();
        Task GetProfile();
    }
}
