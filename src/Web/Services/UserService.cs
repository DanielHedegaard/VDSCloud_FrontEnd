using Blazored.LocalStorage;
using Microsoft.IdentityModel.Tokens;

namespace Web.Services
{
    public class UserService : IUserService
    {
        private const string LoginTokenKey = "vdslogintoken";

        private readonly ISyncLocalStorageService _storageService;
        private readonly IApiService _apiService;

        public UserService(ISyncLocalStorageService storageService, IApiService apiService)
        {
            _storageService = storageService;
            _apiService = apiService;

        }

        //check for token, if token check for validity
        public async Task<bool> IsLoggedIn()
        {
            try
            {
                if (!_storageService.ContainKey(LoginTokenKey))
                {
                    return false;
                }

                var token = _storageService.GetItem<string>(LoginTokenKey);

                if (token is null)
                {
                    return false;
                }

                var tokenValid = await _apiService.IsTokenValid(token);

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

        //send to simone to validate, get token safe in localstorage
        public async Task<bool> LoginAsync(string username, string password)
        {
            if (username.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                return false;
            }

            var token = await _apiService.LogUserIn(username, password);

            if (token is null)
            {
                return false; 
            }

            _storageService.SetItem<string>(LoginTokenKey, token);

            return true;
        }

        public async Task<bool> LogoutAsync()
        {
            bool? isLoggedOut = await _apiService.LogUserOut();

            if (isLoggedOut == false)
            {
                return false;
            }

            _storageService.RemoveItem(LoginTokenKey);

            return true;
        }
    }
}
