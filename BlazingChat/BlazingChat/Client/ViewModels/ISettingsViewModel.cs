using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazingChat.Shared.Models;

namespace BlazingChat.Client.ViewModels
{
    public interface ISettingsViewModel
    {
        bool Notifications { get; set; }
        bool DarkTheme { get; set; }

        Task Save();
        Task GetProfile();
    }
}
