unit U_FrmRelMapaPerfiDesempAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaPerfiDesempAluno = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRGroup1: TQRGroup;
    QRLabel22: TQRLabel;
    QRShape1: TQRShape;
    QRLabel19: TQRLabel;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel10: TQRLabel;
    SummaryBand1: TQRBand;
    QRLParam: TQRLabel;
    QRDBText1: TQRDBText;
    QRDBText2: TQRDBText;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRDBText4: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText6: TQRDBText;
    QRLB1: TQRLabel;
    QRLB2: TQRLabel;
    QRLB3: TQRLabel;
    QRLB4: TQRLabel;
    QRLMedia: TQRLabel;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelMapaPerfiDesempAluno: TFrmRelMapaPerfiDesempAluno;

implementation

Uses U_DataModuleSGE,U_Funcoes, DateUtils, MaskUtils;

{$R *.dfm}

procedure TFrmRelMapaPerfiDesempAluno.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;

  if not QryRelatorio.FieldByName('NR_MEDIA_BIM1_DESEMP').IsNull then
    QRLB1.Caption := FloatToStrF(QryRelatorio.FieldByName('NR_MEDIA_BIM1_DESEMP').AsFloat, ffNumber, 10,2)
  else
    QRLB1.Caption := '-';

  if not QryRelatorio.FieldByName('NR_MEDIA_BIM2_DESEMP').IsNull then
    QRLB2.Caption := FloatToStrF(QryRelatorio.FieldByName('NR_MEDIA_BIM2_DESEMP').AsFloat, ffNumber, 10,2)
  else
    QRLB2.Caption := '-';

  if not QryRelatorio.FieldByName('NR_MEDIA_BIM3_DESEMP').IsNull then
    QRLB3.Caption := FloatToStrF(QryRelatorio.FieldByName('NR_MEDIA_BIM3_DESEMP').AsFloat, ffNumber, 10,2)
  else
    QRLB3.Caption := '-';

  if not QryRelatorio.FieldByName('NR_MEDIA_BIM4_DESEMP').IsNull then
    QRLB4.Caption := FloatToStrF(QryRelatorio.FieldByName('NR_MEDIA_BIM4_DESEMP').AsFloat, ffNumber, 10,2)
  else
    QRLB4.Caption := '-';

  if not QryRelatorio.FieldByName('media').IsNull then
    QRLMedia.Caption := FloatToStrF(QryRelatorio.FieldByName('media').AsFloat, ffNumber, 10,2)
  else
    QRLMedia.Caption := '-';

  if Detail.Color = clWhite then
    Detail.Color := $00D8D8D8
  else
    Detail.Color := clWhite;
end;

end.
