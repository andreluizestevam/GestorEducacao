<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_RegistroConsulMedMod2.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .campoHora
        {
            font-weight:bold;
            display:inline;
            width: 30px;
        }
        .ulDados
        {
            width: 990px;
        }
        input
        {
            height: 13px !important;
        }
        .lblTitInf
        {
            text-transform: uppercase;
            font-weight: bold;
            font-size: 1.0em;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
            width: 100% !important;
            horizontal-align: center;
        }
        .lblTop
        {
            font-size: 9px;
            margin-bottom: 6px;
            color: #436EEE;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            padding: 2px 3px 1px 3px;
        }
        .divGridData
        {
            overflow-y: scroll;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: center;
        }
        .liTituloGrid
        {
            height: 15px;
            width: 100px;
            text-align: center;
            padding: 5 0 5 0;
            clear: both;
        }
        .lachk label
        {
            display: inline;
            margin-left: -3px;
        }
        .pAcesso
        {
            font-size: 1.1em;
            color: #4169E1;
        }
        .liBtnGrdFinan
        {
            margin-top: 10px;
            margin-left: 2px;
            padding: 4px 3px 3px;
            background-color: #EE9572;
            border: 1px solid #8B8989;
            width:10px;
        }
        .pFechar
        {
            font-size: 0.9em;
            color: #FF6347;
            margin-top: 2px;
        }
        #divListarResponsaveisContent
        {
            height: 261px;
            overflow-y: auto;
            border: 1px solid #EBF0FB;
        }
        .liFoto
        {
            float: left !important;
            margin-right: 0 !important;
        }
        .fldFotoAluno
        {
            border: none;
            width: 80px;
            height: 100px;
        }
        .divCarregando
        {
            width: 100%;
            text-align: center;
            position: absolute;
            z-index: 9999;
            left: 50px;
            top: 40%;
        }
        .chkLocais label
        {
            margin-left: -3px;
            display: inline !important;
        }
        .DivResp
        {
            float: left;
            width: 500px;
            height: 207px;
        }
        .chk label
        {
            display: inline;
        }
        .ulDadosResp li
        {
            margin-top: -2px;
            margin-left: 5px;
        }
        .ulDadosPaciente li
        {
            margin-left: 0px;
        }
        .ulDadosResp
        {
            float: left;
        }
        .ulIdentResp li
        {
            margin-left: 0px;
        }
        .ulInfosGerais
        {
            margin-top: -3px;
        }
        .ulInfosGerais li
        {
            margin: 1px 0 3px 0px;
        }
        .lblSubInfos
        {
            color: Orange;
            font-size: 8px;
        }
        .ulEndResiResp
        {
        }
        .ulEndResiResp li
        {
            margin-left: 2px;
        }
        .ulDadosContatosResp li
        {
            margin-left: 0px;
        }
        .liFotoColab
        {
            float: left !important;
            margin-right: 10px !important;
            border: 0 none;
        }
        /*--> CSS DADOS */
        .fldFotoColab
        {
            border: none;
            width: 90px;
            height: 108px;
        }
        .successMessageSMS
        {
            background: url(/Library/IMG/Gestor_ImgGood.png) no-repeat scroll center 10px;
            font-size: 15px;
            font-weight: bold;
            margin: 25% auto 13% auto;
            padding: 35px 10px 10px;
            text-align: center;
            width: 220px;
        }
        
        .divValidationSummary
        {
            cursor: move;
            padding: 10px;
            display: none;
            width: 210px;
            position: absolute;
            top: 35px;
            left: 0;
        }
        .divValidationSummary .divButtons
        {
            text-align: right;
        }
        .divValidationSummary .vsValidation
        {
            margin-left: 10px;
        }
        .divValidationSummary #divValidationSummaryContent
        {
            margin-bottom: 10px;
        }
        .divValidationSummary li
        {
            color: #666666;
            line-height: 11px;
            border-bottom: 1px solid #CFCFCF;
            list-style-type: circle;
            padding-bottom: 2px;
        }
          .divEsquePgto
        {
            border-right: 1px solid #CCCCCC;
            margin-top:15px;
            margin-right:20px !Important;
            width: 165px;
            height: 180px;
            float: left;
        }
        .divDirePgto
        {
            <%--border: 1px solid #CCCCCC;--%>;
            margin-top:15px;
            width: 550px;
            height: 180px;
            float: right;
        }
        .divGrdChequePgto
        {
            border: 1px solid #CCCCCC;
            width: 720px;
            height: 106px;
            overflow-y: scroll;
        }
        .lblchkPgto
        {
            font-weight:bold;
            color: #FFA07A;
            margin-left:-5px;
        }
        .ulReceb li 
        {
            margin-top:-6px;
        }
        .chk label
        {
            display:inline;
            margin-left:-4px;
        }
        .chkDestaque label
        {
            font-weight:bold;
            color: #FFA07A;
            margin-left:-4px;
            display:inline;
        }
        .sitPacPadrao
        {
            color:Blue;
            margin-left:1px;
        }
        .sitPacAnalise
        {
            font-weight:bold;
            color:Black;
            margin-left:1px;
        }
        .sitPacAlta
        {
            font-weight:bold;
            color:Red;
            margin-left:1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hidMultiAgend" />
    <asp:HiddenField runat="server" ID="hidEspelhoAgenda" />
    <ul id="ulDados" class="ulDados">
        <li style="margin-top: -5px;">
            <div id="divBotoes">
                <ul style="width: 1000px;">
                    <%-- <asp:UpdatePanel ID="updTopo" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>--%>
                    <li style="margin: 10px 0 4px 0;"><a class="lnkPesPaci" href="#">
                        <img class="imgPesPac" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade"
                            style="width: 17px; height: 17px;" /></a> </li>
                    <li style="margin-left: 5px;">
                        <label>
                            Nº PRONTUÁRIO</label>
                        <asp:CheckBox runat="server" ID="chkPesqNire" Style="margin-left: -5px"
                            OnCheckedChanged="chkPesqNire_OnCheckedChanged" AutoPostBack="true" />
                        <asp:TextBox runat="server" ID="txtNirePaci" CssClass="txtNireAluno" Width="50px"
                            Style="margin-left: -7px" Enabled="true"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            CPF</label>
                        <asp:CheckBox runat="server" ID="chkPesqCpf" Style="margin-left: -5px" OnCheckedChanged="chkPesqCpf_OnCheckedChanged"
                            AutoPostBack="true" />
                        <asp:TextBox runat="server" ID="txtCPFPaci" Width="75px" CssClass="campoCpf" Style="margin-left: -7px"
                            Enabled="false"></asp:TextBox>
                    </li>
                    <li style="margin-top: 12px; margin-left: -4px;">
                        <asp:ImageButton ID="imgCpfResp" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                            OnClick="imgCpfPac_OnClick" />
                    </li>
                    <li style="margin-left: 10px;">
                        <label>Unidade</label>
                        <asp:DropDownList ID="ddlLocalPaciente" Width="170px" runat="server"
                            OnSelectedIndexChanged="ddlLocalPaciente_OnSelectedIndexChanged" AutoPostBack="true" />
                    </li>
                    <li style="margin-left: 10px;">
                        <label>Profissional</label>
                        <asp:DropDownList ID="drpProfissional" Width="160px" runat="server"
                            OnSelectedIndexChanged="drpProfissional_OnSelectedIndexChanged" AutoPostBack="true" />
                    </li>
                    <li style="margin: 10px -3px 0 0;">
                        <asp:ImageButton ID="imgCadPac" runat="server" ImageUrl="~/Library/IMG/PGN_IconeTelaCadastro2.png"
                            OnClick="imgCadPac_OnClick" Style="width: 18px !important; height: 17px !important;"
                            ToolTip="Cadastro de Pacientes" />
                    </li>
                    <li style="margin-left: 8px">
                        <label for="ddlNomeUsuAteMed" title="Nome do usuário selecionado" class="lblObrigatorio">
                            Paciente</label>
                        <asp:DropDownList ID="ddlNomeUsu" runat="server" ToolTip="Paciente para o qual a consulta será marcada"
                            Width="230px" Visible="false" OnSelectedIndexChanged="ddlNomeUsu_OnSelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtNomePacPesq" Width="230px" ToolTip="Digite o nome ou parte do nome do paciente" runat="server" />
                    </li>
                    <li style="margin-top: 11px; margin-left: -4px;">
                        <asp:ImageButton ID="imgbPesqPacNome" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                            OnClick="imgbPesqPacNome_OnClick" />
                        <asp:ImageButton ID="imgbVoltarPesq" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                            OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" />
                    </li>
                    <li style="margin-left:-205px;">
                        <asp:Label ID="lblSitPaciente" Text=" - " runat="server" />
                    </li>
                    <%--<li>
                        <label for="txtNisUsu" title="Número NIS do usuário selecionado">
                            CNS/SUS</label>
                        <asp:TextBox ID="txtNisUsu" Enabled="false" runat="server" ToolTip="Número NIS do usuário selecionado"
                            Width="70px">
                        </asp:TextBox>
                    </li>--%>
                    <li style="margin-top: 13px;">
                        <label title="Atendimento de Cortesia (Sem Valor)">
                            <asp:CheckBox ID="chkCortesia" style="margin:0 -5px 0 -5px;" runat="server" ToolTip="Atendimento de Cortesia (Sem Valor)" />Cortesia
                        </label>
                    </li>
                    <li style="margin-top: 13px;">
                        <asp:CheckBox runat="server" ID="chkEnviaSms" Text="Enviar SMS"
                            ToolTip="Selecione para enviar um SMS com informações do agendamento ao Paciente automaticamente"
                            CssClass="chkLocais" Checked="false" />
                    </li>
                    <%--<li style="margin-top: -7px !important;">
                        <label for="txtRegAtend" title="Número do Registro">
                            Nº Registro</label>
                        <asp:TextBox ID="txtRegAtend" Enabled="false" CssClass="txtRegAteMed" runat="server"
                            ToolTip="Número do Registro" Text="2014.221.AT.0000001" Width="105px">
                        </asp:TextBox>
                    </li>--%>
                    <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </ul>
            </div>
        </li>
        <li class="liSeparador"></li>
        <li>
            <div id="tabResConsultas" runat="server" clientidmode="Static">
                <ul id="ul10">
                    <li style="margin-top: 10px !important; width: 1000px;">
                        <ul>
                            <li style="width: 460px; float: left; margin-left: 6px;">
                                <ul>
                                    <%--<li class="liTituloGrid" style="width: 420px; margin-right: 0px; background-color: #ffff99;">
                                        GRID DE PROFISSIONAIS </li>--%>
                                    <li class="liTituloGrid" style="width: 100%; height: 24px !important; margin-right: 40px !important;
                                        background-color: #E0EEEE; text-align: center; font-weight: bold; margin-bottom: 5px">
                                        <div style="float: left; margin-left: 10px !important;">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                                                HISTÓRICO DE AGENDAMENTOS DO PACIENTE</label>
                                        </div>
                                        <div style="float: right; margin-top: 5px">
                                            <ul>
                                                <li>
                                                    <asp:TextBox ID="txtDtIniHistoUsuar" runat="server" CssClass="campoData">
                                                    </asp:TextBox>
                                                    &nbsp até &nbsp
                                                    <asp:TextBox ID="txtDtFimHistoUsuar" runat="server" CssClass="campoData">
                                                    </asp:TextBox>
                                                </li>
                                            </ul>
                                        </div>
                                    </li>
                                    <li style="margin-top: 0px">
                                        <label for="ddlUnidResCons" title="Pesquise pela unidade da consulta do paciente">
                                            Unidade</label>
                                        <asp:DropDownList ID="ddlUnidHisPaciente" Width="210px" runat="server" ToolTip="Pesquise pela unidade da consulta do paciente"
                                            OnSelectedIndexChanged="ddlUnidHisPaciente_OnSelectedIndexChanged" AutoPostBack="true" />
                                    </li>
                                    <li>
                                        <label>
                                            Local</label>
                                        <asp:DropDownList runat="server" ID="drpLocalCons" Width="85px">
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <label>
                                            Tipo Agendamento</label>
                                        <asp:DropDownList runat="server" ID="ddlTipoAgendHistPaciente" Width="130px">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: 12px; margin-left: 0px;">
                                        <asp:ImageButton ID="imgPesqHistPaciente" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                            OnClick="imgPesqHistPaciente_OnClick" />
                                    </li>
                                    <li style="margin-top: 5px">
                                        <div id="divHistoricoAgenda" class="divGridData" style="height: 132px; width: 458px;
                                            overflow-y: scroll !important; border: 1px solid #ccc;">
                                            <input type="hidden" id="divHistoricoAgenda_posicao" name="divHistoricoAgenda_posicao" />
                                            <asp:GridView ID="grdHistorPaciente" CssClass="grdBusca" runat="server" Style="width: 100%;"
                                                AutoGenerateColumns="false" ToolTip="Grade de histórico de agendamentos do(a) Paciente selecionado">
                                                <RowStyle CssClass="rowStyle" />
                                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                                <EmptyDataTemplate>
                                                    Nenhum registro encontrado.<br />
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField DataField="DT_HORAR" HeaderText="DATA/HORA">
                                                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LOCAL" HeaderText="LOCAL">
                                                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TP_PROCED" HeaderText="TP PROC">
                                                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="APELIDO_PROFISSIONAL" HeaderText="PROF.">
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TP_CONTRATO" HeaderText="TP CONTR">
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="STATUS_V" HeaderText="STATUS">
                                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                            <li style="width: 500px; float: right; margin-right: 20px !important;">
                                <ul>
                                    <%--<li class="liTituloGrid" style="width: 420px; margin-right: 0px; background-color: #ffff99;">
                                        GRID DE PROFISSIONAIS </li>--%>
                                    <li class="liTituloGrid" style="width: 100%; height: 24px !important; margin-right: 40px !important;
                                        background-color: #ffff99; text-align: center; font-weight: bold; margin-bottom: 5px">
                                        <div style="float: left; margin-left: 10px !important;">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                                                GRID DE PROFISSIONAIS</label>
                                        </div>
                                        <div style="float: left; margin-left: 240px !important; text-align:center; font-family: Tahoma; font-weight: bold;margin-top:5px;">
                                            <a class="lnkPesPaci" href="../../../8000_GestaoAtendimento/8200_CtrlClinicas/8250_RecepcaoEncaminhamento/Cadastro.aspx?moduloId=6644&moduloNome=Recepção%20-%20Unificada%20de%20Atendimento%20de%20Pacientes%20/%20Avaliação%20e%20Procedimentos%20*&moduloId=6644">Ir para tela Recepção</a>
                                        </div>

                                        <div style="float: right; margin-top: 5px">
                                            <ul>
                                            </ul>
                                        </div>
                                    </li>
                                    <li style="margin-top: 0px">
                                        <label for="ddlUnidResCons" title="Selecione a unidade do médico">
                                            Unidade</label>
                                        <asp:DropDownList ID="ddlUnidResCons" Width="185px" runat="server" ToolTip="Selecione a unidade do médico"
                                            OnSelectedIndexChanged="ddlUnidResCons_OnSelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: 0px; margin-bottom: 5px">
                                        <label>
                                            Local</label>
                                        <asp:DropDownList runat="server" ID="ddlDept" Width="105px">
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <label title="Selecione a Classificação Profissional para filtrar os Profissionais">
                                            Classif. Profi.
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlClassProfi" ToolTip="Selecione a Classificação Profissional para filtrar os Profissionais">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: 12px; margin-left: 0px;">
                                        <asp:ImageButton ID="imgPesqProfissionais" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                            OnClick="imgPesqProfissionais_OnClick" />
                                    </li>
                                    <li style="margin-top: 0px">
                                        <div id="divProfi" class="divGridData" style="height: 132px; width: 500px; overflow-y: scroll !important;
                                            border: 1px solid #ccc;">
                                            <input type="hidden" id="divProfi_posicao" name="divProfi_posicao" />
                                            <asp:HiddenField ID="hidCoColSelec" Value="0" runat="server" />
                                            <asp:GridView ID="grdProfi" CssClass="grdBusca" runat="server" Style="width: 100%;"
                                                AutoGenerateColumns="false">
                                                <RowStyle CssClass="rowStyle" />
                                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                                <EmptyDataTemplate>
                                                    Nenhum registro encontrado.<br />
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="CK">
                                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hidCoCol" Value='<%# Eval("CO_COL") %>' runat="server" />
                                                            <asp:CheckBox ID="ckSelect" OnCheckedChanged="ckSelect_CheckedChangedP" AutoPostBack="true"
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NO_COL" HeaderText="PROFISSIONAL DE SAÚDE">
                                                        <ItemStyle Width="223px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DE_CLASS_FUNC" HeaderText="CLASS FUNC">
                                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NO_EMP" HeaderText="UNID">
                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NU_TEL_V" HeaderText="CELULAR">
                                                        <ItemStyle Width="66px" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                            <li style="width: 980px; margin: 14px 0 0 7px !important; clear: both;">
                                <ul>
                                    <%--  <li class="liTituloGrid" style="width: 100%; margin-right: 0px; background-color: #d2ffc2;">
                                        AGENDA DE HORÁRIOS </li>--%>
                                    <li class="liTituloGrid" style="width: 100%; height: 24px !important; margin-right: 0px;
                                        background-color: #bde5ae; text-align: center; font-weight: bold; margin-bottom: 0px">
                                        <div style="float: left; margin-left: 10px;">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                                                AGENDA DE HORÁRIOS</label>
                                        </div>
                                        <div style="float: right; margin-right: 10px; margin-top: 3px;">
                                            <label id="lblMsgErro" style="font-size: 13px; color: #B22222; font-weight: bold;
                                                display: none;">
                                                Usuário sem permissão para agendamento de mais um paciente no mesmo horário.</label>
                                        </div>
                                    </li>
                                    <li class="liTituloGrid" style="width: 100%; height: 24px !important; margin-right: 0px;
                                        background-color: #d2ffc2; text-align: center; font-weight: bold; margin-bottom: 8px">
                                        <div style="float: left; margin-left: 5px; margin-top: 4px;">
                                            <ul>
                                                <li>
                                                    <asp:Label ID="Label1" ToolTip="Classificação Agendamento" runat="server">Class</asp:Label>
                                                    <asp:DropDownList runat="server" ID="ddlClassFunci" OnSelectedIndexChanged="ddlClassFunci_OnSelectedIndexChanged"
                                                        AutoPostBack="true" Width="85px" ToolTip="Classificação Agendamento">
                                                    </asp:DropDownList>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label2" ToolTip="Tipo de Agendamento" runat="server">Tipo</asp:Label>
                                                    <asp:DropDownList runat="server" Width="59px" ID="ddlTipoAg" OnSelectedIndexChanged="ddlTipoAg_OnSelectedIndexChanged"
                                                        AutoPostBack="true" ToolTip="Tipo de Agendamento">
                                                    </asp:DropDownList>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label3" ToolTip="Tipo de Contratação" runat="server">Cont</asp:Label>
                                                    <asp:DropDownList runat="server" Width="65px" ID="ddlOpers" OnSelectedIndexChanged="ddlOpers_OnSelectedIndexChanged" ToolTip="Tipo de Contratação"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </li>
                                            </ul>
                                        </div>
                                        <div style="float: right; margin-top: 4px;">
                                            <ul>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkDom" Text="Do" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo os Domingos" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkSeg" Text="Se" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Segundas"
                                                        Checked="true" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkTer" Text="Te" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Terças"
                                                        Checked="true" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkQua" Text="Qa" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Quartas"
                                                        Checked="true" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkQui" Text="Qi" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Quintas"
                                                        Checked="true" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkSex" Text="Sx" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Sextas"
                                                        Checked="true" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkSab" Text="Sb" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo os Sábados"
                                                        Checked="true" />
                                                </li>
                                                <li>
                                                    <asp:TextBox ID="txtDtIniResCons" runat="server" CssClass="campoData">
                                                    </asp:TextBox>
                                                    &nbsp até &nbsp
                                                    <asp:TextBox ID="txtDtFimResCons" runat="server" CssClass="campoData">
                                                    </asp:TextBox>
                                                </li>
                                                <li>
                                                    <asp:TextBox ID="txtHrIni" runat="server" ToolTip="Informe a hora de início" CssClass="campoHora">
                                                    </asp:TextBox>
                                                    &nbsp até &nbsp
                                                    <asp:TextBox ID="txtHrFim" runat="server" ToolTip="Informe a hora de término" CssClass="campoHora">
                                                    </asp:TextBox>
                                                </li>
                                                <li style="margin-top: 1px">
                                                    <asp:CheckBox ID="chkHorDispResCons" runat="server" Text="Hrs. Disponíveis" CssClass="lachk" />
                                                </li>
                                                <li style="margin-top: 1px; margin-left: -1px;">
                                                    <asp:ImageButton ID="imgPesqGridAgenda" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                        OnClick="imgPesqGridAgenda_OnClick" />
                                                </li>
                                            </ul>
                                        </div>
                                    </li>
                                    <li style="margin-top: 1px">
                                        <%-- <asp:CheckBox ID="chkMarcaTodosItens" runat="server" OnCheckedChanged="ChkTodos_OnCheckedChanged"
                                            AutoPostBack="true"></asp:CheckBox>--%>
                                        <div id="divAgenda" class="divGridData" style="height: 120px; width: 980px; border: 1px solid #ccc;">
                                            <input type="hidden" id="divAgenda_posicao" name="divAgenda_posicao" />
                                            <asp:HiddenField runat="server" ID="hidCoConsul" />
                                            <asp:GridView ID="grdHorario" CssClass="grdBusca" runat="server" AutoGenerateColumns="false" DataKeyNames="CO_AGEND"
                                                Style="width: 100%;" OnRowDataBound="grdHorario_OnRowDataBound">
                                                <RowStyle CssClass="rowStyle" />
                                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                                <EmptyDataTemplate>
                                                    Nenhum registro encontrado.<br />
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="CK">
                                                        <ItemStyle Width="5px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkMarcaTodosItens" CssClass="chk" Style="" runat="server" OnCheckedChanged="ChkTodos_OnCheckedChanged"
                                                                AutoPostBack="true"></asp:CheckBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckSelectHr" CssClass="chk" runat="server" OnCheckedChanged="ckSelectHr_OnCheckedChanged"
                                                                AutoPostBack="true" />
                                                            <asp:HiddenField runat="server" ID="hidCoAgenda" Value='<%# Eval("CO_AGEND") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# Eval("CO_ALU") %>' />
                                                            <asp:HiddenField runat="server" ID="hidTpCons" Value='<%# Eval("TP_CONSUL") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoCol" Value='<%# Eval("CO_COL") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoEspec" Value='<%# Eval("CO_ESPEC") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoDepto" Value='<%# Eval("CO_DEPTO") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoEmp" Value='<%# Eval("CO_EMP") %>' />
                                                            <asp:HiddenField runat="server" ID="hidDataHora" Value='<%# Eval("hora") %>' />
                                                            <asp:HiddenField runat="server" ID="hidData" Value='<%# Eval("dt") %>' />
                                                            <asp:HiddenField runat="server" ID="hidHora" Value='<%# Eval("hr") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="hora" HeaderText="DATA E HORA">
                                                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NO_PAC" HeaderText="nome do paciente">
                                                        <ItemStyle Width="140px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="CLASS FUNCIO">
                                                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField runat="server" ID="hidTpAgend" Value='<%# Eval("CO_TP_AGEND") %>' />
                                                            <asp:DropDownList runat="server" ID="ddlTipoAgendam" Width="100%" Style="margin-left: -4px; margin-bottom:0px;">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TIPO">
                                                        <ItemStyle Width="75px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField runat="server" ID="hidTpConsul" Value='<%# Eval("CO_TP_CONSUL") %>' />
                                                            <asp:DropDownList runat="server" ID="ddlTipo" Width="100%" Style="margin-left: -4px; margin-bottom:0px;"  OnSelectedIndexChanged="ddlTipo_OnSelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="CLASS TIPO">
                                                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField runat="server" ID="hidClasTpConsulta" Value='<%# Eval("CO_CLASS_TP") %>' />
                                                            <asp:DropDownList runat="server" ID="ddlClasTipoConsulta" Width="100%" Style="margin-left: -4px; margin-bottom:0px;">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="CONTRATAÇÃO"><ItemStyle Width="40px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField runat="server" ID="hidIdOper" Value='<%# Eval("CO_OPER") %>' />
                                                            <asp:DropDownList runat="server" ID="ddlOperAgend" OnSelectedIndexChanged="ddlOperAgend_OnSelectedIndexChanged"
                                                                AutoPostBack="true" Width="100%" Style="margin-left: -4px; margin-bottom:0px;">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PLANO">
                                                        <ItemStyle Width="45px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField runat="server" ID="hidIdPlan" Value='<%# Eval("CO_PLAN") %>' />
                                                            <asp:DropDownList runat="server" ID="ddlPlanoAgend" Width="100%" Style="margin-left: -4px; margin-bottom:0px;">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PROCED">
                                                        <ItemStyle Width="65px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField runat="server" ID="hidIdProced" Value='<%# Eval("ID_PROC") %>' />
                                                            <asp:DropDownList runat="server" ID="ddlProcedAgend" OnSelectedIndexChanged="ddlProcedAgend_OnSelectedIndexChanged"
                                                                AutoPostBack="true" Width="100%" Style="margin-left: -4px; margin-bottom:0px;">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="VALOR">
                                                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtValorAgend" Text='<%# Eval("VL_CONSUL") %>' CssClass="campoMoeda" Width="100%" Style="margin-left: -4px; margin-bottom:0px;"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CO_SITU_VALID" HeaderText="STATUS">
                                                        <ItemStyle Width="40px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divLoadShowAlunos" style="display: none; height: 305px !important;" />
        </li>
        <li>
            <div id="divLoadShowResponsaveis" style="display: none; height: 335px !important;" />
        </li>
        <li>
            <div id="divLoadInfosCadas" style="display: none; height: 350px !important;">
                <ul class="ulDados" style="width: 400px !important;">
                    <div class="DivResp" runat="server" id="divResp">
                        <ul class="ulDadosResp" style="margin-left: -100px !important; width: 600px !important;">
                            <li style="margin: 0 0 -3px 0px">
                                <label class="lblTop">
                                    DADOS DO RESPONSÁVEL PELO PACIENTE</label>
                            </li>
                            <li style="clear: both; margin: 9px -1px 0 0px;"><a class="lnkPesResp" href="#">
                                <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade"
                                    style="width: 17px; height: 17px;" /></a> </li>
                            <li>
                                <label class="lblObrigatorio">
                                    CPF</label>
                                <asp:TextBox runat="server" ID="txtCPFResp" Style="width: 74px;" CssClass="campoCpf"
                                    ToolTip="CPF do Responsável"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hidCoResp" />
                            </li>
                            <li style="margin-top: 10px; margin-left: 0px;">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                    Width="13px" Height="13px" OnClick="imgCpfResp_OnClick" />
                            </li>
                            <li>
                                <label class="lblObrigatorio">
                                    Nome</label>
                                <asp:TextBox runat="server" ID="txtNomeResp" Width="216px" ToolTip="Nome do Responsável"></asp:TextBox>
                            </li>
                            <li>
                                <label>
                                    Sexo</label>
                                <asp:DropDownList runat="server" ID="ddlSexResp" Width="44px" ToolTip="Selecione o Sexo do Responsável">
                                    <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                                    <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li>
                                <label class="lblObrigatorio">
                                    Nascimento</label>
                                <asp:TextBox runat="server" ID="txtDtNascResp" CssClass="campoData"></asp:TextBox>
                            </li>
                            <li>
                                <label>
                                    Grau Parentesco</label>
                                <asp:DropDownList runat="server" ID="ddlGrParen" Width="100px">
                                    <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Pai/Mãe" Value="PM"></asp:ListItem>
                                    <asp:ListItem Text="Tio(a)" Value="TI"></asp:ListItem>
                                    <asp:ListItem Text="Avô/Avó" Value="AV"></asp:ListItem>
                                    <asp:ListItem Text="Primo(a)" Value="PR"></asp:ListItem>
                                    <asp:ListItem Text="Cunhado(a)" Value="CN"></asp:ListItem>
                                    <asp:ListItem Text="Tutor(a)" Value="TU"></asp:ListItem>
                                    <asp:ListItem Text="Irmão(ã)" Value="IR"></asp:ListItem>
                                    <asp:ListItem Text="Outros" Value="OU" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li style="clear: both; margin: -5px 0 0 0px;">
                                <ul class="ulIdentResp">
                                    <li>
                                        <asp:Label runat="server" ID="lblcarteIden" Style="font-size: 9px;" CssClass="lblObrigatorio">Carteira de Identidade</asp:Label>
                                    </li>
                                    <li style="clear: both;">
                                        <label>
                                            Número</label>
                                        <asp:TextBox runat="server" ID="txtNuIDResp" Width="70px"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Org Emiss</label>
                                        <asp:TextBox runat="server" ID="txtOrgEmiss" Width="50px"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            UF</label>
                                        <asp:DropDownList runat="server" ID="ddlUFOrgEmis" Width="40px">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </li>
                            <li style="margin: -5px 0 0 10px;">
                                <ul class="ulDadosContatosResp">
                                    <li>
                                        <asp:Label runat="server" ID="Label4" Style="font-size: 9px;" CssClass="lblObrigatorio">Dados de Contato</asp:Label>
                                    </li>
                                    <li style="clear: both;">
                                        <label>
                                            Tel. Fixo</label>
                                        <asp:TextBox runat="server" ID="txtTelFixResp" Width="70px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Tel. Celular</label>
                                        <asp:TextBox runat="server" ID="txtTelCelResp" Width="70px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Tel. Comercial</label>
                                        <asp:TextBox runat="server" ID="txtTelComResp" Width="70px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Nº WhatsApp</label>
                                        <asp:TextBox runat="server" ID="txtNuWhatsResp" Width="70px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Facebook</label>
                                        <asp:TextBox runat="server" ID="txtDeFaceResp" Width="91px"></asp:TextBox>
                                    </li>
                                </ul>
                            </li>
                            <li style="clear: both; width: 206px; border-right: 1px solid #CCCCCC; margin-left: -5px;
                                height: 65px;">
                                <ul style="margin-left: 0px" class="ulInfosGerais">
                                    <li style="margin-left: 5px; margin-bottom: 1px;">
                                        <label class="lblSubInfos">
                                            INFORMAÇÕES GERAIS</label>
                                    </li>
                                    <li style="clear: both">
                                        <asp:CheckBox runat="server" ID="chkPaciEhResp" OnCheckedChanged="chkPaciEhResp_OnCheckedChanged"
                                            AutoPostBack="true" Text="Responsável é o próprio paciente" CssClass="chk" />
                                    </li>
                                    <li style="clear: both">
                                        <asp:CheckBox runat="server" ID="chkPaciMoraCoResp" Text="Paciente mora com o(a) Responsável"
                                            CssClass="chk" />
                                    </li>
                                    <li style="clear: both">
                                        <asp:CheckBox runat="server" ID="chkRespFinanc" Text="É o responsável financeiro"
                                            CssClass="chk" />
                                    </li>
                                </ul>
                            </li>
                            <li>
                                <ul style="margin-left: 0px" class="ulEndResiResp">
                                    <li style="margin-left: 1px; margin-bottom: 1px;">
                                        <label class="lblSubInfos">
                                            ENDEREÇO RESIDENCIAL / CORRESPONDÊNCIA</label>
                                    </li>
                                    <li style="clear: both;">
                                        <label class="lblObrigatorio">
                                            CEP</label>
                                        <asp:TextBox runat="server" ID="txtCEP" Width="55px" CssClass="campoCEP"></asp:TextBox>
                                    </li>
                                    <li style="margin: 11px 2px 0 -2px;">
                                        <asp:ImageButton ID="imgPesqCEP" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                            OnClick="imgPesqCEP_OnClick" Width="13px" Height="13px" />
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio">
                                            UF</label>
                                        <asp:DropDownList runat="server" ID="ddlUF" Width="40px" OnSelectedIndexChanged="ddlUF_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio">
                                            Cidade</label>
                                        <asp:DropDownList runat="server" ID="ddlCidade" Width="130px" OnSelectedIndexChanged="ddlCidade_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio">
                                            Bairro</label>
                                        <asp:DropDownList runat="server" ID="ddlBairro" Width="115px">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="clear: both; margin-top: -8px;">
                                        <label class="lblObrigatorio">
                                            Logradouro</label>
                                        <asp:TextBox runat="server" ID="txtLograEndResp" Width="160px"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 10px; margin-top: -4px;">
                                        <label>
                                            Email</label>
                                        <asp:TextBox runat="server" ID="txtEmailResp" Width="197px"></asp:TextBox>
                                    </li>
                                </ul>
                            </li>
                            <li style="clear: both; margin-left: -5px; margin-top: -6px;">
                                <ul>
                                    <li class="liFotoColab">
                                        <fieldset class="fldFotoColab">
                                            <uc1:ControleImagem ID="upImagemAluno" runat="server" />
                                        </fieldset>
                                    </li>
                                </ul>
                            </li>
                            <li style="margin: -4px 0 0 -23px;">
                                <ul class="ulDadosPaciente">
                                    <li style="margin-bottom: -6px;">
                                        <label class="lblTop">
                                            DADOS DO PACIENTE - USUÁRIO DE SAÚDE</label>
                                    </li>
                                    <li style="clear: both">
                                        <label>
                                            Nº NIRE</label>
                                        <asp:TextBox runat="server" ID="txtNuNis" Enabled="false" Width="60"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            CPF</label>
                                        <asp:TextBox runat="server" ID="txtCPFMOD" CssClass="campoCpf" Width="75px"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hidCoPac" />
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio">
                                            Nome</label>
                                        <asp:TextBox runat="server" ID="txtnompac" ToolTip="Nome do Paciente" Width="308px"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio">
                                            Sexo</label>
                                        <asp:DropDownList runat="server" ID="ddlSexoPaci" Width="44px">
                                            <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="clear: both" class="lisobe">
                                        <label class="lblObrigatorio">
                                            Nascimento</label>
                                        <asp:TextBox runat="server" ID="txtDtNascPaci" CssClass="campoData"></asp:TextBox>
                                    </li>
                                    <li class="lisobe">
                                        <label>
                                            Origem</label>
                                        <asp:DropDownList runat="server" ID="ddlOrigemPaci" Width="90px">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-left: 10px;" class="lisobe">
                                        <label>
                                            Nº Cartão Saúde</label>
                                        <asp:TextBox runat="server" ID="txtNuCarSaude" Width="87px"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 10px;" class="lisobe">
                                        <label>
                                            Tel. Celular</label>
                                        <asp:TextBox runat="server" ID="txtTelResPaci" Width="68px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li class="lisobe">
                                        <label>
                                            Tel. Fixo</label>
                                        <asp:TextBox runat="server" ID="txtTelCelPaci" Width="68px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li class="lisobe">
                                        <label>
                                            Nº WhatsApp</label>
                                        <asp:TextBox runat="server" ID="txtWhatsPaci" Width="68px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li style="clear: both; margin-top: -2px; float: right">
                                        <asp:Label runat="server" ID="lblEmailPaci">Email</asp:Label>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtEmailPaci" Width="220px"></asp:TextBox>
                                    </li>
                                    <li style="clear: both; margin-top: -38px; margin-left: 2px">
                                        <asp:Label runat="server" ID="Label12" class="lblObrigatorio">Apelido</asp:Label>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtApelido" MaxLength="25" Width="80px"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 88px; margin-top: -38px;">
                                        <label for="txtIndicacao" title="Indicação">
                                            Indicação
                                        </label>
                                        <asp:DropDownList ID="ddlIndicacao" Style="width: 194px;" runat="server" ToolTip="Indicação">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </li>
                            <li class="liBtnAddA" style="margin-left: 0px !important; margin-top: 0px !important; clear: both !important; height: 15px;">
                                <asp:LinkButton ID="lnkCadastroCompleto" runat="server" OnClick="lnkCadastroCompleto_OnClick">
                                    <asp:Label runat="server" ID="Label23" Text="CADASTRO COMPLETO" Style="margin-left: 4px; margin-right: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li class="liBtnAddA" style="margin-left: 400px;">
                                <asp:LinkButton ID="lnkSalvar" Enabled="true" runat="server" OnClick="lnkSalvar_OnClick">
                                    <asp:Label runat="server" ID="Label5" Text="SALVAR" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                    <div id="divSuccessoMessage" runat="server" class="successMessageSMS" visible="false">
                        <asp:Label ID="lblMsg" runat="server" Visible="false" />
                        <asp:Label Style="color: #B22222 !important; display: block;" Visible="false" ID="lblMsgAviso"
                            runat="server" />
                    </div>
                </ul>
            </div>
        </li>
        <li>
            <div id="divConfirmMultiplo" style="display: none; height: 180px !important; width: 450px !important;">
                <ul>
                    <li style="margin-bottom: 10px;">
                        <asp:Label ID="lblHorarAgend" Text="Horário com agendamento. Deseja realmente agendar o paciente para este horário?" runat="server" />
                    </li>
                    <li runat="server" id="liBtnGrdFinanMater" class="liBtnGrdFinan" style="clear: both;
                        margin-left: 130px; cursor: pointer; width: 40px">
                        <%-- <asp:LinkButton ID="lnkMultiSim" OnClick="lnkconfm_OnClick" ValidationGroup="vgMontaGridMensa"
                            runat="server" Style="margin: 0 auto; cursor: pointer;" ToolTip="Realiza o agendamento múltiplo">
                            <asp:Label runat="server" ID="Label166" ForeColor="GhostWhite" Text="SIM"></asp:Label>
                        </asp:LinkButton>--%>
                        <asp:LinkButton ID="LinkButtonSIM" runat="server" OnClick="LinkButtonSIM_Click">
                            <asp:Label runat="server" Style="margin-left: 12px" ID="Label11" ForeColor="GhostWhite"
                                Text="SIM"></asp:Label></asp:LinkButton>
                    </li>
                    <li runat="server" id="li9" class="liBtnGrdFinan" style="cursor: pointer; clear: both;
                        margin-left: 100px; margin-top: -20px; width: 40px; margin-left: 200px;">
                        <asp:LinkButton ID="LinkButton2" OnClick="lnkMultiNao_OnClick" ValidationGroup="vgMontaGridMensa"
                            runat="server" Style="margin: 0 auto; margin-left: 10px;" ToolTip="Não realiza o agendamento múltiplo">
                            <asp:Label runat="server" ID="Label10" ForeColor="GhostWhite" Text="NÃO"></asp:Label>
                        </asp:LinkButton>
                        <br />
                    </li>
                </ul>
                <ul id="DadosAgenda" runat="server">
                    <li style="margin-left: -5px">
                        <br />
                        <asp:Label runat="server" ID="Label8" Style="clear: both">Tipo</asp:Label><br />
                        <asp:DropDownList ID="ddTipoM" Style="width: 80px;" runat="server">
                        </asp:DropDownList>
                    </li>
                    <li runat="server" id="li5" style="clear: both; margin-left: 80px; margin-top: -27px;
                        width: 110px">
                        <asp:Label runat="server" Style="clear: both;" ID="Label6">Operadora</asp:Label><br />
                        <asp:DropDownList ID="ddlOperadoraM" Style="width: 200px;" OnSelectedIndexChanged="ddlOperAgendM_OnSelectedIndexChanged"
                            AutoPostBack="true" runat="server">
                        </asp:DropDownList>
                    </li>
                    <li runat="server" id="li6" style="clear: both; margin-left: 285px; margin-top: -27px;
                        width: 110px">
                        <asp:Label runat="server" ID="Label7" Style="clear: both;">Plano</asp:Label>
                        <br />
                        <asp:DropDownList ID="ddlPlanoM" Style="width: 100px" runat="server">
                            <asp:ListItem Value="" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li runat="server" id="li7" class="liBtnGrdFinan" style="clear: both; margin-left: 325px;
                        width: 50px; background-color: #20B2AA">
                        <asp:LinkButton ID="LinkButton1" OnClick="lnkMultiSim_OnClick" ValidationGroup="vgMontaGridMensa"
                            runat="server" Style="margin: 0 auto; margin-left: 3px" ToolTip="Não realiza o agendamento múltiplo">
                            <asp:Label runat="server" ID="Label9" ForeColor="GhostWhite" Text="AVANÇAR"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divConfirmOperadora" style="display: none; height: 200px !important; width: 200px">
                <ul>
                    <li style="margin-bottom: 10px;">
                        <label>
                            Operadora e plano não informados no cadastro do paciente, deseja prosseguir?
                        </label>
                    </li>
                    <li runat="server" id="li8" class="liBtnGrdFinan" style="clear: both; margin: 15px 0 0 45px;
                        width: 30px">
                        <asp:LinkButton ID="lnkOperaSim" OnClick="lnkOperaSim_OnClick" ValidationGroup="vgMontaGridMensa"
                            runat="server" Style="margin: 0 auto;" ToolTip="Confirma que realmente o agendamento é sem operadora e plano">
                            <asp:Label runat="server" ID="Label13" style=" margin-left:5px" ForeColor="GhostWhite" Text="SIM"></asp:Label>
                        </asp:LinkButton>
                    </li>
                    <li runat="server" id="li10" class="liBtnGrdFinan" style="clear: both; width: 32px;
                        margin-left: 100px; margin-top:-20px">
                        <asp:LinkButton ID="lnkOperaNao" OnClick="lnkOperaNao_OnClick" ValidationGroup="vgMontaGridMensa"
                            runat="server" Style="margin: 0 auto;" ToolTip="Aborta a operação pois é preciso ainda informar a operadora e plano no cadastro do paciente">
                            <asp:Label runat="server" style=" margin-left:8px" ID="Label14" ForeColor="GhostWhite" Text="NÃO"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divConfirmAlta" style="display: none; height: 120px !important; width: 300px">
                <ul>
                    <li style="margin-bottom: 10px;">
                        <label>
                            O paciente não se encontra na situação de ATENDIMENTO, caso prossiga a situação atual dele será alterada para EM TRATAMENTO, deseja prosseguir?
                        </label>
                    </li>
                    <li class="liBtnGrdFinan" style="margin: 15px 0 0 90px; width: 30px">
                        <asp:LinkButton ID="lnkbConfAlta" OnClick="lnkbConfAlta_OnClick"
                            runat="server" ToolTip="Confirma o agendamento do paciente na situação de Alta">
                            <label style="margin-left:5px; color:White;">SIM</label>
                        </asp:LinkButton>
                    </li>
                    <li class="liBtnGrdFinan" style="margin: -21px 0 0 150px; width: 30px;">
                        <asp:LinkButton ID="lnkbDesconfAlta"
                            runat="server" ToolTip="Aborta a operação devido a situação atual do paciente">
                            <label style="margin-left:5px; color:White;">NÃO</label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
    </ul>
    <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdProfi">
        <ProgressTemplate>
            <div class="divCarregando">
                <asp:Image ID="imgCarregando" runat="server" AlternateText="Carregando" ImageAlign="Middle"
                    ImageUrl="~/Navegacao/Icones/carregando.gif" ToolTip="Carregando a solicitação" /></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdHora">
        <ProgressTemplate>
            <div class="divCarregando">
                <asp:Image ID="imgCarregando2" runat="server" AlternateText="Carregando" ImageAlign="Middle"
                    ImageUrl="~/Navegacao/Icones/carregando.gif" ToolTip="Carregando a solicitação" /></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updTopo">
        <ProgressTemplate>
            <div class="divCarregando">
                <asp:Image ID="imgCarregando3" runat="server" AlternateText="Carregando" ImageAlign="Middle"
                    ImageUrl="~/Navegacao/Icones/carregando.gif" ToolTip="Carregando a solicitação" /></div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <%--<asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updPlanoSaude">
        <ProgressTemplate>
            <div class="divCarregando">
                <asp:Image ID="imgCarregando4" runat="server" AlternateText="Carregando" ImageAlign="Middle"
                    ImageUrl="~/Navegacao/Icones/carregando.gif" ToolTip="Carregando a solicitação" /></div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <%--  <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="updCadasUsuario">
        <ProgressTemplate>
            <div class="divCarregando">
                <asp:Image ID="imgCarregando5" runat="server" AlternateText="Carregando" ImageAlign="Middle"
                    ImageUrl="~/Navegacao/Icones/carregando.gif" ToolTip="Carregando a solicitação" /></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="UpdFinanceiro">
        <ProgressTemplate>
            <div class="divCarregando">
                <asp:Image ID="imgCarregando6" runat="server" AlternateText="Carregando" ImageAlign="Middle"
                    ImageUrl="~/Navegacao/Icones/carregando.gif" ToolTip="Carregando a solicitação" /></div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <script type="text/javascript">
        function mostraDadosAgenda() {
            document.getElementById("DadosAgenda").style.display = "block";
        }
        function mostraErroPermi() {
            $("#lblMsgErro").fadeIn();

            setInterval(function () {
                $("#lblMsgErro").fadeOut("slow");
            }, 10000);
        }

        window.onload = function () {
            MaintainScrollProfi();
            MaintainScrollAgenda();
            MaintainScrollHistorico();
        }

        function MaintainScrollProfi() {
            var div = document.getElementById("divProfi");
            var div_position = document.getElementById("divProfi_posicao");
            var position = parseInt('<%= Request.Form["divProfi_posicao"] %>');
            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }

        function MaintainScrollAgenda() {
            var div = document.getElementById("divAgenda");
            var div_position = document.getElementById("divAgenda_posicao");
            var position = parseInt('<%= Request.Form["divAgenda_posicao"] %>');
            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }

        function MaintainScrollHistorico() {
            var div = document.getElementById("divHistoricoAgenda");
            var div_position = document.getElementById("divHistoricoAgenda_posicao");
            var position = parseInt('<%= Request.Form["divHistoricoAgenda_posicao"] %>');
            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }


        $(function () {
            $(".campoHora").mask("99:99");
        });

        $(document).ready(function () {
            $("#divOcorrencia").show();
            carregaCss();
        });

        //Inserida função apra abertura de nova janela popup com a url do relatório que apresenta as guias
        function customOpen(url) {
            var w = window.open(url);
            w.focus();
        }

        function carregaCss() {
            $(".campoCpf").unmask();
            $(".campoCpf").mask("999.999.999-99");
            $(".campoTel").unmask();
            $(".campoTel").mask("(99)9999-9999");
            $(".campoCEP").unmask();
            $(".campoCEP").mask("99999-999");
            $(".campoHora").unmask();
            $(".campoHora").mask("99:99");
            $(".campoAnos").unmask();
            $(".campoAnos").mask("99");
            $(".txtNireAluno").unmask();
            $(".txtNireAluno").mask("?999999999");
            $(".txtNIS").mask("?999999999999999?9");
            //            $(".campoData").datepicker();
            //            $(".campoData").mask("99/99/9999");
            $(".campoVenc").unmask();
            $(".campoVenc").mask("99/99");
            $(".numeroCartao").unmask();
            $(".numeroCartao").mask("9999.9999.9999.9999");
            $(".campoMoeda").unmask();
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });

            $(".lnkPesPaci").click(function () {
                $('#divLoadShowAlunos').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE PACIENTES",
                    open: function () { $('#divLoadShowAlunos').load("/Componentes/ListarPacientes.aspx"); }
                });
            });

            $(".lnkPesResp").click(function () {
                $('#divLoadShowResponsaveis').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE RESPONSÁVEIS",
                    open: function () { $('#divLoadShowResponsaveis').load("/Componentes/ListarResponsaveis.aspx"); }
                });
            });
        }

        function AbreModalInfosCadas() {
            $('#divLoadInfosCadas').dialog({ autoopen: false, modal: true, width: 652, height: 350, resizable: false, title: "USUÁRIO DE SAÚDE - CADASTRO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalAgendMulti() {
            $('#divConfirmMultiplo').dialog({ autoopen: false, modal: true, width: 420, height: 170, resizable: false, title: "AGENDAMENTO MÚLTIPLO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModaloperaConfirm() {
            $('#divConfirmOperadora').dialog({ autoopen: false, modal: true, width: 250, height: 130, resizable: false, title: "CONFIRMAÇÃO DE OPERADORA E PLANO DE SAÚDE",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }


            });
        }

        function AbreModalConfirmAlta() {
            $('#divConfirmAlta').dialog({ autoopen: false, modal: true, width: 300, height: 120, resizable: false, title: "CONFIRMAÇÃO DE AGENDAMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

    </script>
</asp:Content>
