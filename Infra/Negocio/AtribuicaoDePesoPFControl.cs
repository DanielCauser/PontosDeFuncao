using Infra.Entidades;
using NHibernate.SqlCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Negocio
{
    public class AtribuicaoDePesoPFControl
    {
        public AtribuicaoDePesoPF Buscar(int id)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    {
                        return session.Get<AtribuicaoDePesoPF>(id);
                    }
                }
            }
        }
        public IList<AtribuicaoDePesoPF> BuscarTodos()
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    return session.QueryOver<AtribuicaoDePesoPF>()
                        .List();
                }
            }
        }

        public IList<AtribuicaoDePesoPF> BuscarPorPFeNC(int[] idsPF, int[] idsNC)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    var query = session.QueryOver<AtribuicaoDePesoPF>();
                    
                    if (idsPF.Count() == idsNC.Count())
                    {
                        List<AtribuicaoDePesoPF> atribuicaoPF = new List<AtribuicaoDePesoPF>();
                        int id = 0;

                        var lista = session.QueryOver<AtribuicaoDePesoPF>()
                        .List();

                        foreach (var pf in idsPF)
                        {
                            atribuicaoPF.Add(lista.Where(x => x.TipoPontoDeFuncao.Id == pf && x.NivelDeComplexidade.Id == idsNC[id]).First());
                            id++;
                        }
                        return atribuicaoPF;
                    }
                    return null;
                }
            }
        }
    }
}