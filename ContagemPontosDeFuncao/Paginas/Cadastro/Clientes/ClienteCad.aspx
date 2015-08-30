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
                                <input class="form-control" id="txtNomeClientePesquisa" runat="server">
                            </div>
                            <div class="form-group">
                                Nome empresa:
                                <input class="form-control" id="txtNomeEmpresaPesquisa" runat="server">
                            </div>

                            <div class="form-group">
                                <asp:Button class="btn btn-default" ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                            </div>
                            <div class="form-group">
                                <h6 class="panel-title">Resultado: </h6>
                            </div>
                            <div class="row">
                                <div class="panel-body">
                                    <asp:GridView runat="server" class="form-control" CssClass="table table-bordered" ID="grdCliente"
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

                                            <asp:TemplateField HeaderText="Nome Cliente" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNome" runat="server"
                                                        Text='<%# Eval("Nome") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nome Empresa" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpresa" runat="server"
                                                        Text='<%# Eval("Empresa") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="E-mail" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail" runat="server"
                                                        Text='<%# Eval("Email") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Telefone" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTelefone" runat="server"
                                                        Text='<%# Eval("Telefone") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tipo Documento" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipoDeRegistro" runat="server"
                                                        Text='<%# Eval("TipoDeRegistro") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Numero Documento" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRegistro" runat="server"
                                                        Text='<%# Eval("Registro") %>'></asp:Label>
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
                    </div>

                </asp:View>
                <!-- /tab pesquisa -->

                <!-- tab cadastro -->
                <asp:View ID="cadastro" runat="server">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h6 class="panel-title">Cadastro de Clientes: </h6>
                        </div>
                        <div class="panel-body">
                            <asp:HiddenField ID="hdfIdCliente" runat="server" />
                            <div class="form-group">
                                Nome do cliente:
                                <input class="form-control" id="txtNomeClienteCadastro" runat="server">
                            </div>
                            <div class="form-group">
                                Nome empresa:
                                <input class="form-control" id="txtNomeEmpresaCadastro" runat="server">
                            </div>
                            <div class="form-group">
                                E-mail:
                                <input class="form-control" id="txtEmailCadastro" runat="server">
                            </div>
                            <div class="form-group">
                                Telefone:
                                <input class="form-control" id="txtTelefoneCadastro" runat="server">
                            </div>
                            <div class="form-group dropdown">
                                Tipo de documento:
                                <asp:DropDownList ID="ddlTipoDocumento" runat="server">
                                    <asp:ListItem Text="CPF" Value="CPF"/>
                                    <asp:ListItem Text="CNPJ" Value="CNPJ"/>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                Numero documento:
                                <input class="form-control" id="txtNumeroDocumentoCadastro" runat="server">
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="input-group">
                                        <asp:Button class="btn btn-primary" ID="btnCadastrarCliente" runat="server" Text="Cadastrar" OnClick="btnCadastrarCliente_Click" />
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

