using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Negocio
{
    public class FuncaoDoProjetoControl
    {

        public void Salvar(FuncaoDoProjeto funcaoDoProjeto)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                try
                {
                    session.Transaction.Begin();
                    session.SaveOrUpdate(funcaoDoProjeto);
                    session.Transaction.Commit();
                }
                catch (Exception e)
                {
                    session.Transaction.Rollback();
                }
            }
        }

        public FuncaoDoProjeto Buscar(int id)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    {
                        return session.Get<FuncaoDoProjeto>(id);
                    }
                }
            }
        }
        public IList<FuncaoDoProjeto> BuscarTodos()
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    return session.QueryOver<FuncaoDoProjeto>()
                        .List();
                }
            }
        }
    }
}