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
    public class SearchResultModel : PageModel
    {
        public List<Tale> Tales { get; set; } = new List<Tale>();
        private readonly ApplicationDbContext _dbContext;
        public SearchResultModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnGet(List<Tale> tales)
        {
            Tales = tales;
            return Page();
        }
            
        public async Task OnGetSearchByTag(string tagName)
        {
            Tag tag = await _dbContext.Tags.Include(t => t.Tales).ThenInclude(t => t.Tags).FirstOrDefaultAsync(t => t.Name == tagName);
            await OnGet(tag.Tales);
        }
    }
}
