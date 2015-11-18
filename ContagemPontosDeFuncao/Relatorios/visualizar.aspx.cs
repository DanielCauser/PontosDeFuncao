using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContagemPontosDeFuncao.Relatorios
{
    public partial class visualizar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Abrir_Relatorio();

                }

                catch
                {

                }
            }
        }

        #region Abrir Relatório

        private void Abrir_Relatorio()
        {
            if (Request["rel"] != null)
            {

                string nmRelatorio;
                string rel = Request["rel"].ToString().ToUpper();
                string sql = string.Empty;
                string dataTable = string.Empty;
                string nmCritico = string.Empty;


                switch (rel)
                {
                    #region CUSTO POR PROJETO
                    case "REP_CUSTO":

                        sql = "SELECT [PF_TOTAL] ,[NOME_PROJETO] FROM [PontosFuncao].[dbo].[vw_PF_ATUAL] WHERE [PF_TOTAL] > 0";

                        dataTable = "vw_PF_ATUAL";
                        nmRelatorio = "\\Gerenciais\\rep_CUSTO.rdlc";

                        ConfiguracoesReport(nmCritico, sql, dataTable, nmRelatorio, "DsPrincipal");

                        break;

                    #endregion

                    #region prazo POR PROJETO
                    case "REP_PRAZO":

                        String FiltroConsulta = String.Empty;
                        string qtdPessoas = Request["qtdPessoas"].ToString();

                        if (!qtdPessoas.Equals(string.Empty))
                        {
                            int tmp;
                            if (int.TryParse(qtdPessoas.Trim(), out tmp))
                            {
                                sql = "SELECT ([PF_TOTAL] / " + qtdPessoas + ") as PF_TOTAL, [NOME_PROJETO] FROM [PontosFuncao].[dbo].[vw_PF_PRAZO] WHERE [PF_TOTAL] > 0";
                            }
                            else
                                sql = "SELECT [PF_TOTAL] ,[NOME_PROJETO] FROM [PontosFuncao].[dbo].[vw_PF_PRAZO] WHERE [PF_TOTAL] > 0";
                        }
                        else
                            sql = "SELECT [PF_TOTAL] ,[NOME_PROJETO] FROM [PontosFuncao].[dbo].[vw_PF_PRAZO] WHERE [PF_TOTAL] > 0";


                        dataTable = "vw_PF_PRAZO";
                        nmRelatorio = "\\Gerenciais\\rep_PRAZO.rdlc";

                        ConfiguracoesReport(nmCritico, sql, dataTable, nmRelatorio, "DsPrincipal");

                        break;

                    #endregion

                }
            }
        }

        private void ConfiguracoesReport(string nmCritico, string sql, string dataTable, string nmRelatorio, string nmDataSet)
        {

            LocalReport rep = ReportViewer1.LocalReport;
            string caminhoRpt = HttpContext.Current.Server.MapPath("~/");
            rep.ReportPath = caminhoRpt + "relatorios\\" + nmRelatorio;


            ReportDataSource dsRpt = new ReportDataSource();
            dsRpt.Name = nmDataSet;

            DataSet ds = ObterDados(sql, dataTable);
            dsRpt.Value = ds.Tables[0];

            rep.DataSources.Add(dsRpt);

        }


        private DataSet ObterDados(string sql, string dataTable)
        {
            try
            {

                DataSet ds = new DataSet();

                SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexaoPrincipal"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand(sql, conexao);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);


                adapter.Fill(ds, dataTable);

                return ds;


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
                return null;
            }
        }

        #endregion

    }
}