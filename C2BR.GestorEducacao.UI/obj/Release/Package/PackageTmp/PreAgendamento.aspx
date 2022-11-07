<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreAgendamento.aspx.cs" Inherits="C2BR.GestorEducacao.UI.PreAgendamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pré-Agendamento</title>
    <link href="/Library/CSS/default.css" rel="stylesheet" type="text/css" />
    <link href="/Library/CSS/intern.css" rel="stylesheet" type="text/css" />
    <link href="/Library/CSS/jScrollPane.css" rel="stylesheet" type="text/css" />
    <link href="/Library/CSS/Jquery.UI/customtheme/default.css" rel="stylesheet" type="text/css" />
    <script src="/Library/JS/jquery.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.ui.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.form.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.corner.js" type="text/javascript"></script>
    <script src="/Library/JS/ui.datepicker-pt-BR.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.jScrollPane.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.mousewheel.min.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.defaults.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.maskedinput-1.2.2.min.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.maskMoney.0.2.js" type="text/javascript"></script>
    <script src="/Library/JS/FormValidation.js" type="text/javascript"></script>
    <style type="text/css">
        .liBtnConfirm
        {
            margin-top: 10px;
            margin-left: 2px;
            padding: 4px 3px 3px;
            background-color: #000066;
            border: 1px solid #8B8989;
        }
        .bodyPrincipal 
        { 
        	/*width: 1264px; */
        	width: 100%; 
        	/*height: 650px;*/
        	background-image: url("../Library/IMG/PreAgendamento/PGS_ImgFundo_PreAgendamentoWeb_v160712.png");
        	/*background-image: url("../Library/IMG/PreAgendamento/PGS_ImgFundo_PreAgendamentoWeb_v160718.jpg");*/
            background-repeat: repeat-x;
            margin: 2px auto 0;
        }
        
        #divPageContainer
        {
            margin: 0 auto;
            float: right;
            width: 665px;
            margin-right: 40px;
            margin-top: 30px;
            background-color: rgba(255, 255, 255, 0.8);
            /*height: 600px;*/
            /*background-image: url("../Library/IMG/PreAgendamento/PGS_ImgBrancoFundo_PreAgendamentoWeb_v160712.png");*/
        }
        #divPageContainer #divHeader
        {
            overflow: hidden;
            height: 80px;
            padding-top: 5px;
            margin-left: 2px;
            margin-right: 2px;
        }
        #divPageContainer #divHeader #divUserInfo img
        {
            float: left;
            margin-right: 10px;
            padding-right: 10px;
            width: 200px;
        }
        .divLoginInfo a 
        { 
            vertical-align: middle;
        }
        .divLoginInfo a 
        { 
            color: #009ACD !important;
        }
        .divLoginInfo
        {
            background-color: #E6E6FA;
            float: right;
            padding: 2px;
            width: 1000px;
        }
        .divLoginInfo ul { float: right; }
        .divLoginInfo ul li { float: left; }
        .divLoginInfo img
        {
            width: 15px;
            height: 15px;
        }
        #divPageContainer #divContent
        {
            padding: 5px 0;
            /*height: 525px;*/
            overflow: visible;
            /*background-color: White;
            border-left: 2px solid white;
            border-right: 2px solid white;*/
        }
        #divPageContainer #divFooter
        {
            overflow: auto;
            width: 994px;
        }
        #divPageContainer #divFooter ul li 
        { 
            float: left; 
        }
        #divPageContainer #divFooter ul li span 
        { 
            color: #FFF; 
        }        
        #ulDescricao a { color: #FFFFFF; }
        #ulDescricao { float: right; }
        .ulDados
        {
            margin: auto !important;
            margin-top: 10px !important;
        }
        .boxCornerTitle
        {
            background-color: #40e0d0;
            text-align: center;
            width: 639px;
        }
        #divHeaderFormData h1
        {
            font: normal normal bold 1.1em Arial;
            margin-top: 1px;
        }
        .helpMessages
        {
            margin-top: 5px;
            font-size: 11px !important;
        }
        .divMensagCamposObrig
        {
            font-size: 11px !important;
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
            height: 15px !important;
            margin-right: 12px;
        }
        select
        {
            height: 17px !important;
            margin-right: 12px;
        }
        *
        {
            font: normal normal normal 11px Tahoma, Verdana, Arial !important;
        }
        input.campoData
        {
                width: 65px !important;
                text-align: center;
                margin-right: 0px;
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
            font-size: 11px;
            margin-bottom: 2px;
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
        .ulReceb li 
        {
            margin-top:-6px;
        }
        .chk label
        {
            display:inline;
            margin-left:-4px;
        }
    </style>
    <script type="text/javascript">
        function Sucesso() {
            //alert('aqui');
            $(".divAgradecimento").show();
            //setTimeout('$(".divAgradecimento").hide("slow");', 20000);
            if (document.getElementById("chkEmail").checked) 
            {
                $("#lblConfirmaEmail").show();

            }
        }
    </script>
</head>
<body class="bodyPrincipal">
    <div id="divPageContainer">
        <form id="form1" runat="server" clientidmode="static">
            <div id="divContent">
                <div id="divHeaderFormData" class="boxCornerTitle" style="height: 13px; margin-left: 0.2%;">
                    <h1 style="color: White;">INFORMAÇÕES DO RESPONSÁVEL E PACIENTE</h1>
                    <img src="/Library/IMG/Gestor_SairSistema.png" alt="Icone Sair" style="height: 16px; width: 16px; margin: -15px 120px 0px 5px; float: right;"/>
                    &nbsp;
                    <a href="javascript:window.history.back();" style="margin: -13px -135px 0px 0px; float: right; color:White !important;">VOLTAR À PRONTO 10</a>
                </div>
                <div class="ulDados" style="width: 660px !important;">
                    <ul class="ulDadosPaciente" style="margin-left: 5px;">
                        <li style="margin-bottom: 10px;">
                            <label class="lblTop">
                                DADOS DO RESPONSÁVEL PELO PACIENTE
                            </label>
                            <label class="lblTop" style="color: #ff6700 !important;">
                                Informe abaixo os dados referentes ao Responsável pelo Paciente
                            </label>
                        </li>
                        <li style="clear: both;">
                            <label>
                                CPF
                                <font style="color: red;"> *</font>
                                <asp:RequiredFieldValidator runat="server" id="rvfTxtCPFResp" CssClass="teste" controltovalidate="txtCPFResp" errormessage=" Obrigatório." ValidationGroup="validaDados"/>
                            </label>
                            <asp:TextBox runat="server" ID="txtCPFResp" CssClass="campoCpf" MaxLength="18" ClientIDMode="Static" ToolTip="Informe o CPF do Responsável"></asp:TextBox>
                            <li style="margin-top: 14px; margin-left: -15px; margin-right: 12px;">
                                <asp:ImageButton ID="imgPesqAgendaAtendimento" 
                                    runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" 
                                    style="height:16px; width:16px; visibility:hidden;" OnClick="imgPesqAgendaAtendimento_OnClick"
                                />
                            </li>
                        </li>
                        <li>
                            <label>
                                Nome 
                                <font style="color: red;"> *</font>
                                <asp:RequiredFieldValidator runat="server" id="rfvTxtNomeResp" controltovalidate="txtNomeResp" errormessage=" Obrigatório." ValidationGroup="validaDados"/>
                            </label>
                            <asp:TextBox runat="server" ID="txtNomeResp" ToolTip="Nome do Responsável" CssClass="campoNome" Width="317px" MaxLength="60" ClientIDMode="Static"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Sexo
                                <font style="color: red;"> *</font>
                                <asp:RequiredFieldValidator runat="server" id="rfvDdlSexResp" controltovalidate="ddlSexoResp" errormessage=" Obrigatório" ValidationGroup="validaDados"/>
                            </label>
                            <asp:DropDownList runat="server" ID="ddlSexoResp" Width="75px" ClientIDMode="Static" ToolTip="Informe o sexo do Responsável">
                                <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Feminino" Value="F"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="lisobe">
                            <label>
                                Nascimento
                                <font style="color: red;"> *</font>
                            </label>
                            <asp:TextBox runat="server" ID="txtDtNascResp" class="campoData" MaxLength="10" ClientIDMode="Static" ToolTip="Informe a data de nascimento do Responsável"></asp:TextBox>
                        </li>
                        <li style="clear: both;" class="lisobe">
                            <label>
                                Tel. Celular
                                <font style="color: red;"> *</font>
                                <asp:RequiredFieldValidator runat="server" id="rfvTelCelularResp" controltovalidate="txtTelCelResp" ValidationGroup="validaDados"/>
                            </label>
                            <asp:TextBox runat="server" ID="txtTelCelResp" Width="95px" CssClass="campoTel" style="text-align: center;" ToolTip="Informe o telefone celular do Responsável"></asp:TextBox>
                        </li>
                        <li class="lisobe">
                            <label>
                                Tel. Fixo</label>
                            <asp:TextBox runat="server" ID="txtTelResResp" Width="95px" CssClass="campoTel" style="text-align: center;" ToolTip="Informe o telefone fixo do Responsável"></asp:TextBox>
                        </li>
                        <li class="lisobe">
                            <label>
                                Nº WhatsApp</label>
                            <asp:TextBox runat="server" ID="txtTelWhsResp"  Width="95px" CssClass="campoTel" style="text-align: center;" ToolTip="Informe o número do WhatsApp do Responsável"></asp:TextBox>
                        </li>
                        <li>
                            <label>Email</label>
                            <asp:TextBox runat="server" ID="txtEmailResp" Width="294px" MaxLength="50" ToolTip="Informe o email do Responsável"></asp:TextBox>
                        </li>
                        <li style="clear: both;" class="lisobe">
                            <label>Grau Parentesco</label>
                            <asp:DropDownList runat="server" ID="ddlGrauParent" Width="100px" ToolTip="Informe o grau de parentesco do Responsável">
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
                        <li style="margin-top: 13px; margin-left: 0px;" style="clear: both;" class="lisobe">
                            <asp:CheckBox runat="server" ID="chkPaciEhResp" CssClass="chk" ToolTip="Marque caso o responsável seja o paciente."/>
                            <label style="margin-top: -15px; margin-left: 23px;">
                                Responsável é o próprio Paciente.
                            </label>
                        </li>
                    </ul>
                    <ul class="ulDadosPaciente" style="width: 600px !important; margin-left: 5px;">
                        <li style="margin-bottom: 2px; margin-top:50px; margin-left:-272px;">
                            <label class="lblTop">
                                ENDEREÇO RESIDENCIAL/CORRESPONDÊNCIA</label>
                        </li>
                        <li style="clear: both;">
                            <label>
                                CEP
                                <font style="color: red;"> *</font>
                                <asp:RequiredFieldValidator runat="server" id="rfvTxtCEP" controltovalidate="txtCEP" errormessage=" Obrigatório" ValidationGroup="validaDados"/>
                            </label>
                            <asp:TextBox runat="server" ID="txtCEP" Width="70px" CssClass="campoCepF" MaxLength="10" ToolTip="Informe o CEP."></asp:TextBox>
                            <li style="margin-top: 14px; margin-left: -15px;">
                                <asp:ImageButton ID="imgPesqCEP" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" style="height:16px; width:16px;" OnClick="imgPesqCEP_OnClick"/>
                            </li>
                        </li>
                        <li>
                            <label>
                                UF
                                <font style="color: red;"> *</font>
                            </label>
                            <asp:DropDownList runat="server" ID="ddlUF" Width="40px" ToolTip="Informe o UF."
                                AutoPostBack="true" OnSelectedIndexChanged="ddlUF_OnSelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label>
                                Cidade
                                <font style="color: red;"> *</font>
                            </label>
                            <asp:DropDownList runat="server" ID="ddlCidade" Width="128px" ToolTip="Informe a Cidade."
                                AutoPostBack="true" OnSelectedIndexChanged="ddlCidade_OnSelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label>
                                Bairro</label>
                            <asp:DropDownList runat="server" ID="ddlBairro" Width="115px" ToolTip="Informe o Bairro."/>
                        </li>
                        <li style="margin-right: -30px !important;">
                            <label>
                                Logradouro
                                <font style="color: red;"> *</font>
                                <asp:RequiredFieldValidator runat="server" id="rfvTxtLogradouro" controltovalidate="txtLogradouro" CssClass="validatorField" errormessage=" Obrigatório" ValidationGroup="validaDados"/>
                            </label>
                            <asp:TextBox runat="server" ID="txtLogradouro" Width="192px" style="margin-right:-15px !important;" MaxLength="100" ToolTip="Informe o Logradouro."></asp:TextBox>
                        </li>
                    </ul>
                    <ul class="ulDadosPaciente" style="margin-left: 5px;">
                        <li style="margin-bottom: 10px;margin-top: 10px;">
                            <label class="lblTop">
                                DADOS DO PACIENTE - USUÁRIO DE SAÚDE</label>
                            <label class="lblTop" style="color: #ff6700 !important;">
                                Informe abaixo os dados referentes ao Paciente
                            </label>
                        </li>
                        <li style="clear: both;">
                            <label>
                                CPF
                            </label>
                            <asp:TextBox runat="server" ID="txtCPFPac" CssClass="campoCpf" MaxLength="18" ClientIDMode="Static" ToolTip="Informe o CPF do paciente."></asp:TextBox>
                            <li style="margin-top: 14px; margin-left: -15px; margin-right: 12px;">
                                <asp:ImageButton ID="imgPesqPacCPF" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" style="height:16px; width:16px; visibility:hidden;" OnClick="imgPesqPacCPF_OnClick"/>
                            </li>
                        </li>
                        <li>
                            <label>
                                Nome
                                <font style="color: red;"> *</font>
                                <asp:RequiredFieldValidator runat="server" id="rfvTxtNomePac" controltovalidate="txtNomePac" errormessage=" Obrigatório" CssClass="validatorField" Display="Dynamic" ValidationGroup="validaDados"/>
                            </label>
                            <asp:TextBox runat="server" ID="txtNomePac" ToolTip="Nome do Paciente" Width="317px" MaxLength="60" CssClass="campoNome" ClientIDMode="Static"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Sexo
                                <font style="color: red;"> *</font>
                                <asp:RequiredFieldValidator runat="server" id="rfvDdlSexoPac" controltovalidate="ddlSexoPac" errormessage=" Obrigatório" ValidationGroup="validaDados"/>
                            </label>
                            <asp:DropDownList runat="server" ID="ddlSexoPac" Width="75px" ClientIDMode="Static" ToolTip="Informe o sexo do paciente.">
                                <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Feminino" Value="F"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label>
                                Nascimento
                                <font style="color: red;"> *</font>
                            </label>
                            <asp:TextBox runat="server" ID="txtDtNascPac" CssClass="campoData" ClientIDMode="Static" ToolTip="Informe a data de nascimento do paciente."></asp:TextBox>
                        </li>
                        <li style="clear: both">
                            <label>Contratação</label>
                            <asp:DropDownList ID="ddlOperadora" ToolTip="Informe o tipo de contratação do paciente."
                                 OnSelectedIndexChanged="ddlOperadora_OnSelectedIndexChanged" AutoPostBack="True" runat="server" Width="221px" />
                        </li>
                        <li>
                            <label>Plano</label>
                            <asp:DropDownList ID="ddlPlano" ToolTip="Selecione um plano"
                                AutoPostBack="True" runat="server" Width="124px" />
                        </li>
                        <li>
                            <label>Numero</label>
                            <asp:TextBox ID="txtNumeroPlano" runat="server" Width="155px" MaxLength="25" ToolTip="Informe o número do plano."> </asp:TextBox>
                        </li>
                        <li>
                            <label title="Data de Vencimento do Plano">
                                Dt Vencim.</label>
                            <asp:TextBox runat="server" ID="txtDtVencPlan" CssClass="campoData" ToolTip="Data de Vencimento do Plano"></asp:TextBox>
                        </li>
                        <li style="clear: both">
                            <label>Deficiência</label>
                            <asp:DropDownList runat="server" ID="ddlDeficiencia" Width="260px" ToolTip="Informe caso haja alguma deficiência."/>
                        </li>
                    </ul>
                    <ul class="ulDadosPaciente" style="margin-left: 5px;">
                        <li style="margin-left: -83px; margin-bottom: 15px; margin-top: 10px;">
                            <div id="div1" class="boxCornerTitle" style="background-color:#b4eeb4 !important;  margin-left: 13%;">
                                <h1 style="color: black !important; font-weight: bold !important;">INFORMAÇÕES DA NECESSIDADE DE CONSULTA</h1>
                            </div>
                        </li>
                        <li style="clear: both;">
                            <label>
                                Unidade
                                <font style="color: red;"> *</font>
                                <asp:RequiredFieldValidator runat="server" id="rfvddlUnidade" controltovalidate="ddlUnidade" ValidationGroup="validaDados" ErrorMessage="Obrigatório."/>
                            </label>
                            <asp:DropDownList ID="ddlUnidade" Width="145px" runat="server" ToolTip="Selecione a unidade"
                                OnSelectedIndexChanged="ddlUnidade_OnSelectedIndexChanged" AutoPostBack="true" />
                        </li>
                        <li>
                            <label>
                                Especialidade
                                <font style="color: red;"> *</font>
                                <asp:RequiredFieldValidator runat="server" id="rfvDdlEspecialidade" controltovalidate="ddlEspecialidade" ValidationGroup="validaDados"/>
                            </label>
                            <asp:DropDownList runat="server" ID="ddlEspecialidade"  Width="125px" ToolTip="Selecione a Especialidade" />
                        </li>
                        <li>
                            <label>
                                Tipo
                                <font style="color: red;"> *</font>
                                <asp:RequiredFieldValidator runat="server" id="rfvDdlTipoAg" controltovalidate="ddlTipoAg" ValidationGroup="validaDados"/>
                            </label>
                            <asp:DropDownList runat="server" Width="59px" ID="ddlTipoAg" ToolTip="Tipo de Agendamento">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label>
                                Data
                                <font style="color: red;"> *</font>
                                <asp:RequiredFieldValidator runat="server" id="rfvTxtData" controltovalidate="txtData" ValidationGroup="validaDados"/>
                            </label>
                            <asp:TextBox ID="txtData" runat="server" CssClass="campoData" ToolTip="Informe a data do agendamento."/>
                        </li>
                        <li style="margin-left: 10px;">
                            <label>
                                Hora
                            </label>
                            <asp:TextBox ID="txtHora" runat="server" ToolTip="Informe a hora de início" CssClass="campoHora" MaxLength="5">
                            </asp:TextBox>
                        </li>
                        <li style="margin-top: -26px; margin-left: 518px;">
                            <label style="margin-top: -13px;">
                                Confirmar:
                            </label>
                            <asp:CheckBox runat="server" ID="chkEnviarSMS" CssClass="chk" style="margin-left:-5px;"/>
                            <label style="margin-top: -14px; margin-left: 18px; margin-bottom:2px;">
                                Por SMS.
                            </label>
                            <asp:CheckBox runat="server" ID="chkEnviarEmail" CssClass="chk" style="margin-left:-5px; display:none;"/>
                            <label style="margin-top: -14px; margin-left: 18px; display:none;">
                                Por E-mail.
                            </label>
                        </li>
                        <li style="margin-bottom: 7px;">
                            <label>
                                Sintomas
                            </label>
                            <asp:TextBox ID="txtSintomas" Rows="3" TextMode="MultiLine" runat="server" style="width: 636px; height: 18px;" MaxLength="400" ToolTip="Informe os sintomas do paciente."/>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="liBtnConfirm" style="width: 148px; float: right; margin: 5px 230px 10px 0px; z-index: 1001;">
                <asp:LinkButton ID="lnkbSalvar" OnClick="lnkbSalvar_OnClick"
                    runat="server" ToolTip="Não realiza o encaminhamento"
                    ValidationGroup="validaDados">
                    <label style="margin-left:5px; color:White; cursor:pointer;">ENVIAR PRÉ AGENDAMENTO</label>
                </asp:LinkButton>
            </div>
            <div id="divFooter">
            </div>
            <div id="divAgradecimento" style="margin-bottom: -55px; display: none;" class="divAgradecimento">
                <label style="color:red; font-size:15px !important; font-family:Tahoma; font-weight: bold !important;" runat="server" id="lblMsgSucesso">
                    Seu Agendamento foi enviado com Sucesso.
                </label>
                <label style="color:red; font-size:15px !important; font-family:Tahoma; font-weight: bold !important;" runat="server" id="lblPreAgendamento">
                    Seu número de Pré-Agendamento é: 
                </label>
                <label style="color:red; font-size:15px !important; font-family:Tahoma; font-weight: bold !important; margin: -17px 0px 0px 275px;" runat="server" id="lblCodigoAtendimento">
                </label>
                <label style="color:red; font-size:15px !important; font-family:Tahoma; font-weight: bold !important; display:none;" runat="server" id="lblConfirmaEmail">
                    Foi enviado e-mail confirmação de pré-agendamento ao e-mail cadastrado.
                </label>
            </div>
        </form>
    </div>
</body>
</html>
<script type="text/javascript">

    $(document).ready(function () {
        $(".campoCpf").mask("999.999.999-99");
        $(".campoHora").mask("99:99");
        $(".campoCepF").mask("99999-999");
        $(".campoData").mask("99/99/9999");
        $(".campoTel").mask("(99)9?9999-9999");
        $(".campoNome").keyup(function () {
            this.value = this.value.replace(/[0-9\.,;:<>]/g, '');
        });
        $("#chkPaciEhResp").click(function () {
            if (document.getElementById('chkPaciEhResp').checked) {
                //Controle do CPF
                $("#txtCPFPac").attr('disabled', 'disabled');
                //$("#txtCPFPac").css('margin-right', '37px');

                $("#txtCPFPac").val($("#txtCPFResp").val());
                //Controle do Nome
                $("#txtNomePac").attr('disabled', 'disabled');
                $("#txtNomePac").val($("#txtNomeResp").val());
                //Controle do Sexo
                $("#ddlSexoPac").attr('disabled', 'disabled');
                $("#ddlSexoPac").val($("#ddlSexoResp").val());
                //Controle da Data de Nascimento
                $("#txtDtNascPac").attr('disabled', 'disabled');
                $("#txtDtNascPac").val($("#txtDtNascResp").val());
            } else {
                //Controle do CPF
                $("#txtCPFPac").removeAttr('disabled');
                //$("#txtCPFPac").css('margin-right', '12px');
                //$("#imgPesqPacCPF").show();
                //Controle do Nome
                $("#txtNomePac").removeAttr('disabled');
                //Controle do Sexo
                $("#ddlSexoPac").removeAttr('disabled');
                //Controle da Data de Nascimento
                $("#txtDtNascPac").removeAttr('disabled');
            }
        });
        if (document.getElementById('chkPaciEhResp').checked) {
            //Controle do CPF
            $("#txtCPFPac").attr('disabled', 'disabled');
            //$("#txtCPFPac").css('margin-right', '37px');
            //$("#imgPesqPacCPF").hide();
            $("#txtCPFPac").val($("#txtCPFResp").val());
            //Controle do Nome
            $("#txtNomePac").attr('disabled', 'disabled');
            $("#txtNomePac").val($("#txtNomeResp").val());
            //Controle do Sexo
            $("#ddlSexoPac").attr('disabled', 'disabled');
            $("#ddlSexoPac").val($("#ddlSexoResp").val());
            //Controle da Data de Nascimento
            $("#txtDtNascPac").attr('disabled', 'disabled');
            $("#txtDtNascPac").val($("#txtDtNascResp").val());
        } else {
            //Controle do CPF
            $("#txtCPFPac").removeAttr('disabled');
            //$("#txtCPFPac").css('margin-right', '12px');
            //$("#imgPesqPacCPF").show();
            //Controle do Nome
            $("#txtNomePac").removeAttr('disabled');
            //Controle do Sexo
            $("#ddlSexoPac").removeAttr('disabled');
            //Controle da Data de Nascimento
            $("#txtDtNascPac").removeAttr('disabled');
        }
        $("#txtNomeResp").keyup(function () {
            if (document.getElementById('chkPaciEhResp').checked) {
                $("#txtNomePac").val($("#txtNomeResp").val());
            }
        });
        $("#txtCPFResp").keyup(function () {
            if (document.getElementById('chkPaciEhResp').checked) {
                $("#txtCPFPac").val($("#txtCPFResp").val());
            }
        });
        $("#ddlSexoResp").change(function () {
            if (document.getElementById('chkPaciEhResp').checked) {
                $("#ddlSexoPac").val($("#ddlSexoResp").val());
            }
        });
        $("#txtDtNascResp").change(function () {
            if (document.getElementById('chkPaciEhResp').checked) {
                $("#txtDtNascPac").val($("#txtDtNascResp").val());
            }
        });
        $("#txtEmailResp").change(function () {
            if ($("#txtEmailResp").val() != "") {
                $("#chkEnviarEmail").attr('checked', true);
            } else {
                $("#chkEnviarEmail").attr('checked', false);
            }
        });
    });
</script>