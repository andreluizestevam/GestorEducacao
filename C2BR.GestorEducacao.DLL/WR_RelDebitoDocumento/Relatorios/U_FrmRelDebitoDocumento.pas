unit U_FrmRelDebitoDocumento;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelDebitoDocumento = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRGroup2: TQRGroup;
    QRLabel2: TQRLabel;
    QRLParametros: TQRLabel;
    QRShape6: TQRShape;
    QRBand1: TQRBand;
    QRDBText4: TQRDBText;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRLMatricula: TQRLabel;
    QRLSerTurNIS: TQRLabel;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioDE_TP_DOC_MAT: TStringField;
    QryRelatorioNO_CUR: TStringField;
    QryRelatorioNO_TUR: TStringField;
    QryRelatorioCO_ALU_CAD: TStringField;
    QryRelatorioCO_ALU: TAutoIncField;
    QryRelatorioNU_NIS: TBCDField;
    QRLNoAlu: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelDebitoDocumento: TFrmRelDebitoDocumento;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelDebitoDocumento.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;
end;

procedure TFrmRelDebitoDocumento.QRGroup2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';

  QrlMatricula.Caption := '';
  QRLSerTurNIS.Caption := '';

  QRLMatricula.Caption := FormatMaskText('00.000.000000;0',QryRelatorio.FieldByName('co_alu_cad').AsString);
  if not QryRelatorio.FieldByName('nu_nis').IsNull then
    QRLSerTurNIS.Caption := '('+ QryRelatorio.FieldByName('no_cur').AsString + '/' +QryRelatorio.FieldByName('no_tur').AsString+
      ' - Nº NIS '+QryRelatorio.FieldByName('nu_nis').AsString+')'
  else
    QRLSerTurNIS.Caption := '('+ QryRelatorio.FieldByName('no_cur').AsString + '/' +QryRelatorio.FieldByName('no_tur').AsString+')';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelDebitoDocumento]);

end.
