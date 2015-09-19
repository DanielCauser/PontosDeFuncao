using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Negocio
{
    public class AtribuicaoDePesoNIControl
    {
        public AtribuicaoDePesoNI Buscar(int id)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    {
                        return session.Get<AtribuicaoDePesoNI>(id);
                    }
                }
            }
        }
        public IList<AtribuicaoDePesoNI> BuscarTodos()
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    return session.QueryOver<AtribuicaoDePesoNI>()
                        .List();
                }
            }
        }
    }
}