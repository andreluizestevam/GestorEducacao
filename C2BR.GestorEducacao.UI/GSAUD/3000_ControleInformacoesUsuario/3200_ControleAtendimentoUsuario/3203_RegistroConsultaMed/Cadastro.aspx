<%--
// EMPRESA:     C2BR Soluções em Tecnologia
// SISTEMA:     PGS (Portal Gestor Saúde)
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO:      Recepção e Encaminhamento de Pacientes
// SUBMÓDULO:   Marcação de Pacientes para Atendimento
// OBJETIVO:    AGENDAMENTO DE CONSULTAS/SESSÕES - MODELO SIMPLES
// DATA DE CRIAÇÃO: **/**/****
//--------------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//--------------------------------------------------------------------------------------
//  DATA      |  NOME DO PROGRAMADOR              | DESCRIÇÃO RESUMIDA
// -----------+-----------------------------------+-------------------------------------
// 21/08/2017 | Diogo Costa                       | Criação da função js para incluir um load na pagina ao executar alguma ação
// 21/08/2017 | Diogo Costa                       | Inclusão de um filtro na agenda para o local de atendimento
// 23/08/2017 | Diogo Costa                       | No cabeçalho de agenda, diminuir espaço do "até" quebrar a linha de "horário disp"
// 23/08/2017 | Diogo Costa                       | Alterar label de "paciente" para "pesquisar outro paciente"
// 23/08/2017 | Diogo Costa                       | Adaptar modal de edição de horário do agendamento para alterar plano de saúde
// --%>

<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3203_RegistroConsultaMed.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 990px;
             margin-top: 15px !important;
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
            text-align: left;
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
        }
        .chkDestaque label
        {
             font-weight:bold;
            color: #FFA07A;
            margin-left:-4px;
            display:inline;
        }
        #divAcaoCarregando
        {
            left: 42%;
    position: absolute;
    display: none;
    margin: 95px auto;
    z-index: 100;
        }
      
            #frmAlterAgend select
            {
                width: 110px;
             }
        #frmAlterAgend ul li
        {
            display: inline-block;
            margin-right: 5px;
        }
        .horaAgenda
        {
            
            height: 160px !important;
        }
      
    </style>
    <script type="text/javascript">

        /*if (navigator.userAgent.toLowerCase().match('chrome'))
        $("#ControleImagem .liControleImagemComp .lblProcurar").hide();*/
    </script>
    <!--[if IE]>
