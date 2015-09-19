using FluentNHibernate.Mapping;
using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Mapeamento
{
    public class NivelDeComplexidadeMap : ClassMap<NivelDeComplexidade>
    {
        public NivelDeComplexidadeMap()
        {
            Table("NivelDeComplexidade");
            Id(x => x.Id, "idNivelComplexidade");
            Map(x => x.Nome, "nome");
        }
    }
}