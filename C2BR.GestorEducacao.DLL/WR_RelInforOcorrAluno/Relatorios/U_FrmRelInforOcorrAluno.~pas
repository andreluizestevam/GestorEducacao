unit U_FrmRelInforOcorrAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelInforOcorrAluno = class(TFrmRelTemplate)
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    Detail: TQRBand;
    QRLabel7: TQRLabel;
    QRLPage: TQRLabel;
    QrlOcorr: TQRLabel;
    QRLDtOcorr: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel14: TQRLabel;
    QRLTotal: TQRLabel;
    QRLabel3: TQRLabel;
    QRLSiglaUnid: TQRLabel;
    QRLabel5: TQRLabel;
    QRLTpOcorr: TQRLabel;
    QRLParam: TQRLabel;
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
  FrmRelInforOcorrAluno: TFrmRelInforOcorrAluno;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelInforOcorrAluno.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('DT_OCORR').IsNull then
    QRLDtOcorr.Caption := FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('DT_OCORR').AsDateTime)
  else
    QRLDtOcorr.Caption := '-';

  if not QryRelatorio.FieldByName('sigla').IsNull then
    QRLSiglaUnid.Caption := UpperCase(QryRelatorio.FieldByName('sigla').AsString)
  else
    QRLSiglaUnid.Caption := '-';

  if not QryRelatorio.FieldByName('DE_TIPO_OCORR').IsNull then
    QRLTpOcorr.Caption := QryRelatorio.FieldByName('DE_TIPO_OCORR').AsString
  else
    QRLTpOcorr.Caption := '-';

  if not QryRelatorio.FieldByName('DE_OCORR').IsNull then
    QrlOcorr.Caption := QryRelatorio.FieldByName('DE_OCORR').AsString
  else
    QrlOcorr.Caption := '-';

  if Detail.Color = clWhite then
    Detail.Color := $00F2F2F2
  else
    Detail.Color := clWhite;

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);
end;

procedure TFrmRelInforOcorrAluno.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

end.
