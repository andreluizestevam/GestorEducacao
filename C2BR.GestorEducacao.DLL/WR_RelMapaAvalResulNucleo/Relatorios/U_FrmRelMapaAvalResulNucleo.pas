unit U_FrmRelMapaAvalResulNucleo;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaAvalResulNucleo = class(TFrmRelTemplate)
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    Detail: TQRBand;
    QRLabel7: TQRLabel;
    QRLPage: TQRLabel;
    QrlMedia: TQRLabel;
    QRLSiglaUnid: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel14: TQRLabel;
    QRLTotal: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel5: TQRLabel;
    QRLUnidade: TQRLabel;
    QRLQtdAval: TQRLabel;
    QRLParam: TQRLabel;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelMapaAvalResulNucleo: TFrmRelMapaAvalResulNucleo;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelMapaAvalResulNucleo.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  numRan : integer;
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

  if not QryRelatorio.FieldByName('qtd').IsNull then
    QRLQtdAval.Caption := QryRelatorio.FieldByName('qtd').AsString
  else
    QRLQtdAval.Caption := '-';

  if QryRelatorio.FieldByName('qtd').AsInteger > 0 then
  begin
    numRan := Round(random(10));
    QrlMedia.Caption := FloatToStrF(numRan, ffNumber, 10, 1);
    while numRan < 5 do
    begin
      numRan := Round(random(10));
      QrlMedia.Caption := FloatToStrF(numRan, ffNumber, 10, 1);
      Next;
    end;
  end
  else
    QrlMedia.Caption := '-';

  if Detail.Color = clWhite then
    Detail.Color := $00F2F2F2
  else
    Detail.Color := clWhite;

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);
end;

procedure TFrmRelMapaAvalResulNucleo.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

procedure TFrmRelMapaAvalResulNucleo.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QrlParam.Caption := 'Núcleo: ' + QryRelatorio.FieldByName('DE_NUCLEO').AsString;
end;

end.
