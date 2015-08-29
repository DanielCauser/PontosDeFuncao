using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infra.Entidades;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra
{
    public class Conexao
    {

        public static ISessionFactory CreateSessionFactory()
        {
            FluentConfiguration configuration = Fluently.Configure()
                   .Database(MsSqlConfiguration.MsSql2008.ConnectionString(
                        x => x.FromConnectionStringWithKey("ConexaoPrincipal")).ShowSql())
                   .Mappings(x => x.FluentMappings.AddFromAssemblyOf<Cliente>());
            return configuration.BuildSessionFactory();
        }
    }
}
