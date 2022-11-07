    <%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno.MatriculaResumida.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
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
            clear: none;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            width:102px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
            clear: both;
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
            margin-top: 6px;
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
            margin-top: 0px;
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
            margin-top: -3px;
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
            height: 145px;
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
            margin-top: 0px;
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
            width: 187px;
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
            width: 40px;
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
        
       .chkPgtoAluno label
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
            top: 107px;
            width: 250px;
            padding-top: 6px;
            border-right: 2px solid #CCCCCC;
            height: 100%;
        }
        #tabResp
        {
            position: fixed;
            right: 0;
            top: 106px;
            width: 748px;
            height: 380px;
            padding: 10px 0 0 10px;
        }
        #tabGradeMater 
        {
            position: fixed;
            right: 0;
            top: 106px;
            width: 748px;
            height: 410px;
            padding: 10px 0 0 10px;
        }
        #tabDocumentos
        {
            position: fixed;
            right: 0;
            top: 106px;
            width: 735px;
            height: 380px;
            padding: 10px 0 0 10px;
        }
        #tabUniMat
        {
            position: fixed;
            right: 0;
            top: 106px;
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
            width: 600px;
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
        #divBotoes .lilnkCarteira
        {
            background-color: #F0FFFF;
            border: 1px solid #D2DFD1;
            clear: none;
            margin-bottom: 4px;
            padding: 2px 3px 1px;
            margin-left: 2px;
            margin-right: 0px;
            width: 68px;
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
        .maskDin
        {
            text-align:right;
        }        
        .txtNoRespCPF
        {
            width: 220px;
            background-color: #FFF8DC !important;
        }
        .txtNoInfAluno
        {
            width: 220px;
            background-color: #FFF8DC !important;
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
       .divGrdChequePgto
        {
            border: 1px solid #CCCCCC;
            width: 720px;
            height: 106px;
            overflow-y: scroll;
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
            top: 109px;
            width: 750px;
            height: 380px;
            padding: 7px 0 0 20px;
        }
        #tabTelAdd, #tabEndAdd, #tabResAliAdd, #tabAtiExtAlu, #tabMenEsc, 
        {
            position: fixed;
            right: 0;
            top: 109px;
            width: 748px;
            height: 380px;
            padding: 7px 0 0 10px;
        }
        .tabFormaPgto
       {
            position: fixed;
            right: 0;
            top: 109px;
            width: 748px;
            height: 380px;
            padding: 7px 0 0 10px;
        }
        
        .tabMaterialColetivo
       {
            position: fixed;
            right: 0;
            top: 109px;
            width: 748px;
            height: 380px;
            padding: 7px 0 0 5px;
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
            width: 200px;
            height: 45px;
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
        
        .divGrdCurso 
        {
            height: 340px;
            width: 374px;
            border: solid #EEEEEE 1px;
            overflow-y: auto;
        }
        
        .divGrdCursoExt
        {
            height: 340px;
            width: 374px;
            border: solid #EEEEEE 1px;
            overflow-y: scroll;
        }
        
        .grdGrdCurso th
        {
            text-align: center !important;
        }

        .th 
        {
            position: relative;
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
        .lblchkPgto
        {
            font-weight:bold;
            color: #FFA07A;
            margin-left:-5px;
        }
        
        <%--================================================================================ MATERIAL COLETIVO ===================================================================--%>
                .liGridMat
        {
            margin-left: -20px;
            margin-top: 12px;
        }
        
        .infoFiledsC
        {
            margin-top: 5px;
        }
        
        .hrLinhaPgto
        {
            color:#CCCCCC; 
            background-color:#CCCCCC;
        }
        
        .ulInfos
        {
            width: 10px;
            margin-top: 15px;
            text-align: right;
        }
        .liEspaco
        {
            margin-left: 5px;
        }
        
        .liEspaco1
        {
            margin-left: 9px;
        }
        
        .liBtnGrdFinan
        {
            margin-top: 10px;
            margin-left: 2px;
            padding: 4px 3px 3px;
            background-color: #EE9572;
            border: 1px solid #8B8989;
        }
        
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 10px;
            margin-left: 150px !important;
            padding: 2px 3px 1px 3px;
            clear: both;
        }
        <%--================================================================================ MATERIAL COLETIVO ===================================================================--%>       
        
        <%--====================================== TESTE MODAL RESPONSÁVEL ==============================--%>
              
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
        <div id="divMensEfe" style="display:none; position:absolute; left:50%; right:50%; top:50%; z-index:1000; width:200px" clientidmode="Static">
                    <p id="pEfeM" clientidmode="Static" style="color:#6495ED; font-weight:bold">EFETIVANDO A MATRÍCULA...</p>
            <asp:Image runat="server" ID="imgLoad" ImageUrl="~/Navegacao/Icones/carregando.gif" style="z-index:99"></asp:Image>
        </div>
        <li style="margin-left: 5px; margin-top:-20px">
            <%--<label for="ddlSituMatAluno" class="lblObrigatorio" title="Tipo de Matrícula">
                Tipo de Matrícula</label>--%>
                <asp:Label runat="server" ID="lblttpsitu" Text="Tipo de Matrícula"></asp:Label>
            <asp:DropDownList ID="ddlSituMatAluno" CssClass="ddlSituMatAluno" ToolTip="Selecione o Tipo de Matrícula"
                runat="server">
                <asp:ListItem Value="R">Com Reserva</asp:ListItem>
                <asp:ListItem Value="S" Selected="true">Sem Reserva</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="width: 460px; margin-right: 0px; clear:both;">
            <div id="divReserva">
                <ul id="ulReserva">
                    <li style="margin-bottom: 2px;">
                        <h7 style="font-weight: bold; margin-bottom: 1px;">Reserva de Vagas</h7>
                    </li>
                    <li style="clear: both;"><span title="Número da Reserva">Nº</span>
                        <asp:TextBox ID="txtNumReserva" CssClass="txtNumReserva" runat="server" ToolTip="Informe o Número da Reserva"
                            MaxLength="20"></asp:TextBox>
                    </li>
                    <li id="liPesqReserva" class="liPesqReserva" runat="server">
                        <asp:ImageButton ID="btnPesqReserva" runat="server" OnClick="btnPesqReserva_Click"
                            ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" CssClass="btnPesqReserva" CausesValidation="false" />
                    </li>
                    <%-- <a href='<%= Request.Url.AbsoluteUri %>'>--%>
                    <li style="margin-top: -12px;">
                        <label title="Dados Reserva">
                            Dados Reserva</label>
                        <asp:TextBox ID="txtDadosReserva" CssClass="txtDadosReserva" Enabled="false" Width="170px"
                            runat="server"></asp:TextBox>
                    </li>
                    <li style="margin-top: -13px; margin-right: 0px;">
                        <label title="Unidade de Ensino">
                            Unid de Ensino</label>
                        <asp:DropDownList ID="ddlUnidade" AutoPostBack="true" CssClass="ddlUnidadeEscolar"
                            Width="160px" ToolTip="Selecione a Unidade de Ensino" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged"
                            runat="server">
                        </asp:DropDownList>
                    </li>
                </ul>
            </div>
        </li>                    
        <li style="margin-right: 0px;">
            <ul id="ulDadosMatricula">
                <li style="margin-top: 10px; margin-left: 15px; float: right;">
                    <div id="divBotoes">
                        <ul>
                            <li>
                                <div id="div4" class="bar" style="margin-left:100px">
                                    <div id="divBarraMatric" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
                                        <ul id="ulNavegacao" style="width: 43px;">
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
                                                <asp:ImageButton runat="server" title="Abre o formulario para Criar um Novo Registro." OnClick="btnNovo_onclick" alt="Icone de Criar Novo Registro."
                                                    src="/BarrasFerramentas/Icones/NovoRegistro.png"  width="20px" height="19px"/>
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
                            <li style="margin-top:17px;">
                            <ul>
                            <li id="lilnkEfetMatric" runat="server" title="Clique para Efetivar Matrícula" class="lilnkEfetMatric" style="margin-left:10px" clientidmode="Static">
                                <%--<asp:LinkButton ID="lnkEfetMatric" OnClientClick="geraPadrao(this);__doPostBack('lnkEfetMatric', '')" OnClick="lnkEfetMatric_Click" runat="server" Style="margin: 0 auto;" ToolTip="EFETIVAR MATRÍCULA" ValidationGroup="atuPgtoAlu">--%>
                                        <img id="imgEfetiMatric" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_CheckSucess.png'
                                        alt="Icone Pesquisa" title="EFETIVAR MATRÍCULA" clientidmode="Static"/>
                                <asp:LinkButton ID="lnkEfetMatric" runat="server" ValidationGroup="atuPgtoAlu" OnClientClick="geraPadrao(this);__doPostBack('lnkEfetMatric', '')" OnClick="lnkEfetMatric_Click" Text="EFETIVAR" ClientIDMode="Static">
                                </asp:LinkButton>
                                <asp:Label runat="server" id="lblEfeTOP" clientidmode="Static" style="display:none;">EFETIVAR</asp:Label>
                            </li>
                            <li id="lilnkRecMatric" runat="server" title="Clique para Imprimir Contrato de Matrícula"
                                class="lilnkRecMatric" style="clear:none !Important; margin-left:26px">
                                <asp:LinkButton ID="lnkRecMatric" OnClick="lnkRecMatric_Click" Enabled="false" runat="server"
                                    Style="margin: 0 auto;" ToolTip="Imprimir Recibo/Protocolo de Matrícula">
                                    <img id="imgRecMatric" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                                        alt="Icone Pesquisa" title="RECIBO MATRIC" />
                                    <asp:Label runat="server" ID="lblRecibo" Text="CONTRATO" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li id="lilnkFichaMatric" runat="server" title="Clique para Imprimir a ficha de Pré-Matrícula"
                                class="lilnkBolCarne" style="margin-left:10px">
                                <asp:LinkButton ID="lnkFichaMatric" Enabled="false" runat="server" Style="margin: 0 auto;"
                                    ToolTip="Imprimir Ficha de Pré-Matrícula" OnClick="lnkFichaMatric_Click">
                                    <img id="img3" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                                        alt="Icone Pesquisa" title="FICHA DE MATRÍCULA" /><asp:Label runat="server" ID="Label21"
                                            Text="FICHA" Style="margin-left: 4px;"></asp:Label></asp:LinkButton>
                            </li>
                            <li visible="false" id="lilnkBolCarne" runat="server" title="Clique para Imprimir Boleto de Mensalidades"
                                class="lilnkBolCarne" style="margin-left:10px">
                                <asp:LinkButton ID="lnkBolCarne" OnClick="lnkBolCarne_Click" Enabled="false" runat="server"
                                    Style="margin: 0 auto;" ToolTip="Imprimir Boleto de Mensalidades">
                                    <img id="imgBolCarne" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                                        alt="Icone Pesquisa" title="BOLETO/CARNÊ" />
                                    <asp:Label runat="server" ID="lblBoleto" Text="BOLETO" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li id="lilnkCarteira" runat="server" title="Clique para Imprimir a Carteira da Escola"
                                class="lilnkCarteira" style="margin-left:10px">
                                <asp:LinkButton ID="lnkCarteira" OnClick="lnkCarteira_Click" runat="server" Style="margin: 0 auto;"
                                    Enabled="false" ToolTip="Imprimir Carteira da Escola">
                                    <img id="img1" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                                        alt="Icone Pesquisa" title="Carteira da Escola" />
                                    <asp:Label runat="server" ID="Label7" Text="CARTEIRA" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li visible="false" id="lilnkRecibo" runat="server" title="Clique para Imprimir o Recibo de Pagamento"
                                class="lilnkBolCarne" style="margin-left:10px">
                                <asp:LinkButton ID="lnkRecibo" runat="server" Style="margin: 0 auto;"
                                    Enabled="false" ToolTip="Imprimir Recibo de Pagamento" OnClick="lnkRecibo_OnClick">
                                    <img id="img2" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                                        alt="Icone Pesquisa" title="Carteira da Escola" />
                                    <asp:Label runat="server" ID="Label27" Text="RECIBO" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            </ul>
                        </li>
                            <!--<li id="li7" runat="server" title="Clique para realizar o controle de cheques" class="lilnkRegPgto">
                                <a id="lnkCheque" runat="server" href="" style="cursor: pointer;">CHEQUE</a>
                            </li>
                            <li id="li5" runat="server" title="Clique para Realizar o Pagamento de Mensalidade(s)"
                                class="lilnkRegPgto"><a id="lnkRealiPagto" runat="server" href="" style="cursor: pointer;">
                                    REG PGTO</a> </li> -->
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
                    <!-- INICIO PARTE DAS INFORMAÇÕES DO RESPONSÁVEL -->
                    <li class="libtnInfResp">
                        <img class="imgInfResp" src='/Library/IMG/Gestor_Numero1Preto.png' alt="Icone Pesquisa"
                            title="Ativa Informações do Responsável." />
                        <asp:Label runat="server" ID="lblbtnInfResp" CssClass="lblTitInf" Text="INFORMAÇÕES RESPONSÁVEL/ALUNO"></asp:Label>
                        <asp:Label runat="server" ID="lblSucInfResp" CssClass="lblSucInfResp" Visible="false"
                            Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                    </li>
                    <li class="liTitInf">
                        <label class="lblSubTitInf">
                            Digite ou atualize os dados do Responsável/Aluno</label>
                    </li>
                    <li style="margin-left: 20px;"><span title="Número do CPF do Responsável">Nº CPF</span>
                        <asp:TextBox ID="txtCPFResp" CssClass="txtCPF" Style="width: 75px;" runat="server"
                            ToolTip="Informe o CPF do Responsável"></asp:TextBox>
                        <asp:HiddenField ID="hdfCPFRespRes" runat="server" />
                    </li>
                    <li id="liCPFResp" runat="server">
                        <asp:ImageButton ID="btnCPFResp" runat="server" OnClick="btnCPFResp_Click" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                            CssClass="btnPesqMat" CausesValidation="false" />
                    </li>
                    <li class="liPesqReserva"><a class="lnkPesResp" href="#">
                        <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade" /></a>
                    </li>
                    <li style="clear: none; margin-right: 0px;">
                        <asp:CheckBox CssClass="chkLocais" ID="chkRecResResp" TextAlign="Right" Enabled="false"
                            runat="server" ToolTip="Recuperar dados do responsável na reserva" Text="Recup Rsv"
                            AutoPostBack="True" OnCheckedChanged="chkRecResResp_CheckedChanged" />
                    </li>
                    <li style="clear: both; margin-left: 20px;">
                        <asp:TextBox ID="txtNoRespCPF" CssClass="txtNoRespCPF" runat="server" Enabled="False"></asp:TextBox>
                    </li>
                    <li style="margin-left: 20px;">
                        <label for="txtNoInfAluno" title="Nire e nome do aluno" class="lblObrigatorio">
                            Aluno</label>
                        <asp:TextBox ID="txtNoInfAluno" CssClass="txtNoInfAluno" ToolTip="Nire e nome do aluno" Style="color: Black !important;"
                            runat="server" Enabled="False"></asp:TextBox>
                    </li>

                    <!-- FIM PARTE DAS INFORMAÇÕES DO RESPONSÁVEL -->
                    <!-- INICIO PARTE DAS INFORMAÇÕES DO ALUNO -->
                    <%--<li style="margin-top: 2px;">
                        <img class="imgInfResp" src='/Library/IMG/Gestor_Numero2Vermelho.png' alt="Icone Pesquisa"
                            title="Ativa Informações do Aluno." />
                        <asp:Label runat="server" ID="Label2" CssClass="lblTitInf" Text="INFORMAÇÕES DO ALUNO"></asp:Label>
                        <asp:Label runat="server" ID="lblSucInfAlu" CssClass="lblSucInfAlu" Visible="false"
                            Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                    </li>
                    <li style="clear: both; margin-left: 20px;"><span title="Nº NIRE do Aluno">Nº NIRE</span>
                        <asp:TextBox ID="txtNumNIRE" CssClass="txtNire" runat="server" ToolTip="Informe o Nº NIRE do Aluno"></asp:TextBox>
                    </li>
                    <li id="Li1" runat="server">
                        <asp:ImageButton ID="btnPesqNIRE" runat="server" OnClick="btnPesqNIRE_Click" Enabled="false"
                            ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" CssClass="btnPesqMat" CausesValidation="false" />
                    </li>
                    <li class="liPesqReserva"><a class="lnkPesNIRE" href="#">
                        <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" title="Pesquisa habilitada só para matrícula sem reserva."
                            alt="Icone" /></a> </li>
                    <li style="margin-left: 20px;">
                        <asp:TextBox ID="txtNoInfAluno" CssClass="txtNoInfAluno" Style="color: Black !important;"
                            runat="server" Enabled="False"></asp:TextBox>
                    </li>--%>
                    <!-- FIM PARTE DAS INFORMAÇÕES DO ALUNO -->
                    <!-- INICIO PARTE DAS INFORMAÇÕES DA MATRÍCULAS -->
                    <li style="margin-top: 2px;">
                        <img class="imgInfResp" src='/Library/IMG/Gestor_Numero2Vermelho.png' alt="Icone Pesquisa"
                            title="Ativa Dados de Matrícula." />
                        <asp:Label runat="server" ID="Label5" CssClass="lblTitInf" Text="DADOS DE MATRÍCULA"></asp:Label>
                    </li>
                    <li style="margin-top: 3px; margin-left: 20px;"><span style="margin-right: 3px;"
                        title="Ano">Ano</span>
                        <asp:TextBox ID="txtAno" ValidationGroup="ModSerTur" CssClass="txtAno" MaxLength="4"
                            Enabled="false" Width="26px" runat="server" AutoPostBack="True" OnTextChanged="txtAno_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAno"
                            ValidationGroup="ModSerTur" ErrorMessage="Ano deve ser informado" Display="None"></asp:RequiredFieldValidator>
                        <asp:Label runat="server" ID="lblSucDadosMatr" CssClass="lblSucDadosMatr" Visible="false"
                            Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                    </li>
                    <li>
                        <ul>
<%--                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
--%>                                    <li id="liModalidade" class="liLeft" style="margin-left: 20px; margin-top: -3px;
                                        clear: both;">
                                        <label for="ddlModalidade" title="Modalidade" class="lblObrigatorio">
                                            Modalidade de Ensino</label>
                                        <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade de Ensino" Enabled="false"
                                            CssClass="campoModalidade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlModalidade"
                                            ValidationGroup="ModSerTur" ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                    </li>
                                    <li id="liSerie" style="margin-top: -3px;">
                                        <label for="ddlSerieCurso" class="lblObrigatorio" title="Série/Curso">
                                            Curso</label>
                                        <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" Enabled="false" ToolTip="Selecione a Série/Curso"
                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="ddlSerieCurso"
                                            ValidationGroup="ModSerTur" ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                    </li>
                                    <%-- <li id="li7" class="liLeft" style="margin-top: -2px; clear: both; margin-left:20px;">
                                <label for="txtAno" title="Turma de Cadastro">
                                    Turma Cad</label>
                                <asp:TextBox ID="txtTurmaCadas" style="padding-left: 2px; height: 13px;" Enabled="false" Width="50px" runat="server"></asp:TextBox>
                            </li>--%>
                                    <li id="liTurma" style="margin-top: -2px; margin-left: 20px; clear: both;">
                                        <label for="ddlTurma" class="lblObrigatorio" title="Turma Matrícula">
                                            Turma Matrícula</label>
                                        <asp:DropDownList ID="ddlTurma" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged"
                                            Enabled="false" AutoPostBack="true" ToolTip="Selecione a Turma de Matrícula"
                                            Width="90px" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="ddlTurma"
                                            ValidationGroup="ModSerTur" ErrorMessage="Turma deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                    </li>
                                    <li style="margin-top: -2px; margin-left: 4px; margin-right: 4px">
                                        <label for="txtTurno" title="Turno">
                                            Turno</label>
                                        <asp:TextBox ID="txtTurno" Enabled="false" CssClass="txtTurno" Width="45px" Style="padding-left: 4px;"
                                            runat="server"></asp:TextBox>
                                    </li>
<%--                                </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </ul>
                    </li>
                    <!-- FIM PARTE DAS INFORMAÇÕES DA MATRÍCULA -->
                    <!-- INICIO PARTE DAS INFORMAÇÕES DAS MENSALIDADES -->
                    <li style="display:none; margin-top: 2px;">
                        <img runat="server" id="imgNumeroMensalidades" class="imgInfResp" src='/Library/IMG/Gestor_Numero3Vermelho.png'
                            alt="Icone Pesquisa" title="Ativa Mensalidades Escolares do Aluno." />
                        <asp:Label runat="server" ID="Label4" CssClass="lblTitInf" Text="MENSALIDADES ESCOLARES"></asp:Label>
                    </li>
                    <li style="display:none;" class="liTitInf">
                        <label class="lblSubTitInf">
                            Confirme as informações dos itens abaixo:</label>
                    </li>
                    <li class="lichkUniforme" style="display:none; margin-left: 16px;">
                        <asp:CheckBox CssClass="chkMenEscAlu" Enabled="false" ClientIDMode="Static" ID="chkMenEscAlu"
                            runat="server" Text="Gerar Mensalidades Escolares" />
                        <asp:Label runat="server" ID="lnkSucMenEscAlu" ClientIDMode="Static" CssClass="lblSucInfAlu"
                            Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                    </li>
					<li class="lichkUniforme" style="display:none; margin-left: 16px; margin-top:2px">
						<asp:CheckBox CssClass="chkPgtoAluno" Enabled="false" ClientIDMode="Static" ID="chkPgtoAluno"
                            runat="server" Text="Forma de Pagamento da Matrícula" />
                        <asp:Label runat="server" ID="lnkSucPagtoEscAlu" ClientIDMode="Static" CssClass="lblSucInfAlu"
                            Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
					</li>
                    <!-- FIM PARTE DAS INFORMAÇÕES DAS MENSALIDADES -->
                    <!-- INICIO PARTE DAS INFORMAÇÕES ADICIONAIS DO ALUNO -->
                    <li style="display:none; margin-top: 2px;">
                        <div runat="server" id="divInformacoesAdd">
                            <ul>
                                <li style="margin-top: 2px;">                                                
                                    <img runat="server" id="imgNumeroInformacoesAdd" class="imgInfResp" src='/Library/IMG/Gestor_Numero4Preto.png' alt="Icone Pesquisa" title="Ativa Informações Adicionais do Aluno." />
                                    <asp:Label runat="server" ID="Label3" CssClass="lblTitInf" Text="INFORMAÇÕES ADICIONAIS"></asp:Label>
                                </li>
                                <li class="liTitInf" style="margin-bottom: -4px;">
                                    <label class="lblSubTitInf">
                                        Marque uma das opções abaixo e informe:</label>                        
                                </li>
                                <li id="liInfAdd">
                                    <ul class="ulServs" style="margin-left: 19px;">                              
                                        <li class="G2Clear">
                                            <asp:CheckBox CssClass="chkEndAddAlu" Enabled="false" ClientIDMode="Static" ID="chkEndAddAlu" runat="server" Text="Endereços Adicionais"/>                               
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
                                            <asp:CheckBox CssClass="chkResAliAlu" Enabled="false" ClientIDMode="Static" ID="chkResAliAlu" runat="server" Text="Restrições Alimentares"/>                                
                                            <asp:Label runat="server" ID="lblSucResAliAlu" ClientIDMode="Static" CssClass="lblSucInfAlu" Visible="false" style="color:Red;clear:none;font-weight:bold;" Text="OK!"></asp:Label>
                                        </li>                         
                                        <li class="G2Clear">         
                                            <asp:CheckBox CssClass="chkRegAtiExt" Enabled="false" ClientIDMode="Static" ID="chkRegAtiExt" runat="server" Text="Registro de Atividades Extra"/>                                                                                     
                                            <asp:Label runat="server" ID="lblSucRegAtiExt" ClientIDMode="Static" CssClass="lblSucInfAlu" Visible="false" style="color:Red;clear:none;font-weight:bold;" Text="OK!"></asp:Label>
                                        </li>
                                        <li class="lichkMatEsc" style="display: none;">
                                            <asp:CheckBox CssClass="chkMatEsc" Enabled="false" ClientIDMode="Static" ID="chkMatEsc" runat="server" Text="Registro de Mater. Coletivo e Uniforme"/>   
                                            <asp:Label runat="server" ID="lblSucMatEsc" ClientIDMode="Static" CssClass="lblSucInfAlu" Visible="false" style="color:Red;clear:none;font-weight:bold;" Text="OK!"></asp:Label>                             
                                        </li>
                                        <li class="lichkUniforme">
                                            <asp:CheckBox CssClass="chkDocMat" Enabled="false" ClientIDMode="Static" ID="chkDocMat" runat="server" Text="Entrega de Documentos de Matrícula"/>                                
                                            <asp:Label runat="server" ID="lblSucDocAlu" ClientIDMode="Static" CssClass="lblSucInfAlu" Visible="false" style="color:Red;clear:none;font-weight:bold;" Text="OK!"></asp:Label>
                                        </li>
                                       <li class="lichkUniforme">
                                            <asp:CheckBox CssClass="chkMatrColet" Enabled="false" ClientIDMode="Static" ID="chkMatrColet" runat="server" Text="Registro de Matr. Coletivo e Uniforme"/>                                
                                            <asp:Label runat="server" ID="lblMatrColet" ClientIDMode="Static" CssClass="lblMatrColet" Visible="false" style="color:Red;clear:none;font-weight:bold;" Text="OK!"></asp:Label>
                                        </li>
                                    </ul>    
                                </li>
                            </ul>
                        </div>  
                    </li>
                    <!-- FIM PARTE DAS INFORMAÇÕES ADICIONAIS DO ALUNO -->
                </ul>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                              FIM MENU LATERAL                                                                                              -->
            <!-- =========================================================================================================================================================================== -->
            <!-- INÍCIO DA INFORMAÇÃO DE ALUNO -->
            <!-- FIM DA INFORMAÇÃO DE ALUNO -->
            <!-- INÍCIO DA INFORMAÇÃO DE RESPONSÁVEL -->
            <div id="tabResp" clientidmode="Static" runat="server">
                <ul id="ul1" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; background-color: #87CEEB;
                        margin-top: -7px; margin-bottom: 0px;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            INFORMAÇÕES DE RESPONSÁVEL DE ALUNO</label>
                    </li>
                    <li id="liPhoto" class="liPhoto">
                        <ul>
                            <li>
                                <img id="imgResp" runat="server" alt="Imagem" style="height: 85px; width: 64px; border-width: 0px;"
                                    src="../../../../../Library/IMG/Gestor_SemImagem.png" />
                            </li>
                        </ul>
                    </li>
                    <li id="liDados1" class="liDados1" style="height: 105px;">
                        <ul>
                            <li style="width: 253px;">
                                <label for="txtNomeResp" class="lblObrigatorio" title="Nome do Responsável">
                                    Nome</label>
                                <asp:TextBox ID="txtNomeResp" CssClass="txtNome" ToolTip="Informe o Nome do Responsável"
                                    runat="server" Width="253px" MaxLength="60"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNome" runat="server" ValidationGroup="resp" ControlToValidate="txtNomeResp"
                                    ErrorMessage="Nome deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                            </li>
                            <li style="margin-left: 5px;">
                                <label for="ddlSexoResp" title="Sexo">
                                    Sexo</label>
                                <asp:DropDownList ID="ddlSexoResp" ToolTip="Selecione o Sexo" CssClass="ddlSexoResp"
                                    runat="server">
                                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                                    <asp:ListItem Value="F">Feminino</asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li style="margin-left: 5px;">
                                <label for="txtDtNascResp" title="Data de Nascimento" class="lblObrigatorio">
                                    Data Nascimento</label>
                                <asp:TextBox ID="txtDtNascResp" ToolTip="Informe a Data de Nascimento" CssClass="txtDtNascimentoResp campoData"
                                    runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDtNasc" ValidationGroup="resp" runat="server"
                                    ControlToValidate="txtDtNascResp" ErrorMessage="Data de Nascimento deve ser informada"
                                    Text="*" Display="None"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revDtNasc" ValidationGroup="resp" ControlToValidate="txtDtNascResp"
                                    runat="server" ErrorMessage="Data de Nascimento inválida" ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$"
                                    Display="None"></asp:RegularExpressionValidator>
                            </li>
                            <li style="margin-left: 5px;">
                                <label title="Tipo de Sangue">
                                    Tp.Sanguíneo</label>
                                <asp:DropDownList ID="ddlTpSangueResp" ToolTip="Selecione o Tipo de Sangue" CssClass="ddlTpSangueAluno"
                                    runat="server">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="A">A</asp:ListItem>
                                    <asp:ListItem Value="B">B</asp:ListItem>
                                    <asp:ListItem Value="AB">AB</asp:ListItem>
                                    <asp:ListItem Value="O">O</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlStaTpSangueResp" ToolTip="Selecione o Status do Tipo de Sangue"
                                    CssClass="ddlStaTpSangueAluno" runat="server">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="P">+</asp:ListItem>
                                    <asp:ListItem Value="N">-</asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li style="margin-left: 5px; width: 135px;">
                                <label for="ddlEstadoCivilResp" title="Estado Civil">
                                    Estado Civil</label>
                                <asp:DropDownList ID="ddlEstadoCivilResp" Width="135px" ToolTip="Selecione o Estado Civil"
                                    CssClass="ddlEstadoCivilResp" runat="server">
                                    <asp:ListItem Value="">Não Informado</asp:ListItem>
                                    <asp:ListItem Value="S">Solteiro(a)</asp:ListItem>
                                    <asp:ListItem Value="C">Casado(a)</asp:ListItem>
                                    <asp:ListItem Value="S">Separado Judicialmente</asp:ListItem>
                                    <asp:ListItem Value="D">Divorciado(a)</asp:ListItem>
                                    <asp:ListItem Value="V">Viúvo(a)</asp:ListItem>
                                    <asp:ListItem Value="P">Companheiro(a)</asp:ListItem>
                                    <asp:ListItem Value="U">União Estável</asp:ListItem>
                                    <asp:ListItem Value="O">Outro</asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <li class="liClear">
                                        <label for="ddlNacioResp" title="Nacionalidade">
                                            Nacionalidade</label>
                                        <asp:DropDownList ID="ddlNacioResp" ToolTip="Selecione a Nacionalidade do Responsável"
                                            CssClass="ddlNacioResp" OnSelectedIndexChanged="ddlNacioResp_SelectedIndexChanged"
                                            AutoPostBack="true" runat="server">
                                            <asp:ListItem Value="B">Brasileira</asp:ListItem>
                                            <asp:ListItem Value="E">Estrangeira</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <label for="txtNaturalidadeResp" title="Cidade de Naturalidade do Responsável">
                                            Naturalidade</label>
                                        <asp:TextBox ID="txtNaturalidadeResp" CssClass="txtNaturalidadeAluno" runat="server"
                                            ToolTip="Informe a Cidade de Naturalidade do Responsável" MaxLength="40"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label for="ddlUfNacionalidadeResp" title="UF de Nacionalidade do Responsável">
                                            UF</label>
                                        <asp:DropDownList ID="ddlUfNacionalidadeResp" CssClass="campoUf" runat="server" ToolTip="Informe a UF de Nacionalidade">
                                        </asp:DropDownList>
                                    </li>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <li style="margin-left: 5px;">
                                <label for="ddlGrauInstrucaoResp" title="Escolaridade">
                                    Escolaridade</label>
                                <asp:DropDownList ID="ddlGrauInstrucaoResp" ToolTip="Selecione a Escolaridade" CssClass="ddlGrauInstrucaoResp"
                                    runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvGrauInstrucao" ValidationGroup="resp" runat="server"
                                    ControlToValidate="ddlGrauInstrucaoResp" ErrorMessage="Escolaridade deve ser informada"
                                    Text="*" Display="None"></asp:RequiredFieldValidator>
                            </li>
                            <li style="margin-left: 5px; width: 210px">
                                <label for="txtMaeResp" title="Nome da Mãe do Responsável">
                                    Nome da Mãe</label>
                                <asp:TextBox ID="txtMaeResp" ToolTip="Informe o Nome da Mãe do Responsável" CssClass="txtMaeResp"
                                    runat="server" MaxLength="60"></asp:TextBox>
                            </li>
                            <li id="liNIS" style="margin-left: 10px;">
                                <label for="txtNISResp" title="NIS">
                                    Nº NIS</label>
                                <asp:TextBox ID="txtNISResp" Width="73px" ToolTip="Informe o NIS" CssClass="txtNISRespValid"
                                    runat="server" MaxLength="15"></asp:TextBox>
                            </li>
                            <li class="liClear" style="margin-left: -10px; margin-right: 0px;">
                                <fieldset class="fldIdentidade">
                                    <legend class="lblObrigatorio">CPF</legend>
                                    <ul>
                                        <li>
                                            <label for="txtCPFRespDados" title="CPF">
                                                Número</label>
                                            <asp:TextBox ID="txtCPFRespDados" ToolTip="Informe o CPF" Width="75px" CssClass="txtCPF"
                                                runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCPF" ValidationGroup="resp" runat="server" ControlToValidate="txtCPFRespDados"
                                                ErrorMessage="CPF deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="hdCodResp" runat="server" />
                                        </li>
                                    </ul>
                                </fieldset>
                            </li>
                            <li style="margin-right: 0px; margin-left: 0px;">
                                <fieldset id="fldIdentidade" class="fldIdentidade" style="padding: 0px;">
                                    <legend class="lblObrigatorio">Carteira de Identidade</legend>
                                    <ul>
                                        <li id="liNrIdentidade" class="liNrIdentidade">
                                            <label for="txtIdentidadeResp" title="Número">
                                                Número</label>
                                            <asp:TextBox ID="txtIdentidadeResp" ToolTip="Informe o Número da Identidade" CssClass="txtIdentidadeResp"
                                                runat="server" MaxLength="20"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="resp" runat="server"
                                                ControlToValidate="txtIdentidadeResp" ErrorMessage="Número do RG deve ser informado"
                                                Text="*" Display="None"></asp:RequiredFieldValidator>
                                        </li>
                                        <li id="liDtEmissaoIden" class="liDtEmissaoIden">
                                            <label for="txtDtEmissaoResp" title="Data de Emissão">
                                                Data Emissão</label>
                                            <asp:TextBox ID="txtDtEmissaoResp" ToolTip="Informe a Data de Emissão" CssClass="txtDtEmissaoResp campoData"
                                                runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="resp"
                                                ControlToValidate="txtDtEmissaoResp" runat="server" ErrorMessage="Data de Emissão inválida"
                                                ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$"
                                                Display="None"></asp:RegularExpressionValidator>
                                        </li>
                                        <li id="liOrgEmissor" class="liOrgEmissor">
                                            <label for="txtOrgEmissorResp" title="Orgão Emissor" class="lblObrigatorio">
                                                Orgão Emissor</label>
                                            <asp:TextBox ID="txtOrgEmissorResp" ToolTip="Informe o Orgão Emissor" CssClass="txtOrgEmissorResp"
                                                runat="server" MaxLength="10"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="resp" runat="server"
                                                ControlToValidate="txtOrgEmissorResp" ErrorMessage="Órgão Emissor deve ser informado"
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
                            <li style="border-width: 0px; padding-left: 0px;">
                                <fieldset id="Fieldset3" class="fldFiliaResp">
                                    <legend>INFORMAÇÃO DE CONTATO</legend>
                                    <ul>
                                        <li>
                                            <label for="txtEmailResp" title="Email">
                                                Email</label>
                                            <asp:TextBox ID="txtEmailResp" ToolTip="Informe o Email do Responsável" Width="195px"
                                                CssClass="txtEmailResp" runat="server" MaxLength="255"></asp:TextBox>
                                        </li>
                                        <li>
                                            <label for="txtTelCelularResp" title="Telefone Celular">
                                                Telefone Celular</label>
                                            <asp:TextBox ID="txtTelCelularResp" ToolTip="Informe o Telefone Celular" CssClass="txtTelCelularResp" Width="70px"
                                                runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtTelCelularRespNonoDig" ToolTip="Informe o Telefone Celular" CssClass="txtTelCelularNoveDigitos" Width="80px"
                                                runat="server" Visible="false"></asp:TextBox>
                                        </li>
                                    </ul>
                                </fieldset>
                            </li>
                        </ul>
                    </li>
                    <li class="liDados3">
                        <ul>
                            <li>
                                <ul>
                                    <li>
                                        <ul>
                                            <li class="liEndResResp">
                                                <fieldset id="Fieldset1" class="fldFiliaResp">
                                                    <legend>ENDEREÇO RESIDENCIAL</legend>
                                                    <ul>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <li class="liCep">
                                                                    <label for="txtCepResp" title="Cep" class="lblObrigatorio">
                                                                        Cep</label>
                                                                    <asp:TextBox ID="txtCepResp" ToolTip="Informe o Cep" CssClass="campoCepValid" runat="server" Width="60px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvCep" ValidationGroup="resp" runat="server" ControlToValidate="txtCepResp"
                                                                        ErrorMessage="CEP deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                                                                </li>
                                                                <li id="liPesqCEPR" class="liPesqCEPResp" runat="server">
                                                                    <asp:ImageButton ID="btnPesqCEPR" runat="server" OnClick="btnPesqCEPR_Click" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                                                                        CssClass="btnPesqMat" CausesValidation="false" />
                                                                </li>
                                                                <li>
                                                                    <label for="ddlUfResp" title="UF" class="lblObrigatorio">
                                                                        UF</label>
                                                                    <asp:DropDownList ID="ddlUfResp" ToolTip="Selecione a UF" CssClass="ddlUF" runat="server"
                                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlUfResp_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvUf" ValidationGroup="resp" runat="server" ControlToValidate="ddlUfResp"
                                                                        ErrorMessage="UF deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                                                                </li>
                                                                <li>
                                                                    <label for="ddlCidadeResp" title="Cidade" class="lblObrigatorio">
                                                                        Cidade</label>
                                                                    <asp:DropDownList ID="ddlCidadeResp" ToolTip="Selecione a Cidade" CssClass="ddlCidadeResp"
                                                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCidadeResp_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvddlCidadeResp" ValidationGroup="resp" runat="server"
                                                                        ControlToValidate="ddlCidadeResp" ErrorMessage="Cidade deve ser informada" Text="*"
                                                                        Display="None"></asp:RequiredFieldValidator>
                                                                </li>
                                                                <li>
                                                                    <label for="ddlBairroResp" title="Bairro" class="lblObrigatorio">
                                                                        Bairro</label>
                                                                    <asp:DropDownList ID="ddlBairroResp" Width="120px" ToolTip="Selecione o Bairro" CssClass="ddlBairroResp"
                                                                        runat="server">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvddlBairroResp" ValidationGroup="resp" runat="server"
                                                                        ControlToValidate="ddlBairroResp" ErrorMessage="Bairro deve ser informado" Text="*"
                                                                        Display="None"></asp:RequiredFieldValidator>
                                                                </li>
                                                                <li>
                                                                    <label for="txtLogradouroResp" class="lblObrigatorio" title="Logradouro">
                                                                        Logradouro</label>
                                                                    <asp:TextBox ID="txtLogradouroResp" CssClass="txtLogradouroResp" Width="180px" ToolTip="Informe o Logradouro"
                                                                        runat="server" MaxLength="40"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvLogradouro" ValidationGroup="resp" runat="server"
                                                                        ControlToValidate="txtLogradouroResp" ErrorMessage="Endereço deve ser informado"
                                                                        Text="*" Display="None"></asp:RequiredFieldValidator>
                                                                </li>
                                                                <li class="liNumero">
                                                                    <label for="txtNumeroResp" title="Número">
                                                                        Nr</label>
                                                                    <asp:TextBox ID="txtNumeroResp" ToolTip="Informe o Número" Width="30px" CssClass="txtNumeroResp"
                                                                        runat="server" MaxLength="5"></asp:TextBox>
                                                                </li>
                                                                <li style="clear:both;">
                                                                    <label for="txtComplementoResp" title="Complemento">
                                                                        Complemento</label>
                                                                    <asp:TextBox ID="txtComplementoResp" ToolTip="Informe o Complemento" CssClass="txtComplementoResp"
                                                                        runat="server" MaxLength="30"></asp:TextBox>
                                                                </li>
                                                                <li>
                                                                    <label for="txtTelResidencialResp" title="Telefone Residencial">
                                                                        Telefone Fixo</label>
                                                                    <asp:TextBox ID="txtTelResidencialResp" ToolTip="Informe o Telefone Residencial"
                                                                        CssClass="txtTelResidencialResp" runat="server"></asp:TextBox>
                                                                </li>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ul>
                                                </fieldset>
                                            </li>
                                        </ul>
                                    </li>
                                    <li style="clear: none; width: 370px; height: 155px;">
                                        <ul>
                                            <li class="liClear" style="border-width: 0px;">
                                                <fieldset class="fldFiliaResp" style="border-width: 0px;">
                                                    <legend>INFORMAÇÂO PROFISSIONAL</legend>
                                                    <ul style="margin-top: 4px;">
                                                        <li class="liClear">
                                                            <label for="ddlProfissaoResp" title="Profissão">
                                                                Profissão</label>
                                                            <asp:DropDownList ID="ddlProfissaoResp" ToolTip="Selecione a Profissão do Responsável"
                                                                CssClass="ddlProfissaoResp" runat="server">
                                                            </asp:DropDownList>
                                                        </li>
                                                        <li>
                                                            <label>Nome Empresa</label>
                                                            <asp:TextBox runat="server" ID="txtNomeEmpresaResp" Width="80px"></asp:TextBox>
                                                        </li>
                                                        <li id="liTrabResp">
                                                            <label>
                                                                Trabalhando?/Desde?</label>
                                                            <asp:DropDownList ID="ddlTrabResp" ToolTip="Responsável Trabalhando?" CssClass="ddlTrabResp"
                                                                runat="server">
                                                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                                                <asp:ListItem Value="N">Não</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtMesAnoTrabResp" ToolTip="Mês/Ano de Início de Trabalho do Responsável"
                                                                CssClass="txtMesAnoTrabResp" runat="server"></asp:TextBox>
                                                        </li>
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <li class="liClear">
                                                                    <label for="txtCepEmpresaResp" title="Cep">
                                                                        Cep</label>
                                                                    <asp:TextBox ID="txtCepEmpresaResp" ToolTip="Informe o Cep" CssClass="txtCepResp"
                                                                        runat="server"></asp:TextBox>
                                                                </li>
                                                                <li id="li6" class="liPesqCEPResp" runat="server">
                                                                    <asp:ImageButton ID="btnCEPEmp" runat="server" OnClick="btnCEPEmp_Click" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                                                                        CssClass="btnPesqMat" CausesValidation="false" />
                                                                </li>
                                                                <li>
                                                                    <label for="ddlUfEmpResp" title="UF">
                                                                        UF</label>
                                                                    <asp:DropDownList ID="ddlUfEmpResp" ToolTip="Selecione a UF" CssClass="ddlUF" runat="server"
                                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlUfEmpResp_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </li>
                                                                <li>
                                                                    <label for="ddlCidadeR" title="Cidade">
                                                                        Cidade</label>
                                                                    <asp:DropDownList ID="ddlCidadeEmpResp" Width="144px" ToolTip="Selecione a Cidade"
                                                                        CssClass="ddlCidadeResp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCidadeEmpResp_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </li>
                                                                <li>
                                                                    <label for="txtTelEmpresaResp" title="Telefone Trabalho">
                                                                        Telefone Trabalho</label>
                                                                    <asp:TextBox ID="txtTelEmpresaResp" ToolTip="Informe o Telefone Trabalho" CssClass="txtTelEmpresaResp"
                                                                        runat="server"></asp:TextBox>
                                                                </li>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ul>
                                                </fieldset>
                                            </li>
                                            <li class="liClear" style="height: 80px;">
                                                <fieldset id="Fieldset2" class="fldFiliaResp">
                                                    <legend>OBSERVAÇÕES</legend>
                                                    <ul>
                                                        <li id="liObsResponsavel" class="liObsResponsavel">
                                                            <asp:TextBox ID="txtObservacoesResp" CssClass="txtObservacoesResp" ToolTip="Informe as Observações sobre o Responsável"
                                                                runat="server" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 500);"></asp:TextBox>
                                                        </li>
                                                        <li>
                                                            <asp:CheckBox CssClass="chkLocais" ID="chkResponAluno" TextAlign="Right" runat="server"
                                                                ToolTip="Responsável é Aluno" Text="Responsável é Aluno" />
                                                        </li>
                                                        <li>
                                                            <asp:CheckBox CssClass="chkLocais" ID="chkRespFunc" TextAlign="Right" runat="server"
                                                                ToolTip="Responsável é Funcionário" Text="Responsável é Funcionário" />
                                                        </li>
                                                        <li onclick="" id="li2" runat="server" title="Clique para Registrar o Responsável"
                                                            style="background-color: #F1FFEF; border: 1px solid #D2DFD1; margin-left: 5px;
                                                            margin-top: 5px; padding: 2px 3px 1px 3px;">
                                                            <asp:LinkButton ID="btnAddRespon" ValidationGroup="resp" CssClass="btnAddRespon"
                                                                runat="server" OnClick="imgAdd_Click">FINALIZAR RESPONSÁVEL</asp:LinkButton>
                                                        </li>
                                                    </ul>
                                                </fieldset>
                                            </li>
                                        </ul>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                </ul>
                <ul id="ul2" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -7px;
                        background-color: #87CEFA; margin-bottom: 0px;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            INFORMAÇÕES CADASTRAIS DO ALUNO - CADASTRO BÁSICO</label>
                    </li>
                    <li class="liPhotoA">
                        <uc1:ControleImagem ID="ControleImagemAluno" runat="server" />
                    </li>
                    <li style="width: 670px; margin-left: 5px; margin-right: 0px;">
                        <ul>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <li class="liDist" style="margin-top: 0px">
                                        <label for="txtNireAluno" title="Número do NIRE">
                                            N° NIRE/SIGE</label>
                                        <asp:TextBox ID="txtNireAluno" CssClass="txtNireAluno" runat="server" ToolTip="Informe o Número do NIRE">
                                        </asp:TextBox>
                                    </li>
                                    <li id="Li10" style="margin-top:10px;" runat="server">
                                        <asp:ImageButton ID="btnPesqNIRE" runat="server" OnClick="btnPesqNIRE_Click" Enabled="false"
                                            ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" CssClass="btnPesqMat" CausesValidation="false" />
                                    </li>
                                    <li class="liPesqReserva" style="margin-top:10px;"><a class="lnkPesNIRE" href="#">
                                        <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" title="Pesquisa habilitada só para matrícula sem reserva."
                                            alt="Icone" /></a> </li>
                                    <li class="liddlSexoAlu" style="margin-top: 0px">
                                        <label for="txtNomeAluno" class="lblObrigatorio" title="Nome do Aluno">
                                            Nome</label>
                                        <asp:TextBox ID="txtNomeAluno" Width="187px" Style="text-transform: uppercase;" runat="server"
                                            CssClass="campoNomePessoa" ToolTip="Informe o Nome do Aluno" MaxLength="80"></asp:TextBox>
                                        <asp:HiddenField ID="hdCodAluno" runat="server" />
                                        <asp:HiddenField ID="hdCodReserva" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ValidationGroup="ALUNO"
                                            runat="server" ControlToValidate="txtNomeAluno" ErrorMessage="Nome deve ser informado"
                                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                    </li>
                                    <li class="liddlSexoAlu" style="margin-top: 0px">
                                        <label for="txtApelAluno" title="Apelido do Aluno">
                                            Apelido</label>
                                        <asp:TextBox ID="txtApelAluno" Style="text-transform: uppercase;" runat="server"
                                            Width="78px" ToolTip="Informe o Apelido do Aluno" MaxLength="15"></asp:TextBox>
                                    </li>
                                    <li style="margin-top: 0px">
                                        <label for="txtCpfAluno" title="CPF do Aluno">
                                            CPF</label>
                                        <asp:TextBox ID="txtCpfAluno" ToolTip="Informe o CPF do Aluno" runat="server" CssClass="campoCpf"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label for="fl_cpf_valido" class="lblObrigatorio" title="CPF válido do Aluno">
                                                CPF válido</label>
                                        <asp:CheckBox CssClass="chkLocais" ID="fl_cpf_valido" TextAlign="Right" runat="server"
                                            ToolTip="CPF do aluno é válido" />
                                    </li>
                                    <li style="margin-top: 0px">
                                        <label for="txtDataNascimentoAluno" class="lblObrigatorio" title="Data de Nascimento">
                                            Data Nascimento</label>
                                        <asp:TextBox ID="txtDataNascimentoAluno" CssClass="campoData" runat="server" ToolTip="Informe a Data de Nascimento"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ValidationGroup="ALUNO"
                                            ControlToValidate="txtDataNascimentoAluno" ErrorMessage="Data de Nascimento deve ser informada"
                                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                    </li>
                                    <li class="liddlSexoAlu" style="margin-top: 0px; margin-left: 0px;">
                                        <label for="ddlSexoAluno" class="lblObrigatorio" title="Sexo do Aluno">
                                            Sexo</label>
                                        <asp:DropDownList ID="ddlSexoAluno" CssClass="ddlSexoAluno" Width="50px" runat="server"
                                            ToolTip="Informe o Sexo do Aluno">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="M">MAS</asp:ListItem>
                                            <asp:ListItem Value="F">FEM</asp:ListItem>
                                            <asp:ListItem Value="S">S/R</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ValidationGroup="ALUNO"
                                            runat="server" ControlToValidate="ddlSexoAluno" ErrorMessage="Sexo deve ser informado"
                                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                    </li>
                                    <li style="margin-top: -2px">
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
                                    <li style="margin-top: -2px;">
                                        <label for="txtNaturalidadeAluno" title="Cidade de Naturalidade do Aluno">
                                            Naturalidade</label>
                                        <asp:TextBox ID="txtNaturalidadeAluno" CssClass="txtNaturalidadeAluno" runat="server"
                                            ToolTip="Informe a Cidade de Naturalidade do Aluno" MaxLength="40"></asp:TextBox>
                                    </li>
                                    <li style="margin-top: -2px;">
                                        <label for="ddlUfNacionalidadeAluno" title="UF de Nacionalidade do Aluno">
                                            UF</label>
                                        <asp:DropDownList ID="ddlUfNacionalidadeAluno" CssClass="campoUf" runat="server"
                                            ToolTip="Informe a UF de Nacionalidade">
                                        </asp:DropDownList>
                                    </li>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <li style="margin-left: 5px; margin-top: -2px;">
                                        <label title="Tipo de Sangue">
                                            Tp.Sanguíneo</label>
                                        <asp:DropDownList ID="ddlTpSangueAluno" ToolTip="Selecione o Tipo de Sangue" CssClass="ddlTpSangueAluno"
                                            runat="server">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="A">A</asp:ListItem>
                                            <asp:ListItem Value="B">B</asp:ListItem>
                                            <asp:ListItem Value="AB">AB</asp:ListItem>
                                            <asp:ListItem Value="O">O</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlStaSangueAluno" ToolTip="Selecione o Status do Tipo de Sangue"
                                            CssClass="ddlStaTpSangueAluno" runat="server">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="P">+</asp:ListItem>
                                            <asp:ListItem Value="N">-</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-left: 10px; margin-top: -2px;">
                                        <label for="ddlDeficienciaAluno" title="Deficiência">
                                            Deficiência</label>
                                        <asp:DropDownList ID="ddlDeficienciaAluno" CssClass="ddlDeficienciaAluno" runat="server"
                                            ToolTip="Selecione a Deficiência do Aluno">
                                            <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                                            <asp:ListItem Value="A">Auditiva</asp:ListItem>
                                            <asp:ListItem Value="V">Visual</asp:ListItem>
                                            <asp:ListItem Value="F">Física</asp:ListItem>
                                            <asp:ListItem Value="M">Intelectual</asp:ListItem>
                                            <asp:ListItem Value="P">Múltiplas</asp:ListItem>
                                            <asp:ListItem Value="O">Surdocegueira</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: -2px;">
                                        <label for="ddlTransporteEscolarAluno" title="Participa do Transporte Escolar?">
                                            Transporte/Rota</label>
                                        <asp:DropDownList ID="ddlTransporteEscolarAluno" CssClass="ddlTransporteEscolarAluno"
                                            runat="server" ToolTip="Informe se o Aluno participará do Transporte Escolar">
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                            <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlRotaA" CssClass="ddlTransporteEscolarAluno" runat="server"
                                            ToolTip="Informe se a Rota de Transporte Escolar do Aluno">
                                            <asp:ListItem Value="208">208</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: -2px;">
                                        <label for="ddlPasseEscolarAluno" title="Precisa de Passe Escolar?">
                                            Passe Esc</label>
                                        <asp:DropDownList ID="ddlPasseEscolarAluno" CssClass="ddlPasseEscolarAluno" runat="server"
                                            ToolTip="Informe se o Aluno Precisa de Passe Escolar">
                                            <asp:ListItem Value="true">Sim</asp:ListItem>
                                            <asp:ListItem Value="false" Selected="True">Não</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: -2px;">
                                        <label for="ddlPermiSaidaAluno" title="Permissão de saída do aluno" style="color: Red;">
                                            Perm Saída</label>
                                        <asp:DropDownList ID="ddlPermiSaidaAluno" CssClass="ddlPasseEscolarAluno" runat="server"
                                            ToolTip="Informe qual será a permissão de saída na Carteira de Estudante - Sim (Liberada) ou Não (Não Autorizada)">
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                            <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li class="liClear" style="margin-top: -2px;">
                                        <label for="txtTelResidencialAluno" title="Telefone Residencial">
                                            Telefone Fixo</label>
                                        <asp:TextBox ID="txtTelResidencialAluno" CssClass="campoTelefone" ToolTip="Informe o Telefone Residencial do Aluno"
                                            runat="server"></asp:TextBox>
                                    </li>
                                    <li style="margin-top: -4px;">
                                        <label for="txtEmailAluno" title="E-mail">
                                            E-mail</label>
                                        <asp:TextBox ID="txtEmailAluno" CssClass="txtEmailAluno" ToolTip="Informe o E-mail do Aluno" Width="170px"
                                            runat="server" MaxLength="50"></asp:TextBox>
                                    </li>
                                    <li style="margin-top: -4px;">
                                        <label for="txtTelCelularAluno" title="Telefone Celular">
                                            Telefone Celular</label>
                                        <asp:TextBox ID="txtTelCelularAluno" CssClass="campoTelefone" ToolTip="Informe o Telefone Celular do Aluno"
                                            runat="server" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtTelCelularAlunoNoveDigit" ToolTip="Informe o Telefone Celular do Aluno" CssClass="txtTelCelularNoveDigitos" Width="80px"
                                            runat="server" Visible="false"></asp:TextBox>
                                    </li>
                                    <li style="margin-top: -3px;">
                                        <label>RG</label>
                                        <asp:TextBox runat="server" ID="txtRGAluno" CssClass="txtIdentidadeResp" MaxLength="20"></asp:TextBox>
                                    </li>
                                    <li style="margin-top: -3px;">
                                        <label>Órgão Em.</label>
                                        <asp:TextBox runat="server" ID="txtOrgEmiRGAlu" Width="46px"></asp:TextBox>
                                    </li>
                                    <li style="margin-top: -3px;">
                                        <label for="txtCepAluno" class="lblObrigatorio" title="CEP">
                                            CEP</label>
                                        <asp:TextBox ID="txtCepAluno" CssClass="campoCepValid" ToolTip="Informe o CEP" Width="60px" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtCepAluno"
                                            ErrorMessage="CEP deve ser informado" ValidationGroup="ALUNO" Text="*" Display="None"
                                            CssClass="validatorField"></asp:RequiredFieldValidator>
                                    </li>
                                    <li class="liClear" style="margin-top: -4px">
                                        <label for="txtNomeMaeAluno" class="lblObrigatorio" title="Nome da Mãe">
                                            Nome da Mãe</label>
                                        <%--<asp:TextBox ID="txtNomeMaeAluno" Width="180px" CssClass="txtNomeMaeAluno" ToolTip="Informe o Nome da Mãe"
                                            runat="server" MaxLength="60"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtNomeMaeAlunoValid" Width="165px" MaxLength="60" runat="server" ToolTip="Informe o Nome da Mãe"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtNomeMaeAlunoValid"
                                            ErrorMessage="Nome da mãe deve ser informado" Text="*" ValidationGroup="ALUNO"
                                            Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                    </li>
                                    <li style="margin-top: -4px;">
                                        <label for="txtNomePaiAluno" title="Nome do Pai">
                                            Nome do Pai</label>
                                        <asp:TextBox ID="txtNomePaiAluno" Width="165px" CssClass="txtNomeMaeAluno" ToolTip="Informe o Nome do Pai"
                                            runat="server" MaxLength="60"></asp:TextBox>
                                    </li>
                                    <li id="li12" class="liPesqCEPResp" style="margin-top: 8px;" runat="server">
                                        <asp:ImageButton ID="btnPesqCEPA" runat="server" OnClick="btnPesqCEPA_Click" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                                            CssClass="btnPesqMat" CausesValidation="false" />
                                    </li>
                                    <li style="margin-top: -3px;">
                                        <label for="ddlCidadeAluno" class="lblObrigatorio" title="Cidade">
                                            Cidade</label>
                                        <asp:DropDownList ID="ddlCidadeAluno" ToolTip="Informe a Cidade" runat="server" Width="110px"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCidadeAluno_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlCidadeAluno"
                                            ErrorMessage="Cidade deve ser informada" ValidationGroup="ALUNO" Text="*" Display="None"
                                            CssClass="validatorField"></asp:RequiredFieldValidator>
                                    </li>
                                    <li style="margin-top: -3px;">
                                        <label for="ddlBairroAluno" class="lblObrigatorio" title="Bairro">
                                            Bairro</label>
                                        <asp:DropDownList ID="ddlBairroAluno" CssClass="ddlBairroAluno" ToolTip="Informe o Bairro"
                                            runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ValidationGroup="ALUNO"
                                            runat="server" ControlToValidate="ddlBairroAluno" ErrorMessage="Bairro deve ser informado"
                                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                    </li>
                                    <li class="liClear" style="margin-top: -8px;">
                                        <label for="txtLogradouroAluno" class="lblObrigatorio" title="Logradouro da Residência do Aluno">
                                            Logradouro</label>
                                        <asp:TextBox ID="txtLogradouroAluno" CssClass="txtLogradouroAluno" ToolTip="Informe o Logradouro da Residência do Aluno"
                                            runat="server" MaxLength="40" style="width: 168px;"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtLogradouroAluno"
                                            ErrorMessage="Logradouro deve ser informado" Text="*" ValidationGroup="ALUNO"
                                            Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                    </li>
                                    <li style="margin-top: -8px;">
                                        <label for="txtNumeroAluno" title="Número da Residência do Aluno">
                                            Nr</label>
                                        <asp:TextBox ID="txtNumeroAluno" CssClass="txtNumeroAlunoValid" ToolTip="Informe o Número da Residência do Aluno" Width="30px"
                                            runat="server" MaxLength="5"></asp:TextBox>
                                    </li>
                                    <li style="margin-top: -8px;">
                                        <label>N° Controle Certidão</label>
                                        <asp:TextBox runat="server" ID="txtCertNascAlu" Width="100px" MaxLength="40" style="width: 185px;"></asp:TextBox>
                                    </li>
                                    <li style="margin-top: -8px;">
                                        <label for="ddlUFAluno" class="lblObrigatorio" title="UF">
                                            UF</label>
                                        <asp:DropDownList ID="ddlUFAluno" runat="server" CssClass="campoUf" ToolTip="Informe a UF"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlUFAluno_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlUFAluno"
                                            ErrorMessage="UF deve ser informado" ValidationGroup="ALUNO" Text="*" Display="None"
                                            CssClass="validatorField"></asp:RequiredFieldValidator>
                                    </li>
                                    <%--<li style="margin-top: -8px;">
                                        <label>N° Registro MEC</label>
                                        <asp:TextBox runat="server" ID="n_registro_mec" MaxLength="15" style="width: 100px;"></asp:TextBox>
                                    </li>--%>
                                    <%--<li class="liClear" style="margin-top: -3px;">
                                        <label>N° Cartão SUS</label>
                                        <asp:TextBox runat="server" ID="n_cartao_sus" MaxLength="16" style="width: 100px;"></asp:TextBox>
                                    </li>--%>
                                    <%--<li style="margin-top: -3px;">
                                        <label>N° Plano Saúde</label>
                                        <asp:TextBox runat="server" ID="n_plano_saude" MaxLength="40" style="width: 200px;"></asp:TextBox>
                                    </li>--%>
                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <li id="li8" runat="server" title="Clique para Cadastrar Aluno" 
                                style="background-color: #F1FFEF; border: 1px solid #D2DFD1;
                                margin-top: 2px; padding: 2px 3px 1px 3px;">
                                <asp:LinkButton ID="lnkAtualizaAlu" runat="server" ValidationGroup="ALUNO" OnClick="lnkAtualizaAlu_Click">FINALIZAR ALUNO</asp:LinkButton>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
            <!-- FIM DA INFORMAÇÃO DE RESPONSÁVEL -->

            <div id="tabGradeMater" runat="server" clientidmode="Static" style="display: none;">
                <ul id="ul17" class="ulDados2" style="width: 374px !important; height: 410px;  float: left; border-right: 1px solid #fff">
                    <li style="width: 99%; text-align: center; text-transform: uppercase; background-color: #87CEEB;
                        margin-top: -7px; margin-bottom: 0px; height:18px;">
                        <asp:Label style="font-size: 1.1em; font-family: Tahoma; vertical-align:middle; margin-top:4px !important;" runat="server">
                            GRADE CURSO</asp:Label>
                    </li>
                    <li style="margin-top: 10px;">
                        <div id="divGrdCurso" class="divGrdCurso" runat="server" style="width: 368px;">
                            <asp:GridView ID="grdGrdCurso"  
                                            CssClass="grdBusca grdGrdCurso" 
                                            style="text-align: center !important;" 
                                            runat="server" 
                                            Width="100%" 
                                            AutoGenerateColumns="false">
                                <RowStyle CssClass="rowStyle" />
                                <HeaderStyle CssClass="th" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum registro encontrado.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle Width="5px" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckSelect" AutoPostBack="true" Checked="true" runat="server" />
                                            <asp:HiddenField runat="server" ID="hidCodMat" Value='<%# Eval("CO_MAT") %>' />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="ckSelAll" OnCheckedChanged="ckSelAll_CheckedChanged" AutoPostBack="true" Checked="true" runat="server" />
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NO_SIGLA_MATERIA" HeaderText="COD">
                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_MATERIA" HeaderText="MATÉRIA">
                                        <ItemStyle Width="203px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="QT_CARG_HORA_MAT" HeaderText="CH">
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField  HeaderText="PRÉ">
                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
                <ul id="ul18" class="ulDados2" style="width: 373px !important; height: 370px; float: right;">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; background-color: #E0EEEE;
                        margin-top: -7px; margin-bottom: 10px; height:18px">
                        <asp:Label style="font-size: 1.1em; font-family: Tahoma;" runat="server">
                            GRADE DE MATÉRIAS EXTRA-CURSO</asp:Label>
                           <asp:DropDownList ID="ddlCurExt" OnSelectedIndexChanged="ddlCurExt_SelectedIndexChanged" AutoPostBack="true" runat="server" style="width: 110px; vertical-align:middle; margin-top:1px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <div id="divCursoExt" class="divGrdCursoExt" runat="server" style="width: 374px;">
                            <asp:GridView ID="grdCursoExt" CssClass="grdBusca grdGrdCurso" style="text-align: center !important;" Width="100%" runat="server" AutoGenerateColumns="false">
                                <RowStyle CssClass="rowStyle" />
                                <HeaderStyle CssClass="th" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum registro encontrado.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckSelect" AutoPostBack="true" runat="server" />
                                            <asp:HiddenField runat="server" ID="hidCodMat" Value='<%# Eval("coMat") %>' />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="ckSelAll" AutoPostBack="true" runat="server" />
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="noCur" HeaderText="CURSO">
                                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nome" HeaderText="MATÉRIA">
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="qtCarga" HeaderText="CH">
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField  HeaderText="PRÉ">
                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
                <ul id="ul29" class="ulDados2">
                    <li style="background-color: #F1FFEF; border: 1px solid #D2DFD1; margin-left: 350px;
                                margin-top: -25px; padding: 2px 3px 1px 3px;">
                        <asp:LinkButton ID="lnkConfirModSerTur" OnClientClick="if($(this).enabled){geraPadrao(this);__doPostBack('lnkConfirModSerTur', '')}" OnClick="lnkConfirModSerTur_Click" ValidationGroup="ModSerTur"
                            runat="server" ToolTip="Clique para Confirmar Modalidade, Curso e Turma" Enabled="false">
                            <asp:Label runat="server" ID="Label20" Text="FINALIZAR"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>

            <div id="tabEndAdd" class="tabFormaPgto" runat="server" clientidmode="Static" style="display: none;">
                <ul id="ul10" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -7px;
                        background-color: #FDF5E6; margin-bottom: 5px;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            INFORMAÇÕES CADASTRAIS DO ALUNO - ENDEREÇOS ADICIONAIS</label>
                    </li>
                    <li class="liNomeAluETA">
                        <label for="txtNomeAluETA" title="Nome do Aluno">
                            NOME</label>
                        <asp:TextBox ID="txtNomeAluETA" CssClass="campoNomePessoa" runat="server" Enabled="false"
                            ToolTip="Nome do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="liNIREAluETA">
                        <label for="txtNIREAluETA" title="Nº NIRE do Aluno">
                            Nº NIRE</label>
                        <asp:TextBox ID="txtNIREAluETA" runat="server" CssClass="txtNireAluno" Enabled="false"
                            ToolTip="Nº NIRE do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="liNISAluETA">
                        <label for="txtNISAluETA" title="Nº NIS do Aluno">
                            Nº NIS</label>
                        <asp:TextBox ID="txtNISAluETA" runat="server" CssClass="txtNisAluno" Enabled="false"
                            ToolTip="Nº NIS do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="G2Clear" style="width: 100%; border-bottom: 1px solid #CCCCCC; margin-left: 15px;
                        padding-bottom: 5px; margin-top: 5px;">
                        <label style="font-family: Tahoma;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Para incluir um novo endereço adicional do aluno informe
                            os dados de cada linha acima da grid de dados correspondente, após clique no botão
                            de INCLUIR.<br />
                            Para excluir, marque a coluna CHECK na grid de dados da informação que deseja eliminar
                            e click no botão excluir para efetivar a exclusão desejada.</label>
                    </li>
                    <li class="liDescTelAdd" style="width: 100%; text-align: center; margin-top: 2px;
                        margin-bottom: 20px;">
                        <label>
                            Os campos com <span class="asteriscoObrigatorio">*</span>&nbsp; são obrigatórios
                            e devem ser informados.
                        </label>
                    </li>
                    <li class="liddlTpTelef">
                        <label for="ddlTpEnderETA" class="lblObrigatorio" title="Tipo de Endereço">
                            Tipo</label>
                        <asp:DropDownList ID="ddlTpEnderETA" ToolTip="Selecione o Tipo de Endereço" CssClass="ddlTpTelef"
                            runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="ddlTpEnderETA"
                            ErrorMessage="Tipo de Endereço deve ser informado" ValidationGroup="incEndAlu"
                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
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
                            <li id="li3" class="liPesqCEPResp" runat="server" style="margin-top: 14px;">
                                <asp:ImageButton ID="btnCEPETA" runat="server" OnClick="btnCEPETA_Click" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                                    CssClass="btnPesqMat" CausesValidation="false" />
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
                                <label for="txtNumETA" title="Número do Endereço">
                                    Número</label>
                                <asp:TextBox ID="txtNumETA" CssClass="txtNumETA" runat="server" ToolTip="Digite o Número do Endereço">
                                </asp:TextBox>
                            </li>
                            <li class="liNomeContETA">
                                <label for="txtCompETA" title="Complemento do Endereço">
                                    Complemento</label>
                                <asp:TextBox ID="txtCompETA" CssClass="txtCompETA" runat="server" ToolTip="Digite o Complemento do Endereço">
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
                                    ErrorMessage="Cidade deve ser informada" ValidationGroup="incEndAlu" Text="*"
                                    Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
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
                        <asp:LinkButton ID="lnkIncEnd" OnClick="lnkIncEnd_Click" ValidationGroup="incEndAlu"
                            runat="server" Style="margin: 0 auto;" ToolTip="Incluir Registro">
                            <img class="imgLnkInc" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa"
                                title="Incluir Registro" />
                            <asp:Label runat="server" ID="Label9" Text="Incluir"></asp:Label>
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkExcEnd" OnClick="lnkExcEnd_Click" ValidationGroup="excEndAlu"
                            runat="server" Style="margin: 0 auto;" ToolTip="Excluir Registro">
                            <img class="imgLnkExc" src='/Library/IMG/Gestor_BtnDel.png' alt="Icone Pesquisa"
                                title="Excluir Registro" />
                            <asp:Label runat="server" ID="Label10" Text="Excluir"></asp:Label>
                        </asp:LinkButton>
                    </li>
                    <li class="liGridTelETA">
                        <div id="div6" runat="server" class="divGridTelETA" style="height: 141px;">
                            <asp:GridView ID="grdEndETA" CssClass="grdBusca" Width="677px" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="ID_ALUNO_ENDERECO">
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
                                    <asp:BoundField DataField="cep" HeaderText="Nº CEP">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DS_ENDERECO" HeaderText="LOGRADOURO">
                                        <ItemStyle Width="220px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NR_ENDERECO" HeaderText="Nº">
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
                        <asp:LinkButton ID="lnkAtualizaEndAlu" runat="server" ValidationGroup="atuEndAlu"
                            OnClick="lnkAtualizaEndAlu_Click">FINALIZAR</asp:LinkButton>
                    </li>
                </ul>
            </div>

            <div id="tabTelAdd" class="tabFormaPgto" runat="server" clientidmode="Static" style="display: none;">
                <ul id="ul6" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -7px;
                        background-color: #FDF5E6; margin-bottom: 5px;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            INFORMAÇÕES CADASTRAIS DO ALUNO - TELEFONES ADICIONAIS</label>
                    </li>
                    <li class="liNomeAluETA">
                        <label for="txtNomeAluETA" title="Nome do Aluno">
                            NOME</label>
                        <asp:TextBox ID="txtNomeAluTA" CssClass="campoNomePessoa" runat="server" Enabled="false"
                            ToolTip="Nome do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="liNIREAluETA">
                        <label for="txtNIREAluETA" title="Nº NIRE do Aluno">
                            Nº NIRE</label>
                        <asp:TextBox ID="txtNIREAluTA" runat="server" CssClass="txtNireAluno" Enabled="false"
                            ToolTip="Nº NIRE do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="liNISAluETA">
                        <label for="txtNISAluETA" title="Nº NIS do Aluno">
                            Nº NIS</label>
                        <asp:TextBox ID="txtNISAluTA" runat="server" CssClass="txtNisAluno" Enabled="false"
                            ToolTip="Nº NIS do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="G2Clear" style="width: 100%; border-bottom: 1px solid #CCCCCC; margin-left: 15px;
                        padding-bottom: 5px; margin-top: 5px;">
                        <label style="font-family: Tahoma;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Para incluir um novo telefone adicional do aluno informe
                            os dados de cada linha acima da grid de dados correspondente, após clique no botão
                            de INCLUIR.
                            <br />
                            Para excluir, marque a coluna CHECK na grid de dados da informação que deseja eliminar
                            e click no botão excluir para efetivar a exclusão desejada.</label>
                    </li>
                    <li class="liDescTelAdd" style="width: 100%; text-align: center; margin-top: 2px;
                        margin-bottom: 20px;">
                        <label>
                            Os campos com <span class="asteriscoObrigatorio">*</span>&nbsp; são obrigatórios
                            e devem ser informados.
                        </label>
                    </li>
                    <li class="liddlTpTelef">
                        <label for="ddlTpTelef" class="lblObrigatorio" title="Tipo de Telefone">
                            Tipo</label>
                        <asp:DropDownList ID="ddlTpTelef" ToolTip="Selecione o Tipo de Telefone" CssClass="ddlTpTelef"
                            runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="ddlTpTelef"
                            ErrorMessage="Tipo de telefone deve ser informado" ValidationGroup="incTelAlu"
                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li class="litxtTelETA">
                        <label for="txtTelETA" class="lblObrigatorio" title="Nº Telefone do Aluno">
                            Nº Telefone</label>
                        <asp:TextBox ID="txtTelETA" runat="server" CssClass="campoTelefone" ToolTip="Digite o Nº Telefone do Aluno">
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
                        <label for="txtObsETA" title="Observação">
                            Observação</label>
                        <asp:TextBox ID="txtObsETA" Style="width: 255px;" runat="server" ToolTip="Digite a Observação">
                        </asp:TextBox>
                    </li>
                    <li id="liBtnsETA" runat="server" class="liBtnsETA">
                        <asp:LinkButton ID="lnkIncTel" OnClick="lnkIncTel_Click" ValidationGroup="incTelAlu"
                            runat="server" Style="margin: 0 auto;" ToolTip="Incluir Registro">
                            <img class="imgLnkInc" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa"
                                title="Incluir Registro" />
                            <asp:Label runat="server" ID="Label1" Text="Incluir"></asp:Label>
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkExcTel" OnClick="lnkExcTel_Click" ValidationGroup="excTelAlu"
                            runat="server" Style="margin: 0 auto;" ToolTip="Excluir Registro">
                            <img class="imgLnkExc" src='/Library/IMG/Gestor_BtnDel.png' alt="Icone Pesquisa"
                                title="Excluir Registro" />
                            <asp:Label runat="server" ID="Label8" Text="Excluir"></asp:Label>
                        </asp:LinkButton>
                    </li>
                    <li class="liGridTelETA">
                        <div id="div5" runat="server" class="divGridTelETA">
                            <asp:GridView ID="grdTelETA" CssClass="grdBusca" Width="677px" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="ID_ALUNO_TELEFONE">
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
                                    <asp:BoundField DataField="telefone" HeaderText="Nº TELEFONE">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_CONTATO" HeaderText="NOME DO CONTATO">
                                        <ItemStyle Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DES_OBSERVACAO" HeaderText="OBSERVAÇÃO">
                                        <ItemStyle Width="250px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                    <li id="li22" runat="server" class="liBtnAddA" style="margin-left: 355px !important;">
                        <asp:LinkButton ID="lnkAtualizaTelAlu" runat="server" ValidationGroup="atuEndAlu"
                            OnClick="lnkAtualizaTelAlu_Click">FINALIZAR</asp:LinkButton>
                    </li>
                </ul>
            </div>

            <div id="tabCuiEspAdd" runat="server" clientidmode="Static" style="display: none;">
                <ul id="ul4" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -7px;
                        background-color: #FDF5E6; margin-bottom: 5px;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            INFORMAÇÕES DO ALUNO - CUIDADOS ESPECIAIS</label>
                    </li>
                    <li class="liNomeAluETA">
                        <label for="txtNomeAluCEA" title="Nome do Aluno">
                            NOME</label>
                        <asp:TextBox ID="txtNomeAluCEA" CssClass="campoNomePessoa" runat="server" Enabled="false"
                            ToolTip="Nome do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="liNIREAluETA">
                        <label for="txtNIREAluCEA" title="Nº NIRE do Aluno">
                            Nº NIRE</label>
                        <asp:TextBox ID="txtNIREAluCEA" runat="server" CssClass="txtNireAluno" Enabled="false"
                            ToolTip="Nº NIRE do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="liNISAluETA">
                        <label for="txtNISAluCEA" title="Nº NIS do Aluno">
                            Nº NIS</label>
                        <asp:TextBox ID="txtNISAluCEA" runat="server" CssClass="txtNisAluno" Enabled="false"
                            ToolTip="Nº NIS do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="G2Clear" style="width: 100%; border-bottom: 1px solid #CCCCCC; margin-left: 15px;
                        padding-bottom: 5px; margin-top: 5px;">
                        <label style="font-family: Tahoma; margin-left: 15px;">
                            Para incluir um novo Cuidado Especial para com o Aluno informe os dados de cada
                            linha acima da Grid de Dados correspondente, após clique em INCLUIR.<br />
                            &nbsp;&nbsp; Para excluir, marque a coluna CHECK na Grid de Dados da informação
                            que deseja eliminar e click no botão EXCLUIR para efetivar a exclusão desejada.</label>
                    </li>
                    <li class="liDescTelAdd" style="width: 100%; text-align: center; margin-top: 2px;
                        margin-bottom: 20px;">
                        <label>
                            Os campos com <span class="asteriscoObrigatorio">*</span>&nbsp; são obrigatórios
                            e devem ser informados.
                        </label>
                    </li>
                    <li class="liddlTpCui">
                        <label for="ddlTpCui" class="lblObrigatorio" title="Tipo de Cuidado">
                            Tipo Cuidado</label>
                        <asp:DropDownList ID="ddlTpCui" ToolTip="Selecione o Tipo de Cuidado" CssClass="ddlTpCui"
                            runat="server">
                            <asp:ListItem Value="M">Medicação</asp:ListItem>
                            <asp:ListItem Value="A">Acompanhamento</asp:ListItem>
                            <asp:ListItem Value="C">Curativo</asp:ListItem>
                            <asp:ListItem Value="O">Outras</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="ddlTpCui"
                            ErrorMessage="Tipo de Cuidado deve ser informado" ValidationGroup="incCuiEspAlu"
                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="ddlTpTelef" class="lblObrigatorio" title="Tipo de Aplicação">
                            Tipo Aplicação</label>
                        <asp:DropDownList ID="ddlTpApli" ToolTip="Selecione o Tipo de Aplicação" Style="width: 75px;"
                            runat="server">
                            <asp:ListItem Value="O">Via Oral</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="ddlTpApli"
                            ErrorMessage="Tipo de Aplicação deve ser informado" ValidationGroup="incCuiEspAlu"
                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="txtHrAplic" title="Hora da Aplicação">
                            Hora</label>
                        <asp:TextBox ID="txtHrAplic" runat="server" CssClass="txtHrAplic" ToolTip="Digite a hora da aplicação">
                        </asp:TextBox>
                    </li>
                    <li>
                        <label for="txtNomeContETA" class="lblObrigatorio" title="Descrição do Cuidado Especial">
                            Descrição</label>
                        <asp:TextBox ID="txtDescCEA" MaxLength="50" CssClass="txtDescCEA" runat="server"
                            ToolTip="Digite a Descrição do Cuidado Especial">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txtDescCEA"
                            ErrorMessage="Descrição do Cuidado Especial deve ser informada" ValidationGroup="incCuiEspAlu"
                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
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
                        <label for="txtObsCEA" title="Observação">
                            Observação</label>
                        <asp:TextBox ID="txtObsCEA" Style="width: 210px;" MaxLength="200" runat="server"
                            ToolTip="Digite a Observação">
                        </asp:TextBox>
                    </li>
                    <li id="liPeriodo" class="liPeriodo">
                        <label for="txtPeriodo">
                            Período</label>
                        <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" runat="server"></asp:TextBox>
                        <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
                        <asp:TextBox ID="txtDataPeriodoFim" CssClass="campoData" runat="server"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="validatorField"
                            ForeColor="Red" ValidationGroup="incCuiEspAlu" ControlToValidate="txtDataPeriodoFim"
                            ControlToCompare="txtDataPeriodoIni" Type="Date" Operator="GreaterThanEqual"
                            ErrorMessage="Data Final não pode ser menor que Data Inicial.">
                        </asp:CompareValidator>
                    </li>
                    <li style="margin-left: 5px;">
                        <label for="txtNomeMedCEA" title="Nome do Médico">
                            Nome do Médico</label>
                        <asp:TextBox ID="txtNomeMedCEA" Style="width: 150px;" MaxLength="60" runat="server"
                            ToolTip="Digite o Nome do Médico">
                        </asp:TextBox>
                    </li>
                    <li>
                        <label for="txtNumCRMCEA" title="Número CRM / UF">
                            Nº CRM / UF</label>
                        <asp:TextBox ID="txtNumCRMCEA" MaxLength="12" CssClass="txtNumCRMCEA" runat="server"
                            ToolTip="Digite o Nº CRM">
                        </asp:TextBox>
                        <asp:DropDownList ID="ddlUFCEA" CssClass="campoUf" runat="server" ToolTip="Informe a UF">
                        </asp:DropDownList>
                    </li>
                    <li class="txtTelCEA">
                        <label for="txtTelETA" title="Nº Telefone">
                            Nº Telefone</label>
                        <asp:TextBox ID="txtTelCEA" runat="server" CssClass="campoTelefone" ToolTip="Digite o Nº Telefone">
                        </asp:TextBox>
                    </li>
                    <li>
                        <label for="ddlRecCEA" title="Possui Receita?">
                            Receita?</label>
                        <asp:DropDownList ID="ddlRecCEA" CssClass="ddlPasseEscolarAluno" runat="server" ToolTip="Informe se o Aluno possui Receita">
                            <asp:ListItem Value="S">Sim</asp:ListItem>
                            <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li id="liBtnsCEA" runat="server" class="liBtnsCEA">
                        <asp:LinkButton ID="lnkIncCEA" OnClick="lnkIncCEA_Click" ValidationGroup="incCuiEspAlu"
                            runat="server" Style="margin: 0 auto;" ToolTip="Incluir Registro">
                            <img class="imgLnkInc" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa"
                                title="Incluir Registro" />
                            <asp:Label runat="server" ID="Label11" Text="Incluir"></asp:Label>
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkExcCEA" OnClick="lnkExcCEA_Click" runat="server" ValidationGroup="excCuiEspAlu"
                            Style="margin: 0 auto; margin-left: 2px;" ToolTip="Excluir Registro">
                            <img class="imgLnkExc" style="margin-left: 3px; margin-top: -5px;" src='/Library/IMG/Gestor_BtnDel.png'
                                alt="Icone Pesquisa" title="Excluir Registro" />
                            <asp:Label runat="server" ID="Label12" Style="margin-left: 3px;" Text="Excluir"></asp:Label>
                        </asp:LinkButton>
                    </li>
                    <li class="liGridCuiEsp">
                        <div id="div8" runat="server" class="divGridCEA" style="height: 130px;">
                            <asp:GridView ID="grdCuiEsp" CssClass="grdBusca" runat="server" Width="680" AutoGenerateColumns="False"
                                DataKeyNames="ID_MEDICACAO">
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
                                    <asp:BoundField DataField="TP_APLICAC_CUIDADO" HeaderText="Aplicação">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HR_APLICAC_CUIDADO" HeaderText="Hora">
                                        <ItemStyle Width="30px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NM_REMEDIO_CUIDADO" HeaderText="DESCRIÇÃO">
                                        <ItemStyle Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DE_DOSE_REMEDIO_CUIDADO" ItemStyle-HorizontalAlign="Right"
                                        HeaderText="Qtde">
                                        <ItemStyle Width="30px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UNIDADE" HeaderText="UNID">
                                        <ItemStyle Width="30px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DE_OBSERV_CUIDADO" HeaderText="OBSERVAÇÃO">
                                        <ItemStyle Width="250px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DT_RECEITA_INI" DataFormatString="{0:dd/MM/yyyy}" HeaderText="PERÍODO">
                                        <ItemStyle Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DT_RECEITA_FIM" DataFormatString="{0:dd/MM/yyyy}" HeaderText="RECEITA">
                                        <ItemStyle Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NM_MEDICO_CUIDADO" HeaderText="Médico">
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
                    <li id="li23" runat="server" class="liBtnAddA" style="margin-left: 355px !important;
                        margin-top: 6px;">
                        <asp:LinkButton ID="lnkCuiEspAlu" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkCuiEspAlu_Click">FINALIZAR</asp:LinkButton>
                    </li>
                </ul>
            </div>
            <div id="tabResAliAdd" class="tabFormaPgto" runat="server" clientidmode="Static" style="display: none;">
                <ul id="ul7" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -7px;
                        background-color: #FDF5E6; margin-bottom: 5px;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            INFORMAÇÕES CADASTRAIS DO ALUNO - RESTRIÇÃO ALIMENTAR</label>
                    </li>
                    <li class="liNomeAluETA">
                        <label for="txtNomeAluRAA" title="Nome do Aluno">
                            NOME</label>
                        <asp:TextBox ID="txtNomeAluRAA" CssClass="campoNomePessoa" runat="server" Enabled="false"
                            ToolTip="Nome do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="liNIREAluETA">
                        <label for="txtNIREAluRAA" title="Nº NIRE do Aluno">
                            Nº NIRE</label>
                        <asp:TextBox ID="txtNIREAluRAA" runat="server" CssClass="txtNireAluno" Enabled="false"
                            ToolTip="Nº NIRE do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="liNISAluETA">
                        <label for="txtNISAluRAA" title="Nº NIS do Aluno">
                            Nº NIS</label>
                        <asp:TextBox ID="txtNISAluRAA" runat="server" CssClass="txtNisAluno" Enabled="false"
                            ToolTip="Nº NIS do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="G2Clear" style="width: 100%; border-bottom: 1px solid #CCCCCC; margin-left: 15px;
                        padding-bottom: 5px; margin-top: 5px;">
                        <label style="font-family: Tahoma;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Para incluir uma nova Restrição Alimentar do aluno
                            informe os dados de cada linha acima da grid de Dados correspondente, após clique
                            em INCLUIR.
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Para excluir, marque
                            a coluna CHECK na Grid de Dados da informação que deseja eliminar e click no botão
                            excluir para efetivar a exclusão desejada.</label>
                    </li>
                    <li class="liDescTelAdd" style="width: 100%; text-align: center; margin-top: 2px;
                        margin-bottom: 20px;">
                        <label>
                            Os campos com <span class="asteriscoObrigatorio">*</span>&nbsp; são obrigatórios
                            e devem ser informados.
                        </label>
                    </li>
                    <li style="margin-left: 30px;">
                        <label for="ddlTpEnderETA" class="lblObrigatorio" title="Tipo de Restrição">
                            Tipo Restrição</label>
                        <asp:DropDownList ID="ddlTpRestri" ToolTip="Selecione o Tipo de Restrição" CssClass="ddlTpRestri"
                            runat="server">
                            <asp:ListItem Value="A">Alimentar</asp:ListItem>
                            <asp:ListItem Value="L">Alergia</asp:ListItem>
                            <asp:ListItem Value="M">Médica</asp:ListItem>
                            <asp:ListItem Value="O">Outros</asp:ListItem>
                            <asp:ListItem Value="R">Responsável</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="ddlTpEnderETA"
                            ErrorMessage="Tipo de Endereço deve ser informado" ValidationGroup="incResAli"
                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="txtDescRestri" class="lblObrigatorio" title="Qual a restrição?">
                            Qual a restrição?</label>
                        <asp:TextBox ID="txtDescRestri" CssClass="txtDescRestri" MaxLength="40" runat="server"
                            ToolTip="Digite a Restrição Alimentar">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator35" ValidationGroup="incResAli"
                            runat="server" ControlToValidate="txtDescRestri" ErrorMessage="Restrição deve ser informada"
                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="txtCodRestri" title="Código da Restrição">
                            Código</label>
                        <asp:TextBox ID="txtCodRestri" MaxLength="12" CssClass="txtCodRestri" ToolTip="Informe o Código da Restrição"
                            runat="server"></asp:TextBox>
                    </li>
                    <li style="margin-top: 13px; margin-left: -3px"><a class="lnkPesqCID" href="#">
                        <img class="btnPesqMat" src="/Library/IMG/Gestor_BtnPesquisa.png" title="Pesquisada de doenças."
                            alt="Icone Trocar Unidade" /></a> </li>
                    <li>
                        <label for="txtAcaoRestri" class="lblObrigatorio" title="Ação a ser aplicada em caso de uso?">
                            Ação a ser aplicada em caso de uso?</label>
                        <asp:TextBox ID="txtAcaoRestri" CssClass="txtAcaoRestri" MaxLength="200" runat="server"
                            ToolTip="Digite a Ação a ser aplicada em caso de uso">
                        </asp:TextBox>
                    </li>
                    <li id="li21">
                        <label for="ddlGrauRestri" class="lblObrigatorio" title="UF">
                            Grau Restrição</label>
                        <asp:DropDownList ID="ddlGrauRestri" runat="server" CssClass="ddlGrauRestri" ToolTip="Informe o Grau de Restrição">
                            <asp:ListItem Value="A">Alto Risco</asp:ListItem>
                            <asp:ListItem Value="M">Médio Risco</asp:ListItem>
                            <asp:ListItem Value="B">Baixo Risco</asp:ListItem>
                            <asp:ListItem Value="N">Nenhum</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li class="liPeriodo" style="clear: none; margin-right: 0px; margin-left: 0px;">
                        <label class="lblObrigatorio" for="txtPeriodo">
                            Período de Restrição</label>
                        <asp:TextBox ID="txtDtIniRestri" CssClass="campoData" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator39" ValidationGroup="incResAli"
                            runat="server" ControlToValidate="txtDtIniRestri" ErrorMessage="Data Inicial deve ser informada"
                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                        <asp:Label ID="Label15" CssClass="lblDivData" runat="server"> à </asp:Label>
                        <asp:TextBox ID="txtDtFimRestri" CssClass="campoData" runat="server"></asp:TextBox>
                        <%--<span style="">Tipo Valor</span>--%>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="validatorField"
                            ForeColor="Red" ValidationGroup="incCuiEspAlu" ControlToValidate="txtDtFimRestri"
                            ControlToCompare="txtDtIniRestri" Type="Date" Operator="GreaterThanEqual" ErrorMessage="Data Final não pode ser menor que Data Inicial.">
                        </asp:CompareValidator>
                    </li>
                    <li class="liBtnsResAli">
                        <asp:LinkButton ID="lnkIncRestAli" OnClick="lnkIncRestAli_Click" ValidationGroup="incResAli"
                            runat="server" Style="margin: 0 auto;" ToolTip="Incluir Registro">
                            <img class="imgLnkInc" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa"
                                title="Incluir Registro" />
                            <asp:Label runat="server" ID="Label13" Text="Incluir"></asp:Label>
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkExcRestAli" OnClick="lnkExcRestAli_Click" ValidationGroup="excResAli"
                            runat="server" Style="margin: 0 auto;" ToolTip="Excluir Registro">
                            <img class="imgLnkExc" src='/Library/IMG/Gestor_BtnDel.png' alt="Icone Pesquisa"
                                title="Excluir Registro" />
                            <asp:Label runat="server" ID="Label14" Text="Excluir"></asp:Label>
                        </asp:LinkButton>
                    </li>
                    <li style="margin-left: 30px;">
                        <div id="div9" runat="server" class="divGridTelETA" style="height: 154px; width: 710px;">
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
                                    <asp:BoundField DataField="TP_RESTR_ALIMEN" HeaderText="TP RESTRIÇÃO">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NM_RESTR_ALIMEN" HeaderText="QUAL A RESTRICÃO">
                                        <ItemStyle Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ID_REFER_GEDUC_RESTR_ALIMEN" HeaderText="COD">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DE_ACAO_RESTR_ALIMEN" HeaderText="AÇÃO A SER APLICADA">
                                        <ItemStyle Width="230px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CO_GRAU_RESTR_ALIMEN" HeaderText="GRAU REST">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DT_INICIO_RESTR_ALIMEN" DataFormatString="{0:dd/MM/yyyy}"
                                        HeaderText="PERÍODO">
                                        <ItemStyle Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DT_TERMI_RESTR_ALIMEN" DataFormatString="{0:dd/MM/yyyy}"
                                        HeaderText="RESTRIÇÃO">
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
              <div id="tabUniMat" style="display: none;" clientidmode="Static" runat="server">
                <ul id="ul12" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -7px;
                        background-color: #FDF5E6; margin-bottom: 5px;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            INFORMAÇÕES DO ALUNO - MATERIAL COLETIVO / UNIFORME</label>
                    </li>
                    <li class="liNomeAluETA">
                        <label for="txtNomeAluDoc" title="Nome do Aluno">
                            NOME</label>
                        <asp:TextBox ID="TextBox1" CssClass="campoNomePessoa" runat="server" Enabled="false"
                            ToolTip="Nome do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="liNIREAluETA">
                        <label for="txtNIREAluDoc" title="Nº NIRE do Aluno">
                            Nº NIRE</label>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="txtNireAluno" Enabled="false"
                            ToolTip="Nº NIRE do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="liNISAluETA">
                        <label for="txtNISAluDoc" title="Nº NIS do Aluno">
                            Nº NIS</label>
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="txtNisAluno" Enabled="false"
                            ToolTip="Nº NIS do Aluno">
                        </asp:TextBox>
                    </li>
                    <li style="clear: both;">
                        <ul>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <li class="liGridMat">
                                        <div id="div3" title="Selecione os Itens Solicitados">
                                            <asp:GridView ID="grdSolicitacoes" CssClass="grdBusca" runat="server" Style="width: 100%;"
                                                AutoGenerateColumns="false">
                                                <RowStyle CssClass="rowStyle" />
                                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" OnCheckedChanged="ck2Select_CheckedChanged" Enabled='<%# bind("Inclu") %>'
                                                                Checked='<%# bind("Checked") %>' runat="server" AutoPostBack="true" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Descrição" DataField="Descricao" />
                                                    <asp:TemplateField HeaderText="R$ Unit">
                                                        <ItemStyle Width="30px" HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblValor" runat="server" Text='<%# bind("Valor") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unidade">
                                                        <ItemStyle Width="54px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnidade" runat="server" Text='<%# bind("DescUnidade") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qtd">
                                                        <ItemStyle Width="24px" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQtdeSolic" CssClass="txtQtdeSolic" Text='<%# bind("Qtde") %>'
                                                                AutoPostBack="true" OnTextChanged="txtQtdeSolic_TextChanged" Enabled="false"
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="R$ Item">
                                                        <ItemStyle Width="30px" HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotal" Enabled="false" Text='<%# bind("Total") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="txMatric" Visible="true">
                                                        <ItemStyle Width="30px" HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="txMatr" Enabled="false" Text='<%# bind("txMatric") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </li>
                                    <li style="clear: both; margin-left: 150px;">
                                        <label for="txtDtPrevisao" title="Previsão de Entrega">
                                            Previsão Entrega</label>
                                        <asp:TextBox ToolTip="Informe a Previsão de Entrega" ID="txtDtPrevisao" CssClass="txtDtVectoSolic campoData"
                                            runat="server"></asp:TextBox>
                                    </li>
                                    <li style="margin-top: 5px; margin-left: 210px; clear: none;">
                                        <label for="txtValorTotal" style="float: left; margin-right: 5px;" title="Valor Total da Solicitação (R$)">
                                            Total da Solicitação R$</label>
                                        <asp:TextBox ID="txtValorTotal" Enabled="false" runat="server" CssClass="txtValorTotal"></asp:TextBox>
                                    </li>
                                    <li class="liClear" style="margin-top: 10px; margin-left: 145px;">
                                        <asp:CheckBox ID="ckbAtualizaFinancSolic" CssClass="chkIsento" Enabled="true" runat="server"
                                            Text="Atualiza Financeiro" ToolTip="Selecione se atualizará o financeiro" AutoPostBack="True"
                                            OnCheckedChanged="ckbAtualizaFinancSolic_CheckedChanged" />
                                    </li>
                                    <li style="margin-top: 0px; clear: none; margin-left: 45px;">
                                        <label for="ddlBoleto" title="Boleto Bancário">
                                            Boleto Bancário</label>
                                        <asp:DropDownList ID="ddlBoletoSolic" runat="server" Enabled="false" Width="210px"
                                            ToolTip="Selecione o Boleto Bancário">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="clear: none;">
                                        <label for="txtDtVectoSolic" title="Data de Vencimento">
                                            Dt Vecto</label>
                                        <asp:TextBox ToolTip="Informe a Data de Vencimento da Solicitação" Enabled="false"
                                            ID="txtDtVectoSolic" CssClass="txtDtVectoSolic campoData" runat="server"></asp:TextBox>
                                    </li>
                                    <li style="margin-bottom: 4px; clear: both; margin-left: 145px; margin-top: -5px;">
                                        <asp:CheckBox ID="chkConsolValorTitul" CssClass="chkIsento" Enabled="false" runat="server"
                                            Text="Consolida Valores Título Único" ToolTip="Selecione se consolidará os valores em um único título financeiro"
                                            AutoPostBack="True" OnCheckedChanged="chkConsolValorTitul_CheckedChanged" />
                                    </li>
                                    <li style="margin-left: 170px; margin-bottom: 0px; clear: both;">
                                        <label for="ddlHistorico" title="Histórico">
                                            Histórico Financeiro</label>
                                        <asp:DropDownList ID="ddlHistorico" Enabled="false" Width="240px" runat="server"
                                            ToolTip="Selecione o Histórico Financeiro">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="clear: none; margin-left: 5px;">
                                        <label for="ddlAgrupador" title="Agrupador de Receita">
                                            Agrupador de Receita</label>
                                        <asp:DropDownList ID="ddlAgrupador" Enabled="false" CssClass="ddlAgrupador" Width="180px"
                                            runat="server" ToolTip="Selecione o Agrupador de Receita" />
                                    </li>
                                    <li style="clear: both; width: 460px; margin-bottom: 2px; margin-left: 170px; margin-top: 5px;">
                                        <label style="font-size: 1.1em;" title="Classificação Contábil">
                                            Classificação Contábil
                                        </label>
                                    </li>
                                    <li style="clear: both; margin-left: 170px;">
                                        <ul>
                                            <li class="liNomeContaContabil" style="clear: both;">
                                                <label style="font-size: 1.2em; margin-top: 10px; width: 55px;" title="Conta Contábil Ativo">
                                                    Cta Ativo</label>
                                            </li>
                                            <li class="liNomeContaContabil">
                                                <label id="Label19" for="ddlTipoContaA" title="Tipo de Conta Contábil" runat="server">
                                                    Tp</label>
                                                <asp:DropDownList ID="ddlTipoContaA" Enabled="false" CssClass="ddlTipoConta" Width="30px"
                                                    runat="server" ToolTip="Selecione o Tipo de Conta Contábil" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlTipoContaA_SelectedIndexChanged">
                                                    <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                                                    <asp:ListItem Value="C">3 - Receita</asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                            <li class="liNomeContaContabil">
                                                <label for="ddlGrupoContaA" title="Grupo de Conta Contábil">
                                                    Grp</label>
                                                <asp:DropDownList ID="ddlGrupoContaA" Enabled="false" CssClass="ddlGrupoConta" Width="35px"
                                                    runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGrupoContaA_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </li>
                                            <li class="liNomeContaContabil">
                                                <label for="ddlSubGrupoContaA" title="SubGrupo de Conta Contábil">
                                                    SGrp</label>
                                                <asp:DropDownList ID="ddlSubGrupoContaA" Enabled="false" CssClass="ddlSubGrupoConta"
                                                    Width="40px" runat="server" ToolTip="Selecione o SubGrupo de Conta Contábil"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlSubGrupoContaA_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </li>
                                            <li class="liNomeContaContabil">
                                                <label for="ddlContaContabilA" title="Conta Contábil">
                                                    Conta Contábil</label>
                                                <asp:DropDownList ID="ddlContaContabilA" Enabled="false" CssClass="ddlContaContabil"
                                                    runat="server" ToolTip="Selecione a Conta Contábil" Width="200px">
                                                </asp:DropDownList>
                                            </li>
                                        </ul>
                                    </li>
                                    <li style="clear: both; margin-left: 170px; margin-top: 4px;">
                                        <ul style="margin-top: -3px;">
                                            <li class="liNomeContaContabil" style="clear: both;">
                                                <label style="font-size: 1.2em; margin-top: 1px; width: 55px;" title="Conta Contábil Ativo">
                                                    Cta Banco</label>
                                            </li>
                                            <li class="liNomeContaContabil">
                                                <asp:DropDownList ID="ddlTipoContaB" Enabled="false" CssClass="ddlTipoConta" Width="30px"
                                                    runat="server" ToolTip="Selecione o Tipo de Conta Contábil" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlTipoContaB_SelectedIndexChanged">
                                                    <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                                                    <asp:ListItem Value="C">3 - Receita</asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                            <li class="liNomeContaContabil">
                                                <asp:DropDownList ID="ddlGrupoContaB" Enabled="false" CssClass="ddlGrupoConta" Width="35px"
                                                    runat="server" ToolTip="Selecione o Grupo de Conta Contábil" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlGrupoContaB_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </li>
                                            <li class="liNomeContaContabil">
                                                <asp:DropDownList ID="ddlSubGrupoContaB" Enabled="false" CssClass="ddlSubGrupoConta"
                                                    Width="40px" runat="server" ToolTip="Selecione o SubGrupo de Conta Contábil"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlSubGrupoContaB_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </li>
                                            <li class="liNomeContaContabil">
                                                <asp:DropDownList ID="ddlContaContabilB" Enabled="false" CssClass="ddlContaContabil"
                                                    runat="server" ToolTip="Selecione a Conta Contábil" Width="200px">
                                                </asp:DropDownList>
                                            </li>
                                        </ul>
                                    </li>
                                    <li style="clear: both; margin-left: 170px; margin-top: 4px;">
                                        <ul style="margin-top: -3px;">
                                            <li class="liNomeContaContabil" style="clear: both;">
                                                <label style="font-size: 1.2em; margin-top: 1px; width: 55px;" title="Conta Contábil Ativo">
                                                    Cta Caixa</label>
                                            </li>
                                            <li class="liNomeContaContabil">
                                                <asp:DropDownList ID="ddlTipoContaC" Enabled="false" CssClass="ddlTipoConta" Width="30px"
                                                    runat="server" ToolTip="Selecione o Tipo de Conta Contábil" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlTipoContaC_SelectedIndexChanged">
                                                    <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                                                    <asp:ListItem Value="C">3 - Receita</asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                            <li class="liNomeContaContabil">
                                                <asp:DropDownList ID="ddlGrupoContaC" Enabled="false" CssClass="ddlGrupoConta" Width="35px"
                                                    runat="server" ToolTip="Selecione o Grupo de Conta Contábil" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlGrupoContaC_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </li>
                                            <li class="liNomeContaContabil">
                                                <asp:DropDownList ID="ddlSubGrupoContaC" Enabled="false" CssClass="ddlSubGrupoConta"
                                                    Width="40px" runat="server" ToolTip="Selecione o SubGrupo de Conta Contábil"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlSubGrupoContaC_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </li>
                                            <li class="liNomeContaContabil">
                                                <asp:DropDownList ID="ddlContaContabilC" Enabled="false" CssClass="ddlContaContabil"
                                                    runat="server" ToolTip="Selecione a Conta Contábil" Width="200px">
                                                </asp:DropDownList>
                                            </li>
                                        </ul>
                                    </li>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ul>
                    </li>
                    <li id="li4" runat="server" class="liBtnAddA" style="margin-left: 342px !important;
                        margin-top: 6px;">
                        <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkMatUniAlu_Click">FINALIZAR</asp:LinkButton>
                    </li>
                </ul>
            </div>
            <div id="tabAtiExtAlu" class="tabFormaPgto" runat="server" clientidmode="Static" style="display: none;">
                <ul id="ul8" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -7px;
                        background-color: #FDF5E6; margin-bottom: 5px;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            INFORMAÇÕES DO ALUNO - ATIVIDADES EXTRAS</label>
                    </li>
                    <li class="liNomeAluETA">
                        <label for="txtNomeAluRAA" title="Nome do Aluno">
                            NOME</label>
                        <asp:TextBox ID="txtNomeAluAEA" CssClass="campoNomePessoa" runat="server" Enabled="false"
                            ToolTip="Nome do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="liNIREAluETA">
                        <label for="txtNIREAluRAA" title="Nº NIRE do Aluno">
                            Nº NIRE</label>
                        <asp:TextBox ID="txtNIREAluAEA" runat="server" CssClass="txtNireAluno" Enabled="false"
                            ToolTip="Nº NIRE do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="liNISAluETA">
                        <label for="txtNISAluRAA" title="Nº NIS do Aluno">
                            Nº NIS</label>
                        <asp:TextBox ID="txtNISAluAEA" runat="server" CssClass="txtNisAluno" Enabled="false"
                            ToolTip="Nº NIS do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="G2Clear" style="width: 100%; border-bottom: 1px solid #CCCCCC; margin-left: 15px;
                        padding-bottom: 5px; margin-top: 5px;">
                        <label style="font-family: Tahoma;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Para incluir uma nova Atividade Extra do Aluno informe
                            os dados de cada linha acima da grid de Dados correspondente, após clique em INCLUIR.
                            <br />
                            &nbsp;&nbsp;Para excluir, marque a coluna CHECK na Grid de Dados da informação que
                            deseja eliminar e click no botão excluir para efetivar a exclusão desejada.</label>
                    </li>
                    <li class="liDescTelAdd" style="width: 100%; text-align: center; margin-top: 2px;
                        margin-bottom: 20px;">
                        <label>
                            Os campos com <span class="asteriscoObrigatorio">*</span>&nbsp; são obrigatórios
                            e devem ser informados.
                        </label>
                    </li>
                    <li style="margin-left: 130px;">
                        <label for="ddlAtivExtra" class="lblObrigatorio" title="Tipo de Restrição">
                            Escolha a Atividade Extra</label>
                        <asp:DropDownList ID="ddlAtivExtra" AutoPostBack="true" OnSelectedIndexChanged="ddlAtivExtra_SelectedIndexChanged"
                            ToolTip="Selecione a Atividade Extra" CssClass="ddlAtivExtra" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="ddlAtivExtra"
                            ErrorMessage="Atividade Extra deve ser informada" ValidationGroup="incAtiExt"
                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li style="margin-left: 10px;">
                        <label for="txtSiglaAEA" title="Qual a restrição?">
                            Sigla</label>
                        <asp:TextBox ID="txtSiglaAEA" CssClass="txtSiglaAEA" runat="server" Enabled="false">
                        </asp:TextBox>
                    </li>
                    <li style="margin-left: 10px;">
                        <label for="txtValorAEA" title="Código da Restrição">
                            Valor</label>
                        <asp:TextBox ID="txtValorAEA" CssClass="campoMoeda" Style="width: 37px;" Enabled="false"
                            runat="server"></asp:TextBox>
                    </li>
                    <li class="liPeriodo" style="clear: none; margin-right: 0px; margin-left: 13px;">
                        <label class="lblObrigatorio" for="txtPeriodo">
                            Período da Atividade Extra</label>
                        <asp:TextBox ID="txtDtIniAEA" CssClass="campoData" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator38" ValidationGroup="incAtiExt"
                            runat="server" ControlToValidate="txtDtIniAEA" ErrorMessage="Data Inicial deve ser informada"
                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                        <asp:Label ID="Label16" CssClass="lblDivData" Style="margin: 0 6px;" runat="server"> à </asp:Label>
                        <asp:TextBox ID="txtDtFimAEA" CssClass="campoData" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator42" ValidationGroup="incAtiExt"
                            runat="server" ControlToValidate="txtDtFimAEA" ErrorMessage="Data Final deve ser informada"
                            Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" CssClass="validatorField"
                            ForeColor="Red" ValidationGroup="incCuiEspAlu" ControlToValidate="txtDtFimAEA"
                            ControlToCompare="txtDtIniAEA" Type="Date" Operator="GreaterThanEqual" ErrorMessage="Data Final não pode ser menor que Data Inicial.">
                        </asp:CompareValidator>
                    </li>
                    <li class="liBtnsAtiExt">
                        <asp:LinkButton ID="lnkIncAtiExt" OnClick="lnkIncAtiExt_Click" ValidationGroup="incAtiExt"
                            runat="server" Style="margin: 0 auto;" ToolTip="Incluir Registro">
                            <img class="imgLnkInc" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa"
                                title="Incluir Registro" />
                            <asp:Label runat="server" ID="Label17" Text="Incluir"></asp:Label>
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkExcAtiExt" OnClick="lnkExcAtiExt_Click" ValidationGroup="excAtiExt"
                            runat="server" Style="margin: 0 auto;" ToolTip="Excluir Registro">
                            <img class="imgLnkExc" src='/Library/IMG/Gestor_BtnDel.png' alt="Icone Pesquisa"
                                title="Excluir Registro" />
                            <asp:Label runat="server" ID="Label18" Text="Excluir"></asp:Label>
                        </asp:LinkButton>
                    </li>
                    <li class="liGridAtv">
                        <div id="Div1" runat="server" class="divGridDoc" style="height: 144px;">
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
                                    <asp:BoundField DataField="VL_ATIV_EXTRA" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"
                                        HeaderText="VALOR">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DT_INI_ATIV" DataFormatString="{0:dd/MM/yyyy}" HeaderText="INÍCIO">
                                        <ItemStyle Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DT_VENC_ATIV" DataFormatString="{0:dd/MM/yyyy}" HeaderText="TÉRMINO">
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
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TABELA DE DOCUMENTOS DE MATRÍCULA                                                                             -->
            <!-- =========================================================================================================================================================================== -->
            <div id="tabDocumentos" style="display: none;" clientidmode="Static" runat="server">
                <ul id="ul3" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -7px;
                        background-color: #FDF5E6; margin-bottom: 5px;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            INFORMAÇÕES DO ALUNO - DOCUMENTOS DE MATRÍCULA</label>
                    </li>
                    <li class="liNomeAluETA">
                        <label for="txtNomeAluDoc" title="Nome do Aluno">
                            NOME</label>
                        <asp:TextBox ID="txtNomeAluDoc" CssClass="campoNomePessoa" runat="server" Enabled="false"
                            ToolTip="Nome do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="liNIREAluETA">
                        <label for="txtNIREAluDoc" title="Nº NIRE do Aluno">
                            Nº NIRE</label>
                        <asp:TextBox ID="txtNIREAluDoc" runat="server" CssClass="txtNireAluno" Enabled="false"
                            ToolTip="Nº NIRE do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="liNISAluETA">
                        <label for="txtNISAluDoc" title="Nº NIS do Aluno">
                            Nº NIS</label>
                        <asp:TextBox ID="txtNISAluDoc" runat="server" CssClass="txtNisAluno" Enabled="false"
                            ToolTip="Nº NIS do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="G2Clear" style="width: 100%; border-bottom: 1px solid #CCCCCC; margin-left: 15px;
                        padding-bottom: 5px; margin-top: 5px;">
                        <label style="font-family: Tahoma;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Para registrar a ENTREGA DE DOCUMENTOS do Aluno necessários
                            para a efetivação de sua Matrícula, marque na coluna CHECK na Grid de Dados
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;os
                            documentos entregues e pressione o botão atulizar a base de dados.</label>
                    </li>
                    <li class="liDescTelAdd" style="width: 100%; text-align: center; margin-top: 2px;
                        margin-bottom: 20px;">
                        <label>
                            Os campos com <span class="asteriscoObrigatorio">*</span>&nbsp; são obrigatórios
                            e devem ser informados.
                        </label>
                    </li>
                    <li style="margin-left: 190px;">
                        <div id="divGrid" runat="server" class="divGridDoc" style="height: 195px;">
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
            <!--                                                               TABELA DE DOCUMENTOS DE MATRÍCULA                                                                             -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TABELA DE REGISTRO DE MATERIAL COLETIVO                                                                       -->
            <!-- =========================================================================================================================================================================== -->
             <div id="tabMaterialColetivo" class="tabMaterialColetivo" style="display: none;" clientidmode="Static" runat="server">
                            <ul id="ul5" class="ulDados2">
                <li style="width: 100%;text-align: center;text-transform:uppercase;margin-top:-7px;background-color: #FDF5E6;margin-bottom: 5px;">
                    <label style="font-size: 1.1em;font-family:Tahoma;">INFORMAÇÕES DO ALUNO - MATERIAL DE APOIO / UNIFORME</label>
                </li>                   
                <li class="liNomeAluETA" style="margin-left: 17px !important;">
                    <label for="txtNomeAluDoc" title="Nome do Aluno">
                        NOME</label>
                    <asp:TextBox ID="txtNomeAluMU" CssClass="campoNomePessoa" runat="server" Enabled="false" ToolTip="Nome do Aluno">
                    </asp:TextBox>
                </li>   
                <li class="liNIREAluETA">
                    <label for="txtNIREAluDoc" title="Nº NIRE do Aluno">
                        Nº NIRE</label>
                    <asp:TextBox ID="txtNIREAluMU" runat="server" CssClass="txtNireAluno" Enabled="false" ToolTip="Nº NIRE do Aluno">
                    </asp:TextBox>
                </li>  
                <li class="liNISAluETA">
                    <label for="txtNISAluDoc" title="Nº NIS do Aluno">
                        Nº NIS</label>
                    <asp:TextBox ID="txtNISAluMU" runat="server" CssClass="txtNisAluno" Enabled="false" ToolTip="Nº NIS do Aluno">
                    </asp:TextBox>
                </li>   

                <li style="clear: both;">
                 <ul>                   
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                    <li class="liGridMat">
                        <div id="divSolicitacoes" title="Selecione os Itens Solicitados" style="overflow-y:scroll; height: 150px; border: 1px solid #ccc; width:480px; margin-left:37px">
                            <asp:GridView  ID="grdSolicitacoesMatrColet" CssClass="grdBusca" runat="server" style="width:100%;" 
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
                                    <asp:BoundField HeaderText="Descrição" DataField="Descricao" />     
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
                                                <asp:TextBox ID="txtDescSolic" Width="30px" ToolTip="Informe o valor de desconto do item, com ',' para separação de centavos" OnTextChanged="chkDescPer_ChenckedChanged" CssClass="txtDescSolic" Text='<%# bind("Desconto") %>' AutoPostBack="true" Enabled="false" runat="server"/>                                
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
                    <asp:CheckBox ID="ckAtualizaFinancSolicMatrColet" CssClass="chkIsento" Enabled="true" Checked="true" runat="server" Text="Atualiza Financeiro" ToolTip="Selecione se atualizará o financeiro" />
                </li>


                <li style="margin-top: 5px; margin-left: 3px; clear: none;">            
                    <asp:CheckBox ID="chkValUnicTituMatrColet" CssClass="chkIsento" Enabled="false" runat="server" Text="Valores Único Título" ToolTip="Selecione se consolidará os valores em um único título financeiro" />
                </li>


                    <li style="clear: none; margin-left: 5px; margin-top: 5px;">
                        <asp:HiddenField ID="hidValorTotal" runat="server" Value="" />
                        <label for="txtValorTotal" title="Valor Total da Solicitação (R$)">Total R$</label>
                        <asp:TextBox ID="txtValorTotalMatrColet" Width="50" CssClass="maskDin" Enabled="false" runat="server"></asp:TextBox>
                    </li>                                                      


                    <li style="clear:none; margin-left: 5px; margin-top: 6px;">
                        <label for="txtQtdParcelas" title="Informe em quantas parcelas será feito o parcelamento" >QP</label>
                        <asp:TextBox ID="txtQtdParcelas" Width="15px" runat="server" ToolTip="Informe em quantas parcelas será feito o parcelamento"></asp:TextBox>
                    </li>

                <li style="clear: none; margin-left: 8px; margin-top: 5px;">
                    <label for="txtDtVectoSolic" title="Data de Vencimento">Data 1ª Parcela</label>
                    <asp:TextBox ToolTip="Informe a Data de Vencimento da Solicitação" ID="txtDtVectoSolicMatrColet" CssClass="txtDtVectoSolic campoData" runat="server"></asp:TextBox>
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
								<label title="Desconto nas Parcelas: Mensal - Permite informar a quantidade de meses e o mês inicial de desconto, distribuindo o valor entre as parcelas; Total - Distribui o valor de desconto entre as parcelas." style="color: Red;">
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
								<asp:TextBox ID="txtDesctoMensaParc" Width="50" CssClass="maskDin" runat="server">
								</asp:TextBox>
							</li>
							<li style="margin-left: 6px;">
								<label for="txtMesIniDescontoParc" title="Parcela de início do desconto">
									PID</label>
								<asp:TextBox ID="txtMesIniDescontoParc" Enabled="false" Width="20px" ToolTip="Parcela de início do desconto" CssClass="txtMesIniDesconto" style="text-align: right;" runat="server">
								</asp:TextBox>
							</li>
						</ContentTemplate>
						</asp:UpdatePanel>                                
					</ul>
				</div>

                <li style="clear: none; margin-left: 5px;">
                    <label for="ddlBoletoSolic" title="Boleto Bancário - Deve estar cadastrado para Material de Apoio/Uniforme em dados de boleto">Boleto Bancário</label>
                    <asp:DropDownList ID="ddlBoletoSolicMatrColet" runat="server" Width="180px"
                        ToolTip="Selecione o Boleto Bancário">
                    </asp:DropDownList>
				</li>



                    <li style="clear: none; margin-left: 5px; margin-top: 1px;">
                        <label for="txtDtPrevisao" title="Previsão de Entrega">Previsão Entrega</label>
                        <asp:TextBox ToolTip="Informe a Previsão de Entrega" ID="txtDtPrevisaoMatrColet" CssClass="txtDtVectoSolic campoData" runat="server"></asp:TextBox>
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
                            <asp:BoundField DataField="NU_DOC" HeaderText="Nº Docto">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NU_PAR" HeaderText="Nº Par">
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
                    </ContentTemplate>
                    </asp:UpdatePanel>
                 </ul>
               </li> 
               <%-- <li id="li9" runat="server" class="liBtnAddA" style="margin-left: 190px !important;margin-top: 6px; background-color: #ffffe0 !important; color: #000 !important;  clear: none !important;">
                    <asp:LinkButton ID="lnkGrava" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkGrava_Click">EFETIVAR E ATUALIZAR FINANCEIRO</asp:LinkButton>
                </li>  --%>                         

                <li id="li1" runat="server" class="liBtnAddA" style="margin-left: 5px !important;margin-top: 6px; background-color: #e0ffff !important; clear: none !important; width:100px;">
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

                <li id="li14" runat="server" class="liBtnAddA" style="margin-left: 181px !important;margin-top: 6px; clear: none !important;">
                    <asp:LinkButton ID="lnkMatUniAlu" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkMatUniAlu_Click">FINALIZAR</asp:LinkButton>
                </li>                           
            </ul>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TABELA DE REGISTRO DE MATERIAL COLETIVO                                                                              -->
            <!-- =========================================================================================================================================================================== -->

            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TABELA DE MENSALIDADES                                                                                        -->
            <!-- =========================================================================================================================================================================== -->
            <div id="tabMenEsc" class="tabFormaPgto" style="display: none;" clientidmode="Static" runat="server">
                <ul id="ul9" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -7px;
                        background-color: #FDF5E6; margin-bottom: 5px;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            INFORMAÇÕES DO ALUNO - MENSALIDADES ESCOLARES</label>
                    </li>
                    <li class="liNomeAluETA" style="margin-left: 33px;">
                        <label for="txtNomeAluDoc" title="Nome do Aluno">
                            NOME</label>
                        <asp:TextBox ID="txtNomeAluME" CssClass="campoNomePessoa" Style="width: 300px !important;"
                            runat="server" Enabled="false" ToolTip="Nome do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="liNIREAluETA">
                        <label for="txtNIREAluDoc" title="Nº NIRE do Aluno">
                            Nº NIRE</label>
                        <asp:TextBox ID="txtNIREAluME" runat="server" Width="56px" CssClass="txtNireAluno"
                            Enabled="false" ToolTip="Nº NIRE do Aluno">
                        </asp:TextBox>
                    </li>
                    <li class="liNISAluETA">
                        <label for="txtNISAluDoc" title="Nº NIS do Aluno">
                            Nº NIS</label>
                        <asp:TextBox ID="txtNISAluME" runat="server" CssClass="txtNisAluno" Enabled="false"
                            ToolTip="Nº NIS do Aluno">
                        </asp:TextBox>
                    </li>
                    <li style="margin-top: 12px; margin-left: 100px;">
                        <asp:CheckBox ID="chkAtualiFinan" CssClass="chkLocais" AutoPostBack="true" OnCheckedChanged="chkAtualiFinan_CheckedChanged"
                            Checked="false" runat="server" Text="Atualizar Financeiro" ToolTip="Marque se deverá atualizar o financeiro" />
                        <br />
                    </li>
                    <!-- Este código foi retirado para adequação do código para a alteração do desconto - Victor Martins Machado - 27/02/2013 -->
                    <!--li class="G2Clear" style="width: 100%; border-bottom:1px solid #CCCCCC;margin-left: 15px;padding-bottom: 2px;text-align:center;">
                    <label style="font-family: Tahoma;">
                    Demonstrativo das Mensalidades Escolares no ano letivo.</label>
                </li-->
                    <li>
                        <ul>
                            <li style="margin-left: 33px; margin-top: 10px; width: 100%;">
                                <ul>
                                    <li>
                                        <div style="float: left; border-right: 2px solid #CCCCCC; width: 337px; margin-left: -5px;">
                                            <!-- Checkbox do tipo de contrato -->
                                            <div>
                                                <asp:CheckBox ID="chkTipoContrato" OnCheckedChanged="chkTipoContrato_CheckedChange"
                                                    Checked="false" CssClass="chkLocais" runat="server" Text="Qual o tipo de valor de Contrato?"
                                                    ToolTip="Marque se deverá utilizar um tipo de contrato diferente." AutoPostBack="true" />
                                                <%--<span style="">Tipo Valor</span>--%>
                                                <asp:DropDownList ID="ddlTipoValorCurso" runat="server" AutoPostBack="True" Enabled="False"
                                                    OnSelectedIndexChanged="ddlTipoValorCurso_SelectedIndexChanged" ToolTip="Selecione o tipo de contrato">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlTipoContrato" OnSelectedIndexChanged="ddlTipoContrato_SelectedIndexChanged"
                                                    AutoPostBack="true" Width="60px" Enabled="false" ToolTip="Selecione o tipo de pagamento"
                                                    runat="server" ClientIDMode="Static">
                                                </asp:DropDownList>
                                            </div>
                                            <!-- Checkbox de alteração do valor de contrato -->
                                            <div>
                                                <asp:CheckBox ID="chkAlterValorContr" CssClass="chkLocais" runat="server" Text="Altera o valor de contrato?"
                                                    ToolTip="Marque se deverá gerar o total de parcelas do curso independente do ano."
                                                    OnCheckedChanged="chkAlterValorContr_CheckedChanged" AutoPostBack="true" />
                                                <asp:TextBox Enabled="false" ID="txtValorContratoCalc" CssClass="maskDin"
                                                    Style="text-align: right; margin-left: 4px;" runat="server" Width="50px" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="vgMontaGridMensa"
                                                    runat="server" ControlToValidate="txtValorContratoCalc" ErrorMessage="Valor de Contrato deve ser informado"
                                                    Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                            </div>
                                            <!-- Checkbox do valor do contrato proporcional -->
                                            <div>
                                                <asp:CheckBox ID="chkValorContratoCalc" Checked="false" runat="server" CssClass="chkLocais"
                                                    ToolTip="Marque se o sistema deverá calcular o valor do contrato."
                                                    Text="Calcular valor de contrato?" ClientIDMode="Static"/>
                                                <asp:DropDownList ID="ddlValorContratoCalc" Width="125px" Enabled="false" Style="margin-left: 4px;"
                                                    ToolTip="Selecione o Nome da Bolsa" runat="server" OnSelectedIndexChanged="ddlValorContratoCalc_OnSelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static">
                                                    <asp:ListItem Value="P" Selected="true">Proporcional Meses</asp:ListItem>
                                                    <asp:ListItem Value="T">Total (Todos os meses)</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <!-- Checkbox de geração do total de parcelas -->
                                            <div>
                                                <asp:CheckBox ID="chkGeraTotalParce" CssClass="chkLocais" runat="server" Text="Altera o n° original de parcelas de cadastro?"
                                                    ToolTip="Marque se deverá alterar o n° original de parcelas cadastrado no curso."
                                                    OnCheckedChanged="chkGeraTotalParce_CheckedChanged" AutoPostBack="true" />
                                                <asp:TextBox ID="txtQtdeParcelas" OnTextChanged="txtQtdeParcelas_TextChanged" AutoPostBack="true"
                                                    ToolTip="Informa a quantidade de parcelas do curso" Width="15px" CssClass="txtQtdeMesesDesctoMensa"
                                                    runat="server" Enabled="false">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="vgMontaGridMensa"
                                                    runat="server" ControlToValidate="txtQtdeParcelas" ErrorMessage="Quantidade de parcelas do curso deve ser informada"
                                                    Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                            </div>
                                            <!-- Checkbox da primeira parcela -->
                                            <div>
                                                <asp:CheckBox ID="chkDataPrimeiraParcela" Checked="false" CssClass="chkLocais" runat="server"
                                                    Text="Altera data/valor 1ª parcela?" 
                                                    ToolTip="Marque se deverá informar a data da primeira parcela." ClientIDMode="Static"/>
                                                <asp:TextBox ID="txtDtPrimeiraParcela" ToolTip="Informa a data de pagamento da primeira parcela."
                                                    CssClass="txtPeriodoIniDesconto campoData" runat="server" Enabled="false" ClientIDMode="Static">
                                                </asp:TextBox>
                                                <span>/ R$</span>
                                                <asp:TextBox ID="txtValorPrimParce" CssClass="maskDin" Width="48px" Style="text-align: right;"
                                                    ToolTip="Informe o valor da primeira parcela" runat="server" Enabled="false" ClientIDMode="Static" >
                                                </asp:TextBox>
                                            </div>
                                            <div style="margin-bottom:1px">
                                               <asp:CheckBox runat="server" Checked="true" CssClass="chkLocais" ID="chkTaxaMatricula" Text="Gera com Taxa de Matrícula/R$"
                                               OnCheckedChanged="chkTaxaMatricula_OnCheckedChanged" AutoPostBack="true"/>
                                               <asp:TextBox ID="txtVlTxMatricula" runat="server" Enabled="false" Width="50px" CssClass="maskDin" ToolTip="O Valor da taxa de                matrícula já virá carregado automaticamente de acordo com as informações cadastrais do curso selecionado"></asp:TextBox>
                                           </div>
                                        </div>
                                        <div style="float: right; margin-left: 6px;">
                                            <!-- Checkbox do desconto por bolsa -->
                                            <div>
                                                <div>
                                                    <asp:CheckBox ID="chkManterDesconto" Checked="false" CssClass="chkLocais" runat="server"
                                                        Text="Altera Bolsa/Convênio " ToolTip="Marque se deverá manter desconto de Bolsa/Convênio"
                                                        OnCheckedChanged="chkManterDesconto_CheckedChanged" AutoPostBack="true" />
                                                    <asp:DropDownList ID="ddlTpBolsaAlt" Width="75px" Enabled="false" OnSelectedIndexChanged="ddlTpBolsaAlt_SelectedIndexChanged"
                                                        ToolTip="Selecione o Nome da Bolsa" AutoPostBack="True" runat="server">
                                                        <asp:ListItem Value="N" Selected="True">Nenhuma</asp:ListItem>
                                                        <asp:ListItem Value="B">Bolsa</asp:ListItem>
                                                        <asp:ListItem Value="C">Convênio</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <span>/</span>
                                                    <asp:DropDownList ID="ddlBolsaAlunoAlt" Enabled="false" CssClass="ddlBolsaAluno"
                                                        Width="135px" ToolTip="Selecione o nome da Bolsa" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlBolsaAlunoAlt_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <br />
                                                </div>
                                                <div style="margin-top: -1px; margin-left: 6px;">
                                                    <span>R$/% mês</span>
                                                    <asp:TextBox ID="txtValorDescto" CssClass="maskDin" Width="50px" ToolTip="Informe o valor do Desconto"
                                                        runat="server" Enabled="true">
                                                    </asp:TextBox>
                                                    <asp:CheckBox CssClass="chkLocais" Style="margin-left: -2px;" ID="chkManterDescontoPerc"
                                                        TextAlign="Right" Enabled="false" runat="server" ToolTip="% de Desconto da Bolsa?"
                                                        Text="%" />
                                                    <span title="Período de Duração do Período do desconto" style="margin-left: 3px;">Período</span>
                                                    <asp:TextBox ID="txtPeriodoIniDesconto" Style="clear: both;" Enabled="false" ToolTip="Informe a Data do Período do desconto"
                                                        runat="server" CssClass="txtPeriodoIniDesconto campoData"></asp:TextBox>
                                                    <span>à</span>
                                                    <asp:TextBox ID="txtPeriodoFimDesconto" Enabled="false" ToolTip="Informe a Data de Término do Período do desconto"
                                                        runat="server" CssClass="txtPeriodoIniDesconto campoData"></asp:TextBox>
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
                                                                <asp:DropDownList ID="ddlTipoDesctoMensa" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoDesctoMensa_SelectedIndexChanged"
                                                                    ToolTip="Selecione o Tipo de Desconto da Mensalidade" CssClass="ddlTipoDesctoMensa"
                                                                    runat="server">
                                                                    <asp:ListItem Selected="true" Text="Total" Value="T"></asp:ListItem>
                                                                    <asp:ListItem Text="Mensal" Value="M"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </li>
                                                            <li style="margin-left: 10px;">
                                                                <label for="txtQtdeMesesDesctoMensa" title="Quantidade de meses de desconto de mensalidade">
                                                                    Qt Meses</label>
                                                                <asp:TextBox ID="txtQtdeMesesDesctoMensa" ToolTip="Informa a quantidade de meses de desconto de mensalidade"
                                                                    CssClass="txtQtdeMesesDesctoMensa" runat="server" Enabled="false">
                                                                </asp:TextBox>
                                                            </li>
                                                            <li style="margin-left: 10px;">
                                                                <label for="txtDesctoMensa" title="R$ Desconto">
                                                                    R$ Desconto</label>
                                                                <asp:TextBox ID="txtDesctoMensa" CssClass="maskDin" runat="server">
                                                                </asp:TextBox>
                                                            </li>
                                                            <li>
                                                                <label for="txtMesIniDesconto" title="Parcela de início do desconto">
                                                                    PID</label>
                                                                <asp:TextBox ID="txtMesIniDesconto" Enabled="false" Width="20px" ToolTip="Parcela de início do desconto"
                                                                    CssClass="txtMesIniDesconto" Style="text-align: right;" runat="server">
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
                                        <label for="ddlBoleto" title="Boleto Bancário">
                                            Boleto</label>
                                        <asp:DropDownList ID="ddlBoleto" runat="server" CssClass="ddlBoleto" Style="clear: both;"
                                            ToolTip="Selecione o Boleto Bancário">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: right; margin-left: 5px;">
                                        <label for="ddlTipoDesctoMensa" title="Dia de vencimento">
                                            Dia</label>
                                        <asp:DropDownList ID="ddlDiaVecto" ToolTip="Selecione o Dia de Vencimento da Mensalidade"
                                            CssClass="ddlDiaVecto" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </li>
                            <li runat="server" id="liBtnGrdFinan" class="liBtnGrdFinan" style="margin-left: 318px;
                                margin-top: 7px; margin-right: 10px;">
                                <asp:LinkButton ID="lnkMontaGridMensa" OnClick="lnkMontaGridMensa_Click" ValidationGroup="vgMontaGridMensa"
                                    runat="server" Style="margin: 0 auto;" ToolTip="Montar Grid Financeira">
                                    <asp:Label runat="server" ID="Label6" ForeColor="GhostWhite" Text="GERAR GRID FINANCEIRA"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </li>
                    <li class="labelInLine" style="width: 681px; margin-left: 33px; margin-top: -3px">
                        <div id="divMensaAluno" runat="server" style="height: 175px; border: 1px solid #CCCCCC;
                            overflow-y: auto; margin-top: 10px;">
                            <asp:GridView runat="server" ID="grdNegociacao" CssClass="grdBusca" ToolTip="Grid demonstrativa das mensalidades do aluno."
                                AutoGenerateColumns="False" Width="100%">
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
                    <li style="clear: both; margin-top: 2px; margin-left: 33px;">
                        <label title="Valor Total do Contrato de Mensalidade" style="margin-right: 5px !important;">
                            R$ Contrato</label>
                        <asp:TextBox ID="txtTotalMensa" CssClass="txtValor campoMoeda" Style="width: 75px;"
                            runat="server" ToolTip="Valor Total do Contrato de Mensalidade" Enabled="false"></asp:TextBox>
                    </li>
                    <li style="margin-top: 2px; margin-left: 15px;">
                        <label title="Valor Total da Bolsa Escolar do Aluno" style="margin-right: 5px !important;">
                            R$ Bolsa Escolar</label>
                        <asp:TextBox ID="txtTotalDesctoBolsa" CssClass="txtValor campoMoeda" Style="width: 75px;"
                            runat="server" ToolTip="Valor Total da Bolsa Escolar do Aluno" Enabled="false"></asp:TextBox>
                    </li>
                    <li style="margin-top: 2px; margin-left: 15px;">
                        <label title="Valor Total do Desconto Especial" style="margin-right: 5px !important;">
                            R$ Descto Esp</label>
                        <asp:TextBox ID="txtTotalDesctoEspec" CssClass="txtValor campoMoeda" Style="width: 75px;"
                            runat="server" ToolTip="Valor Total do Desconto Especial" Enabled="false"></asp:TextBox>
                    </li>
                    <li style="margin-top: 2px; margin-left: 15px;">
                        <label title="Valor Total Total Líquido do Contrato" style="margin-right: 5px !important;">
                            R$ Líquido Contr</label>
                        <asp:TextBox ID="txtTotalLiquiContra" CssClass="txtValor campoMoeda" Style="width: 75px;"
                            runat="server" ToolTip="Valor Total Total Líquido do Contrato" Enabled="false"></asp:TextBox>
                    </li>
                    <li class="liBtnAddA" style="margin-left: 150px !important; margin-top: 10px; clear: none; width:145px">
                        <asp:LinkButton ID="lnkMenAlu" runat="server" ValidationGroup="atuMensaAlu" OnClick="lnkMenAlu_Click">FINALIZAR GRID FINANCEIRA</asp:LinkButton>
                    </li>
                </ul>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TABELA DE FORMA DE PAGAMENTO                                                                                        -->
            <!-- =========================================================================================================================================================================== -->

            <div id="TabFormaPgto" class="tabFormaPgto" style="display: none;" clientidmode="Static" runat="server">
                <ul id="ul11" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -7px;
                        background-color: #FDF5E6; margin-bottom: 5px;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            INFORMAÇÕES DO ALUNO - FORMA DE PAGAMENTO DA MENSALIDADE</label>
                    </li>
                    <li>
                        <label>NOME</label>
                        <asp:TextBox runat="server" ID="txtNomeAlunoPgto" Width="220px"></asp:TextBox>
                    </li>
                    <li>
                        <label>Nº NIRE</label>
                        <asp:TextBox runat="server" ID="txtNireAlunoPgto" Width="60px"></asp:TextBox>
                    </li>
                    <li>
                        <label>Nº NIS</label>
                        <asp:TextBox runat="server" ID="txtNisAlunoPgto" Width="70px"></asp:TextBox>
                    </li>
                    <li>
                                <label>R$ Contrato</label>
                                <asp:TextBox runat="server" ID="txtValContrPgto" Enabled="false" CssClass="campoMoeda" Width="50px" ToolTip="Valor total do contrato da matrícula"></asp:TextBox>
                    </li>
                    <li>
                                <label>QtP</label>
                               <asp:TextBox runat="server" ID="txtQtPgto" Width="15px" ToolTip="Quantidade de parcelas "></asp:TextBox>
                   </li>
                   <li>
                        <label>Desconto</label>
                        <asp:TextBox runat="server" ID="txtValDescTotPGTO" Enabled="false" CssClass="campoMoeda" Width="50px" ToolTip="Valor total de desconto na matrícula"></asp:TextBox>
                   </li>
                    <li style="clear:both;">
                        <div class="divEsquePgto">
                            <ul>
                            <li style="clear:both; margin-left:10px; margin-bottom:5px">
                                <asp:Label runat="server" ID="lblOutForPg" class="lblchkPgto">Outras Formas de Recebim.</asp:Label>
                            </li>
                            <li style="clear:both;">
                                <asp:CheckBox ClientIDMode="Static" ID="chkDinhePgto" class="chkCadBasAlu" runat="server" />
                                <asp:Label runat="server" ID="lblDinPgto" >Dinheiro</asp:Label>
                                <asp:TextBox runat="server" ID="txtValDinPgto" Width="50px" CssClass="maskDin" Enabled="false" clientidmode="Static" style="margin-left:32px"
                                    ToolTip="Informar o valor total de recebimento em Dinheiro na Matrícula"></asp:TextBox>
                            </li>
                            <li style="clear:both;">
                                <asp:CheckBox ClientIDMode="Static" ID="chkDepoPgto" class="chkCadBasAlu" runat="server" />
                                <asp:Label runat="server" ID="Label23" >Depósito</asp:Label>
                                <asp:TextBox runat="server" ID="txtValDepoPgto" Width="50px" CssClass="maskDin" Enabled="false" clientidmode="Static" 
                                    style="margin-left:31px" ToolTip="Informar o valor total de recebimento em Depósito Bancário na Matrícula"></asp:TextBox>
                            </li>
                            <li style="clear:both;">
                                <asp:CheckBox ClientIDMode="Static" ID="chkDebConPgto" class="chkCadBasAlu" runat="server" />
                                <asp:Label runat="server" ID="Label24" >Déb. Conta</asp:Label>
                                <asp:TextBox runat="server" ID="txtValDebConPgto" Width="50px" CssClass="maskDin" Enabled="false" clientidmode="Static"
                                    ToolTip="Informar o valor total de recebimento em Débito em Conta na Matrícula"></asp:TextBox>
                                <asp:TextBox runat="server" ID="txtQtMesesDebConPgto" Width="15px" Enabled="false" clientidmode="Static" ToolTip="A Quantidade de meses que haverá o Débito em Conta"></asp:TextBox>
                            </li>
                            <li style="clear:both;">
                                <asp:CheckBox ClientIDMode="Static" ID="chkTransPgto" class="chkCadBasAlu" runat="server" />
                                <asp:Label runat="server" ID="Label25" >Transferência</asp:Label>
                                <asp:TextBox runat="server" ID="txtValTransPgto" Width="50px" CssClass="maskDin" Enabled="false" clientidmode="Static" 
                                    style="margin-left:9px" ToolTip="Informar o valor total de recebimento em Transferência Bancária na Matrícula"></asp:TextBox>
                            </li>
                            <li style="clear:both;">
                                <asp:CheckBox ClientIDMode="Static" ID="chkOutrPgto" class="chkCadBasAlu" runat="server" />
                                <asp:Label runat="server" ID="lblvlPgto" >Boleto</asp:Label>
                                <asp:TextBox runat="server" ID="txtValOutPgto" Width="50px" CssClass="maskDin" Enabled="false" clientidmode="Static"
                                     style="margin-left:42px" ToolTip="Informar o valor total de recebimento em Cobrança Bancária na Matrícula"></asp:TextBox>
                                <%--<hr class="hrLinhaPgto" style="margin-top:6px"/>--%>
                            </li>
                            <li style="clear:both; margin-left:4px">
                                <div style="border-top:1px solid #CCCCCC; border-bottom: 1px solid #CCCCCC; width:146px; height:40px; margin-top:10px; padding-top:10px;">
                                    <div style="margin-left:7px">
                                        <asp:Label runat="server" ID="Label26" class="lblchkPgto">Valor Total de Boleto</asp:Label>
                                        <asp:TextBox runat="server" ID="txtTotalBolPGTO" Width="50px" CssClass="campoMoeda" 
                                         ToolTip="Valor total de em cobrança bancária calculado de acordo com as informações preenchidas na Grid Financeira" 
                                         Enabled="false" style="margin-left:-5px"></asp:TextBox>
                                    </div>
                                </div>
                            </li>
                        </ul>
                        </div>
                    </li>
                    <li>
                        <div class="divDirePgto">
                            <ul>
                                <li style="margin-bottom:4px; margin-left:-4px;">
                                    <asp:CheckBox runat="server" ID="chkCartaoCreditoPgto" ClientIDMode="Static" />
                                    <asp:Label runat="server" ID="lblCarCrePgto" class="lblchkPgto">Cartão de Crédito</asp:Label>
                                </li>
                                <li style="clear:both;">
                                    <label>Bandeira</label>
                                    <ul>
                                        <li><asp:DropDownList runat="server" ID="ddlBandePgto1" Enabled="false" ClientIDMode="Static">
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
                                        </asp:DropDownList></li>
                                        <li style="clear:both"><asp:DropDownList runat="server" ID="ddlBandePgto2" Enabled="false" ClientIDMode="Static" >
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
                                        </asp:DropDownList></li>
                                        <li style="clear:both"><asp:DropDownList runat="server" ID="ddlBandePgto3" Enabled="false" ClientIDMode="Static" >
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
                                        </asp:DropDownList></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>Número</label>
                                    <ul>
                                        <li><asp:TextBox runat="server" ID="txtNumPgto1" CssClass="numeroCartao" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtNumPgto2" CssClass="numeroCartao" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtNumPgto3" CssClass="numeroCartao" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>Titular</label>
                                    <ul>
                                        <li><asp:TextBox runat="server" ID="txtTitulPgto1" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtTitulPgto2" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtTitulPgto3" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>Venc</label>
                                    <ul>
                                        <li><asp:TextBox runat="server" ID="txtVencPgto1" Width="30px" CssClass="dtVencimentoCartao" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtVencPgto2" Width="30px" CssClass="dtVencimentoCartao" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtVencPgto3" Width="30px" CssClass="dtVencimentoCartao" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>R$ Crédito</label>
                                    <ul>
                                        <li><asp:TextBox runat="server" ID="txtValCarPgto1" Width="50px" CssClass="maskDin" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtValCarPgto2" Width="50px" CssClass="maskDin" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtValCarPgto3" Width="50px" CssClass="maskDin" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>Parcelas</label>
                                    <ul>
                                        <li><asp:TextBox runat="server" ID="txtQtParcCC1" Width="15px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtQtParcCC2" Width="15px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtQtParcCC3" Width="15px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>

                                <li style="clear:both; margin-top:10px; margin-bottom:4px; margin-left:-4px;">
                                    <asp:CheckBox runat="server" ID="chkDebitPgto" ClientIDMode="Static"/>
                                    <asp:Label runat="server" ID="Label2" class="lblchkPgto">Cartão de Débito</asp:Label>
                                </li>
                                <li style="clear:both;">
                                    <label>Bco</label>
                                    <ul>
                                        <li><asp:DropDownList runat="server" ID="ddlBcoPgto1" Enabled="false" ClientIDMode="Static"></asp:DropDownList></li>

                                        <li style="clear:both"><asp:DropDownList runat="server" ID="ddlBcoPgto2" Enabled="false" ClientIDMode="Static"></asp:DropDownList></li>

                                        <li style="clear:both"><asp:DropDownList runat="server" ID="ddlBcoPgto3" Enabled="false" ClientIDMode="Static"></asp:DropDownList></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>Agência</label>
                                    <ul>
                                        <li><asp:TextBox runat="server" ID="txtAgenPgto1" Width="60px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtAgenPgto2" Width="60px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtAgenPgto3" Width="60px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>Nº Conta</label>
                                    <ul>
                                        <li><asp:TextBox runat="server" ID="txtNContPgto1" Width="60px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtNContPgto2" Width="60px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtNContPgto3" Width="60px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>Número</label>
                                    <ul>
                                        <li><asp:TextBox runat="server" ID="txtNuDebtPgto1" Width="80px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtNuDebtPgto2" Width="80px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtNuDebtPgto3" Width="80px" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>Titular</label>
                                    <ul>
                                        <li><asp:TextBox runat="server" ID="txtNuTitulDebitPgto1" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtNuTitulDebitPgto2" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtNuTitulDebitPgto3" Enabled="false" ClientIDMode="Static"></asp:TextBox></li>
                                    </ul>
                                </li>
                                <li>
                                    <label>R$ Débito</label>
                                    <ul>
                                        <li><asp:TextBox runat="server" ID="txtValDebitPgto1" Width="50px" CssClass="maskDin" Enabled="false" ClientIDMode="Static" ></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtValDebitPgto2" Width="50px" CssClass="maskDin" Enabled="false" ClientIDMode="Static" ></asp:TextBox></li>
                                        <li style="clear:both"><asp:TextBox runat="server" ID="txtValDebitPgto3" Width="50px" CssClass="maskDin" Enabled="false" ClientIDMode="Static" ></asp:TextBox></li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </li>
                   <li style="clear:both; margin-left:10px;">
                        <ul>
                                <li style="margin-bottom:4px; margin-left:-4px;">
                                    <asp:CheckBox runat="server" ID="chkChequePgto" />
                                    <asp:Label runat="server" ID="Label22" class="lblchkPgto">Cheque</asp:Label>
                                </li>
                              <li style="clear:both;">
                                            <div class="divGrdChequePgto">
                                               <asp:GridView ID="grdChequesPgto" CssClass="grdBusca" runat="server" Style="width: 100%;
                                                   height: 15px;" AutoGenerateColumns="false" >
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
                                                                      <asp:DropDownList runat="server" Width="50px" ID="ddlBcoChequePgto" style="margin: 0px !Important;"></asp:DropDownList>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Agência">
                                                                  <ItemStyle HorizontalAlign="Center" Width="125px" />
                                                                  <ItemTemplate>
                                                                      <asp:TextBox ID="txtAgenChequePgto" Style="margin: 0px; width:50px;" runat="server"
                                                                          Text='<%# Bind("AgenChe") %>' />
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Nº Conta">
                                                                  <ItemStyle HorizontalAlign="Center" Width="125px" />
                                                                  <ItemTemplate>
                                                                      <asp:TextBox ID="txtNrContaChequeConta" Style="margin: 0px; width:50px;" runat="server"
                                                                          Text='<%# Bind("nuConChe") %>' />
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Nº Cheque">
                                                                  <ItemStyle HorizontalAlign="Center" Width="125px" />
                                                                  <ItemTemplate>
                                                                      <asp:TextBox ID="txtNrChequePgto" Style="margin: 0px; width:60px;" runat="server"
                                                                          Text='<%# Bind("nuCheChe") %>' />
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="CPF">
                                                                  <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                  <ItemTemplate>
                                                                      <asp:TextBox ID="txtNuCpfChePgto" CssClass="campoCpf" Style="margin: 0px; width:75px;" runat="server"
                                                                          Text='<%# Bind("nuCpfChe") %>' />
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Titular">
                                                                  <ItemStyle HorizontalAlign="Center" Width="125px" />
                                                                  <ItemTemplate>
                                                                      <asp:TextBox ID="txtTitulChequePgto" Style="margin: 0px; width:120px;" runat="server"
                                                                          Text='<%# Bind("noTituChe") %>' />
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="R$ Cheque">
                                                                  <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                  <ItemTemplate>
                                                                      <asp:TextBox ID="txtVlChequePgto" CssClass="maskDin" Style="margin: 0px; width:40px;" runat="server"
                                                                          Text='<%# Bind("vlCheChe") %>' />
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
                                <li style="clear:both">
                                    <asp:LinkButton runat="server" ID="btnMaisLinhaChequePgto" OnClick="btnMaisLinhaChequePgto_OnClick">Cadastrar mais Cheques</asp:LinkButton>
                                </li>
                                <li id="liefefpgto" clientidmode="Static" class="liBtnAddA" style="margin-left: 597px !important; margin-top: 10px; clear: none; width:115px;">
                                    <asp:LinkButton ID="lnkFormPgto" runat="server" ValidationGroup="atuPgtoAlu" OnClientClick="geraPadrao(this);__doPostBack('lnkFormPgto', '')" OnClick="lnkEfetMatric_Click" Text="EFETIIVAR MATRÍCULA" ClientIDMode="Static"></asp:LinkButton>
                                    <label id="lblEfeFP" clientidmode="Static" style="display:none;">EFETIVANDO...</label>
                                </li>
                                </ul>
                             </li>
                            <%--</ul>
                        </div>--%>
                </ul>
            </div>

            <%--=========================================================================================================================================================================== -->--%>
            <!--                                                               TABELA DE FORMA DE PAGAMENTO                                                                                        -->
            <!-- =========================================================================================================================================================================== -->
            <div id="Div2" style="display: none;" clientidmode="Static" runat="server">
            </div>

            <div id="divLoadShowReservas" style="display: none;">
            </div>
            <div id="divLoadShowAlunos" style="display: none; height: 305px !important;" />
            <div id="divLoadShowResponsaveis" style="display: none; height: 315px !important;" />
            <div id="divLoadShowDoencas" style="display: none; height: 325px !important;" />
        </li>
    </ul>
    <script type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        //Função chamada depois de se haver postback na página, para restaurar as regras do javascript
        prm.add_endRequest(function () {
            carregaJS();
        });

        $(document).ready(function () {
            carregaJS();
            
            //Estes são declarados apenas na inicialização da página pois eles não são perdidos nos postbacks
            $(".txtHrAplic").mask("99:99");
            $(".txtCPF").mask("999.999.999-99");
            $(".campoCepValid").mask("99999-999");
            $(".txtTelCelularResp").mask("(99) 9999-9999");

        });

        function geraPadrao(el) {
            el.disabled = true;
            el.text = 'EFETIVANDO...';

            //Oculta o linkbutton que chama o método de efetivação e mostra uma label "Efetivando" para evitar clique duplo no botão superior
            $("#lnkEfetMatric").css("display", "none");
            $("#imgEfetiMatric").css("display", "none");
            $("#lblEfeTOP").css("display", "block");
            $("#lilnkEfetMatric").css("background-color", "#F5F5F5");

            //Oculta o linkbutton que chama o método de efetivação e mostra uma label "Efetivando" para evitar clique duplo no botão inferior na aba forma de pagamento
            $("#lnkFormPgto").css("display", "none");
            $("#lblEfeFP").css("display", "block");
            $("#liefefpgto").css("background-color", "#F5F5F5");

            //Mostra a mensagem de Efetivando matrícula e a imagem de Carregando.
            $("#divMensEfe").css("display", "block");

            setInterval(function () {
                $("#pEfeM").fadeIn();
            }, 800);

            setInterval(function () {
                $("#pEfeM").fadeOut();
            }, 400);
        }

        //Função chamada para mostrar/ocultar as abas nos momentos apropriados
        function controleTab(id, idChk) {

            $("#tabResp").hide();
            $("#tabAluno").hide();
            $("#tabResAliAdd").hide();
            $("#tabEndAdd").hide();
            $("#tabTelAdd").hide();
            $("#tabAtiExtAlu").hide();
            $("#tabDocumentos").hide();
            $("#tabMaterialColetivo").hide();
            $("#tabMenEsc").hide();
            $("#TabFormaPgto").hide();
            $("#tabUniMat").hide();
            $("#tabUniMat").hide();
            $("#tabCuiEspAdd").hide();

            $("#" + id).show();

            $("#chkCuiEspAlu").selected(false)
            $("#chkMenEscAlu").selected(false)
            $("#chkPgtoAluno").selected(false)
            $("#chkDocMat").selected(false)
            $("#chkResAliAlu").selected(false)
            $("#chkEndAddAlu").selected(false)
            $("#chkTelAddAlu").selected(false)
            $("#chkMatrColet").selected(false)
            $("#chkRegAtiExt").selected(false)
            
            $("#" + idChk).selected(true)
        }

        //Função responsável por subir todas as regras de Javascript da página
        function carregaJS() {
            $("input.txtPeriodoDeIniBolAluno").datepicker();
            $(".txtPeriodoDeIniBolAluno").mask("99/99/9999");
            $("input.txtDtVectoSolic").datepicker();
            $(".txtDtVectoSolic").mask("99/99/9999");
            $(".txtQtdeMesesDesctoMensa").mask("?99");
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
            $(".txtNISRespValid").mask("?999999999999999");
            $(".txtNisAluno").mask("?999999999999999");
            $(".txtQtdeCEA").mask("?9999");
            $(".txtAno").mask("9999");
            $(".txtQtdeMatEsc").mask("?999");
            $(".txtTelResidencialResp").mask("(99) 9999-9999");
            $(".txtTelCelularNoveDigitos").mask("(99) 99999-9999");
            $(".txtTelEmpresaResp").mask("(99) 9999-9999");
            $(".txtNumReserva").mask("9999.99.9999");
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtQtdMenoresResp").mask("?99");
            $(".txtNumeroEmp").mask("?99999");
            $(".txtPassaporteResp").mask("?999999999");
            $(".txtNumeroAlunoValid").mask("?99999");
            $(".txtQtdeSolic").mask("?99");
            $(".txtMesIniDesconto").mask("?99");
            $(".dtVencimentoCartao").mask("99/99");
            $(".numeroCartao").mask("9999.9999.9999.9999");
            $(".maskDin").maskMoney({ symbol: "", decimal: ",", thousands: "." });

            $('.liListaEndereco').click(function () {
                var strEndereco = $('.txtLogradouroAluno').val() + "";
                strEndereco = strEndereco.replace(/ /g, "*");
                $('#divAddTipo').dialog({ autoopen: false, modal: true, width: 430, height: 390, resizable: false,
                    open: function () { $('#divAddTipo').load("/Componentes/ListarCEPsEndereco.aspx?strEndereco=" + strEndereco); }
                });
            });

            if (($(".lblSucInfAlu").is(":visible"))) {
                $("#tabAluno").hide();
            }

            $(".lnkPesRes").click(function () {
                $("#divLoadShowReservas").load("../../../../../Componentes/ListarReservasMat.aspx", function () {
                    $("#divLoadShowReservas #frmListarReservasMat").attr("action", "../../../../../Componentes/ListarReservasMat.aspx");
                });

                $("#divLoadShowReservas").dialog({ title: "RESERVAS DE MATRÍCULA", modal: true, width: "600px", draggable: false, resizable: false, open: function ()       { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
            });

            $(".lnkPesNIRE").click(function () {
                $('#divLoadShowAlunos').dialog({ title: "LISTA DE ALUNOS", modal: true, width: "970px", draggable: false, resizable: false,
                    open: function () { $('#divLoadShowAlunos').load("/Componentes/ListarAlunos.aspx"); }
                });
            });

            $(".lnkPesResp").click(function () {
                $("#divLoadShowResponsaveis").dialog({ title: "LISTA DE RESPONSÁVEIS", modal: true, width: "690px", draggable: false, resizable: false,
                    open: function () { $('#divLoadShowResponsaveis').load("/Componentes/ListarResponsaveis.aspx"); }
                });
            });

            $('.lnkPesqCID').click(function () {
                $('#divLoadShowDoencas').dialog({ autoopen: false, modal: true, width: 430, height: 390, resizable: false,
                    open: function () { $('#divLoadShowDoencas').load("/Componentes/ListarDoencas.aspx"); }
                });
            });

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
                }
                else {
                    //                    $("#txtValDinPgto").attr('disabled', 'disabled');
                    $("#txtValOutPgto").attr('disabled', true);
                    $("#txtValOutPgto").css("background-color", "#F5F5F5");
                    $("#txtValOutPgto").val("");
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


            //____________________________________INICIO TAB FINANCEIRA_____________________________________
            $("#chkValorContratoCalc").click(function (evento) {
                if ($("#chkValorContratoCalc").attr("checked")) {

                    $("#ddlValorContratoCalc").removeAttr('disabled');
                    $("#ddlValorContratoCalc").css("background-color", "White");
                }
                else {

                    $("#ddlValorContratoCalc").attr('disabled', true);
                    $("#ddlValorContratoCalc").css("background-color", "#F5F5F5");
                }
            });

            $("#chkDataPrimeiraParcela").click(function (evento) {
                if ($("#chkDataPrimeiraParcela").attr("checked")) {

                    $("#txtDtPrimeiraParcela").removeAttr('disabled');
                    $("#txtDtPrimeiraParcela").css("background-color", "White");

                    //Libera a alteração do valor da primeira parcela apenas se o tipo de pagamento for à prazo
                    var e = document.getElementById("ddlTipoContrato");
                    var itSelec = e.options[e.selectedIndex].value;
                    if (itSelec == "P") {

                        $("#txtValorPrimParce").removeAttr('disabled');
                        $("#txtValorPrimParce").css("background-color", "White");
                    }
                }
                else {

                    //Desabilita os DropDownList's de Bandeira
                    $("#txtDtPrimeiraParcela").attr('disabled', true);
                    $("#txtDtPrimeiraParcela").css("background-color", "#F5F5F5");

                    $("#txtValorPrimParce").attr('disabled', true);
                    $("#txtValorPrimParce").css("background-color", "#F5F5F5");
                }
            });


            //=========================================== PARTE RESPONSÁVEL PELA ALTERNAÇÃO ENTRE ABAS E FLUXO DA PÁGINA =========================================

            //Aba de Material Coletivo
            $("#chkMatrColet").change(function () {
                if ($("#chkMatrColet").selected()) {
                    controleTab("tabMaterialColetivo", "chkMatrColet");
                }
            });

            //Aba de Mensalidades Escolares
            $("#chkMenEscAlu").change(function () {
                if ($("#chkMenEscAlu").selected()) {
                    controleTab("tabMenEsc", "chkMenEscAlu");
                }
            });

            //Aba de Restrição Alimentar
            $("#chkResAliAlu").change(function () {
                if ($("#chkResAliAlu").selected()) {
                    controleTab("tabResAliAdd", "chkResAliAlu");
                }
            });

            //Aba de Cuidados Especiais
            $("#chkCuiEspAlu").change(function () {
                if ($("#chkCuiEspAlu").selected()) {
                    controleTab("tabCuiEspAdd", "chkCuiEspAlu");
                }
            });

            //Aba de Documentos de Matrícula
            $("#chkDocMat").change(function () {
                if ($("#chkDocMat").selected()) {
                    controleTab("tabDocumentos", "chkDocMat");
                }
            });

            //Aba de Endereços do Aluno
            $("#chkEndAddAlu").change(function () {
                if ($("#chkEndAddAlu").selected()) {
                    controleTab("tabEndAdd", "chkEndAddAlu");
                }
            });

            //Aba de Telefones do Aluno
            $("#chkTelAddAlu").change(function () {
                if ($("#chkTelAddAlu").selected()) {
                    controleTab("tabTelAdd", "chkTelAddAlu");
                }
            });

            //Aba de Forma de Pagamento
            $("#chkPgtoAluno").change(function () {
                if ($("#chkPgtoAluno").selected()) {
                    controleTab("TabFormaPgto", "chkPgtoAluno");
                }
            });

            //Aba de Atividade Extra
            $("#chkRegAtiExt").change(function () {
                if ($("#chkRegAtiExt").selected()) {
                    controleTab("tabAtiExtAlu", "chkRegAtiExt");
                }
            });
        };
    </script>
</asp:Content>
