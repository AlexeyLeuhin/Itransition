using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fanfic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fanfic.Pages
{
    public class IndexModel : PageModel
    {
        private const int TOPSIZE = 10;
        public List<Tale> DateSortedTales { get; set; } = new List<Tale>();
        public List<Tale> RatingSortedTales { get; set; } = new List<Tale>();

        private readonly ApplicationDbContext _dbContext;

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task LoadAsync()
        {
            IQueryable<Tale> taleSource = _dbContext.Tales;
            DateSortedTales = taleSource.OrderByDescending(t => t.CreationTime).Take(TOPSIZE).ToList();
            RatingSortedTales = taleSource.OrderByDescending(t => t.AverageRating).Take(TOPSIZE).ToList();

        }

        public async Task<IActionResult> OnGet()
        {
            await LoadAsync();
            return Page();
        }
    }
}
