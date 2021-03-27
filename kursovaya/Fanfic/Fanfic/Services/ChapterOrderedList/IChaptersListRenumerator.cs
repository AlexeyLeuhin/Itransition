using Fanfic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic.Services.ChapterOrderedList
{
    public interface IChaptersListRenumerator
    {
        public List<Chapter> ReplaceChapter(List<Chapter> chapters, int oldIndex, int newIndex);
        public List<Chapter> RenumerateChaptersAfterDelete(List<Chapter> chapters, int deletedChapterSerialNumber);
    }
}
