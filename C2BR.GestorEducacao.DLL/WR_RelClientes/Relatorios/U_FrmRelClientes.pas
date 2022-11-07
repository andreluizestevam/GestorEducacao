unit U_FrmRelClientes;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelClientes = class(TFrmRelTemplate)
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    Detail: TQRBand;
    QRLabel7: TQRLabel;
    QRLPage: TQRLabel;
    QrlTel: TQRLabel;
    QrlFax: TQRLabel;
    QRLCPFCNPJ: TQRLabel;
    QRLEndereco: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel14: TQRLabel;
    QRLTotal: TQRLabel;
    QRLNoCli: TQRLabel;
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
  FrmRelClientes: TFrmRelClientes;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelClientes.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_FAN_CLI').IsNull then
    QRLNoCli.Caption := UpperCase(QryRelatorio.FieldByName('NO_FAN_CLI').AsString)
  else
    QRLNoCli.Caption := '-';

  if QryRelatorio.FieldByName('TP_CLIENTE').AsString = 'J' then
  begin
    QRLCPFCNPJ.Caption := FormatMaskText('99.999.999/9999-99;0;_',QryRelatorio.FieldByName('CO_CPFCGC_CLI').AsString);
  end;

  if QryRelatorio.FieldByName('TP_CLIENTE').AsString = 'F' then
  begin
    QRLCPFCNPJ.Caption := FormatMaskText('999.999.999-99;0;_',QryRelatorio.FieldByName('CO_CPFCGC_CLI').AsString);
  end;

    QRLEndereco.Caption := '';

  {if not QryRelatorioDE_END_CLI.IsNull then
    QRLEndereco.Caption := QryRelatorioDE_END_CLI.AsString + ' - ';

  if not QryRelatorioDE_COM_CLI.IsNull then
    QRLEndereco.Caption := QRLEndereco.Caption + QryRelatorioDE_COM_CLI.AsString + ' - ';}

  if not QryRelatorio.FieldByName('CO_UF_CLI').IsNull then
  begin
    QRLEndereco.Caption := QRLEndereco.Caption + QryRelatorio.FieldByName('CO_UF_CLI').AsString + ' - ';
  end;

  if not QryRelatorio.FieldByName('NO_CIDADE').IsNull then
  begin
    QRLEndereco.Caption := QRLEndereco.Caption + QryRelatorio.FieldByName('NO_CIDADE').AsString + ' - ';
  end;

  if not QryRelatorio.FieldByName('NO_BAIRRO').IsNull then
  begin
    QRLEndereco.Caption := QRLEndereco.Caption + QryRelatorio.FieldByName('NO_BAIRRO').AsString;
  end;

  {if not QryRelatorioCO_CEP_CLI.IsNull then
    QRLEndereco.Caption := QRLEndereco.Caption + FormatMaskText('99999-999;0',QryRelatorioCO_CEP_CLI.AsString);}

  if Detail.Color = clWhite then
    Detail.Color := $00F2F2F2
  else
    Detail.Color := clWhite;

  // TELEFONE
  if not QryRelatorio.FieldByName('CO_TEL1_CLI').IsNull then
  begin
    QrlTel.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorio.FieldByName('CO_TEL1_CLI').AsString);
  end
  else
  begin
    QrlTel.Caption := ' - ';
  end;

  // FAX
  if not QryRelatorio.FieldByName('CO_FAX_CLI').IsNull then
  begin
    QrlFax.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorio.FieldByName('CO_FAX_CLI').AsString);
  end
  else
  begin
    QrlFax.Caption := ' - ';
  end;

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);
end;

procedure TFrmRelClientes.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

end.
