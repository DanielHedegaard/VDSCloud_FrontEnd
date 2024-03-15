using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using Models;
using Web.Models;

namespace Web.Services
{
    public class UserService : IUserService
    {
        private readonly IStorageService _storageService;
        private readonly IApiService _apiService;

        public UserService(IStorageService storageService, IApiService apiService)
        {
            _storageService = storageService;
            _apiService = apiService;

        }

        //check for token, if token check for validity
        public async Task<bool> IsLoggedInAsync()
        {
            try
            {
                var token = _storageService.GetToken();
                var userName = _storageService.GetUsername();

                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userName))
                {
                    return false;
                }

                var tokenValid = await _apiService.IsTokenValid(userName, token);

                if (tokenValid is not null)
                {
                    return (bool)tokenValid;
                }
            }
            catch
            {
                // Suppress
            }

            return false;
        }

        public async Task<UserLogin> LoginAsync(string username, string password)
        {
            UserLogin login = new UserLogin();

            if (username.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                login.LoggedIn = false;
                return login;
            }

            var token = await _apiService.LogUserIn(username, password);


            if (token == null || string.IsNullOrEmpty(token.Token))
            {
                login.LoggedIn = false;
                return login;
            }

            _storageService.SetToken(token.Token);
            _storageService.SetUsername(username);
            _storageService.SetUserId(token.UserId);

            login.LoggedIn = true;
            login.UserId = token.UserId;

            return login;
        }

        public bool Logout()
        {
            _storageService.Clear();

            return true;
        }

        public async Task<bool> CreateUserAsync(string username, string password)
        {
            if (username.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                return false;
            }

            var result = await _apiService.CreateUserAsync(username, password);

            if (result is null || result == false)
            {
                return false;
            }

            var token = await _apiService.LogUserIn(username, password);

            if (token == null || string.IsNullOrEmpty(token.Token))
            {
                return false;
            }

            _storageService.SetToken(token.Token);
            _storageService.SetUsername(username);
            _storageService.SetUserId(token.UserId);

            return true;
        }
        
        public int GetUserIdFromLocalStorage() => _storageService.GetUserId();
    }
}
