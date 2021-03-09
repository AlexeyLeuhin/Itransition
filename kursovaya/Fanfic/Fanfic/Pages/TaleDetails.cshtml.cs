using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fanfic.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Fanfic.Areas.TaleDetails
{
    public class TaleDetailsModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;
        public Tale Tale { get; set; }
        public bool IsAuthor { get; set; }
        public User CurrentUser { get; set; }

        public TaleDetailsModel(UserManager<User> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task LoadAsync(long taleId)
        {
            Tale =_dbContext.Tales.Include(c => c.User).Include(c => c.Chapters).FirstOrDefault(t => t.Id == taleId);
            Tale.Chapters.Sort((chapter1, chapter2) => chapter1.SerialNumber.CompareTo(chapter2.SerialNumber));
            CurrentUser = await _userManager.GetUserAsync(User);
            IsAuthor = Tale.User.Id.CompareTo(CurrentUser.Id) == 0 ? true : false;           
        }


        public async Task<IActionResult> OnGetAsync(long taleId)
        {           
            await LoadAsync(taleId);
            return Page();
        }

        public async Task<IActionResult> OnPostAddChapter(long taleId)
        {
            await LoadAsync(taleId);
            Chapter chapter = new Chapter();
            chapter.Name = "New chapter";
            chapter.Tale = Tale;
            chapter.SerialNumber = Tale.Chapters.Count();
            await _dbContext.Chapters.AddAsync(chapter);
            _dbContext.SaveChanges();
            return RedirectToPage("TaleDetails", new { taleId = Tale.Id });
        }

        public async Task<IActionResult> OnPostChangeChaptersOrder(long taleId, int oldIndex, int newIndex)
        {
            await LoadAsync(taleId);
            return Page();
        }

    }
}
