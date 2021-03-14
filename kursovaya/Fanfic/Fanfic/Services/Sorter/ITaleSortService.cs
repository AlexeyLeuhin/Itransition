using Fanfic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fanfic.Services.Sorter
{
    public enum SortState
    {
        Alphabet,
        Rating,
        Date
    }
    public interface ITaleSortService
    {
        public IQueryable<Tale> Sort(IQueryable<Tale> tales, SortState sortType);
    }
}
