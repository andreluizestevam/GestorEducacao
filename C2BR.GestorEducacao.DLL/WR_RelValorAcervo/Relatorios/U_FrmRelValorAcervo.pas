unit U_FrmRelValorAcervo;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, QRCtrls, DB, ADODB, QuickRpt, ExtCtrls;

type
  TFrmRelValorAcervo = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QRDBText1: TQRDBText;
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRShape2: TQRShape;
    QRLabel4: TQRLabel;
    QRBand2: TQRBand;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRDBText7: TQRDBText;
    QRDBText8: TQRDBText;
    QRDBText9: TQRDBText;
    QryRelatorioNome: TStringField;
    QryRelatoriono_editora: TStringField;
    QryRelatoriono_clas_acer: TStringField;
    QryRelatoriono_areacon: TStringField;
    QRLTotal: TQRLabel;
    QRShape3: TQRShape;
    QryRelatorioCO_ISBN_ACER: TBCDField;
    QRLISBN: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    Total : Real;
  end;

var
  FrmRelValorAcervo: TFrmRelValorAcervo;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelValorAcervo.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('CO_ISBN_ACER').IsNull then
    QRLISBN.Caption := FormatMaskText('000-00-0000-000-0;0',FormatFloat('0000000000000;1',QryRelatorio.FieldByName('CO_ISBN_ACER').AsFloat))
  else
    QRLISBN.Caption := '-';
        {
  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;     }

 // if not QryRelatorio.FieldByName('ValordoAcervo').IsNull then
   // QRLTotal.Caption := FloatToStr(StrToFloat(QRLTotal.Caption) + QryRelatorio.FieldByName('ValordoAcervo').AsFloat)
end;

procedure TFrmRelValorAcervo.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

procedure TFrmRelValorAcervo.QRBand2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTotal.Caption := FloatToStrF(StrToFloat(QRLTotal.Caption),ffNumber,15,2);
end;

end.
