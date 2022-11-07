unit U_FrmRelRelacCEPs;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRelacCEPs = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRGroup1: TQRGroup;
    QRLabel22: TQRLabel;
    QRShape1: TQRShape;
    QRLabel19: TQRLabel;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel1: TQRLabel;
    QRLLongitude: TQRLabel;
    QRLabel10: TQRLabel;
    QRLCEP: TQRLabel;
    SummaryBand1: TQRBand;
    QRLCidade: TQRLabel;
    QRLParam: TQRLabel;
    QRLLatitude: TQRLabel;
    QRDBText1: TQRDBText;
    QRDBText2: TQRDBText;
    QRLabel2: TQRLabel;
    QRDBText3: TQRDBText;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelRelacCEPs: TFrmRelRelacCEPs;

implementation

Uses U_DataModuleSGE,U_Funcoes, DateUtils, MaskUtils;

{$R *.dfm}

procedure TFrmRelRelacCEPs.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  valor : String;
begin
  inherited;

  if not QryRelatorio.FieldByName('CO_CEP').IsNull then
    QRLCEP.Caption := FormatMaskText('99999-999;0', QryRelatorio.FieldByName('CO_CEP').AsString)
  else
    QRLCEP.Caption := '-';

  if not QryRelatorio.FieldByName('NR_LATIT_CEP').IsNull then
  begin
    valor := QryRelatorio.FieldByName('NR_LATIT_CEP').AsString;
    if not QryRelatorio.FieldByName('TP_LATIT_CEP').IsNull then
      QRLLatitude.Caption := StringReplace(valor,',','.',[rfReplaceAll]) + '/' + QryRelatorio.FieldByName('TP_LATIT_CEP').AsString
    else
      QRLLatitude.Caption := StringReplace(valor,',','.',[rfReplaceAll]);
  end
  else
    QRLLatitude.Caption := '-';

  if not QryRelatorio.FieldByName('NR_LONGI_CEP').IsNull then
  begin
    valor := QryRelatorio.FieldByName('NR_LONGI_CEP').AsString;
    if not QryRelatorio.FieldByName('TP_LONGI_CEP').IsNull then
      QRLLongitude.Caption := StringReplace(valor,',','.',[rfReplaceAll]) + '/' + QryRelatorio.FieldByName('TP_LONGI_CEP').AsString
    else
      QRLLongitude.Caption := StringReplace(valor,',','.',[rfReplaceAll]);
  end
  else
    QRLLongitude.Caption := '-';

  if Detail.Color = clWhite then
    Detail.Color := $00D8D8D8
  else
    Detail.Color := clWhite;
end;

procedure TFrmRelRelacCEPs.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLCidade.Caption := 'Cidade/UF: ' + UpperCase(QryRelatorio.FieldByName('no_cidade').AsString) + ' / ' + QryRelatorio.FieldByName('CO_UF').AsString;
end;

end.
