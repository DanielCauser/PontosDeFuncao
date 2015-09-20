using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Negocio
{
    public class AtribuicaoDePesoNIControl
    {
        public AtribuicaoDePesoNI Buscar(int id)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    {
                        return session.Get<AtribuicaoDePesoNI>(id);
                    }
                }
            }
        }
        public IList<AtribuicaoDePesoNI> BuscarTodos()
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    return session.QueryOver<AtribuicaoDePesoNI>()
                        .List();
                }
            }
        }

        public IList<AtribuicaoDePesoNI> BuscarPorCGeNI(int[] idsCG, int[] idsNI)
        {
            var sessionFactory = Conexao.CreateSessionFactory();
            {
                using (var session = sessionFactory.OpenSession())
                {
                    var query = session.QueryOver<AtribuicaoDePesoNI>();

                    if (idsCG.Count() == idsNI.Count())
                    {
                        List<AtribuicaoDePesoNI> atribuicaoPF = new List<AtribuicaoDePesoNI>();
                        int id = 0;

                        var lista = session.QueryOver<AtribuicaoDePesoNI>()
                        .List();

                        foreach (var pf in idsCG)
                        {
                            atribuicaoPF.Add(lista.Where(x => x.CaracteristicasGerais.Id == pf && x.NivelDeInfluencia.Id == idsNI[id]).First());
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