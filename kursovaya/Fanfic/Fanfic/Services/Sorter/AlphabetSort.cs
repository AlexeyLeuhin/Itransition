using Fanfic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic.Services.Sorter
{
    public class AlphabetSort : ITaleSortable
    {
        public IQueryable<Tale> Sort(IQueryable<Tale> tales)
        {
            tales = tales.OrderBy(t => t.Name);
            return tales;
        }
    }
}
