using Fanfic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic.Services.ChapterOrderedList
{
    public class ChaptersRenumerator : IChaptersListRenumerator
    {
        private readonly ApplicationDbContext _dbContext;
        public ChaptersRenumerator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Chapter> ReplaceChapter(List<Chapter> chapters, int oldIndex, int newIndex)
        {
            int left = oldIndex;
            int right = newIndex;
            int direction = -1;
            if (oldIndex > newIndex)
            {
                (right, left) = (oldIndex, newIndex);
                direction = 1;
            }
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
            return chaptersToUpdate;
        }

        public List<Chapter> RenumerateChaptersAfterDelete(List<Chapter> chapters, int deletedChapterSerialNumber)
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
    }
}
