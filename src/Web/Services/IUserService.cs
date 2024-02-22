namespace Web.Services
{
    public interface IUserService
    {
        Task<bool> LoginAsync(string username, string password);
        Task<bool> LogoutAsync();
        Task<bool> IsLoggedIn();
    }
}
