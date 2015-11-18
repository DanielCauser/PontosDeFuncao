<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RelatorioDePrazo.aspx.cs" Inherits="ContagemPontosDeFuncao.Relatorios.RelatorioDePrazo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            Relátório de prazo atual dos projetos
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="form-group col-md-4 col-md-offset-1">
                    Quantidade de pessoas trabalhando no projeto:
                                <input class="form-control" id="txtQtdPessoas" runat="server">
                </div>
            </div>
            <div class="row">
                <div class="form-group col-md-4 col-md-offset-1">
                    <asp:Button ID="btnAbrirRelatorio" runat="server" Text="Gerar Relatório"
                        class="btn btn-primary" Width="150px" ToolTip="" OnClick="btnAbrirRelatorio_Click1" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>
