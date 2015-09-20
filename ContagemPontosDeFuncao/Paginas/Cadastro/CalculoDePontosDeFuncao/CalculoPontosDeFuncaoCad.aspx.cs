using Infra.Entidades;
using Infra.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
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



    }
}