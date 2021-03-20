using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic.Data
{
    public class Tag
    {
        public Tag(string name)
        {
            this.Name = name;
        }

        public Tag()
        {

        }

        public int Weight { get; set; } = 1;
        public List<Tale> Tales { get; set; } = new List<Tale>();
        [Key]
        public string Name { get; set; }
    }
}
