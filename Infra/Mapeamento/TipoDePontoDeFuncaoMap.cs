using FluentNHibernate.Mapping;
using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Mapeamento
{
    public class TipoDePontoDeFuncaoMap : ClassMap<TipoDePontoDeFuncao>
    {
        public TipoDePontoDeFuncaoMap()
        {
            Table("TipoDePontoDeFuncao");
            Id(x => x.Id, "idTipoDePontoDeFuncao");
            Map(x => x.Nome, "nome");
            Map(x => x.Sigla, "sigla");
        }
    }
}