using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Fanfic.Data;
using Fanfic.Services.Filtrator;
using Fanfic.Services.Sorter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Fanfic.Data.Tale;

namespace Fanfic.Areas.Identity.Pages.Account.Manage
{
    


    public class TalesModel : PageModel
    {
        private const int pageSize = 3;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly ITaleSortService _taleSorter;
        private readonly ITaleFilterService _taleFiltrator;
        public List<Tale> Tales { get; set; }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();
        public PageStatus PageInfo { get; set; }
        public class PageStatus
        {
            public PageStatus()
            {

            }
            public PageStatus(SortState sortType, TaleGanre? filterGanre)
            {
                SortType = sortType;
                FilterGanre = filterGanre;
            }
            public SortState SortType { get; set; }
            public TaleGanre? FilterGanre { get; set; }
            public PaginationModel Pagination { get; set; }
        }

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
            public string Tags { get; set; }
        }

        public class PaginationModel
        {
                public int PageNumber { get; private set; }
                public int TotalPages { get; private set; }

                public PaginationModel()
                {   

                }
                public PaginationModel(long dataSize, int pageNumber, int pageSize)
                {
                    PageNumber = pageNumber;
                    TotalPages = (int)Math.Ceiling(dataSize / (double)pageSize);
                }

                public bool HasPreviousPage
                {
                    get
                    {
                        return (PageNumber > 1);
                    }
                }

                public bool HasNextPage
                {
                    get
                    {
                        return (PageNumber < TotalPages);
                    }
                }
        }

        public TalesModel (ApplicationDbContext dbContext, UserManager<User> userManager, ITaleSortService taleSorter, ITaleFilterService taleFiltrator)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _taleSorter = taleSorter;
            _taleFiltrator = taleFiltrator;
        }

        public async Task LoadAsync(TaleGanre? sortGanre, SortState sortType, int page)
        {
            var user = await _userManager.GetUserAsync(User);
            IQueryable<Tale> tales = _dbContext.Tales.Where<Tale>(tale => tale.User == user).Include(c => c.Tags);
            tales = _taleFiltrator.FilterByGanre(tales, sortGanre);
            tales = _taleSorter.Sort(tales, sortType);
            await Paginate(tales, page, await tales.CountAsync());
        }

        private async Task Paginate(IQueryable<Tale> tales, int page, long dataSize)
        {
            Tales = await tales.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageInfo.Pagination = new PaginationModel(dataSize, page, pageSize);
        }

        public async Task<IActionResult> OnGetAsync(TaleGanre? filterGanre, SortState sortType = SortState.Alphabet, int pageNumber = 1)
        {
            PageInfo = new PageStatus(sortType, filterGanre);
            await LoadAsync(filterGanre, sortType, pageNumber);
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
                await OnGetAsync(null);
                return null;
            }
            Tale tale = new Tale(_userManager.GetUserAsync(User).Result, Input.Name, Input.ShortDescription, Input.Ganre, DateTime.Now);
            if (Input.Tags != null)
            {
                foreach (string tag in Input.Tags.Split(" ").ToList())
                {
                    Tag newTag = await _dbContext.Tags.FindAsync(tag);
                    if (newTag == null)
                    {
                        newTag = new Tag(tag);
                        await _dbContext.Tags.AddAsync(newTag);
                    }
                    else
                    {
                        newTag.Weight += 1;
                        _dbContext.Update(newTag);
                    }
                    tale.Tags.Add(newTag);
                }
            }
           
           
            await _dbContext.AddAsync(tale);
            _dbContext.SaveChanges();     
            return RedirectToPage("Tales");
        }

        public async Task<IActionResult> OnPostDeleteTale(long Id, int pageNumber, SortState sortType, TaleGanre? filterGanre)
        {
            await _dbContext.DeleteTale(Id);
            await _dbContext.SaveChangesAsync();
            return RedirectToPage("Tales", new { pageNumber, sortType, filterGanre });
        }

        public IActionResult OnGetAutocompleteSearch(string term)
        {
            List<Tag> tags = _dbContext.Tags.ToList();
            var models = tags.Where(a => a.Name.Contains(term))
                            .Select(a => a.Name )
                            .ToList();

            return new JsonResult(models);
        }
    }

}
