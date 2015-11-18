﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContagemPontosDeFuncao.Relatorios
{
    public partial class RelatorioDeCusto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAbrirRelatorio_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientScript", "<script type=\"text/javascript\">" +
                "abrirRelatorio('Visualizar.aspx?rel=rep_CUSTO');</script>");
        }
    }
}