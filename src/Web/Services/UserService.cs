using Blazored.LocalStorage;
using Microsoft.IdentityModel.Tokens;

namespace Web.Services
{
    public class UserService : IUserService
    {
        private const string LoginTokenKey = "vdslogintoken";
        private const string UserNameKey = "vdsusername";

        private readonly ISyncLocalStorageService _storageService;
        private readonly IApiService _apiService;

        public UserService(ISyncLocalStorageService storageService, IApiService apiService)
        {
            _storageService = storageService;
            _apiService = apiService;

        }

        //check for token, if token check for validity
        public async Task<bool> IsLoggedInAsync()
        {
            try
            {
                if (!_storageService.ContainKey(LoginTokenKey) || !_storageService.ContainKey(UserNameKey))
                {
                    return false;
                }

                var token = _storageService.GetItem<string>(LoginTokenKey);
                var userName = _storageService.GetItem<string>(UserNameKey);

                if (token is null || userName is null)
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

        public async Task<bool> LoginAsync(string username, string password)
        {
            if (username.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                return false;
            }

            var token = await _apiService.LogUserIn(username, password);

            if (string.IsNullOrEmpty(token))
            {
                return false; 
            }

            _storageService.SetItem<string>(LoginTokenKey, token);
            _storageService.SetItem<string>(UserNameKey, username);

            return true;
        }

        public bool Logout()
        {
            _storageService.RemoveItem(LoginTokenKey);
            _storageService.RemoveItem(UserNameKey);

            return true;
        }

        public async Task<bool> CreateUserAsync(string userName, string password)
        {
            if (userName.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                return false;
            }

            var result = await _apiService.CreateUserAsync(userName, password);

            if (result is null || result == false)
            {
                return false;
            }

            var token = await _apiService.LogUserIn(userName, password);

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            _storageService.SetItem<string>(LoginTokenKey, token);
            _storageService.SetItem<string>(UserNameKey, userName);

            return true;
        }
    }
}
