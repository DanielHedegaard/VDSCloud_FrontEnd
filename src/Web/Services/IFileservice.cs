using Models;
using Web.Models;

namespace Web.Services
{
    public interface IFileservice
    {
        //file
        Task<bool> UploadFileAsync(Stream stream, VDSFile file, int userId);
        Task<bool> DeleteFileAsync(int fileId);
        Task<bool> UpdateFileAsync(VDSFile file);

        Task<List<FileSystemObject?>> GetFolderStructureAsync(int userId);

        //folder
        Task<bool> UploadFolderAsync(Folder folder);
        Task<bool> DeleteFolderAsync(int folderId);
        Task<bool> UpdateFolderAsync(Folder folder);
    }
}