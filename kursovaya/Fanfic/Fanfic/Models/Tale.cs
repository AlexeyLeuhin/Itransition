using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic.Data
{
    public class Tale
    {
        public Tale()
        {

        }

        public Tale(User user, string name, string shortDiscription, TaleGanre ganre, DateTime creationTime)
        {
            User = user;
            Name = name;
            ShortDiscription = shortDiscription;
            Ganre = ganre;
            CreationTime = creationTime;
        }

        public enum TaleGanre
        {
            Fantastic,
            Humor,
            PWP,
            Songfic,
            Darkfic,
            Detective,
            Drama,
            Historic
        }

        public long Id { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string ShortDiscription { get; set; }
        public TaleGanre Ganre { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<Rating> Ratings { get; set; } = new List<Rating>();
        public float AverageRating { get; set; } = 0;
        public long NumberOfRatings { get; set; } = 0;
        public List<Chapter> Chapters { get; set; } = new List<Chapter>();
        public DateTime CreationTime { get; set; }
        public int ChaptersCount { get; set; } = 0;
        public List<Comment> Comments { get; set; } = new List<Comment>();
     }
}
