<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.Matricula.Cadastro"
    Title="Untitled Page" %>
<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem" TagPrefix="uc1" %>
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
        #divAddTipo { display: none; }
        .txtLograETA { width: 230px; }
        .txtNumETA { width: 45px; }
        .txtCompETA { width: 150px; }                
        .ulDados2 { width: 100%; }
        input[type='text'] { margin-bottom: 4px; }
        label { margin-bottom: 1px; }
        .ulDados
        {
            width: 1003px;
            margin-top: -15px !important;
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
        #helpMessages { visibility: hidden; }
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
        #divBarraPadraoContent{display:none;}
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
    <li style="margin-left: 5px;">
        <label for="ddlSituMatAluno" class="lblObrigatorio" title="Tipo de Matr�cula">
            Tipo de Matr�cula</label>
        <asp:DropDownList ID="ddlSituMatAluno" CssClass="ddlSituMatAluno" ToolTip="Selecione o Tipo de Matr�cula" runat="server">
            <asp:ListItem value="R">Com Reserva</asp:ListItem>
            <asp:ListItem value="S" Selected="true">Sem Reserva</asp:ListItem>
        </asp:DropDownList>
    </li>
    <li style="width: 460px; margin-right: 0px;">
            <div id="divReserva">
                <ul id="ulReserva">
                    <li style="margin-bottom: 2px;">
                        <h7 style="font-weight:bold;margin-bottom: 1px;">Reserva de Vagas</h7>
                    </li>
                    <li style="clear:both;">
                        <span title="N�mero da Reserva">
                            N�</span>
                        <asp:TextBox ID="txtNumReserva" CssClass="txtNumReserva" runat="server" ToolTip="Informe o N�mero da Reserva" MaxLength="20"></asp:TextBox>
                    </li>
                    <li id="liPesqReserva" class="liPesqReserva" runat="server">
                        <asp:ImageButton ID="btnPesqReserva" runat="server" onclick="btnPesqReserva_Click"
                            ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" CssClass="btnPesqReserva"
                            CausesValidation="false"/>
                    </li>
                        <%-- <a href='<%= Request.Url.AbsoluteUri %>'>--%>
                    <li style="margin-top: -12px;">
                        <label title="Dados Reserva">Dados Reserva</label>
                        <asp:TextBox ID="txtDadosReserva" CssClass="txtDadosReserva" Enabled="false" Width="170px" runat="server"></asp:TextBox>
                    </li>
                    <li style="margin-top: -13px; margin-right: 0px;">
                        <label title="Unidade de Ensino">Unid de Ensino</label>
                        <asp:DropDownList ID="ddlUnidade" AutoPostBack="true" CssClass="ddlUnidadeEscolar" Width="160px" ToolTip="Selecione a Unidade de Ensino"
                          OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged"  runat="server">
                        </asp:DropDownList>
                    </li>
                </ul>  
            </div>     
        </li>
        <li style="margin-right: 0px;">
            <ul id="ulDadosMatricula">                                          
                <li style="margin-top: 10px;margin-left: 15px; float: right;">
                    <div id="divBotoes">
                        <ul>
                            <li>
                                <div id="div4" class="bar" > 
                                    <div id="divBarraMatric" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
                                    <ul id="ulNavegacao" style="width: 39px;">
                                        <li id="btnVoltarPainel">
                                            <a href="javascript:parent.showHomePage();">
                                                <img title="Clique para voltar ao Painel Inicial." 
                                                        alt="Icone Voltar ao Painel Inicial." 
                                                        src="/BarrasFerramentas/Icones/VoltarPainelGestor.png" />
                                            </a>
                                        </li>
                                        <li id="btnVoltar" style="margin-right: 0px; clear: none;">
                                            <a href="javascript:BackToHome();">
                                                <img title="Clique para voltar a Pagina Anterior."
                                                        alt="Icone Voltar a Pagina Anterior." 
                                                        src="/BarrasFerramentas/Icones/VoltarPaginaAnterior.png" />
                                            </a>
                                        </li>
                                    </ul>
                                    <ul id="ulEditarNovo" style="width: 39px;">
                                        <li id="btnEditar" style="margin-right: 2px;">
                                            <img title="Abre o registro atualmente selecionado em modo de edicao." alt="Icone Editar." src="/BarrasFerramentas/Icones/EditarRegistro_Desabilitado.png" />
                                        </li>
                                        <li id="btnNovo" style="margin-right: 0px;">
                                            <img title="Abre o formulario para Criar um Novo Registro."
                                                alt="Icone de Criar Novo Registro." 
                                                src="/BarrasFerramentas/Icones/NovoRegistro_Desabilitado.png" />
                                        </li>
                                    </ul>
                                    <ul id="ulGravar">
                                        <li style="margin-right: 0px;">
                                            <img title="Grava o registro atual na base de dados."
                                                    alt="Icone de Gravar o Registro." 
                                                    src="/BarrasFerramentas/Icones/Gravar_Desabilitado.png" />
                                        </li>
                                    </ul>
                                    <ul id="ulExcluirCancelar" style="width: 39px;">
                                        <li id="btnExcluir" style="margin-right: 2px;">
                                            <img title="Exclui o Registro atual selecionado."
                                                    alt="Icone de Excluir o Registro." 
                                                    src="/BarrasFerramentas/Icones/Excluir_Desabilitado.png" />
                                        </li>
                                        <li id="btnCancelar" style="margin-right: 0px;">
                                            <%--  </a>--%>
                                                <img title="Cancela a Pesquisa atual e limpa o Formul�rio de Par�metros de Pesquisa."
                                                        alt="Icone de Cancelar Operacao Atual." 
                                                        src="/BarrasFerramentas/Icones/Cancelar_Desabilitado.png" />
                                            <%-- <li id="li7" class="liLeft" style="margin-top: -2px; clear: both; margin-left:20px;">
                                <label for="txtAno" title="Turma de Cadastro">
                                    Turma Cad</label>
                                <asp:TextBox ID="txtTurmaCadas" style="padding-left: 2px; height: 13px;" Enabled="false" Width="50px" runat="server"></asp:TextBox>
                            </li>--%>
                                        </li>
                                    </ul>
                                    <ul id="ulAcoes" style="width: 39px;">
                                        <li id="btnPesquisar" style="margin-right: 2px;">
                                            <img title="Realiza uma Pesquisa a partir dos Parametros de Pesquisa Informado."
                                                    alt="Icone de Pesquisa." 
                                                    src="/BarrasFerramentas/Icones/Pesquisar_Desabilitado.png" />
                                        </li>
                                        <li id="liImprimir" style="margin-right: 0px;">
                                            <img title="Formula um relat�rio a partir dos par�metros especificados e o exibe em Modo de Impress�o." 
                                                    alt="Icone de Impressao." 
                                                    src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />                    
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            </li>
                            <li id="lilnkEfetMatric" runat="server" title="Clique para Efetivar Matr�cula" class="lilnkEfetMatric">
                                <asp:LinkButton ID="lnkEfetMatric" OnClick="lnkEfetMatric_Click" runat="server" Style="margin: 0 auto;" ToolTip="EFETIVAR MATR�CULA">
                                    <img id="imgEfetiMatric" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa" title="EFETIVAR MATR�CULA" />
                                    <asp:Label runat="server" ID="lblEfetiMatric" Text="EFETIVAR" style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>    
                            <li id="lilnkRecMatric" runat="server" title="Clique para Imprimir Contrato de Matr�cula" class="lilnkRecMatric">
                                <asp:LinkButton ID="lnkRecMatric" OnClick="lnkRecMatric_Click" Enabled="false" runat="server" Style="margin: 0 auto;" ToolTip="Imprimir Recibo/Protocolo de Matr�cula">
                                    <img id="imgRecMatric" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico' alt="Icone Pesquisa" title="RECIBO MATRIC" />
                                    <asp:Label runat="server" ID="lblRecibo" Text="CONTRATO" style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li id="lilnkFichaMatric" runat="server" title="Clique para Imprimir a ficha de Pr�-Matr�cula" class="lilnkBolCarne">
                                <asp:LinkButton ID="lnkFichaMatric" Enabled="false" runat="server" 
                                    Style="margin: 0 auto;" ToolTip="Imprimir Ficha de Pr�-Matr�cula" 
                                    onclick="lnkFichaMatric_Click"> <img id="img3" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico' alt="Icone Pesquisa" title="FICHA DE MATR�CULA" /><asp:Label runat="server" ID="Label21" Text="FICHA" style="margin-left: 4px;"></asp:Label></asp:LinkButton>
                            </li>
                            <li id="lilnkBolCarne" runat="server" title="Clique para Imprimir Boleto de Mensalidades" class="lilnkBolCarne">                                    
                                <asp:LinkButton ID="lnkBolCarne" OnClick="lnkBolCarne_Click" Enabled="false" runat="server" Style="margin: 0 auto;" ToolTip="Imprimir Boleto de Mensalidades">
                                    <img id="imgBolCarne" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico' alt="Icone Pesquisa" title="BOLETO/CARN�" />
                                    <asp:Label runat="server" ID="lblBoleto" Text="BOLETO" style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li id="lilnkCarteira" runat="server" title="Clique para Imprimir a Carteira da Escola" class="lilnkCarteira">                                    
                                <asp:LinkButton ID="lnkCarteira" OnClick="lnkCarteira_Click" 
                                    runat="server" Style="margin: 0 auto;" Enabled="false"
                                    ToolTip="Imprimir Carteira da Escola">
                                    <img id="img1" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico' alt="Icone Pesquisa" title="Carteira da Escola" />
                                    <asp:Label runat="server" ID="Label7" Text="CARTEIRA" style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li id="li7" runat="server" title="Clique para realizar o controle de cheques" class="lilnkRegPgto">                                    
                                <a id="lnkCheque" runat="server" href="" style="cursor: pointer;">CHEQUE</a>
                            </li>
                            <li id="li5" runat="server" title="Clique para Realizar o Pagamento de Mensalidade(s)" class="lilnkRegPgto">                                    
                                <a id="lnkRealiPagto" runat="server" href="" style="cursor: pointer;">REG PGTO</a>
                            </li>
                        </ul>
                    </div>
                </li>   
            </ul>
        </li>
        <li class="liSeparador"></li>
        <li>

<!-- =========================================================================================================================================================================== -->
<!--                                                               INICIO MENU LATERAL                                                                                           -->
<!-- =========================================================================================================================================================================== -->
            <div id="divMenuLateral">
                <ul id="ulMenuLateral">
                    <!-- INICIO PARTE DAS INFORMA��ES DO RESPONS�VEL -->
                    <li class="libtnInfResp">                                            
                            <img class="imgInfResp" src='/Library/IMG/Gestor_Numero1Preto.png' alt="Icone Pesquisa" title="Ativa Informa��es do Respons�vel." />
                            <asp:Label runat="server" ID="lblbtnInfResp" CssClass="lblTitInf" Text="INFORMA��ES RESPONS�VEL"></asp:Label>                            
                            <asp:Label runat="server" ID="lblSucInfResp" CssClass="lblSucInfResp" Visible="false" style="color:Red;clear:none;font-weight:bold;" Text="OK!"></asp:Label>
                    </li>
                    <li class="liTitInf">
                        <label class="lblSubTitInf">
                            Digite ou atualize os dados do Respons�vel</label>                        
                    </li>
                    <li style="margin-left: 20px;">
                        <span title="N�mero do CPF do Respons�vel">
                            N� CPF</span>
                        <asp:TextBox ID="txtCPFResp" CssClass="txtCPF" style="width: 75px;" runat="server" ToolTip="Informe o CPF do Respons�vel"></asp:TextBox>
                        <asp:HiddenField ID="hdfCPFRespRes" runat="server" />
                    </li>                    
                    <li id="liCPFResp" runat="server">
                        <asp:ImageButton ID="btnCPFResp" runat="server" OnClick="btnCPFResp_Click"
                            ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" CssClass="btnPesqMat"
                            CausesValidation="false" />
                    </li>
                    <li class="liPesqReserva">                        
                        <a class="lnkPesResp" href="#"><img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade" /></a>
                    </li>
                    <li style="clear:none; margin-right: 0px;">
                        <asp:CheckBox CssClass="chkLocais" ID="chkRecResResp" TextAlign="Right" Enabled="false"
                            runat="server" ToolTip="Recuperar dados do respons�vel na reserva" 
                            Text="Recup Rsv" AutoPostBack="True" 
                            oncheckedchanged="chkRecResResp_CheckedChanged"/>                                                     
                    </li> 
                    <li style="clear:both; margin-left: 20px;">
                        <asp:TextBox ID="txtNoRespCPF" CssClass="txtNoRespCPF" runat="server" Enabled="False"></asp:TextBox>
                    </li>
                    <!-- FIM PARTE DAS INFORMA��ES DO RESPONS�VEL -->

                    <!-- INICIO PARTE DAS INFORMA��ES DO ALUNO -->
                    <li style="margin-top: 2px;">                        
                            <img class="imgInfResp" src='/Library/IMG/Gestor_Numero2Vermelho.png' alt="Icone Pesquisa" title="Ativa Informa��es do Aluno." />
                            <asp:Label runat="server" ID="Label2" CssClass="lblTitInf" Text="INFORMA��ES DO ALUNO"></asp:Label>  
                            <asp:Label runat="server" ID="lblSucInfAlu" CssClass="lblSucInfAlu" Visible="false" style="color:Red;clear:none;font-weight:bold;" Text="OK!"></asp:Label>                       
                    </li>                                        
                    <li style="clear:both;margin-left: 20px; ">
                        <span title="N� NIRE do Aluno">
                            N� NIRE</span>
                        <asp:TextBox ID="txtNumNIRE" CssClass="txtNire" runat="server" ToolTip="Informe o N� NIRE do Aluno"></asp:TextBox>
                    </li>
                    <li runat="server">
                        <asp:ImageButton ID="btnPesqNIRE" runat="server" OnClick="btnPesqNIRE_Click" Enabled="false"
                            ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" CssClass="btnPesqMat"
                            CausesValidation="false"/>
                    </li>
                    <li class="liPesqReserva">                        
                        <a class="lnkPesNIRE" href="#"><img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" title="Pesquisa habilitada s� para matr�cula sem reserva." alt="Icone" /></a>
                    </li>
                    <li style="margin-left: 20px;">
                        <asp:TextBox ID="txtNoInfAluno" CssClass="txtNoInfAluno" style="color: Black !important;" runat="server" Enabled="False"></asp:TextBox>
                    </li>
                    <!-- FIM PARTE DAS INFORMA��ES DO ALUNO -->

                    <!-- INICIO PARTE DAS INFORMA��ES DA MATR�CULAS -->
                    <li style="margin-top: 2px;">                        
                        <img class="imgInfResp" src='/Library/IMG/Gestor_Numero3Vermelho.png' alt="Icone Pesquisa" title="Ativa Dados de Matr�cula." />
                        <asp:Label runat="server" ID="Label5" CssClass="lblTitInf" Text="DADOS DE MATR�CULA"></asp:Label>                                                 
                    </li>
                    <li style="margin-top: 3px;margin-left: 20px;">
                        <span style="margin-right: 3px;" title="Ano">Ano</span>
                        <asp:TextBox ID="txtAno" ValidationGroup="ModSerTur" CssClass="txtAno" 
                            MaxLength="4" Enabled="false" Width="26px" runat="server" AutoPostBack="True" 
                            ontextchanged="txtAno_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAno" ValidationGroup="ModSerTur"
                                    ErrorMessage="Ano deve ser informado" Display="None"></asp:RequiredFieldValidator>
                        <asp:Label runat="server" ID="lblSucDadosMatr" CssClass="lblSucDadosMatr" Visible="false" style="color:Red;clear:none;font-weight:bold;" Text="OK!"></asp:Label>
                    </li>
                    <li>
                        <ul>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                            <li id="liModalidade" class="liLeft" style="margin-left:20px;margin-top: -3px; clear: both;">
                                <label for="ddlModalidade" title="Modalidade" class="lblObrigatorio">
                                    Modalidade de Ensino</label>
                                <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade de Ensino" Enabled="false" CssClass="campoModalidade" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlModalidade" ValidationGroup="ModSerTur"
                                    ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
                            </li>
                            <li id="liSerie" style="margin-top: -3px;">
                                <label for="ddlSerieCurso" class="lblObrigatorio" title="S�rie/Curso">
                                    S�rie</label>
                                <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" Enabled="false" ToolTip="Selecione a S�rie/Curso" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="ddlSerieCurso" ValidationGroup="ModSerTur"
                                    ErrorMessage="S�rie/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
                            </li>
                            <%-- <li id="li7" class="liLeft" style="margin-top: -2px; clear: both; margin-left:20px;">
                                <label for="txtAno" title="Turma de Cadastro">
                                    Turma Cad</label>
                                <asp:TextBox ID="txtTurmaCadas" style="padding-left: 2px; height: 13px;" Enabled="false" Width="50px" runat="server"></asp:TextBox>
                            </li>--%>
                            <li id="liTurma" style="margin-top: -2px; margin-left:20px; clear: both;">
                                <label for="ddlTurma" class="lblObrigatorio" title="Turma Matr�cula">
                                    Turma Matr�cula</label>
                                <asp:DropDownList ID="ddlTurma" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged" Enabled="false" AutoPostBack="true" ToolTip="Selecione a Turma de Matr�cula" Width="90px" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="ddlTurma" ValidationGroup="ModSerTur"
                                    ErrorMessage="Turma deve ser informada"  Display="None"></asp:RequiredFieldValidator>
                            </li>    
                            <li style="margin-top: -2px;margin-left: 4px;margin-right: 4px">
                                <label for="txtTurno" title="Turno">
                                    Turno</label>
                                <asp:TextBox ID="txtTurno" Enabled="false" CssClass="txtTurno" Width="45px" style="padding-left: 4px;" runat="server"></asp:TextBox>
                            </li>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                            <li id="li4" runat="server" title="Clique para Confirmar Modalidade, S�rie e Turma" class="liBtnConfir" style="margin-left: 4px; margin-top: 8px">                                    
                                <asp:LinkButton ID="lnkConfirModSerTur" OnClick="lnkConfirModSerTur_Click" Enabled="false" ValidationGroup="ModSerTur"
                                    runat="server" ToolTip="Clique para Confirmar Modalidade, S�rie e Turma">
                                    <asp:Label runat="server" ID="Label20" Text="FINALIZAR"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </li>                      
                    <!-- FIM PARTE DAS INFORMA��ES DA MATR�CULA -->

                    <!-- INICIO PARTE DAS INFORMA��ES ADICIONAIS DO ALUNO -->
                    <li style="margin-top: 2px;">
                    <div runat="server" id="divInformacoesAdd">
                    <ul>
                    <li style="margin-top: 2px;">                                                
                        <img runat="server" id="imgNumeroInformacoesAdd" class="imgInfResp" src='/Library/IMG/Gestor_Numero4Preto.png' alt="Icone Pesquisa" title="Ativa Informa��es Adicionais do Aluno." />
                        <asp:Label runat="server" ID="Label3" CssClass="lblTitInf" Text="INFORMA��ES ADICIONAIS"></asp:Label>
                    </li>
                    <li class="liTitInf" style="margin-bottom: -4px;">
                        <label class="lblSubTitInf">
                            Marque uma das op��es abaixo e informe:</label>                        
                    </li>
                    <li id="liInfAdd">
                        <ul class="ulServs" style="margin-left: 19px;">                              
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkEndAddAlu" Enabled="false" ClientIDMode="Static" ID="chkEndAddAlu" runat="server" Text="Endere�os Adicionais"/>                               
                                <asp:Label runat="server" ID="lblSucEndAddAlu" ClientIDMode="Static" CssClass="lblSucInfAlu" Visible="false" style="color:Red;clear:none;font-weight:bold;" Text="OK!"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkTelAddAlu" Enabled="false" ClientIDMode="Static" ID="chkTelAddAlu" runat="server" Text="Telefones Adicionais"/>                               
                                <asp:Label runat="server" ID="lblSucTelAddAlu" ClientIDMode="Static" CssClass="lblSucInfAlu" Visible="false" style="color:Red;clear:none;font-weight:bold;" Text="OK!"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkCuiEspAlu" Enabled="false" ClientIDMode="Static" ID="chkCuiEspAlu" runat="server" Text="Cuidados Especiais"/>                          
                                <asp:Label runat="server" ID="lblSucCuiEspAlu" ClientIDMode="Static" CssClass="lblSucInfAlu" Visible="false" style="color:Red;clear:none;font-weight:bold;" Text="OK!"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkResAliAlu" Enabled="false" ClientIDMode="Static" ID="chkResAliAlu" runat="server" Text="Restri��es Alimentares"/>                                
                                <asp:Label runat="server" ID="lblSucResAliAlu" ClientIDMode="Static" CssClass="lblSucInfAlu" Visible="false" style="color:Red;clear:none;font-weight:bold;" Text="OK!"></asp:Label>
                            </li>                         
                            <li class="G2Clear">         
                                <asp:CheckBox CssClass="chkRegAtiExt" Enabled="false" ClientIDMode="Static" ID="chkRegAtiExt" runat="server" Text="Registro de Atividades Extra"/>                                                                                     
                                <asp:Label runat="server" ID="lblSucRegAtiExt" ClientIDMode="Static" CssClass="lblSucInfAlu" Visible="false" style="color:Red;clear:none;font-weight:bold;" Text="OK!"></asp:Label>
                            </li>
                            <li class="lichkMatEsc">
                                <asp:CheckBox CssClass="chkMatEsc" Enabled="false" ClientIDMode="Static" ID="chkMatEsc" runat="server" Text="Registro de Mater. Coletivo e Uniforme"/>   
                                <asp:Label runat="server" ID="lblSucMatEsc" ClientIDMode="Static" CssClass="lblSucInfAlu" Visible="false" style="color:Red;clear:none;font-weight:bold;" Text="OK!"></asp:Label>                             
                            </li>
                            <li class="lichkUniforme">
                                <asp:CheckBox CssClass="chkDocMat" Enabled="false" ClientIDMode="Static" ID="chkDocMat" runat="server" Text="Entrega de Documentos de Matr�cula"/>                                
                                <asp:Label runat="server" ID="lblSucDocAlu" ClientIDMode="Static" CssClass="lblSucInfAlu" Visible="false" style="color:Red;clear:none;font-weight:bold;" Text="OK!"></asp:Label>
                            </li>
                        </ul>    
                    </li>
                    </ul>
                    </div>  
                    </li>   
                    <!-- FIM PARTE DAS INFORMA��ES ADICIONAIS DO ALUNO -->

                    <!-- INICIO PARTE DAS INFORMA��ES DAS MENSALIDADES -->
                    <li style="margin-top: 2px;">                                                
                        <img runat="server" id="imgNumeroMensalidades" class="imgInfResp" src='/Library/IMG/Gestor_Numero5Preto.png' alt="Icone Pesquisa" title="Ativa Mensalidades Escolares do Aluno." />
                        <asp:Label runat="server" ID="Label4" CssClass="lblTitInf" Text="MENSALIDADES ESCOLARES"></asp:Label>                       
                    </li>
                    <li class="liTitInf">
                        <label class="lblSubTitInf">
                            Confirme as informa��es dos itens abaixo:</label>                        
                    </li>
                    <li class="lichkUniforme" style="margin-left: 16px;">
                        <asp:CheckBox CssClass="chkMenEscAlu" Enabled="false" ClientIDMode="Static" ID="chkMenEscAlu" runat="server" Text="Gerar Mensalidades Escolares"/>   
                        <asp:Label runat="server" ID="lnkSucMenEscAlu" ClientIDMode="Static" CssClass="lblSucInfAlu" Visible="false" style="color:Red;clear:none;font-weight:bold;" Text="OK!"></asp:Label>                                                                      
                    </li>    
                    <!-- FIM PARTE DAS INFORMA��ES DAS MENSALIDADES -->
                </ul>
            </div>    
<!-- =========================================================================================================================================================================== -->
<!--                                                              FIM MENU LATERAL                                                                                              -->
<!-- =========================================================================================================================================================================== -->

        <div id="tabAluno" clientidmode="Static" style="visibility: hidden;" runat="server">        
            <ul id="ul2" class="ulDados2">
                <li style="width: 100%;text-align: center;text-transform:uppercase;margin-top:-7px;background-color: #E8E8E8;margin-bottom: 5px;">
                    <label style="font-size: 1.1em;font-family:Tahoma;">INFORMA��ES CADASTRAIS DO ALUNO - CADASTRO B�SICO</label>
                </li>
                <li class="liPhotoA">
                    <uc1:ControleImagem ID="ControleImagemAluno" runat="server" />
                </li>       
                <li style="width: 670px;margin-left: 5px;margin-right:0px;">
                <ul>             
                    <li class="liDist" style="margin-left: 20px;">
                        <label for="txtNireAluno" title="N�mero do NIRE">
                            N� NIRE</label>     
                        <asp:TextBox ID="txtNireAluno" CssClass="txtNireAluno" runat="server" ToolTip="Informe o N�mero do NIRE">
                        </asp:TextBox>                 
                    </li>
                    <li class="liddlSexoAlu">
                        <label for="txtNisAluno" title="N�mero do NIS">
                            N� NIS</label>
                        <asp:TextBox ID="txtNisAluno" CssClass="txtNisAluno" runat="server" ToolTip="Informe o N�mero do NIS"
                            MaxLength="11"></asp:TextBox>
                    </li>
                    <li class="liddlSexoAlu">
                        <label for="txtNomeAluno" class="lblObrigatorio" title="Nome do Aluno">
                            Nome</label>
                        <asp:TextBox ID="txtNomeAluno" style="text-transform:uppercase;" runat="server" CssClass="campoNomePessoa" ToolTip="Informe o Nome do Aluno"
                            MaxLength="80"></asp:TextBox>
                        <asp:HiddenField ID="hdCodAluno" runat="server" />
                        <asp:HiddenField ID="hdCodReserva" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ValidationGroup="ALUNO"
                            runat="server" ControlToValidate="txtNomeAluno" ErrorMessage="Nome deve ser informado"
                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li class="liddlSexoAlu">
                        <label for="txtApelAluno" title="Apelido do Aluno">
                            Apelido</label>
                        <asp:TextBox ID="txtApelAluno" style="text-transform:uppercase;" runat="server" Width="78px" ToolTip="Informe o Apelido do Aluno"
                            MaxLength="15"></asp:TextBox>
                    </li>
                    <li class="liddlSexoAlu">
                        <label for="ddlSexoAluno" class="lblObrigatorio" title="Sexo do Aluno">
                            Sexo</label>
                        <asp:DropDownList ID="ddlSexoAluno" CssClass="ddlSexoAluno" runat="server" ToolTip="Informe o Sexo do Aluno">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="M">Masculino</asp:ListItem>
                            <asp:ListItem Value="F">Feminino</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ValidationGroup="ALUNO"
                            runat="server" ControlToValidate="ddlSexoAluno" ErrorMessage="Sexo deve ser informado"
                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>                                          
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                    <li style="clear:both;margin-left: 20px;">
                        <label for="txtDataNascimentoAluno" class="lblObrigatorio" title="Data de Nascimento">
                            Data Nascimento</label>
                        <asp:TextBox ID="txtDataNascimentoAluno" CssClass="campoData" runat="server" ToolTip="Informe a Data de Nascimento"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ValidationGroup="ALUNO"
                            ControlToValidate="txtDataNascimentoAluno" ErrorMessage="Data de Nascimento deve ser informada"
                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li class="liEspacamento">
                        <label for="ddlEstadoCivilAluno" title="Estado Civil do Aluno">
                            Estado Civil</label>
                        <asp:DropDownList ID="ddlEstadoCivilAluno" Width="120px" runat="server"
                            ToolTip="Informe o Estado Civil do Aluno">
                            <asp:ListItem Value="">N�o Informado</asp:ListItem>
                            <asp:ListItem Value="O">Solteiro</asp:ListItem>
                            <asp:ListItem Value="C">Casado</asp:ListItem>
                            <asp:ListItem Value="S">Separado Judicialmente</asp:ListItem>
                            <asp:ListItem Value="D">Divorciado</asp:ListItem>
                            <asp:ListItem Value="V">Vi�vo</asp:ListItem>
                            <asp:ListItem Value="P">Companheiro</asp:ListItem>
                            <asp:ListItem Value="U">Uni�o Est�vel</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li class="liEspacamento">
                        <label for="ddlNacionalidadeAluno" class="lblObrigatorio" title="Nacionalidade do Aluno">
                            Nacionalidade</label>
                        <asp:DropDownList ID="ddlNacionalidadeAluno" CssClass="ddlNacionalidadeAluno" runat="server"
                            ToolTip="Informe a Nacionalidade do Aluno">
                            <asp:ListItem Value="B">Brasileiro</asp:ListItem>
                            <asp:ListItem Value="E">Estrangeiro</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlNacionalidadeAluno"
                            ErrorMessage="Nacionalidade deve ser informada" ValidationGroup="ALUNO" Text="*"
                            Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>    
                    <li style="margin-left: 10px;">
                        <label for="txtNaturalidadeAluno" title="Cidade de Naturalidade do Aluno">
                            Naturalidade</label>
                        <asp:TextBox ID="txtNaturalidadeAluno" CssClass="txtNaturalidadeAluno" runat="server" ToolTip="Informe a Cidade de Naturalidade do Aluno"
                            MaxLength="40"></asp:TextBox>
                    </li>
                    <li style="margin-left: 10px;">
                        <label for="ddlUfNacionalidadeAluno" title="UF de Nacionalidade do Aluno">
                            UF</label>
                        <asp:DropDownList ID="ddlUfNacionalidadeAluno" CssClass="campoUf" runat="server" ToolTip="Informe a UF de Nacionalidade">
                        </asp:DropDownList>
                    </li>
                    <li class="liEspacamento">
                        <label for="ddlEtniaAluno" title="Etnia do Aluno">
                            Etnia</label>
                        <asp:DropDownList ID="ddlEtniaAluno" CssClass="ddlEtniaAluno" runat="server" ToolTip="Informe a Etnia do Aluno">
                            <asp:ListItem Value="B">Branca</asp:ListItem>
                            <asp:ListItem Value="N">Negra</asp:ListItem>
                            <asp:ListItem Value="A">Amarela</asp:ListItem>
                            <asp:ListItem Value="P">Parda</asp:ListItem>
                            <asp:ListItem Value="I">Ind�gena</asp:ListItem>
                            <asp:ListItem Value="X" Selected="true">N�o Informada</asp:ListItem>
                        </asp:DropDownList>
                    </li>              
                    <li style="clear: both; margin-left: -5px; margin-top: 10px;">
                        <asp:LinkButton ID="LinkButton1" runat="server">
                            <img id="img2" runat="server" class="imgWebCam" src='/Library/IMG/EW_IconeWebCam.jpg' alt="Icone WebCam" />
                        </asp:LinkButton>
                    </li>                             
                    </ContentTemplate>
                    </asp:UpdatePanel>   
                    <li style="margin-left: 5px;">
                        <label title="Tipo de Sangue">
                            Tp.Sangu�neo</label>
                        <asp:DropDownList ID="ddlTpSangueAluno" ToolTip="Selecione o Tipo de Sangue" CssClass="ddlTpSangueAluno"
                            runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="A">A</asp:ListItem>
                            <asp:ListItem Value="B">B</asp:ListItem>
                            <asp:ListItem Value="AB">AB</asp:ListItem>
                            <asp:ListItem Value="O">O</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlStaSangueAluno" ToolTip="Selecione o Status do Tipo de Sangue" CssClass="ddlStaTpSangueAluno"
                            runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="P">+</asp:ListItem>
                            <asp:ListItem Value="N">-</asp:ListItem>
                        </asp:DropDownList>
                    </li>                                                                                               
                    <li style="margin-left: 10px;">
                        <label for="ddlDeficienciaAluno" title="Defici�ncia">
                            Defici�ncia</label>
                        <asp:DropDownList ID="ddlDeficienciaAluno" CssClass="ddlDeficienciaAluno" runat="server"
                            ToolTip="Selecione a Defici�ncia do Aluno">
                            <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                            <asp:ListItem Value="A">Auditiva</asp:ListItem>
                            <asp:ListItem Value="V">Visual</asp:ListItem>
                            <asp:ListItem Value="F">F�sica</asp:ListItem>
                            <asp:ListItem Value="M">Mental</asp:ListItem>
                            <asp:ListItem Value="P">M�ltiplas</asp:ListItem>
                            <asp:ListItem Value="O">Outras</asp:ListItem>
                        </asp:DropDownList>
                    </li>    
                    <li style="margin-left: 10px;">
                        <label for="ddlOrigem" class="" title="Origem">Origem</label>
                        <asp:DropDownList ID="ddlOrigem" style="width:100px;" runat="server" ToolTip="Origem">
                        <asp:ListItem Value="SR" Text="Sem Registro"></asp:ListItem>
                        <asp:ListItem Value="AI" Text="�rea Ind�gena"></asp:ListItem>
                        <asp:ListItem Value="AQ" Text="�rea Quilombo"></asp:ListItem>
                        <asp:ListItem Value="AR" Text="�rea Rural"></asp:ListItem>
                        <asp:ListItem Value="IE" Text="Interior do Estado"></asp:ListItem>
                        <asp:ListItem Value="MU" Text="Munic�pio"></asp:ListItem>
                        <asp:ListItem Value="OE" Text="Outro Estado"></asp:ListItem>
                        <asp:ListItem Value="OO" Text="Outra Origem"></asp:ListItem>            
                        </asp:DropDownList>
                    </li>       
                    <li style="margin-left: 10px;">
                        <label for="txtResponsavelAluno" title="Nome do Respons�vel pelo Aluno">
                            Nome do Respons�vel</label>
                        <asp:TextBox ID="txtResponsavelAluno" CssClass="txtResponsavelAluno" ToolTip="Informe o Nome do Respons�vel pelo Aluno"
                            Enabled="false" runat="server"></asp:TextBox>
                    </li>
                    <li style="margin-left: 10px;">
                        <label for="ddlGrauParentescoAluno" class="lblObrigatorio" title="Grau de Parentesco do Respons�vel">
                            Grau Parent</label>
                        <asp:DropDownList ID="ddlGrauParentescoAluno" runat="server" CssClass="ddlGrauParentescoAluno"
                            ToolTip="Informe o Grau de Parentesco do Respons�vel">
                            <asp:ListItem Value="PM">Pai/M�e</asp:ListItem>
                            <asp:ListItem Value="TI">Tio(a)</asp:ListItem>
                            <asp:ListItem Value="AV">Av�/Av�</asp:ListItem>
                            <asp:ListItem Value="PR">Primo(a)</asp:ListItem>
                            <asp:ListItem Value="CN">Cunhado(a)</asp:ListItem>
                            <asp:ListItem Value="TU">Tutor(a)</asp:ListItem>
                            <asp:ListItem Value="IR">Irm�o(�)</asp:ListItem>
                            <asp:ListItem Value="OU">Outros</asp:ListItem>
                        </asp:DropDownList>
                    </li>                                                                                                                                                         
                </ul>
                </li>  
                <li class="liClear">
                    <ul>
                        <li style="clear: both;margin-left:5px;margin-top: 3px;">
                            <label style="text-transform:uppercase;font-weight: bold;font-size: 0.9em !important;">Informa��es de Documentos</label>
                        </li>                    
                        <li class="liClear">
                            <fieldset class="fldFiliaResp">
                                <legend style="font-weight:normal;color:#FF7F24;">Certid�o</legend>
                                <ul>
                                    <li>
                                        <label class="lblObrigatorio" for="ddlTipoCertidaoAluno" title="Tipo de Certid�o">
                                            Tipo</label>
                                        <asp:DropDownList ID="ddlTipoCertidaoAluno" CssClass="ddlTipoCertidaoAluno" ToolTip="Selecione o Tipo de Certid�o"
                                            runat="server">
                                            <asp:ListItem Value="N">Nascimento</asp:ListItem>
                                            <asp:ListItem Value="C">Casamento</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="ALUNO"
                                            runat="server" ControlToValidate="ddlTipoCertidaoAluno" ErrorMessage="Tipo de certid�o deve ser informado"
                                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio" for="txtNumeroCertidaoAluno" title="N�mero da Certid�o">
                                            N�mero</label>
                                        <asp:TextBox ID="txtNumeroCertidaoAluno" style="width: 109px" CssClass="txtNumeroCertidaoAluno" ToolTip="Informe o N�mero da Certid�o"
                                            runat="server" MaxLength="32"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtNumeroCertidaoAluno"
                                            ErrorMessage="N�mero da Certid�o deve ser informado" ValidationGroup="ALUNO"
                                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                    </li>
                                    <li id="liLivroA">
                                        <label class="lblObrigatorio" for="txtLivroAluno" title="Livro da Certid�o">
                                            Livro</label>
                                        <asp:TextBox ID="txtLivroAluno" CssClass="txtLivroAluno" ToolTip="Informe o Livro da Certid�o"
                                            runat="server" MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtLivroAluno"
                                            ErrorMessage="Livro da certid�o deve ser informado" ValidationGroup="ALUNO" Text="*"
                                            Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio" for="txtFolhaAluno" title="Folha da Certid�o">
                                            Folha</label>
                                        <asp:TextBox ID="txtFolhaAluno" CssClass="txtFolhaAluno" ToolTip="Informe a Folha da Certid�o"
                                            runat="server" MaxLength="8"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtFolhaAluno"
                                            ErrorMessage="Folha da certid�o deve ser informada" ValidationGroup="ALUNO" Text="*"
                                            Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                    </li>
                                    <li class="liClear">
                                        <label class="lblObrigatorio" for="txtCartorioAluno" title="Cart�rio da Certid�o">
                                            Nome do Cart�rio</label>
                                        <asp:TextBox ID="txtCartorioAluno" CssClass="txtCartorioAluno" ToolTip="Informe o Cart�rio da Certid�o"
                                            runat="server" MaxLength="80"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtCartorioAluno"
                                            ErrorMessage="Cart�rio deve ser informado" ValidationGroup="ALUNO" Text="*" Display="None"
                                            CssClass="validatorField"></asp:RequiredFieldValidator>
                                    </li>
                                    <li class="liClear">  
                                        <label class="lblObrigatorio" for="txtCidadeCertidaoAlu" title="Cidade">Cidade</label>
                                        <asp:TextBox ID="txtCidadeCertidaoAlu" CssClass="txtCidadeCertidaoAlu" ToolTip="Informe a Cidade da Certid�o" runat="server" MaxLength="40"></asp:TextBox> 
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="ALUNO" ControlToValidate="txtCidadeCertidaoAlu" ErrorMessage="Cidade da Certid�o deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>                                                                      
                                    </li>
                                    <li>      
                                        <label class="lblObrigatorio" for="ddlUFCertidaoAlu" title="UF da Certid�o">UF</label>
                                        <asp:DropDownList ID="ddlUFCertidaoAlu" CssClass="campoUf" ToolTip="Informe a UF da Certid�o" runat="server"></asp:DropDownList>  
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="ALUNO" ControlToValidate="ddlUFCertidaoAlu" ErrorMessage="UF da Certid�o deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                    </li>                                    
                                </ul>
                            </fieldset>
                        </li>      
                        <li class="liClear" style="margin-top: -8px;">
                            <fieldset class="fldFiliaResp">
                                <legend style="font-weight:normal;color:#FF7F24;">Carteira de Identidade</legend>
                                <ul>
                                    <li>
                                        <label for="txtRgAluno" title="N�mero da Identidade">
                                            N�mero</label>
                                        <asp:TextBox ID="txtRgAluno" CssClass="txtRgAluno" ToolTip="Informe o N�mero da Identidade"
                                            runat="server" MaxLength="20"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label for="txtDataEmissaoRgAluno" title="Data de Emiss�o da Identidade">
                                            Emiss�o</label>
                                        <asp:TextBox ID="txtDataEmissaoRgAluno" CssClass="campoData" ToolTip="Informe a Data de Emiss�o da Identidade"
                                            runat="server"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label for="txtOrgaoEmissorAluno" title="�rg�o Emissor da Identidade">
                                            �rg�o</label>
                                        <asp:TextBox ID="txtOrgaoEmissorAluno" CssClass="txtOrgaoEmissorAluno" ToolTip="Informe o �rg�o Emissor da Identidade"
                                            runat="server" MaxLength="12"></asp:TextBox>
                                    </li>                                
                                    <li>
                                        <label for="ddlUfRgAluno" title="UF do �rg�o Emissor">
                                            UF</label>
                                        <asp:DropDownList ID="ddlUfRgAluno" CssClass="campoUf" ToolTip="Informe a UF do �rg�o Emissor"
                                            runat="server">
                                        </asp:DropDownList>
                                    </li>                                
                                </ul>
                            </fieldset>
                        </li>    
                        <li class="liClear" style="margin-top: -8px;">
                            <fieldset class="fldFiliaResp">
                                <legend style="font-weight:normal;color:#FF7F24;">Passaporte</legend>
                                <ul>
                                    <li class="liClear">
                                        <label for="txtCartaoSaudeAluno" title="N�mero do Passaporte">
                                            N�mero</label>
                                        <asp:TextBox ID="txtPassaAluno" ToolTip="Informe o N�mero do Passaporte do Aluno" runat="server"
                                            CssClass="txtCartaoSaudeAluno"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label for="ddlNacioPassaAluno" title="Nacionalidade do Passaporte do Aluno">
                                            Nacionalidade</label>
                                        <asp:DropDownList ID="ddlNacioPassaAluno" CssClass="ddlNacionalidadeAluno" runat="server"
                                            ToolTip="Informe a Nacionalidade do Passaporte do Aluno">
                                            <asp:ListItem Value="B">Brasileiro</asp:ListItem>
                                            <asp:ListItem Value="E">Estrangeiro</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                             </fieldset>
                        </li>          
                        <li class="liClear" style="margin-top: -8px;">
                            <fieldset class="fldFiliaResp">
                                <legend style="font-weight:normal;color:#FF7F24;">T�tulo de Eleitor</legend>
                                <ul>
                                    <li class="liClear">
                                        <label for="txtNumeroTituloAluno" title="N�mero do T�tulo de Eleitor">
                                            N�mero</label>
                                        <asp:TextBox ID="txtNumeroTituloAluno" CssClass="txtNumeroTituloAluno" ToolTip="Informe o N�mero do T�tulo de Eleitor"
                                            runat="server" MaxLength="15"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label for="txtZonaAluno" title="Zona do T�tulo de Eleitor">
                                            Zona</label>
                                        <asp:TextBox ID="txtZonaAluno" CssClass="txtZonaAluno" ToolTip="Informe a Zona do T�tulo de Eleitor"
                                            runat="server" MaxLength="10"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label for="txtSecaoAluno" title="Se��o do T�tulo de Eleitor">
                                            Se��o</label>
                                        <asp:TextBox ID="txtSecaoAluno" CssClass="txtSecaoAluno" ToolTip="Informe a Se��o do T�tulo de Eleitor"
                                            runat="server" MaxLength="10"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label for="ddlUfTituloAluno" title="UF do T�tulo de Eleitor">
                                            UF</label>
                                        <asp:DropDownList ID="ddlUfTituloAluno" CssClass="campoUf" ToolTip="Informe a UF do T�tulo de Eleitor"
                                            runat="server">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </fieldset>
                        </li>
                        <li class="liClear" style="margin-top: -8px;">
                            <fieldset class="fldFiliaResp">
                                <legend style="font-weight:normal;color:#FF7F24;">Outros Doctos</legend>
                                <ul>
                                    <li>
                                        <label for="txtCpfAluno" title="CPF do Aluno">
                                            CPF</label>
                                        <asp:TextBox ID="txtCpfAluno" ToolTip="Informe o CPF do Aluno" runat="server" CssClass="campoCpf"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label for="txtCartaoSaudeAluno" title="N�mero da Carteira de Sa�de">
                                            N� Cart Sa�de</label>
                                        <asp:TextBox ID="txtCartaoSaudeAluno" ToolTip="Informe o N�mero da Carteira de Sa�de" runat="server"
                                            CssClass="txtCartaoSaudeAluno"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label for="txtCartaoVacinAluno" title="N�mero da Carteira de Vacina">
                                            N� Cart Vacina</label>
                                        <asp:TextBox ID="txtCartaoVacinAluno" ToolTip="Informe o N�mero da Carteira de Vacina" runat="server"
                                            CssClass="txtCartaoSaudeAluno"></asp:TextBox>
                                    </li>                                    
                                </ul>
                            </fieldset>
                        </li>                        
                    </ul>
                </li>       
                <li style="clear: none; width: 460px; float: left;">
                    <ul>
                        <li>
                            <ul>
                                <li style="clear: both;margin-left:5px;margin-top: 3px;">
                                    <label style="text-transform:uppercase;font-weight: bold;font-size: 0.9em !important;">Informa��o de Contato</label>
                                </li>
                                <li class="liClear" style="margin-left: 5px;">
                                    <label for="txtEmailAluno" title="E-mail">
                                        E-mail</label>
                                    <asp:TextBox ID="txtEmailAluno" CssClass="txtEmailAluno" ToolTip="Informe o E-mail do Aluno"
                                        runat="server" MaxLength="50"></asp:TextBox>
                                </li>
                                <li>
                                    <label for="txtTelResidencialAluno" title="Telefone Residencial">
                                        Telefone Fixo</label>
                                    <asp:TextBox ID="txtTelResidencialAluno" CssClass="campoTelefone" ToolTip="Informe o Telefone Residencial do Aluno"
                                        runat="server"></asp:TextBox>
                                </li>
                                <li>
                                    <label for="txtTelCelularAluno" title="Telefone Celular">
                                        Telefone Celular</label>
                                    <asp:TextBox ID="txtTelCelularAluno" CssClass="campoTelefone" ToolTip="Informe o Telefone Celular do Aluno"
                                        runat="server"></asp:TextBox>
                                </li>
                            </ul>
                        </li>   
                        <li>
                            <ul>
                                <li style="clear: both;margin-left:5px;margin-top: 3px;">
                                    <label style="text-transform:uppercase;font-weight: bold;font-size: 0.9em !important; margin-bottom: -7px;">Filia��o</label>
                                </li>
                                <li class="liFiliacaoAlu">
                                    <fieldset class="fldFiliaResp">                                        
                                        <ul>
                                            <li class="liClear">
                                                <label for="txtNomeMaeAluno" class="lblObrigatorio" title="Nome da M�e">
                                                    Nome da M�e</label>
                                                <asp:TextBox ID="txtNomeMaeAluno" CssClass="txtNomeMaeAluno" ToolTip="Informe o Nome da M�e"
                                                    runat="server" MaxLength="60"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtNomeMaeAluno"
                                                    ErrorMessage="Nome da m�e deve ser informado" Text="*" ValidationGroup="ALUNO"
                                                    Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                            </li>
                                            <li style="clear:both;">
                                                <label for="txtNomePaiAluno" title="Nome do Pai">
                                                    Nome do Pai</label>
                                                <asp:TextBox ID="txtNomePaiAluno" CssClass="txtNomeMaeAluno" ToolTip="Informe o Nome do Pai"
                                                    runat="server" MaxLength="60"></asp:TextBox>
                                            </li>
                                            <li class="liClear">
                                                <label for="ddlEstadCivilPais" title="Estado Civil do Aluno">
                                                    Estado Civil</label>
                                                <asp:DropDownList ID="ddlEstadCivilPais" Width="120px" runat="server"
                                                    ToolTip="Informe o Estado Civil dos Pais">
                                                    <asp:ListItem Value="">N�o Informado</asp:ListItem>
                                                    <asp:ListItem Value="O">Solteiro</asp:ListItem>
                                                    <asp:ListItem Value="C">Casado</asp:ListItem>
                                                    <asp:ListItem Value="S">Separado Judicialmente</asp:ListItem>
                                                    <asp:ListItem Value="D">Divorciado</asp:ListItem>
                                                    <asp:ListItem Value="V">Vi�vo</asp:ListItem>
                                                    <asp:ListItem Value="P">Companheiro</asp:ListItem>
                                                    <asp:ListItem Value="U">Uni�o Est�vel</asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                            <li style="clear:none; margin-top: 13px; margin-left: 10px;">
                                            <asp:CheckBox CssClass="chkLocais" ID="chkPaisMorJunt" TextAlign="Right"
                                                runat="server" ToolTip="Os pais moram juntos?" 
                                                Text="Moram juntos" />  </li>                          
                                        </ul>
                                    </fieldset>
                                </li>    
                            </ul>
                        </li> 
                        <li style="clear: none; float: left; margin-right: 0px; margin-left: 10px;">
                            <ul>
                                <li style="clear: both;margin-left:5px;margin-top: 3px;">
                                    <label style="text-transform:uppercase;font-weight: bold;font-size: 0.9em !important;">Bolsa / Conv�nio</label>
                                </li>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                <li class="liClear" style="margin-right: 0px;">
                                    <fieldset class="fldFiliaResp">
                                        <ul>
                                            <li class="liClear">
                                                <label for="ddlTipoBolsa" title="Tipo">Tipo</label>
                                                <asp:DropDownList ID="ddlTipoBolsa" Width="65px"
                                                    ToolTip="Selecione o Nome da Bolsa" AutoPostBack="True"  runat="server" 
                                                    onselectedindexchanged="ddlTipoBolsa_SelectedIndexChanged">
                                                <asp:ListItem Value="N" Selected="True">Nenhuma</asp:ListItem>
                                                 <asp:ListItem Value="B">Bolsa</asp:ListItem>
                                                 <asp:ListItem Value="C">Conv�nio</asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                            <li class="liClear">
                                                <label for="ddlBolsaAluno" title="Nome da Bolsa">
                                                    Nome da Bolsa</label>
                                                <asp:DropDownList ID="ddlBolsaAluno" CssClass="ddlBolsaAluno" ToolTip="Selecione o nome da Bolsa"
                                                    runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBolsaAluno_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </li>
                                            <li style="margin-left: 5px; margin-right: 0px;">
                                                <label for="txtDescontoAluno" title="Valor de Desconto">
                                                    Valor</label>
                                                <asp:TextBox ID="txtDescontoAluno" CssClass="txtDescontoAluno" ToolTip="Informe o Valor de Desconto"
                                                    runat="server" Enabled="False"></asp:TextBox>
                                            </li>
                                            <li style="margin-left: 0px; margin-right: 0px; margin-top: 13px;">
                                                <asp:CheckBox CssClass="chkLocais" ID="chkDesctoPercBolsa" TextAlign="Right" 
                                                        Enabled="false" runat="server" ToolTip="% de Desconto da Bolsa?" Text="%" /> 
                                            </li>
                                            <li class="liPeriodoDeA" style="clear:both;">
                                                <label for="txtPeriodoDeIniBolAluno" title="Per�odo de Dura��o da Bolsa">
                                                    Per�odo</label>
                                                <asp:TextBox ID="txtPeriodoDeIniBolAluno" ToolTip="Informe a Data de In�cio da Bolsa" runat="server"
                                                    CssClass="txtPeriodoDeIniBolAluno campoData" Enabled="False"></asp:TextBox>
                                            </li>
                                            <li class="formAuxText1"><span>�</span> </li>
                                            <li class="liPeriodoAteA" style="margin-right: 0px;">
                                                <asp:TextBox ID="txtPeriodoTerBolAluno" ToolTip="Informe a Data de T�rmino da Bolsa" runat="server"
                                                    CssClass="txtPeriodoDeIniBolAluno campoData" Enabled="False"></asp:TextBox>
                                            </li>
                                        </ul>
                                    </fieldset>
                                </li>    
                                </ContentTemplate>
                                </asp:UpdatePanel>    
                            </ul>
                        </li>
                        <li class="liClear" style="margin-right: 0px; width: 325px; margin-top: -10px;">
                            <ul>
                                <li style="clear: both;margin-left:5px;margin-top: 3px;">
                                    <label style="text-transform:uppercase;font-weight: bold;font-size: 0.9em !important; margin-bottom: -5px;">Endere�o Residencial</label>
                                </li>
                                <li class="liEnderecoAlu">
                                    <fieldset class="fldFiliaResp">
                                        <ul>    
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>               
                                            <li>
                                                <label for="txtCepAluno" class="lblObrigatorio" title="CEP">
                                                    CEP</label>
                                                <asp:TextBox ID="txtCepAluno" CssClass="campoCep" ToolTip="Informe o CEP" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtCepAluno"
                                                    ErrorMessage="CEP deve ser informado" ValidationGroup="ALUNO" Text="*" Display="None"
                                                    CssClass="validatorField"></asp:RequiredFieldValidator>
                                            </li>
                                            <li id="li12" class="liPesqCEPResp" runat="server">
                                                <asp:ImageButton ID="btnPesqCEPA" runat="server" onclick="btnPesqCEPA_Click"
                                                    ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" CssClass="btnPesqMat"
                                                    CausesValidation="false"/>
                                            </li>
                                            <li>
                                                <label for="txtLogradouroAluno" class="lblObrigatorio" title="Logradouro da Resid�ncia do Aluno">
                                                    Logradouro</label>
                                                <asp:TextBox ID="txtLogradouroAluno" CssClass="txtLogradouroAluno" ToolTip="Informe o Logradouro da Resid�ncia do Aluno"
                                                    runat="server" MaxLength="40"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtLogradouroAluno"
                                                    ErrorMessage="Logradouro deve ser informado" Text="*" ValidationGroup="ALUNO"
                                                    Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                            </li>
                                            <li class="liListaEndereco" title="Clique para Listar os Endere�os">
                                                <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Listar Endere�o" />
                                            </li>
                                            <div id="divAddTipo">
                                            </div>
                                            <li>
                                                <label for="txtNumeroAluno" title="N�mero da Resid�ncia do Aluno">
                                                    N�mero</label>
                                                <asp:TextBox ID="txtNumeroAluno" CssClass="txtNumeroAluno" ToolTip="Informe o N�mero da Resid�ncia do Aluno"
                                                    runat="server" MaxLength="5"></asp:TextBox>
                                            </li>      
                                            <li class="liClear">
                                                <label for="txtComplementoAluno" title="Complemento">
                                                    Complemento</label>
                                                <asp:TextBox ID="txtComplementoAluno" CssClass="txtComplementoAluno" ToolTip="Informe o Complemento"
                                                    runat="server" MaxLength="100"></asp:TextBox>
                                            </li>                      
                                            <li>
                                                <label for="ddlBairroAluno" class="lblObrigatorio" title="Bairro">
                                                    Bairro</label>
                                                <asp:DropDownList ID="ddlBairroAluno" CssClass="ddlBairroAluno" ToolTip="Informe o Bairro"
                                                    runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ValidationGroup="ALUNO"
                                                    runat="server" ControlToValidate="ddlBairroAluno" ErrorMessage="Bairro deve ser informado"
                                                    Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                            </li>                                                                            
                                            <li class="liClear">
                                                <label for="ddlCidadeAluno" class="lblObrigatorio" title="Cidade">
                                                    Cidade</label>
                                                <asp:DropDownList ID="ddlCidadeAluno" ToolTip="Informe a Cidade" runat="server" Width="147px"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCidadeAluno_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlCidadeAluno"
                                                    ErrorMessage="Cidade deve ser informada" ValidationGroup="ALUNO" Text="*" Display="None"
                                                    CssClass="validatorField"></asp:RequiredFieldValidator>
                                            </li>                                
                                            <li class="liUfA">
                                                <label for="ddlUFAluno" class="lblObrigatorio" title="UF">
                                                    UF</label>
                                                <asp:DropDownList ID="ddlUFAluno" runat="server" CssClass="campoUf" ToolTip="Informe a UF"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlUFAluno_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlUFAluno"
                                                    ErrorMessage="UF deve ser informado" ValidationGroup="ALUNO" Text="*" Display="None"
                                                    CssClass="validatorField"></asp:RequiredFieldValidator>
                                            </li>  
                                            <li style="clear:none;margin-left: 10px; margin-top: 13px; margin-right: 0px;">
                                                <asp:CheckBox CssClass="chkLocais" ID="chkMoraPais" TextAlign="Right"
                                                    runat="server" ToolTip="Aluno mora com os pais?" 
                                                    Text="Mora com os Pais" />                                                     
                                            </li> 
                                            </ContentTemplate>
                                            </asp:UpdatePanel>                           
                                        </ul>
                                    </fieldset>
                                </li>  
                            </ul>
                        </li>
                        <li style="clear:none; float: left; margin-right: 0px; width: 135px; margin-top: -10px;">
                            <ul>
                                <li style="clear: both;margin-left:5px;margin-top: 3px;">
                                    <label style="text-transform:uppercase;font-weight: bold;font-size: 0.9em !important;">Outras Informa��es</label>
                                </li>
                                <li class="liSituacaoAlunoA" style="clear:both; width: 100%;">
                                    <fieldset class="fldFiliaResp">
                                        <ul>
                                            <li>
                                                <label for="ddlRendaFamiliarAluno" class="lblObrigatorio" title="Renda Familiar do Aluno">
                                                    Renda Familiar</label>
                                                <asp:DropDownList ID="ddlRendaFamiliarAluno" CssClass="ddlRendaFamiliarAluno" runat="server"
                                                    ToolTip="Informe a Renda Familiar do Aluno">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                    <asp:ListItem Value="1">1 a 3 SM</asp:ListItem>
                                                    <asp:ListItem Value="2">3 a 5 SM</asp:ListItem>
                                                    <asp:ListItem Value="3">5 a 10 SM</asp:ListItem>
                                                    <asp:ListItem Value="4">+10 SM</asp:ListItem>
                                                    <asp:ListItem Value="5">Sem Renda</asp:ListItem>
                                                    <asp:ListItem Value="6">N�o informada</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlRendaFamiliarAluno"
                                                    ErrorMessage="Renda familiar deve ser informada" ValidationGroup="ALUNO" Text="*"
                                                    Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                            </li>
                                            <li style="clear:none;margin-right:0px;">
                                                <label for="ddlMerendAluno" title="Precisa de Merenda Escolar?">
                                                    Merenda</label>
                                                <asp:DropDownList ID="ddlMerendAluno" CssClass="ddlPasseEscolarAluno" runat="server"
                                                    ToolTip="Informe se Aluno Possui Merenda Escolar">
                                                    <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    <asp:ListItem Value="N" Selected="True">N�o</asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                            <li style="clear:both;">
                                                <label for="ddlTransporteEscolarAluno" title="Participa do Transporte Escolar?">
                                                    Transporte/Rota</label>
                                                <asp:DropDownList ID="ddlTransporteEscolarAluno" CssClass="ddlTransporteEscolarAluno" runat="server"
                                                    ToolTip="Informe se o Aluno participar� do Transporte Escolar">
                                                    <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    <asp:ListItem Value="N" Selected="True">N�o</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlRotaA" CssClass="ddlTransporteEscolarAluno" runat="server"
                                                    ToolTip="Informe se a Rota de Transporte Escolar do Aluno">
                                                    <asp:ListItem Value="208">208</asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                            <li class="liClear">
                                                <label for="ddlPasseEscolarAluno" title="Precisa de Passe Escolar?">
                                                    Passe Esc</label>
                                                <asp:DropDownList ID="ddlPasseEscolarAluno" CssClass="ddlPasseEscolarAluno" runat="server"
                                                    ToolTip="Informe se o Aluno Precisa de Passe Escolar">
                                                    <asp:ListItem Value="True">Sim</asp:ListItem>
                                                    <asp:ListItem Value="False" Selected="True">N�o</asp:ListItem>
                                                </asp:DropDownList>
                                            </li>   
                                            <li>
                                                <label for="ddlPermiSaidaAluno" title="Permiss�o de sa�da do aluno" style="color: Red;">
                                                    Perm Sa�da</label>
                                                <asp:DropDownList ID="ddlPermiSaidaAluno" CssClass="ddlPasseEscolarAluno" runat="server"
                                                    ToolTip="Informe qual ser� a permiss�o de sa�da na Carteira de Estudante - Sim (Liberada) ou N�o (N�o Autorizada)">
                                                    <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    <asp:ListItem Value="N" Selected="True">N�o</asp:ListItem>
                                                </asp:DropDownList>
                                            </li>                                                                                 
                                        </ul>
                                    </fieldset>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>         
                                  
                <li id="li8" runat="server" title="Clique para Cadastrar Aluno" class="liBtnAddA" style="margin-top: -10px; margin-left: 455px !important;">
                    <asp:LinkButton ID="lnkAtualizaAlu" runat="server" ValidationGroup="ALUNO" OnClick="lnkAtualizaAlu_Click">FINALIZAR ALUNO</asp:LinkButton>
                </li>
            </ul>
        </div> 

        <div id="tabResp" clientidmode="Static" runat="server">
            <ul id="ul1" class="ulDados2">
                <li style="width: 100%;text-align: center;text-transform:uppercase; background-color:#E8E8E8;margin-top:-7px;margin-bottom: 5px;">
                    <label style="font-size: 1.1em;font-family:Tahoma;">INFORMA��ES DE RESPONS�VEL DE ALUNO</label>
                </li>
                <li id="liPhoto" class="liPhoto">
                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator41" ValidationGroup="incResAli"
                        runat="server" ControlToValidate="txtDtFimRestri" ErrorMessage="Data Final deve ser informada"
                        Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>    --%>

                    <img id="imgResp" runat="server" alt="Imagem" style="height:85px;width:64px;border-width:0px;" src="../../../../../Library/IMG/Gestor_SemImagem.png">                
                </li>
                
                <li id="liDados1" class="liDados1">
                    <ul>       
                        <li id="liNIS" class="liNISR">
                            <label for="txtNISResp" title="NIS">
                                N� NIS</label>
                            <asp:TextBox ID="txtNISResp" ToolTip="Informe o NIS" CssClass="txtNISResp" runat="server"
                                MaxLength="15"></asp:TextBox>
                        </li>                     
                        <li class="liNome">
                            <label for="txtNomeResp" class="lblObrigatorio" title="Nome do Respons�vel">
                                Nome</label>
                            <asp:TextBox ID="txtNomeResp" CssClass="txtNome" ToolTip="Informe o Nome do Respons�vel"
                                runat="server" MaxLength="60"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNome" runat="server" ValidationGroup="resp" ControlToValidate="txtNomeResp"
                                ErrorMessage="Nome deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>                            
                        <li style="margin-left: 15px;">
                            <label for="ddlSexoResp" title="Sexo">
                                Sexo</label>
                            <asp:DropDownList ID="ddlSexoResp" ToolTip="Selecione o Sexo" CssClass="ddlSexoResp" runat="server">
                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                <asp:ListItem Value="F">Feminino</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="margin-left: 15px;">
                            <label title="Tipo de Sangue">
                                Tp.Sangu�neo</label>
                            <asp:DropDownList ID="ddlTpSangueResp" ToolTip="Selecione o Tipo de Sangue" CssClass="ddlTpSangueAluno"
                                runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="A">A</asp:ListItem>
                                <asp:ListItem Value="B">B</asp:ListItem>
                                <asp:ListItem Value="AB">AB</asp:ListItem>
                                <asp:ListItem Value="O">O</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlStaTpSangueResp" ToolTip="Selecione o Status do Tipo de Sangue" CssClass="ddlStaTpSangueAluno"
                                runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="P">+</asp:ListItem>
                                <asp:ListItem Value="N">-</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="margin-left: 15px;">
                            <label for="ddlDeficienciaResp" title="Defici�ncia">
                                Defici�ncia</label>
                            <asp:DropDownList ID="ddlDeficienciaResp" ToolTip="Selecione a Defici�ncia" CssClass="ddlDeficienciaResp"
                                runat="server">
                                <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                                <asp:ListItem Value="A">Auditivo</asp:ListItem>
                                <asp:ListItem Value="V">Visual</asp:ListItem>
                                <asp:ListItem Value="F">F�sico</asp:ListItem>
                                <asp:ListItem Value="M">Mental</asp:ListItem>
                                <asp:ListItem Value="I">M�ltiplas</asp:ListItem>
                                <asp:ListItem Value="O">Outros</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liClear">
                            <label for="txtDtNascResp" title="Data de Nascimento" class="lblObrigatorio">
                                Data Nascimento</label>
                            <asp:TextBox ID="txtDtNascResp" ToolTip="Informe a Data de Nascimento" CssClass="txtDtNascimentoResp campoData"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDtNasc" ValidationGroup="resp" runat="server"
                                ControlToValidate="txtDtNascResp" ErrorMessage="Data de Nascimento deve ser informada"
                                Text="*" Display="None"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revDtNasc" ValidationGroup="resp" ControlToValidate="txtDtNascResp"
                                runat="server" ErrorMessage="Data de Nascimento inv�lida" ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$"
                                Display="None"></asp:RegularExpressionValidator>
                        </li>
                        <li style="margin-left: 15px;">
                            <label for="ddlEstadoCivilResp" title="Estado Civil">
                                Estado Civil</label>
                            <asp:DropDownList ID="ddlEstadoCivilResp" ToolTip="Selecione o Estado Civil" CssClass="ddlEstadoCivilResp"
                                runat="server">
                                <asp:ListItem Value="">N�o Informado</asp:ListItem>
                                <asp:ListItem Value="S">Solteiro(a)</asp:ListItem>
                                <asp:ListItem Value="C">Casado(a)</asp:ListItem>
                                <asp:ListItem Value="S">Separado Judicialmente</asp:ListItem>
                                <asp:ListItem Value="D">Divorciado(a)</asp:ListItem>
                                <asp:ListItem Value="V">Vi�vo(a)</asp:ListItem>
                                <asp:ListItem Value="P">Companheiro(a)</asp:ListItem>
                                <asp:ListItem Value="U">Uni�o Est�vel</asp:ListItem>
                                <asp:ListItem Value="O">Outro</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                        <li style="margin-left: 15px;">
                            <label for="ddlEstadoCivilR" title="Nacionalidade">
                                Nacionalidade</label>
                            <asp:DropDownList ID="ddlNacioResp" ToolTip="Selecione a Nacionalidade do Respons�vel" CssClass="ddlNacioResp" OnSelectedIndexChanged="ddlNacioResp_SelectedIndexChanged" AutoPostBack="true"
                                runat="server">
                                <asp:ListItem Value="B">Brasileira</asp:ListItem>
                                <asp:ListItem Value="E">Estrangeira</asp:ListItem>
                            </asp:DropDownList>
                        </li>    
                        <li>
                            <label for="txtNaturalidadeResp" title="Cidade de Naturalidade do Respons�vel">
                                Naturalidade</label>
                            <asp:TextBox ID="txtNaturalidadeResp" CssClass="txtNaturalidadeAluno" runat="server" ToolTip="Informe a Cidade de Naturalidade do Respons�vel"
                                MaxLength="40"></asp:TextBox>
                        </li>       
                        <li>
                            <label for="ddlUfNacionalidadeResp" title="UF de Nacionalidade do Respons�vel">
                                UF</label>
                            <asp:DropDownList ID="ddlUfNacionalidadeResp" CssClass="campoUf" runat="server" ToolTip="Informe a UF de Nacionalidade">
                            </asp:DropDownList>
                        </li>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        <li style="margin-left: 15px;">
                            <label for="ddlGrauInstrucaoResp" title="Escolaridade">
                                Escolaridade</label>
                            <asp:DropDownList ID="ddlGrauInstrucaoResp" ToolTip="Selecione a Escolaridade"
                                CssClass="ddlGrauInstrucaoResp" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvGrauInstrucao" ValidationGroup="resp" runat="server"
                                ControlToValidate="ddlGrauInstrucaoResp" ErrorMessage="Escolaridade deve ser informada"
                                Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>                                                 
                        <li class="liClear">
                            <label for="txtMaeResp" title="Nome da M�e do Respons�vel">
                                Nome da M�e</label>
                            <asp:TextBox ID="txtMaeResp" ToolTip="Informe o Nome da M�e do Respons�vel" CssClass="txtMaeResp"
                                runat="server" MaxLength="60"></asp:TextBox>
                            
                        </li>
                        <li id="liPaiResp" class="liPaiResp">
                            <label for="txtPaiResp" title="Nome do Pai do Respons�vel">
                                Nome do Pai</label>
                            <asp:TextBox ID="txtPaiResp" ToolTip="Informe o Nome do Pai do Respons�vel" CssClass="txtPaiResp"
                                runat="server" MaxLength="60"></asp:TextBox>
                        </li>      
                        <li style="margin-left: 12px;clear: none !important;margin-top: 13px;"><asp:CheckBox CssClass="chkLocais" ID="chkRespFunc" TextAlign="Right"
                                runat="server" ToolTip="Respons�vel � Funcion�rio" Text="Funcion�rio" />
                        </li>                                                                                                                                                                         
                    </ul>
                </li> 
                    <li class="liDados3">
                    <ul>  
                        <li style="clear: both;margin-top: 3px;">
                            <label style="text-transform:uppercase;font-weight: bold;font-size: 0.9em !important;">Informa��o de Documentos</label>
                        </li>
                        <li class="liClear" style="margin-left:-10px; margin-right: 0px;">
                            <fieldset class="fldIdentidade">
                                <legend class="lblObrigatorio">CPF</legend>
                                <ul>
                                    <li>
                                    <label for="txtCPFRespDados" title="CPF">
                                        N�mero</label>
                                    <asp:TextBox ID="txtCPFRespDados" ToolTip="Informe o CPF" Width="75px" CssClass="txtCPF"
                                        runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCPF" ValidationGroup="resp" runat="server" ControlToValidate="txtCPFRespDados"
                                        ErrorMessage="CPF deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="hdCodResp" runat="server" />
                                    </li>
                                </ul>
                            </fieldset>
                        </li>  
                        <li style="margin-right: 0px;">
                            <fieldset id="fldIdentidade" class="fldIdentidade">
                                <legend class="lblObrigatorio">Carteira de Identidade</legend>
                                <ul>
                                    <li id="liNrIdentidade" class="liNrIdentidade">
                                        <label for="txtIdentidadeResp" title="N�mero">
                                            N�mero</label>
                                        <asp:TextBox ID="txtIdentidadeResp" ToolTip="Informe o N�mero da Identidade" CssClass="txtIdentidadeResp"
                                            runat="server" MaxLength="20"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="resp" runat="server"
                                            ControlToValidate="txtIdentidadeResp" ErrorMessage="N�mero do RG deve ser informado"
                                            Text="*" Display="None"></asp:RequiredFieldValidator>
                                    </li>
                                    <li id="liDtEmissaoIden" class="liDtEmissaoIden">
                                        <label for="txtDtEmissaoResp" title="Data de Emiss�o">
                                            Data Emiss�o</label>
                                        <asp:TextBox ID="txtDtEmissaoResp" ToolTip="Informe a Data de Emiss�o" CssClass="txtDtEmissaoResp campoData"
                                            runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="resp"
                                            ControlToValidate="txtDtEmissaoResp" runat="server" ErrorMessage="Data de Emiss�o inv�lida"
                                            ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$"
                                            Display="None"></asp:RegularExpressionValidator>
                                        
                                    </li>
                                    <li id="liOrgEmissor" class="liOrgEmissor">
                                        <label for="txtOrgEmissorResp" title="Org�o Emissor">
                                            Org�o Emissor</label>
                                        <asp:TextBox ID="txtOrgEmissorResp" ToolTip="Informe o Org�o Emissor" CssClass="txtOrgEmissorResp"
                                            runat="server" MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="resp" runat="server"
                                            ControlToValidate="txtOrgEmissorResp" ErrorMessage="�rg�o Emissor deve ser informado"
                                            Text="*" Display="None"></asp:RequiredFieldValidator>
                                    </li>
                                    <li id="liIdentidadeUF" class="liIdentidadeUF">
                                        <label for="ddlIdentidadeUFResp" title="UF">
                                            UF</label>
                                        <asp:DropDownList ID="ddlIdentidadeUFResp" ToolTip="Selecione a UF" CssClass="ddlUF"
                                            runat="server">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </fieldset>
                        </li>                          
                        <li style="margin-right: 0px;">
                            <fieldset id="fldTituloEleitor" class="fldTituloEleitor">
                                <legend>T�tulo de Eleitor</legend>
                                <ul>
                                    <li id="liNumeroTitulo" class="liNumeroTitulo">
                                        <label for="txtNumeroTituloResp" title="N�mero do T�tulo">
                                            N�mero</label>
                                        <asp:TextBox ID="txtNumeroTituloResp" ToolTip="Informe o N�mero do T�tulo" CssClass="txtNumeroTituloResp"
                                            runat="server" MaxLength="15"></asp:TextBox>
                                    </li>
                                    <li id="liZona" class="liZona">
                                        <label for="txtZonaResp" title="Zona Eleitoral">
                                            Zona</label>
                                        <asp:TextBox ID="txtZonaResp" ToolTip="Informe a Zona Eleitoral" CssClass="txtZonaResp"
                                            runat="server" MaxLength="10"></asp:TextBox>
                                    </li>
                                    <li id="liSecao" class="liSecao">
                                        <label for="txtSecaoResp" title="Se��o">
                                            Se��o</label>
                                        <asp:TextBox ID="txtSecaoResp" ToolTip="Informe a Se��o" CssClass="txtSecaoResp" runat="server"
                                            MaxLength="10"></asp:TextBox>
                                    </li>
                                    <li id="liUfTitulo" class="liUfTitulo">
                                        <label for="ddlUfTituloResp" title="UF">
                                            UF</label>
                                        <asp:DropDownList ID="ddlUfTituloResp" ToolTip="Informe a UF" CssClass="ddlUF" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvUfTituloResp" ValidationGroup="resp" runat="server"
                                            ControlToValidate="ddlUfTituloResp" ErrorMessage="A UF do t�tulo de eleitor do respons�vel deve ser informada"
                                            Text="*"></asp:RequiredFieldValidator>
                                    </li>
                                </ul>
                            </fieldset>
                        </li>
                        <li style="margin-right: 0px;">
                            <fieldset id="Fieldset4" class="fldTituloEleitor">
                                <legend>Passaporte</legend>
                                <ul>
                                    <li id="li11" class="liNumeroTitulo">
                                        <label for="txtPassaporteResp" title="N�mero do Passaporte">
                                            N�mero</label>
                                        <asp:TextBox ID="txtPassaporteResp" ToolTip="Informe o N�mero do Passaporte" CssClass="txtPassaporteResp"
                                            runat="server" MaxLength="9"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label for="ddlNacioPassaResp" title="Nacionalidade">
                                            Nacionalidade</label>
                                        <asp:DropDownList ID="ddlNacioPassaResp" Width="70px" ToolTip="Selecione a Nacionalidade Passaporte do Respons�vel" CssClass="ddlNacioResp" OnSelectedIndexChanged="ddlNacioResp_SelectedIndexChanged" AutoPostBack="true"
                                            runat="server">
                                            <asp:ListItem Value="B">Brasileira</asp:ListItem>
                                            <asp:ListItem Value="E">Estrangeira</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </fieldset>
                        </li>
                        <li>
                            <ul>
                                <li>
                                    <ul>
                                        <li class="liEndResResp">
                                            <fieldset id="Fieldset1" class="fldFiliaResp">
                                                <legend>ENDERE�O RESIDENCIAL</legend>
                                                <ul>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                    <li class="liCep">
                                                        <label for="txtCepResp" title="Cep" class="lblObrigatorio">
                                                            Cep</label>
                                                        <asp:TextBox ID="txtCepResp" ToolTip="Informe o Cep" CssClass="txtCepResp" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvCep" ValidationGroup="resp" runat="server" ControlToValidate="txtCepResp"
                                                            ErrorMessage="CEP deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                                                    </li>
                                                    <li id="liPesqCEPR" class="liPesqCEPResp" runat="server">
                                                        <asp:ImageButton ID="btnPesqCEPR" runat="server" onclick="btnPesqCEPR_Click"
                                                            ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" CssClass="btnPesqMat"
                                                            CausesValidation="false"/>
                                                    </li>
                                                    <li>
                                                        <label for="txtLogradouroResp" class="lblObrigatorio" title="Logradouro">
                                                            Logradouro</label>
                                                        <asp:TextBox ID="txtLogradouroResp" CssClass="txtLogradouroResp" ToolTip="Informe o Logradouro"
                                                            runat="server" MaxLength="40"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvLogradouro" ValidationGroup="resp" runat="server"
                                                            ControlToValidate="txtLogradouroResp" ErrorMessage="Endere�o deve ser informado"
                                                            Text="*" Display="None"></asp:RequiredFieldValidator>
                                                    </li>
                                                    <li class="liNumero">
                                                        <label for="txtNumeroResp" title="N�mero">
                                                            N�mero</label>
                                                        <asp:TextBox ID="txtNumeroResp" ToolTip="Informe o N�mero" CssClass="txtNumeroResp" runat="server"
                                                            MaxLength="5"></asp:TextBox>
                                                    </li>
                                                    <li class="liComplementoR">
                                                        <label for="txtComplementoResp" title="Complemento">
                                                            Complemento</label>
                                                        <asp:TextBox ID="txtComplementoResp" ToolTip="Informe o Complemento" CssClass="txtComplementoResp"
                                                            runat="server" MaxLength="40"></asp:TextBox>
                                                    </li>
                                                    <li class="liBairroR">
                                                        <label for="ddlBairroResp" title="Bairro" class="lblObrigatorio">
                                                            Bairro</label>
                                                        <asp:DropDownList ID="ddlBairroResp" ToolTip="Selecione o Bairro" CssClass="ddlBairroResp"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlBairroResp" ValidationGroup="resp" runat="server" ControlToValidate="ddlBairroResp"
                                                            ErrorMessage="Bairro deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                                                    </li>
                                                    <li style="clear:both;margin-top:-3px;">
                                                        <label for="ddlCidadeResp" title="Cidade" class="lblObrigatorio">
                                                            Cidade</label>
                                                        <asp:DropDownList ID="ddlCidadeResp" ToolTip="Selecione a Cidade" CssClass="ddlCidadeResp"
                                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCidadeResp_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlCidadeResp" ValidationGroup="resp" runat="server" ControlToValidate="ddlCidadeResp"
                                                            ErrorMessage="Cidade deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                                                    </li>                                        
                                                    <li style="margin-top:-3px;">
                                                        <label for="ddlUfResp" title="UF" class="lblObrigatorio">
                                                            UF</label>
                                                        <asp:DropDownList ID="ddlUfResp" ToolTip="Selecione a UF" CssClass="ddlUF" runat="server"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlUfResp_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvUf" ValidationGroup="resp" runat="server" ControlToValidate="ddlUfResp"
                                                            ErrorMessage="UF deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                                                    </li>   
                                                    </ContentTemplate>
                                                    </asp:UpdatePanel>                                     
                                                </ul>
                                                </fieldset>
                                        </li>
                                        <li id="liEndProfResp" class="liInfSocResp">
                                            <fieldset id="fldEndProfResp" class="fldFiliaResp">
                                                <legend>ENDERE�O PROFISSIONAL</legend>
                                                <ul>
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                    <li class="liCep">
                                                        <label for="txtCepEmpresaResp" title="Cep">
                                                            Cep</label>
                                                        <asp:TextBox ID="txtCepEmpresaResp" ToolTip="Informe o Cep" CssClass="txtCepResp" runat="server"></asp:TextBox>                                            
                                                    </li>
                                                    <li id="li6" class="liPesqCEPResp" runat="server">
                                                        <asp:ImageButton ID="btnCEPEmp" runat="server" onclick="btnCEPEmp_Click"
                                                            ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" CssClass="btnPesqMat"
                                                            CausesValidation="false"/>
                                                    </li>
                                                    <li>
                                                        <label for="txtLogradouroEmpResp" title="Logradouro">
                                                            Logradouro</label>
                                                        <asp:TextBox ID="txtLogradouroEmpResp" CssClass="txtLogradouroResp" ToolTip="Informe o Logradouro"
                                                            runat="server" MaxLength="100"></asp:TextBox>                                            
                                                    </li>
                                                    <li class="liNumero">
                                                        <label for="txtNumeroEmpResp" title="N�mero">
                                                            N�mero</label>
                                                        <asp:TextBox ID="txtNumeroEmpResp" ToolTip="Informe o N�mero" CssClass="txtNumeroResp" runat="server"
                                                            MaxLength="5"></asp:TextBox>
                                                    </li>
                                                    <li class="liComplementoR">
                                                        <label for="txtComplementoEmpResp" title="Complemento">
                                                            Complemento</label>
                                                        <asp:TextBox ID="txtComplementoEmpResp" ToolTip="Informe o Complemento" CssClass="txtComplementoResp"
                                                            runat="server" MaxLength="15"></asp:TextBox>
                                                    </li>
                                                    <li class="liBairroR">
                                                        <label for="ddlBairroEmpResp" title="Bairro">
                                                            Bairro</label>
                                                        <asp:DropDownList ID="ddlBairroEmpResp" ToolTip="Selecione o Bairro" CssClass="ddlBairroResp"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                    </li>
                                                    <li style="clear:both;margin-top:-3px;">
                                                        <label for="ddlCidadeR" title="Cidade">
                                                            Cidade</label>
                                                        <asp:DropDownList ID="ddlCidadeEmpResp" ToolTip="Selecione a Cidade" CssClass="ddlCidadeResp"
                                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCidadeEmpResp_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </li>                                        
                                                    <li style="margin-top:-3px;">
                                                        <label for="ddlUfEmpResp" title="UF">
                                                            UF</label>
                                                        <asp:DropDownList ID="ddlUfEmpResp" ToolTip="Selecione a UF" CssClass="ddlUF" runat="server"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlUfEmpResp_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </li>   
                                                    </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <li class="liTelEmp">
                                                        <label for="txtTelEmpresaResp" title="Telefone Trabalho">
                                                            Telefone Trabalho</label>
                                                        <asp:TextBox ID="txtTelEmpresaResp" ToolTip="Informe o Telefone Trabalho" CssClass="txtTelEmpresaResp"
                                                            runat="server"></asp:TextBox>
                                                    </li>                                                                             
                                                </ul>
                                                </fieldset>
                                        </li>
                                    </ul>
                                </li>
                                <li style="clear: none; width: 370px;">
                                    <ul>
                                        <li style="border-width: 0px;">
                                            <fieldset id="Fieldset3" class="fldFiliaResp">
                                                <legend>INFORMA��O DE CONTATO</legend>
                                                <ul>
                                                    <li class="liClear">
                                                        <label for="txtEmailResp" title="Email">
                                                            Email</label>
                                                        <asp:TextBox ID="txtEmailResp" ToolTip="Informe o Email do Respons�vel" CssClass="txtEmailResp" runat="server"
                                                            MaxLength="255"></asp:TextBox>
                                                    </li>
                                                    <li>
                                                        <label for="txtTelResidencialResp" title="Telefone Residencial">
                                                            Telefone Fixo</label>
                                                        <asp:TextBox ID="txtTelResidencialResp" ToolTip="Informe o Telefone Residencial" CssClass="txtTelResidencialResp"
                                                            runat="server"></asp:TextBox>
                                                    </li>                                                                                                                                                                                 
                                                    <li>
                                                        <label for="txtTelCelularResp" title="Telefone Celular">
                                                            Telefone Celular</label>
                                                        <asp:TextBox ID="txtTelCelularResp" ToolTip="Informe o Telefone Celular" CssClass="txtTelCelularResp"
                                                            runat="server"></asp:TextBox>
                                                    </li>
                                                </ul>
                                            </fieldset>
                                        </li>
                                        <li class="liClear" style="border-width: 0px;">
                                            <fieldset class="fldFiliaResp" style="border-width: 0px;">
                                                <legend>INFORMA��ES SOCIAIS</legend>
                                                <ul style="margin-top: 4px;">
                                                    <li style="margin-left: -5px;margin-right: 10px !important;">
                                                        <fieldset class="fldDepenResp">
                                                        <legend>Dependentes</legend>
                                                        <ul>
                                                            <li id="liDepMenResp" class="liDepMenResp">
                                                                <span title="Depend�ncia Menores de 18">
                                                                    Menor 18</span>
                                                                <asp:TextBox ID="txtDepMenResp" ToolTip="Informe a Quantidade de Dependentes Menores de 18" CssClass="txtQtdMenoresResp"
                                                                    runat="server"></asp:TextBox>
                                                            </li>
                                                            <li id="liDepMaiResp" class="liDepMaiResp">
                                                                <span title="Depend�ncia Maiores de 18">
                                                                    Maior 18</span>
                                                                <asp:TextBox ID="txtDepMaiResp" ToolTip="Informe a Quantidade de Dependentes Maiores de 18" CssClass="txtQtdMenoresResp"
                                                                    runat="server"></asp:TextBox>
                                                            </li>
                                                        </ul>
                                                        </fieldset>
                                                    </li>     
                                                    <li class="liRenda">
                                                        <label for="ddlRendaResp" title="Renda Familiar">
                                                            Renda Familiar</label>
                                                        <asp:DropDownList ID="ddlRendaResp" ToolTip="Selecione a Renda Familiar" CssClass="ddlRendaResp"
                                                            runat="server">
                                                            <asp:ListItem Value="1">1 a 3 SM</asp:ListItem>
                                                            <asp:ListItem Value="2">3 a 5 SM</asp:ListItem>
                                                            <asp:ListItem Value="3">5 a 10 SM</asp:ListItem>
                                                            <asp:ListItem Value="4">+10 SM</asp:ListItem>
                                                            <asp:ListItem Value="5">Sem Renda</asp:ListItem>
                                                            <asp:ListItem Value="6">N�o informada</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </li>                                   
                                                    <li id="liResidResp" class="liResidResp">
                                                        <label>
                                                            Tp Resid�ncia/Anos</label>
                                                        <asp:DropDownList ID="ddlTpResidResp" ToolTip="Selecione o Tipo de Resid�ncia do Respons�vel" CssClass="ddlTpResidResp"
                                                            runat="server">
                                                            <asp:ListItem Value="A">Alugada</asp:ListItem>
                                                            <asp:ListItem Value="P">Pr�pria</asp:ListItem>
                                                            <asp:ListItem Value="M">Com os Pais</asp:ListItem>
                                                            <asp:ListItem Value="C">Cedida</asp:ListItem>
                                                            <asp:ListItem Value="O">Outros</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtQtdAnoResidResp" ToolTip="Informe a Quantidade de Anos de Resid�ncia do Respons�vel" CssClass="txtQtdMenoresResp"
                                                            runat="server"></asp:TextBox>
                                                    </li>                                                                                            
                                                    <li class="liClear">
                                                        <label for="ddlProfissaoResp" title="Profiss�o">
                                                            Profiss�o</label>
                                                        <asp:DropDownList ID="ddlProfissaoResp" ToolTip="Selecione a Profiss�o do Respons�vel"
                                                            CssClass="ddlProfissaoResp" runat="server">
                                                        </asp:DropDownList>
                                                    </li>
                                                    <li id="liTrabResp" class="liResidResp">
                                                        <label>
                                                            Trabalhando?/Desde?</label>
                                                        <asp:DropDownList ID="ddlTrabResp" ToolTip="Respons�vel Trabalhando?" CssClass="ddlTrabResp"
                                                            runat="server">
                                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                                            <asp:ListItem Value="N">N�o</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtMesAnoTrabResp" ToolTip="M�s/Ano de In�cio de Trabalho do Respons�vel" CssClass="txtMesAnoTrabResp"
                                                            runat="server"></asp:TextBox>
                                                    </li>  
                                                    <li style="margin-right: 0px;">
                                                      <label for="ddlOrigemResp" class="" title="Origem">Origem</label>
                                                      <asp:DropDownList ID="ddlOrigemResp" style="width:100px;" runat="server" ToolTip="Origem">
                                                        <asp:ListItem Value="SR" Text="Sem Registro"></asp:ListItem>
                                                        <asp:ListItem Value="AI" Text="�rea Ind�gena"></asp:ListItem>
                                                        <asp:ListItem Value="AQ" Text="�rea Quilombo"></asp:ListItem>
                                                        <asp:ListItem Value="AR" Text="�rea Rural"></asp:ListItem>
                                                        <asp:ListItem Value="IE" Text="Interior do Estado"></asp:ListItem>
                                                        <asp:ListItem Value="MU" Text="Munic�pio"></asp:ListItem>
                                                        <asp:ListItem Value="OE" Text="Outro Estado"></asp:ListItem>
                                                        <asp:ListItem Value="OO" Text="Outra Origem"></asp:ListItem>            
                                                      </asp:DropDownList>
                                                    </li>                                      
                                                </ul>
                                            </fieldset>
                                        </li>  
                                        <li class="liClear">
                                            <fieldset id="Fieldset2" class="fldFiliaResp">
                                                <legend>OBSERVA��ES</legend>
                                                <ul style="margin-top: 4px;">
                                                    <li id="liObsResponsavel" class="liObsResponsavel">
                                                        <asp:TextBox ID="txtObservacoesResp" CssClass="txtObservacoesResp" ToolTip="Informe as Observa��es sobre o Respons�vel" runat="server" 
                                                        TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 500);"></asp:TextBox>
                                                    </li>
                                                    <li style="margin-left: -5px;clear: both;margin-top: 5px;">
                                                    <asp:CheckBox CssClass="chkLocais" ID="chkResponAluno" TextAlign="Right"
                                                            runat="server" ToolTip="Respons�vel � Aluno" Text="Respons�vel � Aluno" />
                                                    </li>
                                                </ul>
                                             </fieldset>
                                        </li>  
                                    </ul>
                                </li>
                            </ul>
                        </li>                                                                                                                                                                               
                        <li onclick="" id="li1" style="margin-top: -5px;" runat="server" title="Clique para Registrar o Respons�vel" class="liBtnAdd">
                            <asp:LinkButton ID="btnAddRespon" ValidationGroup="resp" CssClass="btnAddRespon" runat="server" OnClick="imgAdd_Click">FINALIZAR RESPONS�VEL</asp:LinkButton>
                        </li>                                                                                                                                                                                                                
                    </ul>
                </li>                                                                               
            </ul>
        </div>                                       
            
        <div id="tabEndAdd" runat="server" clientidmode="Static" style="display: none;">                  
                <ul id="ul10" class="ulDados2">
                    <li style="width: 100%;text-align: center;text-transform:uppercase;margin-top:-7px;background-color: #FDF5E6;margin-bottom: 5px;">
                        <label style="font-size: 1.1em;font-family:Tahoma;">INFORMA��ES CADASTRAIS DO ALUNO - ENDERE�OS ADICIONAIS</label>
                    </li>                   
                    <li class="liNomeAluETA">
                        <label for="txtNomeAluETA" title="Nome do Aluno">
                            NOME</label>
                        <asp:TextBox ID="txtNomeAluETA" CssClass="campoNomePessoa" runat="server" Enabled="false" ToolTip="Nome do Aluno">
                        </asp:TextBox>
                    </li>   
                    <li class="liNIREAluETA">
                        <label for="txtNIREAluETA" title="N� NIRE do Aluno">
                            N� NIRE</label>
                        <asp:TextBox ID="txtNIREAluETA" runat="server" CssClass="txtNireAluno" Enabled="false" ToolTip="N� NIRE do Aluno">
                        </asp:TextBox>
                    </li>  
                    <li class="liNISAluETA">
                        <label for="txtNISAluETA" title="N� NIS do Aluno">
                            N� NIS</label>
                        <asp:TextBox ID="txtNISAluETA" runat="server" CssClass="txtNisAluno" Enabled="false" ToolTip="N� NIS do Aluno">
                        </asp:TextBox>
                    </li>   
                    <li class="G2Clear" style="width: 100%; border-bottom:1px solid #CCCCCC;margin-left: 15px;padding-bottom: 5px;margin-top: 5px;">
                        <label style="font-family: Tahoma;">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Para incluir um novo endere�o adicional do aluno informe os dados de cada linha acima da grid de dados
                        correspondente, ap�s clique no bot�o de INCLUIR.<br /> Para excluir, marque a coluna CHECK na grid de dados da informa��o que
                        deseja eliminar e click no bot�o excluir para efetivar a exclus�o desejada.</label>
                    </li>
                    <li class="liDescTelAdd" style="width: 100%;text-align:center;margin-top: 2px;margin-bottom: 20px;">
                        <label> Os campos com <span class="asteriscoObrigatorio">*</span>&nbsp; s�o obrigat�rios e devem ser informados.
                        </label>
                    </li>                             
                    <li class="liddlTpTelef">
                        <label for="ddlTpEnderETA" class="lblObrigatorio" title="Tipo de Endere�o">
                            Tipo</label>
                        <asp:DropDownList ID="ddlTpEnderETA" ToolTip="Selecione o Tipo de Endere�o" CssClass="ddlTpTelef" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="ddlTpEnderETA"
                                            ErrorMessage="Tipo de Endere�o deve ser informado" ValidationGroup="incEndAlu" Text="*"
                                            Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                    <li style="margin-left: 10px;">
                        <label for="txtCepETA" class="lblObrigatorio" title="CEP">
                            CEP</label>
                        <asp:TextBox ID="txtCepETA" CssClass="campoCep" ToolTip="Informe o CEP" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCepETA"
                            ErrorMessage="CEP deve ser informado" ValidationGroup="incEndAlu" Text="*" Display="None"
                            CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li id="li2" class="liPesqCEPResp" runat="server" style="margin-top: 14px;">
                        <asp:ImageButton ID="btnCEPETA" runat="server" onclick="btnCEPETA_Click"
                            ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" CssClass="btnPesqMat"
                            CausesValidation="false"/>
                    </li>
                    <li class="litxtTelETA">
                        <label for="txtLograETA" class="lblObrigatorio" title="Logradouro do Aluno">
                            Logradouro</label>
                        <asp:TextBox ID="txtLograETA" CssClass="txtLograETA" runat="server" ToolTip="Digite o Logradouro do Aluno">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" ValidationGroup="incEndAlu"
                            runat="server" ControlToValidate="txtLograETA" ErrorMessage="Logradouro deve ser informado"
                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li class="liNomeContETA">
                        <label for="txtNumETA" title="N�mero do Endere�o">
                            N�mero</label>
                        <asp:TextBox ID="txtNumETA" CssClass="txtNumETA" runat="server" ToolTip="Digite o N�mero do Endere�o">
                        </asp:TextBox>
                    </li> 
                    <li class="liNomeContETA">
                        <label for="txtCompETA" title="Complemento do Endere�o">
                            Complemento</label>
                        <asp:TextBox ID="txtCompETA" CssClass="txtCompETA" runat="server" ToolTip="Digite o Complemento do Endere�o">
                        </asp:TextBox>
                    </li> 
                    <li class="liBairroETA">
                        <label for="ddlBairroETA" class="lblObrigatorio" title="Bairro">
                            Bairro</label>
                        <asp:DropDownList ID="ddlBairroETA" CssClass="ddlBairroAluno" ToolTip="Informe o Bairro"
                            runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" ValidationGroup="incEndAlu"
                            runat="server" ControlToValidate="ddlBairroETA" ErrorMessage="Bairro deve ser informado"
                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li id="li16">
                        <label for="ddlCidadeETA" class="lblObrigatorio" title="Cidade">
                            Cidade</label>
                        <asp:DropDownList ID="ddlCidadeETA" ToolTip="Informe a Cidade" runat="server" CssClass="ddlCidadeAluno"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlCidadeETA_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="ddlCidadeETA"
                            ErrorMessage="Cidade deve ser informada" ValidationGroup="incEndAlu" Text="*" Display="None"
                            CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>                                
                    <li id="li18">
                        <label for="ddlUFETA" class="lblObrigatorio" title="UF">
                            UF</label>
                        <asp:DropDownList ID="ddlUFETA" runat="server" CssClass="campoUf" ToolTip="Informe a UF"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlUFETA_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="ddlUFETA"
                            ErrorMessage="UF deve ser informado" ValidationGroup="incEndAlu" Text="*" Display="None"
                            CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>  
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    <li class="liBtnsEndAdd">
                        <asp:LinkButton ID="lnkIncEnd" OnClick="lnkIncEnd_Click" ValidationGroup="incEndAlu" runat="server" Style="margin: 0 auto;" ToolTip="Incluir Registro">
                            <img class="imgLnkInc" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa" title="Incluir Registro" />
                            <asp:Label runat="server" ID="Label9" Text="Incluir"></asp:Label>
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkExcEnd" OnClick="lnkExcEnd_Click" ValidationGroup="excEndAlu" runat="server" Style="margin: 0 auto;" ToolTip="Excluir Registro">
                            <img class="imgLnkExc" src='/Library/IMG/Gestor_BtnDel.png' alt="Icone Pesquisa" title="Excluir Registro" />
                            <asp:Label runat="server" ID="Label10" Text="Excluir"></asp:Label>
                        </asp:LinkButton>
                    </li>
                    <li class="liGridTelETA">
                        <div id="div6" runat="server" class="divGridTelETA" style="height:141px;">
                            <asp:GridView ID="grdEndETA" CssClass="grdBusca" Width="677px" runat="server"
                                AutoGenerateColumns="False" DataKeyNames="ID_ALUNO_ENDERECO">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum registro encontrado.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Check">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckSelect" runat="server" />
                                            <asp:HiddenField ID="hdID_ALUNO_ENDERECO" runat="server" Value='<%# bind("ID_ALUNO_ENDERECO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NM_TIPO_ENDERECO" HeaderText="TIPO">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cep" HeaderText="N� CEP">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DS_ENDERECO" HeaderText="LOGRADOURO"> 
                                        <ItemStyle Width="220px" />                                       
                                    </asp:BoundField>   
                                    <asp:BoundField DataField="NR_ENDERECO" HeaderText="N�">
                                        <ItemStyle Width="40px" />
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="DS_COMPLEMENTO" HeaderText="COMPLEMENTO"> 
                                        <ItemStyle Width="120px" />                                       
                                    </asp:BoundField>   
                                    <asp:BoundField DataField="NO_BAIRRO" HeaderText="BAIRRO"> 
                                        <ItemStyle Width="170px" />                                       
                                    </asp:BoundField>                                  
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>  
                    <li id="li20" runat="server" class="liBtnAddA" style="margin-left: 355px !important;">
                        <asp:LinkButton ID="lnkAtualizaEndAlu" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkAtualizaEndAlu_Click">FINALIZAR</asp:LinkButton>
                    </li>              
                </ul>
        </div>

        <div id="tabTelAdd" runat="server" clientidmode="Static" style="display: none;">                  
                <ul id="ul6" class="ulDados2">
                    <li style="width: 100%;text-align: center;text-transform:uppercase;margin-top:-7px;background-color: #FDF5E6;margin-bottom: 5px;">
                        <label style="font-size: 1.1em;font-family:Tahoma;">INFORMA��ES CADASTRAIS DO ALUNO - TELEFONES ADICIONAIS</label>
                    </li>                   
                    <li class="liNomeAluETA">
                        <label for="txtNomeAluETA" title="Nome do Aluno">
                            NOME</label>
                        <asp:TextBox ID="txtNomeAluTA" CssClass="campoNomePessoa" runat="server" Enabled="false" ToolTip="Nome do Aluno">
                        </asp:TextBox>
                    </li>   
                    <li class="liNIREAluETA">
                        <label for="txtNIREAluETA" title="N� NIRE do Aluno">
                            N� NIRE</label>
                        <asp:TextBox ID="txtNIREAluTA" runat="server" CssClass="txtNireAluno" Enabled="false" ToolTip="N� NIRE do Aluno">
                        </asp:TextBox>
                    </li>  
                    <li class="liNISAluETA">
                        <label for="txtNISAluETA" title="N� NIS do Aluno">
                            N� NIS</label>
                        <asp:TextBox ID="txtNISAluTA" runat="server" CssClass="txtNisAluno" Enabled="false" ToolTip="N� NIS do Aluno">
                        </asp:TextBox>
                    </li>   
                    <li class="G2Clear" style="width: 100%; border-bottom:1px solid #CCCCCC;margin-left: 15px;padding-bottom: 5px;margin-top: 5px;">
                        <label style="font-family: Tahoma;">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Para incluir um novo telefone adicional do aluno informe os dados de cada linha acima da grid de dados
                        correspondente, ap�s clique no bot�o de INCLUIR. <br /> Para excluir, marque a coluna CHECK na grid de dados da informa��o que
                        deseja eliminar e click no bot�o excluir para efetivar a exclus�o desejada.</label>
                    </li>
                    <li class="liDescTelAdd" style="width: 100%;text-align:center;margin-top: 2px;margin-bottom: 20px;">
                        <label> Os campos com <span class="asteriscoObrigatorio">*</span>&nbsp; s�o obrigat�rios e devem ser informados.
                        </label>
                    </li>
                    <li class="liddlTpTelef">
                        <label for="ddlTpTelef" class="lblObrigatorio" title="Tipo de Telefone">
                            Tipo</label>
                        <asp:DropDownList ID="ddlTpTelef" ToolTip="Selecione o Tipo de Telefone" CssClass="ddlTpTelef" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="ddlTpTelef"
                                            ErrorMessage="Tipo de telefone deve ser informado" ValidationGroup="incTelAlu" Text="*"
                                            Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li class="litxtTelETA">
                        <label for="txtTelETA" class="lblObrigatorio" title="N� Telefone do Aluno">
                            N� Telefone</label>
                        <asp:TextBox ID="txtTelETA" runat="server" CssClass="campoTelefone" ToolTip="Digite o N� Telefone do Aluno">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtTelETA"
                                            ErrorMessage="Telefone deve ser informado" ValidationGroup="incTelAlu" Text="*"
                                            Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li class="liNomeContETA">
                        <label for="txtNomeContETA" title="Nome do Contato do Telefone">
                            Nome do Contato</label>
                        <asp:TextBox ID="txtNomeContETA" CssClass="campoNomePessoa" runat="server" ToolTip="Digite o Nome do Contato do Telefone">
                        </asp:TextBox>
                    </li> 
                    <li class="liObsETA">
                        <label for="txtObsETA" title="Observa��o">
                            Observa��o</label>
                        <asp:TextBox ID="txtObsETA" style="width: 255px;" runat="server" ToolTip="Digite a Observa��o">
                        </asp:TextBox>
                    </li> 
                    <li id="li3" runat="server" class="liBtnsETA">
                        <asp:LinkButton ID="lnkIncTel" OnClick="lnkIncTel_Click" ValidationGroup="incTelAlu" runat="server" Style="margin: 0 auto;" ToolTip="Incluir Registro">
                            <img class="imgLnkInc" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa" title="Incluir Registro" />
                            <asp:Label runat="server" ID="Label1" Text="Incluir"></asp:Label>
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkExcTel" OnClick="lnkExcTel_Click" ValidationGroup="excTelAlu" runat="server" Style="margin: 0 auto;" ToolTip="Excluir Registro">
                            <img class="imgLnkExc" src='/Library/IMG/Gestor_BtnDel.png' alt="Icone Pesquisa" title="Excluir Registro" />
                            <asp:Label runat="server" ID="Label8" Text="Excluir"></asp:Label>
                        </asp:LinkButton>
                    </li>
                    <li class="liGridTelETA">
                        <div id="div5" runat="server" class="divGridTelETA">
                            <asp:GridView ID="grdTelETA" CssClass="grdBusca" Width="677px" runat="server"
                                AutoGenerateColumns="False" DataKeyNames="ID_ALUNO_TELEFONE">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum registro encontrado.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="CHECK">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckSelect" runat="server" />
                                            <asp:HiddenField ID="hdID_ALUNO_TELEFONE" runat="server" Value='<%# bind("ID_ALUNO_TELEFONE") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NM_TIPO_TELEFONE" HeaderText="TIPO">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="telefone" HeaderText="N� TELEFONE">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_CONTATO" HeaderText="NOME DO CONTATO"> 
                                        <ItemStyle Width="200px" />                                       
                                    </asp:BoundField>    
                                    <asp:BoundField DataField="DES_OBSERVACAO" HeaderText="OBSERVA��O">
                                        <ItemStyle Width="250px" />
                                    </asp:BoundField>                               
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>   
                    
                    <li id="li22" runat="server" class="liBtnAddA" style="margin-left: 355px !important;">
                        <asp:LinkButton ID="lnkAtualizaTelAlu" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkAtualizaTelAlu_Click">FINALIZAR</asp:LinkButton>
                    </li>                                       
                </ul>
        </div>

        <div id="tabCuiEspAdd" runat="server" clientidmode="Static" style="display: none;">                  
                <ul id="ul4" class="ulDados2">
                    <li style="width: 100%;text-align: center;text-transform:uppercase;margin-top:-7px;background-color: #FDF5E6;margin-bottom: 5px;">
                        <label style="font-size: 1.1em;font-family:Tahoma;">INFORMA��ES DO ALUNO - CUIDADOS ESPECIAIS</label>
                    </li>                   
                    <li class="liNomeAluETA">
                        <label for="txtNomeAluCEA" title="Nome do Aluno">
                            NOME</label>
                        <asp:TextBox ID="txtNomeAluCEA" CssClass="campoNomePessoa" runat="server" Enabled="false" ToolTip="Nome do Aluno">
                        </asp:TextBox>
                    </li>   
                    <li class="liNIREAluETA">
                        <label for="txtNIREAluCEA" title="N� NIRE do Aluno">
                            N� NIRE</label>
                        <asp:TextBox ID="txtNIREAluCEA" runat="server" CssClass="txtNireAluno" Enabled="false" ToolTip="N� NIRE do Aluno">
                        </asp:TextBox>
                    </li>  
                    <li class="liNISAluETA">
                        <label for="txtNISAluCEA" title="N� NIS do Aluno">
                            N� NIS</label>
                        <asp:TextBox ID="txtNISAluCEA" runat="server" CssClass="txtNisAluno" Enabled="false" ToolTip="N� NIS do Aluno">
                        </asp:TextBox>
                    </li>   
                    <li class="G2Clear" style="width: 100%; border-bottom:1px solid #CCCCCC;margin-left: 15px;padding-bottom: 5px;margin-top: 5px;">
                        <label style="font-family: Tahoma;margin-left: 15px;">
                        Para incluir um novo Cuidado Especial para com o Aluno informe os dados de cada linha acima da Grid de Dados
                        correspondente, ap�s clique em INCLUIR.<br /> &nbsp;&nbsp; Para excluir, marque a coluna CHECK na Grid de Dados da informa��o que
                        deseja eliminar e click no bot�o EXCLUIR para efetivar a exclus�o desejada.</label>
                    </li>
                    <li class="liDescTelAdd" style="width: 100%;text-align:center;margin-top: 2px;margin-bottom: 20px;">
                        <label> Os campos com <span class="asteriscoObrigatorio">*</span>&nbsp; s�o obrigat�rios e devem ser informados.
                        </label>
                    </li>
                    <li class="liddlTpCui">
                        <label for="ddlTpCui" class="lblObrigatorio" title="Tipo de Cuidado">
                            Tipo Cuidado</label>
                        <asp:DropDownList ID="ddlTpCui" ToolTip="Selecione o Tipo de Cuidado" CssClass="ddlTpCui" runat="server">
                            <asp:ListItem Value="M">Medica��o</asp:ListItem>
                            <asp:ListItem Value="A">Acompanhamento</asp:ListItem>
                            <asp:ListItem Value="C">Curativo</asp:ListItem>
                            <asp:ListItem Value="O">Outras</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="ddlTpCui"
                                            ErrorMessage="Tipo de Cuidado deve ser informado" ValidationGroup="incCuiEspAlu" Text="*"
                                            Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="ddlTpTelef" class="lblObrigatorio" title="Tipo de Aplica��o">
                            Tipo Aplica��o</label>
                        <asp:DropDownList ID="ddlTpApli" ToolTip="Selecione o Tipo de Aplica��o" style="width: 75px;" runat="server">
                            <asp:ListItem Value="O">Via Oral</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="ddlTpApli"
                                            ErrorMessage="Tipo de Aplica��o deve ser informado" ValidationGroup="incCuiEspAlu" Text="*"
                                            Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="txtHrAplic" title="Hora da Aplica��o">
                            Hora</label>
                        <asp:TextBox ID="txtHrAplic" runat="server" CssClass="txtHrAplic" ToolTip="Digite a hora da aplica��o">
                        </asp:TextBox>
                    </li>
                    <li>
                        <label for="txtNomeContETA" class="lblObrigatorio" title="Descri��o do Cuidado Especial">
                            Descri��o</label>
                        <asp:TextBox ID="txtDescCEA" MaxLength="50" CssClass="txtDescCEA" runat="server" ToolTip="Digite a Descri��o do Cuidado Especial">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txtDescCEA"
                                            ErrorMessage="Descri��o do Cuidado Especial deve ser informada" ValidationGroup="incCuiEspAlu" Text="*"
                                            Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li> 
                    <li>
                        <label for="txtQtdeCEA" title="Quantidade/Unidade">
                            Qtde/Unidade</label>
                        <asp:TextBox ID="txtQtdeCEA" CssClass="txtQtdeCEA" runat="server" ToolTip="Digite a Quantidade">
                        </asp:TextBox>
                        <asp:DropDownList ID="ddlUniCEA" CssClass="campoUf" runat="server" ToolTip="Informe a Unidade">
                        </asp:DropDownList>
                    </li>
                    <li style="margin-right: 0px;">
                        <label for="txtObsCEA" title="Observa��o">
                            Observa��o</label>
                        <asp:TextBox ID="txtObsCEA" style="width: 210px;" MaxLength="200" runat="server" ToolTip="Digite a Observa��o">
                        </asp:TextBox>
                    </li> 
                    <li id="liPeriodo" class="liPeriodo">
                        <label for="txtPeriodo">
                            Per�odo</label>
                                                    
                        <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" runat="server"></asp:TextBox>
                
                        <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> � </asp:Label>
            
                        <asp:TextBox ID="txtDataPeriodoFim" CssClass="campoData" runat="server"></asp:TextBox>    
            
                        <asp:CompareValidator id="CompareValidator1" runat="server" CssClass="validatorField"
                            ForeColor="Red" ValidationGroup="incCuiEspAlu"
                            ControlToValidate="txtDataPeriodoFim"
                            ControlToCompare="txtDataPeriodoIni"
                            Type="Date"       
                            Operator="GreaterThanEqual"      
                            ErrorMessage="Data Final n�o pode ser menor que Data Inicial." >
                        </asp:CompareValidator >                                        
                    </li>
                    <li style="margin-left: 5px;">
                        <label for="txtNomeMedCEA" title="Nome do M�dico">
                            Nome do M�dico</label>
                        <asp:TextBox ID="txtNomeMedCEA" style="width:150px;" MaxLength="60" runat="server" ToolTip="Digite o Nome do M�dico">
                        </asp:TextBox>
                    </li> 
                    <li>
                        <label for="txtNumCRMCEA" title="N�mero CRM / UF">
                            N� CRM / UF</label>
                        <asp:TextBox ID="txtNumCRMCEA" MaxLength="12" CssClass="txtNumCRMCEA" runat="server" ToolTip="Digite o N� CRM">
                        </asp:TextBox>
                        <asp:DropDownList ID="ddlUFCEA" CssClass="campoUf" runat="server" ToolTip="Informe a UF">
                        </asp:DropDownList>
                    </li> 
                    <li class="txtTelCEA">
                        <label for="txtTelETA" title="N� Telefone">
                            N� Telefone</label>
                        <asp:TextBox ID="txtTelCEA" runat="server" CssClass="campoTelefone" ToolTip="Digite o N� Telefone">
                        </asp:TextBox>
                    </li>
                    <li>
                        <label for="ddlRecCEA" title="Possui Receita?">
                            Receita?</label>
                        <asp:DropDownList ID="ddlRecCEA" CssClass="ddlPasseEscolarAluno" runat="server"
                            ToolTip="Informe se o Aluno possui Receita">
                            <asp:ListItem Value="S">Sim</asp:ListItem>
                            <asp:ListItem Value="N" Selected="True">N�o</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li runat="server" class="liBtnsCEA">
                        <asp:LinkButton ID="lnkIncCEA" OnClick="lnkIncCEA_Click" ValidationGroup="incCuiEspAlu" runat="server" Style="margin: 0 auto;" ToolTip="Incluir Registro">
                            <img class="imgLnkInc" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa" title="Incluir Registro" />
                            <asp:Label runat="server" ID="Label11" Text="Incluir"></asp:Label>
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkExcCEA" OnClick="lnkExcCEA_Click" runat="server" ValidationGroup="excCuiEspAlu" Style="margin: 0 auto;margin-left: 2px;" ToolTip="Excluir Registro">
                            <img class="imgLnkExc" style="margin-left: 3px;margin-top: -5px;" src='/Library/IMG/Gestor_BtnDel.png' alt="Icone Pesquisa" title="Excluir Registro" />
                            <asp:Label runat="server" ID="Label12" Style="margin-left: 3px;" Text="Excluir"></asp:Label>
                        </asp:LinkButton>
                    </li>
                    <li class="liGridCuiEsp">
                        <div id="div8" runat="server" class="divGridCEA" style="height:130px;">
                            <asp:GridView ID="grdCuiEsp" CssClass="grdBusca" runat="server" Width="680"
                                AutoGenerateColumns="False" DataKeyNames="ID_MEDICACAO">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum registro encontrado.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="CHECK">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckSelect" runat="server" />
                                            <asp:HiddenField ID="hdID_MEDICACAO" runat="server" Value='<%# bind("ID_MEDICACAO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TP_CUIDADO" HeaderText="TIPO">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TP_APLICAC_CUIDADO" HeaderText="Aplica��o">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HR_APLICAC_CUIDADO" HeaderText="Hora">
                                        <ItemStyle Width="30px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NM_REMEDIO_CUIDADO" HeaderText="DESCRI��O"> 
                                        <ItemStyle Width="200px" />                                       
                                    </asp:BoundField>    
                                    <asp:BoundField DataField="DE_DOSE_REMEDIO_CUIDADO" ItemStyle-HorizontalAlign="Right" HeaderText="Qtde">
                                        <ItemStyle Width="30px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UNIDADE" HeaderText="UNID">
                                        <ItemStyle Width="30px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DE_OBSERV_CUIDADO" HeaderText="OBSERVA��O">
                                        <ItemStyle Width="250px" />
                                    </asp:BoundField>  
                                    <asp:BoundField DataField="DT_RECEITA_INI" DataFormatString="{0:dd/MM/yyyy}" HeaderText="PER�ODO"> 
                                        <ItemStyle Width="50px" />                                       
                                    </asp:BoundField>   
                                    <asp:BoundField DataField="DT_RECEITA_FIM" DataFormatString="{0:dd/MM/yyyy}" HeaderText="RECEITA"> 
                                        <ItemStyle Width="50px" />                                       
                                    </asp:BoundField>   
                                    <asp:BoundField DataField="NM_MEDICO_CUIDADO" HeaderText="M�dico"> 
                                        <ItemStyle Width="200px" />                                       
                                    </asp:BoundField>   
                                    <asp:BoundField DataField="NR_CRM_MEDICO_CUIDADO" HeaderText="CRM"> 
                                        <ItemStyle Width="50px" />                                       
                                    </asp:BoundField>      
                                    <asp:BoundField DataField="CO_UF_MEDICO" HeaderText="UF"> 
                                        <ItemStyle Width="20px" />                                       
                                    </asp:BoundField>  
                                    <asp:BoundField DataField="NR_TELEF_MEDICO" HeaderText="Telefone"> 
                                        <ItemStyle Width="80px" />                                       
                                    </asp:BoundField>   
                                    <asp:BoundField DataField="FL_RECEITA_CUIDADO" HeaderText="Receita"> 
                                        <ItemStyle Width="50px" />                                       
                                    </asp:BoundField>               
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>   
                    <li id="li23" runat="server" class="liBtnAddA" style="margin-left: 355px !important;margin-top: 6px;">
                        <asp:LinkButton ID="lnkCuiEspAlu" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkCuiEspAlu_Click">FINALIZAR</asp:LinkButton>
                    </li>                                                       
                </ul>
        </div>

        <div id="tabResAliAdd" runat="server" clientidmode="Static" style="display: none;">                  
            <ul id="ul7" class="ulDados2">
                <li style="width: 100%;text-align: center;text-transform:uppercase;margin-top:-7px;background-color: #FDF5E6;margin-bottom: 5px;">
                    <label style="font-size: 1.1em;font-family:Tahoma;">INFORMA��ES CADASTRAIS DO ALUNO - RESTRI��O ALIMENTAR</label>
                </li>                   
                <li class="liNomeAluETA">
                    <label for="txtNomeAluRAA" title="Nome do Aluno">
                        NOME</label>
                    <asp:TextBox ID="txtNomeAluRAA" CssClass="campoNomePessoa" runat="server" Enabled="false" ToolTip="Nome do Aluno">
                    </asp:TextBox>
                </li>   
                <li class="liNIREAluETA">
                    <label for="txtNIREAluRAA" title="N� NIRE do Aluno">
                        N� NIRE</label>
                    <asp:TextBox ID="txtNIREAluRAA" runat="server" CssClass="txtNireAluno" Enabled="false" ToolTip="N� NIRE do Aluno">
                    </asp:TextBox>
                </li>  
                <li class="liNISAluETA">
                    <label for="txtNISAluRAA" title="N� NIS do Aluno">
                        N� NIS</label>
                    <asp:TextBox ID="txtNISAluRAA" runat="server" CssClass="txtNisAluno" Enabled="false" ToolTip="N� NIS do Aluno">
                    </asp:TextBox>
                </li>   
                <li class="G2Clear" style="width: 100%; border-bottom:1px solid #CCCCCC;margin-left: 15px;padding-bottom: 5px;margin-top: 5px;">
                    <label style="font-family: Tahoma;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Para incluir uma nova Restri��o Alimentar do aluno informe os dados de cada linha acima da grid de Dados
                    correspondente, ap�s clique em INCLUIR. <br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Para excluir, marque a coluna CHECK na Grid de Dados da informa��o que
                    deseja eliminar e click no bot�o excluir para efetivar a exclus�o desejada.</label>
                </li>
                <li class="liDescTelAdd" style="width: 100%;text-align:center;margin-top: 2px;margin-bottom: 20px;">
                    <label> Os campos com <span class="asteriscoObrigatorio">*</span>&nbsp; s�o obrigat�rios e devem ser informados.
                    </label>
                </li>                                   
                <li style="margin-left: 30px;">
                    <label for="ddlTpEnderETA" class="lblObrigatorio" title="Tipo de Restri��o">
                        Tipo Restri��o</label>
                    <asp:DropDownList ID="ddlTpRestri" ToolTip="Selecione o Tipo de Restri��o" CssClass="ddlTpRestri" runat="server">
                        <asp:ListItem Value="A">Alimentar</asp:ListItem>
                        <asp:ListItem Value="L">Alergia</asp:ListItem>
                        <asp:ListItem Value="M">M�dica</asp:ListItem>
                        <asp:ListItem Value="O">Outros</asp:ListItem>
                        <asp:ListItem Value="R">Respons�vel</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="ddlTpEnderETA"
                                        ErrorMessage="Tipo de Endere�o deve ser informado" ValidationGroup="incResAli" Text="*"
                                        Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="txtDescRestri" class="lblObrigatorio" title="Qual a restri��o?">
                        Qual a restri��o?</label>
                    <asp:TextBox ID="txtDescRestri" CssClass="txtDescRestri" MaxLength="40" runat="server" ToolTip="Digite a Restri��o Alimentar">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" ValidationGroup="incResAli"
                        runat="server" ControlToValidate="txtDescRestri" ErrorMessage="Restri��o deve ser informada"
                        Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="txtCodRestri"  title="C�digo da Restri��o">
                        C�digo</label>
                    <asp:TextBox ID="txtCodRestri" MaxLength="12" CssClass="txtCodRestri" ToolTip="Informe o C�digo da Restri��o" runat="server"></asp:TextBox>
                </li>
                <li style="margin-top: 13px;margin-left:-3px">                        
                    <a class="lnkPesqCID" href="#"><img class="btnPesqMat" src="/Library/IMG/Gestor_BtnPesquisa.png" title="Pesquisada de doen�as." alt="Icone Trocar Unidade" /></a>
                </li>   
                <li>
                    <label for="txtAcaoRestri" class="lblObrigatorio" title="A��o a ser aplicada em caso de uso?">
                        A��o a ser aplicada em caso de uso?</label>
                    <asp:TextBox ID="txtAcaoRestri" CssClass="txtAcaoRestri" MaxLength="200" runat="server" ToolTip="Digite a A��o a ser aplicada em caso de uso">
                    </asp:TextBox>
                </li>       
                <li id="li21">
                    <label for="ddlGrauRestri" class="lblObrigatorio" title="UF">
                        Grau Restri��o</label>
                    <asp:DropDownList ID="ddlGrauRestri" runat="server" CssClass="ddlGrauRestri" ToolTip="Informe o Grau de Restri��o">
                        <asp:ListItem Value="A">Alto Risco</asp:ListItem>
                        <asp:ListItem Value="M">M�dio Risco</asp:ListItem>
                        <asp:ListItem Value="B">Baixo Risco</asp:ListItem>
                        <asp:ListItem Value="N">Nenhum</asp:ListItem>
                    </asp:DropDownList>
                </li>            
                <li class="liPeriodo" style="clear:none;margin-right: 0px;margin-left: 0px;">
                    <label class="lblObrigatorio" for="txtPeriodo">
                        Per�odo de Restri��o</label>
                                                    
                    <asp:TextBox ID="txtDtIniRestri" CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator39" ValidationGroup="incResAli"
                        runat="server" ControlToValidate="txtDtIniRestri" ErrorMessage="Data Inicial deve ser informada"
                        Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                
                    <asp:Label ID="Label15" CssClass="lblDivData" runat="server"> � </asp:Label>
            
                    <asp:TextBox ID="txtDtFimRestri" CssClass="campoData" runat="server"></asp:TextBox>
                    <%--<span style="">Tipo Valor</span>--%>
            
                    <asp:CompareValidator id="CompareValidator2" runat="server" CssClass="validatorField"
                        ForeColor="Red" ValidationGroup="incCuiEspAlu"
                        ControlToValidate="txtDtFimRestri"
                        ControlToCompare="txtDtIniRestri"
                        Type="Date"       
                        Operator="GreaterThanEqual"      
                        ErrorMessage="Data Final n�o pode ser menor que Data Inicial." >
                    </asp:CompareValidator >                                        
                </li>                                                   
                <li class="liBtnsResAli">
                    <asp:LinkButton ID="lnkIncRestAli" OnClick="lnkIncRestAli_Click" ValidationGroup="incResAli" runat="server" Style="margin: 0 auto;" ToolTip="Incluir Registro">
                        <img class="imgLnkInc" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa" title="Incluir Registro" />
                        <asp:Label runat="server" ID="Label13" Text="Incluir"></asp:Label>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkExcRestAli" OnClick="lnkExcRestAli_Click" ValidationGroup="excResAli" runat="server" Style="margin: 0 auto;" ToolTip="Excluir Registro">
                        <img class="imgLnkExc" src='/Library/IMG/Gestor_BtnDel.png' alt="Icone Pesquisa" title="Excluir Registro" />
                        <asp:Label runat="server" ID="Label14" Text="Excluir"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li style="margin-left:30px;">
                    <div id="div9" runat="server" class="divGridTelETA" style="height:154px;width:710px;">
                        <asp:GridView ID="grdRestrAlim" CssClass="grdBusca" Width="695px" runat="server"
                            AutoGenerateColumns="False" DataKeyNames="ID_RESTR_ALIMEN">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Check">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ckSelect" runat="server" />
                                        <asp:HiddenField ID="hdID_RESTR_ALIMEN" runat="server" Value='<%# bind("ID_RESTR_ALIMEN") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TP_RESTR_ALIMEN" HeaderText="TP RESTRI��O">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NM_RESTR_ALIMEN" HeaderText="QUAL A RESTRIC�O"> 
                                    <ItemStyle Width="200px" />                                       
                                </asp:BoundField>
                                <asp:BoundField DataField="ID_REFER_GEDUC_RESTR_ALIMEN" HeaderText="COD">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>  
                                <asp:BoundField DataField="DE_ACAO_RESTR_ALIMEN" HeaderText="A��O A SER APLICADA"> 
                                    <ItemStyle Width="230px" />                                       
                                </asp:BoundField>                                            
                                <asp:BoundField DataField="CO_GRAU_RESTR_ALIMEN" HeaderText="GRAU REST"> 
                                    <ItemStyle Width="80px" />                                       
                                </asp:BoundField>    
                                <asp:BoundField DataField="DT_INICIO_RESTR_ALIMEN" DataFormatString="{0:dd/MM/yyyy}" HeaderText="PER�ODO"> 
                                    <ItemStyle Width="50px" />                                       
                                </asp:BoundField>   
                                <asp:BoundField DataField="DT_TERMI_RESTR_ALIMEN" DataFormatString="{0:dd/MM/yyyy}" HeaderText="RESTRI��O"> 
                                    <ItemStyle Width="50px" />                                       
                                </asp:BoundField>                            
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>   
                    
                <li id="li24" runat="server" class="liBtnAddA" style="margin-left: 355px !important;">
                    <asp:LinkButton ID="lnkResAliAlu" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkResAliAlu_Click">FINALIZAR</asp:LinkButton>
                </li>              
            </ul>
        </div>

        <div id="tabAtiExtAlu" runat="server" clientidmode="Static" style="display: none;">                  
            <ul id="ul8" class="ulDados2">
                <li style="width: 100%;text-align: center;text-transform:uppercase;margin-top:-7px;background-color: #FDF5E6;margin-bottom: 5px;">
                    <label style="font-size: 1.1em;font-family:Tahoma;">INFORMA��ES DO ALUNO - ATIVIDADES EXTRAS</label>
                </li>                   
                <li class="liNomeAluETA">
                    <label for="txtNomeAluRAA" title="Nome do Aluno">
                        NOME</label>
                    <asp:TextBox ID="txtNomeAluAEA" CssClass="campoNomePessoa" runat="server" Enabled="false" ToolTip="Nome do Aluno">
                    </asp:TextBox>
                </li>   
                <li class="liNIREAluETA">
                    <label for="txtNIREAluRAA" title="N� NIRE do Aluno">
                        N� NIRE</label>
                    <asp:TextBox ID="txtNIREAluAEA" runat="server" CssClass="txtNireAluno" Enabled="false" ToolTip="N� NIRE do Aluno">
                    </asp:TextBox>
                </li>  
                <li class="liNISAluETA">
                    <label for="txtNISAluRAA" title="N� NIS do Aluno">
                        N� NIS</label>
                    <asp:TextBox ID="txtNISAluAEA" runat="server" CssClass="txtNisAluno" Enabled="false" ToolTip="N� NIS do Aluno">
                    </asp:TextBox>
                </li>   
                <li class="G2Clear" style="width: 100%; border-bottom:1px solid #CCCCCC;margin-left: 15px;padding-bottom: 5px;margin-top: 5px;">
                    <label style="font-family: Tahoma;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Para incluir uma nova Atividade Extra do Aluno informe os dados de cada linha acima da grid de Dados
                    correspondente, ap�s clique em INCLUIR. <br /> &nbsp;&nbsp;Para excluir, marque a coluna CHECK na Grid de Dados da informa��o que
                    deseja eliminar e click no bot�o excluir para efetivar a exclus�o desejada.</label>
                </li>
                <li class="liDescTelAdd" style="width: 100%;text-align:center;margin-top: 2px;margin-bottom: 20px;">
                    <label> Os campos com <span class="asteriscoObrigatorio">*</span>&nbsp; s�o obrigat�rios e devem ser informados.
                    </label>
                </li> 
                <li style="margin-left: 130px;">
                    <label for="ddlAtivExtra" class="lblObrigatorio" title="Tipo de Restri��o">
                        Escolha a Atividade Extra</label>
                    <asp:DropDownList ID="ddlAtivExtra" AutoPostBack="true" OnSelectedIndexChanged="ddlAtivExtra_SelectedIndexChanged" ToolTip="Selecione a Atividade Extra" CssClass="ddlAtivExtra" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="ddlAtivExtra"
                                        ErrorMessage="Atividade Extra deve ser informada" ValidationGroup="incAtiExt" Text="*"
                                        Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li style="margin-left: 10px;">
                    <label for="txtSiglaAEA" title="Qual a restri��o?">
                        Sigla</label>
                    <asp:TextBox ID="txtSiglaAEA" CssClass="txtSiglaAEA" runat="server" Enabled="false">
                    </asp:TextBox>
                </li>
                <li style="margin-left: 10px;">
                    <label for="txtValorAEA"  title="C�digo da Restri��o">
                        Valor</label>
                    <asp:TextBox ID="txtValorAEA" CssClass="campoMoeda" style="width: 37px;" Enabled="false" runat="server"></asp:TextBox>
                </li>
                <li class="liPeriodo" style="clear:none;margin-right: 0px;margin-left: 13px;">
                    <label class="lblObrigatorio" for="txtPeriodo">
                        Per�odo da Atividade Extra</label>
                                                    
                    <asp:TextBox ID="txtDtIniAEA" CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator38" ValidationGroup="incAtiExt"
                        runat="server" ControlToValidate="txtDtIniAEA" ErrorMessage="Data Inicial deve ser informada"
                        Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                
                    <asp:Label ID="Label16" CssClass="lblDivData" style="margin: 0 6px;" runat="server"> � </asp:Label>
            
                    <asp:TextBox ID="txtDtFimAEA" CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator42" ValidationGroup="incAtiExt"
                        runat="server" ControlToValidate="txtDtFimAEA" ErrorMessage="Data Final deve ser informada"
                        Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>    
            
                    <asp:CompareValidator id="CompareValidator3" runat="server" CssClass="validatorField"
                        ForeColor="Red" ValidationGroup="incCuiEspAlu"
                        ControlToValidate="txtDtFimAEA"
                        ControlToCompare="txtDtIniAEA"
                        Type="Date"       
                        Operator="GreaterThanEqual"      
                        ErrorMessage="Data Final n�o pode ser menor que Data Inicial." >
                    </asp:CompareValidator >                                        
                </li> 
                <li class="liBtnsAtiExt">
                    <asp:LinkButton ID="lnkIncAtiExt" OnClick="lnkIncAtiExt_Click" ValidationGroup="incAtiExt" runat="server" Style="margin: 0 auto;" ToolTip="Incluir Registro">
                        <img class="imgLnkInc" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa" title="Incluir Registro" />
                        <asp:Label runat="server" ID="Label17" Text="Incluir"></asp:Label>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkExcAtiExt" OnClick="lnkExcAtiExt_Click" ValidationGroup="excAtiExt" runat="server" Style="margin: 0 auto;" ToolTip="Excluir Registro">
                        <img class="imgLnkExc" src='/Library/IMG/Gestor_BtnDel.png' alt="Icone Pesquisa" title="Excluir Registro" />
                        <asp:Label runat="server" ID="Label18" Text="Excluir"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li class="liGridAtv">
                    <div runat="server" class="divGridDoc" style="height:144px;">
                        <asp:GridView ID="grdAtividade" CssClass="grdBusca" Width="498px" runat="server"
                            AutoGenerateColumns="False" DataKeyNames="CO_INSC_ATIV,CO_ALU,CO_EMP,CO_ATIV_EXTRA">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Check">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ckSelect" runat="server" />
                                        <asp:HiddenField ID="hdCO_ATIV_EXTRA" runat="server" Value='<%# bind("CO_ATIV_EXTRA") %>' />
                                        <asp:HiddenField ID="hdCO_INSC_ATIV" runat="server" Value='<%# bind("CO_INSC_ATIV") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DES_ATIV_EXTRA" HeaderText="ATIVIDADE">
                                    <ItemStyle Width="220px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SIGLA_ATIV_EXTRA" HeaderText="SIGLA">
                                    <ItemStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VL_ATIV_EXTRA" DataFormatString ="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderText="VALOR">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DT_INI_ATIV" DataFormatString="{0:dd/MM/yyyy}" HeaderText="IN�CIO">
                                    <ItemStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DT_VENC_ATIV" DataFormatString="{0:dd/MM/yyyy}" HeaderText="T�RMINO">
                                    <ItemStyle Width="40px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
                <li id="li25" runat="server" class="liBtnAddA" style="margin-left: 355px !important;">
                    <asp:LinkButton ID="lnkAtiExtAlu" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkAtiExtAlu_Click">FINALIZAR</asp:LinkButton>
                </li>
            </ul>
        </div>

        <div id="tabUniMat" style="display: none;" clientidmode="Static" runat="server">
            <ul id="ul5" class="ulDados2">
                <li style="width: 100%;text-align: center;text-transform:uppercase;margin-top:-7px;background-color: #FDF5E6;margin-bottom: 5px;">
                    <label style="font-size: 1.1em;font-family:Tahoma;">INFORMA��ES DO ALUNO - MATERIAL DE APOIO / UNIFORME</label>
                </li>                   
                <li class="liNomeAluETA" style="margin-left: 17px !important;">
                    <label for="txtNomeAluDoc" title="Nome do Aluno">
                        NOME</label>
                    <asp:TextBox ID="txtNomeAluMU" CssClass="campoNomePessoa" runat="server" Enabled="false" ToolTip="Nome do Aluno">
                    </asp:TextBox>
                </li>   
                <li class="liNIREAluETA">
                    <label for="txtNIREAluDoc" title="N� NIRE do Aluno">
                        N� NIRE</label>
                    <asp:TextBox ID="txtNIREAluMU" runat="server" CssClass="txtNireAluno" Enabled="false" ToolTip="N� NIRE do Aluno">
                    </asp:TextBox>
                </li>  
                <li class="liNISAluETA">
                    <label for="txtNISAluDoc" title="N� NIS do Aluno">
                        N� NIS</label>
                    <asp:TextBox ID="txtNISAluMU" runat="server" CssClass="txtNisAluno" Enabled="false" ToolTip="N� NIS do Aluno">
                    </asp:TextBox>
                </li>   

                <li style="clear: both;">
                 <ul>                   
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                    <li class="liGridMat">
                        <div id="divSolicitacoes" title="Selecione os Itens Solicitados">
                            <asp:GridView  ID="grdSolicitacoes" CssClass="grdBusca" runat="server" style="width:100%; height: 150px;" 
                            AutoGenerateColumns="false">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hidCoTipoSolic" runat="server" Value='<%# bind("Codigo") %>' />
                                            <asp:HiddenField ID="hidTxMatric" runat="server" Value='<%# bind("txMatric") %>' />
                                            <asp:CheckBox ID="chkSelect" oncheckedchanged="chkDescPer_ChenckedChanged" Enabled='<%# bind("Inclu") %>' Checked='<%# bind("Checked") %>' runat="server" AutoPostBack="true"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Descri��o" DataField="Descricao" />     
                                    <asp:TemplateField HeaderText="R$ Unit">
                                        <ItemStyle Width="25px" HorizontalAlign="Right" />
                                        <ItemTemplate>                                
                                                <asp:Label ID="lblValor" runat="server" Text='<%# bind("Valor") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Unid">
                                        <ItemStyle Width="24px" HorizontalAlign="Center" />
                                        <ItemTemplate>                                
                                                <asp:Label  ID="lblUnidade" runat="server" Text='<%# bind("DescUnidade") %>' />                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qtd">
                                        <ItemStyle Width="24px" />
                                        <ItemTemplate>                                
                                                <asp:TextBox ID="txtQtdeSolic" CssClass="txtQtdeSolic" Text='<%# bind("Qtde") %>' AutoPostBack="true" OnTextChanged="chkDescPer_ChenckedChanged" Enabled="false" runat="server"/>                                
                                        </ItemTemplate>
                                    </asp:TemplateField>         
                                    <asp:TemplateField HeaderText="Desconto">
                                        <ItemStyle Width="70px" />
                                        <ItemTemplate>
                                                <asp:TextBox ID="txtDescSolic" ToolTip="Informe o valor de desconto do item, com ',' para separa��o de centavos" OnTextChanged="chkDescPer_ChenckedChanged" CssClass="txtDescSolic" Text='<%# bind("Desconto") %>' AutoPostBack="true" Enabled="false" runat="server"/>                                
                                                <asp:CheckBox ID="chkDescPer" OnCheckedChanged="chkDescPer_ChenckedChanged" Enabled="false" runat="server" AutoPostBack="true"/>%
                                        </ItemTemplate>
                                    </asp:TemplateField>         
                                    <asp:TemplateField HeaderText="R$ Item">
                                        <ItemStyle Width="30px" HorizontalAlign="Right" />
                                        <ItemTemplate>                                
                                                <asp:Label  ID="lblTotal" Enabled="false" Text='<%# bind("Total") %>' runat="server" />                                
                                        </ItemTemplate>
                                    </asp:TemplateField>    
                                </Columns>
                            </asp:GridView>       
                        </div>
                    </li>

                <li style="margin-top: 5px; clear: none;">
                    <asp:CheckBox ID="ckbAtualizaFinancSolic" CssClass="chkIsento" Enabled="true" Checked="true" runat="server" Text="Atualiza Financeiro" ToolTip="Selecione se atualizar� o financeiro" AutoPostBack="True" 
                            oncheckedchanged="ckbAtualizaFinancSolic_CheckedChanged"/>
                </li>


                <li style="margin-top: 5px; margin-left: 3px; clear: none;">            
                    <asp:CheckBox ID="chkConsolValorTitul" CssClass="chkIsento" Enabled="false" runat="server" Text="Valores �nico T�tulo" ToolTip="Selecione se consolidar� os valores em um �nico t�tulo financeiro" AutoPostBack="True" 
                            oncheckedchanged="chkConsolValorTitul_CheckedChanged"/>
                </li>


                    <li style="clear: none; margin-left: 5px; margin-top: 5px;">
                        <asp:HiddenField ID="hidValorTotal" runat="server" Value="" />
                        <label for="txtValorTotal" title="Valor Total da Solicita��o (R$)">Total R$</label>
                        <asp:TextBox ID="txtValorTotal" Width="50" CssClass="txtDesctoMensa" Enabled="false" runat="server"></asp:TextBox>
                    </li>                                                      


                    <li style="clear:none; margin-left: 5px; margin-top: 6px;">
                        <label for="txtQtdParcelas" title="Informe em quantas parcelas ser� feito o parcelamento" >QP</label>
                        <asp:TextBox ID="txtQtdParcelas" Width="15px" runat="server" ToolTip="Informe em quantas parcelas ser� feito o parcelamento"></asp:TextBox>
                    </li>

                <li style="clear: none; margin-left: 8px; margin-top: 5px;">
                    <label for="txtDtVectoSolic" title="Data de Vencimento">Data 1� Parcela</label>
                    <asp:TextBox ToolTip="Informe a Data de Vencimento da Solicita��o" ID="txtDtVectoSolic" CssClass="txtDtVectoSolic campoData" runat="server"></asp:TextBox>
                </li>

                <li style="clear: none; margin-left: 5px; margin-top: 5px;">
					<label for="ddlDiaVectoParcMater" title="Selecione o melhor Dia de Vencimento das Parcelas">
						Dia</label>
					<asp:DropDownList ID="ddlDiaVectoParcMater" Width="35px" ToolTip="Selecione o melhor Dia de Vencimento das Parcelas" runat="server">
								
					</asp:DropDownList>
				</li>

				<div style="float: left; margin-left: 5px;">
					<ul>                                
						<asp:UpdatePanel ID="UpdatePanel11" runat="server">
						<ContentTemplate>
							<li style="margin-bottom: -2px;">
								<label title="Desconto nas Parcelas: Mensal - Permite informar a quantidade de meses e o m�s inicial de desconto, distribuindo o valor entre as parcelas; Total - Distribui o valor de desconto entre as parcelas." style="color: Red;">
									Desconto sobre o valor total</label>
							</li>
							<li class="liClear">
								<label for="ddlTipoDesctoParc" title="Tipo de desconto da mensalidade">
									Tipo Desconto</label>
								<asp:DropDownList ID="ddlTipoDesctoParc" OnSelectedIndexChanged="ddlTipoDesctoParc_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecione o Tipo de Desconto" CssClass="ddlTipoDesctoMensa" runat="server">
									<asp:ListItem Selected="true" Text="Total" Value="T"></asp:ListItem>
									<asp:ListItem Text="Mensal" Value="M"></asp:ListItem>
								</asp:DropDownList>
							</li>
							<li style="margin-left: 10px;">
								<label for="txtQtdeMesesDesctoParc" title="Quantidade de meses de desconto">
									Qt Meses</label>
								<asp:TextBox ID="txtQtdeMesesDesctoParc" ToolTip="Informa a quantidade de meses de desconto" CssClass="txtQtdeMesesDesctoMensa" runat="server" Enabled="false">
								</asp:TextBox>
							</li>
							<li style="margin-left: 10px;">
								<label for="txtDesctoMensaParc" title="R$ Desconto">
									R$ Desconto</label>
								<asp:TextBox ID="txtDesctoMensaParc" Width="50" CssClass="txtDesctoMensa" runat="server">
								</asp:TextBox>
							</li>
							<li style="margin-left: 6px;">
								<label for="txtMesIniDescontoParc" title="Parcela de in�cio do desconto">
									PID</label>
								<asp:TextBox ID="txtMesIniDescontoParc" Enabled="false" Width="20px" ToolTip="Parcela de in�cio do desconto" CssClass="txtMesIniDesconto" style="text-align: right;" runat="server">
								</asp:TextBox>
							</li>
						</ContentTemplate>
						</asp:UpdatePanel>                                
					</ul>
				</div>

                <li style="clear: none; margin-left: 5px;">
                    <label for="ddlBoletoSolic" title="Boleto Banc�rio - Deve estar cadastrado para Material de Apoio/Uniforme em dados de boleto">Boleto Banc�rio</label>
                    <asp:DropDownList ID="ddlBoletoSolic" runat="server" Width="180px"
                        ToolTip="Selecione o Boleto Banc�rio">
                    </asp:DropDownList>
				</li>



                    <li style="clear: none; margin-left: 5px; margin-top: 1px;">
                        <label for="txtDtPrevisao" title="Previs�o de Entrega">Previs�o Entrega</label>
                        <asp:TextBox ToolTip="Informe a Previs�o de Entrega" ID="txtDtPrevisao" CssClass="txtDtVectoSolic campoData" runat="server"></asp:TextBox>
                    </li>



				<li runat="server" id="liBtnGrdFinanMater" class="liBtnGrdFinan" style="margin-top: 7px; margin-right: 28px; float: right;">
					<asp:LinkButton ID="lnkMontaGridParcMater" OnClick="lnkMontaGridParcMaterial_Click" ValidationGroup="vgMontaGridMensa" runat="server" Style="margin: 0 auto;" ToolTip="Montar Grid com as parcelas.">                                        
						<asp:Label runat="server" ID="Label166" ForeColor="GhostWhite" Text="GERAR GRID FINANCEIRA"></asp:Label>
					</asp:LinkButton>
				</li>                
  
                <li class="labelInLine" style="width: 685px; margin-left: 17px;margin-top: 2px">
                    <div id="divMateriaisAluno" runat="server" style="height: 150px; border: 1px solid #CCCCCC; overflow-y: auto; margin-top: 10px;">
                    <asp:GridView runat="server" ID="grdParcelasMaterial" CssClass="grdBusca" ShowHeader="true" ShowHeaderWhenEmpty="true" ToolTip="Grid demonstrativa das parcelas de materiais coletivos do aluno." AutoGenerateColumns="False" Width="100%">
                        <RowStyle CssClass="rowStyle" />
                        <HeaderStyle CssClass="th" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="NU_DOC" HeaderText="N� Docto">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NU_PAR" HeaderText="N� Par">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dtVencimento" HeaderText="Dt Vencto" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valorParcela" DataFormatString="{0:N2}" HeaderText="R$ Mensal">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valorDescto" DataFormatString="{0:N2}" HeaderText="R$ Descto">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valorLiquido" DataFormatString="{0:N2}" HeaderText="R$ Liquido">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valorMulta" DataFormatString="{0:N2}" HeaderText="% Multa">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valorJuros" DataFormatString="{0:N2}" HeaderText="% Jur/Dia">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>                        
                    </asp:GridView>
                    </div>
                </li>                                           




                    <!--li style="margin-left: 170px;margin-bottom: 0px; clear: both;">
                        <label for="ddlHistorico" title="Hist�rico">Hist�rico Financeiro</label>
                        <asp:DropDownList ID="ddlHistorico" Enabled="false" Width="240px" runat="server" ToolTip="Selecione o Hist�rico Financeiro"></asp:DropDownList>
                    </li-->        
                    <!--li style="clear: none; margin-left: 5px;">
                        <label for="ddlAgrupador" title="Agrupador de Receita">Agrupador de Receita</label>
                        <asp:DropDownList ID="ddlAgrupador" Enabled="false" CssClass="ddlAgrupador" Width="180px" runat="server" ToolTip="Selecione o Agrupador de Receita"/>
                    </li-->
                    <!--li style="clear: both; width: 460px; margin-bottom: 2px;margin-left: 170px; margin-top: 5px;">
                        <label style="font-size: 1.1em;" title="Classifica��o Cont�bil">
                            Classifica��o Cont�bil
                        </label>
                    </li>
                    <li style="clear: both;margin-left: 170px;">
                        <ul>
                            <li class="liNomeContaContabil" style="clear: both;">
                                <label style="font-size: 1.2em; margin-top: 10px; width: 55px;" title="Conta Cont�bil Ativo">
                                    Cta Ativo</label>
                            </li>
                            <li class="liNomeContaContabil">
                                <label id="Label19" for="ddlTipoContaA" title="Tipo de Conta Cont�bil" runat="server">
                                    Tp</label>
                                <asp:DropDownList ID="ddlTipoContaA" Enabled="false" CssClass="ddlTipoConta" Width="30px" runat="server"
                                    ToolTip="Selecione o Tipo de Conta Cont�bil" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoContaA_SelectedIndexChanged">
                                    <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                                    <asp:ListItem Value="C">3 - Receita</asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li class="liNomeContaContabil">
                                <label for="ddlGrupoContaA" title="Grupo de Conta Cont�bil">
                                    Grp</label>
                                <asp:DropDownList ID="ddlGrupoContaA" Enabled="false" CssClass="ddlGrupoConta" Width="35px" runat="server"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlGrupoContaA_SelectedIndexChanged">
                                </asp:DropDownList>
                            </li>
                            <li class="liNomeContaContabil">
                                <label for="ddlSubGrupoContaA" title="SubGrupo de Conta Cont�bil">
                                    SGrp</label>
                                <asp:DropDownList ID="ddlSubGrupoContaA" Enabled="false" CssClass="ddlSubGrupoConta" Width="40px"
                                    runat="server" ToolTip="Selecione o SubGrupo de Conta Cont�bil" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSubGrupoContaA_SelectedIndexChanged">
                                </asp:DropDownList>
                            </li>
                            <li class="liNomeContaContabil">
                                <label for="ddlContaContabilA"  title="Conta Cont�bil">
                                    Conta Cont�bil</label>
                                <asp:DropDownList ID="ddlContaContabilA" Enabled="false" CssClass="ddlContaContabil" runat="server"
                                    ToolTip="Selecione a Conta Cont�bil" Width="200px">
                                </asp:DropDownList>
                            </li>
                        </ul>
                    </li-->
                    <!--li style="clear: both;margin-left: 170px; margin-top: 4px;">
                        <ul style="margin-top: -3px;">
                            <li class="liNomeContaContabil" style="clear: both;">
                                <label style="font-size: 1.2em; margin-top: 1px; width: 55px;" title="Conta Cont�bil Ativo">
                                    Cta Banco</label>
                            </li>
                            <li class="liNomeContaContabil">
                                <asp:DropDownList ID="ddlTipoContaB" Enabled="false" CssClass="ddlTipoConta" Width="30px" runat="server"
                                    ToolTip="Selecione o Tipo de Conta Cont�bil" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoContaB_SelectedIndexChanged">
                                    <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                                    <asp:ListItem Value="C">3 - Receita</asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li class="liNomeContaContabil">
                                <asp:DropDownList ID="ddlGrupoContaB" Enabled="false" CssClass="ddlGrupoConta" Width="35px" runat="server"
                                    ToolTip="Selecione o Grupo de Conta Cont�bil" AutoPostBack="true" OnSelectedIndexChanged="ddlGrupoContaB_SelectedIndexChanged">
                                </asp:DropDownList>
                            </li>
                            <li class="liNomeContaContabil">
                                <asp:DropDownList ID="ddlSubGrupoContaB" Enabled="false" CssClass="ddlSubGrupoConta" Width="40px"
                                    runat="server" ToolTip="Selecione o SubGrupo de Conta Cont�bil" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSubGrupoContaB_SelectedIndexChanged">
                                </asp:DropDownList>
                            </li>
                            <li class="liNomeContaContabil">
                                <asp:DropDownList ID="ddlContaContabilB" Enabled="false" CssClass="ddlContaContabil" runat="server"
                                    ToolTip="Selecione a Conta Cont�bil" Width="200px">
                                </asp:DropDownList>
                            </li>
                        </ul>
                    </li-->
                    <!--li style="clear: both;margin-left: 170px; margin-top: 4px;">
                        <ul style="margin-top: -3px;">
                            <li class="liNomeContaContabil" style="clear: both;">
                                <label style="font-size: 1.2em; margin-top: 1px; width: 55px;" title="Conta Cont�bil Ativo">
                                    Cta Caixa</label>
                            </li>
                            <li class="liNomeContaContabil">
                                <asp:DropDownList ID="ddlTipoContaC" Enabled="false" CssClass="ddlTipoConta" Width="30px" runat="server"
                                    ToolTip="Selecione o Tipo de Conta Cont�bil" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoContaC_SelectedIndexChanged">
                                    <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                                    <asp:ListItem Value="C">3 - Receita</asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li class="liNomeContaContabil">
                                <asp:DropDownList ID="ddlGrupoContaC" Enabled="false" CssClass="ddlGrupoConta" Width="35px" runat="server"
                                    ToolTip="Selecione o Grupo de Conta Cont�bil" AutoPostBack="true" OnSelectedIndexChanged="ddlGrupoContaC_SelectedIndexChanged">
                                </asp:DropDownList>
                            </li>
                            <li class="liNomeContaContabil">
                                <asp:DropDownList ID="ddlSubGrupoContaC" Enabled="false" CssClass="ddlSubGrupoConta" Width="40px"
                                    runat="server" ToolTip="Selecione o SubGrupo de Conta Cont�bil" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSubGrupoContaC_SelectedIndexChanged">
                                </asp:DropDownList>
                            </li>
                            <li class="liNomeContaContabil">
                                <asp:DropDownList ID="ddlContaContabilC" Enabled="false" CssClass="ddlContaContabil" runat="server"
                                    ToolTip="Selecione a Conta Cont�bil" Width="200px">
                                </asp:DropDownList>
                            </li>
                        </ul>
                    </li-->
                    </ContentTemplate>
                    </asp:UpdatePanel>
                 </ul>
               </li> 
                <li id="li9" runat="server" class="liBtnAddA" style="margin-left: 190px !important;margin-top: 6px; background-color: #ffffe0 !important; color: #000 !important;  clear: none !important;">
                    <asp:LinkButton ID="lnkGrava" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkGrava_Click">EFETIVAR E ATUALIZAR FINANCEIRO</asp:LinkButton>
                </li>                           

                <li id="li10" runat="server" class="liBtnAddA" style="margin-left: 5px !important;margin-top: 6px; background-color: #e0ffff !important; clear: none !important;">
                    <asp:LinkButton ID="lnkBoletoMater" runat="server" ValidationGroup="atuEndAlu" Enabled="false" OnClick="lnkBoletoMater_Click">
                        <img id="img4" runat="server" width="12" height="12" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico' alt="Icone Pesquisa" title="Imprimir Boleto de Material de Apoio / Uniforme" />
                        BOLETO
                    </asp:LinkButton>
                </li>                           

                <li id="li13" runat="server" class="liBtnAddA" style="margin-left: 5px !important;margin-top: 6px; background-color: #e0ffff !important; clear: none !important;">
                    <asp:LinkButton ID="LinkButton3" Enabled="false" runat="server" ValidationGroup="atuEndAlu">
                        <img id="img5" runat="server" width="12" height="12" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico' alt="Icone Pesquisa" title="Imprimir Extrato de Material de Apoio / Uniforme" />
                        EXTRATO
                    </asp:LinkButton>
                </li>                           

                <li id="li14" runat="server" class="liBtnAddA" style="margin-left: 131px !important;margin-top: 6px; clear: none !important;">
                    <asp:LinkButton ID="lnkMatUniAlu" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkMatUniAlu_Click">FINALIZAR</asp:LinkButton>
                </li>                           
            </ul>
        </div>

