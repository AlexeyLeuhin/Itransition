using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Fanfic.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static Fanfic.Data.Tale;

namespace Fanfic.Areas.Identity.Pages.Account.Manage
{
    public class TalesModel : PageModel
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        public List<Tale> Tales { get; set; }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        { 
            [Display(Name = "Name")]
            [Required(ErrorMessage = "Enter name pls")]
            public string Name { get; set; }

            [Display(Name = "Ganre")]
            public TaleGanre Ganre { get; set; }

            [Display(Name = "Short Description")]
            [Required(ErrorMessage = "Enter Short Description pls")]
            [StringLength(300, MinimumLength = 10, ErrorMessage = "String length must be between 10 and 300 symbols")]
            public string ShortDescription { get; set; }
        }

        public TalesModel (ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task LoadAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            Tales = _dbContext.Tales.Where<Tale>(tale => tale.User == user).ToList();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostSortByRating()
        {
            await LoadAsync();
            Tales.Sort((tale1, tale2) => { return tale1.AverageRating.CompareTo(tale2.AverageRating); });
            return Page();
        }

        public async Task<IActionResult> OnPostSortByCreationDate()
        {
            await LoadAsync();
            Tales.Sort((tale1, tale2) => { return tale2.CreationTime.CompareTo(tale1.CreationTime); });
            return Page();
        }
        
        public RedirectResult OnPostRedirectToTaleDetails(long Id)
        {
            string url = Url.Page("/TaleDetails", new { taleId = Id });
            return Redirect(url);
        }

        public async Task<IActionResult> OnPostCreateTale()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return null;
            }
            Tale tale = new Tale();
            tale.Name = Input.Name;
            tale.ShortDiscription = Input.ShortDescription;
            tale.User = _userManager.GetUserAsync(User).Result;
            tale.Ganre = Input.Ganre;
            tale.CreationTime = DateTime.Now;
            await _dbContext.AddAsync(tale);
            _dbContext.SaveChanges();     
            return RedirectToPage("Tales");
        }
    }
}
