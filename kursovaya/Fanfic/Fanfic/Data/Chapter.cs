using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic.Data
{
    public class Chapter
    {

        public Chapter()
        {

        }
        public Chapter(Tale tale, string name, int serialNumber, string text = "", string pictureUrl = "")
        {
            this.Tale = tale;
            this.Name = name;
            this.SerialNumber = serialNumber;
            this.Text = text;
            this.PictureUrl = pictureUrl;
        }

        public long Id { get; set; }
        public Tale Tale { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string PictureUrl { get; set; }
        public int SerialNumber { get; set; }
    }
}
