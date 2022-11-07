<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.CadastramentoInfUnidEnsinoApoio.Cadastro"
    ValidateRequest="false" %>

<%@ Register Src="../../../../Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        legend
        {
            padding: 0 3px 0 3px;
        }
        .ulDados
        {
            margin-top: 0px !important;
        }
        input[type="text"]
        {
            font-size: 10px !important;
            font-family: Arial !important;
        }
        select
        {
            font-family: Arial !important;
            border: 1px solid #BBBBBB !important;
            font-size: 0.9em !important;
            height: 15px !important;
        }
        .liChk > span
        {
            margin-bottom: 3px;
        }
        /*--> CSS GRID */
        .grdHorarios tr
        {
            border: solid 1px #CCCCCC;
        }
        .grdHorarios th
        {
            padding: 3px;
            font-family: Arial;
            white-space: normal;
            background-color: #5F9EA0;
            font-weight: bold;
            color: #ffffff;
            text-align: center;
            text-transform: uppercase;
            border: solid 1px #CCCCCC;
        }
        .grdHorarios td
        {
            padding: 2px 2px 2px 5px;
            border: solid 1px #CCCCCC;
        }
        .grdHorarios .rowStyle
        {
            padding-left: 5px;
            background-color: #FFFFF0;
            color: #333333;
            text-align: left;
            vertical-align: middle;
        }
        
        /* FIM CSS GRID */
        
        .hStyle
        {
            display: none;
        }
        
        .divGridimpostos
        {
            width: 670px;
            height: 350px;
            border: 1px solid #CCCCCC;
            float: none;
            margin: 20px 0 15px 50px;
            overflow-y: scroll;
        }
        .chk label
        {
            display: inline;
        }
        /*--> CSS LIs */
        .liUnidade
        {
            margin-top: 4px;
            padding-right: 6px;
            width: 370px;
            border-right: 1px solid #CCCCCC;
            height: 94px;
        }
        .liUnidade2
        {
            margin-top: 20px;
            border-right: 1px solid #CCCCCC;
            padding-right: 10px;
        }
        .liEmail
        {
            margin-top: 20px;
            border-right: 1px solid #CCCCCC;
            padding-right: 10px;
            margin-right: 13px;
            padding-left: 0;
            clear: both;
        }
        .liUnidade3
        {
            margin-top: 20px;
            padding-left: 10px;
        }
        .liUnidade4
        {
            margin-top: 20px;
            border-right: 1px solid #CCCCCC;
            padding-right: 10px;
            padding-left: 10px;
            height: 114px;
        }
        .liUnidade5
        {
            margin-top: 20px;
            border-right: 1px solid #CCCCCC;
            padding-right: 10px;
            padding-left: 10px;
        }
        .liUnidade6
        {
            margin-top: 20px;
            padding-left: 10px;
        }
        .liNucleo
        {
            margin-left: 15px;
        }
        .liClear
        {
            clear: both;
        }
        .liUnidade li, .liUnidade2 li
        {
            margin-top: -7px;
        }
        .liContrAval
        {
            clear: both;
            margin-top: 7px;
        }
        .liPerioAval
        {
            margin-top: 4px;
            clear: both;
        }
        .liAprovDireta, .liAprovGeral
        {
            clear: both;
            width: 190px;
        }
        .liMediaRecuperacao
        {
            clear: both;
            margin-left: 7px;
            margin-left: 25px;
        }
        .liLimiteMedia
        {
            margin-left: 7px;
        }
        #liQtdMateriasRecuperacao, #liQtdMateriasDependencia, #liQtdMaxMaterias
        {
            margin-left: 2px;
            margin-right: 0px;
        }
        .liControleMetodologia
        {
            width: 220px;
            margin-top: 7px;
            clear: both;
        }
        .liMetodEnsino
        {
            width: 220px;
            clear: both;
            margin-top: 3px;
        }
        .liLogradouro, .liBairro
        {
            margin-left: 10px;
        }
        .liPesqCEP
        {
            margin-top: 7px !important;
            margin-left: -3px;
        }
        .liDataReserva
        {
            width: 120px;
            padding: 4px 0 4px 0px;
            clear: both;
        }
        .liPedagMatric
        {
            background-color: #F9F9FF;
            padding: 4px 0 4px 0px;
            color: #006699;
            text-align: center;
            width: 220px;
        }
        .liEquivNotaConce
        {
            background-color: #FFE8C4;
            color: #797979;
            padding: 4px 0 4px 0px;
            text-align: center;
            width: 220px;
            clear: both;
            margin-top: 10px;
        }
        .liTopData
        {
            margin-top: 4px;
        }
        .liHorarioTp1
        {
            background-color: #F9F9FF;
            width: 80px;
            padding: 2px 0 2px 10px;
            clear: both;
            margin-top: 11px;
        }
        .liHorarioTp
        {
            background-color: #F9F9FF;
            width: 80px;
            padding: 2px 0 2px 10px;
            clear: both;
        }
        .liNotaConceito
        {
            clear: both;
            margin-top: 2px;
        }
        .liNotaConceito input
        {
            margin-bottom: 2px;
        }
        
        /*--> CSS DADOS */
        #tabDadosCadas
        {
            height: 282px;
            padding: 10px 0 0 10px;
            width: 800px;
        }
        #tabQuemSomos, #tabNossaHisto, #tabPropoPedag, #tabFrequFunci, #tabSecreEscol, #tabBibliEscol, #tabPedagMatric, #tabMensaSMS, #tabContabil, #tabFinanceiro, #tabImpostos, #tabGestorUnidade, #tabControleSaude, #tabControleSaude2
        {
            height: 282px;
            padding: 6px 0 0 30px;
            width: 780px;
        }
        .txtRazaoSocialIUE
        {
            width: 300px;
            text-transform: uppercase;
        }
        .txtInstitEnsino
        {
            width: 300px;
            text-transform: uppercase;
            text-align: center;
        }
        .txtNomeIUE
        {
            width: 225px;
        }
        .txtSiglaIUE
        {
            width: 85px;
        }
        .txtCNPJIUE
        {
            width: 100px;
            text-align: right;
        }
        .txtTelefone
        {
            width: 74px;
        }
        .txtLogradouroIUE
        {
            width: 225px;
        }
        .txtComplementoIUE
        {
            width: 164px;
        }
        .ddlBairroIUE
        {
            width: 170px;
        }
        .ddlCidadeIUE
        {
            width: 195px;
        }
        .txtCEPIUE
        {
            width: 55px;
        }
        .txtObservacaoIUE
        {
            width: 200px;
            height: 63px;
        }
        .txtEmailIUE, .txtWebSiteIUE
        {
            width: 236px;
        }
        .txtQtdeDiasEntreSolic
        {
            margin-bottom: 0px !important;
            text-align: right;
            width: 18px;
        }
        .txtNumControle
        {
            width: 86px;
        }
        .ddlAlterPrazoEntreSolic
        {
            width: 40px;
        }
        .txtHorarioFuncManha, .txtHorarioFuncTarde, .txtHorarioFuncNoite
        {
            width: 30px;
        }
        .txtQtdItensAcervoIUE
        {
            width: 30px;
        }
        .txtCabecalhoRelatorio
        {
            width: 345px;
        }
        .liControleMetodologia label, .liMetodEnsino label, .liContrAval label, .liPerioAval label
        {
            display: inline;
        }
        .liAprovDireta label, .liAprovGeral label, .liMediaRecuperacao label, .liRecuperacao label, #liDependencia label
        {
            display: inline;
        }
        #liConselho label, #liQtdMaxMaterias label, .liLimiteMedia label, #liQtdMateriasRecuperacao label, #liQtdMateriasDependencia label
        {
            display: inline;
        }
        .liMediaRecuperacao input, .liMediaRecuperacao select
        {
            float: right;
            margin-left: 3px;
        }
        .campoMoeda
        {
            width: 30px;
            text-align: right;
        }
        .ddlTipoAplic
        {
            width: 40px;
        }
        .campoNumerico
        {
            width: 30px;
        }
        .divTabs
        {
            height: 383px;
            padding: 0.2em;
            position: relative;
        }
        #ControleImagem .liControleImagemComp .fakefile
        {
            width: 120px !important;
        }
        .rblInforCadastro label
        {
            display: inline;
            font-size: 1.1em;
        }
        .rblInforCadastro
        {
            border-width: 0px;
        }
        .rblInforControle label
        {
            display: inline;
            font-size: 1.1em;
        }
        .rblInforControle
        {
            border-width: 0px;
        }
        .divTipoEnsino
        {
            height: 100px;
            width: 128px;
            border: solid 1px #CCCCCC;
            background-color: #F0FFFF;
        }
        .divTipoEnsino table tr td label
        {
            display: inline;
            margin-left: 0px;
        }
        .divTipoEnsino table
        {
            border: none;
        }
        .ddlNucleoIUE
        {
            width: 115px;
        }
        .btnPesqCEP
        {
            width: 13px;
        }
        .txtLatitude, .txtLongitude
        {
            width: 120px;
        }
        .txtNumDocto
        {
            width: 90px;
        }
        .ddlStatusIUE
        {
            width: 95px;
        }
        .txtDescrConce
        {
            width: 95px;
            padding-left: 3px;
        }
        .ddlUFIUE
        {
            width: 40px;
        }
        .txtTipoCtrlFrequ
        {
            width: 105px;
        }
        .txtContrMetod, .txtInscEstadualIUE, .ddlCtrlHoraExtra, .txtContrAval
        {
            width: 100px;
        }
        .ddlMetodEnsino, .ddlFormaAvali, .ddlPerioAval
        {
            width: 105px;
        }
        .txtSiglaConce
        {
            width: 25px;
            padding-left: 3px;
        }
        .txtNotaIni, .txtNotaFim
        {
            width: 30px;
            text-align: right;
        }
        .chkRecuperEscol label
        {
            color: #336699;
        }
        .txtNumIniciSolicAuto
        {
            width: 55px;
        }
        .ddlSiglaUnidSecreEscol
        {
            width: 60px;
        }
        .ddlNomeSecreEscol
        {
            width: 250px;
        }
        .ddlQtdeSecretario
        {
            width: 35px;
        }
        .ddlPermiMultiFrequ, .ddlDirGeralDir
        {
            width: 45px;
        }
        .txtTitulDirCoor
        {
            width: 220px;
        }
        .ddlTipoEnsinoDir
        {
            width: 110px;
        }
        .chknonoDigito label
        {
            display: inline;
        }
        .rblInforCadastro tr, .rblInforControle tr
        {
            height: 18px;
        }
        .rdb label
        {
            display: inline;
        }
        .qtdeDias input
        {
            width: 27px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul style="width: 1000px; margin-top: 10px;">
        <li class="liClear" style="margin-left: 420px; margin-top: 0px;">
            <asp:TextBox ID="txtInstituicaoPrinc" ToolTip="Instituição de Ensino" Enabled="false"
                BackColor="#FFFFE1" CssClass="txtInstitEnsino" runat="server"></asp:TextBox>
        </li>
        <li style="float: left; width: 160px; margin-top: 12px; padding-left: 15px; height: 380px;
            margin-right: -2px;">
            <ul>
                <li style="background-color: #4682B4; padding: 3px 0 3px 0; text-align: center;"><span
                    style="font-size: 1.1em; font-family: arial; font-weight: bold; color: white;">MENU
                    OPÇÕES</span> </li>
                <li style="background-color: #B0C4DE; padding: 3px 0 3px 2px; text-align: center;
                    margin-bottom: 2px;"><span style="text-transform: uppercase; font-size: 1.1em;">Informações
                        Cadastrais</span> </li>
                <li style="background-color: #F5FFFF; height: 114px; padding-top: 10px;">
                    <asp:RadioButtonList ID="rblInforCadastro" ClientIDMode="Static" CssClass="rblInforCadastro"
                        runat="server" RepeatDirection="Vertical" Width="190px">
                        <asp:ListItem Text="Dados Cadastrais" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Quem somos" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Nossa História" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Proposta Pedagógica" Value="4"></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
                <li style="background-color: #B0C4DE; padding: 3px 0 3px 2px; text-align: center;
                    margin-bottom: 2px;"><span style="text-transform: uppercase; font-size: 1.1em;">Informações
                        de Controle</span> </li>
                <li style="background-color: #F5FFFF; height: 2220px; padding-top: 10px;">
                    <asp:RadioButtonList ID="rblInforControle" ClientIDMode="Static" CssClass="rblInforControle"
                        runat="server" RepeatDirection="Vertical" Width="190px">
                        <asp:ListItem Text="Controle Class. Funcional" Value="14"></asp:ListItem>
                        <asp:ListItem Text="Controle de Saúde" Value="15"></asp:ListItem>
                        <asp:ListItem Text="Frequência Funcional" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Pedagógico / Matrículas" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Gestores da Unidade" Value="12"></asp:ListItem>
                        <asp:ListItem Text="Secretaria Escolar" Value="7"></asp:ListItem>
                        <asp:ListItem Text="Biblioteca" Value="8"></asp:ListItem>
                        <asp:ListItem Text="Contábil" Value="9"></asp:ListItem>
                        <asp:ListItem Text="Mensagens SMS" Value="10"></asp:ListItem>
                        <asp:ListItem Text="Financeiro" Value="11"></asp:ListItem>
                        <asp:ListItem Text="Impostos" Value="13"></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
            </ul>
        </li>
        <li style="float: left; width: 820px;">
            <div class="divTabs">
                <div id="tabDadosCadas" clientidmode="Static" class="tabDadosCadas" runat="server">
                    <ul id="ulDados1" class="ulDados">
                        <li>
                            <ul>
                                <li class="liFoto">
                                    <uc1:ControleImagem ID="imgUnidadeIUE" runat="server" />
                                </li>
                            </ul>
                        </li>
                        <li class="liUnidade">
                            <ul>
                                <li class="liClear" style="margin-top: -7px;">
                                    <label for="txtRazaoSocialIUE" class="lblObrigatorio" title="Razão Social">
                                        Unidade</label>
                                    <asp:TextBox ID="txtRazaoSocialIUE" ToolTip="Informe a Razão Social" Style="text-transform: none;
                                        width: 240px;" CssClass="txtRazaoSocialIUE" MaxLength="80" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRazaoSocialIUE"
                                        ErrorMessage="Razão Social deve ser informada" Display="None">
                                    </asp:RequiredFieldValidator>
                                </li>
                                <li style="margin-left: 28px; margin-top: -7px; margin-right: 0px;">
                                    <label for="txtSiglaIUE" class="lblObrigatorio" title="Código de Identificação">
                                        Cód. Identificação</label>
                                    <asp:TextBox ID="txtSiglaIUE" ToolTip="Informe o Código de Identificação" CssClass="txtSiglaIUE"
                                        MaxLength="5" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSiglaIUE"
                                        ErrorMessage="Código de Identificação deve ser informado" Display="None">
                                    </asp:RequiredFieldValidator>
                                </li>
                                <li id="liNome" style="margin-top: -2px !important;">
                                    <label for="txtNomeIUE" class="lblObrigatorio" title="Nome">
                                        Nome de Fantasia (Apelido)</label>
                                    <asp:TextBox ID="txtNomeIUE" Style="margin-bottom: 0;" ToolTip="Informe o Nome da Unidade"
                                        CssClass="txtNomeIUE" MaxLength="80" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNomeIUE"
                                        ErrorMessage="Nome deve ser informada" Display="None">
                                    </asp:RequiredFieldValidator>
                                </li>
                                <li style="margin-top: 41px !important; margin-left: -235px; width: 151px">
                                    <asp:CheckBox ID="chkUnidadeMatriz" CssClass="chk" runat="server" Text="Unidade Matriz" />
                                </li>
                                <li class="liNucleo" style="margin-top: -2px !important; margin-right: 0px;">
                                    <label for="ddlNucleoIUE" title="Núcleo">
                                        N&uacute;cleo / Regional</label>
                                    <asp:DropDownList ID="ddlNucleoIUE" CssClass="ddlNucleoIUE" ToolTip="Selecione o Núcleo"
                                        runat="server">
                                    </asp:DropDownList>
                                </li>
                            </ul>
                        </li>
                        <li style="border-right: 1px solid #CCCCCC; margin-top: 4px; padding-right: 5px;
                            padding-left: 5px;">
                            <ul>
                                <li class="liClear" style="margin-top: -7px !important;">
                                    <label for="txtCNPJIUE" class="lblObrigatorio" title="CNPJ">
                                        Nº CNPJ</label>
                                    <asp:TextBox ID="txtCNPJIUE" ToolTip="Informe o CNPJ" CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCNPJIUE"
                                        ErrorMessage="CNPJ deve ser informado" Display="None">
                                    </asp:RequiredFieldValidator>
                                </li>
                                <li class="liClear" style="margin-top: -2px !important;">
                                    <label for="txtInscEstadualIUE" title="Número Inscrição Estadual">
                                        Nº Inscrição Estadual</label>
                                    <asp:TextBox ID="txtInscEstadualIUE" MaxLength="20" ToolTip="Informe a Inscrição Estadual"
                                        CssClass="txtInscEstadualIUE" runat="server"></asp:TextBox>
                                </li>
                                <li class="liClear" style="margin-top: -2px !important;">
                                    <label for="txtINEPIUE" class="lblObrigatorio" title="Número do NIS">
                                        N° NIS</label>
                                    <asp:TextBox ID="txtINEPIUE" Style="margin-bottom: 0px;" ToolTip="Informe o Número do NIS"
                                        CssClass="txtNumControle" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtINEPIUE"
                                        ErrorMessage="NIS deve ser informado" Display="None">
                                    </asp:RequiredFieldValidator>
                                </li>
                            </ul>
                        </li>
                        <li style="margin-right: 0px; padding-left: 5px;">
                            <ul>
                                <li style="background-color: #DDDDDD; text-transform: uppercase; width: 130px; text-align: center;
                                    padding: 1px 0 1px 0; margin-bottom: 1px;">
                                    <label for="cblTipoEnsino" title="Tipos de Ensino da Unidade Escolar">
                                        Tipos de Ensino</label></li>
                                <li class="liClear">
                                    <div class="divTipoEnsino">
                                        <asp:CheckBoxList ID="cblTipoEnsino" CssClass="cblTipoEnsino" ToolTip="Marque os Tipos de Ensino existentes"
                                            runat="server">
                                        </asp:CheckBoxList>
                                    </div>
                                </li>
                            </ul>
                        </li>
                        <li class="liUnidade2">
                            <ul>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <li style="margin-bottom: 9px; margin-top: 0px;"><span style="text-transform: uppercase;
                                            color: #87CEFA; font-size: 1.0em;">Informações de Endereço</span></li>
                                        <li class="liClear">
                                            <label for="txtCEPIUE" class="lblObrigatorio" title="CEP">
                                                CEP</label>
                                            <asp:TextBox ID="txtCEPIUE" ToolTip="Informe o CEP" CssClass="txtCEPIUE" runat="server"></asp:TextBox>
                                        </li>
                                        <li class="liPesqCEP">
                                            <asp:ImageButton ID="btnPesqCEP" runat="server" OnClick="btnPesqCEP_Click" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                                                class="btnPesqCEP" CausesValidation="false" />
                                        </li>
                                        <li class="liLogradouro">
                                            <label for="txtLogradouroIUE" class="lblObrigatorio" title="Endereço">
                                                Descrição do Endere&ccedil;o</label>
                                            <asp:TextBox ID="txtLogradouroIUE" ToolTip="Informe o Endereço" CssClass="txtLogradouroIUE"
                                                MaxLength="60" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLogradouroIUE" ErrorMessage="Logradouro deve ser informado"
                                                Display="None">
                                            </asp:RequiredFieldValidator>
                                        </li>
                                        <li>
                                            <label for="txtComplementoIUE" title="Número">
                                                Número</label>
                                            <asp:TextBox ID="txtNumeroLograIUE" ToolTip="Informe o Número do Logradouro" CssClass="txtQtdItensAcervoIUE"
                                                runat="server"></asp:TextBox>
                                        </li>
                                        <li class="liClear">
                                            <label for="txtComplementoIUE" title="Complemento">
                                                Complemento</label>
                                            <asp:TextBox ID="txtComplementoIUE" ToolTip="Informe o Complemento" CssClass="txtComplementoIUE"
                                                MaxLength="30" runat="server"></asp:TextBox>
                                        </li>
                                        <li class="liBairro">
                                            <label for="ddlBairroIUE" class="lblObrigatorio" title="Bairro">
                                                Bairro</label>
                                            <asp:DropDownList ID="ddlBairroIUE" ToolTip="Selecione o Bairro" CssClass="ddlBairroIUE"
                                                runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlBairroIUE" ErrorMessage="Bairro deve ser informado"
                                                Display="None">
                                            </asp:RequiredFieldValidator>
                                        </li>
                                        <li class="liClear">
                                            <label for="ddlCidadeIUE" class="lblObrigatorio" title="Cidade">
                                                Cidade</label>
                                            <asp:DropDownList ID="ddlCidadeIUE" ToolTip="Selecione a Cidade" CssClass="ddlCidadeIUE"
                                                OnSelectedIndexChanged="ddlCidadeIUE_SelectedIndexChanged" AutoPostBack="true"
                                                runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCidadeIUE" ErrorMessage="Cidade deve ser informada"
                                                Display="None">
                                            </asp:RequiredFieldValidator>
                                        </li>
                                        <li>
                                            <label for="ddlUFIUE" class="lblObrigatorio" title="UF">
                                                UF</label>
                                            <asp:DropDownList ID="ddlUFIUE" ToolTip="Selecione a UF" CssClass="ddlUFIUE" OnSelectedIndexChanged="ddlUFIUE_SelectedIndexChanged"
                                                AutoPostBack="true" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlUFIUE" ErrorMessage="UF deve ser informada"
                                                Display="None">
                                            </asp:RequiredFieldValidator>
                                        </li>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ul>
                        </li>
                        <li class="liUnidade4">
                            <ul>
                                <li style="margin-bottom: 2px;"><span style="text-transform: uppercase; color: #87CEFA;
                                    font-size: 1.0em;">Georeferenciamento</span></li>
                                <li class="liClear">
                                    <label for="txtLatitude" title="Latitude">
                                        Latitude</label>
                                    <asp:TextBox ID="txtLatitude" ToolTip="Informe a Latitude" MaxLength="20" CssClass="txtLatitude"
                                        runat="server"></asp:TextBox>
                                </li>
                                <li class="liClear" style="margin-top: -7px;">
                                    <label for="txtLongitude" title="Longitude">
                                        Longitude</label>
                                    <asp:TextBox ID="txtLongitude" MaxLength="20" ToolTip="Informe a Longitude" CssClass="txtLongitude"
                                        runat="server"></asp:TextBox>
                                </li>
                            </ul>
                        </li>
                        <li class="liUnidade3">
                            <ul>
                                <li style="margin-bottom: 2px;"><span style="text-transform: uppercase; color: #87CEFA;
                                    font-size: 1.0em;">Informações de Contato</span></li>
                                <li class="liClear">
                                    <label for="txtTelefoneIUE" title="Número Telefone Geral">
                                        Nº Tel Geral</label>
                                    <asp:TextBox ID="txtTelefoneIUE" ToolTip="Informe o Número do Telefone" CssClass="txtTelefone"
                                        runat="server"></asp:TextBox>
                                </li>
                                <li>
                                    <label for="txtTelefoneIUE2" title="Número Telefone Diretoria">
                                        Nº Tel Diretoria</label>
                                    <asp:TextBox ID="txtTelefoneIUE2" ToolTip="Informe o Número do Telefone" CssClass="txtTelefone"
                                        runat="server"></asp:TextBox>
                                </li>
                                <li>
                                    <label for="txtFaxIUE" title="Número Telefone Secretaria">
                                        Nº Tel Secretaria</label>
                                    <asp:TextBox ID="txtFaxIUE" ToolTip="Informe o Número do Telefone" CssClass="txtTelefone"
                                        runat="server"></asp:TextBox>
                                </li>
                                <li class="liClear" style="margin-top: -7px;">
                                    <label for="txtEmailIUE" title="E-mail Institucional">
                                        E-mail Institucional</label>
                                    <asp:TextBox ID="txtEmailIUE" ToolTip="Informe o E-mail Institucional" CssClass="txtEmailIUE"
                                        MaxLength="60" runat="server"></asp:TextBox>
                                </li>
                                <li class="liClear" style="margin-top: -7px;">
                                    <label for="txtWebSiteIUE" title="Home Page">
                                        Home Page</label>
                                    <asp:TextBox ID="txtWebSiteIUE" ToolTip="Informe o Home Page" CssClass="txtWebSiteIUE"
                                        MaxLength="60" runat="server"></asp:TextBox>
                                </li>
                                <li class="liClear" style="margin-left: -4px; margin-top: -5px">
                                    <asp:CheckBox runat="server" ID="chkNonoDigito" class="chknonoDigito" Text="9º Dígito Telefones" />
                                </li>
                            </ul>
                        </li>
                        <li class="liEmail" style="">
                            <ul>
                                <li style="margin-bottom: 2px;"><span style="text-transform: uppercase; color: #87CEFA;
                                    font-size: 1.0em;">Mensageria</span> </li>
                                <li class="liClear">
                                    <label for="txtEmail" title="Latitude">
                                        Email *(apenas conta do gmail)</label>
                                    <asp:TextBox ID="txtEmail" ToolTip="Informe o email (apenas conta do gmail)" MaxLength="100"
                                        CssClass="txtEmail" runat="server"></asp:TextBox>
                                </li>
                                <li class="liClear" style="margin-top: -7px;">
                                    <label for="txtSenha" title="Longitude">
                                        Senha</label>
                                    <asp:TextBox ID="txtSenha" MaxLength="100" ToolTip="Informe a senha do email" CssClass="txtSenha"
                                        runat="server"></asp:TextBox>
                                    <%--<asp:CheckBox type="button" ID="chcSenha" runat="server" OnCheckedChanged="chcSenha_OnCheckedChanged" AutoPostBack="true" ToolTip="Visualizar senha"/>--%>
                                </li>
                            </ul>
                        </li>
                        <li class="liUnidade5" style="padding-left: 0px;">
                            <ul>
                                <li style="margin-bottom: 2px;"><span style="text-transform: uppercase; color: #87CEFA;
                                    font-size: 1.0em;">Horário de Atividades</span></li>
                                <li class="liClear" style="border-right: 1px solid #CCCCCC; border-bottom: 1px solid #CCCCCC;
                                    margin-right: 0px;">
                                    <ul>
                                        <li><span title="Turno 1">Turno 1</span></li>
                                        <li class="liClear">
                                            <asp:TextBox ID="txtHoraIniTurno1" Style="margin-bottom: 5px;" ToolTip="Informe o Horário Inicial do Turno 1"
                                                CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                                        </li>
                                        <li><span>às </span></li>
                                        <li>
                                            <asp:TextBox ID="txtHoraFimTurno1" Style="margin-bottom: 5px;" ToolTip="Informe o Horário Final do Turno 1"
                                                CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                                        </li>
                                    </ul>
                                </li>
                                <li style="border-bottom: 1px solid #CCCCCC; padding-left: 5px;">
                                    <ul>
                                        <li><span title="Turno 2">Turno 2</span></li>
                                        <li class="liClear">
                                            <asp:TextBox ID="txtHoraIniTurno2" Style="margin-bottom: 5px;" ToolTip="Informe o Horário Inical do Turno 2"
                                                CssClass="txtHorarioFuncTarde" runat="server"></asp:TextBox>
                                        </li>
                                        <li><span>às </span></li>
                                        <li>
                                            <asp:TextBox ID="txtHoraFimTurno2" Style="margin-bottom: 5px;" ToolTip="Informe o Horário Final do Turno 2"
                                                CssClass="txtHorarioFuncTarde" runat="server"></asp:TextBox>
                                        </li>
                                    </ul>
                                </li>
                                <li class="liClear" style="border-right: 1px solid #CCCCCC;">
                                    <ul>
                                        <li class="liClear" title="Turno 3"><span>Turno 3</span></li>
                                        <li class="liClear">
                                            <asp:TextBox ID="txtHoraIniTurno3" Style="margin-bottom: 5px;" ToolTip="Informe o Horário Inicial do Turno 3"
                                                CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                        </li>
                                        <li><span>às </span></li>
                                        <li>
                                            <asp:TextBox ID="txtHoraFimTurno3" Style="margin-bottom: 5px;" ToolTip="Informe o Horário Final do Turno 3"
                                                CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <ul>
                                        <li class="liClear" title="Turno 4"><span>Turno 4</span></li>
                                        <li class="liClear">
                                            <asp:TextBox ID="txtHoraIniTurno4" Style="margin-bottom: 5px;" ToolTip="Informe o Horário Inicial do Turno 4"
                                                CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                        </li>
                                        <li><span>às </span></li>
                                        <li>
                                            <asp:TextBox ID="txtHoraFimTurno4" Style="margin-bottom: 5px;" ToolTip="Informe o Horário Final do Turno 4"
                                                CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li class="liUnidade5" style="border-right: none;">
                            <ul>
                                <li style="margin-bottom: 4px;"><span style="text-transform: uppercase; color: #87CEFA;
                                    font-size: 1.0em;">Observações</span></li>
                                <li class="liClear">
                                    <ul>
                                        <li>
                                            <asp:TextBox ID="txtObservacaoIUE" runat="server" ToolTip="Informe a Observação"
                                                Style="overflow-y: hidden;" CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 150);"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li class="liUnidade5" style="clear: both !important; padding-left: 0 !important;">
                            <ul>
                                <li style="margin-bottom: 2px;"><span style="text-transform: uppercase; color: #87CEFA;
                                    font-size: 1.0em;">Constituição e Cadastro</span></li>
                                <li class="liClear">
                                    <label for="txtDtCadastroIUE" title="Data Cadastro">
                                        Data de Constituição</label>
                                    <asp:TextBox ID="txtDataConstituicao" ToolTip="Informe a Data de Constituição" CssClass="campoData"
                                        runat="server"></asp:TextBox>
                                </li>
                                <li style="margin-top: 0px;">
                                    <label for="txtNumDocto" title="Nº do Documento">
                                        Nº do Documento</label>
                                    <asp:TextBox ID="txtNumDocto" MaxLength="20" ToolTip="Informe o Número do Documento"
                                        CssClass="txtNumDocto" runat="server"></asp:TextBox>
                                </li>
                                <li class="liClear" style="margin-top: -4px;">
                                    <label for="txtDtCadastroIUE" title="Data Cadastro">
                                        Data de Cadastro</label>
                                    <asp:TextBox ID="txtDtCadastroIUE" Style="margin-bottom: 5px;" ToolTip="Informe a Data de Cadastro"
                                        CssClass="campoData" Enabled="false" runat="server"></asp:TextBox>
                                </li>
                            </ul>
                        </li>
                        <li class="liUnidade6">
                            <ul>
                                <li style="margin-bottom: 2px;"><span style="text-transform: uppercase; color: #87CEFA;
                                    font-size: 1.0em;">Situação</span></li>
                                <li class="liClear">
                                    <label for="txtDtStatusIUE" title="Data Situação">
                                        Data da Situação</label>
                                    <asp:TextBox ID="txtDtStatusIUE" Enabled="false" ToolTip="Informe a Data Situação"
                                        CssClass="campoData" runat="server"></asp:TextBox>
                                </li>
                                <li class="liClear" style="margin-top: -4px;">
                                    <label for="ddlStatusIUE" class="lblObrigatorio" title="Status">
                                        Situação da Unidade</label>
                                    <asp:DropDownList ID="ddlStatusIUE" CssClass="ddlStatusIUE" ToolTip="Selecione a Situação"
                                        runat="server">
                                        <asp:ListItem Value="A">Ativo</asp:ListItem>
                                        <asp:ListItem Value="I">Inativo</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlStatusIUE"
                                        ErrorMessage="Situação da Unidade deve ser informada" Display="None">
                                    </asp:RequiredFieldValidator>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div id="tabQuemSomos" class="tabQuemSomos" style="display: none;">
                    <ul id="ulDados2" class="ulDados">
                        <li class="liClear" style="margin-left: 130px;">
                            <label for="txtUnidadeEscolarQS" title="Razão Social">
                                Unidade Escolar</label>
                            <asp:TextBox ID="txtUnidadeEscolarQS" Enabled="false" ClientIDMode="Static" ToolTip="Informe a Razão Social"
                                CssClass="txtRazaoSocialIUE" MaxLength="80" runat="server"></asp:TextBox>
                        </li>
                        <li class="liCodIdentificacao">
                            <label for="txtCodIdenticacaoQS" title="Código de Identificação">
                                Cód. Identificação</label>
                            <asp:TextBox ID="txtCodIdenticacaoQS" Enabled="false" ClientIDMode="Static" ToolTip="Informe o Código de Identificação"
                                CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-left: 10px;">
                            <label for="txtCNPJQS" title="CNPJ">
                                Nº CNPJ</label>
                            <asp:TextBox ID="txtCNPJQS" Enabled="false" ClientIDMode="Static" ToolTip="Informe o CNPJ"
                                CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="width: 100%; text-align: center;"><span style="color: #6495ED;
                            font-size: 1.3em; font-weight: bold;">Quem somos?</span></li>
                        <li class="liClear" style="margin-top: 2px; margin-left: 322px;">
                            <label for="txtTipoCtrlQuemSomos" style="float: left;" title="Tipo de Controle do Quem Somos">
                                Por</label>
                            <asp:TextBox ID="txtTipoCtrlQuemSomos" Enabled="false" Style="margin-left: 7px; margin-bottom: 5px;
                                padding-left: 3px;" ClientIDMode="static" ToolTip="Tipo de Controle do Quem Somos"
                                BackColor="#FFFFE1" CssClass="txtTipoCtrlFrequ" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-bottom: 5px;"><span style="color: #87CEFA; font-size: 1.1em;">
                            Utilize o editor de texto abaixo para descrever sobre a Unidade Escolar, sua missão,
                            características, equipes administrativas e de profissionais de educação, etc...</span></li>
                        <li>
                            <dx:ASPxHtmlEditor ID="txtQuemSomos" Height="300px" Width="745px" runat="server"
                                Theme="Office2010Blue" ClientInstanceName="txtQuemSomos">
                                <Settings AllowHtmlView="False" AllowPreview="False" />
                                <SettingsResize MinHeight="300" MinWidth="745" />
                            </dx:ASPxHtmlEditor>
                        </li>
                    </ul>
                </div>
                <div id="tabNossaHisto" class="tabNossaHisto" style="display: none;">
                    <ul id="ul5" class="ulDados">
                        <li class="liClear" style="margin-left: 130px;">
                            <label for="txtUnidadeEscolarNH" title="Razão Social">
                                Unidade Escolar</label>
                            <asp:TextBox ID="txtUnidadeEscolarNH" Enabled="false" ClientIDMode="Static" ToolTip="Informe a Razão Social"
                                CssClass="txtRazaoSocialIUE" MaxLength="80" runat="server"></asp:TextBox>
                        </li>
                        <li class="liCodIdentificacao">
                            <label for="txtCodIdenticacaoNH" title="Código de Identificação">
                                Cód. Identificação</label>
                            <asp:TextBox ID="txtCodIdenticacaoNH" Enabled="false" ClientIDMode="Static" ToolTip="Informe o Código de Identificação"
                                CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-left: 10px;">
                            <label for="txtCNPJNH" title="CNPJ">
                                Nº CNPJ</label>
                            <asp:TextBox ID="txtCNPJNH" Enabled="false" ClientIDMode="Static" ToolTip="Informe o CNPJ"
                                CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="width: 100%; text-align: center;"><span style="color: #6495ED;
                            font-size: 1.3em; font-weight: bold;">Nossa história?</span></li>
                        <li class="liClear" style="margin-top: 2px; margin-left: 322px;">
                            <label for="txtTipoCtrlNossaHisto" style="float: left;" title="Tipo de Controle do Nossa História">
                                Por</label>
                            <asp:TextBox ID="txtTipoCtrlNossaHisto" Enabled="false" Style="margin-left: 7px;
                                margin-bottom: 5px; padding-left: 3px;" ToolTip="Tipo de Controle do Nossa História"
                                BackColor="#FFFFE1" CssClass="txtTipoCtrlFrequ" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-bottom: 5px;"><span style="color: #87CEFA; font-size: 1.1em;">
                            Utilize o editor de texto abaixo para descrever sobre a história da Unidade Escolar,
                            desde sua criação contemplando fases e fatos importantes a repeito da mesma, dentre
                            outras informações...</span></li>
                        <li>
                            <dx:ASPxHtmlEditor ID="txtNossaHisto" Height="300px" Width="745px" runat="server"
                                Theme="Office2010Blue" ClientInstanceName="txtNossaHisto">
                                <Settings AllowHtmlView="False" AllowPreview="False" />
                                <SettingsResize MinHeight="300" MinWidth="745" />
                            </dx:ASPxHtmlEditor>
                        </li>
                    </ul>
                </div>
                <div id="tabPropoPedag" class="tabPropoPedag" style="display: none;">
                    <ul id="ul6" class="ulDados">
                        <li class="liClear" style="margin-left: 130px;">
                            <label for="txtUnidadeEscolarPP" title="Razão Social">
                                Unidade Escolar</label>
                            <asp:TextBox ID="txtUnidadeEscolarPP" Enabled="false" ClientIDMode="Static" ToolTip="Informe a Razão Social"
                                CssClass="txtRazaoSocialIUE" MaxLength="80" runat="server"></asp:TextBox>
                        </li>
                        <li class="liCodIdentificacao">
                            <label for="txtCodIdenticacaoNH" title="Código de Identificação">
                                Cód. Identificação</label>
                            <asp:TextBox ID="txtCodIdenticacaoPP" Enabled="false" ClientIDMode="Static" ToolTip="Informe o Código de Identificação"
                                CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-left: 10px;">
                            <label for="txtCNPJNH" title="CNPJ">
                                Nº CNPJ</label>
                            <asp:TextBox ID="txtCNPJPP" Enabled="false" ClientIDMode="Static" ToolTip="Informe o CNPJ"
                                CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="width: 100%; text-align: center;"><span style="color: #6495ED;
                            font-size: 1.3em; font-weight: bold;">Proposta pedagógica?</span></li>
                        <li class="liClear" style="margin-top: 2px; margin-left: 322px;">
                            <label for="txtTipoCtrlPropoPedag" style="float: left;" title="Tipo de Controle do Proposta Pedagógica">
                                Por</label>
                            <asp:TextBox ID="txtTipoCtrlPropoPedag" Enabled="false" Style="margin-left: 7px;
                                margin-bottom: 5px; padding-left: 3px;" ToolTip="Tipo de Controle do Proposta Pedagógica"
                                BackColor="#FFFFE1" CssClass="txtTipoCtrlFrequ" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-bottom: 18px; text-align: center; margin-left: 30px;">
                            <span style="color: #87CEFA; font-size: 1.1em;">Utilize o editor de texto abaixo para
                                descrever sobre a Proposta Pedagógica da Instituição de Ensino e/ou Unidade Escolar.</span></li>
                        <li>
                            <dx:ASPxHtmlEditor ID="txtPropoPedag" Height="300px" Width="745px" runat="server"
                                Theme="Office2010Blue" ClientInstanceName="txtPropoPedag">
                                <Settings AllowHtmlView="False" AllowPreview="False" />
                                <SettingsResize MinHeight="300" MinWidth="745" />
                            </dx:ASPxHtmlEditor>
                        </li>
                    </ul>
                </div>
                <div id="tabControleSaude2" class="tabControleSaude2" style="display: none;">
                    <ul class="ulDados">
                        <li class="liClear" style="margin-left: 130px;">
                            <label for="txtUnidadeEscolarGU" title="Razão Social">
                                Unidade</label>
                            <asp:TextBox ID="TextBox2" Enabled="false" ClientIDMode="Static" ToolTip="Informe a Razão Social"
                                CssClass="txtRazaoSocialIUE" MaxLength="80" runat="server"></asp:TextBox>
                        </li>
                        <li class="liCodIdentificacao">
                            <label for="txtCodIdenticacaoGU" title="Código de Identificação">
                                Cód. Identificação</label>
                            <asp:TextBox ID="TextBox3" Enabled="false" ClientIDMode="Static" ToolTip="Informe o Código de Identificação"
                                CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-left: 10px;">
                            <label for="txtCNPJGU" title="CNPJ">
                                Nº CNPJ</label>
                            <asp:TextBox ID="TextBox4" Enabled="false" ClientIDMode="Static" ToolTip="Informe o CNPJ"
                                CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="width: 100%; text-align: center;"><span style="color: #6495ED;
                            font-size: 1.3em; font-weight: bold;">Controle de Saúde</span></li>
                        Controles de Ações de Recepção
                        <li class="liClear" style="display: inline-block">
                            <ul>
                                <li>
                                    <ul style="display: grid;">
                                        <li>
                                            <label>
                                                <strong>Recepção</strong></label>
                                            <label style="display: inline-block" title="Tempo em dias para validar o retorno no fluxo de atendimento">
                                                Tempo de Validade Retorno:</label>
                                            <asp:TextBox runat="server" ID="txtValidRetorno" CssClass="txtValidRetorno" MaxLength="3"
                                                Width="20px" ToolTip="Insira a quantidade de dias para validar o tempo de retorno nos fluxos de atendimento"></asp:TextBox>
                                            <asp:RadioButtonList ID="RadioButtonListAtendimento" ClientIDMode="Static" CssClass="rblInforControle"
                                                runat="server" RepeatDirection="Vertical">
                                                <asp:ListItem Text="Avaliação de Risco (Triagem) e Atendimento" Value="S"></asp:ListItem>
                                                <asp:ListItem Text="Atendimento" Value="N"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </li>
                                        <li style="display: grid" class="liChk">
                                            <asp:CheckBox Text="Agendamento de Paciente" ToolTip="Marque se no painel de recepção haverá o componente de Agendamento de Pacientes"
                                                class="chk" ID="CheckBoxAgendamento" runat="server" />
                                            <asp:CheckBox Text="Encaixe de Paciente em Agenda" ToolTip="Marque se no painel de recepção haverá o componente de Encaixe de Paciente em horários livres ou agendados"
                                                class="chk" ID="CheckBoxEncaixe" runat="server" />
                                            <asp:CheckBox Text="Movimentação de Paciente Agendado" ToolTip="Marque se no painel de recepção haverá o componente de Movimentação de agenda de Pacientes já agendados"
                                                class="chk" ID="CheckBoxMovimentacao" runat="server" />
                                            <asp:CheckBox Text="Emissão de Guia (Plano/Convênio)" ToolTip="Marque se no painel de recepção haverá o componente de emissãoi da Guia de Atendimento Plano/Convênio"
                                                class="chk" ID="CheckBoxGuia" runat="server" />
                                            <asp:CheckBox Text="Emissão Ficha de Atendimento Paciente" ToolTip="Marque se no painel de recepção haverá o componente de emissão da Ficha de atendimento do Paciente"
                                                class="chk" ID="CheckBoxFicha" runat="server" />
                                            <asp:CheckBox Text="Recebimento Partcelas de Contrato" ToolTip="Marque se no painel de recepção haverá o componente de Recebimento Financeiro Parcelas de Contrato do Paciente"
                                                class="chk" ID="CheckBoxRecContrato" runat="server" />
                                            <asp:CheckBox Text="Recebimento Simples do Atendimento" ToolTip="Marque se no painel de recepção haverá o componente de Recebimento Financeiro Simples do Atendimento ao Paciente"
                                                class="chk" ID="CheckBoxRecSimples" runat="server" />
                                            <asp:CheckBox Text="Recebimento no Formato de Caixa" ToolTip="Marque se no painel de recepção haverá o componente de Recebimento Financeiro no Formato de Caixa Financeiro"
                                                class="chk" ID="CheckBoxRecCaixa" runat="server" />
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <ul style="display: grid">
                                        <li style="display: grid" class="liChk">
                                            <label>
                                                <strong>Atendimento</strong></label>
                                            <br />
                                            <asp:CheckBox Text="Emissão de Prontuário Padrão" ToolTip="Emissão do Prontuário do Paciente no Modelo Convencional"
                                                class="chk" ID="CheckBoxProntPadrao" runat="server" />
                                            <asp:CheckBox Text="Emissão de Prontuário Modular" ToolTip="Emissão do Prontuário do Paciente no Modelo Modular"
                                                class="chk" ID="CheckBoxProntModular" runat="server" />
                                            <asp:CheckBox Text="Prescrição de Medicamento" ToolTip="Prescrever medicamentos ao Paciente"
                                                class="chk" ID="CheckBoxPresqMedicamentos" runat="server" />
                                            <asp:CheckBox Text="Prescrição de Exames" ToolTip="Prescrever exames ao Paciente"
                                                class="chk" ID="CheckBoxPresqExames" runat="server" />
                                            <asp:CheckBox Text="Prescrição  Ambulatórial" ToolTip="Prescrever serviços ambulatoriais ao Paciente"
                                                class="chk" ID="CheckBoxPresqAmbu" runat="server" />
                                            <asp:CheckBox Text="Emissão de Guia TISS" ToolTip="Emitir a Guia TISS do Plano/Convênio do Paciente"
                                                class="chk" ID="CheckBoxEmitirGuia" runat="server" />
                                            <asp:CheckBox Text="Emissão de Fixa de Atendimento" ToolTip="Emitir a Ficha de Atendimento do Paciente"
                                                class="chk" ID="CheckBoxFichaAtend" runat="server" />
                                            <asp:CheckBox Text="Emissão de Atestado Médico" ToolTip="Emitir Atestado Médico e/ou Comparecimento"
                                                class="chk" ID="CheckBoxEmitirAtestado" runat="server" />
                                            <asp:CheckBox Text="Anexar arquivos ao Prontuário do Paciente" ToolTip="Anexar arquivos (Exames; Laudos; Imagens; etc.) ao Prontuário do Paciente"
                                                class="chk" ID="CheckBoxAnexarArq" runat="server" />
                                            <asp:CheckBox Text="Registrar observações" ToolTip="Registrar observações sobre o atendimento ao Paciente"
                                                class="chk" ID="CheckBoxObserv" runat="server" />
                                            <asp:CheckBox Text="Fazer Encaminhamento" ToolTip="Fazer o encaminhamento do Paciente a Profissional Especializado"
                                                class="chk" ID="CheckBoFazerxEncaminha" runat="server" />
                                            <asp:CheckBox Text="Registro de Internação" ToolTip="Fazer a solicitação de Registro de Internação para o Paciente"
                                                class="chk" ID="CheckBoxRegInternacao" runat="server" />
                                            <asp:CheckBox Text="Elaborar Laudo Técnico" ToolTip="Elaborar Laudo Técnico para o Paciente"
                                                class="chk" ID="CheckBoxEmissaoAtestado" runat="server" />
                                            <asp:CheckBox Text="Solicitação de Cirurgia" ToolTip="Fazer a solicitação de Cirurgia do Pacinte com a emissão da GUIA"
                                                class="chk" ID="CheckBoxSolicCirurgia" runat="server" />
                                            <asp:CheckBox Text="Salvar o Atendimento" ToolTip="Salvar o atendimento do Paciente"
                                                class="chk" ID="CheckBoxSalvarAtend" runat="server" />
                                            <asp:CheckBox Text="Atendimento em Espera" ToolTip="Manter o atendimento do Paciente em Espera"
                                                class="chk" ID="CheckBoxManterEspera" runat="server" />
                                            <asp:CheckBox Text="Finalizar Atendimento" ToolTip="Finalizar (Encerrar) o atendimento ao Paciente"
                                                class="chk" ID="CheckBoxFinalizarAtend" runat="server" />
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <ul>
                                        <li>
                                            <label>
                                                <strong>Agendamento</strong></label>
                                            <div style="margin-top: 10px;">
                                                <span>Histórico Agenda: </span>
                                                <div class="qtdeDias">
                                                    QDA:
                                                    <asp:TextBox runat="server" ID="txtQtdeDiasAnterior" MaxLength="3"></asp:TextBox>
                                                    QDP:
                                                    <asp:TextBox runat="server" ID="txtQtdeDiasPosterior" MaxLength="3"></asp:TextBox>
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <div style="margin-right: 3px; right; margin-top: 10px; float: right;">
                                        *QDA (Quantidade de Dias Anterior a data atual (Sistema para a Pesquisa) - Máximo
                                        120 dias
                                    </div>
                                    <div style="margin-top: 3px; float: right;">
                                        *QDP (Quantidade de Dias Posterior a data atual (Sistema para a Pesquisa) - Máximo
                                        120 dias</div>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div id="tabControleSaude" class="tabControleSaude" style="display: none;">
                    <ul class="ulDados">
                        <li class="liClear" style="margin-left: 130px;">
                            <label for="txtUnidadeEscolarGU" title="Razão Social">
                                Unidade</label>
                            <asp:TextBox ID="txtUnidadeCS" Enabled="false" ClientIDMode="Static" ToolTip="Informe a Razão Social"
                                CssClass="txtRazaoSocialIUE" MaxLength="80" runat="server"></asp:TextBox>
                        </li>
                        <li class="liCodIdentificacao">
                            <label for="txtCodIdenticacaoGU" title="Código de Identificação">
                                Cód. Identificação</label>
                            <asp:TextBox ID="txtCodIdenticacaoCS" Enabled="false" ClientIDMode="Static" ToolTip="Informe o Código de Identificação"
                                CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-left: 10px;">
                            <label for="txtCNPJGU" title="CNPJ">
                                Nº CNPJ</label>
                            <asp:TextBox ID="txtCNPJCS" Enabled="false" ClientIDMode="Static" ToolTip="Informe o CNPJ"
                                CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="width: 100%; text-align: center;"><span style="color: #6495ED;
                            font-size: 1.3em; font-weight: bold;">Controle Classificação Funcional</span></li>
                        <li class="liClear">
                            <ul>
                                <li>
                                    <ul>
                                        <li class="liClear" style="margin-top: 10px; width: 850px;">
                                            <ul>
                                                <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                                    font-weight: bold;">Classificação Funcional para Agendamento de Atendimento</span>
                                                </li>
                                                <li style="clear: both; margin-bottom: 10px"></li>
                                                <li style="clear: both">
                                                    <asp:CheckBox runat="server" ID="chkPermMedicAgend" CssClass="rdb" Text="Médico" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermOdontAgend" CssClass="rdb" Text="Odontólogo" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermEstetAgend" CssClass="rdb" Text="Esteticista" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermEnferAgend" CssClass="rdb" Text="Enfermeiro" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermPsicoAgend" CssClass="rdb" Text="Psicólogo" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermFisioAgend" CssClass="rdb" Text="Fisioterapeuta" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermFonoaAgend" CssClass="rdb" Text="Fonoaudiólogo" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermTeOcuAgend" CssClass="rdb" Text="Terap. Ocupacional" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermNutriAgend" CssClass="rdb" Text="Nutricionista" />
                                                </li>
                                                <li style="clear: both">
                                                    <asp:CheckBox runat="server" ID="chkPermMusicAgend" CssClass="rdb" Text="Musicista" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermServMovAgend" CssClass="rdb" Text="Vacina" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermOutroAgend" CssClass="rdb" Text="Triagem" />
                                                </li>
                                            </ul>
                                        </li>
                                        <li class="liClear" style="margin-top: 40px; width: 850px;">
                                            <ul>
                                                <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                                    font-weight: bold;">Classificação Funcional para Direcionamento/Atendimento</span>
                                                </li>
                                                <li style="clear: both; margin-bottom: 10px"></li>
                                                <li style="clear: both">
                                                    <asp:CheckBox runat="server" ID="chkPermMedicDirec" CssClass="rdb" Text="Médico" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermOdontDirec" CssClass="rdb" Text="Odontólogo" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermEstetDirec" CssClass="rdb" Text="Esteticista" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermEnfermDirec" CssClass="rdb" Text="Enfermeiro" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermPsicoDirec" CssClass="rdb" Text="Psicólogo" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermFisioDirec" CssClass="rdb" Text="Fisioterapeuta" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermFonoaDirec" CssClass="rdb" Text="Fonoaudiólogo" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermTeOCuDirec" CssClass="rdb" Text="Terap. Ocupacional" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermNutriDirec" CssClass="rdb" Text="Nutricionista" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermOutroDirec" CssClass="rdb" Text="Outra" />
                                                </li>
                                            </ul>
                                        </li>
                                        <li class="liClear" style="margin-top: 40px; width: 850px;">
                                            <ul>
                                                <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                                    font-weight: bold;">Classificação Funcional para Acolhimento/Triagem</span>
                                                </li>
                                                <li style="clear: both; margin-bottom: 10px"></li>
                                                <li style="clear: both">
                                                    <asp:CheckBox runat="server" ID="chkPermMedicAcolh" CssClass="rdb" Text="Médico" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermOdontAcolh" CssClass="rdb" Text="Odontólogo" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermEstetAcolh" CssClass="rdb" Text="Esteticista" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermEnfermAcolh" CssClass="rdb" Text="Enfermeiro" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermPsicoAcolh" CssClass="rdb" Text="Psicólogo" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermFisioAcolh" CssClass="rdb" Text="Fisioterapeuta" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermFonoaAcolh" CssClass="rdb" Text="Fonoaudiólogo" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermTeOcuAcolh" CssClass="rdb" Text="Terap. Ocupacional" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermNutriAcolh" CssClass="rdb" Text="Nutricionista" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermOutroAcolh" CssClass="rdb" Text="Triagem" />
                                                </li>
                                            </ul>
                                        </li>
                                        <li class="liClear" style="margin-top: 40px; width: 850px;">
                                            <ul>
                                                <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                                    font-weight: bold;">Classificação Funcional para Atendimento à Pacientes</span>
                                                </li>
                                                <li style="clear: both; margin-bottom: 10px"></li>
                                                <li style="clear: both">
                                                    <asp:CheckBox runat="server" ID="chkPermMedicAtend" CssClass="rdb" Text="Médico" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermOdontAtend" CssClass="rdb" Text="Odontólogo" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermEstetAtend" CssClass="rdb" Text="Esteticista" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermEnfermAtend" CssClass="rdb" Text="Enfermeiro" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermPsicoAtend" CssClass="rdb" Text="Psicólogo" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermFisioAtend" CssClass="rdb" Text="Fisioterapeuta" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermFonoaAtend" CssClass="rdb" Text="Fonoaudiólogo" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermTeOcuAtend" CssClass="rdb" Text="Terap. Ocupacional" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermNutriAtend" CssClass="rdb" Text="Nutricionista" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkPermOutroAtend" CssClass="rdb" Text="Outra" />
                                                </li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div id="tabFrequFunci" class="tabFrequFunci" style="display: none;">
                    <ul id="ul9" class="ulDados">
                        <li class="liClear" style="margin-left: 130px;">
                            <label for="txtUnidadeEscolarFF" title="Razão Social">
                                Unidade Escolar</label>
                            <asp:TextBox ID="txtUnidadeEscolarFF" Enabled="false" ClientIDMode="Static" ToolTip="Informe a Razão Social"
                                CssClass="txtRazaoSocialIUE" MaxLength="80" runat="server"></asp:TextBox>
                        </li>
                        <li class="liCodIdentificacao">
                            <label for="txtCodIdenticacaoFF" title="Código de Identificação">
                                Cód. Identificação</label>
                            <asp:TextBox ID="txtCodIdenticacaoFF" Enabled="false" ClientIDMode="Static" ToolTip="Informe o Código de Identificação"
                                CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-left: 10px;">
                            <label for="txtCNPJFF" title="CNPJ">
                                Nº CNPJ</label>
                            <asp:TextBox ID="txtCNPJFF" Enabled="false" ClientIDMode="Static" ToolTip="Informe o CNPJ"
                                CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="width: 100%; text-align: center;"><span style="color: #3366FF;
                            font-size: 1.3em; font-weight: bold;">Controle de Frequência Funcional</span></li>
                        <li class="liClear" style="margin-top: 2px; margin-left: 325px;">
                            <label for="txtTipoCtrlFrequ" style="float: left;" title="Tipo de Controle de Frequência">
                                Por</label>
                            <asp:TextBox ID="txtTipoCtrlFrequ" Enabled="false" Style="margin-left: 7px; padding-left: 3px;"
                                ToolTip="Tipo de Controle de Frequência" BackColor="#FFFFE1" CssClass="txtTipoCtrlFrequ"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-top: 2px; margin-left: 170px;">
                            <label style="color: #BEBEBE">
                                Os Horários são cadastrados em funcionalidade específica - Cadastramento de Horário
                                Funcional</label>
                        </li>
                        <li style="">
                            <ul>
                                <li class="liClear" style="margin-top: 1px; width: 100%; text-align: center; background-color: #F9F9FF;
                                    padding: 2px 0;"><span style="font-size: 1.3em; text-transform: uppercase; font-weight: bold;">
                                        Quadro de Horários Funcionais</span></li>
                                <li class="liClear">
                                    <asp:GridView runat="server" ID="grdHorarios" CssClass="grdHorarios" AutoGenerateColumns="False"
                                        DataKeyNames="ID_QUADRO_HORAR_FUNCI" AllowPaging="True" EnableModelValidation="True"
                                        PageSize="20">
                                        <Columns>
                                            <asp:BoundField DataField="CO_SIGLA_TIPO_PONTO" HeaderText="Sigla" HeaderStyle-Width="75px" />
                                            <asp:BoundField DataField="LIMIT_ENTRA" HeaderText="Entrada - Limite" ItemStyle-BackColor="#FFFFB7"
                                                ItemStyle-ForeColor="#FF0000" />
                                            <asp:BoundField DataField="ENTRA_TURNO1" HeaderText="1º Turno - Entrada" />
                                            <asp:BoundField DataField="SAIDA_TURNO1" HeaderText="1º Turno - Saída" />
                                            <asp:BoundField DataField="ENTRA_INTER" HeaderText="Intervalo - Entrada" />
                                            <asp:BoundField DataField="SAIDA_INTER" HeaderText="Intervalo - Saída" />
                                            <asp:BoundField DataField="ENTRA_TURNO2" HeaderText="2º Turno - Entrada" />
                                            <asp:BoundField DataField="SAIDA_TURNO2" HeaderText="2º Turno - Saída" />
                                            <asp:BoundField DataField="LIMIT_SAIDA" HeaderText="Saída - Limite" ItemStyle-BackColor="#FFFFB7"
                                                ItemStyle-ForeColor="#FF0000" />
                                            <asp:BoundField DataField="ENTRA_EXTRA" HeaderText="Extra - Entrada" />
                                            <asp:BoundField DataField="SAIDA_EXTRA" HeaderText="Extra - Saída" />
                                            <asp:BoundField DataField="LIMIT_SAIDA_EXTRA" HeaderText="Extra - Limite Saída" ItemStyle-BackColor="#FFFFB7"
                                                ItemStyle-ForeColor="#FF0000" />
                                        </Columns>
                                        <RowStyle CssClass="rowStyle" />
                                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                        <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                                        <EmptyDataTemplate>
                                            Nenhum registro encontrado.<br />
                                        </EmptyDataTemplate>
                                        <PagerStyle CssClass="grdFooter" />
                                    </asp:GridView>
                                </li>
                                <li class="liClear" style="margin-top: 10px;">
                                    <label for="ddlUFIUE" title="Controle de Hora Extra" style="float: left;">
                                        Controle de Hora Extra</label>
                                    <asp:TextBox ID="txtCtrlHoraExtra" Enabled="false" Style="margin-left: 5px; padding-left: 3px;"
                                        ToolTip="Controle de Hora Extra" Text="Com Controle" CssClass="ddlCtrlHoraExtra"
                                        runat="server"></asp:TextBox>
                                </li>
                                <li style="margin-top: 10px; margin-left: 10px;">
                                    <label for="txtCtrlHoraExtra" title="Permite Multifrequência?" style="float: left;">
                                        Permite Multifrequência?</label>
                                    <asp:DropDownList ID="ddlPermiMultiFrequ" ToolTip="Permite Multifrequência?" Style="margin-left: 5px;"
                                        CssClass="ddlPermiMultiFrequ" runat="server">
                                        <asp:ListItem Value="S" Selected="true">Sim</asp:ListItem>
                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div id="tabPedagMatric" class="tabPedagMatric" style="display: none; width: 830px;">
                    <ul id="ul8" class="ulDados">
                        <li class="liClear" style="margin-left: 130px;">
                            <label for="txtUnidadeEscolarPM" title="Razão Social">
                                Unidade Escolar</label>
                            <asp:TextBox ID="txtUnidadeEscolarPM" Enabled="false" ClientIDMode="Static" ToolTip="Informe a Razão Social"
                                CssClass="txtRazaoSocialIUE" MaxLength="80" runat="server"></asp:TextBox>
                        </li>
                        <li class="liCodIdentificacao">
                            <label for="txtCodIdenticacaoPM" title="Código de Identificação">
                                Cód. Identificação</label>
                            <asp:TextBox ID="txtCodIdenticacaoPM" Enabled="false" ClientIDMode="Static" ToolTip="Informe o Código de Identificação"
                                CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-left: 10px;">
                            <label for="txtCNPJQS" title="CNPJ">
                                Nº CNPJ</label>
                            <asp:TextBox ID="txtCNPJPM" Enabled="false" ClientIDMode="Static" ToolTip="Informe o CNPJ"
                                CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-bottom: 10px; width: 100%; text-align: center;"><span
                            style="color: #6495ED; font-size: 1.3em; font-weight: bold;">Controle Pedagógico
                            / Matrículas</span></li>
                        <li>
                            <ul>
                                <li>
                                    <ul>
                                        <li class="liPedagMatric"><span style="font-size: 1.0em; text-transform: uppercase;
                                            font-weight: bold;">Parâmetros de Metodologia</span></li>
                                        <li class="liControleMetodologia">
                                            <label title="Controle de Metodologia">
                                                Controle de Metodologia</label>
                                            <asp:TextBox ID="txtContrMetod" Enabled="false" ClientIDMode="Static" ToolTip="Controle de Metodologia"
                                                BackColor="#FFFFE1" Style="margin-bottom: 0; padding-left: 3px;" CssClass="txtContrMetod"
                                                runat="server"></asp:TextBox>
                                        </li>
                                        <li class="liMetodEnsino">
                                            <label title="Metodologia de Ensino">
                                                Metodologia de Ensino</label>
                                            <asp:DropDownList ID="ddlMetodEnsino" Style="margin-left: 9px;" ToolTip="Informe a Metodologia de Ensino<"
                                                CssClass="ddlMetodEnsino" runat="server">
                                                <asp:ListItem Value="S">Seriada</asp:ListItem>
                                                <asp:ListItem Value="P">Progressiva</asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <li class="liMetodEnsino">
                                                    <label title="Formato de Avaliação">
                                                        Formato de Avaliação</label>
                                                    <asp:DropDownList ID="ddlFormaAvali" Style="margin-left: 13px;" ToolTip="Informe a Forma de Avaliação"
                                                        CssClass="ddlFormaAvali" runat="server" OnSelectedIndexChanged="ddlFormaAvali_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                        <asp:ListItem Value="N">Nota</asp:ListItem>
                                                        <asp:ListItem Value="C">Conceito</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li class="liMetodEnsino" style="margin-bottom: -10px">
                                                    <label>
                                                        Nome da Avaliação de Aluno</label>
                                                    <asp:TextBox runat="server" ID="txtNomAvalAluno" Width="215px" MaxLength="120" ToolTip="Informe o texto que aparecerá como sugestão para Título do Boletim"></asp:TextBox>
                                                </li>
                                                <li class="liEquivNotaConce"><span style="font-size: 1.0em; text-transform: uppercase;
                                                    font-weight: bold;">Equivalência de Nota/Conceito</span></li>
                                                <li style="background-color: #FFFFCC; width: 220px; clear: both;"><span style="font-size: 1.0em;
                                                    margin-left: 3px;">Conceito (Nome e Sigla)</span><span style="font-size: 1.0em; margin-left: 22px;">Intervalo
                                                        de Notas</span></li>
                                                <li class="liNotaConceito" style="margin-top: 4px;">
                                                    <asp:TextBox ID="txtDescrConce1" ToolTip="Descrição do Conceito" CssClass="txtDescrConce"
                                                        runat="server" Enabled="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtSiglaConce1" ToolTip="Sigla do Conceito" CssClass="txtSiglaConce"
                                                        runat="server" Enabled="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtNotaIni1" ToolTip="Nota Inicial" CssClass="txtNotaIni" Style="margin-left: 7px;"
                                                        runat="server" Enabled="false"></asp:TextBox>
                                                    <span style="font-size: 1.0em;">a</span>
                                                    <asp:TextBox ID="txtNotaFim1" ToolTip="Nota Final" CssClass="txtNotaFim" runat="server"
                                                        Enabled="false"></asp:TextBox>
                                                </li>
                                                <li class="liNotaConceito">
                                                    <asp:TextBox ID="txtDescrConce2" ToolTip="Descrição do Conceito" CssClass="txtDescrConce"
                                                        runat="server" Enabled="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtSiglaConce2" ToolTip="Sigla do Conceito" CssClass="txtSiglaConce"
                                                        runat="server" Enabled="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtNotaIni2" ToolTip="Nota Inicial" CssClass="txtNotaIni" Style="margin-left: 7px;"
                                                        runat="server" Enabled="false"></asp:TextBox>
                                                    <span style="font-size: 1.0em;">a</span>
                                                    <asp:TextBox ID="txtNotaFim2" ToolTip="Nota Final" CssClass="txtNotaFim" runat="server"
                                                        Enabled="false"></asp:TextBox>
                                                </li>
                                                <li class="liNotaConceito">
                                                    <asp:TextBox ID="txtDescrConce3" ToolTip="Descrição do Conceito" CssClass="txtDescrConce"
                                                        runat="server" Enabled="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtSiglaConce3" ToolTip="Sigla do Conceito" CssClass="txtSiglaConce"
                                                        runat="server" Enabled="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtNotaIni3" ToolTip="Nota Inicial" CssClass="txtNotaIni" Style="margin-left: 7px;"
                                                        runat="server" Enabled="false"></asp:TextBox>
                                                    <span style="font-size: 1.0em;">a</span>
                                                    <asp:TextBox ID="txtNotaFim3" ToolTip="Nota Final" CssClass="txtNotaFim" runat="server"
                                                        Enabled="false"></asp:TextBox>
                                                </li>
                                                <li class="liNotaConceito">
                                                    <asp:TextBox ID="txtDescrConce4" ToolTip="Descrição do Conceito" CssClass="txtDescrConce"
                                                        runat="server" Enabled="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtSiglaConce4" ToolTip="Sigla do Conceito" CssClass="txtSiglaConce"
                                                        runat="server" Enabled="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtNotaIni4" ToolTip="Nota Inicial" CssClass="txtNotaIni" Style="margin-left: 7px;"
                                                        runat="server" Enabled="false"></asp:TextBox>
                                                    <span style="font-size: 1.0em;">a</span>
                                                    <asp:TextBox ID="txtNotaFim4" ToolTip="Nota Final" CssClass="txtNotaFim" runat="server"
                                                        Enabled="false"></asp:TextBox>
                                                </li>
                                                <li class="liNotaConceito">
                                                    <asp:TextBox ID="txtDescrConce5" ToolTip="Descrição do Conceito" CssClass="txtDescrConce"
                                                        runat="server" Enabled="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtSiglaConce5" ToolTip="Sigla do Conceito" CssClass="txtSiglaConce"
                                                        runat="server" Enabled="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtNotaIni5" ToolTip="Nota Inicial" CssClass="txtNotaIni" Style="margin-left: 7px;"
                                                        runat="server" Enabled="false"></asp:TextBox>
                                                    <span style="font-size: 1.0em;">a</span>
                                                    <asp:TextBox ID="txtNotaFim5" ToolTip="Nota Final" CssClass="txtNotaFim" runat="server"
                                                        Enabled="false"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <li style="width: 220px; margin-left: 5px; margin-top: 10px; clear: both;">
                                            <ul>
                                                <li class="liPedagMatric" style="text-align: center; width: 220px; margin-right: 0px;">
                                                    <span style="font-size: 1.0em; text-transform: uppercase; font-weight: bold;">Controle
                                                        Operacional</span></li>
                                                <li class="liContrAval">
                                                    <label for="txtTipoContrOpera" title="Controle por">
                                                        Controle por</label>
                                                    <asp:TextBox ID="txtTipoContrOpera" Enabled="false" ClientIDMode="Static" ToolTip="Controle Operacional"
                                                        Style="float: right; margin-left: 10px;" BackColor="#FFFFE1" CssClass="txtContrAval"
                                                        runat="server"></asp:TextBox>
                                                </li>
                                                <li class="liMetodEnsino" style="width: 220px; margin-top: 5px; margin-right: 0px;">
                                                    <label title="Gerar N° de Rede (NIRE) Automático">
                                                        Gerar N° de Rede (NIRE) Autom&aacute;tico</label>
                                                    <asp:DropDownList ID="ddlGerarNireIIE" Style="margin-right: 5px; float: right;" ToolTip="Informe se o NIRE será gerado automaticamente"
                                                        CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                        <asp:ListItem Value="N">N&atilde;o</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li class="liMetodEnsino" style="width: 220px; margin-right: 0px;">
                                                    <label title="Gerar N° de Matrícula Automático">
                                                        Gerar N° de Matr&iacute;cula Autom&aacute;tico</label>
                                                    <asp:DropDownList ID="ddlGerarMatriculaIIE" Style="margin-right: 5px; float: right;"
                                                        ToolTip="Informe se o N° de Matrícula de Aluno será gerado automaticamente" CssClass="ddlAlterPrazoEntreSolic"
                                                        runat="server">
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                        <asp:ListItem Value="N">N&atilde;o</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li class="liMetodEnsino" style="width: 220px; margin-right: 0px;">
                                                    <label title="Agrupador de receita (CAR)">
                                                        Agrupador de receita (CAR)</label>
                                                    <asp:DropDownList ID="ddlAgrupCAR" Style="margin-right: 5px; float: right;" ToolTip="Informe se o Agrupador de receita (CAR)"
                                                        CssClass="ddlAgrupReceita" runat="server">
                                                    </asp:DropDownList>
                                                </li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li style="padding-left: 10px; padding-right: 5px; border-left: 1px solid #CCCCCC;
                                    border-right: 1px solid #CCCCCC;">
                                    <ul>
                                        <li class="liPedagMatric" style="width: 190px;"><span style="font-size: 1.0em; text-transform: uppercase;
                                            font-weight: bold;">Parâmetros de Avaliação</span></li>
                                        <li class="liContrAval">
                                            <label for="txtContrAval" title="Controle por">
                                                Controle por</label>
                                            <asp:TextBox ID="txtContrAval" Enabled="false" ClientIDMode="Static" ToolTip="Controle de Avaliação"
                                                Style="margin-left: 5px; margin-bottom: 0; padding-left: 3px;" BackColor="#FFFFE1"
                                                CssClass="txtContrAval" runat="server"></asp:TextBox>
                                        </li>
                                        <li class="liPerioAval">
                                            <label for="ddlPerioAval" title="Periodicidade da Avaliação">
                                                Periodicidade</label>
                                            <asp:DropDownList ID="ddlPerioAval" ToolTip="Informe a Periodicidade da Avaliação"
                                                Style="margin-left: 3px;" CssClass="ddlPerioAval" runat="server">
                                                <asp:ListItem Value="M">Mensal</asp:ListItem>
                                                <asp:ListItem Value="B">Bimestral</asp:ListItem>
                                                <asp:ListItem Value="T">Trimestral</asp:ListItem>
                                                <asp:ListItem Value="S">Semestral</asp:ListItem>
                                                <asp:ListItem Value="A">Anual</asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <li class="liEquivNotaConce" style="width: 190px;"><span style="font-size: 1.0em;
                                                    text-transform: uppercase; font-weight: bold;">Controle de Médias</span></li>
                                                <li style="background-color: #FFFFCC; clear: both; width: 190px;"><span style="font-size: 1.0em;
                                                    margin-left: 3px;">Descrição</span><span style="font-size: 1.0em; margin-left: 107px;">Média</span></li>
                                                <li class="liAprovGeral" style="margin-top: 5px;">
                                                    <label title="Média de Aprovação Geral">
                                                        Aprovação Geral</label>
                                                    <asp:TextBox ID="txtMediaAprovGeral" ToolTip="Informe a Média de Aprovação Geral"
                                                        Enabled="false" CssClass="campoMoeda" Style="margin-left: 73px; margin-bottom: 2px;"
                                                        runat="server"></asp:TextBox>
                                                </li>
                                                <li class="liAprovDireta">
                                                    <label title="Média para Aprovação Direta">
                                                        Aprovação Direta</label>
                                                    <asp:TextBox ID="txtMediaAprovDireta" ToolTip="Informe a Média para Aprovação Direta"
                                                        Enabled="false" CssClass="campoMoeda" Style="margin-left: 70px; margin-bottom: 2px;"
                                                        runat="server"></asp:TextBox>
                                                </li>
                                                <li class="liAprovDireta">
                                                    <label title="Média Prova Final">
                                                        Prova Final</label>
                                                    <asp:TextBox ID="txtMediaProvaFinal" ToolTip="Informe a Média Prova Final" Enabled="false"
                                                        CssClass="campoMoeda" Style="margin-left: 98px;" runat="server"></asp:TextBox>
                                                </li>
                                                <li class="liEquivNotaConce" style="width: 190px; margin-top: 0px; margin-bottom: 3px;">
                                                    <span style="font-size: 1.0em; text-transform: uppercase; font-weight: bold;">Controle
                                                        de Aprovação</span></li>
                                                <li class="liEquivNotaConce" style="width: 190px; margin-top: 0px; margin-bottom: 3px;">
                                                    <span style="font-size: 1.0em; text-transform: uppercase; font-weight: bold;">Seleção
                                                        de Boletins</span></li>
                                                <li class="liLimiteMedia liClear">
                                                    <div id="divGrid" runat="server" class="divGrid" style="height: 50px; width: 180px;
                                                        overflow: auto;">
                                                        <asp:GridView ID="grdBoletim" CssClass="grdBusca" Width="160px" runat="server" AutoGenerateColumns="False">
                                                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                                            <HeaderStyle CssClass="hStyle" />
                                                            <EmptyDataTemplate>
                                                                Nenhum boletim encontrado.
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemStyle Width="20px" />
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdCoBol" runat="server" Value='<%# bind("CO_BOL") %>' />
                                                                        <asp:CheckBox ID="ckSelect" Checked='<%# bind("CHK_SEL") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="NO_BOL">
                                                                    <ItemStyle Width="140px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ul>
                                </li>
                                <li>
                                    <asp:CheckBox runat="server" ID="chkLancaFreqHomol" Text="Lança Atividades / Frequências Homologadas"
                                        CssClass="chknonoDigito" />
                                </li>
                                <li style="width: 305px; margin-left: 5px;">
                                    <ul>
                                        <li class="liPedagMatric" style="text-align: left; padding-left: 4px; width: 295px;">
                                            <span style="font-size: 1.0em; text-transform: uppercase; font-weight: bold;">Datas
                                                de Controle</span></li>
                                        <li class="liContrAval">
                                            <label for="txtTipoContrDatas" title="Controle por">
                                                Controle por</label>
                                            <asp:TextBox ID="txtTipoContrDatas" Enabled="false" ClientIDMode="Static" ToolTip="Datas de Controle"
                                                Style="margin-left: 64px; margin-bottom: 0; padding-left: 3px;" BackColor="#FFFFE1"
                                                CssClass="txtContrAval" runat="server"></asp:TextBox>
                                        </li>
                                        <li class="liDataReserva" style="margin-top: 4px;"><span title="Data de Reserva de Matrícula"
                                            style="font-size: 1.0em;">Reserva de Vagas</span></li>
                                        <li class="liTopData" style="margin-top: 8px;">
                                            <asp:TextBox ID="txtReservaMatriculaDtInicioIUE" Style="margin-bottom: 2px;" ToolTip="Informe a Data Inicial de Reserva de Matrícula"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator1" ControlToValidate="txtReservaMatriculaDtInicioIUE"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Reserva de Matrícula"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataReservaMatricula_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li class="liTopData" style="margin-top: 8px;"><span>até </span></li>
                                        <li class="liTopData" style="margin-top: 8px;">
                                            <asp:TextBox ID="txtReservaMatriculaDtFimIUE" Style="margin-bottom: 2px;" ToolTip="Informe a Data Final de Reserva de Matrícula"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator2" ControlToValidate="txtReservaMatriculaDtFimIUE"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Reserva de Matrícula"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataReservaMatricula_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li class="liDataReserva"><span title="Data de Renovação de Matrículas" style="font-size: 1.0em;">
                                            Renovação de Matrículas</span></li>
                                        <li class="liTopData">
                                            <asp:TextBox ID="txtRematriculaInicioIUE" Style="margin-bottom: 2px;" ToolTip="Informe a Data Inicial de Renovação de Matrículas"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator11" ControlToValidate="txtRematriculaInicioIUE"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Renovação de Matrículas"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataRematricula_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li class="liTopData"><span>até </span></li>
                                        <li class="liTopData">
                                            <asp:TextBox ID="txtRematriculaFimIUE" Style="margin-bottom: 2px;" ToolTip="Informe a Data Final de Rematrícula"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator12" ControlToValidate="txtRematriculaFimIUE"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Renovação de Matrículas"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataRematricula_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li class="liDataReserva"><span title="Data de Efetivação de Matrícula" style="font-size: 1.0em;">
                                            Matrículas Novas</span></li>
                                        <li class="liTopData">
                                            <asp:TextBox ID="txtMatriculaInicioIUE" Style="margin-bottom: 2px;" ToolTip="Informa a Data Incial de Efetivação de Matrícula"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator5" ControlToValidate="txtMatriculaInicioIUE"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Matrícula"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataMatricula_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li class="liTopData"><span>até </span></li>
                                        <li class="liTopData">
                                            <asp:TextBox ID="txtMatriculaFimIUE" Style="margin-bottom: 2px;" ToolTip="Informa a Data Final de Efetivação de Matrícula"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator6" ControlToValidate="txtMatriculaFimIUE"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Matrícula"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataMatricula_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li class="liDataReserva"><span title="Data de Rematrícula" style="font-size: 1.0em;">
                                            Remanejamento de Alunos</span></li>
                                        <li class="liTopData">
                                            <asp:TextBox ID="txtDataRemanAlunoIni" Style="margin-bottom: 2px;" ToolTip="Informe a Data Inicial de Renovação de Matrícula"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator13" ControlToValidate="txtDataRemanAlunoIni"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Remanejamento de Alunos"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataRemanAluno_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li class="liTopData"><span>até </span></li>
                                        <li class="liTopData">
                                            <asp:TextBox ID="txtDataRemanAlunoFim" Style="margin-bottom: 2px;" ToolTip="Informe a Data Final de Rematrícula"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator14" ControlToValidate="txtDataRemanAlunoFim"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Remanejamento de Alunos"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataRemanAluno_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li class="liDataReserva"><span title="Data de Transferências Internas" style="font-size: 1.0em;">
                                            Transferências Internas</span></li>
                                        <li class="liTopData">
                                            <asp:TextBox ID="txtDtInicioTransInter" Style="margin-bottom: 2px;" ToolTip="Informe a Data Inicial de Transferências Internas"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator3" ControlToValidate="txtDtInicioTransInter"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Transferências Internas"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataTransInter_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li class="liTopData"><span>até </span></li>
                                        <li class="liTopData">
                                            <asp:TextBox ID="txtDtFimTransInter" Style="margin-bottom: 2px;" ToolTip="Informe a Data Final de Transferências Internas"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator4" ControlToValidate="txtDtFimTransInter"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Transferências Internas"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataTransInter_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li class="liDataReserva"><span title="Data de Trancamento de Matrícula" style="font-size: 1.0em;">
                                            Trancamento de Matrículas</span></li>
                                        <li class="liTopData">
                                            <asp:TextBox ID="txtTrancamentoMatriculaInicioIUE" Style="margin-bottom: 2px;" ToolTip="Informe a Data Inicial de Trancamento de Matrícula"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator7" ControlToValidate="txtTrancamentoMatriculaInicioIUE"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Trancamento de Matrícula"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataTrancamentoMatricula_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li class="liTopData"><span>até </span></li>
                                        <li class="liTopData">
                                            <asp:TextBox ID="txtTrancamentoMatriculaFimIUE" Style="margin-bottom: 2px;" ToolTip="Informe a Data Final de Trancamento de Matrícula"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator8" ControlToValidate="txtTrancamentoMatriculaFimIUE"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Trancamento de Matrícula"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataTrancamentoMatricula_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li class="liDataReserva"><span title="Data de Alteração de Matrícula" style="font-size: 1.0em;">
                                            Altera&ccedil;&atilde;o de Matrículas</span></li>
                                        <li class="liTopData">
                                            <asp:TextBox ID="txtAlteracaoMatriculaInicioIUE" Style="margin-bottom: 2px;" ToolTip="Informe a Data Inicial de Alteração de Matrícula"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator9" ControlToValidate="txtAlteracaoMatriculaInicioIUE"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Alteração de Matrícula"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataAlteracaoMatricula_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li class="liTopData"><span>até </span></li>
                                        <li class="liTopData">
                                            <asp:TextBox ID="txtAlteracaoMatriculaFimIUE" Style="margin-bottom: 2px;" ToolTip="Informe a Data Final de Alteração de Matrícula"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator10" ControlToValidate="txtAlteracaoMatriculaFimIUE"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Alteração de Matrícula"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataAlteracaoMatricula_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                    </ul>
                                </li>
                                <li style="width: 350px; margin-left: 5px;">
                                    <ul>
                                        <li class="liPedagMatric" style="text-align: left; padding-left: 4px; width: 340px;
                                            margin-bottom: -2px;"><span style="font-size: 1.0em; text-transform: uppercase; font-weight: bold;">
                                                Controle de Bimestres</span></li>
                                        <li class="liDataReserva" style="margin-top: 0px; clear: none; width: 175px;"><span
                                            title="Período do 1º Bimestre" style="font-size: 1.0em;">Período do 1º Bimestre</span></li>
                                        <li class="liDataReserva" style="margin-top: 0px; clear: none; width: 130px;"><span
                                            title="Lançamento do 1º Bimestre" style="font-size: 1.0em;">Lançamento do 1º Bimestre</span></li>
                                        <li style="clear: both;">
                                            <asp:TextBox ID="txtPeriodoIniBim1" Style="margin-bottom: 2px;" ToolTip="Informe a Data Início do Período do 1º Bimestre"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator21" ControlToValidate="txtPeriodoIniBim1"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Controle do 1º Bimestre"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataPeriodoBim1_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li style=""><span>à </span></li>
                                        <li style="">
                                            <asp:TextBox ID="txtPeriodoFimBim1" Style="margin-bottom: 2px;" ToolTip="Informe a Data Final do Período do 1º Bimestre"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator22" ControlToValidate="txtPeriodoFimBim1"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Controle do 1º Bimestre"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataPeriodoBim1_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li style="margin-left: 10px;">
                                            <asp:TextBox ID="txtLactoIniBim1" Style="margin-bottom: 2px;" ToolTip="Informe a Data Início do Lançamento do 1º Bimestre"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator29" ControlToValidate="txtLactoIniBim1" runat="server"
                                                ErrorMessage="É necessário informar a Data de Início e Fim de Lançamento do 1º Bimestre"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataLactoBim1_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li style=""><span>à </span></li>
                                        <li style="">
                                            <asp:TextBox ID="txtLactoFimBim1" Style="margin-bottom: 2px;" ToolTip="Informe a Data Final do Lançamento do 1º Bimestre"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator30" ControlToValidate="txtLactoFimBim1" runat="server"
                                                ErrorMessage="É necessário informar a Data de Início e Fim de Lançamento do 1º Bimestre"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataLactoBim1_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li class="liDataReserva" style="width: 175px;"><span title="Período do 2º Bimestre"
                                            style="font-size: 1.0em;">Período do 2º Bimestre</span></li>
                                        <li class="liDataReserva" style="clear: none; width: 130px;"><span title="Lançamento do 2º Bimestre"
                                            style="font-size: 1.0em;">Lançamento do 2º Bimestre</span></li>
                                        <li style="clear: both;">
                                            <asp:TextBox ID="txtPeriodoIniBim2" Style="margin-bottom: 2px;" ToolTip="Informe a Data Início do Período do 2º Bimestre"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator23" ControlToValidate="txtPeriodoIniBim2"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Controle do 2º Bimestre"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataPeriodoBim2_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li class="liTopData" style=""><span>à </span></li>
                                        <li style="">
                                            <asp:TextBox ID="txtPeriodoFimBim2" Style="margin-bottom: 2px;" ToolTip="Informe a Data Final do Período do 2º Bimestre"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator24" ControlToValidate="txtPeriodoFimBim2"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Controle do 2º Bimestre"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataPeriodoBim2_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li style="margin-left: 10px;">
                                            <asp:TextBox ID="txtLactoIniBim2" Style="margin-bottom: 2px;" ToolTip="Informe a Data Início do Lançamento do 2º Bimestre"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator31" ControlToValidate="txtLactoIniBim2" runat="server"
                                                ErrorMessage="É necessário informar a Data de Início e Fim de Lançamento do 2º Bimestre"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataLactoBim2_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li style=""><span>à </span></li>
                                        <li style="">
                                            <asp:TextBox ID="txtLactoFimBim2" Style="margin-bottom: 2px;" ToolTip="Informe a Data Final do Lançamento do 2º Bimestre"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator32" ControlToValidate="txtLactoFimBim2" runat="server"
                                                ErrorMessage="É necessário informar a Data de Início e Fim de Lançamento do 2º Bimestre"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataLactoBim2_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li class="liDataReserva" style="width: 175px;"><span title="Período do 3º Bimestre"
                                            style="font-size: 1.0em;">Período do 3º Bimestre</span></li>
                                        <li class="liDataReserva" style="clear: none; width: 130px;"><span title="Lançamento do 3º Bimestre"
                                            style="font-size: 1.0em;">Lançamento do 3º Bimestre</span></li>
                                        <li style="clear: both;">
                                            <asp:TextBox ID="txtPeriodoIniBim3" Style="margin-bottom: 2px;" ToolTip="Informe a Data Início do Período do 3º Bimestre"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator25" ControlToValidate="txtPeriodoIniBim3"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Controle do 3º Bimestre"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataPeriodoBim3_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li style=""><span>à </span></li>
                                        <li style="">
                                            <asp:TextBox ID="txtPeriodoFimBim3" Style="margin-bottom: 2px;" ToolTip="Informe a Data Final do Período do 3º Bimestre"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator26" ControlToValidate="txtPeriodoFimBim3"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Controle do 3º Bimestre"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataPeriodoBim3_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li style="margin-left: 10px;">
                                            <asp:TextBox ID="txtLactoIniBim3" Style="margin-bottom: 2px;" ToolTip="Informe a Data Início do Lançamento do 3º Bimestre"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator33" ControlToValidate="txtLactoIniBim3" runat="server"
                                                ErrorMessage="É necessário informar a Data de Início e Fim de Lançamento do 3º Bimestre"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataLactoBim3_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li style=""><span>à </span></li>
                                        <li style="">
                                            <asp:TextBox ID="txtLactoFimBim3" Style="margin-bottom: 2px;" ToolTip="Informe a Data Final do Lançamento do 3º Bimestre"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator34" ControlToValidate="txtLactoFimBim3" runat="server"
                                                ErrorMessage="É necessário informar a Data de Início e Fim de Lançamento do 3º Bimestre"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataLactoBim3_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li class="liDataReserva" style="width: 175px;"><span title="Período do 4º Bimestre"
                                            style="font-size: 1.0em;">Período do 4º Bimestre</span></li>
                                        <li class="liDataReserva" style="clear: none; width: 130px;"><span title="Lançamento do 4º Bimestre"
                                            style="font-size: 1.0em;">Lançamento do 4º Bimestre</span></li>
                                        <li style="clear: both;">
                                            <asp:TextBox ID="txtPeriodoIniBim4" Style="margin-bottom: 2px;" ToolTip="Informe a Data Início do Período do 4º Bimestre"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator27" ControlToValidate="txtPeriodoIniBim4"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Controle do 4º Bimestre"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataPeriodoBim4_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li style=""><span>à </span></li>
                                        <li style="">
                                            <asp:TextBox ID="txtPeriodoFimBim4" Style="margin-bottom: 2px;" ToolTip="Informe a Data Final do Período do 4º Bimestre"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator28" ControlToValidate="txtPeriodoFimBim4"
                                                runat="server" ErrorMessage="É necessário informar a Data de Início e Fim de Controle do 4º Bimestre"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataPeriodoBim4_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li style="margin-left: 10px;">
                                            <asp:TextBox ID="txtLactoIniBim4" Style="margin-bottom: 2px;" ToolTip="Informe a Data Início do Lançamento do 4º Bimestre"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator35" ControlToValidate="txtLactoIniBim4" runat="server"
                                                ErrorMessage="É necessário informar a Data de Início e Fim de Lançamento do 4º Bimestre"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataLactoBim4_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                        <li style=""><span>à </span></li>
                                        <li style="">
                                            <asp:TextBox ID="txtLactoFimBim4" Style="margin-bottom: 2px;" ToolTip="Informe a Data Final do Lançamento do 4º Bimestre"
                                                CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator36" ControlToValidate="txtLactoFimBim4" runat="server"
                                                ErrorMessage="É necessário informar a Data de Início e Fim de Lançamento do 4º Bimestre"
                                                Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataLactoBim4_ServerValidate">
                                            </asp:CustomValidator>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div id="tabGestorUnidade" class="tabGestorUnidade" clientidmode="Static" style="display: none;"
                    runat="server">
                    <ul class="ulDados">
                        <li class="liClear" style="margin-left: 130px;">
                            <label for="txtUnidadeEscolarGU" title="Razão Social">
                                Unidade</label>
                            <asp:TextBox ID="txtUnidadeEscolarGU" Enabled="false" ClientIDMode="Static" ToolTip="Informe a Razão Social"
                                CssClass="txtRazaoSocialIUE" MaxLength="80" runat="server"></asp:TextBox>
                        </li>
                        <li class="liCodIdentificacao">
                            <label for="txtCodIdenticacaoGU" title="Código de Identificação">
                                Cód. Identificação</label>
                            <asp:TextBox ID="txtCodIdenticacaoGU" Enabled="false" ClientIDMode="Static" ToolTip="Informe o Código de Identificação"
                                CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-left: 10px;">
                            <label for="txtCNPJGU" title="CNPJ">
                                Nº CNPJ</label>
                            <asp:TextBox ID="txtCNPJGU" Enabled="false" ClientIDMode="Static" ToolTip="Informe o CNPJ"
                                CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="width: 100%; text-align: center;"><span style="color: #6495ED;
                            font-size: 1.3em; font-weight: bold;">Controle Gestores Unidade</span></li>
                        <li class="liClear">
                            <ul>
                                <li>
                                    <ul>
                                        <li class="liClear" style="margin-top: 10px; width: 850px;">
                                            <ul>
                                                <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                                    font-weight: bold;">Direção da Unidade de Ensino</span></li>
                                                <li class="liClear" style="margin-top: 5px; width: 85px;">
                                                    <label for="ddlSiglaUnidSecreEscol1" title="Descrição" style="color: #006699;">
                                                        Descrição</label>
                                                    <label title="Diretor(a) 1">
                                                        Diretor(a) 1</label>
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <label for="txtTitulDir1" title="Título da Direção" style="color: #006699;">
                                                        Título</label>
                                                    <asp:TextBox ID="txtTitulDir1" ToolTip="Informe o Título da Direção" CssClass="txtTitulDirCoor"
                                                        MaxLength="25" runat="server"></asp:TextBox>
                                                </li>
                                                <li style="margin-top: 5px; margin-left: 5px;">
                                                    <label for="ddlFuncDir1" title="Funcionário" style="color: #006699;">
                                                        Funcionário</label>
                                                    <asp:DropDownList ID="ddlFuncDir1" runat="server" ToolTip="Selecione o Nome Funcionário"
                                                        CssClass="ddlNomeSecreEscol" />
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <label for="ddlTipoEnsinoDir1" title="Tipo de Ensino" style="color: #006699;">
                                                        Tipo de Ensino</label>
                                                    <asp:DropDownList ID="ddlTipoEnsinoDir1" runat="server" ToolTip="Selecione o Tipo de Ensino"
                                                        CssClass="ddlTipoEnsinoDir">
                                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                                        <asp:ListItem Value="S">Ensino Superior</asp:ListItem>
                                                        <asp:ListItem Value="M">Ensino Médio</asp:ListItem>
                                                        <asp:ListItem Value="F">Ensino Fundamental</asp:ListItem>
                                                        <asp:ListItem Value="I">Ensino Infantil</asp:ListItem>
                                                        <asp:ListItem Value="O">Outros</asp:ListItem>
                                                        <asp:ListItem Value="T">Todos</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <label for="ddlDirGeralDir1" title="Diretor(a) Geral" style="color: #006699;">
                                                        Geral</label>
                                                    <asp:DropDownList ID="ddlDirGeralDir1" runat="server" ToolTip="Selecione se é um(a) diretor(a) geral"
                                                        CssClass="ddlDirGeralDir">
                                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li class="liClear" style="margin-top: 5px; width: 85px;">
                                                    <label title="Diretor(a) 2">
                                                        Diretor(a) 2</label>
                                                </li>
                                                <li style="margin-top: 5px; margin-left: 5px;">
                                                    <asp:TextBox ID="txtTitulDir2" ToolTip="Informe o Título da Direção" CssClass="txtTitulDirCoor"
                                                        MaxLength="25" runat="server"></asp:TextBox>
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <asp:DropDownList ID="ddlFuncDir2" runat="server" ToolTip="Selecione o Nome Funcionário"
                                                        CssClass="ddlNomeSecreEscol" />
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <asp:DropDownList ID="ddlTipoEnsinoDir2" runat="server" ToolTip="Selecione o Tipo de Ensino"
                                                        CssClass="ddlTipoEnsinoDir">
                                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                                        <asp:ListItem Value="S">Ensino Superior</asp:ListItem>
                                                        <asp:ListItem Value="M">Ensino Médio</asp:ListItem>
                                                        <asp:ListItem Value="F">Ensino Fundamental</asp:ListItem>
                                                        <asp:ListItem Value="I">Ensino Infantil</asp:ListItem>
                                                        <asp:ListItem Value="O">Outros</asp:ListItem>
                                                        <asp:ListItem Value="T">Todos</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-right: 0px; margin-top: 5px; margin-left: 5px;">
                                                    <asp:DropDownList ID="ddlDirGeralDir2" runat="server" ToolTip="Selecione se é um(a) diretor(a) geral"
                                                        CssClass="ddlDirGeralDir">
                                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li class="liClear" style="margin-top: 5px; width: 85px;">
                                                    <label title="Diretor(a) 3">
                                                        Diretor(a) 3</label>
                                                </li>
                                                <li style="margin-top: 5px; margin-left: 5px;">
                                                    <asp:TextBox ID="txtTitulDir3" ToolTip="Informe o Título da Direção" CssClass="txtTitulDirCoor"
                                                        MaxLength="25" runat="server"></asp:TextBox>
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <asp:DropDownList ID="ddlFuncDir3" runat="server" ToolTip="Selecione o Nome Funcionário"
                                                        CssClass="ddlNomeSecreEscol" />
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <asp:DropDownList ID="ddlTipoEnsinoDir3" runat="server" ToolTip="Selecione o Tipo de Ensino"
                                                        CssClass="ddlTipoEnsinoDir">
                                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                                        <asp:ListItem Value="S">Ensino Superior</asp:ListItem>
                                                        <asp:ListItem Value="M">Ensino Médio</asp:ListItem>
                                                        <asp:ListItem Value="F">Ensino Fundamental</asp:ListItem>
                                                        <asp:ListItem Value="I">Ensino Infantil</asp:ListItem>
                                                        <asp:ListItem Value="O">Outros</asp:ListItem>
                                                        <asp:ListItem Value="T">Todos</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-right: 0px; margin-top: 5px; margin-left: 5px;">
                                                    <asp:DropDownList ID="ddlDirGeralDir3" runat="server" ToolTip="Selecione se é um(a) diretor(a) geral"
                                                        CssClass="ddlDirGeralDir">
                                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li class="liClear" style="margin-top: 5px; width: 85px;">
                                                    <label title="Diretor(a) 4">
                                                        Diretor(a) 4</label>
                                                </li>
                                                <li style="margin-top: 5px; margin-left: 5px;">
                                                    <asp:TextBox ID="txtTitulDir4" ToolTip="Informe o Título da Direção" CssClass="txtTitulDirCoor"
                                                        MaxLength="25" runat="server"></asp:TextBox>
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <asp:DropDownList ID="ddlFuncDir4" runat="server" ToolTip="Selecione o Nome Funcionário"
                                                        CssClass="ddlNomeSecreEscol" />
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <asp:DropDownList ID="ddlTipoEnsinoDir4" runat="server" ToolTip="Selecione o Tipo de Ensino"
                                                        CssClass="ddlTipoEnsinoDir">
                                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                                        <asp:ListItem Value="S">Ensino Superior</asp:ListItem>
                                                        <asp:ListItem Value="M">Ensino Médio</asp:ListItem>
                                                        <asp:ListItem Value="F">Ensino Fundamental</asp:ListItem>
                                                        <asp:ListItem Value="I">Ensino Infantil</asp:ListItem>
                                                        <asp:ListItem Value="O">Outros</asp:ListItem>
                                                        <asp:ListItem Value="T">Todos</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-right: 0px; margin-top: 5px; margin-left: 5px;">
                                                    <asp:DropDownList ID="ddlDirGeralDir4" runat="server" ToolTip="Selecione se é um(a) diretor(a) geral"
                                                        CssClass="ddlDirGeralDir">
                                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                            </ul>
                                        </li>
                                        <li class="liClear" style="margin-top: 10px; width: 850px;">
                                            <ul>
                                                <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                                    font-weight: bold;">Coordenação da Unidade de Ensino</span></li>
                                                <li class="liClear" style="margin-top: 5px; width: 85px;">
                                                    <label for="ddlSiglaUnidSecreEscol1" title="Descrição" style="color: #006699;">
                                                        Descrição</label>
                                                    <label title="Coordenador(a) 1">
                                                        Coordenador(a) 1</label>
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <label for="txtTitulCoord1" title="Título da Direção" style="color: #006699;">
                                                        Título</label>
                                                    <asp:TextBox ID="txtTitulCoord1" ToolTip="Informe o Título da Coordenação" CssClass="txtTitulDirCoor"
                                                        MaxLength="25" runat="server"></asp:TextBox>
                                                </li>
                                                <li style="margin-top: 5px; margin-left: 5px;">
                                                    <label for="ddlFuncCoord1" title="Funcionário" style="color: #006699;">
                                                        Funcionário</label>
                                                    <asp:DropDownList ID="ddlFuncCoord1" runat="server" ToolTip="Selecione o Nome Funcionário"
                                                        CssClass="ddlNomeSecreEscol" />
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <label for="ddlTipoEnsinoCoord1" title="Tipo de Ensino" style="color: #006699;">
                                                        Tipo de Ensino</label>
                                                    <asp:DropDownList ID="ddlTipoEnsinoCoord1" runat="server" ToolTip="Selecione o Tipo de Ensino"
                                                        CssClass="ddlTipoEnsinoDir">
                                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                                        <asp:ListItem Value="S">Ensino Superior</asp:ListItem>
                                                        <asp:ListItem Value="M">Ensino Médio</asp:ListItem>
                                                        <asp:ListItem Value="F">Ensino Fundamental</asp:ListItem>
                                                        <asp:ListItem Value="I">Ensino Infantil</asp:ListItem>
                                                        <asp:ListItem Value="O">Outros</asp:ListItem>
                                                        <asp:ListItem Value="T">Todos</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <label for="ddlDirGeralCoord1" title="Coordenador(a) Geral" style="color: #006699;">
                                                        Geral</label>
                                                    <asp:DropDownList ID="ddlDirGeralCoord1" runat="server" ToolTip="Selecione se é um(a) coordenador(a) geral"
                                                        CssClass="ddlDirGeralDir">
                                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li class="liClear" style="margin-top: 5px; width: 85px;">
                                                    <label title="Coordenador(a) 2">
                                                        Coordenador(a) 2</label>
                                                </li>
                                                <li style="margin-top: 5px; margin-left: 5px;">
                                                    <asp:TextBox ID="txtTitulCoord2" ToolTip="Informe o Título da Coordenação" CssClass="txtTitulDirCoor"
                                                        MaxLength="25" runat="server"></asp:TextBox>
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <asp:DropDownList ID="ddlFuncCoord2" runat="server" ToolTip="Selecione o Nome Funcionário"
                                                        CssClass="ddlNomeSecreEscol" />
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <asp:DropDownList ID="ddlTipoEnsinoCoord2" runat="server" ToolTip="Selecione o Tipo de Ensino"
                                                        CssClass="ddlTipoEnsinoDir">
                                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                                        <asp:ListItem Value="S">Ensino Superior</asp:ListItem>
                                                        <asp:ListItem Value="M">Ensino Médio</asp:ListItem>
                                                        <asp:ListItem Value="F">Ensino Fundamental</asp:ListItem>
                                                        <asp:ListItem Value="I">Ensino Infantil</asp:ListItem>
                                                        <asp:ListItem Value="O">Outros</asp:ListItem>
                                                        <asp:ListItem Value="T">Todos</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-right: 0px; margin-top: 5px; margin-left: 5px;">
                                                    <asp:DropDownList ID="ddlDirGeralCoord2" runat="server" ToolTip="Selecione se é um(a) coordenador(a) geral"
                                                        CssClass="ddlDirGeralDir">
                                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li class="liClear" style="margin-top: 5px; width: 85px;">
                                                    <label title="Coordenador(a) 3">
                                                        Coordenador(a) 3</label>
                                                </li>
                                                <li style="margin-top: 5px; margin-left: 5px;">
                                                    <asp:TextBox ID="txtTitulCoord3" ToolTip="Informe o Título da Coordenação" CssClass="txtTitulDirCoor"
                                                        MaxLength="25" runat="server"></asp:TextBox>
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <asp:DropDownList ID="ddlFuncCoord3" runat="server" ToolTip="Selecione o Nome Funcionário"
                                                        CssClass="ddlNomeSecreEscol" />
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <asp:DropDownList ID="ddlTipoEnsinoCoord3" runat="server" ToolTip="Selecione o Tipo de Ensino"
                                                        CssClass="ddlTipoEnsinoDir">
                                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                                        <asp:ListItem Value="S">Ensino Superior</asp:ListItem>
                                                        <asp:ListItem Value="M">Ensino Médio</asp:ListItem>
                                                        <asp:ListItem Value="F">Ensino Fundamental</asp:ListItem>
                                                        <asp:ListItem Value="I">Ensino Infantil</asp:ListItem>
                                                        <asp:ListItem Value="O">Outros</asp:ListItem>
                                                        <asp:ListItem Value="T">Todos</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-right: 0px; margin-top: 5px; margin-left: 5px;">
                                                    <asp:DropDownList ID="ddlDirGeralCoord3" runat="server" ToolTip="Selecione se é um(a) coordenador(a) geral"
                                                        CssClass="ddlDirGeralDir">
                                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li class="liClear" style="margin-top: 5px; width: 85px;">
                                                    <label title="Coordenador(a) 4">
                                                        Coordenador(a) 4</label>
                                                </li>
                                                <li style="margin-top: 5px; margin-left: 5px;">
                                                    <asp:TextBox ID="txtTitulCoord4" ToolTip="Informe o Título da Coordenação" CssClass="txtTitulDirCoor"
                                                        MaxLength="25" runat="server"></asp:TextBox>
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <asp:DropDownList ID="ddlFuncCoord4" runat="server" ToolTip="Selecione o Nome Funcionário"
                                                        CssClass="ddlNomeSecreEscol" />
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <asp:DropDownList ID="ddlTipoEnsinoCoord4" runat="server" ToolTip="Selecione o Tipo de Ensino"
                                                        CssClass="ddlTipoEnsinoDir">
                                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                                        <asp:ListItem Value="S">Ensino Superior</asp:ListItem>
                                                        <asp:ListItem Value="M">Ensino Médio</asp:ListItem>
                                                        <asp:ListItem Value="F">Ensino Fundamental</asp:ListItem>
                                                        <asp:ListItem Value="I">Ensino Infantil</asp:ListItem>
                                                        <asp:ListItem Value="O">Outros</asp:ListItem>
                                                        <asp:ListItem Value="T">Todos</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-right: 0px; margin-top: 5px; margin-left: 5px;">
                                                    <asp:DropDownList ID="ddlDirGeralCoord4" runat="server" ToolTip="Selecione se é um(a) coordenador(a) geral"
                                                        CssClass="ddlDirGeralDir">
                                                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div id="tabSecreEscol" class="tabSecreEscol" clientidmode="Static" style="display: none;"
                    runat="server">
                    <ul class="ulDados">
                        <li class="liClear" style="margin-left: 130px;">
                            <label for="txtUnidadeEscolarSE" title="Razão Social">
                                Unidade Escolar</label>
                            <asp:TextBox ID="txtUnidadeEscolarSE" Enabled="false" ClientIDMode="Static" ToolTip="Informe a Razão Social"
                                CssClass="txtRazaoSocialIUE" MaxLength="80" runat="server"></asp:TextBox>
                        </li>
                        <li class="liCodIdentificacao">
                            <label for="txtCodIdenticacaoSE" title="Código de Identificação">
                                Cód. Identificação</label>
                            <asp:TextBox ID="txtCodIdenticacaoSE" Enabled="false" ClientIDMode="Static" ToolTip="Informe o Código de Identificação"
                                CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-left: 10px;">
                            <label for="txtCNPJSE" title="CNPJ">
                                Nº CNPJ</label>
                            <asp:TextBox ID="txtCNPJSE" Enabled="false" ClientIDMode="Static" ToolTip="Informe o CNPJ"
                                CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="width: 100%; text-align: center;"><span style="color: #6495ED;
                            font-size: 1.3em; font-weight: bold;">Controle Secretaria Escolar</span></li>
                        <li class="liClear" style="margin-top: 2px; margin-left: 323px;">
                            <label for="txtTipoCtrlSecreEscol" style="float: left;" title="Tipo de Secretaria Escolar">
                                Por</label>
                            <asp:TextBox ID="txtTipoCtrlSecreEscol" Enabled="false" Style="margin-left: 7px;
                                padding-left: 3px;" ToolTip="Tipo de Secretaria Escolar" BackColor="#FFFFE1"
                                CssClass="txtTipoCtrlFrequ" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <ul>
                                <li style="border-right: 1px solid #CCCCCC; width: 370px; height: 295px;">
                                    <ul>
                                        <li style="padding-right: 10px; border-right: 1px solid #CCCCCC; height: 105px;">
                                            <ul>
                                                <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                                    font-weight: bold;">Usuários de Secretaria</span></li>
                                                <li class="liRecuperacao liClear" style="margin-top: 7px; margin-left: -5px;">
                                                    <asp:CheckBox ID="chkUsuarFunc" ToolTip="Informe se existirá usuário Funcionário"
                                                        runat="server" Text="Funcionários" />
                                                </li>
                                                <li class="liRecuperacao liClear" style="margin-left: -5px;">
                                                    <asp:CheckBox ID="chkUsuarProf" ToolTip="Informe se existirá usuário Professor" runat="server"
                                                        Text="Professores" />
                                                </li>
                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                    <ContentTemplate>
                                                        <li class="liRecuperacao liClear" style="margin-left: -5px;">
                                                            <asp:CheckBox ID="chkUsuarAluno" ToolTip="Informe se existirá usuário Aluno" runat="server"
                                                                OnCheckedChanged="chkUsuarAluno_CheckedChanged" AutoPostBack="true" Text="Alunos" />
                                                        </li>
                                                        <li style="margin-left: 4px;">
                                                            <label for="txtIdadeMinimAlunoSecreEscol" style="float: left;" title="Idade Mínima para Aluno">
                                                                Idade Mínima</label>
                                                            <asp:TextBox ID="txtIdadeMinimAlunoSecreEscol" Style="margin-left: 7px;" ToolTip="Informe a Idade Mínima do aluno"
                                                                Enabled="false" CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                                        </li>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <li class="liRecuperacao liClear" style="margin-left: -5px;">
                                                    <asp:CheckBox ID="chkUsuarPaisRespo" ToolTip="Informe se existirá usuário Pais/Responsáveis"
                                                        runat="server" Text="Pais/Responsáveis" />
                                                </li>
                                                <li class="liRecuperacao liClear" style="margin-left: -5px;">
                                                    <asp:CheckBox ID="chkUsuarOutro" ToolTip="Informe se existirá usuário Outros" runat="server"
                                                        Text="Outros" />
                                                </li>
                                            </ul>
                                        </li>
                                        <li style="padding-left: 5px;">
                                            <ul>
                                                <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                                    font-weight: bold;">Horário de Atividades</span></li>
                                                <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                                                    <ContentTemplate>
                                                        <li class="liRecuperacao liClear" style="margin-left: -5px; margin-bottom: 3px; margin-top: 7px;">
                                                            <asp:CheckBox ID="chkHorAtiSecreEscol" ToolTip="Informe se utiliza mesmo horário da Unidade"
                                                                runat="server" Text="Utiliza mesmo horário da Unidade" ForeColor="#006699" OnCheckedChanged="chkHorAtiSecreEscol_CheckedChanged"
                                                                AutoPostBack="true" />
                                                        </li>
                                                        <li class="liClear">
                                                            <ul>
                                                                <li><span title="Turno 1">Turno 1</span></li>
                                                                <li class="liClear">
                                                                    <asp:TextBox ID="txtHorarIniT1SecreEscol" Style="margin-bottom: 3px;" ToolTip="Informe o Horário Inicial do Turno 1"
                                                                        CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                                                                </li>
                                                                <li><span>às </span></li>
                                                                <li>
                                                                    <asp:TextBox ID="txtHorarFimT1SecreEscol" Style="margin-bottom: 3px;" ToolTip="Informe o Horário Final do Turno 1"
                                                                        CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                                                                </li>
                                                            </ul>
                                                        </li>
                                                        <li>
                                                            <ul>
                                                                <li><span title="Turno 2">Turno 2</span></li>
                                                                <li class="liClear">
                                                                    <asp:TextBox ID="txtHorarIniT2SecreEscol" Style="margin-bottom: 3px;" ToolTip="Informe o Horário Inical do Turno 2"
                                                                        CssClass="txtHorarioFuncTarde" runat="server"></asp:TextBox>
                                                                </li>
                                                                <li><span>às </span></li>
                                                                <li>
                                                                    <asp:TextBox ID="txtHorarFimT2SecreEscol" Style="margin-bottom: 3px;" ToolTip="Informe o Horário Final do Turno 2"
                                                                        CssClass="txtHorarioFuncTarde" runat="server"></asp:TextBox>
                                                                </li>
                                                            </ul>
                                                        </li>
                                                        <li class="liClear">
                                                            <ul>
                                                                <li class="liClear" title="Turno 3"><span>Turno 3</span></li>
                                                                <li class="liClear">
                                                                    <asp:TextBox ID="txtHorarIniT3SecreEscol" Style="margin-bottom: 5px;" ToolTip="Informe o Horário Inicial do Turno 3"
                                                                        CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                                                </li>
                                                                <li><span>às </span></li>
                                                                <li>
                                                                    <asp:TextBox ID="txtHorarFimT3SecreEscol" Style="margin-bottom: 5px;" ToolTip="Informe o Horário Final do Turno 3"
                                                                        CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                                                </li>
                                                            </ul>
                                                        </li>
                                                        <li>
                                                            <ul>
                                                                <li class="liClear" title="Turno 4"><span>Turno 4</span></li>
                                                                <li class="liClear">
                                                                    <asp:TextBox ID="txtHorarIniT4SecreEscol" Style="margin-bottom: 5px;" ToolTip="Informe o Horário Inicial do Turno 4"
                                                                        CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                                                </li>
                                                                <li><span>às </span></li>
                                                                <li>
                                                                    <asp:TextBox ID="txtHorarFimT4SecreEscol" Style="margin-bottom: 5px;" ToolTip="Informe o Horário Final do Turno 4"
                                                                        CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                                                </li>
                                                            </ul>
                                                        </li>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ul>
                                        </li>
                                        <li class="liClear" style="margin-top: 10px; width: 360px;">
                                            <ul>
                                                <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                                    font-weight: bold;">Secretário(a) Escolar</span></li>
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
                                                        <li class="liClear" style="margin-top: 5px;">
                                                            <label for="ddlSiglaUnidSecreEscol1" title="Unidade" style="color: #006699;">
                                                                Unidade</label>
                                                            <asp:DropDownList ID="ddlSiglaUnidSecreEscol1" AutoPostBack="true" runat="server"
                                                                ToolTip="Selecione a Sigla da Unidade Escolar" CssClass="ddlSiglaUnidSecreEscol"
                                                                OnSelectedIndexChanged="ddlSiglaUnidSecreEscol1_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </li>
                                                        <li style="margin-left: 5px; margin-top: 5px;">
                                                            <label for="ddlNomeSecreEscol1" title="Nome Secretário Escolar" style="color: #006699;">
                                                                Nome Secretário Escolar</label>
                                                            <asp:DropDownList ID="ddlNomeSecreEscol1" runat="server" ToolTip="Selecione o Nome Secretário Escolar"
                                                                CssClass="ddlNomeSecreEscol" />
                                                        </li>
                                                        <li style="margin-right: 0px; margin-top: 5px;">
                                                            <label for="ddlClassifSecre1" title="Classificação do Secretário" style="color: #006699;">
                                                                Classif.</label>
                                                            <asp:DropDownList ID="ddlClassifSecre1" runat="server" ToolTip="Selecione a classificação de secretário"
                                                                CssClass="ddlQtdeSecretario">
                                                                <asp:ListItem Selected="True" Value="1">1º</asp:ListItem>
                                                                <asp:ListItem Value="2">2º</asp:ListItem>
                                                                <asp:ListItem Value="3">3º</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </li>
                                                        <li class="liClear" style="margin-top: 5px;">
                                                            <asp:DropDownList ID="ddlSiglaUnidSecreEscol2" AutoPostBack="true" runat="server"
                                                                ToolTip="Selecione a Sigla da Unidade Escolar" CssClass="ddlSiglaUnidSecreEscol"
                                                                OnSelectedIndexChanged="ddlSiglaUnidSecreEscol2_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </li>
                                                        <li style="margin-left: 5px; margin-top: 5px;">
                                                            <asp:DropDownList ID="ddlNomeSecreEscol2" runat="server" ToolTip="Selecione o Nome Secretário Escolar"
                                                                CssClass="ddlNomeSecreEscol" />
                                                        </li>
                                                        <li style="margin-right: 0px; margin-top: 5px;">
                                                            <asp:DropDownList ID="ddlClassifSecre2" runat="server" ToolTip="Selecione a classificação de secretário"
                                                                CssClass="ddlQtdeSecretario">
                                                                <asp:ListItem Value="1">1º</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="2">2º</asp:ListItem>
                                                                <asp:ListItem Value="3">3º</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </li>
                                                        <li class="liClear" style="margin-top: 5px;">
                                                            <asp:DropDownList ID="ddlSiglaUnidSecreEscol3" AutoPostBack="true" runat="server"
                                                                ToolTip="Selecione a Sigla da Unidade Escolar" CssClass="ddlSiglaUnidSecreEscol"
                                                                OnSelectedIndexChanged="ddlSiglaUnidSecreEscol3_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </li>
                                                        <li style="margin-left: 5px; margin-top: 5px;">
                                                            <asp:DropDownList ID="ddlNomeSecreEscol3" runat="server" ToolTip="Selecione o Nome Secretário Escolar"
                                                                CssClass="ddlNomeSecreEscol" />
                                                        </li>
                                                        <li style="margin-right: 0px; margin-top: 5px;">
                                                            <asp:DropDownList ID="ddlClassifSecre3" runat="server" ToolTip="Selecione a classificação de secretário"
                                                                CssClass="ddlQtdeSecretario">
                                                                <asp:ListItem Value="1">1º</asp:ListItem>
                                                                <asp:ListItem Value="2">2º</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="3">3º</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </li>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li style="padding-left: 5px;">
                                    <ul>
                                        <li>
                                            <ul>
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <li class="liPedagMatric" style="width: 375px;"><span style="font-size: 1.0em; text-transform: uppercase;
                                                            font-weight: bold;">Serviços de Secretaria Escolar</span></li>
                                                        <li class="liControleMetodologia" style="width: 190px;">
                                                            <label title="Gera nº solicitação automático?">
                                                                Gera nº solicitação automático?</label>
                                                            <asp:DropDownList ID="ddlGeraNumSolicAuto" OnSelectedIndexChanged="ddlGeraNumSolicAuto_SelectedIndexChanged"
                                                                AutoPostBack="true" ToolTip="Informe se gera nº de solicitação automático" CssClass="ddlAlterPrazoEntreSolic"
                                                                runat="server">
                                                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                                                <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </li>
                                                        <li style="margin-top: 7px;">
                                                            <label for="txtNumIniciSolicAuto" style="float: left;" title="Número Inicial de Solicitação Automática">
                                                                Nº Inicial</label>
                                                            <asp:TextBox ID="txtNumIniciSolicAuto" Enabled="false" Style="margin-left: 8px; margin-bottom: 0;
                                                                text-align: right;" ToolTip="Informe o Número Inicial de Solicitação Automática"
                                                                CssClass="txtNumIniciSolicAuto" runat="server"></asp:TextBox>
                                                        </li>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                        <li class="liControleMetodologia" style="width: 190px; margin-top: 5px;">
                                                            <label title="Controle de prazo de entrega?">
                                                                Controle de prazo de entrega?</label>
                                                            <asp:DropDownList ID="ddlContrPrazEntre" Style="margin-left: 3px;" OnSelectedIndexChanged="ddlContrPrazEntre_SelectedIndexChanged"
                                                                AutoPostBack="true" ToolTip="Informe se possui controle de prazo de entrega"
                                                                CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                                                <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </li>
                                                        <li style="margin-top: 5px;">
                                                            <label for="txtQtdeDiasEntreSolic" style="float: left;" title="Quantidade de dias para entrega">
                                                                Qtde dias</label>
                                                            <asp:TextBox ID="txtQtdeDiasEntreSolic" Style="margin-left: 7px;" ToolTip="Informe a Quantidade de dias para entrega"
                                                                Enabled="false" CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                                        </li>
                                                        <li style="margin-top: 5px; margin-left: 13px;">
                                                            <label for="ddlAlterPrazoEntreSolic" style="float: left;" title="Pode ser alterado o prazo de entrega?">
                                                                Altera?</label>
                                                            <asp:DropDownList ID="ddlAlterPrazoEntreSolic" Style="margin-left: 7px;" ToolTip="Informe se o Prazo de Entrega pode ser alterado"
                                                                Enabled="false" CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                                                <asp:ListItem Value="N">Não</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </li>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                                                    <ContentTemplate>
                                                        <li class="liControleMetodologia" style="width: 190px; margin-top: 5px;">
                                                            <label title="Controle de prazo de entrega?">
                                                                Apresenta valores de Serviços?</label>
                                                            <asp:DropDownList ID="ddlFlagApresValorServi" Style="" OnSelectedIndexChanged="ddlFlagApresValorServi_SelectedIndexChanged"
                                                                AutoPostBack="true" ToolTip="Informe se possui controle de prazo de entrega"
                                                                Enabled="false" CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                                                <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </li>
                                                        <li style="margin-top: 5px;">
                                                            <label for="ddlAbonaValorServiSolic" style="float: left;" title="Pode ser abonado o valor de serviço?">
                                                                Abona?</label>
                                                            <asp:DropDownList ID="ddlAbonaValorServiSolic" Style="margin-left: 7px;" ToolTip="Informe se o valor de serviço pode ser abonado"
                                                                Enabled="false" CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                                                <asp:ListItem Value="N">Não</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </li>
                                                        <li style="margin-top: 5px;">
                                                            <label for="ddlApresValorServiSolic" style="float: left;" title="Pode ser alterado o valor de serviço?">
                                                                Altera?</label>
                                                            <asp:DropDownList ID="ddlApresValorServiSolic" Style="margin-left: 7px;" ToolTip="Informe se o valor de serviço pode ser alterado"
                                                                Enabled="false" CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                                                <asp:ListItem Value="N">Não</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </li>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <li class="liClear" style="margin-top: 5px;">
                                                    <label for="ddlFlagIncluContaReceb" style="float: left;" title="Inclui taxas no contas a receber?">
                                                        Inclui taxas no CAR?</label>
                                                    <asp:DropDownList ID="ddlFlagIncluContaReceb" Style="margin-left: 50px;" ToolTip="Informe se inclui no contas a receber"
                                                        runat="server" CssClass="ddlAlterPrazoEntreSolic" AutoPostBack="true" OnSelectedIndexChanged="ddlFlagIncluContaReceb_SelectedIndexChanged">
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                            </ul>
                                        </li>
                                        <li class="liClear" style="margin-top: 15px;">
                                            <ul>
                                                <li class="liPedagMatric" style="width: 100%; background-color: #FFFFCC;"><span style="font-size: 1.0em;
                                                    text-transform: uppercase; font-weight: bold;">Boleto Bancário</span></li>
                                                <li class="liClear">
                                                    <ul>
                                                        <li class="liClear">
                                                            <ul>
                                                                <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                                                    <ContentTemplate>
                                                                        <li class="liClear" style="margin-top: 7px;">
                                                                            <label for="ddlFlagGerarBoletoIUE" style="float: left;" title="Gera Boleto Bancário?">
                                                                                Gera Boleto Bancário?</label>
                                                                            <asp:DropDownList ID="ddlGeraBoletoServiSecre" Style="margin-left: 21px;" OnSelectedIndexChanged="ddlGeraBoletoServiSecre_SelectedIndexChanged"
                                                                                AutoPostBack="true" ToolTip="Informe se serão gerados Boletos Bancários" runat="server"
                                                                                CssClass="ddlAlterPrazoEntreSolic">
                                                                                <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                                                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </li>
                                                                        <li class="liClear" style="margin-top: 5px;">
                                                                            <label for="ddlTipoBoletoServiSecre" style="float: left;" title="Selecione o tipo do boleto bancário (Modelo 1 - Carnê; Modelo 2 - Com recibo do sacado; Modelo 3 - Com recibo do sacado e comprovante de entrega; Modelo 4 - Com recibo do sacado e valor do desconto nas instruções)">
                                                                                Tipo Boleto</label>
                                                                            <asp:DropDownList ID="ddlTipoBoletoServiSecre" Enabled="false" ToolTip="Selecione o tipo do boleto bancário (Modelo 1 - Carnê; Modelo 2 - Com recibo do sacado; Modelo 3 - Com recibo do sacado e comprovante de entrega; Modelo 4 - Com recibo do sacado e valor do desconto nas instruções)"
                                                                                Style="margin-left: 7px;" runat="server">
                                                                                <asp:ListItem Value=""></asp:ListItem>
                                                                                <asp:ListItem Value="N">Modelo 1</asp:ListItem>
                                                                                <asp:ListItem Value="C">Modelo 2</asp:ListItem>
                                                                                <asp:ListItem Value="S">Modelo 3</asp:ListItem>
                                                                                <asp:ListItem Value="M">Modelo 4</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </li>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </ul>
                                                        </li>
                                                        <li style="margin-top: 7px; margin-left: 5px;">
                                                            <ul>
                                                                <li class="liClear" style="">
                                                                    <label for="txtVlJurosSecre" style="float: left;" title="Juros Diário">
                                                                        Juros Diário</label>
                                                                    <asp:TextBox ID="txtVlJurosSecre" CssClass="txtVlJurosSecre" runat="server" MaxLength="5"
                                                                        Style="width: 30px; margin-left: 5px; margin-bottom: 0;" ToolTip="Informe o Valor do Juros"></asp:TextBox>
                                                                </li>
                                                                <li>
                                                                    <label for="ddlFlagTipoJurosSecre" style="float: left;" title="Aplicação">
                                                                        Aplicação</label>
                                                                    <asp:DropDownList ID="ddlFlagTipoJurosSecre" CssClass="ddlTipoAplic" runat="server"
                                                                        ToolTip="Informe o Tipo do Valor de Juros" Style="margin-left: 3px;">
                                                                        <asp:ListItem Value="P">%</asp:ListItem>
                                                                        <asp:ListItem Value="V">R$</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </li>
                                                                <li class="liClear" style="margin-top: 5px;">
                                                                    <label for="txtVlMultaSecre" style="float: left; width: 51px;" title="Multa">
                                                                        Multa</label>
                                                                    <asp:TextBox ID="txtVlMultaSecre" CssClass="campoMoeda" runat="server" Style="width: 30px;
                                                                        margin-left: 6px;" ToolTip="Informe o Valor da Multa"></asp:TextBox>
                                                                </li>
                                                                <li style="margin-top: 5px;">
                                                                    <label for="ddlFlagTipoMultaSecre" style="float: left;" title="Aplicação">
                                                                        Aplicação</label>
                                                                    <asp:DropDownList ID="ddlFlagTipoMultaSecre" CssClass="ddlTipoAplic" runat="server"
                                                                        ToolTip="Informe o Tipo do Valor da Multa" Style="margin-left: 3px;">
                                                                        <asp:ListItem Value="P">%</asp:ListItem>
                                                                        <asp:ListItem Value="V">R$</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </li>
                                                            </ul>
                                                        </li>
                                                    </ul>
                                                </li>
                                            </ul>
                                        </li>
                                        <%--<li class="liClear" style="margin-top: 10px;">  
                                <ul>
                                    <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Linhas de Relatório</span></li>                                                                                                                          
                                    <li class="liClear" style="margin-top: 5px;">
                                        <asp:TextBox ID="TextBox19" Enabled="false" Text="1" style="margin-bottom: 0;text-align: center; width: 15px;"
                                            CssClass="txtNumIniciSolicAuto" runat="server"></asp:TextBox>
                                    </li>  
                                     
                                    <li style="margin-top: 5px;">
                                        <asp:TextBox ID="txtCabec1Relatorio" style="margin-bottom: 0;"
                                            ToolTip="Informe o texto que será apresentado na Linha 1 do Cabeçalho do Relatório"
                                        CssClass="txtCabecalhoRelatorio" MaxLength="255" runat="server"></asp:TextBox>
                                    </li>
                                    <li class="liClear" style="margin-top: 3px;">
                                        <asp:TextBox ID="TextBox21" Enabled="false" Text="2" style="margin-bottom: 0;text-align: center; width: 15px;"
                                        runat="server"></asp:TextBox>
                                    </li> 
                                    <li style="margin-top: 3px;">
                                        <asp:TextBox ID="txtCabec2Relatorio" style="margin-bottom: 0;"
                                            ToolTip="Informe o texto que será apresentado na Linha 2 do Cabeçalho do Relatório"
                                        CssClass="txtCabecalhoRelatorio" MaxLength="255" runat="server"></asp:TextBox>                                
                                    </li>
                                    <li class="liClear" style="margin-top: 3px;">
                                        <asp:TextBox ID="TextBox23" Enabled="false" Text="3" style="margin-bottom: 0;text-align: center; width: 15px;"
                                        runat="server"></asp:TextBox>
                                    </li> 
                                    <li style="margin-top: 3px;">
                                        <asp:TextBox ID="txtCabec3Relatorio" style="margin-bottom: 0;"
                                            ToolTip="Informe o texto que será apresentado na Linha 3 do Cabeçalho do Relatório"
                                        CssClass="txtCabecalhoRelatorio" MaxLength="255" runat="server"></asp:TextBox>                                       
                                    </li>
                                    <li class="liClear" style="margin-top: 3px;">
                                        <asp:TextBox ID="TextBox25" Enabled="false" style="margin-bottom: 0;text-align: center; width: 15px;"
                                        Text="4" runat="server"></asp:TextBox>
                                    </li> 
                                    <li style="margin-top: 3px;">
                                        <asp:TextBox ID="txtCabec4Relatorio" style="margin-bottom: 0;"
                                            ToolTip="Informe o texto que será apresentado na Linha 4 do Cabeçalho do Relatório"
                                        CssClass="txtCabecalhoRelatorio" MaxLength="255" runat="server"></asp:TextBox>                                       
                                    </li>
                                    
                                </ul>
                            </li>--%>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div id="tabBibliEscol" class="tabBibliEscol" style="display: none;">
                    <ul class="ulDados">
                        <li class="liClear" style="margin-left: 130px;">
                            <label for="txtUnidadeEscolarBE" title="Razão Social">
                                Unidade Escolar</label>
                            <asp:TextBox ID="txtUnidadeEscolarBE" Enabled="false" ClientIDMode="Static" ToolTip="Informe a Razão Social"
                                CssClass="txtRazaoSocialIUE" MaxLength="80" runat="server"></asp:TextBox>
                        </li>
                        <li class="liCodIdentificacao">
                            <label for="txtCodIdenticacaoBE" title="Código de Identificação">
                                Cód. Identificação</label>
                            <asp:TextBox ID="txtCodIdenticacaoBE" Enabled="false" ClientIDMode="Static" ToolTip="Informe o Código de Identificação"
                                CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-left: 10px;">
                            <label for="txtCNPJBE" title="CNPJ">
                                Nº CNPJ</label>
                            <asp:TextBox ID="txtCNPJBE" Enabled="false" ClientIDMode="Static" ToolTip="Informe o CNPJ"
                                CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="width: 100%; text-align: center;"><span style="color: #6495ED;
                            font-size: 1.3em; font-weight: bold;">Controle Biblioteca</span></li>
                        <li class="liClear" style="margin-top: 2px; margin-left: 324px;">
                            <label for="txtTipoCtrlBibliEscol" style="float: left;" title="Tipo de Biblioteca Escolar">
                                Por</label>
                            <asp:TextBox ID="txtTipoCtrlBibliEscol" Enabled="false" Style="margin-left: 7px;
                                padding-left: 3px;" ToolTip="Tipo de Biblioteca Escolar" BackColor="#FFFFE1"
                                CssClass="txtTipoCtrlFrequ" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <ul>
                                <li style="padding-right: 10px; border-right: 1px solid #CCCCCC; height: 195px;">
                                    <ul>
                                        <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                            font-weight: bold;">Parametrização Geral</span></li>
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <li class="liControleMetodologia" style="width: 170px;">
                                                    <label title="Permite reserva de livros?">
                                                        Permite reserva?</label>
                                                    <asp:DropDownList ID="ddlFlagReserBibli" OnSelectedIndexChanged="ddlFlagReserBibli_SelectedIndexChanged"
                                                        AutoPostBack="true" ToolTip="Informe se permite reserva de livros" Style="margin-left: 49px;"
                                                        CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                        <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-top: 7px;">
                                                    <label for="txtQtdeItensReser" style="float: left;" title="Quantidade de itens para reserva">
                                                        Qtde de itens</label>
                                                    <asp:TextBox ID="txtQtdeItensReser" Style="margin-left: 11px;" ToolTip="Informe a quantidade de itens para reserva"
                                                        Enabled="false" CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                                </li>
                                                <li style="margin-top: 7px;">
                                                    <label for="txtQtdeMaxDiasReser" style="float: left;" title="Quantidade máxima de dias para reserva">
                                                        Máx. de dias</label>
                                                    <asp:TextBox ID="txtQtdeMaxDiasReser" Style="margin-left: 10px;" ToolTip="Informe a quantidade máxima de dias para reserva"
                                                        Enabled="false" CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                            <ContentTemplate>
                                                <li class="liControleMetodologia" style="width: 170px; margin-top: 5px;">
                                                    <label title="Permite empréstimo de livros?">
                                                        Permite empréstimo?</label>
                                                    <asp:DropDownList ID="ddlFlagEmpreBibli" OnSelectedIndexChanged="ddlFlagEmpreBibli_SelectedIndexChanged"
                                                        AutoPostBack="true" ToolTip="Informe se permite empréstimo de livros" Style="margin-left: 31px;"
                                                        CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                        <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-top: 5px;">
                                                    <label for="txtQtdeItensEmpre" style="float: left;" title="Quantidade de itens para empréstimo">
                                                        Qtde de itens</label>
                                                    <asp:TextBox ID="txtQtdeItensEmpre" Style="margin-left: 11px;" ToolTip="Informe a quantidade de itens para empréstimo"
                                                        Enabled="false" CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                                </li>
                                                <li style="margin-top: 5px;">
                                                    <label for="txtQtdeMaxDiasEmpre" style="float: left;" title="Quantidade máxima de dias para empréstimo">
                                                        Máx. de dias</label>
                                                    <asp:TextBox ID="txtQtdeMaxDiasEmpre" Style="margin-left: 10px;" ToolTip="Informe a quantidade máxima de dias para empréstimo"
                                                        Enabled="false" CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                                            <ContentTemplate>
                                                <li class="liControleMetodologia" style="width: 170px; margin-top: 5px;">
                                                    <label title="Gera nº empréstimo automático?">
                                                        Gera nº emp. automático?</label>
                                                    <asp:DropDownList ID="ddlGeraNumEmpreAuto" OnSelectedIndexChanged="ddlGeraNumEmpreAuto_SelectedIndexChanged"
                                                        AutoPostBack="true" ToolTip="Informe se gera nº de empréstimo automático" Style="margin-left: 6px;"
                                                        CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                        <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-top: 5px;">
                                                    <label for="txtNumIniciEmpreAuto" style="float: left;" title="Número Inicial de Empréstimo Automática">
                                                        Nº Inicial</label>
                                                    <asp:TextBox ID="txtNumIniciEmpreAuto" Enabled="false" Style="margin-left: 29px;
                                                        margin-bottom: 0; text-align: right;" ToolTip="Informe o Número Inicial de Empréstimo Automático"
                                                        CssClass="txtNumIniciSolicAuto" runat="server"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                            <ContentTemplate>
                                                <li class="liControleMetodologia" style="width: 170px; margin-top: 5px;">
                                                    <label title="Cobra taxa de empréstimo?">
                                                        Cobra taxa de empréstimo?</label>
                                                    <asp:DropDownList ID="ddlFlagTaxaEmpre" OnSelectedIndexChanged="ddlFlagTaxaEmpre_SelectedIndexChanged"
                                                        AutoPostBack="true" ToolTip="Informe se cobra taxa de empréstimo" Style="margin-left: 1px;"
                                                        CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                        <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-top: 5px;">
                                                    <label for="txtDiasBonusTaxaEmpre" style="float: left;" title="Quantidade de dias bônus para taxa de empréstimo">
                                                        Dias de bônus</label>
                                                    <asp:TextBox ID="txtDiasBonusTaxaEmpre" Style="margin-left: 7px;" ToolTip="Informe a quantidade de dias bônus para taxa de empréstimo"
                                                        Enabled="false" CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                                </li>
                                                <li style="margin-top: 5px;">
                                                    <label for="txtValorTaxaEmpre" style="float: left;" title="Valor da taxa do empréstimo">
                                                        Valor diário</label>
                                                    <asp:TextBox ID="txtValorTaxaEmpre" Style="margin-left: 7px; margin-bottom: 0; width: 25px;"
                                                        ToolTip="Informe o valor da taxa do empréstimo" Enabled="false" CssClass="campoMoeda"
                                                        MaxLength="4" runat="server"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                            <ContentTemplate>
                                                <li class="liControleMetodologia" style="width: 170px; margin-top: 5px;">
                                                    <label title="Multa de atraso devolução?">
                                                        Multa de atraso devolução?</label>
                                                    <asp:DropDownList ID="ddlFlagMultaAtraso" OnSelectedIndexChanged="ddlFlagMultaAtraso_SelectedIndexChanged"
                                                        AutoPostBack="true" ToolTip="Informe se cobra taxa de empréstimo" CssClass="ddlAlterPrazoEntreSolic"
                                                        runat="server">
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                        <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-top: 5px;">
                                                    <label for="txtDiasBonusMultaEmpre" style="float: left;" title="Quantidade de dias bônus para multa de empréstimo">
                                                        Dias de bônus</label>
                                                    <asp:TextBox ID="txtDiasBonusMultaEmpre" Style="margin-left: 7px;" ToolTip="Informe a quantidade de dias bônus para multa de empréstimo"
                                                        Enabled="false" CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                                </li>
                                                <li style="margin-top: 5px;">
                                                    <label for="txtValorTaxaEmpre" style="float: left;" title="Valor da multa do empréstimo">
                                                        Valor diário</label>
                                                    <asp:TextBox ID="txtValorMultaEmpre" Style="margin-left: 7px; margin-bottom: 0; width: 25px;"
                                                        ToolTip="Informe o valor da multa do empréstimo" Enabled="false" CssClass="campoMoeda"
                                                        MaxLength="4" runat="server"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                            <ContentTemplate>
                                                <li class="liControleMetodologia" style="width: 170px; margin-top: 5px;">
                                                    <label title="Gera nº item automático?">
                                                        Gera nº item automático?</label>
                                                    <asp:DropDownList ID="ddlGeraNumItemAuto" OnSelectedIndexChanged="ddlGeraNumItemAuto_SelectedIndexChanged"
                                                        AutoPostBack="true" ToolTip="Informe se gera nº de item automático" Style="margin-left: 10px;"
                                                        CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                        <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-top: 5px;">
                                                    <label for="txtNumIniciItemAuto" style="float: left;" title="Número Inicial de Item Automática">
                                                        Nº Inicial</label>
                                                    <asp:TextBox ID="txtNumIniciItemAuto" Enabled="false" Style="margin-left: 29px; margin-bottom: 0;
                                                        text-align: right;" ToolTip="Informe o Número Inicial de Solicitação Automática"
                                                        CssClass="txtNumIniciSolicAuto" runat="server"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <li class="liClear" style="margin-top: 5px;">
                                            <label for="ddlNumISBNObrig" style="float: left;" title="Nº de ISBN é obrigatório?">
                                                Nº de ISBN é obrigatório?</label>
                                            <asp:DropDownList ID="ddlNumISBNObrig" Style="margin-left: 8px;" ToolTip="Informe se nº de ISBN é obrigatório"
                                                runat="server" CssClass="ddlAlterPrazoEntreSolic">
                                                <asp:ListItem Value="N" Selected="true">Não</asp:ListItem>
                                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                        <li class="liClear" style="margin-top: 5px;">
                                            <label for="ddlFlarIncluContaRecebBibli" style="float: left;" title="Inclui taxas no contas a receber?">
                                                Inclui taxas no CAR?</label>
                                            <asp:DropDownList ID="ddlFlarIncluContaRecebBibli" Style="margin-left: 33px;" ToolTip="Informe se inclui no contas a receber"
                                                runat="server" CssClass="ddlAlterPrazoEntreSolic">
                                                <asp:ListItem Value="N" Selected="true">Não</asp:ListItem>
                                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                    </ul>
                                </li>
                                <li style="width: 370px; padding-left: 5px; margin-right: 0;">
                                    <ul>
                                        <li style="padding-right: 10px; border-right: 1px solid #CCCCCC; height: 105px;">
                                            <ul>
                                                <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                                    font-weight: bold;">Usuários de Biblioteca</span></li>
                                                <li class="liRecuperacao liClear" style="margin-top: 6px; margin-left: -5px;">
                                                    <asp:CheckBox ID="chkUsuarFuncBibli" ToolTip="Informe se existirá usuário Funcionário"
                                                        runat="server" Text="Funcionários" />
                                                </li>
                                                <li class="liRecuperacao liClear" style="margin-left: -5px;">
                                                    <asp:CheckBox ID="chkUsuarProfBibli" ToolTip="Informe se existirá usuário Professor"
                                                        runat="server" Text="Professores" />
                                                </li>
                                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                    <ContentTemplate>
                                                        <li class="liRecuperacao liClear" style="margin-left: -5px;">
                                                            <asp:CheckBox ID="chkUsuarAlunoBibli" ToolTip="Informe se existirá usuário Aluno"
                                                                runat="server" OnCheckedChanged="chkUsuarAlunoBibli_CheckedChanged" AutoPostBack="true"
                                                                Text="Alunos" />
                                                        </li>
                                                        <li style="margin-left: 4px;">
                                                            <label for="txtIdadeMinimAlunoBibli" style="float: left;" title="Idade Mínima para Aluno">
                                                                Idade Mínima</label>
                                                            <asp:TextBox ID="txtIdadeMinimAlunoBibli" Style="margin-left: 7px;" ToolTip="Informe a Idade Mínima do aluno"
                                                                Enabled="false" CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                                        </li>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <li class="liRecuperacao liClear" style="margin-left: -5px;">
                                                    <asp:CheckBox ID="chkUsuarRespBibli" ToolTip="Informe se existirá usuário Pais/Responsáveis"
                                                        runat="server" Text="Pais/Responsáveis" />
                                                </li>
                                                <li class="liRecuperacao liClear" style="margin-left: -5px;">
                                                    <asp:CheckBox ID="chkUsuarOutroBibli" ToolTip="Informe se existirá usuário Outros"
                                                        runat="server" Text="Outros" />
                                                </li>
                                            </ul>
                                        </li>
                                        <li style="padding-left: 5px;">
                                            <ul>
                                                <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                                    font-weight: bold;">Horário de Atividades</span></li>
                                                <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                                                    <ContentTemplate>
                                                        <li class="liRecuperacao liClear" style="margin-left: -5px; margin-bottom: 3px; margin-top: 7px;">
                                                            <asp:CheckBox ID="chkHorAtiBibli" ToolTip="Informe se utiliza mesmo horário da Unidade"
                                                                runat="server" Text="Utiliza mesmo horário da Unidade" ForeColor="#006699" OnCheckedChanged="chkHorAtiBibli_CheckedChanged"
                                                                AutoPostBack="true" />
                                                        </li>
                                                        <li class="liClear">
                                                            <ul>
                                                                <li><span title="Turno 1">Turno 1</span></li>
                                                                <li class="liClear">
                                                                    <asp:TextBox ID="txtHorarIniT1Bibli" Style="margin-bottom: 3px;" ToolTip="Informe o Horário Inicial do Turno 1"
                                                                        CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                                                                </li>
                                                                <li><span>às </span></li>
                                                                <li>
                                                                    <asp:TextBox ID="txtHorarFimT1Bibli" Style="margin-bottom: 3px;" ToolTip="Informe o Horário Final do Turno 1"
                                                                        CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                                                                </li>
                                                            </ul>
                                                        </li>
                                                        <li>
                                                            <ul>
                                                                <li><span title="Turno 2">Turno 2</span></li>
                                                                <li class="liClear">
                                                                    <asp:TextBox ID="txtHorarIniT2Bibli" Style="margin-bottom: 3px;" ToolTip="Informe o Horário Inical do Turno 2"
                                                                        CssClass="txtHorarioFuncTarde" runat="server"></asp:TextBox>
                                                                </li>
                                                                <li><span>às </span></li>
                                                                <li>
                                                                    <asp:TextBox ID="txtHorarFimT2Bibli" Style="margin-bottom: 3px;" ToolTip="Informe o Horário Final do Turno 2"
                                                                        CssClass="txtHorarioFuncTarde" runat="server"></asp:TextBox>
                                                                </li>
                                                            </ul>
                                                        </li>
                                                        <li class="liClear">
                                                            <ul>
                                                                <li class="liClear" title="Turno 3"><span>Turno 3</span></li>
                                                                <li class="liClear">
                                                                    <asp:TextBox ID="txtHorarIniT3Bibli" Style="margin-bottom: 3px;" ToolTip="Informe o Horário Inicial do Turno 3"
                                                                        CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                                                </li>
                                                                <li><span>às </span></li>
                                                                <li>
                                                                    <asp:TextBox ID="txtHorarFimT3Bibli" Style="margin-bottom: 3px;" ToolTip="Informe o Horário Final do Turno 3"
                                                                        CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                                                </li>
                                                            </ul>
                                                        </li>
                                                        <li>
                                                            <ul>
                                                                <li class="liClear" title="Turno 4"><span>Turno 4</span></li>
                                                                <li class="liClear">
                                                                    <asp:TextBox ID="txtHorarIniT4Bibli" Style="margin-bottom: 3px;" ToolTip="Informe o Horário Inicial do Turno 4"
                                                                        CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                                                </li>
                                                                <li><span>às </span></li>
                                                                <li>
                                                                    <asp:TextBox ID="txtHorarFimT4Bibli" Style="margin-bottom: 3px;" ToolTip="Informe o Horário Final do Turno 4"
                                                                        CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                                                </li>
                                                            </ul>
                                                        </li>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ul>
                                        </li>
                                        <li class="liClear" style="margin-top: 7px; width: 360px;">
                                            <ul>
                                                <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                                    font-weight: bold;">Bibliotecário(a)</span></li>
                                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                    <ContentTemplate>
                                                        <li class="liClear" style="margin-top: 5px;">
                                                            <label for="ddlSiglaUnidBibliEscol" title="Unidade" style="color: #006699;">
                                                                Unidade</label>
                                                            <asp:DropDownList ID="ddlSiglaUnidBibliEscol1" AutoPostBack="true" runat="server"
                                                                ToolTip="Selecione a Sigla da Unidade Escolar" CssClass="ddlSiglaUnidSecreEscol"
                                                                OnSelectedIndexChanged="ddlSiglaUnidBibliEscol1_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </li>
                                                        <li style="margin-left: 5px; margin-top: 5px;">
                                                            <label for="ddlNomeBibliEscol" title="Nome Bibliotecário Escolar" style="color: #006699;">
                                                                Nome Bibliotecário Escolar</label>
                                                            <asp:DropDownList ID="ddlNomeBibliEscol1" runat="server" ToolTip="Selecione o Nome Bibliotecário(a)"
                                                                CssClass="ddlNomeSecreEscol" />
                                                        </li>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <li style="margin-right: 0px; margin-top: 5px;">
                                                    <label for="ddlClassifBibli1" title="Classificação do Bibliotecário" style="color: #006699;">
                                                        Classif.</label>
                                                    <asp:DropDownList ID="ddlClassifBibli1" runat="server" ToolTip="Selecione a classificação de bibliotecário"
                                                        CssClass="ddlQtdeSecretario">
                                                        <asp:ListItem Selected="True" Value="1">1º</asp:ListItem>
                                                        <asp:ListItem Value="2">2º</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                                                    <ContentTemplate>
                                                        <li class="liClear" style="margin-top: 5px;">
                                                            <asp:DropDownList ID="ddlSiglaUnidBibliEscol2" AutoPostBack="true" runat="server"
                                                                ToolTip="Selecione a Sigla da Unidade Escolar" CssClass="ddlSiglaUnidSecreEscol"
                                                                OnSelectedIndexChanged="ddlSiglaUnidBibliEscol2_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </li>
                                                        <li style="margin-left: 5px; margin-top: 5px;">
                                                            <asp:DropDownList ID="ddlNomeBibliEscol2" runat="server" ToolTip="Selecione o Nome Bibliotecário(a)"
                                                                CssClass="ddlNomeSecreEscol" />
                                                        </li>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <li style="margin-right: 0px; margin-top: 5px;">
                                                    <asp:DropDownList ID="ddlClassifBibli2" runat="server" ToolTip="Selecione a classificação de bibliotecário"
                                                        CssClass="ddlQtdeSecretario">
                                                        <asp:ListItem Value="1">1º</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="2">2º</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div id="tabContabil" class="tabContabil" style="display: none;">
                    <ul class="ulDados">
                        <li class="liClear" style="margin-left: 130px;">
                            <label for="txtUnidadeEscolarCO" title="Razão Social">
                                Unidade Escolar</label>
                            <asp:TextBox ID="txtUnidadeEscolarCO" Enabled="false" ClientIDMode="Static" ToolTip="Informe a Razão Social"
                                CssClass="txtRazaoSocialIUE" MaxLength="80" runat="server"></asp:TextBox>
                        </li>
                        <li class="liCodIdentificacao">
                            <label for="txtCodIdenticacaoCO" title="Código de Identificação">
                                Cód. Identificação</label>
                            <asp:TextBox ID="txtCodIdenticacaoCO" Enabled="false" ClientIDMode="Static" ToolTip="Informe o Código de Identificação"
                                CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-left: 10px;">
                            <label for="txtCNPJCO" title="CNPJ">
                                Nº CNPJ</label>
                            <asp:TextBox ID="txtCNPJCO" Enabled="false" ClientIDMode="Static" ToolTip="Informe o CNPJ"
                                CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="width: 100%; text-align: center;"><span style="color: #6495ED;
                            font-size: 1.3em; font-weight: bold;">Controle Contábil</span></li>
                        <li class="liClear" style="margin-top: 2px; margin-left: 324px;">
                            <label for="txtTipoCtrlContabil" style="float: left;" title="Tipo de Controle Contábil">
                                Por</label>
                            <asp:TextBox ID="txtTipoCtrlContabil" Enabled="false" Style="margin-left: 7px; padding-left: 3px;"
                                ToolTip="Tipo de Controle Contábil" BackColor="#FFFFE1" CssClass="txtTipoCtrlFrequ"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <ul>
                                <li style="padding-right: 10px; border-right: 1px solid #CCCCCC;">
                                    <ul>
                                        <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                            font-weight: bold;">Taxas</span></li>
                                        <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                                            <ContentTemplate>
                                                <li class="liClear" style="margin-top: 5px;">
                                                    <label style="color: #006699;" title="Item">
                                                        ITEM</label>
                                                    <label style="float: left;" title="Taxa Serviço Secretaria">
                                                        Tx Serviço</label>
                                                </li>
                                                <li style="margin-left: 5px; width: 205px; margin-top: 5px;">
                                                    <label style="color: #006699;" title="CONTA CONTÁBIL (Tipo/Grupo/SubGrupo/Conta)">
                                                        CONTA CONTÁBIL (Tp/Grp/SGrp/SGrp2/Cta)</label>
                                                    <asp:TextBox ID="TextBox11" Enabled="false" Style="width: 10px; margin-bottom: 0;"
                                                        ToolTip="Crédito" runat="server">C</asp:TextBox>
                                                    <asp:DropDownList ID="ddlGrupoTxServSecre" runat="server" Width="35px" ToolTip="Selecione o Grupo de Receita"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoTxServSecre_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator17" ControlToValidate="ddlGrupoTxServSecre"
                                                        runat="server" ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil da Tx Serv. Secretaria"
                                                        Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvContaContabilTxServSecre_ServerValidate">
                                                    </asp:CustomValidator>
                                                    <asp:DropDownList ID="ddlSubGrupoTxServSecre" runat="server" Width="40px" ToolTip="Selecione o Subgrupo"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoTxServSecre_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlSubGrupo2TxServSecre" runat="server" Width="40px" ToolTip="Selecione o Subgrupo 2"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupo2TxServSecre_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlContaContabilTxServSecre" ToolTip="Selecione a Conta Contábil"
                                                        runat="server" Width="45px" AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilTxServSecre_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 5px;">
                                                    <label style="color: #006699;" title="CENTRO DE CUSTO">
                                                        CENTRO DE CUSTO</label>
                                                    <asp:DropDownList ID="ddlCentroCustoTxServSecre" Width="93px" ToolTip="Selecione o Centro de Custo"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </li>
                                                <li class="liClear" style="margin-top: 2px;">
                                                    <label style="float: left; margin-top: -5px;" title="Taxa Serviço Secretaria">
                                                        Secretaria</label>
                                                    <asp:TextBox ID="txtCtaContabTxServSecre" Enabled="false" Style="width: 250px; margin-bottom: 0;
                                                        margin-left: 14px;" ToolTip="Taxas de Serviços de Secretaria" runat="server"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                                            <ContentTemplate>
                                                <li class="liClear" style="margin-top: 10px;">
                                                    <label style="float: left;" title="Taxa Serviço Biblioteca">
                                                        Tx Serviço</label>
                                                </li>
                                                <li style="margin-left: 5px; width: 205px; margin-top: 10px;">
                                                    <asp:TextBox ID="TextBox12" Enabled="false" Style="width: 10px; margin-bottom: 0;"
                                                        ToolTip="Crédito" runat="server">C</asp:TextBox>
                                                    <asp:DropDownList ID="ddlGrupoTxServBibli" runat="server" Width="35px" ToolTip="Selecione o Grupo de Receita"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoTxServBibli_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator16" ControlToValidate="ddlGrupoTxServBibli"
                                                        runat="server" ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil de Biblioteca"
                                                        Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvContaContabilTxServBibli_ServerValidate">
                                                    </asp:CustomValidator>
                                                    <asp:DropDownList ID="ddlSubGrupoTxServBibli" runat="server" Width="40px" ToolTip="Selecione o Subgrupo"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoTxServBibli_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlSubGrupo2TxServBibli" runat="server" Width="40px" ToolTip="Selecione o Subgrupo 2"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupo2TxServBibli_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlContaContabilTxServBibli" ToolTip="Selecione a Conta Contábil"
                                                        runat="server" Width="45px" AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilTxServBibli_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 10px;">
                                                    <asp:DropDownList ID="ddlCentroCustoTxServBibli" Width="93px" ToolTip="Selecione o Centro de Custo"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </li>
                                                <li class="liClear" style="margin-top: 2px;">
                                                    <label style="float: left; margin-top: -5px;" title="Taxa Serviço Biblioteca">
                                                        Biblioteca</label>
                                                    <asp:TextBox ID="txtCtaContabTxServBibli" Enabled="false" Style="width: 250px; margin-bottom: 0;
                                                        margin-left: 16px;" ToolTip="Taxas de Serviços de Biblioteca Escolar" runat="server"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                                            <ContentTemplate>
                                                <li class="liClear" style="margin-top: 10px;">
                                                    <label style="float: left;" title="Tx Negociação de Débito">
                                                        Tx Negoc.</label>
                                                </li>
                                                <li style="margin-left: 7px; width: 205px; margin-top: 10px;">
                                                    <asp:TextBox ID="TextBox13" Enabled="false" Style="width: 10px; margin-bottom: 0;"
                                                        ToolTip="Crédito" runat="server">C</asp:TextBox>
                                                    <asp:DropDownList ID="ddlGrupoTxMatri" runat="server" Width="35px" ToolTip="Selecione o Grupo de Receita"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoTxMatri_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator15" ControlToValidate="ddlGrupoTxMatri" runat="server"
                                                        ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil de Negociação de Débito"
                                                        Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvContaContabilTxMatri_ServerValidate">
                                                    </asp:CustomValidator>
                                                    <asp:DropDownList ID="ddlSubGrupoTxMatri" runat="server" Width="40px" ToolTip="Selecione o Subgrupo"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoTxMatri_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlSubGrupo2TxMatri" runat="server" Width="40px" ToolTip="Selecione o Subgrupo 2"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupo2TxMatri_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlContaContabilTxMatri" ToolTip="Selecione a Conta Contábil"
                                                        runat="server" Width="45px" AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilTxMatri_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 10px;">
                                                    <asp:DropDownList ID="ddlCentroCustoTxMatri" runat="server" Width="93px" ToolTip="Selecione o Centro de Custo">
                                                    </asp:DropDownList>
                                                </li>
                                                <li class="liClear" style="margin-top: 2px;">
                                                    <label style="float: left; margin-top: -5px;" title="Negociação de Débito">
                                                        De Débito</label>
                                                    <asp:TextBox ID="txtCtaContabTxMatri" Enabled="false" Style="width: 250px; margin-bottom: 0;
                                                        margin-left: 13px;" ToolTip="Taxas Contratuais de Negociação de Débito" runat="server"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel47" runat="server">
                                            <ContentTemplate>
                                                <li class="liClear" style="margin-top: 10px;">
                                                    <label style="float: left;" title="Atividades Extras">
                                                        Atividades</label>
                                                </li>
                                                <li style="margin-left: 7px; width: 205px; margin-top: 10px;">
                                                    <asp:TextBox ID="TextBox15" Enabled="false" Style="width: 10px; margin-bottom: 0;"
                                                        ToolTip="Crédito" runat="server">C</asp:TextBox>
                                                    <asp:DropDownList ID="ddlGrupoAtiviExtra" runat="server" Width="35px" ToolTip="Selecione o Grupo de Receita"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoAtiviExtra_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator19" ControlToValidate="ddlGrupoAtiviExtra"
                                                        runat="server" ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil de Atividade Extra"
                                                        Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvContaContabilAtividaExtra_ServerValidate">
                                                    </asp:CustomValidator>
                                                    <asp:DropDownList ID="ddlSubGrupoAtiviExtra" runat="server" Width="40px" ToolTip="Selecione o Subgrupo"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoAtiviExtra_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlSubGrupo2AtiviExtra" runat="server" Width="40px" ToolTip="Selecione o Subgrupo 2"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupo2AtiviExtra_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlContaContabilAtiviExtra" ToolTip="Selecione a Conta Contábil"
                                                        runat="server" Width="45px" AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilAtiviExtra_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 10px;">
                                                    <asp:DropDownList ID="ddlCentroCustoAtiviExtra" runat="server" Width="93px" ToolTip="Selecione o Centro de Custo">
                                                    </asp:DropDownList>
                                                </li>
                                                <li class="liClear" style="margin-top: 2px;">
                                                    <label style="float: left; margin-top: -5px;" title="Atividades Extras">
                                                        Extras</label>
                                                    <asp:TextBox ID="txtCtaContabAtiviExtra" Enabled="false" Style="width: 250px; margin-bottom: 0;
                                                        margin-left: 31px;" ToolTip="Mensalidades de Atividades Extra Classe" runat="server"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel46" runat="server">
                                            <ContentTemplate>
                                                <li class="liClear" style="margin-top: 10px; margin-right: 7px;">
                                                    <label style="float: left;" title="Conta Contábil de Caixa">
                                                        Conta Caixa</label>
                                                </li>
                                                <li style="margin-left: -1px; width: 205px; margin-top: 10px;">
                                                    <asp:TextBox ID="TextBox14" Enabled="false" Style="width: 10px; margin-bottom: 0;"
                                                        ToolTip="Crédito" runat="server">C</asp:TextBox>
                                                    <asp:DropDownList ID="ddlGrupoContaCaixa" runat="server" Width="35px" ToolTip="Selecione o Grupo de Receita"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoContaCaixa_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator18" ControlToValidate="ddlGrupoContaCaixa"
                                                        runat="server" ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil de Caixa"
                                                        Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvContaContabilCaixa_ServerValidate">
                                                    </asp:CustomValidator>
                                                    <asp:DropDownList ID="ddlSubGrupoContaCaixa" runat="server" Width="40px" ToolTip="Selecione o Subgrupo"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoContaCaixa_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlSubGrupo2ContaCaixa" runat="server" Width="40px" ToolTip="Selecione o Subgrupo 2"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupo2ContaCaixa_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlContaContabilContaCaixa" ToolTip="Selecione a Conta Contábil"
                                                        runat="server" Width="45px" AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilContaCaixa_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 10px;">
                                                    <asp:DropDownList ID="ddlCentroCustoContaCaixa" runat="server" Width="93px" ToolTip="Selecione o Centro de Custo">
                                                    </asp:DropDownList>
                                                </li>
                                                <li class="liClear" style="margin-top: 2px; margin-left: 58px;">
                                                    <asp:TextBox ID="txtCtaContabCaixa" Enabled="false" Style="width: 250px; margin-bottom: 0;"
                                                        ToolTip="Conta Contábil de Caixa" runat="server"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                            <ContentTemplate>
                                                <li class="liClear" style="margin-top: 10px; margin-right: 3px;">
                                                    <label style="float: left;" title="Conta Contábil de Banco">
                                                        Conta Banco</label>
                                                </li>
                                                <li style="margin-left: -1px; width: 205px; margin-top: 10px;">
                                                    <asp:TextBox ID="TextBox1" Enabled="false" Style="width: 10px; margin-bottom: 0;"
                                                        ToolTip="Crédito" runat="server">C</asp:TextBox>
                                                    <asp:DropDownList ID="ddlGrupoContaBanco" runat="server" Width="35px" ToolTip="Selecione o Grupo de Receita"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoContaBanco_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator20" ControlToValidate="ddlGrupoContaBanco"
                                                        runat="server" ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil de Banco"
                                                        Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvContaContabilBanco_ServerValidate">
                                                    </asp:CustomValidator>
                                                    <asp:DropDownList ID="ddlSubGrupoContaBanco" runat="server" Width="40px" ToolTip="Selecione o Subgrupo"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoContaBanco_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlSubGrupo2ContaBanco" runat="server" Width="40px" ToolTip="Selecione o Subgrupo 2"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupo2ContaBanco_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlContaContabilContaBanco" ToolTip="Selecione a Conta Contábil"
                                                        runat="server" Width="45px" AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilContaBanco_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-left: 5px; margin-top: 10px;">
                                                    <asp:DropDownList ID="ddlCentroCustoContaBanco" runat="server" Width="93px" ToolTip="Selecione o Centro de Custo">
                                                    </asp:DropDownList>
                                                </li>
                                                <li class="liClear" style="margin-top: 2px; margin-left: 58px;">
                                                    <asp:TextBox ID="txtCtaContabBanco" Enabled="false" Style="width: 250px; margin-bottom: 0;"
                                                        ToolTip="Conta Contábil de Banco" runat="server"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ul>
                                </li>
                                <%-- 
                    <li style="padding-left: 5px;">
                        <ul>                            
                            <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Gestão de Matrículas</span></li>
                            <asp:UpdatePanel ID="UpdatePanel43" runat="server">                     
                            <ContentTemplate>
                            <li class="liClear" style="margin-top: 5px;">
                                <label style="color:#006699;" title="Item">ITEM</label>
                                <label style="float:left;" title="Taxa Matrícula Nova">Tx Matrícula</label>
                            </li> 
                            <li style="margin-left: 8px;width: 182px;margin-top: 5px;">
                                <label style="color:#006699;" title="CONTA CONTÁBIL (Tipo/Grupo/SubGrupo/Conta)">CONTA CONTÁBIL (Tp/Grp/SGrp/Cta)</label>
                                <asp:TextBox ID="TextBox16" Enabled="false" style="width: 10px; margin-bottom: 0;"
                                    ToolTip="Crédito" runat="server">C</asp:TextBox>

                                <asp:DropDownList ID="ddlGrupoTxMatricNova" runat="server" Width="40px"
                                    ToolTip="Selecione o Grupo de Receita"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoTxMatricNova_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator20" ControlToValidate="ddlGrupoTxMatricNova" runat="server" 
                                    ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil de Matrícula Nova"
                                    Display="None" CssClass="validatorField" 
                                    EnableClientScript="false" OnServerValidate="cvContaContabilTxMatricNova_ServerValidate">
                                </asp:CustomValidator>

                                <asp:DropDownList ID="ddlSubGrupoTxMatricNova" runat="server" Width="45px"
                                    ToolTip="Selecione o Subgrupo"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoTxMatricNova_SelectedIndexChanged">
                                </asp:DropDownList>

                                <asp:DropDownList ID="ddlContaContabilTxMatricNova" ToolTip="Selecione a Conta Contábil" runat="server" Width="49px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilTxMatricNova_SelectedIndexChanged">
                                </asp:DropDownList>
                            </li> 
                            <li style="margin-left: 5px;margin-top: 5px;">
                                <label style="color:#006699;" title="CENTRO DE CUSTO">CENTRO DE CUSTO</label>
                                <asp:DropDownList ID="ddlCentroCustoTxMatricNova" Width="93px" ToolTip="Selecione o Centro de Custo" runat="server">
                                </asp:DropDownList>
                            </li>
                            <li class="liClear" style="margin-top: 2px;">
                                <label style="float:left; margin-top: -5px;" title="Taxa Matrícula Nova">Nova</label>
                                <asp:TextBox ID="txtCtaContabTxMatricNova" Enabled="false" style="width: 250px;margin-bottom: 0;margin-left: 43px;"
                                    ToolTip="Taxas de Serviços de Secretaria" runat="server"></asp:TextBox>
                            </li> 
                            </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="UpdatePanel44" runat="server">                     
                            <ContentTemplate>
                            <li class="liClear" style="margin-top: 10px;">
                                <label style="float:left;" title="Tx Renovação">Tx Renovação</label>
                            </li> 
                            <li style="margin-left: -2px;width: 182px;margin-top: 10px;">
                                <asp:TextBox ID="TextBox17" Enabled="false" style="width: 10px; margin-bottom: 0;"
                                    ToolTip="Crédito" runat="server">C</asp:TextBox>

                                <asp:DropDownList ID="ddlGrupoTxRenov" runat="server" Width="40px"
                                    ToolTip="Selecione o Grupo de Receita"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoTxRenov_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator21" ControlToValidate="ddlGrupoTxRenov" runat="server" 
                                    ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil de Renovação de Matrícula"
                                    Display="None" CssClass="validatorField" 
                                    EnableClientScript="false" OnServerValidate="cvContaContabilTxRenov_ServerValidate">
                                </asp:CustomValidator>

                                <asp:DropDownList ID="ddlSubGrupoTxRenov" runat="server" Width="45px"
                                    ToolTip="Selecione o Subgrupo"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoTxRenov_SelectedIndexChanged">
                                </asp:DropDownList>

                                <asp:DropDownList ID="ddlContaContabilTxRenov" ToolTip="Selecione a Conta Contábil" runat="server" Width="49px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilTxRenov_SelectedIndexChanged">
                                </asp:DropDownList>
                            </li> 
                            <li style="margin-left: 5px;margin-top: 10px;">
                                <asp:DropDownList ID="ddlCentroCustoTxRenov" Width="93px" ToolTip="Selecione o Centro de Custo" runat="server">
                                </asp:DropDownList>
                            </li>
                            <li class="liClear" style="margin-top: 2px;margin-left: 67px;">
                                <asp:TextBox ID="txtCtaContabTxRenov" Enabled="false" style="width: 250px;margin-bottom: 0;"
                                    ToolTip="Outras taxas de serviços de Secretaria" runat="server"></asp:TextBox>
                            </li> 
                            </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel ID="UpdatePanel45" runat="server">                     
                            <ContentTemplate>
                            <li class="liClear" style="margin-top: 10px;">
                                <label style="float:left;" title="Taxa Serviço Secretaria">Tx Serviço</label>
                            </li> 
                            <li style="margin-left: 14px;width: 182px;margin-top: 10px;">
                                <asp:TextBox ID="TextBox18" Enabled="false" style="width: 10px; margin-bottom: 0;"
                                    ToolTip="Crédito" runat="server">C</asp:TextBox>

                                <asp:DropDownList ID="ddlGrupoTxServSecreMatri" runat="server" Width="40px"
                                    ToolTip="Selecione o Grupo de Receita"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoTxServSecreMatri_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator22" ControlToValidate="ddlGrupoTxServSecreMatri" runat="server" 
                                    ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil de Serviço de Secretaria da Gestão de Matrícula"
                                    Display="None" CssClass="validatorField" 
                                    EnableClientScript="false" OnServerValidate="cvContaContabilTxServSecreMatri_ServerValidate">
                                </asp:CustomValidator>

                                <asp:DropDownList ID="ddlSubGrupoTxServSecreMatri" runat="server" Width="45px"
                                    ToolTip="Selecione o Subgrupo"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoTxServSecreMatri_SelectedIndexChanged">
                                </asp:DropDownList>

                                <asp:DropDownList ID="ddlContaContabilTxServSecreMatri" ToolTip="Selecione a Conta Contábil" runat="server" Width="49px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilTxServSecreMatri_SelectedIndexChanged">
                                </asp:DropDownList>
                            </li> 
                            <li style="margin-left: 5px;margin-top: 10px;">
                                <asp:DropDownList ID="ddlCentroCustoTxServSecreMatri" runat="server" Width="93px" ToolTip="Selecione o Centro de Custo">
                                </asp:DropDownList>
                            </li>
                            <li class="liClear" style="margin-top: 2px;">
                                <label style="float:left; margin-top: -5px;" title="Taxa Serviço Secretaria">Secretaria</label>
                                <asp:TextBox ID="txtCtaContabTxServSecreMatri" Enabled="false" style="width:250px;margin-bottom: 0;margin-left: 23px;"
                                    ToolTip="Taxas de Serviços de Secretaria" runat="server"></asp:TextBox>
                            </li>                                    
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </ul>
                    </li> --%>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div id="tabMensaSMS" class="tabMensaSMS" style="display: none;">
                    <ul class="ulDados">
                        <li class="liClear" style="margin-left: 130px;">
                            <label for="txtUnidadeEscolarMS" title="Razão Social">
                                Unidade Escolar</label>
                            <asp:TextBox ID="txtUnidadeEscolarMS" Enabled="false" ClientIDMode="Static" ToolTip="Informe a Razão Social"
                                CssClass="txtRazaoSocialIUE" MaxLength="80" runat="server"></asp:TextBox>
                        </li>
                        <li class="liCodIdentificacao">
                            <label for="txtCodIdenticacaoMS" title="Código de Identificação">
                                Cód. Identificação</label>
                            <asp:TextBox ID="txtCodIdenticacaoMS" Enabled="false" ClientIDMode="Static" ToolTip="Informe o Código de Identificação"
                                CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-left: 10px;">
                            <label for="txtCNPJMS" title="CNPJ">
                                Nº CNPJ</label>
                            <asp:TextBox ID="txtCNPJMS" Enabled="false" ClientIDMode="Static" ToolTip="Informe o CNPJ"
                                CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="width: 100%; text-align: center;"><span style="color: #6495ED;
                            font-size: 1.3em; font-weight: bold;">Controle de Mensagens SMS</span></li>
                        <li class="liClear" style="margin-top: 2px; margin-left: 324px;">
                            <label for="txtTipoCtrlMensagSMS" style="float: left;" title="Tipo de Biblioteca Escolar">
                                Por</label>
                            <asp:TextBox ID="txtTipoCtrlMensagSMS" Enabled="false" Style="margin-left: 7px; padding-left: 3px;"
                                ToolTip="Tipo de Secretaria Escolar" BackColor="#FFFFE1" CssClass="txtTipoCtrlFrequ"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <ul>
                                <li style="padding-right: 10px; border-right: 1px solid #CCCCCC;">
                                    <ul>
                                        <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                            font-weight: bold;">Secretaria Escolar</span></li>
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                                    <asp:CheckBox ID="chkSMSSolicSecreEscol" ToolTip="Informe se enviará SMS para uma solicitação da Secretaria Escolar"
                                                        runat="server" OnCheckedChanged="chkSMSSolicSecreEscol_CheckedChanged" AutoPostBack="true"
                                                        Text="Solicitação" />
                                                </li>
                                                <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                                    <label for="ddlFlagSMSSolicEnvAuto" style="float: left;" title="Envio automático">
                                                        Env.Aut.:</label>
                                                    <asp:DropDownList ID="ddlFlagSMSSolicEnvAuto" Style="margin-left: 3px;" ToolTip="Informe se o envio é automático"
                                                        runat="server" Enabled="false" CssClass="ddlAlterPrazoEntreSolic">
                                                        <asp:ListItem Value="N" Selected="true">Não</asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-top: -13px; margin-left: -10px;">
                                                    <label for="txtMsgSMSSolic" style="float: left;" title="Mensagem">
                                                        Msg</label>
                                                    <asp:TextBox ID="txtMsgSMSSolic" runat="server" Style="margin-left: 3px; height: 28px;
                                                        width: 240px; overflow-y: hidden;" ToolTip="Informe a Mensagem SMS de solicitação da Secretaria Escolar"
                                                        Enabled="false" CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                            <ContentTemplate>
                                                <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                                    <asp:CheckBox ID="chkSMSEntreSecreEscol" ToolTip="Informe se enviará SMS para uma entrega da Secretaria Escolar"
                                                        runat="server" OnCheckedChanged="chkSMSEntreSecreEscol_CheckedChanged" AutoPostBack="true"
                                                        Text="Entrega" />
                                                </li>
                                                <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                                    <label for="ddlFlagSMSEntreEnvAuto" style="float: left;" title="Envio automático">
                                                        Env.Aut.:</label>
                                                    <asp:DropDownList ID="ddlFlagSMSEntreEnvAuto" Style="margin-left: 3px;" ToolTip="Informe se o envio é automático"
                                                        runat="server" Enabled="false" CssClass="ddlAlterPrazoEntreSolic">
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-top: -13px; margin-left: -10px;">
                                                    <label for="txtMsgSMSEntre" style="float: left;" title="Mensagem">
                                                        Msg</label>
                                                    <asp:TextBox ID="txtMsgSMSEntre" runat="server" Style="margin-left: 3px; height: 28px;
                                                        width: 240px; overflow-y: hidden;" ToolTip="Informe a Mensagem SMS de entrega da Secretaria Escolar"
                                                        Enabled="false" CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                            <ContentTemplate>
                                                <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                                    <asp:CheckBox ID="chkSMSOutroSecreEscol" ToolTip="Informe se enviará SMS para outras ocorrências da Secretaria Escolar"
                                                        runat="server" OnCheckedChanged="chkSMSOutroSecreEscol_CheckedChanged" AutoPostBack="true"
                                                        Text="Outros" />
                                                </li>
                                                <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                                    <label for="ddlFlagSMSOutroEnvAuto" style="float: left;" title="Envio automático">
                                                        Env.Aut.:</label>
                                                    <asp:DropDownList ID="ddlFlagSMSOutroEnvAuto" Style="margin-left: 3px;" Enabled="false"
                                                        ToolTip="Informe se o envio é automático" runat="server" CssClass="ddlAlterPrazoEntreSolic">
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-top: -13px; margin-left: -10px;">
                                                    <label for="txtMsgSMSOutro" style="float: left;" title="Mensagem">
                                                        Msg</label>
                                                    <asp:TextBox ID="txtMsgSMSOutro" runat="server" Style="margin-left: 3px; height: 28px;
                                                        width: 240px; overflow-y: hidden;" ToolTip="Informe a Mensagem SMS de outras ocorrências da Secretaria Escolar"
                                                        CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);" Enabled="false"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ul>
                                </li>
                                <li style="padding-left: 5px;">
                                    <ul>
                                        <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                            font-weight: bold;">Gestão de Matrículas</span></li>
                                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                            <ContentTemplate>
                                                <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                                    <asp:CheckBox ID="chkSMSReserVagas" ToolTip="Informe se enviará SMS para uma reserva de vagas"
                                                        runat="server" OnCheckedChanged="chkSMSReserVagas_CheckedChanged" AutoPostBack="true"
                                                        Text="Reserva Vagas" />
                                                </li>
                                                <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                                    <label for="ddlFlagSMSReserVagasEnvAuto" style="float: left;" title="Envio automático">
                                                        Env.Aut.:</label>
                                                    <asp:DropDownList ID="ddlFlagSMSReserVagasEnvAuto" Style="margin-left: 3px;" Enabled="false"
                                                        ToolTip="Informe se o envio é automático" runat="server" CssClass="ddlAlterPrazoEntreSolic">
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-top: -13px; margin-left: -10px;">
                                                    <label for="txtMsgSMSReserVagas" style="float: left;" title="Mensagem">
                                                        Msg</label>
                                                    <asp:TextBox ID="txtMsgSMSReserVagas" runat="server" Style="margin-left: 3px; height: 28px;
                                                        width: 240px; overflow-y: hidden;" ToolTip="Informe a Mensagem SMS de reserva de vagas"
                                                        Enabled="false" CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                            <ContentTemplate>
                                                <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                                    <asp:CheckBox ID="chkSMSRenovMatri" ToolTip="Informe se enviará SMS para uma renovação de matrícula"
                                                        runat="server" OnCheckedChanged="chkSMSRenovMatri_CheckedChanged" AutoPostBack="true"
                                                        Text="Renovação" />
                                                </li>
                                                <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                                    <label for="ddlFlagSMSRenovMatriEnvAuto" style="float: left;" title="Envio automático">
                                                        Env.Aut.:</label>
                                                    <asp:DropDownList ID="ddlFlagSMSRenovMatriEnvAuto" Style="margin-left: 3px;" Enabled="false"
                                                        ToolTip="Informe se o envio é automático" runat="server" CssClass="ddlAlterPrazoEntreSolic">
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-top: -13px; margin-left: -10px;">
                                                    <label for="txtMsgSMSRenovMatri" style="float: left;" title="Mensagem">
                                                        Msg</label>
                                                    <asp:TextBox ID="txtMsgSMSRenovMatri" runat="server" Style="margin-left: 3px; height: 28px;
                                                        width: 240px; overflow-y: hidden;" ToolTip="Informe a Mensagem SMS de renovação de matrícula"
                                                        Enabled="false" CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                            <ContentTemplate>
                                                <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                                    <asp:CheckBox ID="chkSMSMatriNova" ToolTip="Informe se enviará SMS para matrícula nova"
                                                        runat="server" OnCheckedChanged="chkSMSMatriNova_CheckedChanged" AutoPostBack="true"
                                                        Text="Matrícula Nova" />
                                                </li>
                                                <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                                    <label for="ddlFlagSMSMatriNovaEnvAuto" style="float: left;" title="Envio automático">
                                                        Env.Aut.:</label>
                                                    <asp:DropDownList ID="ddlFlagSMSMatriNovaEnvAuto" Style="margin-left: 3px;" Enabled="false"
                                                        ToolTip="Informe se o envio é automático" runat="server" CssClass="ddlAlterPrazoEntreSolic">
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-top: -13px; margin-left: -10px;">
                                                    <label for="txtMsgSMSMatriNova" style="float: left;" title="Mensagem">
                                                        Msg</label>
                                                    <asp:TextBox ID="txtMsgSMSMatriNova" runat="server" Style="margin-left: 3px; height: 28px;
                                                        width: 240px; overflow-y: hidden;" ToolTip="Informe a Mensagem SMS de matrícula nova"
                                                        Enabled="false" CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ul>
                                </li>
                                <li style="padding-right: 10px; border-right: 1px solid #CCCCCC; margin-top: 10px;">
                                    <ul>
                                        <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                            font-weight: bold;">Biblioteca Escolar</span></li>
                                        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                            <ContentTemplate>
                                                <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                                    <asp:CheckBox ID="chkSMSReserBibli" ToolTip="Informe se enviará SMS para uma reserva de biblioteca"
                                                        runat="server" OnCheckedChanged="chkSMSReserBibli_CheckedChanged" AutoPostBack="true"
                                                        Text="Reserva" />
                                                </li>
                                                <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                                    <label for="ddlFlagSMSReserBibliEnvAuto" style="float: left;" title="Envio automático">
                                                        Env.Aut.:</label>
                                                    <asp:DropDownList ID="ddlFlagSMSReserBibliEnvAuto" Style="margin-left: 3px;" Enabled="false"
                                                        ToolTip="Informe se o envio é automático" runat="server" CssClass="ddlAlterPrazoEntreSolic">
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-top: -13px; margin-left: -10px;">
                                                    <label for="txtMsgSMSReserBibli" style="float: left;" title="Mensagem">
                                                        Msg</label>
                                                    <asp:TextBox ID="txtMsgSMSReserBibli" runat="server" Style="margin-left: 3px; height: 28px;
                                                        width: 240px; overflow-y: hidden;" ToolTip="Informe a Mensagem SMS de reserva de biblioteca"
                                                        Enabled="false" CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                            <ContentTemplate>
                                                <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                                    <asp:CheckBox ID="chkSMSEmpreBibli" ToolTip="Informe se enviará SMS para um empréstimo de biblioteca"
                                                        runat="server" OnCheckedChanged="chkSMSEmpreBibli_CheckedChanged" AutoPostBack="true"
                                                        Text="Empréstimo" />
                                                </li>
                                                <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                                    <label for="ddlFlagSMSEmpreBibliEnvAuto" style="float: left;" title="Envio automático">
                                                        Env.Aut.:</label>
                                                    <asp:DropDownList ID="ddlFlagSMSEmpreBibliEnvAuto" Style="margin-left: 3px;" Enabled="false"
                                                        ToolTip="Informe se o envio é automático" runat="server" CssClass="ddlAlterPrazoEntreSolic">
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-top: -13px; margin-left: -10px;">
                                                    <label for="txtMsgSMSEmpreBibli" style="float: left;" title="Mensagem">
                                                        Msg</label>
                                                    <asp:TextBox ID="txtMsgSMSEmpreBibli" runat="server" Style="margin-left: 3px; height: 28px;
                                                        width: 240px; overflow-y: hidden;" ToolTip="Informe a Mensagem SMS de empréstimo de biblioteca"
                                                        Enabled="false" CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                            <ContentTemplate>
                                                <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                                    <asp:CheckBox ID="chkSMSDiverBibli" ToolTip="Informe se enviará SMS para diversas operações da biblioteca"
                                                        runat="server" OnCheckedChanged="chkSMSDiverBibli_CheckedChanged" AutoPostBack="true"
                                                        Text="Diversas" />
                                                </li>
                                                <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                                    <label for="ddlFlagSMSDiverBibliEnvAuto" style="float: left;" title="Envio automático">
                                                        Env.Aut.:</label>
                                                    <asp:DropDownList ID="ddlFlagSMSDiverBibliEnvAuto" Style="margin-left: 3px;" Enabled="false"
                                                        ToolTip="Informe se o envio é automático" runat="server" CssClass="ddlAlterPrazoEntreSolic">
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-top: -13px; margin-left: -10px;">
                                                    <label for="txtMsgSMSDiverBibli" style="float: left;" title="Mensagem">
                                                        Msg</label>
                                                    <asp:TextBox ID="txtMsgSMSDiverBibli" runat="server" Style="margin-left: 3px; height: 28px;
                                                        width: 240px; overflow-y: hidden;" ToolTip="Informe a Mensagem SMS para diversas operações da biblioteca"
                                                        Enabled="false" CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ul>
                                </li>
                                <li style="padding-left: 5px; margin-top: 10px;">
                                    <ul>
                                        <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                            font-weight: bold;">Controles Diversos</span></li>
                                        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                            <ContentTemplate>
                                                <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                                    <asp:CheckBox ID="chkSMSFaltaAluno" ToolTip="Informe se enviará SMS para uma falta do aluno"
                                                        runat="server" OnCheckedChanged="chkSMSFaltaAluno_CheckedChanged" AutoPostBack="true"
                                                        Text="Falta de Aluno" />
                                                </li>
                                                <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                                    <label for="ddlFlagSMSFaltaAlunoEnvAuto" style="float: left;" title="Envio automático">
                                                        Env.Aut.:</label>
                                                    <asp:DropDownList ID="ddlFlagSMSFaltaAlunoEnvAuto" Style="margin-left: 3px;" Enabled="false"
                                                        ToolTip="Informe se o envio é automático" runat="server" CssClass="ddlAlterPrazoEntreSolic">
                                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-top: -13px; margin-left: -10px;">
                                                    <label for="txtMsgSMSFaltaAluno" style="float: left;" title="Mensagem">
                                                        Msg</label>
                                                    <asp:TextBox ID="txtMsgSMSFaltaAluno" runat="server" Style="margin-left: 3px; height: 28px;
                                                        width: 240px; overflow-y: hidden;" ToolTip="Informe a Mensagem SMS de reserva de vagas"
                                                        Enabled="false" CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </li>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                            <asp:CheckBox ID="chkEnvioSMS" ToolTip="Informe se usuários enviarão SMS" runat="server"
                                                Text="Msg SMS" />
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div id="tabFinanceiro" class="tabFinanceiro" style="display: none;">
                    <ul class="ulDados">
                        <li class="liClear" style="margin-left: 130px;">
                            <label for="txtUnidadeEscolarFI" title="Razão Social">
                                Unidade Escolar</label>
                            <asp:TextBox ID="txtUnidadeEscolarFI" Enabled="false" ClientIDMode="Static" ToolTip="Informe a Razão Social"
                                CssClass="txtRazaoSocialIUE" MaxLength="80" runat="server"></asp:TextBox>
                        </li>
                        <li class="liCodIdentificacao">
                            <label for="txtCodIdenticacaoCO" title="Código de Identificação">
                                Cód. Identificação</label>
                            <asp:TextBox ID="txtCodIdenticacaoFI" Enabled="false" ClientIDMode="Static" ToolTip="Informe o Código de Identificação"
                                CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-left: 10px;">
                            <label for="txtCNPJCO" title="CNPJ">
                                Nº CNPJ</label>
                            <asp:TextBox ID="txtCNPJFI" Enabled="false" ClientIDMode="Static" ToolTip="Informe o CNPJ"
                                CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear" style="width: 100%; text-align: center;"><span style="color: #6495ED;
                            font-size: 1.3em; font-weight: bold;">Controle Financeiro</span></li>
                        <li class="liClear" style="margin-top: 2px; margin-left: 324px;">
                            <label for="txtTipoCtrlFinanceiro" style="float: left;" title="Tipo de Controle Financeiro">
                                Por</label>
                            <asp:TextBox ID="txtTipoCtrlFinanceiro" Enabled="false" Style="margin-left: 7px;
                                padding-left: 3px;" Text="Por Unidade" ToolTip="Tipo de Controle Financeiro"
                                BackColor="#FFFFE1" CssClass="txtTipoCtrlFrequ" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <ul>
                                <li style="padding-right: 10px; border-right: 1px solid #CCCCCC;">
                                    <ul>
                                        <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em; text-transform: uppercase;
                                            font-weight: bold;">Boletos Bancários</span></li>
                                        <li class="liClear" style="margin-top: 5px; width: 80px;">
                                            <label style="color: #006699;" title="Item">
                                                TIPO</label>
                                            <label style="float: left;" title="Boleto de Matrícula">
                                                Matrícula</label>
                                        </li>
                                        <li style="margin-left: 5px; width: 212px; margin-top: 5px;">
                                            <label style="color: #006699;" title="(Banco Agência Cta Corrente)">
                                                Banco - Agência - Conta Corrente</label>
                                            <asp:DropDownList ID="ddlBoletoMatric" runat="server" Width="200px" ToolTip="Selecione o Banco">
                                            </asp:DropDownList>
                                        </li>
                                        <li class="liClear" style="margin-top: 10px; width: 80px;">
                                            <label style="float: left;" title="Boleto de Renovação">
                                                Renovação</label>
                                        </li>
                                        <li style="margin-left: 5px; width: 212px; margin-top: 10px;">
                                            <asp:DropDownList ID="ddlBoletoRenovacao" runat="server" Width="200px" ToolTip="Selecione o boleto de renovação">
                                            </asp:DropDownList>
                                        </li>
                                        <li class="liClear" style="margin-top: 10px; width: 80px;">
                                            <label style="float: left;" title="Boleto de Mensalidade">
                                                Mensalidade</label>
                                        </li>
                                        <li style="margin-left: 5px; width: 212px; margin-top: 10px;">
                                            <asp:DropDownList ID="ddlBoletoMensalidade" runat="server" Width="200px" ToolTip="Selecione o boleto de mensalidade">
                                            </asp:DropDownList>
                                        </li>
                                        <li class="liClear" style="margin-top: 10px; width: 80px;">
                                            <label style="float: left;" title="Boleto de Atividades Extras">
                                                Atividades Extras</label>
                                        </li>
                                        <li style="margin-left: 5px; width: 212px; margin-top: 10px;">
                                            <asp:DropDownList ID="ddlBoletoAtiviExtra" runat="server" Width="200px" ToolTip="Selecione o boleto de atividades extras">
                                            </asp:DropDownList>
                                        </li>
                                        <li class="liClear" style="margin-top: 10px; width: 80px;">
                                            <label style="float: left;" title="Boleto de Biblioteca">
                                                Biblioteca</label>
                                        </li>
                                        <li style="margin-left: 5px; width: 212px; margin-top: 10px;">
                                            <asp:DropDownList ID="ddlBoletoBiblioteca" runat="server" Width="200px" ToolTip="Selecione o boleto de biblioteca">
                                            </asp:DropDownList>
                                        </li>
                                        <li class="liClear" style="margin-top: 10px; width: 80px;">
                                            <label style="float: left;" title="Boleto de Serviço de Secretaria">
                                                Serv. Secretaria</label>
                                        </li>
                                        <li style="margin-left: 5px; width: 212px; margin-top: 10px;">
                                            <asp:DropDownList ID="ddlBoletoServSecre" runat="server" Width="200px" ToolTip="Selecione o boleto de serviço de secretaria">
                                            </asp:DropDownList>
                                        </li>
                                        <li class="liClear" style="margin-top: 10px; width: 80px;">
                                            <label style="float: left;" title="Boleto de Serviços Diversos">
                                                Serv. Diversos</label>
                                        </li>
                                        <li style="margin-left: 5px; width: 212px; margin-top: 10px;">
                                            <asp:DropDownList ID="ddlBoletoServDiver" runat="server" Width="200px" ToolTip="Selecione o boleto de serviços diversos">
                                            </asp:DropDownList>
                                        </li>
                                        <li class="liClear" style="margin-top: 10px; width: 80px;">
                                            <label style="float: left;" title="Boleto de Negociação">
                                                Negociação</label>
                                        </li>
                                        <li style="margin-left: 5px; width: 212px; margin-top: 10px;">
                                            <asp:DropDownList ID="ddlBoletoNegociacao" runat="server" Width="200px" ToolTip="Selecione o boleto de negociação">
                                            </asp:DropDownList>
                                        </li>
                                        <li class="liClear" style="margin-top: 10px; width: 80px;">
                                            <label style="float: left;" title="Boleto de Outros">
                                                Outros</label>
                                        </li>
                                        <li style="margin-left: 5px; width: 212px; margin-top: 10px;">
                                            <asp:DropDownList ID="ddlBoletoOutros" runat="server" Width="200px" ToolTip="Selecione o boleto de outros">
                                            </asp:DropDownList>
                                        </li>
                                    </ul>
                                </li>
                                <li style="width: 275px;">
                                    <ul>
                                        <li class="liPedagMatric" style="width: 100%; margin-bottom: 5px;"><span style="font-size: 1.0em;
                                            text-transform: uppercase; font-weight: bold;">Informações Financeiras</span></li>
                                        <li class="liClear" style="">
                                            <label for="txtJurosDiario" style="float: left;" title="% Juros Diário">
                                                % Juros Diário</label>
                                            <asp:TextBox ID="txtJurosDiario" CssClass="txtVlJurosSecre" MaxLength="5" runat="server"
                                                Style="width: 30px; margin-left: 5px; margin-bottom: 0;" ToolTip="Informe o Valor % do Juros Diário"></asp:TextBox>
                                        </li>
                                        <li class="liClear" style="margin-top: 5px;">
                                            <label for="txtMultaMensal" style="float: left;" title="% Multa">
                                                % Multa</label>
                                            <asp:TextBox ID="txtMultaMensal" CssClass="campoMoeda" runat="server" Style="width: 30px;
                                                margin-left: 34px; margin-bottom: 0;" ToolTip="Informe o Valor % da Multa"></asp:TextBox>
                                        </li>
                                        <li class="liClear" style="margin-top: 5px;">
                                            <label for="txtDiaVencto" style="float: left;" title="Dia do vencimento">
                                                Dia Vencto</label>
                                            <asp:TextBox ID="txtDiaVencto" Style="margin-left: 23px; width: 30px;" ToolTip="Informe o dia de vencimento"
                                                CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                        </li>
                                        <li class="liPedagMatric" style="width: 100%; margin-bottom: 5px; margin-top: 5px;">
                                            <span style="font-size: 1.0em; text-transform: uppercase; font-weight: bold;">Fluxo
                                                de Caixa</span></li>
                                        <li class="liClear" style="margin-top: 0;">
                                            <label for="txtDtSaldo" style="float: left;" title="Data Saldo Inicial">
                                                Data Saldo Inicial</label>
                                            <asp:TextBox ID="txtDtSaldo" CssClass="campoData" Style="margin-left: 3px;" runat="server"
                                                ToolTip="Informe a Data do Saldo Inicial"></asp:TextBox>
                                        </li>
                                        <li class="liClear" style="margin-top: 5px;">
                                            <label for="txtSaldo" style="float: left;" title="Saldo Inicial">
                                                Saldo Inicial</label>
                                            <asp:TextBox ID="txtSaldo" Style="margin-left: 27px; width: 70px;" CssClass="campoMoeda"
                                                ToolTip="Informe o saldo inicial" runat="server"></asp:TextBox>
                                        </li>
                                        <li class="liClear" style="margin-top: 5px; margin-left: -5px;">
                                            <asp:CheckBox ID="chkIntegFinan" CssClass="chkRecuperEscol" ToolTip="Informe se existirá integração com o financeiro"
                                                runat="server" />
                                            <span style="margin-left: -5px;">Integração com o financeiro</span> </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div id="tabImpostos" class="tabImpostos" style="display: none;">
                    <ul class="ulDados">
                        <div class="divGridimpostos">
                            <li style="width: 100%; height: 15px; text-align: center; text-transform: uppercase;
                                margin-top: 0px; margin-left: auto; background-color: #BCD2EE; margin-bottom: auto;">
                                <label style="font-size: 1.1em; font-family: Tahoma; text-align: center;">
                                    Impostos</label>
                            </li>
                            <li>
                                <asp:GridView runat="server" ID="grdImpostos" CssClass="grdBusca" ShowHeader="true"
                                    ShowHeaderWhenEmpty="true" ToolTip="Grid de informações e de Impostos" AutoGenerateColumns="False"
                                    Width="652px">
                                    <RowStyle CssClass="rowStyle" />
                                    <HeaderStyle CssClass="th" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectImposto" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SIGLA" HeaderText="SIGLA">
                                            <ItemStyle HorizontalAlign="Left" Width="65px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IMPOS" HeaderText="IMPOSTO">
                                            <ItemStyle HorizontalAlign="Left" Width="370px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="APLI" HeaderText="APLICAÇÃO">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TIPO" HeaderText="TIPO">
                                            <ItemStyle HorizontalAlign="Left" Width="70px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="%">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPer" CssClass="campoPer" Width="25px" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R$ REF">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtVlRef" Width="60px" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </li>
                        </div>
                    </ul>
                </div>
            </div>
        </li>
    </ul>
    <script type="text/javascript">

        $('.qtdeDias input').keyup(function () {
            this.value = this.value.replace(/[^0-9\.]/g, '');
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".txtCEPIUE").mask("99999-999");
            $(".txtQtdItensAcervoIUE").mask("?999999");
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".campoNumerico").mask("?99");
            $('.txtNumIniciSolicAuto').mask("?9999999");
            $('.txtQtdeDiasEntreSolic').mask("?99");
            $('.txtHorarioFuncManha').mask("99:99");
            $('.txtHorarioFuncTarde').mask("99:99");
            $('.txtHorarioFuncNoite').mask("99:99");
        });

        $(document).ready(function () {
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".campoNumerico").mask("?99");
            $(".txtQtdItensAcervoIUE").mask("?999999");
            $(".txtCNPJIUE").mask("99.999.999/9999-99");
            $(".txtTelefone").mask("(99) 9999-9999");
            $(".txtCEPIUE").mask("99999-999");
            $('.txtHorarioFuncManha').mask("99:99");
            $('.txtHorarioFuncTarde').mask("99:99");
            $('.txtHorarioFuncNoite').mask("99:99");
            $('.txtQtdeDiasEntreSolic').mask("?99");
            $('.txtNumControle').mask("?999999999");
            $('.txtNossoNumero').mask("?99999999999999999999");
            $('.txtNumIniciSolicAuto').mask("?9999999");
            $('.txtAnoSaldo').mask("9999");
            $('.txtValidRetorno').mask('?999');


            function preencheInformacoesUnidade() {
                $("#txtUnidadeEscolarQS").val($(".txtRazaoSocialIUE").val());
                $("#txtUnidadeEscolarNH").val($(".txtRazaoSocialIUE").val());
                $("#txtUnidadeEscolarPP").val($(".txtRazaoSocialIUE").val());
                $("#txtUnidadeEscolarFF").val($(".txtRazaoSocialIUE").val());
                $("#txtUnidadeEscolarPM").val($(".txtRazaoSocialIUE").val());
                $("#txtUnidadeEscolarSE").val($(".txtRazaoSocialIUE").val());
                $("#txtUnidadeEscolarBE").val($(".txtRazaoSocialIUE").val());
                $("#txtUnidadeEscolarMS").val($(".txtRazaoSocialIUE").val());
                $("#txtUnidadeEscolarCO").val($(".txtRazaoSocialIUE").val());
                $("#txtUnidadeEscolarFI").val($(".txtRazaoSocialIUE").val());
                $("#txtUnidadeEscolarGU").val($(".txtRazaoSocialIUE").val());

                $("#txtCodIdenticacaoQS").val($(".txtSiglaIUE").val());
                $("#txtCodIdenticacaoNH").val($(".txtSiglaIUE").val());
                $("#txtCodIdenticacaoPP").val($(".txtSiglaIUE").val());
                $("#txtCodIdenticacaoFF").val($(".txtSiglaIUE").val());
                $("#txtCodIdenticacaoPM").val($(".txtSiglaIUE").val());
                $("#txtCodIdenticacaoSE").val($(".txtSiglaIUE").val());
                $("#txtCodIdenticacaoBE").val($(".txtSiglaIUE").val());
                $("#txtCodIdenticacaoMS").val($(".txtSiglaIUE").val());
                $("#txtCodIdenticacaoCO").val($(".txtSiglaIUE").val());
                $("#txtCodIdenticacaoFI").val($(".txtSiglaIUE").val());
                $("#txtCodIdenticacaoGU").val($(".txtSiglaIUE").val());

                $("#txtCNPJQS").val($(".txtCNPJIUE").val());
                $("#txtCNPJNH").val($(".txtCNPJIUE").val());
                $("#txtCNPJPP").val($(".txtCNPJIUE").val());
                $("#txtCNPJFF").val($(".txtCNPJIUE").val());
                $("#txtCNPJPM").val($(".txtCNPJIUE").val());
                $("#txtCNPJSE").val($(".txtCNPJIUE").val());
                $("#txtCNPJBE").val($(".txtCNPJIUE").val());
                $("#txtCNPJMS").val($(".txtCNPJIUE").val());
                $("#txtCNPJCO").val($(".txtCNPJIUE").val());
                $("#txtCNPJFI").val($(".txtCNPJIUE").val());
                $("#txtCNPJGU").val($(".txtCNPJIUE").val());
            }

            $(".rblInforCadastro").click(function () {
                preencheInformacoesUnidade();
                $(".rblInforControle :checked").removeAttr('checked');

                if ($(":checked").val() == "1" || $(":checked").val() == "N" || $(":checked").val() == "on" || $(":checked").val() == "") {
                    $(".tabDadosCadas").show();
                    $(".tabQuemSomos").hide();
                    $(".tabNossaHisto").hide();
                    $(".tabPropoPedag").hide();
                    $(".tabPedagMatric").hide();
                    $(".tabFrequFunci").hide();
                    $(".tabControleSaude").hide();
                    $(".tabControleSaude2").hide();
                    $(".tabSecreEscol").hide();
                    $(".tabBibliEscol").hide();
                    $(".tabMensaSMS").hide();
                    $("#rblInforCadastro_0").attr("checked", "checked");
                    $(".tabContabil").hide();
                    $(".tabFinanceiro").hide();
                    $(".tabImpostos").hide();
                    $(".tabGestorUnidade").hide();
                    $(".divMensagCamposObrig").css("visibility", "visible");
                }
                if ($(":checked").val() == "2") {
                    $(".tabDadosCadas").hide();
                    $(".tabQuemSomos").show();
                    $(".tabNossaHisto").hide();
                    $(".tabPropoPedag").hide();
                    $(".tabPedagMatric").hide();
                    $(".tabFrequFunci").hide();
                    $(".tabControleSaude").hide();
                    $(".tabControleSaude2").hide();
                    $(".tabSecreEscol").hide();
                    $(".tabBibliEscol").hide();
                    $(".tabMensaSMS").hide();
                    $(".tabContabil").hide();
                    $(".tabFinanceiro").hide();
                    $(".tabImpostos").hide();
                    $(".tabGestorUnidade").hide();
                    $(".divMensagCamposObrig").css("visibility", "collapse");
                    txtQuemSomos.AdjustControl();
                }
                if ($(":checked").val() == "3") {
                    $(".tabDadosCadas").hide();
                    $(".tabQuemSomos").hide();
                    $(".tabNossaHisto").show();
                    $(".tabPropoPedag").hide();
                    $(".tabPedagMatric").hide();
                    $(".tabFrequFunci").hide();
                    $(".tabControleSaude").hide();
                    $(".tabControleSaude2").hide();
                    $(".tabSecreEscol").hide();
                    $(".tabBibliEscol").hide();
                    $(".tabMensaSMS").hide();
                    $(".tabContabil").hide();
                    $(".tabGestorUnidade").hide();
                    $(".tabFinanceiro").hide();
                    $(".tabImpostos").hide();
                    $(".divMensagCamposObrig").css("visibility", "collapse");
                    txtNossaHisto.AdjustControl();
                }
                if ($(":checked").val() == "4") {
                    $(".tabDadosCadas").hide();
                    $(".tabQuemSomos").hide();
                    $(".tabNossaHisto").hide();
                    $(".tabPropoPedag").show();
                    $(".tabPedagMatric").hide();
                    $(".tabFrequFunci").hide();
                    $(".tabControleSaude").hide();
                    $(".tabControleSaude2").hide();
                    $(".tabSecreEscol").hide();
                    $(".tabBibliEscol").hide();
                    $(".tabMensaSMS").hide();
                    $(".tabContabil").hide();
                    $(".tabFinanceiro").hide();
                    $(".tabImpostos").hide();
                    $(".tabGestorUnidade").hide();
                    $(".divMensagCamposObrig").css("visibility", "collapse");
                    txtPropoPedag.AdjustControl();
                }
            });

            $(".rblInforControle").click(function () {
                preencheInformacoesUnidade();
                $(".rblInforCadastro :checked").removeAttr('checked');

                if ($(":checked").val() == "5" || $(":checked").val() == "N" || $(":checked").val() == "on" || $(":checked").val() == "1" || $(":checked").val() == "") {
                    $(".tabDadosCadas").hide();
                    $(".tabQuemSomos").hide();
                    $(".tabNossaHisto").hide();
                    $(".tabPropoPedag").hide();
                    $(".tabPedagMatric").hide();
                    $(".tabFrequFunci").show();
                    $(".tabControleSaude").hide();
                    $(".tabControleSaude2").hide();
                    $(".tabSecreEscol").hide();
                    $(".tabBibliEscol").hide();
                    $(".tabMensaSMS").hide();
                    $("#rblInforControle_0").attr("checked", "checked");
                    $(".tabContabil").hide();
                    $(".tabFinanceiro").hide();
                    $(".tabImpostos").hide();
                    $(".tabGestorUnidade").hide();
                    $(".divMensagCamposObrig").css("visibility", "collapse");
                }
                else if ($(":checked").val() == "6") {
                    $(".tabDadosCadas").hide();
                    $(".tabQuemSomos").hide();
                    $(".tabNossaHisto").hide();
                    $(".tabPropoPedag").hide();
                    $(".tabPedagMatric").show();
                    $(".tabFrequFunci").hide();
                    $(".tabControleSaude").hide();
                    $(".tabControleSaude2").hide();
                    $(".tabSecreEscol").hide();
                    $(".tabBibliEscol").hide();
                    $(".tabMensaSMS").hide();
                    $(".tabContabil").hide();
                    $(".tabFinanceiro").hide();
                    $(".tabImpostos").hide();
                    $(".tabGestorUnidade").hide();
                    $(".divMensagCamposObrig").css("visibility", "collapse");
                }
                else if ($(":checked").val() == "7") {
                    $(".tabDadosCadas").hide();
                    $(".tabQuemSomos").hide();
                    $(".tabNossaHisto").hide();
                    $(".tabPropoPedag").hide();
                    $(".tabPedagMatric").hide();
                    $(".tabFrequFunci").hide();
                    $(".tabControleSaude").hide();
                    $(".tabControleSaude2").hide();
                    $(".tabSecreEscol").show();
                    $(".tabBibliEscol").hide();
                    $(".tabMensaSMS").hide();
                    $(".tabContabil").hide();
                    $(".tabFinanceiro").hide();
                    $(".tabImpostos").hide();
                    $(".tabGestorUnidade").hide();
                    $(".divMensagCamposObrig").css("visibility", "collapse");
                }
                else if ($(":checked").val() == "8") {
                    $(".tabDadosCadas").hide();
                    $(".tabQuemSomos").hide();
                    $(".tabNossaHisto").hide();
                    $(".tabPropoPedag").hide();
                    $(".tabPedagMatric").hide();
                    $(".tabFrequFunci").hide();
                    $(".tabControleSaude").hide();
                    $(".tabControleSaude2").hide();
                    $(".tabSecreEscol").hide();
                    $(".tabBibliEscol").show();
                    $(".tabMensaSMS").hide();
                    $(".tabContabil").hide();
                    $(".tabFinanceiro").hide();
                    $(".tabImpostos").hide();
                    $(".tabGestorUnidade").hide();
                    $(".divMensagCamposObrig").css("visibility", "collapse");
                }
                else if ($(":checked").val() == "9") {
                    $(".tabDadosCadas").hide();
                    $(".tabQuemSomos").hide();
                    $(".tabNossaHisto").hide();
                    $(".tabPropoPedag").hide();
                    $(".tabPedagMatric").hide();
                    $(".tabFrequFunci").hide();
                    $(".tabControleSaude").hide();
                    $(".tabControleSaude2").hide();
                    $(".tabSecreEscol").hide();
                    $(".tabBibliEscol").hide();
                    $(".tabMensaSMS").hide();
                    $(".tabContabil").show();
                    $(".tabFinanceiro").hide();
                    $(".tabImpostos").hide();
                    $(".tabGestorUnidade").hide();
                    $(".divMensagCamposObrig").css("visibility", "collapse");
                }
                else if ($(":checked").val() == "10") {
                    $(".tabDadosCadas").hide();
                    $(".tabQuemSomos").hide();
                    $(".tabNossaHisto").hide();
                    $(".tabPropoPedag").hide();
                    $(".tabPedagMatric").hide();
                    $(".tabFrequFunci").hide();
                    $(".tabControleSaude").hide();
                    $(".tabControleSaude2").hide();
                    $(".tabSecreEscol").hide();
                    $(".tabBibliEscol").hide();
                    $(".tabMensaSMS").show();
                    $(".tabContabil").hide();
                    $(".tabGestorUnidade").hide();
                    $(".tabFinanceiro").hide();
                    $(".tabImpostos").hide();
                    $(".divMensagCamposObrig").css("visibility", "collapse");
                }
                else if ($(":checked").val() == "11") {
                    $(".tabDadosCadas").hide();
                    $(".tabQuemSomos").hide();
                    $(".tabNossaHisto").hide();
                    $(".tabPropoPedag").hide();
                    $(".tabPedagMatric").hide();
                    $(".tabFrequFunci").hide();
                    $(".tabControleSaude").hide();
                    $(".tabControleSaude2").hide();
                    $(".tabSecreEscol").hide();
                    $(".tabBibliEscol").hide();
                    $(".tabMensaSMS").hide();
                    $(".tabContabil").hide();
                    $(".tabFinanceiro").show();
                    $(".tabImpostos").hide();
                    $(".tabGestorUnidade").hide();
                    $(".divMensagCamposObrig").css("visibility", "collapse");
                }
                else if ($(":checked").val() == "12") {
                    $(".tabDadosCadas").hide();
                    $(".tabQuemSomos").hide();
                    $(".tabNossaHisto").hide();
                    $(".tabPropoPedag").hide();
                    $(".tabPedagMatric").hide();
                    $(".tabFrequFunci").hide();
                    $(".tabControleSaude").hide();
                    $(".tabControleSaude2").hide();
                    $(".tabSecreEscol").hide();
                    $(".tabBibliEscol").hide();
                    $(".tabMensaSMS").hide();
                    $(".tabContabil").hide();
                    $(".tabFinanceiro").hide();
                    $(".tabImpostos").hide();
                    $(".tabGestorUnidade").show();
                    $(".divMensagCamposObrig").css("visibility", "collapse");
                }
                else if ($(":checked").val() == "13") {
                    $(".tabDadosCadas").hide();
                    $(".tabQuemSomos").hide();
                    $(".tabNossaHisto").hide();
                    $(".tabPropoPedag").hide();
                    $(".tabPedagMatric").hide();
                    $(".tabFrequFunci").hide();
                    $(".tabControleSaude").hide();
                    $(".tabControleSaude2").hide();
                    $(".tabSecreEscol").hide();
                    $(".tabBibliEscol").hide();
                    $(".tabMensaSMS").hide();
                    $(".tabContabil").hide();
                    $(".tabFinanceiro").hide();
                    $(".tabImpostos").show();
                    $(".tabGestorUnidade").hide();
                    $(".divMensagCamposObrig").css("visibility", "collapse");
                }
                else if ($(":checked").val() == "14") {
                    $(".tabDadosCadas").hide();
                    $(".tabQuemSomos").hide();
                    $(".tabNossaHisto").hide();
                    $(".tabPropoPedag").hide();
                    $(".tabPedagMatric").hide();
                    $(".tabFrequFunci").hide();
                    $(".tabControleSaude").show();
                    $(".tabControleSaude2").hide();
                    $(".tabSecreEscol").hide();
                    $(".tabBibliEscol").hide();
                    $(".tabMensaSMS").hide();
                    $(".tabContabil").hide();
                    $(".tabFinanceiro").hide();
                    $(".tabImpostos").hide();
                    $(".tabGestorUnidade").hide();
                    $(".divMensagCamposObrig").css("visibility", "collapse");
                }
                else if ($(":checked").val() == "15") {
                    $(".tabDadosCadas").hide();
                    $(".tabQuemSomos").hide();
                    $(".tabNossaHisto").hide();
                    $(".tabPropoPedag").hide();
                    $(".tabPedagMatric").hide();
                    $(".tabFrequFunci").hide();
                    $(".tabControleSaude").hide();
                    $(".tabControleSaude2").show();
                    $(".tabSecreEscol").hide();
                    $(".tabBibliEscol").hide();
                    $(".tabMensaSMS").hide();
                    $(".tabContabil").hide();
                    $(".tabFinanceiro").hide();
                    $(".tabImpostos").hide();
                    $(".tabGestorUnidade").hide();
                    $(".divMensagCamposObrig").css("visibility", "collapse");
                }
            });
        });

        $(document).ready(function () {
            $(".campoPer").mask;
        });
    </script>
</asp:Content>
