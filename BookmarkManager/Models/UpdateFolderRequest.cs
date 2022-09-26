namespace BookmarkManager.Models
{
    public class UpdateFolderRequest
    {
        public int FolderId { get; set; }
        public string FolderName { get; set; }
        public string Description { get; set; }
    }
}
