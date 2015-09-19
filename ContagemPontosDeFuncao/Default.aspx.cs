using Infra.Entidades;
using Infra.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContagemPontosDeFuncao
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //IList<FuncaoDoProjeto> clientes = new FuncaoDoProjetoControl().BuscarTodos();
            //IList<TipoDePontoDeFuncao> clientes = new TipoDePontoDeFuncaoControl().BuscarTodos();
            //IList<NivelDeComplexidade> clientes = new NivelDeComplexidadeControl().BuscarTodos();
            //IList<AtribuicaoDePesoPF> clientes = new AtribuicaoDePesoPFControl().BuscarTodos();
            //IList<CaracteristicasGerais> clientes = new CaracteristicasGeraisControl().BuscarTodos();
            //IList<NivelDeInfluencia> clientes = new NivelDeInfluenciaControl().BuscarTodos();
            IList<AtribuicaoDePesoNI> clientes = new AtribuicaoDePesoNIControl().BuscarTodos();
        }
    }
}