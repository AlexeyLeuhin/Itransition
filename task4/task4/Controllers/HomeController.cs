using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using task4.Areas.Identity.Data;
using task4.Data;
using task4.Models;

namespace task4.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AuthDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AuthDbContext dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {        
            return View(_userManager.Users.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Index(IEnumerable<ApplicationUser> users, string action)
        {
            foreach (var user in users)
            {
                if (user.IsSelected)
                {
                    if (action == "DeleteSelected")
                    {
                        await DeleteUser(user);
                    }
                    if (action == "BlockSelected")
                    {
                        await BlockUser(user);
                    }
                    if (action == "UnblockSelected")
                    {
                        await UnblockUser(user);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteUser(ApplicationUser user)
        {
            var currentUser = await _userManager.FindByIdAsync(user.Id);
            if (currentUser != null)
            {
                var result = await _userManager.DeleteAsync(currentUser);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
        
        //[HttpPost]
        //public async Task<IActionResult> DeleteSelected(IEnumerable<ApplicationUser> users)
        //{
        //    foreach (var user in users)
        //    {
        //        if (user.IsSelected)
        //        {
        //            var u = await _userManager.FindByIdAsync(user.Id);
        //            if (u != null)
        //            {
        //                var result = await _userManager.DeleteAsync(u);
        //                await _dbContext.SaveChangesAsync();
        //                if (!result.Succeeded)
        //                {
        //                    foreach (var error in result.Errors)
        //                    {
        //                        ModelState.AddModelError(string.Empty, error.Description);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return RedirectToAction("Index", "Home");
        //}
        
        public async Task<IActionResult> BlockUser(ApplicationUser user)
        {
            var currentUser = await _userManager.FindByIdAsync(user.Id);
            if (currentUser != null)
            {               
                currentUser.LockoutEnabled = false;
                await _dbContext.SaveChangesAsync();
                if (User.Identity.Name == currentUser.UserName)
                {
                    await _signInManager.SignOutAsync();
                }               
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> UnblockUser(ApplicationUser user)
        {
            var currentUser = await _userManager.FindByIdAsync(user.Id);
            if (currentUser != null)
            {
                currentUser.LockoutEnabled = true;
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
