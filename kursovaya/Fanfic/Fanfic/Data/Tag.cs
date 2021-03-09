using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic.Data
{
    public class Tag
    {
        public long Id { get; set; }
        public List<Tale> Tales { get; set; } = new List<Tale>();
        public string Name { get; set; }
    }
}
