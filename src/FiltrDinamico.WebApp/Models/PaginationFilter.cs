using FiltrDinamico.Core.Models;
using System.Collections.Generic;

namespace FiltrDinamico.WebApp.Models
{
    public class PaginationFilter
    {
        public IEnumerable<FiltroOperatoGrouped> Filtro { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
