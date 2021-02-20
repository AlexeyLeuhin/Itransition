using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task5.Models;


namespace task5.Context
{
    public class ElementsContext: DbContext
    {
        public ElementsContext(DbContextOptions<ElementsContext> options) : base(options)
        {

        }
        public DbSet<TextArea> TextAreas { get; set; }
        public DbSet<Path> Pathes { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Text> Textes { get; set; }
    }
}
