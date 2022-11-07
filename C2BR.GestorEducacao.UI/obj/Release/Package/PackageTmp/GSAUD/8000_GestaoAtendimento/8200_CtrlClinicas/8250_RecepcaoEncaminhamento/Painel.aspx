<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Painel.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8250_RecepcaoEncaminhamento.Painel" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">        
        .liFotoColab
        {
            float: left !important;
            margin-right: 10px !important;
            border: 0 none;
        }
        .liBtnGrdFinan
        {
            margin-top: 10px;
            margin-left: 2px;
            padding: 4px 3px 3px;
            background-color: #EE9572;
            border: 1px solid #8B8989;
        }
        .fixedHeader
        {
            position:absolute;
            font-weight:bold;
        }
        /*--> CSS DADOS */
        .fldFotoColab
        {
            border: none;
            width: 90px;
            height: 108px;
        }
                .lblSubInfos
        {
            color: Orange;
            font-size: 8px;
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
       <%-- #divBarraMatric ul li
        {
            display: inline;
            margin-left: -2px;
        }--%>
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
            top: 294px;
            width: 184px;
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
        
        #tabSelAgenda, #tabInfosCadas, #tabSelAvaliador, #tabRegisItens, #tabSelInfosResp
        {
            position: fixed;
            top: 391px;
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
        .chk label
        {
            display:inline;
            margin-left:-4px
        }
          .ulDadosLog li
        {
            float: left;
            margin-left: 10px;
        }
        .liBtnConfirm
        {
            margin-top: 10px;
            margin-left: 2px;
            padding: 4px 3px 3px;
            background-color: #EE9572;
            border: 1px solid #8B8989;
        }
        .agndCortesia
        {
            color:Red;
        }
        .agndParticular
        {
            color:Green;
        }
        .agndConvenio
        {
            color:Blue;
        }
        /* FIM DA CSS PARA A GRID DE SERVIÇOS AMBULAOTIAIS PERSONALIZADA*/
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <ul style="float: right; width: 980px; margin: 15px 0 0 10px;">
                <li style="clear: both; margin: -18px 0 0 217px; display: none">
                    <asp:Button ID="btnAnamRapida" Style="background-color: #04B486; margin: 0 2px 0 -1px;
                        width: 111px; height: 15px !important; cursor: pointer; color: #fff; border-color: #04B486"
                        Font-Bold="true" runat="server" Text="ANAMNESE RÁPIDA" ToolTip="Imprimir documento 'Anamnese Rápida' para preenchimento pelo paciente."
                        OnClick="btnAnamRapida_Click" />
                </li>
                <li class="liTituloGrid" style="width: 987px; height: 19px !important; margin-right: 0px;
                    background-color: #ADD8E6; text-align: center; font-weight: bold; margin-bottom: 5px;
                    padding-top: 2px;">
                    <ul>
                        <li style="margin: 0px 0 0 10px; float: left">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                HISTÓRICO - ATENDIMENTO</label>
                        </li>
                        <li style="float: right; margin-top: 1px;">
                            <ul>
                                <%--<li style="margin-right: 10px;">
                                    <asp:TextBox runat="server" ID="txtNomeContratoPesqAtend" Width="130px" placeholder=" Pesquise pelo Convênio" />
                                </li>--%>
                                <li style="margin-right: 10px;">
                                    <asp:TextBox runat="server" ID="txtNomeProfPesqAtend" Width="190px" placeholder=" Pesquise pelo Nome do Profissional" />
                                </li>
                                <li>
                                    <asp:TextBox runat="server" ID="txtNomePacPesqAgendAtend" Width="190px" placeholder=" Pesquise pelo Nome do Paciente"></asp:TextBox>
                                </li>
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
                                    <asp:ImageButton ID="imgPesqAgendaAtendimento" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                        OnClick="imgPesqAgendaAtendimento_OnClick" />
                                </li>
                                <%--<li style="margin-left: 5em;">
                                    <asp:Button runat="server" ID="FinalizarAgend" Style="cursor: pointer; color: #000000; border: none; background-color: #add8e6;
                                        font-weight: 600" Text="FINALIZAR" ToolTip="Alterar a situação do agendamento para 'ATENDIDO'."
                                        OnClick="AbreFinalizarAgend_Click" />
                                </li>--%>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li>
                    <div style="width: 985px; height: 260px; border: 1px solid #CCC; margin-bottom: 2px;"
                        id="divAgendaAt">
                        <div style="overflow-y: scroll; height: 100%; width: 100%">
                        <input type="hidden" id="divAgendaAt_posicao" name="divAgendaAt_posicao" />
                        <asp:GridView ID="grdAgendamentos" CssClass="grdBusca headFixo" runat="server" Style="width: 100%;
                            height: 260px; position: relative;" AutoGenerateColumns="false" AllowPaging="false"
                            GridLines="Vertical" OnRowDataBound="grdAgendamentos_OnRowDataBound">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhuma Agendamento de Atendimento nesses parâmetros<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="hora" HeaderText="DATA/HORA">
                                    <ItemStyle Width="65px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_PAC" HeaderText="PACIENTE">
                                    <ItemStyle Width="240px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <%--<asp:TemplateField HeaderText="">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgInfosCadasPaciente" ImageUrl="/Library/IMG/PGS_CentralRegulacao_Icone_EmissaoGuia.png"
                                            ToolTip="Informações cadastrais do paciente e responsável" OnClick="imgInfosCadasPaciente_OnClick"
                                            Style="width: 16px !important; height: 19px !important; margin: 0 0 0 0 !important" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="RESPONSÁVEL">
                                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hidPendFinanc" Value='<%# Eval("flPendFinanc") %>' runat="server" />
                                        <asp:HiddenField ID="hidFaltasConsec" Value='<%# Eval("FaltasConsec") %>' runat="server" />
                                        <asp:Image ID="imgPendFinanc" ImageUrl="/Library/IMG/PGS_CONTRT_PARTICULAR.png" Visible='<%# Eval("flPendFinanc") %>'
                                            Width="16px" Height="16px" Style="margin-left: -5px;" ToolTip="O paciente possui pendencia finaceira"
                                            runat="server" />
                                        <asp:Image ID="imgFaltasConsec" ImageUrl="/Library/IMG/Gestor_ServicosAgendaAtividades.png"
                                            Visible='<%# Eval("FaltasConsec") %>' Width="16px" Height="16px" Style="margin-left: -3px;"
                                            ToolTip="O paciente possui três faltas anteriores a este agendamento" runat="server" />
                                        <asp:Label ID="lblResp" Text='<%# Eval("NO_RESP_DINAMICO_V") %>' Style="margin-left: -3px;"
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="TELEFONE_RESP_DINAMICO" HeaderText="TEL RESP">
                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="NO_COL" HeaderText="PROFISSIONAL">
                                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_CLASS_PROFI" HeaderText="ESPECIALIDADE">
                                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NU_TELEFONE_PROFI_V" HeaderText="TEL PROF">
                                    <ItemStyle Width="70px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="CONTRAT">
                                    <ItemStyle Width="60px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblContratacao" CssClass='<%# Eval("tpContr_CLS") %>' Text='<%# Eval("tpContr_TXT") %>'
                                            ToolTip='<%# Eval("tpContr_TIP") %>' Style="margin-left: -3px;" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TP_CONSUL_VALID" HeaderText="TIPO">
                                    <ItemStyle Width="70px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="PR">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidIdAgend" Value='<%# Eval("CO_AGEND_MEDIC") %>' />
                                        <asp:HiddenField runat="server" ID="hidCoPac" Value='<%# Eval("CO_ALU") %>' />
                                        <asp:HiddenField runat="server" ID="hidCoResp" Value='<%# Eval("CO_RESP") %>' />
                                        <asp:HiddenField runat="server" ID="hidFlConfirmado" Value='<%# Eval("FL_CONF") %>' />
                                        <asp:ImageButton Enabled="false" runat="server" ID="imgPresente" ImageUrl="/Library/IMG/PGS_PacienteNaoChegou.ico"
                                            ToolTip="Marca presença ou ausência" Style="width: 17px !important;
                                            height: 17px !important; margin: 0 0 0 0 !important" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EN">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidFlEncaminhado" Value='<%# Eval("FL_AGEND_ENCAM") %>' />
                                        <asp:HiddenField runat="server" ID="hidIdAgendEncam" Value='<%# Eval("CO_AGEND_MEDIC") %>' />
                                        <asp:ImageButton Enabled="false" runat="server" ID="imgEncam" ImageUrl="/Library/IMG/PGS_IC_EncaminharOut.png"
                                            ToolTip="Encaminha/Cancela encaminhamento" Style="width: 17px !important;
                                            height: 17px !important; margin: 0 0 0 0 !important" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CA">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidCoSitua" Value='<%# Eval("CO_SITU") %>' />
                                        <asp:ImageButton Enabled="false" runat="server" ID="imgCancelar" ImageUrl="/Library/IMG/PGS_SF_AgendaEmAberto.png"
                                            ToolTip="Cancela agendamento de consulta"
                                            Style="width: 17px !important; height: 17px !important; margin: 0 0 0 0 !important" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ST">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton Enabled="false" runat="server" ID="imgSituacao" ImageUrl='<%# Eval("imagem_URL") %>'
                                            ToolTip='<%# Eval("imagem_TIP") %>' Style="width: 17px !important; height: 17px !important;
                                            margin: 0 0 0 0 !important"  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    </div>
                </li>
            </ul>
        </li>
        <li style="margin-right: 0px;">
            <ul id="ulDadosMatricula">
                <li style="margin-top: 10px; margin-left: 15px; float: right;"></li>
            </ul>
        </li>
        <li class="liTituloGrid" style="width: 98.5%; height: 19px !important; margin-right: 0px;
            background-color: #9AFF9A; text-align: center; font-weight: bold; margin-bottom: 5px;
            margin-top: 0px; margin-left: 10px;">
            <ul>
                <li style="margin: 0px 0 0 10px; float: left">
                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                        HISTÓRICO - CONSULTA/AVALIAÇÃO</label>
                </li>
                <li style="float: right; margin-top: 2px;">
                    <ul>
                        <li>
                            <asp:TextBox runat="server" ID="txtNomePacPesqAgendAvaliacao" Width="240px" placeholder="Pesquise pelo Nome do Paciente"></asp:TextBox>
                        </li>
                        <li>
                            <asp:TextBox runat="server" class="campoData" ID="txtDtIniAgendaAval" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="validatorField"
                                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="txtDtIniAgendaAval"></asp:RequiredFieldValidator>
                            <asp:Label runat="server" ID="Label10"> &nbsp à &nbsp </asp:Label>
                            <asp:TextBox runat="server" class="campoData" ID="txtDtFimAgendaAval" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" CssClass="validatorField"
                                ErrorMessage="O campo data Final é requerido" ControlToValidate="txtDtFimAgendaAval"></asp:RequiredFieldValidator><br />
                        </li>
                        <li style="margin-top: 0px; margin-left: 5px;">
                            <asp:ImageButton ID="imgPesqAgendaAval" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                OnClick="imgPesqAgendaAval_OnClick" />
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <!-- =========================================================================================================================================================================== -->
        <!--                                            INÍCIO SELEÇÃO DO AGENDAMENTO                                                                       -->
        <!-- =========================================================================================================================================================================== -->
        <li id="tabSelAgenda" runat="server" clientidmode="Static">
            <asp:HiddenField runat="server" ID="hidIdAgendaAvalicao" />
            <asp:HiddenField runat="server" ID="IDAGENDA" />
            <ul style="width: 100%;">
                <li style="margin-left: 0px;">
                    <div style="width: 985px; height: 100px; border: 1px solid #CCC; margin-bottom: 2px;">
                        <asp:GridView ID="grdAgendaAvaliacoes" CssClass="grdBusca headFixo" runat="server"
                            Style="width: 100%; height: 100px; position: relative;" AutoGenerateColumns="false"
                            AllowPaging="false" GridLines="Vertical" OnRowDataBound="grdAgendaAvaliacoes_OnRowDataBound">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhuma Agendamento de Avaliação nesses parâmetros<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="DATA/HORA">
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# Eval("CO_ALU") %>' />
                                        <asp:HiddenField runat="server" ID="hidCoResp" Value='<%# Eval("CO_RESP") %>' />
                                        <asp:HiddenField runat="server" ID="hidAgendaAval" Value='<%# Eval("ID_AGENDA_AVAL") %>' />
                                        <asp:Label runat="server" ID="txtDataHora" Text='<%# Eval("hora") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NO_PAC" HeaderText="PACIENTE">
                                    <ItemStyle Width="255px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_RESP_DINAMICO_V" HeaderText="RESPONSÁVEL">
                                    <ItemStyle Width="195px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TELEFONE_V" HeaderText="TELEFONE">
                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPO" HeaderText="TIPO">
                                    <ItemStyle Width="40px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LOCAL" HeaderText="LOCAL">
                                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="INFORM.">
                                    <ItemStyle Width="55px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidIdCadAlu" Value='<%# Eval("FL_ST_CAD_ALU") %>' />
                                        <asp:HiddenField runat="server" ID="hidIdCadRes" Value='<%# Eval("FL_ST_CAD_RSP") %>' />
                                        <asp:HiddenField runat="server" ID="hidIdRegItn" Value='<%# Eval("FL_ST_REG_INF") %>' />
                                        <asp:HiddenField runat="server" ID="hidIdSlcAvl" Value='<%# Eval("FL_ST_SLC_AVL") %>' />
                                        <asp:HiddenField runat="server" ID="hitTipoAgend" Value='<%# Eval("TIPO") %>' />
                                        <%--<asp:ImageButton runat="server" ID="imgInfosCadasPac" ImageUrl="/Library/IMG/IC_PGS_Recepcao_CadPacien.png"
                                            ToolTip="Informações cadastrais do paciente" OnClick="imgInfosCadasPac_OnClick"
                                            Style="width: 16px !important; height: 19px !important;" />
                                        <asp:ImageButton runat="server" ID="imgInfosCadasRes" ImageUrl="/Library/IMG/IC_PGS_Recepcao_CadRespon.png"
                                            ToolTip="Informações cadastrais do responsável" OnClick="imgInfosCadasRes_OnClick"
                                            Style="width: 16px !important; height: 19px !important;" />--%>
                                        <asp:ImageButton runat="server" ID="imgRegisItens" ImageUrl="/Library/IMG/IC_PGS_Recepcao_RegItens.png"
                                            ToolTip="Registro de Itens" OnClick="imgRegisItens_OnClick" Style="width: 16px !important;
                                            height: 19px !important;" />
                                        <asp:ImageButton runat="server" ID="imgSelAvaliador" ImageUrl="/Library/IMG/IC_PGS_Recepcao_Avaliador.png"
                                            ToolTip="Seleção do Avaliador" OnClick="imgSelAvaliador_OnClick" Style="width: 16px !important;
                                            height: 19px !important;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CONTRAT">
                                    <ItemStyle Width="80px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblContratacao" CssClass='<%# Eval("tpContr_CLS") %>' Text='<%# Eval("tpContr_TXT") %>'
                                            ToolTip='<%# Eval("tpContr_TIP") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PR">
                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidFlConfirmado" Value='<%# Eval("FL_CONF") %>' />
                                        <asp:ImageButton Enabled="false" runat="server" ID="imgPresenteAA" ImageUrl="/Library/IMG/PGS_PacienteNaoChegou.ico"
                                            ToolTip="Marca presença ou ausência" Style="width: 17px !important;
                                            height: 17px !important; margin: 0 0 0 0 !important" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EN">
                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton Enabled="false" runat="server" ID="imgEncamAA" ImageUrl="/Library/IMG/PGS_IC_EncaminharOut.png"
                                            ToolTip="Encaminha/Cancela encaminhamento do agendamento para atendimento"
                                            Style="width: 17px !important; height: 17px !important; margin: 0 0 0 0 !important" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CA">
                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidCoSitua" Value='<%# Eval("CO_SITU") %>' />
                                        <asp:ImageButton Enabled="false" runat="server" ID="imgCancelarAA" ImageUrl="/Library/IMG/PGS_SF_AgendaEmAberto.png"
                                            ToolTip="Cancela um item de agenda de consulta"
                                            Style="width: 17px !important; height: 17px !important; margin: 0 0 0 0 !important" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ST">
                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Image Enabled="false" runat="server" ID="imgSituacao" ImageUrl='<%# Eval("imagem_URL") %>' ToolTip='<%# Eval("imagem_TIP") %>'
                                            Style="width: 17px !important; height: 17px !important; margin: 0 0 0 0 !important" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
                <%--<li runat="server" id="LiRecCaixa" class="liBtnFinAten" style="height: 15px; margin-top: 5px; background-color: #607d8b;
                    margin-right: 10px; float: right;">
                    <asp:LinkButton ID="lnkbRecCaixa" runat="server" OnClick="lnkbRecCaixa_OnClick">
                        <asp:Label runat="server" ID="Label22" Text="REC CAIXA" Font-Bold="true" Style="margin-left: 5px;
                            margin-right: 5px; color: White;"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li runat="server" id="LiRecSimples" class="liBtnFinAten" style="height: 15px; margin-top: 5px; background-color: #04B486;
                    margin-right: 10px; float: right;">
                    <asp:LinkButton ID="lnkbRecebimento" runat="server" OnClick="lnkbRecebimento_OnClick">
                        <asp:Label runat="server" ID="Label17" Text="REC ATEND" Font-Bold="true" Style="margin-left: 5px;
                            margin-right: 5px; color: White;"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li runat="server" id="LiRecContrato" class="liBtnFinAten" style="height: 15px; margin-top: 5px; background-color: #04B486;
                    margin-right: 10px; float: right;">
                    <asp:LinkButton ID="lnkbRecebContrato" runat="server">
                        <asp:Label runat="server" ID="Label16" Text="REC CONTR" Font-Bold="true" Style="margin-left: 5px;
                            margin-right: 5px; color: White;"></asp:Label>
                    </asp:LinkButton>
                </li>--%>
                <li runat="server" id="LiFichaAten"  class="liBtnFinAten" style="height: 15px; margin-top: 5px; margin-right: 5px;
                    float: right;">
                    <asp:LinkButton ID="lnkbFichaAtend" runat="server" OnClick="lnkbFichaAtend_OnClick">
                        <asp:Label runat="server" ID="lblFichaAtend" Text="FICHA ATEND" Font-Bold="true"
                            Style="margin-left: 4px; margin-right: 4px;"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li  runat="server" id="LiGuia"  class="liBtnFinAten" style="height: 15px; margin-top: 5px; margin-right: 10px;
                    float: right;">
                    <asp:LinkButton ID="lnkbGuia" runat="server" OnClick="lnkbGuia_OnClick" OnClientClick="LimparDadosGuia()">
                        <asp:Label runat="server" ID="Label19" Text="GUIA" Font-Bold="true" Style="margin-left: 4px;
                            margin-right: 4px;"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li  runat="server" id="LiMovimentacao" class="liBtnFinAten" style="height: 15px; margin-top: 5px; background-color: #5858FA;
                    margin-right: 5px; float: right;">
                    <asp:LinkButton ID="lnkbMovimentacao" runat="server" OnClick="lnkbMovimentacao_OnClick">
                        <asp:Label runat="server" ID="Label18" Text="MOVIMEN" ToolTip="Movimentação" Font-Bold="true"
                            Style="margin-left: 4px; margin-right: 4px; color: White;"></asp:Label>
                    </asp:LinkButton>
                </li>
<%--                <li runat="server" id="LiEncaixe" class="liBtnFinAten" style="height: 15px; margin-top: 5px; background-color: DarkOrange;
                    margin-right: 10px; float: right;">
                    <asp:LinkButton ID="lnkbEncaixe" runat="server" OnClick="lnkEncaixe_OnClick">
                        <asp:Label runat="server" ID="lblTriagem" Text="ENCAIXE" Font-Bold="true" Style="margin-left: 10px;
                            margin-right: 10px; color: White;"></asp:Label>
                    </asp:LinkButton>
                </li>--%>
                <li  runat="server" id="LiAgenda" class="liBtnFinAten" style="height: 15px; margin-top: 5px; background-color: DarkOrange;
                    margin-right: 10px; float: right;">
                    <asp:LinkButton ID="lnkbAgenda" runat="server" OnClick="lnkbAgend_OnClick">
                        <asp:Label runat="server" ID="lblEncaixe" Text="AGENDA" Font-Bold="true" Style="margin-left: 10px;
                            margin-right: 10px; color: White;"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li style="margin-top: 1px; margin-left: 5px; font-size: 8px">
                    <asp:Label runat="server" ID="lblLegenda" Text="LEGENDA : " Font-Bold="true" Font-Size="8px" />
                    Contratação:
                    <label class="agndConvenio" style="display: inline; font-size: 8px">
                        G (Plano de Saúde)</label>
                    -
                    <label class="agndParticular" style="display: inline; font-size: 8px">
                        $ (Particular)</label>
                    -
                    <label class="agndCortesia" style="display: inline; font-size: 8px">
                        X (Cortesia)</label>
                   / Controle: PR (Presença) - EN (Encaminhado) - CA (Cancelado) - ST (Situação)
                </li>
            </ul>
        </li>
        <!-- =========================================================================================================================================================================== -->
        <!--                                                           FIM TELA DE SELEÇÃO DO AGENDAMENTO
                                                                           INÍCIO DA TELA DE INFORMAÇÕES DO PACIENTE                                                                                   -->
        <!-- =========================================================================================================================================================================== -->
        <div id="divInfosCadasPac" style="display: none; height: 300px !important;">
            <asp:HiddenField runat="server" ID="hidCoAlu" />
            <ul class="ulDados" style="width: 805px; margin-top: 0px !important">
                <li>
                    <div class="DivResp" style="margin-left: 5px;">
                        <ul class="ulDadosResp">
                            <li style="clear: both; margin-left: -3px; margin-top: 8px;">
                                <ul>
                                    <li class="liFotoColab">
                                        <fieldset class="fldFotoColab">
                                            <uc1:ControleImagem ID="upImagemAluno" runat="server" />
                                        </fieldset>
                                    </li>
                                </ul>
                            </li>
                            <li style="margin: 6px 0 0 -23px;">
                                <ul class="ulDadosPaciente">
                                    <li style="margin-bottom: -2px;">
                                        <label class="lblTop">
                                            DADOS DO PACIENTE</label>
                                    </li>
                                    <li style="clear: both">
                                        <label class="lblObrigatorio">
                                            Nº CONTROLE</label>
                                        <asp:TextBox runat="server" ID="txtNuProntuario" Width="66" CssClass="campoNire"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 8px;">
                                        <label>
                                            Nº CPF</label>
                                        <asp:TextBox runat="server" ID="txtCpfPaci" CssClass="campoCpf" Width="75px"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hidCoPac" />
                                    </li>
                                    <li style="margin-left: 8px;">
                                        <label>
                                            Nº CNES/SUS</label>
                                        <asp:TextBox runat="server" ID="txtNuNisPaci" Width="96" CssClass="campoNis" MaxLength="16"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 12px;">
                                        <label class="lblObrigatorio">
                                            Nome do Paciente</label>
                                        <asp:TextBox runat="server" ID="txtnompac" ToolTip="Nome do Paciente" Width="270"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 39px;">
                                        <label for="ddlSituacaoAlu" class="lblObrigatorio" title="Situação do Aluno">
                                            Situação</label>
                                        <asp:DropDownList ID="ddlSituacaoAlu" CssClass="ddlSituacaoAlu" ToolTip="Informe a Situação do Aluno"
                                            runat="server" Width="98px" Enabled="false">
                                            <asp:ListItem Value="A" Selected="True">Em Atendimento</asp:ListItem>
                                            <asp:ListItem Value="V">Em Análise</asp:ListItem>
                                            <asp:ListItem Value="E">Alta(Normal)</asp:ListItem>
                                            <asp:ListItem Value="D">Alta (Desistência)</asp:ListItem>
                                            <asp:ListItem Value="I">Inativo</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="clear: both; margin-bottom: 6px;"></li>
                                    <li style="clear: both;">
                                        <label class="lblObrigatorio">
                                            Apelido</label>
                                        <asp:TextBox runat="server" ID="txtApelidoPaciente" Width="120px" MaxLength="25"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 24px;">
                                        <label class="lblObrigatorio">
                                            Sexo</label>
                                        <asp:DropDownList runat="server" ID="ddlSexoPaci" Width="44px">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-left: 21px">
                                        <label class="lblObrigatorio">
                                            Nascimento</label>
                                        <asp:TextBox runat="server" ID="txtDtNascPaci" CssClass="campoData"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 21px">
                                        <label>
                                            Origem</label>
                                        <asp:DropDownList runat="server" ID="ddlOrigemPaci" Width="100px">
                                            <asp:ListItem Value="SR" Text="Sem Registro"></asp:ListItem>
                                            <asp:ListItem Value="MU" Text="Local - Escola Pública"></asp:ListItem>
                                            <asp:ListItem Value="EP" Text="Local - Escola Particular"></asp:ListItem>
                                            <asp:ListItem Value="IE" Text="Interior do Estado"></asp:ListItem>
                                            <asp:ListItem Value="OE" Text="Outro Estado"></asp:ListItem>
                                            <asp:ListItem Value="AR" Text="Área Rural"></asp:ListItem>
                                            <asp:ListItem Value="AI" Text="Área Indígena"></asp:ListItem>
                                            <asp:ListItem Value="AQ" Text="Área Quilombo"></asp:ListItem>
                                            <asp:ListItem Value="OO" Text="Outra Origem"></asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-left: 21px">
                                        <label for="txtNaturalidade" title="Cidade de Naturalidade do Aluno">
                                            Naturalidade</label>
                                        <asp:TextBox ID="txtNaturalidade" CssClass="txtNaturalidade" Style="width: 140px;"
                                            runat="server" ToolTip="Informe a Cidade de Naturalidade do Usuario" MaxLength="40"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 21px">
                                        <label for="txtEstadoCivel " title="Estado Civil">
                                            Estado Civil</label>
                                        <asp:DropDownList ID="ddlEstadoCivil" Style="width: 86px;" runat="server" ToolTip="Estado cível">
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
                                </ul>
                            </li>
                            <li style="clear: both">
                                <label class="lblObrigatorio">
                                    Início Ativ</label>
                                <asp:TextBox runat="server" ID="txtAnoAtiv" Width="28px"></asp:TextBox>
                                <asp:TextBox runat="server" ID="txtMesAtiv" Width="17px"></asp:TextBox>
                            </li>
                            <li style="margin-left: 25px;">
                                <label>
                                    Nº Cartão Saúde</label>
                                <asp:TextBox runat="server" ID="txtNuCarSaude" Width="78px"></asp:TextBox>
                            </li>
                            <li style="margin-left: 16px">
                                <label title="Deficiência">
                                    Deficiência</label>
                                <asp:DropDownList ID="ddlDeficienciaAlu" Width="98px" CssClass="ddlDeficienciaAlu"
                                    runat="server" ToolTip="Selecione a Deficiência do Usuario">
                                </asp:DropDownList>
                            </li>
                            <li style="margin-left: 20px;">
                                <label title="Etnia do Aluno">
                                    Etnia</label>
                                <asp:DropDownList ID="ddlEtniaAlu" CssClass="ddlEtniaAlu" runat="server" ToolTip="Informe a Etnia do Usuario"
                                    Width="76px">
                                    <asp:ListItem Value="B">Branca</asp:ListItem>
                                    <asp:ListItem Value="N">Negra</asp:ListItem>
                                    <asp:ListItem Value="A">Amarela</asp:ListItem>
                                    <asp:ListItem Value="P">Parda</asp:ListItem>
                                    <asp:ListItem Value="I">Indígena</asp:ListItem>
                                    <asp:ListItem Value="X" Selected="true">Não Informada</asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li style="margin-left: 18px">
                                <label>
                                    Altura</label>
                                <asp:TextBox runat="server" ID="txtAltura" Width="30px" ClientIDMode="Static"></asp:TextBox>
                            </li>
                            <li>
                                <label>
                                    Peso
                                </label>
                                <asp:TextBox runat="server" ID="txtPeso" Width="30px" ClientIDMode="Static"></asp:TextBox>
                            </li>
                            <li style="margin-left: 31px;">
                                <label title="CPF do Responsável do Aluno">
                                    Indicação
                                </label>
                                <asp:DropDownList ID="ddlIndicacao" Style="width: 273px;" runat="server" ToolTip="Indicação">
                                </asp:DropDownList>
                            </li>
                            <li style="clear: both; margin-bottom: 6px;"></li>
                            <li class="ulIdentResp" style="clear: both">
                                <ul>
                                    <li>
                                        <asp:Label runat="server" ID="Label14" Style="font-size: 9px;">Carteira de Identidade</asp:Label>
                                    </li>
                                    <li style="clear: both;">
                                        <label>
                                            Número</label>
                                        <asp:TextBox runat="server" ID="txtNumeroRGPaciente" Width="70px" ClientIDMode="Static"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Org Emiss</label>
                                        <asp:TextBox runat="server" ID="txtOrgEmissRGPaciente" Width="50px" ClientIDMode="Static"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            UF</label>
                                        <asp:DropDownList runat="server" ID="ddlUFRGPaciente" Width="40px">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </li>
                            <li class="ulIdentResp" style="margin-left: 14px">
                                <ul>
                                    <li>
                                        <asp:Label runat="server" ID="Label15" Style="font-size: 9px;">Dados de Contato</asp:Label>
                                    </li>
                                    <li style="clear: both;">
                                        <label>
                                            Tel. Fixo</label>
                                        <asp:TextBox runat="server" ID="txtTelCelPaci" Width="78px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 5px;">
                                        <label>
                                            Tel. Celular</label>
                                        <asp:TextBox runat="server" ID="txtTelResPaci" Width="78px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 5px;">
                                        <label>
                                            Tel. Comercial</label>
                                        <asp:TextBox runat="server" ID="txtTeComePaci" Width="78px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 5px;">
                                        <label>
                                            Nº WhatsApp</label>
                                        <asp:TextBox runat="server" ID="txtWhatsPaci" Width="78px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 5px;">
                                        <label>
                                            Facebook</label>
                                        <asp:TextBox runat="server" ID="txtFacePaci" Width="80px"></asp:TextBox>
                                    </li>
                                </ul>
                            </li>
                            <li>
                                <ul>
                                    <li style="margin-left: 2px; margin-bottom: 2px;">
                                        <label class="lblSubInfos">
                                            FILIACAO</label>
                                    </li>
                                    <li style="clear: both">
                                        <label class="lblObrigatorio">
                                            Nome da Mãe</label>
                                        <asp:TextBox runat="server" ID="txtNomeMae" Width="185px"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Óbito</label>
                                        <asp:DropDownList runat="server" ID="ddlObitoMae" Width="45px" OnSelectedIndexChanged="ddlObitoMae_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                                            <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <label title="Profissão">
                                            Profissão
                                        </label>
                                        <asp:DropDownList ID="ddlProfissaoNomeMae" Style="width: 103px;" runat="server" ToolTip="Profissão">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-left: -1px">
                                        <label>
                                            Tel. Mãe</label>
                                        <asp:TextBox runat="server" ID="txtTelMae" Width="70px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li style="margin: 0px 0 0 -3px;">
                                        <label title="Responsável pelo acompanhamento do Paciente" style="margin-left: 5px;">
                                            RA</label>
                                        <asp:CheckBox runat="server" ID="chkMaeResp" CssClass="chk" ToolTip="Responsável pelo acompanhamento do Paciente" />
                                    </li>
                                    <li style="margin: 0px 0 0 -3px;">
                                        <label title="Responsável financeiro do Paciente" style="margin-left: 5px;">
                                            RF</label>
                                        <asp:CheckBox runat="server" ID="chkMaeRespFinanc" CssClass="chk" ToolTip="Ao marcar, determina que a mãe é responsável financeira"
                                            OnCheckedChanged="chkMaeRespFinanc_OnCheckedChanged" AutoPostBack="true" />
                                    </li>
                                    <li style="clear: both;">
                                        <label class="lblObrigatorio">
                                            Nome da Pai</label>
                                        <asp:TextBox runat="server" ID="txtNomePai" Width="185px"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Óbito</label>
                                        <asp:DropDownList runat="server" ID="ddlObitoPai" Width="45px" OnSelectedIndexChanged="ddlObitoPai_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                                            <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <label title="Profissão">
                                            Profissão
                                        </label>
                                        <asp:DropDownList ID="ddlProfiPai" Style="width: 103px;" runat="server" ToolTip="Profissão">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-left: -1px">
                                        <label>
                                            Tel. Pai</label>
                                        <asp:TextBox runat="server" ID="txtTelPai" Width="70px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li style="margin: 0px 0 0 -3px;">
                                        <label title="Responsável pelo acompanhamento do Paciente" style="margin-left: 5px;">
                                            RA</label>
                                        <asp:CheckBox runat="server" ID="chkPaiResp" CssClass="chk" ToolTip="Responsável pelo acompanhamento do Paciente" />
                                    </li>
                                    <li style="margin: 0px 0 0 -3px;">
                                        <label title="Responsável financeiro do Paciente" style="margin-left: 5px;">
                                            RF</label>
                                        <asp:CheckBox runat="server" ID="chkPaiRespFinanc" CssClass="chk" ToolTip="Responsável financeiro do Paciente"
                                            OnCheckedChanged="chkPaiRespFinanc_OnCheckedChanged" AutoPostBack="true" />
                                    </li>
                                </ul>
                            </li>
                            <li id="li1" runat="server" class="liBtnAddA" style="height: 15px; margin-top: -24px;
                                margin-right: 5px; float: right">
                                <asp:LinkButton ID="lnkConfirmarInfoCadas" runat="server" ValidationGroup="atuEndAlu"
                                    OnClick="lnkConfirmarInfoCadas_OnClick">
                                    <asp:Label runat="server" ID="Label3848" Text="CONFIRMAR" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </li>
            </ul>
        </div>
        <!-- =========================================================================================================================================================================== -->
        <!--                                                           FIM TELA DE SELEÇÃO DO INFORMAÇÕES DO PACIENTE
                                                                           INÍCIO DA TELA DE INFORMAÇÕES DO RESPONSÁVEL                                                                                   -->
        <!-- =========================================================================================================================================================================== -->
        <div id="divInfosCadasResp" style="display: none; height: 250px !important;">
            <asp:HiddenField runat="server" ID="hidCoResp" />
            <ul class="ulDados" style="width: 805px; margin-top: 0px !important">
                <li style="margin: 0px 0 -5px 0px">
                    <label class="lblTop">
                        DADOS DO RESPONSÁVEL</label>
                </li>
                <li style="clear: both;">
                    <ul style="margin-left: 10px" class="ulEndResiResp">
                        <li style="clear: both; width: 190px; border-right: 1px solid #CCCCCC; height: 104px;
                            margin-top: 0px; margin-bottom: 20px">
                            <ul style="margin-left: -10px" class="ulInfosGerais">
                                <li style="margin-left: 5px; margin-bottom: 1px;">
                                    <label class="lblSubInfos">
                                        INFORMAÇÕES GERAIS</label>
                                </li>
                                <li style="clear: both; margin-top: 8px;">
                                    <asp:CheckBox runat="server" ID="chkPaciEhResp" OnCheckedChanged="chkPaciEhResp_OnCheckedChanged"
                                        AutoPostBack="true" Text="Paciente é o próprio Responsável" CssClass="chk" />
                                </li>
                                <li style="clear: both; margin-top: 8px;">
                                    <asp:CheckBox runat="server" ID="chkPaciMoraCoResp" Text="Paciente mora com o(a) Responsável"
                                        CssClass="chk" />
                                </li>
                                <li style="clear: both; margin-top: 8px;">
                                    <asp:CheckBox runat="server" ID="chkRespFinanc" Text="É o responsável financeiro"
                                        CssClass="chk" />
                                </li>
                            </ul>
                        </li>
                        <li style="border-right: 1px solid #CCCCCC; height: 104px; margin-top: 0px; margin-left: 12px;
                            width: 310px; margin-bottom: 20px">
                            <ul>
                                <li style="margin-left: 1px; margin-bottom: 1px;">
                                    <label class="lblSubInfos">
                                        ENDEREÇO RESIDENCIAL / CORRESPONDÊNCIA</label>
                                </li>
                                <li style="clear: both;">
                                    <label class="lb lObrigatorio">
                                        CEP</label>
                                    <asp:TextBox runat="server" ID="txtCEP" Width="55px" CssClass="campoCepF" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="txtCEP_PADRAO" ClientIDMode="Static" Style="display: none" />
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
                                    <asp:DropDownList runat="server" ID="ddlCidade" Width="172px" OnSelectedIndexChanged="ddlCidade_OnSelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </li>
                                <li style="clear: both">
                                    <label class="lblObrigatorio">
                                        Bairro</label>
                                    <asp:DropDownList runat="server" ID="ddlBairro" Width="125px">
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label class="lblObrigatorio">
                                        Logradouro</label>
                                    <asp:TextBox runat="server" ID="txtLograEndResp" Width="160px" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="txtLograEndResp_PADRAO" ClientIDMode="Static" Style="display: none" />
                                </li>
                            </ul>
                        </li>
                        <li style="height: 65px; margin-top: 0px; margin-left: 14px;">
                            <ul>
                                <li style="margin-left: 1px; margin-bottom: 1px;">
                                    <label class="lblSubInfos">
                                        PLANO DE SAÚDE</label>
                                </li>
                                <li style="clear: both">
                                    <label>
                                        Operadora</label>
                                    <asp:DropDownList runat="server" ID="ddlOperadoraPacie" Width="120px" OnSelectedIndexChanged="ddlOperadoraPacie_OnSelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label>
                                        Plano</label>
                                    <asp:DropDownList runat="server" ID="ddlPlanoPacie" Width="112px" OnSelectedIndexChanged="ddlPlanoPacie_OnSelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </li>
                                <li style="clear: both">
                                    <label>
                                        Categoria</label>
                                    <asp:DropDownList runat="server" ID="ddlCategoriaPacie" Width="140px">
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label>
                                        Nº Plano</label>
                                    <asp:TextBox runat="server" ID="txtNuPlanoPacie" Width="90px" MaxLength="22"></asp:TextBox>
                                </li>
                                <li style="clear: both">
                                    <label>
                                        Vencimento</label>
                                    <asp:TextBox ID="txtVencPlano" runat="server" CssClass="campoData" />
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li style="clear: both">
                    <label class="lblObrigatorio">
                        CPF</label>
                    <asp:TextBox runat="server" ID="txtCPFResp" Style="width: 74px;" CssClass="campoCpf"
                        ToolTip="CPF do Responsável" Text="000.000.000-00" ClientIDMode="Static"></asp:TextBox>
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
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                    <label class="lblObrigatorio">
                        Nascimento</label>
                    <asp:TextBox runat="server" ID="txtDtNascResp" CssClass="campoData" ClientIDMode="Static"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Grau Parentesco</label>
                    <asp:DropDownList runat="server" ID="ddlGrParen" Width="100px">
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
                <li>
                    <label>
                        Profissão</label>
                    <asp:DropDownList runat="server" ID="ddlFuncao" Width="100px">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Email</label>
                    <asp:TextBox runat="server" ID="txtEmailResp" Width="140px"></asp:TextBox>
                </li>
                <li style="clear: both; margin: 4px 0 0 0px;">
                    <ul class="ulIdentResp">
                        <li>
                            <asp:Label runat="server" ID="lblcarteIden" Style="font-size: 9px;" CssClass="lblObrigatorio">Carteira de Identidade</asp:Label>
                        </li>
                        <li style="clear: both;">
                            <label>
                                Número</label>
                            <asp:TextBox runat="server" ID="txtNuIDResp" Width="70px" ClientIDMode="Static" MaxLength="20"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Org Emiss</label>
                            <asp:TextBox runat="server" ID="txtOrgEmiss" Width="50px" ClientIDMode="Static"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                UF</label>
                            <asp:DropDownList runat="server" ID="ddlUFOrgEmis" Width="40px">
                            </asp:DropDownList>
                        </li>
                    </ul>
                </li>
                <li style="margin: 4px 0 0 10px; clear: none">
                    <ul class="ulDadosContatosResp">
                        <li>
                            <asp:Label runat="server" ID="Label1" Style="font-size: 9px;">Dados de Contato</asp:Label>
                        </li>
                        <li style="clear: both;">
                            <label>
                                Tel. Fixo</label>
                            <asp:TextBox runat="server" ID="txtTelFixResp" Width="78px" CssClass="campoTel"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Tel. Celular</label>
                            <asp:TextBox runat="server" ID="txtTelCelResp" Width="78px" CssClass="campoTel8dig"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Tel. Comercial</label>
                            <asp:TextBox runat="server" ID="txtTelComResp" Width="78px" CssClass="campoTel"></asp:TextBox>
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
                <li id="li8" runat="server" class="liBtnAddA" style="float: right; margin: 24px 24px 0 0 !important;
                    clear: none !important; height: 15px;">
                    <asp:LinkButton ID="lnkConfirmarInfosCadasResponsavel" runat="server" ValidationGroup="atuEndAlu"
                        OnClick="lnkConfirmarInfosCadasResponsavel_OnClick">
                        <asp:Label runat="server" ID="Label5" Text="CONFIRMAR" Style="margin-left: 4px;"></asp:Label>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>
        <!-- =========================================================================================================================================================================== -->
        <!--                                                           FIM TELA DE INFORMAÇÕES CADASTRAIS DO RESPONSÁVEL  
                                                                           INÍCIO DA TELA DE SELEÇÃO DO AVALIADOR                                                                                   -->
        <!-- =========================================================================================================================================================================== -->
        <div id="divSelAvaliador" style="display: none; height: 250px !important;">
            <asp:HiddenField runat="server" ID="hidCoCol" />
            <ul class="ulDados" style="width: 805px; margin-top: 0px !important">
                <li class="liTituloGrid" style="width: 100.5%; height: 16px !important; margin: -4px 0 0 -5px;
                    background-color: #d2ffc2; text-align: center; font-weight: bold; padding-top: 2px;">
                    <ul>
                        <li style="margin-left: 10px;">
                            <label style="font-family: Tahoma; font-weight: bold; color: #8B1A1A; margin-top: 1px;">
                                PROFISSIONAL ANÁLISE PRÉVIA</label>
                        </li>
                    </ul>
                </li>
                <li>
                    <ul>
                        <li>
                           <div style="border: 1px solid #CCCCCC; width: 781px; height: 170px; overflow-y: scroll;
                                    margin: 10px 0 0 10px">
                                    <asp:GridView ID="grdMedicosPlanto" CssClass="grdBusca" runat="server" Style="width: 100% !important;
                                        cursor: default" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical">
                                        <RowStyle CssClass="rowStyle" />
                                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                        <EmptyDataTemplate>
                                            Nenhum Avaliador ativo<br />
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="CK">
                                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hidcoCol" Value='<%# Eval("co_col") %>' runat="server" />
                                                    <asp:HiddenField ID="hidcoEmpColPla" Value='<%# Eval("_co_emp_col_pla") %>' runat="server" />
                                                    <asp:CheckBox ID="chkProfAvali" runat="server" Checked='<%# Eval("check") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="_NO_COL" HeaderText="PROFISSINAL SAÚDE">
                                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="_NO_CLASS_PROFI" HeaderText="CLASS PROFI">
                                                <ItemStyle HorizontalAlign="Left" Width="115px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="_TELEFONE_V" HeaderText="TELEFONE">
                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:CommandField HeaderText="Nome" ShowSelectButton="false" Visible="False" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                        </li>
                    </ul>
                </li>
                <li id="li2" runat="server" class="liBtnAddA" style="float: right; margin: 10px 15px 0 0 !important;
                    clear: none !important; height: 15px;">
                    <asp:LinkButton ID="lnkConfirmarAvaliador" runat="server" ValidationGroup="atuEndAlu"
                        OnClick="lnkConfirmarAvaliador_OnClick">
                        <asp:Label runat="server" ID="Label4" Text="CONFIRMAR" Style="margin-left: 4px;"></asp:Label>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>
        <!-- =========================================================================================================================================================================== -->
        <!--                                                           FIM TELA DE SELEÇÃO DO AVALIADOR
                                                                           INÍCIO DA TELA DE REGISTRO DE ITENS                                                                                    -->
        <!-- =========================================================================================================================================================================== -->
        <div id="divRegisItens" style="display: none; height: 250px !important;">
            <ul class="ulDados" style="width: 805px; margin-top: 0px !important">
                <li class="liTituloGrid" style="width: 100.5%; height: 19px !important; margin-right: 0px;
                    margin: -4px 0 0 -5px; background-color: #ADD8E6; text-align: center; font-weight: bold;
                    margin-bottom: 5px; padding-top: 2px;">
                    <ul>
                        <li style="margin: 0px 0 0 10px; float: none">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                REGISTRO DE SOLICITAÇÕES DE PROCEDIMENTOS</label>
                        </li>
                    </ul>
                </li>
                <li>
                    <div style="width: 783px; height: 160px; border: 1px solid #CCC; overflow-y: scroll;
                        margin-bottom: 2px; margin-left: 7px">
                        <asp:GridView ID="grdSolicitacoes" CssClass="grdBusca" runat="server" Style="width: 100%;
                            cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhuma solicitação inserida<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="PLANO">
                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidIdOper" />
                                        <asp:HiddenField runat="server" ID="hidIdPlano" />
                                        <asp:HiddenField runat="server" ID="hidIdCateg" />
                                        <asp:HiddenField runat="server" ID="hidNuPlano" />
                                        <asp:HiddenField runat="server" ID="hidNuGuia" />
                                        <asp:HiddenField runat="server" ID="hidNuAutor" />
                                        <asp:HiddenField runat="server" ID="hidItemSolic" />
                                        <asp:HiddenField runat="server" ID="hidAgendaSolic" />
                                        <asp:ImageButton runat="server" ID="imgInfosPlano" ImageUrl="/Library/IMG/PGS_CentralRegulacao_Icone_Ocorrencias.png"
                                            ToolTip="Informações do Plano de Saúde" OnClick="imgInfosPlano_OnClick" Style="margin: 0 0 0 0 !important" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GRUPO">
                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlGrupoProc" Width="120px" OnSelectedIndexChanged="ddlGrupoProc_OnSelectedIndexChanged"
                                            AutoPostBack="true" Style="margin: 0 0 0 0 !important">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SUBGRUPO">
                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlSubGrupo" Width="120px" OnSelectedIndexChanged="ddlSubGrupo_OnSelectedIndexChanged"
                                            AutoPostBack="true" Style="margin: 0 0 0 0 !important">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PROCEDIMENTO">
                                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlProcedimento" OnSelectedIndexChanged="ddlProcedimento_OnSelectedIndexChanged"
                                            AutoPostBack="true" Width="240px" Style="margin: 0 0 0 0 !important">
                                        </asp:DropDownList>
                                        <asp:HiddenField runat="server" ID="hidValUnitProc" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QTDE">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtQtde" CssClass="nuQtde" ClientIDMode="Static"
                                            Width="20px" OnTextChanged="txtQtde_OnTextChanged" AutoPostBack="true" Style="margin: 0 0 0 0 !important"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R$ TOTAL">
                                    <ItemStyle Width="68px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgInfos" ImageUrl="/Library/IMG/Gestor_ComoChegar.png"
                                            ToolTip="Visualizar maiores informações sobre o Procedimento" Style="margin: 0 0 0 0 !important;"
                                            OnClick="imgInfos_OnClick" />
                                        <asp:TextBox runat="server" ID="txtVlUnitario" Width="50px" ClientIDMode="Static"
                                            CssClass="campoDin" Enabled="false" Style="margin: 0 0 0 0 !important"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R$ DESC">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtVlDesconto" Width="50px" ClientIDMode="Static"
                                            CssClass="campoDin" OnTextChanged="txtVlDesconto_OnTextChanged" AutoPostBack="true"
                                            Style="margin: 0 0 0 0 !important"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EX">
                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgExc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                            ToolTip="Excluir item de solicitação" OnClick="imgExc_OnClick" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
                <li>
                    <ul>
                        <li style="margin-left: 8px;">
                            <asp:Label runat="server" ID="lblVlBase">Valor Base R$</asp:Label>
                            <asp:TextBox runat="server" ID="txtVlBaseTotal" Enabled="false" CssClass="campoMoeda"
                                Width="70px"></asp:TextBox>
                        </li>
                        <li style="margin-left: 30px;">
                            <asp:Label runat="server" ID="Label7">Valor Desconto R$</asp:Label>
                            <asp:TextBox runat="server" ID="txtVlDescontoTotal" Enabled="false" CssClass="campoMoeda"
                                Width="70px"></asp:TextBox>
                        </li>
                        <li style="margin-left: 30px;">
                            <asp:Label runat="server" ID="Label8">Valor Líquido R$</asp:Label>
                            <asp:TextBox runat="server" ID="txtVlLiquidoTotal" Enabled="false" CssClass="campoMoeda"
                                Width="70px"></asp:TextBox>
                        </li>
                        <li style="float: right; margin-top: 2px; margin-left: 92px;">
                            <img alt="" class="imgAdd" title="Adicionar Questão" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                                height="15px" width="15px" />
                            <asp:LinkButton runat="server" ID="btnMaisSolicitacoes" Style="margin-left: 4px;"
                                OnClick="btnMaisSolicitacoes_OnClick"><span style="color:#FF7050">INSERIR MAIS SOLICITAÇÕES</span></asp:LinkButton>
                        </li>
                        <li id="li5" runat="server" class="liBtnAddA" style="margin-top: 5px !important;
                            clear: both !important; height: 15px; margin-left: 140px">
                            <asp:LinkButton ID="lnkFinalizar" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkFinalizar_OnClick">
                                <asp:Label runat="server" ID="Label9" Text="CONFIRMAR" Style="margin-left: 4px;"></asp:Label>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
        <li>
            <div id="divLoadShowInfoPlano" style="display: none; height: 117px !important;">
                <asp:HiddenField runat="server" ID="hidIndexGridRefer" />
                <ul class="ulDados" style="width: 407px; margin-top: 0px !important">
                    <li>
                        <label>
                            Operadora</label>
                        <asp:DropDownList runat="server" ID="ddlOperadora" Width="200px" OnSelectedIndexChanged="ddlOperadora_OnSelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            Plano</label>
                        <asp:DropDownList runat="server" ID="ddlPlano" Width="190px" OnSelectedIndexChanged="ddlPlano_OnSelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both">
                        <label>
                            Categoria</label>
                        <asp:DropDownList runat="server" ID="ddlCategoria" Width="160px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            Nº Plano</label>
                        <asp:TextBox runat="server" ID="txtNuPlano"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Nº Guia</label>
                        <asp:TextBox runat="server" ID="txtNuGuia"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Nº Autorização</label>
                        <asp:TextBox runat="server" ID="txtNuAutorizacao"></asp:TextBox>
                    </li>
                    <li id="li3" runat="server" class="liBtnAddA" style="float: right; margin-top: 0px !important;
                        clear: none !important; height: 15px;">
                        <asp:LinkButton ID="lnkConfirmarPlano" runat="server" ValidationGroup="atuEndAlu"
                            OnClick="lnkConfirmarPlano_OnClick" ToolTip="Armazena as informações na solicitação em questão">
                            <asp:Label runat="server" ID="Label6" Text="CONFIRMAR" Style="margin-left: 4px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divLoadShowInfoProcedimento" style="display: none; height: 180px !important;">
                <ul class="ulDados" style="width: 407px; margin-top: 0px !important">
                    <li>
                        <label>
                            Código</label>
                        <asp:TextBox runat="server" ID="txtCodProc" Enabled="false" Width="70px"></asp:TextBox>
                    </li>
                    <li style="float: right">
                        <label>
                            R$ Unitário</label>
                        <asp:TextBox runat="server" ID="txtVlUnitProc" Enabled="false" Width="70px"></asp:TextBox>
                    </li>
                    <li style="clear: both">
                        <label>
                            Nome</label>
                        <asp:TextBox runat="server" ID="txtNomeProc" Enabled="false" Width="400px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Observação</label>
                        <asp:TextBox runat="server" ID="txtObservacaoProc" Enabled="false" TextMode="MultiLine"
                            Width="400px" Height="50px"></asp:TextBox>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divLoadShowCancelaAgenda" style="display: none; height: 140px !important;">
                <asp:HiddenField runat="server" ID="hidIdAgendaCancel" />
                <asp:HiddenField runat="server" ID="hidTipoAgenda" />
                <!-- AV para Avaliação | AT para Atendimento -->
                <ul class="ulDados" style="width: 407px; margin-top: 0px !important">
                    <div id="divDesCance" runat="server">
                        <li>
                            <label style="font-size: 12px; margin-bottom: 10px;">
                                Tem certeza que deseja desfazer o cancelamento deste agendamento?</label>
                        </li>
                    </div>
                    <div id="divCance" runat="server">
                        <li>
                            <label title="Data e Hora do cancelamento">
                                Data/Hora</label>
                            <asp:TextBox runat="server" ID="txtDtCancelamento" CssClass="campoData" ToolTip="Data do Cancelamento"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtHrCancelamento" CssClass="campoHora" ToolTip="Hora do Cancelamento"></asp:TextBox>
                        </li>
                        <li style="float: right;">
                            <label style="margin-left: 9px; color: Red;">
                                CANCELAMENTO</label>
                            <label style="margin: -13px 0 0 140px; color: Blue;">
                                FALTA</label>
                            <asp:RadioButtonList ID="rdblTiposCancelamento" CssClass="chk" BorderWidth="0px"
                                RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem Value="C" Text="Clinica" />
                                <asp:ListItem Value="P" Text="Paciente" />
                                <asp:ListItem Value="N" Text="Não Just." style="margin-left: 20px;" />
                                <asp:ListItem Value="S" Text="Justificada" />
                            </asp:RadioButtonList>
                        </li>
                    </div>
                    <li>
                        <label>
                            Observação</label>
                        <asp:TextBox runat="server" ID="txtObserCancelamento" TextMode="MultiLine" Width="400px"
                            Height="50px"></asp:TextBox>
                    </li>
                    <li id="li6" runat="server" class="liBtnAddA" style="float: right; margin-top: 10px !important;
                        clear: none !important; height: 15px;">
                        <asp:LinkButton ID="lnkConfirmaCancelamento" runat="server" OnClick="lnkConfirmaCancelamento_OnClick"
                            ToolTip="Confirma o cancelamento/descancelamento de item de agenda " >
                            <asp:Label runat="server" ID="Label11" Text="CONFIRMAR" Style="margin-left: 4px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divLoadShowInfosCadasPaciente" style="display: none; height: 275px !important;">
                <ul class="ulDados" style="width: 750px; margin-top: 0px !important">
                    <asp:HiddenField runat="server" ID="hidCoPacModal" />
                    <asp:HiddenField runat="server" ID="hidCoRespModal" />
                    <div class="DivResp" style="margin-left: 5px;">
                        <ul class="ulDadosResp">
                            <li style="clear: both; margin-left: -5px; margin-top: 20px;">
                                <ul>
                                    <li class="liFotoColab">
                                        <fieldset class="fldFotoColab">
                                            <uc1:ControleImagem ID="updImagePacienteMODAL" runat="server" />
                                        </fieldset>
                                    </li>
                                </ul>
                            </li>
                            <li style="margin: 0px 0 0 -23px;">
                                <ul class="ulDadosPaciente">
                                    <li style="margin-bottom: -6px;">
                                        <label class="lblTop">
                                            DADOS DO PACIENTE - USUÁRIO DE SAÚDE</label>
                                    </li>
                                    <li style="clear: both">
                                        <label>
                                            Nº PRONTUÁRIO</label>
                                        <asp:TextBox runat="server" ID="txtNuProntuMODAL" Width="56" CssClass="campoNire"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            CPF</label>
                                        <asp:TextBox runat="server" ID="txtCPFPaciMODAL" CssClass="campoCpf" Width="100px"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Nº CNES/SUS</label>
                                        <asp:TextBox runat="server" ID="txtSUSPaciMODAL" Width="96" CssClass="campoNis" MaxLength="16"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio">
                                            Nome</label>
                                        <asp:TextBox runat="server" ID="txtNomePaciMODAL" ToolTip="Nome do Paciente" Width="220px"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio">
                                            Sexo</label>
                                        <asp:DropDownList runat="server" ID="ddlSexoPaciMODAL" Width="44px">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <label for="ddlEtniaAlu" title="Etnia do Aluno">
                                            Etnia</label>
                                        <asp:DropDownList ID="ddlEtniaPaciMODAL" CssClass="ddlEtniaAlu" runat="server" ToolTip="Informe a Etnia do Usuario"
                                            Width="90px">
                                            <asp:ListItem Value="B">Branca</asp:ListItem>
                                            <asp:ListItem Value="N">Negra</asp:ListItem>
                                            <asp:ListItem Value="A">Amarela</asp:ListItem>
                                            <asp:ListItem Value="P">Parda</asp:ListItem>
                                            <asp:ListItem Value="I">Indígena</asp:ListItem>
                                            <asp:ListItem Value="X" Selected="true">Não Informada</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="clear: both" class="lisobe">
                                        <label class="lblObrigatorio">
                                            Nascimento</label>
                                        <asp:TextBox runat="server" ID="txtDtNascPaciMODAL" CssClass="campoData"></asp:TextBox>
                                    </li>
                                    <li class="lisobe">
                                        <label>
                                            Origem</label>
                                        <asp:DropDownList runat="server" ID="ddlOrigemPaciMODAL" Width="90px">
                                            <asp:ListItem Value="SR" Text="Sem Registro"></asp:ListItem>
                                            <asp:ListItem Value="MU" Text="Local - Escola Pública"></asp:ListItem>
                                            <asp:ListItem Value="EP" Text="Local - Escola Particular"></asp:ListItem>
                                            <asp:ListItem Value="IE" Text="Interior do Estado"></asp:ListItem>
                                            <asp:ListItem Value="OE" Text="Outro Estado"></asp:ListItem>
                                            <asp:ListItem Value="AR" Text="Área Rural"></asp:ListItem>
                                            <asp:ListItem Value="AI" Text="Área Indígena"></asp:ListItem>
                                            <asp:ListItem Value="AQ" Text="Área Quilombo"></asp:ListItem>
                                            <asp:ListItem Value="OO" Text="Outra Origem"></asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-left: 10px;" class="lisobe">
                                        <label>
                                            Nº Cartão Saúde</label>
                                        <asp:TextBox runat="server" ID="txtNuCartSauPaciMODAL" Width="87px"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 10px;" class="lisobe">
                                        <label>
                                            Tel. Celular</label>
                                        <asp:TextBox runat="server" ID="txtCelPaciMODAL" Width="68px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li class="lisobe">
                                        <label>
                                            Tel. Fixo</label>
                                        <asp:TextBox runat="server" ID="txtFixPaciMODAL" Width="68px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li class="lisobe">
                                        <label>
                                            Nº WhatsApp</label>
                                        <asp:TextBox runat="server" ID="txtNuWhatsPaciMODAL" Width="68px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li class="lisobe">
                                        <label>
                                            Pasta Controle</label>
                                        <asp:TextBox runat="server" ID="txtPastaControl" Width="141px" MaxLength="15" ToolTip="Identificação literal da pasta do paciente na instituição"></asp:TextBox>
                                    </li>
                                    <li class="lisobe" style="clear:both">
                                        <label style="color:Red" title="Telefone em formato distinto ao do sistema e geralmente migrados de outras bases">
                                            Telefone*</label>
                                        <asp:TextBox runat="server" ID="txtTelMigrado" Width="320px" MaxLength="200" ToolTip="Telefone em formato distinto ao do sistema e geralmente migrados de outras bases"></asp:TextBox>
                                    </li>
                                </ul>
                            </li>
                            <li style="clear: both; width: 198px; border-right: 1px solid #CCCCCC; margin-left: 74px;
                                height: 65px; margin-top: -8px;">
                                <ul style="margin-left: 0px" class="ulInfosGerais">
                                    <li style="margin-left: 5px; margin-bottom: 1px;">
                                        <label class="lblSubInfos">
                                            INFORMAÇÕES GERAIS</label>
                                    </li>
                                    <li style="clear: both">
                                        <asp:CheckBox runat="server" ID="chkPaciEhRespMODAL" OnCheckedChanged="chkPaciEhRespMODAL_OnCheckedChanged"
                                            AutoPostBack="true" Text="Paciente é o próprio Responsável" CssClass="chk" />
                                    </li>
                                    <li style="clear: both">
                                        <asp:CheckBox runat="server" ID="chkPaciMoraCoRespMODAL" Text="Paciente mora com o(a) Responsável"
                                            CssClass="chk" />
                                    </li>
                                    <li style="clear: both">
                                        <asp:CheckBox runat="server" ID="CheckBox4" Text="É o responsável financeiro" CssClass="chk" />
                                    </li>
                                </ul>
                            </li>
                            <li style="margin-top: -8px;">
                                <ul style="margin-left: 10px; width: 100%;" class="ulEndResiResp">
                                    <li style="margin-left: 1px; margin-bottom: 1px;">
                                        <label class="lblSubInfos">
                                            ENDEREÇO RESIDENCIAL / CORRESPONDÊNCIA</label>
                                    </li>
                                    <li style="clear: both;">
                                        <label class="lb lObrigatorio">
                                            CEP</label>
                                        <asp:TextBox runat="server" ID="txtCEPPaciMODAL" Width="55px" CssClass="campoCepF"
                                            ClientIDMode="Static"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="txtCEPPaciMODALAUXILIAR" ClientIDMode="Static" Style="display: none" />
                                    </li>
                                    <li style="margin: 11px 2px 0 -2px;">
                                        <asp:ImageButton ID="imgPesqCEPMODAL" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                            OnClick="imgPesqCEPMODAL_OnClick" Width="13px" Height="13px" />
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio">
                                            UF</label>
                                        <asp:DropDownList runat="server" ID="ddlUfMODAL" Width="40px" OnSelectedIndexChanged="ddlUfMODAL_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio">
                                            Cidade</label>
                                        <asp:DropDownList runat="server" ID="ddlCidadeMODAL" Width="160px" OnSelectedIndexChanged="ddlCidadeMODAL_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio">
                                            Bairro</label>
                                        <asp:DropDownList runat="server" ID="ddlBairroMODAL" Width="162px">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="clear: both; margin-top: -4px;">
                                        <label class="lblObrigatorio">
                                            Logradouro</label>
                                        <asp:TextBox runat="server" ID="txtLograMODAL" Width="216px" ClientIDMode="Static"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="txtLograMODALAUXILIAR" ClientIDMode="Static" Style="display: none" />
                                    </li>
                                    <li style="margin-left: 10px; margin-top: -4px;">
                                        <label>
                                            Email</label>
                                        <asp:TextBox runat="server" ID="txtEmailMODAL" Width="210px"></asp:TextBox>
                                    </li>
                                </ul>
                            </li>
                            <li style="margin: -10px 0 -5px 0px; clear: both;">
                                <label class="lblTop">
                                    DADOS DO RESPONSÁVEL PELO PACIENTE</label>
                            </li>
                            <li style="clear: both">
                                <label class="lblObrigatorio">
                                    CPF</label>
                                <asp:TextBox runat="server" ID="txtCPFRespMODAL" Style="width: 74px;" CssClass="campoCpf"
                                    ToolTip="CPF do Responsável" Text="000.000.000-00" ClientIDMode="Static"></asp:TextBox>
                            </li>
                            <li>
                                <label class="lblObrigatorio">
                                    Nome</label>
                                <asp:TextBox runat="server" ID="txtNomeRespMODAL" Width="270px" ToolTip="Nome do Responsável"></asp:TextBox>
                            </li>
                            <li>
                                <label>
                                    Sexo</label>
                                <asp:DropDownList runat="server" ID="ddlSexoRespMODAL" Width="44px" ToolTip="Selecione o Sexo do Responsável">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                                    <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li>
                                <label class="lblObrigatorio">
                                    Nascimento</label>
                                <asp:TextBox runat="server" ID="txtDtNascRespMODAL" CssClass="campoData" ClientIDMode="Static"></asp:TextBox>
                            </li>
                            <li>
                                <label>
                                    Grau Parentesco</label>
                                <asp:DropDownList runat="server" ID="ddlGrauParenMODAL" Width="100px">
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
                            <li>
                                <label>
                                    Profissão</label>
                                <asp:DropDownList runat="server" ID="ddlProfiRespMODAL" Width="100px">
                                </asp:DropDownList>
                            </li>
                            <li style="clear: both; margin: -5px 0 0 0px;">
                                <ul class="ulIdentResp">
                                    <li>
                                        <asp:Label runat="server" ID="Label12" Style="font-size: 9px;" CssClass="lblObrigatorio">Carteira de Identidade</asp:Label>
                                    </li>
                                    <li style="clear: both;">
                                        <label>
                                            Número</label>
                                        <asp:TextBox runat="server" ID="txtNuRGRespMODAL" Width="70px" ClientIDMode="Static"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Org Emiss</label>
                                        <asp:TextBox runat="server" ID="txtORGEmissRespMODAL" Width="50px" ClientIDMode="Static"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            UF</label>
                                        <asp:DropDownList runat="server" ID="ddlUFRespMODAL" Width="40px">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </li>
                            <li style="margin: -5px 0 0 10px; clear: none">
                                <ul class="ulDadosContatosResp">
                                    <li>
                                        <asp:Label runat="server" ID="Label13" Style="font-size: 9px;">Dados de Contato</asp:Label>
                                    </li>
                                    <li style="clear: both;">
                                        <label>
                                            Tel. Fixo</label>
                                        <asp:TextBox runat="server" ID="txtTelFixoRespMODAL" Width="70px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Tel. Celular</label>
                                        <asp:TextBox runat="server" ID="txtTelCelRespMODAL" Width="70px" CssClass="campoTel8dig"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Tel. Comercial</label>
                                        <asp:TextBox runat="server" ID="txtTelComRespMODAL" Width="70px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Nº WhatsApp</label>
                                        <asp:TextBox runat="server" ID="txtWhatsRespMODAL" Width="70px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Facebook</label>
                                        <asp:TextBox runat="server" ID="txtFaceRespMODAL" Width="91px"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Email</label>
                                        <asp:TextBox runat="server" ID="txtEmailRespMODAL" Width="140px"></asp:TextBox>
                                    </li>
                                </ul>
                            </li>
                            <li class="liBtnAddA" style="margin-left: 0px !important; margin-top: 0px !important;
                                clear: both !important; height: 15px;">
                                <asp:LinkButton ID="lnkCadastroCompleto" runat="server" OnClick="lnkCadastroCompleto_OnClick">
                                    <asp:Label runat="server" ID="Label23" Text="CADASTRO COMPLETO" Style="margin-left: 4px;
                                        margin-right: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li class="liBtnAddA" style="float: right; margin-top: 0px !important; height: 15px;">
                                <asp:LinkButton ID="lnkConfirmarInfoCadasMODAL" runat="server" ValidationGroup="atuEndAlu"
                                    OnClick="lnkConfirmarInfoCadasMODAL_OnClick">
                                    <asp:Label runat="server" ID="Label3" Text="CONFIRMAR" Style="margin-left: 4px; margin-right: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </ul>
            </div>
        </li>
        <%--<asp:HiddenField runat="server" ClientIDMode="Static" ID="hidLocalTriagem" Value="0" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hidLocalAtend" Value="0" />--%>
        <input type="hidden" runat="server" name="hidLocalTriagem" id="LocalTriagem" value="" />
        <input type="hidden" runat="server" name="hidLocalAtend" id="LocalAtend" value="" />
        <li>
            <div id="divConfirmExclusão" style="display: none; height: 100px !important;">
                <asp:HiddenField runat="server" ID="hidIndexGridAtend" />
                <ul>
                    <li>
                        <asp:Label ID="lblTresFaltasAnteriores" ForeColor="Red" Text="Este paciente já possui três faltas seguidas!"
                            runat="server" Visible="false" />
                    </li>
                    <li style="margin-bottom: 10px;">
                        <label>
                            Deseja já realizar o encaminhamento ?</label>
                    </li>
                    <li class="liBtnConfirm" style="margin-left: 45px; width: 30px">
                        <asp:LinkButton ID="lnkEncamSim" OnClick="lnkEncamSim_OnClick" runat="server" ToolTip="Realiza o encaminhamento do item em contexto">
                            <label style="margin-left:5px; color:White;">SIM</label>
                        </asp:LinkButton>
                    </li>
                    <li class="liBtnConfirm" style="margin: -22px 0 0 100px; width: 30px;">
                        <asp:LinkButton ID="lnkEncamNao" runat="server" ToolTip="Não realiza o encaminhamento do item em contexto">
                            <label style="margin-left:5px; color:White;">NÃO</label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divEncaminharAtend" style="display: none; height: 100px;">
                <ul>
                    <li>
                        <asp:Label ID="Label24" ForeColor="Red" Text="Este paciente já possui três faltas seguidas!"
                            runat="server" Visible="false" />
                    </li>
                      <li class="liBtnConfirm" style="margin: 12px 0 0 55px; width: 116px; cursor: pointer;">
                        <asp:LinkButton ID="LinkButton3" OnClick="lnkNaoEncaminhar_OnClick" runat="server" ToolTip="Não realiza o encaminhamento">
                            <label style="margin-left:5px; color:White; cursor:pointer;">REGISTRAR PRESENÇA</label>
                        </asp:LinkButton>
                    </li>
                    <li style="margin-left: 14px; margin-top: 5px;">
                    <label>Local de Atendimento</label>
                        <asp:DropDownList Style="width: 194px;" ClientIDMode="Static" ID="ddlLocalAtendimentoM3"
                            AutoPostBack="false" runat="server" onchange="attrLocalAtend(this);">
                        </asp:DropDownList>
                    </li>
                    <li class="liBtnConfirm" style="width: 185px; background-color: #d09ad1; margin-left: 14px;
                    cursor: pointer;">
                        <asp:LinkButton ID="lnkEncAtendimento3" OnClick="lnkEncamSim_OnClick" runat="server"
                            ToolTip="Realiza o encaminhamento para o atendimento">
                            <label style="margin-left:5px; color:White; cursor:pointer;">ENCAMINHAR PARA O ATENDIMENTO.</label>
                        </asp:LinkButton>
                    </li>
                  
                </ul>
            </div>
        </li>
        <li>
            <div id="divProximoPasso" style="display: none;">
                <ul>
                    <li>
                        <asp:Label ID="Label21" ForeColor="Red" Text="Este paciente já possui três faltas seguidas!"
                            runat="server" Visible="false" />
                    </li>
                    <li class="liBtnConfirm" style="margin: 12px 0 0 170px; width: 116px; cursor: pointer;">
                        <asp:LinkButton ID="lnkNaoEncaminhar" OnClick="lnkNaoEncaminhar_OnClick" runat="server" ToolTip="Não realiza o encaminhamento">
                            <label style="margin-left:5px; color:White; cursor:pointer;">REGISTRAR PRESENÇA</label>
                        </asp:LinkButton>
                    </li>
                     <li style="margin-left: 14px; margin-top: 5px;">
                      <label>Local de Atendimento</label>
                        <asp:DropDownList Style="width: 203px;" ClientIDMode="Static" ID="ddlLocalAtendimentoM1"
                            AutoPostBack="false" runat="server" onchange="attrLocalAtend(this);">
                        </asp:DropDownList>
                    </li>
                    <li class="liBtnConfirm" style="width: 195px; background-color: #d09ad1; margin-left: 14px;
                        margin-top: 6px; cursor: pointer;">
                        <asp:LinkButton ID="lnkEncAtendimento" OnClick="lnkEncamSim_OnClick" runat="server"
                            ToolTip="Realiza o encaminhamento para o atendimento">
                            <label style="margin-left:14px; color:White; cursor:pointer;">ENCAMINHAR PARA O ATENDIMENTO</label>
                        </asp:LinkButton>
                    </li>
                     <li style="margin-left: 250px; margin-top: -60px;">
                      <label>Local de Avaliação</label>
                        <asp:DropDownList ClientIDMode="Static" AutoPostBack="false" Style="width: 203px;"
                            ID="ddlLocalTriagemM1" runat="server" onchange="attrLocalTriagem(this);">
                        </asp:DropDownList>
                    </li>
                    <li class="liBtnConfirm" style="margin: 6px 0 0 250px; width: 200px; background-color: #656363;
                        cursor: pointer;">
                        <asp:LinkButton ID="lnkEncTriagem" OnClick="lnkEncamTriagem_OnClick" runat="server"
                            ToolTip="Realiza o encaminhamento para a triagem/acolhimento">
                            <label style="margin-left:3px; color:White; cursor:pointer;">ENCAMINHAR PARA AVALIAÇÃO DE RISCO</label>
                        </asp:LinkButton>
                    </li>
                  
                </ul>
            </div>
        </li>
        <li>
            <div id="divEncaminhamento" style="display: none; height: 60px !important;">
                <ul>
                    <li class="liBtnConfirm" style="width: 185px; background-color: #d09ad1; margin-left: 14px;
                        margin-top: 8px; cursor: pointer;">
                        <asp:LinkButton ID="lnkDirEncAtendimento" OnClick="lnkEncamSim_OnClick" runat="server"
                            ToolTip="Realiza o encaminhamento para o atendimento">
                            <label style="margin-left:5px; color:White; cursor:pointer;">ENCAMINHAR PARA O ATENDIMENTO.</label>
                        </asp:LinkButton>
                    </li>
                    <li style="margin-top: 5px; margin-left: 14px;">
                        <asp:DropDownList Style="width: 193px;" AutoPostBack="false" ClientIDMode="Static"
                            ID="ddlLocalAtendimentoM2" runat="server" onchange="attrLocalTriagem(this);">
                        </asp:DropDownList>
                    </li>
                    <li class="liBtnConfirm" style="margin: -47px 0 0 231px; width: 230px; background-color: #656363;
                        cursor: pointer;">
                        <asp:LinkButton ID="lnkDirEncTriagem" OnClick="lnkEncamTriagem_OnClick" runat="server"
                            ToolTip="Realiza o encaminhamento para a triagem/acolhimento">
                            <label style="margin-left:5px; color:White; cursor:pointer;">ENCAMINHAR PARA A TRIAGEM/ACOLHIMENTO.</label>
                        </asp:LinkButton>
                    </li>
                    <li style="margin-left: 231px; margin-top: 5px;">
                        <asp:DropDownList ClientIDMode="Static" AutoPostBack="false" Style="width: 238px;"
                            ID="ddlLocalTriagemM2" runat="server" onchange="attrLocalTriagem(this);">
                        </asp:DropDownList>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divLoadShowLogAgenda" style="display: none; height: 340px !important;">
                <ul class="ulDadosLog">
                    <li>
                        <label>
                            Paciente</label>
                        <asp:TextBox runat="server" ID="txtNomePaciMODLOG" Enabled="false" Width="220px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Sexo</label>
                        <asp:TextBox runat="server" ID="txtSexoMODLOG" Enabled="false" Width="10px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Idade</label>
                        <asp:TextBox runat="server" ID="txtIdadeMODLOG" Enabled="false" Width="70px"></asp:TextBox>
                    </li>
                    <li style="clear: both; margin-left: -5px !important; margin-top: -2px;">
                        <div style="width: 890px; height: 305px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdLogAgendamento" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum Questionário associado<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="Data_V" HeaderText="DATA">
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_PROFI" HeaderText="RESPONSÁVEL">
                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_TIPO" HeaderText="TIPO">
                                        <ItemStyle HorizontalAlign="Left" Width="65px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemStyle Width="18px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <img src='<%# Eval("CAMINHO_IMAGEM") %>' alt="" style="width: 16px !important; height: 16px !important;
                                                margin: 0 0 0 0 !important" title="Representação gráfica da Ação" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DE_TIPO" HeaderText="AÇÃO">
                                        <ItemStyle HorizontalAlign="Left" Width="240px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Observação">
                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtObser" TextMode="MultiLine" Style="margin: 0 0 0 0 !important;
                                                height: 23px !important; width: 180px" ReadOnly="true" Text='<%# Eval("OBS") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divFichaAtendimento" style="display: none; height: 220px !important;">
                <ul class="ulDados" style="width: 420px; margin-top: 0px !important">
                    <li>
                        <label title="Paciente">
                            Paciente</label>
                        <asp:DropDownList ID="drpPacienteFicha" runat="server" Width="240px" />
                    </li>
                    <li>
                        <label>
                            Queixas</label>
                        <asp:TextBox runat="server" ID="txtQxsFicha" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Observação</label>
                        <asp:TextBox runat="server" ID="txtObsFicha" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
                    </li>
                    <li id="li4" runat="server" class="liBtnAddA" style="float: right; margin-top: 10px !important;
                        clear: none !important; height: 15px;">
                        <asp:LinkButton ID="lnkbImprimirFicha" runat="server" OnClick="lnkbImprimirFicha_Click"
                            ToolTip="Imprimir ficha de atendimento">
                            <asp:Label runat="server" ID="lblEmitirFicha" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divGuiaPlano" style="display: none; height: 180px !important;">
                <ul class="ulDados" style="width: 420px; margin-top: 0px !important">
                    <li style="margin-left: 5px;">
                        <label>
                            Nº Prontuário</label>
                        <asp:CheckBox runat="server" ID="chkProntuarioGuia" Style="margin-left: -5px" ClientIDMode="Static" />
                        <asp:TextBox runat="server" ID="txtProntuarioGuia" CssClass="campoNire" Width="50px" Style="margin-left: -7px" Enabled="false" ClientIDMode="Static" />
                    </li>
                    <li>
                        <label>
                            CPF</label>
                        <asp:CheckBox runat="server" ID="chkCpfGuia" Style="margin-left: -5px" ClientIDMode="Static" />
                        <asp:TextBox runat="server" ID="txtCpfGuia" CssClass="campoCpf" Width="75px" Style="margin-left: -7px" Enabled="false" ClientIDMode="Static" />
                    </li>
                    <li style="margin-left: 8px;">
                        <label for="ddlNomeUsuAteMed" title="Nome do usuário selecionado" class="lblObrigatorio">
                            Paciente</label>
                        <asp:DropDownList ID="drpPacienteGuia" runat="server" ToolTip="Paciente para o qual a consulta será marcada"
                            Width="210px" Visible="false" OnSelectedIndexChanged="drpPacienteGuia_OnSelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtPacienteGuia" Width="210px" ToolTip="Digite o nome ou parte do nome do paciente" runat="server" ClientIDMode="Static" />
                    </li>
                    <li style="margin-top: 13px; margin-left: -4px;">
                        <asp:ImageButton ID="imgPesqPacienteGuia" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                            OnClick="imgPesqPacienteGuia_OnClick" />
                        <asp:ImageButton ID="imgVoltarPesqPacienteGuia" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                            OnClick="imgVoltarPesqPacienteGuia_OnClick" Visible="false" runat="server" />
                    </li>
                    <li style="clear: both;">
                        <label title="Operadora">
                            Operadora</label>
                        <asp:DropDownList ID="drpOperGuia" runat="server" Width="90px" />
                    </li>
                    <li>
                        <label title="Data Comparecimento" class="lblObrigatorio">
                            Data Guia</label>
                        <asp:TextBox ID="txtDtGuia" runat="server" ValidationGroup="guia" class="campoData" ToolTip="Informe a data de comparecimento" />
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="guia" ID="rfvDtGuia" Display="Dynamic"
                            CssClass="validatorField" ErrorMessage="O campo data é requerido" ControlToValidate="txtDtGuia"></asp:RequiredFieldValidator>
                    </li>
                    <li style="margin-left: 10px;">
                        <label>Responsável Técnico GUIA</label>
                        <asp:DropDownList ID="drpProfissionalGuia" Width="210px" runat="server" />
                    </li>
                    <li style="clear: both; margin-top: 5px;">
                        <label>
                            <asp:CheckBox runat="server" ID="chkGuiaConsol" />
                            Guia com procedimentos consolidados</label>
                    </li>
                    <li style="margin-top: 5px; margin-left: 7px;">
                        <asp:TextBox runat="server" class="campoData" ID="txtDtGuiaIni" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                        <asp:Label runat="server" ID="Label25"> &nbsp até &nbsp </asp:Label>
                        <asp:TextBox runat="server" class="campoData" ID="txtDtGuiaFim" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                    </li>
                    <li style="margin-top: 5px; margin-left: -4px;">
                        <asp:ImageButton ID="imgPesqPeriodoGuia" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                            OnClick="imgPesqPeriodoGuia_OnClick" />
                    </li>
                    <li style="clear: both;">
                        <label title="Agendamentos"> Agendamentos</label>
                        <asp:DropDownList ID="ddlAgendGuia" runat="server" Width="100%" />
                    </li>
                    <li style="clear: both;">
                        <label title="Observações">
                            Observações / Justificativa</label>
                        <asp:TextBox ID="txtObsGuia" Width="402px" Height="40px" TextMode="MultiLine" MaxLength="180"
                            runat="server" />
                    </li>
                    <li class="liBtnAddA" style="clear: none !important; margin-left: 180px !important;
                        margin-top: 8px !important; height: 15px;">
                        <asp:LinkButton ID="lnkbImprimirGuia" runat="server" ValidationGroup="guia" OnClick="lnkbImprimirGuia_OnClick"
                            ToolTip="Imprimir guia do plano de saúde">
                            <asp:Label runat="server" ID="lblEmitirGuia" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <%--modal encaixe--%>
        <li>
            <div id="divEncaixe" style="display: none; height: 310px !important;">
            <div >
                <ul>
                    <li>
                        <label for="ddlNomeUsuAteMed" title="Nome do usuário selecionado" class="lblObrigatorio">
                            Profissional</label>
                        <asp:DropDownList ID="ddlNomeProf" runat="server" ToolTip="Profissional para o qual a consulta será marcada"
                            Width="230px" Visible="false" OnSelectedIndexChanged="ddlNomeProf_OnSelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtNomePorfPesq" Width="230px" ToolTip="Digite o nome ou parte do nome do profissional"
                            runat="server" />
                             <asp:ImageButton ID="imgbPesqProfNome" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                            OnClick="imgbPesqProfNome_OnClick" />
                        <asp:ImageButton ID="imgbVoltarPesq" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                            OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" />
                            </li>
                               <li style="margin-top: 11px; margin-left: -4px;">
                       
                    </li>
                </ul>
                </div>
                <%-- <asp:GridView ID="grdHorariosEncaixe" CssClass="grdBusca headFixo" runat="server" Style="width: 100%;
                            height: 260px; position: relative;" AutoGenerateColumns="false" AllowPaging="false"
                            GridLines="Vertical" OnRowDataBound="grdHorariosEncaixe_OnRowDataBound">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhuma horario nesses parâmetros<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="hora" HeaderText="DATA/HORA">
                                    <ItemStyle Width="75px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_PAC" HeaderText="PACIENTE">
                                    <ItemStyle Width="185px" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>--%>
            </div>
        </li>
        <%--modal encaixe--%>
        <li>
            <div id="divMovimentacao" style="display: none; height: 310px !important;">
                <ul class="ulDados" style="width: 690px; margin-top: 0px !important">
                    <li>
                        <label title="Profissional de Origem" class="lblObrigatorio">
                            Origem</label>
                        <asp:DropDownList ID="drpProfiOrig" runat="server" Width="240px" ValidationGroup="movimentacao"
                            ToolTip="Profissional de Origem" AutoPostBack="true" OnSelectedIndexChanged="CarregarDadosMovimentacao" />
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="movimentacao" ID="rfvProfiOrig"
                            CssClass="validatorField" ErrorMessage="O campo data é requerido" ControlToValidate="drpProfiOrig"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label title="Data da Movimentação de Origem" class="lblObrigatorio">
                            Data</label>
                        <asp:TextBox ID="txtDtMovimOrigem" runat="server" ValidationGroup="movimentacao"
                            class="campoData" ToolTip="Informe a data da movimentação de origem" AutoPostBack="true"
                            OnTextChanged="CarregarDadosMovimentacao" />
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="movimentacao" ID="rfvDtMovimOrigem"
                            CssClass="validatorField" ErrorMessage="O campo data é requerido" ControlToValidate="txtDtMovimOrigem"></asp:RequiredFieldValidator>
                    </li>
                    <li style="margin-left: 35px;">
                        <label title="Profissional de Destino" class="lblObrigatorio">
                            Destino</label>
                        <asp:DropDownList ID="drpProfiDest" runat="server" Width="240px" ValidationGroup="movimentacao"
                            ToolTip="Profissional de Destino" AutoPostBack="true" OnSelectedIndexChanged="CarregarDadosMovimentacao" />
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="movimentacao" ID="rfvProfiDest"
                            CssClass="validatorField" ErrorMessage="O campo data é requerido" ControlToValidate="drpProfiDest"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label title="Data da Movimentação de Destino" class="lblObrigatorio">
                            Data</label>
                        <asp:TextBox ID="txtDtMovimDestino" runat="server" ValidationGroup="movimentacao"
                            class="campoData" ToolTip="Informe a data da movimentação de destino" AutoPostBack="true"
                            OnTextChanged="CarregarDadosMovimentacao" />
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="movimentacao" ID="rfvDtMovimDestino"
                            CssClass="validatorField" ErrorMessage="O campo data é requerido" ControlToValidate="txtDtMovimDestino"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <div style="border: 1px solid #CCCCCC; width: 680px; height: 220px; overflow-y: scroll;
                            margin-top: 10px;">
                            <asp:GridView ID="grdPacMovimentacoes" CssClass="grdBusca" runat="server" Style="width: 100% !important;
                                cursor: default" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                OnRowDataBound="grdPacMovimentacoes_OnRowDataBound">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum paciente disponivel<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="CK">
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hidIdAgendHorar" Value='<%# Eval("ID_AGEND_HORAR") %>' />
                                            <asp:CheckBox ID="chkPaciente" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="hora" HeaderText="DATA/HORA">
                                        <ItemStyle Width="75px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="H. DESTINO">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drpHoraDest" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                                runat="server" ToolTip="Horários marcados com '*' estão agendados para pacientes." />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NO_PAC" HeaderText="PACIENTE">
                                        <ItemStyle Width="210px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_RESP" HeaderText="RESPONSÁVEL">
                                        <ItemStyle Width="190px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TELEFONE_RESP" HeaderText="">
                                        <ItemStyle Width="70px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                    <li><span>(*) - Existe agendamento para este horário.</span> <span style="margin: 0 0 0 150px;">
                        A movimentação de paciente em horário iguais deve ser individual.</span> </li>
                    <li id="li10" runat="server" class="liBtnAddA" style="margin-top: 10px !important;
                        clear: none !important; height: 15px;">
                        <asp:LinkButton ID="lnkbMovimentar" runat="server" OnClick="lnkbMovimentar_OnClick"
                            ValidationGroup="movimentacao" ToolTip="Movimentar paciente de horário">
                            <asp:Label runat="server" ID="Label20" Text="MOVIMENTAR" Style="margin-left: 5px;
                                margin-right: 5px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divEncaminAtend" style="display: none; height: 100px !important; width: 270px">
                <asp:HiddenField ID="hidAgendSelec" runat="server" />
                <ul>
                    <li style="margin-bottom: 10px;">Deseja encaminhar o paciente para atendimento?
                    </li>
                    <li class="liBtnConfirm" style="margin-left: 85px; width: 30px">
                        <asp:LinkButton ID="lnkbAtendSim" OnClick="lnkbAtendSim_OnClick" runat="server" ToolTip="Confirma o encaminhamento do paciente para atendimento">
                            <label style="margin-left:5px; color:White;">SIM</label>
                        </asp:LinkButton>
                    </li>
                    <li class="liBtnConfirm" style="margin: -22px 0 0 135px; width: 30px;">
                        <asp:LinkButton ID="lnkbAtendNao" runat="server" ToolTip="Seleciona o paciente e mostra a situação atual do atendimento">
                            <label style="margin-left:5px; color:White;">NÃO</label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divFinalizarAgend" style="display: none; height: 438px !important;">
                <ul class="" style="width: auto; margin-top: 0px !important; float: left;">
                    <li style="margin: 8px 0 0 5px;">
                        <asp:TextBox runat="server" Width="400px" Font-Bold="true" style="border:none" ID="txtUnidadeFinalizar"></asp:TextBox>
                    </li>
                    <li class="" style="margin: 0 0 0 17px">
                        <label class="lblObrigatorio" style="margin: 5px;" title="Período dentro do qual existem agendamentos cadastrados">
                            Período</label>
                        <asp:TextBox ID="dtIniMod" runat="server" Style="margin: 0 0 0 4px;" ToolTip="Informe a data de início da agenda"
                            CssClass="campoData">
                        </asp:TextBox>
                        <span style="margin: -20px 2px -12px 0;">até</span>
                        <asp:TextBox ID="dtFimMod" runat="server" ToolTip="Informe a data de término da agenda"
                            CssClass="campoData">
                        </asp:TextBox>
                    </li>
                    <li style="margin: -33px 0 0 203px;">
                        <label title="Nome do profissional que atendeu o paciente">
                            Profissional</label>
                        <asp:TextBox runat="server" ID="txtProSolicitado" Style="margin: 0; width: 177px;"></asp:TextBox>
                        <asp:ImageButton ID="imgPesProfSolicitado" ToolTip="Pesquisar nome do profissional" runat="server"
                            ImageUrl="~/Library/IMG/IC_PGS_Recepcao_Avaliador.png" OnClick="imgPesProfSolicitado_OnClick" />
                        <asp:DropDownList ID="drpProSolicitado" Width="177px" runat="server" Visible="false"/>
                        <asp:ImageButton ID="imgVoltarPesProfSOlicitado" ValidationGroup="pesqPac" Width="16px"
                            Height="16px" Style="margin: 0 0 -4px 0px;" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                            OnClick="imgVoltarPesProfSOlicitado_OnClick" Visible="false" runat="server" />                        
                    </li>
                    <li style="margin: -29px 0 0 407px;">
                        <label title="Nome do profissional que atendeu o paciente">
                            Paciente</label>
                        <asp:TextBox runat="server" ID="txtNomePaciente" Style="margin: 0; width: 177px;" ToolTip="Digite o nome ou parte do nome do paciente, no mínimo 3 letras."></asp:TextBox>
                        <asp:ImageButton ID="imgPesqPaciente" ToolTip="Pesquisar nome do paciente" runat="server"
                            ImageUrl="~/Library/IMG/IC_PGS_Recepcao_CadPacien.png" OnClick="imgPesqPaciente_OnClick" />
                        <asp:DropDownList ID="ddlPacienteFinalizar" Width="177px" runat="server" Visible="false"/>
                        <asp:ImageButton ID="imgVoltarPesqPaciente" ValidationGroup="pesqPac" Width="16px"
                            Height="16px" Style="margin: 0 0 -4px 0px;" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                            OnClick="imgVoltarPesqPaciente_OnClick" Visible="false" runat="server" />
                        <asp:ImageButton ID="imgPesqAgendMod" runat="server" Style="margin: -20px 0 0 20px;" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                            OnClick="imgPesqAgendMod_OnClick" ToolTip="Preencher grid pelo período e/ou profissional e/ou paciente"/>
                    </li>
                    <li style="margin-top: 17px; clear: both; margin-left: 4px;">
                        <div id="div1" runat="server" class="divGridData" style="height: 315px; width: 665px;
                            overflow-y: scroll !important; border: 1px solid #ccc;">
                            <asp:HiddenField ID="hidCoColMod" Value="0" runat="server" />
                            <asp:GridView ID="grdAgendMod" CssClass="grdBusca" runat="server" Style="width: 100%;
                                height: 25px;" AutoGenerateColumns="false">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum registro encontrado.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                 <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label title="Selecione o atendimento a ser finalizado">
                                                Ck</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hidIdAgendHorar" Value='<%# Eval("ID_AGEND_HORAR") %>'/>
                                            <asp:CheckBox runat="server" ID="chkFinalizar" ToolTip="Finalizar este atendimento"/>
                                        </ItemTemplate>
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label title="Data do agendamento">
                                                DATA</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtDtAgendMod" Text='<%# Eval("DE_DT_AGEND") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label title="Hora em que ocorreu o agendamento">
                                                HORA</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtHrAgendMod" Text='<%# Eval("HR_AGEND") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label title="Paciente referente do horário agendado">
                                                PACIENTE</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtPaciAgendMod" Text='<%# Eval("NO_PACI") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label title="Número da pasta de controle do paciente se houver">
                                                PASTA</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtPastaAgenMod" Text='<%# Eval("PASTA") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label title="Nome do profissional de atendimento">
                                                PROFISSIONAL</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtProfAgendMod" Text='<%# Eval("NO_PROF") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label title="Local do agendamento">
                                                LOCAL</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtLocalAgendMod" Text='<%# Eval("LOCAL") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label title="Situação do agendamento">
                                                SITUAÇÃO</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtSitAgendMod" Text='<%# Eval("DE_SITU") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                    <li class="liBtnFinAten" style="float: right;margin: 14px 0 0 0;height: 15px; background-color: #0b3e6e;">
                         <asp:LinkButton ID="lnkFinalizarAgend" runat="server" OnClick="lnkFinalizarAgend_OnClick" ToolTip="Finalizar atendlimento">
                            <asp:Label runat="server" ID="Label26" Text="FINALIZAR" Style="font-weight:bold;margin-left: 5px;
                            margin-right: 5px; color: White;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
    </ul>
    <script type="text/javascript">
        window.onload = function () {
            //$("#divOcorrencia").show();

            var div = document.getElementById("divAgendaAt");
            var div_position = document.getElementById("divAgendaAt_posicao");
            var position = parseInt('<%= Request.Form["divAgendaAt_posicao"] %>');
            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }

        function AbreFinalizarAgend() {
            $('#divFinalizarAgend').dialog({ autoopen: false, modal: true, left: 221, top: 22, width: 701, height: 470, resizable: false, title: "FINALIZAR AGENDAMENTOS",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalLog() {
            $('#divLoadShowLogAgenda').dialog({ autoopen: false, modal: true, width: 902, height: 340, resizable: false, title: "HISTÓRICO DO AGENDAMENTO DE ATENDIMENTO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalInfoPlano() {
            $('#divLoadShowInfoPlano').dialog({ autoopen: false, modal: true, width: 444, height: 160, resizable: false, title: "INFORMAÇÕES DO PLANO DE SAÚDE",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalInfoProcedimento() {
            $('#divLoadShowInfoProcedimento').dialog({ autoopen: false, modal: true, width: 444, height: 180, resizable: false, title: "INFORMAÇÕES DE PROCEDIMENTO DE SAÚDE",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalCancelamentoAgenda() {
            $('#divLoadShowCancelaAgenda').dialog({ autoopen: false, modal: true, width: 444, height: 170, resizable: false, title: "CANCELAMENTO DE ITEM DE AGENDA",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalInfosCadastrais() {
            $('#divLoadShowInfosCadasPaciente').dialog({ autoopen: false, modal: true, width: 800, height: 310, resizable: false, title: "INFORMAÇÕES CADASTRAIS DE PACIENTE E RESPONSÁVEL",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModal() {
            $('#divConfirmExclusão').dialog({ autoopen: false, modal: true, width: 210, height: 100, resizable: false, title: "REALIZAR ENCAMINHAMENTO ?",
                //                open: function () { $('#divLoadRegisOcorr').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalAtend() {
            $('#divEncaminharAtend').dialog({ autoopen: false, modal: true, width: 248, height: 140, resizable: false, title: "REALIZAR ENCAMINHAMENTO ?",
                //                open: function () { $('#divLoadRegisOcorr').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreProximoPasso() {
            $('#divProximoPasso').dialog({ autoopen: false, modal: true, width: 480, height: 150, resizable: false, title: "QUAL AÇÃO QUE DESEJA TOMAR ?",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalEncaminhamento() {
            $('#divEncaminhamento').dialog({ autoopen: false, modal: true, width: 503, height: 90, resizable: false, title: "PARA ONDE DESEJA ENCAMINHAR ?",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalInfosCadasPac() {
            $('#divInfosCadasPac').dialog({ autoopen: false, modal: true, width: 820, height: 315, resizable: false, title: "INFORMAÇÕES CADASTRAIS DE PACIENTE",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalInfosCadasResp() {
            $('#divInfosCadasResp').dialog({ autoopen: false, modal: true, width: 820, height: 260, resizable: false, title: "INFORMAÇÕES CADASTRAIS DE PACIENTE",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalSelAvaliador() {
            $('#divSelAvaliador').dialog({ autoopen: false, modal: true, width: 840, height: 270, resizable: false, title: "INFORMAÇÕES CADASTRAIS DE PACIENTE",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalRegisItens() {
            $('#divRegisItens').dialog({ autoopen: false, modal: true, width: 830, height: 270, resizable: false, title: "INFORMAÇÕES CADASTRAIS DE PACIENTE",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalMovimentacao() {
            $('#divMovimentacao').dialog({ autoopen: false, modal: true, width: 735, height: 340, resizable: false, title: "MOVIMENTAÇÃO DE PACIENTE",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }
        function AbreModalEncaixe() {
            $('#divEncaixe').dialog({ autoopen: false, modal: true, width: 735, height: 340, resizable: false, title: "ENCAIXE DE PACIENTE",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }
        function AbreModalFichaAtendimento() {
            $('#divFichaAtendimento').dialog({ autoopen: false, modal: true, width: 510, height: 330, resizable: false, title: "IMPRESSÃO DA FICHA DE ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalGuiaPlano() {
            $('#divGuiaPlano').dialog({ autoopen: false, modal: true, width: 470, height: 250, top: 200, resizable: false, title: "IMPRESSÃO DA GUIA DE ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalEncamAtend() {
            $('#divEncaminAtend').dialog({ autoopen: false, modal: true, width: 280, height: 80, resizable: false, title: "ENCAMINHAMENTO PARA ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function attrLocalAtend(eval) {
            var valor = $('#' + eval.id + '').val();
            $('#ctl00_content_LocalAtend').val(valor);
            $("#ddlLocalTriagemM1").val("");
        }

        function attrLocalTriagem(eval) {
            var valor = $('#' + eval.id + '').val();
            $('#ctl00_content_LocalTriagem').val(valor);
            $("#ddlLocalAtendimentoM1").val("");
        }

        $(document).ready(function () {
            CarregamentosPadroes();
            $(".campoNire").mask("?999999999");
            $(".campoNis").mask("999999999999999?9");
            $(".campoCpf").mask("999.999.999-99");
            $(".nuGuia").mask("?999999999999999");
            $(".nuQtde").mask("9?99");
            $(".campoHora").mask("99:99");
            $(".campoCepF").mask("99999-999");
            //            $(".campoTel8dig").mask("(99)9999-9999?9");
            //            $(".campoTel").mask("(99)9999-9999");
            $('.campoTel8dig').focusout(function () {
                var phone, element;
                element = $(this);
                element.unmask();
                phone = element.val().replace(/\D/g, '');
                if (phone.length > 10) {
                    element.mask("(99)99999-999?9");
                } else {
                    element.mask("(99)9999-9999?9");
                }
            }).trigger('focusout');
            $('.campoTel').focusout(function () {
                var phone, element;
                element = $(this);
                element.unmask();
                phone = element.val().replace(/\D/g, '');
                if (phone.length > 10) {
                    element.mask("(99)99999-999?9");
                } else {
                    element.mask("(99)9999-9999?9");
                }
            }).trigger('focusout');
            $(".campoDin").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $("#txtAltura").mask("9,99");
            $("#txtPeso").maskMoney({ symbol: "", decimal: ",", thousands: "." });

            //            $("#ddlLocalAtendimentoM1").change(function () {
            //                debugger;
            //                var idLocal = this.value;
            //                document.getElementById('hidLocalAtend').value = idLocal;
            //                $('#hidLocalAtend').val(idLocal);
            //            });

            //            $("#ddlLocalAtendimentoM2").change(function () {
            //                var idLocal = this.value;
            //                $("#hidLocalAtend").val(idLocal);
            //            });

            //            $("#ddlLocalTriagemM1").change(function () {
            //                var idLocal = this.value;
            //                $("#hidLocalTriagem").val(idLocal);
            //            });

            //            $("#ddlLocalTriagemM2").change(function () {
            //                var idLocal = this.value;
            //                $("#hidLocalTriagem").val(idLocal);
            //            });

        });

        $("#txtPacienteGuia").keyup(function () {
            $("#chkCpfGuia").attr("checked", false);
            $("#txtCpfGuia").enable(false);
            $("#txtCpfGuia").val("");
            $("#chkProntuarioGuia").attr("checked", false);
            $("#txtProntuarioGuia").enable(false);
            $("#txtProntuarioGuia").val("");
        });

        $("#chkProntuarioGuia").change(function () {
            $("#txtProntuarioGuia").enable(true);
            $("#txtProntuarioGuia").focus();
            $("#chkCpfGuia").attr("checked", false);
            $("#txtCpfGuia").enable(false);
            $("#txtCpfGuia").val("");
            $("#txtPacienteGuia").val("");
        });

        $("#chkCpfGuia").change(function () {
            $("#txtCpfGuia").enable(true);
            $("#txtCpfGuia").focus();
            $("#chkProntuarioGuia").attr("checked", false);
            $("#txtProntuarioGuia").enable(false);
            $("#txtProntuarioGuia").val("");
            $("#txtPacienteGuia").val("");
        });

        function LimparDadosGuia() {
            $("#txtPacienteGuia").val("");
            $("#chkCpfGuia").attr("checked", false);
            $("#txtCpfGuia").val("");
            $("#txtCpfGuia").enable(false);
            $("#chkProntuarioGuia").attr("checked", false);
            $("#txtProntuarioGuia").val("");
            $("#txtProntuarioGuia").enable(false);


        };

        function CarregamentosPadroes() {
            //CPF do responsável
            $("#txtCPFResp").focus(function () {
                if ($(this).val() == "000.000.000-00")
                    $(this).val("");
            });
            $("#txtCPFResp").blur(function () {
                if ($(this).val() == "")
                    $(this).val("000.000.000-00");
            });

            //Data de Nascimento
            $("#txtDtNascResp").focus(function () {
                if ($(this).val() == "01/01/1900")
                    $(this).val("");
            });
            $("#txtDtNascResp").blur(function () {
                if ($(this).val() == "")
                    $(this).val("01/01/1900");
            });

            //Número RG
            $("#txtNuIDResp").focus(function () {
                if ($(this).val() == "000000")
                    $(this).val("");
            });
            $("#txtNuIDResp").blur(function () {
                if ($(this).val() == "")
                    $(this).val("000000");
            });

            //Órgão do RG do Responsável
            $("#txtOrgEmiss").focus(function () {
                if ($(this).val() == "SSP")
                    $(this).val("");
            });
            $("#txtOrgEmiss").blur(function () {
                if ($(this).val() == "")
                    $(this).val("SSP");
            });

            //CEP padrão igual o da unidade
            $("#txtCEP").focus(function () {
                var cepPad = $("#txtCEP_PADRAO").val().replace("-", "");
                var cep = $("#txtCEP").val().replace("-", "");

                if (cep == cepPad)
                    $(this).val();

                //alert(cepPad + "---------" + cep);
            });
            $("#txtCEP").blur(function () {
                var cepPad = $("#txtCEP_PADRAO").val().replace("-", "");
                var cep = $("#txtCEP").val().replace("-", "");
                if (cep == "")
                    $(this).val(cepPad);
            });

            //Logradouro padrão igual o da unidade
            $("#txtLograEndResp").focus(function () {
                if ($(this).val() == $("#txtLograEndResp_PADRAO").val())
                    $(this).val("");
            });
            $("#txtLograEndResp").blur(function () {
                if ($(this).val() == "")
                    $(this).val($("#txtLograEndResp_PADRAO").val());
            });
        }
    </script>
</asp:Content>
