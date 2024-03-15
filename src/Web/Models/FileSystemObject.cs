using Models;
using MudBlazor;
using System.Drawing;

namespace Web.Models
{
    public enum FSType
    {
        File,
        Folder
    }


    public class FileSystemObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentFolderId { get; set; }
        public FSType Type  { get; set; }
        public HashSet<FileSystemObject> FileSOItem { get; set; } = new HashSet<FileSystemObject>();
        public string FSIconType => Type switch
        {
            FSType.File => Icons.Custom.FileFormats.FileDocument,
            FSType.Folder => Icons.Material.Filled.Folder,
            _ => string.Empty
        };
        public MudBlazor.Color Color => Type switch
        {
            FSType.File => MudBlazor.Color.Info,
            FSType.Folder => MudBlazor.Color.Warning,
            _ => MudBlazor.Color.Primary
        };
    }
}