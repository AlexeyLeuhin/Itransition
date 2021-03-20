using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fanfic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Fanfic.Pages
{
    public class IndexModel : PageModel
    {
        private const int TALESTOPSIZE = 10;
        private const int TAGSTOPSIZE = 15;
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
