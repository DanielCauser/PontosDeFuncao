<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ContagemPontosDeFuncao._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
         <div class="col-md-2 col-md-offset-5"></div>
    </div>

    <div class="jumbotron">
        <h1>Objetivo</h1>
        <p class="lead">O presente trabalho tem por finalidade implementar um sistema, para auxiliar a análise de pontos de função em projetos de sistemas de informação, bem como, verificar indicadores de produtividade que possam melhorar os processos de desenvolvimento, utilizados nesses projetos.</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Descrição</h2>
            <p>
                Conforme comentado no objetivo e justificativa, a seguir, serão descritos os requisitos do sistema. A fim de facilitar a implementação e entendimento, o sistema está modularizado em duas grandes funcionalidades: Análise de APF e Produtividade.
            </p>
        </div>
        <div class="col-md-4">
            <h2>Módulo de análise de Pontos de Função.</h2>
            <p>
                O módulo de Análise de Pontos de Função (APF) contém dados inerentes do Projeto, Funcionalidades do Projeto, Cliente, Tipos de Ponto de Função e seus Níveis de Complexidade, Características Gerais e seus Níveis de Influência.
            </p>
        </div>
        <div class="col-md-4">
            <h2>Modulo de Produtividade</h2>
            <p>
                O módulo de Produtividade é um importante facilitador para auxiliar os gerentes de projetos, no processo de verificação e cálculo do histórico de custos e tempo de cada uma das unidades do projeto, assim como, do projeto propriamente dito.
            </p>
        </div>
    </div>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" Visible="true"/>
</asp:Content>
