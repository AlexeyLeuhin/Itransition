using Fanfic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic.Services.Sorter
{
    public class DateSort : ITaleSortable
    {
        public IQueryable<Tale> Sort(IQueryable<Tale> tales)
        {
            tales = tales.OrderByDescending(t => t.CreationTime);
            return tales;
        }
    }
}
