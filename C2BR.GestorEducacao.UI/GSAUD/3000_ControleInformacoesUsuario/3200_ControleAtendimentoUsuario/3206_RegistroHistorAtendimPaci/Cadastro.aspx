<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3206_RegistroHistorAtendimPaci.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .grdLinhaCenter
        {
            text-align: center !important;
        }
        .lblTop
        {
            font-size: 9px;
            margin-bottom: 6px;
            color: #436EEE;
        }
        .liPReqExam
        {
            margin-left: 240px !important;
        }
        
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
            clear: none;
        }
        .liBtnFinAten
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            padding: 2px 3px 1px 3px;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
            
        }
        .liBtnConfir
        {
            background-color: #F0FFFF;
            border: 1px solid #D2DFD1;
            clear: none;
            margin-bottom: 4px;
            padding: 2px 3px 1px;
            margin-left: 5px;
            margin-right: 0px;
        }
        .liEtniaA
        {
            margin-left: 10px;
        }
        .liNire
        {
            margin-top: 3px;
            margin-left: 120px;
        }
        .liBairroETA
        {
            margin-left: 45px;
        }
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
        .liBtnsETA
        {
            float: right !important;
            margin-top: -4px;
            margin-right: 0px;
        }
        .liBtnsCEA
        {
            width: 55px;
            margin-left: 5px;
            margin-right: 0px;
        }
        .liBtnsEndAdd
        {
            margin-top: 10px;
            margin-left: 30px;
            margin-right: 0px;
        }
        .liGridUni
        {
            clear: both;
            margin-left: 10px;
        }
        .liGridMat
        {
            margin-left: 150px;
            margin-top: 5px;
        }
        .liGridAtv
        {
            margin-left: 120px;
        }
        .liPhoto
        {
            float: left !important;
            clear: both;
            margin-top: 7px;
            margin-left: 4px;
        }
        .liIdentidade
        {
            margin-top: 10px;
            width: 330px;
            margin-left: 35px;
        }
        .liTituloEleitor
        {
            margin: 10px 0 0 30px;
            width: 260px;
        }
        .liDist
        {
            margin-top: 3px;
        }
        .liDados1
        {
            padding-left: 10px;
            margin-top: 4px;
            width: 660px;
            margin-right: 0 !important;
        }
        .liNome, .liSexo, .liTpSangue, .liEstadoCivil, .liNacioResp, .liNisR
        {
            margin-left: 7px;
        }
        .liDeficiencia
        {
            margin-left: 2px;
        }
        .liDados3
        {
            width: 100%;
            padding-left: 5px;
        }
        .liNumero
        {
            margin-left: 2px;
        }
        .liCep
        {
            margin-left: 1px;
        }
        .liTelCelular
        {
            clear: none;
            margin-left: 10px;
            margin-right: 0px !important;
        }
        .liGrauInstrucao
        {
            clear: both;
            margin-top: -3px;
        }
        .liRenda
        {
            margin-right: 15px !important;
            margin-top: -2px;
        }
        .liComplementoR
        {
            clear: both;
            margin-top: 0px;
        }
        .liBairroR
        {
            margin-top: -3px;
        }
        .liTelEmp
        {
            margin-left: 43px;
            margin-top: -3px;
        }
        .liClear
        {
            clear: both;
        }
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
        .liEmailA
        {
            margin-left: 10px;
        }
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
        .liPeriodoDeA
        {
            margin-right: 2px !important;
        }
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
        .liUfA
        {
            margin-top: -2px !important;
        }
        #ulReserva liPesqReserva
        {
            margin-top: 10px;
        }
        .ulServs li
        {
            margin-bottom: 3px;
            margin-left: -3px;
        }
        .liSeparador
        {
            border-bottom: solid 3px #CCCCCC;
            width: 100%;
            padding-bottom: 5px;
            margin-bottom: 5px;
            margin-top: 5px;
        }
        .liCPFResp
        {
            margin-top: 10px;
            margin-left: -20px;
        }
        .liPesqCEPResp
        {
            margin-top: 14px;
            margin-left: -3px;
        }
        .liInfSocResp
        {
            clear: both;
            width: 360px;
            margin-left: -4px;
            border-right: 2px solid #CCCCCC;
            height: 105px;
        }
        .liResidResp
        {
            margin-top: -2px;
            margin-left: 70px;
        }
        .liProfissaoResp
        {
            margin-top: -3px;
        }
        .liEndResResp
        {
            margin-left: -4px;
            border-right: 2px solid #CCCCCC;
            width: 360px;
            height: 110px;
        }
        .liContAlu
        {
            clear: both;
            margin-top: 3px;
        }
        .liEnderecoAlu
        {
            width: 320px;
            margin-left: 3px;
            margin-top: 10px;
        }
        .liddlSexoAlu
        {
            margin-top: 3px;
            margin-left: 10px;
        }
        .liddlTpSangueAlu
        {
            margin-top: 3px;
            margin-left: 5px;
        }
        .litxtNisAlu
        {
            margin-left: 10px;
            margin-right: 0 !important;
        }
        .liFiliacaoAlu
        {
            clear: both;
            margin-top: 10px;
            width: 230px;
            height: 105px;
        }
        .liResidAlu
        {
            margin-left: 5px;
            margin-top: -2px;
        }
        .liNomeAluETA
        {
            margin-left: 178px;
        }
        .liEspacamento
        {
            margin-left: 10px;
        }
        .liDescTelAdd
        {
            margin-left: 5px;
            margin-top: -3px;
        }
        .liddlTpTelef
        {
            margin-left: 45px;
        }
        .liddlTpCui
        {
            margin-left: 30px;
        }
        .litxtTelETA
        {
            margin-left: 10px;
        }
        .liNomeContETA, .liObsETA
        {
            margin-left: 10px;
        }
        #divBarraMatric ul li
        {
            display: inline;
            margin-left: -2px;
        }
        .liNIREAluETA, .liNISAluETA
        {
            margin-left: 10px;
        }
        .liPeriodo
        {
            clear: both;
            margin-left: 110px;
        }
        .liBtnsResAli
        {
            float: right !important;
        }
        .liBtnsAtiExt
        {
            clear: both;
            margin-left: 520px;
        }
        .liBtnGrdFinan
        {
            margin-left: 14px;
            padding: 4px 5px 3px;
            margin-top: 12px;
            background-color: #EE9572;
            border: 1px solid #8B8989;
        }
        .liListaEndereco
        {
            margin-top: 13px;
            margin-left: -3px;
        }
        .liBlocoCtaContabil
        {
            margin: 0;
            padding: 0;
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
            width: 515px;
            overflow-y: auto;
            margin-top: 10px;
        }
        .divGridTelETA
        {
            height: 158px;
            width: 693px;
            overflow-y: auto;
        }
        .divGeralApresenta
        {
            height: 297px; 
            width: 748px !important;
            overflow-y: scroll; 
            border: 1px solid #ccc;
        }
        .divGridCEA
        {
            height: 150px;
            width: 690px;
            overflow-y: auto;
            overflow-x: auto;
        }
        #divAddTipo
        {
            display: none;
        }
        .txtLograETA
        {
            width: 230px;
        }
        .txtNumETA
        {
            width: 45px;
        }
        .txtCompETA
        {
            width: 150px;
        }
        .ulDados2
        {
            width: 100%;
        }
        input[type='text']
        {
            margin-bottom: 4px;
        }
        label
        {
            margin-bottom: 1px;
        }
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
        .fldRegSaude
        {
            padding-left: 9px;
            border-width: 0px;
        }
        .fldDepenResp
        {
            border-width: 0px;
        }
        .fldDepenResp legend
        {
            color: Black !important;
            font-size: 1.1em !important;
            font-weight: normal !important;
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
        .txtNISResp, .txtTelCelularResp
        {
            width: 78px;
        }
        .txtDtNascimentoResp, .txtDtEmissaoResp
        {
            width: 60px;
        }
        .ddlSexoResp
        {
            width: 75px;
        }
        .ddlDeficienciaResp
        {
            width: 70px;
        }
        .ddlGrauInstrucaoResp
        {
            width: 90px;
        }
        .ddlProfissaoResp
        {
            width: 150px;
        }
        .ddlEstadoCivilResp
        {
            width: 142px;
        }
        .ddlRendaResp, .txtCPF
        {
            width: 82px;
        }
        .txtPassaporteResp
        {
            width: 70px;
        }
        .txtOrgEmissorResp, .txtIdentidadeResp
        {
            width: 65px;
        }
        .txtNumeroTituloResp
        {
            width: 70px;
        }
        .txtZonaResp, .txtSecaoResp
        {
            width: 25px;
        }
        .txtLogradouroResp
        {
            width: 209px;
        }
        .txtNumeroResp
        {
            width: 39px;
        }
        .txtComplementoResp
        {
            width: 217px;
        }
        .txtTelResidencialResp, .txtTelEmpresaResp
        {
            width: 78px;
        }
        .ddlCidadeResp
        {
            width: 165px;
        }
        .ddlBairroResp
        {
            width: 145px;
        }
        .txtCepResp
        {
            width: 56px;
        }
        .txtEmailResp
        {
            width: 180px;
        }
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
        .txtLogradouroAluno
        {
            width: 150px;
        }
        .txtComplementoAluno
        {
            width: 143px;
        }
        .ddlCidadeAluno
        {
            width: 177px;
        }
        .ddlBairroAluno
        {
            width: 150px;
        }
        .txtResponsavelAluno
        {
            width: 200px;
        }
        .txtEmailAluno
        {
            width: 206px;
        }
        .txtCartaoSaudeAluno
        {
            width: 78px;
        }
        .txtNireAluno
        {
            width: 66px;
        }
        .txtNisAluno
        {
            width: 75px;
        }
        .ddlSexoAluno
        {
            width: 110px;
        }
        .ddlNacionalidadeAluno
        {
            width: 70px;
        }
        .txtNacionalidadeAluno
        {
            width: 70px;
        }
        .txtNaturalidadeAluno
        {
            width: 95px;
        }
        .ddlEstadoCivilAluno
        {
            width: 90px;
        }
        .ddlEtniaAluno
        {
            width: 90px;
        }
        .ddlRendaFamiliarAluno
        {
            width: 65px;
        }
        .ddlDeficienciaAluno
        {
            width: 70px;
        }
        .ddlPasseEscolarAluno, .ddlTransporteEscolarAluno, .ddlTrabResp
        {
            width: 40px;
        }
        .ddlTipoCertidaoAluno
        {
            width: 79px;
        }
        .txtNumeroCertidaoAluno
        {
            width: 37px;
        }
        .txtLivroAluno
        {
            width: 24px;
        }
        .txtFolhaAluno
        {
            width: 24px;
        }
        .txtCartorioAluno
        {
            width: 255px;
        }
        .txtRgAluno
        {
            width: 70px;
        }
        .txtOrgaoEmissorAluno
        {
            width: 55px;
        }
        .txtNumeroTituloAluno
        {
            width: 70px;
        }
        .txtSecaoAluno
        {
            width: 25px;
        }
        .txtZonaAluno
        {
            width: 25px;
        }
        .txtNumeroAluno
        {
            width: 40px;
        }
        .ddlBolsaAluno
        {
            width: 90px;
        }
        .txtDescontoAluno
        {
            width: 40px;
            text-align: right;
        }
        .txtValorTotal
        {
            width: 50px;
            text-align: right;
        }
        .chkIsento label
        {
            margin-top: 5px;
            display: inline;
        }
        #divReserva
        {
            top: 40px;
            width: 460px;
            background-color: #EEEED1;
            padding-left: 6px;
            padding-top: 2px;
            margin-left: 7px;
            margin-top: -1px;
            height: 30px;
        }
        #helpMessages
        {
            visibility: hidden;
        }
        #ulDadosMatricula .lblDadMatr
        {
            font-weight: bold;
            color: Red;
        }
        .txtAlunoReserva
        {
            width: 160px;
        }
        .txtDadosReserva
        {
            width: 200px;
        }
        #divDadosMatricula
        {
            position: fixed;
            right: 0;
            top: 40px;
            width: 600px;
        }
        #divMenuLateral
        {
            position: fixed;
            left: 0;
            top: 80px;
            width: 215px;
            padding-top: 6px;
            border-right: 2px solid #CCCCCC;
            height: 100%;
        }
        #tabResp
        {
            position: fixed;
            right: 0;
            top: 80px;
            width: 748px;
            height: 380px;
            padding: 10px 0 0 10px;
        }
        #tabDocumentos
        {
            position: fixed;
            right: 0;
            top: 80px;
            width: 735px;
            height: 380px;
            padding: 10px 0 0 10px;
        }
        #tabUniMat
        {
            position: fixed;
            right: 0;
            top: 80px;
            width: 735px;
            height: 380px;
            padding: 10px 0 0 10px;
        }
        .chkLocais label
        {
            display: inline !important;
            margin-left: -4px;
        }
        .chkCadBasAlu label
        {
            display: inline !important;
        }
        .chkMatEsc label
        {
            display: inline !important;
            margin-left: -4px;
        }
        .chkCuiEspAlu label
        {
            display: inline !important;
            margin-left: -4px;
        }
        .chkResAliAlu label
        {
            display: inline !important;
            margin-left: -4px;
        }
        .chkRegAtiExt label
        {
            display: inline !important;
            margin-left: -4px;
        }
        .chkDocMat label
        {
            display: inline !important;
            margin-left: -4px;
        }
        .chkMenEscAlu label
        {
            display: inline !important;
            margin-left: 1px;
        }
        .ulServs
        {
            margin-left: -1px;
            padding-top: 4px;
        }
        .ulServs label
        {
            display: inline !important;
            margin-left: 1px;
        }
        .G2Clear
        {
            clear: both;
        }
        #divBotoes
        {
            position: fixed;
            margin-top: -15px;
            width: 630px;
        }
        #divBotoes .lilnkEfetMatric
        {
            background-color: #FAFAD2;
            border: 1px solid #D2DFD1;
            clear: both;
            margin-bottom: 4px;
            width: 66px;
            text-align: center;
            padding: 2px 3px 1px;
            margin-left: 2px;
        }
        #divBotoes .imgliLnk
        {
            width: 15px;
            height: 13px;
        }
        .imgliLnkModSerTur
        {
            width: 21px;
            height: 19px;
        }
        .imgWebCam
        {
            width: 15px;
            height: 13px;
        }
        #divBotoes .lilnkRecMatric
        {
            background-color: #F0FFFF;
            border: 1px solid #D2DFD1;
            margin-bottom: 4px;
            padding: 2px 3px 1px;
            width: 78px;
            margin-left: 2px;
            margin-right: 0px;
            clear: both !important;
        }
        #divBotoes .lilnkBolCarne
        {
            background-color: #F0FFFF;
            border: 1px solid #D2DFD1;
            clear: none;
            margin-bottom: 4px;
            padding: 2px 3px 1px;
            margin-left: 2px;
            margin-right: 0px;
            width: 58px;
        }
        .divPaciEnca
        {
            border: 1px solid #CCCCCC;
            width: 765px;
            height: 180px;
            overflow-y: scroll;
            margin-left: 5px;
            margin-bottom: 13px;
            <%--display: none;--%>
        }
        .divConsul
        {
            border: 1px solid #CCCCCC;
            width: 765px;
            height: 180px;
            overflow-y: scroll;
            margin-left: 5px;
            margin-bottom: 13px;
            <%--display: none;--%>
        }
        #divBotoes .lilnkCarteira
        {
            background-color: #F0FFFF;
            border: 1px solid #D2DFD1;
            clear: none;
            margin-bottom: 4px;
            padding: 2px 3px 1px;
            margin-left: 2px;
            width: 68px;
            margin-right: 0px;
        }
        #divBotoes .lilnkRegPgto
        {
            background-color: #C1FFC1;
            border: 1px solid #32CD32;
            clear: none;
            margin-bottom: 4px;
            padding: 2px 3px 2px;
            margin-left: 5px;
            width: 52px;
            text-align: center;
            margin-right: 0px;
        }
        #divMenuLateral .lblTitInf
        {
            font-weight: bold;
            color: Black;
        }
        #divMenuLateral .lblSubTitInf
        {
            font-family: Arial;
        }
        #divMenuLateral .liTitInf
        {
            margin-left: 20px;
            margin-top: -5px;
        }
        .btnPesqMat, .btnPesqCID
        {
            width: 13px;
        }
        .btnPesqReserva
        {
            width: 13px;
        }
        .txtNire
        {
            width: 70px;
        }
        .txtNoRespCPF
        {
            width: 220px;
            background-color: #FFF8DC !important;
        }
        .txtNoInfAluno
        {
            width: 220px;
            background-color: #B9D3EE !important;
        }
        .fldFiliaResp
        {
            border-width: 0px;
        }
        .fldFiliaResp legend
        {
            font-weight: bold;
            font-size: 0.9em !important;
        }
        .txtMaeResp, .txtPaiResp
        {
            width: 215px;
        }
        .txtDepMenResp
        {
            width: 25px;
        }
        .txtMesAnoTrabResp
        {
            width: 45px;
            margin-left: 7px;
        }
        .ddlTpSangueAluno
        {
            width: 35px;
            clear: both;
        }
        .ddlStaTpSangueAluno
        {
            width: 28px;
        }
        .ddlNacioResp
        {
            width: 93px;
        }
        .txtQtdMenoresResp
        {
            width: 20px;
        }
        .ddlSituMatAluno
        {
            width: 85px;
        }
        .imgLnkInc
        {
            width: 19px;
            height: 19px;
        }
        .imgLnkExc
        {
            width: 13px;
            height: 13px;
        }
        #tabAluno
        {
            position: fixed;
            right: -5px;
            top: 83px;
            width: 750px;
            height: 380px;
            padding: 7px 0 0 20px;
        }
        #tabTelAdd, #tabEndAdd, #tabResAliAdd, #tabAtiExtAlu, #tabMenEsc, #tabResMedicamentos, #tabResConsultas, #tabAtendMedic, #tabReqMedic, #tabReqExam, #tabReqServAmbu, #tabRegResExam, #tabEncamMed, #tabEncamIntern, #tabExamHist, #tabConsHist, #tabMedcHist, #tabDiagHist, #tabServAmbuHist, #tabRegAtestMedc, #tabAtestMedcHist, #tabImgMedcHist, #tabSelPacien, #tabhistReceiMedic
        {
            position: fixed;
            right: 0;
            top: 109px;
            width: 783px;
            height: 380px;
            padding: 7px 0 0 10px;
        }
        #tabCuiEspAdd
        {
            position: fixed;
            right: 0;
            top: 109px;
            width: 748px;
            height: 380px;
            padding: 7px 0 0 10px;
        }
        #tabAluno legend
        {
            color: Black;
        }
        .ddlTpResidAluno
        {
            width: 70px;
        }
        .ddlGrauParentescoAluno
        {
            width: 62px;
        }
        .txtNumReserva
        {
            width: 75px;
        }
        .imgPesRes
        {
            width: 13px;
            height: 15px;
        }
        .imgInfResp
        {
            width: 17px;
            height: 19px;
        }
        .ddlSerieCurso
        {
            width: 73px;
        }
        .txtObservacoesResp
        {
            width: 355px;
            height: 40px;
            margin-top: 0px;
        }
        .ddlTpResidResp
        {
            width: 68px;
        }
        #divBarraPadraoContent
        {
            display: none;
        }
        #divBarraMatric
        {
            position: absolute;
            margin-left: 140px;
            margin-top: -22px;
            border: 1px solid #CCC;
            overflow: auto;
            width: 230px;
            padding: 3px 0;
        }
        #divBarraMatric ul
        {
            display: inline;
            float: left;
            margin-left: 10px;
        }
        #divBarraMatric ul li img
        {
            width: 19px;
            height: 19px;
        }
        .ddlTpTelef
        {
            width: 110px;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: left;
        }
        .lblDivData
        {
            display: inline;
            margin: 0 1px;
            margin-top: 0px;
        }
        .txtNumCRMCEA
        {
            width: 45px;
        }
        .txtQtdeCEA
        {
            width: 25px;
        }
        .txtHrAplic
        {
            width: 30px;
        }
        .ddlTpCui
        {
            width: 100px;
        }
        .txtCodRestri
        {
            width: 45px;
        }
        .ddlTpRestri
        {
            width: 85px;
        }
        .txtDescRestri
        {
            width: 130px;
        }
        .txtAcaoRestri
        {
            width: 170px;
        }
        .ddlGrauRestri
        {
            width: 75px;
        }
        .txtDescCEA
        {
            width: 170px;
        }
        .ddlAtivExtra
        {
            width: 170px;
        }
        .txtSiglaAEA
        {
            width: 60px;
        }
        #ControleImagem .liControleImagemComp
        {
            margin-top: -2px !important;
        }
        #ControleImagem .liControleImagemComp .fakefile
        {
            width: 60px !important;
        }
        .ddlBoleto
        {
            width: 190px;
        }
        .txtQtdeMesesDesctoMensa
        {
            width: 37px;
            text-align: right;
        }
        .txtDesctoMensa
        {
            width: 70px;
            text-align: right;
        }
        .ddlTipoDesctoMensa
        {
            width: 65px;
        }
        .chkLocais label
        {
            display: inline !important;
            margin-left: -4px;
        }
        .ddlDiaVecto
        {
            width: 35px;
        }
        .txtCidadeCertidaoAlu
        {
            width: 135px;
        }
        .txtNomeMaeAluno
        {
            width: 220px;
        }
        #divSolicitacoes
        {
            height: 110px;
            width: 450px;
            overflow-y: scroll;
            margin-top: 0px;
        }
        .txtQtdeSolic
        {
            text-align: right;
            width: 20px;
        }
         .chkEncam label
        {
            display: inline !important;
        }
        .chkConsul label
        {
             display: inline !important;
        }
        .rbtInfo
        {
            clear: none;
        }
        
        .liPrima
        {
            clear: both;
            margin-left: 10px !important;
        }
        
        .liTituloGrid
        {
            height: 15px;
            width: 100px;
            text-align: center;
            padding: 5 0 5 0;
            clear: both;
        }
        
        .divGridData
        {
            overflow-y: auto;
        }
        
        .liMarginLeft
        {
            margin-left: 6px !important;
        }
        
        .fontAumen
        {
            font-size:13px;    
        }
          .ulLeitura li
        {
            margin-top:-2px !important;
        }
        .ulRisco li
        {
            margin-top:1px !important;
        }
         input
        {
            height: 13px !important;
        }
          .divLeitura
        {
            width: 205px;
            height: 139px;
            float: left;
            margin-left:3px;
            margin-top:10px;
        }
         .liFotoColab
        {
            float: left !important;
            margin-right: 7px !important;
            border:1px solid #CCCCCC;
            margin-left:7px;
        }
        /*--> CSS DADOS */
        .fldFotoColab
        {
            border: none;
            width: 90px;
            height: 94px;
            margin-left:-6px;
        }
                .liFotoColabAluno
        {
            float: left !important;
            margin-right: 10px !important;
            border: 0 none;
        }
        /*--> CSS DADOS */
        .fldFotoColabAluno
        {
            border: none;
            width: 90px;
            height: 108px;
        }
         .divRegRisco
        {
            margin-top:4px;
            padding-left:2px;
            width: 550px;
            height: 120px;
            float: right;
        }
        #divRisco1
        {
            width:114px;
            height:119px;
            border-right:1px solid #CCCCCC;
        }
        #divRisco2
        {
            width:202px;
            height:119px;
            border-right:1px solid #CCCCCC;
            padding-left:5px;
        }
        #divMedicacao
        {
            width:410px;
            height:87px; 
            margin:-5px 0 0 10px !important; 
        }
        #divInfosPrev
        {
            margin:-5px 0 0 2px;   
        }
        .divQuestion
        {
            <%--border: 1px solid #CCCCCC;--%>;
            margin-top:0px;
            width: 580px;
            height: 90px;
            float: right;
        }
                .lblsub
        {
            clear:both;
            color: #436EEE;
            margin-bottom:3px;
        }
        .lblTituGr
        {
            font-size: 12px;
        }
        .chkAreasChk
        {   
            margin-left:-6px;
        }
        .chkItens
        {
            margin-left:-5px;
        }
        .campoHora { width:30px; }
        
        /*CSS PARA A GRID DE EXAMES PERSONALIZADA*/
        
        #divListarProdutosContent .rowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divListarProdutosContent .alternateRowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divListarProdutosContent .alternateRowStyleLD td
        {
            background-color: #EEEEEE;
            padding-left: 5px;
            padding-right: 5px;
        }
        #divListarProdutosContent .rowStyleLD td
        {
            padding-left: 5px;
            padding-right: 5px;
        }
        #divListarProdutosContainer #divRodapeLD
        {
            margin-top: 20px;
            float: right;
        }
        #divListarProdutosContainer #imgLogoGestorLD
        {
            width: 80px;
            height: 30px;
            padding-right: 5px;
            margin-right: 5px;
        }
        #divHelpTxtLD
        {
            float: left;
            margin-top: 10px;
            width: 174px;
            color: #DF6B0D;
            font-weight: bold;
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
        #divListarProdutosContent
        {
            height: 261px;
            overflow-y: auto;
            border: 1px solid #EBF0FB;
        }
        
        /*FIM DA CSS PARA A GRID DE EXAMES PERSONALIZADA*/
        
        /* CSS PARA A GRID DE SERVIÇOS AMBULATORIAIS PERSONALIZADA */
        
           #divListarProdutosContent2 .rowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divListarProdutosContent2 .alternateRowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divListarProdutosContent2 .alternateRowStyleLD td
        {
            background-color: #EEEEEE;
            padding-left: 5px;
            padding-right: 5px;
        }
        #divListarProdutosContent2 .rowStyleLD td
        {
            padding-left: 5px;
            padding-right: 5px;
        }
        #divListarProdutosContainer2 #divRodapeLD
        {
            margin-top: 20px;
            float: right;
        }
        #divListarProdutosContainer2 #imgLogoGestorLD
        {
            width: 80px;
            height: 30px;
            padding-right: 5px;
            margin-right: 5px;
        }
        #divHelpTxtLD2
        {
            float: left;
            margin-top: 10px;
            width: 174px;
            color: #DF6B0D;
            font-weight: bold;
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
        #divListarProdutosContent2
        {
            height: 261px;
            overflow-y: auto;
            border: 1px solid #EBF0FB;
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
        .ulIdentResp li
        {
            margin-left: 0px;
        }
        .ulDadosContatosResp li
        {
            margin-left: 0px;
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
            margin-left: 5px !important;
        }
         .ulDadosPaciente li
        {
            margin-left: 0px;
        }
                .ulInfosGerais
        {
            margin-top: 0px;
        }
        .ulInfosGerais li
        {
            margin: 1px 0 3px 0px;
        }
        /* FIM DA CSS PARA A GRID DE SERVIÇOS AMBULAOTIAIS PERSONALIZADA*/
        
    </style>
    <script type="text/javascript">

        /*if (navigator.userAgent.toLowerCase().match('chrome'))
        $("#ControleImagem .liControleImagemComp .lblProcurar").hide();*/
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
 <ul id="ulDados" class="ulDados">
        <li style="margin-right: 0px;">
            <ul id="ulDadosMatricula">
                <li style="margin-top: 10px; margin-left: 15px; float: right;">
                    <div id="divBotoes">
                        <ul>
                            <li>
                                <div id="div4" class="bar">
                                    <div id="divBarraMatric" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;
                                        margin-left: 700px;">
                                        <ul id="ulNavegacao" style="width: 42px;">
                                            <li id="btnVoltarPainel"><a href="javascript:parent.showHomePage();">
                                                <img title="Clique para voltar ao Painel Inicial." alt="Icone Voltar ao Painel Inicial."
                                                    src="/BarrasFerramentas/Icones/VoltarPainelGestor.png" />
                                            </a></li>
                                            <li id="btnVoltar" style="margin-right: 0px; clear: none;"><a href="javascript:BackToHome();">
                                                <img title="Clique para voltar a Pagina Anterior." alt="Icone Voltar a Pagina Anterior."
                                                    src="/BarrasFerramentas/Icones/VoltarPaginaAnterior.png" />
                                            </a></li>
                                        </ul>
                                        <ul id="ulEditarNovo" style="width: 39px;">
                                            <li id="btnEditar" style="margin-right: 2px;">
                                                <img title="Abre o registro atualmente selecionado em modo de edicao." alt="Icone Editar."
                                                    src="/BarrasFerramentas/Icones/EditarRegistro_Desabilitado.png" />
                                            </li>
                                            <li id="btnNovo" style="margin-right: 0px;">
                                                <img title="Abre o formulario para Criar um Novo Registro." alt="Icone de Criar Novo Registro."
                                                    src="/BarrasFerramentas/Icones/NovoRegistro_Desabilitado.png" />
                                            </li>
                                        </ul>
                                        <ul id="ulGravar">
                                            <li style="margin-right: 0px;">
                                                <img title="Grava o registro atual na base de dados." alt="Icone de Gravar o Registro."
                                                    src="/BarrasFerramentas/Icones/Gravar_Desabilitado.png" />
                                            </li>
                                        </ul>
                                        <ul id="ulExcluirCancelar" style="width: 39px;">
                                            <li id="btnExcluir" style="margin-right: 2px;">
                                                <img title="Exclui o Registro atual selecionado." alt="Icone de Excluir o Registro."
                                                    src="/BarrasFerramentas/Icones/Excluir_Desabilitado.png" />
                                            </li>
                                            <li id="btnCancelar" style="margin-right: 0px;">
                                                <%--  </a>--%>
                                                <img title="Cancela a Pesquisa atual e limpa o Formulário de Parâmetros de Pesquisa."
                                                    alt="Icone de Cancelar Operacao Atual." src="/BarrasFerramentas/Icones/Cancelar_Desabilitado.png" />
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
                                                    alt="Icone de Pesquisa." src="/BarrasFerramentas/Icones/Pesquisar_Desabilitado.png" />
                                            </li>
                                            <li id="liImprimir" style="margin-right: 0px;">
                                                <img title="Formula um relatório a partir dos parâmetros especificados e o exibe em Modo de Impressão."
                                                    alt="Icone de Impressao." src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </li>
                            <li class="liPrima" style="margin-top: -7px !important; margin-left: 0px !important;">
                                <label title="Informe a data do atendimento">
                                    Data</label>
                                <asp:TextBox ID="txtDataAtend" runat="server" CssClass="campoData" ToolTip="Informe a data do atendimento">
                                </asp:TextBox>
                            </li>
                            <%--<asp:UpdatePanel ID="updTopo" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                            <asp:HiddenField runat="server" ID="hidCoProfSaud" />
                            <li style="margin-top: -7px !important;">
                                <label for="ddlMedico" title="Nome do usuário selecionado" style="font-weight: bold">
                                    Médico</label>
                                <div id="divMedic" clientidmode="Static">
                                    <asp:DropDownList ID="ddlMedico" runat="server" ToolTip="Informe o Médico que fará o Atendimento"
                                        Width="250px" OnSelectedIndexChanged="ddlMedico_OnSelectedIndexChanged" AutoPostBack="true"
                                        ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                                <div id="divMedicEn" clientidmode="Static" style="display: none;">
                                    <asp:DropDownList ID="ddlMedicoEn" runat="server" ToolTip="Informe o Médico que fará o Atendimento"
                                        Width="250px" OnSelectedIndexChanged="ddlMedicoEn_OnSelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div id="divMedicCo" clientidmode="Static" style="display: none;">
                                    <asp:DropDownList ID="ddlMedicoCo" runat="server" ToolTip="Informe o Médico que fará o Atendimento"
                                        Width="250px" OnSelectedIndexChanged="ddlMedicoCo_OnSelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <%--                    <asp:TextBox ID="txtNomeUsuAteMed" Enabled="false" runat="server" ToolTip="Nome do usuário selecionado" Width="280px">
                                        </asp:TextBox>--%>
                            </li>
                            <li style="margin-top: -7px !important;">
                                <label>
                                    Paciente</label>
                                <asp:TextBox runat="server" ID="txtPaciente" Width="220px" Enabled="false"></asp:TextBox>
                            </li>
                            <%--<li id="li9" runat="server" style="background-color: #fafad2 !important; padding-top: 0px; padding-bottom: 0px; margin-top:5px" class="liBtnFinAten">
                                <asp:LinkButton ID="lnkFinAtendMedic2" runat="server" OnClick="lnkFinAtendMedic_lnkFinAtendMedic">
                                    <img id="img3" runat="server" width="14" height="14" class="imgliLnk" src='/Library/IMG/Gestor_CheckSucess.png'
                                        alt="Icone Pesquisa" title="Finalizar o Atendimento Médico" />
                                    <asp:Label runat="server" ID="Label44" Text="FINALIZAR" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>--%>
                            <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                            <li id="lilnkFicha" runat="server" style="display: none; width: 70px;" class="lilnkBolCarne"
                                title="Clique para Imprimir a Ficha">
                                <asp:LinkButton ID="lnkFicha" OnClick="lnkFicha_Click" Enabled="false" runat="server"
                                    Style="margin: 0 auto;" ToolTip="Imprimir Ficha">
                                    <img id="imgFicha" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                                        alt="Icone Pesquisa" title="FICHA" />
                                    <asp:Label runat="server" ID="lblFicha" Text="FICHA" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
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
                <%--                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>--%>
                <ul id="ulMenuLateral">
                    <!-- INICIO PARTE DAS INFORMAÇÕES DO RESPONSÁVEL -->
                    <li class="libtnInfResp" style="display: none;">
                        <img class="imgInfResp" src='/Library/IMG/Gestor_Numero1Preto.png' alt="Icone Pesquisa"
                            title="Ativa Informações do Responsável." />
                        <asp:Label runat="server" ID="lblbtnInfResp" CssClass="lblTitInf" Text="INFORMAÇÕES RESPONSÁVEL"></asp:Label>
                        <asp:Label runat="server" ID="lblSucInfResp" CssClass="lblSucInfResp" Visible="false"
                            Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                    </li>
                    <li class="liTitInf" style="display: none;">
                        <label class="lblSubTitInf">
                            Digite ou atualize os dados do Responsável</label>
                    </li>
                    <li style="margin-left: 20px; display: none;"><span title="Número do CPF do Responsável">
                        Nº CPF</span>
                        <asp:TextBox ID="txtCPFResp222" CssClass="txtCPF" Style="width: 75px;" runat="server"
                            ToolTip="Informe o CPF do Responsável"></asp:TextBox>
                        <asp:HiddenField ID="hdfCPFRespRes" runat="server" />
                    </li>
                    <li class="liPesqReserva" style="display: none;"><a class="lnkPesResp" href="#">
                        <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade" /></a>
                    </li>
                    <li style="clear: both; margin-left: 20px; display: none;">
                        <asp:TextBox ID="txtNoRespCPF" CssClass="txtNoRespCPF" runat="server" Enabled="False"></asp:TextBox>
                    </li>
                    <!-- FIM PARTE DAS INFORMAÇÕES DO RESPONSÁVEL -->
                    <!-- INICIO PARTE DAS INFORMAÇÕES DO ALUNO -->
                    <li style="margin-top: 2px; display: none;">
                        <img class="imgInfResp" src='/Library/IMG/Gestor_Numero2Vermelho.png' alt="Icone Pesquisa"
                            title="Ativa Informações do Aluno." />
                        <asp:Label runat="server" ID="Label2" CssClass="lblTitInf" Text="INFORMAÇÕES DO USUÁRIO DE SAÚDE"></asp:Label>
                        <asp:Label runat="server" ID="lblSucInfAlu" CssClass="lblSucInfAlu" Visible="false"
                            Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                    </li>
                    <li class="liTitInf" style="display: none;">
                        <label class="lblSubTitInf">
                            Selecione uma das opções de consulta, informe o código e clique na lupa para pesquisar.</label>
                    </li>
                    <li style="clear: both; margin-left: 20px; display: none;">
                        <input type="radio" name="rdDadosResp" value="NIRS" checked="checked" title="Selecione N° NIRS ou CPF" />
                        N° NIRS
                        <input type="radio" name="rdDadosResp" value="CPF" style="display: none;" title="Selecione N° NIRS ou CPF" />
                        <asp:TextBox ID="txtNumNIRE" CssClass="txtNire" runat="server" ToolTip="Informe o Nº NIRS ou CPF do Usuário"></asp:TextBox>
                    </li>
                    <li class="liPesqReserva" style="display: none;"><a class="lnkPesNIRE" href="#">
                        <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" title="Pesquisa habilitada só para matrícula sem reserva."
                            alt="Icone" /></a> </li>
                    <li style="margin-left: 20px; display: none;">
                        <asp:TextBox ID="txtNoInfAluno" CssClass="txtNoInfAluno" Style="color: Black !important;"
                            runat="server" Enabled="False"></asp:TextBox>
                    </li>
                    <!-- FIM PARTE DAS INFORMAÇÕES DO ALUNO -->
                    <!-- INICIO PARTE DAS INFORMAÇÕES DA MATRÍCULAS -->
                    <li style="margin-top: 2px; margin-bottom: 3px;">
                        <img class="imgInfResp" src='/Library/IMG/Gestor_Numero3Vermelho.png' style="display: none;"
                            alt="Icone Pesquisa" title="Ativa Registro de Necessidades." />
                        <img class="imgInfResp" src='/Library/IMG/Gestor_Numero1Preto.png' alt="Icone Pesquisa"
                            title="Ativa Informações do Responsável." />
                        <asp:Label runat="server" ID="Label5" CssClass="lblTitInf" Text="REGISTRO DE NECESSIDADES"></asp:Label>
                    </li>
                    <li class="liTitInf" style="margin-bottom: -4px;">
                        <label class="lblSubTitInf">
                            Selecione a necessidade abaixo:</label>
                    </li>
                    <li id="li4" style="margin-top: 5px;">
                        <%--<asp:UpdatePanel runat="server" ID="updInfosAtend" UpdateMode="Conditional">
                     <ContentTemplate>--%>
                        <ul class="ulServs" style="margin-left: 19px;">
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkSelPacien" Checked="true" Enabled="true" ClientIDMode="Static"
                                    ID="chkSelPacien" runat="server" Text="Seleção do Paciente" />
                                <asp:Label runat="server" ID="lblSelPacien" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkAtdMedico" Enabled="false" ClientIDMode="Static" ID="chkAtdMedico"
                                    runat="server" Text="Atendimento Médico" />
                                <asp:Label runat="server" ID="lblAtdMedico" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkReqMedicPaci" Enabled="false" ClientIDMode="Static" ID="chkReqMedicPaci"
                                    runat="server" Text="Receita Médica" />
                                <asp:Label runat="server" ID="lblReqMediPaci" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkReqExamPaci" Enabled="false" ClientIDMode="Static" ID="chkReqExamPaci"
                                    runat="server" Text="Exame - Requisição" />
                                <asp:Label runat="server" ID="lblReqExamPaci" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkRegResExame" Enabled="false" ClientIDMode="Static" ID="chkRegResExame"
                                    runat="server" Text="Exame - Registro de Resultado" />
                                <asp:Label runat="server" ID="lblRegResExame" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkReqSevAmbuPaci" Enabled="false" ClientIDMode="Static"
                                    ID="chkReqSevAmbuPaci" runat="server" Text="Requisição de Serv. Ambulatorial" />
                                <asp:Label runat="server" ID="lblReqSevAmbuPaci" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkResMedicamentos" Enabled="false" ClientIDMode="Static"
                                    ID="chkResMedicamentos" runat="server" Text="Reserva Medicamentos" />
                                <asp:Label runat="server" ID="lblResMedicamentos" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkRegAtestMedcPaci" Enabled="false" ClientIDMode="Static"
                                    ID="chkRegAtestMedcPaci" runat="server" Text="Atestado Médico" />
                                <asp:Label runat="server" ID="lblRegAtestMedcPaci" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkEncaMedicPaci" Enabled="false" ClientIDMode="Static" ID="chkEncaMedicPaci"
                                    runat="server" Text="Encaminhamento Médico" />
                                <asp:Label runat="server" ID="lblEncaMedicPaci" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="Encaminhamento Médico"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkEncaIntern" Enabled="false" ClientIDMode="Static" ID="chkEncaIntern"
                                    runat="server" Text="Encaminhamento Internação" />
                                <asp:Label runat="server" ID="lblEncaIntern" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="Encaminhamento Internação"></asp:Label>
                            </li>
                            <li class="G2Clear" style="display: none;">
                                <asp:CheckBox CssClass="chkResConsulta" Enabled="true" ClientIDMode="Static" ID="chkResConsulta"
                                    runat="server" Text="Reserva Consultas" />
                                <asp:Label runat="server" ID="lblResConsulta" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                            </li>
                            <li class="G2Clear" style="display: none;">
                                <asp:CheckBox CssClass="chkResExames" Enabled="true" ClientIDMode="Static" ID="chkResExames"
                                    runat="server" Text="Reserva Exames" />
                                <asp:Label runat="server" ID="lblResExames" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                            </li>
                            <li class="G2Clear" style="display: none;">
                                <asp:CheckBox CssClass="chkAtdAmbulatorial" Enabled="true" ClientIDMode="Static"
                                    ID="chkAtdAmbulatorial" runat="server" Text="Atendimento Ambulatorial" />
                                <asp:Label runat="server" ID="lblAtdAmbulatorial" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                            </li>
                            <li class="G2Clear" style="display: none;">
                                <asp:CheckBox CssClass="chkPrtAtendimento" Enabled="true" ClientIDMode="Static" ID="chkPrtAtendimento"
                                    runat="server" Text="Pronto Atendimento" />
                                <asp:Label runat="server" ID="lblPrtAtendimento" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                            </li>
                            <li class="G2Clear" style="display: none;">
                                <asp:CheckBox CssClass="chkOutros" Enabled="true" ClientIDMode="Static" ID="chkOutros"
                                    runat="server" Text="Outros" />
                                <asp:Label runat="server" ID="lblOutros" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                            </li>
                            <li id="li8" runat="server" class="liBtnFinAten" style="clear: both !important; background-color: #fafad2 !important;
                                margin: 3px 0 10px 1px;">
                                <asp:LinkButton ID="lnkFinAtendMedic" runat="server" OnClick="lnkFinAtendMedic_lnkFinAtendMedic"
                                    Enabled="false">
                                    <img id="img2" runat="server" width="14" height="14" class="imgliLnk" src='/Library/IMG/Gestor_CheckSucess.png'
                                        alt="Icone Pesquisa" title="Finaliza o Atendimento Médico" />
                                    <asp:Label runat="server" ID="Label43" Text="FINALIZAR ATENDIMENTO" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                        <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </li>
                    <!-- FIM PARTE DAS INFORMAÇÕES DA MATRÍCULA -->
                    <!-- INICIO PARTE DO HISTÓRICO DO PACIÊNTE -->
                    <li style="margin-top: 2px; margin-bottom: 3px;">
                        <img class="imgInfResp" src='/Library/IMG/Gestor_Numero3Vermelho.png' style="display: none;"
                            alt="Icone Pesquisa" title="Ativa Registro de Necessidades." />
                        <img class="imgInfResp" src='/Library/IMG/Gestor_Numero2Preto.png' alt="Icone Pesquisa"
                            title="Ativa Informações do Responsável." />
                        <asp:Label runat="server" ID="Label37" CssClass="lblTitInf" Text="PRONTUÁRIO DO PACIENTE" style="color:#4682b4;"></asp:Label>
                    </li>
                    <li class="liTitInf" style="margin-bottom: -4px;">
                        <label class="lblSubTitInf">
                            Selecione a necessidade abaixo:</label>
                    </li>
                    <li style="margin-top: 5px;">
                        <%--<asp:UpdatePanel runat="server" ID="updHistPaci" UpdateMode="Conditional">
                      <ContentTemplate>--%>
                        <ul class="ulServs" style="margin-left: 19px;">
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkDiagPaci" Enabled="false" ClientIDMode="Static" ID="chkDiagPaci"
                                    runat="server" Text="Diagnósticos" />
                                <asp:Label runat="server" ID="lblDiagPaci" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="Diagnósticos"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkConsPaci" Enabled="false" ClientIDMode="Static" ID="chkConsPaci"
                                    runat="server" Text="Consultas" />
                                <asp:Label runat="server" ID="lblConsPaci" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="Consultas"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkMedicPaci" Enabled="false" ClientIDMode="Static" ID="chkMedicPaci"
                                    runat="server" Text="Medicamentos" />
                                <asp:Label runat="server" ID="lblMedicPaci" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="Medicamentos"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkHistReceiMedic" Enabled="false" ClientIDMode="Static"
                                    ID="chkHistReceiMedic" runat="server" Text="Receitas Médicas" />
                                <asp:Label runat="server" ID="Label19" ClientIDMode="Static" CssClass="lblSucReceMedicH"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="Medicamentos"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkExmPaci" Enabled="false" ClientIDMode="Static" ID="chkExmPaci"
                                    runat="server" Text="Exames" />
                                <asp:Label runat="server" ID="lblExmPaci" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="Exames"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkServAmbPaci" Enabled="false" ClientIDMode="Static" ID="chkServAmbPaci"
                                    runat="server" Text="Serviços Ambulatoriais" />
                                <asp:Label runat="server" ID="lblServAmbPaci" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="Serviços Ambulatoriais"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkAtestMedcPaci" Enabled="false" ClientIDMode="Static" ID="chkAtestMedcPaci"
                                    runat="server" Text="Atestados Médicos" />
                                <asp:Label runat="server" ID="lblAtestMedcPaci" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="Atestados Médicos"></asp:Label>
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkImgAtendPaci" Enabled="false" ClientIDMode="Static" ID="chkImgAtendPaci"
                                    runat="server" Text="Imagens" />
                                <asp:Label runat="server" ID="lblImgAtendPaci" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                    Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="Imagens"></asp:Label>
                            </li>

                        </ul>
                        <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </li>
                    <!-- FIM PARTE DO HISTÓRICO DO PACIÊNTE -->
                    <!-- INICIO PARTE DAS INFORMAÇÕES ADICIONAIS DO ALUNO -->
                    <li style="margin-top: 2px; display: none;">
                        <div runat="server" id="divInformacoesAdd">
                            <ul>
                                <li style="margin-top: 2px;">
                                    <img runat="server" id="imgNumeroInformacoesAdd" class="imgInfResp" src='/Library/IMG/Gestor_Numero4Preto.png'
                                        alt="Icone Pesquisa" title="Ativa Informações Adicionais do Aluno." />
                                    <asp:Label runat="server" ID="Label3" CssClass="lblTitInf" Text="INFORMAÇÕES ADICIONAIS"></asp:Label>
                                </li>
                                <li class="liTitInf" style="margin-bottom: -4px;">
                                    <label class="lblSubTitInf">
                                        Marque uma das opções abaixo e informe:</label>
                                </li>
                                <li id="liInfAdd">
                                    <ul class="ulServs" style="margin-left: 19px;">
                                        <li class="G2Clear">
                                            <asp:CheckBox CssClass="chkEndAddAlu" Enabled="true" ClientIDMode="Static" ID="chkEndAddAlu"
                                                runat="server" Text="Endereços Adicionais" />
                                            <asp:Label runat="server" ID="lblSucEndAddAlu" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                                Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                                        </li>
                                        <li class="G2Clear">
                                            <asp:CheckBox CssClass="chkTelAddAlu" Enabled="true" ClientIDMode="Static" ID="chkTelAddAlu"
                                                runat="server" Text="Telefones Adicionais" />
                                            <asp:Label runat="server" ID="lblSucTelAddAlu" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                                Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                                        </li>
                                        <li class="G2Clear">
                                            <asp:CheckBox CssClass="chkCuiEspAlu" Enabled="true" ClientIDMode="Static" ID="chkCuiEspAlu"
                                                runat="server" Text="Cuidados Especiais" />
                                            <asp:Label runat="server" ID="lblSucCuiEspAlu" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                                Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                                        </li>
                                        <li class="G2Clear">
                                            <asp:CheckBox CssClass="chkResAliAlu" Enabled="true" ClientIDMode="Static" ID="chkResAliAlu"
                                                runat="server" Text="Restrições Alimentares" />
                                            <asp:Label runat="server" ID="lblSucResAliAlu" ClientIDMode="Static" CssClass="lblSucInfAlu"
                                                Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <!-- FIM PARTE DAS INFORMAÇÕES ADICIONAIS DO ALUNO -->
                    <!-- INICIO PARTE DAS INFORMAÇÕES DAS MENSALIDADES -->
                    <li style="margin-top: 2px; display: none;">
                        <img runat="server" id="imgNumeroMensalidades" class="imgInfResp" src='/Library/IMG/Gestor_Numero5Preto.png'
                            alt="Icone Pesquisa" title="Ativa Mensalidades Escolares do Aluno." />
                        <asp:Label runat="server" ID="Label4" CssClass="lblTitInf" Text="MENSALIDADES ESCOLARES"></asp:Label>
                    </li>
                    <li class="liTitInf" style="display: none;">
                        <label class="lblSubTitInf">
                            Confirme as informações dos itens abaixo:</label>
                    </li>
                    <li class="lichkUniforme" style="margin-left: 16px; display: none;">
                        <asp:CheckBox CssClass="chkMenEscAlu" Enabled="false" ClientIDMode="Static" ID="chkMenEscAlu"
                            runat="server" Text="Gerar Mensalidades Escolares" />
                        <asp:Label runat="server" ID="lnkSucMenEscAlu" ClientIDMode="Static" CssClass="lblSucInfAlu"
                            Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                    </li>
                    <!-- FIM PARTE DAS INFORMAÇÕES DAS MENSALIDADES -->
                </ul>
                <%--                    </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                              FIM MENU LATERAL                                                                                              -->
            <!-- =========================================================================================================================================================================== -->
            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                <ContentTemplate>--%>
            <div id="tabResMedicamentos" runat="server" clientidmode="Static" style="display: none;">
                <ul id="ulMedicamentos" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6; margin-bottom: 5px;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            RESERVA DE MEDICAMENTOS</label>
                    </li>
                    <li style="clear: both; margin-left: 20px">
                        <ul>
                            <li style="width: 750px; text-align: center; text-transform: uppercase; margin-top: 0px;
                                background-color: #E6E6FA; margin-bottom: 0px;">
                                <label style="font-size: 1.1em; font-family: Tahoma;">
                                    MEDICAMENTOS RECEITADOS</label>
                                <asp:HiddenField runat="server" ID="hidCoReservaMedicamentos" />
                            </li>
                            <li>
                                <div id="div13" runat="server" class="divGridTelETA divGeralApresenta" style="height: 150px;">
                                    <asp:GridView ID="grdMedicReceitados" CssClass="grdBusca" Width="100%" runat="server"
                                        AutoGenerateColumns="False" DataKeyNames="CO_PROD" ToolTip="Grid apresentativa dos medicamentos receitados no Atendimento em questão">
                                        <RowStyle CssClass="rowStyle" />
                                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                        <EmptyDataTemplate>
                                            Nenhum registro encontrado.<br />
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="CK">
                                                <ItemStyle Width="15px" CssClass="grdLinhaCenter" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ckSelectResMedic" OnCheckedChanged="ckSelectResMedic_OnCheckedChanged"
                                                        AutoPostBack="true" runat="server" Enabled='<%# bind("HABIL_SELEC") %>' />
                                                    <asp:HiddenField ID="hidCoProd" runat="server" Value='<%# bind("CO_PROD") %>' />
                                                    <asp:HiddenField ID="hidNoProd" runat="server" Value='<%# bind("medicamento") %>' />
                                                    <asp:HiddenField ID="hidQTD" runat="server" Value='<%# bind("qtd") %>' />
                                                    <asp:HiddenField ID="hidUSO" runat="server" Value='<%# bind("uso") %>' />
                                                    <asp:HiddenField ID="hidIdReceitu" runat="server" Value='<%# bind("ID_RECEI") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="medicamento" HeaderText="MEDICAMENTO">
                                                <ItemStyle Width="220px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="qtd" HeaderText="QTD">
                                                <ItemStyle Width="20px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="uso" HeaderText="USO">
                                                <ItemStyle Width="20px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="prescricao" HeaderText="PRESCRIÇÃO">
                                                <ItemStyle Width="320px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="FP">
                                                <ItemStyle Width="15px" CssClass="grdLinhaCenter" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ckFP" runat="server" Checked='<%# bind("FP_V") %>' Enabled="false"
                                                        ToolTip="Se marcado, indica que o medicamento em questão se enquadra em Farmácia Popular" />
                                                    <asp:HiddenField runat="server" ID="hidQtEstoque" Value='<%# bind("ESTOQUE") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ESTOQUE" HeaderText="ESTOQUE">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </li>
                        </ul>
                    </li>
                    <li style="clear: both; margin: 20px 0 0 20px">
                        <ul>
                            <li class="liClear" style="margin: 2px 0 0 -4px">
                                <fieldset class="fldFiliaResp">
                                    <legend>MEDICAMENTO</legend>
                                    <ul>
                                        <li>
                                            <asp:TextBox runat="server" Width="45px" ID="txtMedicamento"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hidCoProdResMedic" />
                                            <asp:HiddenField runat="server" ID="hidNoGrupo" />
                                        </li>
                                        <li>
                                            <asp:ImageButton ID="btnPesqMed" OnClick="btnPesqMed_Click" Width="12px" runat="server"
                                                ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" CssClass="btnPesqMed" />
                                        </li>
                                        <li class="liPesqMed"><a class="lnkPesMed" href="#">
                                            <img class="imgPesMed" width="12px" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone" /></a>
                                        </li>
                                        <li>
                                            <asp:TextBox runat="server" Width="180px" ID="txtDescMedicamento"></asp:TextBox>
                                        </li>
                                    </ul>
                                </fieldset>
                            </li>
                            <%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
                            <li>
                                <label for="txtQTM1" title="QT M1">
                                    QT M1</label>
                                <asp:TextBox ID="txtQTM1" Width="30px" CssClass="txtQTM1" runat="server" ToolTip="QT M1">
                                </asp:TextBox>
                            </li>
                            <li>
                                <label for="txtQTM2" title="QT M2">
                                    QT M2</label>
                                <asp:TextBox ID="txtQTM2" Width="30px" CssClass="txtQTM2" runat="server" ToolTip="QT M2">
                                </asp:TextBox>
                            </li>
                            <li>
                                <label for="txtQTM3" title="QT M3">
                                    QT M3</label>
                                <asp:TextBox ID="txtQTM3" Width="30px" CssClass="txtQTM3" runat="server" ToolTip="QT M3">
                                </asp:TextBox>
                            </li>
                            <li>
                                <label for="txtQTM3" title="QT M4">
                                    QT M4</label>
                                <asp:TextBox ID="txtQTM4" Width="30px" CssClass="txtQTM4" runat="server" ToolTip="QT M4">
                                </asp:TextBox>
                            </li>
                            <li id="li5" runat="server" class="liBtnsETA" style="margin-top: 10px; margin-right: 6px !important">
                                <asp:LinkButton ID="lncIncMed" OnClick="lnkIncMed_Click" runat="server" Style="margin: 0 auto;"
                                    ToolTip="Incluir Registro">
                                    <img class="imgLnkInc" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa"
                                        title="Incluir Registro" />
                                    <asp:Label runat="server" ID="Label20" Text="Incluir"></asp:Label>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lncExcMed" OnClientClick="if (!confirm('Deseja realmente Excluir o registro?')) return false;"
                                    OnClick="lnkExcMed_Click" runat="server" Style="margin: 0 auto;" ToolTip="Excluir Registro">
                                    <img class="imgLnkExc" src='/Library/IMG/Gestor_BtnDel.png' alt="Icone Pesquisa"
                                        title="Excluir Registro" />
                                    <asp:Label runat="server" ID="Label22" Text="Excluir"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li class="liClear">
                                <div id="div2" runat="server" class="divGridTelETA divGeralApresenta" style="height: 150px;
                                    border: 1px solid #CCCCCC; overflow-y: scroll">
                                    <asp:GridView ID="grdMedicamentos" CssClass="grdBusca" Width="100%" runat="server"
                                        AutoGenerateColumns="False" DataKeyNames="CO_PROD">
                                        <RowStyle CssClass="rowStyle" />
                                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                        <EmptyDataTemplate>
                                            Nenhum registro encontrado.<br />
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="CK">
                                                <ItemStyle Width="15px" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ckSelect" runat="server" />
                                                    <asp:HiddenField ID="hidCoProd" runat="server" Value='<%# bind("CO_PROD") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Medicamento" HeaderText="MEDICAMENTO">
                                                <ItemStyle Width="220px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GRUPO" HeaderText="GRUPO">
                                                <ItemStyle Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QTM1" HeaderText="QT M1">
                                                <ItemStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QTM2" HeaderText="QT M2">
                                                <ItemStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QTM3" HeaderText="QT M3">
                                                <ItemStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QTM4" HeaderText="QT M3">
                                                <ItemStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TOTAL" HeaderText="TOTAL">
                                                <ItemStyle Width="30px" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </li>
                        </ul>
                    </li>
                    <li style="width: 727px">
                        <ul>
                            <li id="liEmissGuiaReserMedic" runat="server" class="liBtnAddA liPrima" style="margin-left: 20px !important;
                                margin-top: 10px !important; float: left;">
                                <asp:LinkButton ID="lnkEmissGuiaReserMedic" Enabled="false" runat="server" ValidationGroup="atuEndAlu"
                                    OnClick="lnkEmissGuiaReserMedic_OnClick" ToolTip="Imprime a Guia de Reserva de Medicamentos">
                                    <img id="img3" runat="server" width="14" height="14" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                                        alt="Icone Pesquisa" title="Imprime Receita Médica" />
                                    <asp:Label runat="server" ID="Label1" Text="GUIA DE RESERVA" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li id="li10" runat="server" class="liBtnAddA" style="margin-left: 355px !important;
                                float: right; margin-top: 10px">
                                <asp:LinkButton ID="lkbFinalizarMedicamento" OnClick="lkbFinalizarMedicamento_Click"
                                    runat="server">FINALIZAR</asp:LinkButton>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
            <div id="tabResConsultas" runat="server" clientidmode="Static" style="display: none;">
                <ul id="ul10" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6; margin-bottom: 5px;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            RESERVA DE CONSULTAS MÉDICAS</label>
                    </li>
                    <li class="liPrima" style="margin-left: 60px !important;">
                        <label for="txtNisUsuResCons" title="Número NIS do usuário selecionado">
                            N° NIS</label>
                        <asp:TextBox ID="txtNisUsuResCons" Enabled="false" runat="server" Text="0000000"
                            ToolTip="Número NIS do usuário selecionado" Width="45px">
                        </asp:TextBox>
                    </li>
                    <li>
                        <label for="txtNomeUsuResCons" title="Nome do usuário selecionado">
                            Nome</label>
                        <asp:DropDownList ID="ddlNomeUsuResCons" runat="server" ToolTip="Informe o usuário"
                            Width="280px">
                        </asp:DropDownList>
                        <%--                        <asp:TextBox ID="txtNomeUsuResCons" Enabled="false" runat="server" ToolTip="Nome do usuário selecionado" Width="280px">
                        </asp:TextBox>--%>
                    </li>
                    <li style="margin-left: 10px !important;">
                        <label for="ddlSexoUsuResCons" title="Sexo do usuário selecionado">
                            Sexo</label>
                        <asp:DropDownList ID="ddlSexoUsuResCons" Enabled="false" runat="server" ToolTip="Sexo do usuário selecionado"
                            Width="70px">
                            <asp:ListItem Value="M">Masculino</asp:ListItem>
                            <asp:ListItem Value="F">Feminino</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li style="margin-left: 10px !important;">
                        <label title="Data de nascimento e idade do usuário selecionado">
                            Nascimento/Idade</label>
                        <asp:TextBox ID="txtDtNascUsuResCons" runat="server" ToolTip="Data de nascimento do usuário selecionado"
                            Width="55px">
                        </asp:TextBox>
                        <asp:TextBox ID="txtIdadeUsuResCons" runat="server" ToolTip="Idade do usuário selecionado"
                            Width="20px">
                        </asp:TextBox>
                    </li>
                    <%--                    <li style="margin-left: 5px;">
                        <label for="txtAnoRegResCons" title="Ano">
                            Ano</label>
                        <asp:TextBox ID="txtAnoRegResCons" Enabled="false" Width="35px" CssClass="txtAnoMed" runat="server"  ToolTip="Ano">
                        </asp:TextBox>
                    </li>--%>
                    <%--                    <li style="margin-left: 5px; float: right; margin-right: 29px !important;">
                        <label for="txtRegResCons" title="Número do Registro">
                            Nº Registro</label>
                        <asp:TextBox ID="txtRegResCons" Enabled="false" CssClass="txtRegResCons" runat="server" Text="2014.XXXX.0000001"  ToolTip="Número do Registro" Width="100px">
                        </asp:TextBox>
                    </li>--%>
                    <li class="liPrima" style="margin-left: 60px !important; margin-top: 10px !important;">
                        <ul>
                            <li style="width: 415px;">
                                <%-- Grid de profissionais --%>
                                <ul>
                                    <li class="liTituloGrid" style="width: 420px; margin-right: 0px; background-color: #ffff99;">
                                        GRID DE PROFISSIONAIS </li>
                                    <li style="margin-top: 5px">
                                        <label for="ddlEspMedResCons" title="Selecione a especialidade médica solicitada pelo usuário">
                                            Especialidade Médica</label>
                                        <asp:DropDownList ID="ddlEspMedResCons" Width="200px" runat="server" ToolTip="Selecione a especialidade médica solicitada pelo usuário">
                                            <asp:ListItem Value="0">Todas</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: 5px">
                                        <label for="ddlUnidResCons" title="Selecione a unidade do médico">
                                            Unidade</label>
                                        <asp:DropDownList ID="ddlUnidResCons" Width="205px" runat="server" ToolTip="Selecione a unidade do médico">
                                            <asp:ListItem Value="0">Todas</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: 5px">
                                        <div id="divGrdProfi" runat="server" class="divGridData" style="height: 227px; width: 420px;
                                            overflow-y: scroll !important;">
                                            <asp:GridView ID="grdProfi" CssClass="grdBusca" runat="server" Style="width: 403px;
                                                height: 150px;" AutoGenerateColumns="false">
                                                <RowStyle CssClass="rowStyle" />
                                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                <EmptyDataTemplate>
                                                    Nenhum registro encontrado.<br />
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hidCoCol" Value='<%# Eval("CO_COL") %>' runat="server" />
                                                            <asp:CheckBox ID="ckSelect" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NO_COL" HeaderText="MÉDICO(A)">
                                                        <ItemStyle Width="223px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DE_ESP" HeaderText="ESPECIALIDADE">
                                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NO_EMP" HeaderText="UNIDADE">
                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </li>
                                </ul>
                                <%-- Grid de profissionais --%>
                            </li>
                            <li style="clear: none !important; width: 188px; margin-left: 25px !important;">
                                <%-- Grid de horário --%>
                                <ul>
                                    <li class="liTituloGrid" style="width: 100%; margin-right: 0px; background-color: #d2ffc2;">
                                        AGENDA DE HORÁRIOS </li>
                                    <li>
                                        <label>
                                            Intervalo de datas</label>
                                        <asp:TextBox ID="txtDtIniResCons" runat="server" CssClass="campoData">
                                        </asp:TextBox>
                                        até
                                        <asp:TextBox ID="txtDtFimResCons" runat="server" CssClass="campoData">
                                        </asp:TextBox>
                                    </li>
                                    <li class="liClear">
                                        <label>
                                            Tipo de Consulta</label>
                                        <asp:DropDownList ID="ddlTpCons" Style="margin: 0px;" runat="server">
                                            <asp:ListItem Value="">Selecione</asp:ListItem>
                                            <asp:ListItem Value="N">Normal</asp:ListItem>
                                            <asp:ListItem Value="R">Retorno</asp:ListItem>
                                            <asp:ListItem Value="U">Urgência</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="float: right;">
                                        <asp:CheckBox ID="chkHorDispResCons" runat="server" Style="display: inline; float: left;
                                            margin-top: 12px; margin-left: 10px;" />
                                        <div style="float: right; margin-top: 5px;">
                                            Horários
                                            <br />
                                            Disponíveis
                                        </div>
                                    </li>
                                    <li>
                                        <div id="div3" runat="server" class="divGridData" style="height: 226px; width: 187px;">
                                            <asp:GridView ID="grdHorario" CssClass="grdBusca" runat="server" Style="width: 170px;
                                                height: 50px;" AutoGenerateColumns="false">
                                                <RowStyle CssClass="rowStyle" />
                                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                <EmptyDataTemplate>
                                                    Nenhum registro encontrado.<br />
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemStyle Width="5px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckSelect" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="hr" HeaderText="DATA E HORA">
                                                        <ItemStyle Width="120px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="CONF">
                                                        <ItemStyle Width="5px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckConf" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </li>
                                </ul>
                                <%-- Grid de horário --%>
                            </li>
                        </ul>
                    </li>
                    <li id="li20" runat="server" class="liBtnAddA" style="margin-top: 20px !important;
                        margin-left: 245px !important; clear: none !important; background-color: #fafad2 !important;">
                        <asp:LinkButton ID="lnkAtualizaEndAlu" runat="server" ValidationGroup="atuEndAlu">
                            <img id="img4" runat="server" width="14" height="14" class="imgliLnk" src='/Library/IMG/Gestor_CheckSucess.png'
                                alt="Icone Pesquisa" title="Efetivar Reserve de Consulta" />
                            <asp:Label runat="server" ID="Label9" Text="EFETIVAR" Style="margin-left: 4px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                    <li id="li2" runat="server" class="liBtnAddA" style="margin-top: 20px !important;
                        margin-left: 5px !important; clear: none !important;">
                        <asp:LinkButton ID="lnkImpriProtoc" Enabled="false" runat="server" ValidationGroup="atuEndAlu">
                            <img id="imgImpriProtoc" runat="server" width="14" height="14" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                                alt="Icone Pesquisa" title="Imprime Protocolo da Reserva de Consulta" />
                            <asp:Label runat="server" ID="Label10" Text="PROTOCOLO" Style="margin-left: 4px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                    <li id="li13" runat="server" class="liBtnAddA" style="margin-top: 20px !important;
                        margin-left: 5px !important; clear: none !important;">
                        <asp:LinkButton ID="lnkImpriGuiaMed" Enabled="false" runat="server" ValidationGroup="atuEndAlu">
                            <img id="imgImpriGuiaMed" runat="server" width="14" height="14" class="imgliLnk"
                                src='/Library/IMG/Gestor_IcoImpres.ico' alt="Icone Pesquisa" title="Imprime Guia Médica" />
                            <asp:Label runat="server" ID="Label26" Text="GUIA MED" Style="margin-left: 4px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE SELEÇÃO DO PACIENTE
<!-- =========================================================================================================================================================================== -->
            <%--<asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick">
                    </asp:Timer>
            <asp:UpdatePanel ID="updSelecPaci" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger EventName="Tick" ControlID="Timer1" />
            </Triggers>
            <ContentTemplate>--%>
            <div id="tabSelPacien" runat="server" clientidmode="Static">
                <asp:HiddenField runat="server" ID="hidCoPac" />
                <asp:HiddenField runat="server" ID="hidCoResp" />
                <asp:HiddenField runat="server" ID="hidIdOper" />
                <asp:HiddenField runat="server" ID="hidIdPlano" />
                <asp:HiddenField runat="server" ID="hidIdEncam" />
                <asp:HiddenField runat="server" ID="hidIdEspec" />
                <asp:HiddenField runat="server" ID="hidCoTpRisco" />
                <asp:HiddenField runat="server" ID="hidAbaAberta" />
                <ul id="ul26" class="ulDados2">
                <li style="width: 98.5%; text-align: center; height: 18px; background-color: #FDF5E6; padding-top:6px; padding-right:6px; margin-top:-29px;">
                    <div>
                                <div style="float: left;">
                                    <asp:Label ID="Label47" runat="server" Style="font-size: 10px; font-family: Tahoma;
                                        vertical-align: middle; margin-left: 4px !important; font-weight:bold; color:#4682b4">
                                                DEMONSTRATIVO DE ATENDIMENTO MÉDICO A PACIENTES</asp:Label>
                                </div>
                                <div style="float:right">
                                           
                                </div>
                    </div>
                </li>
                    <%--<li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            SELEÇÃO DO PACIENTE</label>

                    </li>--%>
                    <li style="clear: both; margin-top: 5px;">
                        <%--<div class="divPaciEnca" style="display:none; margin-left: 5px" ClientIDMode="static">--%>
                        <div id="divPaciEnca" runat="server" clientidmode="static">

                            <%--<div style="width: 100%; text-align: center; text-transform: uppercase; background-color: #90EE90;">
                                <ul>
                                    <li style="float:left">
                                    <label style="font-size: 1.1em; font-family: Tahoma;">
                                        RELAÇÃO DE PACIENTES - DIRECIONADOS PELO PRÉ-ATENDIMENTO</label>
                                    </li>
                                </ul>
                            </div>--%>
                                <ul style="margin:20px 0 0 10px;">
                                <li style="margin-bottom: -6px;">
                                                <label class="lblTop">
                                                    UNIDADE DE ATENDIMENTO</label>
                                            </li>
                                            <li style="clear:both">
                                                <asp:DropDownList runat="server" ID="ddlUnidAtend"></asp:DropDownList>
                                            </li>
                                            <li style="margin-bottom: -6px;">
                                                <label class="lblTop">
                                                    DADOS DO PACIENTE - USUÁRIO DE SAÚDE</label>
                                            </li>
                                            <li style="margin: 9px 3px 0 0px; clear: both"><a class="lnkPesNIRE" href="#">
                                                <img class="imgPesPac" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade"
                                                    style="width: 17px; height: 17px;" /></a> </li>
                                            <li>
                                                <label>
                                                    Nº PRONTUÁRIO</label>
                                                <asp:CheckBox runat="server" ID="chkPesqNuNisPac" Style="margin-left: -5px" OnCheckedChanged="chkPesqNuNisPac_OnCheckedChanged"
                                                    AutoPostBack="true" />
                                                <asp:TextBox runat="server" ID="txtPesqNuNisPaci" Width="70" Style="margin-left: -6px"></asp:TextBox>
                                            </li>
                                            <li>
                                                <label>Nº Cartão de Saúde</label>
                                                <asp:CheckBox runat="server" ID="chkPesqNuCarPac" Style="margin-left: -5px" OnCheckedChanged="chkPesqNuCarPac_OnCheckedChanged"
                                                    AutoPostBack="true" />
                                                <asp:TextBox runat="server" ID="txtPesqNuCartaoSaude" Style="margin-left: -6px; width:120px !important;" ></asp:TextBox>
                                            </li>
                                            <li>
                                                <label>
                                                    CPF</label>
                                                <asp:CheckBox runat="server" ID="chkPesqCPFUsu" OnCheckedChanged="chkPesqCPFUsu_OnCheckedChanged" AutoPostBack="true" />
                                                <asp:TextBox runat="server" ID="txtPesqCpfPaci" CssClass="campoCpf" Width="75px" Style="margin-left: -6px" ></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="HiddenField1" />
                                            </li>
                                            <li style="margin-left: -1px; margin-top: 12px;">
                                                <asp:ImageButton ID="imbPesqPaci" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                    Width="13px" Height="13px" OnClick="imbPesqPaci_OnClick" />
                                            </li>
                                             <li style="margin: 10px -3px 0 15px;">
                                             <asp:ImageButton ID="imgCadPac" runat="server" ImageUrl="~/Library/IMG/PGN_IconeTelaCadastro2.png"
                                    OnClick="imgCadPac_OnClick" Style="width: 18px !important; height: 17px !important;"
                                    ToolTip="Cadastro de Pacientes" />
                                            </li>
                                            <li style="margin-left:10px;">
                                                <label class="lblObrigatorio">
                                                    Nome</label>
                                                    <asp:DropDownList runat="server" ID="ddlNomPacienteSelec" Width="260px" OnSelectedIndexChanged="ddlNomPacienteSelec_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </li>
                                            <li style="clear:both; margin-bottom: -6px; margin-top:20px;">
                                                <label class="lblTop">
                                                    DADOS DO PROFISSIONAL DE SAÚDE</label>

                                            </li>
                                            <li style="clear:both">
                                                <label>Unidade</label>
                                                <asp:DropDownList runat="server" ID="ddlUnidPesqProfi" OnSelectedIndexChanged="ddlUnidPesqProfi_OnSelectedIndexChanged" AutoPostBack="true" Width="210px"></asp:DropDownList>
                                            </li>
                                            <li>
                                                <label>Prof. Saúde</label>
<asp:DropDownList runat="server" ID="ddlProfSaude" OnSelectedIndexChanged="ddlProfSaude_OnSelectedIndexChanged" AutoPostBack="true" Width="250px"></asp:DropDownList>
                                            </li>
                              </ul>
                        </div>
                    </li>
                    <li style="clear: both; display:none">
                        <asp:HiddenField runat="server" ID="hidCoConsul" />
                        <div class="divConsul" id="divConsul" clientidmode="Static">
                            <%--<div style="width: 100%; text-align: center; text-transform: uppercase; background-color: #99FF99;">
                                <label style="font-size: 1.1em; font-family: Tahoma;">
                                    Consultas Médicas</label>
                            </div>--%>
                            <div style="width: 100%; text-align: center; height: 17px; background-color: #99FF99;">
                                <div style="float: left;">
                                    <asp:Label ID="Label46" runat="server" Style="font-size: 1.1em; font-family: Tahoma;
                                        vertical-align: middle; margin-left: 4px !important;">
                                                RELAÇÃO DE PACIENTES - AGENDAMENTO PRÉVIO DE CONSULTAS</asp:Label>
                                </div>
                            </div>
                            <asp:GridView ID="grdAgendConsulMedic" CssClass="grdBusca" runat="server" Style="width: 100%;
                                height: 15px;" AutoGenerateColumns="false" ToolTip="Seleção do paciente com consulta marcada para o qual será feito o atendimento (Registros em vermelho estão atrasados)">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhuma Consulta Médica em aberto<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkselectCon" runat="server" AutoPostBack="true" OnCheckedChanged="chkselectCon_OnCheckedChanged" />
                                            <asp:HiddenField runat="server" ID="hidCoHr" Value='<%# Eval("hr_Consul") %>' />
                                            <asp:HiddenField ID="hidAntigos" Value='<%# Eval("ANTIGOS") %>' runat="server" />
                                            <asp:HiddenField runat="server" ID="hidCoConsul" Value='<%# Eval("CO_AGEND_MEDIC") %>' />
                                            <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# Eval("CO_ALU") %>' />
                                            <asp:HiddenField runat="server" ID="hidCoCol" Value='<%# Eval("CO_COL") %>' />
                                            <asp:HiddenField ID="hidCoEspec" Value='<%# Eval("CO_ESPEC") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="hora" HeaderText="DATA/HORA">
                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_PAC" HeaderText="NOME DO PACIENTE">
                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CO_SEXO" HeaderText="SX">
                                        <ItemStyle HorizontalAlign="Left" Width="40px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NU_IDADE" HeaderText="ID">
                                        <ItemStyle HorizontalAlign="Left" Width="40px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_ESPEC" HeaderText="ESPECIALIDADE">
                                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TP_CONSUL_VALID" HeaderText="TP. CONSUL.">
                                        <ItemStyle HorizontalAlign="Left" Width="70px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CO_SITU_VALID" HeaderText="STATUS">
                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="CONF">
                                        <ItemStyle Width="5px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckConf" runat="server" Checked='<%# Eval("FL_CONF_VALID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="INFOS_PLANO_SAUDE" HeaderText="PLANO DE SAÚDE">
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>--%>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                          TERMINO DA TELA DE SELEÇÃO DO PACIENTE
<!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE ATENDIMENTO MÉDICO                                                                                    -->
            <!-- =========================================================================================================================================================================== -->
            <%--<asp:UpdatePanel runat="server" ID="updAtenMedic" UpdateMode="Conditional">
            <ContentTemplate>--%>
            <div id="tabAtendMedic" runat="server" clientidmode="Static" style="display: none">
                <%--<asp:HiddenField ID="hidIdPreAtend" runat="server" Value="0" />--%>
                <asp:HiddenField ID="hidIdAtendimentoMedico" runat="server" />
                <asp:HiddenField ID="hidIdDiagnostico" runat="server" />
                <asp:HiddenField runat="server" ID="hidCoPreAtend" />
                <ul id="ul11" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            ATENDIMENTO MÉDICO</label>
                    </li>
                    <li class="liPrima" style="margin-top: -12px !important; display: none;">
                        <label title="Informe a data do atendimento">
                            Data</label>
                        <asp:TextBox ID="txtDataAtendMed" runat="server" CssClass="campoData" ToolTip="Informe a data do atendimento">
                        </asp:TextBox>
                    </li>
                    <%--<asp:UpdatePanel ID="uppNisUsuAteMed" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                    <li style="margin-top: -12px !important; display: none;">
                        <label for="ddlNomeUsuAteMed" title="Nome do usuário selecionado">
                            Nome</label>
                        <asp:DropDownList ID="ddlNomeUsuAteMed" OnSelectedIndexChanged="ddlNomeUsuAteMed_SelectedIndexChanged"
                            AutoPostBack="true" runat="server" ToolTip="Informe o usuário" Width="280px">
                        </asp:DropDownList>
                        <%--                    <asp:TextBox ID="txtNomeUsuAteMed" Enabled="false" runat="server" ToolTip="Nome do usuário selecionado" Width="280px">
                            </asp:TextBox>--%>
                    </li>
                    <li style="margin-top: -12px !important; display: none;">
                        <label for="txtNisUsuAteMed" title="Número NIS do usuário selecionado">
                            N° NIS</label>
                        <asp:TextBox ID="txtNisUsuAteMed" Enabled="false" runat="server" ToolTip="Número NIS do usuário selecionado"
                            Width="90px">
                        </asp:TextBox>
                    </li>
                    <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                    <li style="margin-left: 138px; margin-top: -12px !important; display: none;">
                        <label for="txtAnoRegAteMed" title="Ano">
                            Ano</label>
                        <asp:TextBox ID="txtAnoRegAteMed" Enabled="false" Width="35px" CssClass="txtAnoRegAteMed"
                            runat="server" ToolTip="Ano">
                        </asp:TextBox>
                    </li>
                    <li style="margin-left: 5px; margin-top: -12px !important; display: none;">
                        <label for="txtRegResCons" title="Número do Registro">
                            Nº Registro</label>
                        <asp:TextBox ID="txtRegAteMed" Enabled="false" CssClass="txtRegAteMed" runat="server"
                            ToolTip="Número do Registro" Width="65px">
                        </asp:TextBox>
                    </li>
                    <li class="liClear" style="margin-top: -7px !important;">
                        <ul>
                            <li>
                                <ul>
                                    <li class="liPrima" style="font-weight: bolder; color:#4682b4; margin:-6px 0 5px 8px !important; font-size:10px;">INFORMAÇÕES DO ACOLHIMENTO DO PACIENTE
                                        - PRÉ-ATENDIMENTO </li>
                                    <li id="Li1" class="liMarginLeft" style="margin-top: 2px !important;" runat="server" visible="false">
                                        <label title="Data de nascimento e idade do usuário selecionado">
                                            Nascimento/Idade</label>
                                        <asp:TextBox ID="txtDtNascUsuAteMed" Enabled="true" runat="server" ToolTip="Data de nascimento do usuário selecionado"
                                            Width="55px">
                                        </asp:TextBox>
                                        <asp:TextBox ID="txtIdadeUsuAteMed" Enabled="true" runat="server" ToolTip="Idade do usuário selecionado"
                                            Width="20px">
                                        </asp:TextBox>
                                    </li>
                                    <li id="Li9" style="margin-top: 5px !important;" runat="server" visible="false">
                                        <label class="lblObrigatorio">
                                            Classf. Risco</label>
                                        <asp:DropDownList runat="server" ID="ddlClassRisco" Width="97px" ClientIDMode="Static">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="clear: both">
                                        <div class="divLeitura">
                                            <ul class="ulLeitura">
                                                <li style="clear: both;">
                                                    <ul>
                                                        <li class="liFotoColab">
                                                            <fieldset class="fldFotoColab">
                                                                <uc1:ControleImagem ID="upImagemAluno" runat="server" />
                                                            </fieldset>
                                                        </li>
                                                        <li style="margin:3px 0 0 15px !important; clear:both">
                                                           <ul>
                                                                <li>
                                                                    <asp:Label runat="server" ID="lblDtNascPaci"></asp:Label>
                                                                </li>
                                                                <li style="clear:both; margin:1px 0 0 2.4px !important;">
                                                               <asp:Label runat="server" ID="lblIdadeCalc" style="color:#ff4500; font-weight:bold; font-size:12px;"></asp:Label>
                                                                </li>
                                                            </ul>
                                                        </li>
                                                    </ul>
                                                </li>
                                                <li style="width:83px; height:133px; border-right:1px solid #CCCCCC;">
                                                    <ul>
                                                        <li class="lblsub" style="margin: -4px 0 5px 0px !important; clear: none !important;">
                                                            <asp:Label runat="server" ID="Label23" class="lblTituGr">Leitura</asp:Label>
                                                        </li>
                                                        <li style="clear: both">
                                                            <label>
                                                                Altura</label>
                                                            <asp:TextBox runat="server" ID="txtAltuUsuPreAtend" Width="30px" CssClass="campoAltu"></asp:TextBox>
                                                        </li>
                                                        <li style="margin-left: 4px">
                                                            <label>
                                                                Peso</label>
                                                            <asp:TextBox runat="server" ID="txtPesoUsuPreAtend" Width="30px" CssClass="campoPeso"></asp:TextBox>
                                                        </li>
                                                        <li style="clear: both">
                                                            <label>
                                                                Pressão (Val/HR)</label>
                                                            <asp:TextBox runat="server" ID="txtPresUsuPreAtend" Width="30px" CssClass="campoPressArteri"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtHoraPressArt" Width="30" CssClass="campoHora"
                                                                Style="margin-left: 6px"></asp:TextBox>
                                                        </li>
                                                        <li style="clear: both">
                                                            <label>
                                                                Temper (Val/HR)</label>
                                                            <asp:TextBox runat="server" ID="txtTemper" Width="30px" CssClass="campoGrau"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtHoraTemper" Width="30" CssClass="campoHora" Style="margin-left: 6px"></asp:TextBox>
                                                        </li>
                                                        <li style="clear: both">
                                                            <label>
                                                                Glicem (Val/HR)</label>
                                                            <asp:TextBox runat="server" ID="txtGlicem" Width="30px" CssClass="campoGlicem"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtHoraGlicem" Width="30" CssClass="campoHora" Style="margin-left: 6px"></asp:TextBox>
                                                        </li>
                                                    </ul>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="divRegRisco">
                                            <ul>
                                                <li class="lblsub">
                                                    <asp:Label runat="server" ID="Label24" class="lblTituGr">Registro de Risco</asp:Label>
                                                </li>
                                                <li id="divRisco1" style="margin-left: -0px !important; clear: both">
                                                    <ul class="ulRisco">
                                                        <li style="margin-top: 0px !important;">
                                                            <label>
                                                                Diabetes</label>
                                                            <asp:CheckBox runat="server" ID="chkDiabetsPreAtend" class="chkItens" />
                                                            <asp:DropDownList runat="server" ID="ddlDiabetsPreAtend" Width="60px" class="chkAreasChk">
                                                                <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="Tipo 1" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Tipo 2" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </li>
                                                        <li style="clear: both; margin-top: -2px !important;">
                                                            <label>
                                                                Hipertensão</label>
                                                            <asp:CheckBox runat="server" ID="chkHipertPreAtend" class="chkItens" />
                                                            <asp:TextBox runat="server" ID="txtHiperPreAtend" Width="86px" class="chkAreasChk"
                                                                MaxLength="20">
                                                            </asp:TextBox>
                                                        </li>
                                                        <li style="clear: both; margin-top: -2px !important;">
                                                            <label>
                                                                Fumante (St/Anos)</label>
                                                            <asp:DropDownList runat="server" ID="ddlFumanPreAtend" Width="80px">
                                                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                                                <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Ex-Fumante" Value="E"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox runat="server" ID="txtTempFumanPreAtend" Width="20px"></asp:TextBox>
                                                        </li>
                                                        <li style="clear: both; margin-top: -2px !important;">
                                                            <label>
                                                                Alcool (St/Anos)</label>
                                                            <asp:DropDownList runat="server" ID="ddlAlcooPreAtend" Width="80px">
                                                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                                                <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="As Vezes" Value="V"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox runat="server" ID="txtTempAlcooPreAtend" Width="20px"></asp:TextBox>
                                                        </li>
                                                    </ul>
                                                </li>
                                                <li style="margin: 0px 0 0 -1px;" id="divRisco2">
                                                    <ul>
                                                        <li>
                                                            <label>
                                                                Cirurgia</label>
                                                            <asp:CheckBox runat="server" ID="chkCirurPreAtend" class="chkItens" />
                                                            <asp:TextBox runat="server" ID="txtCirurPreAtend" Width="174px" class="chkAreasChk"
                                                                MaxLength="50">
                                                            </asp:TextBox>
                                                        </li>
                                                        <li style="clear: both; margin-top: -2px; margin-bottom: -2px;">
                                                            <label>
                                                                Alergia</label>
                                                            <asp:CheckBox runat="server" ID="chkAlergia" class="chkItens" />
                                                            <asp:TextBox runat="server" ID="txtAlergia" Width="174px" class="chkAreasChk" MaxLength="40">
                                                            </asp:TextBox>
                                                        </li>
                                                        <li style="clear: both;">
                                                            <label>
                                                                Marcapasso</label>
                                                            <asp:CheckBox runat="server" ID="chkMarcPass" class="chkItens" />
                                                            <asp:TextBox runat="server" ID="txtMarcPass" Width="69px" class="chkAreasChk" MaxLength="20">
                                                            </asp:TextBox>
                                                        </li>
                                                        <li style="margin-left:9px;">
                                                            <label>
                                                                Válvula</label>
                                                            <asp:CheckBox runat="server" ID="chkValvulas" class="chkItens" />
                                                            <asp:TextBox runat="server" ID="txtValvula" Width="69px" class="chkAreasChk" MaxLength="30">
                                                            </asp:TextBox>
                                                        </li>
                                                        <li style="clear: both; margin-top:-2px;">
                                                            <label>
                                                                Teve Enjôos?</label>
                                                            <asp:DropDownList runat="server" ID="ddlEnjoo" Width="50px">
                                                                <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                                                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </li>
                                                        <li style="margin-left: 4px; margin-top:-2px;">
                                                            <label>
                                                                Teve Vômitos?</label>
                                                            <asp:DropDownList runat="server" ID="ddlVomito" Width="50px">
                                                                <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                                                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </li>
                                                        <li style="margin-left: 1px; margin-top:-2px;">
                                                            <label>
                                                                Teve Febre?</label>
                                                            <asp:DropDownList runat="server" ID="ddlFebre" Width="50px">
                                                                <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                                                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </li>
                                                    </ul>
                                                </li>
                                                <li style="margin: 0px 0 0 10px;">
                                                    <ul>
                                                        <li>
                                                            <label>
                                                                Dores?</label>
                                                            <asp:DropDownList runat="server" ID="ddlDores" Width="63px">
                                                                <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                                                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                                                <asp:ListItem Text="As Vezes" Value="A"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </li>
                                                        <li style="margin-left: 7px;">
                                                            <label>
                                                                Data</label>
                                                            <asp:TextBox runat="server" ID="txtDtDor" CssClass="campoData"></asp:TextBox>
                                                        </li>
                                                        <li style="margin-left: 2px;">
                                                            <label>
                                                                Hora</label>
                                                            <asp:TextBox runat="server" ID="txtHrDor" CssClass="campoHora"></asp:TextBox>
                                                        </li>
                                                        <li style="clear: both; margin-top: -2px !important;" class="liDores">
                                                            <label>
                                                                Classificação da Dor</label>
                                                            <asp:TextBox runat="server" ID="txtClassDor" TextMode="MultiLine" Width="200px" Height="73px"></asp:TextBox>
                                                        </li>
                                                    </ul>
                                                </li>
                                            </ul>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                        <li id="divMedicacao">
                            <ul>
                                <li class="lblsub" style="margin-bottom:-1px">
                                    <asp:Label runat="server" ID="Label25" class="lblTituGr">Medicação</asp:Label>
                                </li>
                                <li style="clear: both">
                                    <label title="Medicação de uso contínuo do(a) paciente">
                                        Medicação de Uso Contínuo</label>
                                    <asp:TextBox runat="server" ID="txtMedicUsoContiPreAtend" TextMode="MultiLine" Style="width: 190px;
                                        height: 45px;" MaxLength="200" ToolTip="Medicação de uso contínuo do(a) paciente"></asp:TextBox>
                                </li>
                                <li style="margin-left:10px;">
                                    <label title="Medicação administrada no acolhimento no(a) paciente">
                                        Medicação
                                    </label>
                                    <asp:TextBox runat="server" ID="txtMedicacaoAdmin" TextMode="MultiLine" Style="width: 180px;
                        height: 45px;" MaxLength="200" ToolTip="Medicação administrada no acolhimento no(a) paciente"></asp:TextBox>
                                      </li>
                                  </ul>
                              </li>
                              <li id="divInfosPrev">
                                  <ul>
                                      <li class="lblsub" style="margin-bottom: -1px;">
                                          <asp:Label runat="server" ID="Label44" class="lblTituGr">Informações Prévias</asp:Label>
                                      </li>
                                      <li style="clear: both;">
                                          <label>
                                              Sintomas</label>
                                          <asp:TextBox runat="server" ID="txtSintomas" TextMode="MultiLine" Style="width: 325px;
                                              height: 45px;" MaxLength="200"></asp:TextBox><br />
                                      </li>
                                  </ul>
                              </li>
                            <li style="clear:both;">
                                <ul>
                                    <li style="margin:-8px 0 -5px 10px;">
                                        <label style="font-weight:bolder; color:#ff8c00;">INFORMAÇÕES DA CONSULTA MÉDICA</label>
                                    </li>
                                    <li class="liPrima" style="clear:both; margin-top:4px;">
                                        <label title="Informe a Anamnese completa">
                                            Anamnese</label>
                                        <asp:TextBox ID="txtAnamAtendMed" runat="server" TextMode="MultiLine" Height="114px"
                                            Width="350px" ToolTip="Informe a Anamnese completa" CssClass="fontAumen">
                                        </asp:TextBox>
                                    </li>
                                    <li style="width:400px;">
                                    <ul>
                                    <li style="margin: 4px 0 0 10px !important;">
                                        <label title="Informe o Diagnóstico completo" class="lblObrigatorio">
                                            Diagnóstico</label>
                                        <asp:TextBox ID="txtDiagAtendMed" runat="server" TextMode="MultiLine" Height="40px"
                                            Width="370px" ToolTip="Informe o Diagnóstico completo" CssClass="fontAumen">
                                        </asp:TextBox>
                                    </li>
                                    <li style="margin: 7px 0 0 10px !important; clear:both;">
                                    <ul>
                                    <li>                                  
                                        <label title="Informe o CID da doença diagnosticada no paciente">CID<span style="color:Red">*</span> - Código Internacional de Doença atribuído ao Paciente</label>
                                    </li>
                                    <li class="lnkPesqCID" style="margin-top: 0px; clear:both"><a class="lnkPesqCID" href="#">
                                        <img class="imgPesMed" width="16px" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone" /></a>
                                    </li>
                                    <li>
                                        <asp:TextBox ID="txtCidAtendMed" runat="server" Width="50px" ToolTip="Informe o CID da doença diagnosticada no paciente">
                                        </asp:TextBox>
                                    </li>
                                    <li style="margin-top: 0px; margin-left: 0px;">
                                        <asp:ImageButton ID="imgPesqCID" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                            ToolTip="Realiza uma busca na base pelo CID informado no campo anterior." OnClick="imgPesqCID_OnClick" />
                                    </li>
                                    <li>
                                        <asp:TextBox runat="server" ID="txtDescCID" Width="274px" ToolTip="Descrição do CID"></asp:TextBox>
                                    </li>
                                    <li class="lnkPesqCID" style="margin-top: 0px; clear:both"><a class="lnkPesqCID" href="#">
                                        <img class="imgPesMed" width="16px" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone" /></a>
                                    </li>
                                    <li>
                                        <asp:TextBox ID="txtCidAtendMed2" runat="server" Width="50px" ToolTip="Informe o CID da doença diagnosticada no paciente">
                                        </asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:ImageButton ID="imgPesqCID2" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                            ToolTip="Realiza uma busca na base pelo CID informado no campo anterior." OnClick="imgPesqCID2_OnClick" />
                                    </li>
                                    <li>
                                        <asp:TextBox runat="server" ID="txtDescCID2" Width="274px" ToolTip="Descrição do CID"></asp:TextBox>
                                    </li>
                                    <li class="lnkPesqCID" style="margin-top: 0px; clear:both"><a class="lnkPesqCID" href="#">
                                        <img class="imgPesMed" width="16px" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone" /></a>
                                    </li>
                                    <li>
                                        <asp:TextBox ID="txtCidAtendMed3" runat="server" Width="50px" ToolTip="Informe o CID da doença diagnosticada no paciente">
                                        </asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:ImageButton ID="imgPesqCID3" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                            ToolTip="Realiza uma busca na base pelo CID informado no campo anterior." OnClick="imgPesqCID3_OnClick" />
                                    </li>
                                    <li>
                                        <asp:TextBox runat="server" ID="txtDescCID3" Width="274px" ToolTip="Descrição do CID"></asp:TextBox>
                                    </li>
                                    </ul>
                                    </li>
                                    </ul>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                    <li id="li16" runat="server" class="liBtnAddA liPrima" style="margin-left: 270px !important;
                        margin-top: 10px !important; clear: none !important; background-color: #fafad2 !important;">
                        <asp:LinkButton ID="lnkEfetAtendMed" OnClick="lnkEfetAtendMed_Click" runat="server"
                            ValidationGroup="atuEndAlu">
                            <img id="img5" runat="server" width="14" height="14" class="imgliLnk" src='/Library/IMG/Gestor_CheckSucess.png'
                                alt="Icone Pesquisa" title="Efetivar Atendimento Médico" />
                            <asp:Label runat="server" ID="Label31" Text="EFETIVAR" Style="margin-left: 4px;"></asp:Label>
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
                    <li id="li28" runat="server" class="liBtnAddA liPrima" style="clear: none !important;
                        display: none; background-color: #fafad2 !important; margin-right: 25px !important;
                        float: right;">
                        <asp:LinkButton ID="lnkResExames" runat="server" ValidationGroup="atuEndAlu">
                            <asp:Label runat="server" ID="Label36" Text="RESULT. EXAMES" Style="margin-left: 4px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE ATENDIMENTO MÉDICO                                                                                    -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE RECEITA MÉDICA                                                                                        -->
            <!-- =========================================================================================================================================================================== -->
            <%--<asp:UpdatePanel runat="server" ID="updReceiMedic" UpdateMode="Conditional">
            <ContentTemplate>--%>
            <div id="tabReqMedic" runat="server" clientidmode="static" style="display: none;">
                <ul id="ul12" class="ulDados2" style="margin-left: 110px !important;">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6; margin-left: -110px !important;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            RECEITA MÉDICA</label>
                    </li>
                    <li style="margin-left: 50px">
                        <ul>
                            <li style="margin-left: 55px;" class="liPrima">
                                <label>
                                    Medicamento</label>
                            </li>
                            <li style="margin-left: 3px !important;">
                                <asp:TextBox ID="txtCodMedic" runat="server" Width="40px" ToolTip="Informe o medicamento">
                                </asp:TextBox>
                                <asp:HiddenField runat="server" ID="hidCoMedic" />
                            </li>
                            <li>
                                <asp:ImageButton ID="imgCoMedic" runat="server" Width="12px" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                    OnClick="imgCoMedic_OnClick" />
                            </li>
                            <li class="liPesqMed"><a class="lnkPesMed" href="#">
                                <img class="imgPesMed" width="12px" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone" /></a>
                            </li>
                            <li>
                                <asp:TextBox runat="server" ID="txtdescMedic" Width="220px"></asp:TextBox>
                            </li>
                            <li style="margin-left: 38px; margin-top: 6px !important;" class="liPrima">
                                <label>
                                    Princípio Ativo</label>
                            </li>
                            <li style="margin-top: 6px !important;">
                                <asp:TextBox ID="txtPrincAtivMedc" runat="server" Enabled="false" Width="306px" ToolTip="Informe o pricípio ativo">
                                </asp:TextBox>
                            </li>
                            <li class="liPrima">
                                <label title="Informe a quantidade utilizada do medicamento">
                                    Qtd</label>
                                <asp:TextBox ID="txtQtdMedcaAtendMed" class="txtQtdMedcaAtendMed" runat="server"
                                    Width="25px" ToolTip="Informe a quantidade utilizada do medicamento">
                                </asp:TextBox>
                            </li>
                            <li>
                                <label title="Informe a quantidade de dias de uso do medicamento, sendo 0 como Uso Contínuo"
                                    class="lblObrigatorio">
                                    Uso</label>
                                <asp:TextBox ID="txtDiasUsoMedcaAtendMed" class="txtDiasUsoMedcaAtendMed" runat="server"
                                    Width="25px" ToolTip="Informe a quantidade de dias de uso do medicamento, sendo 0 como Uso Contínuo">
                                </asp:TextBox>
                            </li>
                            <li>
                                <label title="Informe a prescrição médica do medicamento">
                                    Prescrição</label>
                                <asp:TextBox ID="txtPrescMedicaAtendMed" runat="server" Width="310px" ToolTip="Informe a prescrição médica do medicamento"
                                    MaxLength="180">
                                </asp:TextBox>
                            </li>
                            <li id="li14" runat="server" style="margin-top: 10px !important; margin-left: 10px !important;">
                                <asp:LinkButton ID="lnkIncMedicaGrid" runat="server" Style="margin: 0 auto;" ToolTip="Incluir Medicamento na Grid"
                                    OnClick="lnkIncMedicaGrid_OnClick">
                                    <img class="imgLnkInc" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa"
                                        title="Incluir Registro" />
                                    <asp:Label runat="server" ID="Label27" Text="Incluir"></asp:Label>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lnkDelMedicaGrid" OnClientClick="if (!confirm('Deseja realmente Excluir o registro?')) return false;"
                                    runat="server" Style="margin: 0 auto;" ToolTip="Excluir Medicamento da Grid"
                                    OnClick="lnkDelMedicaGrid_OnClick">
                                    <img class="imgLnkExc" src='/Library/IMG/Gestor_BtnDel.png' alt="Icone Pesquisa"
                                        title="Excluir Registro" />
                                    <asp:Label runat="server" ID="Label28" Text="Excluir"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </li>
                    <li class="liPrima" style="margin-top: 5px !important; margin-left: -95px !important;">
                        <div id="div6" runat="server" class="divGridTelETA divGeralApresenta">
                            <asp:GridView ID="grdMedcAtendMed" CssClass="grdBusca" Width="100%" runat="server"
                                AutoGenerateColumns="False" DataKeyNames="CO_PROD" ToolTip="Grid apresentativa dos medicamentos receitados no Atendimento em questão">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum registro encontrado.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="CK">
                                        <ItemStyle Width="15px" CssClass="grdLinhaCenter" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckSelect" runat="server" />
                                            <asp:HiddenField ID="hidCoProd" runat="server" Value='<%# bind("CO_PROD") %>' />
                                            <asp:HiddenField ID="hidIdReceitu" runat="server" Value='<%# bind("ID_RECEI") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="medicamento" HeaderText="MEDICAMENTO">
                                        <ItemStyle Width="220px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="qtd" HeaderText="QTD">
                                        <ItemStyle Width="20px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="uso" HeaderText="USO">
                                        <ItemStyle Width="20px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="prescricao" HeaderText="PRESCRIÇÃO">
                                        <ItemStyle Width="320px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                    <li style="width: 660px !important;">
                        <ul>
                            <li id="li18" runat="server" class="liBtnAddA liPrima" style="clear: both !important;
                                margin-left: -95px !important; margin-top: 10px !important; float: left;">
                                <asp:LinkButton ID="lnkImpReceita" Enabled="false" runat="server" ValidationGroup="atuEndAlu"
                                    OnClick="lnkImpReceita_OnClick" ToolTip="Imprimir a guia de exame">
                                    <img id="img7" runat="server" width="14" height="14" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                                        alt="Icone Pesquisa" title="Imprime Receita Médica" />
                                    <asp:Label runat="server" ID="Label33" Text="EMISSÃO DA RECEITA" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li id="li30" runat="server" class="liBtnAddA " style="margin-top: 10px !important;
                                float: right">
                                <asp:LinkButton ID="lnkFinalReceit" runat="server" OnClick="lnkFinalReceit_OnClick">
                                    <asp:Label runat="server" ID="Label41" Text="FINALIZAR" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE RECEITA MÉDICA                                                                                        -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE REQUISIÇÃO DE EXAMES                                                                                  -->
            <!-- =========================================================================================================================================================================== -->
            <div id="tabReqExam" runat="server" clientidmode="static" style="display: none;">
                <ul id="ul13" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            REQUISIÇÃO DE EXAMES</label>
                    </li>
                    <li style="margin-left: 15px;">
                        <ul>
                            <li style="margin: 11px 4px 4px 170px !important;">
                                <asp:ImageButton class="imgPesExame" style="width:17px !important; height:17px !important;" ImageUrl="/Library/IMG/Gestor_TrocarEscola.png"
                                    alt="Icone" ToolTip="Lista os exames disponíveis para a unidade." runat="server" ID="imgPesqExames" OnClick="OnClick_imgPesqExames" /> </li>
                            <li>
                                <label title="Informe o nome do exame desejado" class="lblObrigatorio">
                                    Exame</label>
                                <asp:TextBox runat="server" ID="txtCodExame" Width="49px" ToolTip="Informe o Código do Exame para pesquisá-lo" CssClass="campoCodProcMedic"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hidCodExame" />
                                <asp:HiddenField runat="server" ID="FL_REQUE_APROV_EX" />
                            </li>
                            <li style="margin-top: 13px !important;">
                                <asp:ImageButton ID="lnkPesExaAtendMed" Width="13px" runat="server" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                                    CssClass="btnPesqDescMed" ToolTip="Pesquisa pelo código do exame informado ao lado."
                                    OnClick="lnkPesExaAtendMed_OnClick" />
                            </li>
                            <li style="margin-top: 12px !important;">
                                <asp:TextBox ID="txtExameAtendMed" runat="server" Width="200px" ToolTip="Nome do Exame."></asp:TextBox>
                            </li>
                            <li style="margin:13px 0 0 0">
                                <asp:CheckBox runat="server" ID="chkExecuExameInsti" Text="Executará na Instituição" ToolTip="Quando marcado, o exame será providenciado na instituição" CssClass="checkboxLabel" Checked="true" OnCheckedChanged="chkExecuExameInsti_OnCheckedChanged" AutoPostBack="true"/>
                            </li>
                            <li class="liPrima liPReqExam" style="margin-left: 145px !important;">
                                <label>
                                    Unidade</label>
                                <asp:DropDownList ID="ddlUnidReqExam" runat="server" ToolTip="Selecione a unidade em que o exame será realizado"
                                    OnSelectedIndexChanged="ddlUnidReqExam_OnSelectedIndexChanged" AutoPostBack="true"
                                    Width="200px">
                                </asp:DropDownList>
                            </li>
                            <li>
                                <label>
                                    Local</label>
                                <asp:DropDownList ID="ddlLocalReqExam" runat="server" ToolTip="Selecione o departamento em que o exame será realizado, de acordo com a unidade selecionada"
                                    Width="180px">
                                </asp:DropDownList>
                            </li>
                            <li id="li15" runat="server" style="margin-top: 10px !important; margin-left: 2px !important;">
                                <asp:LinkButton ID="lnkIncExam" runat="server" Style="margin: 0 auto;" ToolTip="Incluir o Exame na Grid"
                                    OnClick="lnkIncExam_OnClick">
                                    <img class="imgLnkInc" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa"
                                        title="Incluir Registro" />
                                    <asp:Label runat="server" ID="Label29" Text="Incluir"></asp:Label>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lnkExcReqExam" OnClientClick="if (!confirm('Deseja realmente Excluir o registro?')) return false;"
                                    runat="server" Style="margin: 0 auto;" ToolTip="Excluir Medicamento da Grid"
                                    OnClick="lnkExcReqExam_OnClick">
                                    <img class="imgLnkExc" src='/Library/IMG/Gestor_BtnDel.png' alt="Icone Pesquisa"
                                        title="Excluir Registro" />
                                    <asp:Label runat="server" ID="Label30" Text="Excluir"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </li>
                    <li class="liPrima" style="margin-left: 15px !important; margin-top: 7px !important;">
                        <div id="div7" runat="server" class="divGridTelETA divGeralApresenta" style="height: 305px">
                            <asp:GridView ID="grdExaAtendMed" CssClass="grdBusca" Width="100%" runat="server"
                                AutoGenerateColumns="False" ToolTip="Grid apresentativa dos exames médicos solicitados no Atendimento em questão">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum registro encontrado.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="CK">
                                        <ItemStyle Width="15px" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckSelect" runat="server" />
                                            <asp:HiddenField runat="server" ID="hidCoReqExam" Value='<%# bind("ID_EXAME") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="exame" HeaderText="EXAME">
                                        <ItemStyle Width="220px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="unidade" HeaderText="UNID">
                                        <ItemStyle Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="local" HeaderText="LOCAL">
                                        <ItemStyle Width="200px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                    <li style="width: 770px">
                        <ul>
                            <li id="li19" runat="server" class="liBtnAddA liPrima" style="clear: both !important;
                                margin-top: 10px !important; margin-left: 15px !important; float: left;">
                                <asp:LinkButton ID="lnkImpGuiaExame" Enabled="false" runat="server" ValidationGroup="atuEndAlu"
                                    OnClick="lnkImpGuiaExame_OnClick">
                                    <img id="img8" runat="server" width="14" height="14" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                                        alt="Icone Pesquisa" title="Imprime Guia de Exames" />
                                    <asp:Label runat="server" ID="Label34" Text="GUIA DE EXAME" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li style="margin-top:12px;">
                                <asp:DropDownList runat="server" ID="ddlTipoEmissGuiaExame" Width="100px" ToolTip="Selecione se desaja emitir a guia de exames com Todos, de Execução Interna ou Externa">
                                <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Execução Interna" Value="I"></asp:ListItem>
                                <asp:ListItem Text="Execução Externa" Value="E"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li id="li31" runat="server" class="liBtnAddA" style="margin-top: 10px !important;
                                float: right">
                                <asp:LinkButton ID="lnkFimReqExam" runat="server" OnClick="lnkFimReqExam_OnClick">
                                    <asp:Label runat="server" ID="Label38" Text="FINALIZAR" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE REQUISIÇÃO DE EXAMES                                                                                  -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE REQUISIÇÃO DE SERV. AMBULATORIAL                                                                      -->
            <!-- =========================================================================================================================================================================== -->
            <div id="tabReqServAmbu" runat="server" clientidmode="static" style="display: none;">
                <ul id="ul14" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            REQUISIÇÃO DE SERVIÇOS AMBULATORIAIS</label>
                        <asp:HiddenField runat="server" ID="hidCodServAmbu" />
                        <asp:HiddenField runat="server" ID="FL_REQUE_APROV_SA" />
                    </li>
                    <li style="margin-left: 0px;">
                        <ul>
                            <li style="margin-left: 22px">
                                <label title="Informe o nome do serviço ambulatorial desejado">
                                    Serv. Ambulatorial</label>
                            </li>
                             <li style="margin: -1px 4px 4px 22px !important; clear:both">
                                <asp:ImageButton class="imgPesExame" style="width:17px !important; height:17px !important;" ImageUrl="/Library/IMG/Gestor_TrocarEscola.png"
                                    alt="Icone" ToolTip="Lista os Procedimentos disponíveis para a Unidade." runat="server" ID="imgListaServAmbu" OnClick="imgListaServAmbu_OnClick" /> </li>
                            <li style="margin-bottom: 10px">
                                <asp:TextBox runat="server" ID="txtCodServAmbu" ToolTip="Código do serviço ambulatorial que será usado na pesquisa."
                                    Width="49px" CssClass="campoCodProcMedic"></asp:TextBox>
                            </li>
                            <li>
                                <asp:ImageButton ID="imgPesqReqServAmbu" Width="13px" runat="server" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                                    CssClass="btnPesqDescMed" OnClick="imgPesqReqServAmbu_OnClick" />
                            </li>
                            <li>
                                <asp:TextBox ID="txtNoServAmu" runat="server" Width="290px" ToolTip="Nome do Serviço Ambulatorial"></asp:TextBox>
                            </li>
                            <li style="margin:1px 0 0 0">
                                <asp:CheckBox runat="server" ID="chkExecuServAmbuInst" Text="Executará na Instituição" ToolTip="Quando marcado, o Procedimento Médico será providenciado na instituição" CssClass="checkboxLabel" Checked="true" OnCheckedChanged="chkExecuServAmbuInst_OnCheckedChanged" AutoPostBack="true"/>
                            </li>
                            <li style="margin: -14px 0 5px 2px;">
                                <label class="lblObrigatorio">
                                    Unidade</label>
                                <asp:DropDownList runat="server" ID="ddlUnidAtendServAmbu" Width="210px" ToolTip="A Unidade onde deverá ser recebido o Serviço Ambulatorial">
                                </asp:DropDownList>
                            </li>
                            <li style="clear: both; margin-left: 70px;">
                                <label class="lblObrigatorio">
                                    Local</label>
                                <asp:DropDownList runat="server" ID="ddlLocalServAmbu" Width="190px">
                                </asp:DropDownList>
                            </li>
                            <li>
                                <label>
                                    Tipo de Serviço</label>
                                <asp:DropDownList ID="ddlTipoServAmbu" runat="server" Width="165px" ToolTip="Selecione o tipo de serviço ambulatorial desejado">
                                    <asp:ListItem Value="" Selected="True">Selecione</asp:ListItem>
                                    <asp:ListItem Value="M">Medicação</asp:ListItem>
                                    <asp:ListItem Value="A">Acompanhamento</asp:ListItem>
                                    <asp:ListItem Value="C">Curativo</asp:ListItem>
                                    <asp:ListItem Value="O">Outras</asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li>
                                <label>
                                    Tipo de Aplicação</label>
                                <asp:DropDownList ID="ddlTipoAplicServAmbu" runat="server" Width="100px" ToolTip="Selecione o tipo de aplicação do serviço ambulatorio desejado">
                                    <asp:ListItem Value="N" Selected="True">Nenhum</asp:ListItem>
                                    <asp:ListItem Value="O">Via Oral</asp:ListItem>
                                    <asp:ListItem Value="I">Via Intravenosa</asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li>
                                <label>
                                    QTD/Unidade</label>
                                <asp:TextBox ID="txtQtdServAmbu" runat="server" Width="25" ToolTip="Informe a quantidade do serviço ambulatorial, se necessário">
                                </asp:TextBox>
                                <asp:DropDownList ID="ddlUnidServAmbu" runat="server" Width="40px" ToolTip="Selecione a unidade utilizada no serviço ambulatorial, se necessário">
                                </asp:DropDownList>
                            </li>
                            <li style="margin-top: 10px;">
                                <asp:LinkButton ID="lnkIncServAmbu" runat="server" Style="margin: 0 auto;" ToolTip="Incluir Requisição de Serviço Ambulatorial na Grid"
                                    OnClick="lnkIncServAmbu_OnClick">
                                    <img class="imgLnkInc" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa"
                                        title="Incluir Registro" />
                                    <asp:Label runat="server" ID="Label39" Text="Incluir"></asp:Label>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lnkExcServAmbu" OnClientClick="if (!confirm('Deseja realmente Excluir o registro?')) return false;"
                                    runat="server" Style="margin: 0 auto;" ToolTip="Incluir a Requisição de Serviço Ambulatorial selecionada na Grid"
                                    OnClick="lnkExcServAmbu_OnClick">
                                    <img class="imgLnkExc" src='/Library/IMG/Gestor_BtnDel.png' alt="Icone Pesquisa"
                                        title="Excluir Registro" />
                                    <asp:Label runat="server" ID="Label40" Text="Excluir"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </li>
                    <li class="liClear" style="margin-left: 15px !important; margin-top: 7px !important;
                        border: 1px solid #ccc;">
                        <div id="divServAmbu" runat="server" class="divGeralApresenta">
                            <asp:GridView ID="grdServAmbu" CssClass="grdBusca" Width="100%" runat="server" AutoGenerateColumns="False">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum registro encontrado.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="CK">
                                        <ItemStyle Width="15px" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckSelect" runat="server" />
                                            <asp:HiddenField runat="server" ID="hidCoServAmbu" Value='<%# bind("ID_ATEND_SERV_AMBUL") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="servico" HeaderText="SERVIÇO AMBULATÓRIO">
                                        <ItemStyle Width="300px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipoValid" HeaderText="TIPO">
                                        <ItemStyle Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="aplicacaoValid" HeaderText="APLICAÇÃO">
                                        <ItemStyle Width="120px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                    <li style="width: 767px; margin-left: 5px !important;">
                        <ul>
                            <li id="li27" runat="server" class="liBtnAddA liPrima" style="clear: both !important;
                                margin-top: 10px !important; float: left;">
                                <asp:LinkButton ID="lnkImpGuiaServAmbu" Enabled="false" runat="server" ValidationGroup="atuEndAlu"
                                    OnClick="lnkImpGuiaServAmbu_OnClick">
                                    <img id="img9" runat="server" width="14" height="14" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                                        alt="Icone Pesquisa" title="Imprime Guia de Exames" />
                                    <asp:Label runat="server" ID="Label35" Text="GUIA SERV. ABULATORIAL" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li style="margin-top:12px;">
                                <asp:DropDownList runat="server" ID="ddlTipoEmissGuiaServAmbu" Width="100px" ToolTip="Selecione se desaja emitir a guia de Procedimentos Médicos com Todos, de Execução Interna ou Externa">
                                <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Execução Interna" Value="I"></asp:ListItem>
                                <asp:ListItem Text="Execução Externa" Value="E"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li id="li223" runat="server" class="liBtnAddA" style="margin-top: 10px !important;
                                float: right">
                                <asp:LinkButton ID="lnkFimReqServAmbu" runat="server" OnClick="lnkFimReqServAmbu_OnClick">
                                    <asp:Label runat="server" ID="lblFinaREqServAmbu" Text="FINALIZAR" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE REQUISIÇÃO DE SERV. AMBULATORIAL                                                                      -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE REGISTRO DE ATESTADO MÉDICO                                                                           -->
            <!-- =========================================================================================================================================================================== -->
            <div id="tabRegAtestMedc" runat="server" clientidmode="static" style="display: none;">
                <ul id="ul23" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            REGISTRO DE ATESTADO MÉDICO</label>
                    </li>
                    <li class="liPrima liPReqExam" style="margin-left: 180px !important;">
                        <label title="Informe o nome do exame desejado">
                            Atestado</label>
                        <asp:TextBox runat="server" ID="txtCodAtestado" Width="40px" ToolTip="Informe o Código do Atestado para pesquisá-lo"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hidCodAtesMedic" />
                    </li>
                    <li style="margin-top: 13px !important;">
                        <asp:ImageButton ID="imgbtnPesqAtestado" Width="12px" runat="server" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                            OnClick="imgbtnPesqAtestado_OnClick" CssClass="btnPesqDescMed" ToolTip="Pesquisa pelo código do Atestado informado abaixo." />
                    </li>
                    <li style="margin-top: 12px !important;"><a class="lnkPesTipoAtes" href="#">
                        <img class="lnkPesTipoAtes" width="12px" src="/Library/IMG/Gestor_TrocarEscola.png"
                            alt="Icone" title="Lista os documentos atestados cadastrados." /></a> </li>
                    <li style="margin-top: 12px !important;">
                        <asp:TextBox ID="txtAtestMedic" runat="server" Width="200px" ToolTip="Nome do Atestado."></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            CID</label>
                        <asp:DropDownList runat="server" ID="ddlCidAtesMedc" Width="135px" ToolTip="CID que o paciente em atendimento porta.">
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both; margin-left: 180px">
                        <label>
                            Unidade</label>
                        <asp:DropDownList runat="server" ID="ddlUnidAtestMedic" Width="140px">
                        </asp:DropDownList>
                    </li>
                    <li style="margin-left: 5px;">
                        <label>
                            Data</label>
                        <asp:TextBox runat="server" ID="txtDataAtes" CssClass="campoData"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Hora</label>
                        <asp:TextBox runat="server" ID="txtHrAtes" CssClass="campoHora" Width="30px" ToolTip="A Hora do atendimento."></asp:TextBox>
                    </li>
                    <li style="margin-left: 5px">
                        <label>
                            Qt Dias</label>
                        <asp:TextBox runat="server" ID="txtQtDiasAtes" Width="40px" ToolTip="A Quantidade de dias de afastamento."></asp:TextBox>
                    </li>
                    <li id="li3" runat="server" style="margin-top: 10px !important; margin-left: 2px !important;">
                        <asp:LinkButton ID="lnkIncAtesMedic" runat="server" Style="margin: 0 auto;" ToolTip="Incluir o Exame na Grid"
                            OnClick="lnkIncAtesMedic_OnClick">
                            <img class="imgLnkInc" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa"
                                title="Incluir Registro" />
                            <asp:Label runat="server" ID="Label6" Text="Incluir"></asp:Label>
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkExcAtesMedic" OnClientClick="if (!confirm('Deseja realmente Excluir o registro?')) return false;"
                            runat="server" Style="margin: 0 auto;" ToolTip="Excluir Medicamento da Grid"
                            OnClick="lnkExcAtesMedic_OnClick">
                            <img class="imgLnkExc" src='/Library/IMG/Gestor_BtnDel.png' alt="Icone Pesquisa"
                                title="Excluir Registro" />
                            <asp:Label runat="server" ID="Label7" Text="Excluir"></asp:Label>
                        </asp:LinkButton>
                    </li>
                    <li style="clear: both; margin-left: 180px">
                        <label>
                            Obs:</label>
                        <asp:TextBox runat="server" ID="txtObsAtesMedic" TextMode="MultiLine" Height="50px"
                            Width="420px"></asp:TextBox>
                    </li>
                    <li class="liPrima" style="margin-left: 15px !important; margin-top: 7px !important;">
                        <div id="div1" runat="server" class="divGeralApresenta" style="height: 240px">
                            <asp:GridView ID="grdAtesMedic" CssClass="grdBusca" Width="100%" runat="server" AutoGenerateColumns="False">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum registro encontrado.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="CK">
                                        <ItemStyle Width="30px" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckSelectAt" runat="server" OnCheckedChanged="ckSelectAt_OnCheckedChanged" />
                                            <asp:HiddenField runat="server" ID="hidIdAtesMedic" Value='<%# bind("ID_ATESTADO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ATESTADO" HeaderText="ATESTADO">
                                        <ItemStyle Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UNIDADE" HeaderText="UNID">
                                        <ItemStyle Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DATA" HeaderText="DATA">
                                        <ItemStyle Width="70px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DIASREP" HeaderText="DIAS REP.">
                                        <ItemStyle Width="30px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CID" HeaderText="CID">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                    <li style="width: 765px; margin-left: 5px">
                        <ul>
                            <li id="li6" runat="server" class="liBtnAddA liPrima" style="clear: both !important;
                                margin-top: 10px !important; float: left;">
                                <asp:LinkButton ID="lnkImpAtesMedic" Enabled="false" runat="server" ValidationGroup="atuEndAlu"
                                    OnClick="lnkImpAtesMedic_OnClick">
                                    <img id="img1" runat="server" width="14" height="14" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                                        alt="Icone Pesquisa" title="Imprime o Atestado Médico" />
                                    <asp:Label runat="server" ID="Label8" Text="EMISSÃO DO ATESTADO" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li id="li7" runat="server" class="liBtnAddA" style="float: right; margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkFimAtesMedic" runat="server" OnClick="lnkFimAtesMedic_OnClick">
                                    <asp:Label runat="server" ID="Label11" Text="FINALIZAR" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE REGISTRO DE ATESTADO MÉDICO                                                                           -->
            d
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE REGISTRO DE RESULTADO DE EXAMES                                                                       -->
            <!-- =========================================================================================================================================================================== -->
            <div id="tabRegResExam" runat="server" clientidmode="static" style="display: none;">
                <ul id="ul15" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            REGISTRO DE RESULTADO DE DEXAMES</label>
                    </li>
                </ul>
            </div>
            d
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE REGISTRO DE RESULTADO DE EXAMES                                                                       -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE ENCAMINHAMENTO MÉDICO                                                                                 -->
            <!-- =========================================================================================================================================================================== -->
            <div id="tabEncamMed" runat="server" clientidmode="static" style="display: none;">
                <ul id="ul16" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            ENCAMINHAMENTO MÉDICO</label>
                    </li>
                </ul>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE ENCAMINHAMENTO MÉDICO                                                                                 -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE ENCAMINHAMENTO INTERNAÇÃO                                                                             -->
            <!-- =========================================================================================================================================================================== -->
            <div id="tabEncamIntern" runat="server" clientidmode="static" style="display: none;">
                <ul id="ul17" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            ENCAMINHAMENTO INTERNAÇÃO</label>
                    </li>
                </ul>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE ENCAMINHAMENTO INTERNAÇÃO                                                                             -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE HISTÓRICO DE EXAMES                                                                                   -->
            <!-- =========================================================================================================================================================================== -->
            <div id="tabExamHist" runat="server" clientidmode="static" style="display: none;">
                <ul id="ul18" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            HISTÓRICO DE EXAMES</label>
                    </li>
                    <li style="margin-left: 30px">
                        <label>
                            Unidade</label>
                        <asp:DropDownList runat="server" ID="ddlUnidHistExames" Width="160px" OnSelectedIndexChanged="ddlUnidHistExames_OnSelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            Local</label>
                        <asp:DropDownList runat="server" ID="ddlLocalHistExames" Width="140px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            Exame</label>
                        <asp:DropDownList runat="server" ID="ddlExamHistExames" Width="180px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <asp:Label runat="server" ID="Label15" class="lblObrigatorio">Período</asp:Label><br />
                        <asp:TextBox runat="server" class="campoData" ID="txtIniPeriHistExames" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                        <asp:Label runat="server" ID="Label16"> &nbsp à &nbsp </asp:Label>
                        <asp:TextBox runat="server" class="campoData" ID="txtFimPeriHistExames" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                    </li>
                    <li style="margin-top: 13px">
                        <asp:ImageButton ID="imgHistExames" Width="12px" runat="server" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                            CssClass="btnPesqDescMed" ToolTip="Pesquisa pelo código do Atestado informado abaixo."
                            OnClick="imgHistExames_OnClick" />
                    </li>
                    <li class="liPrima" style="margin-left: 15px !important; margin-top: 7px !important;">
                        <div id="div10" runat="server" class="divGeralApresenta" style="height: 360px; overflow-y: scroll;">
                            <asp:GridView ID="grdHistExames" CssClass="grdBusca" runat="server" AutoGenerateColumns="False"
                                Width="100%" ToolTip="Grid que apresenta todo o histórico de Exames do paciente selecionado de acordo com os parâmetros escolhidos para busca">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum Exame encontrado nos parâmetros informados.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="DT_V" HeaderText="DATA">
                                        <ItemStyle Width="45px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CO_REGIS_V" HeaderText="COD">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="exame" HeaderText="EXAME">
                                        <ItemStyle Width="160px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="unidade" HeaderText="UNID">
                                        <ItemStyle Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="local" HeaderText="LOCAL">
                                        <ItemStyle Width="140px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_COL" HeaderText="SOLICITANTE">
                                        <ItemStyle Width="160px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE HISTÓRICO DE EXAMES                                                                                   -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE HISTÓRICO DE CONSULTAS                                                                                -->
            <!-- =========================================================================================================================================================================== -->
            <div id="tabConsHist" runat="server" clientidmode="static" style="display: none;">
                <ul id="ul19" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            HISTÓRICO DE CONSULTAS</label>
                    </li>
                    <li style="margin-left: 140px">
                        <label>
                            Unidade</label>
                        <asp:DropDownList runat="server" ID="ddlUnidHistConsultas" Width="190px" OnSelectedIndexChanged="ddlUnidHistConsultas_OnSelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            Especialidade</label>
                        <asp:DropDownList runat="server" ID="ddlEspecHistConsultas" Width="180px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            Departamento</label>
                        <asp:DropDownList runat="server" ID="ddlDeptHistConsultas" Width="100px">
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both; margin-left: 200px">
                        <label>
                            Situação</label>
                        <asp:DropDownList runat="server" ID="ddlSituHistConsultas" Width="90px">
                            <asp:ListItem Selected="True" Text="Todos" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Aberto" Value="A"></asp:ListItem>
                            <asp:ListItem Text="Finalizado" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            Tipo de Consulta</label>
                        <asp:DropDownList ID="ddlConsuHistTipo" Style="margin: 0px;" runat="server">
                            <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                            <asp:ListItem Value="N">Normal</asp:ListItem>
                            <asp:ListItem Value="R">Retorno</asp:ListItem>
                            <asp:ListItem Value="U">Urgência</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li>
                        <asp:Label runat="server" ID="Label17" class="lblObrigatorio">Período</asp:Label><br />
                        <asp:TextBox runat="server" class="campoData" ID="txtIniPeriHistConsul" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                        <asp:Label runat="server" ID="Label18"> &nbsp à &nbsp </asp:Label>
                        <asp:TextBox runat="server" class="campoData" ID="txtFimPeriHistConsul" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                    </li>
                    <li style="margin-top: 13px">
                        <asp:ImageButton ID="imgHistConsul" Width="12px" runat="server" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                            CssClass="btnPesqDescMed" ToolTip="Pesquisa pelo código do Atestado informado abaixo."
                            OnClick="imgHistConsul_OnClick" />
                    </li>
                    <li class="liPrima" style="margin-top: 5px !important; margin-left: 20px !important;">
                        <div id="div11" runat="server" class="divGeralApresenta" style="height: 330px;">
                            <asp:GridView ID="grdHistConsul" CssClass="grdBusca" Width="100%" runat="server"
                                AutoGenerateColumns="False" ToolTip="Grid que apresenta todo o histórico de Consultas do paciente selecionado de acordo com os parâmetros escolhidos para busca">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhuma Consulta Médica encontrada nos parâmetros informados.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="hora" HeaderText="DATA e HORA">
                                        <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_ESPEC" HeaderText="ESPECIALIDADE">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DEPTO" HeaderText="DEPART.">
                                        <ItemStyle Width="70px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_COL" HeaderText="PROF. SAÚDE(MAT - NOME)">
                                        <ItemStyle Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TP_CONSUL_VALID" HeaderText="TIPO">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CO_SITU_VALID" HeaderText="STATUS">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE HISTÓRICO DE CONSULTAS                                                                                -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE HISTÓRICO DE MEDICAMENTOS                                                                             -->
            <!-- =========================================================================================================================================================================== -->
            <div id="tabMedcHist" runat="server" clientidmode="static" style="display: none;">
                <ul id="ul20" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            HISTÓRICO DE MEDICAMENTOS</label>
                    </li>
                </ul>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE HISTÓRICO DE MEDICAMENTOS                                                                             -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE HISTÓRICO DE DIAGNÓSTICOS                                                                             -->
            <!-- =========================================================================================================================================================================== -->
            <div id="tabDiagHist" runat="server" clientidmode="static" style="display: none;">
                <ul id="ul21" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            HISTÓRICO DE DIAGNÓSTICOS</label>
                    </li>
                    <li style="margin-left: 86px">
                        <label>
                            Unidade</label>
                        <asp:DropDownList runat="server" ID="ddlUnidHistDiag" Width="190px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            CID</label>
                        <asp:DropDownList runat="server" ID="ddlCIDHistDiag" Width="180px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <asp:Label runat="server" ID="lblPeriodo" class="lblObrigatorio">Período</asp:Label><br />
                        <asp:TextBox runat="server" class="campoData" ID="txtIniDtHistDiag" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                        <asp:Label runat="server" ID="Label12"> &nbsp à &nbsp </asp:Label>
                        <asp:TextBox runat="server" class="campoData" ID="txtFimDtHistDiag" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                    </li>
                    <li style="margin-top: 13px">
                        <asp:ImageButton ID="imgPesqHistDiag" Width="12px" runat="server" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                            CssClass="btnPesqDescMed" ToolTip="Pesquisa pelo código do Atestado informado abaixo."
                            OnClick="imgPesqHistDiag_OnClick" />
                    </li>
                    <li class="liPrima" style="margin-top: 5px !important; margin-left: 20px !important;">
                        <div id="div5" runat="server" class="divGeralApresenta" style="overflow-y: scroll;
                            border: 1px solid #ccc; margin-left: -5px; height: 362px;">
                            <asp:GridView ID="grdHistDiag" CssClass="grdBusca" Width="100%" runat="server" AutoGenerateColumns="False"
                                ToolTip="Grid que apresenta todo o histórico de Diagnósticos do paciente selecionado de acordo com os parâmetros escolhidos para busca">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum Diagnóstico encontrado nos parâmetros informados.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="dt_valid" HeaderText="DATA">
                                        <ItemStyle Width="35px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CO_REGIS_V" HeaderText="COD">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CID" HeaderText="CID">
                                        <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_COL" HeaderText="PROF.(MAT/CRM - NOME)">
                                        <ItemStyle Width="70px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="DIAGNÓSTICO">
                                        <ItemStyle Width="250px" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtHistDiag" TextMode="MultiLine" Enabled="true"
                                                Width="100%" Height="40px" Text='<%# bind("DIAG") %>' ToolTip="Diagnóstico do(a) Paciente"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE HISTÓRICO DE DIAGNÓSTICOS                                                                             -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE HISTÓRICO DE SERV. AMBULATORIAL                                                                       -->
            <!-- =========================================================================================================================================================================== -->
            <div id="tabServAmbuHist" runat="server" clientidmode="static" style="display: none;">
                <ul id="ul22" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            HISTÓRICO DE SERVIÇOS AMBULATORIAIS</label>
                    </li>
                    <li style="margin-left: 23px">
                        <label>
                            Unidade</label>
                        <asp:DropDownList runat="server" ID="ddlUnidHistServAmbu" Width="160px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            Serviços</label>
                        <asp:DropDownList runat="server" ID="ddlTpServAmbuHistorico" Width="160px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            Tipo de Serviço</label>
                        <asp:DropDownList ID="ddlHistoTipoServ" runat="server" Width="165px" ToolTip="Selecione o tipo de serviço ambulatorial para pesquisa.">
                            <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                            <asp:ListItem Value="M">Medicação</asp:ListItem>
                            <asp:ListItem Value="A">Acompanhamento</asp:ListItem>
                            <asp:ListItem Value="C">Curativo</asp:ListItem>
                            <asp:ListItem Value="O">Outras</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li>
                        <asp:Label runat="server" ID="Label13" class="lblObrigatorio">Período</asp:Label><br />
                        <asp:TextBox runat="server" class="campoData" ID="txtIniPeriHisServAmbu" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                        <asp:Label runat="server" ID="Label14"> &nbsp à &nbsp </asp:Label>
                        <asp:TextBox runat="server" class="campoData" ID="txtFimPeriHisServAmbu" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                    </li>
                    <li style="margin-top: 13px">
                        <asp:ImageButton ID="imgHistServAmbu" Width="12px" runat="server" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                            CssClass="btnPesqDescMed" ToolTip="Pesquisa pelo código do Atestado informado abaixo."
                            OnClick="imgHistServAmbu_OnClick" />
                    </li>
                    <li class="liPrima" style="margin-top: 5px !important; margin-left: 20px !important;">
                        <div id="div8" runat="server" class="divGeralApresenta" style="height: 360px;">
                            <asp:GridView ID="grdHistServAmbu" CssClass="grdBusca" Width="100%" runat="server"
                                ToolTip="Grid que apresenta todo o histórico de Serviços Ambulatoriais do paciente selecionado de acordo com os parâmetros escolhidos para busca"
                                AutoGenerateColumns="False">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum Serviço Ambulatorial encontrado nos parâmetros informados.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="dt_valid" HeaderText="DATA">
                                        <ItemStyle Width="45px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CO_REGIS_V" HeaderText="COD">
                                        <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="servico" HeaderText="SERVIÇO AMBULATÓRIO">
                                        <ItemStyle Width="170px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_EMP" HeaderText="UNIDADE">
                                        <ItemStyle Width="220px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_DEPTO" HeaderText="DEPTO.">
                                        <ItemStyle Width="130px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipoValid" HeaderText="TIPO">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="aplicacaoValid" HeaderText="APLICAÇÃO">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE HISTÓRICO DE SERV. AMBULATORIAL                                                                       -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE HISTÓRICO DE ATESTADOS MÉDICOS                                                                        -->
            <!-- =========================================================================================================================================================================== -->
            <div id="tabAtestMedcHist" runat="server" clientidmode="static" style="display: none;">
                <ul id="ul24" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            HISTÓRICO DE ATESTADOS MÉDICOS</label>
                    </li>
                    <li style="margin-left: 30px">
                        <label>
                            Unidade</label>
                        <asp:DropDownList runat="server" ID="ddlUnidHistAtestMedic" Width="160px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            Tipo</label>
                        <asp:DropDownList runat="server" ID="ddlTipoAtestMedic" Width="140px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            CID</label>
                        <asp:DropDownList runat="server" ID="ddlCIDHistAtestados" Width="180px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <asp:Label runat="server" ID="lblIniPeriHisAtesMed" class="lblObrigatorio">Período</asp:Label><br />
                        <asp:TextBox runat="server" class="campoData" ID="txtIniPeriHistoAtestMedic" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                        <asp:Label runat="server" ID="lblFimPeriHisAtesMed"> &nbsp à &nbsp </asp:Label>
                        <asp:TextBox runat="server" class="campoData" ID="txtFimPeriHistoAtestMedic" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                    </li>
                    <li style="margin-top: 13px">
                        <asp:ImageButton ID="imgHistAtestMedic" Width="12px" runat="server" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                            CssClass="btnPesqDescMed" ToolTip="Pesquisa pelo código do Atestado informado abaixo."
                            OnClick="imgHistAtestMedic_OnClick" />
                    </li>
                    <li class="liPrima" style="margin-top: 5px !important; margin-left: 20px !important;">
                        <div id="div9" runat="server" class="divGeralApresenta" style="height: 360px;">
                            <asp:GridView ID="grdHistAtestados" CssClass="grdBusca" Width="100%" runat="server"
                                ToolTip="Grid que apresenta todo o histórico de Atestados Médicos do paciente selecionado de acordo com os parâmetros escolhidos para busca"
                                AutoGenerateColumns="False">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum Atestado Médico encontrado nos parâmetros informados.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="DATA" HeaderText="DATA">
                                        <ItemStyle Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ATESTADO" HeaderText="ATESTADO">
                                        <ItemStyle Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UNIDADE" HeaderText="UNID">
                                        <ItemStyle Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DIASREP" HeaderText="DIAS REP.">
                                        <ItemStyle Width="25px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CID" HeaderText="CID">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE HISTÓRICO DE ATESTADOS MÉDICOS                                                                        -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                            TELA DE HISTÓRICO DE RECEITAS MÉDICAS                                                                        -->
            <!-- =========================================================================================================================================================================== -->
            <div id="tabhistReceiMedic" runat="server" clientidmode="static" style="display: none;">
                <ul id="ul1" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            HISTÓRICO DE RECEITAS MÉDICAS</label>
                    </li>
                    <li style="margin-left: 90px">
                        <label>
                            Unidade</label>
                        <asp:DropDownList runat="server" ID="ddlUnidHistReceitMedic" Width="200px" ToolTip="Empresa onde foi feita a Receita Médica"
                            OnSelectedIndexChanged="ddlUnidHistReceitMedic_OnSelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            Medicamento</label>
                        <asp:DropDownList runat="server" ID="ddlProduHistReceitMedic" Width="140px" ToolTip="Medicamento da Receita Médica">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <asp:Label runat="server" ID="Label21" class="lblObrigatorio">Período</asp:Label><br />
                        <asp:TextBox runat="server" class="campoData" ID="txtIniPeriHistReceiMedic" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                        <asp:Label runat="server" ID="Label42"> &nbsp à &nbsp </asp:Label>
                        <asp:TextBox runat="server" class="campoData" ID="txtFimPeriHistReceiMedic" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                    </li>
                    <li style="margin-top: 13px">
                        <asp:ImageButton ID="imgPesqHistReceiMedic" Width="12px" runat="server" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                            CssClass="btnPesqDescMed" ToolTip="Pesquisa pelo código do Atestado informado abaixo."
                            OnClick="imgPesqHistReceiMedic_OnClick" />
                    </li>
                    <li class="liPrima" style="margin-top: 5px !important; margin-left: 20px !important;">
                        <div id="div12" runat="server" class="divGeralApresenta" style="height: 360px;">
                            <asp:GridView ID="grdHistReceiMedic" CssClass="grdBusca" Width="100%" runat="server"
                                ToolTip="Grid que apresenta todo o histórico de Receitas Médicas do paciente selecionado de acordo com os parâmetros escolhidos para busca"
                                AutoGenerateColumns="False">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhuma Receita Médica encontrada nos parâmetros informados.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="DATA" HeaderText="DATA">
                                        <ItemStyle Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="medicamento" HeaderText="MEDICAMENTO">
                                        <ItemStyle Width="170px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="qtd" HeaderText="QTD">
                                        <ItemStyle Width="20px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="uso" HeaderText="USO">
                                        <ItemStyle Width="20px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="prescricao" HeaderText="PRESCRIÇÃO">
                                        <ItemStyle Width="240px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_COL" HeaderText="PROF. SAÚDE (MAT - NOME)">
                                        <ItemStyle Width="200px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
            <!--======================================================================================================================================================================= -->
            <!--                                                           FIM DA TELA DE HISTÓRICO DE RECEITAS MÉDICAS                                                                   -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE HISTÓRICO DE IMAGENS                                                                                  -->
            <!-- =========================================================================================================================================================================== -->
            <div id="tabImgMedcHist" runat="server" clientidmode="static" style="display: none;">
                <ul id="ul25" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            HISTÓRICO DE IMAGENS</label>
                    </li>
                </ul>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE HISTÓRICO DE IMAGENS                                                                                  -->
            <!-- =========================================================================================================================================================================== -->
            <div id="divLoadShowReservas" style="display: none;">
            </div>
            <div id="divLoadShowAlunos" style="display: none; height: 305px !important;" />
            <div id="divLoadShowAlunosOutra" style="display: none; height: 305px !important;" />
            <div id="divLoadShowResponsaveis" style="display: none; height: 315px !important;" />
            <div id="divLoadShowDoencas" style="display: none; height: 325px !important;" />
            <div id="divLoadShowProduto" style="display: none; height: 325px !important;" />
            <div id="divLoadShowExames" style="display: none; height: 360px !important;">
            <ul style="float:left;clear:none;">
                <li>
                     <label style="color: #FAA460; font-weight: bold;">
                                                    Pesquisa</label>
                </li>
                <li style="float:left; clear:both">
                    <label title="Filtre pela Unidade onde o exame será executado">Unidade de Execução</label>
                    <asp:DropDownList runat="server" ID="ddlUnidExecExamMOD" ToolTip="Filtre pela Unidade onde o exame será executado" OnSelectedIndexChanged="ddlUnidExecExamMOD_OnSelectedIndexChanged" AutoPostBack="true" Width="200px"></asp:DropDownList>
                </li>
                <li style="float:left; margin-left:5px;">
                    <label title="Pesquise pelo exame de acordo com o Grupo">Grupo</label>
                    <asp:DropDownList runat="server" ID="ddlGrupExamMOD" ToolTip="Pesquise pelo exame de acordo com o Grupo" OnSelectedIndexChanged="ddlGrupExamMOD_OnSelectedIndexChanged" AutoPostBack="true" Width="170px"></asp:DropDownList>
                </li>
                <li style="float:left; margin-left:5px; margin-right:5px;">
                    <label title="Pesquise pelo exame de acordo com o SubGrupo">SubGrupo</label>
                        <asp:DropDownList runat="server" ID="ddlSubGrupExamMOD" ToolTip="Pesquise pelo exame de acordo com o SubGrupo" OnSelectedIndexChanged="ddlSubGrupExamMOD_OnSelectedIndexChanged" AutoPostBack="true" Width="210px"></asp:DropDownList>
                </li>
                <li>
                    <label title="Pesquise pelo exame de acordo com o Nome">Nome</label>
                    <asp:TextBox runat="server" ID="txtNomeExamMOD" ToolTip="Pesquise pelo exame de acordo com o Nome" OnTextChanged="txtNomeExamMOD_OnTextChanged" AutoPostBack="true" Width="313px"></asp:TextBox>
                </li>
                <li style="clear:both">
                  <div id="divListarProdutosContainer">
        <div id="frmListarProdutos" runat="server">
        <div id="divListarProdutosContent">
            <asp:GridView runat="server" ID="grdListarExames" AutoGenerateColumns="false" AllowPaging="false"
                GridLines="Vertical" DataKeyNames="ID_PROC_MEDI_PROCE" Width="100%"
                OnRowDataBound="grdListarExames_OnRowDataBound" OnSelectedIndexChanged="grdListarExames_OnSelectedIndexChanged" AutoGenerateSelectButton="false">
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum Exame Disponível<br />
                </EmptyDataTemplate>
                <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" />
                <AlternatingRowStyle CssClass="alternateRowStyleLD" Height="15" />
                <RowStyle CssClass="rowStyleLD" Height="15" />
                <Columns>
                    <asp:BoundField Visible="false" DataField="ID_PROC_MEDI_PROCE" HeaderText="Cod." SortExpression="ID_PROC_MEDI_PROCE"
                                HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                <HeaderStyle CssClass="noprint"></HeaderStyle>
                                <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="COD. EXAME" DataField="CO_PROC_MEDI">
                        <ItemStyle Width="50px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="GRUPO" DataField="NM_PROC_MEDIC_GRUPO">
                        <ItemStyle Width="150px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="SUBGRUPO" DataField="NM_PROC_MEDIC_SGRUP">
                        <ItemStyle Width="150px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="EXAME" DataField="NM_PROC_MEDI">
                        <ItemStyle Width="300px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="STA" DataField="STATUS">
                        <ItemStyle Width="40px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DISP" DataField="DISP">
                        <ItemStyle Width="30px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="CO" DataField="CO">
                        <ItemStyle Width="30px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="VL TABELA" DataField="VL_TABELA">
                        <ItemStyle Width="60px" VerticalAlign="Middle" HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="VL OPERAD" DataField="VL_OPERADORA">
                        <ItemStyle Width="60px" VerticalAlign="Middle" HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:CommandField HeaderText="Nome" ShowSelectButton="false" Visible="False" />
                </Columns>
            </asp:GridView>
        </div>
         <div id="div14" style="margin:10px 0 5px 0;">
            <p id="p1" class="pFechar">
                *Clique na linha da grade para selecionar o Exame correspondente.</p>
        </div>
        <div>
            <p id="pFechar" class="pFechar">
                **Clique no X para fechar a janela.</p>
        </div>
        <%--<div id="divRodapeLD" margin-top="-20px">
            <img id="imgLogoGestorLD" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
        </div>--%>
        </div>
    </div>
    </li>
    </ul>
            </div>
            <div id="divLoadShowServAmbu" style="display: none; height: 360px !important;">
             <ul style="float:left;clear:none;">
                <li>
                     <label style="color: #FAA460; font-weight: bold;">
                                                    Pesquisa</label>
                </li>
                <li style="float:left; clear:both">
                    <label title="Filtre pela Unidade onde o Procedimento será executado">Unidade de Execução</label>
                    <asp:DropDownList runat="server" ID="ddlUnidExecServAMbuMOD" ToolTip="Filtre pela Unidade onde o Procedimento será executado" OnSelectedIndexChanged="ddlUnidExecServAMbuMOD_OnSelectedIndexChanged" AutoPostBack="true" Width="200px"></asp:DropDownList>
                </li>
                <li style="float:left; margin-left:5px;">
                    <label title="Pesquise pelo Procedimento de acordo com o Grupo">Grupo</label>
                    <asp:DropDownList runat="server" ID="ddlGrupProcMOD" ToolTip="Pesquise pelo Procedimento de acordo com o Grupo" OnSelectedIndexChanged="ddlGrupProcMOD_OnSelectedIndexChanged" AutoPostBack="true" Width="170px"></asp:DropDownList>
                </li>
                <li style="float:left; margin-left:5px;">
                    <label title="Pesquise pelo exame de acordo com o SubGrupo">SubGrupo</label>
                        <asp:DropDownList runat="server" ID="ddlSubGrupProcMOD" ToolTip="Pesquise pelo exame de acordo com o SubGrupo" OnSelectedIndexChanged="ddlSubGrupProcMOD_OnSelectedIndexChanged" AutoPostBack="true" Width="210px"></asp:DropDownList>
                </li>
                <li style="float:left; margin-left:5px;">
                    <label title="Pesquise pelo exame de acordo com o Nome">Nome</label>
                    <asp:TextBox runat="server" ID="txtNomeProcMOD" ToolTip="Pesquise pelo Procedimento de acordo com o Nome" OnTextChanged="txtNomeProcMOD_OnTextChanged" AutoPostBack="true" Width="313px"></asp:TextBox>
                </li>
                <li>
                  <div id="divListarProdutosContainer2">
        <div id="frmListarProdutos2" runat="server">
        <div id="divListarProdutosContent2">
            <asp:GridView runat="server" ID="grdListarProcMedic" AutoGenerateColumns="false" AllowPaging="false"
                GridLines="Vertical" DataKeyNames="ID_PROC_MEDI_PROCE" Width="100%"
                OnRowDataBound="grdListarProcMedic_OnRowDataBound" OnSelectedIndexChanged="grdListarProcMedic_OnSelectedIndexChanged" AutoGenerateSelectButton="false">
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum Procedimento Disponível<br />
                </EmptyDataTemplate>
                <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" /> 
                <AlternatingRowStyle CssClass="alternateRowStyleLD" Height="15" />
                <RowStyle CssClass="rowStyleLD" Height="15" />
                <Columns>
                    <asp:BoundField Visible="false" DataField="ID_PROC_MEDI_PROCE" HeaderText="Cod." SortExpression="ID_PROC_MEDI_PROCE"
                                HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                <HeaderStyle CssClass="noprint"></HeaderStyle>
                                <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="COD. EXAME" DataField="CO_PROC_MEDI">
                        <ItemStyle Width="50px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="GRUPO" DataField="NM_PROC_MEDIC_GRUPO">
                        <ItemStyle Width="150px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="SUBGRUPO" DataField="NM_PROC_MEDIC_SGRUP">
                        <ItemStyle Width="150px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="PROCEDIMENTO MÉDICO" DataField="NM_PROC_MEDI">
                        <ItemStyle Width="300px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="STA" DataField="STATUS">
                        <ItemStyle Width="40px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DISP" DataField="DISP">
                        <ItemStyle Width="30px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="CO" DataField="CO">
                        <ItemStyle Width="30px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="VL TABELA" DataField="VL_TABELA">
                        <ItemStyle Width="60px" VerticalAlign="Middle" HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="VL OPERAD" DataField="VL_OPERADORA">
                        <ItemStyle Width="60px" VerticalAlign="Middle" HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:CommandField HeaderText="Nome" ShowSelectButton="false" Visible="False" />
                </Columns>
            </asp:GridView>
        </div>
         <div id="div18" style="margin:10px 0 5px 0;">
            <p id="p2" class="pFechar">
                *Clique na linha da grade para selecionar o Exame correspondente.</p>
        </div>
        <div id="div19">
            <p id="p3" class="pFechar">
                **Clique no X para fechar a janela.</p>
        </div>
        <%--<div id="divRodapeLD" margin-top="-20px">
            <img id="imgLogoGestorLD" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
        </div>--%>
        </div>
    </div>
    </li>
    </ul>
            </div>
            <div id="divLoadShowDocuAtes" style="display: none; height: 325px !important;" />
               <div id="divLoadInfosCadas" style="display: none; height: 300px !important;">
                <%--<asp:UpdatePanel runat="server" ID="updCadasUsuario" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                <ul class="ulDados" style="width: 400px !important;">
                    <div class="DivResp" runat="server" id="divResp">
                        <ul class="ulDadosResp" style="margin-left: -100px !important; width: 600px !important;">
                            <li style="margin: 30px 0 -3px 0px">
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
                                <asp:HiddenField runat="server" ID="HiddenField2" />
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
                                <label class="lblObrigatorio">
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
                            <li style="clear: both; margin: 0px 0 0 0px;">
                                <ul class="ulIdentResp">
                                    <li>
                                        <asp:Label runat="server" ID="lblcarteIden" Style="font-size: 9px;">Carteira de Identidade</asp:Label>
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
                            <li style="margin: 0px 0 0 10px;">
                                <ul class="ulDadosContatosResp">
                                    <li>
                                        <asp:Label runat="server" ID="Label45" Style="font-size: 9px;" CssClass="lblObrigatorio">Dados de Contato</asp:Label>
                                    </li>
                                    <li style="clear: both;">
                                        <label>
                                            Tel. Fixo</label>
                                        <asp:TextBox runat="server" ID="txtTelFixResp" Width="70px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio">
                                            Tel. Celular</label>
                                        <asp:TextBox runat="server" ID="txtTelCelResp" Width="76px" CssClass="Tel9Dig"></asp:TextBox>
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
                                        <asp:TextBox runat="server" ID="txtDeFaceResp" Width="85px"></asp:TextBox>
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
                            <li class="ulInfosGerais">
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
                                    <li style="margin: 11px 2px 0 2px;">
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
                                    <li style="clear: both; margin-top: -4px;">
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
                                    <li class="liFotoColabAluno">
                                        <fieldset class="fldFotoColabAluno">
                                            <uc1:ControleImagem ID="upImageCadas" runat="server" />
                                        </fieldset>
                                    </li>
                                </ul>
                            </li>
                            <li style="margin: -2px 0 0 -23px;">
                                <ul class="ulDadosPaciente">
                                    <li style="margin-bottom: -2px;">
                                        <label class="lblTop">
                                            DADOS DO PACIENTE - USUÁRIO DE SAÚDE</label>
                                    </li>
                                    <li style="clear: both" class="lblObrigatorio">
                                        <label>
                                            Nº NIS</label>
                                        <asp:TextBox runat="server" ID="txtNuNisPaci" Width="60" CssClass="txtNireAluno"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            CPF</label>
                                        <asp:TextBox runat="server" ID="txtCPFMOD" CssClass="campoCpf" Width="75px"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="HiddenField3" />
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio">
                                            Nome</label>
                                        <asp:TextBox runat="server" ID="txtnompac" ToolTip="Nome do Paciente" Width="298px"></asp:TextBox>
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
                                    <li style="clear: both; margin-top: 0px; float: right">
                                        <asp:Label runat="server" ID="lblEmailPaci">Email</asp:Label>
                                        <asp:TextBox runat="server" ID="txtEmailPaci" Width="220px"></asp:TextBox>
                                    </li>
                                </ul>
                            </li>
                            <li id="li11" runat="server" class="liBtnAddA" style="margin: -10px 0 0 548px !important;">
                                <asp:LinkButton ID="lnkSalvar" Enabled="true" runat="server" OnClick="lnkSalvar_OnClick">
                                    <asp:Label runat="server" ID="Label48" Text="SALVAR" Style="margin-left: 4px;"></asp:Label>
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
        <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
    </ul>
    <%--<script src="/Library/JS/jquery.ui.js" type="text/javascript"></script>
    <script src="/Library/JS/ui.datepicker-pt-BR.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.maskedinput-1.2.2.min.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.maskMoney.0.2.js" type="text/javascript"></script>
    <script src="/Library/JS/FormValidation.js" type="text/javascript"></script>--%>
    <script type="text/javascript">

        $(document).ready(function () {
            JavscriptAtend();
        });

        function controleTab(id, idChk) {

            $("#tabSelPacien").hide();
            $("#tabAtendMedic").hide();
            $("#tabReqMedic").hide();
            $("#tabReqExam").hide();
            $("#tabReqServAmbu").hide();
            $("#tabRegResExam").hide();
            $("#tabEncamMed").hide();
            $("#tabEncamIntern").hide();
            $("#tabRegAtestMedc").hide();

            $("#tabExamHist").hide();
            $("#tabConsHist").hide();
            $("#tabMedcHist").hide();
            $("#tabDiagHist").hide();
            $("#tabServAmbuHist").hide();
            $("#tabAtestMedcHist").hide();
            $("#tabImgMedcHist").hide();
            $("#tabhistReceiMedic").hide();
            $("#tabResMedicamentos").hide();

            $("#" + id).show();

            $("#chkSelPacien").selected(false)
            $("#chkAtdMedico").selected(false)
            $("#chkReqMedicPaci").selected(false)
            $("#chkReqExamPaci").selected(false)
            $("#chkReqSevAmbuPaci").selected(false)
            $("#chkRegResExame").selected(false)
            $("#chkEncaMedicPaci").selected(false)
            $("#chkEncaIntern").selected(false)
            $("#chkRegAtestMedcPaci").selected(false)
            $("#chkExmPaci").selected(false)
            $("#chkConsPaci").selected(false)
            $("#chkHistReceiMedic").selected(false)
            $("#chkMedicPaci").selected(false)
            $("#chkDiagPaci").selected(false)
            $("#chkServAmbPaci").selected(false)
            $("#chkAtestMedcPaci").selected(false);
            $("#chkImgAtendPaci").selected(false);
            $("#chkResMedicamentos").selected(false);

            $("#" + idChk).selected(true)
        }

        function JavscriptAtend() {

            $("input.txtPeriodoDeIniBolAluno").datepicker();
            $(".txtPeriodoDeIniBolAluno").mask("99/99/9999");
            $(".campoCodProcMedic").mask("99999999");
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
            $(".txtDiasUsoMedcaAtendMed").mask("?999");
            $(".txtQtdMedcaAtendMed").mask("?999");
            $("#txtTempFumanPreAtend").mask("?999");
            $(".campoHora").mask("99:99");
            $(".campoPressArteri").mask("99/9");
            $(".campoGrau").mask("99,9");
            $(".campoAltu").mask("9,99");
            $(".Tel9Dig").mask("(99) 9999-9999?9");
            $(".campoTel").mask("(99) 9999-9999");
            //            $(".campoPeso").mask({ symbol: "", decimal: ",", thousands: "." });

//            $(".campoData").datepicker();
//            $(".campoData").mask("99/99/9999");

            //            $("#divLoadShowExames").appendTo('body');

            //============================>Função de atualização da grid de encaminhamentos

            //            var jq = jQuery.noConflict();
            //            f1();
            //            function f1() {
            //                jq(document).ready(function() {
            //                    var lastid = jq.cookie("maxletterid");
            //                    if(lastid != "0")

            //                    jq.get("test2.aspx", function(dataReturned) {
            //                        if(dataReturned != "")
            //                        {
            //                            jq('#
            //                        }
            //                    }
            //                }
            //            }

            //============================>Função de atualização da grid de encaminhamentos

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

                $("#divLoadShowReservas").dialog({ title: "RESERVAS DE MATRÍCULA", modal: true, width: "600px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
            });

            $(".lnkPesNIRE").click(function () {
                if ((($(".lblSucInfResp").is(":visible")))) {
                    $("#divLoadShowAlunos").load("../../../../../Componentes/ListarAlunos.aspx", function () {
                        $("#divLoadShowAlunos #frmListarAlunos").attr("action", "../../../../../Componentes/ListarAlunos.aspx");
                    });

                    $("#divLoadShowAlunos").dialog({ title: "LISTA DE USUÀRIOS", modal: true, width: "970px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
                }
            });

            $(".lnkPesNIRE").click(function () {
                $('#divLoadShowAlunosOutra').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE PACIENTES",
                    open: function () { $('#divLoadShowAlunosOutra').load("/Componentes/ListarPacientes.aspx"); }
                });
            });

            $(".lnkPesResp").click(function () {
                if ($(".lblSucInfResp").is(":visible") == false) {
                    $("#divLoadShowResponsaveis").load("../../../../../Componentes/ListarResponsaveis.aspx", function () {
                        $("#divLoadShowResponsaveis #frmListarResponsaveis").attr("action", "../../../../../Componentes/ListarResponsaveis.aspx");
                    });

                    $("#divLoadShowResponsaveis").dialog({ title: "LISTA DE RESPONSÁVEIS", modal: true, width: "690px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
                }
            });

            //Mostra a grid de medicamentos
            $(".lnkPesMed").click(function () {
                $('#divLoadShowProduto').dialog({ autoopen: false, modal: true, width: 430, height: 390, resizable: false, title: "LISTA DE MEDICAMENTOS",
                    open: function () { $('#divLoadShowProduto').load("/Componentes/ListarProdutos.aspx"); }
                });
            });

            //Mostra a grid de Serviços Ambulatoriais
            $(".lnkPesServAmbu").click(function () {
                $('#divLoadShowServAmbu').dialog({ autoopen: false, modal: true, width: 430, height: 390, resizable: false, title: "LISTA DE SERVIÇOS AMBULATORIAIS",
                    open: function () { $('#divLoadShowServAmbu').load("/Componentes/ListarServicoesAmbulatoriais.aspx"); }
                });
            });

            $('.lnkPesqCID').click(function () {
                $('#divLoadShowDoencas').dialog({ autoopen: false, modal: true, width: 430, height: 390, resizable: false, title: "LISTA DE CÓDIGOS DE DOENÇAS (CID)",
                    open: function () { $('#divLoadShowDoencas').load("/Componentes/ListarDoencas.aspx"); }
                });
            });

            $('.lnkPesTipoAtes').click(function () {
                $('#divLoadShowDocuAtes').dialog({ autoopen: false, modal: true, width: 430, height: 390, resizable: false, title: "LISTA OS DOCUMENTOS ATESTADOS CADASTRADOS",
                    open: function () { $('#divLoadShowDocuAtes').load("/Componentes/ListarAtestados.aspx"); }
                });
            });

            //Controla a apresentação das gride de Encaminhamento Médico
            $("#chkEncam").click(function (evento) {
                if ($("#chkEncam").attr("checked")) {

                    //Verifica se o checkbox para mostrar as consultas está marcado, caso esteja, diminui a altura da div das consultas e do encaminhamento, para caber os dois, caso não esteja,
                    //aumenta a div do encaminhamento para abranger a tela inteira
                    if ($("#chkConsul").attr("checked")) {
                        $("#divConsul").css("height", "170px");
                        $("#divPaciEnca").css("height", "170px");
                    } else {
                        $("#divPaciEnca").css("height", "365px");
                    }

                    $("#divPaciEnca").show("slow");
                } else {
                    $("#divPaciEnca").hide("slow");

                    //Verifica se está marcado para mostrar as consultas, caso esteja, expande ele no espaço que antes estava a grid de Consultas
                    if ($("#chkConsul").attr("checked")) {
                        $("#divConsul").css("height", "365px");
                    }
                }

                //Verifica qual das opções está selecionada para mostrar o dropdownlist de profissionais de saúde conforme desejo
                if ($("#chkEncam").attr("checked")) {
                    if ($("#chkConsul").attr("checked")) {
                        $("#divMedicCo").hide();
                        $("#divMedicEn").hide();
                        $("#divMedic").show();
                    } else {
                        $("#divMedicCo").hide();
                        $("#divMedicEn").show();
                        $("#divMedic").hide();
                    }
                } else if ($("#chkConsul").attr("checked")) {
                    $("#divMedic").hide();
                    $("#divMedicCo").show();
                    $("#divMedicEn").hide();
                }
            });

            //Controla a apresentação das gride de Consultas Médicas conforme o usuário clica para marcar ou desmarcar o checkbox para mostrar as consultas
            $("#chkConsul").click(function (evento) {
                if ($("#chkConsul").attr("checked")) {
                    //Verifica se o checkbox para mostrar o encaminhamento está marcado, caso esteja, diminui a altura da div do encaminhamento para caber os dois, caso não esteja,
                    //aumenta a div de consultas para abranger a tela toda
                    if ($("#chkEncam").attr("checked")) {
                        $("#divPaciEnca").css("height", "170px");
                        $("#divConsul").css("height", "170px");
                    } else {
                        $("#divConsul").css("height", "365px");
                    }

                    //                    $("#divConsul").css("display", "block");
                    $("#divConsul").show("slow");
                } else {
                    $("#divConsul").stop().hide("slow");

                    //Verifica se está marcado para mostrar o encaminhamento, caso esteja, expande ele no espaço que antes estava a grid de Consultas
                    if ($("#chkEncam").attr("checked")) {
                        $("#divPaciEnca").css("height", "365px");
                    }
                }

                //Verifica qual das opções está selecionada para mostrar o dropdownlist de profissionais de saúde conforme desejo
                if ($("#chkEncam").attr("checked")) {
                    if ($("#chkConsul").attr("checked")) {
                        $("#divMedicCo").hide();
                        $("#divMedicEn").hide();
                        $("#divMedic").show();
                    } else {
                        $("#divMedicCo").hide();
                        $("#divMedicEn").show();
                        $("#divMedic").hide();
                    }
                } else if ($("#chkConsul").attr("checked")) {
                    $("#divMedic").hide();
                    $("#divMedicCo").show();
                    $("#divMedicEn").hide();
                }
            });

            // Seleção do Paciente
            $("#chkSelPacien").change(function () {
                if ($("#chkSelPacien").selected()) {
                    controleTab("tabSelPacien", "chkSelPacien");
                }
            });

            $("#ddlMedico").change(function (evento) {
                var e = document.getElementById("ddlMedico");
                var itSelec = e.options[e.selectedIndex].value;
                if (itSelec == "ENC") {
                    $("#ddlMedico").val("0");
                    //                    return false;
                }
                else if (itSelec == "CON") {
                    $("#ddlMedico").val("0");
                    //                    return false;
                }
            });

            // Atendimento Médico
            $("#chkAtdMedico").change(function () {
                if ($("#chkAtdMedico").selected()) {
                    controleTab("tabAtendMedic", "chkAtdMedico");
                }
            });

            //Requisição de Medicamento
            $("#chkReqMedicPaci").change(function () {
                if ($("#chkReqMedicPaci").selected()) {
                    controleTab("tabReqMedic", "chkReqMedicPaci");
                }
            });

            //Requisição de Exames
            $("#chkReqExamPaci").change(function () {
                if ($("#chkReqExamPaci").selected()) {
                    controleTab("tabReqExam", "chkReqExamPaci");
                }
            });

            //Requisição de Serv. Ambulatorial
            $("#chkReqSevAmbuPaci").change(function () {
                if ($("#chkReqSevAmbuPaci").selected()) {
                    controleTab("tabReqServAmbu", "chkReqSevAmbuPaci");
                }
            });

            //Requisição de Reserva de Medicamentos
            $("#chkResMedicamentos").change(function () {
                if ($("#chkResMedicamentos").selected()) {
                    controleTab("tabResMedicamentos", "chkResMedicamentos");
                }
            });

            //Registro de atestado médico
            $("#chkRegAtestMedcPaci").change(function () {
                if ($("#chkRegAtestMedcPaci").selected()) {
                    controleTab("tabRegAtestMedc", "chkRegAtestMedcPaci");
                }
            });

            //Registro de resultado de exame
            $("#chkRegResExame").change(function () {
                if ($("#chkRegResExame").selected()) {
                    controleTab("tabRegResExam", "chkRegResExame");
                }
            });

            //Encaminhamento médico
            $("#chkEncaMedicPaci").change(function () {
                if ($("#chkEncaMedicPaci").selected()) {
                    controleTab("tabEncamMed", "chkEncaMedicPaci");
                }
            });

            //Encaminhamento internação (tabEncamIntern)
            $("#chkEncaIntern").change(function () {
                if ($("#chkEncaIntern").selected()) {
                    controleTab("tabEncamIntern", "chkEncaIntern");
                }
            });

            //Tela de histórico de exames
            $("#chkExmPaci").change(function () {
                if ($("#chkExmPaci").selected()) {
                    controleTab("tabExamHist", "chkExmPaci");
                }
            });

            //Tela de histórico de consultas médicas
            $("#chkConsPaci").change(function () {
                if ($("#chkConsPaci").selected()) {
                    controleTab("tabConsHist", "chkConsPaci");
                }
            });

            //Tela de histórico de medicamentos
            $("#chkMedicPaci").change(function () {
                if ($("#chkMedicPaci").selected()) {
                    controleTab("tabMedcHist", "chkMedicPaci");
                }
            });

            //Tela de histórico de receitas médicas
            $("#chkHistReceiMedic").change(function () {
                if ($("#chkHistReceiMedic").selected()) {
                    controleTab("tabhistReceiMedic", "chkHistReceiMedic");
                }
            });

            //Tela de histórico de diagnósticos
            $("#chkDiagPaci").change(function () {
                if ($("#chkDiagPaci").selected()) {
                    controleTab("tabDiagHist", "chkDiagPaci");
                }
            });

            //Tela de histórico de diagnósticos
            $("#chkServAmbPaci").change(function () {
                if ($("#chkServAmbPaci").selected()) {
                    controleTab("tabServAmbuHist", "chkServAmbPaci");
                }
            });

            //Tela de histórico de atestados médicos
            $("#chkAtestMedcPaci").change(function () {
                if ($("#chkAtestMedcPaci").selected()) {
                    controleTab("tabAtestMedcHist", "chkAtestMedcPaci");
                }
            });

            //Tela de histórico de imagens
            $("#chkImgAtendPaci").change(function () {
                if ($("#chkImgAtendPaci").selected()) {
                    controleTab("tabImgMedcHist", "chkImgAtendPaci");
                }
            });
        }

        function AbreModalInfosCadas() {
            $('#divLoadInfosCadas').dialog({ autoopen: false, modal: true, width: 652, height: 350, resizable: false, title: "USUÁRIO DE SAÚDE - CADASTRO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function mostraModalExames() {
            //Mostra a grid de tipos de exames
            $('#divLoadShowExames').dialog({ autoopen: false, modal: true, width: 930, height: 420, resizable: false, title: "LISTA DE EXAMES",
                //                open: function () { $('#divLoadShowExames').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function mostraModalProce() {
            //Mostra a grid Procedimentos Médicos
            $('#divLoadShowServAmbu').dialog({ autoopen: false, modal: true, width: 930, height: 420, resizable: false, title: "LISTA DE PROCEDIMENTOS",
                //                open: function () { $('#divLoadShowServAmbu').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        //                var prm = Sys.WebForms.PageRequestManager.getInstance();
        //                prm.add_endRequest(function () {
        //                    JavscriptAtend();
        //                });
    </script>
</asp:Content>
