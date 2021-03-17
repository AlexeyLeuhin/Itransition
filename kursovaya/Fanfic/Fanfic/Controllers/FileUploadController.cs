using Azure.Storage.Blobs;
using Fanfic.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fanfic.Services;

namespace Fanfic.Controllers
{
    public class FileUploadController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly IBlobService _blobService;
        private readonly ApplicationDbContext _dbContext;
        public FileUploadController(UserManager<User> userManager, IBlobService blobService, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _blobService = blobService;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAvatar(IFormFile avatar)
        {   
            var user = await _userManager.GetUserAsync(User);
            string blobName = "user - " + user.Id + " - avatar";
            user.AvatarPath = await _blobService.UploadToBlobContainerAsync(avatar, blobName, "avatarcontainer");    //to keystore
            await _userManager.UpdateAsync(user);
            return null;
            
        }

        [HttpPost]
        public async Task<IActionResult> UploadChapterImage(IFormFile chapterImage, long chapterId)
        {
            string blobName = "chapter-" + chapterId;
            Chapter chapter = await _dbContext.Chapters.FindAsync(chapterId);
            chapter.PictureUrl = await _blobService.UploadToBlobContainerAsync(chapterImage, blobName, "chapterimagecontainer");  //to keystore
            _dbContext.Update(chapter);
            await _dbContext.SaveChangesAsync();
            return new JsonResult(chapter.PictureUrl);
        }
    }
}
