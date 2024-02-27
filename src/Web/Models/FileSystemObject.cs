using Models;
using MudBlazor;

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
        public FSType Type  { get; set; } 
        public string FSIconType => Type switch
        {
            FSType.File => Icons.Material.Outlined.InsertDriveFile,
            FSType.Folder => Icons.Material.Filled.Folder,
            _ => string.Empty
        };
    }
}