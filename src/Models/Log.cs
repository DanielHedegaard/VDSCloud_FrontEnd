

namespace Models
{
    public class Log
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FolderId { get; set; }
        public int FileId { get; set; }
        public string UserAction { get; set; }
        public DateTime Date { get; set; }
       

    }
}
