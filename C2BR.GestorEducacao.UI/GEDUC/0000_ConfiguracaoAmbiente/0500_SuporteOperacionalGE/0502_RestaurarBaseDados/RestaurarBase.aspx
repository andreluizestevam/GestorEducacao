<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoGenericas.Master"
    AutoEventWireup="true" CodeBehind="RestaurarBase.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0502_RestaurarBaseDados.RestaurarBase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 400px; }
        
        /*--> CSS LIs */
        #divBarraRestaurarBaseDados ul li { display: inline; margin-left: -2px; }
        
        /*--> CSS DADOS */
        #divBarraRestaurarBaseDados { position:absolute; margin-left: 750px; margin-top:-40px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
        #divBarraRestaurarBaseDados ul { display: inline; float: left; margin-left: 10px; }        
        #divBarraRestaurarBaseDados ul li img { width: 19px; height: 19px; }
        
    </style>
<!--[if IE]>
<style type="text/css">
       #divBarraRestaurarBaseDados { width: 238px; }
</style>
<![endif]-->
    
<script src="/Library/JS/Grid.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="div1" class="bar" > 
            <div id="divBarraRestaurarBaseDados" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
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
                <li id="btnEditar" style="float:left;">
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
                    <img title="Grava o registro atual na base de dados."
                         alt="Icone de Gravar o Registro." 
                         src="/BarrasFerramentas/Icones/Gravar_Desabilitado.png" />
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
    <ul class="ulDados">
        <li>
            <asp:GridView ID="grdBackupsRBD" runat="server" CssClass="grdBusca" AutoGenerateColumns="False"
                DataKeyNames="ID" onrowcommand="grdBackupsRBD_RowCommand" PageSize="5" onrowdatabound="grdBackupsRBD_RowDataBound">
                <RowStyle CssClass="rowStyle" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhuma Cópia de Segurança encontrada para poder efetuar a restauração dos dados.<br />
                </EmptyDataTemplate>
                <PagerStyle CssClass="grdFooter" />
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Nome" />
                    <asp:BoundField DataField="Description" HeaderText="Descrição" />
                    <asp:BoundField DataField="BackupFinishDate" HeaderText="Data/Hora" />                    
                </Columns>
            </asp:GridView>
        </li>
    </ul>
    <div id="divButtonsBar" class="corner" style="text-align:center;">
        <ul id="ulButtons">
            <li id="liBtnAction">
                <asp:LinkButton runat="server" CssClass="btnActionRBD" ToolTip="Restaurar Base de Dados" OnClientClick="javascript:ExecuteBackup();return false;">
                    <img id="imgIconSaveRBD" width="20" height="20" src='/Library/IMG/Gestor_DBRestore.png' alt="Restaurar Base de Dados" />
                    <asp:Label runat="server" ID="lblBtnActionRBD" CssClass="lblBtnActionRBD" Text="Restaurar Base de Dados"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <asp:HiddenField runat="server" ID="hfSelectedRow" EnableViewState="false" Value="-1" />
    <script type="text/javascript">
        $('.alternatingRowStyle, .rowStyle').dblclick(function() {
            ExecuteBackup();
        });

        function ExecuteBackup() {
            if (!confirm("Todos os dados serão restaurados e todas as modificações feitas depois da data serão perdidas, \n\n Confirmar Restauração da Base de Dados?")) {
                return false;
            }
            __doPostBack('ctl00$content$grdBackupsRBD', 'Select$0');
        }
    </script>
</asp:Content>
