using Infra.Entidades;
using Infra.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContagemPontosDeFuncao.Paginas.Cadastro.CalculoDePontosDeFuncao
{
    public partial class CalculoPontosDeFuncaoCad : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MenuConsulta();
            }

        }

        #region MULTIVIEW
        protected void mnuPrincipal_MenuItemClick(object sender, MenuEventArgs e)
        {
            string value = ((Menu)sender).SelectedValue;

            switch (value)
            {
                //Cadastro de documento
                case "1": MenuCadastro();
                    //LimparCampos();
                    break;
                //Consultar documento
                case "0": MenuConsulta();
                    popularGridView(new FuncaoDoProjetoControl().BuscarTodos());
                    grdItemProjetos.DataBind();
                    break;

            }
        }
        private void MenuCadastro()
        {
            MultiView1.ActiveViewIndex = 1;
        }
        private void MenuConsulta()
        {
            MultiView1.ActiveViewIndex = 0;
        }
        #endregion

        #region BOTOES
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            popularGridView(new FuncaoDoProjetoControl().Buscar(txtPesquisaItemProjetoProjetoNome.Value.Trim(), txtPesquisaProjetoNome.Value.Trim()));
            grdItemProjetos.DataBind();
            frmCalculoPontosFuncao.Visible = false;
        }
        protected void btnCalcularPontosDeFuncao_Click(object sender, EventArgs e)
        {
            var idsPF = BuscaIdsPF();
            var idsNC = BuscaIdsNC();
            var idsCG = BuscaIdsCG();
            var idsNI = BuscaIdsNI();

            if (Validar(idsPF.ToArray(), idsNC.ToArray(), idsCG.ToArray(), idsNI.ToArray()))
            {
                var aPF = new AtribuicaoDePesoPFControl().BuscarPorPFeNC(idsPF.ToArray(), idsNC.ToArray());
                var aCG = new AtribuicaoDePesoNIControl().BuscarPorCGeNI(idsCG.ToArray(), idsNI.ToArray());

                var itemProjeto = new FuncaoDoProjetoControl().Buscar(Convert.ToInt16(hdfIdItemProjeto.Value));


                int PFB = 0;
                int TNI = 0;
                double FA = 0;
                //calcular pontos de função brutos
                foreach (var p in aPF)
                {
                    PFB = p.Avaliacao + PFB;
                }
                //soma dos NI
                foreach (var n in aCG)
                {
                    TNI = n.Avaliacao + TNI;
                }
                //calcular fator de ajuste 
                FA = 0.65 + (0.01 * TNI);
                //CALCULAR PONTOS DE FUNÇÃO AJUSTADP
                //PFA = PFB * FA
                itemProjeto.PfAjustado = PFB * FA;
                itemProjeto.NivelDeInfluenciaTotal = TNI;
                itemProjeto.PfBruto = PFB;
                itemProjeto.FatorDeAjuste = FA;


                DateTime dataCalculo = DateTime.Now;
                try
                {
                    //AMARRAR O ITEM DE PROJETO COM ASSOCIACAOPF 
                    foreach (var p in aPF)
                    {
                        var associacaoPf = new AssociacaoPF();
                        associacaoPf.FuncaoDoProjeto = itemProjeto;
                        associacaoPf.AtribuicaoDePesoPF = p;
                        associacaoPf.DataAvaliacao = dataCalculo;
                        new AssociacaoPFControl().Salvar(associacaoPf);
                    }
                    foreach (var c in aCG)
                    {
                        var associacaoNI = new AssociacaoNI();
                        associacaoNI.FuncaoDoProjeto = itemProjeto;
                        associacaoNI.AtribuicaoDePesoNI = c;
                        associacaoNI.DataAvaliacao = dataCalculo;
                        new AssociacaoNIControl().Salvar(associacaoNI);
                    }

                    new FuncaoDoProjetoControl().Salvar(itemProjeto);

                    lblPontosDeFuncaoAjustados.Text = itemProjeto.PfAjustado.ToString();
                }
                catch (Exception ex) { }
                //AMARRAR O ITEM DE PROJETO COM ASSOCIACAONI

                //ATUALIZAR O ITEM DE PROJETO COM OS CÁLCULOS

            }
        }

        private bool Validar(int[] idsPF, int[] idsNC, int[] idsCG, int[] idsNI)
        {
            StringBuilder sbMsn = new StringBuilder();

            if (idsPF.Count() == 0 && idsNC.Count() == 0)
                sbMsn.Append("Selecione os tipos de pontos de função e seus níveis de complexidade; ");
            if (idsCG.Count() == 0 && idsNI.Count() == 0)
                sbMsn.Append("Selecione as características gerais e seus níveis de influência; ");
            if (!idsPF.Count().Equals(idsNC.Count()))
                sbMsn.Append("Selecione o nível de complexidade faltante; ");
            if (!idsCG.Count().Equals(idsNI.Count()))
                sbMsn.Append("Selecione o nível de complexidade faltante; ");

            if (sbMsn.ToString().Trim().Equals(string.Empty))
                return true;
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientScript", "<script type=\"text/javascript\">alert('" + sbMsn.ToString() + "');</script>");
                return false;
            }
        }

        #region BUSCAR LABELS
        public List<int> BuscaIdsPF()
        {
            List<int> Labels = new List<int>();

            if (!HiddenFieldPFId1.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldPFId1.Value));

            if (!HiddenFieldPFId2.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldPFId2.Value));

            if (!HiddenFieldPFId3.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldPFId3.Value));

            if (!HiddenFieldPFId4.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldPFId4.Value));

            if (!HiddenFieldPFId5.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldPFId5.Value));

            return Labels;
        }

        public List<int> BuscaIdsNC()
        {
            List<int> Labels = new List<int>();

            if (!HiddenFieldNCId1.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNCId1.Value));

            if (!HiddenFieldNCId2.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNCId2.Value));

            if (!HiddenFieldNCId3.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNCId3.Value));

            if (!HiddenFieldNCId4.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNCId4.Value));

            if (!HiddenFieldNCId5.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNCId5.Value));

            return Labels;
        }

        public List<int> BuscaIdsCG()
        {
            List<int> Labels = new List<int>();

            if (!HiddenFieldCGId1.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldCGId1.Value));

            if (!HiddenFieldCGId2.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldCGId2.Value));

            if (!HiddenFieldCGId3.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldCGId3.Value));

            if (!HiddenFieldCGId4.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldCGId4.Value));

            if (!HiddenFieldCGId5.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldCGId5.Value));

            if (!HiddenFieldCGId6.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldCGId6.Value));

            if (!HiddenFieldCGId7.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldCGId7.Value));

            if (!HiddenFieldCGId8.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldCGId8.Value));

            if (!HiddenFieldCGId9.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldCGId9.Value));

            if (!HiddenFieldCGId10.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldCGId10.Value));

            if (!HiddenFieldCGId11.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldCGId11.Value));

            if (!HiddenFieldCGId12.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldCGId12.Value));

            if (!HiddenFieldCGId13.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldCGId13.Value));

            if (!HiddenFieldCGId14.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldCGId14.Value));

            return Labels;
        }

        public List<int> BuscaIdsNI()
        {
            List<int> Labels = new List<int>();

            if (!HiddenFieldNIId1.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNIId1.Value));

            if (!HiddenFieldNIId2.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNIId2.Value));

            if (!HiddenFieldNIId3.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNIId3.Value));

            if (!HiddenFieldNIId4.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNIId4.Value));

            if (!HiddenFieldNIId5.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNIId5.Value));

            if (!HiddenFieldNIId6.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNIId6.Value));

            if (!HiddenFieldNIId7.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNIId7.Value));

            if (!HiddenFieldNIId8.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNIId8.Value));

            if (!HiddenFieldNIId9.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNIId9.Value));

            if (!HiddenFieldNIId10.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNIId10.Value));

            if (!HiddenFieldNIId11.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNIId11.Value));

            if (!HiddenFieldNIId12.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNIId12.Value));

            if (!HiddenFieldNIId13.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNIId13.Value));

            if (!HiddenFieldNIId14.Value.Equals(string.Empty))
                Labels.Add(Convert.ToInt16(HiddenFieldNIId14.Value));


            return Labels;
        }

        #endregion

        #endregion

        #region CARREGAR GRIDS
        public void popularGridView(IList<FuncaoDoProjeto> fp)
        {
            grdItemProjetos.DataSource = fp;
            grdItemProjetos.DataBind();
        }

        public void popularGridView(IList<TipoDePontoDeFuncao> fp)
        {
            grdTipoPontoDeFuncao.DataSource = fp;
            grdTipoPontoDeFuncao.DataBind();
        }

        public void popularGridView(IList<NivelDeComplexidade> fp)
        {
            grdNivelDeComplexidade.DataSource = fp;
            grdNivelDeComplexidade.DataBind();
        }

        public void popularGridView(IList<CaracteristicasGerais> fp)
        {
            grdCaracteristicasGerais.DataSource = fp;
            grdCaracteristicasGerais.DataBind();
        }

        public void popularGridView(IList<NivelDeInfluencia> fp)
        {
            grdNivelDeInfluencia.DataSource = fp;
            grdNivelDeInfluencia.DataBind();
        }
        #endregion

        #region GRID ITEM PROJETO
        protected void grdItemProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Add"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow linha = grdItemProjetos.Rows[index];
                Label lblId = (Label)linha.FindControl("lblId");

                var itemProj = new FuncaoDoProjetoControl().Buscar(Convert.ToInt16(lblId.Text));

                frmCalculoPontosFuncao.Visible = true;
                lblNomeItemProjeto.Text = itemProj.Nome;
                hdfIdItemProjeto.Value = itemProj.Id.ToString();
                lblnomeProjeto.Text = itemProj.Projeto.Nome;

                grdItemProjetos.DataSource = null;
                grdItemProjetos.DataBind();

                popularGridView(new TipoDePontoDeFuncaoControl().BuscarTodos());
                popularGridView(new NivelDeComplexidadeControl().BuscarTodos());
                popularGridView(new CaracteristicasGeraisControl().BuscarTodos());
                popularGridView(new NivelDeInfluenciaControl().BuscarTodos());

            }
        }
        protected void grdItemProjetos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }
        #endregion

        #region GRID TIPO PONTO DE FUNCAO
        protected void grdTipoPontoDeFuncao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Add"))
            {
                if (PodeIncluirTipoPontoFuncao() != null)
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow linha = grdTipoPontoDeFuncao.Rows[index];
                    Label lblId = (Label)linha.FindControl("lblId");

                    var labelFim = PodeIncluirTipoPontoFuncao();

                    if (LabelAnteriorPrecisaDeNivelDeComplexidade(labelFim))
                    {
                        var tipoPontoFuncao = new TipoDePontoDeFuncaoControl().Buscar(Convert.ToInt16(lblId.Text));
                        labelFim.Text = tipoPontoFuncao.Nome;
                        var HiddenFim = PodeIncluirIdTipoPontoFuncao();
                        HiddenFim.Value = tipoPontoFuncao.Id.ToString();
                        MostraResultadoTipoPontoFuncao();
                        popularGridView(new TipoDePontoDeFuncaoControl().BuscarMenosAlguns(MontaListaIdTipoPontoFuncao()));
                    }
                    else
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientScript", "<script type=\"text/javascript\">alert('Selecione nivel de complexidade do item anterior');</script>");
                }
            }
        }


        public bool LabelAnteriorPrecisaDeNivelDeComplexidade(Label lblAtual)
        {
            if (lblAtual.ID.Equals("LabelPF2"))
                if (LabelNC1.Text.Equals(string.Empty))
                    return false;

            if (lblAtual.ID.Equals("LabelPF3"))
                if (LabelNC2.Text.Equals(string.Empty))
                    return false;

            if (lblAtual.ID.Equals("LabelPF4"))
                if (LabelNC3.Text.Equals(string.Empty))
                    return false;

            if (lblAtual.ID.Equals("LabelPF5"))
                if (LabelNC4.Text.Equals(string.Empty))
                    return false;

            return true;
        }
        public int[] MontaListaIdTipoPontoFuncao()
        {
            List<int> lista = new List<int>();

            if (HiddenFieldPFId1.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldPFId1.Value));

            if (HiddenFieldPFId2.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldPFId2.Value));

            if (HiddenFieldPFId3.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldPFId3.Value));

            if (HiddenFieldPFId4.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldPFId4.Value));

            if (HiddenFieldPFId5.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldPFId5.Value));

            return lista.ToArray();
        }
        public void MostraResultadoTipoPontoFuncao()
        {
            if (!LabelPF1.Text.Equals(string.Empty))
                rwPF1.Visible = true;

            if (!LabelPF2.Text.Equals(string.Empty))
                rwPF2.Visible = true;

            if (!LabelPF3.Text.Equals(string.Empty))
                rwPF3.Visible = true;

            if (!LabelPF4.Text.Equals(string.Empty))
                rwPF4.Visible = true;

            if (!LabelPF5.Text.Equals(string.Empty))
                rwPF5.Visible = true;

        }
        public Label PodeIncluirTipoPontoFuncao()
        {
            if (LabelPF1.Text.Equals(string.Empty))
                return LabelPF1;

            if (LabelPF2.Text.Equals(string.Empty))
                return LabelPF2;

            if (LabelPF3.Text.Equals(string.Empty))
                return LabelPF3;

            if (LabelPF4.Text.Equals(string.Empty))
                return LabelPF4;

            if (LabelPF5.Text.Equals(string.Empty))
                return LabelPF5;

            return null;
        }
        public HiddenField PodeIncluirIdTipoPontoFuncao()
        {
            if (!LabelPF1.Text.Equals(string.Empty))
                if (HiddenFieldPFId1.Value.Equals(string.Empty))
                    return HiddenFieldPFId1;

            if (!LabelPF2.Text.Equals(string.Empty))
                if (HiddenFieldPFId2.Value.Equals(string.Empty))
                    return HiddenFieldPFId2;

            if (!LabelPF3.Text.Equals(string.Empty))
                if (HiddenFieldPFId3.Value.Equals(string.Empty))
                    return HiddenFieldPFId3;

            if (!LabelPF4.Text.Equals(string.Empty))
                if (HiddenFieldPFId4.Value.Equals(string.Empty))
                    return HiddenFieldPFId4;

            if (!LabelPF5.Text.Equals(string.Empty))
                if (HiddenFieldPFId5.Value.Equals(string.Empty))
                    return HiddenFieldPFId5;

            return null;
        }

        #region BOTÕES
        protected void btnPFExcluir1_Click(object sender, ImageClickEventArgs e)
        {
            LabelPF1.Text = string.Empty;
            HiddenFieldPFId1.Value = string.Empty;

            LabelNC1.Text = string.Empty;
            HiddenFieldNCId1.Value = string.Empty;

            rwPF1.Visible = false;

            popularGridView(new TipoDePontoDeFuncaoControl().BuscarMenosAlguns(MontaListaIdTipoPontoFuncao()));

        }
        protected void btnPFExcluir2_Click(object sender, ImageClickEventArgs e)
        {
            LabelPF2.Text = string.Empty;
            HiddenFieldPFId2.Value = string.Empty;

            LabelNC2.Text = string.Empty;
            HiddenFieldNCId2.Value = string.Empty;

            rwPF2.Visible = false;

            popularGridView(new TipoDePontoDeFuncaoControl().BuscarMenosAlguns(MontaListaIdTipoPontoFuncao()));
        }
        protected void btnPFExcluir3_Click(object sender, ImageClickEventArgs e)
        {
            LabelPF3.Text = string.Empty;
            HiddenFieldPFId3.Value = string.Empty;

            LabelNC3.Text = string.Empty;
            HiddenFieldNCId3.Value = string.Empty;

            rwPF3.Visible = false;

            popularGridView(new TipoDePontoDeFuncaoControl().BuscarMenosAlguns(MontaListaIdTipoPontoFuncao()));
        }
        protected void btnPFExcluir4_Click(object sender, ImageClickEventArgs e)
        {
            LabelPF4.Text = string.Empty;
            HiddenFieldPFId4.Value = string.Empty;

            LabelNC4.Text = string.Empty;
            HiddenFieldNCId4.Value = string.Empty;

            rwPF4.Visible = false;

            popularGridView(new TipoDePontoDeFuncaoControl().BuscarMenosAlguns(MontaListaIdTipoPontoFuncao()));
        }
        protected void btnPFExcluir5_Click(object sender, ImageClickEventArgs e)
        {
            LabelPF5.Text = string.Empty;
            HiddenFieldPFId5.Value = string.Empty;

            LabelNC5.Text = string.Empty;
            HiddenFieldNCId5.Value = string.Empty;

            rwPF5.Visible = false;

            popularGridView(new TipoDePontoDeFuncaoControl().BuscarMenosAlguns(MontaListaIdTipoPontoFuncao()));
        }
        #endregion

        #endregion

        #region GRID NIVEL DE COMPLEXIDADE

        protected void grdNivelDeComplexidade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Add"))
            {
                if (PodeIncluirNivelComplexidade() != null)
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow linha = grdNivelDeComplexidade.Rows[index];
                    Label lblId = (Label)linha.FindControl("lblId");

                    var nivelComplexidade = new NivelDeComplexidadeControl().Buscar(Convert.ToInt16(lblId.Text));
                    var labelFim = PodeIncluirNivelComplexidade();
                    var HiddenFim = PodeIncluirIdNivelDeComplexidade();
                    labelFim.Text = nivelComplexidade.Nome;
                    HiddenFim.Value = nivelComplexidade.Id.ToString();
                }
                else
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientScript", "<script type=\"text/javascript\">alert('Selecione o tipo de ponto de função primeiro');</script>");

            }
        }

        public Label PodeIncluirNivelComplexidade()
        {
            if (!LabelPF1.Text.Equals(string.Empty))
                if (LabelNC1.Text.Equals(string.Empty))
                    return LabelNC1;

            if (!LabelPF2.Text.Equals(string.Empty))
                if (LabelNC2.Text.Equals(string.Empty))
                    return LabelNC2;

            if (!LabelPF3.Text.Equals(string.Empty))
                if (LabelNC3.Text.Equals(string.Empty))
                    return LabelNC3;

            if (!LabelPF4.Text.Equals(string.Empty))
                if (LabelNC4.Text.Equals(string.Empty))
                    return LabelNC4;

            if (!LabelPF5.Text.Equals(string.Empty))
                if (LabelNC5.Text.Equals(string.Empty))
                    return LabelNC5;

            return null;
        }
        public HiddenField PodeIncluirIdNivelDeComplexidade()
        {
            if (!LabelPF1.Text.Equals(string.Empty))
                if (LabelNC1.Text.Equals(string.Empty))
                    return HiddenFieldNCId1;

            if (!LabelPF2.Text.Equals(string.Empty))
                if (LabelNC2.Text.Equals(string.Empty))
                    return HiddenFieldNCId2;

            if (!LabelPF3.Text.Equals(string.Empty))
                if (LabelNC3.Text.Equals(string.Empty))
                    return HiddenFieldNCId3;

            if (!LabelPF4.Text.Equals(string.Empty))
                if (LabelNC4.Text.Equals(string.Empty))
                    return HiddenFieldNCId4;

            if (!LabelPF5.Text.Equals(string.Empty))
                if (LabelNC5.Text.Equals(string.Empty))
                    return HiddenFieldNCId5;

            return null;
        }

        #endregion

        #region GRID CARACTERISTICAS GERAIS


        protected void grdCaracteristicasGerais_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Add"))
            {
                if (PodeIncluirCaracteristicaGeral() != null)
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow linha = grdCaracteristicasGerais.Rows[index];
                    Label lblId = (Label)linha.FindControl("lblId");

                    var labelFim = PodeIncluirCaracteristicaGeral();

                    if (LabelAnteriorPrecisaDeNivelDeInfluencia(labelFim))
                    {
                        var caracteristicaGeral = new CaracteristicasGeraisControl().Buscar(Convert.ToInt16(lblId.Text));
                        labelFim.Text = caracteristicaGeral.Nome;
                        var HiddenFim = PodeIncluirIdCaracteristicaGeral();
                        HiddenFim.Value = caracteristicaGeral.Id.ToString();
                        MostraResultadoTipoCaracteristicasGerais();
                        popularGridView(new CaracteristicasGeraisControl().BuscarMenosAlguns(MontaListaIdCaracteristicaGeral()));
                    }
                    else
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientScript", "<script type=\"text/javascript\">alert('Selecione nivel de influência do item anterior');</script>");
                }
            }
        }

        public Label PodeIncluirCaracteristicaGeral()
        {
            if (LabelCG1.Text.Equals(string.Empty))
                return LabelCG1;

            if (LabelCG2.Text.Equals(string.Empty))
                return LabelCG2;

            if (LabelCG3.Text.Equals(string.Empty))
                return LabelCG3;

            if (LabelCG4.Text.Equals(string.Empty))
                return LabelCG4;

            if (LabelCG5.Text.Equals(string.Empty))
                return LabelCG5;

            if (LabelCG6.Text.Equals(string.Empty))
                return LabelCG6;

            if (LabelCG7.Text.Equals(string.Empty))
                return LabelCG7;

            if (LabelCG8.Text.Equals(string.Empty))
                return LabelCG8;

            if (LabelCG9.Text.Equals(string.Empty))
                return LabelCG9;

            if (LabelCG10.Text.Equals(string.Empty))
                return LabelCG10;

            if (LabelCG11.Text.Equals(string.Empty))
                return LabelCG11;

            if (LabelCG12.Text.Equals(string.Empty))
                return LabelCG12;

            if (LabelCG13.Text.Equals(string.Empty))
                return LabelCG13;

            if (LabelCG14.Text.Equals(string.Empty))
                return LabelCG14;


            return null;
        }
        public bool LabelAnteriorPrecisaDeNivelDeInfluencia(Label lblAtual)
        {
            if (lblAtual.ID.Equals("LabelCG2"))
                if (LabelNI1.Text.Equals(string.Empty))
                    return false;

            if (lblAtual.ID.Equals("LabelCG3"))
                if (LabelNI2.Text.Equals(string.Empty))
                    return false;

            if (lblAtual.ID.Equals("LabelCG4"))
                if (LabelNI3.Text.Equals(string.Empty))
                    return false;

            if (lblAtual.ID.Equals("LabelCG5"))
                if (LabelNI4.Text.Equals(string.Empty))
                    return false;

            if (lblAtual.ID.Equals("LabelCG6"))
                if (LabelNI5.Text.Equals(string.Empty))
                    return false;

            if (lblAtual.ID.Equals("LabelCG7"))
                if (LabelNI6.Text.Equals(string.Empty))
                    return false;

            if (lblAtual.ID.Equals("LabelCG8"))
                if (LabelNI7.Text.Equals(string.Empty))
                    return false;

            if (lblAtual.ID.Equals("LabelCG9"))
                if (LabelNI8.Text.Equals(string.Empty))
                    return false;

            if (lblAtual.ID.Equals("LabelCG10"))
                if (LabelNI9.Text.Equals(string.Empty))
                    return false;

            if (lblAtual.ID.Equals("LabelCG11"))
                if (LabelNI10.Text.Equals(string.Empty))
                    return false;

            if (lblAtual.ID.Equals("LabelCG12"))
                if (LabelNI11.Text.Equals(string.Empty))
                    return false;


            if (lblAtual.ID.Equals("LabelCG13"))
                if (LabelNI12.Text.Equals(string.Empty))
                    return false;

            if (lblAtual.ID.Equals("LabelCG14"))
                if (LabelNI13.Text.Equals(string.Empty))
                    return false;

            return true;
        }
        public HiddenField PodeIncluirIdCaracteristicaGeral()
        {
            if (!LabelCG1.Text.Equals(string.Empty))
                if (HiddenFieldCGId1.Value.Equals(string.Empty))
                    return HiddenFieldCGId1;

            if (!LabelCG2.Text.Equals(string.Empty))
                if (HiddenFieldCGId2.Value.Equals(string.Empty))
                    return HiddenFieldCGId2;

            if (!LabelCG3.Text.Equals(string.Empty))
                if (HiddenFieldCGId3.Value.Equals(string.Empty))
                    return HiddenFieldCGId3;

            if (!LabelCG4.Text.Equals(string.Empty))
                if (HiddenFieldCGId4.Value.Equals(string.Empty))
                    return HiddenFieldCGId4;

            if (!LabelCG5.Text.Equals(string.Empty))
                if (HiddenFieldCGId5.Value.Equals(string.Empty))
                    return HiddenFieldCGId5;

            if (!LabelCG6.Text.Equals(string.Empty))
                if (HiddenFieldCGId6.Value.Equals(string.Empty))
                    return HiddenFieldCGId6;

            if (!LabelCG7.Text.Equals(string.Empty))
                if (HiddenFieldCGId7.Value.Equals(string.Empty))
                    return HiddenFieldCGId7;

            if (!LabelCG8.Text.Equals(string.Empty))
                if (HiddenFieldCGId8.Value.Equals(string.Empty))
                    return HiddenFieldCGId8;

            if (!LabelCG9.Text.Equals(string.Empty))
                if (HiddenFieldCGId9.Value.Equals(string.Empty))
                    return HiddenFieldCGId9;

            if (!LabelCG10.Text.Equals(string.Empty))
                if (HiddenFieldCGId10.Value.Equals(string.Empty))
                    return HiddenFieldCGId10;

            if (!LabelCG11.Text.Equals(string.Empty))
                if (HiddenFieldCGId11.Value.Equals(string.Empty))
                    return HiddenFieldCGId11;

            if (!LabelCG12.Text.Equals(string.Empty))
                if (HiddenFieldCGId12.Value.Equals(string.Empty))
                    return HiddenFieldCGId12;

            if (!LabelCG13.Text.Equals(string.Empty))
                if (HiddenFieldCGId13.Value.Equals(string.Empty))
                    return HiddenFieldCGId13;

            if (!LabelCG14.Text.Equals(string.Empty))
                if (HiddenFieldCGId14.Value.Equals(string.Empty))
                    return HiddenFieldCGId14;

            return null;
        }
        public void MostraResultadoTipoCaracteristicasGerais()
        {
            if (!LabelCG1.Text.Equals(string.Empty))
                rwCG1.Visible = true;

            if (!LabelCG2.Text.Equals(string.Empty))
                rwCG2.Visible = true;

            if (!LabelCG3.Text.Equals(string.Empty))
                rwCG3.Visible = true;

            if (!LabelCG4.Text.Equals(string.Empty))
                rwCG4.Visible = true;

            if (!LabelCG5.Text.Equals(string.Empty))
                rwCG5.Visible = true;

            if (!LabelCG6.Text.Equals(string.Empty))
                rwCG6.Visible = true;

            if (!LabelCG7.Text.Equals(string.Empty))
                rwCG7.Visible = true;

            if (!LabelCG8.Text.Equals(string.Empty))
                rwCG8.Visible = true;

            if (!LabelCG9.Text.Equals(string.Empty))
                rwCG9.Visible = true;

            if (!LabelCG10.Text.Equals(string.Empty))
                rwCG10.Visible = true;

            if (!LabelCG11.Text.Equals(string.Empty))
                rwCG11.Visible = true;

            if (!LabelCG12.Text.Equals(string.Empty))
                rwCG12.Visible = true;

            if (!LabelCG13.Text.Equals(string.Empty))
                rwCG13.Visible = true;

            if (!LabelCG14.Text.Equals(string.Empty))
                rwCG14.Visible = true;

        }
        public int[] MontaListaIdCaracteristicaGeral()
        {
            List<int> lista = new List<int>();

            if (HiddenFieldCGId1.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldCGId1.Value));

            if (HiddenFieldCGId2.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldCGId2.Value));

            if (HiddenFieldCGId3.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldCGId3.Value));

            if (HiddenFieldCGId4.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldCGId4.Value));

            if (HiddenFieldCGId5.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldCGId5.Value));

            if (HiddenFieldCGId6.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldCGId6.Value));

            if (HiddenFieldCGId7.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldCGId7.Value));

            if (HiddenFieldCGId8.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldCGId8.Value));

            if (HiddenFieldCGId9.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldCGId9.Value));

            if (HiddenFieldCGId10.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldCGId10.Value));

            if (HiddenFieldCGId11.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldCGId11.Value));

            if (HiddenFieldCGId12.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldCGId12.Value));

            if (HiddenFieldCGId13.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldCGId13.Value));

            if (HiddenFieldCGId14.Value != string.Empty)
                lista.Add(Convert.ToInt16(HiddenFieldCGId14.Value));

            return lista.ToArray();
        }

        #region BOTÕES
        protected void btnCGExcluir1_Click(object sender, ImageClickEventArgs e)
        {
            LabelCG1.Text = string.Empty;
            HiddenFieldCGId1.Value = string.Empty;

            LabelNI1.Text = string.Empty;
            HiddenFieldNIId1.Value = string.Empty;

            rwCG1.Visible = false;

            popularGridView(new CaracteristicasGeraisControl().BuscarMenosAlguns(MontaListaIdCaracteristicaGeral()));

        }
        protected void btnCGExcluir2_Click(object sender, ImageClickEventArgs e)
        {
            LabelCG2.Text = string.Empty;
            HiddenFieldCGId2.Value = string.Empty;

            LabelNI2.Text = string.Empty;
            HiddenFieldNIId2.Value = string.Empty;

            rwCG2.Visible = false;

            popularGridView(new CaracteristicasGeraisControl().BuscarMenosAlguns(MontaListaIdCaracteristicaGeral()));
        }
        protected void btnCGExcluir3_Click(object sender, ImageClickEventArgs e)
        {
            LabelCG3.Text = string.Empty;
            HiddenFieldCGId3.Value = string.Empty;

            LabelNI3.Text = string.Empty;
            HiddenFieldNIId3.Value = string.Empty;

            rwCG3.Visible = false;

            popularGridView(new CaracteristicasGeraisControl().BuscarMenosAlguns(MontaListaIdCaracteristicaGeral()));
        }
        protected void btnCGExcluir4_Click(object sender, ImageClickEventArgs e)
        {
            LabelCG4.Text = string.Empty;
            HiddenFieldCGId4.Value = string.Empty;

            LabelNI4.Text = string.Empty;
            HiddenFieldNIId4.Value = string.Empty;

            rwCG4.Visible = false;

            popularGridView(new CaracteristicasGeraisControl().BuscarMenosAlguns(MontaListaIdCaracteristicaGeral()));
        }
        protected void btnCGExcluir5_Click(object sender, ImageClickEventArgs e)
        {
            LabelCG5.Text = string.Empty;
            HiddenFieldCGId5.Value = string.Empty;

            LabelNI5.Text = string.Empty;
            HiddenFieldNIId5.Value = string.Empty;

            rwCG5.Visible = false;

            popularGridView(new CaracteristicasGeraisControl().BuscarMenosAlguns(MontaListaIdCaracteristicaGeral()));
        }
        protected void btnCGExcluir6_Click(object sender, ImageClickEventArgs e)
        {
            LabelCG6.Text = string.Empty;
            HiddenFieldCGId6.Value = string.Empty;

            LabelNI6.Text = string.Empty;
            HiddenFieldNIId6.Value = string.Empty;

            rwCG6.Visible = false;

            popularGridView(new CaracteristicasGeraisControl().BuscarMenosAlguns(MontaListaIdCaracteristicaGeral()));
        }
        protected void btnCGExcluir7_Click(object sender, ImageClickEventArgs e)
        {
            LabelCG7.Text = string.Empty;
            HiddenFieldCGId7.Value = string.Empty;

            LabelNI7.Text = string.Empty;
            HiddenFieldNIId7.Value = string.Empty;

            rwCG7.Visible = false;

            popularGridView(new CaracteristicasGeraisControl().BuscarMenosAlguns(MontaListaIdCaracteristicaGeral()));
        }
        protected void btnCGExcluir8_Click(object sender, ImageClickEventArgs e)
        {
            LabelCG8.Text = string.Empty;
            HiddenFieldCGId8.Value = string.Empty;

            LabelNI8.Text = string.Empty;
            HiddenFieldNIId8.Value = string.Empty;

            rwCG8.Visible = false;

            popularGridView(new CaracteristicasGeraisControl().BuscarMenosAlguns(MontaListaIdCaracteristicaGeral()));
        }
        protected void btnCGExcluir9_Click(object sender, ImageClickEventArgs e)
        {
            LabelCG9.Text = string.Empty;
            HiddenFieldCGId9.Value = string.Empty;

            LabelNI9.Text = string.Empty;
            HiddenFieldNIId9.Value = string.Empty;

            rwCG9.Visible = false;

            popularGridView(new CaracteristicasGeraisControl().BuscarMenosAlguns(MontaListaIdCaracteristicaGeral()));
        }
        protected void btnCGExcluir10_Click(object sender, ImageClickEventArgs e)
        {
            LabelCG10.Text = string.Empty;
            HiddenFieldCGId10.Value = string.Empty;

            LabelNI10.Text = string.Empty;
            HiddenFieldNIId10.Value = string.Empty;

            rwCG10.Visible = false;

            popularGridView(new CaracteristicasGeraisControl().BuscarMenosAlguns(MontaListaIdCaracteristicaGeral()));
        }
        protected void btnCGExcluir11_Click(object sender, ImageClickEventArgs e)
        {
            LabelCG11.Text = string.Empty;
            HiddenFieldCGId11.Value = string.Empty;

            LabelNI11.Text = string.Empty;
            HiddenFieldNIId11.Value = string.Empty;

            rwCG11.Visible = false;

            popularGridView(new CaracteristicasGeraisControl().BuscarMenosAlguns(MontaListaIdCaracteristicaGeral()));
        }
        protected void btnCGExcluir12_Click(object sender, ImageClickEventArgs e)
        {
            LabelCG12.Text = string.Empty;
            HiddenFieldCGId12.Value = string.Empty;

            LabelNI12.Text = string.Empty;
            HiddenFieldNIId12.Value = string.Empty;

            rwCG12.Visible = false;

            popularGridView(new CaracteristicasGeraisControl().BuscarMenosAlguns(MontaListaIdCaracteristicaGeral()));
        }
        protected void btnCGExcluir13_Click(object sender, ImageClickEventArgs e)
        {
            LabelCG13.Text = string.Empty;
            HiddenFieldCGId13.Value = string.Empty;

            LabelNI13.Text = string.Empty;
            HiddenFieldNIId13.Value = string.Empty;

            rwCG13.Visible = false;

            popularGridView(new CaracteristicasGeraisControl().BuscarMenosAlguns(MontaListaIdCaracteristicaGeral()));
        }
        protected void btnCGExcluir14_Click(object sender, ImageClickEventArgs e)
        {
            LabelCG14.Text = string.Empty;
            HiddenFieldCGId14.Value = string.Empty;

            LabelNI14.Text = string.Empty;
            HiddenFieldNIId14.Value = string.Empty;

            rwCG14.Visible = false;

            popularGridView(new CaracteristicasGeraisControl().BuscarMenosAlguns(MontaListaIdCaracteristicaGeral()));
        }
        #endregion

        #endregion

        #region GRID NIVEL DE INFLUENCIA

        protected void grdNivelDeInfluencia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Add"))
            {
                if (PodeIncluirNivelInfluencia() != null)
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow linha = grdNivelDeInfluencia.Rows[index];
                    Label lblId = (Label)linha.FindControl("lblId");

                    var nivelInfluencia = new NivelDeInfluenciaControl().Buscar(Convert.ToInt16(lblId.Text));
                    var labelFim = PodeIncluirNivelInfluencia();
                    var HiddenFim = PodeIncluirIdNivelInfluencia();
                    labelFim.Text = nivelInfluencia.Nome;
                    HiddenFim.Value = nivelInfluencia.Id.ToString();
                }
                else
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientScript", "<script type=\"text/javascript\">alert('Selecione a característica geral primeiro');</script>");
            }
        }

        public Label PodeIncluirNivelInfluencia()
        {
            if (!LabelCG1.Text.Equals(string.Empty))
                if (LabelNI1.Text.Equals(string.Empty))
                    return LabelNI1;

            if (!LabelCG2.Text.Equals(string.Empty))
                if (LabelNI2.Text.Equals(string.Empty))
                    return LabelNI2;

            if (!LabelCG3.Text.Equals(string.Empty))
                if (LabelNI3.Text.Equals(string.Empty))
                    return LabelNI3;

            if (!LabelCG4.Text.Equals(string.Empty))
                if (LabelNI4.Text.Equals(string.Empty))
                    return LabelNI4;

            if (!LabelCG5.Text.Equals(string.Empty))
                if (LabelNI5.Text.Equals(string.Empty))
                    return LabelNI5;

            if (!LabelCG6.Text.Equals(string.Empty))
                if (LabelNI6.Text.Equals(string.Empty))
                    return LabelNI6;

            if (!LabelCG7.Text.Equals(string.Empty))
                if (LabelNI7.Text.Equals(string.Empty))
                    return LabelNI7;

            if (!LabelCG8.Text.Equals(string.Empty))
                if (LabelNI8.Text.Equals(string.Empty))
                    return LabelNI8;

            if (!LabelCG9.Text.Equals(string.Empty))
                if (LabelNI9.Text.Equals(string.Empty))
                    return LabelNI9;

            if (!LabelCG10.Text.Equals(string.Empty))
                if (LabelNI10.Text.Equals(string.Empty))
                    return LabelNI10;

            if (!LabelCG11.Text.Equals(string.Empty))
                if (LabelNI11.Text.Equals(string.Empty))
                    return LabelNI11;

            if (!LabelCG12.Text.Equals(string.Empty))
                if (LabelNI12.Text.Equals(string.Empty))
                    return LabelNI12;

            if (!LabelCG13.Text.Equals(string.Empty))
                if (LabelNI13.Text.Equals(string.Empty))
                    return LabelNI13;

            if (!LabelCG14.Text.Equals(string.Empty))
                if (LabelNI14.Text.Equals(string.Empty))
                    return LabelNI14;

            return null;
        }
        public HiddenField PodeIncluirIdNivelInfluencia()
        {
            if (!LabelCG1.Text.Equals(string.Empty))
                if (LabelNI1.Text.Equals(string.Empty))
                    return HiddenFieldNIId1;

            if (!LabelCG2.Text.Equals(string.Empty))
                if (LabelNI2.Text.Equals(string.Empty))
                    return HiddenFieldNIId2;

            if (!LabelCG3.Text.Equals(string.Empty))
                if (LabelNI3.Text.Equals(string.Empty))
                    return HiddenFieldNIId3;

            if (!LabelCG4.Text.Equals(string.Empty))
                if (LabelNI4.Text.Equals(string.Empty))
                    return HiddenFieldNIId4;

            if (!LabelCG5.Text.Equals(string.Empty))
                if (LabelNI5.Text.Equals(string.Empty))
                    return HiddenFieldNIId5;

            if (!LabelCG6.Text.Equals(string.Empty))
                if (LabelNI6.Text.Equals(string.Empty))
                    return HiddenFieldNIId6;

            if (!LabelCG7.Text.Equals(string.Empty))
                if (LabelNI7.Text.Equals(string.Empty))
                    return HiddenFieldNIId7;

            if (!LabelCG8.Text.Equals(string.Empty))
                if (LabelNI8.Text.Equals(string.Empty))
                    return HiddenFieldNIId8;

            if (!LabelCG9.Text.Equals(string.Empty))
                if (LabelNI9.Text.Equals(string.Empty))
                    return HiddenFieldNIId9;

            if (!LabelCG10.Text.Equals(string.Empty))
                if (LabelNI10.Text.Equals(string.Empty))
                    return HiddenFieldNIId10;

            if (!LabelCG11.Text.Equals(string.Empty))
                if (LabelNI11.Text.Equals(string.Empty))
                    return HiddenFieldNIId11;

            if (!LabelCG12.Text.Equals(string.Empty))
                if (LabelNI12.Text.Equals(string.Empty))
                    return HiddenFieldNIId12;

            if (!LabelCG13.Text.Equals(string.Empty))
                if (LabelNI13.Text.Equals(string.Empty))
                    return HiddenFieldNIId13;

            if (!LabelCG14.Text.Equals(string.Empty))
                if (LabelNI14.Text.Equals(string.Empty))
                    return HiddenFieldNIId14;

            return null;
        }

        #endregion

    }
}