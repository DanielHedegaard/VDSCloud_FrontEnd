using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using System.Security.Cryptography;
using Web.Components.Dialogs.File;
using Web.Components.Dialogs.Folder;
using Web.Models;

namespace Web.Components
{
    public partial class HomeComponent
    {
        [Inject]
        public Session Session { get; set; }

        public HashSet<FileSystemObject> UserFolderFiles { get; set; }

        private int windowHeight, windowWidth;

        private IJSObjectReference jsModule;

        protected async override Task OnInitializedAsync()
        {
            jsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/getwindowsize.js");

            UserFolderFiles = await Session.GetUserFolderFileStructure();
            Session.UpdateFileFolderStructureEvent += Handle_UpdateFileFolderStructure;

            var dimension = await jsModule.InvokeAsync<WindowDimensions>("getWindowSize");
            windowHeight = dimension.Height;
            windowWidth = dimension.Width;
        }

        private void OpenUploadFolderDialog(int parentId)
        {
            DialogOptions closeOnEscapeKey = new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseOnEscapeKey = true };

            var dialogParameters = new DialogParameters<UploadFolderDialog>();

            if (parentId != 0)
            {
                dialogParameters.Add(x => x.ParentId, parentId);
            }

            DialogService.Show<UploadFolderDialog>("Upload folder", dialogParameters, closeOnEscapeKey);
        }

        private void OpenDeleteFileFolderDialog(int id, FSType fsType)
        {
            DialogOptions dialogOptions = new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseOnEscapeKey = true };

            if (fsType == FSType.Folder)
            {
                var dialogParameters = new DialogParameters<DeleteFolderDialog>();
                dialogParameters.Add(x => x.Id, id);

                DialogService.Show<DeleteFolderDialog>("Delete folder", dialogParameters, dialogOptions);
            }
            else
            {
                var dialogParameters = new DialogParameters<DeleteFileDialog>();
                dialogParameters.Add(x => x.Id, id);

                DialogService.Show<DeleteFileDialog>("Delete file", dialogParameters, dialogOptions);
            }
        }

        private void OpenUpdateFileFolderDialog(int id, FSType fsType)
        {
            DialogOptions dialogOptions = new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseOnEscapeKey = true };

            if (fsType == FSType.Folder)
            {
                var dialogParameters = new DialogParameters<UpdateFolderDialog>();
                dialogParameters.Add(x => x.Id, id);

                DialogService.Show<UpdateFolderDialog>("Edit folder", dialogParameters, dialogOptions);
            }
            else
            {
                var dialogParameters = new DialogParameters<UpdateFileDialog>();
                dialogParameters.Add(x => x.Id, id);

                DialogService.Show<UpdateFileDialog>("Edit file", dialogParameters, dialogOptions);
            }
        }

        private void OpenUploadFileDialog(int id)
        {
            DialogOptions dialogOptions = new DialogOptions() { MaxWidth = MaxWidth.Small, CloseOnEscapeKey = true };
            
            var dialogParameters = new DialogParameters<UploadFileDialog>();
            dialogParameters.Add(x => x.ParentId, id);

            DialogService.Show<UploadFileDialog>("Upload file", dialogParameters, dialogOptions);
        }

        private async void Handle_UpdateFileFolderStructure()
        {
            UserFolderFiles = await Session.GetUserFolderFileStructure();
            StateHasChanged();
        }

        private bool IsFolderEmpty() => UserFolderFiles == null || UserFolderFiles.Count == 0 ? true : false;

        public class WindowDimensions
        {
            public int Width { get; set; }
            public int Height { get; set; }
        }
    }
}
