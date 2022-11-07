unit U_FrmRelDemonstrativoInscricao;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, QRCtrls, DB, ADODB, QuickRpt, ExtCtrls;

type
  TFrmRelDemonstrativoInscricao = class(TFrmRelTemplate)
    detail: TQRBand;
    QRDBText3: TQRDBText;
    QRDBText13: TQRDBText;
    QRDBText12: TQRDBText;
    QRDBText11: TQRDBText;
    QRLStatusInsc: TQRLabel;
    QRLNumero: TQRLabel;
    QRGroup1: TQRGroup;
    QRShape6: TQRShape;
    QRLabel24: TQRLabel;
    QRLabel23: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel25: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel10: TQRLabel;
    QRDBText9: TQRDBText;
    QRLIdade: TQRLabel;
    QRDBText8: TQRDBText;
    QRDBText7: TQRDBText;
    qrlParametros: TQRLabel;
    QrlTel: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel19: TQRLabel;
    QRLTotal: TQRLabel;
    QryRelatorioNU_TELE_RESI_RESP: TStringField;
    QryRelatorioNO_RESP: TStringField;
    QryRelatorioNU_CPF_RESP: TStringField;
    QryRelatorioCO_SEXO_RESP: TStringField;
    QryRelatorioDT_NASC_RESP: TDateTimeField;
    QryRelatorioNO_BAIRRO: TStringField;
    QryRelatorioNO_CIDADE: TStringField;
    QryRelatorioDESCRICAOUF: TStringField;
    QryRelatorioNO_CUR: TStringField;
    QryRelatorioCO_EMP: TIntegerField;
    QryRelatorioNU_INSC_ALU: TIntegerField;
    QryRelatorioTIPO: TStringField;
    QryRelatorioCO_ALU: TIntegerField;
    QryRelatorioDT_INSC_ALU: TDateTimeField;
    QryRelatorioCO_STAT_ENTR_RG: TStringField;
    QryRelatorioCO_STAT_ENTR_CPF: TStringField;
    QryRelatorioCO_STAT_ENTR_CUR: TStringField;
    QryRelatorioCO_STAT_ENTR_DIP: TStringField;
    QryRelatorioCO_STAT_ENTR_HIS: TStringField;
    QryRelatorioCO_STAT_ENTR_FOT: TStringField;
    QryRelatorioCO_STAT_ENTR_CAR: TStringField;
    QryRelatorioCO_STAT_PAGA_INS: TStringField;
    QryRelatorioNU_DOCU_PAGA_INS: TStringField;
    QryRelatorioCO_STAT_PS_ENTR: TStringField;
    QryRelatorioCO_STAT_PS_DISS: TStringField;
    QryRelatorioCO_RESU_PS: TStringField;
    QryRelatorioDT_RESU_PS: TDateTimeField;
    QryRelatorioCO_RES_INS: TStringField;
    QryRelatorioVL_CURS_ORIG: TBCDField;
    QryRelatorioVL_CURS_CORR: TBCDField;
    QryRelatorioCO_SITU_INS: TStringField;
    QryRelatorioCO_SITU_MAT: TStringField;
    QryRelatorioVL_INSC_CUR: TBCDField;
    QryRelatorioDE_ANO_INSC: TStringField;
    QryRelatorioCO_CUR: TIntegerField;
    QryRelatorioDE_PAR_INSC: TStringField;
    QryRelatorioCO_RESE_MATR: TIntegerField;
    QryRelatorioCO_STAT_PS_TEST: TStringField;
    QryRelatorioCO_STAT_PS_TEST_NOTA: TBCDField;
    QryRelatorioCO_STAT_PS_DISS_NOTA: TBCDField;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioNU_CPF_ALU: TStringField;
    QryRelatorioCO_SEXO_ALU: TStringField;
    QryRelatorioDT_NASC_ALU: TDateTimeField;
    QryRelatorioDE_END_ALU: TStringField;
    QryRelatorioNU_END_ALU: TIntegerField;
    QryRelatorioDE_COMP_ALU: TStringField;
    QryRelatorioNU_TELE_RESI_ALU: TStringField;
    QryRelatorioCO_BAIRRO: TIntegerField;
    QryRelatorioCO_CIDADE: TIntegerField;
    QryRelatorioCO_ESTA_ALU: TStringField;
    QryRelatorioNU_PREMAT: TStringField;
    QryRelatorioCO_CEP_ALU: TStringField;
    QryRelatorioNU_TELE_CELU_ALU: TStringField;
    QryRelatorioCO_RESP: TIntegerField;
    QryRelatorioNU_MATRICULA: TIntegerField;
    QryRelatorioCO_MODU_CUR: TIntegerField;
    QryRelatorioNU_NIS: TBCDField;
    QryRelatoriode_modu_cur: TStringField;
    QRLNoAlu: TQRLabel;
    procedure detailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure SummaryBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelDemonstrativoInscricao: TFrmRelDemonstrativoInscricao;

implementation

uses U_DataModuleSGE, MaskUtils,DateUtils;

{$R *.dfm}

procedure TFrmRelDemonstrativoInscricao.detailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  diasnoano: Real;
  numero : double;
begin
  inherited;
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';
    
  with QryRelatorio do
  begin
    if FieldByName('CO_SITU_INS').AsString = 'E' then
      QRLStatusInsc.Caption := 'Em Aberto';
    if FieldByName('CO_SITU_INS').AsString = 'F' then
      QRLStatusInsc.Caption := 'Finalizada';
    if FieldByName('CO_SITU_INS').AsString = 'C' then
      QRLStatusInsc.Caption := 'Cancelada';

    numero := FieldByName('NU_INSC_ALU').AsFloat;
    QRLnumero.Caption := FormatFloat('000000',numero);
  end;

  // TELEFONE
  if not qryRelatorioNU_TELE_RESI_RESP.IsNull then
  begin
    QrlTel.Caption := FormatMaskText('(00) 0000-0000;0', qryRelatorioNU_TELE_RESI_RESP.AsString);
  end
  else
  begin
    QrlTel.Caption := ' - ';
  end;

  //IDADE
  if not qryRelatorio.FieldByName('DT_NASC_ALU').IsNull then
  begin
    diasnoano := 365.6;
    QRLIdade.Caption := FormatFloat('00;0',Trunc(DaysBetween(now,qryRelatorio.FieldByName('DT_NASC_ALU').AsDateTime) / diasnoano));
  end
  else
    QRLIdade.Caption := '-';

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);

  if Detail.Color = clWhite then
    Detail.Color := $00D8D8D8
  else
    Detail.Color := clWhite;
end;

procedure TFrmRelDemonstrativoInscricao.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

procedure TFrmRelDemonstrativoInscricao.SummaryBand1AfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

procedure TFrmRelDemonstrativoInscricao.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

end.
