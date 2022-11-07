unit U_FrmRelRelacPlanoContas;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRelacPlanoContas = class(TFrmRelTemplate)
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    DetailBand1: TQRBand;
    QRBand1: TQRBand;
    QRLabel1: TQRLabel;
    QRLTotal: TQRLabel;
    QRDBText1: TQRDBText;
    QRGroup1: TQRGroup;
    QRDBText3: TQRDBText;
    QRDBText6: TQRDBText;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRBand2: TQRBand;
    QRLabel10: TQRLabel;
    QRShape1: TQRShape;
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
  FrmRelRelacPlanoContas: TFrmRelRelacPlanoContas;

implementation

uses U_DataModuleSGE, MaskUtils, DateUtils;

{$R *.dfm}

procedure TFrmRelRelacPlanoContas.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

procedure TFrmRelRelacPlanoContas.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00F2F2F2
  else
    DetailBand1.Color := clWhite;

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);
end;

end.
