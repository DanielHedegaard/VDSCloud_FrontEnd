using Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Web.Services
{
    public class ApiService : IApiService
    {
        private readonly string _apiRoot;

        private readonly HttpClient _httpClient;

        public ApiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiRoot = "https://localhost:7193/api";
        }

        //IsAlive
        private async Task<bool> IsApiAliveAsync()
        {
            var uri = $"{_apiRoot}/isAlive";

            try
            {
                var result = await _httpClient.GetAsync(uri);

                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        //user
        public async Task<bool?> CreateUserAsync(string userName, string password)
        {
            var IsApiAlive = await IsApiAliveAsync();

            if (!IsApiAlive)
            {
                return null;
            }

            var uri = $"{_apiRoot}/user";

            var newUser = new User() { UserName = userName, Password = password };

            try
            {
                var result = await _httpClient.PostAsJsonAsync(uri, newUser);

                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        //login
        public async Task<bool?> IsTokenValid(string userName, string token)
        {
            var IsApiAlive = await IsApiAliveAsync();

            if (!IsApiAlive)
            {
                return null;
            }

            var uri = $"{_apiRoot}/user/validatetoken";

            var validateToken = new ValidateToken() { Token = token, UserName = userName };

            try
            {
                var validatedTokenValue = await _httpClient.PostAsJsonAsync(uri, validateToken);

                return validatedTokenValue.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ValidateToken?> LogUserIn(string username, string password)
        {
            var IsApiAlive = await IsApiAliveAsync();

            if (!IsApiAlive)
            {
                return null;
            }

            var uri = $"{_apiRoot}/user/login";

            var newUser = new User
            {
                UserName = username,
                Password = password
            };

            try
            {
                var result = await _httpClient.PostAsJsonAsync(uri, newUser);

                if (!result.IsSuccessStatusCode)
                {
                    return null;
                }   

                var jsonResult = JsonDocument.Parse(await result.Content.ReadAsStringAsync());

                var tokenResult = jsonResult.RootElement.GetProperty("token").ToString();

                var id = jsonResult.RootElement.GetProperty("userId").GetInt32();
               
                ValidateToken token = new ValidateToken() { UserId = id, Token = tokenResult};

                return token;
            }
            catch
            {
                return null;
            }
        }

        //file
        public async Task<bool?> CreateFileAsync(Stream stream, VDSFile file, int userId)
        {
            var IsApiAlive = await IsApiAliveAsync();

            if (!IsApiAlive)
            {
                return null;
            }

            var uri = $"{_apiRoot}/file?folderId={file.FolderId}&userId={userId}&fileName={file.FileName}";
            
            var streamContent = new StreamContent(stream);

            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            try
            {
                var result = await _httpClient.PostAsync(uri, streamContent);
             
                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool?> UpdateFileAsync(VDSFile file)
        {
            var IsApiAlive = await IsApiAliveAsync();

            if (!IsApiAlive)
            {
                return null;
            }

            var uri = $"{_apiRoot}/file?id={file.Id}&newFileName={file.FileName}";

            var fileContent = new StringContent(file?.ToString() ?? "");

            try
            {
                var result = await _httpClient.PutAsync(uri, fileContent);
             
                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool?> DeleteFileAsync(int fileId)
        {
            var IsApiAlive = await IsApiAliveAsync();

            if (!IsApiAlive)
            {
                return null;
            }

            var uri = $"{_apiRoot}/file?id={fileId}";

            try
            {
                var result = await _httpClient.DeleteAsync(uri);

                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }


        //folder
        public async Task<List<Folder>?> GetFolderStructureAsync(int userId)
        {
            var IsApiAlive = await IsApiAliveAsync();

            if (!IsApiAlive)
            {
                return null;
            }

            var uri = $"{_apiRoot}/folder?userId={userId}";

            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<Folder>>(uri);

                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool?> CreateFolderAsync(Folder folder)
        {
            var IsApiAlive = await IsApiAliveAsync();

            if (!IsApiAlive)
            {
                return null;
            }

            var uri = $"{_apiRoot}/folder";

            try
            {
                var result = await _httpClient.PostAsJsonAsync<Folder>(uri, folder);

                return result?.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool?> UpdateFolderAsync(Folder folder)
        {
            var IsApiAlive = await IsApiAliveAsync();

            if (!IsApiAlive)
            {
                return null;
            }

            var uri = $"{_apiRoot}/folder?folderId={folder.Id}&folderName={folder.FolderName}";

            var folderContent = new StringContent(folder?.ToString() ?? "");

            try
            {
                var result = await _httpClient.PutAsync(uri, folderContent);

                return result?.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool?> DeleteFolderAsync(int folderId)
        {
            var IsApiAlive = await IsApiAliveAsync();

            if (!IsApiAlive)
            {
                return null;
            }

            var uri = $"{_apiRoot}/folder?folderId={folderId}";

            try
            {
                var result = await _httpClient.DeleteAsync(uri);

                return result?.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}