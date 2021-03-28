using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fanfic.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Fanfic.Pages
{
    public class IndexModel : PageModel
    {
        private const int TALESTOPSIZE = 6;
        private const int TAGSTOPSIZE = 20;
        public List<Tale> DateSortedTales { get; set; } = new List<Tale>();
        public List<Tale> RatingSortedTales { get; set; } = new List<Tale>();

        private readonly ApplicationDbContext _dbContext;

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task LoadAsync()
        {
            IQueryable<Tale> taleSource = _dbContext.Tales.Include(c => c.Tags);
            DateSortedTales = await taleSource.OrderByDescending(t => t.CreationTime).Take(TALESTOPSIZE).ToListAsync();
            RatingSortedTales = await taleSource.OrderByDescending(t => t.AverageRating).Take(TALESTOPSIZE).ToListAsync();
        }


        public IActionResult OnPostCultureManagment(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
            return LocalRedirect("/");
        }

        public async Task<IActionResult> OnGetTags()
        {
            List<Tag> tags = await _dbContext.Tags.OrderBy(c => c.Weight).Take(TAGSTOPSIZE).ToListAsync();
            return new JsonResult(new { tags });
        }

        public async Task<IActionResult> OnGet()
        {
            await LoadAsync();
            return Page();
        }
    }
}
