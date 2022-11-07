unit U_FrmRelRelacTipoPessoa;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRelacTipoPessoa = class(TFrmRelTemplate)
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QRShape1: TQRShape;
    QRLabel10: TQRLabel;
    DetailBand1: TQRBand;
    QRBand1: TQRBand;
    QRLabel1: TQRLabel;
    QRLTotal: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel6: TQRLabel;
    QRDBText1: TQRDBText;
    QRDBText5: TQRDBText;
    QRLParam: TQRLabel;
    QRLSitu: TQRLabel;
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelRelacTipoPessoa: TFrmRelRelacTipoPessoa;

implementation

uses U_DataModuleSGE, MaskUtils, DateUtils;

{$R *.dfm}

procedure TFrmRelRelacTipoPessoa.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

procedure TFrmRelRelacTipoPessoa.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if QryRelatorio.FieldByName('CO_SITUACAO').AsString = 'A' then
    QRLSitu.Caption := 'Ativo - ' + FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_SITUACAO').AsDateTime)
  else
    QRLSitu.Caption := 'Inativo - ' + FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_SITUACAO').AsDateTime);

  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00F2F2F2
  else
    DetailBand1.Color := clWhite;

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);
end;

end.
