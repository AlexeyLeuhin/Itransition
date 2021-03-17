using Fanfic.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fanfic.Data
{
    
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        private readonly IBlobService _blobService;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IBlobService blobService)
            : base(options)
        {
            _blobService = blobService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Tale>()
                .Property(c => c.Ganre)
                .HasConversion<string>();            

            base.OnModelCreating(builder);
        }

        public async Task DeleteChapter(long chapterId)
        {
            Chapter chapter = await Chapters.Include(c => c.Likes).FirstOrDefaultAsync(c => c.Id == chapterId);
            Likes.RemoveRange(chapter.Likes.ToArray());
            await _blobService.DeleteChapterImageFromBlobsIfExists(chapterId);
            Chapters.Remove(chapter);
            this.SaveChanges();
        }

        public async Task DeleteTale(long taleId)
        {
            Tale tale = await Tales.Include(c => c.Chapters).Include(c => c.Ratings).FirstAsync(tale => tale.Id == taleId);
            foreach(Chapter chapter in tale.Chapters.ToArray())
            {
                await DeleteChapter(chapter.Id);
            }
            this.RemoveRange(tale.Ratings);
            Tales.Remove(tale);
            this.SaveChanges();
        }

        public DbSet<Tale> Tales { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
