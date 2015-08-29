using FluentNHibernate.Mapping;
using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Mapeamento
{
    public class ProjetoMap : ClassMap<Projeto>
    {
        public ProjetoMap()
        {
            Table("Projeto");
            Id(x => x.Id, "idProjeto");
            Map(x => x.Nome, "nome");
            Map(x => x.Descricao, "descricao");
            //References(x => x.Cliente, "Cliente")
            //    .Column("idCliente");
            References(x => x.Cliente, "idCliente")
                .Not
                .LazyLoad()
                .Not
                .Nullable()
                .Cascade
                .None();
        }
    }
}