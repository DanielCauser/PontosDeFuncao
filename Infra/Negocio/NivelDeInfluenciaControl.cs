using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Negocio
{
    public class NivelDeInfluenciaControl
    {
        public NivelDeInfluencia Buscar(int id)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    {
                        return session.Get<NivelDeInfluencia>(id);
                    }
                }
            }
        }
        public IList<NivelDeInfluencia> BuscarTodos()
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    return session.QueryOver<NivelDeInfluencia>()
                        .List();
                }
            }
        }
    }
}