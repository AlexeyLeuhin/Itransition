using Fanfic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Fanfic.Data.Tale;
using static Fanfic.Services.Sorter.ITaleSortService;

namespace Fanfic.Services.Sorter
{
    public class TaleSorter: ITaleSortService
    {
        public ITaleSortable SortType { private get;set; }

        public IQueryable<Tale> Sort(IQueryable<Tale> tales, SortState sortType)
        {
            switch (sortType)
            {
                case SortState.Alphabet:
                    SortType = new AlphabetSort();
                    break;
                case SortState.Date:
                    SortType = new DateSort();
                    break;
                case SortState.Rating:
                    SortType = new RatingSort();
                    break;
                default:
                    SortType = new AlphabetSort();
                    break;
            }
            return SortType.Sort(tales);
        }
    }
}
