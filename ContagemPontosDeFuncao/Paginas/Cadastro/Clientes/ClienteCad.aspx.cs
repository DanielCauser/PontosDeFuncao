using Infra.Entidades;
using Infra.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContagemPontosDeFuncao
{
    public partial class ClienteCad : Page
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
                    popularGridView(new ClienteControl().BuscarTodos());
                    grdCliente.DataBind();
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
            var clientes = new ClienteControl().Buscar(
                txtNomeClientePesquisa.Value.Trim(),
                txtNomeEmpresaPesquisa.Value.Trim());
            popularGridView(clientes);
        }

        protected void btnCadastrarCliente_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                try
                {
                    if (hdfIdCliente.Value.Equals(string.Empty))
                    {
                        var cliente = new Cliente();

                        cliente.Nome = txtNomeClienteCadastro.Value.Trim();
                        cliente.Empresa = txtNomeEmpresaCadastro.Value.Trim();
                        cliente.Email = txtEmailCadastro.Value.Trim();
                        cliente.Telefone = txtTelefoneCadastro.Value.Trim();
                        cliente.Registro= txtNumeroDocumentoCadastro.Value.Trim();
                        cliente.TipoDeRegistro = ddlTipoDocumento.SelectedValue.Trim();


                        new ClienteControl().Salvar(cliente);

                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientScript", "<script type=\"text/javascript\">alert('Cliente cadastrado com sucesso!');</script>");

                        LimparCampos();
                        grdCliente.DataSource = null;
                        popularGridView(new ClienteControl().BuscarTodos());
                        MenuConsulta();
                    }
                    else
                    {
                        var cliente = new Cliente();

                        cliente.Id = Convert.ToInt16(hdfIdCliente.Value);
                        cliente.Nome = txtNomeClienteCadastro.Value.Trim();
                        cliente.Empresa = txtNomeEmpresaCadastro.Value.Trim();
                        cliente.Email = txtEmailCadastro.Value.Trim();
                        cliente.Telefone = txtTelefoneCadastro.Value.Trim();
                        cliente.Registro = txtNumeroDocumentoCadastro.Value.Trim();
                        cliente.TipoDeRegistro = ddlTipoDocumento.SelectedValue.Trim();

                        new ClienteControl().Salvar(cliente);

                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientScript", "<script type=\"text/javascript\">alert('Cliente editado com sucesso!');</script>");

                        LimparCampos();
                        grdCliente.DataSource = null;
                        popularGridView(new ClienteControl().BuscarTodos());
                        MenuConsulta();
                        btnCadastrarCliente.Text = "Cadastrar";
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

            if (txtNomeClienteCadastro.Value.Equals(string.Empty))
                sbMsn.Append("Informe: Nome do cliente; ");

            if (txtNomeEmpresaCadastro.Value.Trim().Equals(string.Empty))
                sbMsn.Append("Informe: A Empresado cliente; ");

            if (txtEmailCadastro.Value.Trim().Equals(string.Empty))
                sbMsn.Append("Informe: O e-mail do cliente; ");

            if (txtTelefoneCadastro.Value.Trim().Equals(string.Empty))
                sbMsn.Append("Informe: O Telefone do cliente; ");

            if (ddlTipoDocumento.SelectedItem.Equals(string.Empty))
                sbMsn.Append("Informe: O tipo do documento do cliente; ");

            if (txtNumeroDocumentoCadastro.Value.Trim().Equals(string.Empty))
                sbMsn.Append("Informe: O numero do documento do cliente; ");

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
            txtNomeClienteCadastro.Value = string.Empty;
            txtNomeEmpresaCadastro.Value = string.Empty;
            txtEmailCadastro.Value = string.Empty;
            txtTelefoneCadastro.Value = string.Empty;
            txtNumeroDocumentoCadastro.Value = string.Empty;
            btnCadastrarCliente.Text = "Cadastrar";
        }

        #endregion

        #region GRIDS
        public void popularGridView(IList<Cliente> c)
        {
            grdCliente.DataSource = c;
            grdCliente.DataBind();
        }

        
        protected void grdCliente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }
        
        protected void grdCliente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow linha = grdCliente.Rows[index];
                Label lblId = (Label)linha.FindControl("lblId");

                var cliente = new ClienteControl().Buscar(Convert.ToInt16(lblId.Text));

                hdfIdCliente.Value = cliente.Id.ToString();
                txtNomeClienteCadastro.Value = cliente.Nome;
                txtNomeEmpresaCadastro.Value = cliente.Empresa;
                txtEmailCadastro.Value = cliente.Email;
                txtTelefoneCadastro.Value = cliente.Telefone;
                txtNumeroDocumentoCadastro.Value = cliente.Registro;
                ddlTipoDocumento.SelectedValue = cliente.TipoDeRegistro;
                btnCadastrarCliente.Text = "Salvar";

                MenuCadastro();
            }
        }
        #endregion


    }
}