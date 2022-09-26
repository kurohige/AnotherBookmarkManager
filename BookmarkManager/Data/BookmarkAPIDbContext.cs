using BookmarkManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BookmarkManager.Data
{
    public class BookmarkAPIDbContext : DbContext
    {
        public BookmarkAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Folder> Folders { get; set; }

    }
}
