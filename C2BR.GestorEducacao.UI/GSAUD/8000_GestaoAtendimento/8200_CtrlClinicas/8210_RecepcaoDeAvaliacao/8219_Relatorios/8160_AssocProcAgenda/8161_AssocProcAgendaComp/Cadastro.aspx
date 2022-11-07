<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8160_AssocProcAgenda._8161_AssocProcAgendaComp.Cadastro" %>

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
        .grdBusca1 th
        {
            background-color: #d2ffc2 !important;
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
        .liBtnFinAten
        {
            border: 1px solid #D2DFD1;
            float: right !Important;
            margin-top:5px;
            padding: 2px 8px 1px 8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li style="margin-top: -5px; margin-left: 44px;">
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
                        <asp:DropDownList ID="ddlNomeUsu" runat="server" ToolTip="Paciente para o qual a consulta será marcada"
                            Width="270px" OnSelectedIndexChanged="ddlNomeUsu_OnSelectedIndexChanged" AutoPostBack="true" Visible="false">
                        </asp:DropDownList>
                        <asp:TextBox style="margin-bottom:-10px; height:13px;" ID="txtNomePacPesq" Width="230px" ToolTip="Digite o nome ou parte do nome do paciente" runat="server" />
                        <asp:RequiredFieldValidator ValidationGroup="pesquisa" runat="server" ID="rfvNomeUsu" CssClass="validatorField"
                        ErrorMessage="O paciente é obrigatório" ControlToValidate="ddlNomeUsu" Display="Dynamic" />
                    </li>
                    </li>
                    <li style="margin-top: 12px; margin-left: -4px;">
                        <asp:ImageButton ID="imgbPesqPacNome" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                            OnClick="imgbPesqPacNome_OnClick" />
                        <asp:ImageButton ID="imgbVoltarPesq" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                            OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" />
                    </li>
                    <li>
                        <label for="txtNisUsu" title="Número NIS do usuário selecionado">
                            CNS/SUS</label>
                        <asp:TextBox ID="txtNisUsu" Enabled="false" runat="server" ToolTip="Número NIS do usuário selecionado"
                            Width="90px">
                        </asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Operadora</label>
                        <asp:DropDownList runat="server" ID="ddlOperadora" OnSelectedIndexChanged="ddlOperadora_OnSelectedIndexChanged"
                            AutoPostBack="true" Width="130px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            Plano</label>
                        <asp:DropDownList runat="server" ID="ddlPlano" Width="130px">
                        </asp:DropDownList>
                    </li>
                </ul>
            </div>
        </li>
        <li class="liSeparador"></li>
        <li>
            <div id="tabResConsultas" runat="server" clientidmode="Static">
                <ul id="ul10" style="margin-top: 6px !important; width: 1000px;">
                    <li style="width: 981px; float: left; margin-left: 6px;">
                        <%--   <asp:UpdatePanel runat="server" ID="UpdProfi" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                        <%-- Grid de profissionais --%>
                        <ul>
                            <%--<li class="liTituloGrid" style="width: 420px; margin-right: 0px; background-color: #ffff99;">
                                GRID DE PROFISSIONAIS </li>--%>
                            <li class="liTituloGrid" style="width: 100%; height: 24px !important; margin-right: 40px !important;
                                background-color: #E0EEEE; text-align: center; font-weight: bold; margin-bottom: -0px">
                                <div style="float: left; margin-left: 10px !important;">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                                        HISTÓRICO DE AGENDAMENTOS</label>
                                </div>
                                <div style="float: right; margin-top: 5px">
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
                                            <asp:CheckBox runat="server" ID="chkQua" Text="Qu" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Quartas"
                                                Checked="true" />
                                        </li>
                                        <li>
                                            <asp:CheckBox runat="server" ID="chkQui" Text="Qu" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Quintas"
                                                Checked="true" />
                                        </li>
                                        <li>
                                            <asp:CheckBox runat="server" ID="chkSex" Text="Se" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Sextas"
                                                Checked="true" />
                                        </li>
                                        <li>
                                            <asp:CheckBox runat="server" ID="chkSab" Text="Sa" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo os Sábados" />
                                        </li>
                                        <li style="margin-left: 10px;">
                                            <asp:Label runat="server" ID="lblPrf">
                                                Profissional</asp:Label>
                                            <asp:DropDownList runat="server" ID="ddlProfi" Width="180px">
                                            </asp:DropDownList>
                                        </li>
                                        <li style="margin-left: 14px;">
                                            <asp:TextBox ID="txtDtIniHistoUsuar" runat="server" CssClass="campoData">
                                            </asp:TextBox>
                                            à
                                            <asp:TextBox ID="txtDtFimHistoUsuar" runat="server" CssClass="campoData">
                                            </asp:TextBox>
                                        </li>
                                        <li style="margin-top: 0px; margin-left: 0px;">
                                            <asp:ImageButton ID="imgPesqHistPaciente" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                ValidationGroup="pesquisa" OnClick="imgPesqHistPaciente_OnClick" />
                                        </li>
                                    </ul>
                                </div>
                            </li>
                            <li style="margin-top: 5px">
                                <div id="divHistorPac" class="divGridData" style="height: 168px; width: 979px;
                                    overflow-y: scroll !important; border: 1px solid #ccc;">
                                    <input type="hidden" id="divHistorPac_posicao" name="divHistorPac_posicao" />
                                    <asp:GridView ID="grdHistorPaciente" CssClass="grdBusca" runat="server" Style="width: 100%;"
                                        AutoGenerateColumns="false" ToolTip="Grade de histórico de agendamentos do(a) Paciente selecionado"
                                        ShowHeaderWhenEmpty="true">
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
                                                    <asp:CheckBox runat="server" ID="chkSelectHist" OnCheckedChanged="chkSelectHist_OnCheckedChanged"
                                                        AutoPostBack="true" Enabled='<%# Eval("permiteClicar") %>' />
                                                    <asp:HiddenField runat="server" ID="hidIdAgend" Value='<%# Eval("ID_AGEND_HORAR") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DT_HORAR" HeaderText="DATA/HORA">
                                                <ItemStyle Width="110px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OPER" HeaderText="OPERADORA">
                                                <ItemStyle Width="160px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NO_PROFI" HeaderText="PROFISSIONAL">
                                                <ItemStyle Width="210px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TEL_COL_V" HeaderText="TELEFONE">
                                                <ItemStyle Width="70px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CO_CLASS_V" HeaderText="CLASSIFICAÇÃO">
                                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UNID_COL" HeaderText="UNID">
                                                <ItemStyle Width="40px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="ST">
                                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <img src='<%# Eval("IMG_URL") %>' alt="" style="width: 16px !important; height: 16px !important;
                                                        margin: 0 0 0 0 !important" title='<%# Eval("IMG_TIP") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </li>
                        </ul>
                        <%-- Grid de profissionais --%>
                        <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </li>
                    <li style="width: 980px; margin: 10px 0 0 7px !important; clear: both;">
                        <%--<asp:UpdatePanel runat="server" ID="UpdHora" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                        <%-- Grid de horário --%>
                        <ul>
                            <%--  <li class="liTituloGrid" style="width: 100%; margin-right: 0px; background-color: #d2ffc2;">
                                AGENDA DE HORÁRIOS </li>--%>
                            <li class="liTituloGrid" style="width: 100%; height: 24px !important; margin-right: 0px;
                                background-color: #bde5ae; text-align: center; font-weight: bold; margin-bottom: 0px">
                                <div style="float: left; margin: -3px 0 0 10px;">
                                    <label style="font-family: Tahoma; color: #5f9ea0 !important; font-weight: bold;
                                        font-size: 16px; margin-top: 5px;">
                                        ASSOCIAÇÃO</label>
                                </div>
                                <ul style="margin-top: 6px;">
                                    <li style="margin-left: 40px;">
                                        <label>
                                            GRUPO</label>
                                    </li>
                                    <li style="margin-left: 51px;">
                                        <label>
                                            SUBGRUPO</label>
                                    </li>
                                    <li style="margin-left: 33px;">
                                        <label>
                                            PROCEDIMENTO APLICADO AO PACIENTE</label>
                                    </li>
                                    <li style="margin-left: 135px;">
                                        <label>
                                            AÇÃO A SER REALIZADA PARA O PACIENTE NA DATA</label>
                                    </li>
                                    <li style="margin-left: 18px;">
                                        <label>
                                            EXC</label>
                                    </li>
                                </ul>
                            </li>
                            <li class="liTituloGrid" style="width: 100%; height: 24px !important; margin-right: 0px;
                                background-color: #d2ffc2; text-align: center; font-weight: bold; margin-bottom: 2px">
                                <div style="float: left; margin-left: 2px; margin-top: 4.5px;">
                                    <ul>
                                        <li style="margin-top: 1px; margin-left: 5px;">
                                            <asp:CheckBox runat="server" ID="chkReplicar" Text=" Todas" CssClass="chk"
                                                OnCheckedChanged="chkReplicar_OnCheckedChanged" AutoPostBack="true" />
                                        </li>
                                        <li style="margin-left: 90px;">
                                            <asp:DropDownList runat="server" ID="ddlGrupoPr" OnSelectedIndexChanged="ddlGrupoPr_OnSelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </li>
                                        <li style="margin-left: 3px;">
                                            <asp:DropDownList runat="server" ID="ddlSubGrupoPr" OnSelectedIndexChanged="ddlSubGrupoPr_OnSelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </li>
                                        <li style="margin-left: 3px;">
                                            <asp:DropDownList runat="server" ID="ddlProcedimento" OnSelectedIndexChanged="ddlProcedimento_OnSelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </li>
                                        <li style="margin-left: 3px;">
                                            <asp:TextBox runat="server" ID="txtDesProcedimento" Width="220px" Enabled="false"></asp:TextBox>
                                        </li>
                                        <li style="margin-left: 3px;">
                                            <asp:TextBox runat="server" ID="txtNrAcao" Width="25px" Enabled="false"></asp:TextBox>
                                        </li>
                                        <li style="margin-left: 3px;">
                                            <asp:TextBox runat="server" ID="txtDeAcao" Width="250px" OnTextChanged="txtDeAcao_OnTextChanged"
                                                AutoPostBack="true"></asp:TextBox>
                                        </li>
                                    </ul>
                                </div>
                                <div style="float: right; margin-top: 4px;">
                                    <ul>
                                    </ul>
                                </div>
                            </li>
                            <li style="margin-top: 1px">
                                <%-- <asp:CheckBox ID="chkMarcaTodosItens" runat="server" OnCheckedChanged="ChkTodos_OnCheckedChanged"
                                    AutoPostBack="true"></asp:CheckBox>--%>
                                <div id="divItens" class="divGridData" style="height: 110px; width: 980px;
                                    border: 1px solid #ccc;">
                                    <input type="hidden" id="divItens_posicao" name="divItens_posicao" />
                                    <asp:HiddenField runat="server" ID="hidCoConsul" />
                                    <asp:GridView ID="grdHorario" CssClass="grdBusca1 grdBusca" runat="server" AutoGenerateColumns="false"
                                        Style="width: 100%;" ShowHeaderWhenEmpty="true" ShowHeader="false" OnRowDataBound="grdHorario_OnRowDataBound">
                                        <RowStyle CssClass="rowStyle" />
                                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                        <EmptyDataTemplate>
                                            Nenhum registro encontrado.<br />
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="CK">
                                                <ItemStyle Width="5px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ckSelectHr" CssClass="chk" runat="server" OnCheckedChanged="ckSelectHr_OnCheckedChanged"
                                                        AutoPostBack="true" />
                                                    <asp:HiddenField runat="server" ID="hidIdItemPlane" Value='<%# Eval("ID_ITEM_PLAN") %>' />
                                                    <asp:HiddenField runat="server" ID="hidSituaItemPlanej" Value='<%# Eval("hidSituaItemPlanej") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="hora" HeaderText="DATA E HORA">
                                                <ItemStyle Width="120px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlGrupo" Style="margin: 0 0 0 0 !important;"
                                                        OnSelectedIndexChanged="ddlGrupo_OnSelectedIndexChanged" AutoPostBack="true"
                                                        Enabled="false">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField runat="server" ID="hidIdGrupo" Value='<%# Eval("ID_GRUPO")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlSubGrupo" Style="margin: 0 0 0 0 !important;"
                                                        OnSelectedIndexChanged="ddlSubGrupo_OnSelectedIndexChanged" AutoPostBack="true"
                                                        Enabled="false">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField runat="server" ID="hidIdSubGrupo" Value='<%# Eval("ID_SUB_GRUPO")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlProcedimento" Style="margin: 0 0 0 0 !important;"
                                                        OnSelectedIndexChanged="ddlProcedimentoGr_OnSelectedIndexChanged" AutoPostBack="true"
                                                        Enabled="false">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField runat="server" ID="hidIdProced" Value='<%# Eval("ID_PROCED")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DES">
                                                <ItemStyle Width="220px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" Text='<%# Eval("NM_PROCED") %>' ID="txtDesProced" Width="220px"
                                                        Style="margin: 0 0 0 0 !important;" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AÇÃO">
                                                <ItemStyle Width="25px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtNuAcao" Width="25px" Style="margin: 0 0 0 0 !important;
                                                        text-align: center" Text='<%# Eval("NR_ACAO_V") %>' Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DESCRIÇÃO AÇÃO">
                                                <ItemStyle Width="250px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtDesAcao" Width="250px" Style="margin: 0 0 0 0 !important;"
                                                        Text='<%# Eval("DE_ACAO") %>' Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CAN">
                                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ID="imgExc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                        ToolTip="Cancelar item de planejamento" OnClientClick="return confirm('Tem certeza de que deseja EXCLUIR o item de planejamento ?')"
                                                        OnClick="imgExc_OnClick" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </li>
                            <li class="liBtnFinAten" style="margin-right: 15px; background-color: #5882FA;">
                                <img alt="" class="imgAdd" title="Adicionar novo item de procedimento" src="/Library/IMG/Gestor_SaudeEscolar.png"
                                    height="15px" width="15px" />
                                <asp:LinkButton runat="server" ID="btnMaisItens" Style="margin-left:2px;" ToolTip="Adicionar novo item de procedimento"
                                    Enabled="true" OnClick="btnMaisItens_OnClick" ForeColor="White" Font-Bold="true">INSERIR</asp:LinkButton>
                            </li>
                            <li class="liBtnFinAten" style="margin-right: 15px; background-color: #F78181;">
                                <img alt="" class="imgAdd" title="Excluir itens de procedimento selecionados" src="/Library/IMG/Gestor_BtnDel.png"
                                    height="15px" width="15px" />
                                <asp:LinkButton runat="server" ID="btnExcProcs" Style="margin-left:2px;" ToolTip="Excluir itens de procedimento selecionados"
                                    Enabled="true" OnClick="btnExcProcs_OnClick" ForeColor="White" Font-Bold="true">EXCLUIR</asp:LinkButton>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divLoadInfosCadas" style="display: none; height: 300px !important;">
                <%--<asp:UpdatePanel runat="server" ID="updCadasUsuario" UpdateMode="Conditional">
                    <ContentTemplate>--%>
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
                            <li style="margin: -5px 0 0 10px;">
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
                                            Nº CNES</label>
                                        <asp:TextBox runat="server" ID="txtNuNisPaci" Width="60" CssClass="txtNIS"></asp:TextBox>
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
                                    <li style="clear: both; margin-top: -7px; float: right">
                                        <asp:Label runat="server" ID="lblEmailPaci">Email</asp:Label>
                                        <asp:TextBox runat="server" ID="txtEmailPaci" Width="220px"></asp:TextBox>
                                    </li>
                                </ul>
                            </li>
                            <li id="li1" runat="server" class="liBtnAddA" style="margin-left: 438px;">
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
    </ul>
    <script type="text/javascript">

        window.onload = function () {
            maintainScroll1();
            maintainScroll2();
        }

        function maintainScroll1() {
            var div = document.getElementById("divHistorPac");
            var div_position = document.getElementById("divHistorPac_posicao_posicao");
            var position = parseInt('<%= Request.Form["divHistorPac_posicao_posicao"] %>');
            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }

        function maintainScroll2() {
            var div = document.getElementById("divHistorPac");
            var div_position = document.getElementById("divItens_posicao");
            var position = parseInt('<%= Request.Form["divItens_posicao"] %>');
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
            $(".campoData").datepicker();
            $(".campoData").mask("99/99/9999");
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
            $('#divLoadInfosCadas').dialog({ autoopen: false, modal: true, width: 652, height: 350, resizable: false, title: "USUÁRIO DE SAÚDE - CADASTRO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
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

        }
    </script>
</asp:Content>
