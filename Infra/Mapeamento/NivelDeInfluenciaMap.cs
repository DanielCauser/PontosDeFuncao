using FluentNHibernate.Mapping;
using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Mapeamento
{
    public class NivelDeInfluenciaMap : ClassMap<NivelDeInfluencia>
    {
        public NivelDeInfluenciaMap()
        {
            Table("NivelDeInfluencia");
            Id(x => x.Id, "idNivelDeInfluencia");
            Map(x => x.Nome, "nome");
        }
    }
}