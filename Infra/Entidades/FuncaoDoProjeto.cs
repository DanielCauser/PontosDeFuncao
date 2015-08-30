using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Entidades
{
    public class FuncaoDoProjeto
    {
        public virtual int Id { get; set; }
        public virtual Projeto Projeto { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Descricao { get; set; }
        public virtual int PfBruto { get; set; }
        public virtual int FatorDeAjuste { get; set; }
        public virtual int PfAjustado { get; set; }
        public virtual int NivelDeInfluênciaTotal { get; set; }
        public virtual int TempoRealDeDesenvolvimento { get; set; }
        public virtual int EstimativaDeDesenvolvimento { get; set; }
        public virtual int QtdFatorDeProdutividade { get; set; }
        public virtual string Status { get; set; }

        public FuncaoDoProjeto()
        {
            //this.Pessoas = new List<Pessoa>();
        }
    }
}