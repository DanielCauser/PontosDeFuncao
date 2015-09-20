using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Negocio
{
    public class CaracteristicasGeraisControl
    {

        public CaracteristicasGerais Buscar(int id)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    {
                        return session.Get<CaracteristicasGerais>(id);
                    }
                }
            }
        }
        public IList<CaracteristicasGerais> BuscarTodos()
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    return session.QueryOver<CaracteristicasGerais>()
                        .List();
                }
            }
        }

        public IList<CaracteristicasGerais> BuscarMenosAlguns(int[] ids)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    return session.QueryOver<CaracteristicasGerais>()
                        .WhereRestrictionOn(bp => bp.Id)
                        .Not.IsIn(ids)
                        .List();
                }
            }
        }
    }
}