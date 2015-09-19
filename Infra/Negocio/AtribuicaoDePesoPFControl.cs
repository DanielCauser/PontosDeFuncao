using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Negocio
{
    public class AtribuicaoDePesoPFControl
    {
        public AtribuicaoDePesoPF Buscar(int id)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    {
                        return session.Get<AtribuicaoDePesoPF>(id);
                    }
                }
            }
        }
        public IList<AtribuicaoDePesoPF> BuscarTodos()
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    return session.QueryOver<AtribuicaoDePesoPF>()
                        .List();
                }
            }
        }
    }
}