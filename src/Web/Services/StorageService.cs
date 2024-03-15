using Blazored.LocalStorage;

namespace Web.Services
{
    public class StorageService : IStorageService
    {
        private const string LoginTokenKey = "vdslogintoken";
        private const string UserNameKey = "vdsusername";
        private const string UserIdKey = "vdsuserid";

        private readonly ISyncLocalStorageService _storageService;

        public StorageService(ISyncLocalStorageService storageService)
        {
            _storageService = storageService;
        }

        public string GetToken()
        {
            if (!_storageService.ContainKey(LoginTokenKey))
            {
                return "";
            }

            var token = _storageService.GetItem<string>(LoginTokenKey);

            if (string.IsNullOrEmpty(token))
            {
                return "";
            }

            return token;
        }

        public int GetUserId()
        {
            if (!_storageService.ContainKey(UserIdKey))
            {
                return 0;
            }

            return _storageService.GetItem<int>(UserIdKey);
        }

        public string GetUsername()
        {
            if (!_storageService.ContainKey(UserNameKey))
            {
                return "";
            }

            var username = _storageService.GetItem<string>(UserNameKey);

            if (string.IsNullOrEmpty(username))
            {
                return "";
            }

            return username;
        }

        public void SetToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException(nameof(token));
            }

            _storageService.SetItem(LoginTokenKey, token);
        }

        public void SetUserId(int userId)
        {
            if (userId == 0)
            {
                throw new ArgumentException(nameof(userId));
            }

            _storageService.SetItem(UserIdKey, userId);
        }

        public void SetUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException(nameof(username));
            }

            _storageService.SetItem(UserNameKey, username);
        }

        public void Clear() => _storageService.Clear();
    }
}