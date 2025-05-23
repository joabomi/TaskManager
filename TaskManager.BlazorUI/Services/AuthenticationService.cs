﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Providers;
using TaskManager.BlazorUI.Services.Base;

namespace TaskManager.BlazorUI.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IClient client, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider): base(client, localStorage)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            try
            {
                AuthRequest authenticationRequest = new AuthRequest() { Email = email, Password = password };
                var authenticationResponse = await _client.LoginAsync(authenticationRequest);
                if (authenticationResponse.Token != string.Empty)
                {
                    await _localStorage.SetItemAsync("token", authenticationResponse.Token);
                    await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task Logout()
        {
            //remove claims in Blazor and invalidate login state
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
        }

        public async Task<bool> RegisterAsync(string firstName, string lastName, string userName, string email, string password)
        {
            RegistrationRequest registrationRequest = new RegistrationRequest()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = userName,
                Password = password
            };
            var registrationResponse = await _client.RegisterAsync(registrationRequest);

            if (!string.IsNullOrEmpty(registrationResponse.UserId))
            {
                return true;
            }
            return false;
        }
    }
}
