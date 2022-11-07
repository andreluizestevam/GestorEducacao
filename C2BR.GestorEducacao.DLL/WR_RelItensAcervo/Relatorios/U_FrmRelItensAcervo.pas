unit U_FrmRelItensAcervo;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelItensAcervo = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QrlParametros: TQRLabel;
    QRLabel5: TQRLabel;
    QRShape6: TQRShape;
    QRLabel3: TQRLabel;
    QRLabel7: TQRLabel;
    QRGroup1: TQRGroup;
    QRLabel1: TQRLabel;
    QRDBText2: TQRDBText;
    QRDBText1: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    SummaryBand1: TQRBand;
    QRLabel2: TQRLabel;
    qrlTotal: TQRLabel;
    QRLISBN: TQRLabel;
    QRLabel6: TQRLabel;
    QRDBText3: TQRDBText;
    QRLabel4: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRDBText6: TQRDBText;
    QRDBText7: TQRDBText;
    QRDBText8: TQRDBText;
    QRDBText9: TQRDBText;
    QRLNoAreCon: TQRLabel;
    QRBand1: TQRBand;
    QRLabel10: TQRLabel;
    QRLTotParc: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    totParc : integer;
  public
    { Public declarations }
  end;

var
  FrmRelItensAcervo: TFrmRelItensAcervo;

implementation

uses MaskUtils;

{$R *.dfm}

procedure TFrmRelItensAcervo.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  qrlTotal.Caption := IntToStr(StrToInt(qrlTotal.Caption) + 1);
  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00D8D8D8
  else
    DetailBand1.Color := clWhite;

  if not QryRelatorio.FieldByName('CO_ISBN_ACER').IsNull then
    QRLISBN.Caption := FormatMaskText('000-00-0000-000-0;0',FormatFloat('0000000000000',QryRelatorio.FieldByName('CO_ISBN_ACER').AsFloat))
  else
    QRLISBN.Caption := '-';

  totParc := totParc + 1;
    
end;

procedure TFrmRelItensAcervo.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  qrlTotal.Caption := '0';
  totParc := 0;
end;

procedure TFrmRelItensAcervo.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLNoAreCon.Caption := UpperCase(QryRelatorio.FieldByName('NO_AREACON').AsString) + ' / ' + UpperCase(QryRelatorio.FieldByName('NO_CLAS_ACER').AsString);
end;

procedure TFrmRelItensAcervo.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTotParc.Caption := IntToStr(totParc);

  totParc := 0;
end;

end.
