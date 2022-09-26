namespace BookmarkManager.Models
{
    public class Bookmark
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int FolderID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
