using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Entidades
{
    public class TipoDePontoDeFuncao
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Sigla { get; set; }
    }
}