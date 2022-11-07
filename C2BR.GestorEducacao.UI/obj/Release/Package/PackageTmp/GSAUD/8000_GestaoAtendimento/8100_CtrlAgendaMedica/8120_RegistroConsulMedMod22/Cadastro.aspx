<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_RegistroConsulMedMod22.Cadastro" %>

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
        .lichkRetornaProced
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
        .ID_ITENS_PLANE_AVALI
        {
            display:none;
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
                        <asp:CheckBox runat="server" ID="chkPesqNire" Style="margin-left: -5px" OnCheckedChanged="chkPesqNire_OnCheckedChanged"
                            AutoPostBack="true" />
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
                        <label>
                            Unidade</label>
                        <asp:DropDownList ID="ddlLocalPaciente" Width="170px" runat="server" OnSelectedIndexChanged="ddlLocalPaciente_OnSelectedIndexChanged"
                            AutoPostBack="true" />
                    </li>
                    <li style="margin-left: 10px;">
                        <label>
                            Profissional</label>
                        <asp:DropDownList ID="drpProfissional" Width="160px" runat="server" OnSelectedIndexChanged="drpProfissional_OnSelectedIndexChanged"
                            AutoPostBack="true" />
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
                            Width="230px" Visible="false" OnSelectedIndexChanged="ddlNomeUsu_OnSelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtNomePacPesq" Width="230px" ToolTip="Digite o nome ou parte do nome do paciente"
                            runat="server" />
                    </li>
                    <li style="margin-top: 11px; margin-left: -4px;">
                        <asp:ImageButton ID="imgbPesqPacNome" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                            OnClick="imgbPesqPacNome_OnClick" />
                        <asp:ImageButton ID="imgbVoltarPesq" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                            OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" />
                    </li>
                    <li style="margin-left: -205px;">
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
                            <asp:CheckBox ID="chkCortesia" Style="margin: 0 -5px 0 -5px;" runat="server" ToolTip="Atendimento de Cortesia (Sem Valor)" />Cortesia
                        </label>
                    </li>
                    <li style="margin-top: 13px;">
                        <asp:CheckBox runat="server" ID="chkEnviaSms" Text="Enviar SMS" ToolTip="Selecione para enviar um SMS com informações do agendamento ao Paciente automaticamente"
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
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <label title="Procedimentos agendados e suas respectivas formas de contratação">
                                                                PR</label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hidCoAgend" Value='<%# Eval("CO_AGEND") %>' runat="server" />
                                                            <asp:ImageButton ID="imgProcedHistor" ImageUrl="/Library/IMG/PGS_CentralRegulacao_Icone_EmissaoGuia.png"
                                                                ToolTip="Lista os procedimentos e suas formas de contratação" runat="server"
                                                                Style="width: 13px; height: 19px;" OnClick="imgProcedHistor_OnClick" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="APELIDO_PROFISSIONAL" HeaderText="PROF.">
                                                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ESPEC" HeaderText="FUNÇÃO">
                                                        <ItemStyle Width="20px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="STATUS_V" HeaderText="STATUS">
                                                        <ItemStyle Width="20px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                            <li style="width: 500px; float: right; margin-right: 20px !important;">
                                <ul>
                                    <li class="liTituloGrid" style="width: 100%; height: 24px !important; margin-right: 40px !important;
                                        background-color: #ffff99; text-align: center; font-weight: bold; margin-bottom: 5px">
                                        <div style="float: left; margin-left: 10px !important;">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                                                GRID DE PROFISSIONAIS</label>
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
                                        <asp:DropDownList runat="server" ID="ddlDept" Style="width: 185px;">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="float: right; margin-left: -105px; margin-right: 25px;">
                                        <label style="color: Blue;" title="Selecione a Classificação Profissional para filtrar os Profissionais">
                                            Classif. Profi.
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlClassProfi" ToolTip="Selecione a Classificação Profissional para filtrar os Profissionais">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: 12px; margin-left: 0px; float: right;">
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
                                                            <asp:HiddenField ID="hidClassFuncProfi" Value='<%# Eval("CO_CLASS_FUNC") %>' runat="server" />
                                                            <asp:CheckBox ID="ckSelect" OnCheckedChanged="ckSelect_CheckedChangedP" AutoPostBack="true"
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NO_COL" HeaderText="PROFISSIONAL DE SAÚDE">
                                                        <ItemStyle Width="223px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DescEspec" HeaderText="FUNÇÃO">
                                                        <ItemStyle Width="40px" HorizontalAlign="Left" />
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
                                                    <asp:Label ToolTip="Classificação Agendamento" runat="server">Class</asp:Label>
                                                    <asp:DropDownList runat="server" ID="ddlClassFunci" 
                                                        AutoPostBack="true" Width="85px" ToolTip="Classificação Agendamento">
                                                    </asp:DropDownList>
                                                </li>
                                                <li>
                                                    <asp:Label ToolTip="Tipo de Agendamento" runat="server">Tipo</asp:Label>
                                                    <asp:DropDownList runat="server" Width="59px" ID="ddlTipoAg" OnSelectedIndexChanged="ddlTipoAg_OnSelectedIndexChanged"
                                                        AutoPostBack="true" ToolTip="Tipo de Agendamento">
                                                    </asp:DropDownList>
                                                </li>
                                                <li>
                                                    <asp:Label ToolTip="Tipo de Contratação" runat="server">Cont</asp:Label>
                                                    <asp:DropDownList runat="server" Width="65px" ID="ddlOpers" OnSelectedIndexChanged="ddlOpers_OnSelectedIndexChanged"
                                                        ToolTip="Tipo de Contratação" AutoPostBack="true">
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
                                        <div id="divAgenda" class="divGridData" style="height: 120px; width: 980px; border: 1px solid #ccc;">
                                            <input type="hidden" id="divAgenda_posicao" name="divAgenda_posicao" />
                                            <asp:HiddenField runat="server" ID="hidQtpValor" Value='' />
                                            <asp:HiddenField runat="server" ID="hidIdClient" Value='' />
                                            <asp:HiddenField runat="server" ID="hidCoConsul" />
                                            <asp:GridView ID="grdHorario" CssClass="grdBusca" runat="server" AutoGenerateColumns="false"
                                                Style="width: 100%;" OnRowDataBound="grdHorario_OnRowDataBound">
                                                <RowStyle CssClass="rowStyle" />
                                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                                <EmptyDataTemplate>
                                                    Nenhum registro encontrado.<br />
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <%--<asp:BoundField DataField="numOrdem" HeaderText="">
                                                        <ItemStyle Width="10px" HorizontalAlign="Left" />
                                                    </asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemStyle Width="20px" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndice" runat="server" Text='<%# Eval("numOrdem") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CK">
                                                        <ItemStyle Width="5px" HorizontalAlign="Center" />
                                                        <HeaderStyle Width="5px" HorizontalAlign="Center" />
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkMarcaTodosItens" CssClass="chk" Style="" runat="server" OnCheckedChanged="ChkTodos_OnCheckedChanged"
                                                                AutoPostBack="true"></asp:CheckBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckSelectHr" CssClass="chk" runat="server" OnCheckedChanged="ckSelectHr_OnCheckedChanged"
                                                                AutoPostBack="true" />
                                                            <asp:HiddenField runat="server" ID="hidNuRapRetorno" Value='<%# Eval("NU_RAP_RETORNO") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoAgenda" Value='<%# Eval("CO_AGEND") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# Eval("CO_ALU") %>' />
                                                            <asp:HiddenField runat="server" ID="hidTpCons" Value='<%# Eval("TP_CONSUL") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoCol" Value='<%# Eval("CO_COL") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoEspec" Value='<%# Eval("CO_ESPEC") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoDepto" Value='<%# Eval("CO_DEPTO") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoRecep" Value='<%# Eval("ID_DEPTO_LOCAL_RECEP") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoEmp" Value='<%# Eval("CO_EMP") %>' />
                                                            <asp:HiddenField runat="server" ID="hidDataHora" Value='<%# Eval("hora") %>' />
                                                            <asp:HiddenField runat="server" ID="hidData" Value='<%# Eval("dt") %>' />
                                                            <asp:HiddenField runat="server" ID="hidHora" Value='<%# Eval("hr") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="hora" HeaderText="DATA E HORA">
                                                        <ItemStyle Width="125px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="REF">
                                                        <HeaderTemplate>
                                                            <label style="font-weight: bold;" title="índice para definir a referência / dependência entre os agendamentos">
                                                                REF</label>
                                                        </HeaderTemplate>
                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:DropDownList Style="background: rgba(255, 165, 0, 0.5);" ID="ddlNumOrdem" runat="server"
                                                                Width="100%" ToolTip="Selecione o índice do agendamento que será usado como referência">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NO_PAC" HeaderText="nome do paciente">
                                                        <ItemStyle Width="235px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="LOCAL RECEP.">
                                                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField runat="server" ID="hidLocalRecep" Value='<%# Eval("ID_DEPTO_LOCAL_RECEP") %>' />
                                                            <asp:DropDownList runat="server" ID="ddlLocalRecep" Width="100%" Style="margin-left: -4px;
                                                                margin-bottom: 0px;">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TIPO">
                                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField runat="server" ID="hidTpConsul" Value='<%# Eval("CO_TP_CONSUL") %>' />
                                                            <asp:DropDownList runat="server" ID="ddlTipo" AutoPostBack="true" Width="100%" Style="margin-left: -4px;
                                                                margin-bottom: 0px;" OnSelectedIndexChanged="ddlTipo_OnSelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <label style="font-weight: bold;" title="Controle dos procedimentos">
                                                                PR</label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgProcedHorar" ImageUrl="/Library/IMG/PGS_CentralRegulacao_Icone_EmissaoGuia.png"
                                                                ToolTip="Informar ou alterar os procedimentos e sua forma de contratação" runat="server"
                                                                Style="width: 13px; height: 19px;" OnClick="imgProcedHorar_OnClick" />
                                                            <%--<asp:CheckBox ID="chkRetornaProced" ToolTip="Recuperar procedimentos anteriores"
                                                                runat="server" Style="margin-top: -5px;" />--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QTP">
                                                        <ItemStyle Width="10px" HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField runat="server" ID="hidQtp" Value='<%# Eval("QTPTotal") %>' />
                                                            <asp:TextBox runat="server" ID="txtQtp" Text='<%# Eval("QTPTotal") %>' CssClass=""
                                                                Width="100%" Style="text-align: right; margin-left: -4px; margin-bottom: 0px;"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="R$ QTP">
                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField runat="server" ID="hidQtpValor" Value='<%# Eval("QTPVTotal") %>' />
                                                            <asp:TextBox runat="server" ID="txtValorAgend" Text='<%# Eval("QTPVTotal") %>' CssClass="campoMoeda"
                                                                Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CO_SITU_VALID" HeaderText="STATUS">
                                                        <ItemStyle Width="20px" HorizontalAlign="Left" />
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
                <asp:HiddenField runat="server" ID="hidCoAluMod" />
                <asp:HiddenField runat="server" ID="hidCoRespMod" />
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
                                <asp:TextBox runat="server" ID="txtNomeResp" Width="170px" ToolTip="Nome do Responsável"></asp:TextBox>
                            </li>
                            <li> 
                                <label class="">
                                    Pasta</label>
                                <asp:TextBox runat="server" MaxLength="15" ID="txtPastaControle" Width="50px" ToolTip="Número da pasta de controle, se não preenchida, será o mesmo que o NIRE"></asp:TextBox>
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
                                <asp:DropDownList runat="server" ID="ddlGrParen" Width="87px">
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
                            <li style="margin: -45px 0 0 180px;">
                                <ul class="ulDadosContatosResp">
                                    <li>
                                        <asp:Label runat="server" ID="Label1" Style="font-size: 9px;" CssClass="lblObrigatorio">Dados de Contato</asp:Label>
                                    </li>
                                    <li style="clear: both;">
                                        <label>
                                            Tel. Fixo</label>
                                        <asp:TextBox runat="server" ID="txtTelFixResp" Width="77px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Tel. Celular</label>
                                        <asp:TextBox runat="server" ID="txtTelCelResp" Width="77px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Tel. Comercial</label>
                                        <asp:TextBox runat="server" ID="txtTelComResp" Width="77px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Nº WhatsApp</label>
                                        <asp:TextBox runat="server" ID="txtNuWhatsResp" Width="77px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Facebook</label>
                                        <asp:TextBox runat="server" ID="txtDeFaceResp" Width="77px"></asp:TextBox>
                                    </li>
                                     <li class="lisobe" style="clear:both;margin-left:-180px;">
                                        <label style="color:Red" title="Telefone em formato distinto ao do sistema e geralmente migrados de outras bases">
                                            Telefone*</label>
                                        <asp:TextBox runat="server" ID="txtTelMigrado" Width="320px" MaxLength="200" ToolTip="Telefone em formato distinto ao do sistema e geralmente migrados de outras bases"></asp:TextBox>
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
                                    <li style="margin-left: 0px;" class="lisobe">
                                        <label>
                                            Nº Cartão Saúde</label>
                                        <asp:TextBox runat="server" ID="txtNuCarSaude" Width="87px"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 0px;" class="lisobe">
                                        <label>
                                            Tel. Celular</label>
                                        <asp:TextBox runat="server" ID="txtTelResPaci" Width="76px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li class="lisobe">
                                        <label>
                                            Tel. Fixo</label>
                                        <asp:TextBox runat="server" ID="txtTelCelPaci" Width="76px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li class="lisobe">
                                        <label>
                                            Nº WhatsApp</label>
                                        <asp:TextBox runat="server" ID="txtWhatsPaci" Width="76px" CssClass="campoTel"></asp:TextBox>
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
                            <li class="liBtnAddA" style="margin-left: 0px !important; margin-top: 0px !important;
                                clear: both !important; height: 15px;">
                                <asp:LinkButton ID="lnkCadastroCompleto" runat="server" OnClick="lnkCadastroCompleto_OnClick">
                                    <asp:Label runat="server" ID="Label23" Text="CADASTRO COMPLETO" Style="margin-left: 4px;
                                        margin-right: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li class="liBtnAddA" style="margin-left: 423px;">
                                <asp:LinkButton ID="lnkSalvar" Enabled="true" runat="server" OnClick="lnkSalvar_OnClick">
                                    <asp:Label runat="server" ID="Label2" Text="SALVAR" Style="margin-left: 4px;"></asp:Label>
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
                        <asp:Label ID="lblHorarAgend" Text="Horário com agendamento. Deseja realmente agendar o paciente para este horário?"
                            runat="server" />
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
                            <asp:Label runat="server" ID="Label13" Style="margin-left: 5px" ForeColor="GhostWhite"
                                Text="SIM"></asp:Label>
                        </asp:LinkButton>
                    </li>
                    <li runat="server" id="li10" class="liBtnGrdFinan" style="clear: both; width: 32px;
                        margin-left: 100px; margin-top: -20px">
                        <asp:LinkButton ID="lnkOperaNao" OnClick="lnkOperaNao_OnClick" ValidationGroup="vgMontaGridMensa"
                            runat="server" Style="margin: 0 auto;" ToolTip="Aborta a operação pois é preciso ainda informar a operadora e plano no cadastro do paciente">
                            <asp:Label runat="server" Style="margin-left: 8px" ID="Label14" ForeColor="GhostWhite"
                                Text="NÃO"></asp:Label>
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
                            O paciente não se encontra na situação de ATENDIMENTO, caso prossiga a situação
                            atual dele será alterada para EM TRATAMENTO, deseja prosseguir?
                        </label>
                    </li>
                    <li class="liBtnGrdFinan" style="margin: 15px 0 0 90px; width: 30px">
                        <asp:LinkButton ID="lnkbConfAlta" OnClick="lnkbConfAlta_OnClick" runat="server" ToolTip="Confirma o agendamento do paciente na situação de Alta">
                            <label style="margin-left:5px; color:White;">SIM</label>
                        </asp:LinkButton>
                    </li>
                    <li class="liBtnGrdFinan" style="margin: -21px 0 0 150px; width: 30px;">
                        <asp:LinkButton ID="lnkbDesconfAlta" runat="server" ToolTip="Aborta a operação devido a situação atual do paciente">
                            <label style="margin-left:5px; color:White;">NÃO</label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divLoadProcedHorar" style="display: none; height: 120px !important; width: 300px">
                <ul class="ulDados">
                    <li>
                        <ul style="width: 766px; margin-top: -10px;">
                            <li class="liTituloGrid" style="width: 957px; height: 20px !important; margin-left: -5px;
                                background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                                <li style="margin: -15px 0 0 2px; float: left">
                                    <%--<label style="font-family: Tahoma; font-weight: bold; margin-top: -16px;" runat="server"
                                        id="lblTituloProcedMod">
                                    </label>--%>
                                    <asp:Label Style="font-family: Tahoma; font-weight: bold; margin-top: -16px;" runat="server"
                                        ID="lblTituloProcedMod" Text=""></asp:Label>
                                </li>
                            </li>
                            <li id="Li1" runat="server" title="Marque para recuperar procedimentos anteriores"
                                class="" style="float: right; margin: -15px -161px 2px 2px; height: 15px; width: 96px;">
                                <asp:Label Style="font-family: Tahoma; font-weight: bold; margin-top: -16px;" runat="server"
                                    Text="Retornar anteriores"></asp:Label>
                            </li>
                            <li runat="server" title="Marque para recuperar procedimentos anteriores" class=""
                                style="float: right; margin: -17px -170px 2px 2px; height: 15px; width: 12px;">
                                <asp:CheckBox ID="chkRetornaProced" ToolTip="Recuperar procedimentos anteriores"
                                    runat="server" Style="margin-top: -5px;" AutoPostBack="true" OnCheckedChanged="chkRetornaProced_OnCheckedChanged" />
                            </li>
                            <li id="li4" runat="server" title="Clique para adicionar um exame ao atendimento"
                                class="liBtnAddA" style="float: right; margin: -20px -205px 2px 2px; height: 15px;
                                width: 12px;">
                                <asp:ImageButton ID="lnkAddProcPla" Height="15px" Width="15px" Style="margin-top: 0px;
                                    margin-left: -1px;" ImageUrl="/Library/IMG/Gestor_SaudeEscolar.png" OnClick="lnkAddProcPla_OnClick"
                                    runat="server" />
                            </li>
                        </ul>
                    </li>
                    <li style="clear: both; margin: 3px 0 0 -4px !important;">
                        <asp:HiddenField runat="server" ID="hidCoPaciProced" />
                        <asp:HiddenField runat="server" ID="hidCoAgendProced" />
                        <div style="width: 972px; height: 238px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdProcedimentos" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum Procedimento de Plano de Saúde associado<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="ID_ITENS_PLANE_AVALI">
                                        <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="ID_ITENS_PLANE_AVALI" />
                                        <HeaderStyle CssClass="ID_ITENS_PLANE_AVALI" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtCoItemProced" Width="100%"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SOLICIT">
                                        <ItemStyle Width="75px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlSolicProc" Width="100%" Style="margin: 0 0 0 -4px !important;">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CONTRAT">
                                        <ItemStyle Width="75px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlContratProc" Width="100%" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlOpers_OnSelectedIndexChanged" Style="margin: 0 0 0 -4px !important;">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PLANO">
                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlPlanoProc" Width="100%" Style="margin: 0 0 0 -4px !important;">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nº CART">
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtNrCartProc" Width="100%" Style="margin-left: -4px;
                                                margin-bottom: 0px;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PROCEDIMENTO">
                                        <ItemStyle Width="270px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlProcMod" Width="100%" Style="margin: 0 0 0 -4px !important;"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlProcedAgend_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CORT">
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkCortProc" OnCheckedChanged="chkCortProc_OnChanged"
                                                AutoPostBack="true"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="R$ UNIT">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtValorUnit" Width="100%" CssClass="campoMoeda"
                                                Style="margin-left: -4px; margin-bottom: 0px;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTP">
                                        <ItemStyle Width="10px" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtQTPMod" Width="100%" Text="1" Style="text-align: right;
                                                margin-left: -4px; margin-bottom: 0px;" OnTextChanged="Qtp_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="R$ TOTAL">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtValorTotalMod" Width="100%" CssClass="campoMoeda"
                                                Style="margin-left: -4px; margin-bottom: 0px;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EX">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgExcPla" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                ToolTip="Excluir Exame" OnClick="imgExcPla_OnClick" Style="margin: 0 0 0 0 !important;"
                                                OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                    <li class="liBtnAddA" style="margin-left: 465px;">
                        <asp:LinkButton ID="btnConfirmarProced" Enabled="true" runat="server" OnClick="lnkConfirmarProced_OnClick">
                            <asp:Label runat="server" ID="lblConfirmarProced" Text="Confirmar" Style="margin-left: 2px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divLoadProcedHistor" style="display: none; height: 120px !important; width: 300px">
                <ul class="ulDados">
                    <li>
                        <ul style="width: 766px">
                            <li class="liTituloGrid" style="width: 752px; height: 20px !important; margin-left: -0px;
                                background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                                <li style="margin: 0 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: -16px;">
                                        PROCEDIMENTOS</label>
                                </li>
                            </li>
                        </ul>
                    </li>
                    <li style="clear: both; margin: 6px 0 0 3px !important;">
                        <div style="width: 766px; height: 238px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdProcedHistor" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum Procedimento de Plano de Saúde associado<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="CODIGO">
                                        <ItemStyle Width="35px" HorizontalAlign="Left" CssClass="CODIGO" />
                                        <HeaderStyle CssClass="CODIGO" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCodigoProced" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TIPO">
                                        <ItemStyle Width="35px" HorizontalAlign="Left" CssClass="TIPO" />
                                        <HeaderStyle CssClass="TIPO" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTipoProced" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DESCRIÇÃO">
                                        <ItemStyle Width="351px" HorizontalAlign="Left" CssClass="DESCRICAO" />
                                        <HeaderStyle CssClass="DESCRICAO" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDescricaoProced" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="R$ UNIT">
                                        <ItemStyle Width="38px" HorizontalAlign="Right" CssClass="ValorUnit" />
                                        <HeaderStyle CssClass="ValorUnit" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblValorUnitProced" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTD">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="QTD" />
                                        <HeaderStyle CssClass="QTD" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblQtdProced" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="R$ TOTAL">
                                        <ItemStyle Width="40px" HorizontalAlign="Right" CssClass="TOTAL" />
                                        <HeaderStyle CssClass="TOTAL" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalProced" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CONTRAT">
                                        <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="CONTRAT" />
                                        <HeaderStyle CssClass="CONTRAT" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblContratProced" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CORT">
                                        <ItemStyle Width="10px" HorizontalAlign="Center" CssClass="CORT" />
                                        <HeaderStyle CssClass="CORT" />
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkCortProced" Enabled="false"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divLoadAgendaRetorno" style="display: none; height: 120px !important; width: 300px">
                <asp:HiddenField runat="server" ID="hidCoColAgendRetorno" />
                <asp:HiddenField runat="server" ID="hidCoAgendRetorno" />
                <asp:HiddenField runat="server" ID="hidDataLimiteRetorno" />
                <ul class="ulDados">
                    <li>
                        <ul style="width: 665px">
                            <li class="liTituloGrid" style="width: 665px; height: 20px !important; margin-left: -0px;
                                background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                                <li style="margin: 0 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: -16px;">
                                        AGENDAS VÁLIDAS</label>
                                </li>
                                <li style="margin: 0 0 0 10px; float: right">
                                    <label runat="server" id="lblValidadeRetorno" style="font-family: Tahoma; font-weight: bold;
                                        margin-top: -16px;">
                                        VALIDADE DE RETORNO: 0 DIAS</label>
                                </li>
                            </li>
                        </ul>
                    </li>
                    <li style="clear: both; margin: 6px 0 0 3px !important;">
                        <div style="width: 660px; height: 238px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdAgendaRetorno" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhuma agenda associada<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="CO_COL" Visible="false">
                                        <ItemStyle Width="50px" HorizontalAlign="Left" CssClass="CO_COL" />
                                        <HeaderStyle CssClass="CO_COL" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCoColAgendaR" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemStyle Width="20px" HorizontalAlign="Left" CssClass="" />
                                        <HeaderStyle CssClass="" />
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkAgendaR" AutoPostBack="true" OnCheckedChanged="chkAgendaR_OnCheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DATA">
                                        <ItemStyle Width="50px" HorizontalAlign="Left" CssClass="DATA" />
                                        <HeaderStyle CssClass="DATA" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDataAgendaR" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HORA">
                                        <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="HORA" />
                                        <HeaderStyle CssClass="HORA" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblHoraAgendaR" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RAP">
                                        <ItemStyle Width="80px" HorizontalAlign="Center" CssClass="RAP" />
                                        <HeaderStyle CssClass="RAP" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblRapAgendaR" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PROFISSIONAL">
                                        <ItemStyle Width="100px" HorizontalAlign="Left" CssClass="PROFISSIONAL" />
                                        <HeaderStyle CssClass="PROFISSIONAL" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblProfiAgendaR" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ESPECIALIDADE">
                                        <ItemStyle Width="150px" HorizontalAlign="Left" CssClass="ESPECIALIDADE" />
                                        <HeaderStyle CssClass="ESPECIALIDADE" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblEspecAgendaR" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                    <li class="liBtnAddA" style="margin-left: 300px;">
                        <asp:LinkButton ID="lnkConfirmarRetorno" Enabled="true" runat="server" OnClick="lnkConfirmarRetorno_OnClick">
                            <asp:Label runat="server" ID="lblConfirmarRetorno" Text="CONFIRMAR" Style="margin-left: 0px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
    </ul>
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
            //            $(".campoTel").unmask();
            //            $(".campoTel").mask("(99)9999-9999");
            $('.campoTel').focusout(function () {
                var phone, element;
                element = $(this);
                element.unmask();
                phone = element.val().replace(/\D/g, '');
                if (phone.length > 10) {
                    element.mask("(99)99999-999?9");
                } else {
                    element.mask("(99)9999-9999?9");
                }
            }).trigger('focusout');
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
            $('#divLoadInfosCadas').dialog({ autoopen: false, modal: true, width: 652, height: 380, resizable: false, title: "USUÁRIO DE SAÚDE - CADASTRO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalProcedHorar() {
            $('#divLoadProcedHorar').dialog({ autoopen: false, modal: true, width: 990, height: 350, top: 87, left: 4, resizable: false, title: "AÇÃO APLICADA AO PACIENTE E SUA FORMA DE CONTRATAÇÃO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalProcedHistor() {
            $('#divLoadProcedHistor').dialog({ autoopen: false, modal: true, width: 795, height: 350, resizable: false, title: "PROCEDIMENTOS E SUAS FORMAS DE CONTRATAÇÃO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalAgendaRetorno() {
            $('#divLoadAgendaRetorno').dialog({ autoopen: false, modal: true, width: 690, height: 350, left: 320, resizable: false, title: "AGENDAMENTOS PARA RETORNO",
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
