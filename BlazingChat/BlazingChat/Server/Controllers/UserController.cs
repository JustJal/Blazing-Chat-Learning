using BlazingChat.Server.Models;
using BlazingChat.Client.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingChat.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly BlazingChatContext _context;

        public UserController(ILogger<UserController> logger, BlazingChatContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("getallcontacts")]
        public List<User> GetContacts()
        {
            return _context.Users.ToList();
        }

        [HttpPut("updateprofile/{userid}")]
        public async Task<User> UpdateProfile(int userid, [FromBody] User _user)
        {
            User UserToUpdate = await _context.Users.Where(User => User.UserId == userid)
                                                    .FirstOrDefaultAsync();

            UserToUpdate.FirstName = _user.FirstName;
            UserToUpdate.LastName = _user.LastName;
            UserToUpdate.EmailAddress = _user.EmailAddress;
            UserToUpdate.AboutMe = _user.AboutMe;

            await _context.SaveChangesAsync();

            return await Task.FromResult(UserToUpdate);
        }

        [HttpGet("getprofile/{userid}")]
        public async Task<User> GetProfile(int userid) => await _context.Users.Where(user => user.UserId == userid)
                                                                              .FirstOrDefaultAsync();

        [HttpGet("updatetheme")]
        public async Task<User> UpdateTheme(string userId, string value)
        {
            User user = _context.Users.Where(u => u.UserId == Convert.ToInt32(userId)).FirstOrDefault();
            user.DarkTheme = value == "True" ? 1 : 0;

            await _context.SaveChangesAsync();

            return await Task.FromResult(user);
        }

        [HttpGet("updatenotifications")]
        public async Task<User> UpdateNotifications(string userId, string value)
        {
            User user = _context.Users.Where(u => u.UserId == Convert.ToInt32(userId)).FirstOrDefault();
            user.Notifications = value == "True" ? 1 : 0;

            await _context.SaveChangesAsync();

            return await Task.FromResult(user);
        }
    }
}
