using FluentNHibernate.Mapping;
using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Mapeamento
{
    public class AtribuicaoDePesoPFMap : ClassMap<AtribuicaoDePesoPF>
    {
        public AtribuicaoDePesoPFMap()
        {
            Table("AtribuicaoDePesoPF");
            Id(x => x.Id, "idAtribuicaoDePesoPF");
            Map(x => x.Avaliacao, "avaliacao");

            References(x => x.TipoPontoDeFuncao, "idTipoDePontoDeFuncao")
                .Not
                .LazyLoad()
                .Not
                .Nullable()
                .Cascade
                .None();

            References(x => x.NivelDeComplexidade, "idNivelDeComplexidade")
                .Not
                .LazyLoad()
                .Not
                .Nullable()
                .Cascade
                .None();
        }
    }
}