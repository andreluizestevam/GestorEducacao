unit U_FrmRelFicCadFuncionario;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls, U_DataModuleSGE,
  QRPDFFilter;

const
  OffsetMemoryStream : Int64 = 0;


type
  TFrmRelFicCadFuncionario = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel46: TQRLabel;
    QRLabel47: TQRLabel;
    QRLabel48: TQRLabel;
    QRLabel49: TQRLabel;
    QRLabel50: TQRLabel;
    QRDBText7: TQRDBText;
    QrlMatricula: TQRLabel;
    QRLIdade: TQRLabel;
    QryFreqFunc: TADOQuery;
    QryFreqFuncCO_COL: TIntegerField;
    QryFreqFuncFLA_PRESENCA: TStringField;
    QryFreqFuncDT_FREQ: TDateTimeField;
    QryTF: TADOQuery;
    QryTFCO_COL: TIntegerField;
    QryTFFLA_PRESENCA: TStringField;
    QryTFDT_FREQ: TDateTimeField;
    QryTFtotJan: TIntegerField;
    QryTFtotFev: TIntegerField;
    QryTFtotMar: TIntegerField;
    QryTFtotAbr: TIntegerField;
    QryTFtotMai: TIntegerField;
    QryTFtotJun: TIntegerField;
    QryTFtotJul: TIntegerField;
    QryTFtotAgo: TIntegerField;
    QryTFtotSet: TIntegerField;
    QryTFtotOut: TIntegerField;
    QryTFtotNov: TIntegerField;
    QryTFtotDez: TIntegerField;
    QRDBText1: TQRDBText;
    QRDBText2: TQRDBText;
    QryRelatorioNO_INST: TStringField;
    QryRelatorioNO_TPCON: TStringField;
    QryRelatorioNO_FUN: TStringField;
    QryRelatorioNO_DEPTO: TStringField;
    QryRelatorioDE_ESPEC: TStringField;
    QryRelatorioCO_EMP: TIntegerField;
    QryRelatorioCO_COL: TAutoIncField;
    QryRelatorioORG_CODIGO_ORGAO: TIntegerField;
    QryRelatorioNO_COL: TStringField;
    QryRelatorioCO_MAT_COL: TStringField;
    QryRelatorioNO_APEL_COL: TStringField;
    QryRelatorioDT_NASC_COL: TDateTimeField;
    QryRelatorioCO_SEXO_COL: TStringField;
    QryRelatorioNU_CPF_COL: TStringField;
    QryRelatorioCO_RG_COL: TStringField;
    QryRelatorioCO_EMIS_RG_COL: TStringField;
    QryRelatorioCO_ESTA_RG_COL: TStringField;
    QryRelatorioDT_EMIS_RG_COL: TDateTimeField;
    QryRelatorioDE_ENDE_COL: TStringField;
    QryRelatorioNU_ENDE_COL: TIntegerField;
    QryRelatorioDE_COMP_ENDE_COL: TStringField;
    QryRelatorioNO_BAIR_ENDE_COL: TStringField;
    QryRelatorioNO_CIDA_ENDE_COL: TStringField;
    QryRelatorioCO_ESTA_ENDE_COL: TStringField;
    QryRelatorioNU_CEP_ENDE_COL: TStringField;
    QryRelatorioNU_TELE_RESI_COL: TStringField;
    QryRelatorioNU_TELE_CELU_COL: TStringField;
    QryRelatorioCO_TPCAL: TIntegerField;
    QryRelatorioDT_CADA_COL: TDateTimeField;
    QryRelatorioDT_INIC_ATIV_COL: TDateTimeField;
    QryRelatorioDT_TERM_ATIV_COL: TDateTimeField;
    QryRelatorioCO_EMAI_COL: TStringField;
    QryRelatorioCO_WEB_COL: TStringField;
    QryRelatorioIM_FOTO_COL: TBlobField;
    QryRelatorioCO_FUN: TIntegerField;
    QryRelatorioCO_INST: TIntegerField;
    QryRelatorioCO_TPCON: TIntegerField;
    QryRelatorioCO_DEPTO: TIntegerField;
    QryRelatorioCO_ESPEC: TIntegerField;
    QryRelatorioCO_SITU_COL: TStringField;
    QryRelatorioDT_SITU_COL: TDateTimeField;
    QryRelatorioFLA_PROFESSOR: TStringField;
    QryRelatorioNU_TIT_ELE: TStringField;
    QryRelatorioNU_ZONA_ELE: TStringField;
    QryRelatorioNU_SEC_ELE: TStringField;
    QryRelatorioNOM_USUARIO: TStringField;
    QryRelatorioDT_ALT_REGISTRO: TDateTimeField;
    QryRelatorioCO_CURFORM: TIntegerField;
    QryRelatorioCO_CIDADE: TIntegerField;
    QryRelatorioCO_BAIRRO: TIntegerField;
    QryRelatorioNU_CARGA_HORARIA: TIntegerField;
    QryRelatorioCO_ESTADO_CIVIL: TStringField;
    QryRelatorioTP_DEF: TStringField;
    QryRelatorioCO_ESTA_RG_TIT: TStringField;
    QryRelatorioVL_SALAR_COL: TFloatField;
    QryRelatorioTIPO_SITU: TStringField;
    QryRelatorioCO_PIS_PASEP: TBCDField;
    QryRelatorioCO_CTPS_NUMERO: TIntegerField;
    QryRelatorioCO_CTPS_SERIE: TIntegerField;
    QryRelatorioCO_CTPS_VIA: TIntegerField;
    QryRelatorioCO_CTPS_UF: TStringField;
    QryRelatorioCO_CNH_NREG: TBCDField;
    QryRelatorioCO_CNH_NDOC: TBCDField;
    QryRelatorioCO_CNH_CATEG: TStringField;
    QryRelatorioCO_CNH_VALID: TDateTimeField;
    QryRelatorioNO_FUNC_MAE: TStringField;
    QryRelatorioNO_FUNC_PAI: TStringField;
    QryRelatorioTP_RACA: TStringField;
    QryRelatorioNU_CARTAO_SAUDE: TBCDField;
    QryRelatorioCO_UNID: TIntegerField;
    QryRelatorioNU_FOTO_FUNC: TIntegerField;
    QryRelatorioImageId: TIntegerField;
    QryRelatorioNO_RAZSOC_EMP: TStringField;
    QryRelatorioDE_END_EMP: TStringField;
    QryRelatorioNO_BAIRRO: TStringField;
    QryRelatorioCO_CEP_EMP: TStringField;
    QryRelatorioCO_UF_EMP: TStringField;
    QryRelatorioNO_CIDADE: TStringField;
    QryRelatorioCO_TEL1_EMP: TStringField;
    QryRelatorioCATEGORIA: TStringField;
    QryRelatorioDEFICIENCIA: TStringField;
    QryRelatorioESTADOCIVIL: TStringField;
    QryRelatorioNO_TPCAL: TStringField;
    QryRelatorioImageStream: TBlobField;
    QryRelatorioSITUACAO: TStringField;
    QRLNoCol: TQRLabel;
    qryOcorrFunci: TADOQuery;
    qryCursoFormacao: TADOQuery;
    QRBand1: TQRBand;
    QRShape57: TQRShape;
    QRShape58: TQRShape;
    QRLabel92: TQRLabel;
    QRLabel93: TQRLabel;
    QRLabel94: TQRLabel;
    QRLabel96: TQRLabel;
    QRLabel97: TQRLabel;
    QRLabel102: TQRLabel;
    QRLabel104: TQRLabel;
    QRLabel105: TQRLabel;
    QRSubDetail1: TQRSubDetail;
    QRLNrCurFor: TQRLabel;
    QRLTpCurFor: TQRLabel;
    QRLNomeCurFor: TQRLabel;
    QRLCHCurFor: TQRLabel;
    QRLDtConcCurFor: TQRLabel;
    QRLLocalCurFor: TQRLabel;
    QRLNoInstCurFor: TQRLabel;
    GroupHeaderBand1: TQRBand;
    QRShape54: TQRShape;
    QRShape55: TQRShape;
    QRLabel27: TQRLabel;
    QRLabel89: TQRLabel;
    QRLabel90: TQRLabel;
    QRLabel91: TQRLabel;
    QRLabel95: TQRLabel;
    QRSubDetailRegOcoFunc: TQRSubDetail;
    QRDBText16: TQRDBText;
    QRDBText17: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText20: TQRDBText;
    QRBand2: TQRBand;
    QRShape2: TQRShape;
    QRShape3: TQRShape;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel20: TQRLabel;
    QRLabel21: TQRLabel;
    QRSubDetail2: TQRSubDetail;
    QRLabel24: TQRLabel;
    qryMovimFunc: TADOQuery;
    QRLData: TQRLabel;
    QRLTM: TQRLabel;
    QRLMotivo: TQRLabel;
    QRLReferencia: TQRLabel;
    QRLDestino: TQRLabel;
    QRBand3: TQRBand;
    QRLDescFreq: TQRLabel;
    QRShape5: TQRShape;
    QRLabel29: TQRLabel;
    QRShape9: TQRShape;
    QRLabel30: TQRLabel;
    QRShape27: TQRShape;
    QRLabel31: TQRLabel;
    QRLabel33: TQRLabel;
    QRLabel42: TQRLabel;
    QRLabel58: TQRLabel;
    QRLabel59: TQRLabel;
    QRLabel60: TQRLabel;
    QRLabel61: TQRLabel;
    QRLabel62: TQRLabel;
    QRLabel63: TQRLabel;
    QRLabel64: TQRLabel;
    QRLabel65: TQRLabel;
    QRLabel66: TQRLabel;
    QRLabel67: TQRLabel;
    QRLabel68: TQRLabel;
    QRLabel69: TQRLabel;
    QRLabel70: TQRLabel;
    QRLabel71: TQRLabel;
    QRLabel72: TQRLabel;
    QRLabel73: TQRLabel;
    QRLabel74: TQRLabel;
    QRLabel75: TQRLabel;
    QRLabel76: TQRLabel;
    QRLabel77: TQRLabel;
    QRLabel78: TQRLabel;
    QRLabel79: TQRLabel;
    QRLabel80: TQRLabel;
    QRLabel81: TQRLabel;
    QRLabel82: TQRLabel;
    QRLabel83: TQRLabel;
    QRLabel84: TQRLabel;
    QRLabel85: TQRLabel;
    QRShape10: TQRShape;
    QRLabel86: TQRLabel;
    QRLabel103: TQRLabel;
    QrlJan: TQRLabel;
    QRShape51: TQRShape;
    QRLabel87: TQRLabel;
    QRShape11: TQRShape;
    QRLabel119: TQRLabel;
    QRShape12: TQRShape;
    QRLabel151: TQRLabel;
    QRShape15: TQRShape;
    QRLabel183: TQRLabel;
    QRShape14: TQRShape;
    QRLabel215: TQRLabel;
    QRLabel247: TQRLabel;
    QRShape18: TQRShape;
    QRLabel279: TQRLabel;
    QRShape17: TQRShape;
    QRLabel311: TQRLabel;
    QRLabel343: TQRLabel;
    QRShape19: TQRShape;
    QRLabel375: TQRLabel;
    QRShape20: TQRShape;
    QRLabel407: TQRLabel;
    QrlJan01: TQRLabel;
    QrlFev01: TQRLabel;
    QrlMar01: TQRLabel;
    QrlAbr01: TQRLabel;
    QrlMai01: TQRLabel;
    QrlJun01: TQRLabel;
    QrlJul01: TQRLabel;
    QrlAgo01: TQRLabel;
    QrlSet01: TQRLabel;
    QrlOut01: TQRLabel;
    QrlNov01: TQRLabel;
    QrlDez01: TQRLabel;
    QRShape28: TQRShape;
    QrlJan02: TQRLabel;
    QrlFev02: TQRLabel;
    QrlMar02: TQRLabel;
    QrlAbr02: TQRLabel;
    QrlMai02: TQRLabel;
    QrlJun02: TQRLabel;
    QrlJul02: TQRLabel;
    QrlAgo02: TQRLabel;
    QrlSet02: TQRLabel;
    QrlOut02: TQRLabel;
    QrlNov02: TQRLabel;
    QrlDez02: TQRLabel;
    QRShape21: TQRShape;
    QrlJan03: TQRLabel;
    QrlFev03: TQRLabel;
    QrlMar03: TQRLabel;
    QrlAbr03: TQRLabel;
    QrlMai03: TQRLabel;
    QrlJun03: TQRLabel;
    QrlJul03: TQRLabel;
    QrlAgo03: TQRLabel;
    QrlSet03: TQRLabel;
    QrlOut03: TQRLabel;
    QrlNov03: TQRLabel;
    QrlDez03: TQRLabel;
    QRShape22: TQRShape;
    QrlJan04: TQRLabel;
    QrlFev04: TQRLabel;
    QrlMar04: TQRLabel;
    QrlAbr04: TQRLabel;
    QrlMai04: TQRLabel;
    QrlJun04: TQRLabel;
    QrlJul04: TQRLabel;
    QrlAgo04: TQRLabel;
    QrlSet04: TQRLabel;
    QrlOut04: TQRLabel;
    QrlNov04: TQRLabel;
    QrlDez04: TQRLabel;
    QRShape23: TQRShape;
    QrlJan05: TQRLabel;
    QrlFev05: TQRLabel;
    QrlMar05: TQRLabel;
    QrlAbr05: TQRLabel;
    QrlMai05: TQRLabel;
    QrlJun05: TQRLabel;
    QRShape13: TQRShape;
    QrlJul05: TQRLabel;
    QrlAgo05: TQRLabel;
    QrlSet05: TQRLabel;
    QrlOut05: TQRLabel;
    QrlNov05: TQRLabel;
    QrlDez05: TQRLabel;
    QRShape24: TQRShape;
    QrlJan06: TQRLabel;
    QrlFev06: TQRLabel;
    QrlMar06: TQRLabel;
    QrlAbr06: TQRLabel;
    QrlMai06: TQRLabel;
    QrlJun06: TQRLabel;
    QrlJul06: TQRLabel;
    QrlAgo06: TQRLabel;
    QrlSet06: TQRLabel;
    QrlOut06: TQRLabel;
    QrlNov06: TQRLabel;
    QrlDez06: TQRLabel;
    QRShape25: TQRShape;
    QrlJan07: TQRLabel;
    QrlFev07: TQRLabel;
    QrlMar07: TQRLabel;
    QrlAbr07: TQRLabel;
    QrlMai07: TQRLabel;
    QrlJun07: TQRLabel;
    QrlJul07: TQRLabel;
    QrlAgo07: TQRLabel;
    QrlSet07: TQRLabel;
    QrlOut07: TQRLabel;
    QrlNov07: TQRLabel;
    QrlDez07: TQRLabel;
    QRShape26: TQRShape;
    QrlJan08: TQRLabel;
    QrlFev08: TQRLabel;
    QrlMar08: TQRLabel;
    QrlAbr08: TQRLabel;
    QrlMai08: TQRLabel;
    QrlJun08: TQRLabel;
    QrlJul08: TQRLabel;
    QrlAgo08: TQRLabel;
    QrlSet08: TQRLabel;
    QrlOut08: TQRLabel;
    QrlNov08: TQRLabel;
    QrlDez08: TQRLabel;
    QRShape29: TQRShape;
    QrlJan09: TQRLabel;
    QrlFev09: TQRLabel;
    QrlMar09: TQRLabel;
    QrlAbr09: TQRLabel;
    QrlMai09: TQRLabel;
    QrlJun09: TQRLabel;
    QrlJul09: TQRLabel;
    QrlAgo09: TQRLabel;
    QrlSet09: TQRLabel;
    QrlOut09: TQRLabel;
    QrlNov09: TQRLabel;
    QrlDez09: TQRLabel;
    QRShape30: TQRShape;
    QrlJan10: TQRLabel;
    QrlFev10: TQRLabel;
    QrlMar10: TQRLabel;
    QrlAbr10: TQRLabel;
    QrlMai10: TQRLabel;
    QrlJun10: TQRLabel;
    QrlJul10: TQRLabel;
    QrlAgo10: TQRLabel;
    QrlSet10: TQRLabel;
    QrlOut10: TQRLabel;
    QrlNov10: TQRLabel;
    QrlDez10: TQRLabel;
    QRShape31: TQRShape;
    QrlJan11: TQRLabel;
    QrlFev11: TQRLabel;
    QrlMar11: TQRLabel;
    QrlAbr11: TQRLabel;
    QrlMai11: TQRLabel;
    QrlJun11: TQRLabel;
    QrlJul11: TQRLabel;
    QrlAgo11: TQRLabel;
    QrlSet11: TQRLabel;
    QrlOut11: TQRLabel;
    QrlNov11: TQRLabel;
    QrlDez11: TQRLabel;
    QRShape32: TQRShape;
    QrlJan12: TQRLabel;
    QrlFev12: TQRLabel;
    QrlMar12: TQRLabel;
    QrlAbr12: TQRLabel;
    QrlMai12: TQRLabel;
    QrlJun12: TQRLabel;
    QrlJul12: TQRLabel;
    QrlAgo12: TQRLabel;
    QrlSet12: TQRLabel;
    QrlOut12: TQRLabel;
    QrlNov12: TQRLabel;
    QrlDez12: TQRLabel;
    QRShape33: TQRShape;
    QrlJan13: TQRLabel;
    QrlFev13: TQRLabel;
    QrlMar13: TQRLabel;
    QrlAbr13: TQRLabel;
    QrlMai13: TQRLabel;
    QrlJun13: TQRLabel;
    QrlJul13: TQRLabel;
    QrlAgo13: TQRLabel;
    QrlSet13: TQRLabel;
    QrlOut13: TQRLabel;
    QrlNov13: TQRLabel;
    QrlDez13: TQRLabel;
    QRShape34: TQRShape;
    QrlJan14: TQRLabel;
    QrlFev14: TQRLabel;
    QrlMar14: TQRLabel;
    QrlAbr14: TQRLabel;
    QrlMai14: TQRLabel;
    QrlJun14: TQRLabel;
    QrlJul14: TQRLabel;
    QrlAgo14: TQRLabel;
    QrlSet14: TQRLabel;
    QrlOut14: TQRLabel;
    QrlNov14: TQRLabel;
    QrlDez14: TQRLabel;
    QRShape35: TQRShape;
    QrlJan15: TQRLabel;
    QrlFev15: TQRLabel;
    QrlMar15: TQRLabel;
    QrlAbr15: TQRLabel;
    QrlMai15: TQRLabel;
    QrlJun15: TQRLabel;
    QrlJul15: TQRLabel;
    QrlAgo15: TQRLabel;
    QrlSet15: TQRLabel;
    QrlOut15: TQRLabel;
    QrlNov15: TQRLabel;
    QrlDez15: TQRLabel;
    QRShape36: TQRShape;
    QrlJan16: TQRLabel;
    QrlFev16: TQRLabel;
    QrlMar16: TQRLabel;
    QrlAbr16: TQRLabel;
    QrlMai16: TQRLabel;
    QrlJun16: TQRLabel;
    QrlJul16: TQRLabel;
    QrlAgo16: TQRLabel;
    QrlSet16: TQRLabel;
    QrlOut16: TQRLabel;
    QrlNov16: TQRLabel;
    QrlDez16: TQRLabel;
    QRShape37: TQRShape;
    QrlJan17: TQRLabel;
    QrlFev17: TQRLabel;
    QrlMar17: TQRLabel;
    QrlAbr17: TQRLabel;
    QrlMai17: TQRLabel;
    QrlJun17: TQRLabel;
    QrlJul17: TQRLabel;
    QrlAgo17: TQRLabel;
    QrlSet17: TQRLabel;
    QrlOut17: TQRLabel;
    QrlNov17: TQRLabel;
    QrlDez17: TQRLabel;
    QRShape38: TQRShape;
    QrlJan18: TQRLabel;
    QrlFev18: TQRLabel;
    QrlMar18: TQRLabel;
    QrlAbr18: TQRLabel;
    QrlMai18: TQRLabel;
    QrlJun18: TQRLabel;
    QrlJul18: TQRLabel;
    QrlAgo18: TQRLabel;
    QrlSet18: TQRLabel;
    QrlOut18: TQRLabel;
    QrlNov18: TQRLabel;
    QrlDez18: TQRLabel;
    QRShape39: TQRShape;
    QrlJan19: TQRLabel;
    QrlFev19: TQRLabel;
    QrlMar19: TQRLabel;
    QrlAbr19: TQRLabel;
    QrlMai19: TQRLabel;
    QrlJun19: TQRLabel;
    QrlJul19: TQRLabel;
    QrlAgo19: TQRLabel;
    QrlSet19: TQRLabel;
    QrlOut19: TQRLabel;
    QrlNov19: TQRLabel;
    QrlDez19: TQRLabel;
    QRShape40: TQRShape;
    QrlJan20: TQRLabel;
    QrlFev20: TQRLabel;
    QrlMar20: TQRLabel;
    QrlAbr20: TQRLabel;
    QrlMai20: TQRLabel;
    QrlJun20: TQRLabel;
    QrlJul20: TQRLabel;
    QrlAgo20: TQRLabel;
    QrlSet20: TQRLabel;
    QRShape16: TQRShape;
    QrlOut20: TQRLabel;
    QrlNov20: TQRLabel;
    QrlDez20: TQRLabel;
    QRShape41: TQRShape;
    QrlJan21: TQRLabel;
    QrlFev21: TQRLabel;
    QrlMar21: TQRLabel;
    QrlAbr21: TQRLabel;
    QrlMai21: TQRLabel;
    QrlJun21: TQRLabel;
    QrlJul21: TQRLabel;
    QrlAgo21: TQRLabel;
    QrlSet21: TQRLabel;
    QrlOut21: TQRLabel;
    QrlNov21: TQRLabel;
    QrlDez21: TQRLabel;
    QRShape42: TQRShape;
    QrlJan22: TQRLabel;
    QrlFev22: TQRLabel;
    QrlMar22: TQRLabel;
    QrlAbr22: TQRLabel;
    QrlMai22: TQRLabel;
    QrlJun22: TQRLabel;
    QrlJul22: TQRLabel;
    QrlAgo22: TQRLabel;
    QrlSet22: TQRLabel;
    QrlOut22: TQRLabel;
    QrlNov22: TQRLabel;
    QrlDez22: TQRLabel;
    QRShape43: TQRShape;
    QrlJan23: TQRLabel;
    QrlFev23: TQRLabel;
    QrlMar23: TQRLabel;
    QrlAbr23: TQRLabel;
    QrlMai23: TQRLabel;
    QrlJun23: TQRLabel;
    QrlJul23: TQRLabel;
    QrlAgo23: TQRLabel;
    QrlSet23: TQRLabel;
    QrlOut23: TQRLabel;
    QrlNov23: TQRLabel;
    QrlDez23: TQRLabel;
    QRShape44: TQRShape;
    QrlJan24: TQRLabel;
    QrlFev24: TQRLabel;
    QrlMar24: TQRLabel;
    QrlAbr24: TQRLabel;
    QrlMai24: TQRLabel;
    QrlJun24: TQRLabel;
    QrlJul24: TQRLabel;
    QrlAgo24: TQRLabel;
    QrlSet24: TQRLabel;
    QrlOut24: TQRLabel;
    QrlNov24: TQRLabel;
    QrlDez24: TQRLabel;
    QRShape45: TQRShape;
    QrlJan25: TQRLabel;
    QrlFev25: TQRLabel;
    QrlMar25: TQRLabel;
    QrlAbr25: TQRLabel;
    QrlMai25: TQRLabel;
    QrlJun25: TQRLabel;
    QrlJul25: TQRLabel;
    QrlAgo25: TQRLabel;
    QrlSet25: TQRLabel;
    QrlOut25: TQRLabel;
    QrlNov25: TQRLabel;
    QrlDez25: TQRLabel;
    QRShape46: TQRShape;
    QrlJan26: TQRLabel;
    QrlFev26: TQRLabel;
    QrlMar26: TQRLabel;
    QrlAbr26: TQRLabel;
    QrlMai26: TQRLabel;
    QrlJun26: TQRLabel;
    QrlJul26: TQRLabel;
    QrlAgo26: TQRLabel;
    QrlSet26: TQRLabel;
    QrlOut26: TQRLabel;
    QrlNov26: TQRLabel;
    QrlDez26: TQRLabel;
    QRShape47: TQRShape;
    QrlJan27: TQRLabel;
    QrlFev27: TQRLabel;
    QrlMar27: TQRLabel;
    QrlAbr27: TQRLabel;
    QrlMai27: TQRLabel;
    QrlJun27: TQRLabel;
    QrlJul27: TQRLabel;
    QrlAgo27: TQRLabel;
    QrlSet27: TQRLabel;
    QrlOut27: TQRLabel;
    QrlNov27: TQRLabel;
    QrlDez27: TQRLabel;
    QRShape48: TQRShape;
    QrlJan28: TQRLabel;
    QrlFev28: TQRLabel;
    QrlMar28: TQRLabel;
    QrlAbr28: TQRLabel;
    QrlMai28: TQRLabel;
    QrlJun28: TQRLabel;
    QrlJul28: TQRLabel;
    QrlAgo28: TQRLabel;
    QrlSet28: TQRLabel;
    QrlOut28: TQRLabel;
    QrlNov28: TQRLabel;
    QrlDez28: TQRLabel;
    QRShape49: TQRShape;
    QrlJan29: TQRLabel;
    QrlFev29: TQRLabel;
    QrlMar29: TQRLabel;
    QrlAbr29: TQRLabel;
    QrlMai29: TQRLabel;
    QrlJun29: TQRLabel;
    QrlJul29: TQRLabel;
    QrlAgo29: TQRLabel;
    QrlSet29: TQRLabel;
    QrlOut29: TQRLabel;
    QrlNov29: TQRLabel;
    QrlDez29: TQRLabel;
    QRShape50: TQRShape;
    QrlJan30: TQRLabel;
    QrlFev30: TQRLabel;
    QrlMar30: TQRLabel;
    QrlAbr30: TQRLabel;
    QrlMai30: TQRLabel;
    QrlJun30: TQRLabel;
    QrlJul30: TQRLabel;
    QrlAgo30: TQRLabel;
    QrlSet30: TQRLabel;
    QrlOut30: TQRLabel;
    QrlNov30: TQRLabel;
    QrlDez30: TQRLabel;
    QRShape52: TQRShape;
    QrlJan31: TQRLabel;
    QrlFev31: TQRLabel;
    QrlMar31: TQRLabel;
    QrlAbr31: TQRLabel;
    QrlMai31: TQRLabel;
    QrlJun31: TQRLabel;
    QrlJul31: TQRLabel;
    QrlAgo31: TQRLabel;
    QrlSet31: TQRLabel;
    QrlOut31: TQRLabel;
    QrlNov31: TQRLabel;
    QrlDez31: TQRLabel;
    QrlTFdez: TQRLabel;
    QrlTFnov: TQRLabel;
    QrlTFout: TQRLabel;
    QrlTFset: TQRLabel;
    QrlTFago: TQRLabel;
    QrlTFjul: TQRLabel;
    QrlTFjun: TQRLabel;
    QrlTFmai: TQRLabel;
    QrlTFabr: TQRLabel;
    QrlTFmar: TQRLabel;
    QrlTFfev: TQRLabel;
    QrlTFjan: TQRLabel;
    QrlTPjan: TQRLabel;
    QrlTPfev: TQRLabel;
    QrlTPmar: TQRLabel;
    QrlTPabr: TQRLabel;
    QrlTPmai: TQRLabel;
    QrlTPjun: TQRLabel;
    QrlTPjul: TQRLabel;
    QrlTPago: TQRLabel;
    QrlTPset: TQRLabel;
    QrlTPout: TQRLabel;
    QrlTPnov: TQRLabel;
    QrlTPdez: TQRLabel;
    QRLDescPercTotal: TQRLabel;
    QRShape53: TQRShape;
    QRLabel88: TQRLabel;
    QrlTotFal: TQRLabel;
    QrlTotPres: TQRLabel;
    QRBand4: TQRBand;
    QRDBText32: TQRDBText;
    QRLabel57: TQRLabel;
    QRLabel56: TQRLabel;
    QrlVLSalario: TQRLabel;
    QRDBText10: TQRDBText;
    QRLabel55: TQRLabel;
    QRLabel41: TQRLabel;
    QRLDescDtDemis: TQRLabel;
    QRDBText38: TQRDBText;
    QRLabel40: TQRLabel;
    QRDBText41: TQRDBText;
    QRLabel54: TQRLabel;
    QRLabel2: TQRLabel;
    QRDBText8: TQRDBText;
    QRDBText37: TQRDBText;
    QRLabel36: TQRLabel;
    QRLabel35: TQRLabel;
    QRDBText36: TQRDBText;
    QRDBText34: TQRDBText;
    QRLabel34: TQRLabel;
    QRShape8: TQRShape;
    QRLabel53: TQRLabel;
    QRLabel7: TQRLabel;
    QRDBText26: TQRDBText;
    QRLabel45: TQRLabel;
    QRDBText5: TQRDBText;
    QRDBText13: TQRDBText;
    QRLabel38: TQRLabel;
    QRDBText33: TQRDBText;
    QRLabel5: TQRLabel;
    QRLabel8: TQRLabel;
    QRDBText31: TQRDBText;
    QRLabel23: TQRLabel;
    QRDBText12: TQRDBText;
    QrlTituloEleitor: TQRLabel;
    QRLabel22: TQRLabel;
    QRDBText29: TQRDBText;
    QRLabel4: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel3: TQRLabel;
    QrlEndereco: TQRLabel;
    QRDBText21: TQRDBText;
    QRLabel11: TQRLabel;
    QRShape6: TQRShape;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QrlRG: TQRLabel;
    QRDBText9: TQRDBText;
    QRShape7: TQRShape;
    QRLabel17: TQRLabel;
    QRLabel51: TQRLabel;
    QRShape4: TQRShape;
    QRLabel18: TQRLabel;
    QRLabel52: TQRLabel;
    QRLabel19: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel37: TQRLabel;
    QRLabel39: TQRLabel;
    QRDBText35: TQRDBText;
    QrlCurForm: TQRLabel;
    QRDBText6: TQRDBText;
    QRDBText11: TQRDBText;
    QRDBText4: TQRDBText;
    QRLabel12: TQRLabel;
    QRLabel44: TQRLabel;
    QRLabel43: TQRLabel;
    QRLabel28: TQRLabel;
    QRLabel32: TQRLabel;
    QRDBText27: TQRDBText;
    QRDBText22: TQRDBText;
    QRDBText42: TQRDBText;
    QRLSitua: TQRLabel;
    QrlRemunera: TQRLabel;
    QRDBText44: TQRDBText;
    QRImage1: TQRImage;
    QRDBText39: TQRDBText;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRSubDetail1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure GroupHeaderBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand2AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRSubDetail2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    anoBase,codigoEmpresa, tipoPresenca : String;
    contCurFor : Integer;
  end;

