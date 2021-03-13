using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic.Data
{
    public class Rating
    {
        public Rating(User user, Tale tale, int points)
        {
            User = user;
            Tale = tale;
            Points = points;
        }

        public long RatingId { get; set; }
        public User User { get; set; }
        public Tale Tale { get; set; }
        public int Points { get; set; }
    }
}
