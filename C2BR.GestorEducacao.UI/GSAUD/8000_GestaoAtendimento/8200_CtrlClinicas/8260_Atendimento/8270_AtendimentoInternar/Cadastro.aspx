<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8270_AtendimentoInternar.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 980px;
        }
        label
        {
            margin-bottom: 1px;
        }
        input
        {
            height: 13px !important;
        }
        .ulDadosGerais li
        {
            margin-left: 5px;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
            width: 100% !important;
            horizontal-align: center;
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
        .divPaciPreAtend
        {
            border: 1px solid #CCCCCC;
            width: 978px;
            height: 135px;
            overflow-y: scroll;
            margin-left: 0px;
            margin-bottom: 5px;
            margin-top: -4px;
        }
        .divGeral
        {
            border-top: 1px solid #CCCCCC;
            width: 1120;
            height: 270px;
            padding-top: 6px;
            margin-top: 6px;
        }
        .divDadosPaciResp
        {
            border-right: 1px solid #CCCCCC;
            float: left;
            width: 600px;
            height: 264px;
            clear: both;
        }
        .DivResp
        {
            float: left;
            width: 600px;
            height: 207px;
        }
        .divEncamMedicoGeral
        {
            width: 370px;
            height: 50px;
            float: right;
        }
        .dvDadosInternacao
        {
            width: 370px;
            height: 300px;
            float: right;
        }
        .divEncamMedico
        {
            border: 1px solid #CCCCCC;
            width: 367px;
            height: 119px;
            float: right;
            overflow-y: scroll;
        }
        .ulIdentResp li
        {
            margin-left: 0px;
        }
        
        .ulDadosContatosResp li
        {
            margin-left: 0px;
        }
        
        .lblsub
        {
            color: #436EEE;
            font-size: 11px;
        }
        .lblTop
        {
            font-size: 9px;
            margin-bottom: 6px;
            color: #436EEE;
        }
        .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            padding: 2px 3px 1px 3px;
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
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: left;
        }
        .chk label
        {
            display: inline;
        }
        .lblSubInfos
        {
            color: Orange;
            font-size: 8px;
        }
        .ulInfosGerais
        {
            margin-top: -3px;
        }
        .ulInfosGerais li
        {
            margin: 1px 0 3px 0px;
        }
        .ulEndResiResp
        {
        }
        .ulEndResiResp li
        {
            margin-left: 2px;
        }
        .divClassPri
        {
        }
        .divClassRed
        {
            background-color: Red;
        }
        .divClassOrange
        {
            background-color: Orange;
        }
        .divClassYellow
        {
            background-color: Yellow;
        }
        .divClassGreen
        {
            background-color: Green;
        }
        .divClassBlue
        {
            background-color: Blue;
        }
        .lisobe
        {
            margin-top: -9px !important;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <%-- <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick">
        </asp:Timer>--%>
        <li class="liTituloGrid" style="width: 100%; height: 20px !important; margin-right: 0px;
            margin-top: -5px; background-color: #E0EEE0; text-align: center; font-weight: bold;
            margin-bottom: 5px">
            <ul>
                <li style="margin-left: 313px;">
                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                        GRADE DE PACIENTES COM INTERNAÇÃO REQUERIDA</label>
                </li>
                <li style="float: right; margin-top: 2px;">
                    <ul>
                        <%--<li style="margin: 0 20px;">
                            <asp:TextBox runat="server" placeholder="Pesquisar pelo paciente" Width="150px" class="" ID="txtNomePacPesq" ToolTip="Informe a nome do paciente"></asp:TextBox>
                        </li>--%>
                        <li>
                            <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
                            <asp:Label runat="server" ID="Label2"> &nbsp à &nbsp </asp:Label>
                            <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator><br />
                        </li>
                        <li style="margin-top: 0px; margin-left: 5px;">
                            <asp:ImageButton ID="imgPesqGrid" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                OnClick="imgPesqGrid_OnClick" />
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <%--<Triggers>
                    <asp:AsyncPostBackTrigger EventName="Tick" ControlID="Timer1" />
                </Triggers>--%>
                <ContentTemplate>
                    <div class="divPaciPreAtend">
                        <asp:GridView ID="grdPacientesInternacao" CssClass="grdBusca" runat="server" Style="width: 100%;
                            height: 15px;" AllowPaging="false" GridLines="Vertical" AutoGenerateColumns="false"
                            ToolTip="Grid de requerimento de internação em aberto (Clique no checkbox ou em qualquer local da linha para selecionar)"
                            AutoGenerateSelectButton="false">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum requerimento de internação em aberto<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="CK">
                                    <ItemStyle Width="15px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hidIdAtendInter" runat="server" Value='<%# Eval("ID_ATEND_INTER") %>' />
                                        <asp:HiddenField ID="hidIdAtendAgend" runat="server" Value='<%# Eval("ID_ATEND_AGEND") %>' />
                                        <asp:CheckBox ID="chkselect" runat="server" AutoPostBack="true" OnCheckedChanged="chkselect_OnCheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NOME_PACIENTE" HeaderText="PACIENTE" />
                                <asp:BoundField DataField="SEXO" HeaderText="SEXO" />
                                <asp:BoundField DataField="NOME_RESP" HeaderText="RESPONSÁVEL" />
                                <asp:BoundField DataField="NOME_COL_SOL" HeaderText="MÉDICO" />
                                <asp:BoundField DataField="ESPE_COL_SOL" HeaderText="ESPECIALIDADE" />
                                <asp:BoundField DataField="TELE_COL_SOL" HeaderText="TEL. MÉDICO">
                                    <ItemStyle CssClass="campoTelefone" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DT_PROVAVEL_INTER" HeaderText="DATA PREVISTA" />
                                <asp:BoundField DataField="DT_SOLIC_INTER" HeaderText="DATA SOLICITAÇÃO" />
                                <asp:BoundField DataField="CO_PRIOR_RISCO" HeaderText="PRIORIDADE" />
                                <asp:BoundField DataField="NU_REGIS_INTER" HeaderText="Nº REGISTRO" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </li>
        <div class="divGeral">
            <div class="divDadosPaciResp">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <ul>
                            <li class="liTituloGrid" style="width: 96.8%; height: 20px !important; margin-right: 0px;
                                background-color: #E0EEE0; text-align: center; font-weight: bold; margin-bottom: 5px;
                                padding-left: 10px;">
                                <ul style="float: left;">
                                    <li>
                                        <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                                            DADOS RESPONSÁVEL E PACIENTE</label>
                                    </li>
                                </ul>
                            </li>
                            <div class="DivResp">
                                <ul class="ulDadosResp">
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
                                        <asp:ImageButton ID="imgCpfResp" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
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
                                    <li style="margin: -46px 0 0 178px;">
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
                                                <asp:TextBox runat="server" ID="txtNuWhatsResp" Width="78px" CssClass="campoTel"></asp:TextBox>
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
                                                <asp:TextBox runat="server" ID="txtCEP" Width="55px" CssClass="campoCepF"></asp:TextBox>
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
                                            <li style="margin-left: 10px; margin-top: -8px;">
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
                                    <li style="margin: -4px 0 0 -28px;">
                                        <ul class="ulDadosPaciente">
                                            <li style="margin-bottom: -6px;">
                                                <label class="lblTop">
                                                    DADOS DO PACIENTE - USUÁRIO DE SAÚDE</label>
                                            </li>
                                            <li style="margin: 9px 3px 0 0px; clear: both"><a class="lnkPesNIRE" href="#">
                                                <img class="imgPesPac" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade"
                                                    style="width: 17px; height: 17px;" /></a> </li>
                                            <li>
                                                <label>
                                                    Nº NIS</label>
                                                <asp:CheckBox runat="server" ID="chkPesqNuNisPac" Style="margin-left: -5px" OnCheckedChanged="chkPesqNuNisPac_OnCheckedChanged"
                                                    AutoPostBack="true" />
                                                <asp:TextBox runat="server" ID="txtNuNisPaci" Width="70" Style="margin-left: -6px"></asp:TextBox>
                                            </li>
                                            <li>
                                                <label>
                                                    CPF</label>
                                                <asp:CheckBox runat="server" ID="chkPesqCPFUsu" />
                                                <asp:TextBox runat="server" ID="txtCpfPaci" CssClass="campoCpf" Width="75px" Style="margin-left: -6px"
                                                    OnTextChanged="txtCpfPaci_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hidCoPac" />
                                            </li>
                                            <li style="margin-left: -1px; margin-top: 12px;">
                                                <asp:ImageButton ID="imbPesqPaci" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                    Width="13px" Height="13px" OnClick="imbPesqPaci_OnClick" />
                                            </li>
                                            <li>
                                                <label class="lblObrigatorio">
                                                    Nome</label>
                                                <asp:TextBox runat="server" ID="txtnompac" ToolTip="Nome do Paciente" Width="220px"></asp:TextBox>
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
                                            <li style="margin-left: 0px;" class="lisobe">
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
                                                <asp:TextBox runat="server" ID="txtWhatsPaci" Width="78px" CssClass="campoTel"></asp:TextBox>
                                            </li>
                                            <li style="clear: both; margin-top: -7px; float: right">
                                                <asp:Label runat="server" ID="lblEmailPaci">Email</asp:Label>
                                                <asp:TextBox runat="server" ID="txtEmailPaci" Width="220px"></asp:TextBox>
                                            </li>
                                        </ul>
                                    </li>
                                </ul>
                            </div>
                        </ul>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="divEncamMedicoGeral">
                <ul>
                    <asp:UpdatePanel runat="server" ID="updInfoPlaSaude" UpdateMode="Conditional">
                        <ContentTemplate>
                            <li style="margin: -2px 0 -2px 0">
                                <label class="lblTop" style="margin-bottom: 1px; margin-top: 1px;">
                                    INFORMAÇÕES DO PLANO DE SAÚDE</label>
                            </li>
                            <li style="clear: both;">
                                <label>
                                    Operadora</label>
                                <asp:DropDownList runat="server" ID="ddlOperPlano" OnSelectedIndexChanged="ddlOperPlano_OnSelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </li>
                            <li>
                                <label>
                                    Plano</label>
                                <asp:DropDownList runat="server" ID="ddlPlano">
                                </asp:DropDownList>
                            </li>
                            <li>
                                <label>
                                    Categoria</label>
                                <asp:DropDownList runat="server" ID="ddlCateOper" Width="80px">
                                </asp:DropDownList>
                            </li>
                            <li>
                                <label>
                                    Venc</label>
                                <asp:TextBox Width="30px" runat="server" ID="txtDtVenciPlan" CssClass="campoVenc"></asp:TextBox>
                            </li>
                            <li>
                                <label>
                                    Número</label>
                                <asp:TextBox runat="server" ID="txtNumeroCartPla" Width="71px"></asp:TextBox>
                            </li>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ul>
            </div>
            <div class="dvDadosInternacao">
                <ul>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                        <ContentTemplate>
                            <li style="margin: -2px 0 -2px 0">
                                <label class="lblTop" style="margin-bottom: 1px; margin-top: 1px;">
                                    DADOS DA INTERNAÇÃO</label>
                            </li>
                            <li style="margin-top: 5px;">
                                <ul>
                                    <li style="clear: both; margin-right: 20px;">
                                        <label title="Registro de encaminhamento da internação">
                                            Nº REG. ENCAMIN.</label>
                                        <asp:Label runat="server" ID="lblNRegistro" ToolTip="Registro de encaminhamento para a internação"
                                            Font-Bold="true" Style="color: Red;"></asp:Label>
                                    </li>
                                    <li style="margin-right: 20px;">
                                        <asp:Label runat="server" ID="lblClassRiscoInternar" CssClass="colorTextRed">PRIORIDADE</asp:Label><br />
                                        <asp:DropDownList runat="server" ID="drpClassRiscoInternar" ClientIDMode="Static"
                                            Width="93px">
                                            <asp:ListItem Value="">Selecione</asp:ListItem>
                                            <asp:ListItem Value="X">Nenhuma</asp:ListItem>
                                            <asp:ListItem Value="A">Alta</asp:ListItem>
                                            <asp:ListItem Value="M">Média</asp:ListItem>
                                            <asp:ListItem Value="N">Normal</asp:ListItem>
                                            <asp:ListItem Value="B">Baixa</asp:ListItem>
                                            <asp:ListItem Value="U">Urgente</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="" id="liRegInter" runat="server" visible="false">
                                        <label title="Registro de encaminhamento da internação">
                                            Nº REG. INTER.</label>
                                        <asp:Label runat="server" ID="lblRegInter" ToolTip="Registro da internação" Font-Bold="true"
                                            Style="color: #004276;"></asp:Label>
                                    </li>
                                    <li style="margin: 6px 0 0 0; clear: both">
                                        <label class="lblSubInfos" style="margin-bottom: 1px; margin-top: 1px;">
                                            INFORMAÇÕES GERAIS</label>
                                    </li>
                                    <li style="clear: both">
                                        <label id="lblCareterInternar" class="lblObrigatorio">
                                            Caráter da Internação</label>
                                        <asp:DropDownList runat="server" Width="107px" ID="ddlCaraterInternar">
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <label id="Label3" class="lblObrigatorio">
                                            Tipo da Internação</label>
                                        <asp:DropDownList runat="server" Width="107px" ID="ddlTipoInternar">
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <label id="Label4" class="lblObrigatorio">
                                            Regime da Internação</label>
                                        <asp:DropDownList runat="server" Width="103px" ID="ddlRegimeInternar">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin: 0">
                                        <asp:Label runat="server" ID="lblDS" ToolTip="Quantidade de diárias solicitadas"
                                            class="lblObrigatorio">DS</asp:Label><br />
                                        <asp:TextBox runat="server" ToolTip="Quantidade de diárias solicitadas" Width="30px"
                                            ID="txtDS" onkeypress="return fixedlength(this, event, 3);" onkeyup="return fixedlength(this, event, 3);"
                                            MaxLength="3"></asp:TextBox>
                                    </li>
                                    <li style="clear: both; margin: -6px 0 0 0;">
                                        <ul>
                                            <li>
                                                <ul>
                                                    <li style="">
                                                        <asp:Label runat="server" ID="lblIndicacaoClinica">Indicação Clínica</asp:Label><br />
                                                        <asp:TextBox TextMode="MultiLine" Width="253px" Rows="4" MaxLength="100" runat="server"
                                                            ID="txtIndicacaoClinica" onkeydown="checkTextAreaMaxLength(this,event,'100');"></asp:TextBox>
                                                    </li>
                                                </ul>
                                            </li>
                                            <li style="width: 100px; margin: 0">
                                                <ul>
                                                    <li style="">
                                                        <label id="Label5">
                                                            Tipo de Acomodação</label>
                                                        <asp:DropDownList runat="server" Width="100px" ID="ddlTipoAcomodacao">
                                                        </asp:DropDownList>
                                                    </li>
                                                    <li style="clear: both; float: right; margin: 0" class="lblObrigatorio">
                                                        <asp:Label runat="server" ID="lblDataProvavelAH" ToolTip="Data da admissão hospitalar">Data AH</asp:Label><br />
                                                        <asp:TextBox runat="server" Width="57px" CssClass="campoData" ID="txtDataProvavelAH"
                                                            ToolTip="Informe a data da internação"></asp:TextBox>
                                                    </li>
                                                </ul>
                                            </li>
                                        </ul>
                                    </li>
                                    <li style="clear: both">
                                        <asp:Label runat="server" ID="lblTipoDoenca" ToolTip="Tipo de doença referida pelo paciente"
                                            class="lblObrigatorio">Tipo de Doença</asp:Label><br />
                                        <asp:DropDownList runat="server" Width="107px" ID="drpTipoDoenca">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="">
                                        <asp:Label runat="server" ID="lblTDRP" ToolTip="Tempo de doença referida pelo paciente"
                                            class="lblObrigatorio">TDRP</asp:Label><br />
                                        <asp:TextBox runat="server" Width="29px" ID="txtTDRP" ToolTip="Quantidade de tempo em dias, meses ou ano"
                                            onkeypress="return fixedlength(this, event, 3);" onkeyup="return fixedlength(this, event, 3);"
                                            MaxLength="3"></asp:TextBox>
                                        <asp:DropDownList runat="server" Width="107px" ID="drpTDRP" ToolTip="Dias, meses ou ano (referente ao número indicado na TDRP)">
                                            <asp:ListItem Value="">Selecione</asp:ListItem>
                                            <asp:ListItem Value="A">Anos</asp:ListItem>
                                            <asp:ListItem Value="M">Meses</asp:ListItem>
                                            <asp:ListItem Value="D">Dias</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin: 0">
                                        <asp:Label runat="server" ID="lblIndicacaoAcidente" class="lblObrigatorio">Indicação de Acidente</asp:Label><br />
                                        <asp:DropDownList runat="server" Width="107px" ID="drpIndicacaoAcidente">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="clear: both; margin: 6px 0 0 0">
                                        <ul>
                                            <li style="margin: 0">
                                                <asp:Button ID="btnCID" Style="background-color: #c1ffc1; margin-left: 0px; width: 120px;
                                                    height: 15px !important; cursor: pointer;" Font-Bold="true" Font-Size="8px" runat="server"
                                                    Text="CID" ToolTip="CID Internação." OnClientClick="AbreModalCID();" />
                                                <asp:Button ID="btnProcedimento" Style="background-color: #c1ffc1; margin-left: 0px;
                                                    width: 119px; height: 15px !important; margin-bottom: 5px; cursor: pointer;"
                                                    Font-Bold="true" Font-Size="8px" runat="server" Text="PROCEDIMENTO" ToolTip="Procedimentos solicitados"
                                                    OnClientClick="AbreModalProcedimento();" />
                                                <asp:Button ID="btnOPM" Style="background-color: #c1ffc1; margin-left: 0px; width: 120px;
                                                    height: 15px !important; margin: 0; cursor: pointer;" Font-Bold="true" Font-Size="8px"
                                                    runat="server" Text="OPM" ToolTip="Órteses, próteses e materiais especiais."
                                                    OnClientClick="AbreModalOPM();" />
                                            </li>
                                        </ul>
                                    </li>
                                </ul>
                            </li>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ul>
            </div>
        </div>
        <div id="divLoadShowResponsaveis" style="display: none; height: 315px !important;">
        </div>
        <div id="divLoadShowAlunos" style="display: none; height: 305px !important;">
        </div>
        <div id="dvCID" style="display: none; padding-top: 16px;">
            <ul>
                <li style="">
                    <ul>
                        <li>
                            <label style="color: Orange; font-size: 9px; width: 62px;">
                                DEFINIR A CID</label>
                        </li>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional">
                            <ContentTemplate>
                                <li style="margin-bottom: 5px">
                                    <asp:TextBox runat="server" ID="txtDefCid" Width="246px" Style="margin: 0;"></asp:TextBox>
                                    <asp:ImageButton ID="imgbPesqPacNome" CssClass="btnProcurar" ValidationGroup="pesqPac"
                                        runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" OnClick="imgbPesqCID_OnClick" />
                                    <asp:DropDownList runat="server" ID="drpDefCid" Visible="false" AutoPostBack="true"
                                        Width="246px" OnSelectedIndexChanged="drpDefCid_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="imgbVoltarPesq" ValidationGroup="pesqPac" CssClass="btnProcurar"
                                        Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico" Visible="false"
                                        runat="server" OnClick="imgbVoltarPesq_OnClick" />
                                </li>
                                <li style="width: 100%; display: flex;">
                                    <div id="div13" style="width: 263px; height: 211px; border: 1px solid #CCC; overflow-y: scroll">
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
                                                        <asp:Label runat="server" ID="lblCIDPrincipal" ToolTip="Escolha a(s) CID(s) principal(is)">CP</asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="10px" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hidCIDPrincipal" Value='<%# Eval("isCIDPrincipal") %>' />
                                                        <asp:CheckBox runat="server" ID="chcCIDPrincipal" ToolTip="CID principal" Checked='<%# Eval("isCIDPrincipal") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CID">
                                                    <ItemStyle HorizontalAlign="Left" Width="170px" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hidIdCIDInternar" Value='<%# Eval("idCID") %>' />
                                                        <asp:HiddenField runat="server" ID="hidIdInterCID" Value='<%# Eval("idInterCID") %>' />
                                                        <asp:Label runat="server" ID="lblCIDInternar" Text='<%# Eval("nomeCID") %>' ToolTip='<%# Eval("descCID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JE">
                                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hidCIDExisteProtocolo" Value='<%# Eval("existeProtocolo") %>' />
                                                        <asp:ImageButton runat="server" Width="15px" ID="imgProtCIDInternar" ImageUrl="/Library/IMG/PGS_CentralRegulacao_Icone_Obs_15x15.png"
                                                            ToolTip="Definir o(s) protocolo(s) da CID utilizado neste atendimento" OnClick="btnObsProtCIDInternar_OnClick"
                                                            Visible='<%# Eval("existeProtocolo") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EX">
                                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" Width="15px" ID="btnDelCIDInternar" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                            ToolTip="Excluir CID desta internação" OnClick="btnDelCIDInternar_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div style="margin: 0 0 0 23px;" runat="server" id="dvProtocolosCID">
                                        <asp:Label runat="server" ID="lblTitProtocolo" Style="color: Orange; font-size: 10px;
                                            width: 62px;">
                                        </asp:Label>
                                        <ul>
                                            <li>
                                                <asp:Repeater runat="server" ID="repProtocolosCID" OnItemDataBound="repProtocolosCID_OnItemDataBound">
                                                    <ItemTemplate>
                                                        <table style="border: none" runat="server" id="tbProtocoloCID">
                                                            <tr style="border: none">
                                                                <td style="">
                                                                    <asp:HiddenField runat="server" ID="hidIdProtocoloCID" Value='<%# Eval("idProtocolo") %>' />
                                                                    <input id="imgUsarProtocoloCID" style="height: 8px !important; width: 8px; background: url('/Library/IMG/Gestor_Adicionar_8px_8px.png');
                                                                        border: none; margin: 0; cursor: pointer" runat="server" title="Listar itens deste protocolo"
                                                                        onclick="imgUsarProtocoloCID_OnClick(this);" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtName" Font-Size="8px" Style="margin: 0" Width="235px" runat="server"
                                                                        BorderStyle="None" Text='<%# Eval("nomeProtocolo") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:Repeater runat="server" ID="repItenProtocoloCID">
                                                            <ItemTemplate>
                                                                <table style="border: none; display: none" runat="server" id="tbItensProtocoloCID">
                                                                    <tr style="width: 11px; padding: 2px 0 0px 12px;" id="trItensProtocoloCID">
                                                                        <td style="padding-left: 10px;">
                                                                            <asp:HiddenField runat="server" ID="hidRegisInterItemCID" Value='<%# Eval("regisInterItemCID") %>' />
                                                                            <asp:HiddenField runat="server" ID="hidIdItemProtocoloCID" Value='<%# Eval("idItenProtocolo") %>' />
                                                                            <asp:CheckBox runat="server" ToolTip="Caso desmarcado, o procedimento não será feito nesta internação."
                                                                                ID="chkIdItenProtocoloCID" Width="10px" Checked='<%# Eval("flAplicado") %>' onchange="chcItenCID(this)" />
                                                                        </td>
                                                                        <td style="">
                                                                            <asp:Label runat="server" ID="lblItenProtocoloCID" Width="150px" Style="padding-left: 7px;"
                                                                                Text='<%# Eval("nomeItemProtocolo") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox runat="server" Enabled='<%# Eval("flEnabled") %>' Style="margin: 0;
                                                                                width: 312px;" ToolTip="Explicação relativa ao item que não será utilizado nesta internação."
                                                                                ID="txtItensProtocoloCID" Value='<%# Eval("obsItemCID") %>'> </asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </li>
                                        </ul>
                                    </div>
                                </li>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <li style="margin-top: 15px; float: right;">
                            <ul>
                                <li>
                                    <asp:Button runat="server" ID="btnSalvarCID" Text="Salvar" Font-Bold="true" Style="height: 22px !important;
                                        background-color: #0b3e6f; color: #fff; cursor: pointer; width: 56px" OnClick="btnSalvarCID_OnClick" />
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
        <div id="dvProcedimento" style="padding: 23px 10px 10px 10px; display: none">
            <ul>
                <li>
                    <ul>
                        <li style="clear: both">
                            <ul>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <li style="clear: both; margin-bottom: 8px">
                                            <asp:DropDownList runat="server" Width="300px" ID="ddlTipoProcedimentoInternar" ClientIDMode="Static"
                                                AutoPostBack="true" ToolTip="Procedimento que será utilizado no processo de internação"
                                                OnSelectedIndexChanged="ddlTipoProcedimentoInternar_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </li>
                                        <li style="clear: both;">
                                            <div id="div5" style="width: 662px; height: 130px; border: 1px solid #CCC; overflow-y: scroll">
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
                                                        <asp:BoundField DataField="idProcedimentoOPM" HeaderText="Item" HtmlEncode="false" />
                                                        <asp:BoundField DataField="tipoProcedimentoOPM" HeaderText="Tipo" HtmlEncode="false" />
                                                        <asp:BoundField DataField="nomeProcedimentoOPM" HeaderText="Procedimento" HtmlEncode="false" />
                                                        <asp:BoundField DataField="codigoProcedimentoOPM" HeaderText="Código" HtmlEncode="false" />
                                                        <asp:TemplateField HeaderText="Qtde">
                                                            <ItemStyle Width="30px" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="hidIdInterProcedimento" Value='<%# Eval("idInterProcOPM") %>' />
                                                                <asp:TextBox Width="20px" Style="margin: 0;" runat="server" ID="qtdProcedimento"
                                                                    MaxLength="3" onblur="vlTotalProcedimentoIternar(this);" onkeypress="return fixedlength(this, event, 3);"
                                                                    onkeyup="return fixedlength(this, event, 3);" Text='<%# Eval("qtdProcedimentoOPM") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="R$ Unitário">
                                                            <ItemTemplate>
                                                                <asp:TextBox Width="46px" Enabled="false" Style="margin: 0; border: none" runat="server"
                                                                    ID="vlUnitarioProcedimentoInternar" Text='<%# Eval("vlUnitarioProcedimentoOPM") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="R$ Total">
                                                            <ItemTemplate>
                                                                <asp:TextBox Width="46px" Enabled="false" Style="margin: 0; border: none" runat="server"
                                                                    ID="vlTotalProcedimentoInternar" Text='<%# Eval("vlTotalProcedimentoOPM") %>'></asp:TextBox>
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <li style="margin-top: 15px; float: right;">
                                    <ul>
                                        <li>
                                            <asp:Button runat="server" ID="btnSalvarProcedimento" Text="Salvar" Font-Bold="true"
                                                Style="height: 22px !important; background-color: #0b3e6f; color: #fff; cursor: pointer;
                                                width: 56px" OnClick="btnSalvarProcedimento_OnClick" />
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
        </div>
        <div id="dvOPM" style="padding: 23px 10px 10px 10px; display: none">
            <ul>
                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                    <ContentTemplate>
                        <li style="clear: both; margin-bottom: 8px">
                            <asp:DropDownList runat="server" Width="300px" ID="ddlOPMInternar" ClientIDMode="Static"
                                AutoPostBack="true" ToolTip="Órteses, Próteses e/ou Materiais especiais utilizados no processo de internação"
                                OnSelectedIndexChanged="ddlOPMInternar_OnSelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li style="clear: both;">
                            <div id="div6" style="width: 662px; height: 130px; border: 1px solid #CCC; overflow-y: scroll">
                                <input type="hidden" id="Hidden7" name="" />
                                <asp:GridView ID="grdOPM" CssClass="grdBusca grdExamFis" runat="server" Style="width: 100%;
                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                    ShowHeaderWhenEmpty="false" AllowUserToAddRows="false">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum item referente a OPM foi adicionado<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="idProcedimentoOPM" HeaderText="Item" HtmlEncode="false" />
                                        <asp:BoundField DataField="tipoProcedimentoOPM" HeaderText="Tipo" HtmlEncode="false" />
                                        <asp:BoundField DataField="nomeProcedimentoOPM" HeaderText="Procedimento" HtmlEncode="false" />
                                        <asp:BoundField DataField="codigoProcedimentoOPM" HeaderText="Código" HtmlEncode="false" />
                                        <asp:TemplateField HeaderText="Qtde">
                                            <ItemStyle Width="30px" />
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hidIdInterOPM" Value='<%# Eval("idInterProcOPM") %>' />
                                                <asp:TextBox Width="20px" Style="margin: 0;" runat="server" ID="qtdOPM" Text='<%# Eval("qtdProcedimentoOPM")%>'
                                                    onblur="vlTotalOPM(this);" onkeypress="return fixedlength(this, event, 3);" onkeyup="return fixedlength(this, event, 3);"
                                                    MaxLength="3"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fabricante">
                                            <ItemTemplate>
                                                <asp:TextBox Width="100px" Style="margin: 0;" runat="server" ID="fabricanteOPM" Text='<%# Eval("fabricanteOPM")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R$ Unitário">
                                            <ItemTemplate>
                                                <asp:TextBox Width="46px" Enabled="false" Style="margin: 0; border: none" runat="server"
                                                    ID="VlUnitarioOPM" Text='<%# Eval("vlUnitarioProcedimentoOPM")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R$ Total">
                                            <ItemTemplate>
                                                <asp:TextBox Width="46px" Enabled="false" Style="margin: 0; border: none" runat="server"
                                                    ID="qtdVlTotalOPM" Text='<%# Eval("vlTotalProcedimentoOPM")%>'></asp:TextBox>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
                <li style="margin-top: 15px; float: right;">
                    <ul>
                        <li>
                            <asp:Button runat="server" ID="btnSalvarOPM" Text="Salvar" Font-Bold="true" Style="height: 22px !important;
                                background-color: #0b3e6f; color: #fff; cursor: pointer; width: 56px" OnClick="btnSalvarOPM_OnClick" />
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".campoCpf").mask("999.999.999-99");
            $(".campoCepF").mask("99999-999");
            $('#txtNuNisPaci').mask('9999999999999999');
            if ($('.campoTel').val().length <= 10) {
                $('.campoTel').mask("(99)9?999-99999");
            } else {
                $('.campoTel').mask("(99)9?9999-9999");
            }
            $(".campoVenc").mask("99/99");

            $(".lnkPesResp").click(function () {
                $('#divLoadShowResponsaveis').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE RESPONSÁVEIS",
                    open: function () { $('#divLoadShowResponsaveis').load("/Componentes/ListarResponsaveis.aspx"); }
                });
            });

            $(".lnkPesNIRE").click(function () {
                $('#divLoadShowAlunos').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE PACIENTES",
                    open: function () { $('#divLoadShowAlunos').load("/Componentes/ListarPacientes.aspx"); }
                });
            });

        });

        //fixa a quantidade de inetiros e limita o textbox apenas a inteiros também
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

        //Determina a quantidade de caracteres do textbox multiline
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

        function carregaPadroes() {
            $(".campoCpf").mask("999.999.999-99");
            $(".campoCepF").mask("99999-999");
            $(".campoTel").mask("(99)9999-9999");
            $(".campoVenc").mask("99/99");
            $(".campoData").datepicker();
            $(".campoData").mask("99/99/9999");
        }

        //Função que é chamada quando se abre a página e depois dos postbacks
        function carregaCss() {

            $(".lnkPesResp").click(function () {
                $('#divLoadShowResponsaveis').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE RESPONSÁVEIS",
                    open: function () { $('#divLoadShowResponsaveis').load("/Componentes/ListarResponsaveis.aspx"); }
                });
            });

            $(".lnkPesNIRE").click(function () {
                $('#divLoadShowAlunos').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE PACIENTES",
                    open: function () { $('#divLoadShowAlunos').load("/Componentes/ListarPacientes.aspx"); }
                });
            });

            //Executado ao trocar a seleção no DropDownList para mudar a cor para a correspondente
            $("#ddlClassRisco").change(function (evento) {
                var e = document.getElementById("ddlClassRisco");
                var itSelec = e.options[e.selectedIndex].value;
                if (itSelec == "") {

                    $("#divClassRiscCorSelec").hide()
                    $("#divClassRiscCorSelec").css("background-color", "White");
                    $("#divClassRiscCorSelec").show(100);
                }
                else if (itSelec == 1) {
                    $("#divClassRiscCorSelec").hide()
                    $("#divClassRiscCorSelec").css("background-color", "Red");
                    $("#divClassRiscCorSelec").show(100);
                }
                else if (itSelec == 2) {
                    $("#divClassRiscCorSelec").hide();
                    $("#divClassRiscCorSelec").css("background-color", "Orange");
                    $("#divClassRiscCorSelec").show(100);
                }
                else if (itSelec == 3) {
                    $("#divClassRiscCorSelec").hide();
                    $("#divClassRiscCorSelec").css("background-color", "Yellow");
                    $("#divClassRiscCorSelec").show(100);
                }
                else if (itSelec == 4) {
                    $("#divClassRiscCorSelec").hide();
                    $("#divClassRiscCorSelec").css("background-color", "Green");
                    $("#divClassRiscCorSelec").show(100);
                }
                else if (itSelec == 5) {
                    $("#divClassRiscCorSelec").hide();
                    $("#divClassRiscCorSelec").css("background-color", "Blue");
                    $("#divClassRiscCorSelec").show(100);
                }
            });
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            carregaCss();
        });

        //---------------------------------functions para abrir modais----------------------------------------------------

        function AbreModalHipDiagnosticas() {
            $('#dvHipotesesDiagnosticas').dialog({
                autoopen: false, modal: true, width: 200, height: 200, resizable: false, title: "HIPÓTESES DIAGNÓSTICAS",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalCID() {
            $('#dvCID').dialog({
                autoopen: false, modal: true, width: 821, height: 338, resizable: false, title: "CID e PROTOCOLOS",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalProcedimento() {
            $('#dvProcedimento').dialog({
                autoopen: false, modal: true, width: 684, height: 260, resizable: false, title: "PROCEDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalOPM() {
            $('#dvOPM').dialog({
                autoopen: false, modal: true, width: 684, height: 260, resizable: false, title: "ÓRTESE, PRÓTESES E MATERIAIS ESPECIAIS",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }


        //-----------------------------------------------------------------------------------------------------------------

        function imgUsarProtocoloCID_OnClick(btn) {
            var idBtn = btn.id;
            var idItens = idBtn.replace('imgUsarProtocoloCID', 'repItenProtocoloCID');
            if ($("[id*=" + idItens + "]").is(":visible")) {
                $("[id*=" + idItens + "]").css('display', 'none');
            } else {
                $("[id*=" + idItens + "]").css('display', 'block');
            }
        }

        function chcItenCID(chk) {
            var idChk = chk.firstChild.id;
            var idTxt = idChk.replace('chkIdItenProtocoloCID', 'txtItensProtocoloCID');
            if ($('#' + idChk + '').is(':checked')) {
                $('#' + idTxt + '').attr('disabled', 'disabled');
                $('#' + idTxt + '').val('');
                $('#' + idTxt + '').attr("disabled", true);
            } else {
                $('#' + idTxt + '').removeAttr();
                $('#' + idTxt + '').attr("disabled", false);
            }
        }

        function vlTotalProcedimentoIternar(txt) {
            var $this = $('#' + txt.id + '').val();
            var idThis = txt.id;
            var idTotal = idThis.replace('qtdProcedimento', 'vlTotalProcedimentoInternar');
            var idUnit = idThis.replace('qtdProcedimento', 'vlUnitarioProcedimentoInternar');
            var qtdProcedimento = parseInt($this);
            var vl = $('#' + idUnit + '').val().replace(',', '.');
            var vlUnit = parseFloat(vl);
            var vlTotal = (qtdProcedimento * vlUnit).toFixed(2);
            $('#' + idTotal + '').val(vlTotal.replace('.', ','));
        };

        function vlTotalOPM(txt) {
            debugger
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

        function closeModalCID() {
            $("#dvCID").dialog('close');
            alert("Não foi possível concluir a operação, por favor verifique se um encaminhamento de internação foi selecionado.");
        }

        function closeModalCIDSucesso() {
            alert("Registro efetuado com sucesso.");
            $("#dvCID").dialog('close');
        }

        function closeModalProcedimentoSucesso() {
            $("#dvProcedimento").dialog('close');
            alert("Registro efetuado com sucesso.");
        }

        function closeModalProcedimento() {
            $("#dvProcedimento").dialog('close');
            alert("Não foi possível concluir a operação, por favor verifique se um encaminhamento de internação foi selecionado.");
        }

        function closeModalOPMSucesso() {
            $("#dvOPM").dialog('close');
            alert("Registro efetuado com sucesso.");
        }

        function closeModalOPM() {
            $("#dvOPM").dialog('close');
            alert("Não foi possível concluir a operação, por favor verifique se um encaminhamento de internação foi selecionado.");
        }

    </script>
</asp:Content>
