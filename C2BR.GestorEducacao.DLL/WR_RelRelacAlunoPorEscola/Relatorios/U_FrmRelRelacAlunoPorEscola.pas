unit U_FrmRelRelacAlunoPorEscola;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRelacAlunoPorEscola = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QRShape2: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRBand2: TQRBand;
    QRLabel6: TQRLabel;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    QrlMatricula: TQRLabel;
    QRLParametros: TQRLabel;
    QRLNoAlu: TQRLabel;
    QRLDtNascAlu: TQRLabel;
    QRLabel7: TQRLabel;
    QRLDef: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel15: TQRLabel;
    QRDBText1: TQRDBText;
    QRDBText2: TQRDBText;
    QRDBText6: TQRDBText;
    QRLTotal: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelRelacAlunoPorEscola: TFrmRelRelacAlunoPorEscola;

implementation

uses U_DataModuleSGE, U_Funcoes, MaskUtils, DateUtils;

{$R *.dfm}

procedure TFrmRelRelacAlunoPorEscola.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
  var diasnoano : real;
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
  else
    QRLNoAlu.Caption := '-';

  diasnoano := 365.6;

  QrlMatricula.Caption := FormatMaskText('00.000.0000.###;0', QryRelatorio.FieldByName('CO_ALU_CAD').AsString);

  if (not QryRelatorio.FieldByName('DT_NASC_ALU').IsNull) and (QryRelatorio.FieldByName('DT_NASC_ALU').AsString <> '') then
    QRLDtNascAlu.Caption := FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('DT_NASC_ALU').AsDateTime) + '(' +
     IntToStr(Trunc(DaysBetween(now,QryRelatorio.FieldByName('DT_NASC_ALU').AsDateTime) / diasnoano)) + ')'
  else
    QRLDtNascAlu.Caption := '-';

  if QryRelatorio.FieldByName('TP_DEF').AsString = 'N' then
    QRLDef.Caption := 'Nenhuma'
  else if QryRelatorio.FieldByName('TP_DEF').AsString = 'A' then
    QRLDef.Caption := 'Auditiva'
  else if QryRelatorio.FieldByName('TP_DEF').AsString = 'V' then
    QRLDef.Caption := 'Visual'
  else if QryRelatorio.FieldByName('TP_DEF').AsString = 'F' then
    QRLDef.Caption := 'Física'
  else if QryRelatorio.FieldByName('TP_DEF').AsString = 'M' then
    QRLDef.Caption := 'Mental'
  else if QryRelatorio.FieldByName('TP_DEF').AsString = 'P' then
    QRLDef.Caption := 'Múltiplas'
  else
    QRLDef.Caption := 'Outras';

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);
end;

procedure TFrmRelRelacAlunoPorEscola.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelRelacAlunoPorEscola]);

end.
