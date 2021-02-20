using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace task5.Models
{
    [Table(name: "Text")]
    public class Text
    {
        [Key]
        public long Id { get; set; }
        public string Data { get; set; }
    }
}
