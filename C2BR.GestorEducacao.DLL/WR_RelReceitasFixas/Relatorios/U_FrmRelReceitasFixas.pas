unit U_FrmRelReceitasFixas;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelDespesasFixas, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelReceitasFixas = class(TFrmRelDespesasFixas)
    QRLPage: TQRLabel;
    QRLDtContrato: TQRLabel;
    QRDBText13: TQRDBText;
    QRLabel14: TQRLabel;
    QRDBText3: TQRDBText;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
    totalReceitasFixas : Double;
  public
    { Public declarations }
  end;

var
  FrmRelReceitasFixas: TFrmRelReceitasFixas;

implementation

uses MaskUtils;

{$R *.dfm}

procedure TFrmRelReceitasFixas.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;

  if QryRelatorio.FieldByName('TP_CLIENTE').AsString = 'J' then
    QRLCPFCNPJ.Caption := FormatMaskText('99.999.999/9999-99;0',QryRelatorio.FieldByName('CO_CPFCGC_CLI').AsString)
  else
    QRLCPFCNPJ.Caption := FormatMaskText('000.000.000-00;0',QryRelatorio.FieldByName('CO_CPFCGC_CLI').AsString);

  if not(QryRelatorio.FieldByName('DT_INI_CON_RECDES').IsNull) and not(QryRelatorio.FieldByName('DT_FIM_CON_RECDES').IsNull) then
  begin
    QRLDtContrato.Caption := FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('DT_INI_CON_RECDES').AsDateTime) + ' - ' + FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('DT_FIM_CON_RECDES').AsDateTime);
  end
  else
    QRLDtContrato.Caption := '-';

  if not QryRelatorio.FieldByName('VR_RECDES').IsNull then
    totalReceitasFixas := totalReceitasFixas + QryRelatorio.FieldByName('VR_RECDES').AsFloat;

end;

procedure TFrmRelReceitasFixas.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  //QRLDescTotal.Caption := IntToStr(QryRelatorio.RecordCount) + ' Receitas Fixas    - R$ ' +
  FloatToStrF(totalReceitasFixas,ffNumber,15,2);
end;

procedure TFrmRelReceitasFixas.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  totalReceitasFixas := 0;
end;

end.
