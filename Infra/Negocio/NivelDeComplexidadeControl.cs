using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Negocio
{
    public class NivelDeComplexidadeControl
    {
        public NivelDeComplexidade Buscar(int id)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    {
                        return session.Get<NivelDeComplexidade>(id);
                    }
                }
            }
        }
        public IList<NivelDeComplexidade> BuscarTodos()
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    return session.QueryOver<NivelDeComplexidade>()
                        .List();
                }
            }
        }
    }
}