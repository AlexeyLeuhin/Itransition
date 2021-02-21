using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace task5.Models
{
    [Table(name: "Line")]
    public class Line
    {
        [Key]
        public long Id { get; set; }
        public String Data { get; set; }
    }
}
