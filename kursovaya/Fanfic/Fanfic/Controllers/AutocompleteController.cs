using Fanfic.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic.Controllers
{
    public class AutocompleteController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public AutocompleteController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult AutocompleteSearch(string term)
        {
            List<Tag> tags = _dbContext.Tags.ToList();
            var models = tags.Where(a => a.Name.Contains(term))
                            .Select(a => new { value = a.Name })
                            .Distinct();

            return new JsonResult(models);
        }
    }
}