<!-- =========================================================================================================================================================================== -->
<!--                                                               TABELA DE DOCUMENTOS DE MATR�CULA                                                                             -->
<!-- =========================================================================================================================================================================== -->
        <div id="tabDocumentos" style="display: none;" clientidmode="Static" runat="server">
            <ul id="ul3" class="ulDados2">
                <li style="width: 100%;text-align: center;text-transform:uppercase;margin-top:-7px;background-color: #FDF5E6;margin-bottom: 5px;">
                    <label style="font-size: 1.1em;font-family:Tahoma;">INFORMA��ES DO ALUNO - DOCUMENTOS DE MATR�CULA</label>
                </li>                   
                <li class="liNomeAluETA">
                    <label for="txtNomeAluDoc" title="Nome do Aluno">
                        NOME</label>
                    <asp:TextBox ID="txtNomeAluDoc" CssClass="campoNomePessoa" runat="server" Enabled="false" ToolTip="Nome do Aluno">
                    </asp:TextBox>
                </li>   
                <li class="liNIREAluETA">
                    <label for="txtNIREAluDoc" title="N� NIRE do Aluno">
                        N� NIRE</label>
                    <asp:TextBox ID="txtNIREAluDoc" runat="server" CssClass="txtNireAluno" Enabled="false" ToolTip="N� NIRE do Aluno">
                    </asp:TextBox>
                </li>  
                <li class="liNISAluETA">
                    <label for="txtNISAluDoc" title="N� NIS do Aluno">
                        N� NIS</label>
                    <asp:TextBox ID="txtNISAluDoc" runat="server" CssClass="txtNisAluno" Enabled="false" ToolTip="N� NIS do Aluno">
                    </asp:TextBox>
                </li>   
                <li class="G2Clear" style="width: 100%; border-bottom:1px solid #CCCCCC;margin-left: 15px;padding-bottom: 5px;margin-top: 5px;">
                    <label style="font-family: Tahoma;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Para registrar a ENTREGA DE DOCUMENTOS do Aluno necess�rios para a efetiva��o de sua Matr�cula,
                    marque na coluna CHECK na Grid de Dados <br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;os documentos entregues e pressione o bot�o atulizar a base de dados.</label>
                </li>
                <li class="liDescTelAdd" style="width: 100%;text-align:center;margin-top: 2px;margin-bottom: 20px;">
                    <label> Os campos com <span class="asteriscoObrigatorio">*</span>&nbsp; s�o obrigat�rios e devem ser informados.
                    </label>
                </li>   
                <li style="margin-left: 190px;">
                    <div id="divGrid" runat="server" class="divGridDoc" style="height:195px;">
                        <asp:GridView ID="grdDocumentos" CssClass="grdBusca" Width="350px" runat="server"
                            AutoGenerateColumns="False">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Check">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <%--<asp:CheckBox ID="ckSelect" Checked='<%# bind("chkSel") %>' runat="server" />--%>
                                        <asp:CheckBox ID="ckSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DE_TP_DOC_MAT" HeaderText="Documento">
                                    <ItemStyle Width="180px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Sigla">
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSigla" runat="server" Text='<%# bind("SIG_TP_DOC_MAT") %>'></asp:Label>
                                        <asp:HiddenField ID="hdCoTpDoc" runat="server" Value='<%# bind("CO_TP_DOC_MAT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Entrega">
                                    <ItemStyle Width="40px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
                
                <li id="Li26" runat="server" class="liBtnAddA" style="margin-left: 342px !important;">
                    <asp:LinkButton ID="lnkAtuDoctos" runat="server" ValidationGroup="atuDocAlu" OnClick="lnkAtuDoctos_Click">FINALIZAR</asp:LinkButton>
                </li>                    
            </ul>
        </div>
