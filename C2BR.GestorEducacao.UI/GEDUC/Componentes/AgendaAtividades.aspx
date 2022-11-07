<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgendaAtividades.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.AgendaAtividades" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Agenda de Atividades</title>
<style type="text/css">
    #divAgendaAtividadesContainer { width: 680px; }
    #divAgendaAtividadesContainer a:hover { text-decoration: none; }
    #divAgendaAtividadesContainer #divAgendaAtividadesContent { height: 99px; border: 1px solid #E5E6E9; }
    #divAgendaAtividadesContainer #divAgendaAtividadesTitle { background-color: #667AB3; text-align: left; }
    #divAgendaAtividadesContainer #divAgendaAtividadesTitle h1 { color: #FFF; }
    #divAgendaAtividadesContainer #divAgendaAtividadesTitle #imgAgendaAtividades 
    { 
        float: left;
        margin: -3px 6px 0 0;
        width: 29px; 
    }
    #divAgendaAtividadesContainer #divAgendaAtividadesTitle img { margin: -2px 3px 0 }
    #divAgendaAtividadesContainer #divAgendaAtividadesTitle span { color: #EEEEEE; }
    
    #divAgendaAtividadesContent #divGridTarefas { height:80px; overflow-y:auto; width:100%; }
    #divAgendaAtividadesContent #divGridTarefas table { border: 0; width: 100%;}
    #divAgendaAtividadesContent #divGridTarefas th { font-size: 0.9em; text-transform: uppercase; color: #FFFFFF; background-color: #667AB3; font-weight: bold; padding: 3px; text-align: left;}
    #divAgendaAtividadesContent #divGridTarefas td { padding: 2px; }
    #divAgendaAtividadesContent #divGridTarefas table tr.tarefacadastradaRow td {color: #000000; }
    #divAgendaAtividadesContent #divGridTarefas table tr.emandamentoRow td {color: #1F1F8C !important; }
    #divAgendaAtividadesContent #divGridTarefas table tr.tarefaaceitaRow td {color: #000000 !important; text-align:left; }
    #divAgendaAtividadesContent #divGridTarefas table tr.atrasadaRow td {color: Red; }
    
    #divAgendaAtividadesContent #divAgendaAtividadesFooter { border-top: solid 1px #B1B1B1; padding: 2px 0 0; }
    #divAgendaAtividadesContent #divAgendaAtividadesFooter #ulFooterButtons { width: 75px; margin: 0 auto; }
    #divAgendaAtividadesContent #divAgendaAtividadesFooter #ulFooterButtons li { display: inline; }
    #divAgendaAtividadesContent #ulFooterButtons li span { color:#FF6600; }
    #divAgendaAtividadesContent #divAgendaAtividadesFooter #ulFooterButtons input { vertical-align: middle; width: 13px; height: 13px; }
    #divAgendaAtividadesContent #divAgendaAtividadesFooter #ulAvisosImportantes .liTarefasAgendadas {}
    #divAgendaAtividadesContent #divAgendaAtividadesFooter #ulAvisosImportantes .liTarefasAgendadasAlternatingRowStyle {}
    
    #divAgendaAtividadesContent #divGridTarefas .emptyDataRowStyle td
    {
        background: url("/Library/IMG/Gestor_ImgInformacao.png") no-repeat scroll 10px 20px #FFFFFF;
        padding: 0 10px 10px 45px !important;
        height: 70px;
    }
    
    #divIncluirEditar { float: left; font-size: 0.9em; }
    #divImprimirTarefas { float: right; font-size: 0.9em; }
    #divAgendaAtividadesFooter img { width: 17px; height: 17px; }
    .btnSubmitCalendarClick { display: none; }
    .displayNone { display: none; }
    .agendaAtividadesGridRow { height: 15px; }
    .tarefasAgendadasGridRow:hover { cursor: pointer; background-color: #DDDDDD; }
    #divAgendaAtividadesTitle ul { float: right; margin-top:5px; }
    #divAgendaAtividadesTitle ul li label { text-transform:none; display: inline; color: #FFF; }
    #divAgendaAtividadesTitle ul li input[type='text'] { width: 57px; text-align: right; }
    .ui-datepicker-trigger { width: 15px; height: 15px; float: none !important; }
    
    #divAgendaAtividadesContainer #contentHeader
    {
        background-color: #667AB3;
        width: 100%;
        height: 18px;
    }
    #divAgendaAtividadesContainer #contentHeader table { border: 0; margin: 0; padding: 0; width: 100%;}
    #divAgendaAtividadesContainer #contentHeader #thData { padding-left: 7px; width: 25px; }
    #divAgendaAtividadesContainer #contentHeader #thDescricao { width: 200px; }
    #divAgendaAtividadesContainer #contentHeader #thLimite { width: 25px; }
    #divAgendaAtividadesContainer #contentHeader #thPrioridade { width:25px; }
    #divAgendaAtividadesContainer #contentHeader #thSolicitante { width: 110px; }
    #divAgendaAtividadesContainer #contentHeader #thStatus { width: 55px; }
    #divAgendaAtividadesContainer #contentHeader th { text-align: left; }
    #divAgendaAtividadesContainer #contentHeader th span
    {
       color:#FFFFFF;
        font-family:Tahoma;
        font-size:1.1em;
        font-weight:bold;
        text-transform:uppercase;
    }    
    #divAgendaAtividadesContainer #divRodape
    {
        margin-top: 10px !important;
        float: right !important;
    }
    #divAgendaAtividadesContainer #imgLogoGestor
    {
        width: 127px;
        height: 30px;
        padding-right: 5px;
        margin-right: 5px;
    }
    #divHelpTxtAA
    {
        float: left;
        margin-top: 10px;
        width: 205px;
        color: #DF6B0D;
        font-weight: bold;
    }
    .pAcesso
    {
        font-size: 1.1em;
        color: #4169E1;
    }
    .pFechar
    {
        font-size: 0.9em;
        color: #FF6347;
        margin-top: 2px;
    }
</style>
</head>
<body id="bdyAgendaAtividades">
    <form id="frmAgendaAtividades" runat="server" class="frmAgendaAtividades">
    <div id="divAgendaAtividadesContainer">
        <div id="divAgendaAtividadesTitle" class="boxCornerTitle">
            <ul>
                <li>
                    <input type="text" name="txtAgendaAtividadesData" id="txtAgendaAtividadesData" />
                </li>
            </ul>
            <img id="imgAgendaAtividades" src="../Library/IMG/Gestor_ico_AgendaAtividades.png" alt="Icone Agenda de Atividades" />
            <h1>Agenda de Atividades</h1>
            <span>Atividade(s) a partir da data selecionada ao lado</span>
        </div>        
        <div id="divAgendaAtividadesContent">
            <div id="divGridTarefas">
                <asp:GridView runat="server" ID="grdAgendaAtividades" 
                                AutoGenerateColumns="False" 
                                GridLines="None" 
                                AllowPaging="False"
                                ShowFooter="False"
                                PageSize="4" 
                                DataKeyNames="CO_CHAVE_UNICA_TAREF, CO_IDENT_TAREF"
                                OnPreRender="grdAgendaAtividades_PreRender" 
                                OnRowDataBound="grdAgendaAtividades_RowDataBound" 
                                OnPageIndexChanging="grdAgendaAtividades_PageIndexChanging" 
                                OnRowCommand="grdAgendaAtividades_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="Data" ShowHeader="true" DataField="DT_COMPR_TAREF_AGEND" DataFormatString='{0:dd/MM/yy}' ItemStyle-Width="45px" />
                    <asp:BoundField HeaderText="Descrição" ShowHeader="true" DataField="NM_RESUM_TAREF_AGEND" ItemStyle-Width="362px" />
                    <asp:BoundField HeaderText="Limite" ShowHeader="true" DataField="DT_LIMIT_TAREF_AGEND" DataFormatString='{0:dd/MM/yy}' ItemStyle-Width="56px" />
                    <asp:BoundField HeaderText="Prioridade" ShowHeader="true" DataField="CO_PRIOR_TAREF_AGEND" ItemStyle-Width="114px" />
                    <asp:BoundField HeaderText="Solicitante" ShowHeader="true" DataField="NomeColaborador" ItemStyle-Width="180px" />
                    <asp:BoundField HeaderText="Status" ShowHeader="true" DataField="CO_SITU_TAREF_AGEND" ItemStyle-Width="80px" />
                </Columns>
                <%-- <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" />--%>
                <HeaderStyle CssClass="agendaAtividadesGridHeader" />      
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                        Nenhuma tarefa a partir da data atual selecionada no calendário, você pode procurar por tarefas alterando a data selecionada no calendário a cima.
                </EmptyDataTemplate>
                </asp:GridView>
            </div>
                <div id="divAgendaAtividadesFooter">
                    <div id="divImprimirTarefas">
                        <a id="lnkImprimirTarefa" title="Imprimir Relatório de Tarefas Agendadas" href="/GEDUC/1000_CtrlAdminEscolar/1300_ServicosApoioAdministrativo/1310_CtrlAgendaAtividadesFuncional/1319_Relatorios/RelHistoTarefAgendada.aspx?moduloNome=Emiss%C3%A3o%20do%20Hist%C3%B3rico%20de%20Tarefa%20Agendada">
                            Imprimir Tarefas <img alt="Imprimir Tarefa" src="/Library/IMG/Gestor_IcoImpres.ico" />
                        </a>
                    </div>
                    <div id="divIncluirEditar">
                        <a id="lnkIncluirAterarTarefa" title="Incluir/Editar Tarefas Agendadas" href="/GEDUC/1000_CtrlAdminEscolar/1300_ServicosApoioAdministrativo/1310_CtrlAgendaAtividadesFuncional/1311_RegistroAgendaAtividade/Busca.aspx?moduloNome=Cadastro%20de%20Tarefas%20Agendadas.">
                            <img alt="Incluir e Editar Tarefa" src="/Library/IMG/Gestor_ImgDocto.png" /> Incluir e Editar Tarefa
                        </a>
                    </div>
                </div>
                <asp:HiddenField runat="server" ID="HFDataSelecCalendario" OnValueChanged="HFDataSelecCalendario_ValueChanged" />
                <asp:Button runat="server" ID="btnSubmitCalendarClick" CssClass="btnSubmitCalendarClick" />                    
<div class="divModalTarefaDetails"></div>
<script type="text/javascript">

    $(document).ready(function () {
        defultAjaxSuccessActions();

        $("[id=txtAgendaAtividadesData]").datepicker({
            buttonImageOnly: true,
            buttonImage: '/Library/IMG/Gestor_IcoCalendario.gif',
            onSelect: function (dateText, inst) { $('[id=HFDataSelecCalendario]').val(dateText); ReloadAgendaAtividades(); }            
        });

        $("[id=txtAgendaAtividadesData]").datepicker("setDate", $('[id=HFDataSelecCalendario]').val());

        $('[id=lnkImprimirTarefa]').click(function (e) {
            $('#divContent').hide();
            $('#ifrmData').attr('src', $(this).attr('href'));
            $('#ifrmData').load(function () {
                $('#divLoadTelaFuncionalidade').show();

            });

            $("#divBreadCrumb").load("/Componentes/BreadCrumb.aspx?moduloId=299");

            $('#divLoginInfo').hide();

            // Previne a execução do link
            e.preventDefault();
            return false;
        });

        $('[id=lnkIncluirAterarTarefa]').click(function (e) {
            $('#divContent').hide();
            $('#ifrmData').attr('src', $(this).attr('href'));
            $('#ifrmData').load(function () {
                $('#divLoadTelaFuncionalidade').show();
            });

            $("#divBreadCrumb").load("/Componentes/BreadCrumb.aspx?moduloId=295");

            $('#divLoginInfo').hide();

            // Previne a execução do link
            e.preventDefault();
            return false;
        });

        $('[id=grdTarefasAgendadas]').click(function (e) {
            $("#divBreadCrumb").load("/Componentes/BreadCrumb.aspx?moduloId=295");

            $('#divLoginInfo').hide();
            // Previne a execução do link
            e.preventDefault();
            return false;
        });
    });

    function ReloadAgendaAtividades() {
        $("[id=frmAgendaAtividades]").ajaxSubmit({ target: '[id=divLoadShowAgendaAtividades]', url: '/Componentes/AgendaAtividades.aspx' })
    }
</script>
        </div>  
        <div id="divHelpTxtAA">
            <p id="pAcesso" class="pAcesso">
                Resultado da agenda de atividades.</p>
            <p id="pFechar" class="pFechar">
                Clique no X para fechar a janela.</p>
        </div>
        <div id="divRodape">
            <img id="imgLogoGestor" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
        </div>             
    </div>
    </form>
</body>
</html>
