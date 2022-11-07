﻿<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5500_CtrlFluxoCaixa.F5501_GeraFluxoCaixa.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 370px;
        }
        
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
        }
        .liBtnAdd
        {
            clear: both;
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-left: 120px;
            margin-top: 12px;
            margin-bottom: 8px;
            margin-right: 8px !important;
            padding: 2px 3px 1px 3px;
        }
        
        /*--> CSS DADOS */
        .divGrid
        {
            width: 400px;
            overflow-y: auto;
            margin-top: 10px;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
        }
        #divBarraPadraoContent
        {
            display: none;
        }
        #divBarraComoChegar
        {
            position: absolute;
            margin-left: 750px;
            margin-top: -25px;
            border: 1px solid #CCC;
            overflow: auto;
            width: 230px;
            padding: 3px 0;
        }
        #divBarraComoChegar ul
        {
            display: inline;
            float: left;
            margin-left: 10px;
        }
        #divBarraComoChegar ul li
        {
            display: inline;
            margin-left: -2px;
        }
        #divBarraComoChegar ul li img
        {
            width: 19px;
            height: 19px;
        }
        .ddlUnidade { width:260px; }
        .check label{ display: inline; }
        .check input{ margin-left: -5px; }
        .fieldSet { border: 0px; }
        .fieldSet legend { font-size: 1.2em; }
        .liResumo { clear: both; margin-top: 10px; margin-left: 95px; }
        .liObser { clear: both; margin-top: 10px; }
    
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="div1" class="bar">
        <div id="divBarraComoChegar" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
            <ul id="ulNavegacao" style="width: 39px;">
                <li id="btnVoltarPainel"><a href="javascript:parent.showHomePage()">
                    <img title="Clique para voltar ao Painel Inicial." alt="Icone Voltar ao Painel Inicial."
                        src="/BarrasFerramentas/Icones/VoltarPainelGestor.png" />
                </a></li>
                <li id="btnVoltar"><a href="javascript:BackToHome();">
                    <img title="Clique para voltar a Pagina Anterior." alt="Icone Voltar a Pagina Anterior."
                        src="/BarrasFerramentas/Icones/VoltarPaginaAnterior.png" />
                </a></li>
            </ul>
            <ul id="ulEditarNovo" style="width: 39px;">
                <li id="btnEditar">
                    <img title="Abre o registro atualmente selecionado em modo de edicao." alt="Icone Editar."
                        src="/BarrasFerramentas/Icones/EditarRegistro_Desabilitado.png" />
                </li>
                <li id="btnNovo">
                    <img title="Abre o formulario para Criar um Novo Registro." alt="Icone de Criar Novo Registro."
                        src="/BarrasFerramentas/Icones/NovoRegistro_Desabilitado.png" />
                </li>
            </ul>
            <ul id="ulGravar">
                <li>
                    <img title="Grava o registro atual na base de dados." alt="Icone de Gravar o Registro."
                        src="/BarrasFerramentas/Icones/Gravar_Desabilitado.png" />
                </li>
            </ul>
            <ul id="ulExcluirCancelar" style="width: 39px;">
                <li id="btnExcluir">
                    <img title="Exclui o Registro atual selecionado." alt="Icone de Excluir o Registro."
                        src="/BarrasFerramentas/Icones/Excluir_Desabilitado.png" />
                </li>
                <li id="btnCancelar"><a href='<%= Request.Url.AbsoluteUri %>'>
                    <img title="Cancela a Pesquisa atual e limpa o Formulário de Parâmetros de Pesquisa."
                        alt="Icone de Cancelar Operacao Atual." src="/BarrasFerramentas/Icones/Cancelar.png" />
                </a></li>
            </ul>
            <ul id="ulAcoes" style="width: 39px;">
                <li id="btnPesquisar">
                    <img title="Realiza uma Pesquisa a partir dos Parametros de Pesquisa Informado."
                        alt="Icone de Pesquisa." src="/BarrasFerramentas/Icones/Pesquisar_Desabilitado.png" />
                </li>
                <li id="liImprimir">
                    <img title="Formula um relatório a partir dos parâmetros especificados e o exibe em Modo de Impressão."
                        alt="Icone de Impressao." src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />
                </li>
            </ul>
        </div>
    </div>
    <ul id="ulDados" class="ulDados">
        <li style="margin-left: 25px;">
            <label for="ddlUnidade" title="Unidade" class="lblObrigatorio">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade" CssClass="ddlUnidade"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlUnidade"
                ErrorMessage="A unidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear" style="margin-top: 10px; margin-left: 75px;">
            <label class="lblObrigatorio" for="txtPeriodo" title="Período">
                Período</label>
            <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" ToolTip="Informe a Data Inicial"
                runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
                ControlToValidate="txtDataPeriodoIni" Text="*" ErrorMessage="Campo Data Período Início é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            <asp:TextBox ID="txtDataPeriodoFim" ToolTip="Informe a Data Final" CssClass="campoData"
                runat="server"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="validatorField"
                ForeColor="Red" ControlToValidate="txtDataPeriodoFim" ControlToCompare="txtDataPeriodoIni"
                Type="Date" Operator="GreaterThanEqual" ErrorMessage="Data Final não pode ser menor que Data Inicial.">
            </asp:CompareValidator>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoFim" runat="server" CssClass="validatorField"
                ControlToValidate="txtDataPeriodoFim" Text="*" ErrorMessage="Campo Data Período Fim é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <fieldset class="fieldSet">
                <legend title="Títulos de CAR">Títulos de CAR</legend>
                <asp:CheckBoxList ID="rblTituloCAR" runat="server" RepeatDirection="Vertical" CssClass="check"
                    Width="200px" BorderStyle="None">
                    <asp:ListItem style="margin-top: 5px;" Value="A" Selected="True">Aberto</asp:ListItem>
                    <asp:ListItem Value="P" Selected="True">Parcialmente Quitado</asp:ListItem>
                    <asp:ListItem Value="Q" Selected="True">Quitado</asp:ListItem>
                </asp:CheckBoxList>
            </fieldset>
        </li>
        <li>
            <fieldset class="fieldSet">
                <legend title="Títulos de CAP">Títulos de CAP</legend>
                <asp:CheckBoxList ID="rblTituloCAP" runat="server" RepeatDirection="Vertical" CssClass="check"
                    Width="130px" BorderStyle="None">                    
                    <asp:ListItem Value="A" style="margin-top: 5px;" Selected="True">Aberto</asp:ListItem>
                    <asp:ListItem Value="P" Selected="True">Parcialmente Quitado</asp:ListItem>
                    <asp:ListItem Value="Q" Selected="True">Quitado</asp:ListItem>
                </asp:CheckBoxList>
            </fieldset>
        </li>
        <li id="Li1" runat="server" title="Gerar Fluxo de Caixa" class="liBtnAdd">
            <asp:LinkButton ID="btnGerar" runat="server" class="btnLabel" 
            OnClick="btnGerar_Click">GERAR FLUXO</asp:LinkButton>
        </li>
        <li class="liResumo" id="liResumo" runat="server" visible="false">
            <span style="font-size: 1.4em; font-weight: bold;">*** RESUMO ***</span>    
        </li>
        <li class="liClear">
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdResumo" CssClass="grdBusca" Width="320px" runat="server" 
                AutoGenerateColumns="False">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro gerado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="TipoTitulo" HeaderText="Descrição">
                            <ItemStyle Width="110px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="QtdTitulos" HeaderText="Qtd Tit">
                            <ItemStyle Width="40px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VlTotal" HeaderText="Total Títulos">
                            <ItemStyle Width="130px" HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
        <li class="liObser" id="liObser" runat="server" visible="false">
            <span style="font-size: 1.3em; font-weight: bold; color: #FF6347;">Observação: Fluxo de Caixa Gerado com Sucesso.</span>
        </li>
    </ul>
</asp:Content>
