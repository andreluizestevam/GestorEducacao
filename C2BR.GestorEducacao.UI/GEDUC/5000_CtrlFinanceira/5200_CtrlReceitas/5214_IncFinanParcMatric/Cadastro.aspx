<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5214_IncFinanParcMatric.Cadastro" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        input[type="text"]
        {
            font-size: 10px !important;
            font-family: Arial !important;
        }
        select
        {
            margin-bottom: 4px;
            font-family: Arial !important;
            border: 1px solid #BBBBBB !important;
            font-size: 0.9em !important;
            height: 15px !important;
        }
        
        /*--> CSS LIs */
        .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: -18px;
            margin-bottom: 8px;
            padding: 2px 3px 1px 3px;
            margin-left: 605px;
            clear:none;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
            clear:both;
        }
        .liBtnConfir
        {
            background-color:#F0FFFF;
            border:1px solid #D2DFD1;
            clear:none;
            margin-bottom:4px;
            padding:2px 3px 1px;
            margin-left: 5px;
            margin-right: 0px;
        }
        .liEtniaA { margin-left: 10px; }
        .liNire
        {
            margin-top: 3px;
            margin-left: 120px;
        }
        .liBairroETA { margin-left: 45px; }
        .liGridTelETA
        {
            clear: both;
            margin-left: 45px;
        }
        .liGridCuiEsp
        {
            clear: both;
            margin-left: 30px;
            margin-top: 10px;
        }
        .liBtnsETA {float:right !important;margin-top:-4px; margin-right: 0px; }
        .liBtnsCEA {width: 55px; margin-left: 5px;margin-right: 0px;}
        .liBtnsEndAdd { margin-top: 10px; margin-left: 30px; margin-right: 0px; }
        .liGridUni
        {
            clear: both;
            margin-left: 10px;
        }
        .liGridMat { margin-left: 17px; margin-top: 6px; }
        .liGridAtv { margin-left: 120px; }  
        .liPhoto
        {
            float: left !important;
            clear: both;
            margin-top: 7px;
            margin-left:4px;
        }
        .liIdentidade
        {
            margin-top: 10px;
            width: 330px;
            margin-left:35px;
        }
        .liTituloEleitor
        {
            margin: 10px 0 0 30px;
            width: 260px;
        }
        .liDist { margin-top: 3px; }
        .liDados1
        {
            padding-left: 10px;
            margin-top: 4px;
            width: 660px;
            margin-right:0 !important;
        }
        .liNome, .liSexo, .liTpSangue, .liEstadoCivil, .liNacioResp, .liNisR { margin-left: 7px; }
        .liDeficiencia { margin-left: 2px; }
        .liDados3 { width: 100%; padding-left: 5px; }
        .liNumero { margin-left: 2px; }
        .liCep { margin-left: 1px; }
        .liTelCelular
        {
            clear: none;
            margin-left: 10px;
            margin-right: 0px !important;
        }
        .liGrauInstrucao
        {
            clear: both;
            margin-top:-3px;
        }
        .liRenda
        {
            margin-right: 15px !important;
            margin-top: -2px;
        }
        .liComplementoR {clear:both;margin-top:-3px;}
        .liBairroR {margin-top:-3px;}
        .liTelEmp
        {
            margin-left: 43px;
            margin-top: -3px;
        }
        .liClear { clear: both; }
        .liCidadeA
        {
            margin-top: -2px !important;
            clear: both;
        }
        .liDataNascimentoA
        {
            margin-left: 10px;
            margin-top: 3px;
        }
        .liEmailA { margin-left: 10px; }
        .liEmail
        {
            margin-left: 22px;
            margin-right: 0px !important;
        }
        .liPeriodoAteA
        {
            margin-top: 12px !important;
            margin-right: 0px !important;
        }
        .liPeriodoDeA { margin-right: 2px !important; }
        .liPhotoA
        {
            height: 100px;
            margin-top: 6px;
        }
        .liSituacaoAlunoA
        {
            margin-top: 2px;
            height: 100px;
        }
        .liUfA { margin-top: -2px !important; }
        #ulReserva liPesqReserva { margin-top: 10px; }
        .ulServs li { margin-bottom: 3px; margin-left:-3px;}
        .liSeparador {border-bottom: solid 3px #CCCCCC; width:100%;padding-bottom:5px;margin-bottom:5px; margin-top: 5px;}     
        .liCPFResp {margin-top: 10px;margin-left:-20px;}
        .liPesqCEPResp { margin-top: 14px; margin-left: -3px; }
        .liInfSocResp { clear:both; width: 360px; margin-left:-4px;border-right:2px solid #CCCCCC;height:105px;}
        .liResidResp { margin-top: -2px; }
        .liProfissaoResp { margin-top: -3px; }
        .liEndResResp { margin-left:-4px; border-right:2px solid #CCCCCC;width:360px;height: 110px;}
        .liContAlu {clear:both;margin-top:3px;}
        .liEnderecoAlu { width: 320px; margin-left: 3px; margin-top: 10px;}
        .liddlSexoAlu { margin-top: 3px; margin-left: 10px;}
        .liddlTpSangueAlu { margin-top: 3px; margin-left: 5px; }
        .litxtNisAlu { margin-left: 10px; margin-right: 0 !important; }
        .liFiliacaoAlu { clear:both; margin-top: 10px; width: 230px;height: 105px;}
        .liResidAlu { margin-left: 5px; margin-top: -2px;}   
        .liNomeAluETA { margin-left: 10px; }
        .liEspacamento {margin-left: 10px;}           
        .liDescTelAdd { margin-left: 5px; margin-top: -3px; }     
        .liddlTpTelef { margin-left: 45px; }
        .liddlTpCui { margin-left: 30px; }
        .litxtTelETA { margin-left: 10px; }
        .liNomeContETA,.liObsETA { margin-left: 10px; }
        #divBarraMatric ul li { display: inline; margin-left: -2px; }
        .liNIREAluETA,.liNISAluETA {margin-left: 10px;}
        .liPeriodo {clear:both; margin-left: 110px;}
        .liBtnsResAli { float: right !important; }
        .liBtnsAtiExt { clear: both; margin-left: 520px; }
        .liBtnGrdFinan { margin-left: 14px; padding: 4px 5px 3px; margin-top: 12px; background-color: #EE9572; border: 1px solid #8B8989; }
        .liListaEndereco { margin-top: 13px; margin-left: -3px; }
        .liBlocoCtaContabil
         {
             margin:0;
             padding:0;
             margin-top: -14px;
         }
        .liBlocoCtaContabil ul
        {
            width: 100%;
        }
        .liBlocoCtaContabil ul li
        {
            display: inline;
            margin-right: 0px;
            padding-top: 2px;
            height: 16px;
        }
        
        /*--> CSS DADOS */
        .divGridUM
        {
            height: 166px;
            width: 340px;
            overflow-y: auto;
            overflow-x: auto;
            margin-top: 4px;
        }
        .divGridUni
        {
            height: 136px;
            width: 353px;
            overflow-y: auto;
            overflow-x: auto;
            margin-top: 4px;
        }
        .divGridDoc
        {
            height: 158px;
            width: 367px;
            overflow-y: auto;
            margin-top: 10px;
        }
        .divGridTelETA
        {
        	height: 158px;
            width: 693px;
            overflow-y: auto;
        }
        .divGridCEA
        {
        	height: 150px;
            width: 690px;
            overflow-y: auto;
            overflow-x: auto;
        }        
        /*#divAddTipo { display: none; }*/
        .txtLograETA { width: 230px; }
        .txtNumETA { width: 45px; }
        .txtCompETA { width: 150px; }                
        .ulDados2 { width: 100%; }
        input[type='text'] { margin-bottom: 4px; }
        label { margin-bottom: 1px; }
        .ulDados
        {
            width: 1003px;
            margin-top: -20px !important;
        }
        .fieldset
        {
            width: 800px;
            margin: 5px 0 0 5px;
            padding-bottom: 5px;
        }                        
        .fldPhoto
        {
            border: none;
            width: 64px;
            height: 85px;
        }
        .fldIdentidade
        {
            padding-left: 11px;
            border-width: 0px;
        }
        .fldTituloEleitor
        {
            padding-left: 9px;
            border-width: 0px;
        }
        .fldDepenResp { border-width: 0px; }
        .fldDepenResp legend 
        {
        	color:Black !important;
            font-size:1.1em !important; 
            font-weight:normal !important;
        }
        .liDados3 legend 
        {
        	color: Black !important;
        	font-size: 1.0em !important;
        }
        .txtNome
        {
            width: 194px;
            text-transform: uppercase;
        }
        .txtNISResp, .txtTelCelularResp { width: 78px; }
        .txtDtNascimentoResp, .txtDtEmissaoResp { width: 60px; }
        .ddlSexoResp { width: 75px; }
        .ddlDeficienciaResp { width: 70px; }
        .ddlGrauInstrucaoResp { width: 90px; }
        .ddlProfissaoResp { width: 150px; }
        .ddlEstadoCivilResp { width: 142px; }
        .ddlRendaResp, .txtCPF { width: 82px; }
        .txtPassaporteResp { width: 70px; }
        .txtOrgEmissorResp, .txtIdentidadeResp { width: 65px; }
        .txtNumeroTituloResp { width: 70px; }
        .txtZonaResp, .txtSecaoResp { width: 25px; }
        .txtLogradouroResp { width: 209px; }
        .txtNumeroResp { width: 39px; }
        .txtComplementoResp { width: 187px; }
        .txtTelResidencialResp, .txtTelEmpresaResp { width: 78px; }
        .ddlCidadeResp { width: 165px; }
        .ddlBairroResp { width: 145px; }
        .txtCepResp { width: 56px; }
        .txtEmailResp { width: 180px; }             
        .formAuxText1
        {
            padding-top: 12px;
            margin-right: 2px !important;
        }
        fieldset
        {
            padding: 0px 0px 5px 5px;
            margin: 0;
        }        
        .txtLogradouroAluno { width: 150px; }
        .txtComplementoAluno { width: 143px; }
        .ddlCidadeAluno { width: 177px; }
        .ddlBairroAluno { width: 150px; }
        .txtResponsavelAluno { width: 200px; }
        .txtEmailAluno { width: 206px; }
        .txtCartaoSaudeAluno { width: 78px; }
        .txtNireAluno { width: 66px; }
        .txtNisAluno { width: 75px; }
        .ddlSexoAluno { width: 110px; }
        .ddlNacionalidadeAluno { width: 70px; }
        .txtNacionalidadeAluno { width: 70px; }
        .txtNaturalidadeAluno { width: 95px; }
        .ddlEstadoCivilAluno { width: 90px; }
        .ddlEtniaAluno { width: 90px; }
        .ddlRendaFamiliarAluno { width: 65px; }
        .ddlDeficienciaAluno { width: 70px; }
        .ddlPasseEscolarAluno, .ddlTransporteEscolarAluno, .ddlTrabResp { width: 40px; }
        .ddlTipoCertidaoAluno { width: 79px; }
        .txtNumeroCertidaoAluno { width: 37px; }
        .txtLivroAluno { width: 24px; }
        .txtFolhaAluno { width: 24px; }
        .txtCartorioAluno { width: 255px; }
        .txtRgAluno { width: 70px; }
        .txtOrgaoEmissorAluno { width: 55px; }
        .txtNumeroTituloAluno { width: 70px; }
        .txtSecaoAluno { width: 40px; }
        .txtZonaAluno { width: 25px; }
        .txtNumeroAluno { width: 40px; }
        .ddlBolsaAluno { width: 90px; }
        .txtDescontoAluno { width: 40px; text-align: right; }
        .txtValorTotal { width: 50px; text-align: right; }
        .chkIsento label{ margin-top: 5px; display:inline; }
        #divReserva
        {
        	top: 40px; 
        	width: 460px;
        	background-color:#EEEED1;
        	padding-left:6px;
        	padding-top:2px;
        	margin-left:7px;
        	margin-top:-1px;
        	height: 30px;
        }
        /*#helpMessages { visibility: hidden; }*/
        #helpMessagens { margin-bottom:30px !important; position: relative !important; }
        #ulDadosMatricula .lblDadMatr { font-weight:bold; color:Red; }                
        .txtAlunoReserva {width: 160px;}
        .txtDadosReserva {width: 200px;}
        #divDadosMatricula {position: fixed; right:0; top: 40px; width: 600px;}
        #divMenuLateral{position:fixed; left:0; top:107px; width:250px;padding-top:6px;border-right:2px solid #CCCCCC;height:100%;}
        #tabResp 
        {
        	position:fixed; 
        	right: 0; 
        	top: 106px; 
        	width: 748px;
        	height: 380px;
            padding: 10px 0 0 10px;
        }
        #tabDocumentos
        {
        	position:fixed; 
        	right: 0; 
        	top:106px; 
        	width: 735px;
        	height: 380px;
            padding: 10px 0 0 10px;
        }
        #tabUniMat
        {
        	position:fixed; 
        	right: 0; 
        	top:106px; 
        	width: 735px;
        	height: 380px;
            padding: 10px 0 0 10px;
        }
        .chkLocais label { display: inline !important; margin-left:-4px;}
        .chkCadBasAlu label { display: inline !important; }
        .chkMatEsc label { display: inline !important; margin-left:-4px; }
        .chkCuiEspAlu label { display: inline !important; margin-left:-4px; }
        .chkResAliAlu label { display: inline !important; margin-left:-4px; }
        .chkRegAtiExt label { display: inline !important; margin-left:-4px; }        
        .chkDocMat label { display: inline !important; margin-left:-4px; }
        .chkMenEscAlu label { display: inline !important; margin-left: 1px; }
        .ulServs { margin-left: -1px; padding-top:4px; }
        .ulServs label { display: inline !important; margin-left: 1px;}        
        .G2Clear{clear: both;}        
        #divBotoes {position:fixed; margin-top: -15px; width: 500px;}
        #divBotoes .lilnkEfetMatric 
        {
            background-color:#FAFAD2;
            border:1px solid #D2DFD1;
            clear:both;
            margin-bottom:4px;
            width: 66px;
            text-align:center;
            padding:2px 3px 1px;
            margin-left: 2px;
        }
        #divBotoes .imgliLnk { width: 15px; height: 13px; }
        .imgliLnkModSerTur { width: 21px; height: 19px; } 
        .imgWebCam { width: 15px; height: 13px; }
        #divBotoes .lilnkRecMatric 
        {
            background-color:#F0FFFF;
            border:1px solid #D2DFD1;
            margin-bottom:4px;
            padding:2px 3px 1px;     
            width: 78px;  
            margin-left:2px; 
            margin-right:0px; 
            clear:both !important;
        }
        #divBotoes .lilnkBolCarne 
        {
            background-color:#F0FFFF;
            border:1px solid #D2DFD1;
            clear:none;
            margin-bottom:4px;
            padding:2px 3px 1px;
            margin-left: 2px;
            margin-right:0px; 
            width: 58px;
        }   
        #divBotoes .lilnkCarteira
        {
            background-color:#F0FFFF;
            border:1px solid #D2DFD1;
            clear:none;
            margin-bottom:4px;
            padding:2px 3px 1px;
            margin-left: 2px;
            width: 68px;
            margin-right: 0px;
        }        
        #divBotoes .lilnkRegPgto 
        {
            background-color:#C1FFC1;
            border:1px solid #32CD32;
            clear:none;
            margin-bottom:4px;
            padding: 2px 3px 2px;
            margin-left: 5px;
            width: 52px;
            text-align: center;
            margin-right: 0px;
        }
        #divMenuLateral .lblTitInf { font-weight:bold; color:Black; }
        #divMenuLateral .lblSubTitInf { font-family: Arial; }
        #divMenuLateral .liTitInf {margin-left: 20px; margin-top: -5px;}
        .btnPesqMat,.btnPesqCID { width: 13px;}
        .btnPesqReserva { width: 13px; }
        .txtNire { width: 70px; }
        .txtNoRespCPF { width: 220px; background-color: #FFF8DC !important; }
        .txtNoInfAluno { width: 220px; background-color: #B9D3EE !important; }
        .fldFiliaResp {border-width: 0px;}
        .fldFiliaResp legend {font-weight:bold; font-size: 0.9em !important;}
        .txtMaeResp, .txtPaiResp {width: 215px;}
        .txtDepMenResp { width: 25px; }
        .txtMesAnoTrabResp { width: 45px; margin-left: 7px; }
        .ddlTpSangueAluno { width: 35px; clear: both; }
        .ddlStaTpSangueAluno { width: 28px; }
        .ddlNacioResp { width: 93px; }
        .txtQtdMenoresResp {width:20px;}
        .ddlSituMatAluno { width: 85px;}       
        .imgLnkInc { width: 19px; height: 19px; } 
        .imgLnkExc { width: 13px; height: 13px; }  
        #tabAluno  
        {
        	position:fixed; 
        	right: -5px; 
        	top:109px; 
        	width: 750px;
            height: 380px;
            padding: 7px 0 0 20px;            
        }
        #tabTelAdd, #tabEndAdd, #tabResAliAdd, #tabAtiExtAlu, #tabMenEsc
        {
        	position:fixed;
        	right: 0; 
        	top:109px; 
        	width: 748px;
            height: 380px;
            padding: 7px 0 0 10px;
        }
        #tabCuiEspAdd
        {
        	position:fixed; 
        	right: 0; 
        	top:109px; 
        	width: 748px;
            height: 380px;
            padding: 7px 0 0 10px;
        }        
        #tabAluno legend {color:Black;}
        .ddlTpResidAluno {width:70px;}
        .ddlGrauParentescoAluno {width:62px;}
        .txtNumReserva { width: 75px;}
        .imgPesRes { width: 13px; height: 15px;}
        .imgInfResp { width: 17px; height: 19px; }
        .ddlSerieCurso { width: 73px; }
        .txtObservacoesResp{ width: 355px; height: 40px; margin-top: 0px;}
        .ddlTpResidResp { width: 68px; }             
        /*#divBarraPadraoContent{display:none;}*/
        #divBarraMatric { position: absolute; margin-left: 140px; margin-top:-22px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
        #divBarraMatric ul { display: inline; float: left; margin-left: 10px; }        
        #divBarraMatric ul li img { width: 19px; height: 19px; }        
        .ddlTpTelef { width: 110px;}        
        .grdBusca th        
        { 
        	background-color: #CCCCCC;
        	color: Black;
        	text-align: left;
        }        
        .lblDivData
        {
        	display:inline;
        	margin: 0 1px;
        	margin-top: 0px;
        }         
        .txtNumCRMCEA { width: 45px; }
        .txtQtdeCEA { width: 25px; }
        .txtHrAplic { width: 30px; }
        .ddlTpCui { width: 100px; }
        .txtCodRestri { width: 45px; }
        .ddlTpRestri { width:85px; }
        .txtDescRestri { width:130px; }
        .txtAcaoRestri { width:170px; }
        .ddlGrauRestri { width:75px; }        
        .txtDescCEA { width:170px; }
        .ddlAtivExtra { width: 170px; }
        .txtSiglaAEA { width: 60px; }
        #ControleImagem .liControleImagemComp { margin-top: -2px !important; }
        #ControleImagem .liControleImagemComp .fakefile { width: 60px !important; }        
        .ddlBoleto { width:190px; }
        .txtQtdeMesesDesctoMensa { width:37px; text-align: right; }
        .txtDesctoMensa { width:70px; text-align: right; }
        .ddlTipoDesctoMensa { width:65px; }
        .chkLocais label { display: inline !important; margin-left:-4px; } 
        .ddlDiaVecto { width: 35px; }
        .txtCidadeCertidaoAlu { width: 135px; }
        .txtNomeMaeAluno { width: 220px; }
        #divSolicitacoes
        {
        	height: 150px; 
        	width: 450px;
        	overflow-y: scroll;
        	margin-top: 0px;
        }
        .txtQtdeSolic { text-align: right; width: 20px; }
        .txtDescSolic { text-align: right; width: 30px; }
        
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
    <ul id="ulDados" class="ulDados">
        <li style="width: 100%; margin-top:20px; margin-bottom: 5px;">
        </li>       
        <li style="margin-left: 10px; border-right: 2px solid #e8e8e8; height: 431px; padding-right: 9px;">
            <ul>
                <li style="margin-top: 70px;">
                    <label title="Selecione a unidade desejada">Unidade</label>
                    <asp:DropDownList ID="ddlUnidade" runat="server" Width="230px" ToolTip="Selecione a unidade desejada">
                    </asp:DropDownList>
                </li>
                <li style="clear: both;">
                    <label title="Selecione o Ano de Referência desejado">Ano</label>
                    <asp:DropDownList ID="ddlAno" runat="server" Width="47px" ToolTip="Selecione o Ano de Referência desejado">
                    </asp:DropDownList>
                </li>
                <li style="clear: both;">
                    <label title="Selecione a modalidade desejada">Modalidade</label>
                    <asp:DropDownList ID="ddlModalidade" runat="server" Width="120px" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecione a modalidade desejada">
                    </asp:DropDownList>
                </li>
                <li style="clear: both;">
                    <label title="Selecione a série/curso desejada">Série/Curso</label>
                    <asp:DropDownList ID="ddlSerieCurso" runat="server" Width="120px" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecione a série/curso desejada">
                    </asp:DropDownList>
                </li>
                <li style="clear: both;">
                    <label title="Selecione a Turma desejada">Turma</label>
                    <asp:DropDownList ID="ddlTurma" runat="server" Width="120px" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecione a Turma desejada">
                    </asp:DropDownList>
                </li>
                <li style="clear: both;">
                    <label title="Selecione o Aluno desejado">Aluno</label>
                    <asp:DropDownList ID="ddlAluno" runat="server" Width="230px" ToolTip="Selecione o Aluno desejado">
                    </asp:DropDownList>
                </li>
                <li runat="server" id="li1" class="liBtnGrdFinan" style="margin-left: 80px !important; margin-top: 5px !important; clear: both;">
                    <asp:LinkButton ID="lnkCarregaInfo" ValidationGroup="lbCarregaInfo" OnClick="lnkCarregaInfo_Click" runat="server" Style="margin: 0 auto;" ToolTip="Carrega as informações necessárias para a geração da Grid Financeira">
                        <asp:Label runat="server" ID="Label1" ForeColor="GhostWhite" Text="PROSSEGUIR"></asp:Label>
                    </asp:LinkButton>
                </li>
            </ul>
        </li>
        <!-- Este código foi retirado para adequação do código para a alteração do desconto - Victor Martins Machado - 27/02/2013 -->
        <!--li class="G2Clear" style="width: 100%; border-bottom:1px solid #CCCCCC;margin-left: 15px;padding-bottom: 2px;text-align:center;">
            <label style="font-family: Tahoma;">
            Demonstrativo das Mensalidades Escolares no ano letivo.</label>
        </li-->      
        <li>
        <div runat="server" id="divFin" style="visibility:hidden">
        <li style="margin-left: 8px !important; clear: none;">
            <ul>
                <li style="margin-top: 10px; width: 100%;">
                    <ul>                                
                        <li>
                            <div style="float: left; border-right:2px solid #e8e8e8; width: 337px; margin-left: -5px;">
                                <!-- Checkbox do tipo de contrato -->
                                <div>
                                    <asp:CheckBox ID="chkTipoContrato" OnCheckedChanged="chkTipoContrato_CheckedChange" Checked="false" CssClass="chkLocais" runat="server" Text="Qual o tipo de valor de Contrato?"
                                    ToolTip="Marque se deverá utilizar um tipo de contrato diferente." AutoPostBack="true" />
                                    
                                    <%--<span style="">Tipo Valor</span>--%>
                                    <asp:DropDownList ID="ddlTipoValorCurso" runat="server" AutoPostBack="True" 
                                        Enabled="False" 
                                        onselectedindexchanged="ddlTipoValorCurso_SelectedIndexChanged" 
                                        ToolTip="Selecione o tipo de contrato">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlTipoContrato"  Width="60px" Enabled="false"
                                        ToolTip="Selecione o tipo de pagamento" runat="server" >
                                    </asp:DropDownList>
                                </div>

                                <!-- Checkbox de alteração do valor de contrato -->
                                <div>
                                    <asp:CheckBox ID="chkAlterValorContr" CssClass="chkLocais" runat="server" Text="Altera o valor de contrato?" ToolTip="Marque se deverá gerar o total de parcelas do curso independente do ano."
                                        OnCheckedChanged="chkAlterValorContr_CheckedChanged" AutoPostBack="true"/>

                                    <asp:TextBox Enabled="false" ID="txtValorContratoCalc" OnTextChanged="txtValorContratoCalc_TextChanged" 
                                    AutoPostBack="true" CssClass="txtValorContratoCalc" style="text-align: right;margin-left: 4px;" 
                                    runat="server" Width="50px" />

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="vgMontaGridMensa"
                                    runat="server" ControlToValidate="txtValorContratoCalc" ErrorMessage="Valor de Contrato deve ser informado"
                                    Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                </div>

                                <!-- Checkbox do valor do contrato proporcional -->
                                <div>
                                        <asp:CheckBox ID="chkValorContratoCalc" Checked="false" runat="server" CssClass="chkLocais" OnCheckedChanged="chkValorContratoCalc_CheckedChanged" ToolTip="Marque se o sistema deverá calcular o valor do contrato." Text="Calcular valor de contrato?"
                                        AutoPostBack="true" />                                            

                                    <asp:DropDownList ID="ddlValorContratoCalc" Width="125px" Enabled="false" style="margin-left: 4px;"
                                        ToolTip="Selecione o Nome da Bolsa" runat="server" >
                                        <asp:ListItem Value="T" Selected="true">Total (Todos os meses)</asp:ListItem>
                                        <asp:ListItem Value="P">Proporcional Meses</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <!-- Checkbox de geração do total de parcelas -->
                                <div>
                                    <asp:CheckBox ID="chkGeraTotalParce" CssClass="chkLocais" runat="server" Text="Altera o n° original de parcelas de cadastro?" ToolTip="Marque se deverá alterar o n° original de parcelas cadastrado na série."
                                        OnCheckedChanged="chkGeraTotalParce_CheckedChanged" AutoPostBack="true"/>

                                    <asp:TextBox ID="txtQtdeParcelas" OnTextChanged="txtQtdeParcelas_TextChanged" AutoPostBack="false" ToolTip="Informa a quantidade de parcelas da série/curso" Width="15px" CssClass="txtQtdeMesesDesctoMensa" runat="server" Enabled="false">
                                    </asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="vgMontaGridMensa"
                                    runat="server" ControlToValidate="txtQtdeParcelas" ErrorMessage="Quantidade de parcelas da série/curso deve ser informada"
                                    Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                    <asp:Label Text="NPI" runat="server" ToolTip="Número da Parcela Inicial" />
                                    <asp:TextBox ID="txtNPI" Width="18px" runat="server" Text="1" ToolTip="Número da Parcela Inicial" CssClass="campoNumerico" Enabled="false" />
                                </div>         

                                <!-- Checkbox da primeira parcela -->
                                <div>
                                    <asp:CheckBox ID="chkDataPrimeiraParcela" Checked="false" CssClass="chkLocais"
                                        runat="server" Text="Altera data/valor 1ª parcela?" OnCheckedChanged="chkDataPrimeiraParcela_CheckedChange"
                                        ToolTip="Marque se deverá informar a data da primeira parcela."
                                        AutoPostBack="true"/>

                                    <asp:TextBox ID="txtDtPrimeiraParcela" ToolTip="Informa a data de pagamento da primeira parcela." 
                                        CssClass="txtPeriodoIniDesconto campoData" runat="server" Enabled="false">
                                    </asp:TextBox>

                                    <span> / R$</span>

                                    <asp:TextBox ID="txtValorPrimParce" CssClass="campoMoeda" Width="48px" style="text-align: right;" ToolTip="Informe o valor da primeira parcela" runat="server" Enabled="false" >
                                    </asp:TextBox>
                                </div>
                                <div style="margin-bottom:5px">
                                    <asp:CheckBox runat="server" Checked="true" CssClass="chkLocais" ID="chkTaxaMatricula" Text="Gera com Taxa de Matrícula"
                                    OnCheckedChanged="chkTaxaMatricula_OnCheckedChanged" AutoPostBack="true"/>
                                    <asp:TextBox ID="txtVlTxMatricula" runat="server" Enabled="false" Width="50px" CssClass="campoMoeda"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: right; margin-left: 6px;">
                                <!-- Checkbox do desconto por bolsa -->
                                <div>
                                    <div>
                                        <asp:CheckBox ID="chkManterDesconto" Checked="false" CssClass="chkLocais" runat="server" Text="Altera Bolsa/Convênio " ToolTip="Marque se deverá manter desconto de Bolsa/Convênio"
                                        OnCheckedChanged="chkManterDesconto_CheckedChanged" AutoPostBack="true"/>

                                        <asp:DropDownList ID="ddlTpBolsaAlt" Width="75px" Enabled="false" OnSelectedIndexChanged="ddlTpBolsaAlt_SelectedIndexChanged"
                                            ToolTip="Selecione o Nome da Bolsa" AutoPostBack="True"  runat="server" >
                                        <asp:ListItem Value="N" Selected="True">Nenhuma</asp:ListItem>
                                            <asp:ListItem Value="B">Bolsa</asp:ListItem>
                                            <asp:ListItem Value="C">Convênio</asp:ListItem>
                                        </asp:DropDownList>

                                        <span>/</span>

                                        <asp:DropDownList ID="ddlBolsaAlunoAlt" Enabled="false" CssClass="ddlBolsaAluno" Width="135px" ToolTip="Selecione o nome da Bolsa"
                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBolsaAlunoAlt_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <br />
                                    </div>
                                    <div style="margin-top: 5px; margin-left: 6px;">
                                        <span>R$/% mês</span>

                                        <asp:TextBox ID="txtValorDescto" CssClass="txtDescontoAluno" Width="50px" ToolTip="Informe o valor do Desconto" runat="server" Enabled="true" >
                                        </asp:TextBox>

                                        <asp:CheckBox CssClass="chkLocais" style="margin-left: -2px;" ID="chkManterDescontoPerc" TextAlign="Right" 
                                        Enabled="false" runat="server" ToolTip="% de Desconto da Bolsa?" Text="%" /> 

                                        <span title="Período de Duração do Período do desconto" style="margin-left: 3px;">
                                            Período</span>
                                        <asp:TextBox ID="txtPeriodoIniDesconto" style="clear: both;" Enabled="false" ToolTip="Informe a Data do Período do desconto" runat="server"
                                            CssClass="txtPeriodoIniDesconto campoData"></asp:TextBox>

                                        <span>à</span>

                                        <asp:TextBox ID="txtPeriodoFimDesconto" Enabled="false" ToolTip="Informe a Data de Término do Período do desconto" runat="server"
                                            CssClass="txtPeriodoIniDesconto campoData"></asp:TextBox>
                                    </div>
                                </div>

                                <!-- Div de desconto especial -->
                                <div style="margin-left: 6px; margin-top: 9px;">
                                    <ul>                                
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                        <li style="margin-bottom: -2px;">
                                            <label title="Desconto Especial de Contrato" style="color: Red;">
                                                Desconto Especial de Contrato</label>
                                        </li>
                                        <li class="liClear">
                                            <label for="ddlTipoDesctoMensa" title="Tipo de desconto da mensalidade">
                                                Tipo Desconto</label>
                                            <asp:DropDownList ID="ddlTipoDesctoMensa" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoDesctoMensa_SelectedIndexChanged" ToolTip="Selecione o Tipo de Desconto da Mensalidade" CssClass="ddlTipoDesctoMensa" runat="server">
                                                <asp:ListItem Selected="true" Text="Total" Value="T"></asp:ListItem>
                                                <asp:ListItem Text="Mensal" Value="M"></asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                        <li style="margin-left: 10px;">
                                            <label for="txtQtdeMesesDesctoMensa" title="Quantidade de meses de desconto de mensalidade">
                                                Qt Meses</label>
                                            <asp:TextBox ID="txtQtdeMesesDesctoMensa" ToolTip="Informa a quantidade de meses de desconto de mensalidade" CssClass="txtQtdeMesesDesctoMensa" runat="server" Enabled="false">
                                            </asp:TextBox>
                                        </li>
                                        <li style="margin-left: 10px;">
                                            <label for="txtDesctoMensa" title="R$ Desconto">
                                                R$ Desconto</label>
                                            <asp:TextBox ID="txtDesctoMensa" CssClass="txtDesctoMensa" runat="server">
                                            </asp:TextBox>
                                        </li>
                                        <li>
                                            <label for="txtMesIniDesconto" title="Parcela de início do desconto">
                                                PID</label>
                                            <asp:TextBox ID="txtMesIniDesconto" Enabled="false" Width="20px" ToolTip="Parcela de início do desconto" CssClass="txtMesIniDesconto" style="text-align: right;" runat="server">
                                            </asp:TextBox>
                                        </li>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>                                
                                    </ul>
                                </div>
                            </div>
                        </li>
                    </ul>
                </li>
                <li class="liClear" style="margin-top: 0px;">
                    <div>
                        <div style="float: left;">
                            <label for="ddlBoleto" title="Boleto Bancário">Boleto</label>
                            <asp:DropDownList ID="ddlBoleto" runat="server" CssClass="ddlBoleto" style="clear: both;"
                                ToolTip="Selecione o Boleto Bancário">
                            </asp:DropDownList>
                        </div>

                        <div style="float: right; margin-left: 5px;">
                            <div style="float: left;">
                                <label for="ddlTipoDesctoMensa" title="Dia de vencimento">
                                    Dia</label>
                                <asp:DropDownList ID="ddlDiaVecto" ToolTip="Selecione o Dia de Vencimento da Mensalidade" CssClass="ddlDiaVecto" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div style="float:right; margin-left: 5px; margin-top: 14px;">
                                <asp:CheckBox ID="chkAtualiFinan" CssClass="chkLocais" AutoPostBack="true" OnCheckedChanged="chkAtualiFinan_CheckedChanged" Checked="true" runat="server" Text="Atualizar Financeiro" ToolTip="Marque se deverá atualizar o financeiro"/>
                            </div>
                        </div>
                    </div>
                </li>
                <li runat="server" id="liBtnGrdFinan" class="liBtnGrdFinan" style="margin-left: 201px; margin-top: 7px; margin-right: 10px;">
                    <asp:LinkButton ID="lnkMontaGridMensa" OnClick="lnkMontaGridMensa_Click" ValidationGroup="vgMontaGridMensa" runat="server" Style="margin: 0 auto;" ToolTip="Montar Grid Financeira">                                        
                        <asp:Label runat="server" ID="Label6" ForeColor="GhostWhite" Text="GERAR GRID FINANCEIRA"></asp:Label>
                    </asp:LinkButton>
                </li>                  
            </ul>                                        
        </li>                                                             
        <li class="labelInLine" style="margin-left: 8px !important; width: 681px; margin-top:-3px">
            <ul>
                <li style="width: 681px;">
                    <div id="divMensaAluno" runat="server" style="height: 245px; border: 1px solid #CCCCCC; overflow-y: auto; margin-top: 10px;">
                    <asp:GridView runat="server" ID="grdNegociacao" CssClass="grdBusca" ToolTip="Grid demonstrativa das mensalidades do aluno." AutoGenerateColumns="False" Width="100%">
                        <RowStyle CssClass="rowStyle" />
                        <HeaderStyle CssClass="th" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="NU_DOC" HeaderText="Nº Docto">
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="NU_PAR" HeaderText="Nº Par">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dtVencimento" HeaderText="Dt Vencto" DataFormatString="{0:d}">
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="valorParcela" DataFormatString="{0:N2}" HeaderText="R$ Mensal">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valorBolsa" DataFormatString="{0:N2}" HeaderText="R$ Bolsa">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valorDescto" DataFormatString="{0:N2}" HeaderText="R$ Descto">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valorLiquido" DataFormatString="{0:N2}" HeaderText="R$ Liquido">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valorMulta" DataFormatString="{0:N2}" HeaderText="% Multa">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valorJuros" DataFormatString="{0:N2}" HeaderText="% Juros">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>                        
                    </asp:GridView>
                    </div>
                </li>
                <li style="clear: both;">
                    <ul>
                        <li style="clear:both;margin-top: 2px;">
                            <label title="Valor Total do Contrato de Mensalidade" style="margin-right:5px !important;">R$ Contrato</label>
                            <asp:TextBox ID="txtTotalMensa" CssClass="txtValor campoMoeda" style="width: 75px;" runat="server" ToolTip="Valor Total do Contrato de Mensalidade" Enabled="false"></asp:TextBox>                    
                        </li>  
                        <li style="margin-top: 2px; margin-left: 15px;">
                            <label title="Valor Total da Bolsa Escolar do Aluno" style="margin-right:5px !important;">R$ Bolsa Escolar</label>          
                            <asp:TextBox ID="txtTotalDesctoBolsa" CssClass="txtValor campoMoeda" style="width: 75px;" runat="server" ToolTip="Valor Total da Bolsa Escolar do Aluno" Enabled="false"></asp:TextBox>
                        </li>    
                        <li style="margin-top: 2px; margin-left: 15px;">
                            <label title="Valor Total do Desconto Especial" style="margin-right:5px !important;">R$ Descto Esp</label>          
                            <asp:TextBox ID="txtTotalDesctoEspec" CssClass="txtValor campoMoeda" style="width: 75px;" runat="server" ToolTip="Valor Total do Desconto Especial" Enabled="false"></asp:TextBox>
                        </li>
                        <li style="margin-top: 2px; margin-left: 15px;">
                            <label title="Valor Total Total Líquido do Contrato" style="margin-right:5px !important;">R$ Líquido Contr</label>          
                            <asp:TextBox ID="txtTotalLiquiContra" CssClass="txtValor campoMoeda" style="width: 75px;" runat="server" ToolTip="Valor Total Total Líquido do Contrato" Enabled="false"></asp:TextBox>
                        </li>
                    </ul>
                </li>
            </ul>
        </li> 
        </div>            
        </li>                                      
    </ul>
    <script type="text/javascript">
            $(document).ready(function () {
                $(".campoNumerico").mask("?999");
                $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });

                $(".txtNumeroImpressaoSerCur").mask("?99");
                $(".txtCargaHorariaSerCur").mask("?9999");
                $(".campoMoeda").blur(function () {
                    var a = $(this).val();
                    if (a == "") {
                        $(this).val("0,00");
                    }

                });
            });

    
    </script>
</asp:Content>