var
  FrmRelFicCadFuncionario: TFrmRelFicCadFuncionario;

implementation

uses U_Funcoes, DateUtils, MaskUtils, Math, JPEG;

{$R *.dfm}

procedure TFrmRelFicCadFuncionario.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
  var
  mes, dia : Integer;
  diasnoano: real;
  Jpg : TJPEGImage;
  MemoryStream : TMemoryStream;
begin
  inherited;
  if not QryRelatorioNO_COL.IsNull then
    QRLNoCol.Caption := UpperCase(QryRelatorioNO_COL.AsString)
  else
    QRLNoCol.Caption := '-';

  if not QryRelatorioImageStream.IsNull then
  begin
    Try
      try
        MemoryStream := TMemoryStream.Create;
        (QryRelatorio.FieldByName('ImageStream') as TBlobField).SaveToStream(MemoryStream);
        MemoryStream.Position := OffsetMemoryStream;
        Jpg := TJpegImage.Create;
        Jpg.LoadFromStream(MemoryStream);
        QRImage1.Picture.Assign(Jpg);
      except
        //QRImage1.Enabled := false;
      end;
      finally
          MemoryStream.Free;
          Jpg.Free;
      end;
  end;

  // Endereço
  QrlEndereco.Caption := QryRelatorioDE_ENDE_COL.AsString + ', nº ' + QryRelatorioNU_ENDE_COL.AsString;

  if QryRelatorioDT_TERM_ATIV_COL.IsNull then
    QRLDescDtDemis.Visible := true
  else
    QRLDescDtDemis.Visible := false;

  // Mascara do Salário
  if not QryRelatorioVL_SALAR_COL.IsNull then
  begin
    if not QryRelatorioNO_TPCAL.IsNull then
    begin
      if (QryRelatorioNO_TPCAL.AsString = 'Hora') then
      begin
        if qryRelatorioNU_CARGA_HORARIA.IsNull then
          QrlVLSalario.Caption := 'R$ ' + FloatToStrF(QryRelatorioVL_SALAR_COL.AsFloat,ffNumber,15,2) + ' / ' + QryRelatorioNO_TPCAL.AsString
        else
          QrlVLSalario.Caption := 'R$ ' + FloatToStrF(QryRelatorioVL_SALAR_COL.AsFloat,ffNumber,15,2) + ' / ' + QryRelatorioNO_TPCAL.AsString + ' (R$ ' + FloatToStrF(QryRelatorioVL_SALAR_COL.AsFloat * qryRelatorioNU_CARGA_HORARIA.AsFloat,ffNumber,15,2) + ')';
      end
      else
        QrlVLSalario.Caption := 'R$ ' + FloatToStrF(QryRelatorioVL_SALAR_COL.AsFloat,ffNumber,15,2) + ' / ' + QryRelatorioNO_TPCAL.AsString;
    end
    else
      QrlVLSalario.Caption := 'R$ ' + FloatToStrF(QryRelatorioVL_SALAR_COL.AsFloat,ffNumber,15,2);
  end
  else
    QrlVLSalario.Caption := '-';
    
  // Remuneração
  if QryRelatorioTIPO_SITU.AsString = 'R' then QrlRemunera.Caption := 'Remunerado';
  if QryRelatorioTIPO_SITU.AsString = 'N' then QrlRemunera.Caption := 'Não Remunerado';

  // Nome do Curso de Formação
  //with DataModuleSGE.QrySql do
  with DM.QrySql do
  begin
    Close;
    Sql.Clear;
    if not QryRelatorioCO_CURFORM.IsNull then
    begin
      Sql.Text := ' select CO_ESPEC,DE_ESPEC '+
                  ' from tb100_especializacao '+
                  ' where CO_ESPEC = ' + QryRelatorioCO_CURFORM.AsString;
      Open;

      if not IsEmpty then
        QrlCurForm.Caption := FieldByName('DE_ESPEC').AsString
      else
        QrlCurForm.Caption := '-';
    end
    else
      QrlCurForm.Caption := '-';
  end;

  // Número do RG
  if not QryRelatorioCO_RG_COL.IsNull then
    QrlRG.Caption := QryRelatorioCO_RG_COL.AsString + ' - ' + QryRelatorioCO_EMIS_RG_COL.AsString + ' - ' + QryRelatorioCO_ESTA_RG_COL.AsString + ' - ' + QryRelatorioDT_EMIS_RG_COL.AsString
  else
    QrlRG.Caption := '-';

  // Título de Eleitor
  if (not QryRelatorioNU_TIT_ELE.IsNull) and ( not QryRelatorioNU_ZONA_ELE.IsNull) and ( not QryRelatorioNU_SEC_ELE.IsNull) and (QryRelatorioNU_TIT_ELE.AsString <> '') and ( QryRelatorioNU_ZONA_ELE.AsString <> '') and ( QryRelatorioNU_SEC_ELE.AsString <> '') then
    QrlTituloEleitor.Caption := QryRelatorioNU_TIT_ELE.AsString + ' - Z ' + QryRelatorioNU_ZONA_ELE.AsString + ' - S ' + QryRelatorioNU_SEC_ELE.AsString
  else
    QrlTituloEleitor.Caption := '-';

  QrlMatricula.Caption := FormatMaskText('00.000-0;0', QryRelatorioCO_MAT_COL.AsString);

  diasnoano := 365.6;

  QRLIdade.Caption := QryRelatorioDT_NASC_COL.AsString + ' (' + IntToStr(Trunc(DaysBetween(now,QryRelatorio.fieldbyname('DT_NASC_COL').AsDateTime) / diasnoano)) + ')';

  if not QryRelatorioSITUACAO.IsNull then
    QRLSitua.Caption := QryRelatorioSITUACAO.AsString
  else
    QRLSitua.Caption := '-';

  //TOTAL DE PRESENÇAS
  //with DataModuleSGE.QrySql do
  with DM.QrySql do
  begin
    Close;
    Sql.Clear;
    Sql.Text := ' SET LANGUAGE PORTUGUESE '+
                ' SELECT A.CO_COL, A.FLA_PRESENCA, A.DT_FREQ, A.CO_EMP, '+
                ' (SELECT COUNT(DISTINCT F.DT_FREQ) '+
                ' 	FROM TB199_FREQ_FUNC F '+
                ' 	WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                '		AND F.DT_FREQ BETWEEN ' + QuotedStr('01/01/' + anoBase) + ' AND ' + QuotedStr('31/12/'+ anoBase) +
                '		AND F.FLA_PRESENCA = ' + QuotedStr('S') +
                '   AND F.CO_EMP = A.CO_EMP ' +
                ' ) TOTALPRES, '+
                ' (SELECT COUNT(DISTINCT F.DT_FREQ) '+
                ' 	FROM TB199_FREQ_FUNC F '+
                ' 	WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                '		AND F.DT_FREQ BETWEEN ' + QuotedStr('01/01/' + anoBase) + ' AND ' + QuotedStr('31/12/' + anoBase) +
                ' 		AND F.FLA_PRESENCA = ' + QuotedStr('N') +
                '   AND F.CO_EMP = A.CO_EMP ' +
                ' ) TOTALFALTA '+
                ' FROM TB199_FREQ_FUNC A '+
                ' WHERE A.CO_COL = ' + QryRelatorioCO_COL.AsString +
                ' AND A.CO_EMP = ' + codigoEmpresa +
                ' GROUP by CO_COL,FLA_PRESENCA,DT_FREQ, CO_EMP';
    Open;

    if not IsEmpty then
    begin
      QrlTotPres.Caption := FieldByName('TOTALPRES').AsString;
      QrlTotFal.Caption := FieldByName('TOTALFALTA').AsString;
    end;
  end;

 // FREQUENCIA DO FUNCIONÁRIO PARA CADA DIA DE CADA MÊS DO ANO
 // para cada MES
  for mes := 1 to 12 do
  begin
    // PARA CADA DIA
    for dia := 1 to 31 do
    begin
      with QryFreqFunc do
      begin
        Close;
        Sql.Clear;
        SQL.Text := 'SET LANGUAGE PORTUGUESE ' +
                   ' SELECT DISTINCT A.DT_FREQ, A.CO_COL,A.FLA_PRESENCA '+
                   ' FROM TB199_FREQ_FUNC A '+
                   ' JOIN TB03_COLABOR C ON A.CO_COL = C.CO_COL AND A.CO_EMP = C.CO_EMP' +
                   ' JOIN TB25_EMPRESA E ON A.CO_EMP = E.CO_EMP ' +
                   ' WHERE YEAR(A.DT_FREQ) = ' + QuotedStr(anoBase) +
                   '	AND MONTH(A.DT_FREQ) = ' + IntToStr(mes) +
                   '	AND DAY(A.DT_FREQ) = ' + IntToStr(dia) +
                   ' AND C.CO_COL = ' + QryRelatorioCO_COL.AsString +
                   ' AND A.CO_EMP = ' + codigoEmpresa +
                   ' GROUP BY A.DT_FREQ, A.CO_COL, A.FLA_PRESENCA';
        Open;

      end;

