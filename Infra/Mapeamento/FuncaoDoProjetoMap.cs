using FluentNHibernate.Mapping;
using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Mapeamento
{
    public class FuncaoDoProjetoMap : ClassMap<FuncaoDoProjeto>
    {
        public FuncaoDoProjetoMap()
        {
            Table("FuncaoDoProjeto");
            Id(x => x.Id, "idFuncaoDoProjeto");
            Map(x => x.Nome, "nome");
            Map(x => x.Descricao, "descricao");
            Map(x => x.PfBruto, "PFBruto");
            Map(x => x.FatorDeAjuste, "fatorDeAjuste");
            Map(x => x.PfAjustado, "PFAjustado");
            Map(x => x.NivelDeInfluenciaTotal, "NivelDeInfluenciaTotal");
            Map(x => x.TempoRealDeDesenvolvimento, "tempoRealDeDesenvolvimento");
            Map(x => x.EstimativaDeDesenvolvimento, "estimativaDeDesenvolvimento");
            Map(x => x.QtdFatorDeProdutividade, "qtdFatorDeProdutividade");

            References(x => x.Projeto, "idProjeto")
                .Not
                .LazyLoad()
                .Not
                .Nullable()
                .Cascade
                .None();
        }
    }
}