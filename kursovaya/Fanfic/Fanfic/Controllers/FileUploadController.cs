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
        public FileUploadController(UserManager<User> userManager, IBlobService blobService)
        {
            _userManager = userManager;
            _blobService = blobService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAvatar(IFormFile avatar)
        {   
            var user = await _userManager.GetUserAsync(User);
            string blobName = "user - " + user.Id + " - avatar";
            user.AvatarPath = await _blobService.UploadAvatar(avatar, blobName);
            await _userManager.UpdateAsync(user);
            return null;    
        }
    }
}
