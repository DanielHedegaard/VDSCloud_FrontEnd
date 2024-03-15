using Microsoft.IdentityModel.Abstractions;
using Models;
using System.Text;
using System.Xml.Linq;
using Web.Models;

namespace Web.Services
{
    public class Fileservice : IFileservice
    {
        private readonly IApiService _apiService;

        public Fileservice(IApiService apiService)
        {
            _apiService = apiService;
        }

        //file
        public async Task<bool> UploadFileAsync(Stream stream, VDSFile file, int userId)
        {
            if (file == null)
            {
                return false;
            }

            var result = await _apiService.CreateFileAsync(stream, file, userId);

            if (result == null || result == false)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteFileAsync(int fileId)
        {
            var result = await _apiService.DeleteFileAsync(fileId);

            if (result == null || result == false)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateFileAsync(VDSFile file)
        {
            if (file == null)
            {
                return false;
            }

            var result = await _apiService.UpdateFileAsync(file);

            if (result == null || result == false)
            {
                return false;
            }

            return true;
        }

        public async Task<List<FileSystemObject>?> GetFolderStructureAsync(int userId)
        {
            var folderList = await _apiService.GetFolderStructureAsync(userId);

            if (folderList == null)
            {
                return null;
            }

            var fsoObjects = RecursiveConversion(folderList).ToList();

            return fsoObjects;
        }

        //folder

        public async Task<bool> UploadFolderAsync(Folder folder)
        {
            if (folder == null)
            {
                return false;
            }

            if (folder.ParentFolderId == null)
            {
                folder.ParentFolderId = 0;
            }

            var result = await _apiService.CreateFolderAsync(folder);

            if (result == null || result == false)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteFolderAsync(int folderId)
        {
            var result = await _apiService.DeleteFolderAsync(folderId);

            if (result == null || result == false)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateFolderAsync(Folder folder)
        {
            if (folder == null)
            {
                return false;
            }

            var result = await _apiService.UpdateFolderAsync(folder);

            if (result == null || result == false)
            {
                return false;
            }

            return true;
        }

        private static IEnumerable<FileSystemObject> RecursiveConversion(List<Folder> folders)
        {
            var fsoObjects = new List<FileSystemObject>();

            foreach (var folder in folders)
            {
                var fsoObject = new FileSystemObject
                {
                    Id = folder.Id,
                    Name = folder.FolderName,
                    ParentFolderId = folder.Id,
                    Type = FSType.Folder,
                    FileSOItem = RecursiveConversion(folder.SubFolders).ToHashSet()
                };

                if (folder.VdsFiles.Count != 0)
                {
                    foreach (var file in folder.VdsFiles)
                    {
                        fsoObject.FileSOItem.Add(new()
                        {
                            Id = file.Id,
                            Name = file.FileName,
                            ParentFolderId = folder.Id,
                            Type = FSType.File
                        });
                    }
                }

                fsoObjects.Add(fsoObject);
            }

            return fsoObjects;
        }
    }
}
