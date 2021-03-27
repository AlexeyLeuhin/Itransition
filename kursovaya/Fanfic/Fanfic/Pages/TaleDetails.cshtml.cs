using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fanfic.Data;
using Fanfic.Services.ChapterOrderedList;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IChaptersListRenumerator _chaptersRenumerator;
        public Tale Tale { get; set; }
        public bool IsAuthor { get; set; } = false;
        public bool CanRate { get; set; } = false;
        public int UserRating { get; set; }
        public List<long> UserLikedChaptersIds { get; set; }
        public string UserId { get; set; }

        public TaleDetailsModel(UserManager<User> userManager, ApplicationDbContext dbContext, IChaptersListRenumerator chaptersRenumerator)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _chaptersRenumerator = chaptersRenumerator;
        }

        public async Task LoadAsync(long taleId)
        {
            Tale = _dbContext.Tales.Include(c => c.User).
                Include(c => c.Chapters).
                Include(c => c.Comments).ThenInclude(c => c.Author).
                FirstOrDefault(t => t.Id == taleId);
            Tale.Chapters.Sort((chapter1, chapter2) => chapter1.SerialNumber.CompareTo(chapter2.SerialNumber));
            if (User.Identity.IsAuthenticated)
            {
                User user = await _userManager.GetUserAsync(User);
                user = _dbContext.Users.Include(c => c.Ratings).ThenInclude(c => c.Tale).FirstOrDefault(us => us.Id == user.Id);
                UserId = user.Id;
                IsAuthor = Tale.User.Id.CompareTo(user.Id) == 0 ? true : false;
                Rating userRating = user.Ratings.FirstOrDefault(rate => rate.Tale.Id == taleId);
                if (!IsAuthor && userRating == null)
                {
                    CanRate = true;
                }
                if (userRating != null)
                {
                    UserRating = userRating.Points;
                }
            }
        }

        public async Task<IActionResult> OnGetAsync(long taleId)
        {
            await LoadAsync(taleId);
            return Page();
        }

        public async Task<IActionResult> OnGetChapterInfo(int chapterId)
        {
            Chapter chapter = _dbContext.Chapters.Include(c => c.Likes).FirstOrDefault(c => c.Id == chapterId);
            int chapterLikes = chapter.LikesNumber;
            bool userLikedChapter;
            if (User.Identity.IsAuthenticated)
            {
                User user = await _userManager.GetUserAsync(User);
                Like like = chapter.Likes.FirstOrDefault(l => l.UserId == user.Id);
                userLikedChapter = like == null ? false : true;
            }
            else
            {
                userLikedChapter = false;
            }
            return new JsonResult(new { chapter.Text, chapterLikes, userLikedChapter });
        }

        public async Task<IActionResult> OnPostChapterText(int chapterId, string text)
        {
            Chapter cha = _dbContext.Chapters.FirstOrDefault(c => c.Id == chapterId);
            cha.Text = text;
            var res = _dbContext.Chapters.Update(cha);
            await _dbContext.SaveChangesAsync();
            return new JsonResult("Saved");
        }

        public async Task<IActionResult> OnPostRenameChapter(long chapterId, string newName)
        {
            Chapter chapter = await _dbContext.Chapters.FindAsync(chapterId);
            chapter.Name = newName;
            _dbContext.Update(chapter);
            _dbContext.SaveChanges();
            return new JsonResult("Ok");
        }

        public async Task<IActionResult> OnPostAddChapter(long taleId)
        {
            Tale tale = await _dbContext.Tales.FindAsync(taleId);
            tale.ChaptersCount += 1;
            Chapter chapter = new Chapter(tale, "New chapter", tale.ChaptersCount - 1, "", "/chapterPlaceholder.png");
            await _dbContext.Chapters.AddAsync(chapter);
            _dbContext.Update(tale);
            _dbContext.SaveChanges();
            return new JsonResult(chapter.Id);
        }

        public async Task<IActionResult> OnPostDeleteChapter(long chapterId, long taleId)
        {
            Tale = await _dbContext.Tales.Include(c => c.Chapters).FirstOrDefaultAsync(t => t.Id == taleId);
            Tale.ChaptersCount -= 1;
            Chapter chapter = await _dbContext.Chapters.FindAsync(chapterId);
            List<Chapter> chapters = _chaptersRenumerator.RenumerateChaptersAfterDelete(Tale.Chapters, chapter.SerialNumber);
            _dbContext.Update(Tale);
            _dbContext.UpdateRange(chapters);
            await _dbContext.DeleteChapter(chapterId);
            _dbContext.SaveChanges();
            return new JsonResult(chapterId);
        }

        public async Task<IActionResult> OnPostRenumerateChapters(long taleId, int oldIndex, int newIndex)
        {
            Tale tale = await _dbContext.Tales.Include(c => c.Chapters).FirstOrDefaultAsync(t => t.Id == taleId);
            List<Chapter> chaptersToUpdate = _chaptersRenumerator.ReplaceChapter(tale.Chapters, oldIndex, newIndex);
            _dbContext.UpdateRange(chaptersToUpdate);
            _dbContext.SaveChanges();
            return new JsonResult("Ok");
        }

        public async Task<IActionResult> OnPostAddRating(int ratingValue, long taleId)
        {
            Tale = await _dbContext.Tales.FindAsync(taleId);
            Rating rating = new Rating(await _userManager.GetUserAsync(User), Tale, ratingValue);
            RecountTaleAverageRating(ratingValue);
            _dbContext.Tales.Update(Tale);
            await _dbContext.Ratings.AddAsync(rating);
            _dbContext.SaveChanges();
            return new JsonResult("Ok");
        }

        private void RecountTaleAverageRating(int newRatingValue)
        {
            Tale.AverageRating = (Tale.AverageRating * Tale.NumberOfRatings + newRatingValue) / (Tale.NumberOfRatings + 1);
            Tale.NumberOfRatings += 1;
        }

        public async Task<IActionResult> OnPostLikePressed(long chapterId)
        {
            User user = await _userManager.GetUserAsync(User);
            Chapter chapter = _dbContext.Chapters.AsNoTracking().Include(c => c.Likes).FirstOrDefault(c => c.Id == chapterId);
            bool userLikedChapter;
            var like = chapter.Likes.FirstOrDefault(l => l.UserId == user.Id);
            if (like != null)
            {
                userLikedChapter = false;
                await _dbContext.DeleteLike(chapter, like);
            }
            else
            {
                userLikedChapter = true;
                await _dbContext.CreateLike(chapter, user);
            }
            int chapterLikes = chapter.LikesNumber;
            return new JsonResult(new { chapterLikes, userLikedChapter });
         }


    }
}