// JANEIRO - TODOS OS DIAS
      if (mes = 1) and (dia = 1) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P')  then QrlJan01.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan01.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 2) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan02.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan02.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 3) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan03.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan03.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 4) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan04.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan04.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 5) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan05.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan05.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 6) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan06.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan06.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 7) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan07.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan07.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 8) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan08.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan08.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 9) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan09.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan09.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 10) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan10.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan10.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 11) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan11.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan11.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 12) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan12.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan12.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 13) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan13.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan13.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 14) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan14.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan14.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 15) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan15.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan15.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 16) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan16.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan16.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 17) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan17.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan17.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 18) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan18.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan18.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 19) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan19.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan19.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 20) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan20.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan20.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 21) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan21.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan21.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 22) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan22.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan22.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 23) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan23.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan23.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 24) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan24.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan24.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 25) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan25.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan25.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 26) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan26.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan26.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 27) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan27.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan27.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 28) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan28.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan28.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 29) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan29.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan29.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 30) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan30.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan30.Caption := 'F';
        end;
      end;
      if (mes = 1) and (dia = 31) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJan31.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJan31.Caption := 'F';
        end;
      end;
      // fim mês Janeiro

      // FEVEREIRO - TODOS OS DIAS
      if (mes = 2) and (dia = 1) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev01.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev01.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 2) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev02.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev02.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 3) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev03.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev03.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 4) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev04.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev04.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 5) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev05.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev05.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 6) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev06.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev06.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 7) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev07.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev07.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 8) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev08.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev08.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 9) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev09.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev09.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 10) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev10.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev10.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 11) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev11.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev11.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 12) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev12.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev12.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 13) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev13.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev13.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 14) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev14.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev14.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 15) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev15.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev15.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 16) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev16.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev16.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 17) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev17.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev17.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 18) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev18.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev18.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 19) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev19.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev19.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 20) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev20.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev20.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 21) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev21.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev21.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 22) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev22.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev22.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 23) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev23.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev23.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 24) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev24.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev24.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 25) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev25.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev25.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 26) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev26.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev26.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 27) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev27.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev27.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 28) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev28.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev28.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 29) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev29.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev29.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 30) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev30.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev30.Caption := 'F';
        end;
      end;
      if (mes = 2) and (dia = 31) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlFev31.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlFev31.Caption := 'F';
        end;
      end;
      // fim mês Fevereiro
            
      // MARÇO - TODOS OS DIAS
      if (mes = 3) and (dia = 1) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar01.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar01.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 2) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar02.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar02.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 3) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar03.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar03.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 4) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar04.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar04.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 5) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar05.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar05.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 6) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar06.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar06.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 7) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar07.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar07.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 8) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar08.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar08.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 9) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar09.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar09.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 10) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar10.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar10.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 11) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar11.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar11.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 12) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar12.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar12.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 13) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar13.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar13.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 14) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar14.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar14.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 15) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar15.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar15.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 16) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar16.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar16.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 17) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar17.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar17.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 18) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar18.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar18.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 19) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar19.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar19.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 20) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar20.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar20.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 21) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar21.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar21.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 22) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar22.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar22.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 23) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar23.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar23.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 24) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar24.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar24.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 25) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar25.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar25.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 26) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar26.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar26.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 27) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar27.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar27.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 28) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar28.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar28.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 29) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar29.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar29.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 30) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar30.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar30.Caption := 'F';
        end;
      end;
      if (mes = 3) and (dia = 31) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMar31.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMar31.Caption := 'F';
        end;
      end;
      // fim mês Março

      // ABRIL - TODOS OS DIAS
      if (mes = 4) and (dia = 1) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr01.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr01.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 2) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr02.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr02.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 3) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr03.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr03.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 4) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr04.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr04.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 5) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr05.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr05.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 6) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr06.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr06.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 7) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr07.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr07.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 8) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr08.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr08.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 9) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr09.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr09.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 10) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr10.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr10.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 11) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr11.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr11.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 12) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr12.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr12.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 13) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr13.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr13.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 14) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr14.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr14.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 15) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr15.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr15.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 16) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr16.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr16.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 17) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr17.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr17.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 18) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr18.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr18.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 19) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr19.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr19.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 20) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr20.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr20.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 21) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr21.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr21.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 22) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr22.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr22.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 23) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr23.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr23.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 24) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr24.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr24.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 25) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr25.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr25.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 26) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr26.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr26.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 27) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr27.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr27.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 28) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr28.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr28.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 29) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr29.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr29.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 30) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr30.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr30.Caption := 'F';
        end;
      end;
      if (mes = 4) and (dia = 31) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAbr31.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAbr31.Caption := 'F';
        end;
      end;
      // fim mês Abril
      
      // MAIO - TODOS OS DIAS
      if (mes = 5) and (dia = 1) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai01.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai01.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 2) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai02.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai02.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 3) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai03.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai03.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 4) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai04.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai04.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 5) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai05.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai05.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 6) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai06.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai06.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 7) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai07.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai07.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 8) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai08.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai08.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 9) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai09.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai09.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 10) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai10.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai10.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 11) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai11.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai11.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 12) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai12.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai12.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 13) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai13.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai13.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 14) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai14.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai14.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 15) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai15.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai15.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 16) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai16.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai16.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 17) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai17.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai17.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 18) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai18.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai18.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 19) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai19.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai19.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 20) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai20.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai20.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 21) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai21.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai21.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 22) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai22.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai22.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 23) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai23.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai23.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 24) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai24.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai24.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 25) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai25.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai25.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 26) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai26.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai26.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 27) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai27.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai27.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 28) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai28.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai28.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 29) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai29.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai29.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 30) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai30.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai30.Caption := 'F';
        end;
      end;
      if (mes = 5) and (dia = 31) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlMai31.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlMai31.Caption := 'F';
        end;
      end;
      // fim do mês maio

      // JUNHO - TODOS OS DIAS
      if (mes = 6) and (dia = 1) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun01.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun01.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 2) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun02.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun02.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 3) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun03.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun03.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 4) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun04.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun04.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 5) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun05.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun05.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 6) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun06.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun06.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 7) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun07.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun07.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 8) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun08.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun08.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 9) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun09.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun09.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 10) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun10.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun10.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 11) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun11.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun11.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 12) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun12.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun12.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 13) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun13.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun13.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 14) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun14.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun14.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 15) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun15.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun15.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 16) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun16.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun16.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 17) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun17.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun17.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 18) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun18.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun18.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 19) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun19.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun19.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 20) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun20.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun20.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 21) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun21.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun21.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 22) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun22.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun22.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 23) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun23.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun23.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 24) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun24.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun24.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 25) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun25.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun25.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 26) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun26.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun26.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 27) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun27.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun27.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 28) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun28.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun28.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 29) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun29.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun29.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 30) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun30.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun30.Caption := 'F';
        end;
      end;
      if (mes = 6) and (dia = 31) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJun31.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJun31.Caption := 'F';
        end;
      end;
      // fim mês Junho
      // JULHO - TODOS OS DIAS
      if (mes = 7) and (dia = 1) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul01.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul01.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 2) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul02.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul02.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 3) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul03.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul03.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 4) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul04.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul04.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 5) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul05.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul05.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 6) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul06.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul06.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 7) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul07.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul07.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 8) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul08.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul08.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 9) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul09.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul09.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 10) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul10.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul10.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 11) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul11.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul11.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 12) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul12.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul12.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 13) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul13.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul13.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 14) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul14.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul14.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 15) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul15.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul15.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 16) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul16.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul16.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 17) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul17.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul17.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 18) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul18.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul18.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 19) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul19.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul19.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 20) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul20.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul20.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 21) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul21.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul21.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 22) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul22.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul22.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 23) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul23.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul23.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 24) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul24.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul24.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 25) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul25.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul25.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 26) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul26.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul26.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 27) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul27.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul27.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 28) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul28.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul28.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 29) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul29.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul29.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 30) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul30.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul30.Caption := 'F';
        end;
      end;
      if (mes = 7) and (dia = 31) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlJul31.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlJul31.Caption := 'F';
        end;
      end;
      // fim mês Julho
      // AGOSTO - TODOS OS DIAS
      if (mes = 8) and (dia = 1) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo01.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo01.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 2) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo02.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo02.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 3) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo03.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo03.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 4) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo04.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo04.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 5) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo05.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo05.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 6) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo06.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo06.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 7) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo07.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo07.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 8) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo08.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo08.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 9) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo09.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo09.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 10) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo10.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo10.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 11) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo11.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo11.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 12) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo12.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo12.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 13) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo13.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo13.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 14) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo14.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo14.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 15) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo15.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo15.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 16) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo16.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo16.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 17) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo17.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo17.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 18) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo18.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo18.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 19) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo19.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo19.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 20) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo20.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo20.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 21) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo21.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo21.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 22) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo22.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo22.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 23) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo23.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo23.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 24) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo24.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo24.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 25) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo25.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo25.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 26) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo26.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo26.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 27) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo27.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo27.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 28) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo28.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo28.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 29) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo29.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo29.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 30) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo30.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo30.Caption := 'F';
        end;
      end;
      if (mes = 8) and (dia = 31) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlAgo31.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlAgo31.Caption := 'F';
        end;
      end;
      // fim mês Agosto      
      // SETEMBRO - TODOS OS DIAS
      if (mes = 9) and (dia = 1) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet01.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet01.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 2) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet02.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet02.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 3) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet03.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet03.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 4) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet04.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet04.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 5) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet05.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet05.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 6) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet06.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet06.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 7) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet07.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet07.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 8) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet08.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet08.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 9) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet09.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet09.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 10) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet10.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet10.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 11) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet11.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet11.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 12) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet12.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet12.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 13) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet13.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet13.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 14) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet14.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet14.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 15) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet15.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet15.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 16) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet16.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet16.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 17) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet17.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet17.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 18) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet18.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet18.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 19) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet19.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet19.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 20) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet20.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet20.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 21) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet21.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet21.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 22) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet22.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet22.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 23) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet23.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet23.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 24) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet24.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet24.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 25) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet25.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet25.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 26) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet26.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet26.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 27) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet27.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet27.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 28) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet28.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet28.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 29) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet29.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet29.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 30) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet30.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet30.Caption := 'F';
        end;
      end;
      if (mes = 9) and (dia = 31) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlSet31.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlSet31.Caption := 'F';
        end;
      end;
      // fim mês Setembro
      // OUTUBRO - TODOS OS DIAS
      if (mes = 10) and (dia = 1) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut01.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut01.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 2) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut02.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut02.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 3) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut03.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut03.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 4) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut04.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut04.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 5) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut05.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut05.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 6) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut06.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut06.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 7) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut07.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut07.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 8) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut08.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut08.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 9) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut09.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut09.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 10) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut10.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut10.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 11) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut11.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut11.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 12) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut12.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut12.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 13) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut13.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut13.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 14) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut14.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut14.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 15) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut15.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut15.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 16) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut16.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut16.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 17) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut17.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut17.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 18) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut18.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut18.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 19) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut19.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut19.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 20) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut20.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut20.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 21) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut21.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut21.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 22) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut22.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut22.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 23) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut23.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut23.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 24) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut24.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut24.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 25) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut25.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut25.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 26) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut26.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut26.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 27) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut27.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut27.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 28) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut28.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut28.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 29) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut29.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut29.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 30) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut30.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut30.Caption := 'F';
        end;
      end;
      if (mes = 10) and (dia = 31) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlOut31.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlOut31.Caption := 'F';
        end;
      end;
      // fim mês Outubro
      // NOVEMBRO - TODOS OS DIAS
      if (mes = 11) and (dia = 1) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov01.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov01.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 2) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov02.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov02.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 3) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov03.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov03.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 4) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov04.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov04.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 5) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov05.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov05.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 6) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov06.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov06.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 7) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov07.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov07.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 8) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov08.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov08.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 9) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov09.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov09.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 10) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov10.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov10.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 11) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov11.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov11.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 12) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov12.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov12.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 13) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov13.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov13.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 14) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov14.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov14.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 15) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov15.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov15.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 16) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov16.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov16.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 17) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov17.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov17.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 18) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov18.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov18.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 19) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov19.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov19.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 20) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov20.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov20.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 21) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov21.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov21.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 22) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov22.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov22.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 23) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov23.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov23.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 24) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov24.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov24.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 25) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov25.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov25.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 26) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov26.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov26.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 27) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov27.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov27.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 28) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov28.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov28.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 29) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov29.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov29.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 30) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov30.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov30.Caption := 'F';
        end;
      end;
      if (mes = 11) and (dia = 31) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlNov31.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlNov31.Caption := 'F';
        end;
      end;
      // fim mês Novembro
      // DEZEMBRO - TODOS OS DIAS
      if (mes = 12) and (dia = 1) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez01.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez01.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 2) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez02.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez02.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 3) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez03.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez03.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 4) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez04.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez04.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 5) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez05.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez05.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 6) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez06.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez06.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 7) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez07.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez07.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 8) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez08.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez08.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 9) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez09.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez09.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 10) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez10.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez10.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 11) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez11.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez11.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 12) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez12.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez12.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 13) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez13.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez13.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 14) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez14.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez14.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 15) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez15.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez15.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 16) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez16.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez16.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 17) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez17.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez17.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 18) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez18.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez18.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 19) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez19.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez19.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 20) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez20.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez20.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 21) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez21.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez21.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 22) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez22.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez22.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 23) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez23.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez23.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 24) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez24.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez24.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 25) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez25.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez25.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 26) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez26.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez26.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 27) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez27.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez27.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 28) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez28.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez28.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 29) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez29.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez29.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 30) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez30.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez30.Caption := 'F';
        end;
      end;
      if (mes = 12) and (dia = 31) then
      begin
        if QryFreqFuncFLA_PRESENCA.AsString <> '' then
        begin
          if (QryFreqFuncFLA_PRESENCA.AsString = 'S') or (QryFreqFuncFLA_PRESENCA.AsString = 'P') then QrlDez31.Caption := 'P';
          if (QryFreqFuncFLA_PRESENCA.AsString = 'N') or (QryFreqFuncFLA_PRESENCA.AsString = 'F') then QrlDez31.Caption := 'F';
        end;
      end;
      // fim mês Dezembro
    end;
  end;

  // PORCENTAGEM DOS TOTAIS DE CADA MÊS
  with QryTF do
  begin
    Close;
    Sql.Clear;
    SQL.Text := ' SET LANGUAGE PORTUGUESE '+
                '  SELECT F.CO_COL, F.FLA_PRESENCA, F.DT_FREQ, '+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)'+
                '    FROM TB199_FREQ_FUNC F'+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('1') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('S') +
                    ' AND F.CO_EMP = ' + codigoEmpresa +
                '  ) totJan,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)'+
                '    FROM TB199_FREQ_FUNC F'+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('2') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('S') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totFev,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)'+
                '    FROM TB199_FREQ_FUNC F'+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('3') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('S') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totMar,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)'+
                '    FROM TB199_FREQ_FUNC F'+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('4') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('S') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totAbr,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)           '+
                '    FROM TB199_FREQ_FUNC F                    '+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('5') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('S') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totMai,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)           '+
                '    FROM TB199_FREQ_FUNC F                    '+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('6') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('S') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totJun,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)           '+
                '    FROM TB199_FREQ_FUNC F'+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('7') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('S') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totJul,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)           '+
                '    FROM TB199_FREQ_FUNC F'+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('8') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('S') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totAgo,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)           '+
                '    FROM TB199_FREQ_FUNC F                    '+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('9') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('S') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totSet,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)           '+
                '    FROM TB199_FREQ_FUNC F                    '+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('10') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('S') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totOut,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)           '+
                '    FROM TB199_FREQ_FUNC F                    '+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('11') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('S') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                ') totNov,'+
                ' (SELECT COUNT(DISTINCT F.DT_FREQ)           '+
                '  FROM TB199_FREQ_FUNC F                    '+
                '  WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'   AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '   AND MONTH(F.DT_FREQ) = ' + QuotedStr('12') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('S') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                ') totDez '+
                '  FROM TB199_FREQ_FUNC F '+
                'WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString + ' and f.co_emp =' + codigoEmpresa;
    Open;

  end;
  if QryTFtotJan.AsInteger > 0 then                               // 200 = qtde dias para trabalhar;
    QrlTPjan.Caption := QryTFtotJan.AsString;
  if QryTFtotFev.AsInteger > 0 then
    QrlTPfev.Caption := QryTFtotFev.AsString;
  if QryTFtotMar.AsInteger > 0 then
    QrlTPmar.Caption := QryTFtotMar.AsString;
  if QryTFtotAbr.AsInteger > 0 then
    QrlTPabr.Caption := QryTFtotAbr.AsString;
  if QryTFtotMai.AsInteger > 0 then
    QrlTPmai.Caption := QryTFtotMai.AsString;
  if QryTFtotJun.AsInteger > 0 then
    QrlTPjun.Caption := QryTFtotJun.AsString;
  if QryTFtotJul.AsInteger > 0 then
    QrlTPjul.Caption := QryTFtotJul.AsString;
  if QryTFtotAgo.AsInteger > 0 then
    QrlTPago.Caption := QryTFtotAgo.AsString;
  if QryTFtotSet.AsInteger > 0 then
    QrlTPset.Caption := QryTFtotSet.AsString;
  if QryTFtotOut.AsInteger > 0 then
    QrlTPout.Caption := QryTFtotOut.AsString;
  if QryTFtotNov.AsInteger > 0 then
    QrlTPnov.Caption := QryTFtotNov.AsString;
  if QryTFtotDez.AsInteger > 0 then
    QrlTPdez.Caption := QryTFtotDez.AsString;

  // PORCENTAGEM DOS TOTAIS DE CADA MÊS
  with QryTF do
  begin
    Close;
    Sql.Clear;
    SQL.Text := ' SET LANGUAGE PORTUGUESE '+
                '  SELECT F.CO_COL, F.FLA_PRESENCA, F.DT_FREQ, '+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)'+
                '    FROM TB199_FREQ_FUNC F'+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('1') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('N') +
                    ' AND F.CO_EMP = ' + codigoEmpresa +
                '  ) totJan,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)'+
                '    FROM TB199_FREQ_FUNC F'+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('2') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('N') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totFev,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)'+
                '    FROM TB199_FREQ_FUNC F'+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('3') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('N') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totMar,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)'+
                '    FROM TB199_FREQ_FUNC F'+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('4') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('N') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totAbr,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)           '+
                '    FROM TB199_FREQ_FUNC F                    '+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('5') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('N') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totMai,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)           '+
                '    FROM TB199_FREQ_FUNC F                    '+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('6') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('N') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totJun,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)           '+
                '    FROM TB199_FREQ_FUNC F'+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('7') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('N') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totJul,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)           '+
                '    FROM TB199_FREQ_FUNC F'+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('8') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('N') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totAgo,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)           '+
                '    FROM TB199_FREQ_FUNC F                    '+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('9') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('N') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totSet,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)           '+
                '    FROM TB199_FREQ_FUNC F                    '+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('10') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('N') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                '  ) totOut,'+
                '  (SELECT COUNT(DISTINCT F.DT_FREQ)           '+
                '    FROM TB199_FREQ_FUNC F                    '+
                '    WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'      AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '      AND MONTH(F.DT_FREQ) = ' + QuotedStr('11') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('N') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                ') totNov,'+
                ' (SELECT COUNT(DISTINCT F.DT_FREQ)           '+
                '  FROM TB199_FREQ_FUNC F                    '+
                '  WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString +
                //'   AND YEAR(F.DT_FREQ) = ' + QuotedStr('2009') +
                ' AND YEAR(F.DT_FREQ) = ' + QuotedStr(anoBase) +
                '   AND MONTH(F.DT_FREQ) = ' + QuotedStr('12') +
                '      AND F.FLA_PRESENCA = ' + QuotedStr('N') +
                    ' AND F.CO_EMP = ' + codigoEmpresa+
                ') totDez '+
                '  FROM TB199_FREQ_FUNC F '+
                'WHERE F.CO_COL = ' + QryRelatorioCO_COL.AsString + ' and f.co_emp =' + codigoEmpresa;
    Open;

  end;
  if QryTFtotJan.AsInteger > 0 then                               // 200 = qtde dias para trabalhar;
    QrlTFjan.Caption := QryTFtotJan.AsString;
  if QryTFtotFev.AsInteger > 0 then
    QrlTFfev.Caption := QryTFtotFev.AsString;
  if QryTFtotMar.AsInteger > 0 then
    QrlTFmar.Caption := QryTFtotMar.AsString;
  if QryTFtotAbr.AsInteger > 0 then
    QrlTFabr.Caption := QryTFtotAbr.AsString;
  if QryTFtotMai.AsInteger > 0 then
    QrlTFmai.Caption := QryTFtotMai.AsString;
  if QryTFtotJun.AsInteger > 0 then
    QrlTFjun.Caption := QryTFtotJun.AsString;
  if QryTFtotJul.AsInteger > 0 then
    QrlTFjul.Caption := QryTFtotJul.AsString;
  if QryTFtotAgo.AsInteger > 0 then
    QrlTFago.Caption := QryTFtotAgo.AsString;
  if QryTFtotSet.AsInteger > 0 then
    QrlTFset.Caption := QryTFtotSet.AsString;
  if QryTFtotOut.AsInteger > 0 then
    QrlTFout.Caption := QryTFtotOut.AsString;
  if QryTFtotNov.AsInteger > 0 then
    QrlTFnov.Caption := QryTFtotNov.AsString;
  if QryTFtotDez.AsInteger > 0 then
    QrlTFdez.Caption := QryTFtotDez.AsString;

  if StrToInt(QrlTotPres.Caption) + StrToInt(QrlTotPres.Caption) > 0 then
  begin
    QRLDescPercTotal.Caption := '% de Faltas: ' + FloatToStrF((StrToInt(QrlTotFal.Caption) * 100)/(StrToInt(QrlTotPres.Caption) + StrToInt(QrlTotFal.Caption)),ffNumber,10,1) +
    '  -  % de Presença: ' + FloatToStrF((StrToInt(QrlTotPres.Caption) * 100)/(StrToInt(QrlTotPres.Caption) + StrToInt(QrlTotFal.Caption)),ffNumber,10,1);
  end
  else
    QRLDescPercTotal.Caption := '% de Faltas:  -  % de Presença:  ';

