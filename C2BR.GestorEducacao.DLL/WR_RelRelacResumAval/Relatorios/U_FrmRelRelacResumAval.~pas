unit U_FrmRelRelacResumAval;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRelacResumAval = class(TFrmRelTemplate)
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    Detail: TQRBand;
    QRLabel7: TQRLabel;
    QRLPage: TQRLabel;
    QrlNoAval: TQRLabel;
    QRLSigalAval: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel14: TQRLabel;
    QRLTotal: TQRLabel;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelRelacResumAval: TFrmRelRelacResumAval;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelRelacResumAval.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_TP_AVAL_INST').IsNull then
    QrlNoAval.Caption := UpperCase(QryRelatorio.FieldByName('NO_TP_AVAL_INST').AsString)
  else
    QrlNoAval.Caption := '-';

  if not QryRelatorio.FieldByName('CO_SIGLA_TP_AVAL_INST').IsNull then
    QRLSigalAval.Caption := UpperCase(QryRelatorio.FieldByName('CO_SIGLA_TP_AVAL_INST').AsString)
  else
    QRLSigalAval.Caption := '-';

  if Detail.Color = clWhite then
    Detail.Color := $00F2F2F2
  else
    Detail.Color := clWhite;

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);
end;

procedure TFrmRelRelacResumAval.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

end.
