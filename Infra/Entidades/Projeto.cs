using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Entidades
{
    public class Projeto
    {
        public virtual int Id { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string EstimativaDeTempo { get; set; }
        public virtual string EstimativaDeCusto { get; set; }
        public virtual string PFTotalAjustado { get; set; }
        public virtual string ValorAtraso { get; set; }
        public virtual string Status { get; set; }

        public Projeto()
        {
            //this.Pessoas = new List<Pessoa>();
        }

    }
}