<!-- =========================================================================================================================================================================== -->
<!--                                                               TABELA DE DOCUMENTOS DE MATR�CULA                                                                             -->
<!-- =========================================================================================================================================================================== -->

<!-- =========================================================================================================================================================================== -->
<!--                                                               TABELA DE MENSALIDADES                                                                                        -->
<!-- =========================================================================================================================================================================== -->
        <div id="tabMenEsc" style="display: none;" clientidmode="Static" runat="server">
            <ul id="ul9" class="ulDados2">
                <li style="width: 100%;text-align: center;text-transform:uppercase;margin-top:-7px;background-color: #FDF5E6;margin-bottom: 5px;">
                    <label style="font-size: 1.1em;font-family:Tahoma;">INFORMA��ES DO ALUNO - MENSALIDADES ESCOLARES</label>
                </li>                   
                <li class="liNomeAluETA" style="margin-left: 33px;">
                    <label for="txtNomeAluDoc" title="Nome do Aluno">
                        NOME</label>
                    <asp:TextBox ID="txtNomeAluME" CssClass="campoNomePessoa" style="Width:300px !important;" runat="server" Enabled="false" ToolTip="Nome do Aluno">
                    </asp:TextBox>
                </li>   
                <li class="liNIREAluETA">
                    <label for="txtNIREAluDoc" title="N� NIRE do Aluno">
                        N� NIRE</label>
                    <asp:TextBox ID="txtNIREAluME" runat="server" Width="56px" CssClass="txtNireAluno" Enabled="false" ToolTip="N� NIRE do Aluno">
                    </asp:TextBox>
                </li>  
                <li class="liNISAluETA">
                    <label for="txtNISAluDoc" title="N� NIS do Aluno">
                        N� NIS</label>
                    <asp:TextBox ID="txtNISAluME" runat="server" CssClass="txtNisAluno" Enabled="false" ToolTip="N� NIS do Aluno">
                    </asp:TextBox>
                </li>   
                <li style="margin-top: 12px; margin-left: 100px;">
                    <asp:CheckBox ID="chkAtualiFinan" CssClass="chkLocais" AutoPostBack="true" OnCheckedChanged="chkAtualiFinan_CheckedChanged" Checked="true" runat="server" Text="Atualizar Financeiro" ToolTip="Marque se dever� atualizar o financeiro"/>
                    <br />
                </li>
                <!-- Este c�digo foi retirado para adequa��o do c�digo para a altera��o do desconto - Victor Martins Machado - 27/02/2013 -->
                <!--li class="G2Clear" style="width: 100%; border-bottom:1px solid #CCCCCC;margin-left: 15px;padding-bottom: 2px;text-align:center;">
                    <label style="font-family: Tahoma;">
                    Demonstrativo das Mensalidades Escolares no ano letivo.</label>
                </li-->      
                <li>                    
                    <ul>
                        <li style="margin-left: 33px; margin-top: 10px; width: 100%;">
                            <ul>                                
                                <li>
                                    <div style="float: left; border-right:2px solid #CCCCCC; width: 337px; margin-left: -5px;">
                                        <!-- Checkbox do tipo de contrato -->
                                        <div>
                                            <asp:CheckBox ID="chkTipoContrato" OnCheckedChanged="chkTipoContrato_CheckedChange" Checked="false" CssClass="chkLocais" runat="server" Text="Qual o tipo de valor de Contrato?"
                                            ToolTip="Marque se dever� utilizar um tipo de contrato diferente." AutoPostBack="true" />
                                            
                                            <%--<span style="">Tipo Valor</span>--%>
                                            <asp:DropDownList ID="ddlTipoValorCurso" runat="server" AutoPostBack="True" 
                                                Enabled="False" 
                                                onselectedindexchanged="ddlTipoValorCurso_SelectedIndexChanged" 
                                                ToolTip="Selecione o tipo de contrato">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlTipoContrato" OnSelectedIndexChanged="ddlTipoContrato_SelectedIndexChanged" AutoPostBack="true" Width="60px" Enabled="false"
                                                ToolTip="Selecione o tipo de pagamento" runat="server" >
                                            </asp:DropDownList>
                                            
                                            
                                        </div>

                                        <!-- Checkbox de altera��o do valor de contrato -->
                                        <div>
                                            <asp:CheckBox ID="chkAlterValorContr" CssClass="chkLocais" runat="server" Text="Altera o valor de contrato?" ToolTip="Marque se dever� gerar o total de parcelas do curso independente do ano."
                                             OnCheckedChanged="chkAlterValorContr_CheckedChanged" AutoPostBack="true"/>

                                            <asp:TextBox Enabled="false" ID="txtValorContratoCalc" OnTextChanged="txtValorContratoCalc_TextChanged" AutoPostBack="true" CssClass="txtValorContratoCalc" style="text-align: right; margin-left: 4px;" runat="server" Width="50px" />

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="vgMontaGridMensa"
                                            runat="server" ControlToValidate="txtValorContratoCalc" ErrorMessage="Valor de Contrato deve ser informado"
                                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                        </div>

                                        <!-- Checkbox do valor do contrato proporcional -->
                                        <div>
                                             <asp:CheckBox ID="chkValorContratoCalc" Checked="false" runat="server" CssClass="chkLocais" OnCheckedChanged="chkValorContratoCalc_CheckedChanged" ToolTip="Marque se o sistema dever� calcular o valor do contrato." Text="Calcular valor de contrato?"
                                              AutoPostBack="true" />                                            

                                            <asp:DropDownList ID="ddlValorContratoCalc" Width="125px" Enabled="false" style="margin-left: 4px;"
                                                ToolTip="Selecione o Nome da Bolsa" runat="server" >
                                                <asp:ListItem Value="P">Proporcional Meses</asp:ListItem>
                                                <asp:ListItem Value="T" Selected="true">Total (Todos os meses)</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <!-- Checkbox de gera��o do total de parcelas -->
                                        <div>
                                            <asp:CheckBox ID="chkGeraTotalParce" CssClass="chkLocais" runat="server" Text="Altera o n� original de parcelas de cadastro?" ToolTip="Marque se dever� alterar o n� original de parcelas cadastrado na s�rie."
                                             OnCheckedChanged="chkGeraTotalParce_CheckedChanged" AutoPostBack="true"/>

                                            <asp:TextBox ID="txtQtdeParcelas" OnTextChanged="txtQtdeParcelas_TextChanged" AutoPostBack="false" ToolTip="Informa a quantidade de parcelas da s�rie/curso" Width="15px" CssClass="txtQtdeMesesDesctoMensa" runat="server" Enabled="false">
                                            </asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="vgMontaGridMensa"
                                            runat="server" ControlToValidate="txtQtdeParcelas" ErrorMessage="Quantidade de parcelas da s�rie/curso deve ser informada"
                                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                        </div>                                        

                                        <!-- Checkbox da primeira parcela -->
                                        <div>
                                            <asp:CheckBox ID="chkDataPrimeiraParcela" Checked="false" CssClass="chkLocais"
                                             runat="server" Text="Altera data/valor 1� parcela?" OnCheckedChanged="chkDataPrimeiraParcela_CheckedChange"
                                             ToolTip="Marque se dever� informar a data da primeira parcela."
                                             AutoPostBack="true"/>

                                            <asp:TextBox ID="txtDtPrimeiraParcela" ToolTip="Informa a data de pagamento da primeira parcela." 
                                             CssClass="txtPeriodoIniDesconto campoData" runat="server" Enabled="false">
                                            </asp:TextBox>

                                            <span> / R$</span>

                                            <asp:TextBox ID="txtValorPrimParce" CssClass="txtValorPrimParce" Width="48px" style="text-align: right;" ToolTip="Informe o valor da primeira parcela" runat="server" Enabled="false" >
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: right; margin-left: 6px;">
                                        <!-- Checkbox do desconto por bolsa -->
                                        <div>
                                            <div>
                                                <asp:CheckBox ID="chkManterDesconto" Checked="false" CssClass="chkLocais" runat="server" Text="Altera Bolsa/Conv�nio " ToolTip="Marque se dever� manter desconto de Bolsa/Conv�nio"
                                                OnCheckedChanged="chkManterDesconto_CheckedChanged" AutoPostBack="true"/>

                                                <asp:DropDownList ID="ddlTpBolsaAlt" Width="75px" Enabled="false" OnSelectedIndexChanged="ddlTpBolsaAlt_SelectedIndexChanged"
                                                    ToolTip="Selecione o Nome da Bolsa" AutoPostBack="True"  runat="server" >
                                                <asp:ListItem Value="N" Selected="True">Nenhuma</asp:ListItem>
                                                    <asp:ListItem Value="B">Bolsa</asp:ListItem>
                                                    <asp:ListItem Value="C">Conv�nio</asp:ListItem>
                                                </asp:DropDownList>

                                                <span>/</span>

                                                <asp:DropDownList ID="ddlBolsaAlunoAlt" Enabled="false" CssClass="ddlBolsaAluno" Width="135px" ToolTip="Selecione o nome da Bolsa"
                                                    runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBolsaAlunoAlt_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <br />
                                            </div>
                                            <div style="margin-top: -1px; margin-left: 6px;">
                                                <span>R$/% m�s</span>

                                                <asp:TextBox ID="txtValorDescto" CssClass="txtDescontoAluno" Width="50px" ToolTip="Informe o valor do Desconto" runat="server" Enabled="true" >
                                                </asp:TextBox>

                                                <asp:CheckBox CssClass="chkLocais" style="margin-left: -2px;" ID="chkManterDescontoPerc" TextAlign="Right" 
                                                Enabled="false" runat="server" ToolTip="% de Desconto da Bolsa?" Text="%" /> 

                                                <span title="Per�odo de Dura��o do Per�odo do desconto" style="margin-left: 3px;">
                                                    Per�odo</span>
                                                <asp:TextBox ID="txtPeriodoIniDesconto" style="clear: both;" Enabled="false" ToolTip="Informe a Data do Per�odo do desconto" runat="server"
                                                    CssClass="txtPeriodoIniDesconto campoData"></asp:TextBox>

                                                <span>�</span>

                                                <asp:TextBox ID="txtPeriodoFimDesconto" Enabled="false" ToolTip="Informe a Data de T�rmino do Per�odo do desconto" runat="server"
                                                    CssClass="txtPeriodoIniDesconto campoData"></asp:TextBox>
                                            </div>
                                        </div>

                                        <!-- Div de desconto especial -->
                                        <div style="margin-left: 6px; margin-top: 10px;">
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
                                                    <label for="txtMesIniDesconto" title="Parcela de in�cio do desconto">
                                                        PID</label>
                                                    <asp:TextBox ID="txtMesIniDesconto" Enabled="false" Width="20px" ToolTip="Parcela de in�cio do desconto" CssClass="txtMesIniDesconto" style="text-align: right;" runat="server">
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
                        <li class="liClear" style="margin-top: 0px; margin-left: 33px;">
                            <div>
                                <div style="float: left;">
                                    <label for="ddlBoleto" title="Boleto Banc�rio">Boleto</label>
                                    <asp:DropDownList ID="ddlBoleto" runat="server" CssClass="ddlBoleto" style="clear: both;"
                                        ToolTip="Selecione o Boleto Banc�rio">
                                    </asp:DropDownList>
                                </div>

                                <div style="float: right; margin-left: 5px;">
                                    <label for="ddlTipoDesctoMensa" title="Dia de vencimento">
                                        Dia</label>
                                    <asp:DropDownList ID="ddlDiaVecto" ToolTip="Selecione o Dia de Vencimento da Mensalidade" CssClass="ddlDiaVecto" runat="server">
                                        
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </li>
                        <li runat="server" id="liBtnGrdFinan" class="liBtnGrdFinan" style="margin-left: 318px; margin-top: 7px; margin-right: 10px;">
                            <asp:LinkButton ID="lnkMontaGridMensa" OnClick="lnkMontaGridMensa_Click" ValidationGroup="vgMontaGridMensa" runat="server" Style="margin: 0 auto;" ToolTip="Montar Grid Financeira">                                        
                                <asp:Label runat="server" ID="Label6" ForeColor="GhostWhite" Text="GERAR GRID FINANCEIRA"></asp:Label>
                            </asp:LinkButton>
                        </li>                  
                    </ul>                                        
                </li>                                                             
                <li class="labelInLine" style="width: 681px;margin-left: 33px;margin-top:-3px">
                    <div id="divMensaAluno" runat="server" style="height: 200px; border: 1px solid #CCCCCC; overflow-y: auto; margin-top: 10px;">
                    <asp:GridView runat="server" ID="grdNegociacao" CssClass="grdBusca" ToolTip="Grid demonstrativa das mensalidades do aluno." AutoGenerateColumns="False" Width="100%">
                        <RowStyle CssClass="rowStyle" />
                        <HeaderStyle CssClass="th" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="NU_DOC" HeaderText="N� Docto">
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="NU_PAR" HeaderText="N� Par">
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
                <li style="clear:both;margin-top: 2px; margin-left: 33px;">
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
                    <label title="Valor Total Total L�quido do Contrato" style="margin-right:5px !important;">R$ L�quido Contr</label>          
                    <asp:TextBox ID="txtTotalLiquiContra" CssClass="txtValor campoMoeda" style="width: 75px;" runat="server" ToolTip="Valor Total Total L�quido do Contrato" Enabled="false"></asp:TextBox>
                </li>
                <li class="liBtnAddA" style="margin-left: 185px !important; margin-top: 10px; clear: none;">
                    <asp:LinkButton ID="lnkMenAlu" runat="server" ValidationGroup="atuMensaAlu" OnClick="lnkMenAlu_Click">FINALIZAR MENSALIDADE</asp:LinkButton>
                </li>                  
            </ul>
        </div>
