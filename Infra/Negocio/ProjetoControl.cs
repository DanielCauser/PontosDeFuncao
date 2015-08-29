using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using LinqKit;
using NHibernate.Transform;
using NHibernate.SqlCommand;
using NHibernate.Criterion;

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
                        .List();
                }
            }
        }

        public IList<Projeto> Buscar(string nomeCliente, string nomeProjeto)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    var query = session.QueryOver<Projeto>();

                    if (!nomeProjeto.Equals(string.Empty))
                        query.WhereRestrictionOn(p => p.Nome).IsLike("%" + nomeProjeto + "%");

                    if (!nomeCliente.Equals(string.Empty))
                    {
                        Cliente srAlias = null;
                        query.JoinAlias(x => x.Cliente, () => srAlias, JoinType.LeftOuterJoin)
                            .WhereRestrictionOn(x => srAlias.Nome).IsLike("%" + nomeCliente + "%");
                    }

                    IList<Projeto> projetos = query.List();
                    
                    return projetos;
                }
            }
        }
    }
}



