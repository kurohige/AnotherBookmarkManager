namespace BookmarkManager.Models
{
    public class UpdateBookmarkRequest
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public int FolderID { get; set; }
        //public DateTime UpdatedDate { get; set; }
    }
}
