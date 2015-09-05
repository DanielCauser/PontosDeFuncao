<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItensDeProjetoCad.aspx.cs" Inherits="ContagemPontosDeFuncao.Paginas.Cadastro.ItensDeProjeto.ItensDeProjetoCad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-md-12">
        <div class="form-group col-md-offset-5">
            <asp:Menu ID="mnuPrincipal" runat="server" CssClass="menu"
                Orientation="Horizontal"
                DynamicHorizontalOffset="2"
                StaticSubMenuIndent="10px" OnMenuItemClick="mnuPrincipal_MenuItemClick">

                <Items>
                    <asp:MenuItem Text=" Novo" ToolTip="Novo Projeto" Value="1" ImageUrl="~/Imagens/cadastrar.png"></asp:MenuItem>
                    <asp:MenuItem Text=" Consultar" ToolTip="Consultar Projeto" Value="0" ImageUrl="~/Imagens/Consultar_24.png"></asp:MenuItem>
                </Items>
                <StaticHoverStyle CssClass="staticHoverStyle" />
                <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                <StaticSelectedStyle CssClass="staticSelectedStyle" />
            </asp:Menu>
        </div>

        <div class="form-group">
            <asp:MultiView ID="MultiView1" runat="server">
                <!-- tab conteudo -->


                <!-- tab pesquisa -->
                <asp:View ID="pesquisa" runat="server">
                    <!-- tab pesquisa -->
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Pesquisar Funções de projeto
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                Nome do função do projeto:
                                <input class="form-control" id="txtPesquisaItemProjetoProjetoNome" runat="server">
                            </div>
                            <div class="form-group">
                                Nome do projeto:
                                <input class="form-control" id="txtPesquisaProjetoNome" runat="server">
                            </div>
                            <div class="form-group">
                                <asp:Button class="btn btn-default" ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                            </div>
                            <div class="form-group">
                                <h6 class="panel-title">Resultado: </h6>
                            </div>
                            <div class="form-group">
                                <asp:GridView runat="server" CssClass="table table-striped" ID="grdItemProjetos"
                                    AutoGenerateColumns="False"
                                    OnRowDataBound="grdItemProjetos_RowDataBound"
                                    OnRowCommand="grdItemProjetos_RowCommand">
                                    <Columns>

                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server"
                                                    Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Função do Projeto" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNome" runat="server"
                                                    Text='<%# Eval("Nome") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nome Projeto" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNomeProjeto" runat="server"
                                                    Text='<%# Eval("Projeto.Nome") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nome Cliente" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNomeCliente" runat="server"
                                                    Text='<%# Eval("Projeto.Cliente.Nome") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imbEditar" runat="server" CausesValidation="False" ImageUrl="~/Imagens/Editar_24.png"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    CommandName="Editar" Text="Editar" ToolTip="editar projeto" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                </asp:View>
                <!-- /tab pesquisa -->

                <!-- tab cadastro -->
                <asp:View ID="cadastro" runat="server">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h6 class="panel-title">Cadastro de item de projeto: </h6>
                        </div>
                        <div class="panel-body" id="frmProjetoDoItemDeProjeto">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h6 class="panel-title">Selecione o projeto que deseja adcionar itens: </h6>
                                </div>
                                <div class="panel-body">
                                    <asp:HiddenField ID="hdfIdItemProjeto" runat="server" />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="input-group">
                                                <input type="text" class="form-control" runat="server" id="txtNomeProjetoCadastroPesquisa">
                                                <span class="input-group-btn">
                                                    <asp:Button class="btn btn-default" ID="btnPesquisarProjeto" runat="server" Text="Pesquisar Projeto" OnClick="btnPesquisarProjeto_Click" />
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="panel-body">
                                            <asp:GridView runat="server" class="form-control" CssClass="table table-striped" ID="grdProjetos"
                                                AutoGenerateColumns="False"
                                                OnRowDataBound="grdProjetos_RowDataBound"
                                                OnRowCommand="grdProjetos_RowCommand">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblId" runat="server"
                                                                Text='<%# Eval("Id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imbAdcionar" runat="server" CausesValidation="False" ImageUrl="~/Imagens/Add_24.png"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                CommandName="Adcionar" Text="Adcionar" ToolTip="adcionar projeto" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Nome Projeto" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNomeProjeto" runat="server"
                                                                Text='<%# Eval("Nome") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Nome Cliente" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNomeCliente" runat="server"
                                                                Text='<%# Eval("Cliente.Nome") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body" id="frmInformacoesItemProjeto" runat="server" visible="false">
                            <!---->
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h6 class="panel-title">Informações do item do projeto: </h6>
                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="form-group col-md-8"> 
                                            <h2><asp:Label ID="lblAcaoItemProjeto" runat="server" /> funções para o projeto: </h2> 
                                            <h3><strong><asp:Label ID="lblNomeProjeto" runat="server" /></strong></h3>
                                            <asp:HiddenField ID="hdfIdProjeto" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            Nome da função do projeto: 
                                    <input class="form-control" id="txtNomeItemProjeto" runat="server">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            Descrição da função do projeto: 
                                <input class="form-control" id="txtDescricaoItemProjeto" runat="server">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="input-group">
                                                <asp:Button class="btn btn-primary" ID="btnCadastrarProjeto" runat="server" Text="Cadastrar" OnClick="btnCadastrarProjeto_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <!-- /tab cadastro -->
            </asp:MultiView>
        </div>
    </div>

</asp:Content>
