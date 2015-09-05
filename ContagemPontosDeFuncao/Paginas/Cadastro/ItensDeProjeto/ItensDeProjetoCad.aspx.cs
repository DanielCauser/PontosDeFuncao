using Infra.Entidades;
using Infra.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContagemPontosDeFuncao.Paginas.Cadastro.ItensDeProjeto
{
    public partial class ItensDeProjetoCad : System.Web.UI.Page
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
                    LimparCampos();
                    break;
                //Consultar documento
                case "0": MenuConsulta();
                    popularGridView(new ProjetoControl().BuscarTodos());
                    grdProjetos.DataBind();
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
            grdProjetos.DataBind();
        }

        protected void btnPesquisarProjeto_Click(object sender, EventArgs e)
        {
            var projetos = new ProjetoControl().Buscar("", txtNomeProjetoCadastroPesquisa.Value.Trim());
            popularGridView(projetos);
        }

        protected void btnCadastrarProjeto_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                try
                {
                    if (hdfIdItemProjeto.Value.Equals(string.Empty))
                    {
                        var projeto = new Projeto();
                        var funcaoProjeto = new FuncaoDoProjeto();

                        projeto.Id = Convert.ToInt16(hdfIdProjeto.Value);

                        funcaoProjeto.Projeto = projeto;
                        funcaoProjeto.Nome = txtNomeItemProjeto.Value;
                        funcaoProjeto.Descricao = txtDescricaoItemProjeto.Value;

                        new FuncaoDoProjetoControl().Salvar(funcaoProjeto);

                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientScript", "<script type=\"text/javascript\">alert('Item de projeto cadastrado com sucesso!');</script>");

                        LimparCampos();
                        grdProjetos.DataSource = null;
                        popularGridView(new ProjetoControl().BuscarTodos());
                        MenuConsulta();
                    }
                    else
                    {
                        var projeto = new Projeto();
                        var funcaoProjeto = new FuncaoDoProjeto();

                        projeto.Id = Convert.ToInt16(hdfIdProjeto.Value);
                        funcaoProjeto.Projeto = projeto;

                        funcaoProjeto.Id = Convert.ToInt16(hdfIdItemProjeto.Value);
                        funcaoProjeto.Nome = txtNomeItemProjeto.Value;
                        funcaoProjeto.Descricao = txtDescricaoItemProjeto.Value;

                        new FuncaoDoProjetoControl().Salvar(funcaoProjeto);

                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientScript", "<script type=\"text/javascript\">alert('Item de projeto editado com sucesso!');</script>");

                        LimparCampos();
                        grdProjetos.DataSource = null;
                        popularGridView(new FuncaoDoProjetoControl().BuscarTodos());
                        MenuConsulta();
                        btnCadastrarProjeto.Text = "Cadastrar";
                        lblAcaoItemProjeto.Text = "Cadastrar";
                    }

                }
                catch (Exception ex)
                {

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientScript", "<script type=\"text/javascript\">alert('" + ex.Message + "');</script>");
                }
            }
        }

        private bool Validar()
        {
            StringBuilder sbMsn = new StringBuilder();

            if (hdfIdProjeto.Value.Equals(string.Empty))
                sbMsn.Append("Informe: O projeto da função de projeto; ");

            if (txtDescricaoItemProjeto.Value.Trim().Equals(string.Empty))
                sbMsn.Append("Informe: A descrição da função do projeto; ");

            if (txtNomeItemProjeto.Value.Trim().Equals(string.Empty))
                sbMsn.Append("Informe: O nome da função do projeto; ");

            if (sbMsn.ToString().Trim().Equals(string.Empty))
                return true;
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientScript", "<script type=\"text/javascript\">alert('" + sbMsn.ToString() + "');</script>");
                return false;
            }
        }

        private void LimparCampos()
        {
            hdfIdProjeto.Value = string.Empty;
            txtNomeItemProjeto.Value = string.Empty;
            txtDescricaoItemProjeto.Value = string.Empty;
            lblNomeProjeto.Text = string.Empty;
            txtNomeProjetoCadastroPesquisa.Value = string.Empty;
            btnCadastrarProjeto.Text = "Cadastrar";
            hdfIdItemProjeto.Value = string.Empty;
            frmInformacoesItemProjeto.Visible = false;
            lblAcaoItemProjeto.Text = "Cadastrar";
        }

        #endregion

        #region GRIDS
        public void popularGridView(IList<FuncaoDoProjeto> fp)
        {
            grdItemProjetos.DataSource = fp;
            grdItemProjetos.DataBind();
        }


        public void popularGridView(IList<Projeto> p)
        {
            grdProjetos.DataSource = p;
            grdProjetos.DataBind();
        }

        protected void grdProjetos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }

        protected void grdItemProjetos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }

        protected void grdProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Adcionar"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow linha = grdProjetos.Rows[index];
                Label lblId = (Label)linha.FindControl("lblId");

                var projeto = new ProjetoControl().Buscar(Convert.ToInt16(lblId.Text));
                lblNomeProjeto.Text = projeto.Nome;
                hdfIdProjeto.Value = projeto.Id.ToString();

                popularGridView(new ProjetoControl().BuscarTodosMenos(projeto.Id));
                frmInformacoesItemProjeto.Visible = true;
            }
        }

        protected void grdItemProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow linha = grdItemProjetos.Rows[index];
                Label lblId = (Label)linha.FindControl("lblId");

                var itemProj = new FuncaoDoProjetoControl().Buscar(Convert.ToInt16(lblId.Text));

                lblNomeProjeto.Text = itemProj.Projeto.Nome;
                hdfIdProjeto.Value = itemProj.Projeto.Id.ToString();
                txtNomeItemProjeto.Value = itemProj.Nome;
                txtDescricaoItemProjeto.Value = itemProj.Descricao;
                hdfIdItemProjeto.Value = itemProj.Id.ToString();
                MenuCadastro();
                btnCadastrarProjeto.Text = "Salvar";
                lblAcaoItemProjeto.Text = "Editar";
                frmInformacoesItemProjeto.Visible = true;
            }
        }
        #endregion


    }
}