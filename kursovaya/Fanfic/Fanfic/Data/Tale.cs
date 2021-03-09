using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic.Data
{
    public class Tale
    {

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
        public int AverageRating { get; set; }
        public long NumberOfRatings { get; set; }
        public List<Chapter> Chapters { get; set; } = new List<Chapter>();
        public DateTime CreationTime { get; set; }
     }
}
