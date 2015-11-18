<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RelatorioDeCusto.aspx.cs" Inherits="ContagemPontosDeFuncao.Relatorios.RelatorioDeCusto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            Relátório de custo atual dos projetos
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="form-group col-md-4 col-md-offset-5">
                    <asp:Button ID="btnAbrirRelatorio" runat="server" Text="Gerar Relatório"
                        class="btn btn-primary" Width="150px" ToolTip="" OnClick="btnAbrirRelatorio_Click" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>
