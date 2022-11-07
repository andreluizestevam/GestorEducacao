<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3220_CtrlCentralRegulacao._3221_Homologacao.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .grdLinhaCenter
        {
            text-align: center !important;
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
            border-bottom: solid 1px #CCCCCC;
            width: 100%;
            padding-bottom: 5px;
            margin-bottom: -15px;
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
        .txtComplementoResp
        {
            width: 217px;
        }
        .ddlCidadeResp
        {
            width: 165px;
        }
        .ddlBairroResp
        {
            width: 145px;
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
        .txtNireAluno
        {
            width: 66px;
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
            top: 65px;
            width: 180px;
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
        .divAtendPendentes
        {
            border: 1px solid #CCCCCC;
            width: 810px;
            height: 180px;
            overflow-y: scroll;
            margin-left: 5px;
            margin-bottom: 13px;
            <%--display: none;--%>
        }
        .divDetalhePendencia
        {
            border: 1px solid #CCCCCC;
            width: 810px;
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
        #tabTelAdd, #tabEndAdd, #tabResAliAdd, #tabAtiExtAlu, #tabMenEsc, #tabResMedicamentos, #tabResConsultas, #tabExamHist, #tabConsHist, #tabMedcHist, #tabDiagHist, #tabServAmbuHist, #tabAtestMedcHist, #tabImgMedcHist, #tabSelPacien, #tabhistReceiMedic
        {
            position: fixed;
            right: 0;
            top: 90px;
            width: 821px;
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
            text-align: center;
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
        .ulEnvioSMSForm li
        {
            margin-left:10px;
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
        
        /*==================== CSS do Registro de Ocorrências =========================*/
         #divEnvioSMSContent  
        {
            margin:auto;
            width:340px;
        }        
        #divEnvioSMSContainer #divAlterarSenhaFormButtons
        {
            margin-top: 5px;
        }
        #divEnvioSMSContainer #divRodapeSMS
        {
            margin-top: 20px;
            float: right;
        }
        #divEnvioSMSContainer #imgLogoGestor
        {
            width: 120px;
            height: 30px;
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
        .ErrorMessageSMS
        {
            background: url(/Library/IMG/Gestor_ImgError.png) no-repeat scroll center 10px;
            font-size: 15px;
            font-weight: bold;
            margin: 25% auto 13% auto;
            padding: 35px 10px 10px;
            text-align: center;
            width: 220px;   
        }
        #divLoadingSMS  
        {
        	margin: 100px 70px auto 120px; 
            width: 90px;
        }
        .txtMsg{ width: 316px; height: 50px;} 
        .liEmissor { clear: both;}
        .liDestinatarios, .litxtDestinatarios { clear: both; margin-top: 2px;}
        .liTelefoneSMS { clear: none; margin-top: 2px; margin-left: 5px;}
        .liTpContatoSMS { clear:both; margin-top: 2px;}
        .liUnidadeSMS { width:245px; margin-top: 2px; clear:none; margin-left: 5px;}
        .liMsg { clear: both; margin-top: 0px; }        
        #ulEnvioSMSForm li
        {
        	float: left;
        	margin-left:5px;
        }
        #ulEnvioSMSForm input
        {
            height:13px;
        }
        #lblDe, #lblPara, #lblTitMsg
        {
        	color: Black;
        	font-weight: bold;
        }
        #txtEmissor { width: 210px; }
        .liBtnEnviar { clear: both; margin-left: 90px; margin-top: 5px;}
        .liPara { clear: both; }
        .ddlDestinatarios
        {
        	width: 250px;
        }
        .ddlUnidadeEscolarSMS
        {
        	width: 243px;
        }
        #liSucessMsg
        { 
        	width: 210px;
        	margin-top: 5px;
            color: #DF6B0D;
            font-weight: bold;
        }
        #spaCtCarac { color: #FF6347; }
        .vsEnvioSMS { margin-top: -15px; color:#B22222; clear: both; margin-left: -90px; width: 265px; }
        .btnEnviarSMS{ margin-left: 186px; margin-top: -20px; }
        #liHelpTxtSMS
        {
            margin-top: 10px;
            width: 210px;
            color: #DF6B0D;
            font-weight: bold;
            clear: both;
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
        .ddlTpContatoSMS
        {
        	width: 90px;
        }
        .campoTelefoneSMS
        {
        	width: 80px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                            <%--<asp:UpdatePanel ID="updTopo" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                            <asp:HiddenField runat="server" ID="hidCoProfSaud" />
                        </ul>
                    </div>
                </li>
            </ul>
        </li>
        <li class="liSeparador" style="margin-top: -10px"></li>
        <li>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               INICIO MENU LATERAL                                                                                           -->
            <!-- =========================================================================================================================================================================== -->
            <div id="divMenuLateral">
                <asp:UpdatePanel ID="updMenuLateral" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
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
                                    <li style="margin-left: 3px;">
                                        <asp:TextBox Style="background-color: #8FBC8F; color: White; font-size: 9px !important;
                                            vertical-align: middle; font-weight: bold; height: 9px" ReadOnly="true" runat="server"
                                            ID="txtPaciente" Width="150px"></asp:TextBox>
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
                                <asp:Label runat="server" ID="Label37" CssClass="lblTitInf" Text="HISTÓRICO DO PACIENTE"></asp:Label>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                              FIM MENU LATERAL                                                                                              -->
            <!-- =========================================================================================================================================================================== -->
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
                <asp:HiddenField runat="server" ID="hidAbaAberta" />
                <ul id="ul26" class="ulDados2">
                    <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                        background-color: #FDF5E6;">
                        <label style="font-size: 1.1em; font-family: Tahoma;">
                            CENTRAL DE REGULAÇÃO - AVALIAÇÃO</label>
                        <asp:HiddenField runat="server" ID="hidCoPac" />
                    </li>
                    <li style="clear: both; margin-top: -10px;">
                        <%--<div class="divPaciEnca" style="display:none; margin-left: 5px" ClientIDMode="static">--%>
                        <div id="divPaciEnca" class="divAtendPendentes" runat="server" clientidmode="static"
                            style="height: 229px">
                            <div style="width: 100%; text-align: center; height: 17px; background-color: #add8e6;">
                                <div style="float: left;">
                                    <asp:Label runat="server" Style="font-size: 1.1em; font-family: Tahoma; vertical-align: middle;
                                        margin-left: 4px !important;">
                                    GRADE DE SOLICITAÇÕES MÉDICAS</asp:Label>
                                </div>
                                <div style="float: right; margin: 1px 4px 0 3px;">
                                    <asp:Label runat="server" ID="lblo">Ordenado por </asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlOrdeAtendPend" OnSelectedIndexChanged="ddlOrdeAtendPend_OnSelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Text="Class. Risco" Value="C" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Paciente" Value="P"></asp:ListItem>
                                        <asp:ListItem Text="Unidade" Value="U"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick">
                            </asp:Timer>
                            <asp:UpdatePanel ID="updAtendPenden" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger EventName="Tick" ControlID="Timer1" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:HiddenField runat="server" ID="hidIdCentrRegul" />
                                    <asp:HiddenField runat="server" ID="hidItemSelec" />
                                    <asp:GridView ID="grdAtendimPendentes" CssClass="grdBusca" runat="server" Style="width: 100%;"
                                        AutoGenerateColumns="false" OnRowDataBound="grdAtendimPendentes_OnRowDataBound"
                                        ToolTip="Grid de Atendimentos com encaminhamentos pendentes de aprovação">
                                        <RowStyle CssClass="rowStyle" />
                                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                        <EmptyDataTemplate>
                                            Nenhum Atendimento Médico com Encaminhamento em aberto<br />
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="CK">
                                                <ItemStyle Width="15px" CssClass="grdLinhaCenter" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="chkSelecGridDetalhada" Style="margin: 0px !important;"
                                                        OnCheckedChanged="chkSelecGridDetalhada_OnCheckedChanged" AutoPostBack="true" />
                                                    <asp:HiddenField runat="server" ID="hidCoAtend" Value='<%# Eval("ID_ATEND_MEDIC") %>' />
                                                    <asp:HiddenField runat="server" ID="hidCoCentrRegul" Value='<%# Eval("ID_CENTR_REGUL") %>' />
                                                    <asp:HiddenField runat="server" ID="hidFlUso" Value='<%# Eval("FL_USO") %>' />
                                                    <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# Eval("CO_ALU") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UNID">
                                                <ItemStyle HorizontalAlign="Left" Width="40px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NO_PAC_V" HeaderText="PACIENTE">
                                                <ItemStyle HorizontalAlign="Left" Width="158px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CO_SEXO" HeaderText="SX">
                                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NU_IDADE" HeaderText="ID">
                                                <ItemStyle HorizontalAlign="Center" Width="17px" />
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
                                            <asp:BoundField DataField="CO_ATEND_V" HeaderText="Nº ATENDIMENTO">
                                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DT_ATEND" HeaderText="DT/HR ATEND">
                                                <ItemStyle HorizontalAlign="Left" Width="75px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NO_COL" HeaderText="PROF. SAÚDE">
                                                <ItemStyle HorizontalAlign="Left" Width="170px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="EXA">
                                                <ItemStyle Width="25px" CssClass="grdLinhaCenter" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label Text="NÃO" Visible='<%# bind("SW_NAO_PRETO_EX") %>' ID="lblg1e1" runat="server"></asp:Label>
                                                    <asp:Label ID="lblg1e2" Text="SIM" Visible='<%# bind("SW_SIM_PRETO_EX") %>' runat="server"></asp:Label>
                                                    <asp:Label ID="lblg1e3" Style="color: Blue" Text="SIM" Visible='<%# bind("SW_SIM_AZUL_EX") %>'
                                                        runat="server"></asp:Label>
                                                    <asp:Label ID="lblg1e4" Style="color: Red" Text="SIM" Visible='<%# bind("SW_SIM_VERMELHO_EX") %>'
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="SVA">
                                                <ItemStyle Width="25px" CssClass="grdLinhaCenter" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblg1s1" Text="NÃO" Visible='<%# bind("SW_NAO_PRETO_SA") %>' runat="server"></asp:Label>
                                                    <asp:Label ID="lblg1s2" Text="SIM" Visible='<%# bind("SW_SIM_PRETO_SA") %>' runat="server"></asp:Label>
                                                    <asp:Label ID="lblg1s3" Style="color: Blue" Text="SIM" Visible='<%# bind("SW_SIM_AZUL_SA") %>'
                                                        runat="server"></asp:Label>
                                                    <asp:Label ID="lblg1s4" Style="color: Red" Text="SIM" Visible='<%# bind("SW_SIM_VERMELHO_SA") %>'
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RME">
                                                <ItemStyle Width="25px" CssClass="grdLinhaCenter" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblg1s12" Text="NÃO" Visible="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="EME">
                                                <ItemStyle Width="25px" CssClass="grdLinhaCenter" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblg1s13" Text="NÃO" Visible="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="EIN">
                                                <ItemStyle Width="25px" CssClass="grdLinhaCenter" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblg1s14" Text="NÃO" Visible="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="AÇÃO">
                                        <ItemStyle Width="15px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlAcao" Width="70px" OnSelectedIndexChanged="ddlAcao_OnSelectedIndexChange"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                <asp:ListItem Value="EX" Text="Exame"></asp:ListItem>
                                                <asp:ListItem Value="SA" Text="Serviços Ambulatoriais"></asp:ListItem>
                                                <asp:ListItem Value="RM" Text="Reserva de Medicamentos"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </li>
                    <asp:UpdatePanel runat="server" ID="updItensPendentes" UpdateMode="Conditional">
                        <ContentTemplate>
                            <li style="clear: both; margin-top: -7px;" runat="server" id="liItensPendentes" visible="false">
                                <div class="divDetalhePendencia" id="divConsul" clientidmode="Static">
                                    <div style="width: 100%; margin-bottom: 1px; text-align: center; background-color: #8FBC8F;
                                        height: 17px;">
                                        <div style="float: left; margin-left: 4px;">
                                            <asp:Label runat="server" ID="lblTituSegunGrid" Style="font-size: 1.1em; font-family: Tahoma;
                                                vertical-align: middle;">RELAÇÃO DE ITENS SOLICITADOS AO USUÁRIO DE SAÚDE</asp:Label>
                                        </div>
                                        <div style="float: right; margin: 1px 4px 0 3px;">
                                            <asp:Label runat="server" ID="Label9" Text="Tipo"></asp:Label>
                                            <asp:DropDownList runat="server" ID="ddlTipoItem" OnSelectedIndexChanged="ddlTipoItem_OnSelectedIndexChanged"
                                                AutoPostBack="true" Style="margin-right: 8px;">
                                                <asp:ListItem Text="Todos" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Exames" Value="EX"></asp:ListItem>
                                                <asp:ListItem Text="Serviços Ambulatoriais" Value="SA"></asp:ListItem>
                                                <asp:ListItem Text="Reserva de Medicamentos" Value="RM"></asp:ListItem>
                                                <asp:ListItem Text="Encaminhamentos Médicos" Value="EM"></asp:ListItem>
                                                <asp:ListItem Text="Encaminhamentos Internação" Value="EI"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label runat="server" ID="Label8" Text="Ordenado por"></asp:Label>
                                            <asp:DropDownList runat="server" ID="ddlOrdDetalhePendencia" OnSelectedIndexChanged="ddlOrdDetalhePendencia_OnSelectedIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Text="Registro" Value="R" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Código" Value="C"></asp:ListItem>
                                                <asp:ListItem Text="Nome" Value="N"></asp:ListItem>
                                                <asp:ListItem Text="Tipo" Value="T"></asp:ListItem>
                                                <asp:ListItem Text="Situação" Value="S"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:GridView ID="grdDetalhePendencia" CssClass="grdBusca" runat="server" Style="width: 100%;"
                                        AutoGenerateColumns="false" ToolTip="Seleção do paciente com consulta marcada para o qual será feito o atendimento (Registros em vermelho estão atrasados)"
                                        OnRowDataBound="grdDetalhePendencia_OnRowDataBound">
                                        <RowStyle CssClass="rowStyle" />
                                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                        <EmptyDataTemplate>
                                            Nenhum Item pendente de Aprovação<br />
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="NO_TIPO_ITEM" HeaderText="TIPO">
                                                <ItemStyle HorizontalAlign="Center" Width="18px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CO_CAD_ITEM" HeaderText="COD ITEM ">
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NO_ITEM" HeaderText="NOME DO ITEM">
                                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CO_PROTOCOLO" HeaderText="Nº REGISTRO">
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DATA / SITUAÇÃO">
                                                <ItemStyle Width="104px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDataApr" runat="server" Text='<%# bind("DT_ALTER_V") %>' ToolTip="Data de registro da situação"></asp:Label>
                                                    <asp:Label ID="Label1" Style="color: Red" Text="NÃO" Visible='<%# bind("SW_NAO_VERMELHO") %>'
                                                        runat="server" ToolTip="Não Autorizado"></asp:Label>
                                                    <asp:Label ID="Label6" Style="color: Green" Text="SIM" Visible='<%# bind("SW_SIM_VERDE") %>'
                                                        runat="server" ToolTip="Autorizado"></asp:Label>
                                                    <asp:Label ID="Label7" Style="color: Blue" Text="ANA" Visible='<%# bind("SW_ANALISE_AZUL") %>'
                                                        runat="server" ToolTip="Em Análise"></asp:Label>
                                                    <asp:Label ID="Label10" Style="color: #FF7619" Text="PEN" Visible='<%# bind("SW_PENDENTE_ABOBORA") %>'
                                                        runat="server" ToolTip="Pendente"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="APROVACAO">
                                                <ItemStyle Width="75px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:HiddenField runat="server" ID="hidCoStatus" Value='<%# bind("CO_STATUS") %>' />
                                                    <asp:HiddenField runat="server" ID="hidCoAtend" Value='<%# bind("CO_ATEND_MEDIC") %>' />
                                                    <asp:HiddenField runat="server" ID="hidIdCentrRegu" Value='<%# bind("ID_CENTR_REGUL") %>' />
                                                    <asp:HiddenField runat="server" ID="hidNoItem" Value='<%# bind("NO_ITEM") %>' />
                                                    <asp:HiddenField runat="server" ID="hidIdItemCentrRegul" Value='<%# bind("ID_ITEM_CENTR_REGUL") %>' />
                                                    <asp:DropDownList runat="server" ID="ddlAprovacao" Width="75px" ToolTip="Situação da solicitação">
                                                        <asp:ListItem Value="A" Text="Em Análise"></asp:ListItem>
                                                        <asp:ListItem Value="S" Text="Sim"></asp:ListItem>
                                                        <asp:ListItem Value="N" Text="Não"></asp:ListItem>
                                                        <asp:ListItem Value="P" Text="Pendente"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OBSERVAÇÃO">
                                                <ItemStyle Width="200px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtObser" TextMode="MultiLine" Width="100%" Height="100%"
                                                        MaxLength="200" ToolTip="Observação sobre a alteração do status"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RO">
                                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ID="imgReg" ImageUrl="/Library/IMG/Gestor_CheckSucess.png"
                                                        ToolTip="Registro de Ocorrência do item solicitado" OnClick="imgReg_OnClick" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div style="float: left; margin: -7px 0 0 5px;">
                                    <label style="font-size: xx-small">
                                        Legenda: EXA (Exame) - SRV (Serv. Ambulatoriais) - RME (Reser. Medicamentos) - EME
                                        (Encam. Médico) - EIN (Encam. Internação) - OUT (Outros) - SX (Sexo) - ID (Idade)</label>
                                </div>
                                <div style="float: right; margin-top: -17px !important;">
                                    <asp:LinkButton ID="lnkFinaGrid" runat="server" class="liBtnAddA " Style="float: right"
                                        OnClick="lnkFinaGrid_OnClick">
                                        <asp:Label runat="server" ID="Label41" Text="FINALIZAR" Style="margin-left: 4px;"></asp:Label>
                                    </asp:LinkButton>
                                </div>
                            </li>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ul>
            </div>
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                          TERMINO DA TELA DE SELEÇÃO DO PACIENTE
<!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE HISTÓRICO DE EXAMES                                                                                   -->
            <!-- =========================================================================================================================================================================== -->
            <asp:UpdatePanel runat="server" ID="updExamHist" UpdateMode="Conditional">
                <ContentTemplate>
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE HISTÓRICO DE EXAMES                                                                                   -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE HISTÓRICO DE CONSULTAS                                                                                -->
            <!-- =========================================================================================================================================================================== -->
            <asp:UpdatePanel runat="server" ID="updConsHist" UpdateMode="Conditional">
                <ContentTemplate>
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
                </ContentTemplate>
            </asp:UpdatePanel>
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
            <asp:UpdatePanel runat="server" ID="updDiagHist" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="tabDiagHist" runat="server" clientidmode="static" style="display: none;">
                        <ul id="ul21" class="ulDados2">
                            <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: -30px;
                                background-color: #FDF5E6;">
                                <label style="font-size: 1.1em; font-family: Tahoma;">
                                    HISTÓRICO DE DIAGNÓSTICOS</label>
                            </li>
                            <li runat="server" id="liGuardInfos" visible="false">
                                <asp:TextBox runat="server" ID="txtIdCentrRegu"></asp:TextBox>
                                <asp:TextBox runat="server" ID="txtDeOcorrencia" ClientIDMode="Static"></asp:TextBox>
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE HISTÓRICO DE DIAGNÓSTICOS                                                                             -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE HISTÓRICO DE SERV. AMBULATORIAL                                                                       -->
            <!-- =========================================================================================================================================================================== -->
            <asp:UpdatePanel runat="server" ID="updServAmbuHist" UpdateMode="Conditional">
                <ContentTemplate>
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE HISTÓRICO DE SERV. AMBULATORIAL                                                                       -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TELA DE HISTÓRICO DE ATESTADOS MÉDICOS                                                                        -->
            <!-- =========================================================================================================================================================================== -->
            <asp:UpdatePanel runat="server" ID="updAtestMedcHist" UpdateMode="Conditional">
                <ContentTemplate>
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                           FIM TELA DE HISTÓRICO DE ATESTADOS MÉDICOS                                                                        -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                            TELA DE HISTÓRICO DE RECEITAS MÉDICAS                                                                        -->
            <!-- =========================================================================================================================================================================== -->
            <asp:UpdatePanel runat="server" ID="updhistReceiMedic" UpdateMode="Conditional">
                <ContentTemplate>
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
                </ContentTemplate>
            </asp:UpdatePanel>
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
            <asp:UpdatePanel runat="server" ID="updModal" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="divLoadRegisOcorr" style="display: none; height: 385px !important;">
                        <div id="divEnvioSMSContainer">
                            <div id="divEnvioSMSContent" clientidmode="Static" runat="server" style="display: inline;">
                                <asp:HiddenField runat="server" ID="hidIdCentralRegu" />
                                <asp:HiddenField runat="server" ID="hidIdItemCentralRegu" />
                                <asp:HiddenField runat="server" ID="hidCoAtend" />
                                <ul id="ulEnvioSMSForm">
                                    <li>
                                        <label>
                                            Nº Atendimento</label>
                                        <asp:TextBox runat="server" ID="txtNuAtendMod" Width="95px" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Data/Hr Atendimento</label>
                                        <asp:TextBox runat="server" Width="53px" ID="txtdtAtenMod" Enabled="false"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="txtHrAtenMod" Width="30px" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 108px; clear: none">
                                        <label>
                                            Class. Risco</label>
                                        <asp:TextBox runat="server" ID="txtClasRiscMod" Width="95px" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li style="clear: both">
                                        <label>
                                            Médico Solicitante</label>
                                        <asp:TextBox runat="server" ID="txtCRMMedSoliMod" Width="80px" Enabled="false"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="txtNomMedSoliMod" Width="220px" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Unidade</label>
                                        <asp:TextBox runat="server" ID="txtSglUnidSoliMod" Width="40px" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Local</label>
                                        <asp:TextBox runat="server" ID="txtLocalMedicSoliMod" Width="40px" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li style="clear: both">
                                        <label>
                                            Paciente</label>
                                        <asp:TextBox runat="server" ID="txtNisPaciMod" Width="50px" Enabled="false"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="txtNoPacMod" Width="250px" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Sexo</label>
                                        <asp:TextBox runat="server" ID="txtSexPacMod" Width="40px" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Idade</label>
                                        <asp:TextBox runat="server" ID="txtIdaPacMod" Width="40px" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Nº Reg. Item</label>
                                        <asp:TextBox runat="server" ID="txtRegisItemMod" Width="85px" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Tipo Item</label>
                                        <asp:DropDownList ID="ddlTipoItemMod" runat="server" ToolTip="Tipo da Ocorrência Contato"
                                            Enabled="false" Width="75px">
                                            <asp:ListItem Value="" Text=""></asp:ListItem>
                                            <asp:ListItem Value="A" Text="Em Análise"></asp:ListItem>
                                            <asp:ListItem Value="S" Text="Sim"></asp:ListItem>
                                            <asp:ListItem Value="N" Text="Não"></asp:ListItem>
                                            <asp:ListItem Value="P" Text="Pendente"></asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <label>
                                            Nome do Item</label>
                                        <asp:TextBox runat="server" ID="txtNomeItemMod" Width="227px" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li style="clear: both">
                                        <label style="font-weight: bold">
                                            Descrição Ocorrência</label>
                                        <asp:TextBox Style="clear: both; width: 399px; margin-bottom: 5px;" ID="txtDeOcorrMod"
                                            CssClass="txtMsg" ToolTip="Digite a descrição da ocorrência." MaxLength="200"
                                            runat="server" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
                                    </li>
                                    <li style="clear: both">
                                        <label>
                                            Médico Análise</label>
                                        <asp:TextBox runat="server" ID="txtCRMMedAnaMod" Width="80px" Enabled="false"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="txtNomMedAnaMod" Width="220px" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Unidade</label>
                                        <asp:TextBox runat="server" ID="txtUnidAnaMod" Width="40px" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Local</label>
                                        <asp:TextBox runat="server" ID="txtLocAnaMod" Width="40px" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li style="clear: both">
                                        <label>
                                            Registrante</label>
                                        <asp:TextBox runat="server" Enabled="false" ID="txtRegistrante" Width="240px" />
                                    </li>
                                    <li>
                                        <label>
                                            Unidade</label>
                                        <asp:TextBox runat="server" ID="txtUniRegisMod" ToolTip="Unidade de Registro de Ocorrência"
                                            Enabled="false" Width="40px"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 22px;">
                                        <label>
                                            Data/Hr Ocorrência</label>
                                        <asp:TextBox runat="server" Width="53px" ID="txtDataOcoMod" Enabled="false"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="txtHrOcoMod" Width="30px" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li style="clear: both; margin: 10px 0 10px -150px;">
                                        <asp:LinkButton ID="lnkRegisOcorrencia" runat="server" class="liBtnAddA" Style="float: right;
                                            width: 126px" OnClick="lnkRegisOcorrencia_OnClick">
                                            <asp:Label runat="server" ID="Label11" Text="REGISTRAR OCORRÊNCIA" Style="margin-left: 4px;"></asp:Label>
                                        </asp:LinkButton>
                                    </li>
                                    <li style="clear: both">
                                        <p id="pAcesso" class="pAcesso">
                                            Digite a descrição e tecle em SALVAR para registrar a Ocorrência.</p>
                                        <p id="pFechar" class="pFechar">
                                            Clique no X para fechar a janela.</p>
                                    </li>
                                </ul>
                                <div id="divRodapeSMS">
                                    <img id="imgLogoGestor" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
                                </div>
                            </div>
                            <div id="divSuccessoMessage" runat="server" class="successMessageSMS" visible="false">
                                <asp:Label ID="lblMsg" runat="server" Visible="false" />
                                <asp:Label Style="color: #B22222 !important; display: block;" Visible="false" ID="lblMsgAviso"
                                    runat="server" />
                            </div>
                            <div id="divErrorMessage" runat="server" class="ErrorMessageSMS" visible="false">
                                <asp:Label ID="lblError" runat="server" />
                                <asp:Label Style="color: #B22222 !important; display: block;" ID="lblErrorAviso"
                                    runat="server" />
                            </div>
                            <div id="divLoadingSMS" clientidmode="Static" runat="server" style="display: none">
                                <img alt="icone carregando" title="Aguarde enquanto a página carrega." src="/Library/IMG/Gestor_Carregando.gif" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
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
            $("#chkExmPaci").selected(false)
            $("#chkConsPaci").selected(false)
            $("#chkHistReceiMedic").selected(false)
            $("#chkMedicPaci").selected(false)
            $("#chkDiagPaci").selected(false)
            $("#chkServAmbPaci").selected(false)
            $("#chkAtestMedcPaci").selected(false);
            $("#chkImgAtendPaci").selected(false);

            $("#" + idChk).selected(true)
        }

        function sucess() {
        }

//        function callToAjax() {
//            alert("entrou no ajax");
//            $.ajax({
//                type: "POST",
//                url: "GSAUD/3000_ControleInformacoesUsuario/3200_ControleAtendimentoUsuario/3220_CtrlCentralRegulacao/3221_Homologacao/Cadastro.aspx/ArmazenaDescOcorrencia",
//                data: '{de: "$(#txtDeOcorrMod).val()"}',
//                contentType: "application/json: charset=utf-8",
//                dataType: "json",
//                success: OnSuccess,
//                failure: function (response) {
//                    alert("Não Funcionou");
//                }
//            });
//        }

//        function OnSuccess(response) {
//            alert("Funcionou");
//        }

        function JavscriptAtend() {

            $(".txtNire").mask("?999999999");
            $(".txtNireAluno").mask("?999999999");
            $(".campoGrau").mask("99,9");
            $(".campoAltu").mask("9,99");

            $(".campoData").datepicker();
            $(".campoData").mask("99/99/9999");

            // Seleção do Paciente
            $("#chkSelPacien").change(function () {
                if ($("#chkSelPacien").selected()) {
                    controleTab("tabSelPacien", "chkSelPacien");
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

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            JavscriptAtend();
        });

        function AbreModal() {
            $('#divLoadRegisOcorr').dialog({ autoopen: false, modal: true, width: 432, height: 390, resizable: false, title: "REGISTRO DE OCORRÊNCIA - CENTRAL DE REGULAÇÃO",
//                open: function () { $('#divLoadRegisOcorr').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        //        function AbreModal() {
        //            $('#divLoadRegisOcorr').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "REGISTRO DE OCORRÊNCIA - CENTRAL DE REGULAÇÃO",
        //                open: function () { $('#divLoadRegisOcorr').show(); }

        //            });
        //        }

    </script>
</asp:Content>
