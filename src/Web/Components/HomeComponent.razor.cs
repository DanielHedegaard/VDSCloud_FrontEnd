using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using Web.Components.Dialogs;
using Web.Models;

namespace Web.Components
{
    public partial class HomeComponent
    {
        [Inject]
        public Session Session { get; set; }

        public HashSet<FileSystemObject>? UserFolderFiles { get; set; }

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

        private void OpenFileFolderDialog(int id, FSType fSType, DialogType dialogType)
        {
            DialogOptions standardDialogOption = new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseOnEscapeKey = true };
            DialogOptions uploadDialogOption = new DialogOptions() { MaxWidth = MaxWidth.Small, CloseOnEscapeKey = true };

            var dialogParameters = new DialogParameters<FileFolderDialog>();
            dialogParameters.Add(x => x.FSType, fSType);

            if (fSType == FSType.Folder)
            {
                dialogType = DialogTypeToFolder(dialogType);

                dialogParameters.Add(x => x.DialogType, dialogType);
            }
            else
            {
                dialogParameters.Add(x => x.DialogType, dialogType);
            }

            if (id != 0)
            {
                dialogParameters.Add(x => x.Id, id);
            }

            string dialogName = DialogTypeName(dialogType);

            var dialogOption = standardDialogOption;

            if (dialogType == DialogType.UploadFile){ dialogOption = uploadDialogOption; };
            
            DialogService.Show<FileFolderDialog>(dialogName, dialogParameters, dialogOption);
        }

        private DialogType DialogTypeToFolder(DialogType dialogType) => dialogType switch
        {
            DialogType.UploadFile => DialogType.UploadFolder,
            DialogType.UpdateFile => DialogType.UpdateFolder,
            DialogType.DeleteFile => DialogType.DeleteFolder,
            _ => DialogType.UploadFolder
        };

        private string DialogTypeName(DialogType dialogType) => dialogType switch
        {
            DialogType.UploadFile => "Upload file",
            DialogType.UpdateFile => "Update file",
            DialogType.DeleteFile => "Delete file",
            DialogType.UploadFolder => "Upload folder",
            DialogType.UpdateFolder => "Update folder",
            DialogType.DeleteFolder => "Delete folder",
            _ => ""
        };

        private async void Handle_UpdateFileFolderStructure()
        {
            UserFolderFiles = await Session.GetUserFolderFileStructure();
            StateHasChanged();
        }

        private bool IsFolderEmpty() => UserFolderFiles == null || UserFolderFiles.Count == 0 ? true : false;

        private class WindowDimensions
        {
            public int Width { get; set; }
            public int Height { get; set; }
        }
    }
}
