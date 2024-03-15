using Microsoft.JSInterop;
using Models;
using MudBlazor.Interfaces;
using System.Reflection.Metadata.Ecma335;
using Web.Services;

namespace Web.Models
{
    public class Session
    {
        private readonly IUserService _userService;
        private readonly IFileservice _filesService;
        private readonly IJSRuntime _jsRuntime;

        public User User { get; set; } = new();

        public event Action UserLoggedInEvent;
        public event Action<string> ErrorEvent;
        public event Action<string> SuccessEvent;
        public event Action UserLoggedOutEvent;
        public event Action UpdateFileFolderStructureEvent;

        public Session(IUserService userService, IFileservice fileservice, IJSRuntime jSRuntime)
        {
            _userService = userService;
            _filesService = fileservice;
            _jsRuntime = jSRuntime;
        }

        //user
        public async Task LogUserInAsync()
        {
            if (string.IsNullOrEmpty(User.UserName) || string.IsNullOrEmpty(User.Password))
            {
                ErrorEvent?.Invoke("Error! Username and Password needs an input");
                return;
            }

            var loginResult = await _userService.LoginAsync(User.UserName, User.Password);

            if (loginResult.LoggedIn)
            {
                User.Id = loginResult.UserId;
                UserLoggedInEvent?.Invoke();
            }
            else
            {
                ErrorEvent?.Invoke("Error! error logging in");
            }
        }

        public void LogUserOut()
        {
            var logoutResult = _userService.Logout();

            UserLoggedOutEvent?.Invoke();
        }

        public async Task CreateUserAsync(string reEnterPassword)
        {
            if (string.IsNullOrEmpty(User.UserName) || string.IsNullOrEmpty(User.Password))
            {
                ErrorEvent?.Invoke("Error! Username and pasword needs an input");
                return;
            }

            if (User.Password != reEnterPassword)
            {
                ErrorEvent?.Invoke("Error! Passwords are not identical, please check again");
                return;
            }

            var createUserResult = await _userService.CreateUserAsync(User.UserName, User.Password);

            if (createUserResult)
            {
                UserLoggedInEvent?.Invoke();
            }
            else
            {
                ErrorEvent?.Invoke("Error! Error creating user");
            }
        }

        public async Task<bool> IsUserLoggedIn() => await _userService.IsLoggedInAsync();

        public async Task<HashSet<FileSystemObject>?> GetUserFolderFileStructure()
        {
            int id = User.Id;

            if (id == 0)
            {
                id = _userService.GetUserIdFromLocalStorage();
            }

            List<FileSystemObject> userFolderFiles = await _filesService.GetFolderStructureAsync(id);

            if (userFolderFiles == null)
            {
                return null;
            }

            return userFolderFiles.ToHashSet();
        }

        //folder
        public async Task UploadFolderAsync(Folder folder)
        {
            int id = User.Id;

            if (id == 0)
            {
                id = _userService.GetUserIdFromLocalStorage();
            }

            if (string.IsNullOrEmpty(folder.FolderName))
            {
                ErrorEvent?.Invoke("Error! Upload didnt work, name requires an input!");
                return;
            }

            folder.UserId = id;

            bool result = await _filesService.UploadFolderAsync(folder);

            if (result == false)
            {
                ErrorEvent?.Invoke("Error! Upload didnt work try again!");
                return;
            }

            SuccessEvent?.Invoke("The folder was successfully created");
        }

        public async Task DeleteFolderAsync(int id)
        {
            if (id == 0)
            {
                ErrorEvent?.Invoke("Error! The folder/file couldt be deleted please try again!");
                return;
            }

            var result = await _filesService.DeleteFolderAsync(id);

            if (result == false)
            {
                ErrorEvent?.Invoke("Error! The folder/file couldt be deleted please try again!");
                return;
            }

            SuccessEvent?.Invoke("The folder was successfully deleted!");
        }

        public async Task UpdateFolderAsync(Folder folder)
        {
            if (folder == null || folder.Id == 0)
            {
                ErrorEvent?.Invoke("Error! The folder was not updated, please try again!");
                return;
            }

            var result = await _filesService.UpdateFolderAsync(folder);

            if (result == false)
            {
                ErrorEvent?.Invoke("Error! The folder was not updated, please try again!");
                return;
            }

            SuccessEvent?.Invoke("The folder was successfully renamed!");
        }

        //file
        public async Task UploadFileAsync(Stream stream, VDSFile file)
        {
            if (!Path.HasExtension(file.FileName))
            {
                file.FileName += ".file";
            }

            int userId = User.Id;

            if (userId == 0)
            {
                userId = _userService.GetUserIdFromLocalStorage();
            }

            if (file == null)
            {
                ErrorEvent?.Invoke("Error! The file could not be upload, please try again");
                return;
            }

            var result = await _filesService.UploadFileAsync(stream, file, userId);

            if (result == false)
            {
                ErrorEvent?.Invoke("Error! The file could not be upload, please try again");
                return;
            }

            SuccessEvent?.Invoke("The file was successfully uploaded");
        }

        public async Task DeleteFileAsync(int id)
        {
            if (id == 0)
            {
                ErrorEvent?.Invoke("Error! The file could not be deleted");
                return;
            }

            var result = await _filesService.DeleteFileAsync(id);

            if (result == false)
            {
                ErrorEvent?.Invoke("Error! The file could not be deleted");
                return;
            }

            SuccessEvent?.Invoke("The file was successfully deleted!");
        }

        public async Task UpdateFileAsync(VDSFile file)
        {
            if (!Path.HasExtension(file.FileName))
            {
                file.FileName += ".file";
            }

            if (file == null || file.Id == 0 || string.IsNullOrEmpty(file.FileName))
            {
                ErrorEvent?.Invoke("Error! The file was not updated, please try again!");
                return;
            }

            var result = await _filesService.UpdateFileAsync(file);

            if (result == false)
            {
                ErrorEvent?.Invoke("Error! The file was not updated, please try again!");
                return;
            }

            SuccessEvent?.Invoke("The file was successfully renamed!");
        }

        public async Task DownloadFileAsync(int fileId)
        {
            var fileIdStr = fileId.ToString();
            await _jsRuntime.InvokeVoidAsync("downloadFileStream", fileIdStr);

            SuccessEvent?.Invoke("File download started...");
        }

        public void UpdateFileFolderStructure()
        {
            UpdateFileFolderStructureEvent?.Invoke();
        }
    }
}