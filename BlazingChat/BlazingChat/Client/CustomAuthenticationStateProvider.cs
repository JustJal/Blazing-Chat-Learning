using BlazingChat.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazingChat.Client
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;

        public CustomAuthenticationStateProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            User currentUser = await httpClient.GetFromJsonAsync<User>("user/getcurrentuser");

            if(currentUser != null && currentUser.EmailAddress != null)
            {
                //create claim
                var claimName = new Claim(ClaimTypes.Name, currentUser.EmailAddress);
                var claimIdentifier = new Claim(ClaimTypes.NameIdentifier, Convert.ToString(currentUser.UserId));
                //create identity claim
                var claimsIdentity = new ClaimsIdentity(new[] { claimName, claimIdentifier }, "ServerAuth");
                //create principal claim
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                return new AuthenticationState(claimsPrincipal);
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }
}
