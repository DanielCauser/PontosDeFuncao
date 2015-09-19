using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Entidades
{
    public class AssociacaoPF
    {
        public virtual int Id { get; set; }
        public virtual int Avaliacao { get; set; }
        public virtual DateTime DataAvaliacao { get; set; }
        public virtual AtribuicaoDePesoPF AtribuicaoDePesoPF { get; set; }
        public virtual FuncaoDoProjeto FuncaoDoProjeto { get; set; }
    }
}