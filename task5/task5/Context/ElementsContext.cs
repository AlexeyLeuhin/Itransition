using Microsoft.EntityFrameworkCore;
using task5.Models;


namespace task5.Context
{
    public class ElementsContext: DbContext
    {
        public ElementsContext(DbContextOptions<ElementsContext> options) : base(options)
        {

        }
        public DbSet<TextArea> TextAreas { get; set; }
        public DbSet<Text> Textes { get; set; }
        public DbSet<Line> Lines { get; set; }
    }
}
