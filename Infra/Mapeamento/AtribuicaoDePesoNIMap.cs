using FluentNHibernate.Mapping;
using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Mapeamento
{
    public class AtribuicaoDePesoNIMap : ClassMap<AtribuicaoDePesoNI>
    {
        public AtribuicaoDePesoNIMap()
        {
            Table("AtribuicaoDePesoNI");
            Id(x => x.Id, "idAtribuicaoDePesoNI");
            Map(x => x.Avaliacao, "avaliacao");

            References(x => x.CaracteristicasGerais, "idCaracteristicasGerais")
                .Not
                .LazyLoad()
                .Not
                .Nullable()
                .Cascade
                .None();

            References(x => x.NivelDeInfluencia, "idNivelDeInfluencia")
                .Not
                .LazyLoad()
                .Not
                .Nullable()
                .Cascade
                .None();
        }
    }
}