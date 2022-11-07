<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8269_AtendimentoHospitalar.Cadastro" %>
<%@ Register assembly="DevExpress.Web.ASPxHtmlEditor.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHtmlEditor" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxSpellChecker.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpellChecker" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!--CSS-->
    <style type="text/css">
        .divAvisoPermissao
        {
            top: 516px !important;
            left: 390px !important;
        }
        
        .ulDados
        {
            width: 1050px;
        }
        
        .ulDados li
        {
            margin-left: 5px;
            margin-bottom: 5px;
        }
        
        .ulDadosLog li
        {
            float: left;
            margin-left: 10px;
        }
        
        .ulPer label
        {
            text-align: left;
        }
        
        label
        {
            margin-bottom: 1px;
        }
        
        label[for="ctl00_content_divEncaminAtendCheck"]
        {
            display: inline-block;
        }
        
        input
        {
        }
        
        .ulDadosGerais li
        {
            margin-left: 5px;
        }
        
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
        }
        
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: left;
        }
        
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            padding: 2px 3px 1px 3px;
        }
        
        .chk label
        {
            display: inline;
            margin-left: -4px;
        }
        
        .chk label
        {
            display: inline;
        }
        
        .liBtnConfirmarCiencia
        {
            width: 47px;
            background-color: #d09ad1;
            margin-left: 115px;
            margin-top: 10px;
            cursor: pointer;
            border: 1px solid #8B8989;
            padding: 4px 3px 3px;
        }
        
        .liBtnConfirm
        {
            margin-top: 10px;
            margin-left: 2px;
            padding: 4px 3px 3px;
            background-color: #EE9572;
            border: 1px solid #8B8989;
        }
        
        #divCronometro
        {
            text-align: center;
            background-color: #FFE1E1;
            float: left;
            margin-left: 201px;
            margin-top: -26px;
            width: 115px;
            margin-right: -130px;
            display: none;
        }
        
        .LabelHora
        {
            margin-top: 4px;
            font-size: 10px;
        }
        
        .Hora
        {
            font-family: Trebuchet MS;
            font-size: 23px;
            color: #9C3535;
            margin-top: -3px;
        }
        .spInf
        {
            cursor: pointer;
        }
        #cklExameFis tr td
        {
            display: flex;
        }
        .lilnkExaFis
        {
            float: right !important;
            width: 48px;
            height: 15px;
            text-align: -webkit-center;
            border: solid 1px #CCC;
            margin: 0;
            background: #f0f8ff;
        }
        .lnkExcluir
        {
            border: none;
            background-color: #fff;
            color: red;
            font-size: 12px;
            cursor: pointer;
        }
        .modalInf
        {
            z-index: 2000;
            position: absolute;
            background-color: #faebd7;
            top: 34%;
            left: 26%;
            width: 474px;
        }
        .invible
        {
            display: none;
        }
        .indice
        {
            z-index: 2000;
            position: absolute;
            background-color: #fff;
            top: 34%;
            left: 26%;
            padding: 0 20px 20px 20px;
            border-radius: 5px;
        }
        #divBack
        {
            z-index: 1;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            position: absolute;
            opacity: 0.3;
            display: none;
            background: url(images/ui-bg_diagonals-small_100_000000_40x40.png) 50% 50% repeat rgb(0, 0, 0);
        }
        .parecerTec
        {
            z-index: 1000;
            position: absolute;
            background-color: #fff;
            top: 10%;
            left: 19%;
            padding: 0 20px 20px 20px;
            border-radius: 5px;
        }
        .centerCHC
        {
            text-align: center !important;
        }
        .rowColor
        {
            background-color: red;
        }
        .btnProcurar
        {
            margin-top: -3px;
        }
        .mgLeft
        {
            margin-left: 169px;
        }
        .txtNomePacienteInternar
        {
            height: 23px;
            border-radius: 2px;
            background-color: #f6f6f6;
        }
        .colorTextBlue
        {
            color: #0000ff;
        }
        .colorTextRed
        {
            color: #cd0a0a;
        }
        .magin0
        {
            margin: 0 !important;
        }
        .border0
        {
            border: none;
        }
        .btnSalvarAtend
        {
            height: 17px;
            background-color: rgba(251, 236, 135, 0.77);
            margin-left: 0px;
            width: 140px;
            margin-bottom: 5px;
            border: 1px solid #CCC;
            padding: 1px;
        }
    </style>
    <script src="../../../../../Library/JS/TreeView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<div id="editorReceituario" style="position: absolute;
    z-index: 999998;
    margin-left: 130px;
    margin-top: 0px;
    display:none" >
        <dx:ASPxHtmlEditor ID="ASPxHtmlEditor1" Height="350px" Width="750px" runat="server" Theme="Aqua">
            <Settings AllowDesignView="False" AllowHtmlView="False" AllowPreview="False" />
        </dx:ASPxHtmlEditor> 
</div>
<div id="editorExame" style="position: absolute;
    z-index:9999;
    margin-left: 130px;
    margin-top: 0px;
    display:none" >
        <dx:ASPxHtmlEditor ID="ASPxHtmlEditorExame" Height="350px" Width="750px" runat="server" Theme="Aqua">
            <Settings AllowDesignView="False" AllowHtmlView="False" AllowPreview="False" />
        </dx:ASPxHtmlEditor> 
</div>
<div id="editorProcedimento" style="position: absolute;
    z-index:9999;
    margin-left: 100px;
    margin-top: 2px;
    display:none" >
        <dx:ASPxHtmlEditor ID="ASPxHtmlEditorProcedimento" Height="350px" Width="800px" runat="server" Theme="Aqua">
            <Settings AllowDesignView="False" AllowHtmlView="False" AllowPreview="False" />
        </dx:ASPxHtmlEditor> 
</div>

