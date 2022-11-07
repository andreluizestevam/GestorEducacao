unit U_FrmRelRelacaoAcervoObras;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRelacaoAcervoObras = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QrlParametros: TQRLabel;
    QRLabel5: TQRLabel;
    QRShape6: TQRShape;
    QRLabel3: TQRLabel;
    QRLabel7: TQRLabel;
    QRDBText2: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    SummaryBand1: TQRBand;
    QRLabel2: TQRLabel;
    qrlTotal: TQRLabel;
    QRLISBN: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel9: TQRLabel;
    QRDBText8: TQRDBText;
    QRLQtdItens: TQRLabel;
    QRLabel1: TQRLabel;
    QRLTotalObras: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure SummaryBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelRelacaoAcervoObras: TFrmRelRelacaoAcervoObras;

implementation

uses MaskUtils;

{$R *.dfm}

procedure TFrmRelRelacaoAcervoObras.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  SQLString : String;
begin
  inherited;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQLString := 'select count(CO_ISBN_ACER) as totItens from TB204_ACERVO_ITENS ' +
                'where CO_ISBN_ACER = ' + QryRelatorio.FieldByName('CO_ISBN_ACER').AsString;
                
    if not QryRelatorio.FieldByName('CO_EMP').IsNull then
      SQLString := SQLString + ' and CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString;

    SQL.Text := SQLString;
    Open;

    if not IsEmpty then
    begin
      QRLQtdItens.Caption := FieldByName('totItens').AsString;
      qrlTotal.Caption := IntToStr(StrToInt(qrlTotal.Caption) + FieldByName('totItens').AsInteger);
    end
    else
      QRLQtdItens.Caption := '0';
  end;

  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00D8D8D8
  else
    DetailBand1.Color := clWhite;

  if not QryRelatorio.FieldByName('CO_ISBN_ACER').IsNull then
    QRLISBN.Caption := FormatMaskText('000-00-0000-000-0;0',FormatFloat('0000000000000',QryRelatorio.FieldByName('CO_ISBN_ACER').AsFloat))
  else
    QRLISBN.Caption := '-';

  QRLTotalObras.Caption := IntToStr(StrToInt(QRLTotalObras.Caption) + 1);
end;

procedure TFrmRelRelacaoAcervoObras.SummaryBand1AfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;
  qrlTotal.Caption := '0';
  QRLTotalObras.Caption := '0';
end;

procedure TFrmRelRelacaoAcervoObras.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  qrlTotal.Caption := '0';
  QRLTotalObras.Caption := '0';
end;

end.
