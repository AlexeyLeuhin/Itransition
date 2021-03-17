using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fanfic.Data;
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
        public Tale Tale { get; set; }
        public bool IsAuthor { get; set; } = false;
        public bool CanRate { get; set; } = false;
        public int UserRating { get; set; }
        public List<long> UserLikedChaptersIds { get; set; }

        public TaleDetailsModel(UserManager<User> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task LoadAsync(long taleId)
        {
            Tale = _dbContext.Tales.Include(c => c.User).Include(c => c.Chapters).FirstOrDefault(t => t.Id == taleId);
            Tale.Chapters.Sort((chapter1, chapter2) => chapter1.SerialNumber.CompareTo(chapter2.SerialNumber));
            if (User.Identity.IsAuthenticated)
            {
                User user = await _userManager.GetUserAsync(User);
                user = _dbContext.Users.Include(c => c.Ratings).ThenInclude(c => c.Tale).FirstOrDefault(us => us.Id == user.Id);
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
            Chapter cha = _dbContext.Chapters.Include(c => c.Likes).FirstOrDefault(c => c.Id == chapterId);
            int chapterLikes = cha.LikesNumber;
            bool userLikedChapter;
            if (User.Identity.IsAuthenticated)
            {
                User user = await _userManager.GetUserAsync(User);
                Like like = cha.Likes.FirstOrDefault(l => l.UserId == user.Id);
                userLikedChapter = like == null ? false : true;
            }
            else
            {
                userLikedChapter = false;
            }
            return new JsonResult(new { cha.Text, chapterLikes, userLikedChapter });
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
            Chapter chapter = new Chapter(tale, "New chapter", tale.ChaptersCount - 1);
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
            List<Chapter> chapters = RenumerateChaptersAfterDelete(Tale.Chapters, chapter.SerialNumber);
            _dbContext.Update(Tale);
            _dbContext.UpdateRange(chapters);
            await _dbContext.DeleteChapter(chapterId);
            _dbContext.SaveChanges();
            return new JsonResult(chapterId);
        }

        private List<Chapter> RenumerateChaptersAfterDelete(List<Chapter> chapters, int deletedChapterSerialNumber)
        {
            chapters.ForEach(c =>
            {
                if (c.SerialNumber > deletedChapterSerialNumber)
                {
                    c.SerialNumber -= 1;
                }
            });
            return chapters;
        }

        public async Task<IActionResult> OnPostRenumerateChapters(long taleId, int oldIndex, int newIndex)
        {
            int left = oldIndex;
            int right = newIndex;
            int direction = -1;
            if (oldIndex > newIndex)
            {
                right = oldIndex;
                left = newIndex;
                direction = 1;
            }
            Tale tale = await _dbContext.Tales.Include(c => c.Chapters).FirstOrDefaultAsync(t => t.Id == taleId);
            List<Chapter> chapters = tale.Chapters;
            List<Chapter> chaptersToUpdate = new List<Chapter>();
            chapters.ForEach(c =>
            {
                if (c.SerialNumber == oldIndex)
                {
                    c.SerialNumber = newIndex;
                    chaptersToUpdate.Add(c);
                }
                else if (c.SerialNumber >= left && c.SerialNumber <= right)
                {
                    c.SerialNumber += direction;
                    chaptersToUpdate.Add(c);
                }
            });
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
                Chapter chapter = _dbContext.Chapters.Include(c => c.Likes).FirstOrDefault(c => c.Id == chapterId);
                bool userLikedChapter;
                var like = chapter.Likes.FirstOrDefault(l => l.UserId == user.Id);
                if (like != null)
                {
                    userLikedChapter = false;
                    _dbContext.Remove(like);
                    chapter.LikesNumber -= 1;
                }
                else
                {
                    userLikedChapter = true;
                    like = new Like(user.Id, chapter);
                    chapter.LikesNumber += 1;
                    _dbContext.Add(like);
                }
                _dbContext.Update(chapter);
                await _dbContext.SaveChangesAsync();
                int chapterLikes = chapter.LikesNumber;
                return new JsonResult(new { chapterLikes, userLikedChapter });
        }
    }
}
