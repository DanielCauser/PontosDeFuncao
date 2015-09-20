<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CalculoPontosDeFuncaoCad.aspx.cs" Inherits="ContagemPontosDeFuncao.Paginas.Cadastro.CalculoDePontosDeFuncao.CalculoPontosDeFuncaoCad" %>

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
            <!-- tab conteudo -->
            <asp:MultiView ID="MultiView1" runat="server">
                <!-- tab pesquisa -->
                <asp:View ID="pesquisa" runat="server">
                    <!-- tab CALCULO PONTOS DE FUNCAO -->
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Cálculo de pontos de função
                        </div>
                        <!-- tab PESQUISA ITEM DE PROJETO -->
                        <div class="panel-body">
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
                                                        <asp:ImageButton ID="ibmAdcionar" runat="server" CausesValidation="False" ImageUrl="~/Imagens/Add_24.png"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="Add" Text="Adcionar" ToolTip="selecionar item de projeto" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default" id="frmCalculoPontosFuncao" runat="server" visible="false">
                                <div class="panel-heading">
                                    <h2>Calcular pontos de função: </h2>
                                    <h3><strong>
                                        <asp:Label ID="lblNomeItemProjeto" runat="server" />
                                        do projeto
                                        <asp:Label ID="lblnomeProjeto" runat="server" /></strong></h3>
                                    <asp:HiddenField ID="hdfIdItemProjeto" runat="server" />
                                </div>
                                <div class="panel-body">
                                    <!-- tab tipos de funções e niveis de complexidade -->

                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            Selecionar tipos de funções e niveis de complexidade
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <!--OnRowDataBound="grdItemProjetos_RowDataBound"
                                                    -->
                                                <div class="col-md-6">
                                                    <asp:GridView runat="server" CssClass="table table-striped" ID="grdTipoPontoDeFuncao"
                                                        AutoGenerateColumns="False"
                                                        OnRowCommand="grdTipoPontoDeFuncao_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblId" runat="server"
                                                                        Text='<%# Eval("Id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Tipo de ponto de função" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNome" runat="server"
                                                                        Text='<%# Eval("Nome") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ibmAdcionar" runat="server" CausesValidation="False" ImageUrl="~/Imagens/Add_24.png"
                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                        CommandName="Add" Text="Adcionar" ToolTip="selecionar item de projeto" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:GridView runat="server" CssClass="table table-striped" ID="grdNivelDeComplexidade"
                                                        AutoGenerateColumns="False"
                                                        OnRowCommand="grdNivelDeComplexidade_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblId" runat="server"
                                                                        Text='<%# Eval("Id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Nível de complexidade" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNome" runat="server"
                                                                        Text='<%# Eval("Nome") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ibmAdcionar" runat="server" CausesValidation="False" ImageUrl="~/Imagens/Add_24.png"
                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                        CommandName="Add" Text="Adcionar" ToolTip="selecionar item de projeto" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="row">
                                                    <div class="col-md-10 col-md-offset-1">
                                                        <hr />
                                                        <h2>Itens selecionados: </h2>
                                                    </div>
                                                </div>
                                                <div class="row" visible="false" runat="server" id="rwPF1">
                                                    <div class="col-md-2 col-md-offset-4">
                                                        <asp:Label ID="LabelPF1" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldPFId1" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Label ID="LabelNC1" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldNCId1" runat="server" />
                                                    </div>
                                                    <div class="col-md-2" visible="false">
                                                        <asp:ImageButton ID="btnPFExcluir1" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnPFExcluir1_Click"/>
                                                    </div>
                                                </div>
                                                <div class="row" visible="false" runat="server" id="rwPF2">
                                                    <div class="col-md-2 col-md-offset-4" >
                                                        <asp:Label ID="LabelPF2" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldPFId2" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Label ID="LabelNC2" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldNCId2" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:ImageButton ID="btnPFExcluir2" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnPFExcluir2_Click"/>
                                                    </div>
                                                </div>
                                                <div class="row" visible="false" runat="server" id="rwPF3">
                                                    <div class="col-md-2 col-md-offset-4">
                                                        <asp:Label ID="LabelPF3" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldPFId3" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Label ID="LabelNC3" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldNCId3" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" visible="false">
                                                        <asp:ImageButton ID="btnPFExcluir3" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnPFExcluir3_Click" />
                                                    </div>
                                                </div>
                                                <div class="row" visible="false" runat="server" id="rwPF4">
                                                    <div class="col-md-2 col-md-offset-4">
                                                        <asp:Label ID="LabelPF4" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldPFId4" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Label ID="LabelNC4" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldNCId4" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:ImageButton ID="btnPFExcluir4" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnPFExcluir4_Click" />
                                                    </div>
                                                </div>
                                                <div class="row" visible="false" runat="server" id="rwPF5">
                                                    <div class="col-md-2 col-md-offset-4">
                                                        <asp:Label ID="LabelPF5" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldPFId5" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Label ID="LabelNC5" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldNCId5" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:ImageButton ID="btnPFExcluir5" ImageUrl="~/Imagens/deletar_24.png" runat="server" OnClick="btnPFExcluir5_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <!-- tab tipos de funções e niveis de complexidade -->
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            Selecionar características gerais e niveis de influência
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <!--OnRowDataBound="grdItemProjetos_RowDataBound"
                                                    OnRowCommand="grdItemProjetos_RowCommand"-->
                                                <div class="col-md-6">
                                                    <asp:GridView runat="server" CssClass="table table-striped" ID="grdCaracteristicasGerais"
                                                        AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblId" runat="server"
                                                                        Text='<%# Eval("Id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Caracteristicas gerais" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNome" runat="server"
                                                                        Text='<%# Eval("Nome") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ibmAdcionar" runat="server" CausesValidation="False" ImageUrl="~/Imagens/Add_24.png"
                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                        CommandName="Add" Text="Adcionar" ToolTip="selecionar item de projeto" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:GridView runat="server" CssClass="table table-striped" ID="grdNivelDeInfluencia"
                                                        AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblId" runat="server"
                                                                        Text='<%# Eval("Id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Nível de influência" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNome" runat="server"
                                                                        Text='<%# Eval("Nome") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ibmAdcionar" runat="server" CausesValidation="False" ImageUrl="~/Imagens/Add_24.png"
                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                        CommandName="Add" Text="Adcionar" ToolTip="selecionar item de projeto" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                </asp:View>
            </asp:MultiView>
        </div>



    </div>
</asp:Content>
