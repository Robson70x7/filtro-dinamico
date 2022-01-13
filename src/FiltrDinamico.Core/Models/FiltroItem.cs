using System.Collections.Generic;

namespace FiltrDinamico.Core.Models
{
    public struct FiltroItem
    {
        public string Property { get; set; }
        public string FilterType { get; set; }
        public object Value { get; set; }
    }

    public class FiltroOperatoGrouped
    {
        public Operator Operator { get; set; }

        public FiltroOperatoGrouped(Operator @operator)
        {
            Operator = @operator;
        }

        public IEnumerable<FiltroItem> FiltroItems{ get; set; }
    }


    public class FiltroBody
    {
        public IEnumerable<FiltroOperatoGrouped> Filtros { get; set; }

        public void Create()
        {
            Filtros = new List<FiltroOperatoGrouped>
            {
                new FiltroOperatoGrouped(Operator.AND)
                {
                    FiltroItems = new List<FiltroItem>
                    {
                        new FiltroItem
                        {
                            Property = "Nome",
                            FilterType = "startsWhith",
                            Value = "c"
                        },
                        new FiltroItem
                        {
                            Property = "Idade",
                            FilterType = "gratherThan",
                            Value = 18
                        }
                    }
                },

                new FiltroOperatoGrouped(Operator.OR)
                {
                    FiltroItems = new List<FiltroItem>
                    {
                        new FiltroItem
                        {
                            Property = "Sexo",
                            FilterType = "equals",
                            Value = "F"
                        },
                        new FiltroItem
                        {
                            Property = "Idade",
                            FilterType = "gratherThan",
                            Value = 18
                        }
                    }
                },
                //s => ((s.Idade > 10 && s.Idade < 18) || (s.Idade <= 10 && s.Permissao)) && s.Cidade == "X"
            };
        }
    }

    public enum Operator
    {
        AND =1,
        OR=2
    }
}
