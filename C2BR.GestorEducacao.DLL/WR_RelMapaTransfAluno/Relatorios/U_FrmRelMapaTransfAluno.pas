unit U_FrmRelMapaTransfAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaTransfAluno = class(TFrmRelTemplate)
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    Detail: TQRBand;
    QRLabel7: TQRLabel;
    QRLPage: TQRLabel;
    QrlTotParc: TQRLabel;
    QRLSiglaUnid: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel14: TQRLabel;
    QRLTotal: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel5: TQRLabel;
    QRLUnidade: TQRLabel;
    QRLQTE: TQRLabel;
    QRLParam: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel6: TQRLabel;
    QRLQTI: TQRLabel;
    QRLQTT: TQRLabel;
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
  FrmRelMapaTransfAluno: TFrmRelMapaTransfAluno;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelMapaTransfAluno.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_FANTAS_EMP').IsNull then
    QRLUnidade.Caption := UpperCase(QryRelatorio.FieldByName('NO_FANTAS_EMP').AsString)
  else
    QRLUnidade.Caption := '-';

  if not QryRelatorio.FieldByName('sigla').IsNull then
    QRLSiglaUnid.Caption := UpperCase(QryRelatorio.FieldByName('sigla').AsString)
  else
    QRLSiglaUnid.Caption := '-';

  if not QryRelatorio.FieldByName('transEnTur').IsNull then
    QRLQTT.Caption := QryRelatorio.FieldByName('transEnTur').AsString
  else
    QRLQTT.Caption := '-';

  if not QryRelatorio.FieldByName('transEnUni').IsNull then
    QRLQTI.Caption := QryRelatorio.FieldByName('transEnUni').AsString
  else
    QRLQTI.Caption := '-';

  if not QryRelatorio.FieldByName('transExt').IsNull then
    QRLQTE.Caption := QryRelatorio.FieldByName('transExt').AsString
  else
    QRLQTE.Caption := '-';

  QrlTotParc.Caption := IntToStr(QryRelatorio.FieldByName('transEnTur').AsInteger + QryRelatorio.FieldByName('transEnUni').AsInteger
  + QryRelatorio.FieldByName('transExt').AsInteger);

  if Detail.Color = clWhite then
    Detail.Color := $00F2F2F2
  else
    Detail.Color := clWhite;

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + StrToInt(QrlTotParc.Caption));
end;

procedure TFrmRelMapaTransfAluno.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

end.
