using Infra.Entidades;
using Infra.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            popularGridView(new ProjetoControl().BuscarTodos());
            grdProjetos.DataBind();
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {

        }

        protected void btnPesquisarCliente_Click(object sender, EventArgs e)
        {

        }

        protected void mnuPrincipal_MenuItemClick(object sender, MenuEventArgs e)
        {
            string value = ((Menu)sender).SelectedValue;

            switch (value)
            {
                //Cadastro de documento
                case "0": MenuReferencia();
                    break;
                //Consultar documento
                case "1": MenuConsulta();
                    popularGridView(new ProjetoControl().BuscarTodos());
                    grdProjetos.DataBind();
                    break;

            }
        }

        private void MenuReferencia()
        {
            MultiView1.ActiveViewIndex = 0;

            //CarregarComboDisciplina();

        }

        //Consulta de Documento de Engenharia
        private void MenuConsulta()
        {
            MultiView1.ActiveViewIndex = 1;
        }


        public void popularGridView(IList<Cliente> p)
        {
            DataTable dt = new DataTable();
            DataColumn column;
            DataRow row;
            DataView view;

            column = new DataColumn();
            column.ColumnName = "id";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "Nome do Projeto";
            dt.Columns.Add(column);

            foreach (Cliente cli in p)
            {
                row = dt.NewRow();
                row["id"] = cli.Id;
                row["Nome do Cliente"] = cli.Nome;
                dt.Rows.Add(row);
            }
            view = new DataView(dt);
            grdCliente.DataSource = view;
            grdCliente.DataBind();

        }


        public void popularGridView(IList<Projeto> p)
        {
            DataTable dt = new DataTable();
            DataColumn column;
            DataRow row;
            DataView view;

            column = new DataColumn();
            column.ColumnName = "id";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "Nome do Projeto";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "Nome do Cliente";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "Descrição do Projeto";
            dt.Columns.Add(column);

            foreach (Projeto proj in p)
            {
                row = dt.NewRow();
                row["id"] = proj.Id;
                row["Nome do Projeto"] = proj.Nome;
                row["Nome do Cliente"] = proj.Cliente.Nome;
                row["Descrição do Projeto"] = proj.Descricao;
                dt.Rows.Add(row);
            }
            view = new DataView(dt);
            grdProjetos.DataSource = view;
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



    }
}