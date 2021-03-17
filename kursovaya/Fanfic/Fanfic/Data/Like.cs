using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic.Data
{
    public class Like
    {
        public Like()
        {

        }

        public Like(string userId, Chapter chapter)
        {
            this.UserId = userId;
            this.Chapter = chapter;
        }
        public long LikeId { get; set; }
        public string UserId { get; set; }
        public Chapter Chapter { get; set; }
    }
}