<!-- =========================================================================================================================================================================== -->
<!--                                                               TABELA DE MENSALIDADES                                                                                        -->
<!-- =========================================================================================================================================================================== -->

        <div id="divLoadShowReservas" style="display: none;">
        </div>
        <div id="divLoadShowAlunos" style="display: none;height:305px !important;"/>
        <div id="divLoadShowResponsaveis" style="display: none;height:315px !important;"/>     
        <div id="divLoadShowDoencas" style="display: none;height:325px !important;"/>
        </li>           
    </ul>
    <script type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".txtNumeroResp").mask("?99999");
            $(".campoCep").mask("99999-999");
            $(".txtCepResp").mask("99999-999");
            $(".txtNumeroAluno").mask("?99999");
            $(".txtDescontoAluno").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtValorPrimParce").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $("input.txtPeriodoDeIniBolAluno").datepicker();
            $(".txtPeriodoDeIniBolAluno").mask("99/99/9999");
            $("input.txtDtVectoSolic").datepicker();
            $(".txtDtVectoSolic").mask("99/99/9999");
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtValorContratoCalc").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtQtdeMesesDesctoMensa").mask("?99");
            $(".txtDesctoMensa").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtHrAplic").mask("99:99");
            $(".txtAno").mask("9999");
            $(".txtQtdeSolic").mask("?99");
            $(".txtMesIniDesconto").mask("?99");
        });

        $(document).ready(function () {
            $("input.txtPeriodoDeIniBolAluno").datepicker();
            $(".txtPeriodoDeIniBolAluno").mask("99/99/9999");
            $("input.txtDtVectoSolic").datepicker();
            $(".txtDtVectoSolic").mask("99/99/9999");
            $(".txtValorContratoCalc").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtDesctoMensa").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtValorPrimParce").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtQtdeMesesDesctoMensa").mask("?99");
            $(".txtHrAplic").mask("99:99");
            $(".txtCartaoSaude").mask("?9999999999999999");
            $(".txtNire").mask("?999999999");
            $(".txtNireAluno").mask("?999999999");
            $(".txtNumeroResp").mask("?99999");
            $(".txtNumeroAluno").mask("?99999");
            $(".txtMesAnoTrabResp").mask("99/9999");
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".campoCep").mask("99999-999");
            $(".campoTelefone").mask("(99)9999-9999");
            $(".campoCpf").mask("999.999.999-99");
            $(".txtCepResp").mask("99999-999");
            $(".txtNISResp").mask("?999999999999999");
            $(".txtNisAluno").mask("?999999999999999");
            $(".txtQtdeCEA").mask("?9999");
            $(".txtAno").mask("9999");
            $(".txtQtdeMatEsc").mask("?999");
            $(".txtTelResidencialResp").mask("(99) 9999-9999");
            $(".txtTelCelularResp").mask("(99) 9999-9999");
            $(".txtTelEmpresaResp").mask("(99) 9999-9999");
            $(".txtCPF").mask("999.999.999-99");
            $(".txtNumReserva").mask("9999.99.9999");
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtQtdMenoresResp").mask("?99");
            $(".txtNumeroEmp").mask("?99999");
            $(".txtPassaporteResp").mask("?999999999");
            $(".txtQtdeSolic").mask("?99");
            $(".txtMesIniDesconto").mask("?99");


            $('.liListaEndereco').click(function () {
                var strEndereco = $('.txtLogradouroAluno').val() + "";
                strEndereco = strEndereco.replace(/ /g, "*");
                $('#divAddTipo').dialog({ autoopen: false, modal: true, width: 430, height: 390, resizable: false,
                    open: function () { $('#divAddTipo').load("/Componentes/ListarCEPsEndereco.aspx?strEndereco=" + strEndereco); }
                });
            });

            if (($(".lblSucInfResp").is(":visible")) && ($(".lblSucInfAlu").is(":visible") == false)) {
                $("#tabAluno").removeAttr("style");
                $("#tabResp").hide();
                $("#tabAluno").show();
            }

            if (($(".lblSucInfAlu").is(":visible"))) {
                $("#tabAluno").hide();
            }

            $(".lnkPesRes").click(function () {
                $("#divLoadShowReservas").load("../../../../../Componentes/ListarReservasMat.aspx", function () {
                    $("#divLoadShowReservas #frmListarReservasMat").attr("action", "../../../../../Componentes/ListarReservasMat.aspx");
                });

                $("#divLoadShowReservas").dialog({ title: "RESERVAS DE MATR�CULA", modal: true, width: "600px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
            });

            $(".lnkPesNIRE").click(function () {
                if (($(".ddlSituMatAluno").val() == "S") && ($(".lblSucInfResp").is(":visible"))) {
                    $("#divLoadShowAlunos").load("../../../../../Componentes/ListarAlunos.aspx", function () {
                        $("#divLoadShowAlunos #frmListarAlunos").attr("action", "../../../../../Componentes/ListarAlunos.aspx");
                    });

                    $("#divLoadShowAlunos").dialog({ title: "LISTA DE ALUNOS", modal: true, width: "970px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
                }
            });

            $(".lnkPesResp").click(function () {
                if ($(".lblSucInfResp").is(":visible") == false) {
                    $("#divLoadShowResponsaveis").load("../../../../../Componentes/ListarResponsaveis.aspx", function () {
                        $("#divLoadShowResponsaveis #frmListarResponsaveis").attr("action", "../../../../../Componentes/ListarResponsaveis.aspx");
                    });

                    $("#divLoadShowResponsaveis").dialog({ title: "LISTA DE RESPONS�VEIS", modal: true, width: "690px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
                }
            });

            $('.lnkPesqCID').click(function () {
                $('#divLoadShowDoencas').dialog({ autoopen: false, modal: true, width: 430, height: 390, resizable: false,
                    open: function () { $('#divLoadShowDoencas').load("/Componentes/ListarDoencas.aspx"); }
                });
            });

            $(".chkMatEsc").change(function () {
                if ($('.chkMatEsc').selected()) {
                    $("#tabResp").hide();
                    $("#tabAluno").hide();
                    $("#tabUniMat").show();
                    $("#tabResAliAdd").hide();
                    $("#tabEndAdd").hide();
                    $("#tabTelAdd").hide();
                    $("#tabAtiExtAlu").hide();
                    $("#tabDocumentos").hide();
                    $("#tabMenEsc").hide();
                    if ($("#lblSucCuiEspAlu").is(":visible") == false) { $('#chkCuiEspAlu').attr('checked', false); }
                    if ($("#lnkSucMenEscAlu").is(":visible") == false) { $('#chkMenEscAlu').attr('checked', false); }
                    if ($("#lblSucDocAlu").is(":visible") == false) { $('#chkDocMat').attr('checked', false); }
                    if ($("#lblSucResAliAlu").is(":visible") == false) { $('#chkResAliAlu').attr('checked', false); }
                    if ($("#lblSucEndAddAlu").is(":visible") == false) { $('#chkEndAddAlu').attr('checked', false); }
                    if ($("#lblSucTelAddAlu").is(":visible") == false) { $('#chkTelAddAlu').attr('checked', false); }
                    if ($("#lblSucRegAtiExt").is(":visible") == false) { $('#chkRegAtiExt').attr('checked', false); }
                }
            });

            $(".chkMenEscAlu").change(function () {
                if ($('.chkMenEscAlu').selected()) {
                    $("#tabResp").hide();
                    $("#tabAluno").hide();
                    $("#tabMenEsc").show();
                    $("#tabResAliAdd").hide();
                    $("#tabUniMat").hide();
                    $("#tabDocumentos").hide();
                    $("#tabEndAdd").hide();
                    $("#tabTelAdd").hide();
                    $("#tabAtiExtAlu").hide();
                    $("#tabCuiEspAdd").hide();
                    if ($("#lblSucCuiEspAlu").is(":visible") == false) { $('#chkCuiEspAlu').attr('checked', false); }
                    if ($("#lblSucMatEsc").is(":visible") == false) { $('#chkMatEsc').attr('checked', false); }
                    if ($("#lblSucDocAlu").is(":visible") == false) { $('#chkDocMat').attr('checked', false); }
                    if ($("#lblSucResAliAlu").is(":visible") == false) { $('#chkResAliAlu').attr('checked', false); }
                    if ($("#lblSucEndAddAlu").is(":visible") == false) { $('#chkEndAddAlu').attr('checked', false); }
                    if ($("#lblSucTelAddAlu").is(":visible") == false) { $('#chkTelAddAlu').attr('checked', false); }
                    if ($("#lblSucRegAtiExt").is(":visible") == false) { $('#chkRegAtiExt').attr('checked', false); }
                }
            });

            $(".chkResAliAlu").change(function () {
                if ($('.chkResAliAlu').selected()) {
                    $("#tabResp").hide();
                    $("#tabResAliAdd").show();
                    $("#tabAluno").hide();
                    $("#tabUniMat").hide();
                    $("#tabDocumentos").hide();
                    $("#tabEndAdd").hide();
                    $("#tabTelAdd").hide();
                    $("#tabCuiEspAdd").hide();
                    $("#tabAtiExtAlu").hide();
                    $("#tabMenEsc").hide();
                    if ($("#lblSucDocAlu").is(":visible") == false) { $('#chkDocMat').attr('checked', false); }
                    if ($("#lblSucMatEsc").is(":visible") == false) { $('#chkMatEsc').attr('checked', false); }
                    if ($("#lnkSucMenEscAlu").is(":visible") == false) { $('#chkMenEscAlu').attr('checked', false); }
                    if ($("#lblSucCuiEspAlu").is(":visible") == false) { $('#chkCuiEspAlu').attr('checked', false); }
                    if ($("#lblSucEndAddAlu").is(":visible") == false) { $('#chkEndAddAlu').attr('checked', false); }
                    if ($("#lblSucTelAddAlu").is(":visible") == false) { $('#chkTelAddAlu').attr('checked', false); }
                    if ($("#lblSucRegAtiExt").is(":visible") == false) { $('#chkRegAtiExt').attr('checked', false); }
                }
            });

            $(".chkCuiEspAlu").change(function () {
                if ($('.chkCuiEspAlu').selected()) {
                    $("#tabResp").hide();
                    $("#tabAluno").hide();
                    $("#tabCuiEspAdd").show();
                    $("#tabResAliAdd").hide();
                    $("#tabUniMat").hide();
                    $("#tabDocumentos").hide();
                    $("#tabEndAdd").hide();
                    $("#tabTelAdd").hide();
                    $("#tabAtiExtAlu").hide();
                    $("#tabMenEsc").hide();
                    if ($("#lblSucDocAlu").is(":visible") == false) { $('#chkDocMat').attr('checked', false); }
                    if ($("#lblSucMatEsc").is(":visible") == false) { $('#chkMatEsc').attr('checked', false); }
                    if ($("#lnkSucMenEscAlu").is(":visible") == false) { $('#chkMenEscAlu').attr('checked', false); }
                    if ($("#lblSucResAliAlu").is(":visible") == false) { $('#chkResAliAlu').attr('checked', false); }
                    if ($("#lblSucEndAddAlu").is(":visible") == false) { $('#chkEndAddAlu').attr('checked', false); }
                    if ($("#lblSucTelAddAlu").is(":visible") == false) { $('#chkTelAddAlu').attr('checked', false); }
                    if ($("#lblSucRegAtiExt").is(":visible") == false) { $('#chkRegAtiExt').attr('checked', false); }
                }
            });

            $(".chkDocMat").change(function () {
                if ($('.chkDocMat').selected()) {
                    $("#tabUniMat").hide();
                    $("#tabDocumentos").show();
                    $("#tabResp").hide();
                    $("#tabAluno").hide();
                    $("#tabResAliAdd").hide();
                    $("#tabEndAdd").hide();
                    $("#tabTelAdd").hide();
                    $("#tabCuiEspAdd").hide();
                    $("#tabAtiExtAlu").hide();
                    $("#tabMenEsc").hide();
                    if ($("#lblSucCuiEspAlu").is(":visible") == false) { $('#chkCuiEspAlu').attr('checked', false); }
                    if ($("#lblSucMatEsc").is(":visible") == false) { $('#chkMatEsc').attr('checked', false); }
                    if ($("#lnkSucMenEscAlu").is(":visible") == false) { $('#chkMenEscAlu').attr('checked', false); }
                    if ($("#lblSucResAliAlu").is(":visible") == false) { $('#chkResAliAlu').attr('checked', false); }
                    if ($("#lblSucEndAddAlu").is(":visible") == false) { $('#chkEndAddAlu').attr('checked', false); }
                    if ($("#lblSucTelAddAlu").is(":visible") == false) { $('#chkTelAddAlu').attr('checked', false); }
                    if ($("#lblSucRegAtiExt").is(":visible") == false) { $('#chkRegAtiExt').attr('checked', false); }
                }
            });

            $(".chkEndAddAlu").change(function () {
                if ($('.chkEndAddAlu').selected()) {
                    $("#tabUniMat").hide();
                    $("#tabDocumentos").hide();
                    $("#tabResp").hide();
                    $("#tabAluno").hide();
                    $("#tabDocumentos").hide();
                    $("#tabResAliAdd").hide();
                    $("#tabEndAdd").show();
                    $("#tabTelAdd").hide();
                    $("#tabAtiExtAlu").hide();
                    $("#tabCuiEspAdd").hide();
                    $("#tabMenEsc").hide();
                    if ($("#lblSucCuiEspAlu").is(":visible") == false) { $('#chkCuiEspAlu').attr('checked', false); }
                    if ($("#lblSucMatEsc").is(":visible") == false) { $('#chkMatEsc').attr('checked', false); }
                    if ($("#lnkSucMenEscAlu").is(":visible") == false) { $('#chkMenEscAlu').attr('checked', false); }
                    if ($("#lblSucResAliAlu").is(":visible") == false) { $('#chkResAliAlu').attr('checked', false); }
                    if ($("#lblSucDocAlu").is(":visible") == false) { $('#chkDocMat').attr('checked', false); }
                    if ($("#lblSucTelAddAlu").is(":visible") == false) { $('#chkTelAddAlu').attr('checked', false); }
                    if ($("#lblSucRegAtiExt").is(":visible") == false) { $('#chkRegAtiExt').attr('checked', false); }
                }
            });

            $(".chkTelAddAlu").change(function () {
                if ($('.chkTelAddAlu').selected()) {
                    $("#tabUniMat").hide();
                    $("#tabDocumentos").hide();
                    $("#tabResp").hide();
                    $("#tabAluno").hide();
                    $("#tabDocumentos").hide();
                    $("#tabEndAdd").hide();
                    $("#tabAtiExtAlu").hide();
                    $("#tabResAliAdd").hide();
                    $("#tabTelAdd").show();
                    $("#tabCuiEspAdd").hide();
                    $("#tabMenEsc").hide();
                    if ($("#lblSucCuiEspAlu").is(":visible") == false) { $('#chkCuiEspAlu').attr('checked', false); }
                    if ($("#lblSucMatEsc").is(":visible") == false) { $('#chkMatEsc').attr('checked', false); }
                    if ($("#lnkSucMenEscAlu").is(":visible") == false) { $('#chkMenEscAlu').attr('checked', false); }
                    if ($("#lblSucResAliAlu").is(":visible") == false) { $('#chkResAliAlu').attr('checked', false); }
                    if ($("#lblSucEndAddAlu").is(":visible") == false) { $('#chkEndAddAlu').attr('checked', false); }
                    if ($("#lblSucDocAlu").is(":visible") == false) { $('#chkDocMat').attr('checked', false); }
                    if ($("#lblSucRegAtiExt").is(":visible") == false) { $('#chkRegAtiExt').attr('checked', false); }
                }
            });

            $(".chkRegAtiExt").change(function () {
                if ($('.chkRegAtiExt').selected()) {
                    $("#tabAtiExtAlu").show();
                    $("#tabUniMat").hide();
                    $("#tabDocumentos").hide();
                    $("#tabResp").hide();
                    $("#tabResAliAdd").hide();
                    $("#tabAluno").hide();
                    $("#tabDocumentos").hide();
                    $("#tabEndAdd").hide();
                    $("#tabTelAdd").hide();
                    $("#tabCuiEspAdd").hide();
                    $("#tabMenEsc").hide();
                    if ($("#lblSucCuiEspAlu").is(":visible") == false) { $('#chkCuiEspAlu').attr('checked', false); }
                    if ($("#lblSucMatEsc").is(":visible") == false) { $('#chkMatEsc').attr('checked', false); }
                    if ($("#lnkSucMenEscAlu").is(":visible") == false) { $('#chkMenEscAlu').attr('checked', false); }
                    if ($("#lblSucResAliAlu").is(":visible") == false) { $('#chkResAliAlu').attr('checked', false); }
                    if ($("#lblSucDocAlu").is(":visible") == false) { $('#chkDocMat').attr('checked', false); }
                    if ($("#lblSucTelAddAlu").is(":visible") == false) { $('#chkTelAddAlu').attr('checked', false); }
                    if ($("#lblSucEndAddAlu").is(":visible") == false) { $('#chkEndAddAlu').attr('checked', false); }
                }
            });

        });
    </script>
</asp:Content>