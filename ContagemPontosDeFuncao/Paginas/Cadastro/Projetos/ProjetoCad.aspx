<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjetoCad.aspx.cs" Inherits="ContagemPontosDeFuncao.ProjetoCad" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Menu ID="mnuPrincipal" runat="server" CssClass="menu"
        Orientation="Horizontal"
        DynamicHorizontalOffset="2"
        StaticSubMenuIndent="10px" OnMenuItemClick="mnuPrincipal_MenuItemClick">

        <Items>
            <asp:MenuItem  Text=" Novo" ToolTip="Cadastrar Projeto" Value="1"></asp:MenuItem>
            <asp:MenuItem  Text=" Consultar" ToolTip="Consultar Projeto" Value="0"></asp:MenuItem>            
        </Items>
        <StaticHoverStyle CssClass="staticHoverStyle" />
        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
        <StaticSelectedStyle CssClass="staticSelectedStyle" />
    </asp:Menu>

    <asp:MultiView ID="MultiView1" runat="server">
        <!-- tab conteudo -->


        <!-- tab Cadastro -->
        <asp:View ID="pesquisa" runat="server">
            <!-- tab pesquisa -->
            <div class="panel panel-default">
                <asp:Button class="btn btn-default" ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                <div class="panel-heading">
                    <h6 class="panel-title">Resultado: </h6>
                </div>
                <div class="panel-body">
                    <asp:GridView runat="server" CssClass="table table-striped" ID="grdProjetos"
                        OnRowDataBound="grdProjetos_RowDataBound">
                        <Columns>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </asp:View>
        <!-- /tab pesquisa -->
        <asp:View ID="cadastro" runat="server">
            <div class="panel panel-default">

                <div class="panel-heading">
                    <h6 class="panel-title">Cadastro de Projetos: </h6>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="form-group col-md-6">
                            Nome do Projeto: 
                <input class="form-control" id="txtNomeProjeto">
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-6">
                            Descrição do projeto: 
                <input class="form-control" id="txtDescricaoProjeto">
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-6">
                            <div class="input-group">
                                <input type="text" class="form-control">
                                <span class="input-group-btn">
                                    <asp:Button class="btn btn-default" ID="btnPesquisarCliente" runat="server" Text="Pesquisar Cliente" OnClick="btnPesquisarCliente_Click" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <asp:GridView runat="server" class="form-control" CssClass="table table-striped" ID="grdCliente"
                                OnRowDataBound="grdCliente_RowDataBound">
                                <Columns>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>
        </asp:View>

    </asp:MultiView>
</asp:Content>
