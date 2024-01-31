using Models;

namespace Web.Services
{
    public interface IApiService
    {
        //user
        Task<User> GetUser(int id);
        Task<bool> CreateUser(User user);


        //file
        Task<VDSFile> GetFile(int userId);
        Task<List<VDSFile>> GetFileList(int userId);
        Task<bool> CreateFile(VDSFile file);
        Task<bool> DeleteFile(int fileId);
        Task<bool> UpdateFile(VDSFile file);

        //folder
        Task<Folder> GetFolder(int userId); 
        Task<List<Folder>> GetFolderList(int userId);
        Task<bool> CreateFolder(Folder folder);
        Task<bool> DeleteFolder(int folderId);
        Task<bool> UpdateFolder(Folder folder);


        //log
        Task<Log> GetLog();
        Task<List<Log>> GetLogList();
        Task<bool> CreateLog(Log log);
        Task<bool> DeleteLog(int logId);


    }
}
