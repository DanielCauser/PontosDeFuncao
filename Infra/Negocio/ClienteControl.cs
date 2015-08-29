using Infra.Entidades;
using System;
using System.Collections.Generic;

namespace Infra.Negocio
{
    public class ClienteControl
    {
        public void Salvar(Cliente cliente)
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

        public Cliente Buscar(int id)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    {
                        return session.Get<Cliente>(id);
                    }
                }
            }
        }
        public IList<Cliente> BuscarTodos()
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    return session.QueryOver<Cliente>()
                        //.Where(x => x.Nome == "Ctinf")
                        .List();
                }
            }
        }

        public IList<Cliente> BuscarTodosMenos(int id)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    return session.QueryOver<Cliente>()
                        .Where(x => x.Id != id)
                        .List();
                }
            }
        }

        public IList<Cliente> Buscar(string NomeCliente)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    if (NomeCliente != string.Empty)
                    {
                        return session.QueryOver<Cliente>()
                            .WhereRestrictionOn(x => x.Nome).IsLike(NomeCliente)
                            .List();
                    }
                    else
                        return session.QueryOver<Cliente>()
                            .List();
                }
            }
        }
    }
}



