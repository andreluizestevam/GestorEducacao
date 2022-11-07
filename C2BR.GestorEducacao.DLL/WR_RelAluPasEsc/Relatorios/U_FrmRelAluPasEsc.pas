unit U_FrmRelAluPasEsc;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelAluPasEsc = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRGroup1: TQRGroup;
    QrlParametros: TQRLabel;
    QRLabel10: TQRLabel;
    QRShape1: TQRShape;
    QRLabel11: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel19: TQRLabel;
    QRLabel5: TQRLabel;
    QrlTitSerieTurma: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QryRelatorioCO_ALU: TAutoIncField;
    QryRelatorioCO_EMP: TIntegerField;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioNO_APE_ALU: TStringField;
    QryRelatorioCO_INST: TIntegerField;
    QryRelatorioDT_NASC_ALU: TDateTimeField;
    QryRelatorioNU_CPF_ALU: TStringField;
    QryRelatorioCO_SEXO_ALU: TStringField;
    QryRelatorioCO_RG_ALU: TStringField;
    QryRelatorioCO_ORG_RG_ALU: TStringField;
    QryRelatorioCO_ESTA_RG_ALU: TStringField;
    QryRelatorioDT_EMIS_RG_ALU: TDateTimeField;
    QryRelatorioDE_ENDE_ALU: TStringField;
    QryRelatorioNU_ENDE_ALU: TIntegerField;
    QryRelatorioDE_COMP_ALU: TStringField;
    QryRelatorioCO_BAIRRO: TIntegerField;
    QryRelatorioCO_CIDADE: TIntegerField;
    QryRelatorioCO_TIPO_BOLSA: TIntegerField;
    QryRelatorioCO_ESTA_ALU: TStringField;
    QryRelatorioCO_CEP_ALU: TStringField;
    QryRelatorioNU_TELE_RESI_ALU: TStringField;
    QryRelatorioNU_TELE_CELU_ALU: TStringField;
    QryRelatorioNO_PROF_ALU: TStringField;
    QryRelatorioNO_EMPR_ALU: TStringField;
    QryRelatorioNO_CARG_EMPR_ALU: TStringField;
    QryRelatorioDT_ADMI_EMPR_ALU: TDateTimeField;
    QryRelatorioNU_TELE_COME_ALU: TStringField;
    QryRelatorioNU_RAMA_COME_ALU: TStringField;
    QryRelatorioNO_PAI_ALU: TStringField;
    QryRelatorioNO_MAE_ALU: TStringField;
    QryRelatorioCO_NACI_ALU: TStringField;
    QryRelatorioDE_NACI_ALU: TStringField;
    QryRelatorioDE_NATU_ALU: TStringField;
    QryRelatorioCO_UF_NATU_ALU: TStringField;
    QryRelatorioNO_ENDE_ELET_ALU: TStringField;
    QryRelatorioNO_WEB_ALU: TStringField;
    QryRelatorioDT_CADA_ALU: TDateTimeField;
    QryRelatorioCO_SITU_ALU: TStringField;
    QryRelatorioDT_SITU_ALU: TDateTimeField;
    QryRelatorioNU_TIT_ELE: TStringField;
    QryRelatorioNU_ZONA_ELE: TStringField;
    QryRelatorioNU_SEC_ELE: TStringField;
    QryRelatorioCO_UF_TIT_ELE: TStringField;
    QryRelatorioFLA_BOLSISTA: TStringField;
    QryRelatorioNU_PEC_DESBOL: TBCDField;
    QryRelatorioDE_TIPO_BOLSA: TStringField;
    QryRelatorioDT_VENC_BOLSA: TDateTimeField;
    QryRelatorioDT_VENC_BOLSAF: TDateTimeField;
    QryRelatorioNOM_USUARIO: TStringField;
    QryRelatorioDT_ALT_REGISTRO: TDateTimeField;
    QryRelatorioCO_EMP_ORIGEM: TIntegerField;
    QryRelatorioCO_ESTADO_CIVIL: TStringField;
    QryRelatorioDES_OBSERVACAO: TStringField;
    QryRelatorioTP_RACA: TStringField;
    QryRelatorioNU_CERT: TStringField;
    QryRelatorioDE_CERT_LIVRO: TStringField;
    QryRelatorioNU_CERT_FOLHA: TStringField;
    QryRelatorioDE_CERT_CARTORIO: TStringField;
    QryRelatorioTP_DEF: TStringField;
    QryRelatorioDES_DEF: TStringField;
    QryRelatorioRENDA_FAMILIAR: TStringField;
    QryRelatorioFLA_BOLSA_ESCOLA: TBooleanField;
    QryRelatorioDES_OBS_ALU: TMemoField;
    QryRelatorioFLA_PASSE_ESCOLA: TBooleanField;
    QryRelatorioTP_CERTIDAO: TStringField;
    QryRelatorioCO_RESP: TIntegerField;
    QryRelatoriono_bairro: TStringField;
    QryRelatoriocoduf: TStringField;
    QryRelatoriono_cidade: TStringField;
    QryRelatoriono_cur: TStringField;
    QryRelatoriono_tur: TStringField;
    QryRelatorioco_alu_cad: TStringField;
    QryRelatoriono_resp: TStringField;
    QryRelatorionu_tele_celu_resp: TStringField;
    QRLMatricula: TQRLabel;
    QRLNuNis: TQRLabel;
    QRDBText7: TQRDBText;
    QRLCidBai: TQRLabel;
    QRLSerTur: TQRLabel;
    QRDBText1: TQRDBText;
    QRBand1: TQRBand;
    QRLabel2: TQRLabel;
    QRLTotAlu: TQRLabel;
    QryRelatorioCO_SIGL_CUR: TStringField;
    QryRelatorioNU_NIS: TBCDField;
    QRLNoAlu: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelAluPasEsc: TFrmRelAluPasEsc;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelAluPasEsc.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;

  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-'; 

  QRLNuNis.Caption := QryRelatorioNU_NIS.AsString;

  QRLSerTur.Caption :=  QryRelatorioCO_SIGL_CUR.AsString + ' / ' + QryRelatoriono_tur.AsString;

  if not QryRelatorioco_alu_cad.IsNull then
    QRLMatricula.Caption := FormatMaskText('00.000.000000;0',QryRelatorioco_alu_cad.AsString);

  if not(QryRelatoriono_cidade.IsNull) and not(QryRelatoriono_bairro.IsNull) then
    QRLCidBai.Caption := QryRelatoriono_cidade.AsString + '/' + QryRelatoriono_bairro.AsString;

  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00D8D8D8
  else
    DetailBand1.Color := clWhite;

  QRLTotAlu.Caption := IntToStr(StrToInt(QRLTotAlu.Caption) + 1);
end;

procedure TFrmRelAluPasEsc.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotAlu.Caption := '0';
end;

end.
