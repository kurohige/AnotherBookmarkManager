using BookmarkManager.Data;
using BookmarkManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.PostgresTypes;
using Npgsql.TypeMapping;

namespace BookmarkManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookmarkController : Controller
    {
        private readonly BookmarkAPIDbContext dbContext;
        //private readonly BookmarkAPIDbContext dbFolderContext;
        public BookmarkController(BookmarkAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookmarks()
        {
            return Ok( await dbContext.Bookmarks.ToListAsync());
        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetBookmark([FromRoute] Guid id)
        {
            var bookmark = await dbContext.Bookmarks.FindAsync(id);
            if (bookmark == null)
            {
                return NotFound();
            }
            return Ok(bookmark);
        }

        [HttpPost]
        public async Task<IActionResult> AddBookmarks(AddBookmarkRequest addBookmarkRequest)
        {
            var bookmark = new Bookmark()
            {
                Id = Guid.NewGuid(),
                Name = addBookmarkRequest.Name,
                URL = addBookmarkRequest.URL,
                FolderID = addBookmarkRequest.FolderID,
                CreatedDate = DateTime.UtcNow //new DateTime(1977, 12, 28, 6, 0, 0, DateTimeKind.Utc)
            };
            await dbContext.Bookmarks.AddAsync(bookmark);
            await dbContext.SaveChangesAsync();

            return Ok(bookmark);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateBookmark([FromRoute] Guid id,  UpdateBookmarkRequest updateBookmarkRequest)
        {
            var bookmark = await dbContext.Bookmarks.FindAsync(id);
            if (bookmark != null)
            {
                bookmark.Name = updateBookmarkRequest.Name;
                bookmark.URL = updateBookmarkRequest.URL;
                bookmark.FolderID = updateBookmarkRequest.FolderID;
                bookmark.UpdatedDate = DateTime.UtcNow; //new DateTime(1977, 12, 28, 6, 0, 0, DateTimeKind.Utc);

                await dbContext.SaveChangesAsync();

                return Ok();
            }

            return NotFound();
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteBoomark([FromRoute] Guid id)
        {
            var bookmark = await dbContext.Bookmarks.FindAsync(id);
            if (bookmark != null)
            {
                dbContext.Remove(bookmark);
                await dbContext.SaveChangesAsync();

                return Ok(bookmark);
            }
            return NotFound();
        }


        // Get folders: get a list of folders
        [HttpGet]
        [Route("Folders")]
        public async Task<IActionResult> GetFolders()
        {
            return Ok(await dbContext.Folders.ToListAsync());
        }

        ////GET Folders: get specific folder based on ID
        //[HttpGet]
        //[Route("{id:int}")]
        //public async Task<IActionResult> getfolder([FromRoute] int id)
        //{
        //    var folder = await dbFolderContext.Folders.FindAsync(id);
        //    if (folder == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(folder);
        //}

        //POST Folders: Create a new folder
        [HttpPost]
        [Route("v1/Folders")]
        public async Task<IActionResult> AddFolders(AddFolderRequest addFolderRequest)
        {
            var folder = new Folder()
            {
                Id = addFolderRequest.FolderId,
                Name = addFolderRequest.FolderName,
                Description = addFolderRequest.Description,
                CreatedDate = DateTime.UtcNow //new DateTime(1977, 12, 28, 6, 0, 0, DateTimeKind.Utc)
            };
            await dbContext.Folders.AddAsync(folder);
            await dbContext.SaveChangesAsync();

            return Ok(folder);
        }

        // DROP/DELETE Folders: deletes/drops a folder. 
        [HttpDelete]
        [Route("api/v1/Folders/{id:int}")]
        public async Task<IActionResult> DeleteFolder([FromRoute] int id)
        {
            var folder = await dbContext.Folders.FindAsync(id);
            if (folder != null)
            {
                dbContext.Remove(folder);
                await dbContext.SaveChangesAsync();

                return Ok(folder);
            }
            return NotFound();
        }

    }
}
