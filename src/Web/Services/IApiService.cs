using Models;

namespace Web.Services
{
    public interface IApiService
    {
        //user
        Task<bool?> CreateUserAsync(string userName, string password);
        Task<bool?> IsTokenValid(string username, string token);
        Task<ValidateToken?> LogUserIn(string username, string password); // return token

        //file
        Task<bool?> CreateFileAsync(Stream stream, VDSFile file, int userId);
        Task<bool?> DeleteFileAsync(int fileId);
        Task<bool?> UpdateFileAsync(VDSFile file);

        //folder
        Task<List<Folder>?> GetFolderStructureAsync(int userId); 
        Task<bool?> CreateFolderAsync(Folder folder);
        Task<bool?> DeleteFolderAsync(int folderId);
        Task<bool?> UpdateFolderAsync(Folder folder);
    }
}