end;

procedure TFrmRelFicCadFuncionario.QRBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  with qryCursoFormacao do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select cf.NU_CARGA_HORARIA,cf.CO_MESANO_FIM,cf.NO_INSTIT_CURSO,cf.NO_CIDADE_CURSO,cf.CO_UF_CURSO,e.de_espec,'+
    ' tipo = (CASE e.TP_ESPEC '+
               '	WHEN ' + QuotedStr('TE') + ' THEN ' + QuotedStr('Técnico') +
               '	WHEN ' + QuotedStr('GR') + ' THEN ' + QuotedStr('Graduação') +
               ' 	WHEN ' + QuotedStr('ES') + ' THEN ' + QuotedStr('Especialização') +
               '	WHEN ' + QuotedStr('MB') + ' THEN ' + QuotedStr('MBA') +
               '	WHEN ' + QuotedStr('PG') + ' THEN ' + QuotedStr('Pós-Graduação') +
               '	WHEN ' + QuotedStr('ME') + ' THEN ' + QuotedStr('Mestrado') +
               '	WHEN ' + QuotedStr('DO') + ' THEN ' + QuotedStr('Doutorado') +
               '	WHEN ' + QuotedStr('PD') + ' THEN ' + QuotedStr('Pós-Doutorado') +
               '	WHEN ' + QuotedStr('EP') + ' THEN ' + QuotedStr('Específico') +
               '	WHEN ' + QuotedStr('OU') + ' THEN ' + QuotedStr('Outros') +
               '				END) '+
    ' from TB62_CURSO_FORM cf ' +
    ' join tb100_especializacao e on cf.co_espec = e.co_espec ' +
    ' where co_emp = ' + codigoEmpresa +
    ' and co_col = ' + QryRelatorioCO_COL.AsString +
    ' order by e.TP_ESPEC';
    Open;
  end;
