unit U_FrmRelCanhoto;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelCanhoto = class(TFrmRelTemplate)
    QrlParametro: TQRLabel;
    QRBand1: TQRBand;
    QRBand2: TQRBand;
    QRShape2: TQRShape;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel6: TQRLabel;
    QryRelatorioCO_ALU_CAD: TStringField;
    QryRelatorioNO_ALU: TStringField;
    QRExpr1: TQRExpr;
    QryRelatorioCO_SITU_MTR: TStringField;
    QryRelatorioSITUACAO: TStringField;
    QRLabel11: TQRLabel;
    QRLPage: TQRLabel;
    QrlMatricula: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel10: TQRLabel;
    QRLabel9: TQRLabel;
    QRShape13: TQRShape;
    QRLabel1: TQRLabel;
    QRLNoAlu: TQRLabel;
    procedure QRDBText3Print(sender: TObject; var Value: String);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelCanhoto: TFrmRelCanhoto;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelCanhoto.QRDBText3Print(sender: TObject;var Value: String);
begin
  inherited;
  value := '--------' + Value + '--------'
end;

procedure TFrmRelCanhoto.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';

  QrlMatricula.Caption := FormatMaskText('00.000.000000;0', QryRelatorioCO_ALU_CAD.AsString) + ' ( ***' + QryRelatorioSITUACAO.AsString + ' ***)';

  if QRBand1.Color = clWhite then
     QRBand1.Color := $00D8D8D8
  else
     QRBand1.Color := clWhite;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelCanhoto]);

end.
