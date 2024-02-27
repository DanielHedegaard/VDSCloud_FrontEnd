using Microsoft.AspNetCore.Components;
using Web.Models;
using Web.Services;

namespace Web.Pages
{
    public partial class Index
    {
        [Inject]
        public IUserService UserService { get; set; }

        public bool CreateUserbool { get; set; } = false;
        public UserSession UserSession { get; set; }

        protected override void OnInitialized()
        {
            UserSession = new(UserService);
        }

        public async Task HandleLogout()
        {
            await UserService.LogoutAsync();

            StateHasChanged();
        }

        public async Task HandleLogin()
        {
            await UserService.LoginAsync(UserSession.User.UserName, UserSession.User.Password);
            StateHasChanged();
        }

        public async Task HandleCreateUser()
        {
            await UserService.CreateUserAsync(UserSession.User.UserName, UserSession.User.Password);

            CreateUserbool = false;

            StateHasChanged();
        }

        public void InvertCreateUserState() => CreateUserbool = !CreateUserbool;
    }
}
