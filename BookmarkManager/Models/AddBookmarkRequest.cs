namespace BookmarkManager.Models
{
    public class AddBookmarkRequest
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public int FolderID { get; set; }
        //public DateTime CreatedDate { get; set; }
    }
}
