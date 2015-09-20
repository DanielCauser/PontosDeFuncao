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
        public virtual float PfBruto { get; set; }
        public virtual double FatorDeAjuste { get; set; }
        public virtual double PfAjustado { get; set; }
        public virtual float NivelDeInfluenciaTotal { get; set; }
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