end;

procedure TFrmRelFicCadFuncionario.QRSubDetail1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  //Listar os cursos de formação do funcionário
  if not qryCursoFormacao.IsEmpty then
  begin
    QRLTpCurFor.Caption := qryCursoFormacao.FieldByName('tipo').AsString;
    QRLCHCurFor.Caption := FloatToStrF(qryCursoFormacao.FieldByName('NU_CARGA_HORARIA').AsFloat,ffNumber,15,0);
    QRLDtConcCurFor.Caption := qryCursoFormacao.FieldByName('CO_MESANO_FIM').AsString;
    QRLLocalCurFor.Caption := qryCursoFormacao.FieldByName('NO_CIDADE_CURSO').AsString + '/' + qryCursoFormacao.FieldByName('CO_UF_CURSO').AsString;
    QRLNoInstCurFor.Caption := qryCursoFormacao.FieldByName('NO_INSTIT_CURSO').AsString;
    QRLNomeCurFor.Caption := qryCursoFormacao.FieldByName('de_espec').AsString;

    if contCurFor <= 9 then
    begin
      QRLNrCurFor.Caption := '0' + IntToStr(contCurFor);
    end
    else
      QRLNrCurFor.Caption := IntToStr(contCurFor);
      contCurFor := contCurFor + 1;
  end
  else
  begin
    QRLNrCurFor.Caption := '**';
    QRLTpCurFor.Caption := '-';
    QRLCHCurFor.Caption := '-';
    QRLDtConcCurFor.Caption := '-';
    QRLLocalCurFor.Caption := '-';
    QRLNoInstCurFor.Caption := '-';
    QRLNomeCurFor.Caption := '-';
  end;
