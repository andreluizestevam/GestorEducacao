<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0400_CalculosAtualizacaoEstatistica.F0401_CalculoSalarioColabor.Cadastro" %>
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
    .imgliLnk { width: 15px; height: 13px; }    
    .divGrid { height: 308px; overflow-y:auto; width: 637px; border: 1px solid #D2DFD1;}    
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
        <span id="lblMsgGenric" style="margin-top: -3px;">Informe a parametrização e clique em PESQUISAR para a análise e cálculo de dados Gerencais/Estatísticos.</span>
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
                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnCalcSalario_Click">
                    <img title="Calcula o salário dos Funcionários."
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
        <li style="margin-left: 295px;">
            <label id="Label1" class="lblObrigatorio" title="Tipo de Unidade">
                Tipo de Unidade</label>
            <asp:DropDownList ID="ddlTpUnidade" ToolTip="Selecione o Tipo de Unidade" AutoPostBack="true" OnSelectedIndexChanged="ddlTpUnidade_SelectedIndexChanged"
                CssClass="ddlTpUnidade" runat="server">
            </asp:DropDownList>     
        </li>
        <li class="liUnidadeEscolar">
            <label id="Label5" class="lblObrigatorio" title="Unidade Escolar">
                Unidade Escolar</label>
            <asp:DropDownList ID="ddlUnidadeEscolar" ToolTip="Selecione a Unidade Escolar" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>    
        </li>    
        <li id="li3" clientidmode="Static" runat="server" title="Clique para Gerar Grid" class="liBtnAnaRecPag">            
            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="btnPesquisar_Click">
                <img class="imgliLnk" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa" title="Pesquisar" />
                <asp:Label runat="server" ID="Label3" Text="Pesquisar"></asp:Label>            
            </asp:LinkButton>
        </li>
        <li style="margin-left: 95px;" id="liGridCS" runat="server" clientidmode="Static" visible="false">
        <ul>
            <li style="margin-left: 278px;"><label style="text-transform:uppercase;">---------------------------------- TIPO CONTRATO ----------------------------------</label></li>
            <li id="liGrid">
                <div id="divGrid" runat="server" class="divGrid">
                    <asp:GridView ID="grdResulParam" CssClass="grdBusca" Width="620px" runat="server" AutoGenerateColumns="False">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum registro encontrado.<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="NO_FANTAS_EMP" HeaderText="Unidade Escolar">
                                <ItemStyle Width="350px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="COM" HeaderText="COM">
                                <ItemStyle HorizontalAlign="Right" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PJU" HeaderText="PJU">
                                <ItemStyle HorizontalAlign="Right" Width="40px" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="COO" HeaderText="COO">
                                <ItemStyle HorizontalAlign="Right" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EST" HeaderText="EST">
                                <ItemStyle HorizontalAlign="Right" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AUT" HeaderText="AUT">
                                <ItemStyle HorizontalAlign="Right" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EFE" HeaderText="EFE">
                                <ItemStyle HorizontalAlign="Right" Width="40px" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="CTR" HeaderText="CTR">
                                <ItemStyle HorizontalAlign="Right" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TOTAL" HeaderText="TOTAL">
                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
            <li style="width:267px;margin-right: 0px;">
                <label>Total</label>
            </li> 
            <li class="liTotais">
                <asp:Label ID="lblTotCOM" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:41px;">
                <asp:Label ID="lblTotPJU" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:43px;">
                <asp:Label ID="lblTotCOO" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:41px;">
                <asp:Label ID="lblTotEST" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:41px;">
                <asp:Label ID="lblTotAUT" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:41px;">
                <asp:Label ID="lblTotEFE" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:42px;">
                <asp:Label ID="lblTotCTR" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:51px;">
                <asp:Label ID="lblTotal" runat="server"></asp:Label>
            </li>
         </ul>
         </li>
         <li style="clear:none; margin-left: 20px; margin-right: 0px;" id="liLegCS" runat="server" clientidmode="Static" visible="false">
            <ul>
                <li style="background-color:#E0EEE0;width:125px;text-align:center;"><label>LEGENDA</label>
                </li>
                <li style="padding-left: 2px; color:#838B83;width: 130px;">
                    <label style="width:40px;float:left;">COM </label>
                    <label style="clear:none;"> Comissionado</label>
                    <label style="width:40px;float:left;">PJU </label>
                    <label style="clear:none;"> Pessoa Jurídica</label>
                    <label style="width:40px;float:left;">COO </label>
                    <label style="clear:none;"> Cooperado</label>
                    <label style="width:40px;float:left;">EST </label>
                    <label style="clear:none;"> Estagiário</label>
                    <label style="width:40px;float:left;">AUT </label>
                    <label style="clear:none;"> Autonomo</label>
                    <label style="width:40px;float:left;">EFE </label>
                    <label style="clear:none;">Efetivo</label>
                    <label style="width:40px;float:left;">CTR </label>
                    <label style="clear:none;"> Contrato</label>
                </li>
            </ul>
        </li>       
</ul>
<script type="text/javascript">
    $(document).ready(function () {
    });
</script>
</asp:Content>