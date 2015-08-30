<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClienteCad.aspx.cs" Inherits="ContagemPontosDeFuncao.ClienteCad" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
                            Consultar Cliente
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                Nome do cliente:
                                <input class="form-control" id="txtNomeCliente" runat="server">
                            </div>
                            <div class="form-group">
                                Nome empresa:
                                <input class="form-control" id="txtNomeEmpresa" runat="server">
                            </div>
                            <div class="form-group">
                                E-mail:
                                <input class="form-control" id="txtEmail" runat="server">
                            </div>
                            <div class="form-group">
                                Telefone:
                                <input class="form-control" id="txtTelefone" runat="server">
                            </div>
                            <div class="form-group">
                                Tipo de documento:
                                <input class="form-control" id="Text1" runat="server">
                            </div>
                            <div class="form-group">
                                Numero documento:
                                <input class="form-control" id="txtNumeroDocumento" runat="server">
                            </div>

                            <div class="form-group">
                                <asp:Button class="btn btn-default" ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                            </div>
                            <div class="form-group">
                                <h6 class="panel-title">Resultado: </h6>
                            </div>
                            <div class="form-group">
                                <asp:GridView runat="server" CssClass="table table-striped" ID="grdProjetos"
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

                                        <asp:TemplateField HeaderText="Nome do Projeto" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNome" runat="server"
                                                    Text='<%# Eval("Nome") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nome do Cliente" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNomeCliente" runat="server"
                                                    Text='<%# Eval("Cliente.Nome") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Descrição do Projeto" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescricao" runat="server"
                                                    Text='<%# Eval("Descricao") %>'></asp:Label>
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
                            <h6 class="panel-title">Cadastro de Projetos: </h6>
                        </div>
                        <div class="panel-body">
                            <asp:HiddenField ID="hdfIdProjeto" runat="server" />
                            <div class="form-group">
                                Nome do cliente:
                                <input class="form-control" id="Text2" runat="server">
                            </div>
                            <div class="form-group">
                                Nome empresa:
                                <input class="form-control" id="Text3" runat="server">
                            </div>
                            <div class="form-group">
                                E-mail:
                                <input class="form-control" id="Text4" runat="server">
                            </div>
                            <div class="form-group">
                                Telefone:
                                <input class="form-control" id="Text5" runat="server">
                            </div>
                            <div class="form-group">
                                Tipo de documento:
                                <asp:DropDownList ID="ddlTipoDocumento" runat="server">
                                    <asp:ListItem Text="CPF" Value="CPF"/>
                                    <asp:ListItem Text="CNPJ" Value="CNPJ"/>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                Numero documento:
                                <input class="form-control" id="Text7" runat="server">
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="input-group">
                                        <input type="text" class="form-control" runat="server" id="txtNomeClienteCadastroPesquisa">
                                        <span class="input-group-btn">
                                            <asp:Button class="btn btn-default" ID="btnPesquisarCliente" runat="server" Text="Pesquisar Cliente" OnClick="btnPesquisarCliente_Click" />
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="panel-body">
                                    <asp:GridView runat="server" class="form-control" CssClass="table table-striped" ID="grdCliente"
                                        AutoGenerateColumns="False"
                                        OnRowDataBound="grdCliente_RowDataBound"
                                        OnRowCommand="grdCliente_RowCommand">
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
                                                        CommandName="Adcionar" Text="Adcionar" ToolTip="adcionar cliente a projeto" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nome Cliente" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNome" runat="server"
                                                        Text='<%# Eval("Nome") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                        </Columns>
                                    </asp:GridView>
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
                </asp:View>
                <!-- /tab cadastro -->
            </asp:MultiView>
        </div>
    </div>
</asp:Content>

