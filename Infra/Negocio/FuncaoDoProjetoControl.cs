using Infra.Entidades;
using NHibernate.SqlCommand;
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

        public IList<FuncaoDoProjeto> Buscar(string nomeFuncaoProjeto, string nomeProjeto)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    var query = session.QueryOver<FuncaoDoProjeto>();

                    if (!nomeFuncaoProjeto.Equals(string.Empty))
                        query.WhereRestrictionOn(p => p.Nome).IsLike("%" + nomeFuncaoProjeto + "%");

                    if (!nomeProjeto.Equals(string.Empty))
                    {
                        Projeto srAlias = null;
                        query.JoinAlias(x => x.Projeto, () => srAlias, JoinType.LeftOuterJoin)
                            .WhereRestrictionOn(x => srAlias.Nome).IsLike("%" + nomeProjeto + "%");
                    }

                    IList<FuncaoDoProjeto> funcaoProjeto = query.List();

                    return funcaoProjeto;
                }
            }
        }
    }
}