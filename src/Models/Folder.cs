

namespace Models
{
    public class Folder
    {
        public int Id { get; set; }
        public int FolderId{ get; set; }
        public int UserId { get; set; }
        public string FolderName { get; set; }
        public List<VDSFile> Files { get; set; }
        public List<Folder> SubFolders{ get; set; }

    }
}
