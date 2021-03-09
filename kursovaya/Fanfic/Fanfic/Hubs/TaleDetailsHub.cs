using Fanfic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic.Hubs
{
    public class TaleDetailsHub: Hub
    {
        private readonly ApplicationDbContext _dbContext;

        public TaleDetailsHub(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task AddChapter(long taleId)
        {
            Tale tale= await _dbContext.Tales.FindAsync(taleId);
            Chapter chapter = new Chapter();
            chapter.Name = "New chapter";
            chapter.Tale = tale;
            chapter.SerialNumber = tale.Chapters.Count();
            await _dbContext.Chapters.AddAsync(chapter);
            _dbContext.SaveChanges();
            long chapterId = chapter.Id;
            await this.Clients.Caller.SendAsync("AddChapter", chapterId);
        }


    }
}