<div id="editorEncam" style="position: absolute;
    z-index: 999998;
    margin-left: 130px;
    margin-top: 0px;
    display:none" >
        <dx:ASPxHtmlEditor ID="ASPxHtmlEditorEncam" Height="350px" Width="750px" runat="server" Theme="Aqua">
            <Settings AllowDesignView="False" AllowHtmlView="False" AllowPreview="False" />
        </dx:ASPxHtmlEditor> 
    </div>

    <asp:ScriptManager ID="sm" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hidIdAtendimento" />
    <div id="divBack">
    </div>
    <div id="dvLocal" style="margin-bottom: -17px; margin-left: 14px">
        <label style="margin-bottom: -15px;" title="Escolha o local de atendimento de Pacientes">
            LOCAL</label>
        <asp:DropDownList runat="server" ToolTip="Escolha o local de atendimento de Pacientes"
            ID="ddlLocal" AutoPostBack="true" Style="width: auto; margin-left: 35px;" OnSelectedIndexChanged="ddlLocal_OnSelectedIndexChanged">
        </asp:DropDownList>
        <asp:Button ID="btnSalvaAtend" CssClass="btnSalvarAtend" runat="server" Text="SALVAR ATENDIMENTO" OnClick="BtnSalvar_OnClick"
            Style="float: right; margin-right: 14px; margin-top: 2px;" ToolTip="Salvar o atendimento em andamento do Paciente" />
    </div>
    <div id="divCronometro">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hidTimerId" runat="server" Value="" ClientIDMode="Static" />
                <asp:HiddenField ID="hidHoras" runat="server" Value="" ClientIDMode="Static" />
                <asp:HiddenField ID="hidMinutos" runat="server" Value="" ClientIDMode="Static" />
                <asp:HiddenField ID="hidSegundos" runat="server" Value="" ClientIDMode="Static" />
                <label class="LabelHora">
                    Tempo de Atendimento</label>
                <label id="lblHora" class="Hora">
                    00:00:00</label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <%--<div style="float: right; margin-right: 10px; margin-top: -18px; color: Green;">
        <asp:CheckBox ID="chkSalvarAutomat" Text="Salvar Atendimento Automaticamente" CssClass="chk"
            runat="server" />
    </div>--%>
    <ul class="ulDados">
        <li style="margin-left: 3px;">
            <ul>
                <li>
                    <ul>
                        <li class="liTituloGrid" style="width: 479px !important; height: 20px !important;
                            margin-right: 0px; background-color: #ADD8E6; text-align: center; font-weight: bold;
                            margin-bottom: 2px; padding-top: 2px;">
                            <ul>
                                <li style="margin: 0px 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: #FFF">
                                        GRID DE PACIENTES</label>
                                </li>
                                <li style="margin-left: 10px; float: right; margin-top: 2px;">
                                    <ul class="ulPer">
                                        <li>
                                            <asp:TextBox runat="server" ID="txtNomePacPesqAgendAtend" Width="110px" placeholder="Pesquise pelo Nome"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:TextBox runat="server" class="campoData" ID="IniPeriAG" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                                                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeriAG"></asp:RequiredFieldValidator>
                                            <asp:Label runat="server" ID="Label4"> &nbsp à &nbsp </asp:Label>
                                            <asp:TextBox runat="server" class="campoData" ID="FimPeriAG" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                                                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeriAG"></asp:RequiredFieldValidator><br />
                                        </li>
                                        <li style="margin: 0px 2px 0 -2px;">
                                            <asp:ImageButton ID="imgPesqAgendamentos" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                Width="13px" Height="13px" OnClick="imgPesqAgendamentos_OnClick" />
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li style="clear: both">
                            <div style="width: 477px; height: 120px; border: 1px solid #CCC; overflow-y: scroll"
                                id="divAgendasRecp">
                                <input type="hidden" id="divAgendasRecp_posicao" name="divAgendasRecp_posicao" />
                                <asp:HiddenField runat="server" ID="hidIdAgenda" />
                                <asp:GridView ID="grdPacientes" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                    ShowHeaderWhenEmpty="true">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhuma solicitação em Aberto<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CK">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidIdAgenda" Value='<%# Eval("ID_AGEND_HORAR") %>' runat="server" />
                                                <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# ("CO_ALU") %>' />
                                                <asp:HiddenField runat="server" ID="hidSituacao" Value='<%# ("Situacao") %>' />
                                                <asp:CheckBox ID="chkSelectPaciente" runat="server" Enabled='<%# Eval("podeClicar") %>'
                                                    Width="100%" Style="margin: 0 0 0 -15px !important;" OnCheckedChanged="chkSelectPaciente_OnCheckedChanged"
                                                    AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DTHR" HeaderText="HORÁRIO">
                                            <ItemStyle HorizontalAlign="Center" Width="110px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="PACIENTE">
                                            <ItemStyle Width="300px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblNomPaci" Text='<%# Eval("PACIENTE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SX" HeaderText="SX">
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IDADE" HeaderText="IDADE">
                                            <ItemStyle HorizontalAlign="Left" Width="85px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="CR">
                                            <ItemStyle Width="10px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <ul>
                                                    <li>
                                                        <div id="Div1" runat="server" visible='<%# Eval("DIV_1") %>' style="background-color: Red;
                                                            width: 10px; height: 10px; margin: 0px;" title="Atendimento classificado como Emergência">
                                                        </div>
                                                        <div id="Div2" runat="server" visible='<%# Eval("DIV_2") %>' style="background-color: Orange;
                                                            width: 10px; height: 10px; margin: 0px" title="Atendimento classificado como Muito Urgente">
                                                        </div>
                                                        <div id="Div3" runat="server" visible='<%# Eval("DIV_3") %>' style="background-color: Yellow;
                                                            width: 10px; height: 10px; margin: 0px" title="Atendimento classificado como Urgente">
                                                        </div>
                                                        <div id="Div4" runat="server" visible='<%# Eval("DIV_4") %>' style="background-color: Green;
                                                            width: 10px; height: 10px; margin: 0px" title="Atendimento classificado como Pouco Urgente">
                                                        </div>
                                                        <div id="Div5" runat="server" visible='<%# Eval("DIV_5") %>' style="background-color: Blue;
                                                            width: 10px; height: 10px; margin: 0px" title="Atendimento classificado como Não Urgente">
                                                        </div>
                                                    </li>
                                                </ul>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CNT">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgContratacao" ImageUrl='<%# Eval("tpContr_URL") %>' ToolTip='<%# Eval("tpContr_TIP") %>'
                                                    Width="17px" Height="17px" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ST">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgSituacao" ImageUrl='<%# Eval("imagem_URL") %>'
                                                    ToolTip='<%# Eval("imagem_TIP") %>' Style="width: 18px !important; height: 18px !important;
                                                    margin: 0 0 0 0 !important" OnClick="imgSituacao_OnClick" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
                <li style="">
                    <ul style="">
                        <li class="liTituloGrid" style="width: 479px; height: 20px !important; margin-right: 0px;
                            background-color: #c1ffc1; text-align: center; font-weight: bold; margin-bottom: 2px;
                            padding-top: 2px; margin-left: 5px">
                            <ul>
                                <li style="margin: 0px 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                        DEMONSTRATIVO - AGENDA DO PACIENTE</label>
                                </li>
                                <li style="margin-left: 18px; float: right; margin-top: 2px;">
                                    <ul class="ulPer">
                                        <li>
                                            <asp:TextBox runat="server" class="campoData" ID="txtIniAgenda" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                                            <asp:Label runat="server" ID="Label111"> &nbsp à &nbsp </asp:Label>
                                            <asp:TextBox runat="server" class="campoData" ID="txtFimAgenda" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                                        </li>
                                        <li style="margin: 0px 2px 0 -2px;">
                                            <asp:ImageButton ID="imgPesqHistAgend" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                Width="13px" Height="13px" OnClick="imgPesqHistAgend_OnClick" />
                                        </li>
                                    </ul>
                                </li>
                                <li style="clear: both; margin: -14px 0 0 0px;">
                                    <div id="divDemonAge" style="width: 477px; height: 120px; border: 1px solid #CCC;
                                        overflow-y: scroll">
                                        <input type="hidden" id="divDemonAge_posicao" name="divDemonAge_posicao" />
                                        <asp:GridView ID="grdHistoricoAgenda" CssClass="grdBusca" runat="server" Style="width: 100%;
                                            cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                            ShowHeaderWhenEmpty="true">
                                            <RowStyle CssClass="rowStyle" />
                                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                            <EmptyDataTemplate>
                                                Nenhum agendamento para este paciente nos parâmetros acima<br />
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="CK">
                                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="chkSelectHistAge" Enabled='<%# Eval("podeClicar") %>'
                                                            Width="100%" Style="margin: 0 0 0 -15px !important;" OnCheckedChanged="chkSelectHistAge_OnCheckedChanged"
                                                            AutoPostBack="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ST">
                                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgSituacaoHistorico" Style="width: 18px !important;
                                                            height: 18px !important; margin: 0 0 0 0 !important" ImageUrl='<%# Eval("imagem_URL") %>'
                                                            OnClick="imgSituacaoHistorico_OnClick" ToolTip='<%# Eval("imagem_TIP") %>' />
                                                        <asp:HiddenField runat="server" ID="hidIdAgenda" Value='<%# Eval("ID_AGENDA") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="dtAgenda_V" HeaderText="AGENDA">
                                                    <ItemStyle HorizontalAlign="Left" Width="115px" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PROFISSIONAL" HeaderText="PROFISSIONAL">
                                                    <ItemStyle HorizontalAlign="Left" Width="95px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DE_ACAO" HeaderText="AÇÃO ATENDIMENTO">
                                                    <ItemStyle HorizontalAlign="Left" Width="280px" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="FA">
                                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgbFicha" ImageUrl="/BarrasFerramentas/Icones/Imprimir.png"
                                                            ToolTip="Ficha de Atendimento" Style="width: 18px !important; height: 18px !important;
                                                            margin: 0 0 0 0 !important" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li>
            <ul style="margin-left: -1px;">
                <li>
                    <ul>
                        <li style="width: 668px; color: Blue; border-bottom: 2px solid #58ACFA; margin-bottom: 0px;
                            margin-top: -11px;">
                            <label style="font-size: 12px;">
                                Informações Prévias do Paciente</label>
                        </li>
                        <li style="clear: both; margin-left: 7px; margin-bottom: -8px; width: 25px;">
                            <label>
                                Altura</label>
                            <asp:TextBox ID="txtAltura" CssClass="campoAltura" Width="30" runat="server" />
                        </li>
                        <li style="margin: 3px -2px 0 6px; height: 23px; width: 1px; border-left: 1px solid #BDBDBD;">
                        </li>
                        <li style="margin-bottom: -8px; width: 25px;">
                            <label>
                                Peso</label>
                            <asp:TextBox ID="txtPeso" CssClass="campoPeso" Width="32" runat="server" />
                        </li>
                        <li style="margin: 3px -2px 0 6px; height: 23px; width: 1px; border-left: 1px solid #BDBDBD;">
                        </li>
                        <li style="margin-bottom: -8px; margin-right: 0px; width: 70px;">
                            <label>
                                Pressão Val/HR</label>
                            <asp:TextBox ID="txtPressao" Width="30" CssClass="campoPressArteri" runat="server" />
                            <asp:TextBox ID="txtHrPressao" Width="30" CssClass="campoHora" runat="server" />
                        </li>
                        <li style="margin: 3px -2px 0 1px; height: 23px; width: 1px; border-left: 1px solid #BDBDBD;">
                        </li>
                        <li style="margin-bottom: -8px; margin-right: 0px; width: 70px;">
                            <label>
                                Temp Val/HR</label>
                            <asp:TextBox ID="txtTemp" Width="30" CssClass="campoTemp" runat="server" />
                            <asp:TextBox ID="txtHrTemp" Width="30" CssClass="campoHora" runat="server" />
                        </li>
                        <li style="margin: 3px -2px 0 1px; height: 23px; width: 1px; border-left: 1px solid #BDBDBD;">
                        </li>
                        <li style="margin-bottom: -8px; margin-right: 0px; width: 70px;">
                            <label>
                                Glicem Val/HR</label>
                            <asp:TextBox ID="txtGlic" Width="30" CssClass="campoGlicem" runat="server" />
                            <asp:TextBox ID="txtHrGlic" Width="30" CssClass="campoHora" runat="server" />
                        </li>
                        <li style="margin: 3px -2px 0 1px; height: 23px; width: 1px; border-left: 1px solid #BDBDBD;">
                        </li>
                        <li style="margin-bottom: -8px; margin-right: 0px; width: 45px;">
                            <label>
                                Dores?</label>
                            <asp:DropDownList ID="drpDores" Height="13px" Width="40px" runat="server">
                                <asp:ListItem Value="S" Text="Sim" />
                                <asp:ListItem Value="N" Text="Não" Selected="True" />
                            </asp:DropDownList>
                        </li>
                        <li style="margin: 3px -1px 0 0; height: 23px; width: 1px; border-left: 1px solid #BDBDBD;">
                        </li>
                        <li style="margin-bottom: -8px; margin-right: 0px; width: 45px;">
                            <label>
                                Enjôos?</label>
                            <asp:DropDownList ID="drpEnjoos" Height="13px" Width="40px" runat="server">
                                <asp:ListItem Value="S" Text="Sim" />
                                <asp:ListItem Value="N" Text="Não" Selected="True" />
                            </asp:DropDownList>
                        </li>
                        <li style="margin: 3px -1px 0 0; height: 23px; width: 1px; border-left: 1px solid #BDBDBD;">
                        </li>
                        <li style="margin-bottom: -8px; margin-right: 0px; width: 45px;">
                            <label>
                                Vômitos?</label>
                            <asp:DropDownList ID="drpVomitos" Height="13px" Width="40px" runat="server">
                                <asp:ListItem Value="S" Text="Sim" />
                                <asp:ListItem Value="N" Text="Não" Selected="True" />
                            </asp:DropDownList>
                        </li>
                        <li style="margin: 3px -1px 0 0; height: 23px; width: 1px; border-left: 1px solid #BDBDBD;">
                        </li>
                        <li style="margin-bottom: -8px; margin-right: 0px; width: 45px;">
                            <label>
                                Febre?</label>
                            <asp:DropDownList ID="drpFebre" Height="13px" Width="40px" runat="server">
                                <asp:ListItem Value="S" Text="Sim" />
                                <asp:ListItem Value="N" Text="Não" Selected="True" />
                            </asp:DropDownList>
                        </li>
                        <li style="margin-top: 1px; color: Red;">
                            <label style="font-size: 0.85em;">
                                CLASSIFICAÇÃO DE RISCO</label>
                            <asp:DropDownList runat="server" ID="ddlClassRisco" Width="90px" ClientIDMode="Static">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <asp:HiddenField runat="server" ID="hidDivAberta" ClientIDMode="Static" />
                            <div id="divClassRisc" title="Selecione a Classificação de risco pela cor" style="cursor: pointer;
                                margin: 13px 0 0 -17px; width: 11px; height: 9px; border: 1px solid #CCCCCC;
                                position: absolute; background-color: White; padding: 2px">
                                <div id="divClassRiscCorSelec" style="height: 9px; width: 100%">
                                </div>
                                <div id="divClass1" title="Emergência" style="display: none; height: 100%; width: 40px;
                                    background-color: Red; cursor: pointer; float: left;">
                                </div>
                                <div id="divClass2" title="Muito Urgente" style="display: none; height: 100%; width: 40px;
                                    background-color: Orange; cursor: pointer; float: left; margin-left: 5px;">
                                </div>
                                <div id="divClass3" title="Urgente" style="display: none; height: 100%; width: 40px;
                                    background-color: Yellow; cursor: pointer; float: left; margin-left: 5px;">
                                </div>
                                <div id="divClass4" title="Pouco Urgente" style="display: none; height: 100%; width: 40px;
                                    background-color: Green; cursor: pointer; float: left; margin-left: 5px;">
                                </div>
                                <div id="divClass5" title="Não Urgente" style="display: none; height: 100%; width: 40px;
                                    background-color: Blue; cursor: pointer; float: left; margin-left: 5px;">
                                </div>
                                <div id="divFecha" title="Fechar paleta" style="display: none; float: right; margin-left: 5px;">
                                    <a id="lnkClose" class="lnkClose" title="Fechar paleta" href="#">[x]</a>
                                </div>
                            </div>
                        </li>
                        <li style="margin-top: 13px">
                            <asp:LinkButton OnClick="SpInf_OnClick" ID="SpInf" runat="server"><strong>+ INF</strong></asp:LinkButton>
                            <%-- <span id="spInf" onclick="AbreModalMaisInfo()" class="spInf">
                            <strong>+ INF</strong></span> --%>
                        </li>
                        <li style="">
                            <label>
                                Dt. Atendimento</label>
                            <asp:TextBox ID="txtDtAtend" runat="server" CssClass="campoData dt" />
                        </li>
                        <li style="margin-left: -1px;">
                            <label>
                                Hora</label>
                            <asp:TextBox ID="txtHrAtend" CssClass="campoHora" Width="28px" runat="server" />
                        </li>
                        <li>
                            <label class="lblObrigatorio">
                                Repasse do Atendimento</label>
                            <asp:DropDownList ID="drpProfResp" Width="185px" runat="server" />
                        </li>
                        <li class="liTituloGrid" style="width: 508px !important; height: 20px !important;
                            clear: both; margin-right: 0px; background-color: #FFEC8B; text-align: center;
                            font-weight: bold; margin-bottom: 2px; padding-top: 2px; display: none">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: Black;
                                float: left; margin-left: 10px;">
                                REGISTRO DO ATENDIMENTO</label>
                            <div style="margin-right: 3px; float: right; margin-top: 4px;">
                                <img title="Emitir Prontuário do Paciente" style="margin-top: -2px" src="/BarrasFerramentas/Icones/Imprimir.png"
                                    height="16px" width="16px" />
                            </div>
                            <div id="divBtnOdontograma" runat="server" style="margin-right: 5px; float: right;
                                margin-top: 4px;">
                                <img title="Emitir Odontograma do Paciente" style="margin-top: -2px" src="/Library/IMG/PGS_IC_Anexo.png"
                                    height="16px" width="16px" />
                                <asp:LinkButton ID="lnkbOdontograma" runat="server" ForeColor="#0099ff">HISTÓRICO</asp:LinkButton>
                            </div>
                        </li>
                        <li style="margin: 0 0 0 5px; border-top: 0; z-index: 1;">
                            <ul>
                                <li style="margin-top: -6px; border-right: solid 1px #BDBDBD; height: 241px; margin-left: -2px;">
                                    <ul>
                                        <li>
                                            <label style="color: Orange; font-size: 9px;">
                                                QUEIXA PRINCIPAL</label>
                                            <asp:TextBox runat="server" ID="txtQueixa" ClientIDMode="Static" BackColor="#FAFAFA"
                                                TextMode="MultiLine" Rows="4" Style="width: 238px; margin-top: 1px; border-top: 1px solid #BDBDBD;
                                                border-left: 0; border-right: 0; border-bottom: 0;" Font-Size="12px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <label style="color: Orange; font-size: 9px;">
                                                ANAMNESE / HDA (História da Doença Atual)</label>
                                            <asp:TextBox runat="server" ID="txtHDA" ClientIDMode="Static" BackColor="#FAFAFA"
                                                TextMode="MultiLine" Rows="4" Style="width: 231px; margin-top: 1px; border-top: 1px solid #BDBDBD;
                                                border-left: 0; border-right: 0; border-bottom: 0;" Font-Size="12px"></asp:TextBox>
                                        </li>
                                        <li style="">
                                            <ul>
                                                <li style="margin-left: -6px; margin-bottom: 0;">
                                                    <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                                                        HIPÓTESE DIAGNÓSTICA / AÇÃO REALIZADA</label>
                                                    <asp:TextBox runat="server" ID="txtHipotese" BackColor="#FAFAFA" TextMode="MultiLine"
                                                        Rows="4" Style="width: 229px; border-top: 1px solid #BDBDBD; border-left: 0;
                                                        border-right: 0; border-bottom: 0;" Font-Size="12px"></asp:TextBox>
                                                </li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li style="margin-top: -166px; z-index: 1000">
                                    <ul>
                                        <li style="clear: both; margin-top: -6px; margin-bottom: -6px; display: inline-flex;">
                                            <ul>
                                                <li>
                                                    <label style="color: Orange; margin-left: -13px; font-size: 9px; width: 478px; border-bottom: 1px solid #BDBDBD;">
                                                        EXAME FÍSICO</label>
                                                </li>
                                                <li style="background-color: #F1FFEF; border: 1px solid #D2DFD1; margin: 0; cursor: pointer">
                                                    <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Formulário" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                                                        height="15px" width="15px" />
                                                    <asp:LinkButton ID="lnkExamFis" runat="server" OnClick="lnkExamFis_OnClick">Adicionar</asp:LinkButton>
                                                </li>
                                                <li style="margin-left: -6px;">
                                                    <li>
                                                        <label style="color: Orange; margin-top: 3px; font-size: 9px; width: 62px; border-bottom: 1px solid #BDBDBD;">
                                                            DEFINIR A CID</label>
                                                    </li>
                                                    <li style="cursor: pointer">
                                                        <asp:TextBox runat="server" ID="txtDefCid" Width="70px" Style="margin: 0;"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbPesqPacNome" CssClass="btnProcurar" ValidationGroup="pesqPac"
                                                            runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" OnClick="imgbPesqCID_OnClick" />
                                                        <asp:DropDownList runat="server" ID="drpDefCid" OnSelectedIndexChanged="drpDefCid_OnSelectedIndexChanged"
                                                            Visible="false" AutoPostBack="true" Width="70px">
                                                        </asp:DropDownList>
                                                        <asp:ImageButton ID="imgbVoltarPesq" ValidationGroup="pesqPac" CssClass="btnProcurar"
                                                            Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico" OnClick="imgbVoltarPesq_OnClick"
                                                            Visible="false" runat="server" />
                                                    </li>
                                                </li>
                                            </ul>
                                        </li>
                                        <li style="clear: both; color: Blue;"><span style="margin-right: 181px; margin-left: -8px;">
                                            DA ESPECIALIDADE</span> <span>OUTRAS SITUAÇÕES A/Z</span></li>
                                        <li style="clear: both; display: flex">
                                            <div id="div1" style="width: 265px; height: 120px; border: 1px solid #CCC; overflow-y: scroll;
                                                margin-right: 8px; margin-left: -8px;">
                                                <input type="hidden" id="Hidden1" name="" />
                                                <asp:GridView ID="grdExamFis" CssClass="grdBusca grdExamFis" runat="server" Style="width: 100%;
                                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                                    ShowHeaderWhenEmpty="true" ShowHeader="False">
                                                    <RowStyle CssClass="rowStyle" />
                                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                                    <EmptyDataTemplate>
                                                        Nenhum exame físico foi adicionado<br />
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemStyle HorizontalAlign="Left" Width="115px" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="idItemEx" Value='<%# Eval("idItem") %>' />
                                                                <asp:Label runat="server" ID="lblExamFis" Text='<%# Eval("Exame")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:ImageButton runat="server" ID="btnDelExa" AlternateText='<%# Eval("Value") %>'
                                                                    ImageUrl="/Library/IMG/Gestor_BtnDel.png" ToolTip="Excluir exame físico do atendimento"
                                                                    OnClick="btnDelExa_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                                                <asp:HiddenField runat="server" ID="hidValue" Value='<%# Eval("Value") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div id="div2" style="width: 265px; height: 120px; border: 1px solid #CCC; overflow-y: scroll">
                                                <input type="hidden" id="Hidden2" name="" />
                                                <asp:GridView ID="grdExaFisAZ" CssClass="grdBusca grdExamFis" runat="server" Style="width: 100%;
                                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                                    ShowHeaderWhenEmpty="true" ShowHeader="False">
                                                    <RowStyle CssClass="rowStyle" />
                                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                                    <EmptyDataTemplate>
                                                        Nenhum exame físico foi adicionado<br />
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemStyle HorizontalAlign="Left" Width="115px" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="idItemExAZ" Value='<%# Eval("idItem") %>' />
                                                                <asp:Label runat="server" ID="lblExamFisAZ" Text='<%# Eval("Exame")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:ImageButton runat="server" ID="btnDelExaAZ" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                                    ToolTip="Excluir exame físico do atendimento" OnClick="btnDelExaAZ_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                                                <asp:HiddenField runat="server" ID="hidIdExamFisAZ" Value='<%# Eval("Value") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div id="div3" style="width: 164px; margin-top: -10px; margin-left: 8px; height: 130px;
                                                border: 1px solid #CCC; overflow-y: scroll">
                                                <input type="hidden" id="Hidden3" name="" />
                                                <asp:GridView ID="grdItensCID" CssClass="grdBusca grdExamFis" runat="server" Style="width: 100%;
                                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                                    ShowHeaderWhenEmpty="false" AllowUserToAddRows="false">
                                                    <RowStyle CssClass="rowStyle" />
                                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                                    <EmptyDataTemplate>
                                                        Nenhuma CID foi adicionada<br />
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="CID">
                                                            <ItemStyle HorizontalAlign="Left" Width="25px" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="hidIdItemCID" Value='<%# Eval("idItem") %>' />
                                                                <asp:HiddenField runat="server" ID="idListaCID" Value='<%# Eval("idCID") %>' />
                                                                <asp:Label runat="server" ID="lblProtCID" Text='<%# Eval("coCID")%>' ToolTip='<%# Eval("descCID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JE">
                                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="idImgCID" Value='<%# Eval("existeProtocolo") %>' />
                                                                <input type="button" runat="server" id="btnObsProtCID" style="background: url('/Library/IMG/PGS_CentralRegulacao_Icone_Obs_15x15.png');
                                                                    width: 15px; height: 15px;" title="Definir o(s) protocolo(s) da CID utilizado neste atendimento"
                                                                    visible='<%# Eval("existeProtocolo")%>' onclick="addProtocoloCID_Click(this)" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EX">
                                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="idDelCID" Value='<%# Eval("idCID") %>' />
                                                                <asp:HiddenField runat="server" ID="hidExisteProtocolo" Value='<%# Eval("existeProtocolo") %>' />
                                                                <asp:ImageButton runat="server" Width="15px" ID="btnDelCID" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                                    ToolTip="Excluir a CID e seu(s) protocolo(s) deste atendimento" OnClick="btnDelCID_OnClick"
                                                                    OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </li>
                                    </ul>
                                </li>
                                <li style="border-right: solid 1px #BDBDBD; margin-top: -245px; margin-left: 738px;
                                    width: 241px;">
                                    <ul>
                                        <li style="margin-left: -6px; clear: both;">
                                            <label style="color: Orange; font-size: 9px; width: 231px; border-bottom: 1px solid #BDBDBD;">
                                                SOLICITAR PARECER</label>
                                        </li>
                                        <li style="clear: both; margin-left: -6px; margin-bottom: 0px;">
                                            <asp:TextBox runat="server" ID="txtProSolicitado" Style="margin: 0; width: 212px;"></asp:TextBox>
                                            <asp:ImageButton ID="imgPesProfSolicitado" ValidationGroup="pesqPac" runat="server"
                                                ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" OnClick="imgPesProfSolicitado_OnClick" />
                                            <asp:DropDownList ID="drpProSolicitado" Width="212px" OnSelectedIndexChanged="drpProSolicitado_OnSelectedIndexChanged"
                                                runat="server" Visible="false" AutoPostBack="true" />
                                            <asp:ImageButton ID="imgVoltarPesProfSOlicitado" ValidationGroup="pesqPac" Width="16px"
                                                Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico" OnClick="imgVoltarPesProfSOlicitado_OnClick"
                                                Visible="false" runat="server" />
                                        </li>
                                        <li style="clear: both; margin-left: -6px; margin-bottom: 6px; margin-top: 4px">
                                            <div id="div4" style="width: 230px; height: 50px; border: 1px solid #CCC; overflow-y: scroll">
                                                <input type="hidden" id="Hidden4" name="" />
                                                <asp:GridView ID="grdProfSolicitado" CssClass="grdBusca grdExamFis" runat="server"
                                                    Style="width: 100%; cursor: default;" AutoGenerateColumns="false" AllowPaging="false"
                                                    GridLines="Vertical" ShowHeaderWhenEmpty="false">
                                                    <RowStyle CssClass="rowStyle" />
                                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                                    <EmptyDataTemplate>
                                                        Nenhum profissional foi adicionado<br />
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="PROFISSIONAL">
                                                            <ItemStyle HorizontalAlign="Left" Width="115px" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="idItemProf" Value='<%# Eval("idItem") %>' />
                                                                <asp:Label runat="server" ID="lblNomeProf" Text='<%# Eval("NomeCol")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PT">
                                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <input type="button" runat="server" id="btnObsProfSol" style="background: url('/Library/IMG/PGS_CentralRegulacao_Icone_Obs_15x15.png');
                                                                    width: 15px; height: 15px;" title="Inserir observação sobre o protocolo da CID utilizado neste atendimento"
                                                                    onclick="hideObsProfSol(this)" />
                                                                <asp:HiddenField runat="server" ID="hidIdProfSol" Value='<%# Eval("coCol") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EX">
                                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:ImageButton runat="server" Width="15px" ID="btnDelProfSol" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                                    ToolTip="Excluir profissional solicitado deste atendimento" OnClick="btnDelProfSol_OnClick"
                                                                    OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderStyle CssClass="invible" />
                                                            <ItemStyle Width="10px" HorizontalAlign="Center" CssClass="invible" />
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtObsProfSol" Value='<%# Eval("Obs") %>'></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtAnamRepas" Value='<%# Eval("Anam") %>'></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtAcaoRepas" Value='<%# Eval("Acao") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </li>
                                        <li style="margin-left: -6px; clear: both; margin-top: -5px;">
                                            <label style="color: Orange; font-size: 9px; width: 231px; border-bottom: 1px solid #BDBDBD;">
                                                PRESCRIÇÃO
                                            </label>
                                        </li>
                                        <li style="margin-left: -6px; clear: both;">
                                            <asp:Button ID="lnkMedic" Style="background-color: #c1ffc1; margin-left: 0px; width: 75px;
                                                cursor: pointer;" runat="server" Height="15px" Text="RECEITA" Font-Bold="true"
                                                Font-Size="8px" OnClick="lnkMedic_OnClick" ToolTip="Solicitar medicamento para o paciente." />
                                            <asp:Button ID="lnkExame" Style="background-color: #c1ffc1; margin-left: 2px; width: 74px;
                                                cursor: pointer;" runat="server" Text="EXAME" Height="15px" Font-Bold="true"
                                                Font-Size="8px" OnClick="lnkExame_OnClick" ToolTip="Emitir guia ou solicitar exame externo." />
                                            <asp:Button ID="lnkAmbul" Style="background-color: #c1ffc1; margin-left: 0px; width: 75px;
                                                cursor: pointer;" runat="server" Text="AMBULATÓRIO" Height="15px" Font-Bold="true"
                                                Font-Size="8px" OnClick="lnkAmbul_OnClick" ToolTip="Solicitar serviço ambulatorial." />
                                        </li>
                                        <li style="margin: 0 -11px; clear: both;">
                                            <ul>
                                                <li>
                                                    <asp:Button ID="BtnFicha" runat="server" Text="FICHA ATEND." Style="background-color: #ccc;
                                                        cursor: pointer; margin-right: 3px;" Width="74px" Height="13px" Font-Bold="true"
                                                        Font-Size="8px" OnClick="lnkFicha_OnClick" />
                                                    <asp:Button ID="lnkbProntuario" runat="server" Text="PRONTUÁRIO" Style="background-color: #ccc;
                                                        cursor: pointer; margin-right: 3px;" Width="74px" Height="13px" Font-Bold="true"
                                                        Font-Size="8px" OnClick="lnkbProntuario_OnClick" />
                                                    <asp:Button ID="BtnLaudo" runat="server" Text="LAUDO" Style="background-color: #ccc;
                                                        cursor: pointer; margin-right: 3px;" Width="74px" Height="13px" Font-Bold="true"
                                                        Font-Size="8px" OnClick="BtnLaudo_Click" />
                                                </li>
                                                <li>
                                                    <asp:Button ID="BtnAtestado" runat="server" Text="ATESTADO" Style="background-color: #ccc;
                                                        cursor: pointer; margin-right: 3px;" Width="74px" Height="13px" Font-Bold="true"
                                                        Font-Size="8px" OnClick="BtnAtestado_Click" />
                                                    <asp:Button ID="BtnObserv" runat="server" Style="background-color: #ccc; cursor: pointer;
                                                        margin-right: 3px;" Width="74px" Height="13px" Font-Bold="true" Font-Size="8px"
                                                        Text="OBSERVAÇÃO" OnClick="BtnObserv_OnClick" />
                                                    <asp:Button ID="lnkbAnexos" Style="background-color: #ccc; cursor: pointer; margin-right: 3px;"
                                                        runat="server" Text="ANEXO" Width="74px" Height="13px" Font-Bold="true" Font-Size="8px"
                                                        AccessKey="A" OnClick="lnkbAnexos_OnClick" ToolTip="Anexos associados ao Atendimento/Paciente"
                                                        CssClass="colorTextBlack" />
                                                    <br />
                                                    <asp:Button ID="BtnInternar" runat="server" Text="INTERNAR" Font-Bold="true" Font-Size="8px"
                                                        Style="background-color: #ccc; cursor: pointer; margin-left: 2px; margin: 4px 0 0 0;"
                                                        Width="74px" Height="13px" OnClick="BtnInternar_OnClick" />
                                                    <asp:Button ID="BtnGuia" runat="server" Text="GUIA CONV." Font-Bold="true" Font-Size="8px"
                                                        Style="background-color: #ccc; cursor: pointer; margin-left: 2px; margin: 4px 0 0 0;"
                                                        Width="77px" Height="13px" OnClick="BtnGuia_OnClick" />
                                                    <asp:Button ID="BtnProntuCon" runat="server" ToolTip="Emissão do Prontuário do Paciente no Modelo Convencional"
                                                        Text="PRONT. PADRÃO" Font-Bold="true" Font-Size="8px"
                                                        Style="background-color: #ccc; cursor: pointer; margin-left: 3px"
                                                        Width="74px" Height="13px" OnClick="BtnProntuCon_Click"/>
                                                    <asp:Button ID="ButtonProntuMod" runat="server" ToolTip="Emissão do Prontuário do Paciente no Modelo Modular"
                                                        Text="PRONT. MODULAR" Font-Bold="true" Font-Size="8px"
                                                        Style="background-color: #ccc; cursor: pointer;margin-top: 4px; margin-left: 0px"
                                                        Width="74px" Height="13px" OnClick="lnkbProntuario_OnClick"/>
                                                    <asp:Button ID="ButtonEncam" runat="server" ToolTip="Fazer o encaminhamento do Paciente a Profissional Especializado"
                                                        Text="ENCAMINHAMENTO" Font-Bold="true" Font-Size="8px"
                                                        Style="background-color: #ccc; cursor: pointer;margin-top: 4px; margin-left: 0px"
                                                        Width="81px" Height="13px" OnClick="BtnEncam_OnClick" />
                                                    <asp:Button ID="ButtonCirurgia" runat="server" ToolTip="Fazer a solicitação de Cirurgia do Pacinte com a emissão da GUIA"
                                                        Text="CIRURGIA" Font-Bold="true" Font-Size="8px"
                                                        Style="background-color: #ccc; cursor: pointer;margin-top: 4px; margin-left: 0px"
                                                        Width="74px" Height="13px" OnClick="BtnObserv_OnClick"/>
                                                    
                                                    <%-- BOTÃO SERÁ MANTIDO CASO TENHA QUE VOLTAR COM A FUNCIONALIDADE
                                                    <asp:Button ID="lnkOrcamento" Style="background-color: #ccc; cursor: pointer; margin: 4px 0 0 0;
                                                        margin-bottom: -3px" Width="74px" Height="13px" runat="server" Text="ORÇAMENTO"
                                                        OnClick="lnkOrcamento_OnClick" />--%>
                                                </li>
                                            </ul>
                                        </li>
                                        <li style="margin-left: -6px; clear: both;">
                                            <ul>
                                                <li style="border-top: 1px solid #BDBDBD; width: 77px; margin: 0"></li>
                                                <li style="border-top: 1px solid #BDBDBD; width: 93px;"></li>
                                                <li style="margin: -5px 0;"><span style="font-size: 8px;margin-left:-92px;">FINALIZAÇÃO</span></li>
                                                <li style="border-top: 1px solid #BDBDBD; width: 46px;"></li>
                                                <li style="margin: 0; clear: both">
                                                    <%--<asp:Button ID="BtnGuia" runat="server" Text="GUIA" Height="20px" Width="74px" OnClick="BtnGuia_OnClick"
                                                        Visible="True" />--%>
                                                    <asp:Button ID="BtnEspera" runat="server" Text="ESPERA" Height="20px" BackColor="#87CEEB"
                                                        Font-Bold="true" Style="color: #fff; cursor: pointer;" Width="74px" OnClick="BtnEspera_OnClick"  />
                                                </li>
                                                <li style="margin: 0 0 0 3px">
                                                    <asp:Button ID="BtnFinalizar" runat="server" Text="ALTA" Height="20px" Width="75px"
                                                        Font-Bold="true" Style="color: #fff; cursor: pointer;" BackColor="#008000" OnClick="BtnFinalizar_OnClick" />
                                                    <asp:Button ID="BtnObito" runat="server" Text="ÓBITO" BackColor="#000000" Font-Bold="true" ToolTip="Finalizar (Encerrar) o atendimento por óbito do Paciente"
                                                        Style="color: #fff; cursor: pointer;" Height="20px" Width="74px" OnClick="BtnObito_OnClick" />
                                                    <%--<input type="button" id="BtnObito" value="ÓBITO" onclick="AbreModalObito()" style="color: #fff;
                                                        cursor: pointer; height: 20px; width: 74px; background-color: #000000; font-weight: bold;" />--%>
                                                    <asp:HiddenField ID="hidmodalid" runat="server" ClientIDMode="Static" />
                                                </li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li>
            <div id="divLoadShowLogAgenda" style="display: none; height: 600px!important;">
                <ul class="ulDadosLog">
                    <li>
                        <label>
                            Paciente</label>
                        <asp:TextBox runat="server" ID="txtNomePaciMODLOG" Enabled="false" Width="220px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Sexo</label>
                        <asp:TextBox runat="server" ID="txtSexoMODLOG" Enabled="false" Width="10px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Idade</label>
                        <asp:TextBox runat="server" ID="txtIdadeMODLOG" Enabled="false" Width="70px"></asp:TextBox>
                    </li>
                    <li style="clear: both; margin-left: -5px !important; margin-top: -2px;">
                        <div style="width: 890px; height: 305px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdLogAgendamento" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum Questionário associado<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="Data_V" HeaderText="DATA">
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_PROFI" HeaderText="RESPONSÁVEL">
                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_TIPO" HeaderText="TIPO">
                                        <ItemStyle HorizontalAlign="Left" Width="65px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemStyle Width="18px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <img src='<%# Eval("CAMINHO_IMAGEM") %>' alt="" style="width: 16px !important; height: 16px !important;
                                                margin: 0 0 0 0 !important" title="Representação gráfica da Ação" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DE_TIPO" HeaderText="AÇÃO">
                                        <ItemStyle HorizontalAlign="Left" Width="240px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Observação">
                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtObser" TextMode="MultiLine" Style="margin: 0 0 0 0 !important;
                                                height: 23px !important; width: 180px" ReadOnly="true" Text='<%# Eval("OBS") %>'></asp:TextBox>
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
            <div id="divAtestado" style="display: none; width: 500px; height: 350px !important;">
                <ul class="ulDados" style="float: left; width: 500px; margin-top: 0px !important;">
                    <li>
                        <label title="Data Comparecimento" class="lblObrigatorio">
                            Data</label>
                        <asp:TextBox ID="txtDtAtestado" runat="server" ValidationGroup="atestado" class="campoData"
                            ToolTip="Informe a data de comparecimento" AutoPostBack="true" OnTextChanged="txtDtAtestado_OnTextChanged" />
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="atestado" ID="rfvDtAtestado"
                            CssClass="validatorField" ErrorMessage="O campo data é requerido" ControlToValidate="txtDtAtestado"></asp:RequiredFieldValidator>
                    </li>
                    <li style="margin-top: 0px !important; margin-left: 70px !important;">
                        <label title="Tipo Documento">
                            Atestado</label>
                        <asp:RadioButton ID="chkAtestado" GroupName="TipoDoc" CssClass="chk" Style="margin-left: -7px !important;"
                            OnCheckedChanged="chkAtestado_OnCheckedChanged" AutoPostBack="true" runat="server"
                            ToolTip="Imprimir um atestado médico" />
                        Dias
                        <asp:TextBox ID="txtQtdDias" runat="server" Width="20px" MaxLength="3" ToolTip="Informe a quantidade de dias de repouso do paciente">
                        </asp:TextBox>
                        <asp:CheckBox ID="chkCid" CssClass="chk" Style="margin-right: -6px !important;" Checked="true"
                            runat="server" ToolTip="Imprimir CID no atestado médico" />
                        CID
                        <asp:TextBox ID="txtCid" runat="server" Width="25px" MaxLength="5" ToolTip="CID">
                        </asp:TextBox>
                    </li>
                    <li style="margin-top: 0px !important; margin-left: 55px !important;">
                        <label title="Tipo Documento">
                            Comparecimento</label>
                        <asp:RadioButton ID="chkComparecimento" GroupName="TipoDoc" CssClass="chk" Style="margin-left: -7px !important;"
                            OnCheckedChanged="chkComparecimento_OnCheckedChanged" AutoPostBack="true" runat="server"
                            ToolTip="Imprimir uma declaração de comparecimento" />
                        Período
                        <asp:DropDownList ID="drpPrdComparecimento" runat="server" Style="vertical-align: top;"
                            ToolTip="Período de comparecimento">
                            <asp:ListItem Value="M" Text="Manhã" />
                            <asp:ListItem Value="T" Text="Tarde" />
                            <asp:ListItem Value="N" Text="Noite" />
                            <asp:ListItem Value="D" Text="Dia" />
                        </asp:DropDownList>
                    </li>
                </ul>
                <ul class="ulDados" style="width: 500px; margin-top: 0px !important; margin-left: 0px !important;">
                    <li>
                        <div style="border: 1px solid #CCCCCC; width: 480px; height: 240px; overflow-y: scroll;
                            margin-top: 10px;">
                            <asp:GridView ID="grdPacAtestado" CssClass="grdBusca" runat="server" Style="width: 100% !important;
                                cursor: default" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum paciente disponivel<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="CK">
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hidCoALuAtes" Value='<%# Eval("CO_ALU") %>' />
                                            <asp:HiddenField runat="server" ID="hidNmPac" Value='<%# Eval("NO_PAC_") %>' />
                                            <asp:HiddenField runat="server" ID="hidNmResp" Value='<%# Eval("NO_RESP_IMP") %>' />
                                            <asp:HiddenField runat="server" ID="hidRgPac" Value='<%# Eval("RG_PAC") %>' />
                                            <asp:HiddenField runat="server" ID="hidHora" Value='<%# Eval("hr_Consul") %>' />
                                            <asp:HiddenField runat="server" ID="hidCoRespAtest" Value='<%# Eval("CO_RESP") %>' />
                                            <asp:RadioButton ID="rbtPaciente" GroupName="rbtPac" runat="server" OnCheckedChanged="rbtPaciente_OnCheckedChanged"
                                                AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="hr_Consul" HeaderText="HORA">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_PAC" HeaderText="PACIENTE">
                                        <ItemStyle Width="210px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_RESP" HeaderText="RESPONSÁVEL">
                                        <ItemStyle Width="190px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                    <li id="li10" runat="server" class="liBtnAddA" style="margin-top: 10px !important;
                        margin-left: 230px !important; height: 15px;">
                        <asp:LinkButton ID="lnkbGerarAtestado" OnClick="lnkbGerarAtestado_Click" runat="server"
                            ValidationGroup="atestado" ToolTip="Emitir documento">
                            <asp:Label runat="server" ID="Label12" Text="EMITIR" Style="margin-left: 5px; margin-right: 5px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divGuiaPlano" style="display: none; height: 150px !important;">
                 <div style="display: inline-flex; width: 100%; border-bottom: 3px solid rgb(169, 208, 245);
                    padding-bottom: 5px;">
                    <div>
                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="RadioButtonGuiaLivre_OnCheckedChanged"
                            GroupName="TipoGuia" runat="server" ID="RadioButtonGuiaLivre" />LIVRE - modelo
                        <asp:DropDownList Width="140" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListModeloGuia_OnSelectedIndexChanged"
                            ID="DropDownListModeloGuia">
                        </asp:DropDownList>
                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="RadioButtonGuiaInterno_OnCheckedChanged"
                            GroupName="TipoGuia" runat="server" ID="RadioButtonGuiaInterno" />INTERNO
                    </div>
                </div>
                <ul class="ulDados" style="width: 430px; margin-top: 0px !important">
                    <li>
                        <label title="Data Comparecimento" class="lblObrigatorio">
                            Data</label>
                        <asp:TextBox ID="txtDtGuia" runat="server" ValidationGroup="guia" class="campoData"
                            ToolTip="Informe a data de comparecimento" AutoPostBack="true" OnTextChanged="txtDtGuia_OnTextChanged" />
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="guia" ID="rfvDtGuia"
                            CssClass="validatorField" ErrorMessage="O campo data é requerido" ControlToValidate="txtDtGuia"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label title="Paciente">
                            Paciente</label>
                        <asp:DropDownList ID="drpPacienteGuia" runat="server" Width="240px" />
                    </li>
                    <li>
                        <label title="Operadora">
                            Operadora</label>
                        <asp:DropDownList ID="drpOperGuia" runat="server" Width="80px" />
                    </li>
                    <li style="clear: both;">
                        <label title="Observações">
                            Observações / Justificativa</label>
                        <asp:TextBox ID="txtObsGuia" Width="410px" Height="40px" TextMode="MultiLine" MaxLength="180"
                            runat="server" />
                    </li>
                    <li class="liBtnAddA" style="clear: none !important; margin-left: 180px !important;
                        margin-top: 8px !important; height: 15px;">
                        <asp:LinkButton ID="lnkbImprimirGuia" runat="server" ValidationGroup="guia" OnClick="lnkbImprimirGuia_OnClick"
                            ToolTip="Imprimir guia do plano de saúde">
                            <asp:Label runat="server" ID="lblEmitirGuia" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
    </ul>
    <asp:HiddenField runat="server" ID="hidObserMedicam" ClientIDMode="Static" />
    <%--<div id="divMedicamentos" style="display: none; height: 430px !important; width: 800px">
        <ul class="ulDados" style="margin: 5px 0 0 -10px !important;">
            <li>
                <ul>
                    <li>
                        <label>
                            Grupo</label>
                        <asp:DropDownList ID="drpGrupoMedic" Width="150px" OnSelectedIndexChanged="drpGrupoMedic_SelectedIndexChanged"
                            AutoPostBack="true" runat="server" />
                    </li>
                    <li>
                        <label>
                            SubGrupo</label>
                        <asp:DropDownList ID="drpSubGrupoMedic" Width="150px" runat="server" />
                    </li>
                    <li>
                        <label>
                            Pesquisa por Medicamento</label>
                        <asp:RadioButton ID="rdbMedic" Style="margin: 0 -6px 0 -5px;" GroupName="PesqMedicamento"
                            Checked="true" runat="server" ClientIDMode="Static" />
                        <asp:TextBox ID="txtMedicamento" runat="server" ClientIDMode="Static" />
                    </li>
                    <li>
                        <label>
                            Pesquisa por Princípio Ativo</label>
                        <asp:RadioButton ID="rdbPrinc" Style="margin: 0 -6px 0 -5px;" GroupName="PesqMedicamento"
                            runat="server" ClientIDMode="Static" />
                        <asp:TextBox ID="txtPrincipio" runat="server" ClientIDMode="Static" />
                    </li>
                    <li style="margin-top: 7px;">
                        <asp:ImageButton ID="imgbPesqMedic" OnClick="imgbPesqMedic_OnClick" runat="server"
                            ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" />
                    </li>
                    <li title="Clique para cadastrar um novo medicamento" class="liBtnAddA" style="margin: 5px 0 0 65px !important;
                        width: 40px;">
                        <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Medicamento" src="/Library/IMG/Gestor_BtnEdit.png"
                            height="15px" width="15px" />
                        <asp:LinkButton ID="lnkNovoMedicam" runat="server" OnClick="lnkNovoMedicam_OnClick">Novo</asp:LinkButton>
                    </li>
                </ul>
            </li>
            <li>
                <div style="margin-left: 5px; width: 755px; height: 100px; border: 1px solid #CCC;
                    overflow-y: scroll">
                    <asp:GridView ID="grdPesqMedic" CssClass="grdBusca" runat="server" Style="width: 100%;
                        cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                        ShowHeaderWhenEmpty="true">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum medicamento encontrado<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="CK">
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:HiddenField ID="hidIdMedic" Value='<%# Eval("CO_PROD") %>' runat="server" />
                                    <asp:HiddenField ID="hidNomeMedic" Value='<%# Eval("NO_PROD") %>' runat="server" />
                                    <asp:HiddenField ID="hidPrincAtiv" Value='<%# Eval("NO_PRINCIPIO_ATIVO") %>' runat="server" />
                                    <asp:RadioButton ID="rdbMedicamento" GroupName="Medicamentos" runat="server" Width="100%"
                                        Style="margin: 0 -15px 0 -25px !important;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NO_PROD" HeaderText="MEDICAMENTO">
                                <ItemStyle HorizontalAlign="Left" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NO_PRINCIPIO_ATIVO" HeaderText="PRINCÍPIO ATIVO">
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DES_PROD" HeaderText="DESCRIÇÃO">
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FORNEC" HeaderText="FORNECEDOR">
                                <ItemStyle HorizontalAlign="Left" Width="55px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VL_UNIT_PROD" HeaderText="VALOR">
                                <ItemStyle HorizontalAlign="Right" Width="15px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
            <li style="clear: both;">
                <ul>
                    <li>
                        <label class="lblObrigatorio" title="Informe a prescrição médica">
                            PRESCRIÇÃO</label>
                        <asp:TextBox ID="txtPrescricao" Width="430px" Height="17px" Font-Size="13px" ValidationGroup="AddMedic"
                            ToolTip="Informe a prescrição médica" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ID="rfvPrescricao" CssClass="validatorField"
                            ValidationGroup="AddMedic" ErrorMessage="O campo uso é obrigatório" ControlToValidate="txtPrescricao"
                            Display="Dynamic" />
                    </li>
                    <li>
                        <label class="lblObrigatorio" title="Informe a quantidade utilizada do medicamento">
                            QTD</label>
                        <asp:TextBox ID="txtQuantidade" CssClass="campoQtd" Width="20px" Height="17px" Font-Size="13px"
                            Style="text-align: right;" ValidationGroup="AddMedic" ToolTip="Informe a quantidade utilizada do medicamento"
                            runat="server" />
                        <asp:RequiredFieldValidator runat="server" ID="rfvQuantidade" CssClass="validatorField"
                            ValidationGroup="AddMedic" ErrorMessage="O campo uso é obrigatório" ControlToValidate="txtQuantidade"
                            Display="Dynamic" />
                    </li>
                    <li>
                        <label class="lblObrigatorio" title="Informe a quantidade de dias de uso do medicamento, sendo 0 como Uso Contínuo">
                            USO</label>
                        <asp:TextBox ID="txtUso" CssClass="campoQtd" Width="20px" Height="17px" Font-Size="13px"
                            Style="text-align: right;" ValidationGroup="AddMedic" ToolTip="Informe a quantidade de dias de uso do medicamento, sendo 0 como Uso Contínuo"
                            runat="server" />
                        <asp:RequiredFieldValidator runat="server" ID="rfvUso" CssClass="validatorField"
                            ValidationGroup="AddMedic" ErrorMessage="O campo uso é obrigatório" ControlToValidate="txtUso"
                            Display="Dynamic" />
                    </li>
                    <li title="Clique para adicionar o medicamento" class="liBtnAddA" style="clear: both;
                        margin-top: -4px; margin-left: 350px !important; width: 75px;">
                        <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Medicamento" src="/Library/IMG/Gestor_SaudeEscolar.png"
                            height="15px" width="15px" />
                        <asp:LinkButton ID="lnkAddMedicamm" runat="server" OnClick="lnkAddMedicam_OnClick"
                            ValidationGroup="AddMedic">ADICIONAR</asp:LinkButton>
                    </li>
                </ul>
            </li>
            <li style="margin-left: 15px !important; clear: both; float: left;">
                <ul>
                    <li>
                        <ul style="width: 745px;">
                            <li class="liTituloGrid" style="height: 20px !important; width: 735px; margin-left: -10px;
                                background-color: #A9D0F5; text-align: center; font-weight: bold; float: left">
                                <ul>
                                    <li style="margin: 0 0 0 10px; float: left">
                                        <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                            MEDICAMENTOS</label>
                                    </li>
                                </ul>
                            </li>
                            <li title="Clique para emitir o Receituario do atendimento" class="liBtnAddA" style="float: right;
                                margin: -25px -2px 3px 0px; width: 12px; height: 15px;">
                                <asp:ImageButton ID="BtnReceituario" runat="server" OnClick="BtnReceituario_Click"
                                    ToolTip="Emitir Receituario do Paciente" Style="margin-top: -2px; margin-left: -3px;"
                                    ImageUrl="/BarrasFerramentas/Icones/Imprimir.png" Height="18px" Width="18px" />
                            </li>
                        </ul>
                    </li>
                    <li style="clear: both; margin: -7px 0 0 -5px;">
                        <div style="width: 755px; height: 80px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdMedicamentos" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum medicamento associado<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="MEDICAMENTO">
                                        <ItemStyle Width="110px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdMedic" Visible="false" runat="server" />
                                            <asp:Label ID="lblMedic" Width="100%" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Princípio Ativo">
                                        <ItemStyle HorizontalAlign="Left" Width="180px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrincipio" Width="100%" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prescrição">
                                        <ItemStyle HorizontalAlign="Left" Width="400px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrescricao" Width="100%" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTD">
                                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQtd" Width="100%" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="USO">
                                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblUso" Width="100%" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EX">
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgExcMedic" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                ToolTip="Excluir Medicamento" OnClick="imgExcMedic_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                    <li style="clear: both; margin: 3px 0 0 -5px;">
                        <asp:TextBox runat="server" ID="txtObserMedicam" TextMode="MultiLine" ClientIDMode="Static"
                            Style="width: 755px; height: 35px;" Font-Size="12px" placeholder=" Digite as observações sobre Medicamentos"></asp:TextBox>
                    </li>
                </ul>
            </li>
        </ul>
    </div>--%>

    <asp:HiddenField runat="server" ID="HiddenField1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="HiddenFieldTipoReceituario" />
    <asp:HiddenField runat="server" ID="HiddenFieldModeloID" />
    <div id="divMedicamentos" style="display: none; margin-left: 12px;overflow: hidden;">
        <div  style="display:inline-flex; width:100%; border-bottom: 3px solid rgb(169, 208, 245); padding-bottom: 5px;">
            <div>
                <label><strong>FORMATO DE PRESCRIÇÃO DE MEDICAMENTOS</strong></label>
                <asp:RadioButton  AutoPostBack="true" OnCheckedChanged="RadioButtonLivre_OnCheckedChanged" GroupName="TipoPrescricao" runat="server" ID="RadioButtonLivre" />LIVRE - modelo
                <asp:DropDownList Width="140" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListModelo_OnSelectedIndexChanged" ID="DropDownListModelo"></asp:DropDownList>
                <asp:RadioButton  AutoPostBack="true" OnCheckedChanged="RadioButtonPesquisa_OnCheckedChanged" GroupName="TipoPrescricao" runat="server" ID="RadioButtonPesquisa" />PESQUISA - Base de Medicamentos
            </div>
            <div style="margin-left: 100px">
              
                <label><strong>TIPO DE RECEITUÁRIO DE MEDICAMENTOS</strong></label>
                <asp:RadioButton  Checked="true" GroupName="TipoReceituario" runat="server" AutoPostBack="true" OnCheckedChanged="RadioButtonNormal_OnCheckedChanged" ID="RadioButtonNormal" />NORMAL
                <asp:RadioButton GroupName="TipoReceituario" runat="server" AutoPostBack="true" OnCheckedChanged="RadioButtonContEspecial_OnCheckedChanged" ID="RadioButtonContEspecial" />CONTROLE ESPECIAL
            </div>
       </div>
        <asp:Panel Visible="false" runat="server" ID="PanelPesquisa">
               <ul class="ulDados" style="margin: 5px 0 0 -10px !important;">
            <li>
                <ul>
                    <li>
                        <label>
                            Grupo</label>
                        <asp:DropDownList ID="drpGrupoMedic" Width="150px" OnSelectedIndexChanged="drpGrupoMedic_SelectedIndexChanged"
                            AutoPostBack="true" runat="server" />
                    </li>
                    <li>
                        <label>
                            SubGrupo</label>
                        <asp:DropDownList ID="drpSubGrupoMedic" Width="150px" runat="server" />
                    </li>
                    <li>
                        <label>
                            Pesquisa por Medicamento</label>
                        <asp:RadioButton ID="rdbMedic" Style="margin: 0 -6px 0 -5px;" GroupName="PesqMedicamento"
                            Checked="true" runat="server" ClientIDMode="Static" />
                        <asp:TextBox ID="txtMedicamento" runat="server" ClientIDMode="Static" />
                    </li>
                    <li>
                        <label>
                            Pesquisa por Princípio Ativo</label>
                        <asp:RadioButton ID="rdbPrinc" Style="margin: 0 -6px 0 -5px;" GroupName="PesqMedicamento"
                            runat="server" ClientIDMode="Static" />
                        <asp:TextBox ID="txtPrincipio" runat="server" ClientIDMode="Static" />
                    </li>
                    <li style="margin-top: 7px;">
                        <asp:ImageButton ID="imgbPesqMedic" OnClick="imgbPesqMedic_OnClick" runat="server"
                            ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" />
                    </li>
                    <li title="Clique para cadastrar um novo medicamento" class="liBtnAddA" style="margin: 5px 0 0 65px !important;
                        width: 40px;">
                        <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Medicamento" src="/Library/IMG/Gestor_BtnEdit.png"
                            height="15px" width="15px" />
                        <asp:LinkButton ID="lnkNovoMedicam" runat="server" OnClick="lnkNovoMedicam_OnClick">Novo</asp:LinkButton>
                    </li>
                </ul>
            </li>
            <li>
                <div style="margin-left: 5px; width: 755px; height: 110px; border: 1px solid #CCC;
                    overflow-y: scroll">
                    <asp:GridView ID="grdPesqMedic" CssClass="grdBusca" runat="server" Style="width: 100%;
                        cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                        ShowHeaderWhenEmpty="true">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum medicamento encontrado<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="CK">
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:HiddenField ID="hidIdMedic" Value='<%# Eval("CO_PROD") %>' runat="server" />
                                    <asp:HiddenField ID="hidNomeMedic" Value='<%# Eval("NO_PROD") %>' runat="server" />
                                    <asp:HiddenField ID="hidPrincAtiv" Value='<%# Eval("NO_PRINCIPIO_ATIVO") %>' runat="server" />
                                    <asp:RadioButton ID="rdbMedicamento" GroupName="Medicamentos" runat="server" Width="100%"
                                        Style="margin: 0 -15px 0 -25px !important;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NO_PROD" HeaderText="MEDICAMENTO">
                                <ItemStyle HorizontalAlign="Left" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NO_PRINCIPIO_ATIVO" HeaderText="PRINCÍPIO ATIVO">
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DES_PROD" HeaderText="DESCRIÇÃO">
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FORNEC" HeaderText="FORNECEDOR">
                                <ItemStyle HorizontalAlign="Left" Width="55px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VL_UNIT_PROD" HeaderText="VALOR">
                                <ItemStyle HorizontalAlign="Right" Width="15px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
            <li style="clear: both; margin-bottom: -4px !important;">
                <ul>
                    <li>
                        <label class="lblObrigatorio" title="Informe a prescrição médica">
                            PRESCRIÇÃO</label>
                        <asp:TextBox ID="txtPrescricao" Width="430px" Height="17px" Font-Size="13px" ValidationGroup="AddMedic"
                            ToolTip="Informe a prescrição médica" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ID="rfvPrescricao" CssClass="validatorField"
                            ValidationGroup="AddMedic" ErrorMessage="O campo uso é obrigatório" ControlToValidate="txtPrescricao"
                            Display="Dynamic" />
                    </li>
                    <li>
                        <label class="lblObrigatorio" title="Informe a quantidade utilizada do medicamento">
                            QTD</label>
                        <asp:TextBox ID="txtQuantidade" CssClass="campoQtd" Width="20px" Height="17px" Font-Size="13px"
                            Style="text-align: right;" ValidationGroup="AddMedic" ToolTip="Informe a quantidade utilizada do medicamento"
                            runat="server" />
                        <asp:RequiredFieldValidator runat="server" ID="rfvQuantidade" CssClass="validatorField"
                            ValidationGroup="AddMedic" ErrorMessage="O campo uso é obrigatório" ControlToValidate="txtQuantidade"
                            Display="Dynamic" />
                    </li>
                    <li>
                        <label class="lblObrigatorio" title="Informe a quantidade de dias de uso do medicamento, sendo 0 como Uso Contínuo">
                            USO</label>
                        <asp:TextBox ID="txtUso" CssClass="campoQtd" Width="20px" Height="17px" Font-Size="13px"
                            Style="text-align: right;" ValidationGroup="AddMedic" ToolTip="Informe a quantidade de dias de uso do medicamento, sendo 0 como Uso Contínuo"
                            runat="server" />
                        <asp:RequiredFieldValidator runat="server" ID="rfvUso" CssClass="validatorField"
                            ValidationGroup="AddMedic" ErrorMessage="O campo uso é obrigatório" ControlToValidate="txtUso"
                            Display="Dynamic" />
                    </li>
                    <li title="Clique para adicionar o medicamento" style="
                        margin-top: 11px; margin-left: 165px !important; width: 75px;">
                        <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Medicamento" src="/Library/IMG/Gestor_SaudeEscolar.png"
                            height="15px" width="15px" />
                        <asp:LinkButton ID="lnkAddMedicamm" runat="server"  OnClick="lnkAddMedicam_OnClick"
                            ValidationGroup="AddMedic">ADICIONAR</asp:LinkButton>
                    </li>
                </ul>
            </li>
            <li style="margin-left: 15px !important; clear: both; float: left;">
                <ul>
                    <li>
                        <ul style="width: 745px;">
                            <li class="liTituloGrid" style="height: 20px !important; width: 757px; margin-left: -10px;
                                background-color: #A9D0F5; text-align: center; font-weight: bold; float: left">
                                <ul>
                                    <li style="margin: 0 0 0 10px; float: left">
                                        <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; text-align:center">
                                            MEDICAMENTOS PRESCRITOS AO PACIENTE</label>
                                    </li>
                                </ul>
                            </li>
                            
                        </ul>
                    </li>
                    <li style="clear: both; margin: -7px 0 0 -5px;">
                        <div style="width: 755px; height: 150px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdMedicamentos" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum medicamento associado<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="MEDICAMENTO">
                                        <ItemStyle Width="110px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdMedic" Visible="false" runat="server" />
                                            <asp:Label ID="lblMedic" Width="100%" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Princípio Ativo">
                                        <ItemStyle HorizontalAlign="Left" Width="180px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrincipio" Width="100%" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prescrição">
                                        <ItemStyle HorizontalAlign="Left" Width="400px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrescricao" Width="100%" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTD">
                                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQtd" Width="100%" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="USO">
                                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblUso" Width="100%" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EX">
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgExcMedic" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                ToolTip="Excluir Medicamento" OnClick="imgExcMedic_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                   
                </ul>
            </li>
        </ul>
        </asp:Panel>  
        <asp:Panel runat="server" ID="PanelLivre">  
        <div style='height: 356px'>
            </div>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="MedicamentoLivre" ControlToValidate="ASPxHtmlEditor1" ></asp:RequiredFieldValidator>
        </asp:Panel>
        <div style="clear: both; margin: 20px 0 0 -5px;">
            <asp:TextBox runat="server" ID="txtObserMedicam" TextMode="MultiLine" ClientIDMode="Static"
                Style="width: 743px; height: 25px; margin-left: 12px;" Font-Size="12px" placeholder=" Digite as observações sobre Medicamentos"></asp:TextBox>
        </div>
        <div style="display:flex">
          <div style="margin-top: 5px">
            
            <div style="margin-bottom: 9px; margin-top: 3px;">
               <button><a id="A1" href="#" onclick="AbreModalSalvarModeloReceita();" runat="server" style="margin-left: 7px;" class="linkButton">
                    SALVAR MODELO</a></button>
            </div>
        </div>
        <div title="Clique para emitir o Receituario do atendimento">
            <asp:Button ID="BtnReceituario" ValidationGroup="MedicamentoLivre" Style="background-color: #D8D8D8; margin-left: 174%;
                margin-top: 3px; width: 125%" runat="server" Height="25px" Text="IMPRIMIR PRESCRIÇÃO DE MEDICAMENTO"
                OnClick="BtnReceituario_Click" ToolTip="Emitir Receituario do Paciente." />
        </div>
        </div>      
    </div>
    <div id="divSalvarModeloReceita" style="display: none; margin-left: 12px;overflow: hidden;z-index: 99999999;">
        <label>Nome do Modelo*</label>
        <asp:TextBox runat="server" Style="width: 258px; height: 17px;" MaxLength="30" ID="TextBoxNomeModelo"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="ModeloReceita" ControlToValidate="TextBoxNomeModelo" ></asp:RequiredFieldValidator>
        <asp:LinkButton ID="LinkButton2"  ValidationGroup="ModeloReceita"  CssClass="linkButton" runat="server" Style="margin-left: 96px;" Text="SALVAR" OnClick="ButtonSalvarModelo_Click"
                ToolTip="Salvar Modelo" />
     </div>
    <asp:HiddenField runat="server" ID="hidCheckEmitirGuiaExame" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hidCheckSolicitarExame" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hidObserExame" ClientIDMode="Static" />
    <%--<div id="divExames" style="display: none; height: 400px !important; width: 800px">
        <ul class="ulDados">
            <li>
                <ul style="width: 766px">
                    <li class="liTituloGrid" style="width: 673px; height: 20px !important; margin-left: -0px;
                        background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                        <ul>
                            <li style="margin: 0 0 0 10px; float: left">
                                <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                    EXAMES</label>
                            </li>
                            <li style="display: flex; margin-top: 3px;">
                                <asp:CheckBox runat="server" ID="chkIsGuiaExame" ClientIDMode="Static" ToolTip="Emitir apenas a guia dos exames selecionados (se nenhum for selecionado, a funcionalidade apenas emitirá a guia)." />Emitir
                                guia
                                <asp:CheckBox runat="server" ID="chkIsExameExterno" ClientIDMode="Static" ToolTip="Solicitar exame para o paciente em atendimento (se nenhum for selecionado, a funcionalidade apenas emitirá a guia)." />Solicitar
                                Exame </li>
                            <li style="float: right; margin-top: 3px; margin-right: 15px;">
                                <ul>
                                    <li>
                                        <asp:Label runat="server" ID="Label5">Contratação</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlOperProcPlan" Width="100px" OnSelectedIndexChanged="ddlOperProcPlan_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-left: -2px;">
                                        <asp:Label runat="server" ID="Label6">Plano</asp:Label>
                                        <asp:DropDownList runat="server" Width="100px" ID="ddlPlanProcPlan" OnSelectedIndexChanged="ddlPlanProcPlan_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                    <li id="li2" runat="server" title="Clique para cadastrar um novo Exame (Procedimento)"
                        class="liBtnAddA" style="float: right; margin: -25px 44px 3px 5px; width: 40px">
                        <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Formulário" src="../../../../../Library/IMG/Gestor_BtnEdit.png"
                            height="15px" width="15px" />
                        <asp:LinkButton ID="imgNovoExam" runat="server" OnClick="imgNovoExam_OnClick">Novo</asp:LinkButton>
                    </li>
                </ul>
            </li>
            <li style="clear: both; margin: -7px 0 0 5px !important;">
                <div style="width: 766px; border: 1px solid #CCC; overflow-y: scroll">
                    <asp:GridView ID="grdExame" CssClass="grdBusca" runat="server" Style="width: 100%;
                        cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                        ShowHeaderWhenEmpty="true">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum Procedimento de Plano de Saúde associado<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="CÓDIGO">
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlExame" Width="100%" OnSelectedIndexChanged="ddlExame_OnSelectedIndexChanged"
                                        AutoPostBack="true" Style="margin: 0 0 0 -4px !important;">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DESCRIÇÃO">
                                <ItemStyle Width="250px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtCodigProcedPla" Width="100%" Style="margin-left: -4px;
                                        margin-bottom: 0px;" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VALOR">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtValorProced" Width="100%" Style="margin-left: -4px;
                                        margin-bottom: 0px;" Enabled="false"></asp:TextBox>
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
            <li style="clear: both; margin: 3px 0 0 5px;">
                <asp:TextBox runat="server" ID="txtObserExame" TextMode="MultiLine" ClientIDMode="Static"
                    Style="width: 766px; height: 15px;" Font-Size="11px" placeholder=" Digite as observações sobre Exames"></asp:TextBox>
            </li>
            <li class="liBtnAddA" style="clear: none !important; margin-left: 727px !important;
                margin-top: 8px !important; height: 15px;">
                <asp:LinkButton ID="btnGuiaExames" runat="server" OnClick="BtnGuiaExames_OnClick"
                    ToolTip="Imprimir laudo técnico">
                    <asp:Label runat="server" ID="Label7" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>--%>
    <asp:HiddenField runat="server" ID="HiddenFieldModeloExamesID" />
    <asp:HiddenField runat="server" ID="HiddenField2" ClientIDMode="Static" />
    <div id="divExames" style="display: none; height: 400px !important; width: 800px">
       <div  style="display:inline-flex; width:100%; border-bottom: 3px solid rgb(169, 208, 245); padding-bottom: 5px;">
            <div style="width: 100%">
                <label><strong>FORMATO DE PRESCRIÇÃO DE EXAMES</strong></label>
                <asp:RadioButton  AutoPostBack="true" OnCheckedChanged="RadioButtonLivreExames_OnCheckedChanged" GroupName="TipoExame" runat="server" ID="RadioButtonLivreExames" />LIVRE - modelo
                <asp:DropDownList Width="140" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListModeloExames_OnSelectedIndexChanged" ID="DropDownListModeloExames"></asp:DropDownList>
                <asp:RadioButton  AutoPostBack="true" OnCheckedChanged="RadioButtonPesquisaExames_OnCheckedChanged" GroupName="TipoExame" runat="server" ID="RadioButtonPesquisaExames" />PESQUISA
              
                    <asp:RadioButton AutoPostBack="true" OnCheckedChanged="RadioButtonGuiaExames_OnCheckedChanged"
                        GroupName="TipoExame" runat="server" Style="margin-left:180px" ID="RadioButtonGuiaExames" />Exames - Guia
                    Modelo
                    <asp:DropDownList Width="140" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListModeloGuiaExames_OnSelectedIndexChanged"
                        ID="DropDownListModeloGuiaExames">
                    </asp:DropDownList>
              
               
            </div>
       </div>
       <asp:Panel Visible="false" runat="server" ID="PanelExamesPesquisa">
            <ul class="ulDados">
            <li>
                <ul style="width: 766px">
                    <li class="liTituloGrid" style="width: 673px; height: 20px !important; margin-left: -0px;
                        background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                        <ul>
                            <li style="margin: 0 0 0 10px; float: left">
                                <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                    EXAMES</label>
                            </li>
                            <li style="float: right; margin-top: 3px; margin-right: 15px;">
                                <ul>
                                    <li>
                                        <asp:Label runat="server" ID="Label5">Contratação</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlOperProcPlan" Width="100px" OnSelectedIndexChanged="ddlOperProcPlan_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-left: -2px;">
                                        <asp:Label runat="server" ID="Label6">Plano</asp:Label>
                                        <asp:DropDownList runat="server" Width="100px" ID="ddlPlanProcPlan" OnSelectedIndexChanged="ddlPlanProcPlan_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                    <li id="li2" runat="server" title="Clique para cadastrar um novo Exame (Procedimento)"
                        class="liBtnAddA" style="float: right; margin: -25px 44px 3px 5px; width: 40px">
                        <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Formulário" src="../../../../../Library/IMG/Gestor_BtnEdit.png"
                            height="15px" width="15px" />
                        <asp:LinkButton ID="imgNovoExam" runat="server" OnClick="imgNovoExam_OnClick">Novo</asp:LinkButton>
                    </li>
                    <li id="li3" runat="server" title="Clique para adicionar um exame ao atendimento"
                        class="liBtnAddA" style="float: right; margin: -25px 22px 3px 5px; height: 15px;
                        width: 12px;">
                        <asp:ImageButton ID="ImageButton1" Height="15px" Width="15px" Style="margin-top: -1px;
                            margin-left: -2px;" ImageUrl="/Library/IMG/Gestor_SaudeEscolar.png" OnClick="lnkAddProcPla_OnClick"
                            runat="server" />
                    </li>
                    <li title="Clique para emitir os exames do paciente" class="liBtnAddA" style="float: right;
                        margin: -25px -2px 3px 0px; width: 12px; height: 15px;">
                        <asp:ImageButton ID="ImageButton2" runat="server" OnClick="BtnExames_OnClick" ToolTip="Emitir Exames do Paciente"
                            Style="margin-top: -2px; margin-left: -3px;" ImageUrl="/BarrasFerramentas/Icones/Imprimir.png"
                            Height="18px" Width="18px" />
                    </li>
                </ul>
            </li>
            <li style="clear: both; margin: -7px 0 0 5px !important;">
                <div style="width: 766px; border: 1px solid #CCC; overflow-y: scroll">
                    <asp:GridView ID="grdExame" CssClass="grdBusca" runat="server" Style="width: 100%;
                        cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                        ShowHeaderWhenEmpty="true">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum Procedimento de Plano de Saúde associado<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="CÓDIGO">
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlExame" Width="100%" OnSelectedIndexChanged="ddlExame_OnSelectedIndexChanged"
                                        AutoPostBack="true" Style="margin: 0 0 0 -4px !important;">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DESCRIÇÃO">
                                <ItemStyle Width="250px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtCodigProcedPla" Width="100%" Style="margin-left: -4px;
                                        margin-bottom: 0px;" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VALOR">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtValorProced" Width="100%" Style="margin-left: -4px;
                                        margin-bottom: 0px;" Enabled="false"></asp:TextBox>
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
        </ul>
        </asp:Panel>  
       <asp:Panel runat="server" ID="PanelExamesLivre">  
      
      <div style="margin-top: 5px">
                    <div style='height:356px'>
                    </div>
             </div>
        </asp:Panel>
      <asp:Panel runat="server" ID="PanelGuiaExames"></asp:Panel>
         <div style="clear: both; margin: 17px 0 0 -5px;">
          <asp:TextBox runat="server" ID="txtObserExame" TextMode="MultiLine" ClientIDMode="Static"
                Style="width: 743px; height: 30px; margin-left: 24px;" Font-Size="12px" placeholder=" Digite as observações sobre Exames"></asp:TextBox>
        </div>
        <div style="display: flex">
            <div style="margin-bottom: 9px; margin-top: 9px;">
                <a href="#" onclick="AbreModalSalvarModeloExame();" style="margin-left: 19px;" class="linkButton">
                    SALVAR MODELO</a>
            </div>
           <div title="Clique para emitir o Receituario do atendimento">
            <asp:Button ID="btnGuiaExames" Style="background-color: #D8D8D8; margin-left: 226%;
                margin-top: 5px; width: 125%" runat="server" Height="25px" Text="IMPRIMIR PRESCRIÇÃO DE EXAMES"
                OnClick="BtnGuiaExames_OnClick" ToolTip="Emitir Receituario do Paciente." />
            </div>
        </div>
    </div>
    <div id="divSalvarModeloExame" style="display: none; margin-left: 12px;overflow: hidden;z-index: 9999999;">
        <label>Nome do Modelo*</label>
        <asp:TextBox runat="server" Style="width: 258px; height: 17px;" MaxLength="30"  ID="TextBoxNomeModeloExame"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="ModeloExam" ControlToValidate="TextBoxNomeModeloExame" ></asp:RequiredFieldValidator>
        <asp:LinkButton ID="LinkButtonSalvarModeloExame" ValidationGroup="ModeloExam" CssClass="linkButton" runat="server" Style="margin-left: 96px;" Text="SALVAR" OnClick="LinkButtonSalvarModeloExame_Click"
                ToolTip="Salvar Modelo" />
     </div>

    <div id="divExameFisico" style="display: none;">
        <ul class="ulDados">
            <li>
                <ul style="width: 417px">
                    <li style="display: flex; margin: 2px">
                        <asp:CheckBox runat="server" ID="chkAZ" OnCheckedChanged="chkAZ_OnCheckedChanged"
                            Text="Exames Físicos A/Z" AutoPostBack="true"></asp:CheckBox>
                    </li>
                    <li class="" style="width: 417px; margin-left: -0px; background-color: #EEEEE0; text-align: center;
                        font-weight: bold; float: left">
                        <div style="width: 417px; height: 190px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:CheckBoxList runat="server" ID="cklExameFis" ClientIDMode="Static" Width="402px"
                                Style="border: #fff">
                            </asp:CheckBoxList>
                        </div>
                    </li>
                    <li class="lilnkExaFis" style="clear: both;"><strong>
                        <asp:LinkButton runat="server" ID="lnkInserirExFis" Text="Inserir" CssClass="lnkInserirExFis"
                            OnClick="lnkInserirExFis_OnClick" /></strong> </li>
                </ul>
            </li>
        </ul>
    </div>
    <div id="divProtocoloCID" style="display: none; height: 261px; width: 542px; border: 1px solid;">
        <ul class="ulDados" style="margin-top: 0">
            <li style="margin-left: 30px; width: 491px">
                <ul>
                    <li style="margin-left: 93px; margin-bottom: 10px">
                        <select id="selProtocoloCID" style="width: 305px">
                        </select>
                    </li>
                    <div id="divItensCID" style="height: 171px; clear: both;">
                    </div>
                    <li style="clear: both; float: right; margin-top: 10px;">
                        <asp:Button ID="btnInserirProt" ClientIDMode="Static" runat="server" Text="Salvar"
                            Height="20px" BackColor="#0b3e6f" Font-Bold="true" Style="color: #fff; cursor: pointer"
                            Width="56px" CssClass="mgLeft" OnClientClick="btnInserirProt_OnSelectedIndexChanged(this)" />
                    </li>
                </ul>
            </li>
        </ul>
    </div>
    
    <%--<div id="divAmbulatorio" style="display: none; height: 600px !important; width: 900px">
        <ul class="ulDados">
            <li>
                <ul>
                    <li class="liTituloGrid" style="width: 861px; height: 20px !important; margin-left: -0px;
                        background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                        <ul>
                            <li style="margin: 0 0 0 10px; float: left">
                                <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                    SERVIÇOS AMBULATORIAIS</label>
                            </li>
                            <li style="float: right; margin-top: 3px; margin-right: 15px;">
                                <ul>
                                    <li>
                                        <asp:Label runat="server" ID="Label8">Contratação</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlOperPlanoServAmbu" Width="265px" OnSelectedIndexChanged="ddlOperPlanoServAmbu_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-left: -2px;">
                                        <asp:Label runat="server" ID="Label13">Plano</asp:Label>
                                        <asp:DropDownList runat="server" Width="150px" ID="ddlPlanoServAmbu" OnSelectedIndexChanged="ddlPlanoServAmbu_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                    <li id="li1" runat="server" style="margin-left: 0px !important; margin-top: 1px;"
                        title="Clique para cadastrar um novo Serviço Ambulatorial (Procedimento)" class="liBtnAddA">
                        <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Formulário" src="../../../../../Library/IMG/Gestor_BtnEdit.png"
                            height="15px" width="15px" />
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="imgNovoExam_OnClick" Enabled="false">Novo</asp:LinkButton>
                    </li>
                    <li id="li4" runat="server" style="margin-left: 0px !important; margin-top: 1px;"
                        title="Clique para adicionar um exame ao atendimento" class="liBtnAddA">
                        <asp:ImageButton ID="lnkAddProcPla" Height="14px" Width="15px" Style="margin-top: -1px;
                            margin-left: -1px;" ImageUrl="/Library/IMG/Gestor_SaudeEscolar.png" OnClick="lnkAddProcPla_OnClick"
                            runat="server" />
                    </li>
                    <li title="Clique para emitir os exames do paciente" class="liBtnAddA" style="margin-left: 0px !important;
                        margin-top: 1px;">
                        <asp:ImageButton ID="BtnExames" runat="server" OnClick="BtnExames_OnClick" ToolTip="Emitir Exames do Paciente"
                            Style="margin-top: -2px; margin-left: -3px; margin-bottom: -3px; margin-right: -3px;"
                            ImageUrl="/BarrasFerramentas/Icones/Imprimir.png" Height="18px" Width="18px" />
                    </li>
                </ul>
            </li>
            <li style="height: 255px;">
                <ul>
                    <li style="clear: both; margin: -7px 0 0 0 !important;">
                        <div style="width: 968px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdServAmbulatoriais" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum Procedimento de Plano de Saúde associado<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="CONTRATAÇÃO *">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlGridContratacaoAmbul" Width="100%" AutoPostBack="true"
                                                Style="margin: 0 0 0 -4px !important;" OnSelectedIndexChanged="ddlGridContratacaoAmbul_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PLANO *">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlGridPlanoAmbul" Width="100%" Style="margin: 0 0 0 -4px !important;">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TIPO *">
                                        <ItemStyle Width="23px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlGridTipoAmbul" Width="100%" Style="margin: 0 0 0 -4px !important;">
                                                <asp:ListItem Value=""></asp:ListItem>
                                                <asp:ListItem Value="M">MED</asp:ListItem>
                                                <asp:ListItem Value="P">PRO</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" DESCRIÇÃO *">
                                        <ItemStyle Width="97px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtDefServAmbulatorial" Style="margin: 0; width: 88%"></asp:TextBox>
                                            <asp:ImageButton ID="imgbPesqServAmbul" CssClass="btnProcurar" ValidationGroup="pesqPac"
                                                runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" OnClick="ddlGridTipoAmbul_SelectedIndexChanged"
                                                Width="10px" />
                                            <asp:DropDownList runat="server" ID="ddlServAmbulatorial" OnSelectedIndexChanged="ddlServAmbu_OnSelectedIndexChanged"
                                                AutoPostBack="true" Style="margin: 0 0 0 -4px !important; width: 88%" Visible="false">
                                            </asp:DropDownList>
                                            <asp:ImageButton ID="imgbVoltarPesqServAmbul" ValidationGroup="pesqPac" CssClass="btnProcurar"
                                                Width="10px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico" OnClick="imgbVoltarPesqServAmbul_OnClick"
                                                Visible="false" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CÓDIGO">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtCodigoServAmbulatorial" Width="100%" Style="margin: 0 0 0 -4px !important;"
                                                Enabled="false">
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VALOR">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtValorServAmbulatorial" Width="100%" Style="margin-left: -4px;
                                                margin-bottom: 0px;" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="COMPLEMENTO">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtComplementoServAmbulatorial" Width="100%" Style="margin-left: -4px;
                                                margin-bottom: 0px;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EX">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgServAmbulatoriaisPla" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                ToolTip="Excluir Serviço Ambulatorial" OnClick="imgServAmbulatoriaisPla_OnClick"
                                                Style="margin: 0 0 0 0 !important;" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                    <li style="clear: both; margin: 3px 0 0 0;">
                        <asp:TextBox runat="server" ID="txtObsServAmbulatoriais" TextMode="MultiLine" ClientIDMode="Static"
                            Style="width: 969px; height: 15px;" Font-Size="11px" MaxLength="200" placeholder=" Digite as observações sobre o Serviço Ambulatorial"></asp:TextBox>
                    </li>
                </ul>
            </li>
            <li style="clear: both; margin-left: 919px;">
                <asp:Button runat="server" ID="Button3" Text="SALVAR" Style="height: 20px; background-color: #0b3e6f;
                    color: #fff; cursor: pointer; width: 56px" Font-Bold="true" OnClick="btnSalvarServAmbulatorial_OnClick" />
            </li>
        </ul>
    </div>--%>

    <asp:HiddenField runat="server" ID="hidObsSerAmbulatoriais" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="didIdServAmbulatorial" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="HiddenFieldModeloAmbID" ClientIDMode="Static" />
    <div id="divAmbulatorio" style="display: none; height: 400px !important; width: 900px; overflow: hidden">
     <div  style="display:inline-flex; width:100%; border-bottom: 3px solid rgb(169, 208, 245); padding-bottom: 5px;">
            <div>
                <label><strong>FORMATO DE ABULATÓRIO </strong></label>
                <asp:RadioButton  AutoPostBack="true" OnCheckedChanged="RadioButtonAmbLivre_OnCheckedChanged" GroupName="TipoPrescricao" runat="server" ID="RadioButtonAmbLivre" />LIVRE
                <asp:DropDownList Width="140" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListModeloAmb_OnSelectedIndexChanged" ID="DropDownListModeloAmb"></asp:DropDownList>
                <asp:RadioButton  AutoPostBack="true" OnCheckedChanged="RadioButtonAmbNormal_OnCheckedChanged" GroupName="TipoPrescricao" runat="server" ID="RadioButtonAmbNormal" />INCLUSÃO
            </div>
       </div>
     <asp:Panel Visible="false" runat="server" ID="PanelNormalAmbulatorio">
           <ul class="ulDados">
            <li>
                <ul style="width: 875px">
                    <li class="liTituloGrid" style="width: 777px; height: 20px !important; margin-left: -0px;
                        background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                        <ul>
                            <li style="margin: 0 0 0 10px; float: left">
                                <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                    SERVIÇOS AMBULATORIAIS</label>
                            </li>
                            <li style="float: right; margin-top: 3px; margin-right: 15px;">
                                <ul>
                                    <li>
                                        <asp:Label runat="server" ID="Label8">Contratação</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlOperPlanoServAmbu" Width="150px" OnSelectedIndexChanged="ddlOperPlanoServAmbu_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-left: -2px;">
                                        <asp:Label runat="server" ID="Label13">Plano</asp:Label>
                                        <asp:DropDownList runat="server" Width="150px" ID="ddlPlanoServAmbu" OnSelectedIndexChanged="ddlPlanoServAmbu_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                    <li id="li1" runat="server" title="Clique para cadastrar um novo Serviço Ambulatorial (Procedimento)"
                        class="liBtnAddA" style="float: right; margin: -25px 44px 3px 5px">
                        <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Formulário" src="../../../../../Library/IMG/Gestor_BtnEdit.png"
                            height="15px" width="15px" />
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="imgNovoExam_OnClick" Enabled="false">Novo</asp:LinkButton>
                    </li>
                    <li id="li01" runat="server" title="Clique para adicionar um serviço ambulatorial ao atendimento"
                        class="liBtnAddA" style="float: right; margin: -25px 22px 3px 5px; height: 15px;
                        width: 12px;">
                        <asp:ImageButton ID="ImageButton3" Height="15px" Width="15px" Style="margin-top: -1px;
                            margin-left: -2px;" ImageUrl="/Library/IMG/Gestor_SaudeEscolar.png" runat="server"
                            OnClick="lnkAddProcPlaAmbulatorial_OnClick" />
                    </li>
                    <li title="Clique para emitir os serviços ambulatoriais" class="liBtnAddA" style="float: right;
                        margin: -25px -2px 3px 0px; width: 12px; height: 15px;">
                        <asp:ImageButton ID="ImageButton" runat="server" OnClick="btnGuiaServAmbulatoriais_OnClick"
                            Style="margin-top: -2px; margin-left: -3px;" ImageUrl="/BarrasFerramentas/Icones/Imprimir.png"
                            Height="18px" Width="18px" />
                    </li>
                </ul>
            </li>
            <li >
                <ul>
                    <li style="clear: both; margin: -7px 0 0 0 !important;">
                        <div style="width: 869px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdServAmbulatoriais" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum Procedimento de Plano de Saúde associado<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="CONTRATAÇÃO *">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlGridContratacaoAmbul" Width="100%" AutoPostBack="true"
                                                Style="margin: 0 0 0 -4px !important;" OnSelectedIndexChanged="ddlGridContratacaoAmbul_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PLANO *">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlGridPlanoAmbul" Width="100%" Style="margin: 0 0 0 -4px !important;">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TIPO *">
                                        <ItemStyle Width="23px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlGridTipoAmbul" Width="100%" Style="margin: 0 0 0 -4px !important;">
                                                <asp:ListItem Value=""></asp:ListItem>
                                                <asp:ListItem Value="M">MEDICAMENTO</asp:ListItem>
                                                <asp:ListItem Value="P">PROCEDIMENTO</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" DESCRIÇÃO *">
                                        <ItemStyle Width="97px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtDefServAmbulatorial" Style="margin: 0; width: 88%"></asp:TextBox>
                                            <asp:ImageButton ID="imgbPesqServAmbul" CssClass="btnProcurar" ValidationGroup="pesqPac"
                                                runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" OnClick="ddlGridTipoAmbul_SelectedIndexChanged"
                                                Width="10px" />
                                            <asp:DropDownList runat="server" ID="ddlServAmbulatorial" OnSelectedIndexChanged="ddlServAmbu_OnSelectedIndexChanged"
                                                AutoPostBack="true" Style="margin: 0 0 0 -4px !important; width: 88%" Visible="false">
                                            </asp:DropDownList>
                                            <asp:ImageButton ID="imgbVoltarPesqServAmbul" ValidationGroup="pesqPac" CssClass="btnProcurar"
                                                Width="10px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico" OnClick="imgbVoltarPesqServAmbul_OnClick"
                                                Visible="false" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CÓDIGO">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtCodigoServAmbulatorial" Width="100%" Style="margin: 0 0 0 -4px !important;"
                                                Enabled="false">
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VALOR">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtValorServAmbulatorial" Width="100%" Style="margin-left: -4px;
                                                margin-bottom: 0px;" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="COMPLEMENTO">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtComplementoServAmbulatorial" Width="100%" Style="margin-left: -4px;
                                                margin-bottom: 0px;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EX">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgServAmbulatoriaisPla" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                ToolTip="Excluir Serviço Ambulatorial" OnClick="imgServAmbulatoriaisPla_OnClick"
                                                Style="margin: 0 0 0 0 !important;" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                   
                </ul>
            </li>  
        </ul>
       </asp:Panel>
     <asp:Panel runat="server" ID="PanelLivreAmbulatorio">
         <div style="margin-top: 5px">
                    <div style='height:370px'>
                    </div>
              
             </div>
       </asp:Panel>
     <div style="clear: both; margin: 10px 0 0 26px;">
            <asp:TextBox runat="server" ID="txtObsServAmbulatoriais" TextMode="MultiLine" ClientIDMode="Static"
                Style="width: 796px; height: 35px; margin-left: 12px;" Font-Size="12px" MaxLength="200"
                placeholder=" Digite as observações sobre o Serviço Ambulatorial"></asp:TextBox>
        </div>
     <div style="display: flex">
            <div style="margin-top: 5px">
                <div style="margin-bottom: 9px; margin-top: 9px;">
                    <a href="#" onclick="AbreModalSalvarModeloAmb();" style="margin-left: 38px;" class="linkButton">
                        SALVAR MODELO</a>
                </div>
            </div>
            <div style="clear: both; margin-left: 633px;    margin-top: 9px;">
                <asp:Button runat="server" ID="Button3" Text="SALVAR" Style="height: 20px; background-color: #0b3e6f;
                    color: #fff; cursor: pointer; width: 56px" Font-Bold="true" OnClick="btnSalvarServAmbulatorial_OnClick" />
            </div>
        </div>
    </div>
    <div id="divSalvarModeloAmb" style="display: none; margin-left: 12px;overflow: hidden;z-index: 99999999;">
        <label>Nome do Modelo*</label>
        <asp:TextBox runat="server" Style="width: 258px; height: 17px;" MaxLength="30" ID="TextBoxNomeModeloAmb"></asp:TextBox><br />
         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="ModeloAmb" ControlToValidate="TextBoxNomeModeloAmb" ></asp:RequiredFieldValidator>
        <asp:LinkButton ID="LinkButton3"  CssClass="linkButton" ValidationGroup="ModeloAmb"   runat="server" Style="margin-left: 96px;" Text="SALVAR" OnClick="ButtonSalvarModeloAmb_Click"
                ToolTip="Salvar Modelo" />
     </div>

    <asp:HiddenField runat="server" ID="hidObsOrcam" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hidDtValidOrcam" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hidCkOrcamAprov" ClientIDMode="Static" />
    <div id="divOrcamentos" style="display: none; height: 340px !important; width: 450px">
        <ul class="ulDados">
            <li>
                <ul style="width: 443px">
                    <li class="liTituloGrid" style="width: 300px; height: 20px !important; margin-left: -5px;
                        background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                        <ul>
                            <li style="margin-left: 5px; float: left; width: 60px;">
                                <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: #FF6347">
                                    ORÇAMENTO</label>
                            </li>
                            <li style="float: right; margin-top: 3px; margin-right: -3px;">
                                <ul>
                                    <li>
                                        <asp:Label runat="server" ID="lbloper">Cont</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlOperOrc" OnSelectedIndexChanged="ddlOperOrc_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <asp:Label runat="server" ID="Label3">Plan</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlPlanOrc">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                    <li title="Clique para Adicionar um item de orçamento" class="liBtnAddA" style="float: right;
                        margin: -25px 77px 3px 0px; width: 61px">
                        <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Formulário" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                            height="15px" width="15px" />
                        <asp:LinkButton ID="btnAddProcOrc" runat="server" OnClick="btnAddProcOrc_OnClick">Adicionar</asp:LinkButton>
                    </li>
                    <li title="Clique para emitir o orçamento do paciente" class="liBtnAddA" style="float: right;
                        margin: -25px -2px 3px 5px; width: 70px">
                        <img title="Emitir Orçamento do Paciente" style="margin-top: -1px" src="/BarrasFerramentas/Icones/Imprimir.png"
                            height="16px" width="16px" />
                        <asp:LinkButton ID="lnkbOrcamento" runat="server" OnClick="lnkbOrcamento_Click">Orçamento</asp:LinkButton>
                    </li>
                </ul>
            </li>
            <li style="clear: both; margin: -7px 0 0 0;">
                <div style="width: 448px; height: 85px; border: 1px solid #CCC; overflow-y: scroll">
                    <asp:GridView ID="grdProcedOrcam" CssClass="grdBusca" runat="server" Style="width: 100%;
                        cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                        ShowHeaderWhenEmpty="true">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum Orçamento associado<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="CÓDIGO">
                                <ItemStyle Width="56px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlProcedOrc" OnSelectedIndexChanged="ddlProcedOrc_OnSelectedIndexChanged"
                                        AutoPostBack="true" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;">
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hidValUnitProc" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PROCEDIMENTO">
                                <ItemStyle Width="180px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtCodigProcedOrc" Width="100%" Style="margin-left: -4px;
                                        margin-bottom: 0px;" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QTD">
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtQtdProcedOrc" Width="100%" Style="margin-left: -4px;
                                        margin-bottom: 0px;" OnTextChanged="txtQtdProcedOrc_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VALOR">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtValorProcedOrc" Width="100%" CssClass="campoDecimal"
                                        Style="margin-left: -4px; margin-bottom: 0px;" OnTextChanged="txtValorProcedOrc_OnTextChanged"
                                        AutoPostBack="true"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EX">
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgExcOrc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                        ToolTip="Excluir Orçamento" OnClick="imgExcOrc_OnClick" Style="margin: 0 0 0 0 !important;"
                                        OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
            <li style="clear: both; margin: 3px 0 0 0;">
                <asp:TextBox runat="server" ID="txtObsOrcam" TextMode="MultiLine" ClientIDMode="Static"
                    Style="width: 448px; height: 15px;" Font-Size="13px" placeholder=" Digite as observações sobre o Orçamento"></asp:TextBox>
            </li>
            <li style="clear: both; margin-top: 5px; margin-left: -2px;">
                <ul>
                    <li style="margin-top: 5px;">
                        <asp:Label ID="Label1" runat="server">VALIDADE</asp:Label>
                        <asp:TextBox runat="server" ID="txtDtValidade" CssClass="campoData" ClientIDMode="Static"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Label ID="Label2" runat="server">DESCONTO - R$</asp:Label>
                        <asp:TextBox runat="server" ID="txtVlDscto" Style="margin-top: 4px; height: 16px !important;
                            width: 45px" CssClass="campoDecimal campoMoeda" OnTextChanged="txtVlDscto_OnTextChanged"
                            AutoPostBack="true"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Label ID="Label33" runat="server">TOTAL - R$</asp:Label>
                        <asp:TextBox runat="server" ID="txtVlTotalOrcam" Enabled="false" Style="margin-top: 4px;
                            height: 16px !important; width: 47px" CssClass="campoMoeda"></asp:TextBox>
                    </li>
                    <li style="margin-top: 5px; margin-left: 2px;">
                        <asp:CheckBox ID="chkAprovado" CssClass="chk" runat="server" Text="Aprovado" ToolTip="Marque se o orçamento foi aprovado para o faturamento"
                            ClientIDMode="Static" />
                    </li>
                </ul>
            </li>
        </ul>
    </div>
    <div id="divAvisoPermissao" style="display: none;">
        <ul>
            <li>
                <label>
                    Este usuário não tem permissão para realizar atendimento, por não ser um profissional
                    de saúde.
                </label>
            </li>
            <li class="liBtnConfirmarCiencia" onclick="javascript:BackToHome()">
                <asp:LinkButton ID="lnkEncAtendimento" runat="server" ToolTip="Realiza o encaminhamento para o atendimento">
                    <label style="margin-left:5px; color:White; cursor:pointer;">VOLTAR</label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div id="divLoadShowNovoMedic" style="display: none; height: 370px !important;">
        <ul class="ulDados" style="width: 350px !important;">
            <li>
                <label for="txtNProduto" class="lblObrigatorio" title="Nome do Produto">
                    Nome do Produto</label>
                <asp:TextBox ID="txtNProduto" Width="300px" ToolTip="Informe o Nome do Produto" runat="server"
                    MaxLength="60"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvProduto"
                    CssClass="validatorField" ErrorMessage="O nome do produto é obrigatório" ControlToValidate="txtNProduto"
                    Display="Dynamic" />
            </li>
            <li style="clear: both">
                <label for="txtNReduz" class="lblObrigatorio" title="Nome Reduzido">
                    Nome Reduzido</label>
                <asp:TextBox ID="txtNReduz" Width="180px" ToolTip="Informe o Nome Reduzido" runat="server"
                    MaxLength="33"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvNReduz"
                    CssClass="validatorField" ErrorMessage="O nome reduzido do produto é obrigatório"
                    ControlToValidate="txtNReduz" Display="Dynamic" />
            </li>
            <li>
                <label for="txtCodRef" class="lblObrigatorio" title="Código de Referência">
                    Cód. Referência</label>
                <asp:TextBox ID="txtCodRef" Width="110px" ToolTip="Informe o Código de Referência"
                    runat="server" MaxLength="9"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvCodRef"
                    CssClass="validatorField" ErrorMessage="O código referência do produto é obrigatório"
                    ControlToValidate="txtCodRef" Display="Dynamic" />
            </li>
            <li style="clear: both">
                <label for="txtDescProduto" class="lblObrigatorio" title="Descrição do Produto">
                    Descrição do Produto</label>
                <asp:TextBox ID="txtDescProduto" Width="300px" Height="51px" TextMode="MultiLine"
                    ToolTip="Informe a Descrição do Produto" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvDescProduto"
                    CssClass="validatorField" ErrorMessage="A descrição do produto é obrigatória"
                    ControlToValidate="txtDescProduto" Display="Dynamic" />
            </li>
            <li style="clear: both;">
                <label class="lblObrigatorio">
                    Princípio Ativo</label>
                <asp:TextBox runat="server" ID="txtPrinAtiv" Width="300px" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvPrinAtiv"
                    CssClass="validatorField" ErrorMessage="O princípio ativo do produto é obrigatório"
                    ControlToValidate="txtPrinAtiv" Display="Dynamic" />
            </li>
            <%--<!--<li style="clear: both;">
                <label for="ddlTipoProduto" title="Tipo de Produto" class="lblObrigatorio">
                    Tipo de Produto</label>
                <asp:DropDownList ID="ddlTipoProduto" Width="115px" ToolTip="Selecione o Tipo de Produto"
                    runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvTipoProduto" CssClass="validatorField"
                ErrorMessage="O tipo do produto é obrigatório" ControlToValidate="ddlTipoProduto" Display="Dynamic" />
            </li>-->--%>
            <li style="clear: both; margin-top: 10px;">
                <label for="ddlGrupo" class="lblObrigatorio" title="Grupo de Produtos">
                    Grupo</label>
                <asp:DropDownList ID="ddlGrupo" Width="150px" ToolTip="Selecione o Grupo de Produtos"
                    runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGrupo_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvGrupo"
                    CssClass="validatorField" ErrorMessage="O grupo do produto é obrigatório" ControlToValidate="ddlGrupo"
                    Display="Dynamic" />
            </li>
            <li style="margin-top: 10px;">
                <label for="ddlSubGrupo" class="lblObrigatorio" title="SubGrupo de Produtos">
                    SubGrupo</label>
                <asp:DropDownList ID="ddlSubGrupo" Width="145px" ToolTip="Selecione o SubGrupo de Produtos"
                    runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvSubGrupo"
                    CssClass="validatorField" ErrorMessage="O SubGrupo do produto é obrigatório"
                    ControlToValidate="ddlSubGrupo" Display="Dynamic" />
            </li>
            <li style="clear: both">
                <label class="lblObrigatorio" title="Unidade do Produtos">
                    Unidade</label>
                <asp:DropDownList ID="ddlUnidade" CssClass="campoDescricao" ToolTip="Unidade do Produtos"
                    runat="server" Width="90px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvUnidade"
                    CssClass="validatorField" ErrorMessage="A unidade do produto é obrigatória" ControlToValidate="ddlUnidade"
                    Display="Dynamic" />
            </li>
            <li id="li6" runat="server" class="liBtnAddA" style="margin-top: 0px !important;
                clear: both !important; height: 15px; margin-left: 140px !important;">
                <asp:LinkButton ID="lnkNovoMedic" runat="server" ValidationGroup="novoMedicamento"
                    OnClick="lnkNovoMedic_OnClick" ToolTip="Armazena as informações na prescrição em questão">
                    <asp:Label runat="server" ID="Label10" Text="SALVAR" Style="margin-left: 4px; margin-right: 4px;"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div id="divLoadShowNovoExam" style="display: none; height: 355px !important;">
        <ul class="ulDados" style="width: 480px; margin-top: 0px !important">
            <li>
                <label title="Nome do Procedimento de Saúde" class="lblObrigatorio">
                    Nome Exame/Procedimento</label>
                <asp:TextBox runat="server" ID="txtNoProcedimento" Width="290px" MaxLength="100"
                    ToolTip="Nome do Procedimento de Saúde"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoProcedimento" runat="server" ID="rfvNoProcedimento"
                    CssClass="validatorField" ErrorMessage="O nome do procedimento é obrigatório"
                    ControlToValidate="txtNoProcedimento" Display="Dynamic" />
            </li>
            <li>
                <label title="Código do Procedimento de Saúde" class="lblObrigatorio">
                    Código Pro.</label>
                <asp:TextBox runat="server" ID="txtCodProcMedic" ToolTip="Código do Procedimento Médico"
                    Width="65px" CssClass="campoCodigo"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoProcedimento" runat="server" ID="rfvCodProcMedic"
                    CssClass="validatorField" ErrorMessage="O código do procedimento é obrigatório"
                    ControlToValidate="txtCodProcMedic" Display="Dynamic" />
            </li>
            <li style="margin: 12px 0 0 -5px">
                <asp:CheckBox runat="server" ID="chkRequerAuto" Text="Requer Autorização" CssClass="chk"
                    ToolTip="Selecione caso o procedimento em questão precise de aprovação na Central de Regulação" />
            </li>
            <li style="clear: both; margin-top: -3px;">
                <label title="Grupo de Procedimentos de Saúde" class="lblObrigatorio">
                    Grupo</label>
                <asp:DropDownList runat="server" ID="ddlGrupo2" Width="153px" ToolTip="Grupo de Procedimentos de Saúde"
                    OnSelectedIndexChanged="ddlGrupo_OnSelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ValidationGroup="novoProcedimento" runat="server" ID="rvfGrupo2"
                    CssClass="validatorField" ErrorMessage="O grupo do procedimento é obrigatório"
                    ControlToValidate="ddlGrupo2" Display="Dynamic" />
            </li>
            <li style="margin-top: -3px;">
                <label title="SubGrupo de Procedimentos de Saúde" class="lblObrigatorio">
                    SubGrupo</label>
                <asp:DropDownList runat="server" ID="ddlSubGrupo2" Width="153px" ToolTip="SubGrupo de Procedimentos de Saúde">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ValidationGroup="novoProcedimento" runat="server" ID="rfvSubGrupo2"
                    CssClass="validatorField" ErrorMessage="O SubGrupo do procedimento é obrigatório"
                    ControlToValidate="ddlSubGrupo2" Display="Dynamic" />
            </li>
            <li style="margin-top: -3px">
                <label title="Pesquise pelo Operadora de Planos de Saúde">
                    Operadora</label>
                <asp:DropDownList runat="server" ID="ddlOper" Width="140px" ToolTip="Pesquise pelo Operadora de Planos de Saúde">
                </asp:DropDownList>
            </li>
            <li style="margin-top: 10px">
                <label style="color: Blue;">
                    VALORES</label>
                <ul style="margin-left: -5px;">
                    <li>
                        <label title="Valor de Custo do procedimento">
                            R$ Custo</label>
                        <asp:TextBox runat="server" ID="txtVlCusto" CssClass="campoDecimal" Width="50px"
                            ToolTip="Valor de Custo atual do procedimento" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Valor base do procedimento de saúde para cálculo">
                            R$ Base</label>
                        <asp:TextBox runat="server" ID="txtVlBase" Enabled="false" Width="60px" CssClass="campoDecimal"
                            ToolTip="Valor base do procedimento de saúde para cálculo" ClientIDMode="Static"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Valor Base da Operadora para o procedimento">
                            R$ Restitu</label>
                        <asp:TextBox runat="server" ID="txtVlRestitu" Enabled="false" Width="60px" CssClass="campoDecimal"
                            ToolTip="Valor de Restituição atual do procedimento" ClientIDMode="Static"></asp:TextBox>
                    </li>
                </ul>
            </li>
            <li style="margin-left: 130px; margin-top: 10px;">
                <label style="color: Blue;">
                    RESTRIÇÕES</label>
                <ul style="margin-left: -5px;">
                    <li>
                        <label title="Quantidade de Seção Autorizadas Pelo plano de Saúde">
                            QSA</label>
                        <asp:TextBox runat="server" Width="35px" CssClass="campoQtd" ID="txtQtSecaoAutorizada"
                            ToolTip="Quantidade de Seção Autorizadas pelo plano de Saúde"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Quantidade de Auxiliares para o procedimento">
                            QAUX</label>
                        <asp:TextBox runat="server" ID="txtQTAux" Width="30px" ToolTip="Quantidade de Auxiliares para o procedimento"
                            CssClass="campoQtd"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Quantidade de Anestesistas para o procedimento">
                            QANE</label>
                        <asp:TextBox runat="server" ID="txtQTAnest" Width="35px" ToolTip="Quantidade de Anestesistas para o procedimento"
                            CssClass="campoQtd"></asp:TextBox>
                    </li>
                </ul>
            </li>
            <li style="clear: both">
                <label title="Observação do Procedimento de Saúde">
                    Observação</label>
                <asp:TextBox runat="server" ID="txtObsProced" TextMode="MultiLine" Width="463px"
                    Height="60px" ToolTip="Observação do Procedimento de Saúde"></asp:TextBox>
            </li>
            <li id="li7" runat="server" class="liBtnAddA" style="margin-top: 0px !important;
                clear: both !important; height: 15px; margin-left: 230px !important;">
                <asp:LinkButton ID="lnkNovoExam" runat="server" ValidationGroup="novoProcedimento"
                    OnClick="lnkNovoExam_OnClick" ToolTip="Armazena as informações na prescrição em questão">
                    <asp:Label runat="server" ID="Label11" Text="SALVAR" Style="margin-left: 4px;"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div id="divLaudo" style="display: none; height: 240px !important;">
        <asp:HiddenField ID="hidIdLaudo" runat="server" />
        <ul class="ulDados" style="width: 545px; margin-top: 0px !important">
            <li>
                <label title="Paciente" class="lblObrigatorio">
                    Paciente</label>
                <asp:DropDownList ID="drpPacienteLaudo" OnSelectedIndexChanged="drpPacienteLaudo_SelectedIndexChanged"
                    AutoPostBack="true" runat="server" Width="225px" />
                <asp:RequiredFieldValidator ValidationGroup="laudo" runat="server" ID="rfvPacienteLaudo"
                    CssClass="validatorField" ErrorMessage="O paciente é obrigatório" ControlToValidate="drpPacienteLaudo"
                    Display="Dynamic" />
            </li>
            <li>
                <label title="Título do Laudo">
                    Título do Laudo</label>
                <asp:TextBox ID="txtTituloLaudo" runat="server" Width="200px" Text="LAUDO TÉCNICO" />
            </li>
            <li>
                <label title="Data Laudo">
                    Data Laudo</label>
                <asp:TextBox ID="txtDtLaudo" runat="server" class="campoData" ToolTip="Informe a data do laudo" />
            </li>
            <li style="clear: both;">
                <label title="Laudo Técnico" style="color: Blue;">
                    LAUDO TÉCNICO</label>
                <asp:TextBox ID="txtObsLaudo" Width="520px" Height="200px" TextMode="MultiLine" runat="server" />
            </li>
            <li class="liBtnAddA" style="clear: none !important; margin-left: 250px !important;
                margin-top: 8px !important; height: 15px;">
                <asp:LinkButton ID="lnkbImprimirLaudo" ValidationGroup="laudo" runat="server" OnClick="lnkbImprimirLaudo_OnClick"
                    ToolTip="Imprimir laudo técnico">
                    <asp:Label runat="server" ID="Label9" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div id="divMaisInfo" style="display: none; height: 166px !important;">
        <ul class="ulDados" style="width: 610px; margin-top: 0px !important">
            <li style="clear: both; width: 120px;">
                <ul>
                    <li style="margin-bottom: 4px;">
                        <label>
                            Diabetes</label>
                        <asp:CheckBox ID="chkDiabetes" Style="margin: 0 -6px 0 -6px;" runat="server" />
                        <asp:DropDownList ID="drpTipoDiabete" Width="90px" runat="server">
                            <asp:ListItem Value="" Text="Selecione" />
                            <asp:ListItem Value="1" Text="Tipo 1" />
                            <asp:ListItem Value="2" Text="Tipo 2" />
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both;">
                        <label>
                            Hipertensão</label>
                        <asp:CheckBox ID="chkHipertensao" Style="margin: 0 -6px 0 -6px;" runat="server" />
                        <asp:TextBox ID="txtHipertensao" Width="90px" runat="server" MaxLength="20" />
                    </li>
                    <li style="clear: both; margin-top: -8px;">
                        <label>
                            Fumante (St/Anos)</label>
                        <asp:DropDownList ID="drpFumante" Width="85px" runat="server">
                            <asp:ListItem Value="S" Text="Sim" />
                            <asp:ListItem Value="N" Text="Não" />
                            <asp:ListItem Value="E" Text="Ex-Fumante" />
                        </asp:DropDownList>
                        <asp:TextBox ID="txtFumanteAnos" CssClass="campoAnos" Style="float: right; margin-left: 1px;"
                            Height="13px" Width="20px" runat="server" />
                    </li>
                    <li style="clear: both; margin-top: -8px;">
                        <label>
                            Alcool (St/Anos)</label>
                        <asp:DropDownList ID="drpAlcool" Width="85px" runat="server">
                            <asp:ListItem Value="S" Text="Sim" />
                            <asp:ListItem Value="N" Text="Não" />
                            <asp:ListItem Value="A" Text="As Vezes" />
                        </asp:DropDownList>
                        <asp:TextBox ID="txtAlcoolAnos" CssClass="campoAnos" Height="13px" Style="float: right;
                            margin-left: 1px;" Width="20px" runat="server" />
                    </li>
                </ul>
            </li>
            <li style="margin-left: -5px; border-left: 1px solid #BDBDBD; width: 185px;">
                <ul>
                    <li>
                        <asp:CheckBox ID="chkCirurgia" Text="Cirurgia" CssClass="chk" Style="margin-left: -5px;"
                            runat="server" />
                        <asp:TextBox ID="txtCirurgia" TextMode="MultiLine" Rows="3" Width="170px" runat="server"
                            MaxLength="40" />
                    </li>
                    <li style="margin-top: -3px;">
                        <asp:CheckBox ID="chkAlergiaMedic" Text="Alergia" CssClass="chk" Style="margin-left: -5px;"
                            ToolTip="O paciente possui alguma alergia a medicamento?" runat="server" />
                        <asp:TextBox ID="txtAlergiaMedic" TextMode="MultiLine" Rows="6" Width="170px" MaxLength="40"
                            ToolTip="Informe qual(is) medicamento(s) que o paciente possui alergia" runat="server" />
                    </li>
                </ul>
            </li>
            <li style="margin-right: -5px; color: Blue;">
                <label>
                    Medicação de uso contínuo</label>
                <asp:TextBox ID="txtMedicacaoCont" TextMode="MultiLine" Rows="11" Width="132px" runat="server"
                    MaxLength="200" />
            </li>
            <li style="margin-left: 18px; color: Blue;">
                <label>
                    Sintomas</label>
                <asp:TextBox ID="txtSintomasModal" TextMode="MultiLine" Rows="11" Width="132px" runat="server"
                    MaxLength="200" />
            </li>
            <li style="clear: both; margin-left: 130px; color: Blue;">
                <label>
                    Dores</label>
                <asp:TextBox ID="txtDores" TextMode="MultiLine" Rows="11" Width="172px" runat="server"
                    MaxLength="200" />
            </li>
            <li style="margin-left: 11px; color: Blue;">
                <label>
                    Medicação</label>
                <asp:TextBox ID="txtMedicacao" TextMode="MultiLine" Rows="11" Width="132px" runat="server"
                    MaxLength="200" />
            </li>
            <li style="margin-left: 9px; color: Blue;">
                <label>
                    Observaçao Avaliação</label>
                <asp:TextBox ID="txtObservacaoAvalia" TextMode="MultiLine" Rows="11" Width="132px"
                    runat="server" MaxLength="200" />
            </li>
        </ul>
    </div>
    <div id="divProntuCon" style="display: none; height: 450px !important; overflow: hidden">
        <asp:HiddenField ID="hidIdProntuCon" runat="server" />
        <ul class="ulDados" style="width: 730px; margin-top: 0px !important; padding-top: 5px;">
            <li>
                <label>
                    Nº PRONTUÁRIO</label>
                <%--<asp:CheckBox runat="server" Checked="true" ID="chkNumPront" OnCheckedChanged="chkNumPront_CheckedChanged" AutoPostBack="true" style="margin: 0 -7px 0 -6px;" />--%>
                <asp:TextBox runat="server" ID="txtNumPront" MaxLength="20" Style="width: 60px;"
                    ToolTip="Número de Prontuário/Cadastro do Paciente"></asp:TextBox>
            </li>
            <li>
                <label>
                    Nº PASTA</label>
                <%-- <asp:CheckBox runat="server" ID="chkNumPasta" Enabled="false" OnCheckedChanged="chkNumPasta_CheckedChanged" AutoPostBack="true" style="margin: 0 -7px 0 -6px;"/>--%>
                <asp:TextBox runat="server" ID="txtNumPasta" MaxLength="20" Style="width: 60px;"
                    ToolTip="Número de identificação da pasta de documentos do Paciente"></asp:TextBox>
            </li>
            <li style="">
                <label for="drpPacienteProntuCon" title="Paciente" class="lblObrigatorio">
                    Paciente</label>
                <asp:DropDownList ID="drpPacienteProntuCon" OnSelectedIndexChanged="drpPacienteProntuCon_SelectedIndexChanged"
                    AutoPostBack="true" runat="server" Width="160px" Visible="false" ToolTip="Nome do Paciente">
                </asp:DropDownList>
                <asp:TextBox ID="txtPacienteProntuCon" Width="160px" ToolTip="Digite o nome ou parte do nome do paciente, no mínimo 3 letras."
                    runat="server" />
            </li>
            <li style="margin-top: 11px; margin-left: -4px;">
                <asp:ImageButton ID="imgbPesqPacienteProntuCon" Style="width: 16px;" runat="server"
                    ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" OnClick="imgbPesqPacNome_OnClick" />
                <asp:ImageButton ID="imgbVoltPacienteProntuCon" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                    OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" />
            </li>
            <li>
                <label>
                    Item de Prontuário</label>
                <asp:DropDownList runat="server" ID="ddlQualifPront" Width="150px" ToolTip="Item de prontuário a ser apresentado no Histórico do Paciente">
                </asp:DropDownList>
            </li>
            <li>
                <label>
                    Início
                </label>
                <asp:TextBox runat="server" ID="txtIniPront" CssClass="campoData" ToolTip="Data inicial de pesquisa do Histórico do Paciente"></asp:TextBox>
            </li>
            <li style="margin: 16px 0 0 0;">até</li>
            <li>
                <label>
                    Fim</label>
                <asp:TextBox runat="server" ID="txtFimPront" CssClass="campoData" ToolTip="Data final de pesquisa do Histórico do Paciente"></asp:TextBox>
            </li>
            <li style="margin: 11px 0 0 0;">
                <asp:ImageButton ID="imgBtnPesqPront" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                    OnClick="imgBtnPesqPront_OnClick" />
            </li>
            <li style="clear: both;">
                <label title="HISTÓRICO DE ATENDIMENTO DO PACIENTE" style="color: orange; font-size: 13px;
                    margin-bottom: -9px;">
                    HISTÓRICO DE ATENDIMENTO DO PACIENTE</label>
                <asp:ImageButton ID="imgBRel" runat="server" ToolTip="Emitir relatório dos prontuários selecionados"
                    ImageUrl="~/Library/IMG/Gestor_IcoImpres.ico" Width="15px" Height="15px" Style="margin: -26px 14px 2px 688px;"
                    OnClick="imgBRel_OnClick" />
                <%--<asp:TextBox ReadOnly="true" Font-Size="12px" ID="txtObsProntuCon" Width="600px"
                    Height="200px" TextMode="MultiLine" runat="server" />--%>
                <div runat="server" id="divObsProntuCon" style="font-size: 12px; width: 707px; height: 182px;
                    overflow: auto; border: 1px solid #BBBBBB;">
                </div>
            </li>
            <li id="descricaoTextBox" runat="server" style="clear: both;">
                <asp:Label ID="labelCadObsProntuCon" title="Inserir descrição" Style="color: Blue;"
                    runat="server">INSIRA A DESCRIÇÃO LOGO ABAIXO</asp:Label>
                <asp:TextBox ID="txtCadObsProntuCon" Font-Size="12px" Width="707px" Height="100px"
                    TextMode="MultiLine" runat="server" />
            </li>
            <li>
                <asp:Label ID="labelConfirmHora" title="Confirme a Data e Hora" runat="server">Confirme a Data e Hora</asp:Label>
                <!--<label title="Confirme a Data e Hora">Confirme a Data e Hora</label>-->
                <asp:TextBox ID="TextBoxConfirDataPront" CssClass="campoData" Font-Size="12px" Width="64px"
                    runat="server" />
                <asp:TextBox ID="TextBoxConfirHoraPront" CssClass="campoHora" Font-Size="12px" Width="33px"
                    runat="server" />
            </li>
            <li id="liConfirmarBotao" runat="server" class="liBtnAddA" style="clear: none !important;
                margin-left: 210px !important; margin-top: 8px !important; height: 15px;">
                <asp:LinkButton ID="lnkbImprimirProntuCon" ValidationGroup="prontu" runat="server"
                    OnClick="lnkbImprimirProntuCon_OnClick  " ToolTip="Confirmar prontuário convencional">
                    <asp:Label runat="server" ID="Label19" Text="CONFIRMAR" StyltxtCadObsProntuCone="margin-left: 2px;"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
     <asp:HiddenField ID="hidAgendSelec" runat="server" />
    <div id="divEncaminAtend" style="display: none; height: 100px !important; width: 370px;
        overflow: hidden">
       
        <ul>
            <li style="margin-bottom: 10px;">
                <asp:Label ID="lblConfEncam" Text=" - " runat="server" />
            </li>
            <li style="margin-bottom: 15px;">
                <asp:CheckBox Style="margin-left: 40px" Text="Visualizar o Histórico do Paciente"
                    runat="server" ID="divEncaminAtendCheck" />
            </li>
            <li class="liBtnConfirm" style="margin-left: 85px; width: 30px">
                <asp:LinkButton ID="lnkbAtendSim" OnClick="lnkbAtendSim_OnClick" runat="server" ToolTip="Confirma o encaminhamento do paciente para atendimento">
                    <label style="margin-left:5px; color:White;">SIM</label>
                </asp:LinkButton>
            </li>
            <li class="liBtnConfirm" style="margin: -22px 0 0 135px; width: 30px;">
                <asp:LinkButton ID="lnkbAtendNao" OnClick="lnkbAtendNao_OnClick" runat="server" ToolTip="Seleciona o paciente e mostra a situação atual do atendimento">
                    <label style="margin-left:5px; color:White;">NÃO</label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>

    <div id="modalReverterSituacao" style="display: none; height: 100px !important; width: 370px;
        overflow: hidden">
       
        <ul>
            <li style="margin-bottom: 10px;">
                <asp:Label ID="lblMsgReverte" Text=" - " runat="server" />
            </li>
            <li class="liBtnConfirm" style="margin-left: 85px; width: 30px">
                <asp:LinkButton ID="lnkReverte" OnClick="lnkReverte_OnClick" runat="server" ToolTip="Retornar o paciente para o status de atendimento">
                    <label style="margin-left:5px; color:White;">SIM</label>
                </asp:LinkButton>
            </li>
            <li class="liBtnConfirm" style="margin: -22px 0 0 135px; width: 30px;">
                <asp:LinkButton ID="lnkNReverte" OnClientClick="fecharReverterSiuacao()" runat="server" ToolTip="Não retornar o paciente para o status de atendimento">
                    <label style="margin-left:5px; color:White;">NÃO</label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>

    <div id="modalReverterSituacaoOK" style="display: none; height: 100px !important; width: 370px;
        overflow: hidden">
       
        <ul>
            <li style="margin-bottom: 10px;">
                <asp:Label ID="Label20" Text="Você não tem permissão para reverter o status desse paciente." runat="server" />
            </li>
            <li class="liBtnConfirm" style="margin-left: 112px; width: 30px">
                <asp:LinkButton ID="lnkReverterOK" runat="server" OnClientClick="fecharReverterSiuacao()">
                    <label style="margin-left:5px; color:White;">OK</label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>

    <%--<div id="divEncaminAtend" style="display: none; height: 100px !important; width: 270px">
        <asp:HiddenField ID="hidAgendSelec" runat="server" />
        <ul>
            <li style="margin-bottom: 10px;">
                <asp:Label ID="lblConfEncam" Text=" - " runat="server" />
            </li>
            <li class="liBtnConfirm" style="margin-left: 85px; width: 30px">
                <asp:LinkButton ID="lnkbAtendSim" OnClick="lnkbAtendSim_OnClick" runat="server" ToolTip="Confirma o encaminhamento do paciente para atendimento">
                    <label style="margin-left:5px; color:White;">SIM</label>
                </asp:LinkButton>
            </li>
            <li class="liBtnConfirm" style="margin: -22px 0 0 135px; width: 30px;">
                <asp:LinkButton ID="lnkbAtendNao" runat="server" ToolTip="Seleciona o paciente e mostra a situação atual do atendimento">
                    <label style="margin-left:5px; color:White;">NÃO</label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>--%>

    <div id="divEncam" style="display: none; margin-left: 12px;overflow: hidden;">
        <div  style="display:inline-flex; width:100%; border-bottom: 3px solid rgb(169, 208, 245); padding-bottom: 5px;">
            <div>
                <label><strong>FORMATO DE ENCAMINHAMENTO DE PACIENTES</strong></label>
                <asp:RadioButton Enabled="false" Checked="true"  AutoPostBack="true" OnCheckedChanged="RadioButtonEncamLivre_OnCheckedChanged" GroupName="TipoEncam" runat="server" ID="RadioButtonEncamLivre" />LIVRE - modelo
                <asp:DropDownList Width="140" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListModeloEncam_OnSelectedIndexChanged" ID="DropDownListModeloEncam"></asp:DropDownList>
            </div>
       </div>
        <asp:Panel runat="server" ID="Panel2">  
        <div style='height: 356px'>
            </div>
        </asp:Panel>
        <div style="clear: both; margin: 20px 0 0 -5px;">
            <asp:TextBox runat="server" ID="TextBoxObserEncam" TextMode="MultiLine" ClientIDMode="Static"
                Style="width: 743px; height: 35px; margin-left: 12px;" Font-Size="12px" placeholder=" Digite as observações sobre o Encaminhamento"></asp:TextBox>
        </div>
        <div style="display:flex">
          <div style="margin-top: 5px">     
            <div style="margin-bottom: 9px; margin-top: 9px;">
                <a href="#" onclick="AbreModalSalvarModeloEncam();" style="margin-left: 7px;" class="linkButton">
                    SALVAR MODELO</a>
            </div>
        </div>
        <div>
            <asp:Button ID="Button6" Style="background-color: #D8D8D8; margin-left: 308%;
                margin-top: 8px; width: 125%" runat="server" Height="25px" Text="IMPRIMIR ENCAMINHAMENTO"
                OnClick="BtnImprmirEmcam_Click" ToolTip="Emitir Receituario do Paciente." />
        </div>
        </div>
      
    </div>

    <div id="divSalvarModeloEncam" style="display: none; margin-left: 12px;overflow: hidden;z-index: 99999999;">
        <label>Nome do Modelo*</label>
        <asp:TextBox runat="server" Style="width: 258px; height: 17px;" MaxLength="30" ID="TextBoxModeloEncam"></asp:TextBox><br />
         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="ModeloEncam" ControlToValidate="TextBoxModeloEncam" ></asp:RequiredFieldValidator>
        <asp:LinkButton ID="LinkButton4"  CssClass="linkButton"  ValidationGroup="ModeloEncam" runat="server" Style="margin-left: 96px;" Text="SALVAR" OnClick="ButtonSalvarModeloEncam_Click"
                ToolTip="Salvar Modelo" />
     </div>

    <asp:HiddenField ID="hidTxtObserv" runat="server" />
    <div id="divObservacao" style="display: none; height: 300px !important; width: 580px">
        <ul>
            <li>
                <asp:TextBox runat="server" ID="txtObservacoes" TextMode="MultiLine" Style="width: 530px;
                    height: 240px;" Font-Size="13px" ToolTip="Digite as observações sobre o Atendimento"></asp:TextBox>
            </li>
            <li class="liBtnConfirm" style="margin: 10px 0 0 240px; width: 45px;">
                <asp:LinkButton ID="lnkbSalvarObserv" runat="server" OnClick="lnkbSalvarObserv_OnClick">
                    <label style="margin-left:5px; color:White;">SALVAR</label>
                </asp:LinkButton>
            </li>
            <li class="liBtnAddA" style="margin: -22px 10px 0 0; float: right; width: 45px;">
                <asp:LinkButton ID="lnkbImprimirObserv" runat="server" OnClick="lnkbImprimirObserv_OnClick">
                    <label style="margin-left:5px;">EMITIR</label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div id="divFichaAtendimento" style="display: none; height: 470px !important;">
        <ul class="ulDados" style="width: 410px; margin-top: 0px !important">
            <li>
                <label>
                    Queixas</label>
                <asp:TextBox runat="server" ID="txtQxsFicha" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
            </li>
            <li>
                <label>
                    Anamnese</label>
                <asp:TextBox runat="server" ID="txtAnamneseFicha" TextMode="MultiLine" Width="400px"
                    Height="50px"></asp:TextBox>
            </li>
            <li>
                <label>
                    Diagnostico</label>
                <asp:TextBox runat="server" ID="txtDiagnosticoFicha" TextMode="MultiLine" Width="400px"
                    Height="50px"></asp:TextBox>
            </li>
            <li>
                <label>
                    Exame</label>
                <asp:TextBox runat="server" ID="txtExameFicha" TextMode="MultiLine" Width="400px"
                    Height="50px"></asp:TextBox>
            </li>
            <li>
                <label>
                    Observação</label>
                <asp:TextBox runat="server" ID="txtObsFicha" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
            </li>
            <li id="li5" runat="server" class="liBtnAddA" style="float: right; margin-top: 10px !important;
                clear: none !important; height: 15px;">
                <asp:LinkButton ID="lnkbImprimirFicha" runat="server" OnClick="lnkbImprimirFicha_Click"
                    ToolTip="Imprimir ficha de atendimento">
                    <asp:Label runat="server" ID="lblEmitirFicha" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div id="divAnexos" style="display: none; height: 360px !important;">
        <asp:HiddenField runat="server" ID="hidTpAnexo" />
        <ul class="ulDados" style="width: 880px; margin-left: -10px !important; margin-top: 0px !important">
            <li style="clear: both; margin: 0 0 0 5px !important;">
                <div style="width: 862px; height: 215px; border: 1px solid #CCC; overflow-y: scroll">
                    <asp:GridView ID="grdAnexos" CssClass="grdBusca" runat="server" Style="width: 100%;
                        cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                        ShowHeaderWhenEmpty="true">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum arquivo associado<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="DT_CADAS" HeaderText="DATA">
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NU_REGIS" HeaderText="REGISTRO">
                                <ItemStyle Width="30px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NM_PROC_MEDI" HeaderText="PROCEDIMENTO">
                                <ItemStyle Width="180px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="TP">
                                <ItemStyle Width="10px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Image runat="server" ID="imgTpAnexo" ImageUrl='<%# Eval("URL_TP_ANEXO") %>'
                                        Width="16" Height="16" Style="margin: 0 0 0 -3px !important;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NM_TITULO" HeaderText="IDENTIFICAÇÃO DO ARQUIVO">
                                <ItemStyle Width="150px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="OBSERVAÇÕES">
                                <ItemStyle Width="250px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hidIdAnexo" Value='<%# Eval("ID_ANEXO_ATEND") %>' />
                                    <asp:Label ID="Label2" Text='<%# Eval("DE_OBSER_RES") %>' ToolTip='<%# Eval("DE_OBSER") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SOLICITANTE" HeaderText="SOLICITANTE">
                                <ItemStyle Width="150px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="BX">
                                <ItemStyle Width="10px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgbBxrAnexo" ImageUrl="/Library/IMG/Gestor_ServicosDownloadArquivos.png"
                                        ToolTip="Baixar Arquivo" OnClick="imgbBxrAnexo_OnClick" Width="16" Height="16"
                                        Style="margin: 0 0 0 -3px !important;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EX">
                                <ItemStyle Width="10px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgbExcAnexo" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                        ToolTip="Excluir Arquivo" OnClick="imgbExcAnexo_OnClick" Width="16" Height="16"
                                        Style="margin: 0 0 0 -3px !important;" OnClientClick="return confirm ('Tem certeza de que deseja excluir o arquivo?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
            <li class="liTituloGrid" style="width: 865px !important; height: 20px !important;
                margin-top: 15px; background-color: #A9E2F3; margin-bottom: 2px; padding-top: 2px;
                clear: both;">
                <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; float: left;
                    margin-left: 10px;">
                    INCLUSÃO DE ARQUIVO AO PACIENTE</label>
            </li>
            <li style="clear: both;">
                <label>
                    Tipo</label>
                <asp:DropDownList ID="drpTipoAnexo" Height="19px" runat="server">
                    <asp:ListItem Text="Anexo" Value="A" Selected="True" />
                    <asp:ListItem Text="Audio" Value="U" />
                    <asp:ListItem Text="Foto" Value="F" />
                    <asp:ListItem Text="Video" Value="V" />
                </asp:DropDownList>
            </li>
            <li>
                <label>
                    Arquivo</label>
                <asp:FileUpload ID="flupAnexo" runat="server" ClientIDMode="Static" />
            </li>
            <li>
                <label>
                    Identificação do Arquivo</label>
                <asp:TextBox ID="txtNomeAnexo" runat="server" Width="200px" Height="17px" ClientIDMode="Static" />
            </li>
            <li>
                <label>
                    Observações</label>
                <asp:TextBox ID="txtObservAnexo" TextMode="MultiLine" Width="318px" Height="17px"
                    runat="server" />
            </li>
            <li class="liBtnAddA" style="margin: 5px 10px 0 0; float: right; width: 45px;">
                <asp:LinkButton ID="lnkbAnexar" runat="server" OnClick="lnkbAnexar_OnClick">
                    <label style="margin-left:5px;">ANEXAR</label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <%--<div id="modalInf" class="modalInf" style="display: none; height: 360px !important;">
        <ul class="ulDados">
            <li style="border: 1px solid #00000">
                <ul>
                    <li style="margin-left: 2px; width: 99%; color: Blue; border-bottom: 2px solid #58ACFA;
                        margin-bottom: 2px; display: inline-flex;">
                        <label style="font-size: 12px; margin-right: 304px">
                            Registro de Informações</label>
                        <input id="btnModalInf" type="button" style="background: url('/Library/IMG/Gestor_BtnDel.png') no-repeat;
                            width: 20px; border: none; cursor: pointer;" onclick="hideModal(this)" />
                    </li>
                    <li style="clear: both; width: 120px;">
                        <ul>
                            <li style="margin-bottom: 4px;">
                                <label>
                                    Diabetes</label>
                                <asp:CheckBox ID="chkDiabetes" Style="margin: 0 -6px 0 -6px;" runat="server" />
                                <asp:DropDownList ID="drpTipoDiabete" Width="90px" runat="server">
                                    <asp:ListItem Value="" Text="Selecione" />
                                    <asp:ListItem Value="1" Text="Tipo 1" />
                                    <asp:ListItem Value="2" Text="Tipo 2" />
                                </asp:DropDownList>
                            </li>
                            <li style="clear: both;">
                                <label>
                                    Hipertensão</label>
                                <asp:CheckBox ID="chkHipertensao" Style="margin: 0 -6px 0 -6px;" runat="server" />
                                <asp:TextBox ID="txtHipertensao" Width="90px" runat="server" MaxLength="20" />
                            </li>
                            <li style="clear: both; margin-top: -8px;">
                                <label>
                                    Fumante (St/Anos)</label>
                                <asp:DropDownList ID="drpFumante" Width="85px" runat="server">
                                    <asp:ListItem Value="S" Text="Sim" />
                                    <asp:ListItem Value="N" Text="Não" />
                                    <asp:ListItem Value="E" Text="Ex-Fumante" />
                                </asp:DropDownList>
                                <asp:TextBox ID="txtFumanteAnos" CssClass="campoAnos" Height="13px" Width="20px"
                                    runat="server" />
                            </li>
                            <li style="clear: both; margin-top: -8px;">
                                <label>
                                    Alcool (St/Anos)</label>
                                <asp:DropDownList ID="drpAlcool" Width="85px" runat="server">
                                    <asp:ListItem Value="S" Text="Sim" />
                                    <asp:ListItem Value="N" Text="Não" />
                                    <asp:ListItem Value="A" Text="As Vezes" />
                                </asp:DropDownList>
                                <asp:TextBox ID="txtAlcoolAnos" CssClass="campoAnos" Height="13px" Width="20px" runat="server" />
                            </li>
                        </ul>
                    </li>
                    <li style="margin-left: -5px; border-left: 1px solid #BDBDBD; width: 185px;">
                        <ul>
                            <li>
                                <asp:CheckBox ID="chkCirurgia" Text="Cirurgia" CssClass="chk" Style="margin-left: -5px;"
                                    runat="server" />
                                <asp:TextBox ID="txtCirurgia" TextMode="MultiLine" Rows="3" Width="170px" runat="server"
                                    MaxLength="40" />
                            </li>
                            <li style="margin-top: -3px;">
                                <asp:CheckBox ID="chkAlergiaMedic" Text="Alergia" CssClass="chk" Style="margin-left: -5px;"
                                    ToolTip="O paciente possui alguma alergia a medicamento?" runat="server" />
                                <asp:TextBox ID="txtAlergiaMedic" TextMode="MultiLine" Rows="6" Width="170px" MaxLength="40"
                                    ToolTip="Informe qual(is) medicamento(s) que o paciente possui alergia" runat="server" />
                            </li>
                        </ul>
                    </li>
                    <li style="margin-right: -5px; color: Blue;">
                        <label>
                            Medicação de uso contínuo</label>
                        <asp:TextBox ID="txtMedicacaoCont" TextMode="MultiLine" Rows="8" Width="132px" runat="server"
                            MaxLength="200" />
                    </li>
                </ul>
            </li>
        </ul>
    </div>--%>
    <div id="obsTr" class="indice" style="display: none; width: 572px;">
        <ul class="ulDados">
            <li style="margin-left: 2px; width: 54%; color: Blue; border-bottom: 2px solid #58ACFA;
                margin-bottom: 2px; display: inline-flex;">
                <label style="font-size: 12px; margin-right: 469px">
                    Oberservação:</label>
                <input id="Button1" type="button" style="background: url('/Library/IMG/Gestor_BtnDel.png') no-repeat;
                    width: 20px; border: none; cursor: pointer;" onclick="hideObs(this)" />
            </li>
            <li>
                <textarea id="txtObsTr" cols="100" title="Observação referente ao uso deste protocolo para este atendimento. (Máximo de 200 carateres)"
                    rows="4" onkeydown="checkTextAreaMaxLength(this,event,'200');"></textarea>
                <input id="txtId" type="text" style="display: none" />
            </li>
        </ul>
    </div>
    <div id="divObsProSol" style="display: none; height: 443px; width: 590px">
        <ul class="ulDados">
            <li style="clear: both;">
                <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                    ANAMNESE / HDA (História da Doença Atual)</label>
                <textarea id="txtAnamRespModal" rows="6" cols="100" style="width: 557px; border-top: 1px solid #BDBDBD;
                    border-left: 0; border-right: 0; border-bottom: 0; font-size: 12px; background-color: #FAFAFA"
                    onkeydown="checkTextAreaMaxLength(this,event,'400');">
                </textarea>
            </li>
            <li style="margin-bottom: 0; clear: both;">
                <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                    HIPÓTESE DIAGNÓSTICA / AÇÃO REALIZADA</label>
                <textarea id="txtAcaoRepasModal" rows="6" cols="100" style="width: 557px; border-top: 1px solid #BDBDBD;
                    border-left: 0; border-right: 0; border-bottom: 0; font-size: 12px; background-color: #FAFAFA"
                    onkeydown="checkTextAreaMaxLength(this,event,'400');">
                </textarea>
            </li>
            <li style="margin-bottom: 0; clear: both;">
                <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                    OBSERVAÇÃO</label>
                <textarea id="txtObsPro" rows="3" cols="100" style="width: 557px; border-top: 1px solid #BDBDBD;
                    border-left: 0; border-right: 0; border-bottom: 0; font-size: 12px; background-color: #FAFAFA"
                    onkeydown="checkTextAreaMaxLength(this,event,'200');">
                </textarea>
                <input id="inputObsPro" type="text" style="display: none" />
            </li>
            <li style="margin-bottom: 0; clear: both;">
                <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                    EEXAMES FÍSICOS</label>
                <asp:DropDownList runat="server" ID="drpExamFisParecer" ClientIDMode="Static" Width="269px"
                    onclientselectedindexchanged="addExameFísico(this)">
                </asp:DropDownList>
            </li>
            <li style="margin-bottom: 0;">
                <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                    CID - PROTOCOLO</label>
                <asp:DropDownList runat="server" ID="drpCIDProtocolo" ClientIDMode="Static" Width="269px"
                    onclientselectedindexchanged="drpCIDProtocolo_OnSelectedIndexChanged(this)">
                </asp:DropDownList>
            </li>
            <li style="clear: both; margin-top: 6px; width: 269px; height: 77px; overflow-y: scroll;">
                <div id="tbExamesFisRepasse">
                    <table id="myTable" style="border: none">
                        <tbody id="myTBody">
                        </tbody>
                    </table>
                </div>
            </li>
            <li style="margin-top: 6px; width: 269px; height: 77px; overflow-y: scroll;">
                <div id="dvCIDRepasse">
                    <table id="tbCIDRepasse" style="border: none">
                        <tbody id="bodyCIDRepasse">
                        </tbody>
                    </table>
                </div>
            </li>
            <li style="clear: both; margin-top: 22px; border: solid 1px #75abff; background: #75abff;
                margin-left: 506px;">
                <input type="button" id="btnInserirProfSol" value="Salvar" style="height: 20px; background-color: #0b3e6f;
                    color: #fff; cursor: pointer; width: 56px" />
            </li>
            <asp:HiddenField ID="coColaboradorParecerMedico" ClientIDMode="Static" runat="server" />
        </ul>
    </div>
    <div id="dvIntarnar" class="" style="display: none; width: 870px;">
        <ul class="ulDados" style="margin-top: 4px !important;">
            <li runat="server" id="liInfoInternar">
                <asp:Label runat="server" Font-Bold="true" Style="color: Red" ID="lblInfoInternar"></asp:Label>
            </li>
            <li style="clear: both; width: 673px;">
                <ul>
                    <li>
                        <asp:HiddenField runat="server" ID="hidIdAtendimentoInternar" />
                        <asp:HiddenField runat="server" ID="hidNomePacienteInternar" />
                        <asp:Label runat="server" ID="lblNomePacienteInternar" CssClass="colorTextBlue">NOME DO PACIENTE</asp:Label><br />
                        <asp:TextBox runat="server" ID="txtNomePacienteInternar" Width="213px" Enabled="false"
                            CssClass="txtNomePacienteInternar"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Label runat="server" ID="lblRegAtendimentoInternar" CssClass="colorTextBlue">Nº REG. DE ATENDIMENTO</asp:Label><br />
                        <asp:TextBox runat="server" ID="txtRegAtendimentoInternar" Width="130px" Enabled="false"
                            CssClass="txtNomePacienteInternar"></asp:TextBox>
                    </li>
                    <li style="display: -webkit-inline-box; width: 134px;">
                        <asp:Label runat="server" ID="lblClassRiscoInternar" CssClass="colorTextRed">PRIORIDADE</asp:Label><br />
                        <asp:DropDownList runat="server" ID="drpClassRiscoInternar" ClientIDMode="Static"
                            Width="90px">
                            <asp:ListItem Value="X">Nenhuma</asp:ListItem>
                            <asp:ListItem Value="A">Alta</asp:ListItem>
                            <asp:ListItem Value="M">Média</asp:ListItem>
                            <asp:ListItem Value="N">Normal</asp:ListItem>
                            <asp:ListItem Value="B">Baixa</asp:ListItem>
                            <asp:ListItem Value="U">Urgente</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li style="float: right">
                        <asp:Label runat="server" ID="lblEncInternacao" CssClass="colorTextRed">Nº ENC. INTERNAÇÃO</asp:Label><br />
                        <asp:TextBox runat="server" ID="txtEncInternacao" Width="113px" Enabled="false" CssClass="txtNomePacienteInternar"></asp:TextBox>
                    </li>
                </ul>
            </li>
            <li>
                <ul>
                    <li>
                        <asp:Label runat="server" CssClass="colorTextBlue" ID="lblDadosIntanacao">DADOS INTERNAÇÃO</asp:Label>
                    </li>
                    <li style="clear: both;">
                        <asp:Label runat="server" ID="lblCaraterInternacao" class="lblObrigatorio">Caráter da Internação</asp:Label><br />
                        <asp:DropDownList runat="server" Width="100px" ID="drpCaraterInternacao">
                        </asp:DropDownList>
                    </li>
                    <li style="">
                        <asp:Label runat="server" ID="lblTipoInternacao" class="lblObrigatorio">Tipo de Internação</asp:Label><br />
                        <asp:DropDownList runat="server" Width="100px" ID="drpTipoInternacao">
                        </asp:DropDownList>
                    </li>
                    <li style="">
                        <asp:Label runat="server" ID="lblRegimeInternacao" class="lblObrigatorio">Regime de Internação</asp:Label><br />
                        <asp:DropDownList runat="server" Width="100px" ID="drpRegimeInternacao">
                        </asp:DropDownList>
                    </li>
                    <li style="">
                        <asp:Label runat="server" ID="lblDS" ToolTip="Quantidade de diárias solicitadas"
                            class="lblObrigatorio">DS</asp:Label><br />
                        <asp:TextBox runat="server" Width="20px" ID="txtDS" ToolTip="Quantidade de diárias solicitadas"
                            onkeypress="return fixedlength(this, event, 3);" onkeyup="return fixedlength(this, event, 3);"
                            MaxLength="3"></asp:TextBox>
                    </li>
                    <li style="clear: both">
                        <asp:Label runat="server" ID="lblIndicacaoClinica">Indicação Clínica</asp:Label><br />
                        <asp:TextBox TextMode="MultiLine" Width="235px" Rows="4" MaxLength="100" runat="server"
                            ID="txtIndicacaoClinica" onkeydown="checkTextAreaMaxLength(this,event,'100');"></asp:TextBox>
                    </li>
                    <li>
                        <ul>
                            <li style="">
                                <asp:Label runat="server" ID="lblTipoAcomodacao">Tipo de Acomodação</asp:Label><br />
                                <asp:DropDownList runat="server" Width="100px" ID="drpTipoAcomodacao">
                                </asp:DropDownList>
                            </li>
                            <li style="clear: both; float: right;">
                                <asp:Label runat="server" ID="lblDataProvavelAH" ToolTip="Data provável da admissão hospitalar"
                                    class="lblObrigatorio">Data Provável AH</asp:Label><br />
                                <asp:TextBox runat="server" class="campoData" ID="txtDataProvavelAH" ToolTip="Informe a data da internação"></asp:TextBox>
                            </li>
                        </ul>
                    </li>
                </ul>
            </li>
            <li>
                <ul>
                    <li>
                        <asp:Label runat="server" CssClass="colorTextBlue" ID="lblHipotesesDiagnosticas">HIPÓTESES DIAGNÓSTICAS</asp:Label>
                    </li>
                    <li style="clear: both">
                        <asp:Label runat="server" ID="lblTipoDoenca" ToolTip="Tipo de doença referida pelo paciente"
                            class="lblObrigatorio">Tipo de Doença</asp:Label><br />
                        <asp:DropDownList runat="server" Width="122px" ID="drpTipoDoenca">
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both">
                        <asp:Label runat="server" ID="lblTDRP" ToolTip="Tempo de doença referida pelo paciente"
                            class="lblObrigatorio">TDRP</asp:Label><br />
                        <asp:TextBox runat="server" Width="20px" ID="txtTDRP" ToolTip="Quantidade de tempo em dias, meses ou ano"
                            onkeypress="return fixedlength(this, event, 3);" onkeyup="return fixedlength(this, event, 3);"
                            MaxLength="3"></asp:TextBox>
                    </li>
                    <li style="margin-top: 10px;">
                        <asp:DropDownList runat="server" Width="88px" ID="drpTDRP" ToolTip="Dias, meses ou ano (referente ao número indicado na TDRP)">
                            <asp:ListItem Value="">Selecione</asp:ListItem>
                            <asp:ListItem Value="A">Anos</asp:ListItem>
                            <asp:ListItem Value="M">Meses</asp:ListItem>
                            <asp:ListItem Value="D">Dias</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both">
                        <asp:Label runat="server" ID="lblIndicacaoAcidente" class="lblObrigatorio">Indicação de Acidente</asp:Label><br />
                        <asp:DropDownList runat="server" Width="122px" ID="drpIndicacaoAcidente">
                        </asp:DropDownList>
                    </li>
                </ul>
            </li>
            <li>
                <ul>
                    <li style="clear: both">
                        <asp:Label runat="server" CssClass="colorTextBlue" ID="lbl">CID INTERNAÇÃO</asp:Label>
                    </li>
                    <li style="clear: both">
                        <div id="div13" style="width: 143px; height: 97px; border: 1px solid #CCC; overflow-y: scroll">
                            <input type="hidden" id="Hidden5" name="" />
                            <asp:GridView ID="grdCIDInternar" CssClass="grdBusca grdExamFis" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="false" AllowUserToAddRows="false">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum item referente ao Protocolo da CID foi adicionado<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label runat="server" ID="lblCIDPrincipal" ToolTip="Escolha a(s) CID(s) principal(is) ">CP</asp:Label>
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="10px" />
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chcCIDPrincipal" Checked='<%# Eval("isPrincipal") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CID">
                                        <ItemStyle HorizontalAlign="Left" Width="25px" />
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hidIdItemCIDInternar" Value='<%# Eval("idItem") %>' />
                                            <asp:HiddenField runat="server" ID="idListaCIDInternar" Value='<%# Eval("idCID") %>' />
                                            <asp:Label runat="server" ID="lblProtCIDInternar" Text='<%# Eval("coCID")%>' ToolTip='<%# Eval("descCID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="JE">
                                        <ItemStyle Width="25px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <input type="button" runat="server" id="btnObsProtCIDInternar" style="background: url('/Library/IMG/PGS_CentralRegulacao_Icone_Obs_15x15.png');
                                                width: 15px; height: 15px;" title="Definir o(s) protocolo(s) da CID utilizado neste atendimento"
                                                visible='<%# Eval("existeProtocolo")%>' onclick="addProtocoloCID_Click(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </li>
            <li style="clear: both">
                <ul>
                    <li>
                        <asp:Label runat="server" CssClass="colorTextBlue" ID="Label14">PROCEDIMENTOS SOLICITADOS</asp:Label>
                    </li>
                    <li style="clear: both;">
                        <asp:DropDownList runat="server" Width="300px" ID="ddlTipoProcedimentoInternar" ClientIDMode="Static"
                            AutoPostBack="true" ToolTip="Procedimento que será utilizado no processo de internação"
                            OnSelectedIndexChanged="ddlTipoProcedimentoInternar_OnSelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both;">
                        <div id="div5" style="width: 662px; height: 60px; border: 1px solid #CCC; overflow-y: scroll">
                            <input type="hidden" id="Hidden6" name="" />
                            <asp:GridView ID="grdProcedimentoInternar" CssClass="grdBusca grdExamFis" runat="server"
                                Style="width: 100%; cursor: default;" AutoGenerateColumns="false" AllowPaging="false"
                                GridLines="Vertical" ShowHeaderWhenEmpty="false" AllowUserToAddRows="false">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum item referente ao Procedimento foi adicionado<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="IdProcedimento" HeaderText="Item" HtmlEncode="false" />
                                    <asp:BoundField DataField="TipoProcedimento" HeaderText="Tipo" HtmlEncode="false" />
                                    <asp:BoundField DataField="NomeProcedimento" HeaderText="Procedimento" HtmlEncode="false" />
                                    <asp:BoundField DataField="CodigoProcedimento" HeaderText="Código" HtmlEncode="false" />
                                    <asp:TemplateField HeaderText="Qtde">
                                        <HeaderStyle CssClass="lblObrigatorio" />
                                        <ItemStyle Width="30px" />
                                        <ItemTemplate>
                                            <asp:TextBox Width="20px" CssClass="magin0" runat="server" ID="qtdProcedimento" MaxLength="3"
                                                onblur="vlTotalProcedimentoIternar(this);" onkeypress="return fixedlength(this, event, 3);"
                                                onkeyup="return fixedlength(this, event, 3);" Text='<%# Eval("QuantidadeProcedimento")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="R$ Unitário">
                                        <ItemTemplate>
                                            <asp:TextBox Width="46px" Enabled="false" CssClass="magin0 border0" runat="server"
                                                ID="VlUnitarioProcedimentoInternar" Text='<%# Eval("VlUnitarioProcedimento")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="R$ Total">
                                        <ItemTemplate>
                                            <asp:TextBox Width="46px" Enabled="false" CssClass="magin0 border0" runat="server"
                                                ID="vlTotalProcedimentoInternar" Text='<%# Eval("VlTotalProcedimento")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EX">
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" Width="15px" ID="btnDelProcedimentoInternar" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                ToolTip="Excluir procedimento desta internação" OnClick="btnDelProcedimentoInternar_OnClick"
                                                OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </li>
            <li style="clear: both">
                <ul>
                    <li>
                        <asp:Label runat="server" CssClass="colorTextBlue" ID="Label15">OPM (Órteses, Próteses e Materiais especiais) SOLICITADOS</asp:Label>
                    </li>
                    <li style="clear: both;">
                        <asp:DropDownList runat="server" Width="300px" ID="ddlOPMInternar" ClientIDMode="Static"
                            AutoPostBack="true" ToolTip="Órteses, Próteses e/ou Materiais especiais utilizados no processo de internação"
                            OnSelectedIndexChanged="ddlOPMInternar_OnSelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both;">
                        <div id="div6" style="width: 662px; height: 60px; border: 1px solid #CCC; overflow-y: scroll">
                            <input type="hidden" id="Hidden7" name="" />
                            <asp:GridView ID="grdOPMInternar" CssClass="grdBusca grdExamFis" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="false" AllowUserToAddRows="false">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum item referente a OPM foi adicionado<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="IdOPM" HeaderText="Item" HtmlEncode="false" />
                                    <asp:BoundField DataField="TipoOPM" HeaderText="Tipo" HtmlEncode="false" />
                                    <asp:BoundField DataField="NomeOPM" HeaderText="Procedimento" HtmlEncode="false" />
                                    <asp:BoundField DataField="CodigoOPM" HeaderText="Código" HtmlEncode="false" />
                                    <asp:TemplateField HeaderText="Qtde">
                                        <HeaderStyle CssClass="lblObrigatorio" />
                                        <ItemStyle Width="30px" />
                                        <ItemTemplate>
                                            <asp:TextBox Width="20px" CssClass="magin0" runat="server" ID="qtdOPM" Text='<%# Eval("QuantidadeOPM")%>'
                                                onblur="vlTotalOPM(this);" onkeypress="return fixedlength(this, event, 3);" onkeyup="return fixedlength(this, event, 3);"
                                                MaxLength="3"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fabricante">
                                        <ItemTemplate>
                                            <asp:TextBox Width="100px" CssClass="magin0" runat="server" ID="fabricanteOPM" Text='<%# Eval("FabricanteOPM")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="R$ Unitário">
                                        <ItemTemplate>
                                            <asp:TextBox Width="46px" Enabled="false" CssClass="magin0 border0" runat="server"
                                                ID="VlUnitarioOPM" Text='<%# Eval("VlUnitarioOPM")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="R$ Total">
                                        <ItemTemplate>
                                            <asp:TextBox Width="46px" Enabled="false" CssClass="magin0 border0" runat="server"
                                                ID="qtdVlTotalOPM" Text='<%# Eval("VlTotalOPM")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EX">
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" Width="15px" ID="btnDelOPMInternar" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                ToolTip="Excluir OPM desta internação" OnClick="btnDelOPMInternar_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </li>
            <li style="clear: both; width: 673px;">
                <ul>
                    <li style="float: right">
                        <asp:Button runat="server" ID="btnSalvarInternar" Text="SALVAR" Style="height: 20px;
                            background-color: #0b3e6f; color: #fff; cursor: pointer; width: 56px" Font-Bold="true"
                            OnClick="btnSalvarInternar_OnClick" />
                    </li>
                </ul>
            </li>
        </ul>
    </div>
    <div id="dvObito" class="" style="display: none; width: 450px; height: 200px;">
        <ul class="ulDados">
            <li style="width: 273px; margin-bottom: 13px;">
                <asp:Label ID="Label17" runat="server" CssClass="colorTextBlue">NOME DO PACIENTE</asp:Label><br />
                <asp:TextBox runat="server" ID="txtPacienteObito" Width="232px" CssClass="txtNomePacienteInternar"></asp:TextBox>
            </li>
            <li>
                <asp:Label ID="Label18" runat="server" CssClass="colorTextBlue">Nº REG. DE ATENDIMENTO:</asp:Label><br />
                <asp:TextBox runat="server" Style="color: Red" ID="txtRegAtendimentoObito" CssClass="txtNomePacienteInternar"></asp:TextBox>
            </li>
            <li style="clear: both">
                <asp:Label ID="lblDataObito" runat="server" CssClass="txtNomePacienteInternar">DATA DO ÓBITO:</asp:Label><br />
                <asp:TextBox CssClass="campoData" runat="server" ID="txtDataObito"></asp:TextBox>
            </li>
            <li style="">
                <asp:Label runat="server" ID="lblHoraObito" CssClass="txtNomePacienteInternar">HORA DO ÓBITO:</asp:Label><br />
                <asp:TextBox runat="server" CssClass="campoHora" Width="55px" ID="txtHoraObito"></asp:TextBox>
            </li>
            <li style="clear: both">
                <asp:Label runat="server" ID="Label16" CssClass="txtNomePacienteInternar">OBSERVAÇÃO:</asp:Label><br />
                <asp:TextBox TextMode="MultiLine" Width="413px" Height="40px" Rows="4" MaxLength="100"
                    runat="server" ID="txtObsObito" onkeydown="checkTextAreaMaxLength(this,event,'200');"
                    CssClass="txtNomePacienteInternar"></asp:TextBox>
            </li>
            <li style="clear: both; margin-left: 364px;">
                <asp:Button runat="server" ID="Button2" Text="SALVAR" Style="height: 20px; background-color: #0b3e6f;
                    color: #fff; cursor: pointer; width: 56px" Font-Bold="true" OnClick="btnSalvarObito_OnClick" />
            </li>
        </ul>
    </div>
    <script src="/Library/JS/Cronometro.js" type="text/javascript"></script>
    <script type="text/javascript">

        window.onload = function () {
            MaintainScroll1();
        }

        $('#drpExamFisParecer').change(function (e) {
            var e = document.getElementById("drpExamFisParecer");
            var itemSelecionadoValue = e.options[e.selectedIndex].value;
            var itemSelecionadoText = e.options[e.selectedIndex].text;
            if (itemSelecionadoValue != "") {
                var tbody = $("#myTBody");
                tbody.append("<tr class='item'><td style=''><span class='text'>" + itemSelecionadoText + "</span></td><td style='display:none'><span class='value'>" + itemSelecionadoValue + "</span></td></tr>");
            }
        });


        $('#drpCIDProtocolo').change(function (e) {
            var e = document.getElementById("drpCIDProtocolo");
            var itemSelecionadoValue = e.options[e.selectedIndex].value;
            var itemSelecionadoText = e.options[e.selectedIndex].text;
            if (itemSelecionadoValue != "") {
                var tbody = $("#bodyCIDRepasse");
                tbody.append("<tr class='itemCID'><td style=''><span class='textCID'>" + itemSelecionadoText + "</span></td><td style='display:none'><span class='valueCID'>" + itemSelecionadoValue + "</span></td></tr>");
            }
        });

        function addItensCID(modal) {
            var idBtn = $("#inputObsPro").val();
            var idTxt = idBtn.replace("btnObsProfSol", "txtObsProfSol");
            var idAnamnese = idBtn.replace("btnObsProfSol", "txtAnamRepas");
            var idAcao = idBtn.replace("btnObsProfSol", "txtAcaoRepas");
            var idlisExa = idBtn.replace("btnObsProfSol", "IdsExamRepas");
            var idlisCID = idBtn.replace("btnObsProfSol", "txtItemCID");

            var obs = $("#txtObsPro").val();
            var anam = $("#txtAnamRespModal").val();
            var acao = $("#txtAcaoRepasModal").val();
            var listExa = listarIdExame();

            $('#' + idTxt + '').val(obs);
            $('#' + idAnamnese + '').val(anam);
            $('#' + idAcao + '').val(acao);
            $('#' + idlisExa + '').val(listExa);
            $('#hidmodalid').val(idBtn);
        }

        function AbreModalProntuCon() {
            $('#divProntuCon').dialog({
                autoopen: false, modal: true, width: 750, height: 455, top: 98, left: 175, resizable: false, title: "PRONTUÁRIO CONVENCIONAL",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }
        function AbreModalSalvarModeloExame() {

            $('#divSalvarModeloExame').dialog({
                autoopen: false, modal: true, width: 300, height: 100, resizable: false, title: "SALVAR MODELO",
                open: function (type, data) { $(this).parent().appendTo("form"); $('#divExames').parent().replaceWith(""); $('#editorExame').hide(); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
            return false;
        }

        function AbreModalParecerProf() {
            var idBtn = $("#hidmodalid").val();
            var idTxt = idBtn.replace("btnObsProfSol", "txtObsProfSol");
            var idAnamnese = idBtn.replace("btnObsProfSol", "txtAnamRepas");
            var idAcao = idBtn.replace("btnObsProfSol", "txtAcaoRepas");
            var idlisExa = idBtn.replace("btnObsProfSol", "IdsExamRepas");
            var idlisCID = idBtn.replace("btnObsProfSol", "txtItemCID");

            var obs = $('#' + idTxt + '').val();
            var anam = $('#' + idAnamnese + '').val();
            var acao = $('#' + idAcao + '').val();
            var exam = $('#' + idlisExa + '').val();

            var arList = new Array();
            if (exam.indexOf(",") >= 0) {
                arList = exam.toString().split(",");
                var tbody = $("#myTBody");
                var nome = "";
                var valr = "";
                $.each(arList, function (i, elem) {
                    if (i % 2 == 0) {
                        nome = elem;
                    }
                    if (i % 2 != 0) {
                        valr = elem;
                        tbody.append("<tr class='item'><td style=''><span class='text'>" + nome + "</span></td><td style='display:none'><span class='value'>" + valr + "</span></td></tr>");
                    }
                })
            }

            $("#txtAnamRespModal").val(anam);
            $("#txtAc0aoRepasModal").val(acao);
            $("#txtObsPro").val(obs);

            $("#divObsProSol").css("display", "block");
            $("#divBack").css("display", "block");

        }

        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                        e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                }
            }
        }
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }

        function MaintainScroll1() {
            var div = document.getElementById("divAgendasRecp");
            var div_position = document.getElementById("divAgendasRecp_posicao");
            var position = parseInt('<%= Request.Form["divAgendasRecp_posicao"] %>');
            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }

        function MaintainScroll2() {
            var div = document.getElementById("divDemonAge");
            var div_position = document.getElementById("divDemonAge_posicao");
            var position = parseInt('<%= Request.Form["divDemonAge_posicao"] %>');
            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }

        $(document).ready(function () {
            $("#flupAnexo").change(function () {
                if ($("#txtNomeAnexo").val() == "") {
                    var fileName = $("#flupAnexo").val();
                    while (fileName.indexOf("\\") != -1)
                        fileName = fileName.slice(fileName.indexOf("\\") + 1);
                    $("#txtNomeAnexo").val(fileName);
                }
            });
        });

        function AbreModalLog() {
            $('#divLoadShowLogAgenda').dialog({
                autoopen: false, modal: true, width: 902, height: 340, resizable: false, title: "HISTÓRICO DO AGENDAMENTO DE ATENDIMENTO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalNovoMedic() {
            $('#divLoadShowNovoMedic').dialog({
                autoopen: false, modal: true, width: 360, height: 390, resizable: false, title: "NOVO MEDICAMENTO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalNovoExam() {
            $('#divLoadShowNovoExam').dialog({
                autoopen: false, modal: true, width: 560, height: 300, resizable: false, title: "NOVO EXAME",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalAtestado() {
            $('#divAtestado').dialog({
                autoopen: false, modal: true, width: 530, height: 370, resizable: false, title: "EMISSÃO DE ATESTADO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalGuiaPlano() {
            $('#divGuiaPlano').dialog({
                autoopen: false, modal: true, width: 450, height: 180, resizable: false, title: "IMPRESSÃO DA GUIA DE ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalLaudo() {
            $('#divLaudo').dialog({
                autoopen: false, modal: true, width: 555, height: 350, resizable: false, title: "LAUDO TÉCNICO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalMaisInfo(nome, data) {

            $('#divMaisInfo').dialog({
                autoopen: false, modal: true, width: 655, height: 330, left: 365, resizable: false, title: "INFORMAÇÕES COMPLEMENTARES DA AVALIAÇÃO DE RISCO - Paciente: " + nome,
                open: function (type, data) { $(this).parent().appendTo("form"); }
            });
        }

        function AbreModalEncam() {

            $('#divEncam').dialog({
                autoopen: false, modal: true, width: 800, height: 530, resizable: false, title: "ENCAMINHAMENTO DE PACIENTES",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); $('#editorEncam').hide(); }
            });
            $('#editorEncam').show();
        }

        function AbreModalEncamAtend() {
            $('#divEncaminAtend').dialog({
                autoopen: false, modal: true, width: 280, height: 120, resizable: false, title: "ENCAMINHAMENTO PARA ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalReverterSituacao() {
            
            $('#modalReverterSituacao').dialog({
                autoopen: false, modal: true, width: 280, height: 120, resizable: false, title: "REVERTER PARA ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }


        function AbreModalReverterSituacaoOK() {

            $('#modalReverterSituacaoOK').dialog({
                autoopen: false, modal: true, width: 280, height: 120, resizable: false, title: "REVERTER PARA ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function fecharReverterSiuacao() {
            $('#modalReverterSituacao').dialog('close');
        }

        function AbreModalObservacao() {
            $('#divObservacao').dialog({
                autoopen: false, modal: true, width: 560, height: 320, resizable: false, title: "OBSERVAÇÕES DO ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalFichaAtendimento() {
            $('#divFichaAtendimento').dialog({
                autoopen: false, modal: true, width: 470, height: 470, resizable: false, title: "IMPRESSÃO DA FICHA DE ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

//        function AbreModalMedicamentos() {
//            $('#divMedicamentos').dialog({
//                autoopen: false, modal: true, width: 800, height: 430, resizable: false, title: "MEDICAMENTOS DO PACIENTE NESTE ATENDIMENTO",
//                open: function (type, data) { $(this).parent().appendTo("form"); },
//                close: function (type, data) { ($(this).parent().replaceWith("")); }
//            });
        //        }
        function AbreModalMedicamentos() {

            $('#divMedicamentos').dialog({
                autoopen: false, modal: true, width: 800, height: 520, resizable: false, title: "MEDICAMENTOS DO PACIENTE NESTE ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); $('#editorReceituario').hide(); }
            });
            if ($("#<%=RadioButtonLivre.ClientID%>").is(':checked')) {
                $('#editorReceituario').show();
            }
        }

//        function AbreModalExames() {
//            $('#divExames').dialog({
//                autoopen: false, modal: true, width: 800, height: 300, resizable: false, title: "EXAMES DO PACIENTE NESTE ATENDIMENTO",
//                open: function (type, data) { $(this).parent().appendTo("form"); },
//                close: function (type, data) { ($(this).parent().replaceWith("")); }
//            });
//        }
        function AbreModalExames() {
            $('#divExames').dialog({
                autoopen: false, modal: true, width: 800, height: 520, resizable: false, title: "EXAMES DO PACIENTE NESTE ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); $('#editorExame').hide(); }
            });
            if ($("#<%=RadioButtonLivreExames.ClientID%>").is(':checked')) {
                $('#editorExame').show();
            }
        }
        function AbreModalExameFisico() {
            $('#divExameFisico').dialog({
                autoopen: false, modal: true, width: 450, height: 300, resizable: false, title: "EXAMES FÍSICOS",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalProtocoloCID() {
            $('#divProtocoloCID').dialog({
                autoopen: false, modal: true, width: 560, height: 300, resizable: false, title: "CID - PROTOCOLOS",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalProfissionalSolicitado() {
            $('#divObsProSol').dialog({
                autoopen: false, modal: true, width: 590, height: 480, resizable: false, title: "PARECER PROFISSIONAL MÉDICO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalProntuCon() {
            $('#divProntuCon').dialog({
                autoopen: false, modal: true, width: 750, height: 455, top: 98, left: 175, resizable: false, title: "PRONTUÁRIO CONVENCIONAL",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalSalvarModeloEncam() {

            $('#divSalvarModeloEncam').dialog({
                autoopen: false, modal: true, width: 300, height: 100, resizable: false, title: "SALVAR MODELO",
                open: function (type, data) { $(this).parent().appendTo("form"); $('#divEncam').parent().replaceWith(""); $('#editorEncam').hide(); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
            return false;
        }
        function AbreModalSalvarModeloReceita() {

            $('#divSalvarModeloReceita').dialog({
                autoopen: false, modal: true, width: 300, height: 100, resizable: false, title: "SALVAR MODELO",
                open: function (type, data) { $(this).parent().appendTo("form"); $('#divMedicamentos').parent().replaceWith(""); $('#editorReceituario').hide(); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
            return false;
        }

//        function AbreModalAmbulatorio() {
//            $('#divAmbulatorio').dialog({
//                autoopen: false, modal: true, width: 990, height: 400, resizable: false, title: "PROCEDIMENTOS AMBULATORIAIS DO PACIENTE NESTE ATENDIMENTO",
//                open: function (type, data) { $(this).parent().appendTo("form"); },
//                close: function (type, data) { ($(this).parent().replaceWith("")); }
//            });
        //        }
        function AbreModalAmbulatorio() {
            $('#divAmbulatorio').dialog({
                autoopen: false, modal: true, width: 900, height: 600, resizable: false, title: "PROCEDIMENTOS AMBULATORIAIS DO PACIENTE NESTE ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); $('#editorProcedimento').hide(); }
            });
            if ($("#<%=RadioButtonAmbLivre.ClientID%>").is(':checked')) {
                $('#editorProcedimento').show();
            }
        }

        function AbreModalOrcamentos() {
            $('#divOrcamentos').dialog({
                autoopen: false, modal: true, width: 580, height: 300, resizable: false, title: "ORÇAMENTO DO PACIENTE NESTE ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalSalvarModeloAmb() {

            $('#divSalvarModeloAmb').dialog({
                autoopen: false, modal: true, width: 300, height: 100, resizable: false, title: "SALVAR MODELO",
                open: function (type, data) { $(this).parent().appendTo("form"); $('#divAmbulatorio').parent().replaceWith(""); $('#editorProcedimento').hide(); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
            return false;
        }

        function AbreModalAnexos() {
            $('#divAnexos').dialog({
                autoopen: false, modal: true, width: 880, height: 365, resizable: false, title: "ARQUIVOS ANEXADOS AO PACIENTE",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalAvisoPermissao() {
            $('#divAvisoPermissao').dialog({
                dialogClass: 'divAvisoPermissao', height: 100, autoopen: false, modal: true, resizable: false, title: "AVISO DE PERMISSÃO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { BackToHome(); }
            });
        }

        function AbreModalInternar() {
            $('#dvIntarnar').dialog({
                autoopen: false, modal: true, width: 707, height: 500, resizable: false, title: "GUIA DE INTERNAÇÃO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalObito() {
            $('#dvObito').dialog({
                autoopen: false, modal: true, width: 447, height: 248, resizable: false, title: "ÓBITO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        jQuery(function ($) {
            $(".campoCodigo").mask("?9999999999");
            $(".campoQtd").mask("?999");
            $(".campoHora").mask("99:99");
            $(".campoAnos").mask("?99");
            $(".campoPeso").mask("?99,99");
            $(".campoTemp").mask("99.9");
            $(".campoPressArteri").mask("?99/99");
            $(".campoGlicem").mask("?9999");
            $(".campoAltura").mask("9,99");
            $(".campoDecimal").maskMoney({ symbol: "", decimal: ",", thousands: "." });

            //Se ao deixar o campo, o mesmo estiver vazio, insere 0
            $("#txtVlCusto").blur(function () {
                if ($("#txtVlCusto").val() == "")
                    $("#txtVlCusto").val("0,00");
            });
            $("#txtVlBase").blur(function () {
                if ($("#txtVlBase").val() == "")
                    $("#txtVlBase").val("0,00");
            });
            $("#txtVlRestitu").blur(function () {
                if ($("#txtVlRestitu").val() == "")
                    $("#txtVlRestitu").val("0,00");
            });

            $("#chkAprovado").change(function () {
                if ($("#chkAprovado").attr("checked"))
                    $("#hidCkOrcamAprov").val('S');
                else
                    $("#hidCkOrcamAprov").val('N');
            });

            $("#txtDtValidade").change(function () {
                $("#hidDtValidOrcam").val($("#txtDtValidade").val());
            });

            $("#txtObserMedicam").change(function () {
                $("#hidObserMedicam").val($("#txtObserMedicam").val());
            });

            $("#txtObsServAmbulatoriais").change(function () {
                $("#hidObsSerAmbulatoriais").val($("#txtObsServAmbulatoriais").val());
            });

            $("#txtObserExame").change(function () {
                $("#hidObserExame").val($("#txtObserExame").val());
            });

            $("#txtObsOrcam").change(function () {
                $("#hidObsOrcam").val($("#txtObsOrcam").val());
            });

            if (!($("#rdbMedic").attr("checked"))) {
                $("#txtMedicamento").enable(false);
                $("#txtPrincipio").val("");
            };

            if (!($("#rdbPrinc").attr("checked"))) {
                $("#txtPrincipio").enable(false);
                $("#txtMedicamento").val("");
            };

            $("#rdbMedic").change(function () {
                if ($("#rdbMedic").attr("checked")) {
                    $("#txtMedicamento").enable(true);
                    $("#txtPrincipio").enable(false);
                    $("#txtPrincipio").val("");
                }
                else {
                    $("#txtMedicamento").enable(false);
                    $("#txtPrincipio").enable(true);
                    $("#txtMedicamento").val("");
                }
            });

            $("#rdbPrinc").change(function () {
                if ($("#rdbPrinc").attr("checked")) {
                    $("#txtPrincipio").enable(true);
                    $("#txtMedicamento").enable(false);
                    $("#txtMedicamento").val("");
                }
                else {
                    $("#txtPrincipio").enable(false);
                    $("#txtMedicamento").enable(true);
                    $("#txtPrincipio").val("");
                }
            });

            //Executado ao trocar a seleção no DropDownList para mudar a cor para a correspondente
            $("#ddlClassRisco").change(function (evento) {
                var e = document.getElementById("ddlClassRisco");
                var itSelec = e.options[e.selectedIndex].value;
                if (itSelec == "" || itSelec == 0) {
                    $("#divClassInternar").hide()
                    $("#divClassInternar").css("background-color", "White");
                    $("#divClassInternar").show(100);
                }
                else if (itSelec == 1) {
                    $("#divClassInternar").hide()
                    $("#divClassInternar").css("background-color", "Red");
                    $("#divClassInternar").show(100);
                }
                else if (itSelec == 2) {
                    $("#divClassInternar").hide();
                    $("#divClassInternar").css("background-color", "Orange");
                    $("#divClassInternar").show(100);
                }
                else if (itSelec == 3) {
                    $("#divClassInternar").hide();
                    $("#divClassInternar").css("background-color", "Yellow");
                    $("#divClassInternar").show(100);
                }
                else if (itSelec == 4) {
                    $("#divClassInternar").hide();
                    $("#divClassInternar").css("background-color", "Green");
                    $("#divClassInternar").show(100);
                }
                else if (itSelec == 5) {
                    $("#divClassInternar").hide();
                    $("#divClassInternar").css("background-color", "Blue");
                    $("#divClassInternar").show(100);
                }
            });

            //Executado quando se clica no box de cor de class risco, e abre o leque de opções disponíveis com animação
            $("#divClassRisc").click(function () {
                var sele = $("#hidDivAberta").val();
                $("#divClassInternar").hide();
                if (sele != 1) {
                    $("#divClassRisc").animate({
                        height: "30px",
                        width: "240px",
                        margin: "-50px 0 0 -150px",
                        padding: "5px"
                    }, 500, function () {
                        //Ao final da animação, são mostradas as opções de cor
                        $("#divClass1").css("display", "block");
                        $("#divClass2").css("display", "block");
                        $("#divClass3").css("display", "block");
                        $("#divClass4").css("display", "block");
                        $("#divClass5").css("display", "block");

                        //Mostra a imagem para fechar a janela
                        $("#divFecha").css("display", "block");

                        //Remove o ícone de "mãozinha" quando termina a animação
                        $("#divClassRisc").css("cursor", "auto");
                    });
                }
            });

            function PrepararAnimacao(numClass, cor) {
                $("#hidDivAberta").val("1");
                $("#divClass1").css("display", "none");
                $("#divClass2").css("display", "none");
                $("#divClass3").css("display", "none");
                $("#divClass4").css("display", "none");
                $("#divClass5").css("display", "none");
                //Esconde a imagem para fechar a janela
                $("#divFecha").css("display", "none");

                $("#divClassRisc").animate({
                    width: "11px",
                    margin: "13px 0 0 -17px",
                    height: "9px",
                    padding: "2px"
                }, 500, function () {
                    //seleciona a classificação de risco correspondente
                    if (numClass != null)
                        $("#ddlClassRisco").val(numClass);

                    //Altera a div pai das classificações conforme necessário
                    $("#divClassRisc").css("cursor", "pointer");

                    if (cor != null)
                        $("#divClassRiscCorSelec").css("background-color", cor);

                    $("#divClassRiscCorSelec").show(100);

                    //Garante que o método pai não vai ser chamado evitando um loop
                    $("#hidDivAberta").val("");
                });
            };

            //Executado quando é selecionada a classificação de risco de EMERGENCIA (VERMELHA)
            $("#divClass1").click(function () {
                PrepararAnimacao("1", "Red");
            });

            //Executado quando é selecionada a classificação de risco de MUITO URGENTE (LARANJA)
            $("#divClass2").click(function () {
                PrepararAnimacao("2", "Orange");
            });

            //Executado quando é selecionada a classificação de risco de URGENTE (AMARELA)
            $("#divClass3").click(function () {
                PrepararAnimacao("3", "Yellow");
            });

            //Executado quando é selecionada a classificação de risco de POUCO URGENTE (VERDE)
            $("#divClass4").click(function () {
                PrepararAnimacao("4", "Green");
            });

            //Executado quando é selecionada a classificação de risco de NÃO URGENTE (AZUL)
            $("#divClass5").click(function () {
                PrepararAnimacao("5", "Blue");
            });

            //Executado quando se clica no botão para fechar a paleta de opções
            $("#lnkClose").click(function () {
                PrepararAnimacao();
            });
        });


        function startUploadLink_Click(modal) {
            $("#obsTr").css("display", "block");
            //            $("#divBack").css("opacity", "0.3");
            $("#divBack").css("display", "block");
            $("#txtId").val(modal.id);
            var idBtn = $("#txtId").val();
            var idTxt = idBtn.replace("btnObsProtCID", "txtIdProt")
            var texto = $('#' + idTxt + '').val();
            if (texto != "" || texto != null) {
                $("#txtObsTr").val(texto);
            }
        };

        function hideObs(modal) {
            var idBtn = $("#txtId").val();
            var idTxt = idBtn.replace("btnObsProtCID", "txtIdProt")
            var texto = $("#txtObsTr").val();
            $('#' + idTxt + '').val(texto);
            $("#obsTr").css("display", "none");
            //            $("#divBack").css("opacity", "1");
            $("#divBack").css("display", "none");
        }

        function startUpObs_Click(modal) {
            $("#divObsProSol").css("display", "block");
            //            $("#divBack").css("opacity", "0.3");
            $("#divBack").css("display", "block");
            $("#inputObsPro").val(modal.id);
            var idBtn = $("#inputObsPro").val();
            var idTxt = idBtn.replace("btnObsProfSol", "txtObsProfSol")
            var texto = $('#' + idTxt + '').val();
            if (texto != "" || texto != null) {
                $("#txtObsPro").val(texto);
            }
        }

        function hideObsProfSol(btn) {
            var idGrid = btn.id;
            var idGridItem = idGrid.replace('btnObsProfSol', 'idItemProf');
            var valIdItem = $('#' + idGridItem + '').val();
            var data = JSON.stringify({ idItemExame: valIdItem });
            $.ajax({
                type: "POST",
                url: "Cadastro.aspx/existeExameFisicoParecerMedico",
                data: data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    for (var i = 0; i < msg.d.length; i++) {
                        listItems += "<tr class='item'><td style=''><span class='text'>" + msg.d[i].nomeExameFisico + "</span></td><td style='display:none'><span class='value'>" + msg.d[i].idExameFisico + "</span></td></tr>";
                    }
                    tbody.append(listitem);
                }
            });
            $("#coColaboradorParecerMedico").val(btn.id);
            AbreModalProfissionalSolicitado();
        }


        function listarCIDs() {
            var cid = [];
            $('#tbCIDRepasse > tbody > tr.itemCID').each(function () {
                $this = $(this)
                var text = $this.find("span.textCID").html();
                cid.push(text);
                var value = $this.find("span.valueCID").html();
                cid.push(value);
            });
            return cid.toString();
        }


        function listarIdExame() {
            var exam = [];
            $('#myTable > tbody > tr.item').each(function () {
                $this = $(this)
                var text = $this.find("span.text").html();
                exam.push(text);
                var value = $this.find("span.value").html();
                exam.push(value);
            });
            return exam.toString();
        }

        function addProtocoloCID_Click(modal) {
            var idHid = modal.id;
            var idHidden = idHid.replace('btnObsProtCID', 'idListaCID');
            var idCID = parseInt($('#' + idHidden + '').val());
            var data = JSON.stringify({ idCID: idCID })
            $.ajax({
                type: "POST",
                url: "Cadastro.aspx/carregarProtocoloCID",
                data: data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var listItems = "<option value=''>Selecione</option>";
                    for (var i = 0; i < msg.d.length; i++) {
                        listItems += "<option value='" + msg.d[i].idProtCID + "' >" + msg.d[i].nomeProtCID + "</option>";
                    }
                    $("#selProtocoloCID").html(listItems);
                    AbreModalProtocoloCID();
                }
            })
        }

        $('#selProtocoloCID').change(function () {
            var protocoloCID = $('#selProtocoloCID').find(':selected').val();
            var idAtendimento = $('#ctl00_content_hidIdAtendimento').val();
            var obj = JSON.stringify({ idProtocoloCID: protocoloCID, idAtendimento: idAtendimento })
            var listItems = "";
            var div = $("#divItensCID");
            $("#divItensCID span").remove();
            $("#divItensCID br").remove();

            $.ajax({
                type: "POST",
                url: "Cadastro.aspx/existeItensProtocoloCID",
                data: obj,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d.length <= 0) {
                        itensProtocolo(protocoloCID);
                    }
                    else {
                        for (var i = 0; i < msg.d.length; i++) {
                            listItems += "<span><input style='margin-bottom: 5px;' onclick='desableOBSItemCID(this)' id='checkItem" + msg.d[i].idItem + "' name='" + msg.d[i].idItem + "' type='checkbox' checked='checked' value='" + msg.d[i].idItem + "'/>" + msg.d[i].nomeItem + "<input type='text' style='margin-bottom: 5px;float: right;width: 248px;' disabled id='" + msg.d[i].idItem + "' maxlength='200' value='" + msg.d[i].Observacao + "'></span><br>";
                        }
                        div.append(listItems);
                        AbreModalProtocoloCID();
                    }
                }
            })
        });

        function itensProtocolo(idProtocoloCID) {
            var data = JSON.stringify({ idProtocoloCID: idProtocoloCID })
            var listItems = "";
            var div = $("#divItensCID");
            $("#divItensCID span").remove();
            $("#divItensCID br").remove();

            $.ajax({
                type: "POST",
                url: "Cadastro.aspx/carregarItensProtocoloCID",
                data: data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    for (var i = 0; i < msg.d.length; i++) {
                        listItems += "<span><input style='margin-bottom: 5px;' onclick='desableOBSItemCID(this)' id='checkItem" + msg.d[i].idItem + "' name='" + msg.d[i].idItem + "' type='checkbox' checked='checked' value='" + msg.d[i].idItem + "'/>" + msg.d[i].nomeProtCID + "<input type='text' style='margin-bottom: 5px;float: right;width: 248px;' disabled id='" + msg.d[i].idItem + "' maxlength='200'></span><br>";
                    }
                    div.append(listItems);
                    AbreModalProtocoloCID();
                }
            });
        }

        function desableOBSItemCID(value) {
            var idObs = $('#' + value.id + '').val();
            if ($('#' + value.id + '').is(":checked") == false) {
                $('#' + idObs + '').removeAttr('disabled');
            } else {
                $('#' + idObs + '').val('');
                $('#' + idObs + '').attr('disabled', 'disabled');
            }
        }

        function bloquearCampos(bloqueia) {

            var $lnkAmbul = $("input[name*='lnkAmbul']");
            var $lnkExame = $("input[name*='lnkExame']");
            var $BtnAtestado = $("input[name*='BtnAtestado']");
            var $BtnObserv = $("input[name*='BtnObserv']");
            var $imgbPesqPacNome = $("input[name*='imgbPesqPacNome']");
            var $imgPesProfSolicitado = $("input[name*='imgPesProfSolicitado']");
                       
            var $divClassRisc = $("#divClassRisc");
            var $datepickerDtAtend = $("input[name*='txtDtAtend']").next();
            
            if (bloqueia) {
                $lnkAmbul.attr('disabled', 'disabled');
                $lnkExame.attr('disabled', 'disabled');
                $BtnAtestado.attr('disabled', 'disabled');
                $BtnObserv.attr('disabled', 'disabled');
                $imgbPesqPacNome.attr('disabled', 'disabled');
                $imgPesProfSolicitado.attr('disabled', 'disabled');

                $divClassRisc.show(false);
                $datepickerDtAtend.show(false);
                return;
            }

            $lnkAmbul.removeAttr('disabled');
            $lnkExame.removeAttr('disabled');
            $BtnAtestado.removeAttr('disabled');
            $BtnObserv.removeAttr('disabled');
            $imgPesProfSolicitado.removeAttr('disabled');
            $imgbPesqPacNome.removeAttr('disabled');
            $divClassRisc.show(true);
            $datepickerDtAtend.show(true);
         
        }

        function btnInserirProt_OnSelectedIndexChanged(btn) {
            var protocoloCID = $('#selProtocoloCID').find(':selected').val();
            var listaItensProtocoloCID = [];
            var idAtendimento = $('#ctl00_content_hidIdAtendimento').val();
            listaItensProtocoloCID.push(protocoloCID);
            var divs = document.querySelectorAll('#divItensCID > span > [id^=checkItem]');
            for (var i = 0; i < divs.length; i++) {
                var id = $('#' + divs[i].id + '').val();
                var obs = $('#' + id + '').val();
                listaItensProtocoloCID.push(id, obs);
            }
            var data = JSON.stringify({ listaItensProtocoloCID: listaItensProtocoloCID, idAtendimento: idAtendimento })
            $.ajax({
                type: "POST",
                url: "Cadastro.aspx/btnInserirProt_OnSelectedIndexChanged",
                data: data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    alert(msg.d);
                }
            })
        }

        $('#btnInserirProfSol').click(function () {
            var $this = this;
            var textos = [];
            var exames = [];
            var cids = [];
            var idAtendimento = $('#ctl00_content_hidIdAtendimento').val();
            debugger
            if (idAtendimento == null) {
                alert("Por favor salve o atendimento!");
            }
            else {
                var idProfissionalResponsavel = parseInt($('#ctl00_content_drpProfResp').val());

                //coCol
                var idBtn = $('#coColaboradorParecerMedico').val();
                var idColaborador = idBtn.replace('btnObsProfSol', 'hidIdProfSol');
                var coCol = parseInt($('#' + idColaborador + '').val());

                //id do item do atendimento na tabela tbs438
                var idItem438 = idBtn.replace('btnObsProfSol', 'idItemProf');
                var idItem = parseInt($('#' + idItem438 + '').val());

                //texttos
                var anamnese = $('#txtAnamRespModal').val();
                textos.push(anamnese);
                var acao = $('#txtAcaoRepasModal').val();
                textos.push(acao);
                var observacao = $('#txtObsPro').val();
                textos.push(observacao);
                //exames
                $('#myTable > tbody > tr.item').each(function () {
                    $this = $(this)
                    var value = $this.find("span.value").html();
                    exames.push(value);
                });

                //cids
                $('#tbCIDRepasse > tbody > tr.itemCID').each(function () {
                    $this = $(this)
                    var value = $this.find("span.valueCID").html();
                    cids.push(value);
                });

                var data = JSON.stringify({ textos: textos, exames: exames, cids: cids, idAtendimento: idAtendimento, coCol: coCol, idProfissionalResponsavel: idProfissionalResponsavel, idItem: idItem })
                $.ajax({
                    type: "POST",
                    url: "Cadastro.aspx/adicionarParecerMedicoSolicitado",
                    data: data,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        alert(msg.d);
                    }
                })
            }
        });

        function vlTotalProcedimentoIternar(txt) {
            var $this = $('#' + txt.id + '').val();
            var idThis = txt.id;
            var idTotal = idThis.replace('qtdProcedimento', 'vlTotalProcedimentoInternar');
            var idUnit = idThis.replace('qtdProcedimento', 'VlUnitarioProcedimentoInternar');
            var qtdProcedimento = parseInt($this);
            var vl = $('#' + idUnit + '').val().replace(',', '.');
            var vlUnit = parseFloat(vl);
            var vlTotal = (qtdProcedimento * vlUnit).toFixed(2);
            $('#' + idTotal + '').val(vlTotal.replace('.', ','));
        };

        function formatReal(int) {
            var tmp = int + '';
            var neg = false;
            if (tmp.indexOf("-") == 0) {
                neg = true;
                tmp = tmp.replace("-", "");
            }

            if (tmp.length == 1) tmp = "0" + tmp

            tmp = tmp.replace(/([0-9]{2})$/g, ",$1");
            if (tmp.length > 6)
                tmp = tmp.replace(/([0-9]{3}),([0-9]{2}$)/g, ".$1,$2");

            if (tmp.length > 9)
                tmp = tmp.replace(/([0-9]{3}).([0-9]{3}),([0-9]{2}$)/g, ".$1.$2,$3");

            if (tmp.length > 12)
                tmp = tmp.replace(/([0-9]{3}).([0-9]{3}).([0-9]{3}),([0-9]{2}$)/g, ".$1.$2.$3,$4");

            if (tmp.indexOf(".") == 0) tmp = tmp.replace(".", "");
            if (tmp.indexOf(",") == 0) tmp = tmp.replace(",", "0,");

            return tmp

        }

        function vlTotalOPM(txt) {
            var $this = $('#' + txt.id + '').val();
            var idThis = txt.id;
            var idTotal = idThis.replace('qtdOPM', 'qtdVlTotalOPM');
            var idUnit = idThis.replace('qtdOPM', 'VlUnitarioOPM');
            var qtdProcedimento = parseInt($this);
            var vl = $('#' + idUnit + '').val().replace(',', '.');
            var vlUnit = parseFloat(vl);
            var vlTotal = (qtdProcedimento * vlUnit).toFixed(2);
            $('#' + idTotal + '').val(vlTotal.replace('.', ','));
        };

        function fixedlength(txt, keyEvent, maxlength) {
            if (txt.value.length > maxlength) {
                txt.value = txt.value.substr(0, maxlength);
            }
            else if (txt.value.length < maxlength || txt.value.length == maxlength) {
                txt.value = txt.value.replace(/[^\d]+/g, '');
                return true;
            }
            else
                return false;
        }

        $('#chkIsGuiaExame').change(function () {
            if (this.checked == true) {
                $('#hidCheckEmitirGuiaExame').val('true');
            } else {
                $('#hidCheckEmitirGuiaExame').val('false');
            }
        });

        $('#chkIsExameExterno').change(function () {
            if (this.checked == true) {
                $('#hidCheckSolicitarExame').val('true');
            } else {
                $('#hidCheckSolicitarExame').val('false');
            }
        });
       
    </script>
</asp:Content>