end;

procedure TFrmRelFicCadFuncionario.GroupHeaderBand1AfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;
  with qryOcorrFunci do
  begin
    Close;
    SQL.Clear;
    SQL.Text :=' SELECT aps.*, ps.DE_TIPO_OCORR '+
               ' FROM TB151_OCORR_COLABOR aps ' +
               ' JOIN TB150_TIPO_OCORR ps on ps.CO_SIGL_OCORR = aps.CO_SIGL_OCORR ' +
               ' WHERE aps.CO_COL = ' + QryRelatorio.FieldByName('CO_COL').AsString +
               ' order by aps.DT_OCORR';
    Open;
  end;
end;

procedure TFrmRelFicCadFuncionario.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  contCurFor := 1;
end;

procedure TFrmRelFicCadFuncionario.QRBand2AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  with qryMovimFunc do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'set language portuguese ' +
                ' select mf.CO_EMP_ORIGEM, c.CO_MAT_COL, c.NO_COL, mf.DT_CADAST as CADASTRO, mf.DT_INI_MOVIM_TRANSF_FUNCI as INICIO, ' +
                ' mf.DT_FIM_MOVIM_TRANSF_FUNCI as FIM, mf.CO_TIPO_MOVIM, mf.CO_MOTIVO_AFAST as MOTIVO, ' +
                ' e.NO_FANTAS_EMP as DESTINO_INT, mf.CO_TIPO_REMUN as REMUN, it.NO_INSTIT_TRANSF ' +
                ' from TB286_MOVIM_TRANSF_FUNCI mf ' +
                ' join TB03_COLABOR c on c.CO_COL = mf.CO_COL and c.CO_EMP = mf.CO_EMP_ORIGEM ' +
                ' left join TB25_EMPRESA e on e.CO_EMP = mf.CO_EMP_DESTIN ' +
                ' left join TB285_INSTIT_TRANSF it on it.ID_INSTIT_TRANSF = mf.ID_INSTIT_TRANSF ' +
                ' where mf.CO_COL = ' + QryRelatorioCO_COL.AsString +
                ' order by mf.DT_CADAST';;
    Open;
  end;
