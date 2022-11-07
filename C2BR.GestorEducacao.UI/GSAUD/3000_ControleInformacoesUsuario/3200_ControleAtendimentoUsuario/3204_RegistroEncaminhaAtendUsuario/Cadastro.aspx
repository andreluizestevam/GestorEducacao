<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3204_RegistroEncaminhaAtendUsuario.Cadastro" %>

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
            margin-top: -10px;
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
            height: 232px;
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
            height: 220px;
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
        <%--<asp:HiddenField runat="server" ID="hdPreAtend" />
        <asp:HiddenField runat="server" ID="hdCoColPlantonista" />
        <asp:HiddenField runat="server" ID="hdCoEmpPlantonista" />--%>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick">
        </asp:Timer>
        <li class="liTituloGrid" style="width: 100%; height: 20px !important; margin-right: 0px;
            margin-top: -5px; background-color: #ffff99; text-align: center; font-weight: bold;
            margin-bottom: 5px">
            <ul>
                <li style="margin-left: 330px;">
                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                        GRADE DE PACIENTES COM PRÉ-ATENDIMENTO - ACOLHIMENTO</label>
                </li>
                <li style="float: right; margin-top: 2px;">
                    <ul>
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger EventName="Tick" ControlID="Timer1" />
            </Triggers>
            <ContentTemplate>
                <div class="divPaciPreAtend">
                    <asp:GridView ID="grdAgendaPlantoes" CssClass="grdBusca" runat="server" Style="width: 100%;
                        height: 15px;" AllowPaging="false" GridLines="Vertical" OnRowDataBound="grdAgendaPlantoes_RowDataBound"
                        AutoGenerateColumns="false" ToolTip="Grid de Pré-Atendimentos em aberto (Clique no checkbox ou em qualquer local da linha para selecionar)"
                        DataKeyNames="ID_PRE_ATEND" OnSelectedIndexChanged="grdAgendaPlantoes_SelectedIndexChanged"
                        AutoGenerateSelectButton="false">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum Pré-Atendimento em aberto<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField Visible="false" DataField="ID_PRE_ATEND" HeaderText="Cod." SortExpression="CO_EMP"
                                HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                <HeaderStyle CssClass="noprint"></HeaderStyle>
                                <ItemStyle Width="20px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="CK">
                                <ItemStyle Width="15px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:HiddenField ID="hidCoPreAtend" Value='<%# Eval("ID_PRE_ATEND") %>' runat="server" />
                                    <asp:HiddenField ID="hidCoEspec" Value='<%# Eval("CO_ESPEC") %>' runat="server" />
                                    <asp:HiddenField ID="hidAntigos" Value='<%# Eval("ANTIGOS") %>' runat="server" />
                                    <asp:HiddenField ID="hidCoRisco" Value='<%# Eval("CO_TIPO_RISCO") %>' runat="server" />
                                    <asp:CheckBox ID="chkselect" runat="server" OnCheckedChanged="chkselect_OnCheckedChanged"
                                        AutoPostBack="true" OnClientClick="alert('Clicou');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField Visible="false" DataField="ID_PRE_ATEND" HeaderText="Cod." SortExpression="CO_EMP"
                                HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                <HeaderStyle CssClass="noprint"></HeaderStyle>
                                <ItemStyle Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NO_PAC" HeaderText="NOME DO PACIENTE">
                                <ItemStyle HorizontalAlign="Left" Width="270px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CO_SEXO" HeaderText="SX">
                                <ItemStyle HorizontalAlign="Left" Width="20px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NU_IDADE" HeaderText="ID">
                                <ItemStyle HorizontalAlign="Left" Width="20px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NO_RESP_V" HeaderText="RESPONSÁVEL">
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NO_ESPEC" HeaderText="ESPECIALIDADE">
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LOCAL" HeaderText="LOCAL">
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DTHR" HeaderText="ACOLHIMENTO">
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="CLAS. DE RISCO">
                                <ItemStyle Width="145px" HorizontalAlign="Left" />
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
                                        <li style="margin-top: -1px; clear: none;">
                                            <asp:Label runat="server" ID="lblg1" Text='<%# Eval("CLASS_RISCO") %>'></asp:Label>
                                        </li>
                                    </ul>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SENHA" HeaderText="SENHA">
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NU_PRE_ATEND" HeaderText="PRÉ-ATENDIM">
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:CommandField HeaderText="Nome" ShowSelectButton="false" Visible="False" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
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
                                <ul style="float: right; margin: 3px 5px 0 0;">
                                    <li>
                                        <asp:CheckBox runat="server" ID="chkEncaComPreAtend" Text="Direcionamento com Pré-Atendimento (Acolhimento)"
                                            CssClass="chk" />
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
                            <li class="liTituloGrid" style="width: 100%; height: 32px !important; margin-right: 0px;
                                background-color: #d2ffc2; text-align: center; font-weight: bold; margin-bottom: 5px;
                                padding-top: 2px;">
                                <ul>
                                    <li style="margin-left: 10px;">
                                        <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: #8B1A1A">
                                            DIRECIONAMENTO
                                            <br />
                                            MÉDICO</label>
                                    </li>
                                    <li style="margin-left: 23px;">
                                        <label>
                                            Pesquisa por Especialidade e/ou Local</label>
                                        <asp:DropDownList runat="server" ID="ddlPesqEspec" Width="140px">
                                        </asp:DropDownList>
                                        <asp:DropDownList runat="server" ID="ddlPesqLocal" Width="70px">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin: 13px 2px 0 -2px;">
                                        <asp:ImageButton ID="imgPesqGridMedic" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                            OnClick="imgPesqGridMedic_OnClick" Width="13px" Height="13px" />
                                    </li>
                                </ul>
                            </li>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ul>
                <div class="divEncamMedico">
                    <ul>
                        <li>
                            <asp:UpdatePanel ID="updProfiPlantao" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdMedicosPlanto" CssClass="grdBusca" runat="server" Style="width: 100%;
                                        cursor: default" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                        OnRowDataBound="grdMedicosPlanto_RowDataBound" DataKeyNames="co_col">
                                        <RowStyle CssClass="rowStyle" />
                                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                        <EmptyDataTemplate>
                                            Nenhum Plantonista agendado para este horário<br />
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField Visible="false" DataField="co_col" HeaderText="Cod." SortExpression="CO_EMP"
                                                HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                                <HeaderStyle CssClass="noprint"></HeaderStyle>
                                                <ItemStyle Width="20px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="CK">
                                                <%--<HeaderTemplate>
                                            <asp:CheckBox ID="chkTodos" runat="server" AutoPostBack="true" OnCheckedChanged="chkTodos_OnCheckedChanged" />
                                        </HeaderTemplate>--%>
                                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hidcoCol" Value='<%# Eval("co_col") %>' runat="server" />
                                                    <asp:HiddenField ID="hidcoEmpColPla" Value='<%# Eval("co_emp_col_pla") %>' runat="server" />
                                                    <asp:HiddenField ID="hidCoEspec" Value='<%# Eval("CO_ESPEC") %>' runat="server" />
                                                    <asp:CheckBox ID="chkselect2" runat="server" OnCheckedChanged="chkselect2_OnCheckedChanged"
                                                        AutoPostBack="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NO_COL" HeaderText="NOME DO MÉDICO">
                                                <ItemStyle HorizontalAlign="Left" Width="230px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NO_ESPEC" HeaderText="ESPECIALIDADE">
                                                <ItemStyle HorizontalAlign="Left" Width="115px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NO_ESPEC" HeaderText="LOCAL">
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField HeaderText="Nome" ShowSelectButton="false" Visible="False" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </li>
                    </ul>
                </div>
                <ul>
                    <asp:UpdatePanel ID="updInfosBottom" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <li style="margin-top: 5px; margin-left: 0px;">
                                <label class="lblObrigatorio">
                                    Especialidade</label>
                                <asp:DropDownList runat="server" ID="ddlEspec" Width="130px">
                                </asp:DropDownList>
                            </li>
                            <li style="margin-top: 5px; margin-left: 0px;">
                                <label>
                                    Local</label>
                                <asp:DropDownList runat="server" ID="ddlLocalEncam" Width="80px">
                                </asp:DropDownList>
                            </li>
                            <li style="margin-top: 5px; margin-left: 5px;">
                                <label class="lblObrigatorio">
                                    Classificação de Risco</label>
                                <asp:DropDownList runat="server" ID="ddlClassRisco" Width="97px" ClientIDMode="Static">
                                </asp:DropDownList>
                            </li>
                            <li style="margin-top: 10px;">
                                <asp:HiddenField runat="server" ID="hidDivAberta" ClientIDMode="Static" />
                                <div id="divClassRisc" title="Selecione a Classificação de risco pela cor" style="margin: 8px 0 0 -6px;
                                    width: 40px; height: 9px; border: 1px solid #CCCCCC; position: absolute; background-color: White;
                                    padding: 2px">
                                    <div runat="server" id="divClassRiscCorSelec" class="divClassPri" style="height: 9px;
                                        width: 100%" clientidmode="Static">
                                    </div>
                                    <div id="divClass1" title="Emergência" style="display: none; height: 100%; width: 30px;
                                        background-color: Red; cursor: pointer; float: left;">
                                    </div>
                                    <div id="divClass2" title="Muito Urgente" style="display: none; height: 100%; width: 30px;
                                        background-color: Orange; cursor: pointer; float: left; margin-left: 5px;">
                                    </div>
                                    <div id="divClass3" title="Urgente" style="display: none; height: 100%; width: 30px;
                                        background-color: Yellow; cursor: pointer; float: left; margin-left: 5px;">
                                    </div>
                                    <div id="divClass4" title="Pouco Urgente" style="display: none; height: 100%; width: 30px;
                                        background-color: Green; cursor: pointer; float: left; margin-left: 5px;">
                                    </div>
                                    <div id="divClass5" title="Não Urgente" style="display: none; height: 100%; width: 30px;
                                        background-color: Blue; cursor: pointer; float: left; margin-left: 5px;">
                                    </div>
                                    <div id="divFecha" title="Fechar paleta" style="display: none; float: right; margin-left: 5px;">
                                        <a id="lnkClose" class="lnkClose" title="Fechar paleta" href="#">[x]</a>
                                    </div>
                                </div>
                            </li>
                            <li id="li16" runat="server" class="liBtnAddA liPrima" style="margin-left: 60px !important;
                                margin-top: 10px !important; clear: both !important; height: 15px;">
                                <asp:LinkButton ID="lnkEfetAtendMed" runat="server" ValidationGroup="atuEndAlu">
                                    <asp:Label runat="server" ID="Label31" Text="FINALIZAR" Style="margin-left: 4px;
                                        color: #0000ee"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li id="li17" runat="server" class="liBtnAddA" style="margin-left: 5px !important;
                                margin-top: 10px !important; clear: none !important;">
                                <asp:LinkButton ID="lnkImpFichaAtendMed" Enabled="false" runat="server" ValidationGroup="atuEndAlu">
                                    <img id="img6" runat="server" width="14" height="14" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                                        alt="Icone Pesquisa" title="Imprime Ficha de Atendimento" />
                                    <asp:Label runat="server" ID="Label32" Text="FICHA DE ATENDIMENTO" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ul>
            </div>
        </div>
        <div id="divLoadShowResponsaveis" style="display: none; height: 315px !important;" />
        <div id="divLoadShowAlunos" style="display: none; height: 305px !important;" />
    </ul>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".campoCpf").mask("999.999.999-99");
            $(".campoCepF").mask("99999-999");
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

        //        Sys.Application.add_load(ApplicationLoadHandler)
        //        function ApplicationLoadHandler(sender, args) {
        //            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(CheckStatus);
        //        }

        //        function CheckStatus(sender, args) {
        //            var prm = Sys.WebForms.PageRequestManager.getInstance();
        //            if(prm.get_isInAscyncPostBack() & args.get_postBackElement().id
        //        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            carregaCss();
        });

    </script>
</asp:Content>
