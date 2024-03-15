namespace Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string FolderName { get; set; }
        public int UserId { get; set; }
        public int? ParentFolderId { get; set; }
        public Folder? ParentFolder { get; set; }
        public List<VDSFile> VdsFiles { get; set; } = new();
        public List<Folder> SubFolders { get; set; } = new();
    }
}
