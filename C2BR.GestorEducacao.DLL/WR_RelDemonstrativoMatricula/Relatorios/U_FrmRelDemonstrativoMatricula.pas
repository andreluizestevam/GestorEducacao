unit U_FrmRelDemonstrativoMatricula;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelDemonstrativoMatricula = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText11: TQRDBText;
    QRDBText13: TQRDBText;
    QRLStatus: TQRLabel;
    QRGroup1: TQRGroup;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel20: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel14: TQRLabel;
    QRShape10: TQRShape;
    QRLabel11: TQRLabel;
    QRLNumPage: TQRLabel;
    QRDBText7: TQRDBText;
    QRDBText8: TQRDBText;
    QRDBText9: TQRDBText;
    QRSysData4: TQRSysData;
    qrlParametros: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel17: TQRLabel;
    QRLTotAlunos: TQRLabel;
    QrlTel: TQRLabel;
    QRLabel8: TQRLabel;
    QRLIdade: TQRLabel;
    QRLNoAlu: TQRLabel;
    QRLCpfResp: TQRLabel;
    QRLMatric: TQRLabel;
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
  FrmRelDemonstrativoMatricula: TFrmRelDemonstrativoMatricula;

implementation

uses U_DataModuleSGE, MaskUtils, DateUtils;

{$R *.dfm}

procedure TFrmRelDemonstrativoMatricula.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  diasnoano: Real;
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
  else
    QRLNoAlu.Caption := '-';
    
  with QryRelatorio do
  begin
    if FieldByName('co_sit_mat').AsString = 'A' then
    QRLStatus.Caption := 'Ativa';
    if FieldByName('co_sit_mat').AsString = 'C' then
    QRLStatus.Caption := 'Cancelada';
    if FieldByName('co_sit_mat').AsString = 'T' then
    QRLStatus.Caption := 'Trancada';
    if FieldByName('co_sit_mat').AsString = 'F' then
    QRLStatus.Caption := 'Finalizado';
    if FieldByName('co_sit_mat').AsString = 'P' then
    QRLStatus.Caption := 'Pendente';
  end;

  QRLTotAlunos.Caption := IntToStr(StrToInt(QRLTotAlunos.Caption) + 1);

  // TELEFONE
  if not QryRelatorio.FieldByName('NU_TELE_CELU_RESP').IsNull then
  begin
    QrlTel.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorio.FieldByName('NU_TELE_CELU_RESP').AsString);
  end
  else
  begin
    QrlTel.Caption := ' - ';
  end;

  // CPF - Responsável
  if not QryRelatorio.FieldByName('NU_CPF_RESP').IsNull then
  begin
    QRLCpfResp.Caption := FormatMaskText('000.000.000-00;0', QryRelatorio.FieldByName('NU_CPF_RESP').AsString);
  end
  else
  begin
    QRLCpfResp.Caption := ' - ';
  end;

  // CPF - Responsável
  if not QryRelatorio.FieldByName('CO_ALU_CAD').IsNull then
  begin
    QRLMatric.Caption := FormatMaskText('00.000.000000;0', QryRelatorio.FieldByName('CO_ALU_CAD').AsString);
  end
  else
  begin
    QRLMatric.Caption := ' - ';
  end;

  //IDADE
  if not QryRelatorio.FieldByName('DT_NASC_ALU').IsNull then
  begin
    diasnoano := 365.6;
    QRLIdade.Caption := FormatFloat('00;0',Trunc(DaysBetween(now,QryRelatorio.FieldByName('DT_NASC_ALU').AsDateTime) / diasnoano));
  end
  else
    QRLIdade.Caption := '-';

  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00D8D8D8
  else
    DetailBand1.Color := clWhite;
end;

procedure TFrmRelDemonstrativoMatricula.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotAlunos.Caption := '0';
end;

end.
