namespace Models
{
    public class VDSFile
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public int FolderId { get; set; }
        public Folder Folder { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
