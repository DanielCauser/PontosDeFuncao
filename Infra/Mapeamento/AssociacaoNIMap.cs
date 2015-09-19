using FluentNHibernate.Mapping;
using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Mapeamento
{
    public class AssociacaoNIMap : ClassMap<AssociacaoNI>
    {
        public AssociacaoNIMap()
        {
            Table("AssociacaoNI");
            Id(x => x.Id, "idAssociacaoNI");
            Map(x => x.Avaliacao, "avaliacao");
            Map(x => x.DataAvaliacao, "data");

            References(x => x.FuncaoDoProjeto, "idFuncaoDoProjeto")
                .Not
                .LazyLoad()
                .Not
                .Nullable()
                .Cascade
                .None();

            References(x => x.AtribuicaoDePesoNI, "idAtribuicaodePesoNI")
                .Not
                .LazyLoad()
                .Not
                .Nullable()
                .Cascade
                .None();
        }
    }
}