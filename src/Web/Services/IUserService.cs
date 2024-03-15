using Models;
using Web.Models;

namespace Web.Services
{
    public interface IUserService
    {
        Task<UserLogin> LoginAsync(string username, string password);
        bool Logout();
        Task<bool> IsLoggedInAsync();
        Task<bool> CreateUserAsync(string userName, string password);
        int GetUserIdFromLocalStorage();
    }
}
