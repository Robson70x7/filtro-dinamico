using System.Collections.Generic;

namespace FiltrDinamico.Core.Models
{
    public class FiltroOperatoGrouped
    {
        public Operator Operator { get; set; }

        public FiltroOperatoGrouped(Operator @operator)
        {
            Operator = @operator;
            FiltroItems = new List<FiltroItem>();
        }

        public bool IsValid => FiltroItems.Count >= 2;
        
        public ICollection<FiltroItem> FiltroItems{ get; set; }
    }
}
