<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0400_CalculosAtualizacaoEstatistica.F0405_CalculoMediaPorSerie.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">      
    .ulDados { width: 985px;margin-top: 20px !important;}
    .ulDados input{ margin-bottom: 0;}
    .ulDados label {clear:none !important;}
    
    /*--> CSS LIs */
    .ulDados li{ margin-bottom: 5px; margin-right: 5px;clear:both;}   
    .liUnidadeEscolar {clear:none !important; display:inline !important;margin-left:10px;}    
    .liBtnAnaRecPag
    {
    	background-color: #FFFFE0;
        border: 1px solid #D2DFD1;     
        padding: 2px 3px 1px 3px;
        margin-left: 460px;
        margin-bottom: 15px !important;
    }
    .liTotais{ width:51px;clear:none !important;text-align:right;margin-right: 0px !important; }
    
    /*--> CSS DADOS */
    .ddlAnoRefer { width: 50px; }
    .imgliLnk { width: 15px; height: 13px; }
    .divGrid { height: 220px; overflow-y:auto; width: 677px; border: 1px solid #D2DFD1;}
    #divBarraPadraoContent{display:none;}
    #divBarraComoChegar { position:absolute; margin-left: 773px; margin-top:-30px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
    #divBarraComoChegar ul { display: inline; float: left; margin-left: 10px; }
    #divBarraComoChegar ul li { display: inline; margin-left: -2px; }
    #divBarraComoChegar ul li img { width: 19px; height: 19px; }  
    #helpMessages {display:none;}
    .helpMessages
    {
        margin-top: -5px;
        font-size: 11px !important;
    }
    
</style>
<!--[if IE]>
<style type="text/css">
       #divBarraComoChegar { width: 238px; margin-left: 760px; }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<div id="helpMessagesFC" class="helpMessages">
    <div id="divMensagGenerica" class="divMensagGenerica">
        <span ID="lblMsgGenric" style="margin-top: -3px;">Informe a parametrização e clique em PESQUISAR para a análise e cálculo de dados Gerencais/Estatísticos.</span>
    </div>
    <div id="divMensagCamposObrig" class="divMensagGenerica" style="margin-top: 2px;">
        <span>Para atualizar a BASE DE DADOS pressione o botão SALVAR (Disquete) existente na barra de comandos. </span>
    </div>
</div>
<div id="div1" class="bar" > 
        <div id="divBarraComoChegar" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
        <ul id="ulNavegacao" style="width: 39px;">
            <li id="btnVoltarPainel">
                <a href="javascript:parent.showHomePage()">
                    <img title="Clique para voltar ao Painel Inicial." 
                            alt="Icone Voltar ao Painel Inicial." 
                            src="/BarrasFerramentas/Icones/VoltarPainelGestor.png" />
                </a>
            </li>
            <li id="btnVoltar">
                <a href="javascript:BackToHome();">
                    <img title="Clique para voltar a Pagina Anterior."
                            alt="Icone Voltar a Pagina Anterior." 
                            src="/BarrasFerramentas/Icones/VoltarPaginaAnterior.png" />
                </a>
            </li>
        </ul>
        <ul id="ulEditarNovo" style="width: 39px;">
            <li id="btnEditar">
                <img title="Abre o registro atualmente selecionado em modo de edicao." alt="Icone Editar." src="/BarrasFerramentas/Icones/EditarRegistro_Desabilitado.png" />
            </li>
            <li id="btnNovo">
                <img title="Abre o formulario para Criar um Novo Registro."
                        alt="Icone de Criar Novo Registro." 
                        src="/BarrasFerramentas/Icones/NovoRegistro_Desabilitado.png" />
            </li>
        </ul>
        <ul id="ulGravar">
            <li>
                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnGravarMedias_Click">
                    <img title="Grava o registro atual na base de dados."
                            alt="Icone de Gravar o Registro." 
                            src="/BarrasFerramentas/Icones/Gravar.png" />
                </asp:LinkButton>
            </li>
        </ul>
        <ul id="ulExcluirCancelar" style="width: 39px;">
            <li id="btnExcluir">
                <img title="Exclui o Registro atual selecionado."
                        alt="Icone de Excluir o Registro." 
                        src="/BarrasFerramentas/Icones/Excluir_Desabilitado.png" />
            </li>
            <li id="btnCancelar">
                <a href='<%= Request.Url.AbsoluteUri %>'>
                    <img title="Cancela a Pesquisa atual e limpa o Formulário de Parâmetros de Pesquisa."
                            alt="Icone de Cancelar Operacao Atual." 
                            src="/BarrasFerramentas/Icones/Cancelar.png" />
                </a>
            </li>
        </ul>
        <ul id="ulAcoes" style="width: 39px;">
            <li  id="btnPesquisar">
                <img title="Realiza uma Pesquisa a partir dos Parametros de Pesquisa Informado."
                        alt="Icone de Pesquisa." 
                        src="/BarrasFerramentas/Icones/Pesquisar_Desabilitado.png" />
            </li>
            <li id="liImprimir">
                <img title="Formula um relatório a partir dos parâmetros especificados e o exibe em Modo de Impressão." 
                        alt="Icone de Impressao." 
                        src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />                    
            </li>
        </ul>
    </div>
</div>
<ul id="ulDados" class="ulDados">  
        <li style="margin-left: 183px;">
            <label id="Label1" class="lblObrigatorio" title="Tipo de Unidade">
                Tipo de Unidade</label>
            <asp:DropDownList ID="ddlTpUnidade" ToolTip="Selecione o Tipo de Unidade" AutoPostBack="true" OnSelectedIndexChanged="ddlTpUnidade_SelectedIndexChanged"
                CssClass="ddlTpUnidade" runat="server">
            </asp:DropDownList>     
        </li>
        <li class="liUnidadeEscolar">
            <label id="Label5" class="lblObrigatorio" title="Unidade Escolar">
                Unidade Escolar</label>
            <asp:DropDownList ID="ddlUnidadeEscolar" ToolTip="Selecione a Unidade Escolar" AutoPostBack="true" OnSelectedIndexChanged="ddlUnidadeEscolar_SelectedIndexChanged"
                CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>    
        </li>          
        <li class="liUnidadeEscolar">
            <label id="Label2" class="lblObrigatorio" title="Modalidade de Ensino">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade de Ensino" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                CssClass="ddlModalidade" runat="server">
            </asp:DropDownList>    
        </li>          
        <li class="liUnidadeEscolar">
            <label id="Label4" class="lblObrigatorio" title="Ano">
                Ano Letivo</label>
            <asp:DropDownList ID="ddlAnoRefer" ToolTip="Selecione o Ano de Referência" 
                CssClass="ddlAnoRefer" runat="server">
            </asp:DropDownList>    
        </li> 
        <li id="li3" clientidmode="Static" runat="server" title="Clique para Gerar Grid" class="liBtnAnaRecPag">            
            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="btnPesquisar_Click">
                <img class="imgliLnk" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa" title="Pesquisar" />
                <asp:Label runat="server" ID="Label3" Text="Pesquisar"></asp:Label>            
            </asp:LinkButton>
        </li>
        <li style="margin-left: 9px;" id="liGridMCFF" runat="server" clientidmode="Static" visible="false">
        <ul>
            <li style="margin-left: 335px;"><label style="text-transform:uppercase;">-------------------------------------- SÉRIES --------------------------------------</label></li>
            <li id="liGrid" style="margin-left: 70px;">
                <div id="divGrid" runat="server" class="divGrid">
                    <asp:GridView ID="grdResulParam" CssClass="grdBusca" Width="660px" runat="server" AutoGenerateColumns="False">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum registro encontrado.<br />
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </li>
            <li style="width:255px;margin-right: 0px;margin-left: 70px;">
                <label>Média Geral</label>
            </li> 
            <li class="liTotais">
                <asp:Label ID="lblTotT1" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:40px;">
                <asp:Label ID="lblTotT2" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:37px;">
                <asp:Label ID="lblTotT3" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:38px;">
                <asp:Label ID="lblTotT4" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:39px;">
                <asp:Label ID="lblTotT5" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:38px;">
                <asp:Label ID="lblTotT6" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:39px;">
                <asp:Label ID="lblTotT7" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:38px;">
                <asp:Label ID="lblTotT8" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:39px;">
                <asp:Label ID="lblTotT9" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:44px;">
                <asp:Label ID="lblTotMed" runat="server"></asp:Label>
            </li>
         </ul>
         </li>
         <li style="clear:none; margin-left: 20px; margin-right: 0px;" id="liLegMCFF" runat="server" clientidmode="Static" visible="false">
            <ul>
                <li style="background-color:#E0EEE0;width:125px;text-align:center;"><label>LEGENDA</label>
                </li>
                <li style="padding-left: 2px; color:#838B83;width: 130px;">
                    <asp:Label runat="server" id="lblSig1" style="width:40px;float:left;"/>
                    <asp:Label runat="server" id="lblM1" style="clear:none;width:83px;float:left;"/>
                    <asp:Label runat="server" id="lblSig2" style="width:40px;clear: both;float:left;"/>
                    <asp:Label runat="server" id="lblM2" style="clear:none;width:83px;float:left;"/>
                    <asp:Label runat="server" id="lblSig3" style="width:40px;clear: both;float:left;"/>
                    <asp:Label runat="server" id="lblM3" style="clear:none;width:83px;float:left;"/>
                    <asp:Label runat="server" id="lblSig4" style="width:40px;clear: both;float:left;"/>
                    <asp:Label runat="server" id="lblM4" style="clear:none;width:83px;float:left;"/>
                    <asp:Label runat="server" id="lblSig5" style="width:40px;clear: both;float:left;"/>
                    <asp:Label runat="server" id="lblM5" style="clear:none;width:83px;float:left;"/>
                    <asp:Label runat="server" id="lblSig6" style="width:40px;clear: both;float:left;"/>
                    <asp:Label runat="server" id="lblM6" style="clear:none;width:83px;float:left;"/>
                    <asp:Label runat="server" id="lblSig7" style="width:40px;clear: both;float:left;"/>
                    <asp:Label runat="server" id="lblM7" style="clear:none;width:83px;float:left;"/>
                    <asp:Label runat="server" id="lblSig8" style="width:40px;clear: both;float:left;"/>
                    <asp:Label runat="server" id="lblM8" style="clear:none;width:83px;float:left;"/>
                    <asp:Label runat="server" id="lblSig9" style="width:40px;clear: both;float:left;"/>
                    <asp:Label runat="server" id="lblM9" style="clear:none;width:83px;float:left;"/>
                </li>
            </ul>
        </li>        
</ul>
<script type="text/javascript">
    $(document).ready(function () {
    });
</script>
</asp:Content>