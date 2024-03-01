using Models;

namespace Web.Services
{
    public interface IApiService
    {
        //user
        Task<User?> GetUserAsync(int id);
        Task<bool?> CreateUserAsync(string userName, string password);
        Task<bool?> IsTokenValid(string username, string token);
        Task<string?> LogUserIn(string username, string password); // return token

        //file
        Task<VDSFile?> GetFileAsync(int fileId);
        Task<List<VDSFile>?> GetFileListAsync();
        Task<bool?> CreateFileAsync(VDSFile file);
        Task<bool?> DeleteFileAsync(int fileId);
        Task<bool?> UpdateFileAsync(VDSFile file);

        //folder
        Task<Folder?> GetFolderAsync(int folderId); 
        Task<List<Folder>?> GetFolderListAsync();
        Task<bool?> CreateFolderAsync(Folder folder);
        Task<bool?> DeleteFolderAsync(int folderId);
        Task<bool?> UpdateFolderAsync(Folder folder);

        //log
        Task<Log?> GetLogAsync(int logId);
        Task<List<Log>?> GetLogListAsync();
    }
}
