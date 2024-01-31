

namespace Models
{
    public class VDSFile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FolderId { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string GUID { get; set; }
        public byte FileContent { get; set; }

    }
}
