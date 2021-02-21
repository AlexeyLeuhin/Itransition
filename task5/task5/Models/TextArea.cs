using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace task5.Models
{
    [Table(name: "TextArea")]
    public class TextArea
    {
        [Key]
        public long Id { get; set; }
        public string Text { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
