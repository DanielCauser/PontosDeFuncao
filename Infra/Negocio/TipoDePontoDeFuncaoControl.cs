using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Negocio
{
    public class TipoDePontoDeFuncaoControl
    {

        public TipoDePontoDeFuncao Buscar(int id)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    {
                        return session.Get<TipoDePontoDeFuncao>(id);
                    }
                }
            }
        }
        public IList<TipoDePontoDeFuncao> BuscarTodos()
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    return session.QueryOver<TipoDePontoDeFuncao>()
                        .List();
                }
            }
        }

        public IList<TipoDePontoDeFuncao> BuscarMenosAlguns(int[] ids)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    return session.QueryOver<TipoDePontoDeFuncao>()
                        .WhereRestrictionOn(bp => bp.Id)
                        .Not.IsIn(ids)
                        .List();
                }
            }
        }
    }
}