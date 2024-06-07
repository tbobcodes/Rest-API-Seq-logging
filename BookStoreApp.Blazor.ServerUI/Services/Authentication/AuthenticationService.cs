using Blazored.LocalStorage;
using BookStoreApp.Blazor.ServerUI.Providers;
using BookStoreApp.Blazor.ServerUI.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreApp.Blazor.ServerUI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<bool> AuthenticateAsync(LoginUserDto loginModel)
        {
            var response = await httpClient.LoginAsync(loginModel);

            //Store Token
            await localStorage.SetItemAsync("accessToken", response.Token);
            //Change auth State of app
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();


            return true;



            
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
        }
    }
}
