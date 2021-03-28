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

        public async Task OnPostSearch(string query)
        {
            IQueryable<Tale> tales = _dbContext.Tales.
                Where(tale => EF.Functions.FreeText(tale.Name, query) || EF.Functions.FreeText(tale.ShortDiscription, query));

            IQueryable<Chapter> chapters = _dbContext.Chapters.Include(chapter => chapter.Tale).
                Where(chapter => EF.Functions.FreeText(chapter.Name, query) || EF.Functions.FreeText(chapter.Text, query));

            IQueryable<Comment> comments = _dbContext.Comments.Include(comment => comment.Tale).
                Where(comment => EF.Functions.FreeText(comment.Message, query));

            HashSet<Tale> searchResult = tales.ToHashSet();
            foreach(var chapter in chapters.ToList())
            {
                searchResult.Add(chapter.Tale);
            }
            foreach (var comment in comments.ToList())
            {
                searchResult.Add(comment.Tale);
            }
            await OnGet(searchResult.ToList());
        }
    }
}
