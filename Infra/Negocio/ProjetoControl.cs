using Infra.Entidades;
using NHibernate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Negocio
{
    public class ProjetoControl
    {
        public void Salvar(Projeto cliente)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                try
                {
                    session.Transaction.Begin();
                    session.SaveOrUpdate(cliente);
                    session.Transaction.Commit();
                }
                catch (Exception e)
                {
                    session.Transaction.Rollback();
                }
            }
        }

        public Projeto Buscar(int id)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    {
                        return session.Get<Projeto>(id);
                    }
                }
            }
        }
        public IList<Projeto> BuscarTodos()
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    return session.QueryOver<Projeto>()
                        //.Where(x => x.Nome == "Ctinf")
                        .List();
                }
            }
        }
    }
}