<style type="text/css">
    #divBarraMatric { width: 238px; }
    #ControleImagem .liControleImagemComp .lblProcurar { visibility:hidden; }
    #fldPhotoR #ControleImagem .liControleImagemComp img { visibility:hidden; }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%-- <div id="divAcaoCarregando" class="divTelaFuncionalidadesCarregando" style="display:none;">
        <img src="/Library/IMG/Gestor_Carregando.gif" alt="Carregando..." />
    </div>--%>
    <ul id="ulDados" class="ulDados">
        <li style="margin-right: 0px;">
            <ul id="ulDadosMatricula">
                <li style="margin-top: 10px; margin-left: 28px; float: right;">
                    <div id="divBotoes">
                        <ul>
                            <li style="margin: 10px 0 4px 0;">
                                <a class="lnkPesPaci" href="#">
                                    <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade"
                                        style="width: 17px; height: 17px;" />
                                </a> 
                            </li>
                            <li style="margin-left: 5px;">
                                <label>
                                    Prontuário</label>
                                <asp:CheckBox runat="server" ID="chkPesqNire" Style="margin-left: -5px" Checked="true"
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
                            <li>
                                <label>
                                    Pasta</label>
                                <asp:CheckBox runat="server" ToolTip="Pesquisar pelo número da pasta" ID="chkPasta"
                                    Style="margin-left: -5px" AutoPostBack="true" OnCheckedChanged="chkPasta_CheckedChanged" />
                                <asp:TextBox runat="server" ID="txtPasta" Width="75px" Style="margin-left: -7px"
                                    Enabled="false"></asp:TextBox>
                            </li>
                            <li style="margin-top: 12px; margin-left: -4px;" class="carregaAcao">
                                <asp:ImageButton ID="imgCpfResp" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                    OnClick="imgCpfResp_OnClick" />
                            </li>
                            <li style="margin: 10px -3px 0 15px;">
                                <asp:ImageButton ID="imgCadPac" runat="server" ImageUrl="~/Library/IMG/PGN_IconeTelaCadastro2.png"
                                    OnClick="imgCadPac_OnClick" Style="width: 18px !important; height: 17px !important;"
                                    ToolTip="Cadastro de Pacientes" />
                            </li>
                            <li style="margin-left: 8px">
                                <label for="ddlNomeUsuAteMed" title="Nome do usuário selecionado" class="lblObrigatorio">
                                    Paciente</label>
                                <asp:TextBox ID="ddlPaciente" runat="server" Width="280px" ToolTip="Digite o nome ou parte do nome do paciente" />
                                <asp:DropDownList ID="ddlNomeUsu" runat="server" ToolTip="Paciente para o qual a consulta será agendada"
                                    OnSelectedIndexChanged="ddlPac_SelectedIndexChanged" AutoPostBack="true" Width="280px"
                                    Visible="false">
                                </asp:DropDownList>
                            </li>
                            <li style="margin-top: 14px; margin-left: -4px;" class="carregaAcao">
                                <asp:ImageButton ID="imgbPesqPacNome" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                    OnClick="imgbPesqPacNome_OnClick" />
                                <asp:ImageButton ID="imgbVoltarPesq" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico" OnClick="imgbVoltarPesq_OnClick"
                                    Visible="false" runat="server" />
                            </li>
                            <li>
                                <label for="txtNisUsu" title="Número NIS do usuário selecionado">
                                    CNS/SUS</label>
                                <asp:TextBox ID="txtNisUsu" Enabled="false" runat="server" ToolTip="Número NIS do usuário selecionado"
                                    Width="90px">
                                </asp:TextBox>
                            </li>
                            <li>
                                <label class="lblObrigatorio">
                                    Tipo de Consulta</label>
                                <asp:DropDownList ID="ddlTpCons" Style="margin: 0px;" runat="server">
                                </asp:DropDownList>
                            </li>
                            <li style="margin-top: 13px;">
                                <asp:CheckBox runat="server" ID="chkEnviaSms" Text="Confirmar por SMS" ToolTip="Selecione para enviar um SMS com informações do agendamento ao Paciente automaticamente"
                                    CssClass="chkLocais" Checked="true" />
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
            </ul>
        </li>
        <li class="liSeparador"></li>
        <li>
            <div id="tabResConsultas" runat="server" clientidmode="Static">
                <ul id="ul10">
                    <li style="margin-top: 10px !important; width: 1000px;">
                        <ul>
                            <li style="width: 450px;">
                                <%--   <asp:UpdatePanel runat="server" ID="UpdProfi" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                <%-- Grid de profissionais --%>
                                <ul>
                                    <%--<li class="liTituloGrid" style="width: 420px; margin-right: 0px; background-color: #ffff99;">
                                        GRID DE PROFISSIONAIS </li>--%>
                                    <li class="liTituloGrid" style="width: 100%; height: 34px !important; margin-right: 40px !important;
                                        background-color: #ffff99; text-align: center; font-weight: bold; margin-bottom: 5px">
                                        <div style="float: left; margin-left: 10px !important;">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 11px;">
                                                GRID DE PROFISSIONAIS</label>
                                        </div>
                                        <div style="float: right; margin-top: 11px">
                                            <ul>
                                                <li>
                                                    <asp:Label runat="server" ID="id" ToolTip="Selecione a Classificação Profissional para filtrar os Profissionais">Classif. Profi.</asp:Label>
                                                    <asp:DropDownList runat="server" ID="ddlClassProfi" OnSelectedIndexChanged="ddlClassProfi_OnSelectedIndexChanged"
                                                        AutoPostBack="true" ToolTip="Selecione a Classificação Profissional para filtrar os Profissionais">
                                                    </asp:DropDownList>
                                                </li>
                                            </ul>
                                        </div>
                                    </li>
                                    <li style="margin-top: 5px">
                                        <label for="ddlUnidResCons" title="Selecione a unidade do médico">
                                            Unidade</label>
                                        <asp:DropDownList ID="ddlUnidResCons" Width="185px" runat="server" ToolTip="Selecione a unidade do médico"
                                            OnSelectedIndexChanged="ddlUnidResCons_OnSelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: 5px; margin-bottom: 5px">
                                        <label>
                                            Local</label>
                                        <asp:DropDownList runat="server" ID="ddlDept" Width="105px" OnSelectedIndexChanged="ddlDept_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: 5px">
                                        <label for="ddlEspMedResCons" title="Selecione a especialidade médica solicitada pelo usuário">
                                            Especialidade Médica</label>
                                        <asp:DropDownList ID="ddlEspMedResCons" Width="145px" runat="server" ToolTip="Selecione a especialidade médica solicitada pelo usuário"
                                            OnSelectedIndexChanged="ddlEspMedResCons_OnSelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <asp:HiddenField runat="server" ID="IDProf" />
                                    <asp:HiddenField runat="server" ID="HiddenField1" />
                                    <li style="margin-top: 5px">
                                        <div id="divGrdProfi" runat="server" class="divGridData" style="height: 293px; width: 447px;
                                            overflow-y: scroll !important; border: 1px solid #ccc;">
                                            <asp:HiddenField ID="hidCoColAge" Value="0" runat="server" />
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
                                                                CssClass="carregaAcao" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NO_COL" HeaderText="PROFISSIONAL DE SAÚDE">
                                                        <ItemStyle Width="223px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DE_ESP" HeaderText="ESPECIALIDADE">
                                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NO_EMP" HeaderText="LOCAL">
                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </li>
                                    <li style="clear: both; margin-top: 5px;">
                                        <label>
                                            OBS: Ao Finalizar, clique no disquete para Salvar</label>
                                    </li>
                                </ul>
                                <%-- Grid de profissionais --%>
                                <%--  </ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </li>
                            <li style="clear: none !important; width: 527px; margin-left: 7px !important; clear: none;">
                                <%--<asp:UpdatePanel runat="server" ID="UpdHora" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                <%-- Grid de horário --%>
                                <ul>
                                    <%--  <li class="liTituloGrid" style="width: 100%; margin-right: 0px; background-color: #d2ffc2;">
                                        AGENDA DE HORÁRIOS </li>--%>
                                    <li class="liTituloGrid" style="width: 100%; height: 68px !important; margin-right: 0px;
                                        background-color: #d2ffc2; text-align: center; font-weight: bold; margin-bottom: 8px">
                                        <div style="float: left; margin-left: 3px;">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 11px;">
                                                AGENDA</label>
                                        </div>
                                        <div style="float: right; margin-top: 10px;">
                                            <ul>
                                                <li style="margin-right: 3px;">
                                                    <asp:TextBox ID="txtDtIniResCons" runat="server" CssClass="campoData">
                                                    </asp:TextBox>
                                                    até
                                                    <asp:TextBox ID="txtDtFimResCons" runat="server" CssClass="campoData">
                                                    </asp:TextBox>
                                                </li>
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
                                            </ul>
                                        </div>
                                        <div style="float: left; margin-top: 3px; margin-left: 5px;">
                                            <span>Loc. Atend</span>
                                            <asp:DropDownList ID="ddlLocalAtend" runat="server" Width="120">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="margin-top: 4px;">
                                            <ul>
                                                <li style="margin-top: 3px; margin-left: 10px;"><span title="Selecione o tipo de agendamento para esse paciente">
                                                    TP Agendamento</span>
                                                    <asp:DropDownList runat="server" ToolTip="Selecione o tipo de agendamento para esse paciente"
                                                        ID="ddlSelecAgendamentos" AppendDataBoundItems="true">
                                                        <asp:ListItem Selected="true" Text="Único" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Multiplos" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-left: 10px; float: left; margin-top: 3px;">
                                                    <asp:CheckBox ID="chkHorDispResCons" runat="server" Text="Horários Disponíveis" CssClass="lachk" />
                                                </li>
                                                <li style="margin: 3px 15px 22px 5px; float: right;" class="carregaAcao">
                                                    <asp:ImageButton ID="imgPesqGridAgenda" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                        OnClick="imgPesqGridAgenda_OnClick" />
                                                </li>
                                            </ul>
                                        </div>
                                    </li>
                                    <li style="">
                                        <label for="ddlNomeUsuAteMed" title="Nome do usuário selecionado">
                                            Pesquisar outro paciente
                                        </label>
                                        <asp:TextBox ID="txtPacAgenda" runat="server" Width="280px" ToolTip="Digite o nome ou parte do nome do paciente" />
                                        <asp:DropDownList ID="ddlPacAgenda" runat="server" ToolTip="Paciente para o qual a consulta será agendada"
                                            Width="280px" Visible="false" OnSelectedIndexChanged="ddlPacAgenda_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: 14px; margin-left: -4px;">
                                        <asp:ImageButton ID="imgPesqPacAgenda" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                            OnClick="imgPesqPacAgenda_OnClick" />
                                        <asp:ImageButton ID="imgVoltaPesqPacAgenda" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                                            OnClick="imgVoltaPesqPacAgenda_OnClick" Visible="false" runat="server" />
                                    </li>
                                    <li>
                                        <div style="margin-left: 10px;">
                                        </div>
                                    </li>
                                    <li style="margin-top: 1px">
                                        <div id="div3" runat="server" class="divGridData" style="height: 206px; width: 525px;
                                            border: 1px solid #ccc;">
                                            <asp:HiddenField runat="server" ID="hidCoConsul" />
                                            <asp:GridView ID="grdHorario" CssClass="grdBusca" runat="server" AutoGenerateColumns="false"
                                                Style="width: 50%;" OnRowDataBound="grdHorario_RowDataBound">
                                                <RowStyle CssClass="rowStyle" />
                                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                                <EmptyDataTemplate>
                                                    Nenhum registro encontrado.<br />
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="CK">
                                                        <ItemStyle Width="0px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckSelectHr" runat="server" OnCheckedChanged="ckSelectHr_OnCheckedChanged"
                                                                AutoPostBack="true" />
                                                            <asp:HiddenField runat="server" ID="hidCoAgenda" Value='<%# Eval("CO_AGEND") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# Eval("CO_ALU") %>' />
                                                            <asp:HiddenField runat="server" ID="local" Value='<%# Eval("NO_EMP") %>' />
                                                            <asp:HiddenField runat="server" ID="hidTpCons" Value='<%# Eval("TP_CONSUL") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoCol" Value='<%# Eval("CO_COL") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoEspec" Value='<%# Eval("CO_ESPEC") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoDepto" Value='<%# Eval("CO_DEPTO") %>' />
                                                            <asp:HiddenField runat="server" ID="HiddenField2" Value='<%# Eval("CO_EMP") %>' />
                                                            <asp:HiddenField runat="server" ID="hora" Value='<%# Eval("hora") %>' />
                                                            <asp:HiddenField runat="server" ID="hidNoPac" Value='<%# Eval("NO_PAC") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="hora" HeaderText="DATA/HORA">
                                                        <ItemStyle Width="35%" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="LOCAL RECEP.">
                                                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:DropDownList runat="server" ID="ddlLocalRecep"  Width="100%"
                                                                Style="margin-left: -4px; margin-bottom: 0px;">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NO_PAC" HeaderText="nome do paciente">
                                                        <ItemStyle Width="180px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TP_CONSUL_VALID" HeaderText="Tipo">
                                                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NO_COL" HeaderText="PROFSSIONAL">
                                                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CO_SITU_VALID" HeaderText="status">
                                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="CONF">
                                                        <ItemStyle Width="0px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckConf" runat="server" Checked='<%# Eval("FL_CONF_VALID") %>' Enabled="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </li>
                                    <li style="clear: both; width: 532px">
                                        <%--<asp:UpdatePanel runat="server" ID="updPlanoSaude" UpdateMode="Conditional">
                                                    <ContentTemplate>--%>
                                        <ul style="float: left;">
                                            <li>
                                                <label style="color: #FAA460; font-weight: bold; margin-top: 12px; margin-bottom: 3px;">
                                                    Informações Plano de Saúde</label>
                                            </li>
                                            <li style="clear: both;">
                                                <label>
                                                    Operadora</label>
                                                <asp:DropDownList runat="server" ID="ddlOperPlano" ToolTip="Selecione a operadora/convênio"
                                                    OnSelectedIndexChanged="ddlOperPlano_OnSelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </li>
                                            <li>
                                                <label>
                                                    Plano</label>
                                                <asp:DropDownList runat="server" ID="ddlPlano" ToolTip="Selecione um plano">
                                                </asp:DropDownList>
                                            </li>
                                            <%--<li>
                                                <label>
                                                    Venc</label>
                                                <asp:TextBox Width="30px" runat="server" ID="txtDtVenciPlan" CssClass="campoVenc"></asp:TextBox>
                                            </li>--%>
                                            <li>
                                                <label>
                                                    Número</label>
                                                <asp:TextBox runat="server" ID="txtNumeroCartPla" Width="71px" MaxLength="12" ToolTip="Informe o número do plano"></asp:TextBox>
                                            </li>
                                            <li>
                                                <div class="liBtnAddA" style="margin-top: 11px; background-color: #DFF1FF;">
                                                    <%--     <label>
                                                    Procedimento Consulta</label>
                                                <asp:DropDownList runat="server" ID="ddlProcConsul" ToolTip="Selecione o Procedimento da consulta"
                                                    Width="279px">
                                                </asp:DropDownList>--%>
                                                    <asp:LinkButton ID="lnkSelecProcedimento" Enabled="true" ToolTip="Escolher os procedimentos associados ao agendamento do paciente"
                                                        runat="server" OnClick="lnkSelecProcedimento_OnClick">
                                                        <asp:Label runat="server" ID="Label4" Text="PROCEDIMENTOS"></asp:Label>
                                                    </asp:LinkButton>
                                                </div>
                                            </li>
                                        </ul>
                                        <%--   </ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                        <ul style="float: right !important; margin-top: 31px;">
                                            <li id="li2" runat="server" class="liBtnAddA" style="margin-left: 0px; height: 14px;">
                                                <asp:LinkButton ID="lnkInfosFinanc" Enabled="true" runat="server" OnClick="lnkInfosFinanc_OnClick">
                                                    <asp:Label runat="server" ID="Label3" Text="PGTO" Style="margin-left: 4px;"></asp:Label>
                                                </asp:LinkButton>
                                            </li>
                                            <li id="li13" runat="server" class="liBtnAddA" style="margin-left: 0px;">
                                                <asp:LinkButton ID="lnkImpriGuiaMed" Enabled="true" runat="server" OnClick="lnkImpriGuiaMed_OnClick">
                                                    <img id="imgImpriGuiaMed" runat="server" width="14" height="14" class="imgliLnk"
                                                        src='/Library/IMG/Gestor_IcoImpres.ico' alt="Icone Pesquisa" title="Imprime Protocolo do Agendamento de Consulta" />
                                                    <asp:Label runat="server" ID="Label26" Text="PROTOCOLO" Style="margin-left: 4px;"></asp:Label>
                                                </asp:LinkButton>
                                            </li>
                                        </ul>
                                    </li>
                                </ul>
                                <%-- Grid de horário --%>
                                <%--     </ContentTemplate>
                                </asp:UpdatePanel>--%>
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
        <li style="margin-right: 22px; margin-left: 6px;">
            <div id="divLoadShowFinan" style="display: none; height: 435px !important;">
                <%--  <asp:UpdatePanel runat="server" ID="UpdFinanceiro" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                <ul id="ul11" class="ulDados" style="width: 746px !important;">
                    <li><span class="lblchkPgto" style="margin: 1px 10px 5px 0;">Lista de procedimentos</span>
                        <div style="height: 75px; overflow-y: auto;">
                            <asp:GridView runat="server" ID="grdDescPagamento" AutoGenerateColumns="false" Style="margin-top: 5px;
                                margin-right: 10px;">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum registro encontrado.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hidCoAgenda" Value='<%# Eval("Item1") %>' />
                                            <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# Eval("Item2") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Item1" HeaderText="PROCEDIMENTO">
                                        <ItemStyle Width="357px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Item2" HeaderText="VALOR">
                                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <%--  <label>
                            NOME</label>
                        <asp:TextBox runat="server" ID="txtNmProcedimento" Width="200px" Enabled="false"></asp:TextBox>--%>
                    </li>
                    <li style="margin-top: 13px;">
                        <label title="Valor base da consulta de acordo com o valor associado ao procedimento de consulta">
                            R$ Base</label>
                        <asp:TextBox runat="server" ID="txtVlBase" CssClass="campoMoeda" Width="50px" Enabled="false"
                            ToolTip="Valor base da consulta de acordo com o valor associado ao procedimento de consulta"></asp:TextBox>
                    </li>
                    <li style="margin-top: 13px;">
                        <label title="Valor de desconto à ser aplicado no valor calculado da consulta">
                            R$ Descto</label>
                        <asp:TextBox runat="server" ID="txtVlDscto" CssClass="campoMoeda" Width="50px" ToolTip="Valor de desconto à ser aplicado no valor calculado da consulta"
                            OnTextChanged="txtVlDscto_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                    </li>
                    <li style="width: 167px; margin-top: 13px;">
                        <label title="Valor da Consulta Calculado de acordo com a procedimento de consulta, operadora, plano de saúde e desconto informado">
                            R$ Líquido</label>
                        <asp:TextBox runat="server" ID="txtVlConsul" CssClass="campoMoeda" Width="50px" ToolTip="Valor da Consulta Calculado de acordo com a procedimento de consulta, operadora, plano de saúde e desconto informado"
                            Enabled="false"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtVlConsulOriginal" Visible="false"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Paciente</label>
                        <asp:TextBox runat="server" ID="txtNmPacienteMODFinan" Width="240px"></asp:TextBox>
                    </li>
                    <li style="clear: both;">
                        <div class="divEsquePgto">
                            <ul class="ulReceb">
                                <li style="clear: both; margin-left: 10px; margin-bottom: 10px">
                                    <asp:Label runat="server" ID="lblOutForPg" class="lblchkPgto">Outras Formas de Recebim.</asp:Label>
                                </li>
                                <li style="clear: both;">
                                    <asp:CheckBox ClientIDMode="Static" ID="chkDinhePgto" class="chkLocais" runat="server"
                                        Text="Dinheiro" />
                                    <asp:TextBox runat="server" ID="txtValDinPgto" Width="50px" CssClass="campoMoeda"
                                        Enabled="false" ClientIDMode="Static" Style="margin-left: 32px" ToolTip="Informar o valor total de recebimento em Dinheiro na Matrícula"></asp:TextBox>
                                </li>
                                <li style="clear: both;">
                                    <asp:CheckBox ClientIDMode="Static" ID="chkDepoPgto" CssClass="chkLocais" runat="server"
                                        Text="Depósito" />
                                    <asp:TextBox runat="server" ID="txtValDepoPgto" Width="50px" CssClass="campoMoeda"
                                        Enabled="false" ClientIDMode="Static" Style="margin-left: 31px" ToolTip="Informar o valor total de recebimento em Depósito Bancário na Matrícula"></asp:TextBox>
                                </li>
                                <li style="clear: both;">
                                    <asp:CheckBox ClientIDMode="Static" ID="chkDebConPgto" class="chkLocais" runat="server"
                                        Text="Déb. Conta" />
                                    <asp:TextBox runat="server" ID="txtValDebConPgto" Width="50px" CssClass="campoMoeda"
                                        Enabled="false" ClientIDMode="Static" ToolTip="Informar o valor total de recebimento em Débito em Conta na Matrícula"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="txtQtMesesDebConPgto" Width="15px" Enabled="false"
                                        ClientIDMode="Static" ToolTip="A Quantidade de meses que haverá o Débito em Conta"></asp:TextBox>
                                </li>
                                <li style="clear: both;">
                                    <asp:CheckBox ClientIDMode="Static" ID="chkTransPgto" class="chkLocais" runat="server"
                                        Text="Transferência" />
                                    <asp:TextBox runat="server" ID="txtValTransPgto" Width="50px" CssClass="campoMoeda"
                                        Enabled="false" ClientIDMode="Static" Style="margin-left: 9px" ToolTip="Informar o valor total de recebimento em Transferência Bancária na Matrícula"></asp:TextBox>
                                </li>
                                <li style="clear: both;">
                                    <asp:CheckBox ClientIDMode="Static" ID="chkOutrPgto" class="chkLocais" runat="server"
                                        Text="Outros" />
                                    <asp:TextBox runat="server" ID="txtValOutPgto" Width="50px" CssClass="campoMoeda"
                                        Enabled="false" ClientIDMode="Static" Style="margin-left: 42px" ToolTip="Informar o valor total de recebimento em Cobrança Bancária na Matrícula"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="txtObsOutPgto" Enabled="false" Width="141px" placeholder="Descrição Outros"
                                        ClientIDMode="Static" Style="margin-left: 6px;" MaxLength="80"></asp:TextBox>
                                    <%--<hr class="hrLinhaPgto" style="margin-top:6px"/>--%>
                                </li>
                                <li style="clear: both; margin-left: 4px">
                                    <div style="border-top: 1px solid #CCCCCC; width: 146px; height: 40px; margin-top: 10px;
                                        padding-top: 10px;">
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li>
                        <div class="divDirePgto">
                            <ul>
                                <li style="margin-bottom: 4px; margin-left: -4px;">
                                    <asp:CheckBox runat="server" ID="chkCartaoCreditoPgto" ClientIDMode="Static" Text="Cartão de Crédito"
                                        CssClass="chkDestaque" />
                                    <asp:Label runat="server" ID="lblCarCrePgto" class="lblchkPgto"></asp:Label>
                                </li>
                                <li style="clear: both;">
                                    <label>
                                        Bandeira</label>
                                    <ul>
                                        <li>
                                            <asp:DropDownList runat="server" ID="ddlBandePgto1" Enabled="false" ClientIDMode="Static">
                                                <asp:ListItem Text="Nenhum" Value="N"></asp:ListItem>
                                                <asp:ListItem Text="American Express" Value="AmeExp"></asp:ListItem>
                                                <asp:ListItem Text="Cartão BNDES" Value="BNDES"></asp:ListItem>
                                                <asp:ListItem Text="Diners Club" Value="DinClub"></asp:ListItem>
                                                <asp:ListItem Text="Elo" Value="Elo"></asp:ListItem>
                                                <asp:ListItem Text="HiperCard" Value="HipCar"></asp:ListItem>
                                                <asp:ListItem Text="MasterCard" Value="MasCar"></asp:ListItem>
                                                <asp:ListItem Text="SoroCred" Value="SorCr"></asp:ListItem>
                                                <asp:ListItem Text="Visa" Value="Vis"></asp:ListItem>
                                                <asp:ListItem Text="Outros" Value="Out"></asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                        <li style="clear: both; margin-top: 5px;">
                                            <asp:DropDownList runat="server" ID="ddlBandePgto2" Enabled="false" ClientIDMode="Static">
                                                <asp:ListItem Text="Nenhum" Value="N"></asp:ListItem>
                                                <asp:ListItem Text="American Express" Value="AmeExp"></asp:ListItem>
                                                <asp:ListItem Text="Cartão BNDES" Value="BNDES"></asp:ListItem>
                                                <asp:ListItem Text="Diners Club" Value="DinClub"></asp:ListItem>
                                                <asp:ListItem Text="Elo" Value="Elo"></asp:ListItem>
                                                <asp:ListItem Text="HiperCard" Value="HipCar"></asp:ListItem>
                                                <asp:ListItem Text="MasterCard" Value="MasCar"></asp:ListItem>
                                                <asp:ListItem Text="SoroCred" Value="SorCr"></asp:ListItem>
                                                <asp:ListItem Text="Visa" Value="Vis"></asp:ListItem>
                                                <asp:ListItem Text="Outros" Value="Out"></asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                        <li style="clear: both; margin-top: 5px;">
                                            <asp:DropDownList runat="server" ID="ddlBandePgto3" Enabled="false" ClientIDMode="Static">
                                                <asp:ListItem Text="Nenhum" Value="N"></asp:ListItem>
                                                <asp:ListItem Text="American Express" Value="AmeExp"></asp:ListItem>
                                                <asp:ListItem Text="Cartão BNDES" Value="BNDES"></asp:ListItem>
                                                <asp:ListItem Text="Diners Club" Value="DinClub"></asp:ListItem>
                                                <asp:ListItem Text="Elo" Value="Elo"></asp:ListItem>
                                                <asp:ListItem Text="HiperCard" Value="HipCar"></asp:ListItem>
                                                <asp:ListItem Text="MasterCard" Value="MasCar"></asp:ListItem>
                                                <asp:ListItem Text="SoroCred" Value="SorCr"></asp:ListItem>
                                                <asp:ListItem Text="Visa" Value="Vis"></asp:ListItem>
                                                <asp:ListItem Text="Outros" Value="Out"></asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <label>
                                        Número</label>
                                    <ul>
                                        <li>
                                            <asp:TextBox runat="server" ID="txtNumPgto1" CssClass="numeroCartao" Enabled="false"
                                                ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtNumPgto2" CssClass="numeroCartao" Enabled="false"
                                                ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtNumPgto3" CssClass="numeroCartao" Enabled="false"
                                                ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>
                                        Titular</label>
                                    <ul>
                                        <li>
                                            <asp:TextBox runat="server" ID="txtTitulPgto1" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtTitulPgto2" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtTitulPgto3" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>
                                        Venc</label>
                                    <ul>
                                        <li>
                                            <asp:TextBox runat="server" ID="txtVencPgto1" Width="30px" CssClass="campoVenc" Enabled="false"
                                                ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtVencPgto2" Width="30px" CssClass="campoVenc" Enabled="false"
                                                ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtVencPgto3" Width="30px" CssClass="campoVenc" Enabled="false"
                                                ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>
                                        R$ Crédito</label>
                                    <ul>
                                        <li>
                                            <asp:TextBox runat="server" ID="txtValCarPgto1" Width="50px" CssClass="campoMoeda"
                                                Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtValCarPgto2" Width="50px" CssClass="campoMoeda"
                                                Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtValCarPgto3" Width="50px" CssClass="campoMoeda"
                                                Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>
                                        Parcelas</label>
                                    <ul>
                                        <li>
                                            <asp:TextBox runat="server" ID="txtQtParcCC1" Width="15px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtQtParcCC2" Width="15px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtQtParcCC3" Width="15px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                                <li style="clear: both; margin-top: 10px; margin-bottom: 4px; margin-left: -4px;">
                                    <asp:CheckBox runat="server" ID="chkDebitPgto" ClientIDMode="Static" Text="Cartão de Débito"
                                        CssClass="chkDestaque" />
                                </li>
                                <li style="clear: both;">
                                    <label>
                                        Bco</label>
                                    <ul>
                                        <li>
                                            <asp:DropDownList runat="server" ID="ddlBcoPgto1" Enabled="false" ClientIDMode="Static">
                                            </asp:DropDownList>
                                        </li>
                                        <li style="clear: both; margin-top: 5px;">
                                            <asp:DropDownList runat="server" ID="ddlBcoPgto2" Enabled="false" ClientIDMode="Static">
                                            </asp:DropDownList>
                                        </li>
                                        <li style="clear: both; margin-top: 5px;">
                                            <asp:DropDownList runat="server" ID="ddlBcoPgto3" Enabled="false" ClientIDMode="Static">
                                            </asp:DropDownList>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <label>
                                        Agência</label>
                                    <ul>
                                        <li>
                                            <asp:TextBox runat="server" ID="txtAgenPgto1" Width="60px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtAgenPgto2" Width="60px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtAgenPgto3" Width="60px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>
                                        Nº Conta</label>
                                    <ul>
                                        <li>
                                            <asp:TextBox runat="server" ID="txtNContPgto1" Width="60px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtNContPgto2" Width="60px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtNContPgto3" Width="60px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>
                                        Número</label>
                                    <ul>
                                        <li>
                                            <asp:TextBox runat="server" ID="txtNuDebtPgto1" Width="80px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtNuDebtPgto2" Width="80px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtNuDebtPgto3" Width="80px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>
                                        Titular</label>
                                    <ul>
                                        <li>
                                            <asp:TextBox runat="server" ID="txtNuTitulDebitPgto1" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtNuTitulDebitPgto2" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtNuTitulDebitPgto3" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>
                                        R$ Débito</label>
                                    <ul>
                                        <li>
                                            <asp:TextBox runat="server" ID="txtValDebitPgto1" Width="50px" CssClass="maskDin"
                                                Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtValDebitPgto2" Width="50px" CssClass="maskDin"
                                                Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear: both; margin-top: -5px;">
                                            <asp:TextBox runat="server" ID="txtValDebitPgto3" Width="50px" CssClass="maskDin"
                                                Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li style="clear: both; margin: 10px 0 0 6px;">
                        <ul>
                            <li style="margin-bottom: 4px; margin-left: -4px;">
                                <asp:CheckBox runat="server" ID="chkChequePgto" CssClass="chkDestaque" Text="Cheque"
                                    OnCheckedChanged="chkChequePgto_OnCheckedChanged" AutoPostBack="true" />
                            </li>
                            <li style="clear: both;">
                                <div class="divGrdChequePgto">
                                    <asp:GridView ID="grdChequesPgto" Enabled="false" CssClass="grdBusca" runat="server"
                                        Style="width: 100%; height: 15px;" AutoGenerateColumns="false">
                                        <RowStyle CssClass="rowStyle" />
                                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkselectGridPgtoCheque" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bco">
                                                <ItemStyle HorizontalAlign="Center" Width="125px" />
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" Width="50px" ID="ddlBcoChequePgto" Style="margin: 0px !Important;">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Agência">
                                                <ItemStyle HorizontalAlign="Center" Width="125px" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAgenChequePgto" Style="margin: 0px; width: 50px;" runat="server"
                                                        Text='<%# Bind("AgenChe") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nº Conta">
                                                <ItemStyle HorizontalAlign="Center" Width="125px" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNrContaChequeConta" Style="margin: 0px; width: 50px;" runat="server"
                                                        Text='<%# Bind("nuConChe") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nº Cheque">
                                                <ItemStyle HorizontalAlign="Center" Width="125px" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNrChequePgto" Style="margin: 0px; width: 60px;" runat="server"
                                                        Text='<%# Bind("nuCheChe") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CPF">
                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNuCpfChePgto" CssClass="campoCpf" Style="margin: 0px; width: 75px;"
                                                        runat="server" Text='<%# Bind("nuCpfChe") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Titular">
                                                <ItemStyle HorizontalAlign="Center" Width="125px" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTitulChequePgto" Style="margin: 0px; width: 120px;" runat="server"
                                                        Text='<%# Bind("noTituChe") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="R$ Cheque">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtVlChequePgto" CssClass="campoMoeda" Style="margin: 0px; width: 40px;"
                                                        runat="server" Text='<%# Bind("vlCheChe") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dt Venc">
                                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDtVencChequePgto" Style="margin: 0px;" runat="server" CssClass="campoData"
                                                        Text='<%# Bind("dtVencChe") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </li>
                            <li style="clear: both">
                                <asp:LinkButton runat="server" ID="btnMaisLinhaChequePgto" OnClick="btnMaisLinhaChequePgto_OnClick">Cadastrar mais Cheques</asp:LinkButton>
                            </li>
                        </ul>
                    </li>
                    <%--<li id="li3" runat="server" class="liBtnAddA" style="margin-left: 610px;">
                                <asp:LinkButton ID="lnkEfetivaFinanceiro" Enabled="true" runat="server" OnClick="lnkEfetivaFinanceiro_OnClick">
                                    <asp:Label runat="server" ID="Label5" Text="CONFIRMAR FINANCEIRO" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>--%>
                    <%--</ul>
                        </div>--%>
                </ul>
                <%--  </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </li>
        <li>
            <div id="divLoadInfosCadas" style="display: none; height: 400px !important;">
                <ul class="ulDados" style="width: 350px !important;">
                    <div class="DivResp" runat="server" id="divResp">
                        <ul class="ulDadosResp" style="margin-left: -100px !important; width: 600px !important;">
                            <li style="margin: 0 0 -1px 0px">
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
                                    <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Pai/Mãe" Value="PM"></asp:ListItem>
                                    <asp:ListItem Text="Tio(a)" Value="TI"></asp:ListItem>
                                    <asp:ListItem Text="Avô/Avó" Value="AV"></asp:ListItem>
                                    <asp:ListItem Text="Primo(a)" Value="PR"></asp:ListItem>
                                    <asp:ListItem Text="Cunhado(a)" Value="CN"></asp:ListItem>
                                    <asp:ListItem Text="Tutor(a)" Value="TU"></asp:ListItem>
                                    <asp:ListItem Text="Irmão(ã)" Value="IR"></asp:ListItem>
                                    <asp:ListItem Text="Outros" Value="OU"></asp:ListItem>
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
                            <li style="margin: -45px 0 0 10px; float: right;">
                                <ul class="ulDadosContatosResp">
                                    <li>
                                        <asp:Label runat="server" ID="Label1" Style="font-size: 9px;" CssClass="lblObrigatorio">Dados de Contato</asp:Label>
                                    </li>
                                    <li style="clear: both;">
                                        <label>
                                            Tel. Fixo</label>
                                        <asp:TextBox runat="server" ID="txtTelFixResp" Width="70px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Tel. Celular</label>
                                        <asp:TextBox runat="server" ID="txtTelCelResp" Width="78px" CssClass="campoTel"></asp:TextBox>
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
                            <li style="clear: both; margin-left: -5px; margin-top: -6px; width: 80px;">
                                <ul>
                                    <li class="liFotoColab">
                                        <fieldset class="fldFotoColab">
                                            <uc1:ControleImagem ID="upImagemAluno" runat="server" />
                                        </fieldset>
                                    </li>
                                </ul>
                            </li>
                            <li style="margin: -4px 0 0 -23px; float: right;">
                                <ul class="ulDadosPaciente">
                                    <li style="margin-bottom: -4px;">
                                        <label class="lblTop">
                                            DADOS DO PACIENTE - USUÁRIO DE SAÚDE</label>
                                    </li>
                                    <li style="clear: both">
                                        <label>
                                            Nº NIS</label>
                                        <asp:TextBox runat="server" ID="txtNuNisPaci" Width="60" CssClass="txtNireAluno"></asp:TextBox>
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
                                        <asp:TextBox runat="server" ID="txtTelResPaci" Width="78px" CssClass="campoTel"></asp:TextBox>
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
                                    <li style="clear: both; margin-top: -5px; float: right">
                                        <label>
                                            Email</label>
                                        <asp:TextBox runat="server" ID="txtEmailPaci" Width="200px"></asp:TextBox>
                                    </li>
                                    <li style="clear: both; margin-top: -35px; margin-left: 2px">
                                        <asp:Label runat="server" ID="Label12" class="apelido">Apelido</asp:Label>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtApelido" MaxLength="25" Width="80px"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 3px; margin-top: -35px;">
                                        <label for="txtIndicacao" title="Indicação">
                                            Indicação
                                        </label>
                                        <asp:DropDownList ID="ddlIndicacao" Style="width: 194px;" runat="server" ToolTip="Indicação">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </li>
                            <li style="margin: 0px 0 0 75px;">
                                <ul>
                                    <li style="margin-left: 5px; margin-bottom: 2px;">
                                        <label class="lblSubInfos">
                                            PLANO DE SAÚDE</label>
                                    </li>
                                    <li style="clear: both">
                                        <label title="Unidade de Contrato" for="txtUnidade">
                                            Operadora</label>
                                        <asp:DropDownList ID="ddlOperadora" ToolTip="Selecione uma Operadora" CssClass=""
                                            OnSelectedIndexChanged="ddlOperadora_CheckedChanged" AutoPostBack="True" runat="server"
                                            Width="70px">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-bottom: 10px">
                                        <label title="Unidade de Contrato" for="txtUnidade">
                                            Plano</label>
                                        <asp:DropDownList ID="ddlPlanoS" ToolTip="Selecione uma Operadora" CssClass="" OnSelectedIndexChanged="ddlPlano_CheckedChanged"
                                            AutoPostBack="True" runat="server" Width="70px">
                                            <asp:ListItem Value="">Nenhuma</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="clear: both; margin-top: -37px; margin-left: 165px;">
                                        <label title="Unidade de Contrato" for="txtUnidade">
                                            Categoria</label>
                                        <asp:DropDownList ID="ddlCategoria" ToolTip="Selecione uma Operadora" CssClass=""
                                            runat="server" Width="70px">
                                            <asp:ListItem Value="">Nenhuma</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: -37px; margin-left: 245px;">
                                        <label for="txtNúmero" title="Número">
                                            Numero
                                        </label>
                                        <asp:TextBox ID="txtNumeroPlano" runat="server" CssClass="" Width="70px" MaxLength="22"> </asp:TextBox>
                                    </li>
                                    <li style="margin-top: -37px; margin-left: 330px;">
                                        <label title="Data de Vencimento do Plano">
                                            Dt Vencim.</label>
                                        <asp:TextBox runat="server" ID="txtDtVencPlan" CssClass="campoData" ToolTip="Data de Vencimento do Plano"></asp:TextBox>
                                    </li>
                                    <li style="margin-top: -37px; margin-left: 420px;">
                                        <label title="Data de Vencimento do Plano">
                                            N° Pasta.</label>
                                        <asp:TextBox runat="server" ID="TextBoxNumeroPasta" CssClass="" Width="70px" MaxLength="30"
                                            ToolTip="Número da pasta"></asp:TextBox>
                                    </li>
                                </ul>
                            </li>
                            <li id="li1" runat="server" class="liBtnAddA" style="margin-left: 219px;">
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
                <%--   </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </li>
        <li>
            <div id="divHoraAgenda" class="horaAgenda" style="display: none; height: 400px; width: 365px !important;">
                <div style="margin: 0 0 -1px 0px;">
                    <label style="color: #FAA460; font-weight: bold;">
                        Alterar horário</label>
                    <asp:UpdatePanel ID="upAgendamentoModal" runat="server">
                        <ContentTemplate>
                            <div style="margin-bottom: 10px;">
                                <label class="lblObrigatorio" style="padding-right: 3px">
                                    Hora</label>
                                <asp:TextBox runat="server" Width="27px" CssClass="campoHora" ID="TextBoxNovaHora"
                                    ToolTip="Hora do atendimento"></asp:TextBox>
                                <div id="frmAlterAgend">
                                    <label style="color: #FAA460; font-weight: bold;">
                                        Informações Plano de Saúde</label>
                                    <ul>
                                        <li style="clear: both;">
                                            <label>
                                                Operadora</label>
                                            <asp:DropDownList runat="server" ID="ddlOperadoraModal" ToolTip="Selecione a operadora/convênio"
                                                OnSelectedIndexChanged="ddlOperPlanoModal_OnSelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </li>
                                        <li>
                                            <label>
                                                Plano</label>
                                            <asp:DropDownList runat="server" ID="ddlPlanoModal" ToolTip="Selecione um plano">
                                            </asp:DropDownList>
                                        </li>
                                        <li>
                                            <label>
                                                Número</label>
                                            <asp:TextBox runat="server" ID="txtNumeroCartPlaModal" ToolTip="Informe o número do plano"
                                                Width="71px" MaxLength="12"></asp:TextBox>
                                        </li>
                                    </ul>
                                </div>
                                <%--  <label style="color: #FAA460; font-weight: bold;">
                                    Procedimento Agendado</label>--%>
                                <%--  <div>
                                    <label>
                                        Procedimento Consulta</label>
                                    <asp:DropDownList runat="server" ID="ddlProcedimentoModal" ToolTip="Selecione o Procedimento da consulta"
                                        Width="309px">
                                    </asp:DropDownList>
                                </div>--%>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:LinkButton ID="LinkButton1" ValidationGroup="ModeloReceita" CssClass="linkButton"
                        runat="server" Style="margin: 0 auto; display: table;" Text="SALVAR" OnClick="ButtonSalvarHorario_Click"
                        ToolTip="SALVAR" />
                </div>
            </div>
        </li>
    </ul>
    <div id="divSelProcedimento" style="display: none; height: 430px !important; width: 800px">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div style="margin: 10px 0 10px 0">
                    <label style="font-family: Tahoma; font-weight: bold; color: #8B1A1A; margin-top: 1px;">
                        LISTA DE PROCEDIMENTOS
                    </label>
                </div>
                <div style="width: 755px; height: 180px; border: 1px solid #CCC; overflow-y: scroll">
                    <asp:GridView ID="GrdProcedimentosPesquisa" CssClass="grdBusca" runat="server" AutoGenerateColumns="false"
                        DataKeyNames="ID_PROC_MEDI_PROCE" Style="width: 100%;">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum registro encontrado.<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="CK">
                                <ItemStyle Width="1px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckSelectProcedimento" CssClass="chk" runat="server" />
                                    <asp:HiddenField runat="server" ID="hidCoAgenda" Value='<%# Eval("ID_PROC_MEDI_PROCE") %>' />
                                    <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# Eval("CO_PROC_MEDI") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:BoundField DataField="ID_PROC_MEDI_PROCE" HeaderText="ID">
                                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                                    </asp:BoundField>--%>
                            <asp:BoundField DataField="CO_PROC_MEDI" HeaderText="Procedimento">
                                <ItemStyle Width="510px" HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div style="margin: 10px 0 10px 0">
                    <ul>
                        <li title="Clique para adicionar o procedimento" class="liBtnAddA" style="margin-left: 350px !important;
                            width: 75px;">
                            <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Procedimento" src="/Library/IMG/Gestor_SaudeEscolar.png"
                                height="15px" width="15px" />
                            <asp:LinkButton ID="lnkAddProcedimento" runat="server" OnClick="lnkAddProcedimento_OnClick">ADICIONAR</asp:LinkButton>
                        </li>
                    </ul>
                </div>
                <div style="margin: 10px 0 10px 0">
                    <label style="font-family: Tahoma; font-weight: bold; color: #8B1A1A; margin-top: 1px;
                        margin-right: 5px;">
                        PROCEDIMENTOS ADICIONADOS (<asp:Label ID="lblHorario" runat="server"></asp:Label>)
                    </label>
                </div>
                <div>
                    <div>
                        <div style="width: 755px; height: 120px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="GrdProcedimentosModal" CssClass="grdBusca" runat="server" AutoGenerateColumns="false"
                                DataKeyNames="ID_PROC_MEDI_PROCE" Style="width: 100%;">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum registro encontrado.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="CK">
                                <ItemStyle Width="1px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckSelectProcedimento" CssClass="chk" runat="server" />
                                   
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="EX">
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hidCoAgenda" Value='<%# Eval("ID_PROC_MEDI_PROCE") %>' />
                                            <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# Eval("CO_PROC_MEDI") %>' />
                                            <asp:ImageButton runat="server" ID="imgExcProcedimentoModal" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                ToolTip="Excluir Procedimento" OnClick="imgExcProcedimentoModal_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir esse item?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="ID_PROC_MEDI_PROCE" HeaderText="ID">
                                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                                    </asp:BoundField>--%>
                                    <asp:BoundField DataField="CO_PROC_MEDI" HeaderText="Procedimento">
                                        <ItemStyle Width="510px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div>
                    <%--     <label>
                                                    Procedimento Consulta</label>
                                                <asp:DropDownList runat="server" ID="ddlProcConsul" ToolTip="Selecione o Procedimento da consulta"
                                                    Width="279px">
                                                </asp:DropDownList>--%>
                    <asp:LinkButton ID="lnkOKProcedimentos" Enabled="true" runat="server" Style="float: right;
                        margin-top: 8px;" OnClientClick="$('#divSelProcedimento').dialog('close');">
                        <asp:Label runat="server" ID="Label5" Text="CONFIRMAR" Style="margin-left: 4px;"></asp:Label>
                    </asp:LinkButton>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <%--<div class="divCarregando">
                <asp:Image ID="imgCarregando" Visible="false" runat="server" AlternateText="Carregando" ImageAlign="Middle"
                    ImageUrl="~/Navegacao/Icones/carregando.gif" ToolTip="Carregando a solicitação" /></div>--%>
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

        $(document).ready(function () {
            carregaCss();
        });

        //Inserida função apra abertura de nova janela popup com a url do relatório que apresenta as guias
        function customOpen(url) {
            var w = window.open(url);
            w.focus();
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            carregaCss();
        });

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
            $(".campoData1").datepicker();
            $(".campoData1").mask("99/99/9999");
            $(".campoVenc").unmask();
            $(".campoVenc").mask("99/99");
            $(".numeroCartao").unmask();
            $(".numeroCartao").mask("9999.9999.9999.9999");
            $(".campoMoeda").unmask();
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            CarregaRegrasFinanceiras();

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
            $('#divLoadInfosCadas').dialog({ autoopen: false, modal: true, width: 652, height: 400, resizable: false, title: "USUÁRIO DE SAÚDE - CADASTRO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }
        function AbreModalHoraAgenda() {
            $('#divHoraAgenda').dialog({ autoopen: false, modal: true, width: 360, height: 145, resizable: false, title:

"ALTERAR DADOS DO AGENDAMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }
        function AbreModalInfosFinan() {
            $('#divLoadShowFinan').dialog({ autoopen: false, modal: true, width: 832, height: 540, resizable: false, title: "FORMA DE PAGAMENTO DE SERVIÇOS DE SAÚDE",
                //open: function () { $('#divLoadShowFinan').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                //                open: function (type, data) { $(this).parent().appendTo("form:first"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalSelProcedimento() {
            $('#divSelProcedimento').dialog({ autoopen: false, modal: true, width: 777, height: 470, resizable: false, title: "PROCEDIMENTOS",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function CarregaRegrasFinanceiras() {

            //======================================= PARTE RESPONSÁVEL PELA HABILITAÇÃO/DESABILITAÇÃO DE CAMPOS NOS FORMULÁRIOS =====================================


            //____________________________________TAB FORMA DE PAGAMENTO________________________________

            //Checkbox de recebimento em dinheiro
            $("#chkDinhePgto").click(function (evento) {
                if ($("#chkDinhePgto").attr("checked")) {
                    $("#txtValDinPgto").removeAttr('disabled');
                    $("#txtValDinPgto").css("background-color", "White");
                }
                else {
                    $("#txtValDinPgto").attr('disabled', true);
                    $("#txtValDinPgto").css("background-color", "#F5F5F5");
                    $("#txtValDinPgto").val("");
                }
            });

            //Checkbox de recebimento em Depósito Bancário
            $("#chkDepoPgto").click(function (evento) {
                if ($("#chkDepoPgto").attr("checked")) {
                    $("#txtValDepoPgto").removeAttr('disabled');
                    $("#txtValDepoPgto").css("background-color", "White");
                }
                else {
                    $("#txtValDepoPgto").attr('disabled', true);
                    $("#txtValDepoPgto").css("background-color", "#F5F5F5");
                    $("#txtValDepoPgto").val("");
                }
            });

            //Checkbox de recebimento em Débito em Conta Corrente
            $("#chkDebConPgto").click(function (evento) {
                if ($("#chkDebConPgto").attr("checked")) {
                    $("#txtValDebConPgto").removeAttr('disabled');
                    $("#txtValDebConPgto").css("background-color", "White");
                    $("#txtQtMesesDebConPgto").removeAttr('disabled');
                    $("#txtQtMesesDebConPgto").css("background-color", "White");
                }
                else {
                    $("#txtValDebConPgto").attr('disabled', true);
                    $("#txtValDebConPgto").css("background-color", "#F5F5F5");
                    $("#txtValDebConPgto").val("");
                    $("#txtQtMesesDebConPgto").attr('disabled', true);
                    $("#txtQtMesesDebConPgto").css("background-color", "#F5F5F5");
                    $("#txtQtMesesDebConPgto").val("");
                }
            });

            //Checkbox de recebimento em Transferência Bancária
            $("#chkTransPgto").click(function (evento) {
                if ($("#chkTransPgto").attr("checked")) {
                    $("#txtValTransPgto").removeAttr('disabled');
                    $("#txtValTransPgto").css("background-color", "White");
                }
                else {
                    $("#txtValTransPgto").attr('disabled', true);
                    $("#txtValTransPgto").css("background-color", "#F5F5F5");
                    $("#txtValTransPgto").val("");
                }
            });

            //Checkbox de recebimento em Boleto
            $("#chkOutrPgto").click(function (evento) {
                if ($("#chkOutrPgto").attr("checked")) {
                    $("#txtValOutPgto").removeAttr('disabled');
                    $("#txtValOutPgto").css("background-color", "White");

                    $("#txtObsOutPgto").removeAttr('disabled');
                    $("#txtObsOutPgto").css("background-color", "White");
                }
                else {
                    //                    $("#txtValDinPgto").attr('disabled', 'disabled');
                    $("#txtValOutPgto").attr('disabled', true);
                    $("#txtValOutPgto").css("background-color", "#F5F5F5");
                    $("#txtValOutPgto").val("");

                    $("#txtObsOutPgto").attr('disabled', true);
                    $("#txtObsOutPgto").css("background-color", "#F5F5F5");
                    $("#txtObsOutPgto").val("");
                    //                    document.getElementById("txtValDinPgto").disabled = true;
                }
            });


            //Dropdownlist do Cartão de Crédito LINHA 1
            $("#ddlBandePgto1").change(function (evento) {
                var e = document.getElementById("ddlBandePgto1");
                var itSelec = e.options[e.selectedIndex].value;
                if (itSelec == "N") {
                    //                if($('#ddlBandePgto1').val() == "N") {

                    $("#txtNumPgto1").attr('disabled', true);
                    $("#txtNumPgto1").css("background-color", "#F5F5F5");
                    $("#txtNumPgto1").val("");

                    $("#txtTitulPgto1").attr('disabled', true);
                    $("#txtTitulPgto1").css("background-color", "#F5F5F5");
                    $("#txtTitulPgto1").val("");

                    $("#txtVencPgto1").attr('disabled', true);
                    $("#txtVencPgto1").css("background-color", "#F5F5F5");
                    $("#txtVencPgto1").val("");

                    $("#txtQtParcCC1").attr('disabled', true);
                    $("#txtQtParcCC1").css("background-color", "#F5F5F5");
                    $("#txtQtParcCC1").val("");

                    $("#txtValCarPgto1").attr('disabled', true);
                    $("#txtValCarPgto1").css("background-color", "#F5F5F5");
                    $("#txtValCarPgto1").val("");
                }
                else {

                    $("#txtNumPgto1").removeAttr('disabled');
                    $("#txtNumPgto1").css("background-color", "White");

                    $("#txtTitulPgto1").removeAttr('disabled');
                    $("#txtTitulPgto1").css("background-color", "White");

                    $("#txtVencPgto1").removeAttr('disabled');
                    $("#txtVencPgto1").css("background-color", "White");

                    $("#txtQtParcCC1").removeAttr('disabled');
                    $("#txtQtParcCC1").css("background-color", "White");

                    $("#txtValCarPgto1").removeAttr('disabled');
                    $("#txtValCarPgto1").css("background-color", "White");
                }
            });

            //Dropdownlist do Cartão de Crédito LINHA 2
            $("#ddlBandePgto2").change(function (evento) {
                var e = document.getElementById("ddlBandePgto2");
                var itSelec = e.options[e.selectedIndex].value;
                if (itSelec == "N") {
                    //                if($('#ddlBandePgto1').val() == "N") {

                    $("#txtNumPgto2").attr('disabled', true);
                    $("#txtNumPgto2").css("background-color", "#F5F5F5");
                    $("#txtNumPgto2").val("");

                    $("#txtTitulPgto2").attr('disabled', true);
                    $("#txtTitulPgto2").css("background-color", "#F5F5F5");
                    $("#txtTitulPgto2").val("");

                    $("#txtVencPgto2").attr('disabled', true);
                    $("#txtVencPgto2").css("background-color", "#F5F5F5");
                    $("#txtVencPgto2").val("");

                    $("#txtQtParcCC2").attr('disabled', true);
                    $("#txtQtParcCC2").css("background-color", "#F5F5F5");
                    $("#txtQtParcCC2").val("");

                    $("#txtValCarPgto2").attr('disabled', true);
                    $("#txtValCarPgto2").css("background-color", "#F5F5F5");
                    $("#txtValCarPgto2").val("");
                }
                else {

                    $("#txtNumPgto2").removeAttr('disabled');
                    $("#txtNumPgto2").css("background-color", "White");

                    $("#txtTitulPgto2").removeAttr('disabled');
                    $("#txtTitulPgto2").css("background-color", "White");

                    $("#txtVencPgto2").removeAttr('disabled');
                    $("#txtVencPgto2").css("background-color", "White");

                    $("#txtQtParcCC2").removeAttr('disabled');
                    $("#txtQtParcCC2").css("background-color", "White");

                    $("#txtValCarPgto2").removeAttr('disabled');
                    $("#txtValCarPgto2").css("background-color", "White");
                }
            });

            //Dropdownlist do Cartão de Crédito LINHA 3
            $("#ddlBandePgto3").change(function (evento) {
                var e = document.getElementById("ddlBandePgto3");
                var itSelec = e.options[e.selectedIndex].value;
                if (itSelec == "N") {
                    //                if($('#ddlBandePgto1').val() == "N") {

                    $("#txtNumPgto3").attr('disabled', true);
                    $("#txtNumPgto3").css("background-color", "#F5F5F5");
                    $("#txtNumPgto3").val("");

                    $("#txtTitulPgto3").attr('disabled', true);
                    $("#txtTitulPgto3").css("background-color", "#F5F5F5");
                    $("#txtTitulPgto3").val("");

                    $("#txtVencPgto3").attr('disabled', true);
                    $("#txtVencPgto3").css("background-color", "#F5F5F5");
                    $("#txtVencPgto3").val("");

                    $("#txtQtParcCC3").attr('disabled', true);
                    $("#txtQtParcCC3").css("background-color", "#F5F5F5");
                    $("#txtQtParcCC3").val("");

                    $("#txtValCarPgto3").attr('disabled', true);
                    $("#txtValCarPgto3").css("background-color", "#F5F5F5");
                    $("#txtValCarPgto3").val("");
                }
                else {

                    $("#txtNumPgto3").removeAttr('disabled');
                    $("#txtNumPgto3").css("background-color", "White");

                    $("#txtTitulPgto3").removeAttr('disabled');
                    $("#txtTitulPgto3").css("background-color", "White");

                    $("#txtVencPgto3").removeAttr('disabled');
                    $("#txtVencPgto3").css("background-color", "White");

                    $("#txtQtParcCC3").removeAttr('disabled');
                    $("#txtQtParcCC3").css("background-color", "White");

                    $("#txtValCarPgto3").removeAttr('disabled');
                    $("#txtValCarPgto3").css("background-color", "White");
                }
            });

            //Dropdownlist do Cartão de Débito LINHA 1
            $("#ddlBcoPgto1").change(function (evento) {
                var e = document.getElementById("ddlBcoPgto1");
                var itSelec = e.options[e.selectedIndex].value;
                if (itSelec == "N") {

                    $("#txtAgenPgto1").attr('disabled', true);
                    $("#txtAgenPgto1").css("background-color", "#F5F5F5");
                    $("#txtAgenPgto1").val("");

                    $("#txtNContPgto1").attr('disabled', true);
                    $("#txtNContPgto1").css("background-color", "#F5F5F5");
                    $("#txtNContPgto1").val("");

                    $("#txtNuDebtPgto1").attr('disabled', true);
                    $("#txtNuDebtPgto1").css("background-color", "#F5F5F5");
                    $("#txtNuDebtPgto1").val("");

                    $("#txtNuTitulDebitPgto1").attr('disabled', true);
                    $("#txtNuTitulDebitPgto1").css("background-color", "#F5F5F5");
                    $("#txtNuTitulDebitPgto1").val("");

                    $("#txtValDebitPgto1").attr('disabled', true);
                    $("#txtValDebitPgto1").css("background-color", "#F5F5F5");
                    $("#txtValDebitPgto1").val("");
                }
                else {

                    $("#txtAgenPgto1").removeAttr('disabled');
                    $("#txtAgenPgto1").css("background-color", "White");

                    $("#txtNContPgto1").removeAttr('disabled');
                    $("#txtNContPgto1").css("background-color", "White");

                    $("#txtNuDebtPgto1").removeAttr('disabled');
                    $("#txtNuDebtPgto1").css("background-color", "White");

                    $("#txtNuTitulDebitPgto1").removeAttr('disabled');
                    $("#txtNuTitulDebitPgto1").css("background-color", "White");

                    $("#txtValDebitPgto1").removeAttr('disabled');
                    $("#txtValDebitPgto1").css("background-color", "White");
                }
            });

            //Dropdownlist do Cartão de Débito LINHA 2
            $("#ddlBcoPgto2").change(function (evento) {
                var e = document.getElementById("ddlBcoPgto2");
                var itSelec = e.options[e.selectedIndex].value;
                if (itSelec == "N") {

                    $("#txtAgenPgto2").attr('disabled', true);
                    $("#txtAgenPgto2").css("background-color", "#F5F5F5");
                    $("#txtAgenPgto2").val("");

                    $("#txtNContPgto2").attr('disabled', true);
                    $("#txtNContPgto2").css("background-color", "#F5F5F5");
                    $("#txtNContPgto2").val("");

                    $("#txtNuDebtPgto2").attr('disabled', true);
                    $("#txtNuDebtPgto2").css("background-color", "#F5F5F5");
                    $("#txtNuDebtPgto2").val("");

                    $("#txtNuTitulDebitPgto2").attr('disabled', true);
                    $("#txtNuTitulDebitPgto2").css("background-color", "#F5F5F5");
                    $("#txtNuTitulDebitPgto2").val("");

                    $("#txtValDebitPgto2").attr('disabled', true);
                    $("#txtValDebitPgto2").css("background-color", "#F5F5F5");
                    $("#txtValDebitPgto2").val("");
                }
                else {

                    $("#txtAgenPgto2").removeAttr('disabled');
                    $("#txtAgenPgto2").css("background-color", "White");

                    $("#txtNContPgto2").removeAttr('disabled');
                    $("#txtNContPgto2").css("background-color", "White");

                    $("#txtNuDebtPgto2").removeAttr('disabled');
                    $("#txtNuDebtPgto2").css("background-color", "White");

                    $("#txtNuTitulDebitPgto2").removeAttr('disabled');
                    $("#txtNuTitulDebitPgto2").css("background-color", "White");

                    $("#txtValDebitPgto2").removeAttr('disabled');
                    $("#txtValDebitPgto2").css("background-color", "White");
                }
            });

            //Dropdownlist do Cartão de Débito LINHA 3
            $("#ddlBcoPgto3").change(function (evento) {
                var e = document.getElementById("ddlBcoPgto3");
                var itSelec = e.options[e.selectedIndex].value;
                if (itSelec == "N") {

                    $("#txtAgenPgto3").attr('disabled', true);
                    $("#txtAgenPgto3").css("background-color", "#F5F5F5");
                    $("#txtAgenPgto3").val("");

                    $("#txtNContPgto3").attr('disabled', true);
                    $("#txtNContPgto3").css("background-color", "#F5F5F5");
                    $("#txtNContPgto3").val("");

                    $("#txtNuDebtPgto3").attr('disabled', true);
                    $("#txtNuDebtPgto3").css("background-color", "#F5F5F5");
                    $("#txtNuDebtPgto3").val("");

                    $("#txtNuTitulDebitPgto3").attr('disabled', true);
                    $("#txtNuTitulDebitPgto3").css("background-color", "#F5F5F5");
                    $("#txtNuTitulDebitPgto3").val("");

                    $("#txtValDebitPgto3").attr('disabled', true);
                    $("#txtValDebitPgto3").css("background-color", "#F5F5F5");
                    $("#txtValDebitPgto3").val("");
                }
                else {

                    $("#txtAgenPgto3").removeAttr('disabled');
                    $("#txtAgenPgto3").css("background-color", "White");

                    $("#txtNContPgto3").removeAttr('disabled');
                    $("#txtNContPgto3").css("background-color", "White");

                    $("#txtNuDebtPgto3").removeAttr('disabled');
                    $("#txtNuDebtPgto3").css("background-color", "White");

                    $("#txtNuTitulDebitPgto3").removeAttr('disabled');
                    $("#txtNuTitulDebitPgto3").css("background-color", "White");

                    $("#txtValDebitPgto3").removeAttr('disabled');
                    $("#txtValDebitPgto3").css("background-color", "White");
                }
            });

            //__________________________________CHECKBOX DA OPÇÃO DE CARTÃO DE CRÉDITO_______________________________
            $("#chkCartaoCreditoPgto").click(function (evento) {
                if ($("#chkCartaoCreditoPgto").attr("checked")) {

                    $("#ddlBandePgto1").removeAttr('disabled');
                    $("#ddlBandePgto1").css("background-color", "White");

                    $("#ddlBandePgto2").removeAttr('disabled');
                    $("#ddlBandePgto2").css("background-color", "White");

                    $("#ddlBandePgto3").removeAttr('disabled');
                    $("#ddlBandePgto3").css("background-color", "White");
                }
                else {

                    //Desabilita os DropDownList's de Bandeira
                    $("#ddlBandePgto1").attr('disabled', true);
                    $("#ddlBandePgto1").css("background-color", "#F5F5F5");
                    $("#ddlBandePgto1").val("N");

                    $("#ddlBandePgto2").attr('disabled', true);
                    $("#ddlBandePgto2").css("background-color", "#F5F5F5");
                    $("#ddlBandePgto2").val("N");

                    $("#ddlBandePgto3").attr('disabled', true);
                    $("#ddlBandePgto3").css("background-color", "#F5F5F5");
                    $("#ddlBandePgto3").val("N");

                    //Desabilita a primeira linha
                    $("#txtNumPgto1").attr('disabled', true);
                    $("#txtNumPgto1").css("background-color", "#F5F5F5");
                    $("#txtNumPgto1").val("");

                    $("#txtTitulPgto1").attr('disabled', true);
                    $("#txtTitulPgto1").css("background-color", "#F5F5F5");
                    $("#txtTitulPgto1").val("");

                    $("#txtVencPgto1").attr('disabled', true);
                    $("#txtVencPgto1").css("background-color", "#F5F5F5");
                    $("#txtVencPgto1").val("");

                    $("#txtQtParcCC1").attr('disabled', true);
                    $("#txtQtParcCC1").css("background-color", "#F5F5F5");
                    $("#txtQtParcCC1").val("");

                    $("#txtValCarPgto1").attr('disabled', true);
                    $("#txtValCarPgto1").css("background-color", "#F5F5F5");
                    $("#txtValCarPgto1").val("");

                    //Desabilita a segunda linha
                    $("#txtNumPgto2").attr('disabled', true);
                    $("#txtNumPgto2").css("background-color", "#F5F5F5");
                    $("#txtNumPgto2").val("");

                    $("#txtTitulPgto2").attr('disabled', true);
                    $("#txtTitulPgto2").css("background-color", "#F5F5F5");
                    $("#txtTitulPgto2").val("");

                    $("#txtVencPgto2").attr('disabled', true);
                    $("#txtVencPgto2").css("background-color", "#F5F5F5");
                    $("#txtVencPgto2").val("");

                    $("#txtQtParcCC2").attr('disabled', true);
                    $("#txtQtParcCC2").css("background-color", "#F5F5F5");
                    $("#txtQtParcCC2").val("");

                    $("#txtValCarPgto2").attr('disabled', true);
                    $("#txtValCarPgto2").css("background-color", "#F5F5F5");
                    $("#txtValCarPgto2").val("");

                    //Desabilita a terceira linha
                    $("#txtNumPgto3").attr('disabled', true);
                    $("#txtNumPgto3").css("background-color", "#F5F5F5");
                    $("#txtNumPgto3").val("");

                    $("#txtTitulPgto3").attr('disabled', true);
                    $("#txtTitulPgto3").css("background-color", "#F5F5F5");
                    $("#txtTitulPgto3").val("");

                    $("#txtVencPgto3").attr('disabled', true);
                    $("#txtVencPgto3").css("background-color", "#F5F5F5");
                    $("#txtVencPgto3").val("");

                    $("#txtQtParcCC3").attr('disabled', true);
                    $("#txtQtParcCC3").css("background-color", "#F5F5F5");
                    $("#txtQtParcCC3").val("");

                    $("#txtValCarPgto3").attr('disabled', true);
                    $("#txtValCarPgto3").css("background-color", "#F5F5F5");
                    $("#txtValCarPgto3").val("");
                }
            });

            //__________________________________CHECKBOX DA OPÇÃO DE CARTÃO DE DÉBITO_______________________________
            $("#chkDebitPgto").click(function (evento) {
                if ($("#chkDebitPgto").attr("checked")) {

                    $("#ddlBcoPgto1").removeAttr('disabled');
                    $("#ddlBcoPgto1").css("background-color", "White");

                    $("#ddlBcoPgto2").removeAttr('disabled');
                    $("#ddlBcoPgto2").css("background-color", "White");

                    $("#ddlBcoPgto3").removeAttr('disabled');
                    $("#ddlBcoPgto3").css("background-color", "White");
                }
                else {

                    //Desabilita os DropDownList's de Bandeira
                    $("#ddlBcoPgto1").attr('disabled', true);
                    $("#ddlBcoPgto1").css("background-color", "#F5F5F5");
                    $("#ddlBcoPgto1").val("N");

                    $("#ddlBcoPgto2").attr('disabled', true);
                    $("#ddlBcoPgto2").css("background-color", "#F5F5F5");
                    $("#ddlBcoPgto2").val("N");

                    $("#ddlBcoPgto3").attr('disabled', true);
                    $("#ddlBcoPgto3").css("background-color", "#F5F5F5");
                    $("#ddlBcoPgto3").val("N");

                    //Desabilita a primeira linha
                    $("#txtAgenPgto1").attr('disabled', true);
                    $("#txtAgenPgto1").css("background-color", "#F5F5F5");
                    $("#txtAgenPgto1").val("");

                    $("#txtNContPgto1").attr('disabled', true);
                    $("#txtNContPgto1").css("background-color", "#F5F5F5");
                    $("#txtNContPgto1").val("");

                    $("#txtNuDebtPgto1").attr('disabled', true);
                    $("#txtNuDebtPgto1").css("background-color", "#F5F5F5");
                    $("#txtNuDebtPgto1").val("");

                    $("#txtNuTitulDebitPgto1").attr('disabled', true);
                    $("#txtNuTitulDebitPgto1").css("background-color", "#F5F5F5");
                    $("#txtNuTitulDebitPgto1").val("");

                    $("#txtValDebitPgto1").attr('disabled', true);
                    $("#txtValDebitPgto1").css("background-color", "#F5F5F5");
                    $("#txtValDebitPgto1").val("");

                    //Desabilita a segunda linha
                    $("#txtAgenPgto2").attr('disabled', true);
                    $("#txtAgenPgto2").css("background-color", "#F5F5F5");
                    $("#txtAgenPgto2").val("");

                    $("#txtNContPgto2").attr('disabled', true);
                    $("#txtNContPgto2").css("background-color", "#F5F5F5");
                    $("#txtNContPgto2").val("");

                    $("#txtNuDebtPgto2").attr('disabled', true);
                    $("#txtNuDebtPgto2").css("background-color", "#F5F5F5");
                    $("#txtNuDebtPgto2").val("");

                    $("#txtNuTitulDebitPgto2").attr('disabled', true);
                    $("#txtNuTitulDebitPgto2").css("background-color", "#F5F5F5");
                    $("#txtNuTitulDebitPgto2").val("");

                    $("#txtValDebitPgto2").attr('disabled', true);
                    $("#txtValDebitPgto2").css("background-color", "#F5F5F5");
                    $("#txtValDebitPgto2").val("");

                    //Desabilita a terceira linha
                    $("#txtAgenPgto3").attr('disabled', true);
                    $("#txtAgenPgto3").css("background-color", "#F5F5F5");
                    $("#txtAgenPgto3").val("");

                    $("#txtNContPgto3").attr('disabled', true);
                    $("#txtNContPgto3").css("background-color", "#F5F5F5");
                    $("#txtNContPgto3").val("");

                    $("#txtNuDebtPgto3").attr('disabled', true);
                    $("#txtNuDebtPgto3").css("background-color", "#F5F5F5");
                    $("#txtNuDebtPgto3").val("");

                    $("#txtNuTitulDebitPgto3").attr('disabled', true);
                    $("#txtNuTitulDebitPgto3").css("background-color", "#F5F5F5");
                    $("#txtNuTitulDebitPgto3").val("");

                    $("#txtValDebitPgto3").attr('disabled', true);
                    $("#txtValDebitPgto3").css("background-color", "#F5F5F5");
                    $("#txtValDebitPgto3").val("");
                }
            });
            //____________________________________FIM TAB FORMA DE PAGAMENTO________________________________


            //_________________________ATACHA UMA FUNÇÃO DE CARREGANDO AO ELEMENTO_____________________
            $(".carregaAcao, select[name^='ddlNomeUsu'], input[name^='PesqGridAgenda'], select[name^='ddlClassProfi']").live('click change', function () {
                //exibe uma mensagem de carregando e bloqueia a tela
                carregarAcao(true);

            });
            //_________________________FIM ATACHA UMA FUNÇÃO DE CARREGANDO AO ELEMENTO_____________________
        }

    </script>
    </ul>
</asp:Content>