end;

procedure TFrmRelFicCadFuncionario.QRSubDetail2BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not qryMovimFunc.IsEmpty then
  begin
    if not qryMovimFunc.FieldByName('CADASTRO').IsNull then
      QRLData.Caption := FormatDateTime('dd/MM/yy', qryMovimFunc.FieldByName('CADASTRO').AsDateTime)
    else
      QRLData.Caption := '-';

    if qryMovimFunc.FieldByName('CO_TIPO_MOVIM').AsString = 'ME' then
    begin
      QRLTM.Caption := 'Movimentação Externa';
    end
    else if qryMovimFunc.FieldByName('CO_TIPO_MOVIM').AsString = 'MI' then
    begin
      QRLTM.Caption := 'Movimentação Interna';
    end
    else
    begin
      QRLTM.Caption := 'Transferência Externa';
    end;

    if qryMovimFunc.FieldByName('MOTIVO').AsString = 'TEX' then
      QRLMotivo.Caption := 'Transferência Externa'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'DIS' then
      QRLMotivo.Caption := 'Disponibilidade'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'APO' then
      QRLMotivo.Caption := 'Atividade Pontual'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'OUT' then
      QRLMotivo.Caption := 'Outros'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'PRO' then
      QRLMotivo.Caption := 'Promoção'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'TIN' then
      QRLMotivo.Caption := 'Transferência Interna'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'FER' then
      QRLMotivo.Caption := 'Férias'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'LME' then
      QRLMotivo.Caption := 'Licença Médica'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'LMA' then
      QRLMotivo.Caption := 'Licença Maternidade'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'LPA' then
      QRLMotivo.Caption := 'Licença Paternidade'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'LPR' then
      QRLMotivo.Caption := 'Licença Prêmia'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'LFU' then
      QRLMotivo.Caption := 'Licença Funcional'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'OLI' then
      QRLMotivo.Caption := 'Outras Licenças'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'DEM' then
      QRLMotivo.Caption := 'Demissão'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'ECO' then
      QRLMotivo.Caption := 'Encerramento Contrato'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'AFA' then
      QRLMotivo.Caption := 'Afastamento'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'SUS' then
      QRLMotivo.Caption := 'Suspensão'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'CAP' then
      QRLMotivo.Caption := 'Capacitação'
    else if qryMovimFunc.FieldByName('MOTIVO').AsString = 'TRE' then
      QRLMotivo.Caption := 'Treinamento'
    else
      QRLMotivo.Caption := 'Motivos Outros';

    if not qryMovimFunc.FieldByName('INICIO').IsNull then
    begin
      QRLReferencia.Caption := FormatDateTime('dd/MM/yy',qryMovimFunc.FieldByName('INICIO').AsDateTime) + ' - ';
      if not qryMovimFunc.FieldByName('FIM').IsNull then
        QRLReferencia.Caption := QRLReferencia.Caption + FormatDateTime('dd/MM/yy',qryMovimFunc.FieldByName('FIM').AsDateTime)
      else
        QRLReferencia.Caption := QRLReferencia.Caption + ' **** ';
    end
    else
      QRLReferencia.Caption := '-';

    if qryMovimFunc.FieldByName('CO_TIPO_MOVIM').AsString <> 'TE' then
      QRLDestino.Caption := qryMovimFunc.FieldByName('DESTINO_INT').AsString
    else
    begin
      QRLDestino.Caption := qryMovimFunc.FieldByName('NO_INSTIT_TRANSF').AsString;
    end;
  end
  else
  begin
    QRLData.Caption := '-';
    QRLTM.Caption := '-';
    QRLMotivo.Caption := '-';
    QRLReferencia.Caption := '-';
    QRLDestino.Caption := '-';
  end;
end;

end.
