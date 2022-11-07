<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.MatriculaSimplificada.Cadastro"
    Title="Untitled Page" %>

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
            width: 500px;
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
            margin-left: 5px;
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
            margin-left: 5px;
            clear: both !important;
        }
        #divBotoes .lilnkBolCarne
        {
            background-color: #F0FFFF;
            border: 1px solid #D2DFD1;
            clear: none;
            margin-bottom: 4px;
            padding: 2px 3px 1px;
            margin-left: 5px;
            width: 58px;
        }
        #divBotoes .lilnkCarteira
        {
            background-color: #F0FFFF;
            border: 1px solid #D2DFD1;
            clear: none;
            margin-bottom: 4px;
            padding: 2px 3px 1px;
            margin-left: 5px;
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
            margin-left: 10px;
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
            top: 109px;
            width: 750px;
            height: 380px;
            padding: 7px 0 0 20px;
        }
        #tabTelAdd, #tabEndAdd, #tabResAliAdd, #tabAtiExtAlu, #tabMenEsc
        {
            position: fixed;
            right: 0;
            top: 109px;
            width: 748px;
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
        .ddlNire
        {
            width: 220px;
        }
    </style>
    <script type="text/javascript">

        if (navigator.userAgent.toLowerCase().match('chrome'))
            $("#ControleImagem .liControleImagemComp .lblProcurar").hide();
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
            <label for="ddlSituMatAluno" class="lblObrigatorio" title="Tipo de Matrícula">
                Tipo de Matrícula</label>
            <asp:DropDownList ID="ddlSituMatAluno" CssClass="ddlSituMatAluno" ToolTip="Selecione o Tipo de Matrícula"
                runat="server">
                <asp:ListItem Value="MR" Selected="true">Com Reserva</asp:ListItem>
                <asp:ListItem Value="MS">Sem Reserva</asp:ListItem>
                <asp:ListItem Value="X">Renovação</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="width: 460px; margin-right: 0px;">
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
                <li style="margin-top: 10px; margin-left: 20px; float: right;">
                    <div id="divBotoes">
                        <ul>
                            <li>
                                <div id="div4" class="bar">
                                    <div id="divBarraMatric" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
                                        <ul id="ulNavegacao" style="width: 39px;">
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
                                                <asp:LinkButton ID="lnkNovo" runat="server" OnClick="lnkNovo_Click">
                                                <img title="Abre o formulario para Criar um Novo Registro."
                                                    alt="Icone de Criar Novo Registro." 
                                                    src="/BarrasFerramentas/Icones/NovoRegistro.png" />
                                                </asp:LinkButton>
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
                                                <%-- <a href='<%= Request.Url.AbsoluteUri %>'>--%>
                                                <img title="Cancela a Pesquisa atual e limpa o Formulário de Parâmetros de Pesquisa."
                                                    alt="Icone de Cancelar Operacao Atual." src="/BarrasFerramentas/Icones/Cancelar_Desabilitado.png" />
                                                <%--  </a>--%>
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
                            <li id="lilnkEfetMatric" runat="server" title="Clique para Efetivar Matrícula" class="lilnkEfetMatric">
                                <asp:LinkButton ID="lnkEfetMatric" OnClick="lnkEfetMatric_Click" runat="server" Style="margin: 0 auto;"
                                    ToolTip="EFETIVAR MATRÍCULA">
                                    <img id="imgEfetiMatric" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_CheckSucess.png'
                                        alt="Icone Pesquisa" title="EFETIVAR MATRÍCULA" />
                                    <asp:Label runat="server" ID="lblEfetiMatric" Text="EFETIVAR" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li id="lilnkRecMatric" runat="server" title="Clique para Imprimir Contrato de Matrícula"
                                class="lilnkRecMatric">
                                <asp:LinkButton ID="lnkRecMatric" OnClick="lnkRecMatric_Click" Enabled="false" runat="server"
                                    Style="margin: 0 auto;" ToolTip="Imprimir Recibo/Protocolo de Matrícula">
                                    <img id="imgRecMatric" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                                        alt="Icone Pesquisa" title="RECIBO MATRIC" />
                                    <asp:Label runat="server" ID="lblRecibo" Text="CONTRATO" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li id="lilnkFichaMatric" runat="server" title="Clique para Imprimir a ficha de Pré-Matrícula"
                                class="lilnkBolCarne">
                                <asp:LinkButton ID="lnkFichaMatric" Enabled="false" runat="server" Style="margin: 0 auto;"
                                    ToolTip="Imprimir Ficha de Pré-Matrícula" OnClick="lnkFichaMatric_Click">
                                    <img id="img3" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                                        alt="Icone Pesquisa" title="FICHA DE MATRÍCULA" /><asp:Label runat="server" ID="Label21"
                                            Text="FICHA" Style="margin-left: 4px;"></asp:Label></asp:LinkButton>
                            </li>
                            <li id="lilnkBolCarne" runat="server" title="Clique para Imprimir Boleto de Mensalidades"
                                class="lilnkBolCarne">
                                <asp:LinkButton ID="lnkBolCarne" OnClick="lnkBolCarne_Click" Enabled="false" runat="server"
                                    Style="margin: 0 auto;" ToolTip="Imprimir Boleto de Mensalidades">
                                    <img id="imgBolCarne" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                                        alt="Icone Pesquisa" title="BOLETO/CARNÊ" />
                                    <asp:Label runat="server" ID="lblBoleto" Text="BOLETO" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li id="lilnkCarteira" runat="server" title="Clique para Imprimir a Carteira da Escola"
                                class="lilnkCarteira">
                                <asp:LinkButton ID="lnkCarteira" OnClick="lnkCarteira_Click" runat="server" Style="margin: 0 auto;"
                                    Enabled="false" ToolTip="Imprimir Carteira da Escola">
                                    <img id="img1" runat="server" class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico'
                                        alt="Icone Pesquisa" title="Carteira da Escola" />
                                    <asp:Label runat="server" ID="Label7" Text="CARTEIRA" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li id="li5" runat="server" title="Clique para Realizar o Pagamento de Mensalidade(s)"
                                class="lilnkRegPgto"><a id="lnkRealiPagto" runat="server" href="" style="cursor: pointer;">
                                    REG PGTO</a> </li>
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
                        <asp:Label runat="server" ID="lblbtnInfResp" CssClass="lblTitInf" Text="INFORMAÇÕES RESPONSÁVEL"></asp:Label>
                        <asp:Label runat="server" ID="lblSucInfResp" CssClass="lblSucInfResp" Visible="false"
                            Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                    </li>
                    <li class="liTitInf">
                        <label class="lblSubTitInf">
                            Digite ou atualize os dados do Responsável</label>
                    </li>
                    <li style="margin-left: 20px;"><span title="Número do CPF do Responsável">Nº CPF</span>
                        <asp:TextBox ID="txtCPFResp" CssClass="txtCPF" Style="width: 75px;" runat="server"
                            ToolTip="Informe o CPF do Responsável"></asp:TextBox>
                        <asp:HiddenField ID="hdfCPFRespRes" runat="server" />
                        <asp:HiddenField ID="hdCoRes" runat="server" />
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
                        <asp:DropDownList ID="ddlNoRespCPF" runat="server" ToolTip="Responsável" Width="230px"
                            OnSelectedIndexChanged="ddlNoRespCPF_OnSelectedIndexChanged" AutoPostBack="true"
                            Visible="false">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtNoRespCPF" CssClass="txtNoRespCPF" runat="server"></asp:TextBox>
                    </li>
                    <!-- FIM PARTE DAS INFORMAÇÕES DO RESPONSÁVEL -->
                    <!-- INICIO PARTE DAS INFORMAÇÕES DO ALUNO -->
                    <li style="margin-top: 2px;">
                        <img class="imgInfResp" src='/Library/IMG/Gestor_Numero2Vermelho.png' alt="Icone Pesquisa"
                            title="Ativa Informações do Aluno." />
                        <asp:Label runat="server" ID="Label2" CssClass="lblTitInf" Text="INFORMAÇÕES DO ALUNO"></asp:Label>
                        <asp:Label runat="server" ID="lblSucInfAlu" CssClass="lblSucInfAlu" Visible="false"
                            Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                    </li>
                    <li style="clear: both; margin-left: 20px;"><span title="Nº NIRE do Aluno">Nº NIRE</span>
                        <asp:TextBox ID="txtNumNIRE" CssClass="txtNire" runat="server" ToolTip="Informe o Nº NIRE do Aluno"></asp:TextBox>
                        <asp:HiddenField ID="hdCoAlu" runat="server" />
                    </li>
                    <li runat="server">
                        <asp:ImageButton ID="btnPesqNIRE" runat="server" OnClick="btnPesqNIRE_Click" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                            CssClass="btnPesqMat" CausesValidation="false" />
                    </li>
                    <li style="margin-left: 20px;">
                        <asp:TextBox ID="txtNoInfAluno" CssClass="txtNoInfAluno" Style="color: Black !important;"
                            runat="server" Enabled="False" Visible="false"></asp:TextBox>
                        <asp:DropDownList runat="server" ID="ddlNire" ToolTip="Informe o Nº NIRE do Aluno"
                            OnSelectedIndexChanged="ddlNire_SelectedIndexChanged" AutoPostBack="true" CssClass="ddlNire">
                        </asp:DropDownList>
                    </li>
                    <!-- FIM PARTE DAS INFORMAÇÕES DO ALUNO -->
                    <!-- INICIO PARTE DAS INFORMAÇÕES DA MATRÍCULAS -->
                    <li style="margin-top: 2px;">
                        <img class="imgInfResp" src='/Library/IMG/Gestor_Numero3Vermelho.png' alt="Icone Pesquisa"
                            title="Ativa Dados de Matrícula." />
                        <asp:Label runat="server" ID="Label5" CssClass="lblTitInf" Text="DADOS DE MATRÍCULA"></asp:Label>
                    </li>
                    <li style="margin-top: 3px; margin-left: 20px;"><span style="margin-right: 3px;"
                        title="Ano">Ano</span>
                        <asp:TextBox ID="txtAno" ValidationGroup="ModSerTur" MaxLength="4"
                            Width="26px" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAno"
                            ValidationGroup="ModSerTur" ErrorMessage="Ano deve ser informado" Display="None"></asp:RequiredFieldValidator>
                        <asp:Label runat="server" ID="lblSucDadosMatr" CssClass="lblSucDadosMatr" Visible="false"
                            Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                    </li>
                    <li>
                        <ul>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <li id="liModalidade" class="liLeft" style="margin-left: 20px; margin-top: -3px;
                                        clear: both;">
                                        <label for="ddlModalidade" title="Modalidade" class="lblObrigatorio">
                                            Modalidade de Ensino</label>
                                        <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade de Ensino" CssClass="campoModalidade"
                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlModalidade"
                                            ValidationGroup="ModSerTur" ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                    </li>
                                    <li id="liSerie" style="margin-top: -3px;">
                                        <label for="ddlSerieCurso" class="lblObrigatorio" title="Série/Curso">
                                            Série</label>
                                        <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" ToolTip="Selecione a Série/Curso"
                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="ddlSerieCurso"
                                            ValidationGroup="ModSerTur" ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdSerieCurso" runat="server" />
                                    </li>
                                    <li id="liTurma" style="margin-top: -2px; margin-left: 20px; clear: both;">
                                        <label for="ddlTurma" class="lblObrigatorio" title="Turma Matrícula">
                                            Turma Matrícula</label>
                                        <asp:DropDownList ID="ddlTurma" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged"
                                            AutoPostBack="true" ToolTip="Selecione a Turma de Matrícula" Width="83px" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="ddlTurma"
                                            ValidationGroup="ModSerTur" ErrorMessage="Turma deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                    </li>
                                    <li style="margin-top: -2px; margin-left: -1px; margin-right: 10px">
                                        <label for="txtTurno" title="Turno">
                                            Turno</label>
                                        <asp:TextBox ID="txtTurno" Enabled="false" CssClass="txtTurno" Width="66px" Style="padding-left: 1px;"
                                            runat="server"></asp:TextBox>
                                    </li>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <li id="li4" runat="server" title="Clique para Confirmar Modalidade, Série e Turma"
                                class="liBtnConfir" style="margin-left: -5px; margin-top: 8px">
                                <asp:LinkButton ID="lnkConfirModSerTur" OnClick="lnkConfirModSerTur_Click" ValidationGroup="ModSerTur"
                                    runat="server" ToolTip="Clique para Confirmar Modalidade, Série e Turma">
                                    <asp:Label runat="server" ID="Label20" Text="FINALIZAR"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </li>
                    <!-- FIM PARTE DAS INFORMAÇÕES DA MATRÍCULA -->
                    <!-- INICIO PARTE DAS INFORMAÇÕES DAS MENSALIDADES -->
                    <li style="margin-top: 2px;">
                        <img class="imgInfResp" src='/Library/IMG/Gestor_Numero4Preto.png' alt="Icone Pesquisa"
                            title="Ativa Mensalidades Escolares do Aluno." />
                        <asp:Label runat="server" ID="Label4" CssClass="lblTitInf" Text="MENSALIDADES ESCOLARES"></asp:Label>
                    </li>
                    <li class="liTitInf">
                        <label class="lblSubTitInf">
                            Confirme as informações dos itens abaixo:</label>
                    </li>
                    <li class="lichkUniforme" style="margin-left: 16px;">
                        <asp:CheckBox CssClass="chkMenEscAlu" Enabled="false" ClientIDMode="Static" ID="chkMenEscAlu"
                            runat="server" Text="Gerar Mensalidades Escolares" />
                        <asp:Label runat="server" ID="lnkSucMenEscAlu" ClientIDMode="Static" CssClass="lblSucInfAlu"
                            Visible="false" Style="color: Red; clear: none; font-weight: bold;" Text="OK!"></asp:Label>
                    </li>
                    <!-- FIM PARTE DAS INFORMAÇÕES DAS MENSALIDADES -->
                </ul>
            </div>
            <!-- =========================================================================================================================================================================== -->
            <!--                                                              FIM MENU LATERAL                                                                                              -->
            <!-- =========================================================================================================================================================================== -->
            <!-- =========================================================================================================================================================================== -->
            <!--                                                               TABELA DE MENSALIDADES                                                                                        -->
            <!-- =========================================================================================================================================================================== -->
            <div id="tabMenEsc" style="display: block;" clientidmode="Static" runat="server">
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
                        <asp:CheckBox ID="chkAtualiFinan" CssClass="chkLocais" AutoPostBack="true" Enabled="false"
                            OnCheckedChanged="chkAtualiFinan_CheckedChanged" runat="server" Text="Atualizar Financeiro"
                            ToolTip="Marque se deverá atualizar o financeiro" />
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
                                                <asp:DropDownList ID="ddlTipoContrato" OnSelectedIndexChanged="ddlTipoContrato_SelectedIndexChanged"
                                                    AutoPostBack="true" Width="60px" Enabled="false" ToolTip="Selecione o tipo do contrato"
                                                    runat="server">
                                                    <asp:ListItem Value="P">A Prazo</asp:ListItem>
                                                    <asp:ListItem Value="V">A Vista</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CheckBox CssClass="chkLocais" ID="chkIntegMensaSerie" OnCheckedChanged="chkIntegMensaSerie_CheckedChange"
                                                    AutoPostBack="true" TextAlign="Right" Enabled="false" runat="server" ToolTip="Mensalidade Valor Integral" />
                                                <span style="">Valor Integral</span>
                                            </div>
                                            <!-- Checkbox de alteração do valor de contrato -->
                                            <div>
                                                <asp:CheckBox ID="chkAlterValorContr" CssClass="chkLocais" runat="server" Text="Altera o valor de contrato?"
                                                    ToolTip="Marque se deverá gerar o total de parcelas do curso independente do ano."
                                                    OnCheckedChanged="chkAlterValorContr_CheckedChanged" AutoPostBack="true" />
                                                <asp:TextBox Enabled="false" ID="txtValorContratoCalc" CssClass="txtValorContratoCalc"
                                                    Style="text-align: right; margin-left: 4px;" runat="server" Width="50px" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="vgMontaGridMensa"
                                                    runat="server" ControlToValidate="txtValorContratoCalc" ErrorMessage="Valor de Contrato deve ser informado"
                                                    Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                            </div>
                                            <!-- Checkbox do valor do contrato proporcional -->
                                            <div>
                                                <asp:CheckBox ID="chkValorContratoCalc" Checked="false" runat="server" CssClass="chkLocais"
                                                    OnCheckedChanged="chkValorContratoCalc_CheckedChanged" ToolTip="Marque se o sistema deverá calcular o valor do contrato."
                                                    Text="Calcular valor de contrato?" AutoPostBack="true" />
                                                <asp:DropDownList ID="ddlValorContratoCalc" Width="125px" Enabled="false" Style="margin-left: 4px;"
                                                    ToolTip="Selecione o Nome da Bolsa" runat="server">
                                                    <asp:ListItem Value="P" Selected="true">Proporcional Meses</asp:ListItem>
                                                    <asp:ListItem Value="T">Total (Todos os meses)</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <!-- Checkbox de geração do total de parcelas -->
                                            <div>
                                                <asp:CheckBox ID="chkGeraTotalParce" CssClass="chkLocais" runat="server" Text="Altera o n° original de parcelas de cadastro?"
                                                    ToolTip="Marque se deverá alterar o n° original de parcelas cadastrado na série."
                                                    OnCheckedChanged="chkGeraTotalParce_CheckedChanged" AutoPostBack="true" />
                                                <asp:TextBox ID="txtQtdeParcelas" OnTextChanged="txtQtdeParcelas_TextChanged" AutoPostBack="false"
                                                    ToolTip="Informa a quantidade de parcelas da série/curso" Width="15px" CssClass="txtQtdeMesesDesctoMensa"
                                                    runat="server" Enabled="false">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="vgMontaGridMensa"
                                                    runat="server" ControlToValidate="txtQtdeParcelas" ErrorMessage="Quantidade de parcelas da série/curso deve ser informada"
                                                    Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                            </div>
                                            <!-- Checkbox da primeira parcela -->
                                            <div>
                                                <asp:CheckBox ID="chkDataPrimeiraParcela" Checked="false" CssClass="chkLocais" runat="server"
                                                    Text="Altera data/valor 1ª parcela?" OnCheckedChanged="chkDataPrimeiraParcela_CheckedChange"
                                                    ToolTip="Marque se deverá informar a data da primeira parcela." AutoPostBack="true" />
                                                <asp:TextBox ID="txtDtPrimeiraParcela" ToolTip="Informa a data de pagamento da primeira parcela."
                                                    CssClass="txtPeriodoIniDesconto campoData" runat="server" Enabled="false">
                                                </asp:TextBox>
                                                <span>/ R$</span>
                                                <asp:TextBox ID="txtValorPrimParce" CssClass="txtValorPrimParce" Width="48px" Style="text-align: right;"
                                                    ToolTip="Informe o valor da primeira parcela" runat="server" Enabled="false">
                                                </asp:TextBox>
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
                                                    <asp:TextBox ID="txtValorDescto" CssClass="txtDescontoAluno" Width="50px" ToolTip="Informe o valor do Desconto"
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
                                                                <asp:TextBox ID="txtDesctoMensa" CssClass="txtDesctoMensa" runat="server">
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
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                            <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                            <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                            <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                            <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                            <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                            <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                            <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                            <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                            <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                            <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                            <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                            <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                            <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                            <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                            <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                            <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </li>
                            <li runat="server" id="liBtnGrdFinan" class="liBtnGrdFinan" style="margin-left: 318px;
                                margin-top: 7px; margin-right: 10px;">
                                <asp:LinkButton ID="lnkMontaGridMensa" Enabled="false" OnClick="lnkMontaGridMensa_Click"
                                    ValidationGroup="vgMontaGridMensa" runat="server" Style="margin: 0 auto;" ToolTip="Montar Grid Financeira">
                                    <asp:Label runat="server" ID="Label6" ForeColor="GhostWhite" Text="GERAR GRID FINANCEIRA"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </li>
                    <li class="labelInLine" style="width: 681px; margin-left: 33px; margin-top: -3px">
                        <div id="divMensaAluno" runat="server" style="height: 200px; border: 1px solid #CCCCCC;
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
            <div id="divLoadShowAlunos" style="display: none; height: 305px !important;" />
            <div id="divLoadShowResponsaveis" style="display: none; height: 315px !important;" />
        </li>
    </ul>
    <script type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".txtNumeroResp").mask("?99999");
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
            $(".txtNire").mask("?999999999");
            $(".txtNireAluno").mask("?999999999");
            $(".txtMesAnoTrabResp").mask("99/9999");
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtNisAluno").mask("?999999999999999");
            $(".txtQtdeCEA").mask("?9999");
            $(".txtAno").mask("9999");
            $(".txtQtdeMatEsc").mask("?999");
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtQtdMenoresResp").mask("?99");
            $(".txtNumeroEmp").mask("?99999");
            $(".txtQtdeSolic").mask("?99");
            $(".txtMesIniDesconto").mask("?99");
            $(".txtCPF").mask("999.999.999-99");

            $(".lnkPesRes").click(function () {
                if ($(".lblSucDadosMatr").is(":visible") == false) {
                    $("#divLoadShowReservas").load("../../../../../Componentes/ListarReservasMat.aspx", function () {
                        $("#divLoadShowReservas #frmListarReservasMat").attr("action", "../../../../../Componentes/ListarReservasMat.aspx");
                    });

                    $("#divLoadShowReservas").dialog({ title: "RESERVAS DE MATRÍCULA", modal: true, width: "600px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
                }
            });


            $(".lnkPesNIRE").click(function () {
                if ($(".lblSucDadosMatr").is(":visible") == false) {
                    $("#divLoadShowAlunos").load("../../../../../Componentes/ListarAlunos.aspx", function () {
                        $("#divLoadShowAlunos #frmListarAlunos").attr("action", "../../../../../Componentes/ListarAlunos.aspx");
                    });

                    $("#divLoadShowAlunos").dialog({ title: "LISTA DE ALUNOS", modal: true, width: "970px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
                }
            });

            $(".lnkPesResp").click(function () {
                if ($(".lblSucDadosMatr").is(":visible") == false) {
                    $("#divLoadShowResponsaveis").load("../../../../../Componentes/ListarResponsaveis.aspx", function () {
                        $("#divLoadShowResponsaveis #frmListarResponsaveis").attr("action", "../../../../../Componentes/ListarResponsaveis.aspx");
                    });

                    $("#divLoadShowResponsaveis").dialog({ title: "LISTA DE RESPONSÁVEIS", modal: true, width: "690px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
                }
            });
        });
    </script>
</asp:Content>
