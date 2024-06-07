using BookStoreApp.Blazor.ServerUI.Services.Base;

namespace BookStoreApp.Blazor.ServerUI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LoginUserDto loginModel);

        public Task Logout();
    }
}
