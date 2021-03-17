using Fanfic.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic.SignalR
{
    public class CommentsHub: Hub
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User>_userManager;

        public CommentsHub(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        
        public async Task PostComment(string commentText, long taleId, string userId)
        {
            
            Tale tale = _dbContext.Tales.Find(taleId);
            User user = await _userManager.FindByIdAsync(userId);
            Comment comment = new Comment(commentText, DateTime.Now, user);
            _dbContext.Add(comment);
            tale.Comments.Add(comment);
            _dbContext.Update(tale);
            await _dbContext.SaveChangesAsync();
            await this.Clients.All.SendAsync("PostComment", comment);
        }
    }
}
