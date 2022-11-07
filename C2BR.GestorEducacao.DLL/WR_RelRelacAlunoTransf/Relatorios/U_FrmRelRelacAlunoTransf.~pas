unit U_FrmRelRelacAlunoTransf;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRelacAlunoTransf = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QRShape2: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRBand2: TQRBand;
    QRLabel6: TQRLabel;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRDBText3: TQRDBText;
    QrlMatricula: TQRLabel;
    QRLParametros: TQRLabel;
    QRLNoAlu: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel15: TQRLabel;
    QRDBText1: TQRDBText;
    QRDBText2: TQRDBText;
    QRDBText6: TQRDBText;
    QRLTotal: TQRLabel;
    QRLabel3: TQRLabel;
    QRLDtTransf: TQRLabel;
    QRLabel7: TQRLabel;
    QRLMotivo: TQRLabel;
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
  FrmRelRelacAlunoTransf: TFrmRelRelacAlunoTransf;

implementation

uses U_DataModuleSGE, U_Funcoes, MaskUtils, DateUtils;

{$R *.dfm}

procedure TFrmRelRelacAlunoTransf.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
  var diasnoano : real;
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
  else
    QRLNoAlu.Caption := '-';

  QrlMatricula.Caption := FormatMaskText('00.000.0000.###;0', QryRelatorio.FieldByName('CO_ALU_CAD').AsString);

  if (not QryRelatorio.FieldByName('DT_TRANSF').IsNull) then
    QRLDtTransf.Caption := FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('DT_TRANSF').AsDateTime)
  else
    QRLDtTransf.Caption := '-';

  if not QryRelatorio.FieldByName('MOTIVO').IsNull then
    QRLMotivo.Caption := QryRelatorio.FieldByName('MOTIVO').AsString
  else
    QRLMotivo.Caption := '-';

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);
end;

procedure TFrmRelRelacAlunoTransf.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelRelacAlunoTransf]);

end.
