using Fanfic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Fanfic.Data.Tale;

namespace Fanfic.Services.Filtrator
{
    public interface ITaleFilterService
    {
        public IQueryable<Tale> FilterByGanre(IQueryable<Tale> tales, TaleGanre? sortGanre);
    }
}
