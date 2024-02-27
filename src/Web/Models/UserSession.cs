using Models;
using MudBlazor;
using Web.Services;

namespace Web.Models
{
    public class UserSession
    {
        private readonly IUserService _userService;

        public UserSession(IUserService userService)
        {
            _userService = userService;
            User = new();
        }

        public User User { get; set; } 

        public bool LoggedIn() => _userService.IsLoggedInAsync().GetAwaiter().GetResult();
    }
}