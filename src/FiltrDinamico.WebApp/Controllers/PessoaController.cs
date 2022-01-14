using FiltrDinamico.Core;
using FiltrDinamico.Core.Models;
using FiltrDinamico.WebApp.Context;
using FiltrDinamico.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FiltrDinamico.WebApp.Controllers
{
    [Route("pessoa")]
    public class PessoaController : Controller
    {
        private readonly IFiltroDinamico _filtroDinamico;
        private readonly FiltroDinamicoContext _context;

        public PessoaController(IFiltroDinamico filtroDinamico, FiltroDinamicoContext context)
        {
            _filtroDinamico = filtroDinamico;
            _context = context;
        }

        [HttpPost("filtrar")]
        public IActionResult Filtrar([FromBody] PaginationFilter paginationFilter)
        {
            var expressionDynamic = _filtroDinamico.FromFiltroItemList<Pessoa>(paginationFilter.Filtro.ToList());

            var pessoas = _context.Pessoa.Where(expressionDynamic).ToList();

            return Ok(pessoas);
        }

        [HttpPost("statico")]
        public IActionResult Statico()
        {
            var paginationFilter = new List<FiltroOperatoGrouped>()
            {
                new FiltroOperatoGrouped(Operator.AND)
                {
                    FiltroItems = new List<FiltroItem>()
                    {
                        new FiltroItem
                        {
                           Property = "Nome",
                           FilterType = "equals",
                           Value = "Eliane"
                        },

                        new FiltroItem
                        {
                            Property = "Idade",
                            FilterType = "greaterThan",
                            Value = 10
                        }
                    }
                }
                ,
                 new FiltroOperatoGrouped(Operator.OR)
                {
                    FiltroItems = new List<FiltroItem>()
                    {
                        new FiltroItem
                        {
                           Property = "Nome",
                           FilterType = "equals",
                           Value = "Robson"
                        },

                        new FiltroItem
                        {
                            Property = "Idade",
                            FilterType = "lessThan",
                            Value = 40
                        }
                    }
                }
                 ,
                 new FiltroOperatoGrouped(Operator.AND)
                {
                    FiltroItems = new List<FiltroItem>()
                    {
                        new FiltroItem
                        {
                           Property = "Nome",
                           FilterType = "equals",
                           Value = "Robson"
                        },

                        new FiltroItem
                        {
                            Property = "Idade",
                            FilterType = "lessThan",
                            Value = 40
                        }
                    }
                }
            };

            var expressionDynamic = _filtroDinamico.FromFiltroItemList<Pessoa>(paginationFilter);

            //var pessoas = _context.Pessoa.Where(expressionDynamic).ToList();
            //{P =>   ( ( (  ((P.Nome == "Eliane") AndAlso Invoke(P => (P.Idade > 10), P))
            //           AndAlso Invoke(P => (P.Nome == "Robson"), P))
            //           AndAlso Invoke(P => (P.Idade < 40), P))
            //           AndAlso ((P.Nome == "Robson")
            //           OrElse Invoke(P => (P.Idade < 40), P)))}
            return Ok();
        }
    }
}
