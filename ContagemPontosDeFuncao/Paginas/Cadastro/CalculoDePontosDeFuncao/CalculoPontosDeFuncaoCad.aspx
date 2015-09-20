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
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        1° Selecione o tipo de pontos de função
                                                    </div>
                                                    <div class="form-group">
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
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        2° Selecione o nivel de complexidade do tipo de função selecionado
                                                    </div>
                                                    <div class="form-group">
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
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-10 col-md-offset-1">
                                                        <hr />
                                                        <h3>Itens selecionados: </h3>
                                                    </div>
                                                </div>
                                                <div class="form-group" visible="false" runat="server" id="rwPF1">
                                                    <div class="col-md-3 col-md-offset-3">
                                                        <asp:Label ID="LabelPF1" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldPFId1" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="LabelNC1" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldNCId1" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" visible="false">
                                                        <asp:ImageButton ID="btnPFExcluir1" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnPFExcluir1_Click" />
                                                    </div>
                                                </div>
                                                <div class="form-group" visible="false" runat="server" id="rwPF2">
                                                    <div class="col-md-3 col-md-offset-3">
                                                        <asp:Label ID="LabelPF2" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldPFId2" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="LabelNC2" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldNCId2" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:ImageButton ID="btnPFExcluir2" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnPFExcluir2_Click" />
                                                    </div>
                                                </div>
                                                <div class="form-group" visible="false" runat="server" id="rwPF3">
                                                    <div class="col-md-3 col-md-offset-3">
                                                        <asp:Label ID="LabelPF3" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldPFId3" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="LabelNC3" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldNCId3" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" visible="false">
                                                        <asp:ImageButton ID="btnPFExcluir3" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnPFExcluir3_Click" />
                                                    </div>
                                                </div>
                                                <div class="form-group" visible="false" runat="server" id="rwPF4">
                                                    <div class="col-md-3 col-md-offset-3">
                                                        <asp:Label ID="LabelPF4" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldPFId4" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="LabelNC4" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldNCId4" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:ImageButton ID="btnPFExcluir4" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnPFExcluir4_Click" />
                                                    </div>
                                                </div>
                                                <div class="form-group" visible="false" runat="server" id="rwPF5">
                                                    <div class="col-md-3 col-md-offset-3">
                                                        <asp:Label ID="LabelPF5" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldPFId5" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
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
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        1° Selecione a característica geral
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:GridView runat="server" CssClass="table table-striped" ID="grdCaracteristicasGerais"
                                                            AutoGenerateColumns="False"
                                                            OnRowCommand="grdCaracteristicasGerais_RowCommand">
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
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        2° Selecione o nível de influência
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:GridView runat="server" CssClass="table table-striped" ID="grdNivelDeInfluencia"
                                                            AutoGenerateColumns="False"
                                                            OnRowCommand="grdNivelDeInfluencia_RowCommand">
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
                                                <div class="row">
                                                    <div class="form-group">
                                                        <div class="col-md-10 col-md-offset-1">
                                                            <hr />
                                                            <h2>Itens selecionados: </h2>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" visible="false" runat="server" id="rwCG1">
                                                        <div class="col-md-2 col-md-offset-4">
                                                            <asp:Label ID="LabelCG1" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldCGId1" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Label ID="LabelNI1" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldNIId1" runat="server" />
                                                        </div>
                                                        <div class="col-md-1" visible="false">
                                                            <asp:ImageButton ID="btnCGExcluir1" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnCGExcluir1_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group" visible="false" runat="server" id="rwCG2">
                                                        <div class="col-md-2 col-md-offset-4">
                                                            <asp:Label ID="LabelCG2" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldCGId2" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Label ID="LabelNI2" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldNIId2" runat="server" />
                                                        </div>
                                                        <div class="col-md-1" visible="false">
                                                            <asp:ImageButton ID="btnCGExcluir2" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnCGExcluir2_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group" visible="false" runat="server" id="rwCG3">
                                                        <div class="col-md-2 col-md-offset-4">
                                                            <asp:Label ID="LabelCG3" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldCGId3" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Label ID="LabelNI3" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldNIId3" runat="server" />
                                                        </div>
                                                        <div class="col-md-1" visible="false">
                                                            <asp:ImageButton ID="btnCGExcluir3" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnCGExcluir3_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group" visible="false" runat="server" id="rwCG4">
                                                        <div class="col-md-2 col-md-offset-4">
                                                            <asp:Label ID="LabelCG4" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldCGId4" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Label ID="LabelNI4" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldNIId4" runat="server" />
                                                        </div>
                                                        <div class="col-md-1" visible="false">
                                                            <asp:ImageButton ID="btnCGExcluir4" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnCGExcluir4_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group" visible="false" runat="server" id="rwCG5">
                                                        <div class="col-md-2 col-md-offset-4">
                                                            <asp:Label ID="LabelCG5" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldCGId5" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Label ID="LabelNI5" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldNIId5" runat="server" />
                                                        </div>
                                                        <div class="col-md-1" visible="false">
                                                            <asp:ImageButton ID="btnCGExcluir5" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnCGExcluir5_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group" visible="false" runat="server" id="rwCG6">
                                                        <div class="col-md-2 col-md-offset-4">
                                                            <asp:Label ID="LabelCG6" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldCGId6" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Label ID="LabelNI6" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldNIId6" runat="server" />
                                                        </div>
                                                        <div class="col-md-1" visible="false">
                                                            <asp:ImageButton ID="btnCGExcluir6" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnCGExcluir6_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group" visible="false" runat="server" id="rwCG7">
                                                        <div class="col-md-2 col-md-offset-4">
                                                            <asp:Label ID="LabelCG7" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldCGId7" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Label ID="LabelNI7" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldNIId7" runat="server" />
                                                        </div>
                                                        <div class="col-md-1" visible="false">
                                                            <asp:ImageButton ID="btnCGExcluir7" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnCGExcluir7_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group" visible="false" runat="server" id="rwCG8">
                                                        <div class="col-md-2 col-md-offset-4">
                                                            <asp:Label ID="LabelCG8" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldCGId8" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Label ID="LabelNI8" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldNIId8" runat="server" />
                                                        </div>
                                                        <div class="col-md-1" visible="false">
                                                            <asp:ImageButton ID="btnCGExcluir8" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnCGExcluir8_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group" visible="false" runat="server" id="rwCG9">
                                                        <div class="col-md-2 col-md-offset-4">
                                                            <asp:Label ID="LabelCG9" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldCGId9" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Label ID="LabelNI9" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldNIId9" runat="server" />
                                                        </div>
                                                        <div class="col-md-1" visible="false">
                                                            <asp:ImageButton ID="btnCGExcluir9" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnCGExcluir9_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group" visible="false" runat="server" id="rwCG10">
                                                        <div class="col-md-2 col-md-offset-4">
                                                            <asp:Label ID="LabelCG10" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldCGId10" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Label ID="LabelNI10" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldNIId10" runat="server" />
                                                        </div>
                                                        <div class="col-md-1" visible="false">
                                                            <asp:ImageButton ID="btnCGExcluir10" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnCGExcluir10_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group" visible="false" runat="server" id="rwCG11">
                                                        <div class="col-md-2 col-md-offset-4">
                                                            <asp:Label ID="LabelCG11" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldCGId11" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Label ID="LabelNI11" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldNIId11" runat="server" />
                                                        </div>
                                                        <div class="col-md-1" visible="false">
                                                            <asp:ImageButton ID="btnCGExcluir11" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnCGExcluir11_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group" visible="false" runat="server" id="rwCG12">
                                                        <div class="col-md-2 col-md-offset-4">
                                                            <asp:Label ID="LabelCG12" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldCGId12" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Label ID="LabelNI12" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldNIId12" runat="server" />
                                                        </div>
                                                        <div class="col-md-1" visible="false">
                                                            <asp:ImageButton ID="btnCGExcluir12" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnCGExcluir12_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group" visible="false" runat="server" id="rwCG13">
                                                        <div class="col-md-2 col-md-offset-4">
                                                            <asp:Label ID="LabelCG13" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldCGId13" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Label ID="LabelNI13" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldNIId13" runat="server" />
                                                        </div>
                                                        <div class="col-md-1" visible="false">
                                                            <asp:ImageButton ID="btnCGExcluir13" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnCGExcluir13_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group" visible="false" runat="server" id="rwCG14">
                                                        <div class="col-md-2 col-md-offset-4">
                                                            <asp:Label ID="LabelCG14" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldCGId14" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Label ID="LabelNI14" runat="server" />
                                                            <asp:HiddenField ID="HiddenFieldNIId14" runat="server" />
                                                        </div>
                                                        <div class="col-md-1" visible="false">
                                                            <asp:ImageButton ID="btnCGExcluir14" runat="server" ImageUrl="~/Imagens/deletar_24.png" OnClick="btnCGExcluir14_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            Calcular pontos de função
                                        </div>
                                        <div class="panel-body">
                                            <div class="row  col-md-12">
                                                <div class="form-group col-md-4 col-md-offset-3">
                                                    <asp:Button class="btn btn-primary" ID="btnCalcularPontosDeFuncao" runat="server" Text="Calcular pontos de função" OnClick="btnCalcularPontosDeFuncao_Click" />
                                                </div>
                                            </div>
                                            <div class="row col-md-12">
                                                <div class="form-group col-md-8 col-md-offset-3">
                                                    <h3>
                                                        <asp:Label ID="lbltextoResultado" runat="server" Text="Pontos de função ajustados : " />
                                                        <asp:Label ID="lblPontosDeFuncaoAjustados" runat="server" />
                                                    </h3>
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
