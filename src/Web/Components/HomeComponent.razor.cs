using Microsoft.JSInterop;
using Models;
using MudBlazor;
using Web.Models;

namespace Web.Components
{
    public partial class HomeComponent
    {
        private IJSObjectReference jsModule;

        private int windowHeight, windowWidth;

        public List<FileSystemObject> Files{ get; set; } = new List<FileSystemObject>();

        private HashSet<TreeItemData> TreeItems { get; set; } = new HashSet<TreeItemData>();

        protected async override Task OnInitializedAsync()
        {
            jsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/getwindowsize.js");
            SeedData();
            var dimension = await jsModule.InvokeAsync<WindowDimensions>("getWindowSize");
            windowHeight = dimension.Height;
            windowWidth = dimension.Width;
        }   

        private void SeedData()
        {
            TreeItems.Add(new TreeItemData("src", Icons.Material.Filled.Folder)
            {
                TreeItems = new HashSet<TreeItemData>()
            {
                new TreeItemData("MudBlazor", Icons.Custom.FileFormats.FileDocument),
                new TreeItemData("MudBlazor.Docs", Icons.Material.Filled.Folder)
                {
                    TreeItems = new HashSet<TreeItemData>()
                    {
                        new TreeItemData("_Imports.razor", Icons.Custom.FileFormats.FileDocument),
                        new TreeItemData( "compilerconfig.json", Icons.Custom.FileFormats.FileDocument),
                        new TreeItemData( "MudBlazor.Docs.csproj", Icons.Custom.FileFormats.FileDocument),
                        new TreeItemData("NewFilesToBuild.txt" , Icons.Custom.FileFormats.FileDocument),
                    }
                },
            }
            });
        }

        public class WindowDimensions
        {
            public int Width { get; set; }
            public int Height { get; set; }
        }

        public class TreeItemData
        {
            public string Text { get; set; }

            public string Icon { get; set; }

            public HashSet<TreeItemData> TreeItems { get; set; } = new HashSet<TreeItemData>();

            public TreeItemData(string text, string icon)
            {
                Text = text;
                Icon = icon;
            }
            public TreeItemData(string icon)
            {
                Icon = icon;
            }
        }
    }
}
