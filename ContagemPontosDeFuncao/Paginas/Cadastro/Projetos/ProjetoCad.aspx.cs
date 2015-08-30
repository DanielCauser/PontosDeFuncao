using Infra.Entidades;
using Infra.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ContagemPontosDeFuncao
{
    public partial class ProjetoCad : Page
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
            popularGridView(new ProjetoControl().Buscar(txtPesquisaProjetoCliente.Value.Trim(), txtPesquisaProjetoNome.Value.Trim()));
            grdProjetos.DataBind();
        }

        protected void btnPesquisarCliente_Click(object sender, EventArgs e)
        {
            var clientes = new ClienteControl().Buscar(txtNomeClienteCadastroPesquisa.Value.Trim());
            popularGridView(clientes);
        }

        protected void btnCadastrarProjeto_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                try
                {
                    if (hdfIdProjeto.Value.Equals(string.Empty))
                    {
                        var projeto = new Projeto();
                        var cliente = new Cliente();

                        cliente.Id = Convert.ToInt16(hdfIdCliente.Value);

                        projeto.Cliente = cliente;
                        projeto.Nome = txtNomeProjeto.Value;
                        projeto.Descricao = txtDescricaoProjeto.Value;

                        new ProjetoControl().Salvar(projeto);

                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientScript", "<script type=\"text/javascript\">alert('Projeto cadastrado com sucesso!');</script>");

                        LimparCampos();
                        grdCliente.DataSource = null;
                        popularGridView(new ProjetoControl().BuscarTodos());
                        MenuConsulta();
                    }
                    else
                    {
                        var projeto = new Projeto();
                        var cliente = new Cliente();

                        cliente.Id = Convert.ToInt16(hdfIdCliente.Value);
                        projeto.Cliente = cliente;

                        projeto.Id = Convert.ToInt16(hdfIdProjeto.Value);
                        projeto.Nome = txtNomeProjeto.Value;
                        projeto.Descricao = txtDescricaoProjeto.Value;

                        new ProjetoControl().Salvar(projeto);

                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientScript", "<script type=\"text/javascript\">alert('Projeto editado com sucesso!');</script>");

                        LimparCampos();
                        grdCliente.DataSource = null;
                        popularGridView(new ProjetoControl().BuscarTodos());
                        MenuConsulta();
                        btnCadastrarProjeto.Text = "Cadastrar";
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

            if (hdfIdCliente.Value.Equals(string.Empty))
                sbMsn.Append("Informe: O cliente do projeto; ");

            if (txtDescricaoProjeto.Value.Trim().Equals(string.Empty))
                sbMsn.Append("Informe: A descrição do projeto; ");

            if (txtNomeProjeto.Value.Trim().Equals(string.Empty))
                sbMsn.Append("Informe: O nome do projeto; ");

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
            hdfIdCliente.Value = string.Empty;
            txtNomeProjeto.Value = string.Empty;
            txtDescricaoProjeto.Value = string.Empty;
            lblNomeCliente.Text = string.Empty;
            txtNomeClienteCadastroPesquisa.Value = string.Empty;
            btnCadastrarProjeto.Text = "Cadastrar";
            hdfIdProjeto.Value = string.Empty;
        }

        #endregion

        #region GRIDS
        public void popularGridView(IList<Cliente> c)
        {
            grdCliente.DataSource = c;
            grdCliente.DataBind();
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

        protected void grdCliente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }

        protected void grdCliente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Adcionar"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow linha = grdCliente.Rows[index];
                Label lblId = (Label)linha.FindControl("lblId");

                var cliente = new ClienteControl().Buscar(Convert.ToInt16(lblId.Text));
                lblNomeCliente.Text = cliente.Nome;
                hdfIdCliente.Value = cliente.Id.ToString();

                popularGridView(new ClienteControl().BuscarTodosMenos(cliente.Id));
            }
        }

        protected void grdProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow linha = grdProjetos.Rows[index];
                Label lblId = (Label)linha.FindControl("lblId");

                var proj = new ProjetoControl().Buscar(Convert.ToInt16(lblId.Text));

                lblNomeCliente.Text = proj.Cliente.Nome;
                hdfIdCliente.Value = proj.Cliente.Id.ToString();
                txtNomeProjeto.Value = proj.Nome;
                txtDescricaoProjeto.Value = proj.Descricao;
                hdfIdProjeto.Value = proj.Id.ToString();
                MenuCadastro();
                btnCadastrarProjeto.Text = "Salvar";
            }
        }
        #endregion


    }
}