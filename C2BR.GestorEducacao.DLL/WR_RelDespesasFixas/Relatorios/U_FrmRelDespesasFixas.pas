unit U_FrmRelDespesasFixas;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelDespesasFixas = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRShape1: TQRShape;
    QRLParametros: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel13: TQRLabel;
    QRLTotal: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel18: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel3: TQRLabel;
    QRDBText11: TQRDBText;
    QRLCPFCNPJ: TQRLabel;
    QRDBText7: TQRDBText;
    QRDBText5: TQRDBText;
    QRLDtContrato: TQRLabel;
    QRDBText1: TQRDBText;
    QRDBText2: TQRDBText;
    QRDBText6: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText12: TQRDBText;
    QRDBText13: TQRDBText;
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
  FrmRelDespesasFixas: TFrmRelDespesasFixas;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelDespesasFixas.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if Detail.Color = clWhite then
    Detail.Color := $00F2F2F2
  else
    Detail.Color := clWhite;

  QRLCPFCNPJ.Caption := FormatMaskText('99.999.999/9999-99;0',QryRelatorio.FieldByName('CO_CPFCGC_FORN').AsString);


  if not(QryRelatorio.FieldByName('DT_INI_CON_RECDES').IsNull) and not(QryRelatorio.FieldByName('DT_FIM_CON_RECDES').IsNull) then
  begin
    QRLDtContrato.Caption := FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('DT_INI_CON_RECDES').AsDateTime) + ' - ' + FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('DT_FIM_CON_RECDES').AsDateTime);
  end
  else
    QRLDtContrato.Caption := '-';

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);
end;

procedure TFrmRelDespesasFixas.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

end.
