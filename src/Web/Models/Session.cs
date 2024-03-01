using Models;
using MudBlazor.Interfaces;
using Web.Services;

namespace Web.Models
{
    public class Session
    {
        private readonly IUserService _userService;

        public User User { get; set; } = new();

        public event Action UserLoggedInEvent;
        public event Action<string> LoginOrCreateUserErrorEvent;
        public event Action UserLoggedOutEvent;

        public Session(IUserService userService)
        {
            _userService = userService;
        }

        public async Task LogUserInAsync()
        {
            if (string.IsNullOrEmpty(User.UserName) || string.IsNullOrEmpty(User.Password))
            {
                LoginOrCreateUserErrorEvent?.Invoke("Error! Username and Password needs an input");
                return;
            }

            var loginResult = await _userService.LoginAsync(User.UserName, User.Password);

            if (loginResult)
            {
                UserLoggedInEvent?.Invoke();
            }
            else
            {
                LoginOrCreateUserErrorEvent?.Invoke("Error! Username or Password was incorrect");
            }
        }

        public void LogUserOut()
        {
            var logoutResult = _userService.Logout();

            UserLoggedOutEvent?.Invoke();
        }

        public async Task CreateUserAsync(string reEnterPassword)
        {
            if (string.IsNullOrEmpty(User.UserName) || string.IsNullOrEmpty(User.Password))
            {
                LoginOrCreateUserErrorEvent?.Invoke("Error! Username and pasword needs an input");
                return;
            }

            if (User.Password != reEnterPassword)
            {
                LoginOrCreateUserErrorEvent?.Invoke("Error! Passwords are not identical, please check again");
                return;
            }

            var createUserResult = await _userService.CreateUserAsync(User.UserName, User.Password);

            if (createUserResult)
            {
                UserLoggedInEvent?.Invoke();
            }
            else
            {
                LoginOrCreateUserErrorEvent?.Invoke("Error! Username is occupied");
            }
        }

        public async Task<bool> IsUserLoggedIn() => await _userService.IsLoggedInAsync();
    }
}