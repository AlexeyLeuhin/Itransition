using Fanfic.Data;
using Fanfic.Services.Filtrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Fanfic.Data.Tale;

namespace Fanfic.Services
{
    public class TaleFiltator: ITaleFilterService
    {
        public IQueryable<Tale> FilterByGanre(IQueryable<Tale> tales, TaleGanre? sortGanre)
        {
            if (sortGanre != null)
            {
                tales = tales.Where<Tale>(tale => tale.Ganre == sortGanre);
            }
            return tales;
        }
    }
}
