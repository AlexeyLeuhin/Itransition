using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace task5.Models
{
    [Table(name: "Path")]
    public class Path
    {
        public Path() { 
    
        }
        public Path(IEnumerable<Point> points)
        {
            Segments = new List<Point>();
            foreach (var point in points)
            {
                point.PathId = this.Id;
                Segments.Add(point);
            }
        }

        [Key]
        public long Id { get; set; }
        public List<Point> Segments{ get; set; }
    }

    [Table(name: "Point")]
    public class Point
    {
        [Key]
        public long Id { get; set; }
        public int X { get; set; }
        
        public long PathId { get; set; }
        [ForeignKey("PathId")]
        public int Y { get; set; }
    }
}
