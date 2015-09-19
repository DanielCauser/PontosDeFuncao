using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Entidades
{
    public class AtribuicaoDePesoPF
    {
        public virtual int Id { get; set; }
        public virtual int Avaliacao { get; set; }
        public virtual TipoDePontoDeFuncao TipoPontoDeFuncao { get; set; }
        public virtual NivelDeComplexidade NivelDeComplexidade { get; set; }
    }
}