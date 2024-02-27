namespace Web.Services
{
    public interface IUserService
    {
        Task<bool> LoginAsync(string username, string password);
        Task<bool> LogoutAsync();
        Task<bool> IsLoggedInAsync();
        Task<bool> CreateUserAsync(string userName, string password);
    }
}
