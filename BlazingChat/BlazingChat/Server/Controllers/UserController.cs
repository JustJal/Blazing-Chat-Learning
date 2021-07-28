using BlazingChat.Server.Models;
using BlazingChat.Client.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

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

        [HttpPost("loginuser")]
        public async Task<ActionResult<User>> LoginUser(User user)
        {
            User loggedInUser = await _context.Users.Where(u => u.EmailAddress.Equals(user.EmailAddress) && u.Password.Equals(user.Password)).FirstOrDefaultAsync();

            if(loggedInUser != null)
            {
                //create claim
                var claim = new Claim(ClaimTypes.Name, loggedInUser.EmailAddress);
                //create identity claim
                var claimsIdenity = new ClaimsIdentity(new[] { claim }, "ServerAuth");
                //create principal claim
                var claimsPrincipal = new ClaimsPrincipal(claimsIdenity);
                //Sign in
                await HttpContext.SignInAsync(claimsPrincipal);
            }

            return await Task.FromResult(loggedInUser);
        }

        [HttpGet("getcurrentuser")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            User currentUser = new();
            if (User.Identity.IsAuthenticated)
            {
                var emailAddress = User.FindFirstValue(ClaimTypes.Name);
                currentUser = await _context.Users.Where(u => u.EmailAddress == emailAddress).FirstOrDefaultAsync();
            }
            return await Task.FromResult(currentUser);
        }

        [HttpGet("logoutuser")]
        public async Task<ActionResult<string>> LogoutUser()
        {
            await HttpContext.SignOutAsync();
            return "Success";

        }

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
