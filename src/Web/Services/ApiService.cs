using Models;
using System.Net.Http.Json;

namespace Web.Services
{
    public class ApiService : IApiService
    {
        private readonly string _apiRoot;

        private readonly HttpClient _httpClient;

        public ApiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiRoot = "localhost:3001/api";
        }

        //user
        public async Task<User?> GetUserAsync(int id)
        {
            var uri = $"{_apiRoot}/user?id={id}";

            return await _httpClient.GetFromJsonAsync<User>(uri);
        }

        public async Task<bool?> CreateUserAsync(User user)
        {
            var uri = $"{_apiRoot}/user";

            var result = await _httpClient.PostAsJsonAsync<User>(uri, user);

            return result.IsSuccessStatusCode;
        }
        
        public async Task<bool?> UserLoginAsync(User user)
        {
            var uri = $"{_apiRoot}/user/authenticate";

            var result = await _httpClient.PostAsJsonAsync(uri, user);

            return result.IsSuccessStatusCode;
        }


        //file
        public async Task<VDSFile?> GetFileAsync(int fileId)
        {
            var uri = $"{_httpClient}/file?id={fileId}";

            return await _httpClient.GetFromJsonAsync<VDSFile>(uri);
        }

        public async Task<List<VDSFile>?> GetFileListAsync()
        {
            var uri = $"{_httpClient}/file";

            return await _httpClient.GetFromJsonAsync<List<VDSFile>>(uri);
        }

        public async Task<bool?> CreateFileAsync(VDSFile file)
        {
            var uri = $"{_apiRoot}/file";

            var result = await _httpClient.PostAsJsonAsync(uri, file);
            
            return result.IsSuccessStatusCode;
        }

        public async Task<bool?> UpdateFileAsync(VDSFile file)
        {
            var uri = $"{_apiRoot}/file";

            var result = await _httpClient.PutAsJsonAsync(uri, file);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool?> DeleteFileAsync(int fileId)
        {
            var uri = $"/{_apiRoot}/file?id={fileId}";

            var result = await _httpClient.DeleteAsync(uri);

            return result.IsSuccessStatusCode;
        }


        //folder
        public async Task<Folder?> GetFolderAsync(int folderId)
        {
            var uri = $"{_apiRoot}/folder?id={folderId}";

            return await _httpClient.GetFromJsonAsync<Folder>(uri);
        }

        public async Task<List<Folder>?> GetFolderListAsync()
        {
            var uri = $"{_apiRoot}/folder";

            return await _httpClient.GetFromJsonAsync<List<Folder>>(uri);
        }

        public async Task<bool?> CreateFolderAsync(Folder folder)
        {
            var uri = $"{_apiRoot}/folder";

            var result = await _httpClient.PostAsJsonAsync<Folder>(uri, folder);

            return result?.IsSuccessStatusCode;
        }

        public async Task<bool?> UpdateFolderAsync(Folder folder)
        {
            var uri = $"{_apiRoot}/folder";

            var result = await _httpClient.PutAsJsonAsync<Folder>(uri, folder);

            return result?.IsSuccessStatusCode;
        }

        public async Task<bool?> DeleteFolderAsync(int folderId)
        {
            var uri = $"{_apiRoot}/folder?id={folderId}";

            var result = await _httpClient.DeleteAsync(uri);

            return result?.IsSuccessStatusCode;
        }


        //log
        public async Task<Log?> GetLogAsync(int logId)
        {
            var uri = $"{_apiRoot}/log?id={logId}";

            return await _httpClient.GetFromJsonAsync<Log>(uri);
        }

        public async Task<List<Log>?> GetLogListAsync()
        {
            var uri = $"{_apiRoot}/log";

            return await _httpClient.GetFromJsonAsync<List<Log>>(uri);
        }
    }
}