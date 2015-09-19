using FluentNHibernate.Mapping;
using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Mapeamento
{
    public class AssociacaoPFMap : ClassMap<AssociacaoPF>
    {
        public AssociacaoPFMap()
        {
            Table("AssociacaoPF");
            Id(x => x.Id, "idAssociacaoPF");
            Map(x => x.Avaliacao, "quantidade");
            Map(x => x.DataAvaliacao, "data");

            References(x => x.FuncaoDoProjeto, "idFuncaoDeProjeto")
                .Not
                .LazyLoad()
                .Not
                .Nullable()
                .Cascade
                .None();

            References(x => x.AtribuicaoDePesoPF, "idAtribuicaoDePesoPF")
                .Not
                .LazyLoad()
                .Not
                .Nullable()
                .Cascade
                .None();
        }
    }
}